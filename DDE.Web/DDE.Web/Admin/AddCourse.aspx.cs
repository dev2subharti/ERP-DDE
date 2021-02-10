using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class AddCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 8))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateStreamNames(ddlistStream);
                    if (Request.QueryString["CourseID"] != null)
                    {
                        populateCourseByID();
                        btnSubmit.Text = "Update";
                        
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

        private void populateCourseByID()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDECourse where CourseID='"+Request.QueryString["CourseID"]+"'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbCourseCode.Text = dr["CourseCode"].ToString();
                tbCourseSN.Text=dr["CourseShortName"].ToString();
                tbCourseFN.Text = dr["CourseFullName"].ToString();
                tbSpecialization.Text = dr["Specialization"].ToString();
                tbProgCode.Text = dr["ProgramCode"].ToString();
              
                ddlistCourseDuration.Items.FindByValue(dr["CME"].ToString()).Selected = true;
                ddlistCourseMaxDuration.Items.FindByValue(dr["FreeAC"].ToString()).Selected = true;
            }

            con.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["CourseID"] == null)
            {
                if (!FindInfo.isCourseExist(tbCourseCode.Text))
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDECourse values(@CourseCode,@CourseShortName,@CourseFullName,@Specialization,@ProgramCode,@StreamFee,@StreamID,@A09,@C10,@A10,@C11,@A11,@C12,@A12,@C13,@A13,@C14,@A14,@C15,@A15,C16,@A16,@CourseDuration,@CourseMaxDuration,@Online)", con);


                    cmd.Parameters.AddWithValue("@CourseCode", tbCourseCode.Text);
                    cmd.Parameters.AddWithValue("@CourseShortName", tbCourseSN.Text);
                    cmd.Parameters.AddWithValue("@CourseFullName", tbCourseFN.Text);
                    cmd.Parameters.AddWithValue("@Specialization", tbSpecialization.Text);
                    cmd.Parameters.AddWithValue("@ProgramCode", tbProgCode.Text);
                    cmd.Parameters.AddWithValue("@StreamFee",Convert.ToInt32(tbStreamFee.Text) );
                    cmd.Parameters.AddWithValue("@StreamID", Convert.ToInt32(ddlistStream.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@A09", 0);
                    cmd.Parameters.AddWithValue("@C10", 0);
                    cmd.Parameters.AddWithValue("@A10", 0);
                    cmd.Parameters.AddWithValue("@C11", 0);
                    cmd.Parameters.AddWithValue("@A11", 0);
                    cmd.Parameters.AddWithValue("@C12", 0);
                    cmd.Parameters.AddWithValue("@A12", 0);
                    cmd.Parameters.AddWithValue("@C13", 0);
                    cmd.Parameters.AddWithValue("@A13", 0);
                    cmd.Parameters.AddWithValue("@C14", 0);
                    cmd.Parameters.AddWithValue("@A14", 0);
                    cmd.Parameters.AddWithValue("@C14", 0);
                    cmd.Parameters.AddWithValue("@A15", 0);
                    cmd.Parameters.AddWithValue("@C16", 0);
                    cmd.Parameters.AddWithValue("@A16", Convert.ToInt32(tbAdmissionFee.Text));
                    cmd.Parameters.AddWithValue("@CourseDuration", Convert.ToInt32(ddlistCourseDuration.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@CourseMaxDuration", Convert.ToInt32(ddlistCourseMaxDuration.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@Online", "True");
                  

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Log.createLogNow("Create", "Created a course '" + tbCourseSN.Text + " (" + tbSpecialization.Text + ")'", Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "Course has been created successfully";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! this course code is already exist";
                    pnlMSG.Visible = true;
                }
            }

            else if (Request.QueryString["CourseID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDECourse set CourseCode=@CourseCode,CourseShortName=@CourseShortName,CourseFullName=@CourseFullName,Specialization=@Specialization,ProgramCode=@ProgramCode,CourseDuration=@CourseDuration,CourseMaxDuration=@CourseMaxDuration where CourseID='" + Request.QueryString["CourseID"] + "' ", con);
                
                
                cmd.Parameters.AddWithValue("@CourseCode", tbCourseCode.Text);
                cmd.Parameters.AddWithValue("@CourseShortName", tbCourseSN.Text);
                cmd.Parameters.AddWithValue("@CourseFullName", tbCourseFN.Text);
                cmd.Parameters.AddWithValue("@Specialization", tbSpecialization.Text);
                cmd.Parameters.AddWithValue("@ProgramCode", tbProgCode.Text);
               
                cmd.Parameters.AddWithValue("@CourseDuration", Convert.ToInt32(ddlistCourseDuration.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@CourseMaxDuration", Convert.ToInt32(ddlistCourseMaxDuration.SelectedItem.Value));
               
        

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Updated record of course '" + tbCourseSN.Text + " (" + tbSpecialization.Text + ")'", Convert.ToInt32(Session["ERID"].ToString()));
                
                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = true;


            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCourse.aspx");
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowCourse.aspx");
        }

        protected void ddlistStream_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistStream.SelectedItem.Text != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select StreamFee from DDEStreams where StreamID='" + ddlistStream.SelectedItem.Value + "'", con);

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    tbStreamFee.Text = dr[0].ToString();
                }
                con.Close();
            }
            else
            {
                tbStreamFee.Text = "NA";
            }
        }
    }
}
