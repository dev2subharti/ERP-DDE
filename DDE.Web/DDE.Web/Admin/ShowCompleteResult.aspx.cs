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
    public partial class ShowCompleteResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {

                //if (!IsPostBack)
                //{
                //    PopulateDDList.populateExam(ddlistExam);
                //}
            

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
            populateList();
            calculateResult();
            
        }

        private void calculateResult()
        {
            
        }

        private void populateList()
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("RollNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("SCCode");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("Year");
            DataColumn dtcol9 = new DataColumn("Category");
            DataColumn dtcol10 = new DataColumn("Gender");
            DataColumn dtcol11 = new DataColumn("TMarks");
            DataColumn dtcol12 = new DataColumn("OMarks");
            DataColumn dtcol13 = new DataColumn("Result");
            DataColumn dtcol14 = new DataColumn("Grade");
            DataColumn dtcol15 = new DataColumn("Division");


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
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);
            dt.Columns.Add(dtcol15);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = findcommand();
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i + 1;
                drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();
                drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                drow["FatherName"] = ds.Tables[0].Rows[i]["FatherName"].ToString();
                drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
               
                if (ddlistExam.SelectedItem.Value == "A13")
                {
                    if (ds.Tables[0].Rows[i]["AFP1Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP1Year"].ToString() == "True")
                    {
                        drow["Year"] = "1";
                    }
                    else if (ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True")
                    {
                        drow["Year"] = "2";
                    }
                    else if (ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True")
                    {
                        drow["Year"] = "3";
                    }
                    else
                    {
                        drow["Year"] = "0";
                    }

                }
                else
                {
                    drow["Year"] = ds.Tables[0].Rows[i]["Year"].ToString();
                }


                if (ds.Tables[0].Rows[i]["Course"].ToString() == "76")
                {
                    if (drow["Year"].ToString() == "1")
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]));
                    }
                    else if (drow["Year"].ToString() == "2")
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                    }
                    else if (drow["Year"].ToString() == "3")
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                    }
                }
                else
                {
                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]));
                }
                drow["Category"] = ds.Tables[0].Rows[i]["Category"].ToString();
                drow["Gender"] = ds.Tables[0].Rows[i]["Gender"].ToString();
                if (ddlistExam.SelectedItem.Value == "A13")
                {
                    int tmarks;
                    int omarks;
                    string result;
                    string grade;
                    string division;
                    CalculateResult.calculateFullResult(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), drow["Course"].ToString(), Convert.ToInt32(drow["Year"]), ddlistExam.SelectedItem.Value, out tmarks, out omarks, out result, out grade, out division);
                    drow["TMarks"] = tmarks;
                    drow["OMarks"] = omarks;
                    drow["Result"] = result;
                    drow["Grade"] = grade;
                    drow["Division"] = division;
                }
                else
                {
                    drow["TMarks"] = ds.Tables[0].Rows[i]["MaxMarks"].ToString();
                    drow["OMarks"] = ds.Tables[0].Rows[i]["ObtMarks"].ToString();


                    if (ds.Tables[0].Rows[i]["QualifyingStatus"].ToString() == "AC")
                    {
                        drow["Result"] = "Pass";
                        drow["Grade"] = findGrade(Convert.ToInt32(drow["TMarks"]), Convert.ToInt32(drow["OMarks"]));
                        drow["Division"] = findDivision(Convert.ToInt32(drow["TMarks"]), Convert.ToInt32(drow["OMarks"]));
                    }
                    else if (ds.Tables[0].Rows[i]["QualifyingStatus"].ToString() == "PCP")
                    {
                        drow["Result"] = "Fail";
                        drow["Grade"] = "XX";
                        drow["Division"] = "XX";
                    }
                    else
                    {
                        drow["Result"] = "";
                        drow["Grade"] = "";
                        drow["Division"] ="";
                    }
                  
                   
                }

                dt.Rows.Add(drow);

            }

            gvShowReport.DataSource = dt;
            gvShowReport.DataBind();
        
           
        }

        private static string findGrade(int tmarks, int omarks)
        {
            string grade = "";
            int percent = 0;

            if (tmarks != 0)
            {
                percent = ((omarks * 100) / tmarks);
            }


            if (percent >= 85)
            {
                grade = "A++";
            }

            else if (percent < 85 && percent >= 75)
            {
                grade = "A+";
            }

            else if (percent < 75 && percent >= 60)
            {
                grade = "A";
            }
            else if (percent < 60 && percent >= 50)
            {
                grade = "B";
            }

            else if (percent < 50 && percent >= 40)
            {
                grade = "C";
            }

            else if (percent < 40 && percent > 0)
            {
                grade = "D";
            }
            else if (percent == 0)
            {
                grade = "XX";
            }

            return grade;
        }

        private static string findDivision(int tmarks, int omarks)
        {
            string div = "";

            int percent = 0;

            if (tmarks != 0)
            {
                percent = (omarks * 100) / tmarks;
            }


            if (percent >= 60)
            {
                div = "First";

            }

            else if (percent < 60 && percent >= 45)
            {


                div = "Second";

            }

            else if (percent < 45)
            {


                div = "Third";

            }


            return div;
        }

        private string findcommand()
        {
            if (ddlistExam.SelectedItem.Value == "A13")
            {
                return "SELECT  ExamRecord_June13.SRID,DDEStudentRecord.EnrollmentNo,ExamRecord_June13.RollNo,ExamRecord_June13.AFP1Year,ExamRecord_June13.AFP2Year,ExamRecord_June13.AFP3Year,ExamRecord_June13.EFP1Year,ExamRecord_June13.EFP2Year,ExamRecord_June13.EFP3Year,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEStudentRecord.Category,DDEStudentRecord.Gender FROM ExamRecord_June13  inner join DDEStudentRecord on ExamRecord_June13.SRID=DDEStudentRecord.SRID where ExamRecord_June13.MOA='RG' and ExamRecord_June13.ExamRecordID>='" + tbFrom.Text + "' and  ExamRecord_June13.ExamRecordID<='" + tbTo.Text + "' order by DDEStudentRecord.EnrollmentNo";
            }
            else
            {
                return "SELECT DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEStudentRecord.EnrollmentNo,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".RollNo,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".[Year],DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEStudentRecord.Category,DDEStudentRecord.Gender,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MaxMarks,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".ObtMarks,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".QualifyingStatus FROM DDEExamRecord_" + ddlistExam.SelectedItem.Value + "  inner join DDEStudentRecord on DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID=DDEStudentRecord.SRID where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='R' order by DDEStudentRecord.EnrollmentNo";
            }
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistExam.SelectedItem.Value == "A13")
            {
                pnlRange.Visible = true;
            }
            else
            {
                pnlRange.Visible = false;
            }
        }

       
    }
}