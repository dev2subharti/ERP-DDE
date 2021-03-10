using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;
using System.IO;
using System.Drawing;

namespace DDE.Web.Admin
{
    public partial class DStudentRegistration : System.Web.UI.Page
    {
        int counter = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 107))
            {

                if (!IsPostBack)
                {
                   
                    PopulateDDList.populatePreviousCourses(ddlistPreCourse);
                    PopulateDDList.populateSySession(ddlistSySession);
                    PopulateDDList.populateAccountSession(ddlistAcountsSession);                        
                    PopulateDDList.populateExam(ddlistExamination);
                 
                    if (Request.QueryString["SRID"] != null)
                    {
                        PopulateDDList.populateBatch(ddlistSession);
                     
                        int flag= populateStudentRecord();

                        if (flag == 1)
                        {
                            if (Session["RecordType"].ToString() == "Show")
                            {

                                btnSubmit.Visible = false;
                                btnReset.Visible = false;
                            }
                            else if (Session["RecordType"].ToString() == "Edit")
                            {
                                btnSubmit.Text = "Update";
                                btnSubmit.Visible = true;
                                btnReset.Visible = true;
                                rfvMName.Enabled = false;

                            }
                            pnlData.Visible = true;
                            pnlMSG.Visible = false;
                        }
                        else
                        {

                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! No Record found with this ID.";
                            pnlMSG.Visible = true;
                        }
                    }
                    else if (Request.QueryString["PSRID"] != null)
                    {
                       
                        PopulateDDList.populateBatch(ddlistSession);
                       
                        if (Session["ComingFrom"].ToString() == "ALL")
                        {
                           int flag= populatePendingStudentRecord();

                           if (flag == 1)
                           {
                               rblEntryType.Enabled = false;
                               ddlistAcountsSession.Enabled = false;
                               ddlistPaymentMode.Enabled = false;
                               string error;
                               if (FindInfo.isApplicationEligible(Convert.ToInt32(Request.QueryString["PSRID"]), out error))
                               {
                                    if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 107))
                                    {
                                        ddlistEligible.Enabled = true;
                                        ddlistOriginalsVer.Enabled = true;
                                        ddlistAdmissionStatus.Enabled = true;
                                        btnUpd.Visible = true;
                                    }
                                   else
                                    {
                                        ddlistEligible.Enabled = false;
                                        ddlistOriginalsVer.Enabled = false;
                                        ddlistAdmissionStatus.Enabled = false;
                                        btnUpd.Visible = false;

                                    }
                                        
                               }
                               else
                               {
                                   RequiredFieldValidator1.Enabled = false;
                                   ddlistEligible.Items.FindByText("YES").Selected = true;
                                   ddlistOriginalsVer.Items.FindByText("YES").Selected = true;
                                   ddlistAdmissionStatus.Items.FindByText("CONFIRM").Selected = true;
                                   btnUpd.Visible = true;
                               }
                               btnSubmit.Visible = false;
                               btnReset.Visible = false;

                               pnlData.Visible = true;
                               pnlMSG.Visible = false;
                           }
                           else
                           {
                               pnlData.Visible = false;
                               lblMSG.Text = "Sorry !! No Record found with this ID.";
                               pnlMSG.Visible = true;
                           }
                           
                        }
                        else if (Session["ComingFrom"].ToString() == "CONFIRMED")
                        {
                            string error;
                            if (FindInfo.isApplicationEligible(Convert.ToInt32(Request.QueryString["PSRID"]), out error))
                            {
                                if (ddlistSession.SelectedItem.Text == "C 2015")
                                {
                                    if (File.Exists(Server.MapPath("PendingStudentPhotos/" + Request.QueryString["PSRID"] + ".jpg")))
                                    {
                                       int flag= populatePendingStudentRecord();
                                       if (flag == 1)
                                       {
                                           rblEntryType.Enabled = false;
                                           pnlData.Visible = true;
                                           pnlMSG.Visible = false;
                                       }
                                       else
                                       {
                                           pnlData.Visible = false;
                                           lblMSG.Text = "Sorry !! No Record found with this ID.";
                                           pnlMSG.Visible = true;
                                       }
                                    }
                                    else
                                    {
                                        pnlData.Visible = false;
                                        lblMSG.Text = "Sorry !! Student's Photo does not exist";
                                        pnlMSG.Visible = true;
                                    }
                                }
                                else
                                {
                                   int flag= populatePendingStudentRecord();

                                   if (flag == 1)
                                   {
                                       rblEntryType.Enabled = false;
                                       pnlData.Visible = true;
                                       pnlMSG.Visible = false;
                                   }
                                   else
                                   {
                                       pnlData.Visible = false;
                                       lblMSG.Text = "Sorry !! No Record found with this ID.";
                                       pnlMSG.Visible = true;
                                   }
                                }
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = error;
                                pnlMSG.Visible = true;
                            }

                            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 107))
                            {
                                btnUpd.Visible = true;
                                btnUpd.CausesValidation = false;
                                btnReset.Visible = true;
                            }
                            else
                            {
                                btnUpd.Visible = false;
                                btnReset.Visible = false;
                            }

                        }                       
                                   
                    }            
                    else
                    {
                        PopulateDDList.populateBatchByAdmissionOpen(ddlistSession);
                        ddlistSession.Enabled = true;
                        PopulateDDList.populateState(ddlistState);
                        ddlistState.Items.FindByText("UTTAR PRADESH").Selected = true;
                        PopulateDDList.populateCity(ddlistCity, "UTTAR PRADESH");
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;
                        setDOATodayDate();
                        setDCTodayDate();
                        imsStudent.Visible = false;
                    }

                       
                    
                }

               
               
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {

                if (!IsPostBack)
                {
                    if (Request.QueryString["SRID"] != null)
                    {
                        PopulateDDList.populatePreviousCourses(ddlistPreCourse);
                        PopulateDDList.populateBatch(ddlistSession);
                        PopulateDDList.populateAccountSession(ddlistAcountsSession);
                        PopulateDDList.populateSySession(ddlistSySession);
                        PopulateDDList.populateState(ddlistState);
                        ddlistState.Items.FindByText("UTTAR PRADESH").Selected = true;
                        PopulateDDList.populateCity(ddlistCity, "UTTAR PRADESH");
                       
                        PopulateDDList.populateExam(ddlistExamination);
                        
                       int flag= populateStudentRecord();

                       if (flag == 1)
                       {

                           btnSubmit.Visible = false;
                           btnReset.Visible = false;
                           pnlData.Visible = true;
                           pnlMSG.Visible = false;
                       }
                       else
                       {
                           pnlData.Visible = false;
                           lblMSG.Text = "Sorry !! No Record found with this ID.";
                           pnlMSG.Visible = true;
                       }
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! You are not authorised for this control";
                        pnlMSG.Visible = true;
                    }
                      
                   
                }             

            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 96))
            {

                if (!IsPostBack)
                {

                    PopulateDDList.populatePreviousCourses(ddlistPreCourse);
                    PopulateDDList.populateBatch(ddlistSession);
                    PopulateDDList.populateSySession(ddlistSySession);
                    PopulateDDList.populateAccountSession(ddlistAcountsSession);
                  

                    PopulateDDList.populateState(ddlistState);
                    ddlistState.Items.FindByText("UTTAR PRADESH").Selected = true;
                    PopulateDDList.populateCity(ddlistCity, "UTTAR PRADESH");
                  
                    PopulateDDList.populateExam(ddlistExamination);
               
                    if (Request.QueryString["PSRID"] != null)
                    {                      
                        if (Session["ComingFrom"].ToString() == "ALL")
                        {
                           int flag= populatePendingStudentRecord();

                           if (flag == 1)
                           {
                               rblEntryType.Enabled = false;
                               ddlistAcountsSession.Enabled = false;
                               ddlistPaymentMode.Enabled = false;
                               btnUpd.Visible = true;
                               btnSubmit.Visible = false;
                               btnReset.Visible = false;

                               pnlData.Visible = true;
                               pnlMSG.Visible = false;
                           }
                           else
                           {
                               pnlData.Visible = false;
                               lblMSG.Text = "Sorry !! No Record found with this ID.";
                               pnlMSG.Visible = true;
                           }

                        }
                        else if (Session["ComingFrom"].ToString() == "CONFIRMED")
                        {
                            string error;
                            if (FindInfo.isApplicationEligible(Convert.ToInt32(Request.QueryString["PSRID"]), out error))
                            {
                                if (File.Exists(Server.MapPath("PendingStudentPhotos/" + Request.QueryString["PSRID"] + ".jpg")))
                                {
                                   int flag= populatePendingStudentRecord();
                                   if (flag == 1)
                                   {
                                       rblEntryType.Enabled = false;
                                       pnlData.Visible = true;
                                       pnlMSG.Visible = false;
                                   }
                                   else
                                   {
                                       pnlData.Visible = false;
                                       lblMSG.Text = "Sorry !! No Record found with this ID.";
                                       pnlMSG.Visible = true;
                                   }
                                }
                                else
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = "Sorry !! Student's Photo does not exist";
                                    pnlMSG.Visible = true;
                                }
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = error;
                                pnlMSG.Visible = true;
                            }

                            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 107))
                            {
                                btnUpd.Visible = true;
                                btnUpd.CausesValidation = false;
                                btnReset.Visible = true;
                            }
                            else
                            {
                                btnUpd.Visible = false;
                                btnReset.Visible = false;
                            }

                        }
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

        private int populatePendingStudentRecord()
        {
            int flag = 0;
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

         
           cmd.CommandText = "select * from DDEPendingStudentRecord where PSRID='" + Request.QueryString["PSRID"] + "'";

          
          

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                flag = 1;
                

                    imsStudent.ImageUrl = "StudentImgHandler.ashx?PSRID=" + Request.QueryString["PSRID"];
                    tbANo.Text = dr["ApplicationNo"].ToString();
                    //if (dr["ApplicationNo"].ToString() == "" || dr["ApplicationNo"].ToString() == "0" || dr["ApplicationNo"].ToString() == "NA")
                    //{
                       
                    //    tbANo.Enabled = true;
                    //}
                    //else
                    //{
                       
                    //    tbANo.Enabled = false;
                    //}
                    setCorrectValue(ddlistAdmissionThrough, "1", "AdmissionThrough");
                    //PopulateDDList.populateStudyCentreByAT(ddlistStudyCentre, ddlistAdmissionThrough.SelectedItem.Value);
                    PopulateDDList.populateStudyCentre(ddlistStudyCentre);
                    setCorrectText(ddlistStudyCentre, dr["StudyCentreCode"].ToString());
                    ddlistStudyCentre.Enabled = false;
                    if (dr["SCStatus"].ToString() == "O")
                    {
                        rblTrans.Items.FindByText("No").Selected = true;
                        rblTrans.Enabled = false;
                        lblTrans.Visible = false;
                        ddlistTransSC.Visible = false;
                    }
                    else if (dr["SCStatus"].ToString() == "T")
                    {
                        rblTrans.Items.FindByText("Yes").Selected = true;
                        rblTrans.Enabled = false;
                        lblTrans.Visible = true;
                        PopulateDDList.populateStudyCentre(ddlistTransSC);
                        ddlistTransSC.Items.FindByText(dr["PreviousSCCode"].ToString()).Selected = true;
                        ddlistTransSC.Enabled = false;
                        ddlistTransSC.Visible = true;

                    }
                    setCorrectText(ddlistSession, dr["Session"].ToString());
                    if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 16)
                    {
                        rfvAaNo.Enabled = true;
                        revAaNo.Enabled = true;
                    }
                    else
                    {
                        rfvAaNo.Enabled = false;
                        revAaNo.Enabled = false;
                    }
                   
                    if(Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 25 && Convert.ToInt16(ddlistAdmissionType.SelectedItem.Value)==1)
                    {
                        ddlistSySession.Items.FindByText("A 2020-21").Selected = true;
                    }
                    else
                    {
                       setCorrectText(ddlistSySession, dr["SyllabusSession"].ToString().TrimEnd());
                    }
                  
                    tbENo.Text = "";
                    tbICNo.Text = "";
                    if (Convert.ToInt32(ddlistSession.SelectedItem.Value) == 6)
                    {
                        tbENo.Enabled = true;
                    }
                    else
                    {
                        tbENo.Enabled = false;
                    }
                    if (Convert.ToInt32(ddlistSession.SelectedItem.Value) < 8)
                    {
                        tbICNo.Enabled = true;
                    }
                    else
                    {
                        tbICNo.Enabled = false;
                    }
                    setCorrectValue(ddlistAdmissionType, dr["AdmissionType"].ToString(), "AdmissionType");

                    if (dr["AdmissionType"].ToString() == "")
                    {
                        pnlCT.Visible = false;
                    }

                    else if (dr["AdmissionType"].ToString() == "1")
                    {
                        pnlCT.Visible = false;
                    }
                    else if (dr["AdmissionType"].ToString() == "2" || dr["AdmissionType"].ToString() == "3")
                    {
                        setCorrectValue(ddlistPInst, dr["PreviousInstitute"].ToString(), "PreviousInstitute");
                        setCorrectValue(ddlistPreCourse, dr["PreviousCourse"].ToString(), "PreviousCourse");
                        pnlCT.Visible = true;
                    }
                setExam(ddlistSession.SelectedItem.Text, Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value));
                if (ddlistStudyCentre.SelectedItem.Value != "0")
                    {
                        PopulateDDList.populateCoursesBySCStreamsandAT(ddlistCourses, Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value), Convert.ToInt32(ddlistStudyCentre.SelectedItem.Value));
                    }

                    setCorrectValue(ddlistCourses, dr["Course"].ToString(), "Course");

                    setYearList(Convert.ToInt32(ddlistCourses.SelectedItem.Value));

                    setCorrectValue(ddlistCYear, dr["CYear"].ToString(), "CYear");
                    tbSName.Text = dr["StudentName"].ToString();
                    tbFName.Text = dr["FatherName"].ToString();
                    tbMName.Text = dr["MotherName"].ToString();
                    setCorrectText(ddlistGender, dr["Gender"].ToString());
                    if (dr["DOBDay"].ToString() != "" && dr["DOBMonth"].ToString() != "" && dr["DOBYear"].ToString() != "")
                    {
                        setCorrectText(ddlistDOBDay, string.Format("{0:00}", Convert.ToInt32(dr["DOBDay"])));
                        setCorrectText(ddlistDOBMonth, dr["DOBMonth"].ToString());
                        setCorrectText(ddlistDOBYear, dr["DOBYear"].ToString());
                    }
                    tbPAddress.Text = dr["CAddress"].ToString();
                    PopulateDDList.populateState(ddlistState);
                    setCorrectText(ddlistState, dr["State"].ToString());
                    PopulateDDList.populateCity(ddlistCity, dr["State"].ToString());
                    setCorrectText(ddlistCity, dr["City"].ToString());

                    tbPinCode.Text = dr["PinCode"].ToString();
                    tbPNo.Text = dr["PhoneNo"].ToString();
                    tbMNo.Text = dr["MobileNo"].ToString();
                    tbEAddress.Text = dr["Email"].ToString();
                    tbAadhaarNo.Text = dr["AadhaarNo"].ToString();
                    if (dr["DOA"].ToString() == "" || dr["DOA"].ToString() == "1/1/1900 12:00:00 AM")
                    {
                        ddlistDOADay.SelectedItem.Text = "NF";
                        ddlistDOAMonth.SelectedItem.Text = "NF";
                        ddlistDOAYear.SelectedItem.Text = "NF";
                    }
                    else
                    {
                        string doa = Convert.ToDateTime(dr["DOA"].ToString()).ToString("yyyy-MM-dd");
                        ddlistDOADay.Items.FindByText(doa.Substring(8, 2)).Selected = true;
                        ddlistDOAMonth.Items.FindByText(Convert.ToDateTime(dr["DOA"].ToString()).ToString("MMMM").ToUpper()).Selected = true;
                        ddlistDOAYear.Items.FindByText(doa.Substring(0, 4)).Selected = true;
                    }

                    ddlistNationality.Items.FindByText(dr["Nationality"].ToString()).Selected = true;

                    setCorrectText(ddlistCategory, dr["Category"].ToString());
                    setCorrectText(ddlsitEmploymentlist, dr["Employmentstatus"].ToString());

                    lblExam1.Text = dr["examname1"].ToString();
                    lblexam2.Text = dr["examname2"].ToString();
                    lblExam3.Text = dr["examname3"].ToString();
                    lblexam4.Text = dr["examname4"].ToString();
                    lblexam5.Text = dr["examname5"].ToString();

                    lblSubject.Text = dr["subject1"].ToString();
                    lblsubject2.Text = dr["subject2"].ToString();
                    lblSubject3.Text = dr["subject3"].ToString();
                    lblsubject4.Text = dr["subject4"].ToString();
                    lblsubject5.Text = dr["subject5"].ToString();

                    lblYearpass.Text = dr["YearPass1"].ToString();
                    lblyearpass2.Text = dr["YearPass2"].ToString();
                    lblyearpass3.Text = dr["YearPass3"].ToString();
                    lblyearpass4.Text = dr["YearPass4"].ToString();
                    lblyearpass5.Text = dr["YearPass5"].ToString();

                    lblUniversityBoard.Text = dr["UniversityBoard1"].ToString();
                    lblUniversityBoard2.Text = dr["UniversityBoard2"].ToString();
                    lblUniversityBoard3.Text = dr["UniversityBoard3"].ToString();
                    lblUniversityBoard4.Text = dr["UniversityBoard4"].ToString();
                    lblUniversityBoard5.Text = dr["UniversityBoard5"].ToString();

                    lblDivisiongrade.Text = dr["Divisiongrade1"].ToString();
                    lblDivision2.Text = dr["Divisiongrade2"].ToString();
                    lblDivision3.Text = dr["Divisiongrade3"].ToString();
                    lbldivision4.Text = dr["Divisiongrade4"].ToString();
                    lbldivision4.Text = dr["Divisiongrade5"].ToString();

                    if (Session["ComingFrom"].ToString() == "CONFIRMED")
                    {

                        setCorrectText(ddlistEligible, dr["Eligible"].ToString());
                        setCorrectText(ddlistOriginalsVer, dr["OriginalsVerified"].ToString());
                        setCorrectValue(ddlistAdmissionStatus, dr["AdmissionStatus"].ToString(), "AdmissionStatus");
                        tbReason.Text = dr["ReasonIfPending"].ToString();

                       if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 107))
                        {
                            ddlistEligible.Enabled = true;
                            ddlistOriginalsVer.Enabled = true;
                            ddlistAdmissionStatus.Enabled = true;
                        }
                        else
                        {
                           ddlistEligible.Enabled = false;
                           ddlistOriginalsVer.Enabled = false;
                           ddlistAdmissionStatus.Enabled = false;
                        }
                    }
                
            }
            

            con.Close();

            return flag;

        }
      
        private void setDCTodayDate()
        {
            ddlistDDDay.SelectedItem.Selected = false;
            ddlistDDMonth.SelectedItem.Selected = false;
            ddlistDDYear.SelectedItem.Selected = false;
            ddlistDDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        private void setDOATodayDate()
        {
            ddlistDOADay.SelectedItem.Selected = false;
            ddlistDOAMonth.SelectedItem.Selected = false;
            ddlistDOAYear.SelectedItem.Selected = false;
            ddlistDOADay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDOAMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDOAYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        private int populateStudentRecord()
        {
            int flag = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEStudentRecord where SRID='"+Request.QueryString["SRID"]+"'", con);
            SqlDataReader dr;

             con.Open();
             dr=cmd.ExecuteReader();

             if (dr.HasRows)
             {

                 dr.Read();

                 flag = 1;

                 imsStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + Request.QueryString["SRID"].ToString();
                 //imsStudent.ImageUrl = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                 tbANo.Text = dr["ApplicationNo"].ToString();
                 setCorrectValue(ddlistAdmissionThrough, dr["AdmissionThrough"].ToString(), "AdmissionThrough");
                 PopulateDDList.populateStudyCentre(ddlistStudyCentre);
                 setCorrectText(ddlistStudyCentre, dr["StudyCentreCode"].ToString());
                 setCorrectText(ddlistSession, dr["Session"].ToString());
                 setCorrectText(ddlistSySession, dr["SyllabusSession"].ToString().TrimEnd());
                 tbENo.Text = dr["EnrollmentNo"].ToString();
                 tbICNo.Text = dr["ICardNo"].ToString();
                 if (Convert.ToInt32(ddlistSession.SelectedItem.Value) == 6)
                 {
                     tbENo.Enabled = true;
                 }
                 else
                 {
                     tbENo.Enabled = false;
                 }
                 if (Convert.ToInt32(ddlistSession.SelectedItem.Value) < 8)
                 {
                     tbICNo.Enabled = true;
                 }

                 else
                 {
                     tbICNo.Enabled = false;
                 }

                 if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 16)
                 {
                     rfvAaNo.Enabled = true;
                     revAaNo.Enabled = true;
                 }
                 else
                 {
                     rfvAaNo.Enabled = false;
                     revAaNo.Enabled = false;
                 }

                 setCorrectValue(ddlistAdmissionType, dr["AdmissionType"].ToString(), "AdmissionType");

                 if (dr["AdmissionType"].ToString() == "")
                 {
                     pnlCT.Visible = false;
                 }

                 else if (dr["AdmissionType"].ToString() == "1")
                 {
                     pnlCT.Visible = false;
                 }
                 else if (dr["AdmissionType"].ToString() == "2" || dr["AdmissionType"].ToString() == "3")
                 {

                     setCorrectValue(ddlistPInst, dr["PreviousInstitute"].ToString(), "PreviousInstitute");
                     setCorrectValue(ddlistPreCourse, dr["PreviousCourse"].ToString(), "PreviousCourse");
                     pnlCT.Visible = true;
                 }


                 if (ddlistStudyCentre.SelectedItem.Value != "0")
                 {
                     PopulateDDList.populateCoursesBySCStreams(ddlistCourses, Convert.ToInt32(ddlistStudyCentre.SelectedItem.Value));
                 }


                 if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                 {
                     if (dr["CYear"].ToString() == "1")
                     {
                         setCorrectValue(ddlistCourses, dr["Course"].ToString(), "Course");
                     }
                     else if (dr["CYear"].ToString() == "2")
                     {
                         setCorrectValue(ddlistCourses, dr["Course2Year"].ToString(), "Course2Year");
                     }
                     else if (dr["CYear"].ToString() == "3")
                     {
                         setCorrectValue(ddlistCourses, dr["Course3Year"].ToString(), "Course3Year");
                     }
                     else if (dr["CYear"].ToString() == "0")
                     {
                         setCorrectValue(ddlistCourses, dr["Course"].ToString(), "Course");
                     }

                     ddlistCourses.Enabled = false;
                 }
                 else
                 {
                     setCorrectValue(ddlistCourses, dr["Course"].ToString(), "Course");
                 }
                 setYearList(Convert.ToInt32(ddlistCourses.SelectedItem.Value));

                 setCorrectValue(ddlistCYear, dr["CYear"].ToString(), "CYear");
                 tbSName.Text = dr["StudentName"].ToString();
                 tbFName.Text = dr["FatherName"].ToString();
                 tbMName.Text = dr["MotherName"].ToString();
                 setCorrectText(ddlistGender, dr["Gender"].ToString());
                 if (dr["DOBDay"].ToString() != "" && dr["DOBMonth"].ToString() != "" && dr["DOBYear"].ToString() != "")
                 {
                     setCorrectText(ddlistDOBDay, string.Format("{0:00}", Convert.ToInt32(dr["DOBDay"])));
                     setCorrectText(ddlistDOBMonth, dr["DOBMonth"].ToString());
                     setCorrectText(ddlistDOBYear, dr["DOBYear"].ToString());
                 }


                 tbPAddress.Text = dr["CAddress"].ToString();
                 PopulateDDList.populateState(ddlistState);
                 setCorrectText(ddlistState, dr["State"].ToString());
                 PopulateDDList.populateCity(ddlistCity, dr["State"].ToString());
                 setCorrectText(ddlistCity, dr["City"].ToString());

                 tbPinCode.Text = dr["PinCode"].ToString();
                 tbPNo.Text = dr["PhoneNo"].ToString();
                 tbMNo.Text = dr["MobileNo"].ToString();
                 tbEAddress.Text = dr["Email"].ToString();
                 tbAadhaarNo.Text = dr["AadhaarNo"].ToString();

                 if (dr["DOA"].ToString() == "" || dr["DOA"].ToString() == "1/1/1900 12:00:00 AM")
                 {
                     ddlistDOADay.SelectedItem.Text = "NF";
                     ddlistDOAMonth.SelectedItem.Text = "NF";
                     ddlistDOAYear.SelectedItem.Text = "NF";
                 }

                 else
                 {
                     string doa = Convert.ToDateTime(dr["DOA"].ToString()).ToString("yyyy-MM-dd");
                     ddlistDOADay.Items.FindByText(doa.Substring(8, 2)).Selected = true;
                     ddlistDOAMonth.Items.FindByText(Convert.ToDateTime(dr["DOA"].ToString()).ToString("MMMM").ToUpper()).Selected = true;
                     ddlistDOAYear.Items.FindByText(doa.Substring(0, 4)).Selected = true;
                 }

                 ddlistNationality.Items.FindByText(dr["Nationality"].ToString()).Selected = true;

                 setCorrectText(ddlistCategory, dr["Category"].ToString());
                 setCorrectText(ddlsitEmploymentlist, dr["Employmentstatus"].ToString());

                 lblExam1.Text = dr["examname1"].ToString();
                 lblexam2.Text = dr["examname2"].ToString();
                 lblExam3.Text = dr["examname3"].ToString();
                 lblexam4.Text = dr["examname4"].ToString();
                 lblexam5.Text = dr["examname5"].ToString();

                 lblSubject.Text = dr["subject1"].ToString();
                 lblsubject2.Text = dr["subject2"].ToString();
                 lblSubject3.Text = dr["subject3"].ToString();
                 lblsubject4.Text = dr["subject4"].ToString();
                 lblsubject5.Text = dr["subject5"].ToString();

                 lblYearpass.Text = dr["YearPass1"].ToString();
                 lblyearpass2.Text = dr["YearPass2"].ToString();
                 lblyearpass3.Text = dr["YearPass3"].ToString();
                 lblyearpass4.Text = dr["YearPass4"].ToString();
                 lblyearpass5.Text = dr["YearPass5"].ToString();

                 lblUniversityBoard.Text = dr["UniversityBoard1"].ToString();
                 lblUniversityBoard2.Text = dr["UniversityBoard2"].ToString();
                 lblUniversityBoard3.Text = dr["UniversityBoard3"].ToString();
                 lblUniversityBoard4.Text = dr["UniversityBoard4"].ToString();
                 lblUniversityBoard5.Text = dr["UniversityBoard5"].ToString();

                 lblDivisiongrade.Text = dr["Divisiongrade1"].ToString();
                 lblDivision2.Text = dr["Divisiongrade2"].ToString();
                 lblDivision3.Text = dr["Divisiongrade3"].ToString();
                 lbldivision4.Text = dr["Divisiongrade4"].ToString();
                 lbldivision4.Text = dr["Divisiongrade5"].ToString();

                 setCorrectText(ddlistEligible, dr["Eligible"].ToString());

                 setCorrectText(ddlistOriginalsVer, dr["OriginalsVerified"].ToString());
                 setCorrectValue(ddlistAdmissionStatus, dr["AdmissionStatus"].ToString(), "AdmissionStatus");
                 tbReason.Text = dr["ReasonIfPending"].ToString();
                 ddlistPaymentMode.Enabled = false;
                 ddlistAcountsSession.Enabled = false;

             }
            
             con.Close();

             return flag;
        }

        private void setCorrectText(DropDownList ddlist, string value)
        {
            if (value == "" || value == "0")
            {
                ddlist.SelectedItem.Selected = false;
                ddlist.Items.Add("NF");
                ddlist.Items.FindByText("NF").Selected =true;
                ddlist.Items.FindByText("NF").Value = "0";

                ddlist.BackColor = System.Drawing.Color.FromName("#FDD499");
                ddlist.ForeColor = System.Drawing.Color.Black;

                //ddlist.Items.FindByText("NF").Attributes.Add("style", "background-color:Red;color:White");
              
            }
            else
            {
                try
                {
                    ddlist.SelectedItem.Selected = false;
                    ddlist.Items.FindByText(value).Selected = true;
                }
                catch
                {
                    ddlist.SelectedItem.Selected = false;
                    ddlist.Items.Add("NF");
                    ddlist.Items.FindByText("NF").Selected = true;
                    ddlist.Items.FindByText("NF").Value = "0";

                    ddlist.BackColor = System.Drawing.Color.FromName("#FDD499");
                    ddlist.ForeColor = System.Drawing.Color.Black;
                }
            }
            
        }

        private void setCorrectValue(DropDownList ddlist, string value, string cn)
        {

            if (value == "" || value == "0")
            {
                ddlist.SelectedItem.Selected = false;
                ddlist.Items.Add("NF");
                ddlist.Items.FindByText("NF").Selected = true;
                ddlist.Items.FindByText("NF").Value = "0";

                ddlist.BackColor = System.Drawing.Color.FromName("#FDD499");
                ddlist.ForeColor = System.Drawing.Color.Black;

                //ddlist.Items.FindByText("NF").Attributes.Add("style", "background-color:Red;color:White");
            }
            else
            {
                try
                {
                    ddlist.SelectedItem.Selected = false;
                    ddlist.Items.FindByValue(value).Selected = true;
                }
                catch
                {
                    if (ddlist.Items.Count>0)
                    {
                       
                        ddlist.SelectedItem.Selected = false;
                    }
                    ddlist.Items.Add("NF");
                    ddlist.Items.FindByText("NF").Selected = true;
                    ddlist.Items.FindByText("NF").Value = "0";

                    ddlist.BackColor = System.Drawing.Color.FromName("#FDD499");
                    ddlist.ForeColor = System.Drawing.Color.Black;

                }
            }
        }

        private void updateApplicationNo(string applicationno)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update Counter set DDEApplicationNo=@DDEApplicationNo ", con);

            cmd.Parameters.AddWithValue("@DistanceAppNo", applicationno);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
          
        } 

        private string getApplicationNo()
        {
            int an = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEApplicationNo from Counter", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                an = Convert.ToInt32(dr[1]) + 1;
            }

            con.Close();

            return an.ToString();

         
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            btnSubmit.Visible = false;
                try
                {
                    if (validEntry())
                    {
                        if (Request.QueryString["SRID"] == null)
                        {

                            if (ddlistStudyCentre.SelectedItem.Text != "091")
                            {
                                if (newANo(tbANo.Text))
                                {
                                    if (newStudent())
                                    {
                                        string error = "";

                                        //if (ddlistPaymentMode.SelectedItem.Value == "5" || ddlistPaymentMode.SelectedItem.Value == "6")
                                        //{
                                        //    tbTotalAmount.Text = "0";
                                        //}

                                        string frdate = ddlistDOAYear.SelectedItem.Value + "-" + ddlistDOAMonth.SelectedItem.Value + "-" + ddlistDOADay.SelectedItem.Value;
                                        int count = 0;
                                        int iid = 0;

                                        if (ddlistIns.Visible == true)
                                        {
                                            count = 2;
                                            iid = Convert.ToInt32(ddlistIns.SelectedItem.Value);
                                        }
                                        else if (ddlistIns.Visible == false)
                                        {
                                            count = 1;
                                            if (rblEntryType.SelectedItem.Value == "1")
                                            {
                                                iid = Convert.ToInt32(lblIID.Text);
                                            }
                                        }

                                        if (Accounts.validFee(0, Convert.ToInt32(ddlistCourses.SelectedItem.Value), 1, ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)), Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistCYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, 0, Convert.ToInt32(Session["ERID"]), ddlistSession.SelectedItem.Text, frdate, Convert.ToInt32(rblEntryType.SelectedItem.Value), count, iid, ddlistStudyCentre.SelectedItem.Text, out error))
                                        {
                                            string eno = "";
                                            string icno = "";
                                            int srid = 0;
                                            if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                                            {

                                                registerStudent(out eno, out icno);


                                                srid = FindInfo.findSRIDByENo(eno);
                                                if (Request.QueryString["PSRID"] != null)
                                                {
                                                    updateEnrollStatus(Convert.ToInt32(Request.QueryString["PSRID"]));
                                                    if (ddlistSession.SelectedItem.Text == "C 2015")
                                                    {
                                                        System.IO.File.Move(Server.MapPath("PendingStudentPhotos/" + Request.QueryString["PSRID"] + ".jpg"), Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"));
                                                        File.Copy(Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"), Server.MapPath("StudentPhotos/" + eno + ".jpg"));
                                                        File.Copy(Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"), Server.MapPath("OAPhotos/" + eno + ".jpg"));
                                                        File.Delete(Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"));
                                                    }

                                                }
                                            }
                                            else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                                            {
                                                registerStudent(out eno, out icno);
                                                srid = FindInfo.findSRIDByANo(tbANo.Text);
                                            }
                                            else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                                            {
                                                registerStudent(out eno, out icno);
                                                srid = FindInfo.findSRIDByANo(tbANo.Text);

                                            }

                                            if (eno != "AE" && eno != "")
                                            {

                                                Accounts.fillFee(srid, 1, ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)), Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistCYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, frdate, Convert.ToInt32(rblEntryType.SelectedItem.Value));

                                                if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                                                {
                                                    Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + eno + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                                }
                                                else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                                                {

                                                    Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Application No '" + tbANo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                                }

                                                if (rblTrans.SelectedItem.Text == "Yes")
                                                {
                                                    updateSCChangeRecord(srid);
                                                }
                                                if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
                                                {
                                                    insertCTMRecord(srid, ddlistPInst.SelectedItem.Value, ddlistExamination.SelectedItem.Value);
                                                }

                                                if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                                                {

                                                    if (rblEntryType.SelectedItem.Value == "1" && (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ddlistCYear.SelectedItem.Value))))
                                                    {
                                                        int res = registerForSLM(srid, ddlistStudyCentre.SelectedItem.Text, Convert.ToInt32(ddlistCourses.SelectedItem.Value), Convert.ToInt32(ddlistCYear.SelectedItem.Value));
                                                        if (res == 1)
                                                        {
                                                            lblMSG.Text = "Student has been registered successfully !! with <br/> Enrollment No. : " + eno + "<br/> I Card No. : " + icno + "<br/><span style='color:Green'>Student is registered for issuing SLM. </span>";
                                                        }
                                                        else
                                                        {
                                                            pnlData.Visible = false;
                                                            lblMSG.Text = "Sorry!! Student is registered successfully with <br/> Enrollment No.: " + eno + "<br/> I Card No. : " + icno + "<br/><span style='color:Red'> but 'SLM Record is not entered. Please contact ERP Developer'</span>";
                                                            pnlMSG.Visible = true;
                                                            btnOK.Visible = true;

                                                        }
                                                    }
                                                    else
                                                    {
                                                        lblMSG.Text = "Student has been registered successfully !! with <br/> Enrollment No. : " + eno + "<br/> I Card No. : " + icno;
                                                    }
                                                }
                                                else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                                                {

                                                    lblMSG.Text = "Student has been registered successfully !! with 'Pending' Status and with <br/> Application No. : " + tbANo.Text;
                                                }
                                                else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                                                {

                                                    lblMSG.Text = "Student has been registered successfully !! with 'Provisional' Status and with <br/> Application No. : " + tbANo.Text;
                                                }

                                                pnlData.Visible = false;
                                                pnlMSG.Visible = true;
                                                btnOK.Visible = false;
                                            }
                                            else
                                            {
                                                pnlData.Visible = false;
                                                lblMSG.Text = "Sorry!! This Alloted Enrollment No. is already exist.Please press submit button again.";
                                                pnlMSG.Visible = true;
                                                btnOK.Visible = true;
                                            }

                                        }
                                        else
                                        {
                                            pnlData.Visible = false;
                                            lblMSG.Text = error;
                                            pnlMSG.Visible = true;
                                            btnOK.Visible = true;

                                        }
                                    }
                                    else
                                    {
                                        pnlData.Visible = false;
                                        lblMSG.Text = "Sorry !! this student's detail is already exist. Do you still want to register this student?";
                                        pnlMSG.Visible = true;
                                        btnYes.Visible = true;
                                        btnNo.Visible = true;
                                    }
                                }
                                else
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = "Sorry !! this 'Application No.' is already exist";
                                    pnlMSG.Visible = true;
                                    btnOK.Visible = true;
                                }
                            }                            
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! Fresh Admissions are blocked for this Centre";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;
                            }
                            

                        }

                        else if (Request.QueryString["SRID"] != null)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandText = "update DDEStudentRecord set CYear=@CYear,Course=@Course,StudentName=@StudentName,FatherName=@FatherName,MotherName=@MotherName,Gender=@Gender,DOBDay=@DOBDay,DOBMonth=@DOBMonth,DOBYear=@DOBYear,CAddress=@CAddress,City=@City,District=@District,State=@State,PinCode=@PinCode,PhoneNo=@PhoneNo,MobileNo=@MobileNo,Email=@Email,AadhaarNo=@AadhaarNo,Nationality=@Nationality,Category=@Category,examname1=@examname1,examname2=@examname2,examname3=@examname3,examname4=@examname4,examname5=@examname5,subject1=@subject1,subject2=@subject2,subject3=@subject3,subject4=@subject4,subject5=@subject5,YearPass1=@YearPass1,YearPass2=@YearPass2,YearPass3=@YearPass3,YearPass4=@YearPass4,YearPass5=@YearPass5,UniversityBoard1=@UniversityBoard1,UniversityBoard2=@UniversityBoard2,UniversityBoard3=@UniversityBoard3,UniversityBoard4=@UniversityBoard4,UniversityBoard5=@UniversityBoard5,Divisiongrade1=@Divisiongrade1,Divisiongrade2=@Divisiongrade2,Divisiongrade3=@Divisiongrade3,Divisiongrade4=@Divisiongrade4,Divisiongrade5=@Divisiongrade5 where SRID='" + Request.QueryString["SRID"] + "'";

                            cmd.Parameters.AddWithValue("@CYear", getCorrectValue(ddlistCYear));
                            cmd.Parameters.AddWithValue("@Course", getCorrectValue(ddlistCourses));
                            cmd.Parameters.AddWithValue("@StudentName", tbSName.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@FatherName", tbFName.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@MotherName", tbMName.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Gender", getCorrectText(ddlistGender));
                            cmd.Parameters.AddWithValue("@DOBDay", getCorrectText(ddlistDOBDay));
                            cmd.Parameters.AddWithValue("@DOBMonth", getCorrectText(ddlistDOBMonth));
                            cmd.Parameters.AddWithValue("@DOBYear", getCorrectText(ddlistDOBYear));
                            cmd.Parameters.AddWithValue("@CAddress", tbPAddress.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@City", getCorrectText(ddlistCity));
                            cmd.Parameters.AddWithValue("@District", getCorrectText(ddlistCity));
                            cmd.Parameters.AddWithValue("@State", getCorrectText(ddlistState));
                            cmd.Parameters.AddWithValue("@PinCode", tbPinCode.Text);
                            cmd.Parameters.AddWithValue("@PhoneNo", tbPNo.Text);
                            cmd.Parameters.AddWithValue("@MobileNo", tbMNo.Text);
                            cmd.Parameters.AddWithValue("@Email", tbEAddress.Text);
                            cmd.Parameters.AddWithValue("@AadhaarNo", tbAadhaarNo.Text);

                            cmd.Parameters.AddWithValue("@Nationality", ddlistNationality.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@Category", getCorrectText(ddlistCategory));


                            cmd.Parameters.AddWithValue("@examname1", lblExam1.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@examname2", lblexam2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@examname3", lblExam3.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@examname4", lblexam4.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@examname5", lblexam5.Text.ToUpper());

                            cmd.Parameters.AddWithValue("@subject1", lblSubject.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@subject2", lblsubject2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@subject3", lblSubject3.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@subject4", lblsubject4.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@subject5", lblsubject5.Text.ToUpper());

                            cmd.Parameters.AddWithValue("@YearPass1", lblYearpass.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@YearPass2", lblyearpass2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@YearPass3", lblyearpass3.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@YearPass4", lblyearpass4.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@YearPass5", lblyearpass5.Text.ToUpper());

                            cmd.Parameters.AddWithValue("@UniversityBoard1", lblUniversityBoard.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@UniversityBoard2", lblUniversityBoard2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@UniversityBoard3", lblUniversityBoard3.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@UniversityBoard4", lblUniversityBoard4.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@UniversityBoard5", lblUniversityBoard5.Text.ToUpper());

                            cmd.Parameters.AddWithValue("@Divisiongrade1", lblDivisiongrade.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Divisiongrade2", lblDivision2.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Divisiongrade3", lblDivision3.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Divisiongrade4", lbldivision4.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Divisiongrade5", lbldivision5.Text.ToUpper());


                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();


                            Log.createLogNow("Update", "Updated record of student of EnrollmentNo '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                            pnlData.Visible = false;

                            lblMSG.Text = "Record has been updated successfully !!";
                            pnlMSG.Visible = true;

                        }
                    }
                    else
                    {
                        pnlData.Visible = false;

                        lblMSG.Text = "Sorry !! You did not select any of given entry";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;

                    }
                }
                catch (Exception ex)
                {
                    pnlData.Visible = false;

                    lblMSG.Text = ex.Message;
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;


                }
           

        } 
 
        private void updateEnrollStatus(int psrid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEPendingStudentRecord set Enrolled=@Enrolled where PSRID='" + psrid + "' ", con);

            cmd.Parameters.AddWithValue("@Enrolled", "True");
           
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private int registerForSLM(int srid, string sccode,int cid, int year)
        {
            int res = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESLMIssueRecord values(@SRID,@SCCode,@CID,@Year,@TOR,@LNo)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@SCCode", sccode);
            cmd.Parameters.AddWithValue("@CID", cid);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());     
            cmd.Parameters.AddWithValue("@LNo", 0);
       

            con.Open();
            res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        private void insertCTMRecord(int srid, string preins, string exam)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDECTPaperRecord values(@SRID,@Paper1,@Paper2,@Exam)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            if (preins == "1")
            {
                cmd.Parameters.AddWithValue("@Paper1", "CTM 1");
                cmd.Parameters.AddWithValue("@Paper2", "CTM 2");
            }
            else if (preins == "2")
            {
                cmd.Parameters.AddWithValue("@Paper1", "CTM 3");
                cmd.Parameters.AddWithValue("@Paper2", "CTM 4");
            }
            cmd.Parameters.AddWithValue("@Exam", exam);


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void updateSCChangeRecord(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDEChangeSCRecord values(@SRID,@PreviousSC,@CurrentSC,@TimeOfChange)", con);

            cmd.Parameters.AddWithValue("@SRID",srid);
            cmd.Parameters.AddWithValue("@PreviousSC", ddlistTransSC.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@CurrentSC", ddlistStudyCentre.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@TimeOfChange", DateTime.Now.ToString());


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

          
        }

        private string getCorrectText(DropDownList ddlist)
        {
            if (ddlist.SelectedItem.Text == "NF")
            {
                return "";

            }
            else
            {
                return ddlist.SelectedItem.Text;
            }
        }

        private int getCorrectValue(DropDownList ddlist)
        {
            if (ddlist.SelectedItem.Text == "NF")
            {
                return 0;

            }
            else
            {
                return Convert.ToInt32(ddlist.SelectedItem.Value);
            }
        }

        private bool validEntry()
        {
            bool validentry = false;

            if (Request.QueryString["SRID"] == null)
            {
                if (ddlistAdmissionType.SelectedItem.Value == "1")
                {
                    if (ddlistAdmissionStatus.SelectedItem.Text == "--SELECT ONE--" || ddlistSession.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionThrough.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionType.SelectedItem.Text == "--SELECT ONE--" || ddlistCourses.SelectedItem.Text == "--SELECT ONE--" || ddlistCYear.SelectedItem.Text == "--SELECT ONE--" || ddlistGender.SelectedItem.Text == "--SELECT ONE--" || ddlistCity.SelectedItem.Text == "--SELECT ONE--" || ddlsitEmploymentlist.SelectedItem.Text == "--SELECT ONE--" || ddlistAcountsSession.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistEligible.SelectedItem.Text == "--SELECT ONE--" || ddlistOriginalsVer.SelectedItem.Text == "--SELECT ONE--")
                    {
                        validentry = false;
                    }

                    else
                    {
                        validentry = true;
                    }

                }

                else
                {
                    if (ddlistAdmissionStatus.SelectedItem.Text == "--SELECT ONE--" || ddlistSession.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionThrough.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionType.SelectedItem.Text == "--SELECT ONE--" || ddlistPInst.SelectedItem.Text == "--SELECT ONE--" || ddlistPreCourse.SelectedItem.Text == "--SELECT ONE--" || ddlistCourses.SelectedItem.Text == "--SELECT ONE--" || ddlistCYear.SelectedItem.Text == "--SELECT ONE--" || ddlistGender.SelectedItem.Text == "--SELECT ONE--" || ddlistCity.SelectedItem.Text == "--SELECT ONE--" || ddlistState.SelectedItem.Text == "--SELECT ONE--" || ddlistCategory.SelectedItem.Text == "--SELECT ONE--" || ddlsitEmploymentlist.SelectedItem.Text == "--SELECT ONE--" || ddlistAcountsSession.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--" || ddlistEligible.SelectedItem.Text == "--SELECT ONE--" || ddlistOriginalsVer.SelectedItem.Text == "--SELECT ONE--")
                    {
                        validentry = false;
                    }

                    else
                    {
                        validentry = true;
                    }
                }
            }

            else if (Request.QueryString["SRID"] != null)
            {
                if (ddlistAdmissionType.SelectedItem.Value == "0")
                {
                    if (ddlistSession.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionThrough.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionType.SelectedItem.Text == "--SELECT ONE--" || ddlistCourses.SelectedItem.Text == "--SELECT ONE--" || ddlistCYear.SelectedItem.Text == "--SELECT ONE--" || ddlistGender.SelectedItem.Text == "--SELECT ONE--" || ddlistCity.SelectedItem.Text == "--SELECT ONE--" || ddlistState.SelectedItem.Text == "--SELECT ONE--" || ddlistCategory.SelectedItem.Text == "--SELECT ONE--" || ddlsitEmploymentlist.SelectedItem.Text == "--SELECT ONE--" || ddlistEligible.SelectedItem.Text == "--SELECT ONE--" || ddlistOriginalsVer.SelectedItem.Text == "--SELECT ONE--")
                    {
                        validentry = false;
                    }

                    else
                    {
                        validentry = true;
                    }

                }

                else  if (ddlistAdmissionType.SelectedItem.Value == "1")
                {
                    if (ddlistSession.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionThrough.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionType.SelectedItem.Text == "--SELECT ONE--" || ddlistCourses.SelectedItem.Text == "--SELECT ONE--" || ddlistCYear.SelectedItem.Text == "--SELECT ONE--" || ddlistGender.SelectedItem.Text == "--SELECT ONE--" || ddlistCity.SelectedItem.Text == "--SELECT ONE--" || ddlistState.SelectedItem.Text == "--SELECT ONE--" || ddlistCategory.SelectedItem.Text == "--SELECT ONE--" || ddlsitEmploymentlist.SelectedItem.Text == "--SELECT ONE--" || ddlistEligible.SelectedItem.Text == "--SELECT ONE--" ||  ddlistOriginalsVer.SelectedItem.Text == "--SELECT ONE--")
                    {
                        validentry = false;
                    }

                    else
                    {
                        validentry = true;
                    }

                }

                else if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
                {
                    if (ddlistSession.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionThrough.SelectedItem.Text == "--SELECT ONE--" || ddlistAdmissionType.SelectedItem.Text == "--SELECT ONE--" || ddlistPInst.SelectedItem.Text == "--SELECT ONE--" || ddlistPreCourse.SelectedItem.Text == "--SELECT ONE--" || ddlistCourses.SelectedItem.Text == "--SELECT ONE--" || ddlistCYear.SelectedItem.Text == "--SELECT ONE--" || ddlistGender.SelectedItem.Text == "--SELECT ONE--" || ddlistCity.SelectedItem.Text == "--SELECT ONE--" || ddlistState.SelectedItem.Text == "--SELECT ONE--" || ddlistCategory.SelectedItem.Text == "--SELECT ONE--" || ddlsitEmploymentlist.SelectedItem.Text == "--SELECT ONE--" || ddlistEligible.SelectedItem.Text == "--SELECT ONE--" || ddlistOriginalsVer.SelectedItem.Text == "--SELECT ONE--")
                    {
                        validentry = false;
                    }

                    else
                    {
                        validentry = true;
                    }
                }

            }

            return validentry;
           
        }

        private void registerStudent( out string eno, out string icno)
        {
           
            eno="";
            icno = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

          
            if (Request.QueryString["PSRID"]!= null)
            {
                cmd.CommandText = "insert into DDEStudentRecord values (@PSRID,@ProANo,@ApplicationNo,@SCStatus,@StudyCentreCode,@PreviousSCCode,@Session,@SyllabusSession,@EnrollmentNo,@ICardNo,@AdmissionThrough,@AdmissionType,@PreviousInstitute,@PreviousCourse,@RollNoIYear,@RollNoIIYear,@RollNoIIIYear,@RollNoBP,@CYear,@FirstYear,@SecondYear,@ThirdYear,@Course,@Course2Year,@Course3Year,@StudentName,@FatherName,@MotherName,@StudentPhoto,@Gender,@DOBDay,@DOBMonth,@DOBYear,@CAddress,@City,@District,@State,@PinCode,@PhoneNo,@MobileNo,@Email,@AadhaarNo,@DOA,@VDOA,@DDNumber,@DDDay,@DDMonth,@DDYear,@IssuingBankName,@DDAmount,@DDAmountInwords,@Nationality,@Category,@Employmentstatus,@examname1,@examname2,@examname3,@examname4,@examname5,@subject1,@subject2,@subject3,@subject4,@subject5,@YearPass1,@YearPass2,@YearPass3,@YearPass4,@YearPass5,@UniversityBoard1,@UniversityBoard2,@UniversityBoard3,@UniversityBoard4,@UniversityBoard5,@Divisiongrade1,@Divisiongrade2,@Divisiongrade3,@Divisiongrade4,@Divisiongrade5,@Eligible,@CourseFeePaid,@FeeRecIssued,@OriginalsVerified,@QualifyingStatus,@RecordStatus,@AdmissionStatus,@ReasonIfPending)";
                cmd.Parameters.AddWithValue("@PSRID", Convert.ToInt32(Request.QueryString["PSRID"]));
            }
            else
            {
                cmd.CommandText = "insert into DDEStudentRecord (PSRID,ProANo,ApplicationNo,SCStatus,StudyCentreCode,PreviousSCCode,Session,SyllabusSession,EnrollmentNo,ICardNo,AdmissionThrough,AdmissionType,PreviousInstitute,PreviousCourse,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,FirstYear,SecondYear,ThirdYear,Course,Course2Year,Course3Year,StudentName,FatherName,MotherName,Gender,DOBDay,DOBMonth,DOBYear,CAddress,City,District,State,PinCode,PhoneNo,MobileNo,Email,AadhaarNo,DOA,VDOA,DDNumber,DDDay,DDMonth,DDYear,IssuingBankName,DDAmount,DDAmountInwords,Nationality,Category,Employmentstatus,examname1,examname2,examname3,examname4,examname5,subject1,subject2,subject3,subject4,subject5,YearPass1,YearPass2,YearPass3,YearPass4,YearPass5,UniversityBoard1,UniversityBoard2,UniversityBoard3,UniversityBoard4,UniversityBoard5,Divisiongrade1,Divisiongrade2,Divisiongrade3,Divisiongrade4,Divisiongrade5,Eligible,CourseFeePaid,FeeRecIssued,OriginalsVerified,QualifyingStatus,RecordStatus,AdmissionStatus,ReasonIfPending) values (@PSRID,@ProANo,@ApplicationNo,@SCStatus,@StudyCentreCode,@PreviousSCCode,@Session,@SyllabusSession,@EnrollmentNo,@ICardNo,@AdmissionThrough,@AdmissionType,@PreviousInstitute,@PreviousCourse,@RollNoIYear,@RollNoIIYear,@RollNoIIIYear,@RollNoBP,@CYear,@FirstYear,@SecondYear,@ThirdYear,@Course,@Course2Year,@Course3Year,@StudentName,@FatherName,@MotherName,@Gender,@DOBDay,@DOBMonth,@DOBYear,@CAddress,@City,@District,@State,@PinCode,@PhoneNo,@MobileNo,@Email,@AadhaarNo,@DOA,@VDOA,@DDNumber,@DDDay,@DDMonth,@DDYear,@IssuingBankName,@DDAmount,@DDAmountInwords,@Nationality,@Category,@Employmentstatus,@examname1,@examname2,@examname3,@examname4,@examname5,@subject1,@subject2,@subject3,@subject4,@subject5,@YearPass1,@YearPass2,@YearPass3,@YearPass4,@YearPass5,@UniversityBoard1,@UniversityBoard2,@UniversityBoard3,@UniversityBoard4,@UniversityBoard5,@Divisiongrade1,@Divisiongrade2,@Divisiongrade3,@Divisiongrade4,@Divisiongrade5,@Eligible,@CourseFeePaid,@FeeRecIssued,@OriginalsVerified,@QualifyingStatus,@RecordStatus,@AdmissionStatus,@ReasonIfPending)";
                cmd.Parameters.AddWithValue("@PSRID", 0);
            }

            if (ddlistAdmissionStatus.SelectedItem.Value == "3")
            {
                cmd.Parameters.AddWithValue("@ProANo", tbANo.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProANo", "NA");
            }
            cmd.Parameters.AddWithValue("@ApplicationNo", tbANo.Text);
            if (rblTrans.SelectedItem.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@SCStatus", "T");
                cmd.Parameters.AddWithValue("@StudyCentreCode", getCorrectText(ddlistStudyCentre));
                cmd.Parameters.AddWithValue("@PreviousSCCode", getCorrectText(ddlistTransSC));
               
            }
            else if (rblTrans.SelectedItem.Text == "No")
            {
                cmd.Parameters.AddWithValue("@SCStatus", "O");
                cmd.Parameters.AddWithValue("@StudyCentreCode", getCorrectText(ddlistStudyCentre));
                cmd.Parameters.AddWithValue("@PreviousSCCode", "");
               
            }
           
            cmd.Parameters.AddWithValue("@Session", ddlistSession.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@SyllabusSession", ddlistSySession.SelectedItem.Text);

            if(Convert.ToInt32(ddlistSession.SelectedItem.Value)<=7)
            {
                eno= tbENo.Text;
                icno = tbICNo.Text;
                
            }

            else if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >7)
            {
               
                if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                {
                    //if (Accounts.isFullCourseFeePaid(Convert.ToInt32(tbStudentAmount.Text) ,Convert.ToInt32(ddlistCourses.SelectedItem.Value), ddlistSession.SelectedItem.Text))
                    //{
                        eno = allotEnrollmentNo();
                        if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 1 && Convert.ToInt32(ddlistSession.SelectedItem.Value) <= 12)
                        {
                            icno = eno.Substring(0, 3) + "-" + ddlistStudyCentre.SelectedItem.Text + eno.Substring(6, 5);
                        }
                        else if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 13 && Convert.ToInt32(ddlistSession.SelectedItem.Value) <= 20)
                        {
                            icno = eno.Substring(0, 3) + "-" + ddlistStudyCentre.SelectedItem.Text + eno.Substring(9, 5);
                        }
                        else if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 21)
                        {
                            icno = eno.Substring(0, 3) + "-" + eno.Substring(6, 5);
                        }
                    //}
                    //else
                    //{
                    //    eno = "";
                    //    icno = "";
                    //}
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                {
                    eno = "";
                    icno = "";
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                {
                    eno = "";
                    icno = "";
                }
                 
            }
            cmd.Parameters.AddWithValue("@EnrollmentNo", eno);
            cmd.Parameters.AddWithValue("@ICardNo", icno);
            cmd.Parameters.AddWithValue("@AdmissionThrough", Convert.ToInt32(ddlistAdmissionThrough.SelectedItem.Value));


            if (ddlistAdmissionType.SelectedItem.Value == "1")
            {
                cmd.Parameters.AddWithValue("@AdmissionType", Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@PreviousInstitute", "");
                cmd.Parameters.AddWithValue("@PreviousCourse", "");
            }

            else if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
            {
                cmd.Parameters.AddWithValue("@AdmissionType", Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@PreviousInstitute", Convert.ToInt32(ddlistPInst.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@PreviousCourse", Convert.ToInt32(ddlistPreCourse.SelectedItem.Value));
            }

            cmd.Parameters.AddWithValue("@RollNoIYear", "");
            cmd.Parameters.AddWithValue("@RollNoIIYear", "");
            cmd.Parameters.AddWithValue("@RollNoIIIYear", "");
            cmd.Parameters.AddWithValue("@RollNoBP", "");
            cmd.Parameters.AddWithValue("@CYear", Convert.ToInt32(ddlistCYear.SelectedItem.Value));
            if (ddlistCYear.SelectedItem.Value == "1")
            {
                cmd.Parameters.AddWithValue("@FirstYear", "True");
                cmd.Parameters.AddWithValue("@SecondYear", "False");
                cmd.Parameters.AddWithValue("@ThirdYear", "False");
            }
            else if (ddlistCYear.SelectedItem.Value == "2")
            {
                cmd.Parameters.AddWithValue("@FirstYear", "False");
                cmd.Parameters.AddWithValue("@SecondYear", "True");
                cmd.Parameters.AddWithValue("@ThirdYear", "False");
            }
            else if (ddlistCYear.SelectedItem.Value == "3")
            {
                cmd.Parameters.AddWithValue("@FirstYear", "False");
                cmd.Parameters.AddWithValue("@SecondYear", "False");
                cmd.Parameters.AddWithValue("@ThirdYear", "True");
            }
            if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourses.SelectedItem.Value)) == "MBA")
            {
                if (ddlistCYear.SelectedItem.Value == "1" && ddlistAdmissionType.SelectedItem.Value == "1")
                {
                    cmd.Parameters.AddWithValue("@Course", 76);
                    cmd.Parameters.AddWithValue("@Course2Year", "");
                    cmd.Parameters.AddWithValue("@Course3Year", "");
                }
                else if (ddlistCYear.SelectedItem.Value == "2" && ddlistAdmissionType.SelectedItem.Value=="2")
                {
                    cmd.Parameters.AddWithValue("@Course", 76);
                    cmd.Parameters.AddWithValue("@Course2Year", ddlistCourses.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Course3Year", ddlistCourses.SelectedItem.Value);
                }
                else if (ddlistCYear.SelectedItem.Value == "2" && ddlistAdmissionType.SelectedItem.Value == "3")
                {
                    cmd.Parameters.AddWithValue("@Course", 76);
                    cmd.Parameters.AddWithValue("@Course2Year", ddlistCourses.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Course3Year", ddlistCourses.SelectedItem.Value);
                }
               
            }
            else
            {
                cmd.Parameters.AddWithValue("@Course", ddlistCourses.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Course2Year", "");
                cmd.Parameters.AddWithValue("@Course3Year", "");
            }
            cmd.Parameters.AddWithValue("@StudentName", tbSName.Text.ToUpper());
            cmd.Parameters.AddWithValue("@FatherName", tbFName.Text.ToUpper());
            cmd.Parameters.AddWithValue("@MotherName", tbMName.Text.ToUpper());
            
            if (Request.QueryString["PSRID"] != null)
            {
                cmd.Parameters.AddWithValue("@StudentPhoto", FindInfo.findPendingStudentPhoto(Convert.ToInt32(Request.QueryString["PSRID"])));
            }
            
           
            cmd.Parameters.AddWithValue("@Gender", ddlistGender.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@DOBDay", ddlistDOBDay.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@DOBMonth", ddlistDOBMonth.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@DOBYear", ddlistDOBYear.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@CAddress", tbPAddress.Text.ToUpper());
            cmd.Parameters.AddWithValue("@City", ddlistCity.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@District", ddlistCity.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@State", ddlistState.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@PinCode", tbPinCode.Text);
            cmd.Parameters.AddWithValue("@PhoneNo", tbPNo.Text);
            cmd.Parameters.AddWithValue("@MobileNo", tbMNo.Text);
            cmd.Parameters.AddWithValue("@Email", tbEAddress.Text);
            cmd.Parameters.AddWithValue("@AadhaarNo", tbAadhaarNo.Text);
            cmd.Parameters.AddWithValue("@DOA", ddlistDOAYear.SelectedItem.Text + "-" + ddlistDOAMonth.SelectedItem.Value + "-" + ddlistDOADay.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@VDOA", FindInfo.findDOAByBatch(Convert.ToInt32(ddlistSession.SelectedItem.Value)));
            cmd.Parameters.AddWithValue("@DDNumber", "");
            cmd.Parameters.AddWithValue("@DDDay", "");
            cmd.Parameters.AddWithValue("@DDMonth", "");
            cmd.Parameters.AddWithValue("@DDYear", "");
            cmd.Parameters.AddWithValue("@IssuingBankName", "");
            cmd.Parameters.AddWithValue("@DDAmount", "");
            cmd.Parameters.AddWithValue("@DDAmountInwords", "");
            cmd.Parameters.AddWithValue("@Nationality", ddlistNationality.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Category", ddlistCategory.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Employmentstatus", ddlsitEmploymentlist.SelectedItem.Text);

            cmd.Parameters.AddWithValue("@examname1", lblExam1.Text.ToUpper());
            cmd.Parameters.AddWithValue("@examname2", lblexam2.Text.ToUpper());
            cmd.Parameters.AddWithValue("@examname3", lblExam3.Text.ToUpper());
            cmd.Parameters.AddWithValue("@examname4", lblexam4.Text.ToUpper());
            cmd.Parameters.AddWithValue("@examname5", lblexam5.Text.ToUpper());

            cmd.Parameters.AddWithValue("@subject1", lblSubject.Text.ToUpper());
            cmd.Parameters.AddWithValue("@subject2", lblsubject2.Text.ToUpper());
            cmd.Parameters.AddWithValue("@subject3", lblSubject3.Text.ToUpper());
            cmd.Parameters.AddWithValue("@subject4", lblsubject4.Text.ToUpper());
            cmd.Parameters.AddWithValue("@subject5", lblsubject5.Text.ToUpper());

            cmd.Parameters.AddWithValue("@YearPass1", lblYearpass.Text.ToUpper());
            cmd.Parameters.AddWithValue("@YearPass2", lblyearpass2.Text.ToUpper());
            cmd.Parameters.AddWithValue("@YearPass3", lblyearpass3.Text.ToUpper());
            cmd.Parameters.AddWithValue("@YearPass4", lblyearpass4.Text.ToUpper());
            cmd.Parameters.AddWithValue("@YearPass5", lblyearpass5.Text.ToUpper());

            cmd.Parameters.AddWithValue("@UniversityBoard1", lblUniversityBoard.Text.ToUpper());
            cmd.Parameters.AddWithValue("@UniversityBoard2", lblUniversityBoard2.Text.ToUpper());
            cmd.Parameters.AddWithValue("@UniversityBoard3", lblUniversityBoard3.Text.ToUpper());
            cmd.Parameters.AddWithValue("@UniversityBoard4", lblUniversityBoard4.Text.ToUpper());
            cmd.Parameters.AddWithValue("@UniversityBoard5", lblUniversityBoard5.Text.ToUpper());

            cmd.Parameters.AddWithValue("@Divisiongrade1", lblDivisiongrade.Text.ToUpper());
            cmd.Parameters.AddWithValue("@Divisiongrade2", lblDivision2.Text.ToUpper());
            cmd.Parameters.AddWithValue("@Divisiongrade3", lblDivision3.Text.ToUpper());
            cmd.Parameters.AddWithValue("@Divisiongrade4", lbldivision4.Text.ToUpper());
            cmd.Parameters.AddWithValue("@Divisiongrade5", lbldivision5.Text.ToUpper());

            cmd.Parameters.AddWithValue("@Eligible", ddlistEligible.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@CourseFeePaid", "");
            cmd.Parameters.AddWithValue("@FeeRecIssued", "");
            cmd.Parameters.AddWithValue("@OriginalsVerified", ddlistOriginalsVer.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@QualifyingStatus", "AC");

            if (ddlistAdmissionStatus.SelectedItem.Value == "1")
            {
                cmd.Parameters.AddWithValue("@RecordStatus", "True");
            }
            else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
            {
                cmd.Parameters.AddWithValue("@RecordStatus", "False");
            }
            else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
            {
                cmd.Parameters.AddWithValue("@RecordStatus", "False");
            }

            cmd.Parameters.AddWithValue("@AdmissionStatus", Convert.ToInt32(ddlistAdmissionStatus.SelectedItem.Value));

            if (ddlistAdmissionStatus.SelectedItem.Value == "1")
            {
                cmd.Parameters.AddWithValue("@ReasonIfPending", "NA");
            }
            else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
            {
                cmd.Parameters.AddWithValue("@ReasonIfPending", tbReason.Text.ToUpper());
            }
            else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
            {
                cmd.Parameters.AddWithValue("@ReasonIfPending", tbReason.Text.ToUpper());
            }
            

            cmd.Connection = con;
            if (eno!="AE" && eno!="")
            {
                con.Open();

                cmd.ExecuteNonQuery();
                con.Close();

                if (Convert.ToInt32(ddlistSession.SelectedItem.Value) > 7)
                {
                    if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                    {
                        FindInfo.updateEnrollmentCounter(Convert.ToInt32(ddlistSession.SelectedItem.Value), counter);
                    }
                }

                if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                {
                    FindInfo.updateProANo(tbANo.Text);
                }


                if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                {
                    Log.createLogNow("Create", "Registered a student with Enrollment No'" + eno + "'", Convert.ToInt32(Session["ERID"].ToString()));
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                {
                    Log.createLogNow("Create", "Registered a student with Pending Status with Application No '" + tbANo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                {
                    Log.createLogNow("Create", "Registered a student with Provisional Status with Application No '" + tbANo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                }
            }

        }

        private string allotEnrollmentNo()
        {
            string eno="";
            
            int pcode =FindInfo.findProgrammeCode(ddlistCourses.SelectedItem.Value);
            if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 23)
            {
                counter = FindInfo.findENoCounterByExamCode(FindInfo.findApplicableExamByBatch(ddlistSession.SelectedItem.Text));
            }
            else
            {
                counter = FindInfo.findCounter(ddlistSession.SelectedItem.Text);
            }
           
          
            string finalcounter = string.Format("{0:00000}", counter);

            if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 1 && Convert.ToInt32(ddlistSession.SelectedItem.Value) <= 12)
            {

                eno = ddlistSession.SelectedItem.Text.Substring(0, 1) + ddlistSession.SelectedItem.Text.Substring(4, 2) + "20" + pcode.ToString() + finalcounter;

            }
            else if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 13 && Convert.ToInt32(ddlistSession.SelectedItem.Value) <= 20)
            {
                if (ddlistStudyCentre.SelectedItem.Text == "995")
                {
                    eno = ddlistSession.SelectedItem.Text.Substring(0, 1) + ddlistSession.SelectedItem.Text.Substring(4, 2) + "20000" + pcode.ToString() + finalcounter;
                }
                else
                {
                    eno = ddlistSession.SelectedItem.Text.Substring(0, 1) + ddlistSession.SelectedItem.Text.Substring(4, 2) + "20" + ddlistStudyCentre.SelectedItem.Text + pcode.ToString() + finalcounter;
                }
            }
            else if (Convert.ToInt32(ddlistSession.SelectedItem.Value) >= 21)
            {
                string sessioncode = FindInfo.findSessionCodeByID(Convert.ToInt32(ddlistSession.SelectedItem.Value));
                if (sessioncode != "NF")
                {
                    eno = sessioncode + "20" + pcode.ToString() + finalcounter;
                }
                else
                {
                    eno = "NA";

                }
            }

            if (FindInfo.isENoAlreadyExist(eno))
            {
                eno = "AE";
            }

            

            return eno;
                     
        }

        private bool newANo(string ano)
        {
         
            bool newstudent = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
        
            SqlCommand cmd = new SqlCommand("Select ApplicationNo from DDEStudentRecord where ApplicationNo='"+ano.Trim()+"'", con);
           
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString().Trim() == ano.Trim())
                {
                   newstudent = false;
                }

            }
            else
            {
                newstudent = true;
            }
          

            con.Close();

            return newstudent;
        }

        private bool newStudent()
        {

            bool newstudent = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
                  
            SqlCommand cmd = new SqlCommand("Select DOBDay,DOBMonth,DOBYear from DDEStudentRecord where StudentName='" + tbSName.Text + "' and FatherName='"+tbFName.Text+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();
           
            if (dr.HasRows)
            {
                dr.Read();
                string ndob = ddlistDOBDay.SelectedItem.Text + "-" + ddlistDOBMonth.SelectedItem.Text + "-" + ddlistDOBYear.SelectedItem.Text;
                string edob = dr["DOBDay"].ToString() + "-" + dr["DOBMonth"].ToString() + "-" + dr["DOBYear"].ToString();
                if (ndob == edob)
                {
                    newstudent = false;
                }
              
            }
            

            con.Close();

            return newstudent;
        } 

        protected void btnReset_Click(object sender, EventArgs e)
        {

            Response.Redirect("DStudentRegistration.aspx");

            
        }

        protected void ddlistPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
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
                pnlDDFee.Visible = true;

            }
            else
            {
                pnlDDFee.Visible = false;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlData.Visible = true;
           
        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["SRID"] == null)
            {

                if (Convert.ToInt32(ddlistSession.SelectedItem.Value) <= 7)
                {
                    tbENo.Enabled = true;
                }
                else
                {
                    tbENo.Enabled = false;
                }
            }

            else if (Request.QueryString["SRID"] != null)
            {
                tbENo.Enabled = false;
            }

            if(Convert.ToInt32(ddlistSession.SelectedItem.Value)>=25)
            {
                ddlistSySession.SelectedItem.Selected = false;
                ddlistSySession.Items.FindByText("A 2020-21").Selected = true;
                ddlistSySession.Enabled = false;
            }

            setExam(ddlistSession.SelectedItem.Text, Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value));
            
        }

        private void setExam(string batch, int at)
        {
            string exam = FindInfo.findApplicableExamByBatch(batch,at);
            ddlistExamination.Enabled = true;
            ddlistExamination.SelectedItem.Selected = false;
            ddlistExamination.Items.FindByValue(exam).Selected = true;
            ddlistExamination.SelectedValue = exam;
            ddlistExamination.Enabled = false;
        }

        protected void ddlistAdmissionType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (ddlistAdmissionType.SelectedItem.Value == "0")
            {
                pnlCT.Visible = false;

            }
            else  if (ddlistAdmissionType.SelectedItem.Value == "1")
            {
                pnlCT.Visible = false;
               
            }
            else if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
            {
                pnlCT.Visible = true;
                
            }
            if (ddlistCourses.SelectedItem.Text!="--SELECT ONE--")
            {
                setYearList(Convert.ToInt32(ddlistCourses.SelectedItem.Value));
            }

            if (ddlistStudyCentre.SelectedItem.Value != "0")
            {
                if (Request.QueryString["PSRID"] == null)
                {
                    PopulateDDList.populateCoursesBySCStreamsandAT(ddlistCourses, Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value), Convert.ToInt32(ddlistStudyCentre.SelectedItem.Value));
                }
            }
            

            checkYear();
        }

        protected void ddlistCYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            

             checkYear();
           
           
        }

        private void checkYear()
        {
            try
            {
                if (Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value) == 1)
                {
                    lblFeeHeading.Text = "Details Of 1st Year Fee Payment : ";

                   

                  
                    //else if (ddlistCYear.SelectedItem.Value == "2")
                    //{
                    //    pnlData.Visible = false;

                    //    lblMSG.Text = "Sorry !! 'Regular' Student can not be registered in 2nd Year";
                    //    pnlMSG.Visible = true;
                    //    btnOK.Visible = true;
                    //}
                    //else if (ddlistCYear.SelectedItem.Value == "3")
                    //{
                    //    pnlData.Visible = false;

                    //    lblMSG.Text = "Sorry !! 'Regular' Student can not be registered in 3rd Year";
                    //    pnlMSG.Visible = true;
                    //    btnOK.Visible = true;
                    //}

                }

                else if (Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value) == 2)
                {
                    if (ddlistCYear.SelectedItem.Value == "2")
                    {
                        lblFeeHeading.Text = "Details Of 2nd Year Fee Payment : ";
                    }
                    else if (ddlistCYear.SelectedItem.Value == "3")
                    {
                        lblFeeHeading.Text = "Details Of 3rd Year Fee Payment : ";
                    }
                }

                else if (Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value) == 0)
                {
                    pnlData.Visible = false;

                    lblMSG.Text = "Please Select Admission Type Before Selection of Year";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
            catch
            {
                pnlData.Visible = true;

                //lblMSG.Text = "Please Select Admission Type Before Selection of Year";
                //pnlMSG.Visible = true;
                //btnOK.Visible = true;
            }
        }

        protected void ddlistAdmissionThrough_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["SRID"] == null)
            {
                if (ddlistAdmissionThrough.SelectedItem.Text != "--SELECT ONE--" && ddlistAdmissionThrough.SelectedItem.Text != "NF")
                {
                    ddlistStudyCentre.Items.Clear();
                    PopulateDDList.populateStudyCentreByAT(ddlistStudyCentre, ddlistAdmissionThrough.SelectedItem.Value);
                }

                else
                {
                    ddlistStudyCentre.Items.Clear();
                    ddlistStudyCentre.Items.Add("--SELECT ONE--");

                }
            }
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
                    lblNewDD.Text = "Sorry ! this Instrument does not exist";
                    lblNewDD.Visible = true;

                }
                btnSubmit.Visible = true;
                ddlistAcountsSession.Enabled = true;
            }
            else if (rblEntryType.SelectedItem.Value == "1" || rblEntryType.SelectedItem.Value == "3")
            {
                string error;
                int iid;
                bool tran;
                string scmode;
                int count;
                string ardate;
                if (rblTrans.SelectedItem.Text == "Yes")
                {
                    tran = true;
                }
                else
                {
                    tran = false;
                }
                if (Accounts.validInstrument(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value, ddlistStudyCentre.SelectedItem.Text,tran,ddlistTransSC.SelectedItem.Text, out iid,out scmode,out count,out ardate, out error))
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
                        if (ardate != "")
                        {
                            lblARDate.Text = "Amount. Rec. On : " + Convert.ToDateTime(ardate).ToString("dd-MM-yyyy");
                            lblARPDate.Text = Convert.ToDateTime(ardate).ToString("yyyy-MM-dd"); ;
                        }
                        else
                        {
                            lblARDate.Text = "";
                            lblARPDate.Text = "";
                        }
                        lblNewDD.Visible = false;
                        lblIID.Text = iid.ToString();
                        ViewState["noi"] = 1;
                        btnSubmit.Visible = true;

                        tbDDNumber.Enabled = false;
                        ddlistDDDay.Enabled = false;
                        ddlistDDMonth.Enabled = false;
                        ddlistDDYear.Enabled = false;
                        tbTotalAmount.Enabled = false;
                        tbIBN.Enabled = false;
                        tbStudentAmount.Enabled = true;

                        //if (Request.QueryString["PSRID"] != null)
                        //{
                        if (ddlistStudyCentre.SelectedItem.Text == "999" || ddlistStudyCentre.SelectedItem.Text == "998" || ddlistStudyCentre.SelectedItem.Text == "997" || ddlistStudyCentre.SelectedItem.Text == "996" || ddlistStudyCentre.SelectedItem.Text == "995" || ddlistStudyCentre.SelectedItem.Text == "994" || ddlistStudyCentre.SelectedItem.Text == "993" || ddlistStudyCentre.SelectedItem.Text == "462")
                        {
                            tbStudentAmount.Text = ((FindInfo.findCourseFeeByBatch(Convert.ToInt32(ddlistCourses.SelectedItem.Value), Convert.ToInt32(ddlistSession.SelectedItem.Value))) / 2).ToString();
                        }
                        else
                        {
                            tbStudentAmount.Text = FindInfo.findCourseFeeByBatch(Convert.ToInt32(ddlistCourses.SelectedItem.Value), Convert.ToInt32(ddlistSession.SelectedItem.Value)).ToString();
                        }
                            
                           
                        //}
                    }
                    else if (count > 1)
                    {
                        tbDDNumber.Visible = false;
                        lnkbtnFDCDetails.Visible = false;
                        PopulateDDList.populateSameInsByIno(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value, ddlistIns);
                        ddlistIns.Visible = true;
                        lblNewDD.Visible = false;
                        ViewState["noi"] = 2;


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

        protected void ddlistAdmissionStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistAdmissionStatus.SelectedItem.Value == "1")
            {
                if (tbANo.Enabled == false)
                {
                    tbANo.Text = "";
                    
                }
                tbANo.Enabled = true;
                lblReason.Visible = false;
                tbReason.Visible = false;
            }
            else if (ddlistAdmissionStatus.SelectedItem.Value == "2" )
            {
                if (tbANo.Enabled == false)
                {
                    tbANo.Text = "";
                   
                }
                tbANo.Enabled = true;
                lblReason.Visible = true;
                tbReason.Visible = true;
            }
            else if (ddlistAdmissionStatus.SelectedItem.Value == "3" )
            {
                tbANo.Text = FindInfo.findProANo();
                tbANo.Enabled = false;
                lblReason.Visible = true;
                tbReason.Visible = true;
            }
           
        }

        protected void ddlistStudyCentre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistStudyCentre.SelectedItem.Value != "0")
            {              
                PopulateDDList.populateCoursesBySCStreamsandAT(ddlistCourses,Convert.ToInt32(ddlistAdmissionType.SelectedItem.Value),Convert.ToInt32(ddlistStudyCentre.SelectedItem.Value));
            }
        }

        protected void ddlistCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Request.QueryString["SRID"] == null)
            {
                setYearList(Convert.ToInt32(ddlistCourses.SelectedItem.Value));
                setSySession(Convert.ToInt32(ddlistCourses.SelectedItem.Value));
            }
        
        }

        private void setSySession(int cid)
        {
            ddlistSySession.SelectedItem.Selected = false;

            if(Convert.ToInt32(ddlistSession.SelectedItem.Value)>=25)
            {
                ddlistSySession.Items.FindByText("A 2020-21").Selected = true;
            }
            else
            {
                if (cid == 12 || cid == 27 || (FindInfo.findCourseShortNameByID(cid) == "MBA"))
                {
                    ddlistSySession.Items.FindByText("A 2013-14").Selected = true;
                }
                else
                {
                    ddlistSySession.Items.FindByText("A 2010-11").Selected = true;
                }
            }
           
        }

        private void setYearList(int cid)
        {
            int duration = FindInfo.findCourseDuration(cid);
            ddlistCYear.Items.Clear();

            if (Request.QueryString["SRID"] == null)
            {

                if (duration == 1)
                {

                    ddlistCYear.Items.Add("1ST YEAR");
                    ddlistCYear.Items.FindByText("1ST YEAR").Value = "1";

                }
                else if (duration == 2)
                {

                    if (ddlistAdmissionType.SelectedItem.Value == "1")
                    {
                        ddlistCYear.Items.Add("1ST YEAR");
                        ddlistCYear.Items.FindByText("1ST YEAR").Value = "1";

                    }
                    else if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
                    {

                        ddlistCYear.Items.Add("2ND YEAR");
                        ddlistCYear.Items.FindByText("2ND YEAR").Value = "2";
                    }

                }
                else if (duration == 3)
                {
                    if (ddlistAdmissionType.SelectedItem.Value == "1")
                    {
                        ddlistCYear.Items.Add("1ST YEAR");
                        ddlistCYear.Items.FindByText("1ST YEAR").Value = "1";

                    }
                    else if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
                    {
                        ddlistCYear.Items.Add("2ND YEAR");
                        ddlistCYear.Items.FindByText("2ND YEAR").Value = "2";

                        ddlistCYear.Items.Add("3RD YEAR");
                        ddlistCYear.Items.FindByText("3RD YEAR").Value = "3";
                    }

                }
            }
            else if (Request.QueryString["SRID"] != null)
            {
                if (duration == 1)
                {

                    ddlistCYear.Items.Add("1ST YEAR");
                    ddlistCYear.Items.FindByText("1ST YEAR").Value = "1";

                }
                else if (duration == 2)
                {

                    if (ddlistAdmissionType.SelectedItem.Value == "1")
                    {
                        ddlistCYear.Items.Add("1ST YEAR");
                        ddlistCYear.Items.FindByText("1ST YEAR").Value = "1";

                        ddlistCYear.Items.Add("2ND YEAR");
                        ddlistCYear.Items.FindByText("2ND YEAR").Value = "2";

                    }
                    else if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
                    {

                        ddlistCYear.Items.Add("2ND YEAR");
                        ddlistCYear.Items.FindByText("2ND YEAR").Value = "2";
                    }
                }
                else if (duration == 3)
                {
                    if (ddlistAdmissionType.SelectedItem.Value == "1")
                    {
                        if (FindInfo.isMBASpecialazation(cid))
                        {
                           
                            ddlistCYear.Items.Add("2ND YEAR");
                            ddlistCYear.Items.FindByText("2ND YEAR").Value = "2";

                            ddlistCYear.Items.Add("3RD YEAR");
                            ddlistCYear.Items.FindByText("3RD YEAR").Value = "3";
                        }
                        else
                        {
                            ddlistCYear.Items.Add("1ST YEAR");
                            ddlistCYear.Items.FindByText("1ST YEAR").Value = "1";

                            ddlistCYear.Items.Add("2ND YEAR");
                            ddlistCYear.Items.FindByText("2ND YEAR").Value = "2";

                            ddlistCYear.Items.Add("3RD YEAR");
                            ddlistCYear.Items.FindByText("3RD YEAR").Value = "3";
                        }


                    }
                    else if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
                    {
                        ddlistCYear.Items.Add("2ND YEAR");
                        ddlistCYear.Items.FindByText("2ND YEAR").Value = "2";

                        ddlistCYear.Items.Add("3RD YEAR");
                        ddlistCYear.Items.FindByText("3RD YEAR").Value = "3";
                    }

                }

               
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

        protected void rblTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTrans.SelectedItem.Text == "Yes")
            {
                lblTrans.Visible = true;
                PopulateDDList.populateStudyCentre(ddlistTransSC);
                ddlistTransSC.Visible = true;
            }
            else if (rblTrans.SelectedItem.Text == "No")
            {
                lblTrans.Visible = false;
                ddlistTransSC.Visible = false;
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

            if (ddlistStudyCentre.SelectedItem.Text == "999" || ddlistStudyCentre.SelectedItem.Text == "998" || ddlistStudyCentre.SelectedItem.Text == "995" || ddlistStudyCentre.SelectedItem.Text == "994" || ddlistStudyCentre.SelectedItem.Text == "993" || ddlistStudyCentre.SelectedItem.Text == "462")
            {
                tbStudentAmount.Text = ((FindInfo.findCourseFeeByBatch(Convert.ToInt32(ddlistCourses.SelectedItem.Value), Convert.ToInt32(ddlistSession.SelectedItem.Value))) / 2).ToString();
            }
            else
            {
                tbStudentAmount.Text = FindInfo.findCourseFeeByBatch(Convert.ToInt32(ddlistCourses.SelectedItem.Value), Convert.ToInt32(ddlistSession.SelectedItem.Value)).ToString();
            }
                            

            tbStudentAmount.Enabled = true;
            setAccountSession();
            btnSubmit.Visible = true;
        }

        protected void ddlistPreCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnUpd_Click(object sender, EventArgs e)
        {
            if (ddlistEligible.SelectedItem.Text != "--SELECT ONE--" && ddlistOriginalsVer.SelectedItem.Text != "--SELECT ONE--" && ddlistAdmissionStatus.SelectedItem.Text != "--SELECT ONE--" && ddlistState.SelectedItem.Text != "NF" && ddlistCity.SelectedItem.Text != "NF")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEPendingStudentRecord set AdmissionType=@AdmissionType,CYear=@CYear,Session=@Session,City=@City,District=@District,State=@State,Eligible=@Eligible,OriginalsVerified=@OriginalsVerified,AdmissionStatus=@AdmissionStatus,ReasonIfPending=@ReasonIfPending where PSRID='" + Request.QueryString["PSRID"] + "'", con);

                cmd.Parameters.AddWithValue("@AdmissionType", ddlistAdmissionType.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@CYear", ddlistCYear.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Session", ddlistSession.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@City", ddlistCity.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@District", ddlistCity.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@State", ddlistState.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Eligible", ddlistEligible.SelectedItem.Text);              
                cmd.Parameters.AddWithValue("@OriginalsVerified", ddlistOriginalsVer.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@AdmissionStatus", Convert.ToInt32(ddlistAdmissionStatus.SelectedItem.Value));

                if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                {
                    cmd.Parameters.AddWithValue("@ReasonIfPending", "NA");
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                {
                    cmd.Parameters.AddWithValue("@ReasonIfPending", tbReason.Text);
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                {
                    cmd.Parameters.AddWithValue("@ReasonIfPending", tbReason.Text);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Set Eligibility", "Set Eligibility Status '"+ddlistEligible.SelectedItem.Text+"' for Year'"+ddlistCYear.SelectedItem.Value+"' of a Student with OANo-'" + Request.QueryString["PSRID"] + "'", Convert.ToInt32(Session["ERID"].ToString()));

                lblMSG.Text = "Record has been updated successfully !! ";
                pnlData.Visible = false;
                pnlMSG.Visible = true;
                btnOK.Visible = false;
                                    
                                    
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry!! You did not select any of given entry.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }
        }

        protected void ddlistState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistCity.Items.Clear();          
            PopulateDDList.populateCity(ddlistCity, ddlistState.SelectedItem.Text);          
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            btnYes.Visible = false;
            string error = "";

            //if (ddlistPaymentMode.SelectedItem.Value == "5" || ddlistPaymentMode.SelectedItem.Value == "6")
            //{
            //    tbTotalAmount.Text = "0";
            //}

            string frdate = ddlistDOAYear.SelectedItem.Value + "-" + ddlistDOAMonth.SelectedItem.Value + "-" + ddlistDOADay.SelectedItem.Value;
            int count = 0;
            int iid = 0;

            if (Convert.ToInt32(ViewState["noi"]) == 2)
            {
                count = 2;
                iid = Convert.ToInt32(ddlistIns.SelectedItem.Value);
            }
            else if (Convert.ToInt32(ViewState["noi"]) == 1)
            {
                count = 1;
                if (rblEntryType.SelectedItem.Value == "1")
                {
                    iid = Convert.ToInt32(lblIID.Text);
                }
            }

            if (Accounts.validFee(0, Convert.ToInt32(ddlistCourses.SelectedItem.Value), 1, ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)), Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistCYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, 0, Convert.ToInt32(Session["ERID"]), ddlistSession.SelectedItem.Text, frdate, Convert.ToInt32(rblEntryType.SelectedItem.Value), count, iid, ddlistStudyCentre.SelectedItem.Text, out error))
            {
                string eno = "";
                string icno = "";
                int srid = 0;
                if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                {
                    registerStudent(out eno, out icno);

                    srid = FindInfo.findSRIDByENo(eno);
                    if (Request.QueryString["PSRID"] != null)
                    {
                        updateEnrollStatus(Convert.ToInt32(Request.QueryString["PSRID"]));
                        if (ddlistSession.SelectedItem.Text == "C 2015")
                        {
                            System.IO.File.Move(Server.MapPath("PendingStudentPhotos/" + Request.QueryString["PSRID"] + ".jpg"), Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"));
                            File.Copy(Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"), Server.MapPath("StudentPhotos/" + eno + ".jpg"));
                            File.Copy(Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"), Server.MapPath("OAPhotos/" + eno + ".jpg"));
                            File.Delete(Server.MapPath("PendingStudentPhotos/" + eno + ".jpg"));
                        }

                    }
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                {
                    registerStudent(out eno, out icno);
                    srid = FindInfo.findSRIDByANo(tbANo.Text);
                }
                else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                {
                    registerStudent(out eno, out icno);
                    srid = FindInfo.findSRIDByANo(tbANo.Text);

                }

                if (eno != "AE" && eno != "")
                {

                    Accounts.fillFee(srid, 1, ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)), Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(ddlistCYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value, frdate, Convert.ToInt32(rblEntryType.SelectedItem.Value));

                    if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                    {
                        Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + eno + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    }
                    else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                    {

                        Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Application No '" + tbANo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    }

                    if (rblTrans.SelectedItem.Text == "Yes")
                    {
                        updateSCChangeRecord(srid);
                    }
                    if (ddlistAdmissionType.SelectedItem.Value == "2" || ddlistAdmissionType.SelectedItem.Value == "3")
                    {
                        insertCTMRecord(srid, ddlistPInst.SelectedItem.Value, ddlistExamination.SelectedItem.Value);
                    }

                    if (ddlistAdmissionStatus.SelectedItem.Value == "1")
                    {

                        if (rblEntryType.SelectedItem.Value == "1" && (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ddlistCYear.SelectedItem.Value))))
                        {
                            int res = registerForSLM(srid, ddlistStudyCentre.SelectedItem.Text, Convert.ToInt32(ddlistCourses.SelectedItem.Value), Convert.ToInt32(ddlistCYear.SelectedItem.Value));
                            if (res == 1)
                            {
                                lblMSG.Text = "Student has been registered successfully !! with <br/> Enrollment No. : " + eno + "<br/> I Card No. : " + icno + "<br/><span style='color:Green'>Student is registered for issuing SLM. </span>";
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry!! Student is registered successfully with <br/> Enrollment No.: " + eno + "<br/> I Card No. : " + icno + "<br/><span style='color:Red'> but 'SLM Record is not entered. Please contact ERP Developer'</span>";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;

                            }
                        }
                        else
                        {
                            lblMSG.Text = "Student has been registered successfully !! with <br/> Enrollment No. : " + eno + "<br/> I Card No. : " + icno;
                        }
                    }
                    else if (ddlistAdmissionStatus.SelectedItem.Value == "2")
                    {

                        lblMSG.Text = "Student has been registered successfully !! with 'Pending' Status and with <br/> Application No. : " + tbANo.Text;
                    }
                    else if (ddlistAdmissionStatus.SelectedItem.Value == "3")
                    {

                        lblMSG.Text = "Student has been registered successfully !! with 'Provisional' Status and with <br/> Application No. : " + tbANo.Text;
                    }

                    pnlData.Visible = false;
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! This Alloted Enrollment No. is already exist.Please press submit button again.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = error;
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }

        
          
    }
}
