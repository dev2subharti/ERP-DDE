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
using QRCoder;
using System.Drawing;
using System.IO;


namespace DDE.Web.Admin
{
    public partial class PublishMarkSheetBySC5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 112))
            {

                if (!IsPostBack)
                {
                    DataTable dt = (DataTable)Session["Students"];
                    populateStudents(dt);
                    populateMarks();
                    populateMSSNO();
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

        private void populateMSSNO()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                con.Open();

                foreach (DataListItem dli in dtlistMS.Items)
                {



                    System.Web.UI.WebControls.Image imgSt = (System.Web.UI.WebControls.Image)dli.FindControl("imgStudent");
                    Label lblCounter = (Label)dli.FindControl("lblCounter");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label year = (Label)dli.FindControl("lblYear");
                    Label lfresult = (Label)dli.FindControl("lblResult");
                    Label lfmmarks = (Label)dli.FindControl("lblGTMMarks");
                    Label lfomarks = (Label)dli.FindControl("lblGrandTotal");

                    imgSt.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.Text;
                    string qs = "";
                    if (lfresult.Text == "Pass")
                    {
                        qs = "AC";
                    }
                    else if (lfresult.Text == "Not Cleared")
                    {
                        qs = "PCP";
                    }
                    else if (lfresult.Text == "Incomplete")
                    {
                        qs = "IC";
                    }

                    int times = 0;
                    string lastprinted = "NF";
                    int counter = 0;

                    if (FindInfo.IsMSAlreadyPrinted(Convert.ToInt32(srid.Text), Convert.ToInt32(year.Text), Session["ExamCode"].ToString(), Session["MOE"].ToString(), out counter, out times, out lastprinted))
                    {
                        if (counter != 0)
                        {
                            lblCounter.Text = counter.ToString();
                            lblCounter.Visible = true;
                        }

                        Exam.updateMSPrintRecord(Convert.ToInt32(srid.Text), Convert.ToInt32(year.Text), Convert.ToInt32(lfmmarks.Text), Convert.ToInt32(lfomarks.Text), qs, Session["ExamCode"].ToString(), Convert.ToInt32(lblCounter.Text), "N", Session["MOE"].ToString());

                    }
                    else
                    {

                        SqlCommand cmd = new SqlCommand("Select Counter from DDEMSSNOCounter with (TABLOCKX HOLDLOCK)", con);

                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();

                        dr.Read();
                        counter = Convert.ToInt32(dr[0]);
                        lblCounter.Text = (counter + 1).ToString();
                        lblCounter.Visible = true;
                        dr.Close();

                        SqlCommand cmd1 = new SqlCommand("update DDEMSSNOCounter set Counter=@Counter", con);
                        cmd1.Parameters.AddWithValue("@Counter", counter + 1);
                        cmd1.ExecuteNonQuery();

                        Exam.updateMSPrintRecordAndCounter(Convert.ToInt32(srid.Text), Convert.ToInt32(year.Text), Convert.ToInt32(lfmmarks.Text), Convert.ToInt32(lfomarks.Text), qs, Session["ExamCode"].ToString(), counter + 1, "N", Session["MOE"].ToString(), Convert.ToInt32(Session["ERID"]));


                    }
                }

                con.Close();

            }
            catch (Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }


        }

        private void populateMarks()
        {


            foreach (DataListItem dli in dtlistMS.Items)
            {
                Panel qrc = (Panel)dli.FindControl("pnlBC");
                Label ss = (Label)dli.FindControl("lblSS");

                Label srid = (Label)dli.FindControl("lblSRID");
                Label cid = (Label)dli.FindControl("lblCID");
                Label eno = (Label)dli.FindControl("lblENo");
                Label year = (Label)dli.FindControl("lblYear");
                DataList dtlistsub = (DataList)dli.FindControl("dtlistSubMarks");
                DataList dtlistprac = (DataList)dli.FindControl("dtlistPracMarks");

                Label lfmmarks = (Label)dli.FindControl("lblGTMMarks");
                Label lfomarks = (Label)dli.FindControl("lblGrandTotal");
                Label lfgrade = (Label)dli.FindControl("lblGrade");
                Label lfstatus = (Label)dli.FindControl("lblStatus");

                Label lfresult = (Label)dli.FindControl("lblResult");
                Label lfdiv = (Label)dli.FindControl("lblDivision");
                Label lblDOI = (Label)dli.FindControl("lblDOI");
                Label Note = (Label)dli.FindControl("lblNote");

                TableRow tr = (TableRow)dli.FindControl("tr2");

                string fstatus = "NotSet";

                int mmarks = 0;
                int omarks = 0;

                Session["cstatus"] = "NotSet";

                string sess = ss.Text;


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDESubject where SyllabusSession='" + sess + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(srid.Text), Convert.ToInt32(year.Text)) + "' and Year='" + FindInfo.findAlphaYear(year.Text) + "'", con);
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
                    FillSubjectMarks(Convert.ToInt32(srid.Text), Convert.ToInt32(dr["SubjectID"]), drow);

                    mmarks = mmarks + 100;
                    omarks = omarks + getMarks(drow["Total"].ToString());

                    dt.Rows.Add(drow);



                    if (drow["Status"].ToString() == "NC")
                    {
                        if (fstatus == "CC" || fstatus == "NotSet")
                        {
                            fstatus = "NC";
                        }
                    }
                    else if (drow["Status"].ToString() == "CC")
                    {
                        if (fstatus == "NotSet")
                        {
                            fstatus = "CC";
                        }
                    }


                }


                dt.DefaultView.Sort = "SubjectSNo ASC";
                dtlistsub.DataSource = dt;
                dtlistsub.DataBind();

                con.Close();




                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("select * from DDEPractical where SyllabusSession='" + sess + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(srid.Text), Convert.ToInt32(year.Text)) + "' and Year='" + FindInfo.findAlphaYear(year.Text) + "'", con1);
                SqlDataReader dr1;

                con1.Open();
                dr1 = cmd1.ExecuteReader();

                DataTable dt1 = new DataTable();
                DataColumn dtcolpid = new DataColumn("PID");
                DataColumn dtcol21 = new DataColumn("PracticalID");
                DataColumn dtcol31 = new DataColumn("PracticalSNo");
                DataColumn dtcol41 = new DataColumn("PracticalCode");
                DataColumn dtcol51 = new DataColumn("PracticalName");
                DataColumn dtcol61 = new DataColumn("PracticalMaxMarks");
                DataColumn dtcol71 = new DataColumn("PracticalObtainedMarks");
                DataColumn dtcol81 = new DataColumn("PracticalGrade");
                DataColumn dtcol91 = new DataColumn("PracticalStatus");

                dt1.Columns.Add(dtcolpid);
                dt1.Columns.Add(dtcol21);
                dt1.Columns.Add(dtcol31);
                dt1.Columns.Add(dtcol41);
                dt1.Columns.Add(dtcol51);
                dt1.Columns.Add(dtcol61);
                dt1.Columns.Add(dtcol71);
                dt1.Columns.Add(dtcol81);
                dt1.Columns.Add(dtcol91);

                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        DataRow drow1 = dt1.NewRow();
                        drow1["PID"] = "NF";
                        drow1["PracticalID"] = Convert.ToString(dr1["PracticalID"]);
                        drow1["PracticalSNo"] = Convert.ToString(dr1["PracticalSNo"]);
                        drow1["PracticalCode"] = Convert.ToString(dr1["PracticalCode"]);
                        drow1["PracticalName"] = Convert.ToString(dr1["PracticalName"]);
                        drow1["PracticalMaxMarks"] = Convert.ToString(dr1["PracticalMaxMarks"]);

                        fillPracMarks(Convert.ToInt32(srid.Text), Convert.ToInt32(drow1["PracticalID"]), Convert.ToInt32(drow1["PracticalMaxMarks"]), drow1);

                        mmarks = mmarks + getMarks(drow1["PracticalMaxMarks"].ToString());
                        omarks = omarks + getMarks(drow1["PracticalObtainedMarks"].ToString());



                        dt1.Rows.Add(drow1);

                        if (drow1["PracticalStatus"].ToString() == "NC")
                        {
                            if (fstatus == "CC" || fstatus == "NotSet")
                            {
                                fstatus = "NC";
                            }
                        }
                        else if (drow1["PracticalStatus"].ToString() == "CC")
                        {
                            if (fstatus == "NotSet")
                            {
                                fstatus = "CC";
                            }
                        }

                    }
                }

                dt1.DefaultView.Sort = "PracticalSNo ASC";
                dtlistprac.DataSource = dt1;
                dtlistprac.DataBind();

                con1.Close();

                lfmmarks.Text = mmarks.ToString();
                lfomarks.Text = omarks.ToString();
                lfgrade.Text = findFinalGrade(Convert.ToInt32(lfmmarks.Text), Convert.ToInt32(lfomarks.Text));
                lfstatus.Text = fstatus;



                if (lfstatus.Text == "CC")
                {
                    lfresult.Text = "Pass";
                }
                else if (lfstatus.Text == "NC")
                {
                    if (Session["cstatus"].ToString() == "Complete")
                    {
                        lfresult.Text = "Not Cleared";
                    }
                    else if (Session["cstatus"].ToString() == "Incomplete")
                    {
                        lfresult.Text = "Incomplete";
                    }
                }
                else if (lfstatus.Text == "NotSet")
                {
                    lfresult.Text = "Incomplete";
                }

                if (lfresult.Text == "Pass")
                {
                    lfdiv.Text = findDivision(Convert.ToInt32(lfmmarks.Text), Convert.ToInt32(lfomarks.Text));

                }
                else
                {
                    lfdiv.Text = "XX";
                }

                int cd = FindInfo.findCourseDuration(FindInfo.findCourseIDBySRID(Convert.ToInt32(srid.Text)));


                if ((cd == 2) && Convert.ToInt32(year.Text) == 1)
                {
                    tr.Visible = false;
                }
                else if ((cd == 3) && (Convert.ToInt32(year.Text) == 1 || Convert.ToInt32(year.Text) == 2))
                {
                    if ((FindInfo.isEligibleForTwoYearMBA(eno.Text)) && (year.Text == "2") && (FindInfo.findCourseShortNameByID(Convert.ToInt32(cid.Text)) == "MBA"))
                    {
                        tr.Visible = true;
                    }
                    else
                    {
                        tr.Visible = false;
                    }
                }

               


                string code = "For verification and more details please follow the link : www.subhartidde.com/MS.aspx?EN=" + HttpUtility.UrlEncode(eno.Text) + "&Y=" + HttpUtility.UrlEncode(Convert.ToInt32(year.Text).ToString()) + "&E=" + HttpUtility.UrlEncode(Session["ExamCode"].ToString());
                QRCodeGenerator qrGenerator = new QRCodeGenerator();

                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                imgBarCode.Height = 100;
                imgBarCode.Width = 100;
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    qrc.Controls.Add(imgBarCode);
                }




            }
        }

        private void fillPracMarks(int srid, int pid, int mpmarks, DataRow drow1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPracticalMarks_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and PracticalID='" + pid + "' and MOE='R'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                if (dr["PracticalMarks"].ToString() == "AB")
                {
                    drow1["PracticalObtainedMarks"] = "AB";

                    if (Session["cstatus"].ToString() == "NotSet")
                    {
                        Session["cstatus"] = "Complete";
                    }
                }

                else if (dr["PracticalMarks"].ToString() == "")
                {
                    drow1["PracticalObtainedMarks"] = "";

                    if (Session["cstatus"].ToString() == "Complete" || Session["cstatus"].ToString() == "NotSet")
                    {
                        Session["cstatus"] = "Incomplete";
                    }

                }

                else
                {
                    drow1["PracticalObtainedMarks"] = getMarks(dr["PracticalMarks"].ToString()).ToString();
                    if (Session["cstatus"].ToString() == "NotSet")
                    {
                        Session["cstatus"] = "Complete";
                    }
                }


                drow1["PracticalGrade"] = findPracGrade(drow1["PracticalObtainedMarks"].ToString(), mpmarks.ToString());
                drow1["PracticalStatus"] = findPracStatus(drow1["PracticalObtainedMarks"].ToString(), mpmarks.ToString());



            }
            else
            {
                Session["cstatus"] = "Incomplete";
                drow1["PracticalObtainedMarks"] = "NF";
                drow1["PracticalGrade"] = "NF";
                drow1["PracticalStatus"] = "NC";
            }


            con.Close();
        }

        private void FillSubjectMarks(int srid, int subid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEMarkSheet_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='R'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();


                if (dr["Theory"].ToString() == "AB")
                {
                    drow["Theory"] = "AB";
                    if (Session["cstatus"].ToString() == "NotSet")
                    {
                        Session["cstatus"] = "Complete";
                    }

                }
                else if (dr["Theory"].ToString() == "")
                {
                    drow["Theory"] = "";
                    if (Session["cstatus"].ToString() == "Complete" || Session["cstatus"].ToString() == "NotSet")
                    {
                        Session["cstatus"] = "Incomplete";
                    }
                }
                else
                {
                    drow["Theory"] = ((Convert.ToInt32(dr["Theory"]) * 70) / 100).ToString();
                    if (Session["cstatus"].ToString() == "NotSet")
                    {
                        Session["cstatus"] = "Complete";
                    }

                }

                drow["IA"] = dr["IA"].ToString();
                drow["AW"] = dr["AW"].ToString();
                drow["Total"] = (getMarks(drow["Theory"].ToString()) + getMarks(drow["IA"].ToString()) + getMarks(drow["AW"].ToString())).ToString();
                drow["Grade"] = findGrade(drow["Total"].ToString());
                drow["Status"] = findStatus(drow["Theory"].ToString(), drow["IA"].ToString(), drow["AW"].ToString());
            }
            else
            {
                Session["cstatus"] = "Incomplete";
                drow["Theory"] = "NF";
                drow["IA"] = "NF";
                drow["AW"] = "NF";
                drow["Total"] = "NF";
                drow["Grade"] = "NF";
                drow["Status"] = "NC";

            }
            con.Close();

        }

        private void populateStudents(DataTable sdt)
        {
            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SRID");
            DataColumn dtcol2 = new DataColumn("StudentName");
            DataColumn dtcol3 = new DataColumn("FatherName");
            DataColumn dtcol4 = new DataColumn("MotherName");           
            DataColumn dtcol5 = new DataColumn("RollNo");
            DataColumn dtcol6 = new DataColumn("EnrollmentNo");
            DataColumn dtcol7 = new DataColumn("Examination");
            DataColumn dtcol8 = new DataColumn("ExamCenter");
            DataColumn dtcol9 = new DataColumn("Course");
            DataColumn dtcol10 = new DataColumn("CID");
            DataColumn dtcol11 = new DataColumn("Year");
            DataColumn dtcol12 = new DataColumn("SySession");
            DataColumn dtcol13 = new DataColumn("DOA");
            DataColumn dtcol14 = new DataColumn("DOC");

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

            for (int i = 0; i < sdt.Rows.Count; i++)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDEStudentRecord where SRID='" + Convert.ToInt32(sdt.Rows[i]["SRID"]) + "' and RecordStatus='True'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {


                    DataRow drow = dt.NewRow();
                    drow["SRID"] = Convert.ToInt32(sdt.Rows[i]["SRID"]);
                    drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                    drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                    drow["MotherName"] = Convert.ToString(dr["MotherName"]);
                    //drow["SCCode"] = FindInfo.findSCCodeForMarkSheetBySRID(Convert.ToInt32(sdt.Rows[i]["SRID"]));
                    drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(sdt.Rows[i]["SRID"]), Session["ExamCode"].ToString(), "R");
                    drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                    drow["Examination"] = Session["ExamName"].ToString();
                    drow["ExamCenter"] = FindInfo.findVExamCentreBySRID(Convert.ToInt32(drow["SRID"]), Session["ExamCode"].ToString(), "R");
                    drow["Year"] = Convert.ToInt32(sdt.Rows[i]["Year"]);

                    if ((FindInfo.isEligibleForTwoYearMBA(drow["EnrollmentNo"].ToString())) && (drow["Year"].ToString() == "2") && (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"].ToString())) == "MBA"))
                    {
                        drow["Course"] = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(sdt.Rows[i]["SRID"]), Convert.ToInt32(drow["Year"])) + " - FINAL YEAR";
                    }
                    else
                    {

                        drow["Course"] = FindInfo.findCourseAndYearForNewMS(Convert.ToInt32(sdt.Rows[i]["SRID"]), Convert.ToInt32(drow["Year"]));

                    }

                    drow["CID"] = Convert.ToInt32(dr["Course"]);
                    drow["SySession"] = dr["SyllabusSession"].ToString();
                    drow["DOA"] = "Date of Admission : "+ FindInfo.findDOABySRID(Convert.ToInt32(drow["SRID"]));
                    drow["DOC"] = "Date of Completion : " +FindInfo.findDOCByExam(Session["ExamCode"].ToString());
                    dt.Rows.Add(drow);



                }

                con.Close();
            }

            dtlistMS.DataSource = dt;
            dtlistMS.DataBind();
        }

        private string findStatus(string tee, string ia, string aw)
        {

            string status = "";

            int teepercent = 0;
            int iapercent = 0;
            int awpercent = 0;


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


            }

            else
            {
                status = "CC";


            }



            return status;



        }

        private int getMarks(string marks)
        {
            if (marks == "" || marks == "-" || marks == "AB" || marks == "NF" || marks == "*")
            {
                return 0;
            }

            else return Convert.ToInt32(marks);
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

        private string findFinalGrade(int mmarks, int omarks)
        {
            string grade = "";
            int percent = 0;

            if (mmarks.ToString() != "0" && omarks.ToString() != "" && omarks.ToString() != "NF")
            {
                percent = (getMarks(omarks.ToString()) * 100) / getMarks(mmarks.ToString());
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

            }

            else
            {
                status = "CC";

            }

            return status;


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

        private string findDivision(int maxmarks, int marksobtained)
        {
            string div = "";

            int percent = 0;

            if (maxmarks != 0)
            {
                percent = (marksobtained * 100) / maxmarks;
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
    }
}