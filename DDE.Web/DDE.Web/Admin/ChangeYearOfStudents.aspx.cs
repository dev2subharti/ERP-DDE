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
    public partial class ChangeYearOfStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {
                    populateBatch();
                    populateCourses();

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

        private void populateBatch()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Session from DDESession", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistBatch.Items.Add(dr[0].ToString());

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
                    ddlistCourses.Items.Add(dr[1].ToString());
                    ddlistCourses.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlistCourses.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistCourses.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }


            }
            con.Close();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {
                DropDownList dlcyear = (DropDownList)dli.FindControl("ddlistCYear");
                Label srid = (Label)dli.FindControl("lblSRID");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudentRecord set CYear=@CYear where SRID='" + srid.Text + "'", con);

                cmd.Parameters.AddWithValue("@CYear", dlcyear.SelectedItem.Value);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


            }

            dtlistShowRegistration.Visible = false;
            btnUpdate.Visible = false;
            lblMSG.Text = "Record has been updated successfully";
            pnlMSG.Visible = true;

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateStudentRecord();
            populateCYear();
           
        }

        private void populateCYear()
        {
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {
                DropDownList dlcyear = (DropDownList)dli.FindControl("ddlistCYear");
                Label srid = (Label)dli.FindControl("lblSRID");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select CYear from DDEStudentRecord where SRID='" + srid.Text + "'", con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dlcyear.Items.FindByValue(dr[0].ToString()).Selected = true;
                }

                con.Close();


            }

        }

        private void populateStudentRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (ddlistCourses.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,FatherName,Course from DDEStudentRecord  where Session='" + ddlistBatch.SelectedItem.Value + "' and RecordStatus='True' order by StudentName";
            }
            else
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,FatherName,Course from DDEStudentRecord  where Course='" + ddlistCourses.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Value + "' and RecordStatus='True' order by StudentName";
            }

            SqlDataReader dr;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("StudentPhoto");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("FatherName");
            DataColumn dtcol8 = new DataColumn("Course");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            if (dr.HasRows)
            {
               
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                  
                    drow["SRID"] = Convert.ToString(dr["SRID"]);
                    drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                    drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                    if (dr["EnrollmentNo"].ToString().Length == 10)
                    {
                        drow["EC"] = dr["EnrollmentNo"].ToString().Substring(5, 5);
                    }
                    else if (dr["EnrollmentNo"].ToString().Length == 11)
                    {
                        drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 5);
                    }
                    else if (dr["EnrollmentNo"].ToString().Length == 12)
                    {
                        drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 6);
                    }
                    else if (dr["EnrollmentNo"].ToString().Length == 14)
                    {
                        drow["EC"] = dr["EnrollmentNo"].ToString().Substring(9, 5);
                    }
                    else
                    {
                        drow["EC"] = "";
                    }
                    drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                    drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"])); 
                    dt.Rows.Add(drow);
                   
                }

                dt.DefaultView.Sort = "EC ASC";
                DataView dv = dt.DefaultView;


                int j = 1;
                foreach (DataRowView dvr in dv)
                {
                    dvr[0] = j;
                    j++;
                }

                dtlistShowRegistration.Visible = true;
                btnUpdate.Visible = true;
            }

            else
            {
                dtlistShowRegistration.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
            }

            dtlistShowRegistration.DataSource = dt;
            dtlistShowRegistration.DataBind();
            con.Close();

        }

        protected void ddlistBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowRegistration.Visible = false;
            btnUpdate.Visible = false;
        }

        protected void ddlistCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowRegistration.Visible = false;
            btnUpdate.Visible = false;
        }
    }
}
