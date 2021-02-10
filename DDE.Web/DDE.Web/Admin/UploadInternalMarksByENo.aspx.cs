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
    public partial class UploadInternalMarksByENo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorisedSCFor(Session["ERID"].ToString(), 25))
            {
                if (!IsPostBack)
                {
                    ddlistExam.Items.FindByValue("W11").Selected = true;
                    ddlistMOE.Items.FindByValue("R").Selected = true;
                    ddlistMOE.Enabled = false;
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (tbENo.Text != "")
            {
                int srid = FindInfo.findSRIDByENo(tbENo.Text);
                string error;
                if (validENo(srid, out error))
                {
                    if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                    {
                        string cy;
                        string ey;

                        string year = FindInfo.findAllExamYear1(srid, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out cy, out ey);
                        if (year != "" && year != "0")
                        {
                            if (year.Length > 1)
                            {
                                populateYears(year);
                                tbENo.Enabled = false;
                                lblYear.Visible = true;
                                ddlistYear.Visible = true;
                                btnFind.Visible = false;
                                btnFind2.Visible = true;

                            }
                            else
                            {
                                Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                                Session["ExamName"] = ddlistExam.SelectedItem.Text;
                                Session["ResultType"] = ddlistMOE.SelectedItem.Value;
                                if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                                {
                                    Session["SS"] = FindInfo.findSySessionBySRID(srid);
                                }
                                else
                                {
                                    Session["SS"] = "A 2010-11";
                                }
                                Session["Year"] = year;
                                Session["AYear"] = FindInfo.findAlphaYear(year);
                                Session["MarkSheetType"] = "REGULAR";

                                if (ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                                {

                                    Response.Redirect("UploadInternalMarks1.aspx?EnrollmentNo=" + tbENo.Text);
                                }
                                else
                                {
                                    Response.Redirect("UploadInternalMarks.aspx?EnrollmentNo=" + tbENo.Text);
                                }

                            }
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry! Course or Exam Fee or Both are not paid";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                        }
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
                lblMSG.Text = "Please fill Enrollment No.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }

        }

        private void populateYears(string year)
        {
            if (year == "1,2" || year == "2,1")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "REAPPEAR";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "REGULAR";
            }
            else if (year == "2,3" || year == "3,2")
            {
                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "REAPPEAR";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "REGULAR";
            }
            else if (year == "1,3" || year == "3,1")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "REAPPEAR";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "REGULAR";
            }
            else if (year == "1,2,3" || year == "3,2,1" || year == "3,1,2" || year == "2,1,3" || year == "2,3,1" || year == "1,3,2")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
        }

        private bool validENo(int srid, out string error)
        {
            error = "Please Check All Entries";
            bool exist = false;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SRID from DDEStudentRecord where SRID='" + srid + "' and RecordStatus='1'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                exist = true;


            }
            else
            {
                exist = false;
                error = "Sorry !! not a valid Enrollment No.";
            }


            con.Close();




            return exist;
        }

       


        protected void btnFind2_Click(object sender, EventArgs e)
        {
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["ExamName"] = ddlistExam.SelectedItem.Text;
            Session["ResultType"] = ddlistMOE.SelectedItem.Value;
            Session["Year"] = ddlistYear.SelectedItem.Text.Substring(0, 1);
            Session["AYear"] = ddlistYear.SelectedItem.Text;
            Session["MarkSheetType"] = ddlistYear.SelectedItem.Value;

            int srid = FindInfo.findSRIDByENo(tbENo.Text);
            if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
            {
                Session["SS"] = FindInfo.findSySessionBySRID(srid);
            }
            else
            {
                Session["SS"] = "A 2010-11";
            }
            if (ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
            {

                Response.Redirect("UploadInternalMarks1.aspx?EnrollmentNo=" + tbENo.Text);
            }
            else
            {
                Response.Redirect("UploadInternalMarks.aspx?EnrollmentNo=" + tbENo.Text);
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }

    }
}