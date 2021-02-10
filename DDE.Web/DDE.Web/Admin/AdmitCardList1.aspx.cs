using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class AdmitCardList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                if (!IsPostBack)
                {

                    populateAdmitCards();
                    if (Session["SRID"].ToString() == "ALL")
                    {
                        lblTotalCards.Visible = true;
                    }
                    else
                    {
                        lblTotalCards.Visible = false;
                    }


                }

            }


            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private string findSection(string sec)
        {
            if (sec == "1")
            {
                return "AM";
            }
            else if (sec == "2")
            {
                return "PM";
            }
            else if (sec == "3")
            {
                return "NOON";
            }
            else
            {
                return "NF";
            }
        }

        private void populateSubjects()
        {
            foreach (DataListItem dli in dtlistAdmitCards.Items)
            {
                Image stph = (Image)dli.FindControl("imgStudentPhoto");
                Label lblsrid = (Label)dli.FindControl("lblSRID");
                Label lblcourse = (Label)dli.FindControl("lblCourse");
                Label lblyear = (Label)dli.FindControl("lblYear");
                Label lblsysession = (Label)dli.FindControl("lblSySession");

                stph.ImageUrl = "StudentImgHandler.ashx?SRID=" + lblsrid.Text;

                if (Session["CardType"].ToString() == "R")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select DDESubject.SubjectCode,DDESubject.SubjectName,DDESubject.PaperCode,DDEExaminationSchedules.Date,DDEExaminationSchedules.TimeFrom,DDEExaminationSchedules.TimeTo from DDESubject inner join DDEExaminationSchedules on DDEExaminationSchedules.PaperCode=DDESubject.PaperCode where DDESubject.CourseName='" + lblcourse.Text + "' and DDESubject.Year='" + lblyear.Text + "' and DDESubject.SyllabusSession='" + lblsysession.Text.Trim() + "' and  DDEExaminationSchedules.ExaminationCode='"+Session["ACExamCode"].ToString()+"' order by DDEExaminationSchedules.Date", con);
                    con.Open();
                    SqlDataReader dr;

                    dr = cmd.ExecuteReader();

                    //DataTable dt = new DataTable();

                    //DataColumn dtcol1 = new DataColumn("SNo");
                    //DataColumn dtcol2 = new DataColumn("SubjectCode");
                    //DataColumn dtcol3 = new DataColumn("SubjectName");
                    //DataColumn dtcol4 = new DataColumn("Date");
                    //DataColumn dtcol5 = new DataColumn("Time");


                    //dt.Columns.Add(dtcol1);
                    //dt.Columns.Add(dtcol2);
                    //dt.Columns.Add(dtcol3);
                    //dt.Columns.Add(dtcol4);
                    //dt.Columns.Add(dtcol5);


                    int i = 1;
                    string[] pc = { "", "", "", "", "", "", "", "", "", "" };
                    while (dr.Read())
                    {

                        string colsno = "lblSNo" + i.ToString();
                        string colsco = "lblSubCode" + i.ToString();
                        string colsna = "lblSubName" + i.ToString();
                        string coldate = "lblDate" + i.ToString();
                        string coltime = "lblTime" + i.ToString();

                        Label sno = (Label)dli.FindControl(colsno);
                        Label scode = (Label)dli.FindControl(colsco);
                        Label sname = (Label)dli.FindControl(colsna);
                        Label date = (Label)dli.FindControl(coldate);
                        Label time = (Label)dli.FindControl(coltime);


                        int pos = Array.IndexOf(pc, Convert.ToString(dr["PaperCode"]));
                        if (!(pos > -1))
                        {
                            pc[i - 1] = Convert.ToString(dr["PaperCode"]);
                            sno.Text = i.ToString();
                            scode.Text = Convert.ToString(dr["PaperCode"]);
                            sname.Text = Convert.ToString(dr["SubjectName"]);
                            date.Text = Convert.ToDateTime(dr["Date"]).ToString("dd-MM-yyyy");
                            time.Text = dr["TimeFrom"].ToString().Substring(0, 6) + findSection(dr["TimeFrom"].ToString().Substring(6, 1)) + " - " + dr["TimeTo"].ToString().Substring(0, 6) + findSection(dr["TimeTo"].ToString().Substring(6, 1));

                            i = i + 1;
                        }
                    }


                    con.Close();

                    string paper1;
                    string paper2;

                    if (CTStudent(Convert.ToInt32(lblsrid.Text), Session["ExamCode"].ToString(), out paper1, out paper2))
                    {


                        int k = 1;

                        for (int j = i; j <= (i + 1); j++)
                        {
                            string colsno = "lblSNo" + j.ToString();
                            string colsco = "lblSubCode" + j.ToString();
                            string colsna = "lblSubName" + j.ToString();
                            string ctsub = "Paper" + k.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);

                            sno.Text = j.ToString();
                            if (k == 1)
                            {
                                scode.Text = Convert.ToString(paper1);
                                sname.Text = Convert.ToString(paper1);
                            }
                            else if (k == 2)
                            {
                                scode.Text = Convert.ToString(paper2);
                                sname.Text = Convert.ToString(paper2);
                            }

                            k = k + 1;

                        }



                    }

                }

                else if (Session["CardType"].ToString() == "B")
                {

                    string[] sub1 = { };
                    string[] sub2 = { };
                    string[] sub3 = { };

                    string[] prac1 = { };
                    string[] prac2 = { };
                    string[] prac3 = { };

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select BPSubjects1,BPSubjects2,BPSubjects3,BPPracticals1,BPPracticals2,BPPracticals3 from DDEExamRecord_" + Session["ExamCode"] + " where SRID='" + lblsrid.Text + "' and MOE='B'", con);
                    con.Open();
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        sub1 = dr[0].ToString().Split(',');
                        sub2 = dr[1].ToString().Split(',');
                        sub3 = dr[2].ToString().Split(',');

                        prac1 = dr[3].ToString().Split(',');
                        prac2 = dr[4].ToString().Split(',');
                        prac3 = dr[5].ToString().Split(',');
                    }
                    con.Close();





                    int i = 1;
                    if (sub1.Length != 0)
                    {
                        int j = 0;
                        while (j < sub1.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();
                            string coldate = "lblDate" + i.ToString();
                            string coltime = "lblTime" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);
                            Label date = (Label)dli.FindControl(coldate);
                            Label time = (Label)dli.FindControl(coltime);

                            if (sub1[j] != "")
                            {

                                string[] subinfo = FindInfo.findSubjectInfoWithExamDateTimeBySubID(Convert.ToInt32(sub1[j]), Session["ACExamCode"].ToString());
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                date.Text = subinfo[2];
                                time.Text = subinfo[3];
                                i++;
                            }

                            j++;

                        }
                    }
                    if (sub2.Length != 0)
                    {

                        int j = 0;
                        while (j < sub2.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();
                            string coldate = "lblDate" + i.ToString();
                            string coltime = "lblTime" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);
                            Label date = (Label)dli.FindControl(coldate);
                            Label time = (Label)dli.FindControl(coltime);

                            if (sub2[j] != "")
                            {

                                string[] subinfo = FindInfo.findSubjectInfoWithExamDateTimeBySubID(Convert.ToInt32(sub2[j]), Session["ACExamCode"].ToString());
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                date.Text = subinfo[2];
                                time.Text = subinfo[3];
                                i++;
                            }

                            j++;

                        }
                    }
                    if (sub3.Length != 0)
                    {

                        int j = 0;
                        while (j < sub3.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();
                            string coldate = "lblDate" + i.ToString();
                            string coltime = "lblTime" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);
                            Label date = (Label)dli.FindControl(coldate);
                            Label time = (Label)dli.FindControl(coltime);

                            if (sub3[j] != "")
                            {

                                string[] subinfo = FindInfo.findSubjectInfoWithExamDateTimeBySubID(Convert.ToInt32(sub3[j]), Session["ACExamCode"].ToString());
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                date.Text = subinfo[2];
                                time.Text = subinfo[3];
                                i++;
                            }


                            j++;

                        }
                    }



                    if (prac1.Length != 0)
                    {

                        int j = 0;
                        while (j < prac1.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);

                            if (prac1[j] != "")
                            {

                                string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(prac1[j]));
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                i++;
                            }


                            j++;

                        }
                    }

                    if (prac2.Length != 0)
                    {

                        int j = 0;
                        while (j < prac2.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);

                            if (prac2[j] != "")
                            {

                                string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(prac2[j]));
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                i++;
                            }


                            j++;

                        }
                    }

                    if (prac3.Length != 0)
                    {

                        int j = 0;
                        while (j < prac3.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);

                            if (prac3[j] != "")
                            {

                                string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(prac3[j]));
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                i++;
                            }


                            j++;

                        }
                    }

                }





            }
        }

        private bool CTStudent(int srid, string exam, out string paper1, out string paper2)
        {
            bool cts = false;
            paper1 = "";
            paper2 = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDECTPaperRecord where SRID='" + srid + "' and Exam='" + exam + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                cts = true;
                paper1 = dr["Paper1"].ToString();
                paper2 = dr["Paper2"].ToString();
            }
            con.Close();
            return cts;

        }


        private void populateAdmitCards()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (Session["SRID"].ToString() == "ALL")
            {
                if (Session["AdmitCourse"].ToString() == "ALL")
                {
                    cmd.CommandText = "select DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".[Year],DDEStudentRecord.EnrollmentNo,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".RollNo,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects1,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects2,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects3,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.[Session],DDEStudentRecord.[SyllabusSession],DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseShortName,DDECourse.Specialization,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".CentreName,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".Location,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".City from DDEExamRecord_" + Session["ACExamCode"].ToString() + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID inner join DDEExaminationCentres_" + Session["ACExamCode"].ToString() + " on DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".ECID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where  DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode!='0' and DDEExamRecord_" + Session["ACExamCode"].ToString() + ".MOE='" + Session["CardType"].ToString() + "' and (DDEStudentRecord.StudyCentreCode='" + Session["AdmitSCCode"].ToString() + "' or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + Session["AdmitSCCode"].ToString() + "'))";
                }
                else
                {
                    cmd.CommandText = "select DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".[Year],DDEStudentRecord.EnrollmentNo,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".RollNo,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects1,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects2,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects3,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.[Session],DDEStudentRecord.[SyllabusSession],DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseShortName,DDECourse.Specialization,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".CentreName,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".Location,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".City from DDEExamRecord_" + Session["ACExamCode"].ToString() + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID inner join DDEExaminationCentres_" + Session["ACExamCode"].ToString() + " on DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".ECID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode!='0' and DDEExamRecord_" + Session["ACExamCode"].ToString() + ".MOE='" + Session["CardType"].ToString() + "' and (DDEStudentRecord.Course='" + Session["AdmitCourse"].ToString() + "' or DDEStudentRecord.Course2Year='" + Session["AdmitCourse"].ToString() + "' or DDEStudentRecord.Course3Year='" + Session["AdmitCourse"].ToString() + "') and (DDEStudentRecord.StudyCentreCode='" + Session["AdmitSCCode"].ToString() + "' or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + Session["AdmitSCCode"].ToString() + "'))";

                }

            }
            else
            {
                cmd.CommandText = "select DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".[Year],DDEStudentRecord.EnrollmentNo,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".RollNo,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects1,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects2,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects3,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.[Session],DDEStudentRecord.[SyllabusSession],DDEStudentRecord.StudyCentreCode,DDECourse.CourseShortName,DDECourse.Specialization,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".CentreName,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".Location,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".City from DDEExamRecord_" + Session["ACExamCode"].ToString() + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID inner join DDEExaminationCentres_" + Session["ACExamCode"].ToString() + " on DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".ECID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode!='0' and DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID='" + Session["SRID"].ToString() + "' and DDEExamRecord_" + Session["ACExamCode"].ToString() + ".MOE='" + Session["CardType"].ToString() + "'";
            }



            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SRID");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("RollNo");
            //DataColumn dtcol5 = new DataColumn("StudentPhoto");
            DataColumn dtcol6 = new DataColumn("SName");
            DataColumn dtcol7 = new DataColumn("FName");
            DataColumn dtcol8 = new DataColumn("Batch");
            DataColumn dtcol9 = new DataColumn("Course");
            DataColumn dtcol10 = new DataColumn("Year");
            DataColumn dtcol11 = new DataColumn("ExamCentre");
            DataColumn dtcol17 = new DataColumn("ExamCity");
            DataColumn dtcol12 = new DataColumn("SNo");
            DataColumn dtcol13 = new DataColumn("SubjectCode");
            DataColumn dtcol14 = new DataColumn("SubjectName");
            DataColumn dtcol15 = new DataColumn("Date");
            DataColumn dtcol16 = new DataColumn("Time");
            DataColumn dtcol18 = new DataColumn("Exam");
            DataColumn dtcol19 = new DataColumn("SySession");
            DataColumn dtcol20 = new DataColumn("TodayDate");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            //dt.Columns.Add(dtcol5);
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
            dt.Columns.Add(dtcol16);
            dt.Columns.Add(dtcol17);
            dt.Columns.Add(dtcol18);
            dt.Columns.Add(dtcol19);
            dt.Columns.Add(dtcol20);

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string[] detained = FindInfo.findDetainedStudents(Session["ACExamCode"].ToString(), Session["CardType"].ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                    if (!(pos > -1))
                    {
                        DataRow drow = dt.NewRow();

                        drow["SRID"] = ds.Tables[0].Rows[i]["SRID"].ToString();
                        drow["Exam"] = "EXAMINATION - " + Session["Exam"].ToString() + findCardType(Session["CardType"].ToString());
                        if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "O" || ds.Tables[0].Rows[i]["SCStatus"].ToString() == "C")
                        {
                            drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                        }
                        else if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                        {
                            drow["SCCode"] = ds.Tables[0].Rows[i]["PreviousSCCode"].ToString();
                        }
                      
                        drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                        //drow["StudentPhoto"] = "StudentPhotos/" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString() + ".jpg";
                        drow["SName"] = ds.Tables[0].Rows[i]["StudentName"];
                        drow["FName"] = ds.Tables[0].Rows[i]["FatherName"];
                        if (Session["CardType"].ToString() == "R")
                        {
                            drow["Year"] = FindInfo.findAlphaYear(ds.Tables[0].Rows[i]["Year"].ToString()).ToUpper();
                        }
                        else if (Session["CardType"].ToString() == "B")
                        {
                            drow["Year"] = "NA";
                        }
                        if (ds.Tables[0].Rows[i]["CourseShortName"].ToString() == "MBA")
                        {
                            if (Session["CardType"].ToString() == "R")
                            {
                                if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 1)
                                {
                                    drow["Course"] = "MBA";
                                }
                                else if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 2)
                                {
                                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                }
                                else if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 3)
                                {
                                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                }
                            }
                            else if (Session["CardType"].ToString() == "B")
                            {

                                if (ds.Tables[0].Rows[i]["BPSubjects1"].ToString() != "")
                                {
                                    drow["Course"] = "MBA";
                                }
                                if (ds.Tables[0].Rows[i]["BPSubjects2"].ToString() != "")
                                {
                                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                }
                                if (ds.Tables[0].Rows[i]["BPSubjects3"].ToString() != "")
                                {
                                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                }
                            }
                        }
                        else
                        {
                            if (ds.Tables[0].Rows[i]["Specialization"].ToString() == "")
                            {
                                drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString();
                            }
                            else
                            {
                                drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString() + " (" + ds.Tables[0].Rows[i]["Specialization"].ToString() + ")";
                            }
                        }

                        drow["TodayDate"] = DateTime.Now.ToString("dd MMMM yyyy");
                        drow["Batch"] = ds.Tables[0].Rows[i]["Session"].ToString();
                        drow["SySession"] = ds.Tables[0].Rows[i]["SyllabusSession"].ToString();
                        drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();
                        drow["ExamCentre"] = ds.Tables[0].Rows[i]["CentreName"].ToString() + "<br/>" + ds.Tables[0].Rows[i]["Location"].ToString();
                        drow["ExamCity"] = ds.Tables[0].Rows[i]["City"].ToString();
                        drow["SNo"] = "";
                        drow["SubjectCode"] = "";
                        drow["SubjectName"] = "";
                        drow["Date"] = "";
                        drow["Time"] = "";

                        dt.Rows.Add(drow);
                   }


                }

                dtlistAdmitCards.DataSource = dt;
                dtlistAdmitCards.DataBind();

                populateSubjects();

                pnlData.Visible = true;
                dtlistAdmitCards.Visible = true;
                pnlMSG.Visible = false;
                pnlPaging.Visible = true;
                lblTotalCards.Text = "Total Cards : " + (ds.Tables[0].Rows.Count).ToString();
            }
            else
            {
                pnlData.Visible = false;
                dtlistAdmitCards.Visible = false;
                pnlPaging.Visible = false;
                if (Session["SRID"].ToString() == "ALL")
                {
                    lblMSG.Text = "Sorry !! No record found";
                }
                else
                {
                    lblMSG.Text = "Sorry !! Course or Examination or both Fee not paid for "+Session["Exam"].ToString()+" Examination";
                }
                pnlMSG.Visible = true;
            }


        }


        private string findCardType(string moe)
        {
            string fmoe = "";
            if (moe == "R")
            {
                fmoe = "(MAIN EXAMINATION)";

            }
            else
            {
                fmoe = " (BACK PAPER)";
            }

            return fmoe;

        }


        private void PopulatePaging(int totalpages)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("PageNo", typeof(Int32));
            dt.Columns.Add(dc);
            for (int i = 1; i <= totalpages; i++)
            {
                DataRow dr = dt.NewRow();
                dr["PageNo"] = i;
                dt.Rows.Add(dr);
            }
            rptPaging.DataSource = dt;
            rptPaging.DataBind();

        }

        protected void lnkbtnPrevious_Click(object sender, EventArgs e)
        {
            ViewState["Index"] = Convert.ToInt32(ViewState["Index"]) - 1;

            if (Convert.ToInt32(ViewState["Index"]) != 0)
            {
                Session["SNo"] = (Convert.ToInt32(ViewState["Index"]) * 100) + 1;
            }

            else
            {
                Session["SNo"] = 1;
            }
            populateAdmitCards();

            if (Convert.ToInt32(ViewState["Index"]) > 0)
            {
                lnkbtnNext.Visible = true;

            }
            if (Convert.ToInt32(ViewState["Index"]) == 0)
            {

                lnkbtnPrevious.Visible = false;
                lnkbtnNext.Visible = true;

            }
        }

        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            int totalpages = Convert.ToInt32(ViewState["TotalPages"]);
            if (Convert.ToInt32(ViewState["Index"]) != 0)
            {
                Session["SNo"] = (Convert.ToInt32(ViewState["Index"]) * 100) + 1;
            }

            else
            {
                Session["SNo"] = 1;
            }
            if (totalpages > 1)
            {
                if (Convert.ToInt32(ViewState["TotalPages"]) - 1 > Convert.ToInt32(ViewState["Index"]))
                {
                    ViewState["Index"] = Convert.ToInt32(ViewState["Index"]) + 1;
                }

                populateAdmitCards();

                if (Convert.ToInt32(ViewState["TotalPages"]) > 1)
                {
                    lnkbtnPrevious.Visible = true;
                }
                if (Convert.ToInt32(ViewState["TotalPages"]) - 1 == Convert.ToInt32(ViewState["Index"]))
                {
                    lnkbtnNext.Visible = false;
                    lnkbtnPrevious.Visible = true;
                }
            }

        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int totalpages = Convert.ToInt32(ViewState["TotalPages"]);
            ViewState["Index"] = Convert.ToInt32(e.CommandArgument) - 1;

            if (Convert.ToInt32(ViewState["Index"]) != 0)
            {
                Session["SNo"] = (Convert.ToInt32(ViewState["Index"]) * 100) + 1;
            }

            else
            {
                Session["SNo"] = 1;
            }

            populateAdmitCards();





            if (Convert.ToInt32(e.CommandArgument) == 1)
            {
                if (totalpages > 1)
                {
                    lnkbtnPrevious.Visible = false;
                    lnkbtnNext.Visible = true;

                }
                else
                {
                    lnkbtnPrevious.Visible = false;
                    lnkbtnNext.Visible = false;

                }

            }
            else if (Convert.ToInt32(e.CommandArgument) == totalpages)
            {
                lnkbtnNext.Visible = false;
                lnkbtnPrevious.Visible = true;

            }
        }
    }
}