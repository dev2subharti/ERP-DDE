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
    public partial class DegreePR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 114))
            {
                if (!IsPostBack)
                {
                  
                    if (Request.QueryString["DIID"] != null)
                    {
                        populateExistRecord(Convert.ToInt32(Request.QueryString["DIID"]));                    
                        pnlStudentDetails.Visible = true;
                        btnSubmit.Visible = true;
                        btnSubmit.Text = "Confirm PR";
                       
                    }
                   
                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 115))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["DIID"] != null)
                    {
                        populateExistRecord(Convert.ToInt32(Request.QueryString["DIID"]));
                        pnlStudentDetails.Visible = true;
                        btnSubmit.Visible = true;
                        btnSubmit.Text = "Make PR Pending";
                    }

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

        private void populateExistRecord(int diid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.MotherName,DDEStudentRecord.CYear,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEStudentRecord.Gender,DDEStudentRecord.AadhaarNo,DDEDegreeInfo.SNameH,DDEDegreeInfo.FNameH,DDEDegreeInfo.MNameH,DDEDegreeInfo.PassingYear,DDEDegreeInfo.FinalDiv,DDEDegreeInfo.RollNo,DDEDegreeInfo.MailingAddress,DDEDegreeInfo.PinCode,DDEDegreeInfo.MobileNo from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.DIID='" + diid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + dr["SRID"].ToString();
                tbENo.Text = dr["EnrollmentNo"].ToString();
                tbPassingYear.Text= dr["PassingYear"].ToString();
                lblSRID.Text = dr["SRID"].ToString();
                tbSNameE.Text = dr["StudentName"].ToString();
                tbSNameH.Text = dr["SNameH"].ToString();
                tbFNameE.Text = dr["FatherName"].ToString();
                tbFNameH.Text = dr["FNameH"].ToString();
                tbMNameE.Text = dr["MotherName"].ToString();
                tbMNameH.Text = dr["MNameH"].ToString();

                ddlistGender.Items.FindByText(dr["Gender"].ToString()).Selected = true;

                tbRollNo.Text = dr["RollNo"].ToString();

             
                if (dr["FinalDiv"].ToString() != "")
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Confirm PR")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEDegreeInfo set PRStatus=@PRStatus,PRDoneBy=@PRDoneBy,PRDoneOn=@PRDoneOn where DIID='" + Request.QueryString["DIID"] + "'", con);

                cmd.Parameters.AddWithValue("@PRStatus", "True");
                cmd.Parameters.AddWithValue("@PRDoneBy", Convert.ToInt32(Session["ERID"]));
                cmd.Parameters.AddWithValue("@PRDoneOn", DateTime.Now.ToString());
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    Log.createLogNow("Proof Reading Confirmed for Degree", "Proof Reading Confirmed for Degree for Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                    pnlData.Visible = false;
                    lblMSG.Text = "Record has been updated successfully";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Record could not be updated. Please try again";
                    pnlMSG.Visible = true;
                }
            }
            else if (btnSubmit.Text == "Make PR Pending")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEDegreeInfo set PRStatus=@PRStatus,PRDoneBy=@PRDoneBy,PRDoneOn=@PRDoneOn where DIID='" + Request.QueryString["DIID"] + "'", con);

                cmd.Parameters.AddWithValue("@PRStatus", "False");
                cmd.Parameters.AddWithValue("@PRDoneBy", 0);
                cmd.Parameters.AddWithValue("@PRDoneOn", "");
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    Log.createLogNow("Proof Reading Pending for Degree", "Proof Reading Pending for Degree for Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                    pnlData.Visible = false;
                    lblMSG.Text = "Record has been updated successfully";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Record could not be updated. Please try again";
                    pnlMSG.Visible = true;
                }
            }


        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
           
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbRollNo.Text = FindInfo.findRollNoBySRID(FindInfo.findSRIDByENo(tbENo.Text), lblExamCode.Text, "R");
        }
    }
}