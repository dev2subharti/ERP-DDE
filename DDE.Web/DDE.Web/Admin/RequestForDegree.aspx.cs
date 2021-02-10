using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class RequestForDegree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 113) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 123))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    if (Request.QueryString["DIID"] != null)
                    {
                        populateExistRecord(Convert.ToInt32(Request.QueryString["DIID"]));
                        pnlSearch.Visible = true;
                        btnSearch.Visible = false;
                        pnlStudentDetails.Visible = true;

                        if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 113))
                        {
                            btnSubmit.Text = "Update";
                            btnSubmit.Visible = true;
                        }
                        else 
                        {
                            btnSubmit.Text = "";
                            btnSubmit.Visible = false;
                        }

                        pnlData.Visible = true;
                        pnlMSG.Visible = false;
                    }
                    else
                    {
                        if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85))
                        {
                            pnlSearch.Visible = true;
                            pnlStudentDetails.Visible = false;
                            btnSubmit.Visible = false;
                            btnSubmit.Text = "Submit";
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
                   
                                 
                   
                }
               
                
            }          
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private void populateExistRecord(int diid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.MotherName,DDEStudentRecord.CYear,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEStudentRecord.AadhaarNo,DDEStudentRecord.Gender,DDEDegreeInfo.SNameH,DDEDegreeInfo.FNameH,DDEDegreeInfo.MNameH,DDEDegreeInfo.PassingYear,DDEDegreeInfo.FinalDiv,DDEDegreeInfo.RollNo,DDEDegreeInfo.MailingAddress,DDEDegreeInfo.PinCode,DDEDegreeInfo.MobileNo from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.DIID='" + diid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + dr["SRID"].ToString();
                tbENo.Text = tbEnNo.Text = dr["EnrollmentNo"].ToString();
                ddlistExam.Items.FindByText(dr["PassingYear"].ToString()).Selected = true;
                lblSRID.Text = dr["SRID"].ToString();
                tbSNameE.Text = dr["StudentName"].ToString();
                tbSNameH.Text = dr["SNameH"].ToString();
                tbFNameE.Text = dr["FatherName"].ToString();
                tbFNameH.Text = dr["FNameH"].ToString();
                tbMNameE.Text = dr["MotherName"].ToString();
                tbMNameH.Text = dr["MNameH"].ToString();
                tbRollNo.Text = dr["RollNo"].ToString();
               
                tbRollNo.Enabled = true;
                ddlistGender.Items.FindByText(dr["Gender"].ToString()).Selected = true;
                if (dr["FinalDiv"].ToString()!="")
                {
                    ddlistDiv.Items.FindByText(dr["FinalDiv"].ToString()).Selected = true;
                }
                else
                {
                    ddlistDiv.Items.FindByText("NOT FOUND").Selected = true;
                }
               
                tbAddress.Text = dr["MailingAddress"].ToString();
                tbPin.Text = dr["PinCode"].ToString();
                tbMNo.Text = dr["MobileNo"].ToString();
                tbADNo.Text = dr["AadhaarNo"].ToString();

                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                {

                    if (dr["CYear"].ToString() == "1")
                    {
                        if (dr["Course"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course"]));
                            string[] str = cfn.Split('(', ')');

                            tbCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                tbSpecialization.Text = str[1].ToString();

                            }
                            else
                            {
                                tbSpecialization.Text = "NA";
                            }
                        }
                    }
                    else if (dr["CYear"].ToString() == "2")
                    {
                        if (dr["Course2Year"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course2Year"]));
                            string[] str = cfn.Split('(', ')');

                            tbCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                tbSpecialization.Text = str[1].ToString();

                            }
                            else
                            {
                                tbSpecialization.Text = "NA";
                            }
                        }

                    }
                    else if (dr["CYear"].ToString() == "3")
                    {
                        if (dr["Course"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course3Year"]));
                            string[] str = cfn.Split('(', ')');

                            tbCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                tbSpecialization.Text = str[1].ToString();

                            }
                            else
                            {
                                tbSpecialization.Text = "NA";
                            }
                        }

                    }
                }
                else
                {
                    string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course"]));
                    string[] str = cfn.Split('(', ')');

                    tbCourse.Text = str[0].ToString();
                    if (str.Length > 1)
                    {

                        tbSpecialization.Text = str[1].ToString();

                    }
                    else
                    {
                        tbSpecialization.Text = "NA";
                    }
                }

            }

            con.Close();

         
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int srid = FindInfo.findSRIDByENo(tbENo.Text);
            if (Accounts.singlefeePaid(srid, 29)|| Accounts.singlefeePaid(srid, 45) || Accounts.singlefeePaid(srid, 46))
            {
                polulateStudentInfo(srid);
                           
                if (FindInfo.isDegreeInfoExist(srid))
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select * from DDEDegreeInfo where SRID='" + srid + "'", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();

                    dr.Read();
                    tbSNameH.Text = dr["SNameH"].ToString();
                    tbSNameH.Enabled = false;
                    tbFNameH.Text = dr["FNameH"].ToString();
                    tbFNameH.Enabled = false;
                    tbMNameH.Text = dr["MNameH"].ToString();
                    tbMNameH.Enabled = false;
                    tbRollNo.Text = dr["RollNo"].ToString();
                    tbRollNo.Enabled = false;
                  
                    try
                    {
                        ddlistDiv.Items.FindByText(dr["FinalDiv"].ToString()).Selected = true;
                    }
                    catch
                    {
                        ddlistDiv.SelectedItem.Selected = false;
                        ddlistDiv.Items.Add("NA");
                        ddlistDiv.Items.FindByText("NA").Selected = true;
                    }

                    try
                    {
                        ddlistDegreeType.Items.FindByText(dr["DegreeType"].ToString()).Selected = true;
                    }
                    catch
                    {
                        ddlistDegreeType.SelectedItem.Selected = false;
                        ddlistDegreeType.Items.Add("NA");
                        ddlistDegreeType.Items.FindByText("NA").Selected = true;
                    }

                    ddlistExam.Enabled = false;
                    con.Close();

                    btnSubmit.Visible = false;

                    
                }
                else
                {
                    btnSubmit.Visible = true;
                }

                pnlStudentDetails.Visible = true;
               
            }
            else
            {
                pnlData.Visible = false;
               
                lblMSG.Text = "Sorry !! Degree fee is not paid by the Student";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }



        }

        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,MotherName,StudyCentreCode,CYear,Course,Course2Year,Course3Year,CAddress,PinCode,MobileNo,AadhaarNo from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                 imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                tbEnNo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = srid.ToString();
                tbSNameE.Text = dr["StudentName"].ToString();
                tbFNameE.Text = dr["FatherName"].ToString();
                tbMNameE.Text = dr["MotherName"].ToString();
                tbRollNo.Text = FindInfo.findRollNoBySRID(srid,ddlistExam.SelectedItem.Value,"R");
                if (tbRollNo.Text == "")
                {
                    tbRollNo.Enabled = true;
                }

                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                {

                    if (dr["CYear"].ToString() == "1")
                    {
                        if (dr["Course"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn=FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course"]));
                            string[] str = cfn.Split('(',')');

                            tbCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                tbSpecialization.Text = str[1].ToString();

                            }
                            else
                            {
                                tbSpecialization.Text = "NA";
                            }
                        }
                    }
                    else if (dr["CYear"].ToString() == "2")
                    {
                        if (dr["Course2Year"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course2Year"]));
                            string[] str = cfn.Split('(', ')');

                            tbCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                tbSpecialization.Text = str[1].ToString();

                            }
                            else
                            {
                                tbSpecialization.Text = "NA";
                            }
                        }

                    }
                    else if (dr["CYear"].ToString() == "3")
                    {
                        if (dr["Course"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course3Year"]));
                            string[] str = cfn.Split('(', ')');

                            tbCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                tbSpecialization.Text = str[1].ToString();

                            }
                            else
                            {
                                tbSpecialization.Text = "NA";
                            }
                        }

                    }
                }
                else
                {
                    string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course"]));
                    string[] str = cfn.Split('(', ')');

                    tbCourse.Text = str[0].ToString();
                    if (str.Length > 1)
                    {
                        
                        tbSpecialization.Text = str[1].ToString();
                      
                    }
                    else
                    {
                        tbSpecialization.Text = "NA";
                    }
                }

                tbAddress.Text = dr["CAddress"].ToString();
                tbPin.Text = dr["PinCode"].ToString();
                tbMNo.Text = dr["MobileNo"].ToString();
                tbADNo.Text = dr["AadhaarNo"].ToString();

            }

            con.Close();



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Submit")
            {
                if (!FindInfo.isDegreeInfoExist(Convert.ToInt32(lblSRID.Text)))
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEDegreeInfo values(@SRID,@PassingYear,@SNameH,@FNameH,@MNameH,@RollNo,@FinalDiv,@EntryTime,@LetterPublished,@LetterPublishedOn,@CLPublished,@CLPublishedOn,@CLNo,@PRStatus,@PRDoneBY,@PRDoneOn,@NDStatus,@NDDoneBY,@NDDoneOn,@DPStatus,@DPDoneBy,@DPDoneOn,@NOTPrinted,@DegreeReceived,@SNo,@ReceivedOn,@DegreePosted,@PostedOn,@PostingMode,@DocketNo,@MailingAddress,@PinCode,@MNo,@DegreeType)", con);

                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
                    cmd.Parameters.AddWithValue("@PassingYear", ddlistExam.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SNameH", tbSNameH.Text);
                    cmd.Parameters.AddWithValue("@FNameH", tbFNameH.Text);
                    cmd.Parameters.AddWithValue("@MNameH", tbMNameH.Text);
                    cmd.Parameters.AddWithValue("@RollNo", tbRollNo.Text);
                    cmd.Parameters.AddWithValue("@FinalDiv", ddlistDiv.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@EntryTime", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@LetterPublished", "False");
                    cmd.Parameters.AddWithValue("@LetterPublishedOn", "");
                    cmd.Parameters.AddWithValue("@CLPublished", "False");
                    cmd.Parameters.AddWithValue("@CLPublishedOn", "");
                    cmd.Parameters.AddWithValue("@CLNo", 0);

                    cmd.Parameters.AddWithValue("@PRStatus", "False");
                    cmd.Parameters.AddWithValue("@PRDoneBy", 0);
                    cmd.Parameters.AddWithValue("@PRDoneOn", "");

                    cmd.Parameters.AddWithValue("@NDStatus", "False");
                    cmd.Parameters.AddWithValue("@NDDoneBy", 0);
                    cmd.Parameters.AddWithValue("@NDDoneOn", "");

                    cmd.Parameters.AddWithValue("@DPStatus", "False");
                    cmd.Parameters.AddWithValue("@DPDoneBy", 0);
                    cmd.Parameters.AddWithValue("@DPDoneOn", ""); 
                    cmd.Parameters.AddWithValue("@NOTPrinted", 0);

                    cmd.Parameters.AddWithValue("@DegreeReceived", "False");
                    cmd.Parameters.AddWithValue("@SNo", 0);
                    cmd.Parameters.AddWithValue("@ReceivedOn", "");
                    cmd.Parameters.AddWithValue("@DegreePosted", "False");
                    cmd.Parameters.AddWithValue("@PostedOn", "");
                    cmd.Parameters.AddWithValue("@PostingMode", "");
                    cmd.Parameters.AddWithValue("@DocketNo", "");
                    cmd.Parameters.AddWithValue("@MailingAddress", tbAddress.Text);
                    cmd.Parameters.AddWithValue("@PinCode", tbPin.Text);
                    cmd.Parameters.AddWithValue("@MNo", tbMNo.Text);
                    cmd.Parameters.AddWithValue("@DegreeType", ddlistDegreeType.SelectedItem.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Degree Info", "Requested For Degree for Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                    pnlData.Visible = false;
                    lblMSG.Text = "Record has been inserted successfully";
                    pnlMSG.Visible = true;

                }
                else
                {
                    pnlData.Visible = false;

                    lblMSG.Text = "Sorry !! Record is already exist for this student.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEDegreeInfo set PassingYear=@PassingYear,SNameH=@SNameH,FNameH=@FNameH,MNameH=@MNameH,RollNo=@RollNo,FinalDiv=@FinalDiv,MailingAddress=@MailingAddress,PinCode=@PinCode,MobileNo=@MobileNo,DegreeType=@DegreeType where DIID='" + Request.QueryString["DIID"] + "'", con);

                cmd.Parameters.AddWithValue("@PassingYear", ddlistExam.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@SNameH", tbSNameH.Text);
                cmd.Parameters.AddWithValue("@FNameH", tbFNameH.Text);
                cmd.Parameters.AddWithValue("@MNameH", tbMNameH.Text);
                cmd.Parameters.AddWithValue("@RollNo", tbRollNo.Text);
                cmd.Parameters.AddWithValue("@FinalDiv", ddlistDiv.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@MailingAddress", tbAddress.Text);
                cmd.Parameters.AddWithValue("@PinCode", tbPin.Text);
                cmd.Parameters.AddWithValue("@MobileNo", tbMNo.Text);
                cmd.Parameters.AddWithValue("@DegreeType", ddlistDegreeType.SelectedItem.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Degree Info", "Updated Degree Record for Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
            }
           
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbRollNo.Text = FindInfo.findRollNoBySRID(FindInfo.findSRIDByENo(tbENo.Text), ddlistExam.SelectedItem.Value, "R");
        }
    }
}