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
    public partial class TransferCredits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 37))
            {
                if (!IsPostBack)
                {
                    populateSession();
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

        

        private void populateSession()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Session from DDESession", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistSession.Items.Add(dr[0].ToString());

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

        private void PopulateDStudent()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select EnrollmentNo,SRID,StudentName,FatherName from DDEStudentRecord where Course='" + ddlistCourses.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by EnrollmentNo", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("StudentPhoto");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);



            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);

                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowRegistration.DataSource = dt;
            dtlistShowRegistration.DataBind();

            con.Close();
        }


        protected void btnFind_Click(object sender, EventArgs e)
        {
            PopulateDStudent();

            if (rblPhoto.SelectedItem.Text == "Without Photo")
            {
                foreach (DataListItem dli in dtlistShowRegistration.Items)
                {


                    Image img = (Image)dli.FindControl("imgPhoto");
                    img.Visible = false;



                }
            }

        }

        protected void dtlistShowRegistration_ItemCommand(object source, DataListCommandEventArgs e)
        {

                Session["CCourse"] = ddlistCourses.SelectedItem.Value;
                Response.Redirect("TransferCourse.aspx?SRID=" + Convert.ToString(e.CommandArgument));
            

        }

    }
}
