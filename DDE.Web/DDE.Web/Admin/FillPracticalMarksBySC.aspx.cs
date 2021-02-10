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
    public partial class FillPracticalMarksBySC : System.Web.UI.Page
    {
        int maxpracmarks = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorisedSCFor(Session["SCID"].ToString(), 66))
            {
                if (!IsPostBack)
                {
                    if (!IsPostBack)
                    {
                        PopulateDDList.populateExam(ddlistExam);
                        ddlistExam.Items.FindByValue("B13").Selected = true;
                        ddlistExam.Enabled = false;
                        PopulateDDList.populateCoursesBySCStreams(ddlistCourse, Convert.ToInt32(Session["SCID"]));
                        PopulateDDList.populateBatch(ddlistSession);

                    }

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




        private void populatePractical()
        {
            ddlistPractical.Items.Add("--SELECT ONE--");
            ddlistPractical.Items.FindByText("--SELECT ONE--").Value = "0";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalID,PracticalName from DDEPractical where SyllabusSession='A 2010-11' and CourseName='" + ddlistCourse.SelectedItem.Text + "' and Year='" + ddlistYear.SelectedItem.Text + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (FindInfo.muToSCAllowed(Convert.ToInt32(dr[0])))
                {

                    ddlistPractical.Items.Add(dr[1].ToString());
                    ddlistPractical.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }


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
                    TextBox prac = (TextBox)dli.FindControl("tbPracticalMarks");


                    if (prac.Text == "")
                    {
                        prac.Enabled = true;
                    }

                    else
                    {
                        prac.Enabled = false;
                    }

                    if (remark.Text == "Exam fee paid")
                    {

                        prac.BackColor = System.Drawing.Color.FromName("#FFFFFF");
                       
                        remark.ForeColor = System.Drawing.Color.FromName("#0D9103");
                    }
                    else
                    {
                        prac.Enabled = false;
                        prac.BackColor = System.Drawing.Color.FromName("#F8A403");

                        remark.ForeColor = System.Drawing.Color.FromName("#912E03");
                    }


                }

        }

        private void findMaxPracMarks(int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalMaxMarks from DDEPractical where PracticalID='" + pracid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            maxpracmarks = Convert.ToInt32(dr[0]);
            con.Close();

        }

        private void populateMarks(DataRow drow, int srid, int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select PracticalMarks from DDEPracticalMarks_" + ddlistExam.SelectedItem.Value + " where SRID='" + srid + "' and PracticalID='" + pracid + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            drow["PracticalMarks"] = dr[0].ToString();

            con.Close();

        }

        private bool marksFilled(int srid, int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEPracticalMarks_" + ddlistExam.SelectedItem.Value + " where SRID='" + srid + "' and PracticalID='" + pracid + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
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


            if (ddlistMOE.SelectedItem.Value == "R")
            {

                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                {
                    if (ddlistYear.SelectedItem.Value == "1")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
                    }
                    else if (ddlistYear.SelectedItem.Value == "2")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
                    }
                    else if (ddlistYear.SelectedItem.Value == "3")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
                    }

                }
                else
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and RecordStatus='True' order by StudentName ";
                }
            }

            else if (ddlistMOE.SelectedItem.Value == "B")
            {
                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                {
                    if (ddlistYear.SelectedItem.Value == "1")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                    }
                    else if (ddlistYear.SelectedItem.Value == "2")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                    }
                    else if (ddlistYear.SelectedItem.Value == "3")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                    }

                }
                else
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + Session["SCCode"].ToString() + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                }

            }




            SqlDataReader dr;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("EC");
            DataColumn dtcol5 = new DataColumn("RollNo");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("SCCode");
            DataColumn dtcol8 = new DataColumn("MarksFilled");
            DataColumn dtcol9 = new DataColumn("PracticalMarks");
            DataColumn dtcol10 = new DataColumn("CYear");
            DataColumn dtcol11 = new DataColumn("Remark");



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
            dt.Columns.Add(dtcol11);

            int i = 1;
            while (dr.Read())
            {
               
                if (FindInfo.isCourseFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]),Convert.ToInt32(ddlistYear.SelectedItem.Value)))
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

                    drow["CYear"] = Convert.ToString(dr["CYear"]);

                    if (ddlistMOE.SelectedItem.Value == "R")
                    {

                        drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]),"B13","R");
                        
                    }

                    else if (ddlistMOE.SelectedItem.Value == "B")
                    {
                        drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), "B13", "B");
                    }

                    if (marksFilled(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistPractical.SelectedItem.Value)))
                    {
                        drow["MarksFilled"] = "True";
                        populateMarks(drow, Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistPractical.SelectedItem.Value));
                    }

                    else
                    {
                        drow["MarksFilled"] = "False";
                        drow["PracticalMarks"] = "";


                    }
                    if(FindInfo.isExamFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]),Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
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
                pnlMSG.Visible = false;
                btnOK.Visible = false;

            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
                btnOK.Visible = false;

            }

            con.Close();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string error;

            findMaxPracMarks(Convert.ToInt32(ddlistPractical.SelectedItem.Value));

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
                        TextBox pracmarks = (TextBox)dli.FindControl("tbPracticalMarks");
                        Label lpracmarks = (Label)dli.FindControl("lblPracticalMarks");




                        if (lblmf.Text == "False")
                        {
                            if (pracmarks.Text != "")
                            {
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("insert into DDEPracticalMarks_" + ddlistExam.SelectedItem.Value + " values(@SRID,@PracticalID,@StudyCentreCode,@PracticalMarks,@MOE)", con);


                                cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));
                                cmd.Parameters.AddWithValue("@PracticalID", Convert.ToInt32(ddlistPractical.SelectedItem.Value));
                                cmd.Parameters.AddWithValue("@StudyCentreCode", Session["SCCode"].ToString());
                                cmd.Parameters.AddWithValue("@PracticalMarks", pracmarks.Text);
                                cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);




                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();


                                Log.createLogNow("Marks Filling By SC", "Filled '" + pracmarks.Text + "' practical marks of a student with Enrollment No '" + eno.Text + "' of practical '" + ddlistPractical.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["SCCode"].ToString()));
                            }

                        }

                        else if (lblmf.Text == "True")
                        {
                            if (lpracmarks.Text != pracmarks.Text)
                            {
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("update DDEPracticalMarks_" + ddlistExam.SelectedItem.Value + " set PracticalMarks=@PracticalMarks where SRID='" + srid.Text + "' and PracticalID='" + ddlistPractical.SelectedItem.Value + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
                                cmd.Parameters.AddWithValue("@PracticalMarks", pracmarks.Text);

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();


                                Log.createLogNow("Marks Updation By SC", "Updated practical marks from '" + lpracmarks.Text + "' to '" + pracmarks.Text + "' of a student with Enrollment No '" + eno.Text + "' of practical '" + ddlistPractical.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["SCCode"].ToString()));
                            }
                        }



                    }

                    dtlistShowStudents.Visible = false;
                    btnSubmit.Visible = false;
                    lblMSG.Text = "Marks has been submitted successfully";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }
                else
                {
                    int mpm=((maxpracmarks*90)/100);

                    pnlData.Visible = false;
                    dtlistShowStudents.Visible = false;
                    btnSubmit.Visible = false;
                    lblMSG.Text = "Invalid entry at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check marks should be between 0-"+mpm.ToString()+" and should be in numeric form </br> </br>If you want to fill marks more than '"+mpm.ToString()+"' please send the copy of this student to us.</br>The marks will be uploaded by us after checking the copy of student.";
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

            if (ddlistCourse.SelectedItem.Text != "--SELECT ONE--" && ddlistYear.SelectedItem.Text != "--SELECT ONE--" && ddlistPractical.SelectedItem.Text != "--SELECT ONE--" && ddlistSession.SelectedItem.Text != "--SELECT ONE--")
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
                else if (ddlistPractical.SelectedItem.Text == "--SELECT ONE--")
                {
                    error = "Please select any Practical";
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
                    TextBox pracmarks = (TextBox)dli.FindControl("tbPracticalMarks");


                    SNo = Convert.ToInt32(sno.Text);


                    int pmarks;
                    if (pracmarks.Text == "")
                    {
                        pmarks = 0;
                    }
                    else if (pracmarks.Text == "AB")
                    {
                        pmarks = 0;
                    }
                    else
                    {
                        pmarks = Convert.ToInt32(pracmarks.Text);
                    }

                    int mpm = ((maxpracmarks * 90) / 100);

                    if (pmarks >= 0 && pmarks <= mpm)
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
            

           
            lblPractical.Visible = false;
            ddlistPractical.Visible = false;
            ddlistSession.Visible = false;
            lblBatch.Visible = false;

            if (ddlistCourse.SelectedItem.Text != "--SELECT ONE--")
            {

                setYearList(Convert.ToInt32(ddlistCourse.SelectedItem.Value));
            }


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

        protected void ddlistPractical_SelectedIndexChanged(object sender, EventArgs e)
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
                TextBox prac = (TextBox)dli.FindControl("tbPracticalMarks");

                if (remark.Text == "Exam fee paid")
                {

                    prac.BackColor = System.Drawing.Color.FromName("#FFFFFF");

                    remark.ForeColor = System.Drawing.Color.FromName("#0D9103");
                }
                else
                {
                    prac.Enabled = false;
                    prac.BackColor = System.Drawing.Color.FromName("#F8A403");

                    remark.ForeColor = System.Drawing.Color.FromName("#912E03");
                }

            }
        }

       

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistSession.SelectedItem.Selected = false;
            ddlistSession.Items.FindByText("--SELECT ONE--").Selected = true;

            ddlistPractical.Items.Clear();
            populatePractical();
            lblPractical.Visible = true;
            ddlistPractical.Visible = true;

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

       

       
    }
}
