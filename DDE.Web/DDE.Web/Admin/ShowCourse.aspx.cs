using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class ShowCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 8))
            {

                if (!IsPostBack)
                {

                    populateCourses();


                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 9))
            {

                if (!IsPostBack)
                {

                    populateCourses();
                    setAccessibility();

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

        private void setAccessibility()
        {
            foreach (DataListItem dli in dtlistShowCourses.Items)
            {


                LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");
               

                edit.Visible = false;
             


            }
        }

        private void populateCourses()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDECourse order by CourseShortName", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("CourseID");
            DataColumn dtcol3 = new DataColumn("CourseCode");
            DataColumn dtcol4 = new DataColumn("CourseShortName");
            DataColumn dtcol5=  new DataColumn("Specialization");
            DataColumn dtcol6 = new DataColumn("ProgramCode");
           

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
                drow["CourseID"] = Convert.ToString(dr["CourseID"]);
                drow["CourseCode"] = Convert.ToString(dr["CourseCode"]);
                drow["CourseShortName"] = Convert.ToString(dr["CourseShortName"]);    
                drow["Specialization"] = Convert.ToString(dr["Specialization"]);
                drow["ProgramCode"] = Convert.ToString(dr["ProgramCode"]);
               
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowCourses.DataSource = dt;
            dtlistShowCourses.DataBind();

            con.Close();
        }

        protected void dtlistShowCourses_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                Response.Redirect("AddCourse.aspx?CourseID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDECourse where CourseID ='" + Convert.ToString(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Delete", "Delete a course '" + FindInfo.findCourseNameByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
              
                populateCourses();


            }
        }

       
    }
}
