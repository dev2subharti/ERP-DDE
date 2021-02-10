using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class ShowCancelStudents : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("Select SRID,EnrollmentNo,StudentName,FatherName,Course,Session from DDEStudentRecord where RecordStatus='False' and AdmissionStatus='4' order by StudentName", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("Course");
            DataColumn dtcol7 = new DataColumn("Session");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SRID"] = Convert.ToString(dr["SRID"]);
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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set RecordStatus=@RecordStatus,AdmissionStatus=@AdmissionStatus where SRID ='" + Convert.ToString(e.CommandArgument) + "'", con);
            cmd.Parameters.AddWithValue("@RecordStatus", "True");
            cmd.Parameters.AddWithValue("@AdmissionStatus", 1);

            con.Open();
            cmd.ExecuteReader();
            con.Close();
            Log.createLogNow("Re-Admit", "Re-Admit a student with Enrollment No '" + e.CommandName + "'", Convert.ToInt32(Session["ERID"].ToString()));
            populateStudents();


        }
    }
}