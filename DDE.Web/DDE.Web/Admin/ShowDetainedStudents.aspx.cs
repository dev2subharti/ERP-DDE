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
    public partial class ShowDetainedStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
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

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEDetainedStudents where Examination='" + ddlistExam.SelectedItem.Value + "' and DetainedStatus='True' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("ApplicationNo");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("SCCode");
            DataColumn dtcol8 = new DataColumn("Course");
            DataColumn dtcol9 = new DataColumn("Session");
            DataColumn dtcol10 = new DataColumn("Detained");
            DataColumn dtcol11 = new DataColumn("Remark");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                populateStudentInfo(Convert.ToInt32(dr["SRID"]), drow);
               
                drow["Remark"] = Convert.ToString(dr["Remark"]);

                if (Convert.ToString(dr["DetainedStatus"]) == "True")
                {
                    drow["Detained"] = "Yes";
                }
                else if (Convert.ToString(dr["DetainedStatus"]) == "False")
                {
                    drow["Detained"] = "No";
                }
               
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowDetained.DataSource = dt;
            dtlistShowDetained.DataBind();

            con.Close();

            if (i > 1)
            {

                dtlistShowDetained.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;


            }

            else
            {
                dtlistShowDetained.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        private void populateStudentInfo(int srid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where SRID='" + srid + "'", con);
            
            
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();        
            while (dr.Read())
            {
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["ApplicationNo"] = Convert.ToString(dr["ApplicationNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["SCCode"] = FindInfo.findBothTCSCCodeBySRID(Convert.ToInt32(dr["SRID"]));
                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["CYear"]));
                drow["Session"] = Convert.ToString(dr["Session"]);
            }

            con.Close();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            populateStudents();
        }

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDetained.Visible = false;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDetained.Visible = false;
        }

       
      
    }
}
