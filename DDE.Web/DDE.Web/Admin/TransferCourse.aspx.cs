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
    public partial class TransferCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 37))
            {

                if (!IsPostBack)
                {

                    populateCourses();
                    setCurrentStudentCourse();
            

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

        private void setCurrentStudentCourse()
        {
            ddlistCurCourse.Items.FindByValue(Session["CCourse"].ToString()).Selected = true;
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
                    ddlistPreCourse.Items.Add(dr[1].ToString());
                    ddlistPreCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                    ddlistCurCourse.Items.Add(dr[1].ToString());
                    ddlistCurCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlistPreCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistPreCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                    ddlistCurCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistCurCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }


            }
            con.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!FindInfo.alreadyExistInTable(Request.QueryString["SRID"], "DDECreditTransfers"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDECreditTransfers values(@SRID,@PreInstitution,@PreCourseID,@CurrentCourseID)", con);


                cmd.Parameters.AddWithValue("@SRID", Request.QueryString["SRID"]);
                cmd.Parameters.AddWithValue("@PreInstitution", ddlistPInst.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@PreCourseID", ddlistPreCourse.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@CurrentCourseID", ddlistCurCourse.SelectedItem.Value);


                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                FindInfo.changeCourseBySRID(Convert.ToInt32(Request.QueryString["SRID"]), ddlistCurCourse.SelectedItem.Value);
                Log.createLogNow("Update", "Tranfer a student with Enrollment No '" + FindInfo.findENoByID(Convert.ToInt32(Request.QueryString["SRID"])) + "' from " + ddlistPreCourse.SelectedItem.Text + " to " + ddlistCurCourse.SelectedItem.Text, Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Data has been submitted successfully";
                pnlMSG.Visible = true;
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! credits already has been transfered of this student";
                pnlMSG.Visible = true;
            }


           
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
