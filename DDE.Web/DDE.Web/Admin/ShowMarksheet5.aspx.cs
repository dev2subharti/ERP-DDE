﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{

    public partial class ShowMarksheet5 : System.Web.UI.Page
    {
        int fototal = 0;
        int fmtotal = 0;
        string fstatus = "NotSet";
        string cstatus = "NotSet";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 112))
            {
                //if (!IsPostBack)
                //{
                if (Request.QueryString["EnrollmentNo"] != null)
                {

                    if (populateStudentDetail() != 0)
                    {
                        populateMarkSheet();
                        setBarCode();
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

        private void setBarCode()
        {


            string code = "For verification and more details please follow the link : www.subhartidde.com/MS.aspx?EN=" + HttpUtility.UrlEncode(lblENo.Text) + "&Y=" + HttpUtility.UrlEncode(Convert.ToInt32(Session["Year"]).ToString()) + "&E=" + HttpUtility.UrlEncode(Session["ExamCode"].ToString());
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 100;
            imgBarCode.Width = 100;
            using (Bitmap bitMap = qrCode.GetGraphic(15))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                pnlBC.Controls.Add(imgBarCode);
            }
        }

        private string findMSType(string moe)
        {
            if (moe == "R")
            {
                return "MAIN EXAM";
            }
            else if (moe == "B")
            {
                return "BACK PAPER";
            }
            else
            {
                return "";
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
            }
            else
            {
                lblDivision.Text = "XX";
            }

            int cd = FindInfo.findCourseDuration(FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)));




            if ((cd == 2) && Convert.ToInt32(Session["Year"]) == 1)
            {
                tr2.Visible = false;
            }
            else if ((cd == 3) && (Convert.ToInt32(Session["Year"]) == 1 || Convert.ToInt32(Session["Year"]) == 2))
            {
                if ((FindInfo.isEligibleForTwoYearMBA(lblENo.Text)) && (Session["Year"].ToString() == "2") && (FindInfo.findCourseShortNameByID(Convert.ToInt32(lblCID.Text)) == "MBA"))
                {
                    tr2.Visible = true;
                }
                else
                {
                    tr2.Visible = false;
                }
            }


           
                lblDOA.Text = "Date of Admission : " + FindInfo.findDOABySRID(Convert.ToInt32(lblSRID.Text));
                lblDOC.Text = "Date of Completion : " + FindInfo.findDOCByExam(Session["ExamCode"].ToString());
                lblExamCentre.Text = FindInfo.findVExamCentreBySRID(Convert.ToInt32(lblSRID.Text), Session["ExamCode"].ToString(), Session["ResultType"].ToString());

                lblNote.Visible = true;

                pnlData.Visible = true;
                pnlMSG.Visible = false;
        }

        private int populateStudentDetail()
        {
            int srid = FindInfo.findSRIDByENo(Request.QueryString["EnrollmentNo"]);


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select StudyCentreCode,SyllabusSession,Session,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,Course,StudentName,FatherName,MotherName from DDEStudentRecord where SRID='" + srid + "' and StudentPhoto is not null and RecordStatus='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lblSRID.Text = srid.ToString();
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + lblSRID.Text;
                lblSySession.Text = dr["SyllabusSession"].ToString();
                lblSession.Text= dr["Session"].ToString();
                lblSName.Text = dr["StudentName"].ToString();
                lblFName.Text = dr["FatherName"].ToString();
                lblMName.Text = dr["MotherName"].ToString();
                lblENo.Text = dr["EnrollmentNo"].ToString();
                lblExamination.Text = Session["ExamName"].ToString();
                lblCID.Text = dr["Course"].ToString();
                lblSCCode.Text = dr["StudyCentreCode"].ToString();

                lblRNo.Text = FindInfo.findRollNoBySRID1(Convert.ToInt32(srid), Convert.ToInt32(Session["Year"]), Session["ExamCode"].ToString(), Session["ResultType"].ToString());

                if ((FindInfo.isEligibleForTwoYearMBA(lblENo.Text)) && (Session["Year"].ToString() == "2") && (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"].ToString())) == "MBA"))
                {
                    lblCourseFullName.Text = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid), Convert.ToInt32(Session["Year"])) + " - FINAL YEAR";
                }
                else
                {

                    lblCourseFullName.Text = FindInfo.findCourseAndYearForNewMS(Convert.ToInt32(srid), Convert.ToInt32(Session["Year"]));

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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESubject where SyllabusSession='" + lblSySession.Text + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"])) + "' and Year='" + Session["AYear"].ToString() + "'", con);
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
            int counter = 0;
            bool graced = false;

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

                            marks = ((Convert.ToInt32(mr) * 70) / 100).ToString();


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
                            if (Session["ExamCode"].ToString() == "A12")
                            {
                                theory.Text = "AB";
                            }
                            else
                            {
                                theory.Text = "";
                            }
                            if (cstatus == "Complete" || cstatus == "NotSet")
                            {
                                cstatus = "Incomplete";
                            }
                        }
                        else
                        {
                            theory.Text = ((Convert.ToInt32(dr["Theory"]) * 70) / 100).ToString();
                            if (Convert.ToInt32(theory.Text) >= 23 && Convert.ToInt32(theory.Text) <= 27)
                            {
                                if (FindInfo.isEligibleExamForGrace(Session["ExamCode"].ToString()))
                                {
                                    if (counter == 0)
                                    {
                                        if (FindInfo.courseEligibleForGrace(Convert.ToInt32(lblSRID.Text)))
                                        {
                                            int premarks;
                                            string subname;
                                            if (FindInfo.eligibleForGrace1(Convert.ToInt32(lblSRID.Text), Session["ExamCode"].ToString(), Session["Year"].ToString(), out premarks, out subname))
                                            {
                                                theory.Text = "28";
                                                counter = counter + 1;
                                                graced = true;
                                            }
                                        }
                                    }
                                }
                            }
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
                            if (lblSCCode.Text == "995" || lblSCCode.Text == "997")
                            {
                                theory.Text = theory.Text + "*";
                            }


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

            if (graced == true)
            {
                fillGracedRecord(srid, Convert.ToInt32(Session["Year"]), Session["ExamCode"].ToString());
            }
        }

        private void fillGracedRecord(int srid, int year, string exam)
        {
            if (!FindInfo.isGraced(srid, year, exam))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDEGracedStudents values(@SRID,@Year,@Exam)", con);

                cmd.Parameters.AddWithValue("@SRID", srid);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@Exam", exam);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
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
                drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);

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


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical where SyllabusSession='" + lblSySession.Text + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"])) + "' and Year='" + Session["AYear"].ToString() + "'", con);
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

                            if (cstatus == "NotSet")
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

                        if (lblSCCode.Text == "995" || lblSCCode.Text == "997")
                        {
                            omarks.Text = omarks.Text + "*";
                        }
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
                drow["PracticalName"] = Convert.ToString(dr["PracticalName"]);
                drow["PracticalMaxMarks"] = Convert.ToString(dr["PracticalMaxMarks"]);


            }

            con.Close();
        }

        private string findDivision(int maxmarks, int marksobtained)
        {
            string div = "";

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            int times = 0;
            string counter;
            string lastprinted = "NF";
            if (alreadyPrinted(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"]), Session["PrintMode"].ToString(), out counter, out times, out lastprinted))
            {
                pnlData.Visible = false;
                lblMSG.Text = "This Mark Sheet has already printed with this print mode'" + times.ToString() + "' times <br/>With S.No. : '" + counter + "'<br/> Last Printed : " + lastprinted + "<br/> Are you sure to print it again?.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                if (Session["PrintMode"].ToString() == "N")
                {
                    btnGSNo.Visible = false;
                }
                else if (Session["PrintMode"].ToString() == "C" || Session["PrintMode"].ToString() == "D")
                {
                    btnGSNo.Visible = true;
                }
                btnNO.Visible = true;

            }
            else
            {
                btnPrint.Visible = false;

                generateAndUpdateSNo(Session["PrintMode"].ToString());

                ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
            }
        }

        private void generateAndUpdateSNo(string printmode)
        {
            try
            {
                string qs = "";
                if (lblResult.Text == "Pass")
                {
                    qs = "AC";
                }
                else if (lblResult.Text == "Not Cleared")
                {
                    qs = "PCP";
                }
                else if (lblResult.Text == "Incomplete")
                {
                    qs = "IC";
                }

                int counter = 0;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select Counter from DDEMSSNOCounter with (TABLOCKX HOLDLOCK)", con);

                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    counter = Convert.ToInt32(dr[0]);
                    lblCounter.Text =(counter + 1).ToString();
                    lblCounter.Visible = true;

                }
                dr.Close();

                SqlCommand cmd1 = new SqlCommand("update DDEMSSNOCounter set Counter=@Counter", con);
                cmd1.Parameters.AddWithValue("@Counter", counter + 1);
                cmd1.ExecuteNonQuery();

                Exam.updateMSPrintRecordAndCounter(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"]), Convert.ToInt32(lblGTMMarks.Text), Convert.ToInt32(lblGrandTotal.Text), qs, Session["ExamCode"].ToString(), counter + 1, printmode, Session["ResultType"].ToString(), Convert.ToInt32(Session["ERID"]));


                con.Close();


            }
            catch (Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }


        }

        private bool alreadyPrinted(int srid, int year, string printmode, out string counter, out int times, out string lastprinted)
        {
            bool printed = false;
            times = 0;
            lastprinted = "NF";
            counter = "NA";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();
            if(Session["ResultType"].ToString()=="R")
            {
                if (printmode == "N")
                {
                    cmd.CommandText = "Select MSPrinted,Times,LastPrintTime,MSCounter from DDEExamRecord_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and Year='" + year + "' and MSPrinted='True' and MOE='" + Session["ResultType"].ToString() + "'";
                }
                else if (printmode == "C" || printmode == "D")
                {
                    cmd.CommandText = "Select Times,LastPrintTime,MSCounter from DDEMSCountersDC where SRID='" + srid + "' and Year='" + year + "' and Exam='" + Session["ExamCode"].ToString() + "' and PrintMode='" + printmode + "' and MOE='" + Session["ResultType"].ToString() + "'";
                }
            }
            else if (Session["ResultType"].ToString() == "B")
            {
                if (printmode == "N")
                {
                    cmd.CommandText = "Select MSPrinted,Times,LastPrintTime,MSCounter from DDEExamRecord_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and MSPrinted='True' and MOE='" + Session["ResultType"].ToString() + "'";
                }
                else if (printmode == "C" || printmode == "D")
                {
                    cmd.CommandText = "Select Times,LastPrintTime,MSCounter from DDEMSCountersDC where SRID='" + srid + "' and Exam='" + Session["ExamCode"].ToString() + "' and PrintMode='" + printmode + "' and MOE='" + Session["ResultType"].ToString() + "'";
                }
            }


            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                printed = true;
                while (dr.Read())
                {
                    times = Convert.ToInt32(dr["Times"]);
                    lastprinted = dr["LastPrintTime"].ToString();
                    lblCounter.Text = counter = Convert.ToString(dr["MSCounter"]);

                }

            }

            con.Close();

            return printed;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnPrint.Visible = false;
            string qs = "";
            if (lblResult.Text == "Pass")
            {
                qs = "AC";
            }
            else if (lblResult.Text == "Not Cleared")
            {
                qs = "PCP";
            }
            else if (lblResult.Text == "Incomplete")
            {
                qs = "IC";
            }


            lblCounter.Visible = true;
           
            //Exam.updateMSPrintRecord(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"]), Convert.ToInt32(lblGTMMarks.Text), Convert.ToInt32(lblGrandTotal.Text), qs, Session["ExamCode"].ToString(), Convert.ToInt32(lblCounter.Text), Session["PrintMode"].ToString(), Session["ResultType"].ToString());

            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
        }

        protected void btnGSNo_Click(object sender, EventArgs e)
        {
            btnPrint.Visible = false;

            generateAndUpdateSNo(Session["PrintMode"].ToString());

            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
        }

        protected void btnNO_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            btnPrint.Visible = true;
            pnlMSG.Visible = false;
        }


    }

}