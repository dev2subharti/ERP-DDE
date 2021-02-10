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
    public partial class AwardSheetPrac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 70))
            {
                if (!IsPostBack)
                {
                 
                    lblExamName.Text = "( Examination : " + Session["ExamName"].ToString() + " )";
                    lblSCCode.Text=Session["SCCode"].ToString();
                    populatePracticalDetails();                   
                    setGVHeader();
                    populateAwardSheet();

                }

            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private void setGVHeader()
        {
           int pracmarks=((Convert.ToInt32(lblMMarks.Text)*70)/100);
           int vvmarks = Convert.ToInt32(lblMMarks.Text) - pracmarks;
           gvAwarsSheet.Columns[3].HeaderText = "Practical <br/> (MM " + pracmarks + ")";
           gvAwarsSheet.Columns[4].HeaderText = "Viva-voce <br/> (MM " + vvmarks + ")";
           gvAwarsSheet.Columns[5].HeaderText = "Total <br/> (MM " + Convert.ToInt32(lblMMarks.Text) + ")";
        }

        private void populateAwardSheet()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEExamRecord_" + Session["ExamCode"] + ".SRID,DDEExamRecord_" + Session["ExamCode"] + ".RollNo,DDEExamRecord_" + Session["ExamCode"] + ".Year,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.Course,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode from DDEExamRecord_" + Session["ExamCode"] + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + Session["ExamCode"] + ".SRID where DDEStudentRecord.Course='" + Convert.ToInt32(lblCourseID.Text) + "' and ((DDEExamRecord_" + Session["ExamCode"] + ".Year='" + lblYear.Text.Substring(0, 1) + "') or (DDEExamRecord_" + Session["ExamCode"] + ".BPPracticals" + lblYear.Text.Substring(0, 1) + " like '%" + Convert.ToInt32(Session["PracticalID"]) + "%')) and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + Session["SCCode"].ToString() + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + Session["SCCode"].ToString() + "' )) order by DDEStudentrecord.EnrollmentNo", con);
           
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("RollNo");
          


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

           

            if (ds.Tables[0].Rows.Count > 0)
            {
                string[] detained = FindInfo.findDetainedStudents(Session["ExamCode"].ToString(), "ALL");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                    if (!(pos > -1))
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i + 1;
                        drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                        drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();
                        dt.Rows.Add(drow);
                    }
                }

            }

            gvAwarsSheet.DataSource = dt;
            gvAwarsSheet.DataBind();

            


            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry No Record Found !!";
                pnlMSG.Visible = true;
            }
           
        }

        private void populatePracticalDetails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical where PracticalID='" + Convert.ToInt32(Session["PracticalID"]) + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lblPracticalName.Text = dr["PracticalName"].ToString();
                lblPracticalCode.Text = dr["PracticalCode"].ToString();
                lblCourse.Text = dr["CourseName"].ToString();
                lblCourseID.Text = FindInfo.findCourseIDByCourseName(lblCourse.Text).ToString();
                lblYear.Text = dr["Year"].ToString();
                lblMMarks.Text = dr["PracticalMaxMarks"].ToString();
               
            }

            con.Close();
        }
    }
}