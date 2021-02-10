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
    public partial class ClearSIStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95))
            {
                pnlSearch.Visible = true;
                pnlStudentDetails.Visible = false;
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
            if (srid != 0)
            {
             
                polulateStudentInfo(srid);
               
            }
            else
            {
                pnlStudentDetails.Visible = false;
                btnDelete.Visible = false;
                lblMSG.Text = "Sorry !! This Enrollment No does not exist";
                pnlMSG.Visible = true;

            }


        }



        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.StudyCentreCode,DDESLMIssueRecord.CID,DDESLMIssueRecord.LNo from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID where DDESLMIssueRecord.SRID='" + srid + "' and DDESLMIssueRecord.Year='" + ddlistYear.SelectedItem.Value + "' and DDESLMIssueRecord.LNo!='0'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                dr.Read();
                i = 1;
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                tbEnNo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = srid.ToString();
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();
                tbSCCode.Text = dr["StudyCentreCode"].ToString();
                tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["CID"]));
                lblLNo.Text = dr["LNo"].ToString();

            }

            con.Close();

            if (i == 1)
            {
                pnlStudentDetails.Visible = true;
                btnDelete.Visible = true;
                tbEnNo.Enabled = false;
                ddlistYear.Enabled = false;
            }
            else
            {
                pnlStudentDetails.Visible = false;
                btnDelete.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }



        }
      

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDESLMIssueRecord set LNo=@LNo where SRID='" + lblSRID.Text + "' and Year='" + ddlistYear.SelectedItem.Value + "'", con);

           
            cmd.Parameters.AddWithValue("@LNo", 0);
          


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Delete SI Record", "Deleted SLM Issue record of student with Enrollment No '" + tbENo.Text + "' and Year '" + ddlistYear.SelectedItem.Value + "' from SLM Letter No='" + lblLNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

            pnlData.Visible = false;
            lblMSG.Text = "Record has been deleted successfully";
            pnlMSG.Visible = true;
        }

    }
}