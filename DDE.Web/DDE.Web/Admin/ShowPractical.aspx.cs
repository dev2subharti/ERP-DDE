using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class ShowPractical : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 32))
            {

                if (!IsPostBack)
                {

                    populateCourses();
                    populateSySessions();

                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 33))
            {

                if (!IsPostBack)
                {

                    populateCourses();
                    populateSySessions();
                    setAccessbility();
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

        private void setAccessbility()
        {
            foreach (DataListItem dli in dtlistShowPracticals.Items)
            {


                LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");


                edit.Visible = false;



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

        protected void dtlistShowPracticals_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                Response.Redirect("AddPractical.aspx?PracticalID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEPractical where PracticalID ='" + Convert.ToString(e.CommandArgument) + "'", con);
                Log.createLogNow("Delete", "Delete a practical '" + FindInfo.findPracticalDetailByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));

                con.Open();
                cmd.ExecuteReader();
                con.Close();
                

                populatePracticals();


            }

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populatePracticals();
           
        }

       


        private void populatePracticals()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEPractical where SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' and CourseName='" + ddlistCourse.SelectedItem.Text + "' and Year='" + ddlistYear.Text + "' order by PracticalSNo ", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("PracticalID");
            DataColumn dtcol3 = new DataColumn("PracticalSNo");
            DataColumn dtcol4 = new DataColumn("PracticalName");
            DataColumn dtcol5 = new DataColumn("PracticalMaxMarks");
            DataColumn dtcol6 = new DataColumn("AllowedForAS");
           


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
                drow["PracticalID"] = Convert.ToString(dr["PracticalID"]);
                drow["PracticalSNo"] = Convert.ToString(dr["PracticalSNo"]);
                drow["PracticalName"] = Convert.ToString(dr["PracticalName"]);
                drow["PracticalMaxMarks"] = Convert.ToString(dr["PracticalMaxMarks"]);

                if (dr["AllowedForAS"].ToString() == "True")
                {
                    drow["AllowedForAS"] = "Yes";
                }
                else if (dr["AllowedForAS"].ToString() == "False")
                {
                    drow["AllowedForAS"] = "No";
                }

                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowPracticals.DataSource = dt;
            dtlistShowPracticals.DataBind();

            con.Close();

            if (i > 1)
            {
                dtlistShowPracticals.Visible = true;
              
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowPracticals.Visible =false;
           
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
            }

        }

        

    }
}
