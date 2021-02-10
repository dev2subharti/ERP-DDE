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
    public partial class VerificationSheetList2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                if (!IsPostBack)
                {

                    populateAdmitCards();
                    populateStudentPhotos();

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


        private void populateStudentPhotos()
        {
            foreach (DataListItem dli in dtlistAdmitCards.Items)
            {
                Image stph = (Image)dli.FindControl("imgStudentPhoto");
                Label lblsrid = (Label)dli.FindControl("lblSRID");

                stph.ImageUrl = "StudentImgHandler.ashx?SRID=" + lblsrid.Text;
            }
        }

        private void populateAdmitCards()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (Session["SRID"].ToString() == "ALL")
            {
                if (Session["AdmitCourse"].ToString() == "ALL")
                {
                    cmd.CommandText = "select DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".[Year],DDEStudentRecord.EnrollmentNo,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".RollNo,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects1,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects2,DDEExamRecord_" + Session["ACExamCode"].ToString() + ".BPSubjects3,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.[Session],DDEStudentRecord.[SyllabusSession],DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseShortName,DDECourse.Specialization,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".CentreName,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".Location,DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".City from DDEExamRecord_" + Session["ACExamCode"].ToString() + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".SRID inner join DDEExaminationCentres_" + Session["ACExamCode"].ToString() + " on DDEExaminationCentres_" + Session["ACExamCode"].ToString() + ".ECID=DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEExamRecord_" + Session["ACExamCode"].ToString() + ".ExamCentreCode!='0' and DDEExamRecord_" + Session["ACExamCode"].ToString() + ".MOE='" + Session["CardType"].ToString() + "' and (DDEStudentRecord.StudyCentreCode='" + Session["AdmitSCCode"].ToString() + "' or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + Session["AdmitSCCode"].ToString() + "'))";
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
            DataColumn dtcol12 = new DataColumn("ExamCity");
            DataColumn dtcol13 = new DataColumn("Date");
            DataColumn dtcol14 = new DataColumn("Exam");


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

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string[] detained = FindInfo.findDetainedStudents("" + Session["ACExamCode"].ToString() + "", Session["CardType"].ToString());
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

                        drow["Date"] = DateTime.Now.ToString("dd MMMM yyyy");
                        //drow["Date"] = "27 June 2018";
                        drow["Batch"] = ds.Tables[0].Rows[i]["Session"].ToString();

                        drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();
                        drow["ExamCentre"] = ds.Tables[0].Rows[i]["CentreName"].ToString() + "<br/>" + ds.Tables[0].Rows[i]["Location"].ToString();
                        drow["ExamCity"] = ds.Tables[0].Rows[i]["City"].ToString();


                        dt.Rows.Add(drow);
                    }


                }

                dtlistAdmitCards.DataSource = dt;
                dtlistAdmitCards.DataBind();



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
                    lblMSG.Text = "Sorry !! Course or Examination or both Fee not paid for " + Session["Exam"].ToString() + " Examination";
                }
                pnlMSG.Visible = true;
            }


        }


        private string findCardType(string moe)
        {
            string fmoe = "";
            if (moe == "R")
            {
                fmoe = " (MAIN EXAMINATION)";

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