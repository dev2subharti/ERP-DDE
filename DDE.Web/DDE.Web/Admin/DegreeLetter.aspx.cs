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
    public partial class DegreeLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85))
            {
                if (!IsPostBack)
                {
                   
                    populateRecord();

                    if (Session["CF"].ToString() == "New")
                    {
                        lblRefNo.Visible = false;
                        lblDate.Visible = false;
                        btnPrint.Visible = true;
                    }
                    else if (Session["CF"].ToString() == "Old")
                    {
                        lblRefNo.Visible = true;
                        lblDate.Visible = true;
                        btnPrint.Visible = false;
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

        private void populateRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEDegreeInfo.PassingYear,DDEDegreeInfo.SNameH,DDEDegreeInfo.FNameH,DDEDegreeInfo.MNameH,DDEDegreeInfo.RollNo,DDEDegreeInfo.FinalDiv,DDEDegreeInfo.LetterPublishedOn,DDEDegreeInfo.DegreeType,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.MotherName,DDEStudentRecord.CYear,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEStudentRecord.AadhaarNo from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.DIID='" + Convert.ToInt32(Session["DIID"]) + "'", con);
            SqlDataReader dr;

          
            con.Open();

            dr = cmd.ExecuteReader();

            int i = 1;
            if (dr.HasRows)
            {
                dr.Read();

               
                lblDIID.Text =Convert.ToInt32(dr["DIID"]).ToString();
                if (Session["CF"].ToString() == "New")
                {
                    lblRefNo.Text = "Ref.No. DDE/SVSU/" + DateTime.Now.ToString("yyyy") + "/DL/" + dr["DIID"].ToString();
                    lblDate.Text = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");
                }
                else if (Session["CF"].ToString() == "Old")
                {
                    lblRefNo.Text = "Ref.No. DDE/SVSU/" + Convert.ToDateTime(dr["LetterPublishedOn"]).ToString("dd/MM/yyyy") + "/DL/" + dr["DIID"].ToString();
                    lblDate.Text = "Date : " + Convert.ToDateTime(dr["LetterPublishedOn"]).ToString("dd/MM/yyyy");
                }

                lblPassingYear.Text = "Passing Year : " + dr["PassingYear"].ToString();
                lblSNameE.Text = dr["StudentName"].ToString();
                lblSNameH.Text = dr["SNameH"].ToString();
                lblFNameE.Text = dr["FatherName"].ToString();
                lblFNameH.Text = dr["FNameH"].ToString();
                lblMNameE.Text = dr["MotherName"].ToString();
                lblMNameH.Text = dr["MNameH"].ToString();
               
                lblRollNo.Text = dr["RollNo"].ToString();
                lblENo.Text = dr["EnrollmentNo"].ToString();
                lblDiv.Text = dr["FinalDiv"].ToString();
                lblDegreeType.Text = dr["DegreeType"].ToString();
                lblAadhaarNo.Text = dr["AadhaarNo"].ToString();

                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                {

                    if (dr["CYear"].ToString() == "1")
                    {
                        if (dr["Course"].ToString() == "")
                        {
                            lblCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course"]));
                            string[] str = cfn.Split('(', ')');

                            lblCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                lblSpec.Text = str[1].ToString();

                            }
                            else
                            {
                                lblSpec.Text = "NA";
                            }
                        }
                    }
                    else if (dr["CYear"].ToString() == "2")
                    {
                        if (dr["Course2Year"].ToString() == "")
                        {
                            lblCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course2Year"]));
                            string[] str = cfn.Split('(', ')');

                            lblCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {

                                lblSpec.Text = str[1].ToString();

                            }
                            else
                            {
                                lblSpec.Text = "NA";
                            }
                        }

                    }
                    else if (dr["CYear"].ToString() == "3")
                    {
                        if (dr["Course3Year"].ToString() == "")
                        {
                            lblCourse.Text = "Not Set";
                        }
                        else
                        {
                            string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course3Year"]));
                            string[] str = cfn.Split('(', ')');

                            lblCourse.Text = str[0].ToString();
                            if (str.Length > 1)
                            {
                               
                                lblSpec.Text = str[1].ToString();
                              
                            }
                            else
                            {
                                lblSpec.Text = "NA";
                            }
                        }

                    }
                }
                else
                {
                    string cfn = FindInfo.findCourseFullNameByID(Convert.ToInt32(dr["Course"]));
                    string[] str = cfn.Split('(', ')');

                    lblCourse.Text = str[0].ToString();
                    if (str.Length > 1)
                    {

                        lblSpec.Text = str[1].ToString();

                    }
                    else
                    {
                        lblSpec.Text = "NA";
                    }
                }



                


            }



            con.Close();
        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (btnPrint.Visible == true)
            {
                int times;
                string lastprinted;
               
                    if (FindInfo.isDLPrintedByID(Convert.ToInt32(Session["DIID"]), out times, out lastprinted))
                    {
                        lblRefNo.Visible = true;
                        lblDate.Visible = true;
                        btnPrint.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                        //pnlData.Visible = false;
                        //lblMSG.Text = "This Letter has already printed '" + times.ToString() + "' times.<br/>Last printed on '" + lastprinted + "'<br/> Do you want to print this Letter again ?";
                        //btnYes.Visible = true;
                        //btnNo.Visible = true;
                        //pnlMSG.Visible = true;
                        //Session["printcounter"] = times;

                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEDegreeInfo set LetterPublished=@LetterPublished,LetterPublishedOn=@LetterPublishedOn where DIID='" + lblDIID.Text + "'", con);

                        cmd.Parameters.AddWithValue("@LetterPublished", "True");
                        cmd.Parameters.AddWithValue("@LetterPublishedOn", DateTime.Now.ToString());

                        con.Open();
                        object res= cmd.ExecuteNonQuery();
                        con.Close();

                        if (Convert.ToInt32(res) == 1)
                        {

                            Log.createLogNow("Degree Info", "Degree Letter printed with No : " + lblDIID.Text + " with times : 1", Convert.ToInt32(Session["ERID"].ToString()));

                            btnPrint.Visible = false;


                            //lblPNo.Text = "P-1";
                            //lblPNo.Visible = true;
                            lblRefNo.Visible = true;
                            lblDate.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                        }
                        else
                        {

                        }
                   
                    }

               
               
            }


        }

      


        protected void btnNo_Click(object sender, EventArgs e)
        {
            //pnlData.Visible = true;
            //pnlMSG.Visible = false;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            //pnlData.Visible = true;
            //pnlMSG.Visible = false;
            //btnPrint.Visible = false;
            //lblRefNo.Visible = true;
            //lblDate.Visible = true;



            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlCommand cmd = new SqlCommand("update DDEDegreeInfo set LetterPublished=@LetterPublished,LetterPublishedOn=@LetterPublishedOn where DIID='" + lblDIID.Text + "')", con);

            //cmd.Parameters.AddWithValue("@LetterPublished", "True");
            //cmd.Parameters.AddWithValue("@LetterPublishedOn", DateTime.Now.ToString());

            //con.Open();
            //object res = cmd.ExecuteNonQuery();
            //con.Close();



            //if (Convert.ToInt32(res) == 1)
            //{

            //    Log.createLogNow("Verification Letter Printed", "Instrument Verification Letter printed with No : " + lblLNo.Text + "with times : " + (Convert.ToInt32(Session["printcounter"]) + 1).ToString(), Convert.ToInt32(Session["ERID"].ToString()));


            //    lblPNo.Text = "P-" + (Convert.ToInt32(Session["printcounter"]) + 1).ToString();
            //    lblPNo.Visible = true;
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
            //}

           



        }

        
    }
}