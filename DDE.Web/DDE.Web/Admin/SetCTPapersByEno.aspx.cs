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
    public partial class SetCTPapersByEno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                PopulateDDList.populateExam(ddlistExamination);
                ddlistExamination.Items.FindByValue("W10").Selected = true;
                if (Request.QueryString["SRID"] != null)
                {
                    polulateStudentInfo(Convert.ToInt32(Request.QueryString["SRID"]));
                    setPapers();
                    pnlSearch.Visible = false;
                    pnlStudentDetails.Visible = true;
                    btnUpdate.Visible = true;

                }
                else
                {
                    pnlSearch.Visible = true;
                    pnlStudentDetails.Visible = false;
                    btnUpdate.Visible = false;
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

        private void setPapers()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Paper1,Paper2 from DDECTPaperRecord where SRID='" + lblSRID.Text + "' and Exam='"+ddlistExamination.SelectedItem.Value+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ddlistPaper1.SelectedItem.Selected = false;
                ddlistPaper2.SelectedItem.Selected = false;

                ddlistPaper1.Items.FindByText(dr["Paper1"].ToString()).Selected = true;
                ddlistPaper2.Items.FindByText(dr["Paper2"].ToString()).Selected = true;
                
            }
            con.Close();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int srid = FindInfo.findSRIDByENo(tbENo.Text);
            if (validENo(srid))
            {

                polulateStudentInfo(srid);
                setPapers();
                pnlSearch.Visible = false;
                pnlStudentDetails.Visible = true;
                if (FindInfo.isCTPaperAlloted(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value))
                {
                    btnUpdate.Text = "Update Papers";
                }
                else
                {
                    btnUpdate.Text = "Set Papers";
                }
                btnUpdate.Visible = true;

            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! not a valid Enrollment No.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
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

        private bool validENo(int srid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select AdmissionType from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() == "2" || dr[0].ToString() == "3")
                {
                    exist = true;
                }

            }

            con.Close();

            return exist;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (btnUpdate.Text=="Update Papers")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDECTPaperRecord set Paper1=@Paper1,Paper2=@Paper2 where SRID='" + lblSRID.Text + "' and Exam='" + ddlistExamination.SelectedItem.Value + "'", con);

                cmd.Parameters.AddWithValue("@Paper1", ddlistPaper1.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Paper2", ddlistPaper2.SelectedItem.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Update CT Papers", "Updated CT Papers  '" + ddlistPaper1.SelectedItem.Value + "' and '" + ddlistPaper2.SelectedItem.Value + "' for " + ddlistExamination.SelectedItem.Text + " exam with Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
            }
            else if (btnUpdate.Text == "Set Papers")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDECTPaperRecord values(@SRID,@Paper1,@Paper2,@Exam)", con);

                cmd.Parameters.AddWithValue("@SRID", lblSRID.Text);
                cmd.Parameters.AddWithValue("@Paper1", ddlistPaper1.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Paper2", ddlistPaper2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Exam", ddlistExamination.SelectedItem.Value);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Set CT Papers", "Set CT Papers  '" + ddlistPaper1.SelectedItem.Value + "' and '" + ddlistPaper2.SelectedItem.Value + "' for " + ddlistExamination.SelectedItem.Text+ " exam with Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been inserted successfully";
                pnlMSG.Visible = true;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }
    }
}
