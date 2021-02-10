using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class ShowGracedStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 30))
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
            SqlCommand cmd = new SqlCommand("Select distinct DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='R' and (DDEStudentRecord.Course='19' or DDEStudentRecord.Course='20' or DDEStudentRecord.Course='22' or DDEStudentRecord.Course='76')", con);
           
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("Course");
            DataColumn dtcol7 = new DataColumn("Year");
            DataColumn dtcol8 = new DataColumn("SCCode");
            DataColumn dtcol9 = new DataColumn("SubjectName");
            DataColumn dtcol10 = new DataColumn("PMarks");
            DataColumn dtcol11 = new DataColumn("CMarks");
            DataColumn dtcol12 = new DataColumn("Grace");

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
            dt.Columns.Add(dtcol12);

            int counter = 1;
            int premarks;
            string subname;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //if (FindInfo.courseEligibleForGrace(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"])))
                    //{

                        string year = ds.Tables[0].Rows[i]["Year"].ToString();

                        if (FindInfo.eligibleForGrace(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value, year, out premarks, out subname))
                        {
                            DataRow drow = dt.NewRow();
                            drow["SNo"] = counter;
                            drow["SRID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
                            drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                            drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                            drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);          
                            drow["Year"] = Convert.ToInt32(year);

                            if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "O" || ds.Tables[0].Rows[i]["SCStatus"].ToString() == "C")
                            {
                                drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                            {
                                drow["SCCode"] = ds.Tables[0].Rows[i]["PreviousSCCode"].ToString();
                            }
                            if (FindInfo.isMBACourse(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"])))
                            {
                                if (ds.Tables[0].Rows[i]["Year"].ToString() == "1")
                                {
                                    drow["Course"] = "MBA";
                                }
                                else if (ds.Tables[0].Rows[i]["Year"].ToString() == "2")
                                {
                                    try
                                    {
                                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                    }
                                    catch
                                    {
                                        drow["Course"] = "NA";
                                    }
                                }
                                else if (ds.Tables[0].Rows[i]["Year"].ToString() == "3")
                                {
                                    try
                                    {
                                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                    }
                                    catch
                                    {
                                        drow["Course"] = "NA";
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]));
                                }
                                catch
                                {
                                    drow["Course"] = "NA";
                                }
                            }
                            
                           
                            drow["SubjectName"] = subname;
                            drow["PMarks"] = premarks;
                            drow["CMarks"] = "24";
                            drow["Grace"] = 24 - premarks;
                            dt.Rows.Add(drow);
                            counter  =counter + 1;
                        }

                    //}
                }
            }

            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            con.Close();

            if (counter> 1)
            {

                dtlistShowStudents.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;


            }

            else
            {
                dtlistShowStudents.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

      

       

        private bool eligibleForGrace(int srid, string exam,out int premarks)
        {
            bool eligible = false;
            premarks = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select Theory from DDEMarkSheet_" + exam + " where SRID='" + srid + "' and MOE='R'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            string tm = "";
            int counter = 0;
            while (dr.Read())
            {
                tm = dr[0].ToString();
                if (tm != "AB" && tm != "")
                {
                    int theory = ((Convert.ToInt32(tm) * 60) / 100);
                    if (theory >= 19 && theory <= 23)
                    {
                        counter = counter + 1;
                        if (counter == 1)
                        {
                            premarks = Convert.ToInt32(tm);
                        }
                    } 

                }
            }

            con.Close();

            return eligible;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateStudents();
        }

        
    }
}
