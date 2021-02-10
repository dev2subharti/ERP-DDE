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
    public partial class FindAdmitCard : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                if (!IsPostBack)
                {
                    
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("Z11").Selected = true;
                    ////ddlistExam.Enabled = false;
                    PopulateDDList.populateSySession(ddlistSySession);
                    ddlistSySession.Enabled=true;
                    ddlistCourse.Items.Add("ALL");
                    PopulateDDList.populateCourses(ddlistCourse);
                    ddlistSession.Items.Add("ALL");
                    PopulateDDList.populateBatch(ddlistSession);
                    ddlistSCCode.Items.Add("ALL");
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    ddlistExamCentre.Items.Add("ALL");
                   
                    PopulateDDList.populateCity(ddlistExamCentre, "UTTAR PRADESH");
                 
                    ddlistSySession.Enabled = false;
                    ddlistSession.Enabled = false;
                    ddlistSCCode.Items.Remove("ALL");

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
                int srid = FindInfo.findSRIDByENo(tbENo.Text);
              
                if (srid != 0)
                {
                    Session["SRID"] = srid;
                    Session["CardType"] = ddlistCardType.SelectedItem.Value;
                    Session["Exam"] = ddlistExam.SelectedItem.Text;
                    Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                    Session["AdmitSySession"] = ddlistSySession.SelectedItem.Text;
                    Session["AdmitSCCode"] = ddlistSCCode.SelectedItem.Text;
                    Session["AdmitCourse"] = ddlistCourse.SelectedItem.Value;
                    Session["AdmitBatch"] = ddlistSession.SelectedItem.Text;

                    if (ddlistCard.SelectedItem.Text == "ADMIT CARD")
                    {
                        if (ddlistExam.SelectedItem.Value == "A15")
                        {
                            Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                            Response.Redirect("AdmitCardList1.aspx");
                        }
                        if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                        {
                            Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                            Response.Redirect("AdmitCardList2.aspx");
                        }
                        else
                        {
                            Response.Redirect("AdmitCardList.aspx");
                        }
                    }

                    else if (ddlistCard.SelectedItem.Text == "VERIFICATION SEAT")
                    {
                        if (ddlistExam.SelectedItem.Value == "A15")
                        {
                            Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                            Response.Redirect("VerificationSheetList1.aspx");
                        }
                        if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                        {
                            Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                            Response.Redirect("VerificationSheetList2.aspx");
                        }
                        else
                        {
                            Response.Redirect("VerificationSheatList.aspx");
                        }

                    }
                }

                else
                {

                    pnlData.Visible = false;
                    lblMSG.Text = "This Enrollment No Does not Exist";
                    pnlMSG.Visible = true;
                }
 
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Session["SRID"] = "ALL";
            Session["CardType"] = ddlistCardType.SelectedItem.Value;
            Session["Exam"] = ddlistExam.SelectedItem.Text;
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["AdmitSySession"] = ddlistSySession.SelectedItem.Text;
            Session["AdmitSCCode"] = ddlistSCCode.SelectedItem.Text;
            Session["AdmitCourse"] = ddlistCourse.SelectedItem.Value;
            Session["AdmitBatch"] = ddlistSession.SelectedItem.Text;
            Session["ExamCentre"] = ddlistExamCentre.SelectedItem.Text;
            Session["Zone"] = ddlistJone.SelectedItem.Value;

            if (ddlistCard.SelectedItem.Text == "ADMIT CARD")
            {
                if (ddlistExam.SelectedItem.Value == "A15")
                {
                    Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                    Response.Redirect("AdmitCardList1.aspx");
                }
                if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                {
                    Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                    Response.Redirect("AdmitCardList2.aspx");
                }
                else
                {
                    Response.Redirect("AdmitCardList.aspx");
                }
            }

            else if (ddlistCard.SelectedItem.Text == "VERIFICATION SEAT")
            {
                if (ddlistExam.SelectedItem.Value == "A15")
                {
                    Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                    Response.Redirect("VerificationSheetList1.aspx");
                }
                if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                {
                    Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                    Response.Redirect("VerificationSheetList2.aspx");
                }
                else
                {
                    Response.Redirect("VerificationSheetList.aspx");
                }
                
            }
            

        }

        protected void ddlistMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistMode.SelectedItem.Text == "BY ENROLLMENT")
            {
                ddlistExam.Enabled = false;
                ddlistSession.Enabled = false;
                ddlistSySession.Enabled = false;
                ddlistCourse.Enabled = false;
                ddlistSCCode.Enabled = false;
                ddlistExamCentre.Enabled = false;
                ddlistJone.Enabled = false;
                btnPublish.Visible = false;
                pnlFind.Visible = true;

            }

            else if (ddlistMode.SelectedItem.Text == "BY COURSE")
            {
               
               
                ddlistSCCode.Enabled = true;
                ddlistCourse.Enabled = true;
                btnPublish.Visible = true;
                pnlFind.Visible = false;

                if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                {
                   
                    ddlistSession.Enabled = false;
                  
                }
                else
                {
                    ddlistSession.Enabled = true;
                   
                }
            }
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17")
            {
                ddlistSySession.Enabled = false;
                ddlistSession.Enabled = false;
                ddlistSCCode.Items.Remove("ALL");
            }
            else
            {
                ddlistSession.Enabled = true;
                ddlistSySession.Enabled = true;
                ddlistSCCode.Items.Add("ALL");
            }
        }

        protected void ddlistSySession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistExamCentre_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateExamCentreCode(ddlistExamCentre.SelectedItem.Text);
        }

        private void populateExamCentreCode(string examcity)
        {
            ddlistJone.Items.Clear();

            if (examcity == "ALL")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select ECID,ExamCentreCode from DDEExaminationCentres_"+ddlistExam.SelectedItem.Value+" order by ECID", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    
                        ddlistJone.Items.Add(dr[1].ToString());
                        ddlistJone.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
                con.Close();
            }

            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select ECID,ExamCentreCode from DDEExaminationCentres_"+ddlistExam.SelectedItem.Value+" where City='" + examcity + "' order by ExamCentreCode", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   
                        ddlistJone.Items.Add(dr[1].ToString());
                        ddlistJone.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                    


                }
                con.Close();
            }
            if (ddlistJone.Items.Count == 0)
            {
                ddlistJone.Items.Add("NOT FOUND");
                ddlistJone.Items.FindByText("NOT FOUND").Value = "0";
                    

            }
        }

        protected void ddlistJone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistCardType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlistCard.SelectedItem.Text=="ADMIT CARD")
            {
                lblExamCity.Visible = false;
                lblECCode.Visible = false;
                ddlistExamCentre.Visible = false;
                ddlistJone.Visible = false;

            }
            else if (ddlistCard.SelectedItem.Text == "VERIFICATION SEAT")
            {
                lblExamCity.Visible = true;
                lblECCode.Visible = true;
                ddlistExamCentre.Visible = true;
                ddlistJone.Visible = true;
                ddlistExamCentre.Enabled = false;
                ddlistJone.Enabled = false;

            }
        }

       
    }
}
