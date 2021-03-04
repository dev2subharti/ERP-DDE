using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class ShowMarks1 : System.Web.UI.Page
    {
        int fototal = 0;
        int fmtotal = 0;
        string fstatus = "NotSet";
        string cstatus = "NotSet";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 30))
            {
                if (Request.QueryString["EnrollmentNo"] != null)
                {

                    if (populateStudentDetail() != 0)
                    {
                        if (Session["ResultType"].ToString() == "B")
                        {
                           
                           lblBP.Visible = false;
                           
                           
                        }

                        else if (Session["ResultType"].ToString() == "R")
                        {
                            lblBP.Visible = false;
                        }

                        populateMarkSheet();

                        string remark;

                       

                      if (!(FindInfo.isExamCentreSetBySRID(Convert.ToInt32(lblSRID.Text), Session["ExamCode"].ToString(), Session["ResultType"].ToString())) || lblMName.Text == "" || Session["MarkSheetType"].ToString() == "REAPPEAR" || FindInfo.isDetained(Convert.ToInt32(lblSRID.Text), Session["ExamCode"].ToString(), Session["ResultType"].ToString(), out remark))
                      {
                           btnPubMar.Visible = false;

                      }

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! this Enrollment No. does not exist";
                        pnlMSG.Visible = true;

                    }
                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please close this page and open website again";
                    pnlMSG.Visible = true;

                }

                setAccessbility();
                btnPubMar.Visible = false;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 112))
            {
                if (Request.QueryString["EnrollmentNo"] != null)
                {

                    if (populateStudentDetail() != 0)
                    {
                        if (Session["ResultType"].ToString() == "B")
                        {

                            lblBP.Visible = false;


                        }

                        else if (Session["ResultType"].ToString() == "R")
                        {
                            lblBP.Visible = false;
                        }

                        populateMarkSheet();

                        string remark;



                        if (!(FindInfo.isExamCentreSetBySRID(Convert.ToInt32(lblSRID.Text), Session["ExamCode"].ToString(), Session["ResultType"].ToString())) || lblMName.Text == "" || Session["MarkSheetType"].ToString() == "REAPPEAR" || FindInfo.isDetained(Convert.ToInt32(lblSRID.Text), Session["ExamCode"].ToString(), Session["ResultType"].ToString(), out remark))
                        {
                            btnPubMar.Visible = false;

                        }

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! this Enrollment No. does not exist";
                        pnlMSG.Visible = true;

                    }
                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please close this page and open website again";
                    pnlMSG.Visible = true;

                }

                setAccessbility();

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

        private void setAccessbility()
        {
            if (!Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                foreach (DataListItem dli in dtlistSubMarks.Items)
                {

                    LinkButton del = (LinkButton)dli.FindControl("lnkbtnDeleteSMarks");
                    del.Visible = false;

                }
                foreach (DataListItem dli in dtlistPracMarks.Items)
                {

                    LinkButton del = (LinkButton)dli.FindControl("lnkbtnDeletePMarks");
                    del.Visible = false;

                }
            }
        }

        private void populateMarkSheet()
        {
            fototal = 0;
            fmtotal = 0;
            fstatus = "NotSet";

            populateSubjectMarks();
            populatePracticalMarks();

            lblGTMMarks.Text = fmtotal.ToString();
            lblGrandTotal.Text = fototal.ToString();

            if (Session["ResultType"].ToString() == "R")
            {
                lblGrade.Text = findTotalGrade(lblGrandTotal.Text, lblGTMMarks.Text);
            }
            else if (Session["ResultType"].ToString() == "B")
            {
               
              lblGrade.Text = findTotalGrade(lblGrandTotal.Text, lblGTMMarks.Text);
               
               
            }

            if (lblGrade.Text == "XX")
            {
                lblStatus.Text = "XX";
            }
            else
            {
                lblStatus.Text = fstatus.ToString();
            }

            if (cstatus == "Complete")
            {
                lblResult.Text = findResultStatus(Convert.ToInt32(lblGTMMarks.Text), Convert.ToInt32(lblGrandTotal.Text));
            }
            else if (cstatus == "Incomplete" || cstatus == "NotSet")
            {
                lblResult.Text = "Incomplete";
            }

            if (lblResult.Text == "Pass")
            {
                lblDivision.Text = findDivision(Convert.ToInt32(lblGTMMarks.Text), Convert.ToInt32(lblGrandTotal.Text));
                btnPubMar.Visible = true;
               

            }
            else if (lblResult.Text == "Not Cleared")
            {
                lblDivision.Text = "XX";             
                btnPubMar.Visible = true;
               
            }
            else if (lblResult.Text == "Incomplete")
            {
                lblDivision.Text = "XX";

            }

            pnlData.Visible = true;
            pnlMSG.Visible = false;
        }

        private int populateStudentDetail()
        {
            int srid = FindInfo.findSRIDByENo(Request.QueryString["EnrollmentNo"]);


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select StudyCentreCode,SyllabusSession,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,Course,StudentName,FatherName,MotherName from DDEStudentRecord where SRID='" + srid + "' and RecordStatus='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                lblSName.Text = dr["StudentName"].ToString();
                lblFName.Text = dr["FatherName"].ToString();
                lblMName.Text = dr["MotherName"].ToString();
                lblENo.Text = dr["EnrollmentNo"].ToString();
                lblSCCode.Text = FindInfo.findSCCodeForMarkSheetBySRID(srid);
                lblSRID.Text = srid.ToString();
                lblCID.Text = dr["Course"].ToString();
                lblSySession.Text = dr["SyllabusSession"].ToString();

                lblRNo.Text = FindInfo.findRollNoBySRID1(Convert.ToInt32(srid), Convert.ToInt32(Session["Year"]), Session["ExamCode"].ToString(), Session["ResultType"].ToString());

                lblExamination.Text = Session["ExamName"].ToString();

                string course = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid), Convert.ToInt32(Session["Year"]));

                if (Session["ResultType"].ToString() == "R")
                {
                    if (Convert.ToInt32(Session["Year"]) == 1)
                    {

                        if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                        {
                            lblCourseFullName.Text = course + "<br/> FIRST YEAR";

                        }
                        else
                        {
                            lblCourseFullName.Text = course;
                        }
                    }

                    else if (Convert.ToInt32(Session["Year"]) == 2)
                    {

                        if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                        {
                            lblCourseFullName.Text = course + "<br/> SECOND YEAR";

                        }
                        else
                        {
                            lblCourseFullName.Text = course;
                        }
                    }

                    else if (Convert.ToInt32(Session["Year"]) == 3)
                    {

                        if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                        {
                            lblCourseFullName.Text = course + "<br/> THIRD YEAR";

                        }
                        else
                        {
                            lblCourseFullName.Text = course;
                        }
                    }
                }

                else if (Session["ResultType"].ToString() == "B")
                {
                        
                        if (Convert.ToInt32(Session["Year"]) == 1)
                        {

                            if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                            {
                                lblCourseFullName.Text = course + "<br/> FIRST YEAR";
                            }
                            else
                            {
                                lblCourseFullName.Text = course;
                            }
                        }

                        else if (Convert.ToInt32(Session["Year"]) == 2)
                        {

                            if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                            {
                                lblCourseFullName.Text = course + "<br/> SECOND YEAR";
                            }
                            else
                            {
                                lblCourseFullName.Text = course;
                            }
                        }

                        else if (Convert.ToInt32(Session["Year"]) == 3)
                        {

                            if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                            {
                                lblCourseFullName.Text = course + "<br/> THIRD YEAR";

                            }
                            else
                            {
                                lblCourseFullName.Text = course;
                            }
                        }
                       
                        
                }

              


            }


            con.Close();
            return srid;



        }

        private string findCourseFullNameByID(int courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select CourseFullName from DDECourse where CourseID='" + courseid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string cfn = dr[0].ToString();
            con.Close();
            return cfn;

        }

        private int findSRIDByEnrollmentNo(string eno)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select SRID from DDEStudentRecord where EnrollmentNo='" + eno + "' and RecordStatus='True'", con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                int srid = Convert.ToInt32(dr[0]);
                con.Close();
                return srid;
            }

            catch
            {

                return 0;

            }
        }


        private void populateSubjectMarks()
        {
            
                string ss = "";
                if ((Session["SySession"].ToString() == "A 2013-14") && (Session["ExamCode"].ToString() == "B14") && (lblENo.Text.Substring(0, 3) == "C13" || lblENo.Text.Substring(0, 3) == "C14") && (Session["Year"].ToString() == "2") && (FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)) == 76))
                {
                    ss = "A 2010-11";
                }
                else if ((Session["SySession"].ToString() == "A 2013-14") && (Session["ExamCode"].ToString() == "A14") && (lblENo.Text.Substring(0, 3) == "A13") && (Session["Year"].ToString() == "2") && (FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)) == 76))
                {
                    ss = "A 2010-11";
                }
                if (Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10" || Session["ExamCode"].ToString() == "W11")
                {

                    ss = lblSySession.Text;

                }
                else
                {
                    ss = Session["SySession"].ToString();
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDESubject where SyllabusSession='" + ss + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"])) + "' and Year='" + Session["AYear"].ToString() + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("RID");
                DataColumn dtcol2 = new DataColumn("SubjectID");
                DataColumn dtcol3 = new DataColumn("SubjectSNo");
                DataColumn dtcol4 = new DataColumn("SubjectCode");
                DataColumn dtcol5 = new DataColumn("SubjectName");
                DataColumn dtcol6 = new DataColumn("MTheory");
                DataColumn dtcol7 = new DataColumn("MIA");
                DataColumn dtcol8 = new DataColumn("MAW");
                DataColumn dtcol9 = new DataColumn("MTotal");
                DataColumn dtcol10 = new DataColumn("Theory");
                DataColumn dtcol11 = new DataColumn("IA");
                DataColumn dtcol12 = new DataColumn("AW");
                DataColumn dtcol13 = new DataColumn("Total");
                DataColumn dtcol14 = new DataColumn("Grade");
                DataColumn dtcol15 = new DataColumn("Status");

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


                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["RID"] = "NF";
                    drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                    drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                    drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                    drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);
                    drow["MTheory"] = "70";
                    drow["MIA"] = "15";
                    drow["MAW"] = "15";
                    drow["MTotal"] = "100";
                    drow["Theory"] = "NF";
                    drow["IA"] = "NF";
                    drow["AW"] = "NF";
                    drow["Total"] = "NF";
                    drow["Grade"] = "NF";
                    drow["Status"] = "NF";
                    dt.Rows.Add(drow);
                    fmtotal = fmtotal + 100;
                }


                dt.DefaultView.Sort = "SubjectSNo ASC";
                dtlistSubMarks.DataSource = dt;
                dtlistSubMarks.DataBind();

                con.Close();


                populateSMarks(Convert.ToInt32(lblSRID.Text), Session["ResultType"].ToString());
          
           
            

        }

        private void populateSMarks(int srid, string moe)
        {

            foreach (DataListItem li in dtlistSubMarks.Items)
            {
                LinkButton del = (LinkButton)li.FindControl("lnkbtnDeleteSMarks");
                Label subid = (Label)li.FindControl("lblSubjectID");
                Label theory = (Label)li.FindControl("lblTheory");
                Label ia = (Label)li.FindControl("lblIA");
                Label aw = (Label)li.FindControl("lblAW");
                Label total = (Label)li.FindControl("lblTotal");
                Label grade = (Label)li.FindControl("lblGrade");
                Label status = (Label)li.FindControl("lblStatus");

                if (moe == "B")
                {
                    string[] sacy = Session["BPSubjects"].ToString().Split(',');
                    int pos = Array.IndexOf(sacy, subid.Text);
                    if ((pos > -1))
                    {
                        string mr = Exam.findNewTheoryMarks(srid, Convert.ToInt32(subid.Text), Session["ExamCode"].ToString());

                        if (mr == "AB")
                        {
                            theory.Text = "AB*";
                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (mr == "")
                        {
                            theory.Text = "*";
                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Incomplete";
                            }
                        }
                        else
                        {
                            theory.Text = mr + "*";
                            if (theory.Text.Length == 2)
                            {
                                theory.Text = "0" + theory.Text;
                            }
                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }

                        }


                    }
                    else
                    {
                        string mode;
                        string preexam;
                        string mr = Exam.findPreviousMaximumTheoryMarks(srid, Convert.ToInt32(subid.Text), out mode, out preexam);

                        if (mr == "AB")
                        {
                            if (mode == "R")
                            {
                                theory.Text = "AB";
                            }
                            else if (mode == "B")
                            {
                                theory.Text = "AB*";
                            }
                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (mr == "")
                        {

                            if (mode == "R")
                            {
                                theory.Text = "";
                            }
                            else if (mode == "B")
                            {
                                theory.Text = "*";
                            }
                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Incomplete";
                            }
                        }
                        else
                        {
                            string marks = "";
                            if (preexam == "B12")
                            {
                                marks = mr;
                            }
                            else
                            {
                                marks = ((Convert.ToInt32(mr) * 70) / 100).ToString();
                            }

                            if (mode == "R")
                            {
                                if (marks.Length == 1)
                                {
                                    theory.Text = "0" + marks;
                                }
                                else
                                {
                                    theory.Text = marks;
                                }
                            }
                            else if (mode == "B")
                            {
                                if (marks.Length == 1)
                                {
                                    theory.Text = "0" + marks + "*";
                                }
                                else
                                {
                                    theory.Text = marks + "*";
                                }
                            }


                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }

                        }


                    }


                    string[] pim = Exam.findPreviousInternalMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(subid.Text), "R");

                    if (pim[0] == "")
                    {
                        ia.Text = "";
                        if (cstatus == "Complete" || cstatus == "NotSet")
                        {
                            cstatus = "Incomplete";
                        }
                    }
                    else if (pim[0] == "AB")
                    {
                        ia.Text = "";

                    }
                    else
                    {
                        ia.Text = pim[0];
                    }

                    if (pim[1] == "")
                    {
                        aw.Text = "";
                        if (cstatus == "Complete" || cstatus == "NotSet")
                        {
                            cstatus = "Incomplete";
                        }
                    }
                    else if (pim[1] == "AB")
                    {
                        aw.Text = "";

                    }
                    else
                    {
                        aw.Text = pim[1];
                    }

                    total.Text = (getMarks(theory.Text.Trim(new Char[] { '*' })) + getMarks(ia.Text) + getMarks(aw.Text)).ToString();
                    grade.Text = findGrade(total.Text);
                    fototal = fototal + Convert.ToInt32(total.Text);
                    status.Text = findStatus(theory.Text.Trim(new Char[] { '*' }), ia.Text, aw.Text, moe);


                }
                else
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;
                    cmd.CommandText = "select * from DDEMarkSheet_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and SubjectID='" + subid.Text + "' and MOE='" + moe + "'";
                    cmd.Connection = con;

                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        del.CommandArgument = dr["RID"].ToString();

                        if (dr["Theory"].ToString() == "AB")
                        {
                            theory.Text = "AB";
                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (dr["Theory"].ToString() == "")
                        {
                            
                            theory.Text = "";
                            
                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Incomplete";
                            }
                        }
                        else
                        {
                            theory.Text = ((Convert.ToInt32(dr["Theory"]) * 70) / 100).ToString();
                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }

                        }


                        if (moe == "R")
                        {
                            if (dr["IA"].ToString() == "")
                            {
                                if (cstatus == "Complete" || cstatus == "NotSet")
                                {
                                    cstatus = "Incomplete";
                                }
                            }
                            else
                            {
                                ia.Text = dr["IA"].ToString();
                            }

                            if (dr["AW"].ToString() == "")
                            {
                                if (cstatus == "Complete" || cstatus == "NotSet")
                                {
                                    cstatus = "Incomplete";
                                }
                            }
                            else
                            {
                                aw.Text = dr["AW"].ToString();
                            }
                            total.Text = (getMarks(theory.Text) + getMarks(ia.Text) + getMarks(aw.Text)).ToString();
                            grade.Text = findGrade(total.Text);
                            fototal = fototal + Convert.ToInt32(total.Text);

                        }
                        else if (moe == "B")
                        {

                            ia.Text = "-";
                            aw.Text = "-";
                            total.Text = getMarks(theory.Text).ToString();
                            grade.Text = "-";
                            fototal = fototal + Convert.ToInt32(total.Text);


                        }
                        status.Text = findStatus(theory.Text.Trim(new Char[] { '*' }), ia.Text, aw.Text, moe);
                    }
                    else
                    {
                        cstatus = "Incomplete";

                        status.Text = "NC";
                        if (fstatus == "CC" || fstatus == "NotSet")
                        {
                            fstatus = "NC";
                        }
                    }

                    con.Close();
                }

            }
        }


        private void fillSubjectDetail(int subid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESubject where SubjectID='" + subid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                drow["SubjectName"] = Convert.ToString(dr["SubjectName"]) + " (" + Convert.ToString(dr["SyllabusSession"]) + ")";

            }

            con.Close();
        }

        private int getMarks(string marks)
        {
            if (marks == "" || marks == "-" || marks == "AB" || marks == "NF" || marks == "*")
            {
                return 0;
            }
            else return Convert.ToInt32(marks);
        }

        private void populatePracticalMarks()
        {

                string sysession = FindInfo.findSySessionBySRID(Convert.ToInt32(lblSRID.Text));
              
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDEPractical where SyllabusSession='" + sysession + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"])) + "' and Year='" + Session["AYear"].ToString() + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("PID");
                DataColumn dtcol2 = new DataColumn("PracticalID");
                DataColumn dtcol3 = new DataColumn("PracticalSNo");
                DataColumn dtcol4 = new DataColumn("PracticalCode");
                DataColumn dtcol5 = new DataColumn("PracticalName");
                DataColumn dtcol6 = new DataColumn("PracticalMaxMarks");
                DataColumn dtcol7 = new DataColumn("PracticalObtainedMarks");
                DataColumn dtcol8 = new DataColumn("PracticalGrade");
                DataColumn dtcol9 = new DataColumn("PracticalStatus");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);
                dt.Columns.Add(dtcol8);
                dt.Columns.Add(dtcol9);

                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["PID"] = "NF";
                    drow["PracticalID"] = Convert.ToString(dr["PracticalID"]);
                    drow["PracticalSNo"] = Convert.ToString(dr["PracticalSNo"]);
                    drow["PracticalCode"] = Convert.ToString(dr["PracticalCode"]);
                    drow["PracticalName"] = Convert.ToString(dr["PracticalName"]);
                    drow["PracticalMaxMarks"] = Convert.ToString(dr["PracticalMaxMarks"]);
                    drow["PracticalObtainedMarks"] = "NF";
                    drow["PracticalGrade"] = "NF";
                    drow["PracticalStatus"] = "NF";
                    dt.Rows.Add(drow);
                    fmtotal = fmtotal + Convert.ToInt32((drow["PracticalMaxMarks"].ToString()));

                }

                dt.DefaultView.Sort = "PracticalSNo ASC";
                dtlistPracMarks.DataSource = dt;
                dtlistPracMarks.DataBind();

                con.Close();


                populatePracMarks(Convert.ToInt32(lblSRID.Text), Session["ResultType"].ToString());


        }

        private void populatePracMarks(int srid, string moe)
        {
            foreach (DataListItem dli in dtlistPracMarks.Items)
            {
                LinkButton del = (LinkButton)dli.FindControl("lnkbtnDeletePMarks");
                Label pracid = (Label)dli.FindControl("lblPracticalID");
                Label omarks = (Label)dli.FindControl("lblPOMarks");
                Label mmarks = (Label)dli.FindControl("lblPMMarks");
                Label grade = (Label)dli.FindControl("lblPGrade");
                Label status = (Label)dli.FindControl("lblPStatus");

                if (moe == "B")
                {
                    string[] sacy = Session["BPPracticals"].ToString().Split(',');
                    int pos = Array.IndexOf(sacy, pracid.Text);
                    if (!(pos > -1))
                    {
                        string mode;
                        string mr = Exam.findPreviousMaximumPracticalMarks(srid, Convert.ToInt32(pracid.Text), out mode);

                        if (mr == "AB")
                        {
                            if (mode == "R")
                            {
                                omarks.Text = "AB";
                            }
                            else if (mode == "B")
                            {
                                omarks.Text = "AB*";
                            }


                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (mr == "-")
                        {
                            if (mode == "R")
                            {
                                omarks.Text = "-";
                            }
                            else if (mode == "B")
                            {
                                omarks.Text = "-*";
                            }

                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (mr == "")
                        {
                            if (mode == "R")
                            {
                                omarks.Text = "";
                            }
                            else if (mode == "B")
                            {
                                omarks.Text = "*";
                            }


                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Incomplete";
                            }

                        }

                        else
                        {
                            if (mode == "R")
                            {
                                omarks.Text = getMarks(mr).ToString();
                            }
                            else if (mode == "B")
                            {
                                omarks.Text = getMarks(mr).ToString() + "*";
                            }

                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                    }
                    else
                    {
                        string mr = Exam.findNewPracticalMarks(srid, Convert.ToInt32(pracid.Text), Session["ExamCode"].ToString());

                        if (mr == "AB")
                        {
                            omarks.Text = "AB*";

                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (mr == "-")
                        {
                            omarks.Text = "-*";

                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }

                        }
                        else if (mr == "")
                        {
                            omarks.Text = "*";

                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Incomplete";
                            }

                        }

                        else
                        {
                            omarks.Text = getMarks(mr).ToString() + "*";
                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }


                    }

                    fototal = fototal + getMarks((omarks.Text.Trim(new char[] { '*' })));

                    grade.Text = findPracGrade(omarks.Text.Trim(new char[] { '*' }), mmarks.Text);
                    status.Text = findPracStatus(omarks.Text.Trim(new char[] { '*' }), mmarks.Text);
                }
                else
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;


                    cmd.CommandText = "select * from DDEPracticalMarks_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and PracticalID='" + pracid.Text + "' and MOE='" + moe + "'";


                    cmd.Connection = con;
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        del.CommandArgument = dr["PID"].ToString();

                        if (dr["PracticalMarks"].ToString() == "AB")
                        {
                            omarks.Text = "AB";

                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (dr["PracticalMarks"].ToString() == "-")
                        {
                            omarks.Text = "-";

                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }
                        else if (dr["PracticalMarks"].ToString() == "")
                        {
                            omarks.Text = "";

                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Incomplete";
                            }

                        }

                        else
                        {
                            omarks.Text = getMarks(dr["PracticalMarks"].ToString()).ToString();
                            if (cstatus == "NotSet")
                            {
                                cstatus = "Complete";
                            }
                        }

                        fototal = fototal + getMarks((omarks.Text));

                        grade.Text = findPracGrade(omarks.Text, mmarks.Text);
                        status.Text = findPracStatus(omarks.Text, mmarks.Text);


                    }
                    else
                    {
                        cstatus = "Incomplete";

                        status.Text = "NC";
                        if (fstatus == "CC" || fstatus == "NotSet")
                        {
                            fstatus = "NC";
                        }

                    }


                    con.Close();
                }

            }

        }

        private void fillPracticalDetail(DataRow drow, int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical where PracticalID='" + pracid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drow["PracticalID"] = Convert.ToString(dr["PracticalID"]);
                drow["PracticalSNo"] = Convert.ToString(dr["PracticalSNo"]);
                drow["PracticalCode"] = Convert.ToString(dr["PracticalCode"]);
                drow["PracticalName"] = Convert.ToString(dr["PracticalName"]) + " (" + Convert.ToString(dr["SyllabusSession"]) + ")";
                drow["PracticalMaxMarks"] = Convert.ToString(dr["PracticalMaxMarks"]);


            }

            con.Close();
        }




        private string findDivision(int maxmarks, int marksobtained)
        {
            string div = "";

            if (Session["ResultType"].ToString() == "R")
            {
                int percent = 0;

                if (maxmarks != 0)
                {
                    percent = (marksobtained * 100) / maxmarks;
                }


                if (lblStatus.Text == "CC")
                {


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
                }

                else if (lblStatus.Text == "NC" || lblStatus.Text == "NotSet" || lblStatus.Text == "XX")
                {
                    div = "XX";


                }

            }
            else if (Session["ResultType"].ToString() == "B")
            {
                
                    int percent = 0;

                    if (maxmarks != 0)
                    {
                        percent = (marksobtained * 100) / maxmarks;
                    }


                    if (lblStatus.Text == "CC")
                    {


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
                    }

                    else if (lblStatus.Text == "NC" || lblStatus.Text == "NotSet" || lblStatus.Text == "XX")
                    {
                        div = "XX";


                    }
                
                
            }

            return div;
        }

        private string findResultStatus(int maxmarks, int marksobtained)
        {
            string status = "";
            int percent = 0;

            if (marksobtained != 0)
            {
                percent = (marksobtained * 100) / maxmarks;
            }


            if (lblStatus.Text == "CC")
            {
                status = "Pass";

            }

            else if (lblStatus.Text == "NC")
            {


                status = "Not Cleared";

            }


            return status;
        }

        private string findStatus(string tee, string ia, string aw, string moe)
        {

            string status = "";

            int teepercent = 0;
            int iapercent = 0;
            int awpercent = 0;

            if (moe == "R")
            {
                if (tee != "" && tee != "NF")
                {
                    teepercent = (getMarks(tee) * 100) / 70;
                }
                if (ia != "" && ia != "NF")
                {
                    iapercent = (getMarks(ia) * 100) / 15;
                }
                if (aw != "" && aw != "NF")
                {
                    awpercent = (getMarks(aw) * 100) / 15;
                }

                if (teepercent < 40 || iapercent < 40 || awpercent < 40)
                {
                    status = "NC";
                    if (fstatus == "CC" || fstatus == "NotSet")
                    {
                        fstatus = "NC";
                    }

                }

                else
                {
                    status = "CC";
                    if (fstatus == "NotSet")
                    {
                        fstatus = "CC";
                    }

                }
            }
            else if (moe == "B")
            {
                if (Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10" || Session["ExamCode"].ToString() == "W11")
                {
                    if (tee != "" && tee != "NF")
                    {
                        teepercent = (getMarks(tee) * 100) / 70;
                    }
                    if (ia != "" && ia != "NF")
                    {
                        iapercent = (getMarks(ia) * 100) / 15;
                    }
                    if (aw != "" && aw != "NF")
                    {
                        awpercent = (getMarks(aw) * 100) / 15;
                    }

                    if (teepercent < 40 || iapercent < 40 || awpercent < 40)
                    {
                        status = "NC";
                        if (fstatus == "CC" || fstatus == "NotSet")
                        {
                            fstatus = "NC";
                        }

                    }
                    else
                    {
                        status = "CC";
                        if (fstatus == "NotSet")
                        {
                            fstatus = "CC";
                        }

                    }
                }
                else
                {
                    if (tee != "" && tee != "NF")
                    {
                        teepercent = (getMarks(tee) * 100) / 70;
                    }


                    if (teepercent < 40)
                    {
                        status = "NC";
                        if (fstatus == "CC" || fstatus == "NotSet")
                        {
                            fstatus = "NC";
                        }

                    }

                    else
                    {
                        status = "CC";
                        if (fstatus == "NotSet")
                        {
                            fstatus = "CC";
                        }

                    }
                }
            }

            return status;



        }


        private string findPracStatus(string pracmarksobtained, string maxpracmarks)
        {
            string status = "";
            int pracpercent = 0;

            if (pracmarksobtained != "" && pracmarksobtained != "NF")
            {
                pracpercent = (getMarks(pracmarksobtained) * 100 / getMarks(maxpracmarks));
            }



            if (pracpercent < 40)
            {
                status = "NC";
                if (fstatus == "CC" || fstatus == "NotSet")
                {
                    fstatus = "NC";
                }
            }

            else
            {
                status = "CC";
                if (fstatus == "NotSet")
                {
                    fstatus = "CC";
                }
            }

            return status;


        }



        private string findGrade(string total)
        {
            string grade = "XX";
            if (total != "" && total != "NF")
            {

                int percent = (getMarks(total) * 100) / 100;

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

                else if (percent < 40)
                {
                    grade = "D";
                }
            }

            return grade;

        }

        private string findPracGrade(string pracmarksobtained, string maxpracmarks)
        {
            string grade = "";
            int percent = 0;

            if (maxpracmarks != "0" && pracmarksobtained != "" && pracmarksobtained != "NF")
            {
                percent = (getMarks(pracmarksobtained) * 100) / getMarks(maxpracmarks);
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

            else if (percent < 40)
            {
                grade = "D";
            }

            return grade;

        }

        private string findTotalGrade(string marksobtained, string maxmarks)
        {
            string grade = "";
            int percent = 0;

            if (maxmarks != "0" && marksobtained != "" && marksobtained != "NF")
            {
                percent = (getMarks(marksobtained) * 100) / getMarks(maxmarks);
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

        protected void dtlistSubMarks_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEMarkSheet_" + Session["ExamCode"] + "  where RID ='" + Convert.ToString(e.CommandArgument) + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Delete", "Delete subjects marks of a student with Enrollment No '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                populateMarkSheet();
            }
        }

        protected void dtlistPracMarks_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEPracticalMarks_" + Session["ExamCode"] + "  where PID ='" + Convert.ToString(e.CommandArgument) + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Delete", "Delete practical marks of a student with Enrollment No '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                populateMarkSheet();

            }
        }

        protected void btnPubMar_Click(object sender, EventArgs e)
        {
            Session["AutoDate"] = rblAutoDate.SelectedItem.Text;
            Session["PrintMode"] = rblPrintMode.SelectedItem.Value;
            Response.Redirect("ShowMarkSheet5.aspx?EnrollmentNo=" + lblENo.Text);
                     
        }
    }
}