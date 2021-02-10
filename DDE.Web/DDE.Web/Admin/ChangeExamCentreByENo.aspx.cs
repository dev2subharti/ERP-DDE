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
using System.Data.SqlClient;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class ChangeExamCentreByENo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);

                    if (Request.QueryString["SRID"] != null)
                    {
                        polulateStudentInfo(Convert.ToInt32(Request.QueryString["SRID"]));
                        ddlistExam.SelectedItem.Selected = false;
                        ddlistExam.Items.FindByValue(Request.QueryString["ExamCode"]).Selected = true;
                        ddlistExam.Enabled = false;
                        populateandSetExamCentre(Request.QueryString["ExamCode"]);
                        pnlSearch.Visible = false;
                        pnlStudentDetails.Visible = true;
                        btnUpdate.Visible = true;

                    }
                    else
                    {
                        ddlistExam.Items.FindByValue("Z11").Selected = true;
                        pnlSearch.Visible = true;
                        pnlStudentDetails.Visible = false;
                        btnUpdate.Visible = false;
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int srid = FindInfo.findSRIDByENo(tbENo.Text);

           
            string ayear = FindInfo.findAllExamYear(srid, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
            if (ayear != "" && ayear != "0")
            {
                polulateStudentInfo(srid);
                populateandSetExamCentre(ddlistExam.SelectedItem.Value);
                pnlSearch.Visible = false;
                pnlStudentDetails.Visible = true;
                btnUpdate.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Course or Examination or both Fee are not paid for "+ddlistExam.SelectedItem.Text+" Examination";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
            
        }

        private void populateandSetExamCentre(string examcode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ECID,City,ExamCentreCode,CentreName from DDEExaminationCentres_" + examcode + " order by ExamCentreCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistCEC.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString());
                ddlistCEC.Items.FindByText("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString()).Value = dr[0].ToString();

                ddlistNEC.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString());
                ddlistNEC.Items.FindByText("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString()).Value = dr[0].ToString();

            }
            con.Close();

            

            ddlistCEC.Items.Add("NOT FOUND");
            ddlistCEC.Items.FindByText("NOT FOUND").Value = "0";
            ddlistCEC.Items.FindByValue(findExamCentre(Convert.ToInt32(lblSRID.Text))).Selected = true;
            ddlistCEC.Enabled = false;
        }

        private string findExamCentre(int srid)
        {
            string ec = "0";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCentreCode from DDEExamRecord_"+ddlistExam.SelectedItem.Value+" where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if(dr.HasRows) 
            {
                dr.Read();
                ec = dr[0].ToString();

            }
            con.Close();

            return ec;
        }

        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,StudyCentreCode,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "'", con);
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
                        }

                    }
                }
                else
                {
                    tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                }

            }

            con.Close();



        }   

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           
            SqlCommand cmd1 = new SqlCommand("update DDEExamRecord_"+ddlistExam.SelectedItem.Value+" set ExamCentreCode=@ExamCentreCode where SRID='" + lblSRID.Text + "'", con);

          
            cmd1.Parameters.AddWithValue("@ExamCentreCode", ddlistNEC.SelectedItem.Value);
            
          
            con.Open();
           
            cmd1.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Update Exam Centre", "Updated Exam Centre for "+ddlistExam.SelectedItem.Text+" exam from '"+ddlistCEC.SelectedItem.Value+"' to '"+ddlistNEC.SelectedItem.Value+"' with Enrollment No '" + tbEnNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

            pnlData.Visible = false;
            lblMSG.Text = "Record has been updated successfully";
            pnlMSG.Visible = true;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }
    }
}
