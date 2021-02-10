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
    public partial class ShowOnlineBPApplications : System.Web.UI.Page
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
            SqlCommand cmd = new SqlCommand("Select distinct SCCode from DDEOnlineExamRecord where Examination='" + ddlistExamination.SelectedItem.Value + "' and Enrolled='False' and MOE='B' order by SCCode", con);
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
            SqlCommand cmd = new SqlCommand("Select DDEOnlineExamRecord.OERID,DDEOnlineExamRecord.SRID,DDEOnlineExamRecord.Year,DDEOnlineExamRecord.BPSubjects1,DDEOnlineExamRecord.BPSubjects2,DDEOnlineExamRecord.BPSubjects3,DDEOnlineExamRecord.BPPracticals1,DDEOnlineExamRecord.BPPracticals2,DDEOnlineExamRecord.BPPracticals3,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDECourse.CourseName from DDEOnlineExamRecord inner join DDEStudentRecord on DDEOnlineExamRecord.SRID=DDEStudentRecord.SRID inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEOnlineExamRecord.Enrolled='False' and DDEOnlineExamRecord.SCCode='" + ddlistSCCode.SelectedItem.Text + "' and DDEOnlineExamRecord.Examination='" + ddlistExamination.SelectedItem.Value + "' and DDEOnlineExamRecord.MOE='B' order by DDEStudentRecord.EnrollmentNo", con);


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("OERID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("BP Subjects");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);


            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i + 1;
                    drow["OERID"] = Convert.ToString(ds.Tables[0].Rows[i]["OERID"]);
                    drow["SRID"] = Convert.ToString(ds.Tables[0].Rows[i]["SRID"]);
                    drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                    drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                    drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);

                    if (FindInfo.isMBACourse(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"])))
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 1)
                        {
                            drow["Course"] = "MBA";

                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 2)
                        {
                            drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));

                        }
                        else if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 3)
                        {
                            drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));

                        }
                    }
                    else
                    {
                        drow["Course"] = ds.Tables[0].Rows[i]["CourseName"].ToString();

                    }

                    if ((ds.Tables[0].Rows[i]["BPSubjects1"].ToString() != "") || (ds.Tables[0].Rows[i]["BPPracticals1"].ToString() != ""))
                    {
                        if ((ds.Tables[0].Rows[i]["BPSubjects1"].ToString() != ""))
                        {
                            drow["BP Subjects"] = findSubjects(ds.Tables[0].Rows[i]["BPSubjects1"].ToString());
                        }
                        if ((ds.Tables[0].Rows[i]["BPPracticals1"].ToString() != ""))
                        {
                            drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(ds.Tables[0].Rows[i]["BPPracticals1"].ToString());
                        }

                      
                    }
                    if ((ds.Tables[0].Rows[i]["BPSubjects2"].ToString() != "") || (ds.Tables[0].Rows[i]["BPPracticals2"].ToString() != ""))
                    {
                        if (drow["BP Subjects"].ToString() != "")
                        {
                            if ((ds.Tables[0].Rows[i]["BPSubjects2"].ToString() != ""))
                            {
                                drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findSubjects(ds.Tables[0].Rows[i]["BPSubjects2"].ToString());
                            }
                            if ((ds.Tables[0].Rows[i]["BPPracticals2"].ToString() != ""))
                            {
                                drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(ds.Tables[0].Rows[i]["BPPracticals2"].ToString());
                            }
                        }
                        else
                        {
                            if ((ds.Tables[0].Rows[i]["BPSubjects2"].ToString() != ""))
                            {
                                drow["BP Subjects"] = findSubjects(ds.Tables[0].Rows[i]["BPSubjects2"].ToString());
                            }
                            if ((ds.Tables[0].Rows[i]["BPPracticals2"].ToString() != ""))
                            {
                                drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(ds.Tables[0].Rows[i]["BPPracticals2"].ToString());
                            }
                        }

                       

                    }
                    if ((ds.Tables[0].Rows[i]["BPSubjects3"].ToString() != "") || (ds.Tables[0].Rows[i]["BPPracticals3"].ToString() != ""))
                    {
                        if (drow["BP Subjects"].ToString() != "")
                        {
                            if ((ds.Tables[0].Rows[i]["BPSubjects3"].ToString() != ""))
                            {
                                drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findSubjects(ds.Tables[0].Rows[i]["BPSubjects3"].ToString());
                            }
                            if ((ds.Tables[0].Rows[i]["BPPracticals3"].ToString() != ""))
                            {
                                drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(ds.Tables[0].Rows[i]["BPPracticals3"].ToString());
                            }
                        }
                        else
                        {
                            if ((ds.Tables[0].Rows[i]["BPSubjects3"].ToString() != ""))
                            {
                                drow["BP Subjects"] = findSubjects(ds.Tables[0].Rows[i]["BPSubjects3"].ToString());
                            }
                            if ((ds.Tables[0].Rows[i]["BPPracticals3"].ToString() != ""))
                            {
                                drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(ds.Tables[0].Rows[i]["BPPracticals3"].ToString());
                            }
                        }
                        
                    }

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

        private object findSubjects(string subjects)
        {


            string[] s = { };
            string sub = "";
            s = subjects.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                if (sub == "")
                {
                    sub = FindInfo.findSubjectNameByID(Convert.ToInt32(s[i])) + ",";
                }
                else
                {
                    sub = sub + FindInfo.findSubjectNameByID(Convert.ToInt32(s[i])) + ",";
                }


            }

            return sub;
        }

        private string findPracticals(string prac)
        {
            string[] s = { };
            string sub = "";
            s = prac.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                if (sub == "")
                {
                    sub = FindInfo.findPracticalNameByID(Convert.ToInt32(s[i])) + ",";
                }
                else
                {
                    sub = sub + FindInfo.findPracticalNameByID(Convert.ToInt32(s[i])) + ",";
                }


            }

            return sub;
        }

        protected void dtlistShowPending_ItemCommand(object source, DataListCommandEventArgs e)
        {
            populateStudentRecord(Convert.ToInt32(e.CommandArgument));

            Response.Redirect("FillFee.aspx?OERID=" + e.CommandArgument);
        }

        private void populateStudentRecord(int oerid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEOnlineExamRecord.SRID,DDEOnlineExamRecord.Year,DDEOnlineExamRecord.BPSubjects1,DDEOnlineExamRecord.BPSubjects2,DDEOnlineExamRecord.BPSubjects3,DDEOnlineExamRecord.BPPracticals1,DDEOnlineExamRecord.BPPracticals2,DDEOnlineExamRecord.BPPracticals3,DDEOnlineExamRecord.Examination,DDEStudentRecord.Course from DDEOnlineExamRecord inner join DDEStudentRecord on DDEOnlineExamRecord.SRID=DDEStudentRecord.SRID  where DDEOnlineExamRecord.OERID='" + oerid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();
                Session["OEFHID"] = 3;
                Session["OESRID"] = dr["SRID"].ToString();
                Session["OEYear"] = dr["Year"].ToString();
                Session["OECourseID"] = dr["Course"].ToString();
                Session["OEExam"] = dr["Examination"].ToString();
                Session["OEBPSubjects1"] = dr["BPSubjects1"].ToString();
                Session["OEBPSubjects2"] = dr["BPSubjects2"].ToString();
                Session["OEBPSubjects3"] = dr["BPSubjects3"].ToString();
                Session["OEBPPracticals1"] = dr["BPPracticals1"].ToString();
                Session["OEBPPracticals2"] = dr["BPPracticals2"].ToString();
                Session["OEBPPracticals3"] = dr["BPPracticals3"].ToString();
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