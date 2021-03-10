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
    public partial class FillDegreeInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85))
            {
                pnlSearch.Visible = true;
                pnlStudentDetails.Visible = false;
                btnSubmit.Visible = false;

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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEDegreeInfo where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                polulateStudentInfo(srid);
                setTodayDate();
                dr.Read();
                if (dr["DegreeReceived"].ToString() == "False")
                {
                    ddlistRDay.Enabled = true;
                    ddlistRMonth.Enabled = true;
                    ddlistRYear.Enabled = true;
                    tbSNo.Enabled = true;

                    pnlSearch.Visible = false;
                    pnlStudentDetails.Visible = true;
                    btnSubmit.Visible = true;
                }
                else if (dr["DegreeReceived"].ToString() == "True")
                {
                    ddlistRDay.SelectedItem.Selected = false;
                    ddlistRMonth.SelectedItem.Selected = false;
                    ddlistRYear.SelectedItem.Selected = false;

                    string date = Convert.ToDateTime(dr["ReceivedOn"]).ToString("yyyy-MM-dd");
                    ddlistRDay.Items.FindByText(date.Substring(8, 2)).Selected = true;
                    ddlistRMonth.Items.FindByValue(date.Substring(5, 2)).Selected = true;
                    ddlistRYear.Items.FindByText(date.Substring(0, 4)).Selected = true;

                    ddlistRDay.Enabled = false;
                    ddlistRMonth.Enabled = false;
                    ddlistRYear.Enabled = false;

                    tbSNo.Text = dr["SNo"].ToString();
                    tbSNo.Enabled = false;

                    pnlSearch.Visible = false;
                    pnlStudentDetails.Visible = true;
                    btnSubmit.Visible = false;

                    if (srid.ToString().Trim().Length > 0)
                        Session["Srid"] = srid.ToString();
                }

            }
            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Degree request record found of the Student.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }

            con.Close();


        }

        private void setTodayDate()
        {
            ddlistRDay.SelectedItem.Selected = false;
            ddlistRMonth.SelectedItem.Selected = false;
            ddlistRYear.SelectedItem.Selected = false;
            ddlistRDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistRMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistRYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
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



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string rdate = ddlistRYear.SelectedItem.Value + "-" + ddlistRMonth.SelectedItem.Value + "-" + ddlistRDay.SelectedItem.Value;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            String sqlRecDegree = "update DDEDegreeInfo set DegreeReceived=@DegreeReceived,SNo=@SNo,ReceivedOn=@ReceivedOn,prstatus=1,ndstatus=1,dpstatus=1,notprinted=1 ";
            sqlRecDegree+=" where SRID ='" + lblSRID.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlRecDegree, con);

            cmd.Parameters.AddWithValue("@DegreeReceived", "True");
            cmd.Parameters.AddWithValue("@SNo", tbSNo.Text);
            cmd.Parameters.AddWithValue("@ReceivedOn", rdate);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Degree Info", "Received Degree for Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Session["Srid"].ToString().Trim().Length > 0)
                Response.Redirect("showaddress1.aspx");
        }
    }
}
