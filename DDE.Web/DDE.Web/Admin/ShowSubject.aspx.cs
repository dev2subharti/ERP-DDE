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
    public partial class ShowSubject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 35))
            {
                if (!IsPostBack)
                {

                    populateCourses();
                    populateSySessions();

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
            bool edit = false;
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 34))
            {
                edit = true;
            }
           
            foreach (DataListItem dli in dtlistShowSubjects.Items)
            {

                LinkButton lbedit = (LinkButton)dli.FindControl("lnkbtnEdit");
                //TextBox tbpc = (TextBox)dli.FindControl("tbPaperCode");
                Label lblsubid = (Label)dli.FindControl("lblSubjectID");

                lbedit.Visible = edit;

                //if (tbpc.Text != "")
                //{
                //    tbpc.Enabled = false;
                //}

            }
        }

        private void populateSySessions()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SySession from DDESySession", con);
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

        private void populateSubjects()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESubject where SyllabusSession='"+ddlistSySession.SelectedItem.Text+"' and CourseName='"+ddlistCourse.SelectedItem.Text+"' and Year='"+ddlistYear.Text+"' order by SubjectSNo ", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SubjectID");
            DataColumn dtcol3 = new DataColumn("PaperCode");
            DataColumn dtcol4 = new DataColumn("SubjectSNo");
            DataColumn dtcol5 = new DataColumn("SubjectCode");
            DataColumn dtcol6 = new DataColumn("SubjectName");


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
                drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                drow["PaperCode"] = Convert.ToString(dr["PaperCode"]);
                drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);

                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowSubjects.DataSource = dt;
            dtlistShowSubjects.DataBind();

            con.Close();

            if (i > 1)
            {
                dtlistShowSubjects.Visible = true;              
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowSubjects.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }
           
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateSubjects();
            setAccessbility();
           
        }

        

        protected void dtlistShowSubjects_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "Edit")
            {

                Response.Redirect("AddSubject.aspx?SubjectID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDESubject where SubjectID ='" + Convert.ToString(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Delete", "Delete a subject '" + FindInfo.findSubjectDetailByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));

                populateSubjects();


            }

        }

        
    }
}
