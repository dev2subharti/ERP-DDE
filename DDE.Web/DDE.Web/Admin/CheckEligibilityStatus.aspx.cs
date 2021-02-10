using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class CheckEligibilityStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {

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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateStudentDetails();
        }

        private void populateStudentDetails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select PSRID,StudentName,FatherName,StudyCentreCode,Course,CYear,Eligible,ExID,ReasonIfPending from DDEPendingStudentRecord where PSRID='" + tbPANo.Text + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                imgStudent.ImageUrl = "StudentImgHandler.ashx?PSRID=" + dr["PSRID"].ToString();
                tbPSRID.Text = dr["PSRID"].ToString();
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();
                tbSCCode.Text = dr["StudyCentreCode"].ToString();
                tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                tbYear.Text = FindInfo.findAlphaYear(Convert.ToString(dr["CYear"])).ToUpper();
                tbExName.Text = FindInfo.findExaminerByID(Convert.ToInt32(dr["ExID"])).ToString();
                if(dr["Eligible"].ToString()=="")
                {
                    tbCurrentStatus.Text = "NOT SET";
                }
                else
                {
                    tbCurrentStatus.Text = dr["Eligible"].ToString();
                }

                tbRemark.Text = dr["ReasonIfPending"].ToString();
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No record found!!";
                pnlMSG.Visible = true;

            }

            con.Close();
        }
    }
}