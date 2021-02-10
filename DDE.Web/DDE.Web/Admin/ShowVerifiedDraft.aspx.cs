using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class ShowVerifiedDraft : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 67))
            {
                populateVefiedDrafts();

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private void populateVefiedDrafts()
        {
            int i = 1;
            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("Type");
            DataColumn dtcol3 = new DataColumn("DCNumber");
            DataColumn dtcol4 = new DataColumn("DCDate");
            DataColumn dtcol5 = new DataColumn("IBN");
            DataColumn dtcol6 = new DataColumn("DCAmount");
            DataColumn dtcol7 = new DataColumn("VOn");
            DataColumn dtcol8 = new DataColumn("VBy");
          

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
       

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("select distinct DCNumber from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where Verified='True'", con1);

                SqlDataReader dr1;

                con1.Open();
                dr1 = cmd1.ExecuteReader();

               
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i;
                        populateDCDetails(dr1["DCNumber"].ToString(),drow);
                       
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }

                }
                con1.Close();
            }
            con.Close();




            dtlistShowDrafts.DataSource = dt;
            dtlistShowDrafts.DataBind();

            con.Close();

            if (i > 1)
            {
                dtlistShowDrafts.Visible = false;
                dtlistShowDrafts.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
                
            }
        }

        private void populateDCDetails(string dcno, DataRow drow)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("select * from [DDEFeeRecord_2013-14] where DCNumber='"+dcno+"' and Verified='True'", con1);

            SqlDataReader dr1;

            con1.Open();
            dr1 = cmd1.ExecuteReader();


            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                   
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr1["PaymentMode"]));
                    drow["DCNumber"] = Convert.ToString(dr1["DCNumber"]);
                    drow["DCDate"] = Convert.ToDateTime(dr1["DCDate"]).ToString("dd MMMM yyyy").ToUpper();
                    drow["IBN"] = Convert.ToString(dr1["IBN"]);
                    drow["DCAmount"] = Convert.ToString(dr1["TotalDCAmount"]);
                    drow["VOn"] = dr1["VerifiedOn"].ToString();
                    drow["VBy"] =FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr1["VerifiedBy"])) ;
                }

            }
            con1.Close();
            
        }
    }
}
