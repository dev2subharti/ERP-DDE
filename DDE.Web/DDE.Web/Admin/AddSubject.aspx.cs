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
    public partial class AddSubject : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 34))
            {

                if (!IsPostBack)
                {

                    populateCourses();
                    populateSySessions();

                    if (Request.QueryString["SubjectID"] != null)
                    {

                        populateSubjectByID();
                        pnlSubjectEntry.Visible = true;
                        btnSubmit.Text = "Update";
                    }

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


        private void populateSySessions()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SySession from DDESySession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistSySession.Items.Add(dr["SySession"].ToString());


            }

            con.Close();
        }


        private void populateCourses()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[2].ToString() == "")
                {
                    ddlistCourse.Items.Add(dr[1].ToString());
                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }


            }
            con.Close();
        }

        private void populateSubjectByID()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESubject where SubjectID='" + Request.QueryString["SubjectID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ddlistSySession.SelectedItem.Text = dr["SyllabusSession"].ToString();
                ddlistCourse.SelectedItem.Text = dr["CourseName"].ToString();
                ddlistYear.SelectedItem.Text = dr["Year"].ToString();
                tbSNo.Text = dr["SubjectSNo"].ToString();
                tbPaperCode.Text = dr["PaperCode"].ToString();
                tbSubCode.Text = dr["SubjectCode"].ToString();
                tbSubName.Text = dr["SubjectName"].ToString();
            }
                

          
            con.Close();
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["SubjectID"] == null)
            {
                if (!FindInfo.isSubjectExist(ddlistSySession.SelectedItem.Text, ddlistCourse.SelectedItem.Text, tbSubCode.Text))
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDESubject values(@SyllabusSession,@CourseName,@Year,@SubjectSNo,@PaperCode,@SubjectCode,@SubjectName,@NYear,@CourseID)", con);


                    cmd.Parameters.AddWithValue("@SyllabusSession", ddlistSySession.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@CourseName", ddlistCourse.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Year", ddlistYear.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SubjectSNo", tbSNo.Text);
                    cmd.Parameters.AddWithValue("@PaperCode", tbPaperCode.Text);
                    cmd.Parameters.AddWithValue("@SubjectCode", tbSubCode.Text);
                    cmd.Parameters.AddWithValue("@SubjectName", tbSubName.Text);
                    cmd.Parameters.AddWithValue("@NYear",Convert.ToInt32(ddlistYear.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@CourseID", Convert.ToInt32(ddlistCourse.SelectedItem.Value));



                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Log.createLogNow("Create", "Created a subject '" + tbSubName.Text + "' in " + ddlistCourse.SelectedItem.Text, Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "Subject has been created successfully";
                    pnlMSG.Visible = true;
                    btnAddNewSub.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! this subject is already exist.";
                    pnlMSG.Visible = true;
                }
            }

            else if (Request.QueryString["SubjectID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDESubject set SyllabusSession=@SyllabusSession,CourseName=@CourseName,Year=@Year,SubjectSNo=@SubjectSNo,PaperCode=@PaperCode,SubjectCode=@SubjectCode,SubjectName=@SubjectName,NYear=@NYear,CourseID=@CourseID where SubjectID='" + Request.QueryString["SubjectID"] + "' ", con);

                cmd.Parameters.AddWithValue("@SyllabusSession", ddlistSySession.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CourseName", ddlistCourse.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Year", ddlistYear.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@SubjectSNo", tbSNo.Text);
                cmd.Parameters.AddWithValue("@PaperCode", tbPaperCode.Text);
                cmd.Parameters.AddWithValue("@SubjectCode", tbSubCode.Text);
                cmd.Parameters.AddWithValue("@SubjectName", tbSubName.Text);
                cmd.Parameters.AddWithValue("@NYear", Convert.ToInt32(ddlistYear.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@CourseID", Convert.ToInt32(ddlistCourse.SelectedItem.Value));



                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Updated record of subject '" + tbSubName.Text + "' in " + ddlistCourse.SelectedItem.Text, Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Subject has been updated successfully";
                pnlMSG.Visible = true;



            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            tbSNo.Text = "";
            tbSubCode.Text = "";
            tbSubName.Text = "";
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
           
            pnlSubjectEntry.Visible = true;
           
        }

        protected void btnAddNewSub_Click(object sender, EventArgs e)
        {
            tbSNo.Text = "";
            tbSubCode.Text = "";
            tbSubName.Text = "";
            pnlData.Visible = true;
            pnlMSG.Visible = false;
        }

        
    }
}
