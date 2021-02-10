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

namespace DDE.Web.Admin
{
    public partial class FillMultipleFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 38))
            {
                if (!IsPostBack)
                {
                    ViewState["ErrorType"] = "";
                    Session["FeeEntry"] = "New";
                    PopulateDDList.populateSTFeeHead(ddlistFeeHead);
                    PopulateDDList.populateAccountSession(ddlistAcountsSession);
                   
                    ddlistAcountsSession.Items.FindByText("2013-14").Selected = true;
                    PopulateDDList.populateExam(ddlistExamination);
                    pnlSearch.Visible = true;
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (validEntry())
            {

              
                fillFeePanelDetails();
               
               
                btnSubmit.Visible = true;
                ddlistAcountsSession.AutoPostBack = true;

                ddlistPaymentMode.AutoPostBack = true;


                pnlDDFee.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;

               


            }

            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
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

     

       


        private void fillFeePanelDetails()
        {
            if (ddlistPaymentMode.SelectedItem.Value != "0")
            {
                if (ddlistPaymentMode.SelectedItem.Value == "1")
                {
                    lblDCNumber.Text = "DD Number *";
                    lblDCDate.Text = "DD Date *";
                    lblIBN.Text = "Issuing Bank Name *";
                  
                    lblTotalAmount.Text = "DD Total Amount *";

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "2")
                {
                    lblDCNumber.Text = "Cheque Number *";
                    lblDCDate.Text = "Cheque Date *";
                    lblIBN.Text = "Issuing Bank Name *";
               
                    lblTotalAmount.Text = "Cheque Total Amount *";

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "3")
                {
                    lblDCNumber.Text = "Receipt No. *";
                    lblDCDate.Text = "Receiving Date *";
                    lblTotalAmount.Text = "Cash Total Amount *";
                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;

                  



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "4")
                {
                    lblDCNumber.Text = "RTGS No. *";
                    lblDCDate.Text = "RTGS Date *";
                    lblTotalAmount.Text = "RTGS Total Amount *";
                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;

                  



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "5" || ddlistPaymentMode.SelectedItem.Value == "6")
                {
                    lblDCNumber.Visible = false;
                    tbDDNumber.Visible = false;
                    tbDDNumber.Text = "NA";
                    lnkbtnFDCDetails.Visible = false;

                    lblDCDate.Visible = false;
                    ddlistDDDay.Visible = false;
                    ddlistDDMonth.Visible = false;
                    ddlistDDYear.Visible = false;

                    lblTotalAmount.Visible = false;
                    tbTotalAmount.Visible = false;
                    tbTotalAmount.Text = "NA";

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;
                    tbIBN.Text = "NA";

               


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
                SqlCommand cmd = new SqlCommand("select ApplicationNo,EnrollmentNo,StudentName,FatherName,StudyCentreCode,Course,Course2Year,Course3Year,CYear from DDEStudentRecord where SRID='" + srid + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                     imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();

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
        }

        private void setYearList(int cid)
        {
            int duration = FindInfo.findCourseDuration(cid);
            ddlistYear.Items.Clear();

            if (duration == 1)
            {
                ddlistYear.Items.Add("--SELECT ONE--");
                ddlistYear.Items.FindByText("--SELECT ONE--").Value = "0";

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

            }
            else if (duration == 2)
            {
                ddlistYear.Items.Add("--SELECT ONE--");
                ddlistYear.Items.FindByText("--SELECT ONE--").Value = "0";

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";
            }
            else if (duration == 3)
            {
                ddlistYear.Items.Add("--SELECT ONE--");
                ddlistYear.Items.FindByText("--SELECT ONE--").Value = "0";

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (tbENo.Text != "")
            {
                if (validENo(tbENo.Text))
                {

                    if (Session["FeeEntry"].ToString() == "New")
                    {
                        pnlSearch.Visible = false;
                        if (rblMode.SelectedItem.Value == "1")
                        {
                            populateStudentInfo(FindInfo.findSRIDByENo(tbENo.Text));
                        }
                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            populateStudentInfo(FindInfo.findSRIDByANo(tbENo.Text));
                        }
                        setTodayDate();
                        pnlStudentDetail.Visible = true;
                        pnlDDFee.Visible = true;
                    }

                    else if (Session["FeeEntry"].ToString() == "Same")
                    {
                        pnlSearch.Visible = false;
                        if (rblMode.SelectedItem.Value == "1")
                        {
                            populateStudentInfo(FindInfo.findSRIDByENo(tbENo.Text));
                        }
                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            populateStudentInfo(FindInfo.findSRIDByANo(tbENo.Text));
                        }
                        setTodayDate();
                        pnlStudentDetail.Visible = true;
                       
                        pnlDDFee.Visible = true;
                        btnSubmit.Visible = true;
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

            if (rblMode.SelectedItem.Value == "1")
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
            pnlDraftDetails.Visible = true;
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    string error;

        //    if (validEntry())
        //    {

        //        try
        //        {
        //            int totalbpsub = 0;
                    
        //            if (ddlistPaymentMode.SelectedItem.Value == "5" || ddlistPaymentMode.SelectedItem.Value == "6")
        //            {
        //                tbTotalAmount.Text = "0";
        //            }

        //            string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;

        //            if (Accounts.validFee(Convert.ToInt32(lblSRID.Text), FindInfo.findCourseIDBySRID(Convert.ToInt32(lblSRID.Text)), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), tbDDAmountInWords.Text, Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, totalbpsub, Convert.ToInt32(Session["ERID"]), FindInfo.findBatchBySRID(Convert.ToInt32(lblSRID.Text)), frdate, out error))
        //            {
        //                string rollno;


        //                Accounts.fillFee(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), tbDDAmountInWords.Text, Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, frdate);
        //                Log.createLogNow("Fee Submit", "Filled" + ddlistFeeHead.SelectedItem.Text + " Fee of a student with Enrollment No '" + tbEnNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

        //                if (ddlistFeeHead.SelectedItem.Value == "1")
        //                {
        //                    if (FindInfo.findCYearBySRID(Convert.ToInt32(lblSRID.Text)) != Convert.ToInt32(ddlistYear.SelectedItem.Value))
        //                    {
        //                        lblMSG.Text = "Do you want to update 'Current Year' of student to ' " + ddlistYear.SelectedItem.Text + " '";
        //                        pnlData.Visible = false;
        //                        pnlMSG.Visible = true;
        //                        btnOK.Visible = false;
        //                        btnSameFee.Visible = false;
        //                        btnNewFee.Visible = false;
        //                        btnYes.Visible = true;
        //                        btnNo.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        lblMSG.Text = "Fee has been submitted successfully !!";
        //                        pnlData.Visible = false;
        //                        pnlMSG.Visible = true;
        //                        btnOK.Visible = false;
        //                        btnSameFee.Visible = true;
        //                        btnNewFee.Visible = true;
        //                        btnYes.Visible = false;
        //                        btnNo.Visible = false;
        //                    }

        //                }
        //                else if (ddlistFeeHead.SelectedItem.Value == "5" || ddlistFeeHead.SelectedItem.Value == "11" || ddlistFeeHead.SelectedItem.Value == "12" || ddlistFeeHead.SelectedItem.Value == "14" || ddlistFeeHead.SelectedItem.Value == "15" || ddlistFeeHead.SelectedItem.Value == "16" || ddlistFeeHead.SelectedItem.Value == "27")
        //                {
        //                    lblMSG.Text = "Fee has been submitted successfully!!";
        //                    pnlData.Visible = false;
        //                    pnlMSG.Visible = true;
        //                    btnOK.Visible = false;
        //                    btnSameFee.Visible = true;
        //                    btnNewFee.Visible = true;
        //                    btnYes.Visible = false;
        //                    btnNo.Visible = false;
        //                }

        //                else if (ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "26")
        //                {
        //                    int ercounter = 0;
        //                    if (Exam.examRecordExist(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, "R"))
        //                    {
        //                        Exam.updateExamCentre(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "R");
        //                        Log.createLogNow("Updated Exam Record", "Filled 'REGULAR' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
        //                        lblMSG.Text = "Fee has been submitted successfully !!";
        //                    }
        //                    else
        //                    {
        //                        Exam.fillExamRecord(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), "", ddlistExamination.SelectedItem.Value, "", "", "", "", FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "R", out rollno, out ercounter);
        //                        Log.createLogNow("Filled Exam Record", "Filled 'REGULAR' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

        //                        if (ddlistExamination.SelectedItem.Value == "B12" || ddlistExamination.SelectedItem.Value == "B13")
        //                        {

        //                            lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : " + ercounter;
        //                        }
        //                        else if (ddlistExamination.SelectedItem.Value == "A14")
        //                        {

        //                            lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : A14_" + ercounter;
        //                        }
        //                        else
        //                        {
        //                            lblMSG.Text = "Fee and Examination Centre has been submitted successfully !!";
        //                        }

        //                    }

        //                    pnlData.Visible = false;
        //                    pnlMSG.Visible = true;
        //                    btnOK.Visible = false;
        //                    btnSameFee.Visible = true;
        //                    btnNewFee.Visible = true;
        //                    btnYes.Visible = false;
        //                    btnNo.Visible = false;

        //                }
        //                else if (ddlistFeeHead.SelectedItem.Value == "3")
        //                {
        //                    int ercounter = 0;

                            

        //                    if (Exam.examRecordExist(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, "B"))
        //                    {

        //                        Exam.updateBPRecord(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value, sub1, sub2, sub3, "", FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "B", out rollno, out ercounter);
        //                        Log.createLogNow("Filled Exam Record", "Filled 'BACK PAPER' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
        //                        if (rollno != "")
        //                        {
        //                            lblMSG.Text = "Fee and Back Paper Record has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno;
        //                        }
        //                        else
        //                        {
        //                            lblMSG.Text = "Fee and Back Paper Record has been submitted successfully !!";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Exam.fillExamRecord(Convert.ToInt32(lblSRID.Text), 3, Convert.ToInt32(ddlistYear.SelectedItem.Value), "", ddlistExamination.SelectedItem.Value, sub1, sub2, sub3, "", FindInfo.calculateECIDBySRID(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value), "B", out rollno, out ercounter);
        //                        Log.createLogNow("Filled Exam Record", "Filled 'BACK PAPER' exam record for the Exam '" + ddlistExamination.SelectedItem.Text + "'  with Enrollment No'" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
        //                        if (ddlistExamination.SelectedItem.Value == "B12" || ddlistExamination.SelectedItem.Value == "B13")
        //                        {
        //                            lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : " + ercounter;
        //                        }
        //                        else if (ddlistExamination.SelectedItem.Value == "A14")
        //                        {
        //                            lblMSG.Text = "Fee and Examination Centre has been submitted successfully !! <br/> Allotted Roll No. is : " + rollno + "<br/> Allotted Counter is : A14_" + ercounter;
        //                        }
        //                        else
        //                        {
        //                            lblMSG.Text = "Fee and Back Paper Record has been submitted successfully !!";
        //                        }
        //                    }

        //                    pnlData.Visible = false;
        //                    pnlMSG.Visible = true;
        //                    btnOK.Visible = false;
        //                    btnSameFee.Visible = true;
        //                    btnNewFee.Visible = true;
        //                    btnYes.Visible = false;
        //                    btnNo.Visible = false;

        //                }


        //            }

        //            else
        //            {
        //                pnlData.Visible = false;
        //                lblMSG.Text = error;
        //                pnlMSG.Visible = true;
        //                btnOK.Visible = true;
        //                btnSameFee.Visible = false;
        //                btnNewFee.Visible = false;
        //                btnYes.Visible = false;
        //                btnNo.Visible = false;
        //            }
        //        }

        //        catch (FormatException ex)
        //        {
        //            pnlData.Visible = false;
        //            lblMSG.Text = "Sorry !! You din not entered amount in numeric form";
        //            pnlMSG.Visible = true;
        //            btnOK.Visible = true;
        //            btnSameFee.Visible = false;
        //            btnNewFee.Visible = false;
        //            btnYes.Visible = false;
        //            btnNo.Visible = false;
        //        }
        //    }

        //    else
        //    {

        //        pnlData.Visible = false;
        //        lblMSG.Text = "Sorry !! You did not select any of given entries";
        //        pnlMSG.Visible = true;
        //        btnOK.Visible = true;
        //        btnSameFee.Visible = false;
        //        btnNewFee.Visible = false;
        //        btnYes.Visible = false;
        //        btnNo.Visible = false;
        //    }
        //}


       
      


        private bool validEntry()
        {
            if (ddlistFeeHead.SelectedItem.Value == "5" || ddlistFeeHead.SelectedItem.Value == "11" || ddlistFeeHead.SelectedItem.Value == "12" || ddlistFeeHead.SelectedItem.Value == "14" || ddlistFeeHead.SelectedItem.Value == "15" || ddlistFeeHead.SelectedItem.Value == "27")
            {
                if (ddlistAcountsSession.SelectedItem.Text == "--SELECT ONE--" || ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistYear.SelectedItem.Text == "--SELECT ONE--")
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
            if (ddlistFeeHead.SelectedItem.Value == "1" || ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "16")
            {
                if (ddlistAcountsSession.SelectedItem.Text == "--SELECT ONE--" || ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistYear.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--")
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
            if (ddlistFeeHead.SelectedItem.Value == "3")
            {
                if (ddlistAcountsSession.SelectedItem.Text == "--SELECT ONE--" || ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--" )
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
                if (ddlistAcountsSession.SelectedItem.Text == "--SELECT ONE--" || ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistYear.SelectedItem.Text == "--SELECT ONE--" || ddlistExamination.SelectedItem.Text == "--SELECT ONE--")
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
              
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else if (ViewState["ErrorType"].ToString() == "Did not select year")
            {

                pnlStudentDetail.Visible = true;
                pnlSearch.Visible = false;
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
                       
                    }

                }
                else
                {

                    ddlistExamination.Enabled = true;
                   
                }
            }

            else if (ddlistFeeHead.SelectedItem.Value == "2")
            {
                if (ddlistYear.SelectedItem.Text != "--SELECT ONE--")
                {
                    if (ddlistYear.SelectedItem.Text == tbYear.Text)
                    {
                        string exam;
                        if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                        {
                            if (exam != "NA" && exam != "")
                            {
                               
                             
                                
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
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !!Current Year of Student does not match with the year you have selected for exam. Please update the current year and then fill this exam form.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        ViewState["ErrorType"] = "Did not select year";
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
            else if (ddlistFeeHead.SelectedItem.Value == "26")
            {
                if (ddlistYear.SelectedItem.Text != "--SELECT ONE--")
                {
                    string exam;
                    if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                    {
                        if (exam != "NA" && exam != "")
                        {
                          
                          
                           
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

               
            }
            else
            {
                lblYear.Visible = true;
                ddlistYear.Visible = true;

               
            }

            if (ddlistFeeHead.SelectedItem.Value == "1" || ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3" || ddlistFeeHead.SelectedItem.Value == "16" || ddlistFeeHead.SelectedItem.Value == "24" || ddlistFeeHead.SelectedItem.Value == "26")
            {
                try
                {
                    if (ddlistExamination.Items.FindByText("NA").Selected)
                    {
                        ddlistExamination.Items.Remove("NA");
                    }


                    lblExamination.Visible = true;
                    ddlistExamination.Visible = true;
                  
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
             

            }


        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlistFeeHead.SelectedItem.Value == "2")
            {
                if (tbYear.Text == ddlistYear.SelectedItem.Text)
                {
                    string exam;
                    if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                    {
                        if (exam != "NA" && exam != "")
                        {
                           
                            ddlistExamination.SelectedItem.Selected = false;
                            ddlistExamination.Items.FindByValue(exam).Selected = true;
                            ddlistExamination.Enabled = false;

                           
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
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !!Current Year of Student does not match with the year you have selected for exam. Please update the current year and then fill this exam form.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    ViewState["ErrorType"] = "Did not select year";
                }


            }
            else if (ddlistFeeHead.SelectedItem.Value == "26")
            {

                string exam;
                if (Accounts.feePaid(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), 1, ddlistExamination.SelectedItem.Value, out exam))
                {
                    if (exam != "NA" && exam != "")
                    {
                       
                        ddlistExamination.SelectedItem.Selected = false;
                        ddlistExamination.Items.FindByValue(exam).Selected = true;
                        ddlistExamination.Enabled = false;

                        
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
                    lblMSG.Text = "Sorry !!Current Year of Student does not match with the year you have selected for exam. Please update the current year and then fill this exam form.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    ViewState["ErrorType"] = "Did not select year";
                }
            }
           


        }

       

        protected void btnSameFee_Click(object sender, EventArgs e)
        {
            tbENo.Text = "";
            lblNewDD.Visible = false;
            pnlSearch.Visible = true;
            pnlStudentDetail.Visible = false;
            pnlDDFee.Visible = false;
            btnSubmit.Visible = false;
            btnOK.Visible = false;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnNewFee.Visible = false;
            btnSameFee.Visible = false;
          


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
            if (tbDDNumber.Text != "")
            {
                string[] dcdetail = Accounts.findDCDetails(tbDDNumber.Text,Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value) );

                if (dcdetail[0] != "")
                {
                    lblNewDD.Visible = false;
                    ddlistDDDay.SelectedItem.Selected = false;
                    ddlistDDMonth.SelectedItem.Selected = false;
                    ddlistDDYear.SelectedItem.Selected = false;

                    ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
                    ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
                    ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

                    tbTotalAmount.Text = dcdetail[1].ToString();
                    tbIBN.Text = dcdetail[2].ToString();
                }

                else
                {
                    lblNewDD.Visible = true;
                    ddlistDDDay.SelectedItem.Selected = false;
                    ddlistDDMonth.SelectedItem.Selected = false;
                    ddlistDDYear.SelectedItem.Selected = false;

                    ddlistDDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
                    ddlistDDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
                    ddlistDDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

                    tbTotalAmount.Text = "";
                    tbIBN.Text = "";

                }
            }

        }

       

        protected void btnYes_Click(object sender, EventArgs e)
        {


            Exam.updateCurrentYear(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value));
            Exam.setYearStatus(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value));
            lblMSG.Text = "Fee and Current Year have been submitted successfully! <br/> Now Current Year of Student is : " + FindInfo.findAlphaYear(FindInfo.findCYearBySRID(Convert.ToInt32(lblSRID.Text)).ToString()).ToUpper();



            pnlData.Visible = false;
            pnlMSG.Visible = true;
            btnOK.Visible = false;
            btnSameFee.Visible = true;
            btnNewFee.Visible = true;

            btnYes.Visible = false;
            btnNo.Visible = false;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {


            lblMSG.Text = "Fee has been submitted successfully! <br/> Now Current Year of Student is : " + FindInfo.findAlphaYear(FindInfo.findCYearBySRID(Convert.ToInt32(lblSRID.Text)).ToString()).ToUpper();
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

        protected void ddlistExamination_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FHID");
            DataColumn dtcol3 = new DataColumn("FeeHead");
            DataColumn dtcol4 = new DataColumn("Required");
            DataColumn dtcol5 = new DataColumn("Paying");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);

            int i = 1;

            

            foreach (DataListItem dli in dtlistFeeHead.Items)
            {
                Label fhid = (Label)dli.FindControl("lblFHID");
                Label feehead = (Label)dli.FindControl("lblFeeHead");
                Label required = (Label)dli.FindControl("lblRequired");
                TextBox paying = (TextBox)dli.FindControl("tbPaying");

               
                DataRow drow = dt.NewRow();
               
                drow["SNo"] = i;
                drow["FHID"] = Convert.ToString(fhid.Text);
                drow["FeeHead"] = Convert.ToString(feehead.Text);

                drow["Required"] = Convert.ToString(required.Text);
                drow["Paying"] = Convert.ToString(paying.Text);
                dt.Rows.Add(drow);
              
                i = i + 1;               
               
            }
            if (dt.Rows.Count > 0)
            {
                if (!(dt.AsEnumerable().Where(c => c.Field<string>("FHID").Equals(ddlistFeeHead.SelectedItem.Value)).Count() > 0))
                {
                    DataRow drow1 = dt.NewRow();

                    drow1["SNo"] = i;
                    drow1["FHID"] = Convert.ToString(ddlistFeeHead.SelectedItem.Value);
                    drow1["FeeHead"] = Convert.ToString(ddlistFeeHead.SelectedItem.Text);
                    string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                    drow1["Required"] = Convert.ToString(Accounts.findRequiredFee(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblCID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), 0, lblBatch.Text, frdate));
                    drow1["Paying"] = "";
                    dt.Rows.Add(drow1);
                   
                }
                
                //if (!dt.Rows.Contains(Convert.ToString(ddlistFeeHead.SelectedItem.Value)))
                //{
                    
                //}
            }
            else
            {
                DataRow drow1 = dt.NewRow();
               
                drow1["SNo"] = i;
                drow1["FHID"] = Convert.ToString(ddlistFeeHead.SelectedItem.Value);
                drow1["FeeHead"] = Convert.ToString(ddlistFeeHead.SelectedItem.Text);
                string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                drow1["Required"] = Convert.ToString(Accounts.findRequiredFee(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblCID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), 0, lblBatch.Text, frdate));
                drow1["Paying"] = "";
                dt.Rows.Add(drow1);
               
            }
           
          
            dtlistFeeHead.DataSource = dt;
            dtlistFeeHead.DataBind();

            pnlFeeHeadTable.Visible = true;
            setTotalAmount();
        }

       
        protected void dtlistFeeHead_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FHID");
            DataColumn dtcol3 = new DataColumn("FeeHead");
            DataColumn dtcol4 = new DataColumn("Required");
            DataColumn dtcol5 = new DataColumn("Paying");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);

            int i = 1;



            foreach (DataListItem dli in dtlistFeeHead.Items)
            {
                Label fhid = (Label)dli.FindControl("lblFHID");
                Label feehead = (Label)dli.FindControl("lblFeeHead");
                Label required = (Label)dli.FindControl("lblRequired");
                TextBox paying = (TextBox)dli.FindControl("tbPaying");


                DataRow drow = dt.NewRow();

                drow["SNo"] = i;
                drow["FHID"] = Convert.ToString(fhid.Text);
                drow["FeeHead"] = Convert.ToString(feehead.Text);

                drow["Required"] = Convert.ToString(required.Text);
                drow["Paying"] = Convert.ToString(paying.Text);
                dt.Rows.Add(drow);

                i = i + 1;

            }

            int index = Convert.ToInt32(e.Item.ItemIndex);
          

            dt.Rows[index].Delete();
            dt.AcceptChanges();

           
            dtlistFeeHead.EditItemIndex = -1;
            dtlistFeeHead.DataSource = dt;
            dtlistFeeHead.DataBind();
            setTotalAmount();
        }

        private void setTotalAmount()
        {
            int totalrequired = 0;
            int totalpaying = 0;
            foreach (DataListItem dli in dtlistFeeHead.Items)
            {
                Label required = (Label)dli.FindControl("lblRequired");
                TextBox paying = (TextBox)dli.FindControl("tbPaying");
               

                if (required.Text != "")
                {
                    totalrequired = totalrequired + Convert.ToInt32(required.Text);
                }

                if (paying.Text != "")
                {
                    totalpaying = totalpaying + Convert.ToInt32(paying.Text);
                }


            }

            lblTotalRequired.Text = totalrequired.ToString();
            lblTotalPaying.Text = totalpaying.ToString();
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            int totalrequired = 0;
            int totalpaying = 0;
            foreach (DataListItem dli in dtlistFeeHead.Items)
            {
                Label required = (Label)dli.FindControl("lblRequired");
                TextBox paying = (TextBox)dli.FindControl("tbPaying");
                paying.Enabled = false;

                if (required.Text != "")
                {
                    totalrequired = totalrequired + Convert.ToInt32(required.Text);
                }

                if (paying.Text != "")
                {
                    totalpaying = totalpaying + Convert.ToInt32(paying.Text);
                }
               

            }
            ddlistYear.Enabled = false;
            ddlistExamination.Enabled = false;
            ddlistAcountsSession.Enabled = false;
            ddlistFeeHead.Enabled = false;
            btnAdd.Enabled = false;
            btnPay.Enabled = false;
            ddlistPaymentMode.Enabled = true;
            lblTotalRequired.Text = totalrequired.ToString();
            lblTotalPaying.Text =totalpaying.ToString();
        }

    }
}
