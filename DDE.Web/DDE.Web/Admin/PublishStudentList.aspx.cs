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
    public partial class PublishStudentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 112))
            {
                if (!IsPostBack)
                {
                    populateStudents(Session["StudentList"].ToString());
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

        private void populateStudents(string students)
        {
            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
           
            DataColumn dtcol3 = new DataColumn("Enrollment No.");
            DataColumn dtcol4 = new DataColumn("Student Name");
            DataColumn dtcol5 = new DataColumn("Father Name");
            DataColumn dtcol6 = new DataColumn("SC Code");
            DataColumn dtcol7 = new DataColumn("Roll No.");
            DataColumn dtcol8 = new DataColumn("Course");
            DataColumn dtcol9 = new DataColumn("Year");


            dt.Columns.Add(dtcol1);
          
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            string[] srid = students.Split(',');
            for (int i = 0; i < srid.Length; i++)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDEStudentRecord where SRID='" + srid[i] + "' and RecordStatus='True'", con);
                SqlDataReader dr;

              
                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = (i+1).ToString();       
                    drow["Enrollment No."] = Convert.ToString(dr["EnrollmentNo"]);
                    drow["Student Name"] = Convert.ToString(dr["StudentName"]);
                    drow["Father Name"] = Convert.ToString(dr["FatherName"]);
                    drow["SC Code"] = FindInfo.findSCCodeForMarkSheetBySRID(Convert.ToInt32(srid[i]));
                    drow["Roll No."] = FindInfo.findRollNoBySRID(Convert.ToInt32(srid[i]), Session["ExamCode"].ToString(), "R");                 
                    drow["Year"] =FindInfo.findAllExamYear(Convert.ToInt32(srid[i]),Session["ExamCode"].ToString(),"R");

                    if (drow["Year"].ToString() != "" && drow["Year"].ToString() != "0" && drow["Year"].ToString() != "NF")
                    {
                        if (drow["Year"].ToString().Length == 1)
                        {
                            drow["Course"] = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid[i]), Convert.ToInt32(drow["Year"]));
                        }
                        else if (drow["Year"].ToString().Length == 3)
                        {
                            drow["Course"] = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid[i]), Convert.ToInt32(drow["Year"].ToString().Substring(2, 1)));
                        }
                        else if (drow["Year"].ToString().Length == 5)
                        {
                            drow["Course"] = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid[i]), Convert.ToInt32(drow["Year"].ToString().Substring(2, 1)));
                        }
                    }
                    else
                    {
                        drow["Course"] = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid[i]), 1);
                    }
                   
                    dt.Rows.Add(drow);
                   
                }

                con.Close();
            }

           gvShowStudent.DataSource = dt;
           gvShowStudent.DataBind();
        }

      
    }

}
