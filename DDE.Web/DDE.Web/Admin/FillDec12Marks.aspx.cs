using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class FillDec12Marks : System.Web.UI.Page
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 83))
            {
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

        private void setAccessbility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                foreach (DataListItem dli in dtlistSubMarks.Items)
                {

                    TextBox theory = (TextBox)dli.FindControl("tbTheory");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");
                    theory.Enabled = true;
                    ia.Enabled = true;
                    aw.Enabled = true;

                }
                foreach (DataListItem dli in dtlistPracMarks.Items)
                {

                    TextBox pmarks = (TextBox)dli.FindControl("tbPOMarks");
                    pmarks.Enabled = true;

                }
            }
        }

        private void populateMarkSheet()
        {
         
            populateSubjectMarks();
            populatePracticalMarks();

           
           
            pnlData.Visible = true;
            pnlMSG.Visible = false;
        }

        private void populateStudentDetail(int srid, int year)
        {

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
                lblRNo.Text = FindInfo.findRollNoBySRID(Convert.ToInt32(srid),"B12", "R");
                lblExamination.Text = "December 2012";
               
                string course = FindInfo.findCourseFullNameBySRID(Convert.ToInt32(srid),year);

                    if (ddlistMOE.SelectedItem.Value == "R")
                    {
                        if (Convert.ToInt32(Session["Year"]) == 1)
                        {

                            if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                            {
                                lblCourseFullName.Text = course + "<br/> FIRST YEAR";

                            }
                            else
                            {
                                lblCourseFullName.Text = course;
                            }
                        }

                        else if (Convert.ToInt32(Session["Year"]) == 2)
                        {

                            if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                            {
                                lblCourseFullName.Text = course + "<br/> SECOND YEAR";

                            }
                            else
                            {
                                lblCourseFullName.Text = course;
                            }
                        }

                        else if (Convert.ToInt32(Session["Year"]) == 3)
                        {

                            if (!FindInfo.oneYearCourse(Convert.ToInt32(dr["Course"])))
                            {
                                lblCourseFullName.Text = course + "<br/> THIRD YEAR";

                            }
                            else
                            {
                                lblCourseFullName.Text = course;
                            }
                        }
                    }


                  

                }


           


            con.Close();
        



        }

        private string findCourseFullNameByID(int courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select CourseFullName from DDECourse where CourseID='" + courseid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string cfn = dr[0].ToString();
            con.Close();
            return cfn;

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
           
                if (ddlistMOE.SelectedItem.Value == "R")
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select * from DDESubject where SyllabusSession='" + Session["SySession"].ToString() + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"])) + "' and Year='" + Session["AYear"].ToString() + "'", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    DataColumn dtcol1 = new DataColumn("RID");
                    DataColumn dtcol2 = new DataColumn("SubjectID");
                    DataColumn dtcol3 = new DataColumn("SubjectSNo");
                    DataColumn dtcol4 = new DataColumn("SubjectCode");
                    DataColumn dtcol5 = new DataColumn("SubjectName");
                   
                    DataColumn dtcol10 = new DataColumn("Theory");
                    DataColumn dtcol11 = new DataColumn("IA");
                    DataColumn dtcol12 = new DataColumn("AW");
                   

                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);
                    dt.Columns.Add(dtcol4);
                    dt.Columns.Add(dtcol5);
                   
                    dt.Columns.Add(dtcol10);
                    dt.Columns.Add(dtcol11);
                    dt.Columns.Add(dtcol12);
                   


                    while (dr.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["RID"] = "NF";
                        drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                        drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                        drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                        drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);
                       
                        drow["Theory"] = "";
                        drow["IA"] = "";
                        drow["AW"] = "";
                        
                        dt.Rows.Add(drow);
                       
                    }


                    dt.DefaultView.Sort = "SubjectSNo ASC";
                    dtlistSubMarks.DataSource = dt;
                    dtlistSubMarks.DataBind();

                    con.Close();


                }
                else if (ddlistMOE.SelectedItem.Value == "B")
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
                       
                        DataColumn dtcol10 = new DataColumn("Theory");
                        DataColumn dtcol11 = new DataColumn("IA");
                        DataColumn dtcol12 = new DataColumn("AW");
                      

                        dt.Columns.Add(dtcol1);
                        dt.Columns.Add(dtcol2);
                        dt.Columns.Add(dtcol3);
                        dt.Columns.Add(dtcol4);
                        dt.Columns.Add(dtcol5);
                      
                        dt.Columns.Add(dtcol10);
                        dt.Columns.Add(dtcol11);
                        dt.Columns.Add(dtcol12);
                      

                        int j = 0;
                        while (j < sub.Length)
                        {
                            DataRow drow = dt.NewRow();
                            drow["RID"] = "NF";
                            fillSubjectDetail(Convert.ToInt32(sub[j]), drow);                      
                            drow["Theory"] = "";
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

                populateSMarks(Convert.ToInt32(lblSRID.Text), ddlistMOE.SelectedItem.Value);
                         

                if (ddlistMOE.SelectedItem.Value == "B")
                {
                    lblCourseFullName.Text = lblCourseFullName.Text + " - " + FindInfo.findAlphaYear(Session["Year"].ToString()).ToUpper();
                }
            }

       

        private void populateSMarks(int srid, string moe)
        {

            foreach (DataListItem li in dtlistSubMarks.Items)
            {
                
                Label subid = (Label)li.FindControl("lblSubjectID");
                TextBox theory = (TextBox)li.FindControl("tbTheory");
                TextBox ia = (TextBox)li.FindControl("tbIA");
                TextBox aw = (TextBox)li.FindControl("tbAW");

                Label ltheory = (Label)li.FindControl("lblTheory");
                Label lia = (Label)li.FindControl("lblIA");
                Label law = (Label)li.FindControl("lblAW");
              

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                cmd.CommandText = "select * from DDEMarkSheet_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and SubjectID='" + subid.Text + "' and MOE='" + moe + "'";
               

                cmd.Connection = con;

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                   ltheory.Text= theory.Text = dr["Theory"].ToString();
                    if (ddlistMOE.SelectedItem.Value == "R")
                    {
                       lia.Text= ia.Text = dr["IA"].ToString();
                       law.Text= aw.Text = dr["AW"].ToString();

                        ia.Enabled = true;
                        aw.Enabled = true;
                    }
                    else if (ddlistMOE.SelectedItem.Value == "B")
                    {
                        ia.Text = "";
                        aw.Text = "";

                        ia.Enabled = false;
                        aw.Enabled = false;

                    }
                    if (theory.Text != "")
                    {
                        theory.Enabled = false;
                    }
                    if (ia.Text != "")
                    {
                        ia.Enabled = false;
                    }
                    if (aw.Text != "")
                    {
                        aw.Enabled = false;
                    }

                    lblTheoryExist.Text = "Yes";
                    
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

        

        private void populatePracticalMarks()
        {
            
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select * from DDEPractical where SyllabusSession='" + Session["SySession"].ToString() + "' and CourseName='" + FindInfo.findCourseNameBySRID(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(Session["Year"])) + "' and Year='" + Session["AYear"].ToString() + "'", con);
                    SqlDataReader dr;

                    con.Open();
                    dr = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    DataColumn dtcol1 = new DataColumn("PID");
                    DataColumn dtcol2 = new DataColumn("PracticalID");
                    DataColumn dtcol3 = new DataColumn("PracticalSNo");
                    DataColumn dtcol4 = new DataColumn("PracticalCode");
                    DataColumn dtcol5 = new DataColumn("PracticalName");
                    
                    DataColumn dtcol7 = new DataColumn("PracticalObtainedMarks");
                   

                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);
                    dt.Columns.Add(dtcol4);
                    dt.Columns.Add(dtcol5);
                    
                    dt.Columns.Add(dtcol7);
                    

                    while (dr.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["PID"] = "NF";
                        drow["PracticalID"] = Convert.ToString(dr["PracticalID"]);
                        drow["PracticalSNo"] = Convert.ToString(dr["PracticalSNo"]);
                        drow["PracticalCode"] = Convert.ToString(dr["PracticalCode"]);
                        drow["PracticalName"] = Convert.ToString(dr["PracticalName"]);                       
                        drow["PracticalObtainedMarks"] = "";
                        dt.Rows.Add(drow);               

                    }

                    dt.DefaultView.Sort = "PracticalSNo ASC";
                    dtlistPracMarks.DataSource = dt;
                    dtlistPracMarks.DataBind();

                    con.Close();
                   
                }
            
                populatePracMarks(Convert.ToInt32(lblSRID.Text), ddlistMOE.SelectedItem.Value);

        }

        private void populatePracMarks(int srid, string moe)
        {
            foreach (DataListItem dli in dtlistPracMarks.Items)
            {
                
                Label pracid = (Label)dli.FindControl("lblPracticalID");
                TextBox omarks = (TextBox)dli.FindControl("tbPOMarks");
               

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                cmd.CommandText = "select * from DDEPracticalMarks_" + Session["ExamCode"].ToString() + " where SRID='" + srid + "' and PracticalID='" + pracid.Text + "' and MOE='" + moe + "'";
               

                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    omarks.Text = dr["PracticalMarks"].ToString();
                    if (omarks.Text != "")
                    {
                        omarks.Enabled = false;
                    }
                    lblPracticalExist.Text = "Yes";
                }
                   

                con.Close();

            }

        }

        private void fillPracticalDetail(DataRow drow, int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical where PracticalID='" + pracid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drow["PracticalID"] = Convert.ToString(dr["PracticalID"]);
                drow["PracticalSNo"] = Convert.ToString(dr["PracticalSNo"]);
                drow["PracticalCode"] = Convert.ToString(dr["PracticalCode"]);
                drow["PracticalName"] = Convert.ToString(dr["PracticalName"]) + " (" + Convert.ToString(dr["SyllabusSession"]) + ")";
                


            }

            con.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string error;
            int srid;
            if (validENo(out srid,out error))
            {
                if (ddlistYear.SelectedItem.Value != "0")
                {
                    Session["SySession"] = ddlistSySession.SelectedItem.Text;
                    Session["ExamCode"] = "B12";
                    Session["Year"] = ddlistYear.SelectedItem.Value;
                    Session["AYear"] = FindInfo.findAlphaYear(Session["Year"].ToString());

                    populateStudentDetail(srid, Convert.ToInt32(Session["Year"].ToString()));
                    populateMarkSheet();
                    pnlMarkSheet.Visible = true;
                    pnlMS.Visible = true;
                    pnlSearch.Visible = false;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! You did not select any year.Please select any year.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
                           
                  
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text =error;                  
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
           

           
        }

        private bool validENo(out int srid, out string error)
        {
            error = "";
            bool vaild = false;
            srid = FindInfo.findSRIDByENo(tbEnrollmentNo.Text);
            string sccode = FindInfo.findSCCodeForMarkSheetBySRID(srid);
            int tm = findTotalMarkSheet(sccode);
            if (tm != 0)
            {

                if (srid != 0)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("select distinct SRID from DDEMarkSheet_B12 where StudyCentreCode='" + sccode + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();                 
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count < tm)
                    {
                        vaild = true;
                    }
                    else
                    {
                        error = "Sorry !! Total Mark Sheets have been uploaded for SCCode of Student";
                    }
                   
                }
                else
                {
                    error = "Sorry !! Invalid Enrollment No.";
                }
            }
            else
            {
                error = "Sorry !! Total Mark Sheet Record is not set for SC Code of Student";
            }

            return vaild;
        }

        private int findTotalMarkSheet(string sccode)
        {
            int tm = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select TM from DDEDec12MRecord where SCCode='" + sccode + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tm = Convert.ToInt32(dr[0]);
            }

            con.Close();
            return tm;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int tme = 0;
            int tmu = 0;

            int pme = 0;
            int pmu = 0;
            if (lblTheoryExist.Text == "No")
            {
               
                foreach (DataListItem li in dtlistSubMarks.Items)
                {
                    Label subid = (Label)li.FindControl("lblSubjectID");
                    Label subname = (Label)li.FindControl("lblSubjectName");
                    TextBox theory = (TextBox)li.FindControl("tbTheory");
                    TextBox ia = (TextBox)li.FindControl("tbIA");
                    TextBox aw = (TextBox)li.FindControl("tbAW");

                    if (theory.Text != "" || ia.Text != "" || aw.Text != "")
                    {
                        tme = 1;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into [DDEMarkSheet_B12] values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                        cmd.Parameters.AddWithValue("@SRID", lblSRID.Text);
                        cmd.Parameters.AddWithValue("@SubjectID", subid.Text);
                        cmd.Parameters.AddWithValue("@StudyCentreCode", lblSCCode.Text);
                        cmd.Parameters.AddWithValue("@Theory", theory.Text);
                        cmd.Parameters.AddWithValue("@IA", ia.Text);
                        cmd.Parameters.AddWithValue("@AW", aw.Text);
                        cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Marks Filling", "Filled Theory-'" + theory.Text + "', IA-'" + ia.Text + "',AW-'" + aw.Text + "' Marks of Student with EnrollmentNo '" + lblENo.Text + "' in '" + subname.Text + "' for December 2012 Exam ", Convert.ToInt32(Session["ERID"].ToString()));

                      
                    }
                   

                    
                    
                }

               

            }
            else if (lblTheoryExist.Text == "Yes")
            {
               
                foreach (DataListItem li in dtlistSubMarks.Items)
                {
                    Label subid = (Label)li.FindControl("lblSubjectID");
                    Label subname = (Label)li.FindControl("lblSubjectName");

                    TextBox theory = (TextBox)li.FindControl("tbTheory");
                    TextBox ia = (TextBox)li.FindControl("tbIA");
                    TextBox aw = (TextBox)li.FindControl("tbAW");

                    Label ltheory = (Label)li.FindControl("lblTheory");
                    Label lia = (Label)li.FindControl("lblIA");
                    Label law = (Label)li.FindControl("lblAW");

                    if (ltheory.Text != theory.Text || lia.Text != ia.Text || law.Text != aw.Text)
                    {
                        tmu = 1;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update [DDEMarkSheet_B12] set Theory=@Theory,IA=@IA,AW=@AW where SRID='" + lblSRID.Text + "' and SubjectID='" + subid.Text + "'", con);


                        cmd.Parameters.AddWithValue("@Theory", theory.Text);
                        cmd.Parameters.AddWithValue("@IA", ia.Text);
                        cmd.Parameters.AddWithValue("@AW", aw.Text);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Update", "Updated Theory-'" + ltheory.Text + "' to '" + theory.Text + "', IA-'" + lia.Text + "' to '" + ia.Text + "',AW-'" + law.Text + "' to '" + aw.Text + "' Marks of Student with EnrollmentNo '" + lblENo.Text + "' in '" + subname.Text + "' for December 2012 Exam ", Convert.ToInt32(Session["ERID"].ToString()));
                       
                    }
                   

                }

               

              
            }


            if (lblPracticalExist.Text == "No")
            {
                foreach (DataListItem li in dtlistPracMarks.Items)
                {
                    Label pracid = (Label)li.FindControl("lblPracticalID");
                    Label pracname = (Label)li.FindControl("lblPracticalName");
                    TextBox pmarks = (TextBox)li.FindControl("tbPOMarks");
                    Label lpmarks = (Label)li.FindControl("lblPOMarks");


                    if (pmarks.Text != "")
                    {
                        pme = 1;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into [DDEPracticalMarks_B12] values(@SRID,@PracticalID,@StudyCentreCode,@PracticalMarks,@MOE)", con);

                        cmd.Parameters.AddWithValue("@SRID", lblSRID.Text);
                        cmd.Parameters.AddWithValue("@PracticalID", pracid.Text);
                        cmd.Parameters.AddWithValue("@StudyCentreCode", lblSCCode.Text);
                        cmd.Parameters.AddWithValue("@PracticalMarks", pmarks.Text);
                        cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Marks Filling", "Filled Practical Marks-'" + pmarks.Text + "' of Student with EnrollmentNo '" + lblENo.Text + "' in '" + pracname.Text + "' for December 2012 Exam ", Convert.ToInt32(Session["ERID"].ToString()));
                       
                    }
                    
                }
              
               

            }
            else if (lblPracticalExist.Text == "Yes")
            {
                foreach (DataListItem li in dtlistPracMarks.Items)
                {
                    Label pracid = (Label)li.FindControl("lblPracticalID");
                    Label pracname = (Label)li.FindControl("lblPracticalName");
                    TextBox pmarks = (TextBox)li.FindControl("tbPOMarks");
                    Label lpmarks = (Label)li.FindControl("lblPOMarks");


                    if (pmarks.Text != lpmarks.Text)
                    {
                        pmu = 1;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update [DDEPracticalMarks_B12] set PracticalMarks=@PracticalMarks where SRID='" + lblSRID.Text + "' and PracticalID='" + pracid.Text + "'", con);

                        cmd.Parameters.AddWithValue("@PracticalMarks", pmarks.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Update", "Updated Practical Marks from '" + lpmarks.Text + "' to '" + pmarks.Text + "' of Student with EnrollmentNo '" + lblENo.Text + "' in '" + pracname.Text + "' for December 2012 Exam ", Convert.ToInt32(Session["ERID"].ToString()));
                    }
                }

              

            }

            if (tme == 1 || pme == 1)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Marks has been submitted successfully.";
                pnlMSG.Visible = true;
            }
            else if (tmu == 1 || pmu == 1)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Marks has been updated successfully.";
                pnlMSG.Visible = true;

              
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not fill any marks.";
                pnlMSG.Visible = true;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlSearch.Visible = true;
            pnlMarkSheet.Visible = false;
            pnlMS.Visible = false;
            pnlMSG.Visible = false;
        }
    }
}