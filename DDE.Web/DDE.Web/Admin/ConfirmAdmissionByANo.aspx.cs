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
    public partial class ConfirmAdmissionByANo : System.Web.UI.Page
    {
        int counter = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (Request.QueryString["SRID"] != null)
                {
                    tbANo.Text = FindInfo.findANoBySRID(Convert.ToInt32(Request.QueryString["SRID"])) ;
                    polulateStudentInfo(Convert.ToInt32(Request.QueryString["SRID"]));
                    pnlSearch.Visible = true;
                    tbANo.Enabled = false;
                    btnSearch.Visible = false;
                    pnlStudentDetails.Visible = true;
                    btnConfirm.Visible = true;
                }
                else
                {
                                     
                    tbANo.Enabled = true;
                    btnSearch.Visible = true;
                    pnlSearch.Visible = true;
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int srid = FindInfo.findSRIDByANo(tbANo.Text);
            if (validANo(srid))
            {              
                polulateStudentInfo(srid);
                pnlSearch.Visible = false;
                pnlStudentDetails.Visible = true;
                btnConfirm.Visible = true;
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! not a valid Application No.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private bool validANo(int srid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select ApplicationNo,EnrollmentNo,AdmissionStatus from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if(dr["EnrollmentNo"].ToString()=="" && dr["AdmissionStatus"].ToString()=="2")
                {
                   exist = true;
                }


            }

            con.Close();

            return exist;
        }

     

        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select StudentName,FatherName,StudyCentreCode,Session,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();

                lblSRID.Text = srid.ToString();
                tbBatch.Text = dr["Session"].ToString();
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();
                tbSCCode.Text = FindInfo.findSCCodeForMarkSheetBySRID(srid); 

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
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                            lblCourseID.Text = dr["Course"].ToString();
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
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course2Year"]));
                            lblCourseID.Text = dr["Course2Year"].ToString();
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
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course3Year"]));
                            lblCourseID.Text = dr["Course3Year"].ToString();
                        }

                    }
                }
                else
                {
                    tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                    lblCourseID.Text = dr["Course"].ToString();
                }

            }

            con.Close();



        }

        

        

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //string eno=allotEnrollmentNo();
            //string icno = eno.Substring(0, 3) +"-"+tbSCCode.Text+ eno.Substring(6, 5);

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlCommand cmd = new SqlCommand("update DDEStudentRecord set EnrollmentNo=@EnrollmentNo,ICardNo=@ICardNo,Eligible=@Eligible,OriginalsVerified=@OriginalsVerified,RecordStatus=@RecordStatus,AdmissionStatus=@AdmissionStatus where SRID='" + lblSRID.Text + "'", con);

            //cmd.Parameters.AddWithValue("@EnrollmentNo",eno );
            //cmd.Parameters.AddWithValue("@ICardNo", icno);
            //cmd.Parameters.AddWithValue("@Eligible", "YES");
            //cmd.Parameters.AddWithValue("@OriginalsVerified", "YES");
            //cmd.Parameters.AddWithValue("@RecordStatus", 1);
            //cmd.Parameters.AddWithValue("@AdmissionStatus", 1);

            //cmd.Connection = con;
            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();

            //Log.createLogNow("Confirmed Admission", "Confirmed Admission with Enrollment No '" + eno + "'", Convert.ToInt32(Session["ERID"].ToString()));

            //FindInfo.updateEnrollmentCounter(tbBatch.Text, counter);

            //pnlData.Visible = false;
            //lblMSG.Text = "Admission has been confirmed successfully with Enrollment No '" + eno + "' <br/> I Card No. : "+icno;
            //pnlMSG.Visible = true;
        }

        private string  allotEnrollmentNo()
        {
            int pcode = FindInfo.findProgrammeCode(lblCourseID.Text);
            counter = FindInfo.findCounter(tbBatch.Text);

            string finalcounter = string.Format("{0:00000}", counter);

            return tbBatch.Text.Substring(0, 1) + tbBatch.Text.Substring(4, 2) + "20" + pcode.ToString() + finalcounter;
        }
    }
}
