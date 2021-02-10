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
    public partial class ChangeSLMCourse : System.Web.UI.Page
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
                populateSLMIssueDetails();
                setValidation();
               
               
            }
            else
            {
                pnlStudentDetails.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = "Sorry !! This Enrollment No does not exist";
                pnlMSG.Visible = true;

            }


        }

        private void setValidation()
        {
           
            foreach (DataListItem dli in dtlistSLMIssueRecord.Items)
            {


                Label lno = (Label)dli.FindControl("lblLNo");
                LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");

                if (Convert.ToInt32(lno.Text) == 0)
                {
                    edit.Visible = true;
                }
                else
                {
                    edit.Visible = false;
                }
               



            }
           
        }

        private void populateSLMIssueDetails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select* from DDESLMIssueRecord where SRID='"+FindInfo.findSRIDByENo(tbENo.Text)+"' and SLMRID>=11087", con);   

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("Course");
            DataColumn dtcol3 = new DataColumn("Year");
            DataColumn dtcol4 = new DataColumn("SCCode");
            DataColumn dtcol5 = new DataColumn("TOR");
            DataColumn dtcol6 = new DataColumn("LNo");
            DataColumn dtcol7 = new DataColumn("SLMRID");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            con.Open();
            dr = cmd.ExecuteReader();
            int i = 1;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["SLMRID"] = Convert.ToInt32(dr["SLMRID"]);
                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["CID"]));
                    drow["Year"] = Convert.ToInt32(dr["Year"]);
                    drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                    if (dr["TOR"].ToString() != "")
                    {
                        drow["TOR"] = Convert.ToDateTime(dr["TOR"]).ToString("dd MMMM yyyy");
                    }
                    else
                    {
                        drow["TOR"] = "NOT AVAILABLE";
                    }
                   
                    drow["LNo"] = Convert.ToInt32(dr["LNo"]);
                 
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }
            con.Close();




            dtlistSLMIssueRecord.DataSource = dt;
            dtlistSLMIssueRecord.DataBind();


            if (i > 1)
            {
                dtlistSLMIssueRecord.Visible = true;
                btnSearch.Visible = false;
            }
            else
            {
                pnlStudentDetails.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }
        }

        private void polulateStudentInfo(int slmrid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDESLMIssueRecord.SRID,DDESLMIssueRecord.Year,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.StudyCentreCode,DDESLMIssueRecord.CID from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID where DDESLMIssueRecord.SLMRID='" + slmrid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {

                dr.Read();
                i = 1;
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + dr["SRID"].ToString();
                lblSLMRID.Text = slmrid.ToString();
                tbEnNo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = dr["SRID"].ToString();            
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();
                tbSCCode.Text = dr["StudyCentreCode"].ToString();
                lblCID.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["CID"]));
                ddlistCourse.SelectedItem.Selected = false;
                ddlistCourse.Items.FindByValue(dr["CID"].ToString()).Selected = true;
                setYearList(Convert.ToInt32(dr["CID"]));
                ddlistYear.Items.FindByValue(dr["Year"].ToString()).Selected = true;

            }
            

            con.Close();

            if (i > 0)
            {
                dtlistSLMIssueRecord.Visible = false;
                pnlStudentDetails.Visible = true;
                pnlSearch.Visible = false;
                btnUpdate.Visible = true;
            }



        }

        private void setYearList(int cid)
        {
            int duration = FindInfo.findCourseDuration(cid);
            ddlistYear.Items.Clear();

            if (duration == 1)
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";
            }
            else if (duration == 2)
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";
            
            }
            else if (duration == 3)
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";             
            }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlistCourse.SelectedItem.Text != "--SELECT ONE--" && ddlistYear.SelectedItem.Text != "--SELECT ONE--")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDESLMIssueRecord set CID=@CID,Year=@Year where SLMRID='" + lblSLMRID.Text + "'", con);

                cmd.Parameters.AddWithValue("@CID", Convert.ToInt32(ddlistCourse.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(ddlistYear.SelectedItem.Value));


                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Change Student Details for SLM", "Changed Student Details for SLM of Enrollment No '" + tbENo.Text + "' to  Year '" + ddlistYear.SelectedItem.Value + "' and from CourseID '" + lblCID.Text + "' to '" + ddlistCourse.SelectedItem.Value + "'", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
            }
            else
            {
                pnlStudentDetails.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = "Please select all entries.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        protected void dtlistSLMIssueRecord_ItemCommand(object source, DataListCommandEventArgs e)
        {
            PopulateDDList.populateCourses(ddlistCourse);
            polulateStudentInfo(Convert.ToInt32(e.CommandArgument));
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlStudentDetails.Visible = true;
            btnUpdate.Visible = true;      
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }

    }
}