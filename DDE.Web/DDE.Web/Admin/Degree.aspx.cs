using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace DDE.Web.Admin
{
    public partial class Degree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 118) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 119))
            {
                if(!IsPostBack)
                {
                    if (Request.QueryString["DIID"] != null)
                    {
                        string date;
                        int not;
                        int srid = FindInfo.findSRIDByDIID(Convert.ToInt32(Request.QueryString["DIID"]));
                        lblSRID.Text = srid.ToString();
                        if (!FindInfo.isDegreePrinted(srid, out date,out not))
                        {                         
                            populateDegreeDetails();
                            pnlData.Visible = true;
                            pnlMSG.Visible = false;
                        }
                        else
                        {
                            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 119))
                            {
                                populateDegreeDetails();
                                pnlData.Visible = true;
                                pnlMSG.Visible = false;
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! Final Degree for this student is already printed '" + not.ToString() + "' times.<br/> Last Printed on : " + date;
                                pnlMSG.Visible = true;
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

        private void populateDegreeDetails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.Session,DDEDegreeInfo.SNameH,DDEDegreeInfo.FNameH,DDEStudentRecord.Gender,DDEDegreeInfo.PassingYear,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDECourse.CourseFullNameDegree,DDECourse.CourseFullNameHindi,DDEDegreeInfo.FinalDiv,DDECourse.SpecializationDegree,DDECourse.SpecializationHindi from DDEDegreeInfo inner join DDEStudentRecord on DDEDegreeInfo.SRID=DDEStudentRecord.SRID inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEDegreeInfo.DIID='" + Convert.ToInt32(Request.QueryString["DIID"]) + "' and DDEStudentRecord.StudentPhoto is not null and DDEStudentRecord.RecordStatus='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                imgStudent.ImageUrl= "StudentImgHandler.ashx?SRID=" + lblSRID.Text;
                lblENo.Text = dr["EnrollmentNo"].ToString();
                lblBatchID.Text = FindInfo.findBatchID(dr["Session"].ToString()).ToString();

                if (dr["Gender"].ToString()=="MALE")
                {
                    lblStudentNameEnglish.Text = dr["StudentName"].ToString()+" S/O "+ dr["FatherName"].ToString();
                }
                else if (dr["Gender"].ToString() == "FEMALE")
                {
                    lblStudentNameEnglish.Text = dr["StudentName"].ToString() + " D/O " + dr["FatherName"].ToString();
                }
               
                lblExamNameEnglish.Text = dr["PassingYear"].ToString();

                string cne;
                string cnh;
                string spe;
                string sph;

                if (FindInfo.isMBACourse(Convert.ToInt32(dr["Course"])))
                {
                  
                    FindInfo.findCourseDetailsForDegreeByCourseID(Convert.ToInt32(dr["Course2Year"]), out cne, out cnh, out spe, out sph);

                    lblCourseNameEnglish.Text = cne;
                    lblSpecEnglish.Text = spe;
                    lblCourseNameHindi.Text = cnh;
                    lblSpecHindi.Text = sph;
                }
                else
                {
                    FindInfo.findCourseDetailsForDegreeByCourseID(Convert.ToInt32(dr["Course"]), out cne, out cnh, out spe, out sph);

                    lblCourseNameEnglish.Text = cne;
                    lblSpecEnglish.Text = spe;
                    lblCourseNameHindi.Text = cnh;
                    lblSpecHindi.Text = sph;
                }
              
                lblDE.Text = dr["FinalDiv"].ToString();
                

                if (dr["Gender"].ToString() == "MALE")
                {
                    lblStudentNameHindi.Text = dr["SNameH"].ToString() + " पुत्र " + dr["FNameH"].ToString();
                }
                else if (dr["Gender"].ToString() == "FEMALE")
                {
                    lblStudentNameHindi.Text = dr["SNameH"].ToString() + " पुत्री " + dr["FNameH"].ToString();
                }

                lblExamHindi.Text =FindInfo.findExamNameHindi(dr["PassingYear"].ToString());
               
                lblDH.Text =FindInfo.findDivHindi(dr["FinalDiv"].ToString());
              
                lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

                

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }


            con.Close();
         
        }

        private void setBarCode()
        {
            string code = "For verification and more details please follow the link : www.subhartidde.com/Degree.aspx?EN=" + HttpUtility.UrlEncode(lblENo.Text);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 80;
            imgBarCode.Width = 80;
            using (Bitmap bitMap = qrCode.GetGraphic(15))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                pnlBC.Controls.Add(imgBarCode);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string sno;
            if(FindInfo.isDegreePrinted(Convert.ToInt32(lblSRID.Text), out sno))
            {
                              
                lblSNo.Text = sno;
                if (sno != "")
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDEDegreeInfo set DPStatus=@DPStatus,DPDoneBy=@DPDoneBy,DPDoneOn=@DPDoneOn where DIID='" + Request.QueryString["DIID"] + "'", con);

                    cmd.Parameters.AddWithValue("@DPStatus", "True");
                    cmd.Parameters.AddWithValue("@DPDoneBy", Convert.ToInt32(Session["ERID"]));
                    cmd.Parameters.AddWithValue("@DPDoneOn", DateTime.Now.ToString());
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        Log.createLogNow("Degree Re-Printed", "Degree Re-Printed for Enrollment No '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                        setBarCode();
                        btnPrint.Visible = false;
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Some error occured";
                        pnlMSG.Visible = true;
                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Previous SNo not found";
                    pnlMSG.Visible = true;
                }

            }
            else
            {
                string counter = FindInfo.findNextDegreeSNo(Convert.ToInt32(lblBatchID.Text)).ToString();
                if (Convert.ToInt32(counter) > 0)
                {
                    lblSNo.Text = lblENo.Text.Substring(0, 3) + counter;
                  
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDEDegreeInfo set SNo=@SNo,DPStatus=@DPStatus,DPDoneBy=@DPDoneBy,DPDoneOn=@DPDoneOn where DIID='" + Request.QueryString["DIID"] + "'", con);

                    cmd.Parameters.AddWithValue("@SNo", lblSNo.Text);
                    cmd.Parameters.AddWithValue("@DPStatus", "True");
                    cmd.Parameters.AddWithValue("@DPDoneBy", Convert.ToInt32(Session["ERID"]));
                    cmd.Parameters.AddWithValue("@DPDoneOn", DateTime.Now.ToString());
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        Log.createLogNow("Degree Printed", "Degree Printed for Enrollment No '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                        int j = FindInfo.updateDegreeCounter(Convert.ToInt32(lblBatchID.Text), Convert.ToInt32(lblSNo.Text.Substring(3,6)));
                        if (j > 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                            setBarCode();
                            btnPrint.Visible = false;
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! Degree Counetr could not be updated. Please contact ERP developer immidiately.";
                            pnlMSG.Visible = true;
                        }

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Some error occured";
                        pnlMSG.Visible = true;
                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Some error occured";
                    pnlMSG.Visible = true;
                }
            }

            

        }
    }
}