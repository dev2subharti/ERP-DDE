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
    public partial class PublishAwardSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 70))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["PaperCode"] != null)
                    {
                        rblMode.SelectedItem.Selected = false;
                        rblMode.Items.FindByValue("1").Selected = true;
                        rblMode.Enabled = false;

                        ddlistExamination.SelectedItem.Selected = false;
                        ddlistExamination.Items.FindByValue(Session["ExamCode"].ToString()).Selected = true;
                        ddlistExamination.Enabled = false;

                        if (Session["ExamCode"].ToString() == "A13")
                        {
                            pnlCalender.Visible = true;
                        }
                        else if (Session["ExamCode"].ToString() == "B13" || Session["ExamCode"].ToString() == "A14" || Session["ExamCode"].ToString() == "B14" || Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17" || Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                        {
                            pnlCalender.Visible = false;
                        }

                        lblPC.Text = "Paper Code";
                        tbPaperCode.Text = Request.QueryString["PaperCode"];
                        tbPaperCode.Enabled = false;



                    }
                    else if (Request.QueryString["ASNo"] != null)
                    {
                        rblMode.SelectedItem.Selected = false;
                        rblMode.Items.FindByValue("2").Selected = true;
                        rblMode.Enabled = false;

                        ddlistExamination.SelectedItem.Selected = false;
                        ddlistExamination.Items.FindByValue(Session["ExamCode"].ToString()).Selected = true;
                        ddlistExamination.Enabled = false;

                        if (Session["ExamCode"].ToString() == "A13")
                        {
                            pnlCalender.Visible = true;
                        }
                        else if (Session["ExamCode"].ToString() == "B13" || Session["ExamCode"].ToString() == "A14" || Session["ExamCode"].ToString() == "B14" || Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                        {
                            pnlCalender.Visible = false;
                        }

                        lblPC.Text = "Award Sheet No.";
                        tbPaperCode.Text = Request.QueryString["ASNo"];
                        tbPaperCode.Enabled = false;


                    }





                    pnlData.Visible = true;
                    pnlMSG.Visible = false;
                }
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private void setCurrentDate()
        {
            ddlistDFrom.SelectedItem.Selected = false;
            ddlistMonthFrom.SelectedItem.Selected = false;
            ddlistYearFrom.SelectedItem.Selected = false;

            ddlistDTo.SelectedItem.Selected = false;
            ddlistMonthTo.SelectedItem.Selected = false;
            ddlistYearTo.SelectedItem.Selected = false;

            ddlistDFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (ddlistExamination.SelectedItem.Value == "A13")
            {
                string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDFrom.SelectedItem.Text + " 00:00:01";
                string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDTo.SelectedItem.Text + " 23:59:59";

                Session["From"] = from;
                Session["To"] = to;

            }
           
            string pc = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectID,SyllabusSession from DDESubject where SyllabusSession!='A 2009-10' and PaperCode='" + tbPaperCode.Text + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            int i = 0;

            if (dr.HasRows)
            {
                i = i + 1;
                while (dr.Read())
                {
                    if (pc == "")
                    {
                        pc = dr[0].ToString();
                    }
                    else
                    {
                        pc = pc + "," + dr[0].ToString();
                    }
                    

                }
                Session["SubjectID"] = pc;
                Session["SubjectCode"] = tbPaperCode.Text.ToUpper();
                Session["SubjectName"] = FindInfo.findAllSubjectNameByPaperCode(tbPaperCode.Text);


            }

            con.Close();



            if (rblMode.SelectedItem.Value == "1")
            {

                if (i == 0)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Invalid Paper Code. <br/> Please Enter a valid Paper Code";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
                else if ((i == 1))
                {
                    Session["ExamCode"] = ddlistExamination.SelectedItem.Value;
                    Session["ExamName"] = ddlistExamination.SelectedItem.Text;
                    Session["ASPrinted"] = "NO";
                    Session["CF"] = "AAS";
                    Response.Redirect("AwardSheet.aspx");
                }
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                if (ddlistExamination.SelectedItem.Value == "B13" || ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17"|| ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10")
                {
                    try
                    {
                        if (FindInfo.awardSheetNoExist(Convert.ToInt32(tbPaperCode.Text), ddlistExamination.SelectedItem.Value))
                        {

                            Session["ExamCode"] = ddlistExamination.SelectedItem.Value;
                            Session["ExamName"] = ddlistExamination.SelectedItem.Text;
                            string[] subdet = FindInfo.findSubjectDetailByASPRID(Convert.ToInt32(tbPaperCode.Text), ddlistExamination.SelectedItem.Value);
                            Session["SubjectName"] = subdet[0];
                            Session["SubjectCode"] = subdet[1];
                            Session["ASPrinted"] = "NO";
                            Session["CF"] = "AAS";
                            Response.Redirect("AwardSheet.aspx?ASNo=" + String.Format("{0:0000}", Convert.ToInt32(tbPaperCode.Text)));
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! Invalid Award Sheet No. <br/> Please Enter a valid Award Sheet No.";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                        }
                    }
                    catch
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Award Sheet No. should be in numeric form";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! This service is valid only for December 2013 exam";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }

            }



        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlMSG.Visible = false;

        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "1")
            {
                lblPC.Text = "Paper Code";
                pnlCalender.Visible = true;
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                lblPC.Text = "Award Sheet No.";
                pnlCalender.Visible = false;
            }
        }

        protected void ddlistExamination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistExamination.SelectedItem.Value == "A13")
            {
                setCurrentDate();
                pnlCalender.Visible = true;
            }
            else if (ddlistExamination.SelectedItem.Value == "B13" || ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17"|| ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10")
            {
                pnlCalender.Visible = false;
            }
            
        }

    }
}
