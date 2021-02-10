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
    public partial class ShowEligibilityReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]),124))
            {
                if (!IsPostBack)
                {
                   

                    pnlData.Visible = true;

                    pnlMSG.Visible = false;

                }


            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

   

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct DDEPendingStudentRecord.ExID,DDEExaminers.Name from DDEPendingStudentRecord inner join DDEExaminers on DDEPendingStudentRecord.ExID=DDEExaminers.ExID where DDEPendingStudentRecord.ExID!='0' order by DDEExaminers.Name", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ExID");
            DataColumn dtcol3 = new DataColumn("Name");
            DataColumn dtcol4 = new DataColumn("TotalForms");
            DataColumn dtcol5 = new DataColumn("Checked");
            DataColumn dtcol6 = new DataColumn("Pending");


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
                drow["ExID"] = Convert.ToString(dr["ExID"]);
                drow["Name"] = Convert.ToString(dr["Name"]);
                drow["TotalForms"] = FindInfo.findTotalFormsAllotted(ddlistBatch.SelectedItem.Text, Convert.ToInt32(dr["ExID"]));
                drow["Checked"] = FindInfo.findTotalFormsChecked(ddlistBatch.SelectedItem.Text, Convert.ToInt32(dr["ExID"]));
                drow["Pending"] = Convert.ToInt32(drow["TotalForms"])- Convert.ToInt32(drow["Checked"]);

                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowPending.DataSource = dt;
            dtlistShowPending.DataBind();

            con.Close();

            if (i > 1)
            {
              
                dtlistShowPending.Visible = true;
              
                pnlData.Visible = true;
                pnlMSG.Visible = false;


            }

            else
            {
              
                dtlistShowPending.Visible = false;
              
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {

          
             populateStudents();


        }

        protected void ddlistBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            dtlistShowPending.Visible = false;         
            pnlMSG.Visible = false;


        }

     

       
    }
}