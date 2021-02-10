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
    public partial class ChangeStudyCentre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                pnlSearch.Visible = true;
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
            
            polulateStudentInfo(srid);
            PopulateDDList.populateStudyCentre(ddlistCSC);
            PopulateDDList.populateStudyCentre(ddlistTSC);
            pnlStudentDetails.Visible = true;
            btnUpdate.Visible = true;
           
              
        }

      

        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,StudyCentreCode,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
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
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! This Enrollment No does not exist";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }

            con.Close();
        

           
        }

       

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            insertNewRecord();
            updateCentralRecord();
           

            pnlData.Visible = false;
            lblMSG.Text = "Record has been updated successfully";
            pnlMSG.Visible = true;
        }

        private void insertNewRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDEChangeSCRecord values(@SRID,@PreviousSC,@CurrentSC,@TimeOfChange)", con);

            cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
            cmd.Parameters.AddWithValue("@PreviousSC", ddlistCSC.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@CurrentSC", ddlistTSC.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@TimeOfChange", DateTime.Now.ToString());


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Changed Study Centre", "Changed Study centre of student with Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

          
        }

        private void updateCentralRecord()
        {
            string scs = "";

            if (cblistTMode.SelectedItem.Text == "Permanent Change")
            {
                scs = "C";
            }

            else if (cblistTMode.SelectedItem.Text == "Transfer")
            {
                scs = "T";
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set SCStatus=@SCStatus,StudyCentreCode=@StudyCentreCode,PreviousSCCode=@PreviousSCCode where SRID='" + lblSRID.Text + "'", con);

            cmd.Parameters.AddWithValue("@SCStatus", scs);
            cmd.Parameters.AddWithValue("@StudyCentreCode", ddlistTSC.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@PreviousSCCode", ddlistCSC.SelectedItem.Value);
           
            

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

           
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlStudentDetails.Visible = false;
            btnUpdate.Visible = false;
            pnlSearch.Visible = true;
        }
    }
}
