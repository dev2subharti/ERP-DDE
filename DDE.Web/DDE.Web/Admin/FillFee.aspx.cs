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
    public partial class FillFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 38))
            {
                if (!IsPostBack)
                {
                    
                    if (Request.QueryString["OURID"] != null)
                    {
                       
                        ViewState["ErrorType"] = "";
                        Session["FeeEntry"] = "New";
                        pnlrbl.Visible = false;
                        pnlFeeHead.Visible = false;

                        populateStudentInfo(Convert.ToInt32(Session["OUSRID"]));
                        pnlStudentDetail.Visible = true;

                        setYearList(Convert.ToInt32(Session["OUCourseID"]));
                        ddlistYear.Items.FindByValue(Session["OUYear"].ToString()).Selected = true;
                        ddlistYear.Enabled = false;

                        PopulateDDList.populateSTFeeHead(ddlistFeeHead);
                        ddlistFeeHead.Items.FindByValue("1").Selected = true;
                        ddlistFeeHead.Enabled = false;

                        PopulateDDList.populateExam(ddlistExamination);
                        ddlistExamination.Items.FindByValue(Session["OUExam"].ToString()).Selected = true;
                        //ddlistExamination.Enabled = false;
                        lblExamination.Visible = true;
                        ddlistExamination.Visible = true;

                        if (Session["OUIsMBACourse"].ToString() == "True")
                        {
                            Session["SetSp"] = "Yes";
                            PopulateDDList.populateMBACourses(ddlistMBAS);
                            ddlistMBAS.Items.FindByValue(Session["OUMBASpecialization"].ToString()).Selected = true;
                            lblCID.Text = Session["OUMBASpecialization"].ToString();
                            ddlistMBAS.Enabled = false;
                            pnlMBAS.Visible = true;
                            btnSet.Visible = false;
                        }
                        else
                        {
                            Session["SetSp"] = "No";
                            pnlMBAS.Visible = false;
                        }

                        pnlBPExamRecord.Visible = false;  

                        PopulateDDList.populateAccountSession(ddlistAcountsSession);
                        btnFind.Visible = false;
                        fillFeePanelDetails();
                        setTodayDate();
                        setFeeStatus();

                        ddlistAcountsSession.AutoPostBack = true;

                        ddlistPaymentMode.AutoPostBack = true;

                        tbReqFee.Enabled = false;
                                       
                        pnlDDFee.Visible = true;
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;
                    }
                    else if (Request.QueryString["OERID"] != null)
                    {

                        ViewState["ErrorType"] = "";
                        Session["FeeEntry"] = "New";
                        pnlrbl.Visible = false;
                        pnlFeeHead.Visible = false;

                        populateStudentInfo(Convert.ToInt32(Session["OESRID"]));
                        pnlStudentDetail.Visible = true;


                        setYearList(Convert.ToInt32(Session["OECourseID"]));
                        ddlistYear.Items.FindByValue(Session["OEYear"].ToString()).Selected = true;
                        ddlistYear.Enabled = false;

                        PopulateDDList.populateSTFeeHead(ddlistFeeHead);
                        ddlistFeeHead.Items.FindByValue(Session["OEFHID"].ToString()).Selected = true;
                        ddlistFeeHead.Enabled = false;

                        PopulateDDList.populateExam(ddlistExamination);
                        ddlistExamination.Items.FindByValue(Session["OEExam"].ToString()).Selected = true;
                        ddlistExamination.Enabled = false;

                        
                        Session["SetSp"] = "No";
                        pnlMBAS.Visible = false;

                        if (Session["OEFHID"].ToString() == "3")
                        {
                            populateBackPapers();
                            fillBackPapers();
                            pnlBPExamRecord.Visible = true;
                        }
                        else
                        {
                            pnlBPExamRecord.Visible = false;
                        }
                       

                        PopulateDDList.populateAccountSession(ddlistAcountsSession);
                        btnFind.Visible = false;
                        fillFeePanelDetails();
                        setTodayDate();
                        setFeeStatus();

                        ddlistAcountsSession.AutoPostBack = true;

                        ddlistPaymentMode.AutoPostBack = true;

                        tbReqFee.Enabled = false;

                        pnlDDFee.Visible = true;
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;
                    }
                    else if (Request.QueryString["EFSRID"] != null)
                    {

                        ViewState["ErrorType"] = "";
                        Session["FeeEntry"] = "New";
                        pnlrbl.Visible = false;
                        pnlFeeHead.Visible = false;

                        populateStudentInfo(Convert.ToInt32(Session["EFSRID"]));
                        pnlStudentDetail.Visible = true;


                        setYearList(Convert.ToInt32(Session["EFCID"]));
                        ddlistYear.Items.FindByValue(Session["EFYear"].ToString()).Selected = true;
                        ddlistYear.Enabled = false;

                        PopulateDDList.populateSTFeeHead(ddlistFeeHead);
                        ddlistFeeHead.Items.FindByValue("2").Selected = true;
                        ddlistFeeHead.Enabled = false;

                        PopulateDDList.populateExam(ddlistExamination);
                        ddlistExamination.Items.FindByValue(Session["EFExam"].ToString()).Selected = true;
                        ddlistExamination.Enabled = false;

                        Session["SetSp"] = "No";
                        pnlMBAS.Visible = false;
                       
                        pnlBPExamRecord.Visible = false;                      

                        PopulateDDList.populateAccountSession(ddlistAcountsSession);
                        btnFind.Visible = false;
                        fillFeePanelDetails();
                        setTodayDate();
                        setFeeStatus();

                        ddlistAcountsSession.AutoPostBack = true;

                        ddlistPaymentMode.AutoPostBack = true;

                        tbReqFee.Enabled = false;

                        pnlDDFee.Visible = true;
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;
                    }
                    else
                    {
                        Session["SetSp"] = "No";
                        ViewState["ErrorType"] = "";
                        Session["FeeEntry"] = "New";
                        PopulateDDList.populateSTFeeHead(ddlistFeeHead);

                        PopulateDDList.populateExam(ddlistExamination);
                        pnlrbl.Visible = true;
                        pnlFeeHead.Visible = true;
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (validEntry())
            {
                if (validSCCode())
                {
                    PopulateDDList.populateAccountSession(ddlistAcountsSession);
                    btnFind.Visible = false;
                    fillFeePanelDetails();
                    setTodayDate();
                    setFeeStatus();

                    ddlistAcountsSession.AutoPostBack = true;

                    ddlistPaymentMode.AutoPostBack = true;


                    pnlDDFee.Visible = true;
                    pnlMSG.Visible = false;
                    btnOK.Visible = false;

                    if (ddlistFeeHead.SelectedItem.Value == "16")
                    {
                        tbReqFee.Enabled = true;
                    }
                    else
                    {
                        tbReqFee.Enabled = false;
                    }


                    if (ddlistFeeHead.SelectedItem.Value == "3")
                    {

                        pnlBPExamRecord.Visible = true;
                    }
                    else
                    {

                        pnlBPExamRecord.Visible = false;
                    }

                    if (rblEntryType.SelectedItem.Value == "2")
                    {
                        btnSubmit.Visible = true;
                    }
                    if (rblEntryType.SelectedItem.Value == "1" || rblEntryType.SelectedItem.Value == "3")
                    {
                        btnSubmit.Visible = false;
                    }
                }
                else
                {

                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! The Study Centre of the Student is Canceled";
                    pnlMSG.Visible = true;
                    
                }

                
            }

            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private bool validSCCode()
        {
            bool valid = true;


            if ((ddlistYear.SelectedItem.Value == "1") && (ddlistFeeHead.SelectedItem.Value == "1" || ddlistFeeHead.SelectedItem.Value == "2"))
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select Authorised from DDEStudyCentres where SCCode='" + FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(lblSRID.Text)) + "'", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["Authorised"].ToString() == "True")
                    {
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                    }

                }

                con.Close();
            }
           
           

            return valid;
        }

        private void populateBackPapers()
        {
            cb1Year.Items.Clear();
            cb2Year.Items.Clear();
            cb3Year.Items.Clear();

            cbP1Year.Items.Clear();
            cbP2Year.Items.Clear();
            cbP3Year.Items.Clear();

            int cduration = FindInfo.findCourseDuration(FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)));

            if (cduration == 1)
            
            {
                populatePapers(cb1Year, "1st Year");
                populatePracticals(cbP1Year, "1st Year");
               
                lbl1Year.Visible = true;
                lbl2Year.Visible = false;
                lbl3Year.Visible=false;
                cb1Year.Visible=true;
                cb2Year.Visible=false;
                cb3Year.Visible=false;

                lblP1Year.Visible = true;
                lblP2Year.Visible = false;
                lblP3Year.Visible = false;
                cbP1Year.Visible = true;
                cbP2Year.Visible = false;
                cbP3Year.Visible = false;
            }

            else if (cduration == 2)
            {
                populatePapers(cb1Year, "1st Year");
                populatePracticals(cbP1Year, "1st Year");

                populatePapers(cb2Year, "2nd Year");
                populatePracticals(cbP2Year, "2nd Year");
               
                lbl1Year.Visible = true;
                lbl2Year.Visible = true;
                lbl3Year.Visible = false;
                cb1Year.Visible = true;
                cb2Year.Visible = true;
                cb3Year.Visible = false;

                lblP1Year.Visible = true;
                lblP2Year.Visible = true;
                lblP3Year.Visible = false;
                cbP1Year.Visible = true;
                cbP2Year.Visible = true;
                cbP3Year.Visible = false;
                

            }
            else if (cduration == 3)
            {
                populatePapers(cb1Year, "1st Year");
                populatePracticals(cbP1Year, "1st Year");

                populatePapers(cb2Year, "2nd Year");
                populatePracticals(cbP2Year, "2nd Year");

                populatePapers(cb3Year, "3rd Year");
                populatePracticals(cbP3Year, "3rd Year");

                lbl1Year.Visible = true;
                lbl2Year.Visible = true;
                lbl3Year.Visible = true;
                cb1Year.Visible = true;
                cb2Year.Visible = true;
                cb3Year.Visible = true;

                lblP1Year.Visible = true;
                lblP2Year.Visible = true;
                lblP3Year.Visible = true;
                cbP1Year.Visible = true;
                cbP2Year.Visible = true;
                cbP3Year.Visible = true;

            }
            if (Request.QueryString["OERID"] == null)
            {
                if (Exam.examRecordExist(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, "B"))
                {
                    fillBackPapers();
                }
            }
           
        }

        private void populatePracticals(CheckBoxList cblist, string year)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select PracticalID,PracticalCode from DDEPractical where CourseName='" + tbCourse.Text + "' and Year='" + year + "' and SyllabusSession='" + lblSS.Text + "' order by PracticalSNo", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cblist.Items.Add(dr[1].ToString());
                    cblist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }


            con.Close();
        }

        private void fillBackPapers()
        {
            if (Request.QueryString["OERID"] != null)
            {

                string sub1 = Session["OEBPSubjects1"].ToString();
                string sub2 = Session["OEBPSubjects2"].ToString();
                string sub3 = Session["OEBPSubjects3"].ToString();
                string prac1 = Session["OEBPPracticals1"].ToString();
                string prac2 = Session["OEBPPracticals2"].ToString();
                string prac3 = Session["OEBPPracticals3"].ToString();

                if (sub1 != "")
                {
                    string[] sa = sub1.Split(',');

                    for (int i = 0; i < sa.Length; i++)
                    {
                        cb1Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                        cb1Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                    }

                }
                if (sub2 != "")
                {
                    string[] sa = sub2.Split(',');

                    for (int i = 0; i < sa.Length; i++)
                    {
                        cb2Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                        cb2Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                    }

                }
                if (sub3 != "")
                {
                    string[] sa = sub3.Split(',');

                    for (int i = 0; i < sa.Length; i++)
                    {
                        cb3Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                        cb3Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                    }

                }


                if (prac1 != "")
                {
                    string[] sa = prac1.Split(',');

                    for (int i = 0; i < sa.Length; i++)
                    {
                        cbP1Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                        cbP1Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                    }

                }
                if (prac2 != "")
                {
                    string[] sa = prac2.Split(',');

                    for (int i = 0; i < sa.Length; i++)
                    {
                        cbP2Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                        cbP2Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                    }

                }
                if (prac3 != "")
                {
                    string[] sa = prac3.Split(',');

                    for (int i = 0; i < sa.Length; i++)
                    {
                        cbP3Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                        cbP3Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                    }
                }

                    
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select BPSubjects1,BPSubjects2,BPSubjects3,BPPracticals1,BPPracticals2,BPPracticals3 from DDEExamRecord_" + ddlistExamination.SelectedItem.Value + " where SRID='" + lblSRID.Text + "' and MOE='B'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string sub1 = dr[0].ToString();
                    string sub2 = dr[1].ToString();
                    string sub3 = dr[2].ToString();
                    string prac1 = dr[3].ToString();
                    string prac2 = dr[4].ToString();
                    string prac3 = dr[5].ToString();

                    if (sub1 != "")
                    {
                        string[] sa = sub1.Split(',');

                        for (int i = 0; i < sa.Length; i++)
                        {
                            cb1Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                            cb1Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                        }

                    }
                    if (sub2 != "")
                    {                        
                        string[] sa = sub2.Split(',');

                        for (int i = 0; i < sa.Length; i++)
                        {
                            cb2Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                            cb2Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                        }

                    }
                    if (sub3 != "")
                    {
                        string[] sa = sub3.Split(',');

                        for (int i = 0; i < sa.Length; i++)
                        {
                            cb3Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                            cb3Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                        }

                    }


                    if (prac1 != "")
                    {
                        string[] sa = prac1.Split(',');

                        for (int i = 0; i < sa.Length; i++)
                        {
                            cbP1Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                            cbP1Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                        }

                    }
                    if (prac2 != "")
                    {
                        string[] sa = prac2.Split(',');

                        for (int i = 0; i < sa.Length; i++)
                        {
                            cbP2Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                            cbP2Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                        }

                    }
                    if (prac3 != "")
                    {
                        string[] sa = prac3.Split(',');

                        for (int i = 0; i < sa.Length; i++)
                        {
                            cbP3Year.Items.FindByValue(sa[i].ToString()).Selected = true;
                            cbP3Year.Items.FindByValue(sa[i].ToString()).Enabled = false;

                        }

                    }


                }

                con.Close();
            }
        }

        private void populatePapers(CheckBoxList cblist, string year)
        {
            if (FindInfo.findCourseShortNameByID(Convert.ToInt32(FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)))) == "MBA")
            {
                if (year == "1st Year")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='MBA' and Year='1st Year' and SyllabusSession='" + lblSS.Text + "' order by SubjectSNo", con);
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
                    SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='" + tbCourse.Text + "' and Year='2nd Year' and SyllabusSession='" + lblSS.Text + "' order by SubjectSNo", con);
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
                    SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='" + tbCourse.Text + "' and Year='3rd Year' and SyllabusSession='" + lblSS.Text + "' order by SubjectSNo", con);
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
                SqlCommand cmd = new SqlCommand("select SubjectID,SubjectCode from DDESubject where CourseName='" + tbCourse.Text + "' and Year='" + year + "' and SyllabusSession='" + lblSS.Text + "' order by SubjectSNo", con);
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

        private void setTodayDate()
        {
            ddlistDDDay.SelectedItem.Selected = false;
            ddlistDDMonth.SelectedItem.Selected = false;
            ddlistDDYear.SelectedItem.Selected = false;
            ddlistDDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistFRDDay.SelectedItem.Selected = false;
            ddlistFRDMonth.SelectedItem.Selected = false;
            ddlistFRDYear.SelectedItem.Selected = false;
            ddlistFRDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistFRDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistFRDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        private void setFeeStatus()
        {
            if (ddlistYear.SelectedItem.Text != "--SELECT ONE--")
            {
                int totalbpsub = 0;
                if (ddlistFeeHead.SelectedItem.Value == "3")
                {
                    totalbpsub = findSelectedSub(cb1Year, cb2Year, cb3Year, cbP1Year, cbP2Year, cbP3Year);
                }
                if (validEntry())
                {
                    string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                   
                    int reqfee = Accounts.findRequiredFee(Convert.ToInt32(lblSRID.Text), FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), totalbpsub, FindInfo.findBatchBySRID(Convert.ToInt32(lblSRID.Text)), frdate);
                    int paidfee = Accounts.findPreviousPaidFee(Convert.ToInt32(lblSRID.Text), FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value);
                    int duefee = (reqfee - paidfee);
                   
                    tbReqFee.Text = reqfee.ToString();
                    tbPaidFee.Text = paidfee.ToString();
                    tbDueFee.Text = duefee.ToString();
                }
            }
        }

        private int findSelectedSub(CheckBoxList sub1, CheckBoxList sub2, CheckBoxList sub3, CheckBoxList prac1, CheckBoxList prac2, CheckBoxList prac3)
        {
            int totalsub = 0;

            if (Request.QueryString["OERID"] != null)
            {
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



                foreach (ListItem sublist in prac1.Items)
                {

                    if (sublist.Selected)
                    {
                        totalsub = totalsub + 1;
                    }
                }
                foreach (ListItem sublist in prac2.Items)
                {

                    if (sublist.Selected)
                    {
                        totalsub = totalsub + 1;
                    }
                }
                foreach (ListItem sublist in prac3.Items)
                {

                    if (sublist.Selected)
                    {
                        totalsub = totalsub + 1;
                    }
                }
            }
            else
            {
                foreach (ListItem sublist in sub1.Items)
                {

                    if (sublist.Selected && sublist.Enabled)
                    {
                        totalsub = totalsub + 1;
                    }
                }
                foreach (ListItem sublist in sub2.Items)
                {

                    if (sublist.Selected && sublist.Enabled)
                    {
                        totalsub = totalsub + 1;
                    }
                }
                foreach (ListItem sublist in sub3.Items)
                {

                    if (sublist.Selected && sublist.Enabled)
                    {
                        totalsub = totalsub + 1;
                    }
                }



                foreach (ListItem sublist in prac1.Items)
                {

                    if (sublist.Selected && sublist.Enabled)
                    {
                        totalsub = totalsub + 1;
                    }
                }
                foreach (ListItem sublist in prac2.Items)
                {

                    if (sublist.Selected && sublist.Enabled)
                    {
                        totalsub = totalsub + 1;
                    }
                }
                foreach (ListItem sublist in prac3.Items)
                {

                    if (sublist.Selected && sublist.Enabled)
                    {
                        totalsub = totalsub + 1;
                    }
                }
            }

            return totalsub;
        }
       
        private void fillFeePanelDetails()
        {
            if (ddlistPaymentMode.SelectedItem.Value != "0")
            {
                if (ddlistPaymentMode.SelectedItem.Value == "1")
                {
                    

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;
                  

                    
                }
                else if (ddlistPaymentMode.SelectedItem.Value == "2")
                {
                    

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;
                   

                   
                }
                else if (ddlistPaymentMode.SelectedItem.Value == "3")
                {
                    
                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;

                   
                  

                   

                }
                else if (ddlistPaymentMode.SelectedItem.Value == "4")
                {
                   
                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;

                  
                  



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "5")
                {
                    
                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = true;
                    tbIBN.Visible = true;


                }
                else if (ddlistPaymentMode.SelectedItem.Value == "6")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = true;
                    tbIBN.Visible = true;


                }

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Please select any one payment mode";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void populateStudentInfo(int srid)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select ApplicationNo,SyllabusSession,EnrollmentNo,StudentName,FatherName,StudyCentreCode,Course,Course2Year,Course3Year,CYear from DDEStudentRecord where SRID='" + srid + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {

                        dr.Read();
                    
                        imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                        lblSS.Text = dr["SyllabusSession"].ToString();
                        if (rblMode.SelectedItem.Value == "1")
                        {
                            lblNo1.Text = "Enrollment No";
                            tbEnNo.Text = dr["EnrollmentNo"].ToString();
                        }
                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            lblNo1.Text = "Application No";
                            tbEnNo.Text = dr["ApplicationNo"].ToString();
                        }

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
                                lblCID.Text = dr["Course"].ToString();
                            }
                            else if (Convert.ToInt32(dr["CYear"]) == 2)
                            {
                                tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course2Year"]));
                                lblCID.Text = dr["Course2Year"].ToString();
                            }
                            else if (Convert.ToInt32(dr["CYear"]) == 3)
                            {
                                tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course3Year"]));
                                lblCID.Text = dr["Course3Year"].ToString();
                            }
                        }
                        else
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                            lblCID.Text = dr["Course"].ToString();
                        }
                        tbYear.Text = FindInfo.findAlphaYear(Convert.ToString(dr["CYear"])).ToUpper();
                    

                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! No record found!!";
                    pnlMSG.Visible = true;
                   
                }

                con.Close();

                setYearList(Convert.ToInt32(lblCID.Text));

               
            }
            catch (FormatException fe)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Specialisation record of this student is not set. </br> Please set specialsation record of this student first and then try again";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["ErrorType"] = "Specialisation Not Set";
            }
            catch (InvalidCastException invc)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Specialisation record of this student is not set. </br> Please set specialsation record of this student first and then try again";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["ErrorType"] = "Specialisation Not Set";
            }

            if (Request.QueryString["EFSRID"] != null)
            {
                if (Session["EFENo"].ToString() != tbEnNo.Text)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! You can not open this page direcltly.";
                    pnlMSG.Visible = true;
                }

            }
        }

        private void setYearList(int cid)
        {
           int duration = FindInfo.findCourseDuration(cid);
           ddlistYear.Items.Clear();

           if (duration == 1)
           {
               ddlistYear.Items.Add("--SELECT ONE--");
               ddlistYear.Items.FindByText("--SELECT ONE--").Value = "5";

               ddlistYear.Items.Add("1ST YEAR");
               ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

               ddlistYear.Items.Add("NOT APPLICABLE");
               ddlistYear.Items.FindByText("NOT APPLICABLE").Value = "0";

           }
           else if (duration == 2)
           {
               ddlistYear.Items.Add("--SELECT ONE--");
               ddlistYear.Items.FindByText("--SELECT ONE--").Value = "5";

               ddlistYear.Items.Add("1ST YEAR");
               ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

               ddlistYear.Items.Add("2ND YEAR");
               ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

               ddlistYear.Items.Add("NOT APPLICABLE");
               ddlistYear.Items.FindByText("NOT APPLICABLE").Value = "0";
           }
           else if (duration == 3)
           {
               ddlistYear.Items.Add("--SELECT ONE--");
               ddlistYear.Items.FindByText("--SELECT ONE--").Value = "5";

               ddlistYear.Items.Add("1ST YEAR");
               ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

               ddlistYear.Items.Add("2ND YEAR");
               ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

               ddlistYear.Items.Add("3RD YEAR");
               ddlistYear.Items.FindByText("3RD YEAR").Value = "3";

               ddlistYear.Items.Add("NOT APPLICABLE");
               ddlistYear.Items.FindByText("NOT APPLICABLE").Value = "0";
           }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (tbENo.Text!="")
            {
                if (validENo(tbENo.Text))
                {
                   
                    if (Session["FeeEntry"].ToString() == "New")
                    {
                        pnlFeeHead.Visible = false;
                        if (rblMode.SelectedItem.Value == "1")
                        {
                            populateStudentInfo(FindInfo.findSRIDByENo(tbENo.Text));
                        }
                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            populateStudentInfo(FindInfo.findSRIDByANo(tbENo.Text));
                        }

                        pnlStudentDetail.Visible = true;
                        btnFind.Visible = true;
                    }

                    else if (Session["FeeEntry"].ToString() == "Same")
                    {
                        pnlFeeHead.Visible = false;
                        if (rblMode.SelectedItem.Value == "1")
                        {
                            populateStudentInfo(FindInfo.findSRIDByENo(tbENo.Text));
                        }
                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            populateStudentInfo(FindInfo.findSRIDByANo(tbENo.Text));
                        }                 
                        pnlStudentDetail.Visible = true;
                        setFeeStatus();
                        tbStudentAmount.Text = "";

                        btnFind.Visible = false;
                        btnSubmit.Visible = true;
                        pnlDDFee.Visible = true;
                      
                        pnlData.Visible = true;
                        
                        
                    }

                 
                    rblMode.Visible = false;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! this " + rblMode.SelectedItem.Text + " does not exist </br> Please fill valid " + rblMode.SelectedItem.Text + " first";
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

        private bool validENo(string no)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            if (rblMode.SelectedItem.Value=="1")
            {
                cmd.CommandText = "select SRID from DDEStudentRecord where EnrollmentNo='" + no + "'";
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                cmd.CommandText = "select SRID from DDEStudentRecord where ApplicationNo='" + no + "'";
            }

            bool exist = false;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                exist = true;

            }

            con.Close();

            return exist;
        }

        protected void ddlistPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
           
              fillFeePanelDetails();
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string error;
            string examrecorderror;


            if (validEntry()|| ddlistAcountsSession.SelectedItem.Text!="--SELECT ONE--")
            {

                try
                {
                    int totalbpsub = 0;
                    if (ddlistFeeHead.SelectedItem.Value == "3")
                    {
                        totalbpsub = findTotalSelectedSub(cb1Year, cb2Year, cb3Year, cbP1Year, cbP2Year, cbP3Year);
                    }
                   

                    string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                    int count=0;
                    int iid=0;

                    if (ddlistIns.Visible == true)
                    {
                        count = 2;
                        iid = Convert.ToInt32(ddlistIns.SelectedItem.Value);
                    }
                    else if (ddlistIns.Visible == false)
                    {
                        count = 1;
                        if (rblEntryType.SelectedItem.Value == "1" || rblEntryType.SelectedItem.Value == "3")
                        {
                            iid = Convert.ToInt32(lblIID.Text);
                        }
                        
                    }


                    if (Accounts.validFee(Convert.ToInt32(lblSRID.Text), FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)), Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, totalbpsub, Convert.ToInt32(Session["ERID"]), FindInfo.findBatchBySRID(Convert.ToInt32(lblSRID.Text)), frdate,Convert.ToInt32(rblEntryType.SelectedItem.Value),count,iid,tbSCCode.Text, out error))
                    {
                        string rollno;


                        Accounts.fillFee(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)), Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, frdate, Convert.ToInt32(rblEntryType.SelectedItem.Value));
                        
                        Log.createLogNow("Fee Submit", "Filled"+ddlistFeeHead.SelectedItem.Text+" Fee of a student with Enrollment No '" + tbEnNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                      
                         if (ddlistFeeHead.SelectedItem.Value == "1")
                         {
                             if (Request.QueryString["OURID"] != null)
                             {
                                 updateOnlineContRecord(Convert.ToInt32(Request.QueryString["OURID"]), true);
                             }

                             if (Session["SetSp"].ToString() == "Yes")
                             {
                                 setSpec(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblCID.Text));
                                 Session["SetSp"] = "No";
                             }

                             if (rblEntryType.SelectedItem.Value == "1" && (!FindInfo.isRegisteredForSLM(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value))))
                             {
                                 
                                int res= registerForSLM(Convert.ToInt32(lblSRID.Text), tbSCCode.Text,Convert.ToInt32(lblCID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value));
                                if (res == 1)
                                {
                                    if (FindInfo.findCYearBySRID(Convert.ToInt32(lblSRID.Text)) != Convert.ToInt32(ddlistYear.SelectedItem.Value))
                                    {
                                        lblMSG.Text = "Fee has been submitted successfully !!<br/><span style='color:Green'>Student is registered for issuing SLM. </span><br/>Do you want to update 'Current Year' of student to ' " + ddlistYear.SelectedItem.Text + " '";
                                        pnlData.Visible = false;
                                        pnlMSG.Visible = true;
                                        btnOK.Visible = false;
                                        btnSameFee.Visible = false;
                                        btnNewFee.Visible = false;
                                        btnYes.Visible = true;
                                        btnNo.Visible = true;
                                    }
                                    else
                                    {
                                        lblMSG.Text = "Fee has been submitted successfully !!<br/><span style='color:Green'>Student is registered for issuing SLM. </span>";
                                        pnlData.Visible = false;
                                        pnlMSG.Visible = true;
                                        btnOK.Visible = false;
                                        btnSameFee.Visible = true;
                                        btnNewFee.Visible = true;
                                        btnYes.Visible = false;
                                        btnNo.Visible = false;
                                    }
                                }
                                else
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = "Sorry!! Fee is submitted successfully but <br/><span style='color:Red'> SLM Record is not entered. Please contact ERP Developer</span>";
                                    pnlMSG.Visible = true;
                                    btnOK.Visible = true;
                                    btnSameFee.Visible = false;
                                    btnNewFee.Visible = false;
                                    btnYes.Visible = false;
                                    btnNo.Visible = false;
                                }
                             }
                             else
                             {
                                 if (FindInfo.findCYearBySRID(Convert.ToInt32(lblSRID.Text)) != Convert.ToInt32(ddlistYear.SelectedItem.Value))
                                 {
                                     lblMSG.Text = "Do you want to update 'Current Year' of student to ' " + ddlistYear.SelectedItem.Text + " '";
                                     pnlData.Visible = false;
                                     pnlMSG.Visible = true;
                                     btnOK.Visible = false;
                                     btnSameFee.Visible = false;
                                     btnNewFee.Visible = false;
                                     btnYes.Visible = true;
                                     btnNo.Visible = true;
                                 }
                                 else
                                 {
                                     lblMSG.Text = "Fee has been submitted successfully !!";
                                     pnlData.Visible = false;
                                     pnlMSG.Visible = true;
                                     btnOK.Visible = false;
                                     btnSameFee.Visible = true;
                                     btnNewFee.Visible = true;
                                     btnYes.Visible = false;
                                     btnNo.Visible = false;
                                 }
                             }

                         }
                        

                         else if (ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "26")
                         {
                             int ercounter = 0;
                          
                             if (Exam.examRecordExist(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, "R"))
                             {
                                 Exam.updateExamCentre(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "R");
                                 Log.createLogNow("Updated Exam Record", "Filled 'REGULAR' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                 lblMSG.Text = "Fee has been submitted successfully !!";
                             }
                             else
                             {
                                 Exam.fillExamRecord(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), "", ddlistExamination.SelectedItem.Value, "", "", "", "", "", "", "", FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "R", out rollno, out ercounter, out examrecorderror);
                                 if (examrecorderror=="")
                                 {
                                     Log.createLogNow("Filled Exam Record", "Filled 'REGULAR' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                                     if (ddlistExamination.SelectedItem.Value == "B12" || ddlistExamination.SelectedItem.Value == "B13")
                                     {

                                         lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : " + ercounter;
                                     }
                                     else if (ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17" || ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "A19" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10" || ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11" || ddlistExamination.SelectedItem.Value == "W12" || ddlistExamination.SelectedItem.Value == "H10" || ddlistExamination.SelectedItem.Value == "G10" || ddlistExamination.SelectedItem.Value == "H11")
                                     {

                                         lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : " + ddlistExamination.SelectedItem.Value + "_" + ercounter;
                                     }
                                     else
                                     {
                                         lblMSG.Text = "Fee and Examination Centre has been submitted successfully !!";
                                     }
                                 }
                                 else
                                 {
                                     lblMSG.Text = examrecorderror;
                                 }

                             }
                             if (Request.QueryString["OERID"] != null)
                             {
                                 updateOnlineExamFormRecord(Convert.ToInt32(Request.QueryString["OERID"]), true);
                             }
                             pnlData.Visible = false;
                             pnlMSG.Visible = true;
                             btnOK.Visible = false;
                             btnSameFee.Visible = true;
                             btnNewFee.Visible = true;
                             btnYes.Visible = false;
                             btnNo.Visible = false;

                         }
                         else if (ddlistFeeHead.SelectedItem.Value == "3")
                         {
                             int ercounter = 0;
                            
                             string sub1 = findBPSubjects(cb1Year);
                             string sub2 = findBPSubjects(cb2Year);
                             string sub3 = findBPSubjects(cb3Year);

                             string prac1 = findBPSubjects(cbP1Year);
                             string prac2 = findBPSubjects(cbP2Year);
                             string prac3 = findBPSubjects(cbP3Year);

                             if (Exam.examRecordExist(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, "B"))
                             {

                                 Exam.updateBPRecord(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value, sub1, sub2, sub3,prac1,prac2,prac3,"", FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "B", out rollno, out ercounter);
                                 Log.createLogNow("Filled Exam Record", "Filled 'BACK PAPER' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                 if (rollno != "")
                                 {
                                     if (ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17" || ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "A19" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10" || ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11" || ddlistExamination.SelectedItem.Value == "W12" || ddlistExamination.SelectedItem.Value == "H10" || ddlistExamination.SelectedItem.Value == "G10" || ddlistExamination.SelectedItem.Value == "H11")
                                     {
                                         lblMSG.Text = "Fee and Back Paper Record has been submitted successfully !! <br/>Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : " + ddlistExamination.SelectedItem.Value + "_" + ercounter;
                                     }
                                     else
                                     {
                                         lblMSG.Text = "Fee and Back Paper Record has been submitted successfully !! <br/>Allotted Roll No. is : " + rollno;
                                     }
                                 }
                                 else
                                 {
                                     lblMSG.Text = "Fee and Back Paper Record has been submitted successfully !!";
                                 }
                             }
                             else
                             {
                                 Exam.fillExamRecord(Convert.ToInt32(lblSRID.Text), 3, Convert.ToInt32(ddlistYear.SelectedItem.Value), "", ddlistExamination.SelectedItem.Value, sub1, sub2, sub3, prac1, prac2, prac3, "", FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "B", out rollno, out ercounter, out examrecorderror);
                                 if (examrecorderror == "")
                                 {
                                     Log.createLogNow("Filled Exam Record", "Filled 'BACK PAPER' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                     if (ddlistExamination.SelectedItem.Value == "B12" || ddlistExamination.SelectedItem.Value == "B13")
                                     {
                                         lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : " + ercounter;
                                     }
                                     if (ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17" || ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "A19" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10" || ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11" || ddlistExamination.SelectedItem.Value == "W12" || ddlistExamination.SelectedItem.Value == "H10" || ddlistExamination.SelectedItem.Value == "G10" || ddlistExamination.SelectedItem.Value == "H11")
                                     {
                                         lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : " + ddlistExamination.SelectedItem.Value + "_" + ercounter;
                                     }
                                     else
                                     {
                                         lblMSG.Text = "Fee and Back Paper Record has been submitted successfully !!";
                                     }
                                 }
                                 else
                                 {
                                     lblMSG.Text = examrecorderror;
                                 }
                             }

                             if (Request.QueryString["OERID"] != null)
                             {
                                 updateOnlineExamFormRecord(Convert.ToInt32(Request.QueryString["OERID"]), true);
                             }
                             pnlData.Visible = false;
                             pnlMSG.Visible = true;
                             btnOK.Visible = false;
                             btnSameFee.Visible = true;
                             btnNewFee.Visible = true;
                             btnYes.Visible = false;
                             btnNo.Visible = false;

                         }
                         else 
                         {
                             lblMSG.Text = "Fee has been submitted successfully!!";
                             pnlData.Visible = false;
                             pnlMSG.Visible = true;
                             btnOK.Visible = false;
                             btnSameFee.Visible = true;
                             btnNewFee.Visible = true;
                             btnYes.Visible = false;
                             btnNo.Visible = false;
                         }
                                              
                       
                    }

                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = error;
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        btnSameFee.Visible = false;
                        btnNewFee.Visible = false;
                        btnYes.Visible = false;
                        btnNo.Visible = false;
                    }
                }

                catch (FormatException ex)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! You din not entered amount in numeric form";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    btnSameFee.Visible = false;
                    btnNewFee.Visible = false;
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                }
            }

            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                btnSameFee.Visible = false;
                btnNewFee.Visible = false;
                btnYes.Visible = false;
                btnNo.Visible = false;
            }
        }

        private void updateOnlineExamFormRecord(int oerid,bool enrolled)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEOnlineExamRecord set Enrolled=@Enrolled,EnrolledOn=@EnrolledOn where OERID='" + oerid + "'", con);

            cmd.Parameters.AddWithValue("@Enrolled", enrolled);
            cmd.Parameters.AddWithValue("@EnrolledOn", DateTime.Now.ToString());


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }   

        private void updateOnlineContRecord(int ourid, bool enrolled)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEOnlineContinuationRecord set Enrolled=@Enrolled,EnrolledOn=@EnrolledOn where OURID='" + ourid + "' ", con);

            cmd.Parameters.AddWithValue("@Enrolled",enrolled);
            cmd.Parameters.AddWithValue("@EnrolledOn", DateTime.Now.ToString());


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void setSpec(int srid, int cid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set Course=@Course,Course2Year=@Course2Year,Course3Year=@Course3Year where SRID='" +srid + "' ", con);

            cmd.Parameters.AddWithValue("@Course", 76);
            cmd.Parameters.AddWithValue("@Course2Year", cid);
            cmd.Parameters.AddWithValue("@Course3Year", cid);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("insert into DDESpecialisationRecord values(@SRID,@1Y,@2Y,@3Y,@AAuthority,@ChangedBy,@TimeOfChange)", con1);

            string cn=FindInfo.findCourseNameByID(cid);
            cmd1.Parameters.AddWithValue("@SRID", lblSRID.Text);
            cmd1.Parameters.AddWithValue("@1Y", "MBA");
            cmd1.Parameters.AddWithValue("@2Y",cn);
            cmd1.Parameters.AddWithValue("@3Y", cn);
            cmd1.Parameters.AddWithValue("@AAuthority", "");
            cmd1.Parameters.AddWithValue("@ChangedBy", Convert.ToInt32(Session["ERID"]));
            cmd1.Parameters.AddWithValue("@TimeOfChange", DateTime.Now.ToString());

            cmd1.Connection = con1;
            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();

            Log.createLogNow("Set Specialisation", "Specialisation was set with 1 Y-'MBA',2 Y-'" + FindInfo.findCourseNameByID(cid) + "',3 Y-'" + FindInfo.findCourseNameByID(cid) + "' with Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
        }

        private int registerForSLM(int srid,string sccode,int cid,int year)
        {
            int res = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESLMIssueRecord values(@SRID,@SCCode,@CID,@Year,@TOR,@LNo)", con);

            cmd.Parameters.AddWithValue("@SRID",srid);
            cmd.Parameters.AddWithValue("@SCCode", sccode);
            cmd.Parameters.AddWithValue("@CID", cid);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());         
            cmd.Parameters.AddWithValue("@LNo", 0);
           

            con.Open();
            res= cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        private int findTotalSelectedSub(CheckBoxList sub1, CheckBoxList sub2, CheckBoxList sub3, CheckBoxList prac1, CheckBoxList prac2, CheckBoxList prac3)
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



            foreach (ListItem sublist in prac1.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }
            foreach (ListItem sublist in prac2.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }
            foreach (ListItem sublist in prac3.Items)
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

            if (ddlistFeeHead.SelectedItem.Value == "1" || ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3" || ddlistFeeHead.SelectedItem.Value == "4" || ddlistFeeHead.SelectedItem.Value == "5" || ddlistFeeHead.SelectedItem.Value == "7" || ddlistFeeHead.SelectedItem.Value == "13" || ddlistFeeHead.SelectedItem.Value == "15" || ddlistFeeHead.SelectedItem.Value == "16" || ddlistFeeHead.SelectedItem.Value == "17" || ddlistFeeHead.SelectedItem.Value == "25" || ddlistFeeHead.SelectedItem.Value == "26" || ddlistFeeHead.SelectedItem.Value == "39")
            {
                if (Request.QueryString["OURID"] != null || Request.QueryString["OERID"] != null || Request.QueryString["EFSRID"] != null)
                {
                    if (ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" ||  ddlistYear.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--")
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
                    if (ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistYear.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--")
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }

                }
            }
            else if (ddlistFeeHead.SelectedItem.Value == "3")
            {
                if (ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--")
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
                if (ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistYear.SelectedItem.Text == "--SELECT ONE--")
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
            if (ViewState["ErrorType"].ToString() == "Specialisation Not Set")
            {
                tbENo.Text = "";
                pnlStudentDetail.Visible = false;
                btnFind.Visible = false;
                pnlFeeHead.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else if (ViewState["ErrorType"].ToString() == "Did not select year")
            {
              
                pnlStudentDetail.Visible = true;
                btnFind.Visible = false;
                pnlFeeHead.Visible = false;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
        }

        protected void ddlistFeeHead_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlistFeeHead.SelectedItem.Value == "1")
            {
                 string exam;
                 if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                 {
                     if (exam == "NA")
                     {
                         pnlData.Visible = false;
                         lblMSG.Text = "Sorry !! Examination is not set on Course fee of this year. Please set Examination on Course fee of this year and then fill this from.";
                         pnlMSG.Visible = true;
                         btnOK.Visible = true;
                         ddlistExamination.Enabled = false;
                         ViewState["ErrorType"] = "Did not select year";
                     }
                     else
                     {
                         ddlistExamination.SelectedItem.Selected = false;
                         ddlistExamination.Items.FindByValue(exam).Selected = true;
                         ddlistExamination.Enabled = false;
                         btnFind.Visible = true;
                     }                        
                    
                 }
                 else
                 {
                     
                     ddlistExamination.Enabled = true;
                     btnFind.Visible = true;
                 }

                 if (ddlistYear.SelectedItem.Value == "2" || ddlistYear.SelectedItem.Value == "3")
                 {
                     if (Convert.ToInt32(lblCID.Text) == 76)
                     {
                         pnlStudentDetail.Visible = false;
                         btnFind.Visible = false;
                         polulateMBAS();
                         pnlMBAS.Visible = true;
                     }
                 }
            }

            else if (ddlistFeeHead.SelectedItem.Value == "2")
            {
                if (ddlistYear.SelectedItem.Text != "--SELECT ONE--")
                {
                    //if (ddlistYear.SelectedItem.Text == tbYear.Text)
                    //{
                        string exam;
                        if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                        {
                            if (exam != "NA" && exam != "")
                            {
                                setFeeStatus();
                                btnFind.Visible = true;
                                pnlBPExamRecord.Visible = false;
                                ddlistExamination.SelectedItem.Selected = false;
                                ddlistExamination.Items.FindByValue(exam).Selected = true;
                                ddlistExamination.Enabled = false;

                                lblYear.Visible = true;
                                ddlistYear.Visible = true;
                                lblExamination.Visible = true;
                                ddlistExamination.Visible = true;

                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! Examination is not set on Course fee of this year. Please set Examination on Course fee of this year and then fill this from.";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;
                                ddlistExamination.Enabled = false;
                                ViewState["ErrorType"] = "Did not select year";
                            }
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !!Course Fee is not paid for this year. Please fill course fee first and then fill this exam from.";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                            ViewState["ErrorType"] = "Did not select year";
                            ddlistExamination.Enabled = false;
                        }
                   // }
                    //else
                    //{
                    //    pnlData.Visible = false;
                    //    lblMSG.Text = "Sorry !!Current Year of Student does not match with the year you have selected for exam. Please update the current year and then fill this exam form.";
                    //    pnlMSG.Visible = true;
                    //    btnOK.Visible = true;
                    //    ViewState["ErrorType"] = "Did not select year";
                    //}
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please select year before selecting fee head";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    ViewState["ErrorType"] = "Did not select year";
                }
            }
            else if (ddlistFeeHead.SelectedItem.Value == "26")
            {
                if (ddlistYear.SelectedItem.Text != "--SELECT ONE--")
                {
                    string exam;
                    if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                    {
                        if (exam != "NA" && exam != "")
                        {
                            setFeeStatus();
                            btnFind.Visible = true;
                            pnlBPExamRecord.Visible = false;
                            ddlistExamination.SelectedItem.Selected = false;
                            ddlistExamination.Items.FindByValue(exam).Selected = true;
                            ddlistExamination.Enabled = false;

                            lblYear.Visible = true;
                            ddlistYear.Visible = true;
                            lblExamination.Visible = true;
                            ddlistExamination.Visible = true;

                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! Examination is not set on Course fee of this year. Please set Examination on Course fee of this year and then fill this from.";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                            ddlistExamination.Enabled = false;
                            ViewState["ErrorType"] = "Did not select year";
                        }
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !!Course Fee is not paid for this year. Please fill course fee first and then fill this exam from.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        ViewState["ErrorType"] = "Course Fee Not Paid";
                        ddlistExamination.Enabled = false;
                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please select year before selecting fee head";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    ViewState["ErrorType"] = "Did not select year";
                }
            }
            else if (ddlistFeeHead.SelectedItem.Value == "3")
            {
                lblYear.Visible = false;
                ddlistYear.Visible = false;
                    
                btnFind.Visible = true;
            }
            else
            {
                lblYear.Visible = true;
                ddlistYear.Visible = true;           
                pnlBPExamRecord.Visible = false;             
                setFeeStatus();
                btnFind.Visible = true;
            }

            if (ddlistFeeHead.SelectedItem.Value == "1" || ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3" || ddlistFeeHead.SelectedItem.Value == "4" || ddlistFeeHead.SelectedItem.Value == "5" || ddlistFeeHead.SelectedItem.Value == "7" || ddlistFeeHead.SelectedItem.Value == "13" || ddlistFeeHead.SelectedItem.Value == "15" || ddlistFeeHead.SelectedItem.Value == "16" || ddlistFeeHead.SelectedItem.Value == "17" || ddlistFeeHead.SelectedItem.Value == "24" || ddlistFeeHead.SelectedItem.Value == "25" || ddlistFeeHead.SelectedItem.Value == "26" || ddlistFeeHead.SelectedItem.Value == "35" || ddlistFeeHead.SelectedItem.Value == "37" || ddlistFeeHead.SelectedItem.Value == "39" || ddlistFeeHead.SelectedItem.Value == "41" || ddlistFeeHead.SelectedItem.Value == "62" || ddlistFeeHead.SelectedItem.Value == "63" || ddlistFeeHead.SelectedItem.Value == "66" || ddlistFeeHead.SelectedItem.Value == "67")
            {
                try
                {
                    if (ddlistExamination.Items.FindByText("NA").Selected)
                    {
                        ddlistExamination.Items.Remove("NA");
                    }

                   
                    lblExamination.Visible = true;
                    ddlistExamination.Visible = true;
                    btnFind.Visible = true;
                }
                catch
                {
                   
                    lblExamination.Visible = true;
                    ddlistExamination.Visible = true;
                }
                

            }

            else
            {
                ddlistExamination.Items.Add("NA");
                ddlistExamination.Items.FindByText("NA").Selected = true;
               
                lblExamination.Visible = false;
                ddlistExamination.Visible = false;
                btnFind.Visible = true;

            }

            
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {

                if (ddlistFeeHead.SelectedItem.Value == "1")
                {
                    if (ddlistYear.SelectedItem.Value == "5")
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Please select any year.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        ViewState["ErrorType"] = "Did not select year";
                    }

                }
                else if (ddlistFeeHead.SelectedItem.Value == "2")
                {
                    //if (tbYear.Text == ddlistYear.SelectedItem.Text)
                    //{
                        string exam;
                        if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                        {
                            if (exam != "NA" && exam != "")
                            {
                                pnlBPExamRecord.Visible = false;
                                ddlistExamination.SelectedItem.Selected = false;
                                ddlistExamination.Items.FindByValue(exam).Selected = true;
                                ddlistExamination.Enabled = false;

                                setFeeStatus();
                                btnFind.Visible = true;
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! Examination is not set on Course fee of this year. Please set Examination on Course fee of this year and then fill this from.";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;
                                ViewState["ErrorType"] = "Did not select year";
                            }

                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !!Course Fee is not paid for this year. Please fill course fee first and then fill this exam form.";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                            ViewState["ErrorType"] = "Did not select year";
                        }
                    //}
                    //else
                    //{
                    //    pnlData.Visible = false;
                    //    lblMSG.Text = "Sorry !!Current Year of Student does not match with the year you have selected for exam. Please update the current year and then fill this exam form.";
                    //    pnlMSG.Visible = true;
                    //    btnOK.Visible = true;
                    //    ViewState["ErrorType"] = "Did not select year";
                    //}
               

                }
                else if (ddlistFeeHead.SelectedItem.Value == "26")
                {

                    string exam;
                    if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                    {
                        if (exam != "NA" && exam != "")
                        {
                            pnlBPExamRecord.Visible = false;
                            ddlistExamination.SelectedItem.Selected = false;
                            ddlistExamination.Items.FindByValue(exam).Selected = true;
                            ddlistExamination.Enabled = false;

                            setFeeStatus();
                            btnFind.Visible = true;
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! Examination is not set on Course fee of this year. Please set Examination on Course fee of this year and then fill this from.";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                            ViewState["ErrorType"] = "Did not select year";
                        }

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !!Course Fee is not paid for this year. Please fill course fee first and then fill this exam form.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        ViewState["ErrorType"] = "Did not select year";
                    }
                }
                else
                {
                    setFeeStatus();
                }


                
                
        }

        private void polulateMBAS()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if ((FindInfo.isMBASpecialazation(Convert.ToInt32(dr[0]))))
                {
                    ddlistMBAS.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistMBAS.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();
                }

            }
            con.Close();
        }

        protected void ddlistExamination_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFeeStatus();
            if (ddlistFeeHead.SelectedItem.Value == "3")
            {
                populateBackPapers();
                pnlBPExamRecord.Visible = true;
            }
        }

        protected void ddlistAcountsSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFeeStatus();
        }

        protected void btnSameFee_Click(object sender, EventArgs e)
        {
            tbENo.Text = "";
            lblNewDD.Visible = false;
            pnlFeeHead.Visible = true;
            pnlStudentDetail.Visible = false;
            pnlDDFee.Visible = false;
            btnSubmit.Visible = false;
            btnOK.Visible = false;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnNewFee.Visible = false;
            btnSameFee.Visible = false;
            btnFind.Visible = false;
            rblMode.Visible = true;
            pnlBPExamRecord.Visible = false;

          
            btnNewFee.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
            
            Session["FeeEntry"] = "Same";
        }

        protected void btnNewFee_Click(object sender, EventArgs e)
        {
            Response.Redirect("FillFee.aspx");
        }     

        protected void lnkbtnFDCDetails_Click(object sender, EventArgs e)
        {
            if (rblEntryType.SelectedItem.Value == "2")
            {
                if (Accounts.draftExist(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value))
                {
                    string[] dcdetail = Accounts.findDCDetails(tbDDNumber.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value));

                    lblNewDD.Visible = false;
                    ddlistDDDay.SelectedItem.Selected = false;
                    ddlistDDMonth.SelectedItem.Selected = false;
                    ddlistDDYear.SelectedItem.Selected = false;

                    ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
                    ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
                    ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

                    tbTotalAmount.Text = dcdetail[1].ToString();
                    tbIBN.Text = dcdetail[2].ToString();

                    lblNewDD.Visible = false;
                    btnSubmit.Visible = true;

                    tbDDNumber.Enabled = true;
                    ddlistDDDay.Enabled = true;
                    ddlistDDMonth.Enabled = true;
                    ddlistDDYear.Enabled = true;
                    tbTotalAmount.Enabled = true;
                    tbIBN.Enabled = true;

                }
                else
                {
                    lblNewDD.Text="Sorry ! this Instrument does not exist";
                    lblNewDD.Visible = true;
                   
                }
                btnSubmit.Visible = true;
                ddlistAcountsSession.Enabled = true;
            }
            else if (rblEntryType.SelectedItem.Value == "1" || rblEntryType.SelectedItem.Value == "3")
            {
                string error;
                int iid;
                string scmode;
                int count;
                string ardate;

                string[] stu = FindInfo.findStudentSCDetails(Convert.ToInt32(lblSRID.Text));

                if (Accounts.validInstrument(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value,tbSCCode.Text,Convert.ToBoolean(stu[0]),stu[1], out iid,out scmode,out count,out ardate, out error))
                {
                    if (count == 1)
                    {

                        string[] dcdetail = Accounts.findInstrumentsDetailsNew(tbDDNumber.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value));

                        if (scmode == "0")
                        {
                            pnlDDFee.BackColor = System.Drawing.Color.Orange;
                        }

                        lblNewDD.Visible = false;
                        ddlistDDDay.SelectedItem.Selected = false;
                        ddlistDDMonth.SelectedItem.Selected = false;
                        ddlistDDYear.SelectedItem.Selected = false;

                        ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
                        ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
                        ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

                        tbTotalAmount.Text = dcdetail[1].ToString();
                        tbIBN.Text = dcdetail[2].ToString();

                        lblNewDD.Visible = false;
                        lblIID.Text = iid.ToString();
                        btnSubmit.Visible = true;

                        tbDDNumber.Enabled = false;
                        ddlistDDDay.Enabled = false;
                        ddlistDDMonth.Enabled = false;
                        ddlistDDYear.Enabled = false;
                        if (ardate != "")
                        {
                            lblARDate.Text = "Amount. Rec. On : " + Convert.ToDateTime(ardate).ToString("dd-MM-yyyy");
                            lblARPDate.Text = Convert.ToDateTime(ardate).ToString("yyyy-MM-dd"); ;
                        }
                        else
                        {
                            lblARDate.Text = "" ;
                            lblARPDate.Text = "";
                        }
                        tbTotalAmount.Enabled = false;
                        tbIBN.Enabled = false;
                        if ((ddlistFeeHead.SelectedItem.Value == "1") && (tbSCCode.Text == "999" || tbSCCode.Text == "998" || tbSCCode.Text == "997" || tbSCCode.Text == "996" || tbSCCode.Text == "995" || tbSCCode.Text == "994" || tbSCCode.Text == "993" || tbSCCode.Text == "462"))
                        {
                            tbStudentAmount.Text = (Convert.ToInt32(tbReqFee.Text)/2).ToString();
                        }
                        else
                        {
                            tbStudentAmount.Text = tbReqFee.Text;
                        }
                        tbStudentAmount.Enabled = true;

                     
                    }
                    else if(count>1)
                    {
                        tbDDNumber.Visible = false;
                        lnkbtnFDCDetails.Visible = false;
                        PopulateDDList.populateSameInsByIno(tbDDNumber.Text,ddlistPaymentMode.SelectedItem.Value, ddlistIns);
                        ddlistIns.Visible = true;
                        lblNewDD.Visible = false;
   

                    
                        ddlistDDDay.Enabled = false;
                        ddlistDDMonth.Enabled = false;
                        ddlistDDYear.Enabled = false;
                        tbTotalAmount.Enabled = false;
                        tbIBN.Enabled = false;
                     
                        tbStudentAmount.Enabled = false;

                    }

                    setAccountSession();
                }
                else
                {

                    lblNewDD.Text = error;
                    lblNewDD.Visible = true;
                    btnSubmit.Visible = false;
                }
            }

        }

        private void setAccountSession()
        {
            if (lblARPDate.Text == "" || lblARPDate.Text == "1900-01-01")
            {
                ddlistAcountsSession.Enabled = true;
            }
            else
            {
                ddlistAcountsSession.SelectedItem.Selected = false;
                string asess = Accounts.findAccountSessionByARDate(lblARPDate.Text);
                ddlistAcountsSession.Items.FindByText(asess).Selected = true;
                ddlistAcountsSession.Enabled = false;
            }
        }   

        protected void btnYes_Click(object sender, EventArgs e)
        {
          

            Exam.updateCurrentYear(Convert.ToInt32(lblSRID.Text),Convert.ToInt32(ddlistYear.SelectedItem.Value));
            Exam.setYearStatus(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value));
            lblMSG.Text = "Current Year has been updated successfully! <br/> Now Current Year of Student is : "+FindInfo.findAlphaYear(FindInfo.findCYearBySRID(Convert.ToInt32(lblSRID.Text)).ToString()).ToUpper() ;

           

            pnlData.Visible = false ;
            pnlMSG.Visible = true;
            btnOK.Visible = false;

            if (Request.QueryString["OURID"] == null)
            {
                btnSameFee.Visible = true;
                btnNewFee.Visible = true;
            }
           
            btnYes.Visible = false;
            btnNo.Visible = false;

            if (Request.QueryString["OURID"] != null)
            {
                string jScript = "<script>window.close();</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
           

            lblMSG.Text = "Fee has been submitted successfully! <br/> Now Current Year of Student is : "+FindInfo.findAlphaYear(FindInfo.findCYearBySRID(Convert.ToInt32(lblSRID.Text)).ToString()).ToUpper() ;
            pnlData.Visible = false;
            pnlMSG.Visible = true;
            btnOK.Visible = false;
            btnSameFee.Visible = true;
            btnNewFee.Visible = true;
           
            btnYes.Visible = false;
            btnNo.Visible = false;

        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            if (lnkbtnEdit.Text == "Edit")
            {
                ddlistFRDDay.Enabled = true;
                ddlistFRDMonth.Enabled = true;
                ddlistFRDYear.Enabled = true;
                lnkbtnEdit.Text = "Update";
            }
            else if (lnkbtnEdit.Text == "Update")
            {
                ddlistFRDDay.Enabled = false;
                ddlistFRDMonth.Enabled = false;
                ddlistFRDYear.Enabled = false;
                lnkbtnEdit.Text = "Edit";
                setFeeStatus();
            }
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "1")
            {
                lblNo.Text = "Enrollment No.";
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                lblNo.Text = "Application No.";
            }
        }

        protected void rblEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblEntryType.SelectedItem.Value == "2")
            {
               
                ddlistDDDay.Enabled = true;
                ddlistDDMonth.Enabled = true;
                ddlistDDYear.Enabled = true;
                tbTotalAmount.Enabled = true;
                tbIBN.Enabled = true;
                tbStudentAmount.Enabled = true;
            }
            else if (rblEntryType.SelectedItem.Value == "1" || rblEntryType.SelectedItem.Value == "3")
            {
               
                ddlistDDDay.Enabled = false;
                ddlistDDMonth.Enabled = false;
                ddlistDDYear.Enabled = false;
                tbTotalAmount.Enabled = false;
                tbIBN.Enabled = false;
                tbStudentAmount.Enabled = false;
            }

        }

        protected void ddlistIns_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] dcdetail = Accounts.findInstrumentsDetailsNewByIID(Convert.ToInt32(ddlistIns.SelectedItem.Value));
         
            lblNewDD.Visible = false;
            ddlistDDDay.SelectedItem.Selected = false;
            ddlistDDMonth.SelectedItem.Selected = false;
            ddlistDDYear.SelectedItem.Selected = false;

            ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
            ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
            ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

            tbTotalAmount.Text = dcdetail[1].ToString();
            tbIBN.Text = dcdetail[2].ToString();
            if (dcdetail[3].ToString() != "")
            {
                lblARDate.Text = "Amount Rec. On : " + Convert.ToDateTime(dcdetail[3]).ToString("dd-MM-yyyy");
                lblARPDate.Text = Convert.ToDateTime(dcdetail[3]).ToString("yyyy-MM-dd");
            }
            else
            {
                lblARDate.Text = "";
                lblARPDate.Text = "";
            }
            if ((ddlistFeeHead.SelectedItem.Value == "1") && (tbSCCode.Text == "999" || tbSCCode.Text == "998" || tbSCCode.Text == "995" || tbSCCode.Text == "994" || tbSCCode.Text == "993" || tbSCCode.Text == "462"))
            {
                tbStudentAmount.Text = (Convert.ToInt32(tbReqFee.Text) / 2).ToString();
            }
            else
            {
                tbStudentAmount.Text = tbReqFee.Text;
            }
           
            tbStudentAmount.Enabled = true;

            setAccountSession();

            btnSubmit.Visible = true;


        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            btnFind.Visible = true;
            tbCourse.Text = ddlistMBAS.SelectedItem.Text;
            lblCID.Text = ddlistMBAS.SelectedItem.Value;
            pnlStudentDetail.Visible = true;
            pnlMBAS.Visible = false;
            Session["SetSp"] = "Yes";
           
        }
 
    }
}
