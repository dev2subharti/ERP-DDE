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
    public partial class UploadInternalMarks1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Authorisation.authorisedSCFor(Session["ERID"].ToString(), 25) || Authorisation.authorisedSCFor(Session["ERID"].ToString(), 28))
                {
                    if (Request.QueryString["EnrollmentNo"] != null)
                    {

                        if (populateStudentDetail() != 0)
                        {
                            if (Session["ResultType"].ToString() == "B")
                            {
                                lblBP.Visible = true;
                            }

                            else if (Session["ResultType"].ToString() == "R")
                            {
                                lblBP.Visible = false;
                            }

                            populateMarkSheet();


                        }

                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! this Enrollment No. does not exist";
                            pnlMSG.Visible = true;

                        }
                    }

                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Please close this page and open website again";
                        pnlMSG.Visible = true;

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



        }

        private void populateMarkSheet()
        {


            populateSubjectMarks();


            pnlData.Visible = true;
            pnlMSG.Visible = false;
        }

        private int populateStudentDetail()
        {
            int srid = FindInfo.findSRIDByENo(Request.QueryString["EnrollmentNo"]);


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select StudyCentreCode,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,Course,StudentName,FatherName from DDEStudentRecord where SRID='" + srid + "' and RecordStatus='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                lblSName.Text = dr["StudentName"].ToString();
                lblFName.Text = dr["FatherName"].ToString();
                lblENo.Text = dr["EnrollmentNo"].ToString();
                lblSCCode.Text = FindInfo.findSCCodeForMarkSheetBySRID(srid);
                lblSRID.Text = srid.ToString();
                lblRNo.Text = FindInfo.findRollNoBySRID1(Convert.ToInt32(srid), Convert.ToInt32(Session["Year"]), Session["ExamCode"].ToString(), Session["ResultType"].ToString());
                lblExamination.Text = Session["ExamName"].ToString();
                lblCourse.Text = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid), Convert.ToInt32(Session["Year"]));
                lblYear.Text = FindInfo.findAlphaYear(Session["Year"].ToString()).ToUpper();

            }


            con.Close();
            return srid;



        }



        private int findSRIDByEnrollmentNo(string eno)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select SRID from DDEStudentRecord where EnrollmentNo='" + eno + "' and RecordStatus='True'", con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                int srid = Convert.ToInt32(dr[0]);
                con.Close();
                return srid;
            }

            catch
            {

                return 0;

            }
        }


        private void populateSubjectMarks()
        {
            if (Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10" || Session["ExamCode"].ToString() == "W11")
            {
                if (Session["ResultType"].ToString() == "R")
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select * from DDESubject where SyllabusSession='" + Session["SS"].ToString() + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"])) + "' and Year='" + Session["AYear"].ToString() + "'", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    DataColumn dtcol1 = new DataColumn("RID");
                    DataColumn dtcol2 = new DataColumn("SubjectID");
                    DataColumn dtcol3 = new DataColumn("SubjectSNo");
                    DataColumn dtcol4 = new DataColumn("SubjectCode");
                    DataColumn dtcol5 = new DataColumn("SubjectName");
                    DataColumn dtcol6 = new DataColumn("MTH");
                    DataColumn dtcol7 = new DataColumn("MIA");
                    DataColumn dtcol8 = new DataColumn("MAW");
                    DataColumn dtcol9 = new DataColumn("TH");
                    DataColumn dtcol10 = new DataColumn("IA");
                    DataColumn dtcol11 = new DataColumn("AW");


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


                    while (dr.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["RID"] = "NF";
                        drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                        drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                        drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                        drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);
                        drow["MTH"] = "100";
                        drow["MIA"] = "15";
                        drow["MAW"] = "15";
                        drow["TH"] = "";
                        drow["IA"] = "";
                        drow["AW"] = "";

                        dt.Rows.Add(drow);

                    }


                    dt.DefaultView.Sort = "SubjectSNo ASC";
                    dtlistSubMarks.DataSource = dt;
                    dtlistSubMarks.DataBind();

                    con.Close();


                }
                else if (Session["ResultType"].ToString() == "B")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select BPSubjects" + Session["Year"].ToString() + " from DDEExamRecord_" + Session["ExamCode"].ToString() + " where SRID='" + lblSRID.Text + "' and MOE='B'", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();
                    string[] sub = { };
                    while (dr.Read())
                    {
                        sub = dr[0].ToString().Split(',');
                    }


                    con.Close();

                    if (sub.Length != 0)
                    {
                        DataTable dt = new DataTable();
                        DataColumn dtcol1 = new DataColumn("RID");
                        DataColumn dtcol2 = new DataColumn("SubjectID");
                        DataColumn dtcol3 = new DataColumn("SubjectSNo");
                        DataColumn dtcol4 = new DataColumn("SubjectCode");
                        DataColumn dtcol5 = new DataColumn("SubjectName");
                        DataColumn dtcol6 = new DataColumn("MTH");
                        DataColumn dtcol7 = new DataColumn("MIA");
                        DataColumn dtcol8 = new DataColumn("MAW");
                        DataColumn dtcol9 = new DataColumn("TH");
                        DataColumn dtcol10 = new DataColumn("IA");
                        DataColumn dtcol11 = new DataColumn("AW");


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

                        int j = 0;
                        while (j < sub.Length)
                        {
                            DataRow drow = dt.NewRow();
                            drow["RID"] = "NF";
                            fillSubjectDetail(Convert.ToInt32(sub[j]), drow);
                            drow["MTH"] = "100";
                            drow["MIA"] = "15";
                            drow["MAW"] = "15";

                            drow["TH"] = "";
                            drow["IA"] = "";
                            drow["AW"] = "";

                            dt.Rows.Add(drow);
                            j = j + 1;

                        }

                        dt.DefaultView.Sort = "SubjectSNo ASC";
                        dtlistSubMarks.DataSource = dt;
                        dtlistSubMarks.DataBind();
                    }
                }

                populateSMarks(Convert.ToInt32(lblSRID.Text), Session["ResultType"].ToString());
            }


        }

        private void populateSMarks(int srid, string moe)
        {
            bool auth = false;

            if (Authorisation.authorisedSCFor(Session["ERID"].ToString(), 28))
            {
                auth = true;
            }

            foreach (DataListItem li in dtlistSubMarks.Items)
            {

                Label subid = (Label)li.FindControl("lblSubjectID");
                Label mf = (Label)li.FindControl("lblMarksFilled");
                TextBox th = (TextBox)li.FindControl("tbTH");
                TextBox ia = (TextBox)li.FindControl("tbIA");
                TextBox aw = (TextBox)li.FindControl("tbAW");
                Label lth = (Label)li.FindControl("lblTH");
                Label lia = (Label)li.FindControl("lblIA");
                Label law = (Label)li.FindControl("lblAW");


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDEMarkSheet_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and SubjectID='" + subid.Text + "' and MOE='" + moe + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();


                    th.Text = dr["Theory"].ToString();                  
                    lth.Text = dr["Theory"].ToString();
                    ia.Text = dr["IA"].ToString();
                    aw.Text = dr["AW"].ToString();
                    lia.Text = dr["IA"].ToString();
                    law.Text = dr["AW"].ToString();

                    if (th.Text != "")
                    {
                        if (auth == true)
                        {
                            th.Enabled = true;
                        }
                        else
                        {
                            th.Enabled = false;
                        }
                    }

                    if (ia.Text != "")
                    {
                        if (auth==true)
                        {
                            ia.Enabled = true;
                        }
                        else
                        {
                            ia.Enabled = false;
                        }
                    }

                    if (aw.Text != "")
                    {
                        if (auth==true)
                        {
                            aw.Enabled = true;
                        }
                        else
                        {
                            aw.Enabled = false;
                        }
                    }

                    mf.Text = "True";

                }
                else
                {
                    mf.Text = "False";
                }

                con.Close();


            }
        }


        private void fillSubjectDetail(int subid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESubject where SubjectID='" + subid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                drow["SubjectName"] = Convert.ToString(dr["SubjectName"]) + " (" + Convert.ToString(dr["SyllabusSession"]) + ")";

            }

            con.Close();
        }

        private int getMarks(string marks)
        {
            if (marks == "")
            {
                return 0;
            }
            else if (marks == "*")
            {
                return 0;
            }

            else if (marks == "AB")
            {
                return 0;
            }
            else if (marks == "NF")
            {
                return 0;
            }

            else return Convert.ToInt32(marks);
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (validMarks())
            {
                foreach (DataListItem dli in dtlistSubMarks.Items)
                {
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    Label lblsub = (Label)dli.FindControl("lblSubjectID");
                    TextBox th = (TextBox)dli.FindControl("tbTH");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");
                    Label lth = (Label)dli.FindControl("lblTH");
                    Label lia = (Label)dli.FindControl("lblIA");
                    Label law = (Label)dli.FindControl("lblAW");



                    if (lblmf.Text == "False")
                    {
                        if (th.Text != "" || ia.Text != "" || aw.Text != "")
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_" + Session["ExamCode"].ToString() + " values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                            cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
                            cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(lblsub.Text));
                            cmd.Parameters.AddWithValue("@StudyCentreCode", lblSCCode.Text);
                            cmd.Parameters.AddWithValue("@Theory", th.Text);
                            cmd.Parameters.AddWithValue("@IA", ia.Text);
                            cmd.Parameters.AddWithValue("@AW", aw.Text);
                            cmd.Parameters.AddWithValue("@MOE", Session["ResultType"].ToString());

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Log.createLogNow("Marks Filling", "Filled '" + th.Text + "' Theory '" + ia.Text + "' IA & '" + aw.Text + "' AW  marks of a student with Enrollment No '" + lblENo.Text + "' of subject '" + FindInfo.findSubjectDetailByID(Convert.ToInt32(lblsub.Text)) + "' for " + Session["ExamName"].ToString() + " exam", Convert.ToInt32(Session["ERID"]));
                        }
                    }

                    else if (lblmf.Text == "True")
                    {
                        if (lth.Text != th.Text || lia.Text != ia.Text || law.Text != aw.Text)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("update DDEMarkSheet_" + Session["ExamCode"].ToString() + " set Theory=@Theory,IA=@IA,AW=@AW where SRID='" + lblSRID.Text + "' and SubjectID='" + lblsub.Text + "' and MOE='" + Session["ResultType"].ToString() + "'", con);
                            cmd.Parameters.AddWithValue("@Theory", th.Text);
                            cmd.Parameters.AddWithValue("@IA", ia.Text);
                            cmd.Parameters.AddWithValue("@AW", aw.Text);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Log.createLogNow("Marks Updation", "Updated marks Theory from '" + lth.Text + "' to '" + th.Text + "' & IA from '" + lia.Text + "' to '" + ia.Text + "' & AW from '" + law.Text + "' to '" + aw.Text + "' of a student with Enrollment No '" + lblENo.Text + "' of subject '" + FindInfo.findSubjectDetailByID(Convert.ToInt32(lblsub.Text)) + "' for " + Session["ExamName"].ToString() + " exam", Convert.ToInt32(Session["ERID"]));
                        }
                    }



                }

                pnlData.Visible = false;
                pnlMarkSheet.Visible = false;
                lblMSG.Text = "Marks have been submitted successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = false;
            }

            else
            {
                pnlData.Visible = false;
                pnlMarkSheet.Visible = false;


                if (Session["Sub"].ToString() == "001")
                {
                    lblMSG.Text = "Error in Subjects (Error Code : 001)";
                }
                else
                {
                    lblMSG.Text = "Invalid entry at Subject Name - ' " + Session["Sub"].ToString() + " '" + "</br> <div align='left' >Please check : </br> 1. Marks should be in numeric form. </br> 2. Marks should be between 0-12.</br>( If you want to fill 13 to 15 marks than please send the copy of this student to us.</br>The marks will be uploaded by us after checking the copy of student.)</div>";
                }



                pnlMSG.Visible = true;
                btnOK.Visible = true;


            }

        }

        private bool validMarks()
        {

            bool validmarks = false;

            if (dtlistSubMarks.Items.Count > 0)
            {

                foreach (DataListItem dli in dtlistSubMarks.Items)
                {
                    try
                    {
                        Label sn = (Label)dli.FindControl("lblSubjectName");
                        Label srid = (Label)dli.FindControl("lblSRID");
                        TextBox th = (TextBox)dli.FindControl("tbTH");                     
                        TextBox ia = (TextBox)dli.FindControl("tbIA");
                        TextBox aw = (TextBox)dli.FindControl("tbAW");


                        int thmarks;
                        int iamarks;
                        int awmarks;

                        if (th.Text == "")
                        {
                            thmarks = 0;
                        }
                        else if (th.Text == "AB")
                        {
                            thmarks = 0;
                        }
                        else
                        {
                            thmarks = Convert.ToInt32(th.Text);
                        }

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


                        if (thmarks >= 0 && thmarks <= 100 && iamarks >= 0 && iamarks <= 12 && awmarks >= 0 && awmarks <= 12)
                        {
                            validmarks = true;

                        }

                        else
                        {
                            validmarks = false;
                            Session["Sub"] = sn.Text;
                            ViewState["ErrorAt"] = "Subject";
                            break;
                        }
                    }

                    catch
                    {
                        validmarks = false;
                        Session["Sub"] = "001";
                        ViewState["ErrorAt"] = "Subject";
                        break;
                    }


                }
            }



            return validmarks;

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlMarkSheet.Visible = true;

            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }
    }
}