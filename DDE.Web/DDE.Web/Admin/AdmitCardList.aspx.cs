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
using DDE.DAL;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class AdmitCardList : System.Web.UI.Page
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
                        lblTotalCards.Visible =false;
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

       

        private void populateSubjects()
        {
            foreach (DataListItem dli in dtlistAdmitCards.Items)
            {
                Image stph = (Image)dli.FindControl("imgStudentPhoto");
                Label lblsrid = (Label)dli.FindControl("lblSRID");
                Label lblcourse = (Label)dli.FindControl("lblCourse");
                Label lblyear = (Label)dli.FindControl("lblYear");

                stph.ImageUrl = "StudentImgHandler.ashx?SRID=" + lblsrid.Text;

                if (Session["CardType"].ToString() == "R")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select SubjectCode,SubjectName from DDESubject where CourseName='" + lblcourse.Text + "' and Year='" + lblyear.Text + "' and SyllabusSession='" + Session["AdmitSySession"].ToString() + "' order by SubjectSNo", con);
                    con.Open();
                    SqlDataReader dr;

                    dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();

                    DataColumn dtcol1 = new DataColumn("SNo");
                    DataColumn dtcol2 = new DataColumn("SubjectCode");
                    DataColumn dtcol3 = new DataColumn("SubjectName");



                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);



                    int i = 1;
                    while (dr.Read())
                    {

                        string colsno = "lblSNo" + i.ToString();
                        string colsco = "lblSubCode" + i.ToString();
                        string colsna = "lblSubName" + i.ToString();

                        Label sno = (Label)dli.FindControl(colsno);
                        Label scode = (Label)dli.FindControl(colsco);
                        Label sname = (Label)dli.FindControl(colsna);

                        sno.Text = i.ToString();
                        scode.Text = Convert.ToString(dr["SubjectCode"]);
                        sname.Text = Convert.ToString(dr["SubjectName"]);


                        i = i + 1;
                    }
                    

                    con.Close();

                    string paper1;
                    string paper2;

                    if (CTStudent(Convert.ToInt32(lblsrid.Text), out paper1, out paper2))
                    {

                        
                        int k = 1;
                        
                        for(int j=i;j<=(i+1); j++)
                        {
                            string colsno = "lblSNo" + j.ToString();
                            string colsco = "lblSubCode" + j.ToString();
                            string colsna = "lblSubName" + j.ToString();
                            string ctsub = "Paper" +k.ToString();

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
                    string[] sub1={};
                    string[] sub2={};
                    string[] sub3={};

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



                    DataTable dt = new DataTable();
                    DataColumn dtcol1 = new DataColumn("SNo");
                    DataColumn dtcol2 = new DataColumn("SubjectCode");
                    DataColumn dtcol3 = new DataColumn("SubjectName");

                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);

                    int i = 1;
                    if (sub1.Length!=0)
                    {
                        int j = 0;
                        while (j<sub1.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);
                            if (sub1[j] != "")
                            {

                                string[] subinfo = FindInfo.findSubjectInfoByID(Convert.ToInt32(sub1[j]));
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                i++;
                            }

                            j++;
                           
                        }
                    }
                    if (sub2.Length!=0)
                    {

                        int j = 0;
                        while (j <sub2.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);

                            if (sub2[j] != "")
                            {

                                string[] subinfo = FindInfo.findSubjectInfoByID(Convert.ToInt32(sub2[j]));
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
                                i++;
                            }

                            j++;
                           
                        }
                    }
                    if (sub3.Length!=0)
                    {

                        int j = 0;
                        while (j <sub3.Length)
                        {

                            string colsno = "lblSNo" + i.ToString();
                            string colsco = "lblSubCode" + i.ToString();
                            string colsna = "lblSubName" + i.ToString();

                            Label sno = (Label)dli.FindControl(colsno);
                            Label scode = (Label)dli.FindControl(colsco);
                            Label sname = (Label)dli.FindControl(colsna);

                            if (sub3[j] != "")
                            {

                                string[] subinfo = FindInfo.findSubjectInfoByID(Convert.ToInt32(sub3[j]));
                                sno.Text = i.ToString();
                                scode.Text = subinfo[0];
                                sname.Text = subinfo[1];
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

        private bool CTStudent(int srid, out string paper1, out string paper2)
        {
            bool cts = false;
            paper1 = "";
            paper2 = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDECTPaperRecord where SRID='" + srid + "' and Exam='"+Session["ExamCode"].ToString()+"'", con);
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

                if (Session["AdmitSCCode"].ToString() == "ALL")
                {

                    if (Session["AdmitCourse"].ToString() == "ALL")
                    {

                        if (Session["AdmitBatch"].ToString() == "ALL")
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord order by Course";
                        }

                        else
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord where Session='" + Session["AdmitBatch"].ToString() + "' order by Course";
                        }
                    }
                    else
                    {
                        if (Session["AdmitBatch"].ToString() == "ALL")
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord where Course='" + Session["AdmitCourse"].ToString() + "' order by StudentName";
                        }
                        else
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord where Course='" + Session["AdmitCourse"].ToString() + "' and Session='" + Session["AdmitBatch"].ToString() + "' order by StudentName";
                        }
                    }
                }
                else
                {

                    if (Session["AdmitCourse"].ToString() == "ALL")
                    {

                        if (Session["AdmitBatch"].ToString() == "ALL")
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord where StudyCentreCode='" + Session["AdmitSCCode"].ToString() + "' order by Course";
                        }

                        else
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord where StudyCentreCode='" + Session["AdmitSCCode"].ToString() + "' and Session='" + Session["AdmitBatch"].ToString() + "' order by Course";
                        }

                    }

                    else
                    {
                        if (Session["AdmitBatch"].ToString() == "ALL")
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord where StudyCentreCode='" + Session["AdmitSCCode"].ToString() + "' and Course='" + Session["AdmitCourse"].ToString() + "' order by StudentName";
                        }

                        else
                        {
                            cmd.CommandText = "Select * from DDEStudentRecord where StudyCentreCode='" + Session["AdmitSCCode"].ToString() + "' and Course='" + Session["AdmitCourse"].ToString() + "' and Session='" + Session["AdmitBatch"].ToString() + "' order by StudentName";
                        }
                    }

                }
            }
            else
            {
                cmd.CommandText = "Select * from DDEStudentRecord where SRID='" + Session["SRID"].ToString() + "'";
            }

            SqlDataReader dr;

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
            DataColumn dtcol16 = new DataColumn("Exam");
           

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

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
           
            int i = 1;
            while (dr.Read())
            {
               
                 string remark;
                 if (!FindInfo.isDetained(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(),Session["CardType"].ToString(), out remark))
                 {
                   

                     string ayear = FindInfo.findAllExamYear(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(), Session["CardType"].ToString());
                     if (ayear != "" && ayear != "0")
                     {
                         if (Session["CardType"].ToString() == "R")
                         {
                             if (ayear.Length > 1)
                             {
                             
                                 string[] sy = ayear.Split(',');
                                 for (int j = 0; j < sy.Length; j++)
                                 {
                                     DataRow drow = dt.NewRow();
                                     drow["SNo"] = i;
                                     drow["SRID"] = dr["SRID"].ToString();
                                     drow["Exam"] = "EXAMINATION - " + Session["Exam"].ToString() + findCardType(Session["CardType"].ToString());
                                     drow["SCCode"] = FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(dr["SRID"]));
                                     drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                                     //drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                                     drow["SName"] = Convert.ToString(dr["StudentName"]);
                                     drow["FName"] = Convert.ToString(dr["FatherName"]);
                                     drow["Year"] = FindInfo.findAlphaYear(sy[j]).ToUpper();
                                     drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(sy[j]));
                                     drow["Date"] = DateTime.Now.ToString("dd MMMM yyyy");
                                     drow["Batch"] = Convert.ToString(dr["Session"]);
                                     drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(), Session["CardType"].ToString());
                                     string ecity;
                                     drow["ExamCentre"] = FindInfo.findExamCentreForAdmitCard(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(), Session["CardType"].ToString(), out ecity);
                                     drow["ExamCity"] = ecity;
                                     drow["SNo"] = "";
                                     drow["SubjectCode"] = "";
                                     drow["SubjectName"] = "";
                                     dt.Rows.Add(drow);
                                     i = i + 1;                                    

                                 }
                             }
                             else 
                             {
                                 DataRow drow = dt.NewRow();
                                 drow["SNo"] = i;
                                 drow["SRID"] = dr["SRID"].ToString();
                                 drow["Exam"] = "EXAMINATION - " + Session["Exam"].ToString() + findCardType(Session["CardType"].ToString());
                                 drow["SCCode"] = FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(dr["SRID"]));
                                 drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                                 //drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                                 drow["SName"] = Convert.ToString(dr["StudentName"]);
                                 drow["FName"] = Convert.ToString(dr["FatherName"]);
                                 drow["Year"] = FindInfo.findAlphaYear(ayear).ToUpper();
                                 drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ayear));
                                 drow["Date"] = DateTime.Now.ToString("dd MMMM yyyy");
                                 drow["Batch"] = Convert.ToString(dr["Session"]);
                                 drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(), Session["CardType"].ToString());
                                 string ecity;
                                 drow["ExamCentre"] = FindInfo.findExamCentreForAdmitCard(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(), Session["CardType"].ToString(), out ecity);
                                 drow["ExamCity"] = ecity;
                                 drow["SNo"] = "";
                                 drow["SubjectCode"] = "";
                                 drow["SubjectName"] = "";
                                 dt.Rows.Add(drow);
                                 i = i + 1;                       
                                     
                             }
                            

                         }
                         else if (Session["CardType"].ToString() == "B")
                         {
                             DataRow drow = dt.NewRow();
                             drow["SNo"] = i;
                             drow["SRID"] = dr["SRID"].ToString();
                             drow["Exam"] = "EXAMINATION - " + Session["Exam"].ToString() + findCardType(Session["CardType"].ToString());
                             drow["SCCode"] = FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(dr["SRID"]));
                             drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                             //drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                             drow["SName"] = Convert.ToString(dr["StudentName"]);
                             drow["FName"] = Convert.ToString(dr["FatherName"]);
                             drow["Year"] = "NA";
                             drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]),Convert.ToInt32(dr["CYear"]));
                             drow["Date"] = DateTime.Now.ToString("dd MMMM yyyy");
                             drow["Batch"] = Convert.ToString(dr["Session"]);
                             drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(), Session["CardType"].ToString());
                             string ecity;
                             drow["ExamCentre"] = FindInfo.findExamCentreForAdmitCard(Convert.ToInt32(dr["SRID"]), Session["ExamCode"].ToString(), Session["CardType"].ToString(), out ecity);
                             drow["ExamCity"] = ecity;
                             drow["SNo"] = "";
                             drow["SubjectCode"] = "";
                             drow["SubjectName"] = "";
                             dt.Rows.Add(drow);
                             i = i + 1;
                             
                         }
                            

                     }
                 }
                        
                        
            }

            dtlistAdmitCards.DataSource = dt;
            dtlistAdmitCards.DataBind();

            populateSubjects();


            if (i > 1)
            {
                pnlData.Visible = true;
                dtlistAdmitCards.Visible = true;
                pnlMSG.Visible = false;
                pnlPaging.Visible = true;
                lblTotalCards.Text = "Total Cards : " + (i-1).ToString();
            }
            else
            {
                pnlData.Visible = false;
                dtlistAdmitCards.Visible = false;
                pnlPaging.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }


        }

       


        private string findCardType(string moe)
        {
            string fmoe = "";
            if (moe == "R")
            {
                if (Session["ExamCode"].ToString() == "B14")
                {
                    fmoe = " (MAIN EXAMINATION)";
                }
                else
                {
                    fmoe = " (REGULAR)";
                }

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
