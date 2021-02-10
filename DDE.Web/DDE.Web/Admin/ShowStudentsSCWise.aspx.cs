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
    public partial class ShowStudentsSCWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistSession);
                   
                }

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

     
      

       

        private void populateRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SCCode from DDEStudyCentres order by SCCode", con);
             
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("TS");
            DataColumn dtcol4 = new DataColumn("SRIDS");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int j = 1;
            int ts = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = j;
                    drow["SCCode"] = ds.Tables[0].Rows[i]["SCCode"].ToString();
                    string srids;
                    drow["TS"] = FindInfo.findTotalStudentsBySC(drow["SCCode"].ToString(), ddlistSession.SelectedItem.Text, out srids);
                    drow["SRIDS"] = srids;
                    if (Convert.ToInt32(drow["TS"]) != 0)
                    {
                        dt.Rows.Add(drow);
                        j = j + 1;
                        ts = ts + Convert.ToInt32(drow["TS"]);
                    }
                   

                }

               dtlistRecord.DataSource = dt;
               dtlistRecord.DataBind();
               lblTotal.Text ="Total Students : "+ ts.ToString();
               dtlistRecord.Visible = true;
               pnlMSG.Visible = false;
            }
            else
            {
                dtlistRecord.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateRecord();
            
        }



        protected void dtlistRecord_ItemCommand(object source, DataListCommandEventArgs e)
        {

           Session["SRIDS"] = e.CommandArgument; 
           Response.Redirect("ShowStudentListBySC.aspx?SCCode=" + Convert.ToString(e.CommandName));
 
        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistRecord.Visible = false;
        }

      
    }
}
