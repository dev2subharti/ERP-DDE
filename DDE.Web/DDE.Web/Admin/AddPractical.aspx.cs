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
    public partial class AddPractical : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 32))
            {
                if (!IsPostBack)
                {

                    populateCourses();
                    populateSySessions();

                    if (Request.QueryString["PracticalID"] != null)
                    {

                        populatePracticalByID();
                        pnlPracticalEntry.Visible = true;
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

        private void populatePracticalByID()
         {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical where PracticalID='" + Request.QueryString["PracticalID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistSySession.SelectedItem.Text = dr["SyllabusSession"].ToString();
                ddlistCourse.SelectedItem.Text = dr["CourseName"].ToString();
                ddlistYear.SelectedItem.Text = dr["Year"].ToString();
                tbSNo.Text = dr["PracticalSNo"].ToString();
                tbPracName.Text = dr["PracticalName"].ToString();
                tbPracCode.Text = dr["PracticalCode"].ToString();
                tbPracMaxMarks.Text = dr["PracticalMaxMarks"].ToString();
                ddlistAllowAS.Items.FindByValue(dr["AllowedForAS"].ToString()).Selected = true;

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["PracticalID"] == null)
            {
                if (!FindInfo.isPracticalExist(ddlistSySession.SelectedItem.Text, ddlistCourse.SelectedItem.Text, tbPracCode.Text))
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEPractical values(@PType,@SyllabusSession,@CourseName,@Year,@PracticalSNo,@PracticalCode,@PracticalName,@PracticalMaxMarks,@MUAllowedForSC,@AllowedForAS,@NYear,@CourseID)", con);

                    cmd.Parameters.AddWithValue("@PType", ddlistPType.SelectedItem.Value);                  
                    cmd.Parameters.AddWithValue("@SyllabusSession", ddlistSySession.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@CourseName", ddlistCourse.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Year", ddlistYear.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@PracticalSNo", tbSNo.Text);
                    cmd.Parameters.AddWithValue("@PracticalCode", tbPracCode.Text);
                    cmd.Parameters.AddWithValue("@PracticalName", tbPracName.Text);
                    cmd.Parameters.AddWithValue("@PracticalMaxMarks", tbPracMaxMarks.Text);
                    cmd.Parameters.AddWithValue("@MUAllowedForSC", "False");
                    cmd.Parameters.AddWithValue("@AllowedForAS", ddlistAllowAS.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@NYear", Convert.ToInt32(ddlistYear.SelectedItem.Value));
                    cmd.Parameters.AddWithValue("@CourseID", Convert.ToInt32(ddlistCourse.SelectedItem.Value));


                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Log.createLogNow("Create", "Created a practical '" + tbPracName.Text + "' in " + ddlistCourse.SelectedItem.Text, Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "Practical has been created successfully";
                    pnlMSG.Visible = true;
                    btnAddNewPrac.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! This practical is already exist.";
                    pnlMSG.Visible = true;
                }
            }

            else if (Request.QueryString["PracticalID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEPractical set PType=@PType,SyllabusSession=@SyllabusSession,CourseName=@CourseName,Year=@Year,PracticalSNo=@PracticalSNo,PracticalCode=@PracticalCode,PracticalName=@PracticalName,PracticalMaxMarks=@PracticalMaxMarks,AllowedForAS=@AllowedForAS,NYear=@NYear,CourseID=@CourseID) where PracticalID='" + Request.QueryString["PracticalID"] + "' ", con);

                cmd.Parameters.AddWithValue("@PType", ddlistPType.SelectedItem.Value);  
                cmd.Parameters.AddWithValue("@SyllabusSession", ddlistSySession.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@CourseName", ddlistCourse.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Year", ddlistYear.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@PracticalSNo", tbSNo.Text);
                cmd.Parameters.AddWithValue("@PracticalCode", tbPracCode.Text);
                cmd.Parameters.AddWithValue("@PracticalName", tbPracName.Text);      
                cmd.Parameters.AddWithValue("@PracticalMaxMarks", tbPracMaxMarks.Text);
                cmd.Parameters.AddWithValue("@AllowedForAS", ddlistAllowAS.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@NYear", Convert.ToInt32(ddlistYear.SelectedItem.Value));
                cmd.Parameters.AddWithValue("@CourseID", Convert.ToInt32(ddlistCourse.SelectedItem.Value));



                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Updated record of practical '" + tbPracName.Text + "' in " + ddlistCourse.SelectedItem.Text, Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Practical has been updated successfully";
                pnlMSG.Visible = true;



            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            tbSNo.Text = "";
            tbPracName.Text = "";
            tbPracMaxMarks.Text = "";
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {

            pnlPracticalEntry.Visible = true;

        }

        protected void btnAddNewSub_Click(object sender, EventArgs e)
        {
            tbSNo.Text = "";
            tbPracName.Text = "";
            tbPracMaxMarks.Text = "";
            pnlData.Visible = true;
            pnlMSG.Visible = false;
        }


        
    }
}
