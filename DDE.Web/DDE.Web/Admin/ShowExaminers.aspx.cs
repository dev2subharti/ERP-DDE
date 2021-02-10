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
    public partial class ShowExaminers : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
            {
                if (!IsPostBack)
                {

                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("Z10").Selected = true;
                   
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

        private void populateExaminers()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminers where "+ddlistExamination.SelectedItem.Value+"='True' order by ExID", con);        
            SqlDataReader dr;

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");      
            DataColumn dtcol2 = new DataColumn("UserName");
            DataColumn dtcol3 = new DataColumn("Password");
            DataColumn dtcol4 = new DataColumn("Type");
            DataColumn dtcol5 = new DataColumn("Name");
            DataColumn dtcol6 = new DataColumn("Specialization");
            DataColumn dtcol7 = new DataColumn("Qualification");
            DataColumn dtcol8 = new DataColumn("ContactNo");
          

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            int i = 1;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["UserName"] = String.Format("{0:00000}", Convert.ToInt32(dr["ExID"]));
                drow["Password"] = Convert.ToString(dr["Password"]);
                drow["Type"] = Convert.ToString(dr["Type"]);
                drow["Name"] = Convert.ToString(dr["PreFix"]) +" "+ Convert.ToString(dr["Name"]);
                drow["Specialization"] = Convert.ToString(dr["Specialization"]);
                drow["Qualification"] = Convert.ToString(dr["Qualification"]);
                drow["ContactNo"] = Convert.ToString(dr["ContactNo"]);
               
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowExamCentres.DataSource = dt;
            dtlistShowExamCentres.DataBind();

            con.Close();
        }

      

        protected void dtlistShowExamCentres_ItemCommand(object source, DataListCommandEventArgs e)
        {
            
            if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEExaminerAllotment where ExAlID ='" + Convert.ToString(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Delete", "Deleted a Examiner from " + ddlistExamination.SelectedItem.Text + " exam", Convert.ToInt32(Session["ERID"].ToString()));

                populateExaminers();


            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateExaminers();
        }
    }
}