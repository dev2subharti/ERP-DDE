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
    public partial class ShowOnlineCont : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 38))
            {
                if (!IsPostBack)
                {
                   
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;

                }


            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private void populateSCCodes()
        {
            ddlistSCCode.Items.Clear();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct SCCode from DDEOnlineContinuationRecord where ForExam='"+ddlistExamination.SelectedItem.Value+"' and Enrolled='False' order by SCCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0].ToString() != "")
                {
                    ddlistSCCode.Items.Add(dr[0].ToString());
                }

            }
            con.Close();

            if (ddlistSCCode.Items.Count > 0)
            {
                ddlistSCCode.Enabled = true;
            }
            else
            {
                ddlistSCCode.Enabled = false;
            }
        }

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEOnlineContinuationRecord.OURID,DDEOnlineContinuationRecord.SRID,DDEOnlineContinuationRecord.ForYear,DDEOnlineContinuationRecord.IsMBACourse,DDEOnlineContinuationRecord.MBASpecialization,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDECourse.CourseName from DDEOnlineContinuationRecord inner join DDEStudentRecord on DDEOnlineContinuationRecord.SRID=DDEStudentRecord.SRID inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEOnlineContinuationRecord.Enrolled='False' and DDEOnlineContinuationRecord.SCCode='" + ddlistSCCode.SelectedItem.Text + "' and DDEOnlineContinuationRecord.ForExam='"+ddlistExamination.SelectedItem.Value+"' order by DDEStudentRecord.EnrollmentNo", con);
           

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("OURID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("IsMBACourse");
            DataColumn dtcol8 = new DataColumn("MBASpecialization");
            DataColumn dtcol9 = new DataColumn("Course");
            DataColumn dtcol10 = new DataColumn("Year");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i+1;
                    drow["OURID"] = Convert.ToString(ds.Tables[0].Rows[i]["OURID"]);
                    drow["SRID"] = Convert.ToString(ds.Tables[0].Rows[i]["SRID"]);
                    drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                    drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                    drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);

                    if (Convert.ToString(ds.Tables[0].Rows[i]["IsMBACourse"]) == "True")
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["MBASpecialization"]));
                    }
                    else
                    {
                        drow["Course"] = Convert.ToString(ds.Tables[0].Rows[i]["CourseName"]);
                    }
                    drow["Year"] = Convert.ToString(ds.Tables[0].Rows[i]["ForYear"]);

                    dt.Rows.Add(drow);
                }
            }

            dtlistShowPending.DataSource = dt;
            dtlistShowPending.DataBind();

            con.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowPending.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowPending.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowPending_ItemCommand(object source, DataListCommandEventArgs e)
        {
            populateStudentRecord(Convert.ToInt32(e.CommandArgument));
            
            Response.Redirect("FillFee.aspx?OURID=" + e.CommandArgument);
        }

        private void populateStudentRecord(int ourid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEOnlineContinuationRecord.OURID,DDEOnlineContinuationRecord.SRID,DDEOnlineContinuationRecord.ForYear,DDEOnlineContinuationRecord.ForExam,DDEOnlineContinuationRecord.IsMBACourse,DDEOnlineContinuationRecord.MBASpecialization,DDEStudentRecord.Course from DDEOnlineContinuationRecord inner join DDEStudentRecord on DDEOnlineContinuationRecord.SRID=DDEStudentRecord.SRID  where DDEOnlineContinuationRecord.OURID='" + ourid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();
                Session["OUSRID"] = dr["SRID"].ToString();
                Session["OUYear"] = dr["ForYear"].ToString();
                Session["OUCourseID"] = dr["Course"].ToString();
                Session["OUIsMBACourse"] = dr["IsMBACourse"].ToString();
                Session["OUMBASpecialization"] = dr["MBASpecialization"].ToString();
                Session["OUExam"] = dr["ForExam"].ToString();
                
            }
           

            con.Close();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
                        
            populateStudents();
      
        }

        protected void ddlistExamination_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateSCCodes();
            dtlistShowPending.Visible = false;
        }

       
    }
}