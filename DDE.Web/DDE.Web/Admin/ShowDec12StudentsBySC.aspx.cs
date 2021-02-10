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
    public partial class ShowDec12StudentsBySC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 86))
            {
                if (!IsPostBack)
                {
                    populateRecord();

                }

               
            }


            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }






        private void populateRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct DDEMarkSheet_B12.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.StudyCentreCode from DDEMarkSheet_B12 inner join DDEStudentRecord on DDEMarksheet_B12.SRID=DDEStudentRecord.SRID where DDEMarksheet_B12.StudyCentreCode='" + Request.QueryString["SCCode"] + "' order by DDEStudentRecord.EnrollmentNo", con);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("StudentName");
            DataColumn dtcol4 = new DataColumn("FatherName");
            DataColumn dtcol5 = new DataColumn("SCCode");
            DataColumn dtcol6 = new DataColumn("Course");
            DataColumn dtcol7 = new DataColumn("Year");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i + 1;
                    drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                    drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                    drow["FatherName"] = ds.Tables[0].Rows[i]["FatherName"].ToString();
                    drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                    findCourseYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), drow);
                    
                    dt.Rows.Add(drow);

                }

                dtlistRecord.DataSource = dt;
                dtlistRecord.DataBind();

                dtlistRecord.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                dtlistRecord.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }

        }

        private void findCourseYear(int srid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEMarkSheet_B12.SubjectID,DDESubject.CourseName,DDESubject.Year from DDEMarkSheet_B12 inner join DDESubject on DDEMarksheet_B12.SubjectID=DDESubject.SubjectID where DDEMarksheet_B12.SRID='" + srid + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {                   
                    drow["Course"] = ds.Tables[0].Rows[i]["CourseName"].ToString();
                    drow["Year"] = ds.Tables[0].Rows[i]["Year"].ToString().Substring(0,1);
                    break;
                }

             
            }
        }
    }
}