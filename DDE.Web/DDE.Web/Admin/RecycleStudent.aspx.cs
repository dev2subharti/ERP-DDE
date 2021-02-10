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
using System.Data.SqlClient;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class RecycleStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {
                    populateStudents();




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

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID,PSRID,EnrollmentNo,StudentName,FatherName,Course,Session from DDEStudentRecord where RecordStatus='False' order by StudentName", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("PSRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("Session");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["PSRID"] = Convert.ToString(dr["PSRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["Course"] = Convert.ToString(dr["Course"]);
                drow["Session"] = Convert.ToString(dr["Session"]);

                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowRegistration.DataSource = dt;
            dtlistShowRegistration.DataBind();

            con.Close();
        }

        protected void dtlistShowRegistration_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label psrid = (Label)e.Item.FindControl("lblPSRID");
            Label eno = (Label)e.Item.FindControl("lblENo");

            if (e.CommandName == "Recycle")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudentRecord set RecordStatus=@RecordStatus where SRID ='" + Convert.ToString(e.CommandArgument) + "'", con);
                cmd.Parameters.AddWithValue("@RecordStatus", "True");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Recycle", "Recycle a student with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateStudents();
            }
            else if (e.CommandName == "Delete")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Delete from DDEStudentRecord where SRID ='" + Convert.ToString(e.CommandArgument) + "'", con);
              

                con.Open();
                int i= cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    if (Convert.ToInt32(psrid.Text) > 0)
                    {
                        updateOnlineApplication(Convert.ToInt32(psrid.Text));
                    }
                    Log.createLogNow("Delete", "Deleted a student from Recycle Bin with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    populateStudents();
                }
            }
           
                

        }

        private void updateOnlineApplication(int psrid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEPendingStudentRecord set Enrolled=@Enrolled where PSRID ='" + psrid + "'", con);
            cmd.Parameters.AddWithValue("@Enrolled", "False");

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
