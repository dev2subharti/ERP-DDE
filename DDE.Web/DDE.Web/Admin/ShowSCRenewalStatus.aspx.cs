using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class ShowSCRenewalStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 39))
            {

               
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateSCList();
        }

        private void populateSCList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

           
            cmd.CommandText = "Select * from DDEStudyCentres order by SCCode";
           

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("City");
            DataColumn dtcol5 = new DataColumn("SCName");
            DataColumn dtcol6 = new DataColumn("Renewal");
        


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
          


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SCID"] = Convert.ToString(dr["SCID"]);
                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                drow["City"] = Convert.ToString(dr["City"]);
                drow["SCName"] = Convert.ToString(dr["Location"]);

                if (ddlistStatus.SelectedItem.Text == "OK")
                {
                    if (Convert.ToString(dr["REFor"]) == DateTime.Now.ToString("yyyy"))
                    {
                        drow["Renewal"] = "OK";
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }
                }
                else if (ddlistStatus.SelectedItem.Text == "PENDING")
                {
                   if (Convert.ToString(dr["REFor"]) != DateTime.Now.ToString("yyyy"))
                   {
                       drow["Renewal"] = "Pending";
                       dt.Rows.Add(drow);
                       i = i + 1;
                   }
                }
            }

            dtlistShowStudyCentres.DataSource = dt;
            dtlistShowStudyCentres.DataBind();

            con.Close();

            if (i>1)
            {
                dtlistShowStudyCentres.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowStudyCentres.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }
        }
    }
}