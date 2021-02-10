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
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;

namespace DDE.Web.Admin
{
    public partial class ReceiveAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 126))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["SRID"] == null)
                    {
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;

                    }
                    else
                    {
                        tbENo.Text = Convert.ToInt32(Request.QueryString["SRID"]).ToString();
                        if (populateStudentInfo(Convert.ToInt32(Request.QueryString["SRID"])))
                        {
                            pnlSearch.Visible = false;
                            pnlData.Visible = true;
                            pnlMSG.Visible = false;

                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! Course or Exam Fee not paid for '" + ddlistExam.SelectedItem.Text + "' Examination";
                            pnlMSG.Visible = true;
                        }


                    }

                }

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private bool populateStudentInfo(int srid)
        {
            bool valid = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select DDEExamRecord_W11.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.Session,DDEStudentRecord.Course,DDEExamRecord_W11.Year,DDEExamRecord_W11.RollNo from DDEExamRecord_W11 inner join DDEStudentRecord on DDEExamRecord_W11.SRID=DDEStudentRecord.SRID where DDEExamRecord_W11.SRID='" + srid + "' and DDEStudentRecord.StudyCentreCode='" + Session["SCCode"].ToString() + "'";

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                valid = true;
                dr.Read();

                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + dr["SRID"].ToString();

                lblSRID.Text = dr["SRID"].ToString();
                tbEnrollmentNo.Text = dr["EnrollmentNo"].ToString();
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();

                tbBatch.Text = dr["Session"].ToString();
                tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                lblCID.Text = dr["Course"].ToString();
                lblCD.Text = FindInfo.findCourseDuration(Convert.ToInt32(lblCID.Text)).ToString();

                tbYear.Text = FindInfo.findAlphaYear(Convert.ToString(dr["CYear"])).ToUpper();
                lblYear.Text = Convert.ToInt32(dr["Year"]).ToString();
               
                if (FindInfo.isAssignmentUploaded(srid, ddlistExam.SelectedItem.Value))
                {
                    lblDocUploaded.Text = "YES";
                    lblEMsg.Text = "Assignments are already submitted !!";
                    lblEMsg.Visible = true;

                    btnUpload1.Visible = false;
                }
                else
                {
                    lblEMsg.Visible = false;
                    lblDocUploaded.Text = "NO";
                    btnUpload1.Visible = true;
                }

                pnlAssDetails.Visible = true;


            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry ! No Record Found";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                lblET.Text = "Search";

            }

            con.Close();

            return valid;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (tbENo.Text != "")
            {
                int srid = FindInfo.findSRIDByENo(tbENo.Text);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                cmd.CommandText = "select DDEExamRecord_W11.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.Session,DDEStudentRecord.Course,DDEExamRecord_W11.Year,DDEExamRecord_W11.RollNo from DDEExamRecord_W11 inner join DDEStudentRecord on DDEExamRecord_W11.SRID=DDEStudentRecord.SRID where DDEExamRecord_W11.SRID='" + srid + "'";

                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();

                    imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + dr["SRID"].ToString();

                    lblSRID.Text = dr["SRID"].ToString();
                    tbEnrollmentNo.Text = dr["EnrollmentNo"].ToString();
                    tbSName.Text = dr["StudentName"].ToString();
                    tbFName.Text = dr["FatherName"].ToString();

                    tbBatch.Text = dr["Session"].ToString();
                    tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                    lblCID.Text = dr["Course"].ToString();
                    lblCD.Text = FindInfo.findCourseDuration(Convert.ToInt32(lblCID.Text)).ToString();
                    tbYear.Text = FindInfo.findAlphaYear(Convert.ToString(dr["Year"])).ToUpper();

                   
                    lblYear.Text = Convert.ToInt32(dr["Year"]).ToString();
                    pnlAssDetails.Visible = true;
                    pnlSearch.Visible = false;
                    btnUDAnother.Visible = true;

                    if (FindInfo.isAssignmentUploaded(srid, ddlistExam.SelectedItem.Value))
                    {
                        lblDocUploaded.Text = "YES";
                        lblEMsg.Text = "Assignments are already submitted !!";
                        lblEMsg.Visible = true;

                        btnUpload1.Visible = false;
                    }
                    else
                    {
                        lblEMsg.Visible = false;
                        lblDocUploaded.Text = "NO";
                        btnUpload1.Visible = true;
                    }

                    pnlAssDetails.Visible = true;

                }
                else
                {

                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Course or Exam Fee not paid for '" + ddlistExam.SelectedItem.Text + "' Examination";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                    lblET.Text = "Search";
                }

                con.Close();



            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry!! Yod did not fill any Enrollment No </br> Please fill any valid OANo first";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                lblET.Text = "Search";
            }

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (lblET.Text == "Search")
            {
                pnlSearch.Visible = true;
                pnlAssDetails.Visible = false;

            }
            else
            {
                pnlSearch.Visible = false;
                pnlAssDetails.Visible = true;

            }


            btnUDAnother.Visible = false;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }

        private int insertFileRecord(int srid, string fileName, string fileExt, double filesize)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDEStudentAssignments OUTPUT INSERTED.AssID values(@SRID,@FileName,@FileType,@FileSize,@TOE)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@FileName", fileName);
            cmd.Parameters.AddWithValue("@FileType", fileExt);
            cmd.Parameters.AddWithValue("@FileSize", filesize);
            cmd.Parameters.AddWithValue("@TOE", DateTime.Now.ToString());

            cmd.Connection = con;
            con.Open();
            object assid = cmd.ExecuteScalar();
            con.Close();

            return Convert.ToInt32(assid);
        }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            try
            {
                  int docid = insertFileRecord(Convert.ToInt32(lblSRID.Text), "Assignment.pdf", ".pdf", 0);
                  if (docid > 0)
                  {
                    if (Convert.ToInt32(lblYear.Text) < Convert.ToInt32(lblCD.Text))
                    {
                        updateTHMarks(Convert.ToInt32(lblSRID.Text));
                        updatePracMarks(Convert.ToInt32(lblSRID.Text));
                    }
                    else if (Convert.ToInt32(lblYear.Text) == Convert.ToInt32(lblCD.Text))
                    {
                        updatePracMarks(Convert.ToInt32(lblSRID.Text));
                    }

                    lblMSG.Text = "Assignments have been uploaded successfully!!";
                    btnUDAnother.Visible = true;
                    pnlAssDetails.Visible = false;
                    pnlMSG.Visible = true;

                  }

            }
            catch (Exception Ex)
            {

                lblMSG.Text = Ex.Message;
                pnlData.Visible = false;
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                lblET.Text = "Assignment";

            }
        }

        private int updatePracMarks(int srid)
        {
            int counter = 0;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();


            cmd1.CommandText = findPracCommand(srid);

            cmd1.Connection = con1;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (!(isPracEntryExist(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalID"]))))
                {
                    Random rd = new Random();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEPracticalMarks_W11 values(@SRID,@PracticalID,@StudyCentreCode,@PracticalMarks,@MOE)", con);

                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                    cmd.Parameters.AddWithValue("@PracticalID", Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalID"]));
                    cmd.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                    int minpm = (Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalMaxMarks"]) * 60) / 100;
                    int maxpm = (Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalMaxMarks"]) * 75) / 100;
                    cmd.Parameters.AddWithValue("@PracticalMarks", rd.Next(minpm, maxpm));
                    cmd.Parameters.AddWithValue("@MOE", "R");

                    cmd.Connection = con;
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    counter = counter + j;

                }

            }



            return counter;
        }

        private bool isPracEntryExist(int srid, int pracid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select SRID from DDEPracticalMarks_W11 where SRID ='" + srid + "' and PracticalID='" + pracid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        private string findPracCommand(int srid)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDEPractical.PracticalID");
            sb.Append(",DDEPractical.PracticalName");
            sb.Append(",DDEPractical.PracticalMaxMarks");

            sb.Append(" FROM DDEExamRecord_W11");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDEPractical on DDEStudentRecord.Course=DDEPractical.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course!='76' and ([DDEExamRecord_W11].Year= DDEPractical.NYear)");

            sb.Append(" and (DDEStudentRecord.SyllabusSession= DDEPractical.SyllabusSession)");

            sb.Append(" union");

            sb.Append(" SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDEPractical.PracticalID");
            sb.Append(",DDEPractical.PracticalName");
            sb.Append(",DDEPractical.PracticalMaxMarks");

            sb.Append(" FROM DDEExamRecord_W11");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDEPractical on DDEStudentRecord.Course2Year=DDEPractical.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course= '76' and DDEStudentRecord.CYear= '2' and ([DDEExamRecord_W11].Year= DDEPractical.NYear)");

            sb.Append(" and (DDEStudentRecord.SyllabusSession= DDEPractical.SyllabusSession)");

            sb.Append(" order by SRID,PracticalID");


            return sb.ToString();
        }

        private int updateTHMarks(int srid)
        {
            int counter = 0;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();

            cmd1.CommandText = findTHCommand(srid);


            cmd1.Connection = con1;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string thmarks;

                if (isTHEntryExist(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"]), out thmarks))
                {
                    if (thmarks == "")
                    {
                        Random rd = new Random();
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W11 set Theory=@Theory where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) + "' and SubjectID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"]) + "'", con2);

                        cmd2.Parameters.AddWithValue("@Theory", rd.Next(55, 70));

                        con2.Open();
                        int j = cmd2.ExecuteNonQuery();
                        con2.Close();
                        counter = counter + j;
                    }

                }
                else
                {
                    Random rd = new Random();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_W11 values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                    cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"]));
                    cmd.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                    cmd.Parameters.AddWithValue("@Theory", rd.Next(55, 70));
                    cmd.Parameters.AddWithValue("@IA", "");
                    cmd.Parameters.AddWithValue("@AW", "");
                    cmd.Parameters.AddWithValue("@MOE", "R");

                    cmd.Connection = con;
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    counter = counter + j;

                }

            }

            return counter;
        }

        private bool isTHEntryExist(int srid, int subjectid, out string thmarks)
        {
            bool exist = false;
            thmarks = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select RID,Theory from DDEMarkSheet_W11 where SRID ='" + srid + "' and SubjectID='" + subjectid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                thmarks = dr["Theory"].ToString();
                exist = true;
            }
            con.Close();

            return exist;

        }

        private string findTHCommand(int srid)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDESubject.SubjectID");
            sb.Append(",DDESubject.SubjectName");

            sb.Append(" FROM[dbo].[DDEExamRecord_W11]");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDESubject on DDEStudentRecord.Course=DDESubject.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "'  and DDEStudentRecord.Course!='76' and ([DDEExamRecord_W11].Year= DDESubject.NYear)");

            sb.Append(" and(DDEStudentRecord.SyllabusSession= DDESubject.SyllabusSession)");

            sb.Append(" union");

            sb.Append(" SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDESubject.SubjectID");
            sb.Append(",DDESubject.SubjectName");

            sb.Append(" FROM[dbo].[DDEExamRecord_W11]");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDESubject on DDEStudentRecord.Course=DDESubject.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course= '76' and [DDEExamRecord_W11].Year= '1' and ([DDEExamRecord_W11].Year= DDESubject.NYear)");

            sb.Append(" and(DDEStudentRecord.SyllabusSession= DDESubject.SyllabusSession)");

            sb.Append(" union");

            sb.Append(" SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDESubject.SubjectID");
            sb.Append(",DDESubject.SubjectName");

            sb.Append(" FROM[dbo].[DDEExamRecord_W11]");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDESubject on DDEStudentRecord.Course2Year=DDESubject.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course= '76' and [DDEExamRecord_W11].Year= '2' and ([DDEExamRecord_W11].Year= DDESubject.NYear)");

            sb.Append(" and(DDEStudentRecord.SyllabusSession= DDESubject.SyllabusSession)");

            sb.Append(" order by SRID,SubjectID");


            return sb.ToString();

        }

        private int deleteDocID(int docid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("delete from DDEStudentAssignments where AssID ='" + docid + "'", con);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            return i;
        }

        protected void btnUDAnother_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReceiveAssignment.aspx");
        }
    }
}