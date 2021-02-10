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
    public partial class PublishStudentListofSLMLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {

                if (!IsPostBack)
                {
                    lblLNo.Text = "Ref. No. : DDE/SVSU/2016/SLM/"+Request.QueryString["LNo"];
                    
                    populateStudents();


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
            SqlCommand cmd = new SqlCommand("Select DDESLMIssueRecord.SLMRID,DDESLMIssueRecord.CID,DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNO,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDECourse.CourseName,DDESLMIssueRecord.Year from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID inner join DDECourse on DDECourse.CourseID=DDESLMIssueRecord.CID where DDESLMIssueRecord.LNo='" + Request.QueryString["LNo"] + "' order by DDEStudentRecord.EnrollmentNo", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");       
          
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("Course");        
            DataColumn dtcol7 = new DataColumn("Year");
           

            dt.Columns.Add(dtcol1);
           
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
           

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
              
              
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
             
                drow["Course"] = Convert.ToString(dr["CourseName"]);
                drow["Year"] = Convert.ToString(dr["Year"]);
              
                dt.Rows.Add(drow);
                i = i + 1;
            }

           

            gvShowStudent.DataSource = dt;
            gvShowStudent.DataBind();

            con.Close();


            if (i > 1)
            {
                gvShowStudent.Visible = true;
                pnlData.Visible = true;
                lblMSG.Text = "";
                pnlMSG.Visible = false;

               
            }
            else
            {
                pnlData.Visible = false;
                gvShowStudent.Visible = false;              
                lblMSG.Text = "Sorry !! No student Found on this letter";
                pnlMSG.Visible = true;
            }
        }
    }
}