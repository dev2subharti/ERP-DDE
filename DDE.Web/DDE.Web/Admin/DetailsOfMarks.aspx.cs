using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class DetailsOfMarks : System.Web.UI.Page
    {
        int totalsub = 0;
        int totalprac = 0;
        int maxgrandtotal = 0;
        int grandtotal = 0;
        string remark = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 29))
            {

                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    populateCourses();
                    populateSySessions();
                    populateSessions();

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

        private void populateSessions()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Session from DDESession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistSession.Items.Add(dr["Session"].ToString());

            }

            con.Close();

        }

        private void populateSySessions()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SySession from DDESySession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistSySession.Items.Add(dr["SySession"].ToString());

            }

            con.Close();
        }

        private void populateCourses()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               
                if (dr[2].ToString() == "")
                {
                    ddlistCourse.Items.Add(dr[1].ToString());
                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }

            }

            con.Close();

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
      
            popuulateSubjects();
            populatePracticals();
            populateMarks();
            pnlMarksList.Visible = true;
            
        }

       

        private void populateMarks()
        {
            lblTotalMaxMarks.Text = maxgrandtotal.ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10")
                    {
                        if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                        {
                            if (ddlistYear.SelectedItem.Value == "1")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                            }
                            else if (ddlistYear.SelectedItem.Value == "2")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                            }
                            else if (ddlistYear.SelectedItem.Value == "3")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                            }
                        }
                       
                        else
                        {
                            cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                        }
                    }
                    else if (ddlistExam.SelectedItem.Value == "A13")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                    }
                    else
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and RecordStatus='True' order by StudentName ";
                    }
                }


                else if (ddlistMOE.SelectedItem.Value == "B")
                {
                    
                        if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                        {
                            if (ddlistYear.SelectedItem.Value == "1")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                            }
                            else if (ddlistYear.SelectedItem.Value == "2")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                            }
                            else if (ddlistYear.SelectedItem.Value == "3")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "'and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                            }
                        }
                        else
                        {
                            cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and QualifyingStatus='PCP'and RecordStatus='True' order by StudentName ";
                        }
                    
                    
                }
            }

            else
            {
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10")
                    {
                        if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                        {
                            if (ddlistYear.SelectedItem.Value == "1")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='"+ddlistSCCode.SelectedItem.Value+"' and RecordStatus='True' order by StudentName ";
                            }
                            else if (ddlistYear.SelectedItem.Value == "2")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True' order by StudentName ";
                            }
                            else if (ddlistYear.SelectedItem.Value == "3")
                            {
                                cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True' order by StudentName ";
                            }
                        }
                        else
                        {
                            cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True' order by StudentName ";
                        }
                    }
                    else  if (ddlistExam.SelectedItem.Value == "A13")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True' order by StudentName ";
                    }
                    else
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True' order by StudentName ";
                    }
                }


                else if (ddlistMOE.SelectedItem.Value == "B")
                {

                    if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                    {
                        if (ddlistYear.SelectedItem.Value == "1")
                        {
                            cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                        }
                        else if (ddlistYear.SelectedItem.Value == "2")
                        {
                            cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                        }
                        else if (ddlistYear.SelectedItem.Value == "3")
                        {
                            cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                        }
                    }
                    else
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,FatherName,StudyCentreCode,Gender,Category,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and QualifyingStatus='PCP'and RecordStatus='True' order by StudentName ";
                    }


                }

            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("Gender");
            DataColumn dtcol7 = new DataColumn("Category");
            DataColumn dtcol8 = new DataColumn("SCCode");
            DataColumn dtcol9 = new DataColumn("RollNo");
            DataColumn dtcol10 = new DataColumn("CYear");
            DataColumn dtcol11 = new DataColumn("QualifyingStatus");
            DataColumn dtcol12 = new DataColumn("GrandTotal");
            DataColumn dtcol13 = new DataColumn("Remark");
            DataColumn dtcol14 = new DataColumn("Grade");
            DataColumn dtcol15 = new DataColumn("Div");



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
            dt.Columns.Add(dtcol12);
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);
            dt.Columns.Add(dtcol15);

            buildSubCols(dt, totalsub);
            buildPracCols(dt,totalprac);



            int i = 1;

            int m = 0;
            int f = 0;

            while (dr.Read())
            {
                if (ddlistExam.SelectedItem.Value == "A13")
                {
                    if (eligibleForExam(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistYear.SelectedItem.Value)))
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i;
                        drow["SRID"] = Convert.ToString(dr["SRID"]);
                        drow["EnrollmentNo"] = "-" + Convert.ToString(dr["EnrollmentNo"]);
                        drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                        drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                        drow["Gender"] = Convert.ToString(dr["Gender"]);
                        drow["Category"] = Convert.ToString(dr["Category"]);
                        drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                        drow["CYear"] = Convert.ToString(dr["CYear"]);
                        drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                           

                        populateSubMarks(drow, totalsub, Convert.ToInt32(dr["SRID"]));
                        populatePracMarks(drow, totalprac, Convert.ToInt32(dr["SRID"]));

                        drow["GrandTotal"] = grandtotal;
                        drow["Remark"] = remark;
                        drow["Grade"] = findGrade(grandtotal, maxgrandtotal);
                        drow["Div"] = findDivision(remark, maxgrandtotal, grandtotal);

                        if (drow["Div"].ToString() != "XX")
                        {
                            if (drow["Gender"].ToString() == "MALE" || drow["Gender"].ToString() == "M")
                            {
                                m = m + 1;
                            }
                            else if (drow["Gender"].ToString() == "FEMALE" || drow["Gender"].ToString() == "F")
                            {
                                f = f + 1;
                            }
                        }
                        grandtotal = 0;
                        remark = "";
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }
                }
                else
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["SRID"] = Convert.ToString(dr["SRID"]);
                    drow["EnrollmentNo"] = "-" + Convert.ToString(dr["EnrollmentNo"]);
                    drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                    drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                    drow["Gender"] = Convert.ToString(dr["Gender"]);
                    drow["Category"] = Convert.ToString(dr["Category"]);
                    drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                    drow["CYear"] = Convert.ToString(dr["CYear"]);

                    if (ddlistExam.SelectedItem.Value == "B11")
                    {
                        if (ddlistMOE.SelectedItem.Value == "R")
                        {

                            if (Convert.ToInt32(dr["CYear"]) == 1)
                            {
                                drow["RollNo"] = Convert.ToString(dr["RollNoIYear"]);
                            }

                            else if (Convert.ToInt32(dr["CYear"]) == 2)
                            {
                                drow["RollNo"] = Convert.ToString(dr["RollNoIIYear"]);
                            }

                            else if (Convert.ToInt32(dr["CYear"]) == 3)
                            {
                                drow["RollNo"] = Convert.ToString(dr["RollNoIIIYear"]);
                            }
                        }

                        else if (ddlistMOE.SelectedItem.Value == "B")
                        {
                            drow["RollNo"] = Convert.ToString(dr["RollNoBP"]);
                        }
                    }

                    else
                    {
                        drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                    }

                    populateSubMarks(drow, totalsub, Convert.ToInt32(dr["SRID"]));
                    populatePracMarks(drow, totalprac, Convert.ToInt32(dr["SRID"]));

                    drow["GrandTotal"] = grandtotal;
                    drow["Remark"] = remark;
                    drow["Grade"] = findGrade(grandtotal, maxgrandtotal);
                    drow["Div"] = findDivision(remark, maxgrandtotal, grandtotal);


                    if (drow["Div"].ToString() != "XX")
                    {
                        if (drow["Gender"].ToString() == "MALE" || drow["Gender"].ToString() == "M")
                        {
                            m = m + 1;
                        }
                        else if (drow["Gender"].ToString() == "FEMALE" || drow["Gender"].ToString() == "F")
                        {
                            f = f + 1;
                        }
                    }

                    grandtotal = 0;
                    remark = "";
                    dt.Rows.Add(drow);
                    i = i + 1;
                }
            }



            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            
            con.Close();




            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                Panel ps1 = (Panel)dli.FindControl("ps1");
                Panel ps2 = (Panel)dli.FindControl("ps2");
                Panel ps3 = (Panel)dli.FindControl("ps3");
                Panel ps4 = (Panel)dli.FindControl("ps4");
                Panel ps5 = (Panel)dli.FindControl("ps5");
                Panel ps6 = (Panel)dli.FindControl("ps6");
                Panel ps7 = (Panel)dli.FindControl("ps7");
                Panel ps8 = (Panel)dli.FindControl("ps8");


                Label pm1 = (Label)dli.FindControl("lblPM1");
                Label pm2 = (Label)dli.FindControl("lblPM2");
                Label pm3 = (Label)dli.FindControl("lblPM3");

                if(totalsub==4)
                {
                    ps1.Visible = true;
                    ps2.Visible = true;
                    ps3.Visible = true;
                    ps4.Visible = true;
                    ps5.Visible = false;
                    ps6.Visible = false;
                    ps7.Visible = false;
                    ps8.Visible = false;
                }

                else if (totalsub == 5)
                {

                    ps1.Visible = true;
                    ps2.Visible = true;
                    ps3.Visible = true;
                    ps4.Visible = true;
                    ps5.Visible = true;
                    ps6.Visible = false;
                    ps7.Visible = false;
                    ps8.Visible = false;
                }

                else if (totalsub == 6)
                {

                    ps1.Visible = true;
                    ps2.Visible = true;
                    ps3.Visible = true;
                    ps4.Visible = true;
                    ps5.Visible = true;
                    ps6.Visible = true;
                    ps7.Visible = false;
                    ps8.Visible = false;
                }

                else if (totalsub == 7)
                {

                    ps1.Visible = true;
                    ps2.Visible = true;
                    ps3.Visible = true;
                    ps4.Visible = true;
                    ps5.Visible = true;
                    ps6.Visible = true;
                    ps7.Visible = true;
                    ps8.Visible = false;
                }

                else if (totalsub == 8)
                {

                    ps1.Visible = true;
                    ps2.Visible = true;
                    ps3.Visible = true;
                    ps4.Visible = true;
                    ps5.Visible = true;
                    ps6.Visible = true;
                    ps7.Visible = true;
                    ps8.Visible = true;
                }


                if(totalprac==0)
                {
                    pm1.Visible = false;
                    pm2.Visible = false;
                    pm3.Visible = false;
                }

                else if (totalprac == 1)
                {
                    pm1.Visible = true;
                    pm2.Visible = false;
                    pm3.Visible = false;
                }

                else if (totalprac == 2)
                {
                    pm1.Visible = true;
                    pm2.Visible = true;
                    pm3.Visible = false;
                }

                else if (totalprac == 3)
                {
                    pm1.Visible = true;
                    pm2.Visible = true;
                    pm3.Visible = true;
                }
              
            }

            lbldata.Text = "Male Pass : " + m.ToString() + "</br> Female Pass : " + f.ToString();

            
         
        }

        private bool eligibleForExam(int srid, int year)
        {
            bool eligible = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "' ", con);
            con.Open();
           
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (year == 1)
                {
                    if (dr["AFP1Year"].ToString() == dr["EFP1Year"].ToString())
                    {
                        eligible = true;
                    }
                }
                else if (year == 2)
                {
                    if (dr["AFP2Year"].ToString() == dr["EFP2Year"].ToString())
                    {
                        eligible = true;
                    }
                }
                else if (year == 3)
                {
                    if (dr["AFP3Year"].ToString() == dr["EFP3Year"].ToString())
                    {
                        eligible = true;
                    }
                }

              
            }
         
           

            con.Close();


            return eligible;
        }

        private void populatePracMarks(DataRow drow, int totalprac, int srid)
        {
            if (totalprac != 0)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select *  from DDEPracticalMarks_" + ddlistExam.SelectedItem.Value + " where SRID='" + srid + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                int syear = FindInfo.findYearOfStudent(srid);
                string qstatus = FindInfo.findQStatusByID(srid);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (qstatus == "AC")
                        {
                            if (ddlistExam.SelectedItem.Value == "A13")
                            {
                                if (Convert.ToInt32(ddlistYear.SelectedItem.Value) == FindInfo.findYearOfPractical(Convert.ToInt32(dr["PracticalID"])))
                                {
                                    int i = findPracSNo(Convert.ToInt32(dr["PracticalID"]));
                                    string col1 = "PM" + i.ToString();

                                    drow[col1] = dr["PracticalMarks"].ToString();

                                    if (remark != "NC")
                                    {
                                        remark = CalculateResult.findPracRemark(dr["PracticalMarks"].ToString(), FindInfo.findTotalPracMarksByID(Convert.ToInt32(dr["PracticalID"])));
                                    }
                                    grandtotal = grandtotal + getMarks(dr["PracticalMarks"].ToString());
                                }
                            }
                            else
                            {
                                if (syear == FindInfo.findYearOfPractical(Convert.ToInt32(dr["PracticalID"])))
                                {
                                    int i = findPracSNo(Convert.ToInt32(dr["PracticalID"]));
                                    string col1 = "PM" + i.ToString();

                                    drow[col1] = dr["PracticalMarks"].ToString();

                                    if (remark != "NC")
                                    {
                                        remark = CalculateResult.findPracRemark(dr["PracticalMarks"].ToString(), FindInfo.findTotalPracMarksByID(Convert.ToInt32(dr["PracticalID"])));
                                    }
                                    grandtotal = grandtotal + getMarks(dr["PracticalMarks"].ToString());
                                }

                            }
                        }

                        else if (qstatus == "PCP")
                        {
                            if (Convert.ToInt32(ddlistYear.SelectedItem.Value) == FindInfo.findYearOfPractical(Convert.ToInt32(dr["PracticalID"])))
                            {
                                int i = findPracSNo(Convert.ToInt32(dr["PracticalID"]));
                                string col1 = "PM" + i.ToString();
                                
                                drow[col1] = dr["PracticalMarks"].ToString();
                               
                                if (remark != "NC")
                                {
                                    remark = CalculateResult.findPracRemark(dr["PracticalMarks"].ToString(), FindInfo.findTotalPracMarksByID(Convert.ToInt32(dr["PracticalID"])));
                                }
                                grandtotal = grandtotal + getMarks(dr["PracticalMarks"].ToString());
                            }
                        }

                    }
                }
                else
                {
                    
                    remark = "NC";
                }

                con.Close();
            }
  
            
        }

        private int findPracSNo(int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalSNo from DDEPractical where PracticalID='" + pracid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            int sn = 0;
            dr.Read();

            sn = Convert.ToInt32(dr[0]);

            con.Close();

            return sn;
        }

        private void populateSubMarks(DataRow drow, int totalsub, int srid)
        {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select *  from DDEMarkSheet_"+ddlistExam.SelectedItem.Value+" where SRID='"+srid+"' and MOE='"+ddlistMOE.SelectedItem.Value+"' ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                int syear=FindInfo.findYearOfStudent(srid);
                string qstatus = FindInfo.findQStatusByID(srid);
                while (dr.Read())
                {
                    if (qstatus == "AC")
                    {
                        if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10")
                        {
                            int i = findSubSNo(Convert.ToInt32(dr["SubjectID"]));
                            string col1 = "TMF" + i.ToString();
                            string col2 = "TM" + i.ToString();
                            string col3 = "IA" + i.ToString();
                            string col4 = "AW" + i.ToString();
                            string col5 = "Total" + i.ToString();
                            int ftm = 0;
                            drow[col1] = dr["Theory"].ToString();
                            if (dr["Theory"].ToString() == "")
                            {
                                drow[col2] = ""; 
                            }
                            else if (dr["Theory"].ToString() == "AB")
                            {
                                drow[col2] = "AB";
                            }
                            else
                            {
                                ftm = (getMarks(dr["Theory"].ToString()) * 60) / 100;
                                drow[col2] = ftm;
                            }

                            drow[col3] = dr["IA"].ToString();
                            drow[col4] = dr["AW"].ToString();

                            drow[col5] = ftm + getMarks(dr["IA"].ToString()) + getMarks(dr["AW"].ToString());
                            if (remark != "NC")
                            {
                                remark = CalculateResult.findSubRemark(dr["Theory"].ToString(), dr["IA"].ToString(), dr["AW"].ToString());
                            }
                            grandtotal = grandtotal + Convert.ToInt32(drow[col5]);
                        }
                        else if (ddlistExam.SelectedItem.Value == "A13")
                        {
                            if (Convert.ToInt32(ddlistYear.SelectedItem.Value)== FindInfo.findYearOfSubject(Convert.ToInt32(dr["SubjectID"])))
                            {
                                int i = findSubSNo(Convert.ToInt32(dr["SubjectID"]));
                                string col1 = "TMF" + i.ToString();
                                string col2 = "TM" + i.ToString();
                                string col3 = "IA" + i.ToString();
                                string col4 = "AW" + i.ToString();
                                string col5 = "Total" + i.ToString();
                                int ftm = 0;
                              
                                if (dr["Theory"].ToString() == "")
                                {
                                    if (ddlistExam.SelectedItem.Value == "A12")
                                    {
                                        drow[col1] = "AB";
                                        drow[col2] = "AB";
                                    }
                                    else
                                    {
                                        drow[col1] = "";
                                        drow[col2] = "";
                                    }
                                }
                                else if (dr["Theory"].ToString() == "AB")
                                {
                                    drow[col1] = "AB";
                                    drow[col2] = "AB";
                                }
                                else
                                {
                                    drow[col1] = dr["Theory"].ToString();
                                    ftm = (getMarks(dr["Theory"].ToString()) * 60) / 100;
                                    drow[col2] = ftm;
                                }

                               
                                drow[col3] = dr["IA"].ToString();
                                drow[col4] = dr["AW"].ToString();

                                drow[col5] = ftm + getMarks(dr["IA"].ToString()) + getMarks(dr["AW"].ToString());
                                if (remark != "NC")
                                {
                                    remark = CalculateResult.findSubRemark(dr["Theory"].ToString(), dr["IA"].ToString(), dr["AW"].ToString());
                                }
                                grandtotal = grandtotal + Convert.ToInt32(drow[col5]);
                            }
                        }

                        else
                        {
                            if (syear == FindInfo.findYearOfSubject(Convert.ToInt32(dr["SubjectID"])))
                            {
                                int i = findSubSNo(Convert.ToInt32(dr["SubjectID"]));
                                string col1 = "TMF" + i.ToString();
                                string col2 = "TM" + i.ToString();
                                string col3 = "IA" + i.ToString();
                                string col4 = "AW" + i.ToString();
                                string col5 = "Total" + i.ToString();
                                int ftm = 0;
                              
                                if (dr["Theory"].ToString() == "")
                                {
                                    if (ddlistExam.SelectedItem.Value == "A12")
                                    {
                                        drow[col1] = "AB";
                                        drow[col2] = "AB";
                                    }
                                    else
                                    {
                                        drow[col1] = "";
                                        drow[col2] = "";
                                    }
                                }
                                else if (dr["Theory"].ToString() == "AB")
                                {
                                    drow[col1] = "AB";
                                    drow[col2] = "AB";
                                }
                                else
                                {
                                    drow[col1] = dr["Theory"].ToString();
                                    ftm = (getMarks(dr["Theory"].ToString()) * 60) / 100;
                                    drow[col2] = ftm;
                                }

                               
                                drow[col3] = dr["IA"].ToString();
                                drow[col4] = dr["AW"].ToString();

                                drow[col5] = ftm + getMarks(dr["IA"].ToString()) + getMarks(dr["AW"].ToString());
                                if (remark != "NC")
                                {
                                    remark = CalculateResult.findSubRemark(dr["Theory"].ToString(), dr["IA"].ToString(), dr["AW"].ToString());
                                }
                                grandtotal = grandtotal + Convert.ToInt32(drow[col5]);
                            }
                        }
                    }

                    else if (qstatus == "PCP")
                    {
                        if (Convert.ToInt32(ddlistYear.SelectedItem.Value) == FindInfo.findYearOfSubject(Convert.ToInt32(dr["SubjectID"])))
                        {
                            int i = findSubSNo(Convert.ToInt32(dr["SubjectID"]));
                            string col1 = "TMF" + i.ToString();
                            string col2 = "TM" + i.ToString();
                            string col3 = "IA" + i.ToString();
                            string col4 = "AW" + i.ToString();
                            string col5 = "Total" + i.ToString();
                            int ftm=0;

                            if (dr["Theory"].ToString() == "")
                            {
                                if (ddlistExam.SelectedItem.Value == "A12")
                                {
                                    drow[col1] = "AB";
                                    drow[col2] = "AB";
                                }
                                else
                                {
                                    drow[col1] = "";
                                    drow[col2] = "";
                                }
                            }
                            else if (dr["Theory"].ToString() == "AB")
                            {
                                drow[col1] = "AB";
                                drow[col2] = "AB";
                            }
                            else
                            {
                                drow[col1] = dr["Theory"].ToString();
                                ftm = (getMarks(dr["Theory"].ToString()) * 60) / 100;
                                drow[col2] = ftm;
                            }

                            if (dr["IA"].ToString() == "")
                            {
                                if (ddlistExam.SelectedItem.Value == "A12")
                                {

                                    drow[col3] = "AB";
                                }
                                else
                                {

                                    drow[col3] = "";
                                }
                            }

                            else
                            {
                                drow[col3] = dr["IA"].ToString();

                            }


                            if (dr["AW"].ToString() == "")
                            {
                                if (ddlistExam.SelectedItem.Value == "A12")
                                {

                                    drow[col4] = "AB";
                                }
                                else
                                {

                                    drow[col4] = "";
                                }
                            }
                            else
                            {
                                drow[col4] = dr["AW"].ToString();

                            }

                            drow[col5] = ftm + getMarks(dr["IA"].ToString()) + getMarks(dr["AW"].ToString());
                            if (remark != "NC")
                            {
                                remark = CalculateResult.findSubRemark(dr["Theory"].ToString(), dr["IA"].ToString(), dr["AW"].ToString());
                            }
                            grandtotal = grandtotal + Convert.ToInt32(drow[col5]);
                        }
                    }

                }

                con.Close();

           
            
        }

        private int findSubSNo(int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectSNo from DDESubject where SubjectID='" + subid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            int sn=0;
            dr.Read();
           
            sn=Convert.ToInt32(dr[0]);

            con.Close();

            return sn;
           
        }

        private Int32 getMarks(string marks)
        {
            if (marks == "")
            {
                return 0;
            }

           
            else if (marks == "AB")
            {
                return 0;
            }

            else return Convert.ToInt32(marks);


        }

        private void buildPracCols(DataTable dt, int totalprac)
        {
            for (int i = 1; i <= 3; i++)
            {
                string col1 = "PM" + i.ToString();
               
                DataColumn dtcol1 = new DataColumn(col1);
                
                dt.Columns.Add(dtcol1);
               

            }

            
        }

        private void buildSubCols(DataTable dt, int totalsub)
        {
           
                for (int i = 1; i <= 8; i++)
                {
                    string col1 = "TMF" + i.ToString();
                    string col2 = "TM" + i.ToString();
                    string col3 = "IA" + i.ToString();
                    string col4 = "AW" + i.ToString();
                    string col5 = "Total" + i.ToString();

                    DataColumn dtcol1 = new DataColumn(col1);
                    DataColumn dtcol2 = new DataColumn(col2);
                    DataColumn dtcol3 = new DataColumn(col3);
                    DataColumn dtcol4 = new DataColumn(col4);
                    DataColumn dtcol5 = new DataColumn(col5);

                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);
                    dt.Columns.Add(dtcol4);
                    dt.Columns.Add(dtcol5);

                }

           
        }

        
        private void populatePracticals()
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalCode,PracticalMaxMarks from DDEPractical where CourseName='" + ddlistCourse.SelectedItem.Text + "' and SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' and Year='"+ddlistYear.SelectedItem.Text+"'order by PracticalSNo", con);
            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("PracticalCode");
            DataColumn dtcol2 = new DataColumn("PracticalMaxMarks");
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);

            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["PracticalCode"] = dr["PracticalCode"].ToString();
                drow["PracticalMaxMarks"] = dr["PracticalMaxMarks"].ToString();
                maxgrandtotal = maxgrandtotal + getMarks(dr["PracticalMaxMarks"].ToString());

                dt.Rows.Add(drow);
                totalprac++;
            }
            dtlistPrac.DataSource = dt;
            dtlistPrac.DataBind();
            con.Close();
        }

        private void popuulateSubjects()
        {
                           
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select SubjectCode from DDESubject where CourseName='" + ddlistCourse.SelectedItem.Text + "' and SyllabusSession='" + ddlistSySession.SelectedItem.Text + "'and Year='" + ddlistYear.SelectedItem.Text + "'order by SubjectSNo", con);
                con.Open();
                dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
               
                DataColumn dtcol1 = new DataColumn("SubjectCode");
                dt.Columns.Add(dtcol1);

                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SubjectCode"] = dr["SubjectCode"].ToString();
                   
                    maxgrandtotal = maxgrandtotal + 100;

                    dt.Rows.Add(drow);
                    totalsub++;
                }
                dtlistSub.DataSource = dt;
                dtlistSub.DataBind();
             
                con.Close();
           
       
        }

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMarksList.Visible = false;
        }

        protected void ddlistSySession_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMarksList.Visible = false;
        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMarksList.Visible = false;
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMarksList.Visible = false;
        }

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMarksList.Visible = false;
        }
        private string findGrade(int marksobtained, int maxmarks)
        {
            string grade = "";

            int percent = (marksobtained * 100) / maxmarks;

            if (percent >= 85)
            {
                grade = "A++";
            }

            else if (percent < 85 && percent >= 75)
            {
                grade = "A+";
            }

            else if (percent < 75 && percent >= 60)
            {
                grade = "A";
            }
            else if (percent < 60 && percent >= 50)
            {
                grade = "B";
            }

            else if (percent < 50 && percent >= 40)
            {
                grade = "C";
            }

            else if (percent < 40)
            {
                grade = "D";
            }

            return grade;

        }

        private string findDivision(string status, int maxmarks, int marksobtained)
        {
            string div = "";

            int percent = (marksobtained * 100) / maxmarks;

            if (status == "CC")
            {

               

                if (percent >= 85)
                {
                    div = "I";
                }

                else if (percent < 85 && percent >= 75)
                {
                    div = "I";
                }

                else if (percent < 75 && percent >= 60)
                {
                    div = "I";
                }
                else if (percent < 60 && percent >= 50)
                {
                    div = "II";
                }

                else if (percent < 50 && percent >= 40)
                {
                    div = "III";
                }

                else if (percent < 40)
                {
                    div = "Fail";
                }

               
            }

            else if (status == "NC")
            {
                div = "XX";


            }


            return div;


        }

    }


}
