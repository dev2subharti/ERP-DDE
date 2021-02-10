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
using DDE.DAL;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class FindMarkSheetByENo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 30) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 112))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("W11").Selected = true;
                    ddlistSySession.Visible = false;
                    ddlistMOE.Items.FindByValue("R").Selected = true;
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
                int srid=FindInfo.findSRIDByENo(tbENo.Text);
                string error;
                if (validENo(srid,out error))
                {
                    if (ddlistExam.SelectedItem.Value == "A13" || ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                    {
                        string year = FindInfo.findAllExamYear(srid, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                        if (year != "No" && year != "0" && year != "")
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
                                if (ddlistExam.SelectedItem.Value == "A13" || ddlistExam.SelectedItem.Value == "B13")
                                {
                                    Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                                    Session["SySession"] = ddlistSySession.SelectedItem.Value;
                                    Session["ExamName"] = ddlistExam.SelectedItem.Text;
                                    Session["ResultType"] = ddlistMOE.SelectedItem.Value;
                                    Session["Year"] = year;
                                    Session["AYear"] = FindInfo.findAlphaYear(year);
                                    Session["MarkSheetType"] = "REGULAR";
                                    Response.Redirect("ShowMarks.aspx?EnrollmentNo=" + tbENo.Text);
                                }
                                else if (ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                                {
                                    if (ddlistMOE.SelectedItem.Value == "R")
                                    {
                                        Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                                        Session["SySession"] = ddlistSySession.SelectedItem.Value;
                                        Session["ExamName"] = ddlistExam.SelectedItem.Text;
                                        Session["ResultType"] = ddlistMOE.SelectedItem.Value;
                                        Session["Year"] = year;
                                        Session["AYear"] = FindInfo.findAlphaYear(year);
                                        Session["MarkSheetType"] = "REGULAR";
                                        if (ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                                        {
                                            Response.Redirect("ShowMarks1.aspx?EnrollmentNo=" + tbENo.Text);
                                        }
                                        else
                                        {
                                            Response.Redirect("ShowMarks.aspx?EnrollmentNo=" + tbENo.Text);
                                        }
                                    }
                                    else if (ddlistMOE.SelectedItem.Value == "B")
                                    {

                                        string bpsubid;
                                        string bppracid;
                                        string preexams = FindInfo.findPreviousExamForBP(srid, ddlistExam.SelectedItem.Value, year, out bpsubid, out bppracid);

                                     
                                        Session["BPSubjects"] = bpsubid;
                                        Session["BPPracticals"] = bppracid;
                                          
                                        Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                                        Session["SySession"] = ddlistSySession.SelectedItem.Value;
                                        Session["ExamName"] = ddlistExam.SelectedItem.Text;
                                        Session["ResultType"] = "B";
                                        Session["Year"] = year;
                                        Session["AYear"] = FindInfo.findAlphaYear(year);
                                        Session["MarkSheetType"] = "REGULAR";

                                        string [] bpexamcode = preexams.Split(',');
                                       
                                        if (FindInfo.findExamIDByExamCode(bpexamcode[0])<20)
                                        {
                                            Response.Redirect("ShowMarks.aspx?EnrollmentNo=" + tbENo.Text);
                                        }
                                        else
                                        {
                                            Response.Redirect("ShowMarks1.aspx?EnrollmentNo=" + tbENo.Text);
                                        }

                                    }

                                }                             

                            }
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry Exam or Course or both Fee are not received for this Examination";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                        }
                    }
                    else
                    {
                        Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                        Session["SySession"] = ddlistSySession.SelectedItem.Value;
                        Session["ExamName"] = ddlistExam.SelectedItem.Text;
                        Session["ResultType"] = ddlistMOE.SelectedItem.Value;
                        Session["Year"] = ddlistSYear.SelectedItem.Value;
                        Session["AYear"] = FindInfo.findAlphaYear(Session["Year"].ToString());

                        Session["MarkSheetType"] = "REGULAR";

                       
                        Response.Redirect("ShowMarks.aspx?EnrollmentNo=" + tbENo.Text);
                       
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
            if (year == "1")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";
            }
            else if (year == "2")
            {
                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";
            }
            else if (year == "3")
            {
                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
            else if (year == "1,2" || year == "2,1")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";
            }
            else if (year == "2,3" || year == "3,2")
            {
                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
            else if (year == "1,3" || year == "3,1")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
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

        private bool validENo(int srid,out string error)
        {
                error = "Please Check All Entries";
                string remark;
                bool exist = false;
                

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select SRID from DDEStudentRecord where SRID='" + srid + "'", con);
                SqlDataReader dr;


                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (!(FindInfo.isDetained(srid, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out remark)))
                    {
                        exist = true;
                    }
                    else
                    {
                        exist = false;
                        error = "Sorry !! Student is detained for this examination <br/> Reason : "+remark;
                    }

                }
                else
                {
                    exist = false;
                    error = "Sorry !! not a valid Enrollment No.";
                }
                con.Close();

                return exist;       

            
        }

      

        private bool isPCPStudent(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select QualifyingStatus from DDEStudentRecord Where SRID='" + srid + "'", con);
            SqlDataReader dr;

            bool auth = false;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();

            if (dr[0].ToString() == "PCP")
            {
                auth = true;
            }
            else if (dr[0].ToString() == "AC")
            {
                auth = false;
            }


            con.Close();

            return auth;
        }

        protected void btnFind2_Click(object sender, EventArgs e)
        {

            int srid = FindInfo.findSRIDByENo(tbENo.Text);
            if (ddlistExam.SelectedItem.Value == "A13" || ddlistExam.SelectedItem.Value == "B13")
            {
                Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                Session["SySession"] = ddlistSySession.SelectedItem.Value;
                Session["ExamName"] = ddlistExam.SelectedItem.Text;
                Session["ResultType"] = ddlistMOE.SelectedItem.Value;
                Session["Year"] = ddlistYear.SelectedItem.Text.Substring(0,1);
                Session["AYear"] = ddlistYear.SelectedItem.Text;
                Session["MarkSheetType"] = "REGULAR";
                Response.Redirect("ShowMarks.aspx?EnrollmentNo=" + tbENo.Text);
            }
            else if (ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
            {
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                    Session["SySession"] = ddlistSySession.SelectedItem.Value;
                    Session["ExamName"] = ddlistExam.SelectedItem.Text;
                    Session["ResultType"] = ddlistMOE.SelectedItem.Value;
                    Session["Year"] = ddlistYear.SelectedItem.Text.Substring(0, 1);
                    Session["AYear"] = ddlistYear.SelectedItem.Text;
                    Session["MarkSheetType"] = "REGULAR";

                    if (ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                    {
                        Response.Redirect("ShowMarks1.aspx?EnrollmentNo=" + tbENo.Text);
                    }
                    else
                    {
                        Response.Redirect("ShowMarks.aspx?EnrollmentNo=" + tbENo.Text);
                    }
                }
                else if (ddlistMOE.SelectedItem.Value == "B")
                {
                    

                        string bpsubid;
                        string bppracid;
                        string preexams = FindInfo.findPreviousExamForBP(srid, ddlistExam.SelectedItem.Value, ddlistYear.SelectedItem.Text.Substring(0, 1), out bpsubid, out bppracid);


                        Session["BPSubjects"] = bpsubid;
                        Session["BPPracticals"] = bppracid;

                        Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                        Session["SySession"] = ddlistSySession.SelectedItem.Value;
                        Session["ExamName"] = ddlistExam.SelectedItem.Text;
                        Session["ResultType"] = "B";
                        Session["Year"] = ddlistYear.SelectedItem.Text.Substring(0, 1);
                        Session["AYear"] = ddlistYear.SelectedItem.Text;
                        Session["MarkSheetType"] = "REGULAR";

                   
                        Response.Redirect("ShowMarks.aspx?EnrollmentNo=" + tbENo.Text);
                   

                }

            }

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
            {
                lblY.Visible = false;
                ddlistSYear.Visible = false;
                ddlistSySession.Visible = false;
            }
            else if (ddlistExam.SelectedItem.Value == "A13" || ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14")
            {
                lblY.Visible = false;
                ddlistSYear.Visible = false;
                ddlistSySession.Visible = true;
            }
            else
            {
                lblY.Visible = true;
                ddlistSYear.Visible = true;
                ddlistSySession.Visible = true;
            }
        }
  

    }
}
