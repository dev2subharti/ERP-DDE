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
    public partial class SetSpecialisation : System.Web.UI.Page
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
            if (validENo(srid))
            {
                populateCourses();
                polulateStudentInfo(srid);

                
                          
                pnlSearch.Visible = false;
                pnlStudentDetails.Visible = true;
                if (alreadyUpdated(Convert.ToInt32(lblSRID.Text)))
                {
                    populatePreInfo(Convert.ToInt32(lblSRID.Text));
                    //btnUpdate.Visible = false;
                    //ddlist1Year.Enabled = false;
                    //ddlist2Year.Enabled = false;
                    //ddlist3Year.Enabled = false;
                    btnUpdate.Visible = true;
                    //lblMSG.Text = "Specialisation is already updated of this Student";
                    //pnlMSG.Visible = true;
                }
                else
                {
                    tbDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    tbModifiedBy.Text = FindInfo.findEmployeeNameByERID(Convert.ToInt32(Session["ERID"]));
                    btnUpdate.Visible = true;
                    lblMSG.Text = "";
                    pnlMSG.Visible = false;
                }
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! not a valid Enrollment No.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                btnUpdate.Visible =false;
            }
        }

        private void populatePreInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESpecialisationRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {               
                tbDate.Text = dr["TimeOfChange"].ToString();
                ddlistAAuthority.Items.FindByText(dr["AAuthority"].ToString());
                ddlistAAuthority.Enabled = false;
                tbModifiedBy.Text = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["ChangedBy"]));
            }
            con.Close();
        }

        private bool alreadyUpdated(int srid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESpecialisationRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["1Y"].ToString() != "NOT SET" || dr["2Y"].ToString() != "NOT SET" || dr["3Y"].ToString() != "NOT SET")
                {
                    exist = true;
                }
            }

            con.Close();

            return exist;
        }

        private void populateCourses()
        {
            

            ddlist2Year.Items.Add("NOT SET");
            ddlist2Year.Items.FindByText("NOT SET").Value = "0";

            ddlist3Year.Items.Add("NOT SET");
            ddlist3Year.Items.FindByText("NOT SET").Value = "0";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse where CourseShortName='MBA' order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[2].ToString() == "")
                {
                   
                    ddlist2Year.Items.Add(dr[1].ToString());
                    ddlist2Year.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                    ddlist3Year.Items.Add(dr[1].ToString());
                    ddlist3Year.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                   

                    ddlist2Year.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlist2Year.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                    ddlist3Year.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlist3Year.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }


            }
            con.Close();
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
                            tbCourse.Text ="Not Set";
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
                        if (dr["Course3Year"].ToString() == "")
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



                if (dr["Course"].ToString() == "")
                {
                    ddlist1Year.Items.FindByText("NOT SET").Selected = true;
                }
                else
                {
                    ddlist1Year.Items.FindByValue(dr["Course"].ToString()).Selected = true;

                }

                if (dr["Course2Year"].ToString() == "")
                {
                    ddlist2Year.Items.FindByText("NOT SET").Selected = true;
                }
                else
                {
                    ddlist2Year.Items.FindByValue(dr["Course2Year"].ToString()).Selected = true;

                }
                if (dr["Course3Year"].ToString() == "")
                {
                    ddlist3Year.Items.FindByText("NOT SET").Selected = true;
                }
                else
                {

                    ddlist3Year.Items.FindByValue(dr["Course3Year"].ToString()).Selected = true;

                }

                tbYear.Text =FindInfo.findAlphaYear(dr["CYear"].ToString()).ToUpper() ;

                

            }

            con.Close();

           
            
        }

        private bool validENo(int srid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select Course from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

           
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr[0])) == "MBA")
                {
                    exist = true;
                }
               
            }

            con.Close();

            return exist;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set Course=@Course,Course2Year=@Course2Year,Course3Year=@Course3Year where SRID='" + lblSRID.Text + "'", con);

            cmd.Parameters.AddWithValue("@Course", 76);
            cmd.Parameters.AddWithValue("@Course2Year", ddlist2Year.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Course3Year", ddlist3Year.SelectedItem.Value);
          
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Set Specialisation", "Specialisation was set with 1 Y-'MBA',2 Y-'"+ddlist2Year.SelectedItem.Text+"',3 Y-'"+ddlist3Year.SelectedItem.Text+"' with Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
            createSpcialisationRecord();
            pnlData.Visible = false;
            lblMSG.Text = "Record has been updated successfully";
            pnlMSG.Visible = true;
        }

        private void createSpcialisationRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESpecialisationRecord values(@SRID,@1Y,@2Y,@3Y,@AAuthority,@ChangedBy,@TimeOfChange)", con);

            cmd.Parameters.AddWithValue("@SRID", lblSRID.Text);
            cmd.Parameters.AddWithValue("@1Y", 76);
            cmd.Parameters.AddWithValue("@2Y", ddlist2Year.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@3Y", ddlist3Year.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@AAuthority", ddlistAAuthority.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@ChangedBy",Convert.ToInt32(Session["ERID"]));
            cmd.Parameters.AddWithValue("@TimeOfChange", DateTime.Now.ToString());

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }
    }
}
