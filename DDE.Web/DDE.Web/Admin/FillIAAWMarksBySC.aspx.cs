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
    public partial class FillIAAWMarksBySC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorisedSCFor(Session["SCID"].ToString(), 66))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("B13").Selected = true;
                    ddlistExam.Enabled = false;
                    PopulateDDList.populateCoursesBySCStreams(ddlistCourse,Convert.ToInt32(Session["SCID"]));
                    PopulateDDList.populateBatch(ddlistSession);

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



        private void populateSubjects()
        {
            ddlistSubject.Items.Add("--SELECT ONE--");
            ddlistSubject.Items.FindByText("--SELECT ONE--").Value = "0";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectID,SubjectName from DDESubject where SyllabusSession='A 2010-11' and CourseName='" + ddlistCourse.SelectedItem.Text + "' and Year='" + ddlistYear.SelectedItem.Text + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistSubject.Items.Add(dr[1].ToString());
                ddlistSubject.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            string error;
            if (validEntry(out error))
            {

                populateStudents();
                setAccessibility();
            }
            else
            {

                pnlData.Visible = false;
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = error;
                pnlMSG.Visible = true;
                btnOK.Visible = true;

                ViewState["ErrorType"] = "Select Panel";
            }

        }

        private void setAccessibility()
        {
           
                foreach (DataListItem dli in dtlistShowStudents.Items)
                {


                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    Label remark = (Label)dli.FindControl("lblRemark");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");

                    if (ia.Text == "")
                    {
                        ia.Enabled = true;

                    }
                    else
                    {
                        ia.Enabled = false;
                    }


                    if (aw.Text == "")
                    {
                        aw.Enabled = true;

                    }
                    else
                    {
                        aw.Enabled = false;
                    }



                    if (remark.Text == "Exam fee paid")
                    {
                        
                        ia.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                        aw.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                        remark.ForeColor = System.Drawing.Color.FromName("#0D9103");
                    }
                    else
                    {
                        ia.Enabled = false;
                        ia.BackColor = System.Drawing.Color.FromName("#F8A403");

                        aw.Enabled = false;
                        aw.BackColor = System.Drawing.Color.FromName("#F8A403");

                        remark.ForeColor = System.Drawing.Color.FromName("#912E03");
                    }


                }

        }

        private void populateMarks(DataRow drow, int srid, int subid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select IA,AW from DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            drow["IAMarks"] = dr[0].ToString();
            drow["AWMarks"] = dr[1].ToString();
            con.Close();

        }

        private bool marksFilled(int srid, int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            bool marksfilled = false;
            if (dr.HasRows)
            {
                marksfilled = true;
            }

            con.Close();

            return marksfilled;

        }



        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

            if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
            {
                if (ddlistYear.SelectedItem.Value == "1")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "2")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "3")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
                }

            }
            else
            {
                cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("EC");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("SCCode");
            DataColumn dtcol7 = new DataColumn("MarksFilled");
            DataColumn dtcol8 = new DataColumn("IAMarks");
            DataColumn dtcol9 = new DataColumn("AWMarks");
            DataColumn dtcol10 = new DataColumn("Remark");

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

            int i = 1;
            while (dr.Read())
            {
                  

                   if (FindInfo.isCourseFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistYear.SelectedItem.Value)))
                   {
                       DataRow drow = dt.NewRow();
                       drow["SNo"] = i;
                       drow["SRID"] = Convert.ToString(dr["SRID"]);
                       drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                       if (dr["EnrollmentNo"].ToString().Length == 10)
                       {
                           drow["EC"] = dr["EnrollmentNo"].ToString().Substring(5, 5);
                       }
                       else if (dr["EnrollmentNo"].ToString().Length == 11)
                       {
                           drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 5);
                       }
                       else if (dr["EnrollmentNo"].ToString().Length == 12)
                       {
                           drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 6);
                       }
                       else if (dr["EnrollmentNo"].ToString().Length == 14)
                       {
                           drow["EC"] = dr["EnrollmentNo"].ToString().Substring(9, 5);
                       }
                       else
                       {
                           drow["EC"] = "";
                       }

                       drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                       drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                       if (marksFilled(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistSubject.SelectedItem.Value)))
                       {
                           drow["MarksFilled"] = "True";
                           populateMarks(drow, Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistSubject.SelectedItem.Value));
                       }

                       else
                       {
                           drow["MarksFilled"] = "False";
                           drow["IAMarks"] = "";
                           drow["AWMarks"] = "";

                       }
                       if (FindInfo.isExamFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
                       {
                           drow["Remark"] = "Exam fee paid";
                       }
                       else
                       {
                           drow["Remark"] = "Exam Fee not paid";
                       }
                       dt.Rows.Add(drow);
                       i = i + 1;
                   }
                  
                
            }

            dt.DefaultView.Sort = "EC ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }

            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            if (i > 1)
            {

                dtlistShowStudents.Visible = true;
                btnSubmit.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

            con.Close();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string error;
            if (validEntry(out error))
            {

                if (validMarks())
                {
                    foreach (DataListItem dli in dtlistShowStudents.Items)
                    {

                        Label srid = (Label)dli.FindControl("lblSRID");
                        Label eno = (Label)dli.FindControl("lblENo");
                        Label sccode = (Label)dli.FindControl("lblSCCode");
                        Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                        TextBox ia = (TextBox)dli.FindControl("tbIA");
                        TextBox aw = (TextBox)dli.FindControl("tbAW");
                        Label lia = (Label)dli.FindControl("lblIA");
                        Label law = (Label)dli.FindControl("lblAW");



                        if (lblmf.Text == "False")
                        {
                            if (ia.Text != "" || aw.Text != "")
                            {
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                                cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));
                                cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ddlistSubject.SelectedItem.Value));
                                cmd.Parameters.AddWithValue("@StudyCentreCode", Session["SCCode"].ToString());
                                cmd.Parameters.AddWithValue("@Theory", "");
                                cmd.Parameters.AddWithValue("@IA", ia.Text);
                                cmd.Parameters.AddWithValue("@AW", aw.Text);
                                cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                Log.createLogNow("Marks Filling By SC", "Filled '" + ia.Text + "' IA & '" + aw.Text + "' AW internal marks of a student with Enrollment No '" + eno.Text + "' of subject '" + ddlistSubject.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["SCCode"].ToString()));
                            }
                        }

                        else if (lblmf.Text == "True")
                        {
                            if (lia.Text != ia.Text || law.Text != aw.Text)
                            {
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("update DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " set IA=@IA,AW=@AW where SRID='" + srid.Text + "' and SubjectID='" + ddlistSubject.SelectedItem.Value + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
                                cmd.Parameters.AddWithValue("@IA", ia.Text);
                                cmd.Parameters.AddWithValue("@AW", aw.Text);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                Log.createLogNow("Marks Updation By SC", "Updated internal marks IA from '" + lia.Text + "' to '" + ia.Text + "' & AW from '" + law.Text + "' to '" + aw.Text + "' of a student with Enrollment No '" + eno.Text + "' of subject '" + ddlistSubject.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["SCCode"].ToString()));
                            }
                        }



                    }

                    dtlistShowStudents.Visible = false;
                    btnSubmit.Visible = false;
                    lblMSG.Text = "Marks have been submitted successfully";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }

                else
                {
                    pnlData.Visible = false;
                    dtlistShowStudents.Visible = false;
                    btnSubmit.Visible = false;
                    lblMSG.Text = "Invalid entry at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> <div align='left' >Please check : </br> 1. Marks should be in numeric form. </br> 2. Marks should be between 0-18.</br>( If you want to fill 19 or 20 marks than please send the copy of this student to us.</br>The marks will be uploaded by us after checking the copy of student.)</div>";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;

                    ViewState["ErrorType"] = "Marks Panel";
                }
            }
            else
            {

                pnlData.Visible = false;
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = error;
                pnlMSG.Visible = true;
                btnOK.Visible = true;

                ViewState["ErrorType"] = "Select Panel";


            }

        }

        private bool validEntry(out string error)
        {
            error = "";
            bool valid = false;

            if (ddlistCourse.SelectedItem.Text != "--SELECT ONE--" && ddlistYear.SelectedItem.Text != "--SELECT ONE--" && ddlistSubject.SelectedItem.Text != "--SELECT ONE--" && ddlistSession.SelectedItem.Text != "--SELECT ONE--")
            {
                valid = true;
            }
            else
            {
                if (ddlistCourse.SelectedItem.Text == "--SELECT ONE--")
                {
                    error = "Please select any Course";
                }

                else if (ddlistYear.SelectedItem.Text == "--SELECT ONE--")
                {
                    error = "Please select any Year";
                }
                else if (ddlistSubject.SelectedItem.Text == "--SELECT ONE--")
                {
                    error = "Please select any Subject";
                }
                else if (ddlistSession.SelectedItem.Text == "--SELECT ONE--")
                {
                    error = "Please select any Batch";
                }

            }

            return valid;
        }

        private bool validMarks()
        {

            bool validmarks = false;
            int SNo = 0;
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                try
                {
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");

                    SNo = Convert.ToInt32(sno.Text);

                    int iamarks;
                    int awmarks;
                    if (ia.Text == "")
                    {
                        iamarks = 0;
                    }
                    else if (ia.Text == "AB")
                    {
                        iamarks = 0;
                    }
                    else
                    {
                        iamarks = Convert.ToInt32(ia.Text);
                    }


                    if (aw.Text == "")
                    {
                        awmarks = 0;
                    }
                    else if (aw.Text == "AB")
                    {
                        awmarks = 0;
                    }
                    else
                    {
                        awmarks = Convert.ToInt32(aw.Text);
                    }


                    if (iamarks >= 0 && iamarks <= 18 && awmarks >= 0 && awmarks <= 18)
                    {
                        validmarks = true;

                    }

                    else
                    {
                        validmarks = false;
                        Session["SNo"] = SNo;
                        break;
                    }
                }

                catch
                {
                    validmarks = false;
                    Session["SNo"] = SNo;
                    break;
                }


            }

            return validmarks;

        }




       

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlistSubject.Items.Clear();
            ddlistSubject.Visible = false;
            lblSubject.Visible = false;

            ddlistSession.Visible = false;
            lblBatch.Visible = false;

            
            setYearList(Convert.ToInt32(ddlistCourse.SelectedItem.Value));     
           

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        private void setYearList(int cid)
        {
            int duration = FindInfo.findCourseDuration(cid);
            ddlistYear.Items.Clear();

            if (duration == 1)
            {
                ddlistYear.Items.Add("--SELECT ONE--");
                ddlistYear.Items.FindByText("--SELECT ONE--").Value = "0";

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

            }
            else if (duration == 2)
            {
                ddlistYear.Items.Add("--SELECT ONE--");
                ddlistYear.Items.FindByText("--SELECT ONE--").Value = "0";

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";
            }
            else if (duration == 3)
            {
                ddlistYear.Items.Add("--SELECT ONE--");
                ddlistYear.Items.FindByText("--SELECT ONE--").Value = "0";

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
        }

        protected void ddlistSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistSession.SelectedItem.Selected = false;
            ddlistSession.Items.FindByText("--SELECT ONE--").Selected = true;

            ddlistSession.Visible = true;
            lblBatch.Visible = true;

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

       

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (ViewState["ErrorType"].ToString() == "Select Panel")
            {
                pnlData.Visible = true;
                btnFind.Visible = true;
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else if (ViewState["ErrorType"].ToString() == "Marks Panel")
            {
                pnlData.Visible = true;
                btnFind.Visible = true;
                dtlistShowStudents.Visible = true;
                btnSubmit.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;

                setColorCode();
            }

           

        }

        private void setColorCode()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {


               
                Label remark = (Label)dli.FindControl("lblRemark");
                TextBox ia = (TextBox)dli.FindControl("tbIA");
                TextBox aw = (TextBox)dli.FindControl("tbAW");

               



                if (remark.Text == "Exam fee paid")
                {

                    ia.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                    aw.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                    remark.ForeColor = System.Drawing.Color.FromName("#0D9103");
                }
                else
                {
                    ia.Enabled = false;
                    ia.BackColor = System.Drawing.Color.FromName("#F8A403");

                    aw.Enabled = false;
                    aw.BackColor = System.Drawing.Color.FromName("#F8A403");

                    remark.ForeColor = System.Drawing.Color.FromName("#912E03");
                }


            }
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlistSession.SelectedItem.Selected = false;
            ddlistSession.Items.FindByText("--SELECT ONE--").Selected = true;

            ddlistSubject.Items.Clear();
            populateSubjects();
            lblSubject.Visible = true;
            ddlistSubject.Visible = true;
                
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

       

        
    }
}
