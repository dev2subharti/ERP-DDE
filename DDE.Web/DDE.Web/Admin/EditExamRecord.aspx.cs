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
    public partial class EditExamRecord : System.Web.UI.Page
    {
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 38))
            {
                if (!IsPostBack)
                {
               
                    PopulateDDList.populateSySession(ddlistSyllabusSession);                 
                    PopulateDDList.populateExam(ddlistExamination);
                    pnlFeeHead.Visible = true;
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;
                    ViewState["Error"] = "";
                }

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
            if (validEntry())
            {

                btnFind.Visible = false;
                btnUpdate.Visible = true;
                if (ddlistExamMode.SelectedItem.Value == "B")
                {
                    populateBackPapers();
                    pnlBPExamRecord.Visible = true;
                }
              
                PopulateDDList.populateCity(ddlistCity, "UTTAR PRADESH");
              
                fillExamRecord();
                pnlExamRecord.Visible = true;

                
            
            }

            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["Error"] = "You did not select any of given entries";
            }
        }

        private void fillExamRecord()
        {
            if (ddlistExamMode.SelectedItem.Value == "R")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select ExamCentreCity,ExamCentreZone from DDEExamRecord_" + ddlistExamination.SelectedItem.Value + " where SRID='" + lblSRID.Text + "' and Year='" + ddlistYear.SelectedItem.Value + "' and MOE='" + ddlistExamMode.SelectedItem.Value + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ddlistCity.SelectedItem.Selected = false;
                        ddlistZone.SelectedItem.Selected = false;
                        ddlistCity.Items.FindByText(dr[0].ToString()).Selected = true;
                        ddlistZone.Items.FindByText(dr[1].ToString()).Selected = true;

                    }
                }
                else
                {
                    con.Close();
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! no record found !!";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    ViewState["Error"] = "No Record Found";
                }
                con.Close();
            }
            else if (ddlistExamMode.SelectedItem.Value == "B")
            {
                try
                {
                    string[] sublist = findSubjects();



                    string[] sublist1 = sublist[0].Split(',');
                    string[] sublist2 = sublist[1].Split(',');
                    string[] sublist3 = sublist[2].Split(',');


                    for (int k = 0; k < sublist1.Length; k++)
                    {
                        if (sublist1[k] != "")
                        {
                            cb1Year.Items.FindByValue(sublist1[k]).Selected = true;
                        }
                    }


                    for (int k = 0; k < sublist2.Length; k++)
                    {
                        if (sublist2[k] != "")
                        {
                            cb2Year.Items.FindByValue(sublist2[k]).Selected = true;
                        }
                    }


                    for (int k = 0; k < sublist3.Length; k++)
                    {
                        if (sublist3[k] != "")
                        {
                            cb3Year.Items.FindByValue(sublist3[k]).Selected = true;
                        }
                    }
                }
                catch(NullReferenceException nre)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! You Have Selected Wrong Syllabus Session !!";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    ViewState["Error"] = "No Record Found";

                }
            }
        }
        
        private string[] findSubjects()
        {
            string[] subjects = {"","",""};
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select BPSubjects1,BPSubjects2,BPSubjects3 from DDEExamRecord_" + ddlistExamination.SelectedItem.Value + " where SRID='" + lblSRID.Text + "' and MOE='B'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subjects[0] = dr[0].ToString();
                subjects[1] = dr[1].ToString();
                subjects[2] = dr[2].ToString();
            }
            else
            {
                con.Close();
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! no record found !!";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["Error"] = "No Record Found";
            }
            con.Close();

            return subjects;
        }

        
        private void populateBackPapers()
        {
            cb1Year.Items.Clear();
            cb2Year.Items.Clear();
            cb3Year.Items.Clear();

            int cduration = FindInfo.findCourseDuration(FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)));

            if (cduration == 1)
            {
                populatePapers(cb1Year, "1st Year");

                lbl1Year.Visible = true;
                lbl2Year.Visible = false;
                lbl3Year.Visible = false;
                cb1Year.Visible = true;
                cb2Year.Visible = false;
                cb3Year.Visible = false;
            }

            else if (cduration == 2)
            {
                populatePapers(cb1Year, "1st Year");
                populatePapers(cb2Year, "2nd Year");

                lbl1Year.Visible = true;
                lbl2Year.Visible = true;
                lbl3Year.Visible = false;
                cb1Year.Visible = true;
                cb2Year.Visible = true;
                cb3Year.Visible = false;


            }
            else if (cduration == 3)
            {
                populatePapers(cb1Year, "1st Year");
                populatePapers(cb2Year, "2nd Year");
                populatePapers(cb3Year, "3rd Year");

                lbl1Year.Visible = true;
                lbl2Year.Visible = true;
                lbl3Year.Visible = true;
                cb1Year.Visible = true;
                cb2Year.Visible = true;
                cb3Year.Visible = true;

            }


        }

        private void populatePapers(CheckBoxList cblist, string year)
        {
            if (FindInfo.findCourseShortNameByID(Convert.ToInt32(FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)))) == "MBA")
            {
                if (year == "1st Year")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='MBA' and Year='1st Year' and SyllabusSession='" + ddlistSyllabusSession.SelectedItem.Text + "' order by SubjectSNo", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cblist.Items.Add(dr[1].ToString());
                        cblist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                    }

                    con.Close();
                }
                else if (year == "2nd Year")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='" + tbCourse.Text + "' and Year='2nd Year' and SyllabusSession='" + ddlistSyllabusSession.SelectedItem.Text + "' order by SubjectSNo", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cblist.Items.Add(dr[1].ToString());
                        cblist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                    }

                    con.Close();
                }
                else if (year == "3rd Year")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='" + tbCourse.Text + "' and Year='3rd Year' and SyllabusSession='" + ddlistSyllabusSession.SelectedItem.Text + "' order by SubjectSNo", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cblist.Items.Add(dr[1].ToString());
                        cblist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                    }

                    con.Close();
                }
            }

            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='" + tbCourse.Text + "' and Year='" + year + "' and SyllabusSession='" + ddlistSyllabusSession.SelectedItem.Text + "' order by SubjectSNo", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cblist.Items.Add(dr[1].ToString());
                    cblist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }

                con.Close();

            }

        }




        private void populateStudentInfo(int srid)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,StudyCentreCode,Course,Course2Year,Course3Year,CYear from DDEStudentRecord where SRID='" + srid + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                    tbEnNo.Text = dr["EnrollmentNo"].ToString();
                    lblSRID.Text = srid.ToString();
                    tbSName.Text = dr["StudentName"].ToString();
                    tbFName.Text = dr["FatherName"].ToString();
                    tbSCCode.Text = dr["StudyCentreCode"].ToString();
                    string course = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));

                    if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                    {
                        if (Convert.ToInt32(dr["CYear"]) == 1)
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                        }
                        else if (Convert.ToInt32(dr["CYear"]) == 2)
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course2Year"]));
                        }
                        else if (Convert.ToInt32(dr["CYear"]) == 3)
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course3Year"]));
                        }
                    }
                    else
                    {
                        tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                    }

                }

                con.Close();
            }
            catch (FormatException fe)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Specialisation record of this student is not set. </br> Please set specialsation record of this student first and then try again";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["Error"] = "Specialisation Not Set";
            }
            catch (InvalidCastException invc)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Specialisation record of this student is not set. </br> Please set specialsation record of this student first and then try again";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["Error"] = "Specialisation Not Set";
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (tbENo.Text != "")
            {
                if (FindInfo.validENo(tbENo.Text))
                {
                    pnlFeeHead.Visible = false;
                    pnlStudentDetail.Visible = true;
                    pnlDDFee.Visible = true;
                   
                    pnlData.Visible = true;
                    populateStudentInfo(FindInfo.findSRIDByENo(tbENo.Text)); 
                    btnFind.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! this Enrollment No. does not exist </br> Please fill valid Enrollment No first";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry!! Yod did not fill Enrollment No.</br> Please fill Enrollment No first";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
               
            }
        }

       

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            

            if (validEntry())
            {

                Exam.updatePerticularExamCentre(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, ddlistCity.SelectedItem.Text, ddlistZone.SelectedItem.Value, ddlistExamMode.SelectedItem.Value);

                Log.createLogNow("Update Exam Centre", "Changed Exam centre '"+lblExamCentre.Text+"' to '"+ddlistCity.SelectedItem.Text+" "+ddlistZone.SelectedItem.Text+"' for '"+ddlistExamination.SelectedItem.Text+"' exam  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                
                pnlData.Visible = false;
                lblMSG.Text = "Examination centre has been changed successfully !!";
                pnlMSG.Visible = true;
               
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["Error"] = "You did not select any of given entries";
            }
        }


        private int findTotalSelectedSub(CheckBoxList sub1, CheckBoxList sub2, CheckBoxList sub3)
        {
            int totalsub = 0;
            foreach (ListItem sublist in sub1.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }
            foreach (ListItem sublist in sub2.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }
            foreach (ListItem sublist in sub3.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }

            return totalsub;
        }


        private string findBPSubjects(CheckBoxList cblist)
        {
            string subjectlist = "";
            foreach (ListItem sublist in cblist.Items)
            {

                if (sublist.Selected)
                {
                    if (subjectlist == "")
                    {
                        subjectlist = sublist.Value;
                    }
                    else if (subjectlist != "")
                    {
                        subjectlist = subjectlist + "," + sublist.Value;
                    }

                }
            }
            return subjectlist;
        }


        private bool validEntry()
        {
            if (ddlistExamMode.SelectedItem.Value == "R")
            {

                if (ddlistExamMode.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--" || ddlistYear.SelectedItem.Text == "--SELECT ONE--")
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
            else 
            {
                if (ddlistExamMode.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--" || ddlistSyllabusSession.SelectedItem.Text == "--SELECT ONE--")
                {
                    return false;
                }

                else
                {
                    return true;
                }

            }
           
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {

           if(ViewState["Error"].ToString()=="No Record Found")
           {
                
                pnlStudentDetail.Visible = true;
                btnFind.Visible = true;
                pnlExamRecord.Visible = false;
                btnUpdate.Visible = false;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
           }

           else if (ViewState["Error"].ToString() == "You did not select any of given entries")
           {
               pnlStudentDetail.Visible = true;
               btnFind.Visible = true;

               pnlData.Visible = true;
               pnlMSG.Visible = false;
               btnOK.Visible = false;
           }

           else if (ViewState["Error"].ToString() == "Specialisation Not Set")
           {
               tbENo.Text = "";
               pnlStudentDetail.Visible = false;
               btnFind.Visible = false;
               pnlFeeHead.Visible = true;
               pnlData.Visible = true;
               pnlMSG.Visible = false;
               btnOK.Visible = false;
           }

           else
           {
               tbENo.Text = "";
               pnlStudentDetail.Visible = false;
               btnFind.Visible = false;
               pnlFeeHead.Visible = true;
               pnlData.Visible = true;
               pnlMSG.Visible = false;
               btnOK.Visible = false;
           }
           
        }

        protected void ddlistSyllabussession_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateBackPapers();
            fillExamRecord();
        }

        protected void ddlistExamMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistExamMode.SelectedItem.Value == "B")
            {
                lblYear.Visible = false;
                ddlistYear.Visible = false;
                lblSSession.Visible = true;
                ddlistSyllabusSession.Visible = true;
            }
            else
            {
                lblYear.Visible = true;
                ddlistYear.Visible = true;
                lblSSession.Visible =false;
                ddlistSyllabusSession.Visible = false;
            }
        }

        
        protected void ddlistExamination_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
