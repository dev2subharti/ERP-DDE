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
    public partial class SetSpecialisationByList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {

                if (!IsPostBack)
                {
                    
                    PopulateDDList.populateMBACourses(ddlistCourse);            
                    PopulateDDList.populateBatch(ddlistSession);
               
                   
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateStudents();
            populateCourses();
            setCourses();
        }

        private void setCourses()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                Label srid = (Label)dli.FindControl("lblSRID");
                DropDownList y1 = (DropDownList)dli.FindControl("ddlist1Year");
                DropDownList y2 = (DropDownList)dli.FindControl("ddlist2Year");
                DropDownList y3 = (DropDownList)dli.FindControl("ddlist3Year");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select Course,Course2Year,Course3Year from DDEStudentRecord where SRID='"+srid.Text+"'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                dr.Read();

                if (dr[0].ToString() == "")
                {
                    y1.Items.FindByValue(dr[0].ToString()).Selected = true;
                }

                else
                {
                    y1.Items.FindByValue(dr[0].ToString()).Selected = true;
                }
                if (dr[1].ToString() == "")
                {
                    y2.Items.FindByValue(dr[0].ToString()).Selected = true;
                }
                else
                {
                    y2.Items.FindByValue(dr[1].ToString()).Selected = true;
                }
                if (dr[2].ToString() == "")
                {
                    y3.Items.FindByValue(dr[0].ToString()).Selected = true;
                }
                else
                {
                    y3.Items.FindByValue(dr[2].ToString()).Selected = true;
                }
            
                con.Close();

            }
        }

        private void populateCourses()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                DropDownList y1 = (DropDownList)dli.FindControl("ddlist1Year");
                DropDownList y2 = (DropDownList)dli.FindControl("ddlist2Year");
                DropDownList y3 = (DropDownList)dli.FindControl("ddlist3Year");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse where CourseShortName='MBA' order by CourseShortName", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[2].ToString() == "")
                    {
                        y1.Items.Add(dr[1].ToString());
                        y1.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                        y2.Items.Add(dr[1].ToString());
                        y2.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                        y3.Items.Add(dr[1].ToString());
                        y3.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                    }

                    else
                    {
                        y1.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                        y1.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                        y2.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                        y2.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                        y3.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                        y3.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                    }


                }
                con.Close();
              
            }
        }

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

            if (ddlistYear.SelectedItem.Value == "1")
            {
               cmd.CommandText ="select SRID,EnrollmentNo,StudentName,FatherName from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True'";
            }
            else if (ddlistYear.SelectedItem.Value == "2")
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,FatherName from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True'";
            }
            else if (ddlistYear.SelectedItem.Value == "3")
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,FatherName from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True'";
            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("EC"); 
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);

           
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
               
                drow["SRID"] = Convert.ToString(dr["SRID"]);
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


            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            con.Close();

            if (j > 1)
            {

                dtlistShowStudents.Visible = true;
                btnUpdate.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                Label srid = (Label)dli.FindControl("lblSRID");
                Label eno = (Label)dli.FindControl("lblENo");
                DropDownList y1 = (DropDownList)dli.FindControl("ddlist1Year");
                DropDownList y2 = (DropDownList)dli.FindControl("ddlist2Year");
                DropDownList y3 = (DropDownList)dli.FindControl("ddlist3Year");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
             
                SqlCommand cmd = new SqlCommand("update DDEStudentRecord set Course=@Course,Course2Year=@Course2Year,Course3Year=@Course3Year where SRID='"+srid.Text+"'", con);
                

                cmd.Parameters.AddWithValue("@Course", 76);
                cmd.Parameters.AddWithValue("@Course2Year", y2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Course3Year", y3.SelectedItem.Value);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Set Specialisation", "Specialisation was set with 1 Y-'MBA',2 Y-'" + y2.SelectedItem.Text + "',3 Y-'" + y3.SelectedItem.Text + "' with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;

                lblMSG.Text = "Record has been updated successfullly !!";
                pnlMSG.Visible = true;

            }
        }
    }
}
