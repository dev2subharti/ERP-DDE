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
    public partial class PublishMarksheet : System.Web.UI.Page
    {
        int totsub = 0;
        int totprac = 0;
        string finalstatus = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 112))
            {

                if (!IsPostBack)
                {

                    string year = "";
                    string courseshortname = findCourseShortName(Session["CourseID"].ToString());
                    if (Session["Exam"].ToString() == "June 2011")
                    {
                        lblSession.Text = "June 2011";

                    }

                    else if (Session["Exam"].ToString() == "August 2011")
                    {
                        lblSession.Text = "August 2011";
                    }


                    totsub = getAndPopulateSubjects();
                    //totprac = getAndPopulatePracticals();
                    populateMarks(totsub, totprac);
                    if (Session["CourseYear"].ToString() == "1")
                    {
                        year = "FIRST YEAR";

                    }
                    if (Session["CourseYear"].ToString() == "2")
                    {
                        year = "SECOND YEAR";

                    }
                    if (Session["CourseYear"].ToString() == "3")
                    {
                        year = "THIRD YEAR";

                    }
                    if (Session["CourseYear"].ToString() == "4")
                    {
                        year = "FOURTH YEAR";

                    }



                    lblCourseFullName.Text = findCourseFullName(Session["CourseID"].ToString()) + " - " + year;

                    if (courseshortname == "PGDCA" || courseshortname == "DCA" || courseshortname == "DBA" || courseshortname == "PGDHM" || courseshortname == "BLIB" || courseshortname == "MLIB" || courseshortname == "PGDFSQM")
                    {
                        lblCourseFullName.Text = findCourseFullName(Session["CourseID"].ToString());

                    }

                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;

                //Log.createLogNow("Marksheet Publishing", "Published marks sheet of a student with Enrollment No '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private int getAndPopulatePracticals( int totalsub)
        {
           

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical where SyllabusSession='" + Session["SySession"].ToString() + "'and CourseName='" + Session["CourseName"].ToString() + "'and Year='" + Session["CourseYearAlpha"].ToString() + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("PracticalCode");
            DataColumn dtcol2 = new DataColumn("PracticalName");
            DataColumn dtcol3 = new DataColumn("PracticalMaxMarks");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);



            int totalsubject = totsub;

            while (dr.Read())
            {
                totalsubject = totalsubject + 1;
                totprac = totprac + 1;

                DataRow drow = dt.NewRow();
                drow["PracticalCode"] = Convert.ToString(dr["PracticalCode"]); 
                drow["PracticalName"] = Convert.ToString(dr["PracticalName"]);
                drow["PracticalMaxMarks"] = Convert.ToString(dr["PracticalMaxMarks"]);

                dt.Rows.Add(drow);


            }


            if(totalsub==4)
            {
                if (totprac == 0)
                {

                    TableRow5.Visible = false;
                    TableRow6.Visible = false;
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;
                    TableRow10.Visible = false;

                }

                else if (totprac == 1)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC10.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw1["PracticalMaxMarks"].ToString();

                    TableRow5.Visible = false;
                    TableRow6.Visible = false;
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;

                }

                else if(totprac==2)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC9.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac1.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks1.Text = dtrw1["PracticalMaxMarks"].ToString();

                    DataRow dtrw2 = dt.Rows[1];
                    lblCC10.Text = dtrw2["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw2["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw2["PracticalMaxMarks"].ToString();

                    TableRow5.Visible = false;
                    TableRow6.Visible = false;
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;

                }

            }

            else if (totalsub == 5)
            {


                if (totprac == 0)
                {

                    
                    TableRow6.Visible = false;
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;
                    TableRow10.Visible = false;
                }


               else if (totprac == 1)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC10.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw1["PracticalMaxMarks"].ToString();

                   
                    TableRow6.Visible = false;
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;


                }

                else if (totprac == 2)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC9.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac1.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks1.Text = dtrw1["PracticalMaxMarks"].ToString();

                    DataRow dtrw2 = dt.Rows[1];
                    lblCC10.Text = dtrw2["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw2["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw2["PracticalMaxMarks"].ToString();

                   
                    TableRow6.Visible = false;
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;




                }

            }

            else if (totalsub == 6)
            {

                if (totprac == 0)
                {

                    
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;
                    TableRow10.Visible = false;
                }

               else if (totprac == 1)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC10.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw1["PracticalMaxMarks"].ToString();


                   
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;


                }

                else if (totprac == 2)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC9.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac1.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks1.Text = dtrw1["PracticalMaxMarks"].ToString();

                    DataRow dtrw2 = dt.Rows[1];
                    lblCC10.Text = dtrw2["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw2["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw2["PracticalMaxMarks"].ToString();


                   
                    TableRow7.Visible = false;
                    TableRow8.Visible = false;




                }

            }

            else if (totalsub == 7)
            {
                if (totprac == 0)
                {


                    
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;
                    TableRow10.Visible = false;
                }

               else if (totprac == 1)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC10.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw1["PracticalMaxMarks"].ToString();


                   
                    TableRow8.Visible = false;
                    TableRow9.Visible = false;


                }

                else if (totprac == 2)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC9.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac1.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks1.Text = dtrw1["PracticalMaxMarks"].ToString();

                    DataRow dtrw2 = dt.Rows[1];
                    lblCC10.Text = dtrw2["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw2["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw2["PracticalMaxMarks"].ToString();


                    
                    TableRow8.Visible = false;




                }

            }

            else if (totalsub == 8)
            {

                if (totprac == 0)
                {


                   
                    TableRow9.Visible = false;
                    TableRow10.Visible = false;
                }

                else
                if (totprac == 1)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC10.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw1["PracticalMaxMarks"].ToString();


                   
                    TableRow9.Visible = false;


                }

                else if (totprac == 2)
                {
                    DataRow dtrw1 = dt.Rows[0];
                    lblCC9.Text = dtrw1["PracticalCode"].ToString();
                    lblPrac1.Text = dtrw1["PracticalName"].ToString();
                    lblMaxPracMarks1.Text = dtrw1["PracticalMaxMarks"].ToString();

                    DataRow dtrw2 = dt.Rows[1];
                    lblCC10.Text = dtrw2["PracticalCode"].ToString();
                    lblPrac2.Text = dtrw2["PracticalName"].ToString();
                    lblMaxPracMarks2.Text = dtrw2["PracticalMaxMarks"].ToString();

                }

            }

            con.Close();

            return totprac;

        }

        private int getAndPopulateSubjects()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SubjectCode,SubjectName from DDESubject where SyllabusSession='" + Session["SySession"].ToString() + "'and CourseName='" + Session["CourseName"].ToString() + "'and Year='" + Session["CourseYearAlpha"].ToString() + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SubjectCode");
            DataColumn dtcol2 = new DataColumn("SubjectName");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);




            while (dr.Read())
            {
                totsub = totsub + 1;

                DataRow drow = dt.NewRow();
                drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]); 
                drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);

                dt.Rows.Add(drow);


            }

          


            if (totsub == 4)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();

                totprac=getAndPopulatePracticals(totsub);




            }

            if (totsub == 5)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


                DataRow dtrw5 = dt.Rows[4];
                lblCC5.Text = dtrw5["SubjectCode"].ToString();
                lblSub5.Text = dtrw5["SubjectName"].ToString();


                totprac = getAndPopulatePracticals(totsub);

            }

            if (totsub == 6)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


                DataRow dtrw5 = dt.Rows[4];
                lblCC5.Text = dtrw5["SubjectCode"].ToString();
                lblSub5.Text = dtrw5["SubjectName"].ToString();


                DataRow dtrw6 = dt.Rows[5];
                lblCC6.Text = dtrw6["SubjectCode"].ToString();
                lblSub6.Text = dtrw6["SubjectName"].ToString();


                totprac = getAndPopulatePracticals(totsub);
            }

            else if (totsub == 7)
            {

                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


                DataRow dtrw5 = dt.Rows[4];
                lblCC5.Text = dtrw5["SubjectCode"].ToString();
                lblSub5.Text = dtrw5["SubjectName"].ToString();


                DataRow dtrw6 = dt.Rows[5];
                lblCC6.Text = dtrw6["SubjectCode"].ToString();
                lblSub6.Text = dtrw6["SubjectName"].ToString();


                DataRow dtrw7 = dt.Rows[6];
                lblCC7.Text = dtrw7["SubjectCode"].ToString();
                lblSub7.Text = dtrw7["SubjectName"].ToString();

                totprac = getAndPopulatePracticals(totsub);
            }


            else if (totsub == 8)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


                DataRow dtrw5 = dt.Rows[4];
                lblCC5.Text = dtrw5["SubjectCode"].ToString();
                lblSub5.Text = dtrw5["SubjectName"].ToString();


                DataRow dtrw6 = dt.Rows[5];
                lblCC6.Text = dtrw6["SubjectCode"].ToString();
                lblSub6.Text = dtrw6["SubjectName"].ToString();


                DataRow dtrw7 = dt.Rows[6];
                lblCC7.Text = dtrw7["SubjectCode"].ToString();
                lblSub7.Text = dtrw7["SubjectName"].ToString();

                DataRow dtrw8 = dt.Rows[7];
                lblCC8.Text = dtrw8["SubjectCode"].ToString();
                lblSub8.Text = dtrw8["SubjectName"].ToString();

                totprac = getAndPopulatePracticals(totsub);

            }



            con.Close();

            return totsub;
            
        }

       




        private void populateMarks(int tsub, int tprac)
        {
            //try
            //{
            //Response.Write("select * from " + ddlistCourse.SelectedItem.Text + "-" + ddlistYear.SelectedItem.Value + " where EnrollmentNo='" + tbEnrolNo.Text + "'");

            SqlConnection con;
            if (FindInfo.validExamForAugust2011(Session["Exam"].ToString(), Session["CourseYearAlpha"].ToString(), findCourse(Session["CourseID"].ToString())))
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb2"].ToString());
            }

            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            }
            
                
            SqlCommand cmd = new SqlCommand("select * from " + findCourse(Session["CourseID"].ToString())  + "_" + Session["CourseYear"].ToString() + " where Enrollment_No='" + Session["EnrollmentNo"].ToString() + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                lblSName.Text = dr["Student_Name"].ToString();
                lblRNo.Text = dr["Roll_No"].ToString();
                lblFName.Text = dr["Father_Name"].ToString();
                lblENo.Text = dr["Enrollment_No"].ToString();
                lblSCCode.Text = dr["SC_Code"].ToString();

                lblTheory1.Text = getMarksifPresent(dr["a_Theory"].ToString());
                lblIA1.Text = getMarksifPresent(dr["a_IA"].ToString());
                lblAW1.Text = getMarksifPresent(dr["a_AW"].ToString());
                lblTotal1.Text = Convert.ToString(getMarks(dr["a_Theory"].ToString()) + getMarks(dr["a_IA"].ToString()) + getMarks(dr["a_AW"].ToString()));
                lblGrade1.Text = findGrade(lblTotal1.Text);
                lblStatus1.Text = findStatus(lblTheory1.Text, lblIA1.Text, lblAW1.Text);


                lblTheory2.Text = getMarksifPresent(dr["b_Theory"].ToString());
                lblIA2.Text = getMarksifPresent(dr["b_IA"].ToString());
                lblAW2.Text = getMarksifPresent(dr["b_AW"].ToString());
                lblTotal2.Text = Convert.ToString(getMarks(dr["b_Theory"].ToString()) + getMarks(dr["b_IA"].ToString()) + getMarks(dr["b_AW"].ToString()));
                lblGrade2.Text = findGrade(lblTotal2.Text);
                lblStatus2.Text = findStatus(lblTheory2.Text, lblIA2.Text, lblAW2.Text);


                lblTheory3.Text = getMarksifPresent(dr["c_Theory"].ToString());
                lblIA3.Text = getMarksifPresent(dr["c_IA"].ToString());
                lblAW3.Text = getMarksifPresent(dr["c_AW"].ToString());
                lblTotal3.Text = Convert.ToString(getMarks(dr["c_Theory"].ToString()) + getMarks(dr["c_IA"].ToString()) + getMarks(dr["c_AW"].ToString()));
                lblGrade3.Text = findGrade(lblTotal3.Text);
                lblStatus3.Text = findStatus(lblTheory3.Text, lblIA3.Text, lblAW3.Text);


                lblTheory4.Text =getMarksifPresent( dr["d_Theory"].ToString());
                lblIA4.Text = getMarksifPresent(dr["d_IA"].ToString());
                lblAW4.Text = getMarksifPresent(dr["d_AW"].ToString());
                lblTotal4.Text = Convert.ToString(getMarks(dr["d_Theory"].ToString()) + getMarks(dr["d_IA"].ToString()) + getMarks(dr["d_AW"].ToString()));
                lblGrade4.Text = findGrade(lblTotal4.Text);
                lblStatus4.Text = findStatus(lblTheory4.Text, lblIA4.Text, lblAW4.Text);

                

                if(tsub==5)
                {
                    lblTheory5.Text = getMarksifPresent(dr["e_Theory"].ToString());
                    lblIA5.Text =getMarksifPresent( dr["e_IA"].ToString());
                    lblAW5.Text = getMarksifPresent(dr["e_AW"].ToString());
                    lblTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                    lblGrade5.Text = findGrade(lblTotal5.Text);
                    lblStatus5.Text = findStatus(lblTheory5.Text, lblIA5.Text, lblAW5.Text);

                }


                else if (tsub == 6)
                {
                    lblTheory5.Text = getMarksifPresent(dr["e_Theory"].ToString());
                    lblIA5.Text = getMarksifPresent(dr["e_IA"].ToString());
                    lblAW5.Text = getMarksifPresent(dr["e_AW"].ToString());
                    lblTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                    lblGrade5.Text = findGrade(lblTotal5.Text);
                    lblStatus5.Text = findStatus(lblTheory5.Text, lblIA5.Text, lblAW5.Text);


                    lblTheory6.Text = getMarksifPresent(dr["f_Theory"].ToString());
                    lblIA6.Text =getMarksifPresent( dr["f_IA"].ToString());
                    lblAW6.Text = getMarksifPresent(dr["f_AW"].ToString());
                    lblTotal6.Text = Convert.ToString(getMarks(dr["f_Theory"].ToString()) + getMarks(dr["f_IA"].ToString()) + getMarks(dr["f_AW"].ToString()));
                    lblGrade6.Text = findGrade(lblTotal6.Text);
                    lblStatus6.Text = findStatus(lblTheory6.Text, lblIA6.Text, lblAW6.Text);

                }


               else if (tsub == 7)
                {
                    lblTheory5.Text =getMarksifPresent( dr["e_Theory"].ToString());
                    lblIA5.Text = getMarksifPresent(dr["e_IA"].ToString());
                    lblAW5.Text = getMarksifPresent(dr["e_AW"].ToString());
                    lblTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                    lblGrade5.Text = findGrade(lblTotal5.Text);
                    lblStatus5.Text = findStatus(lblTheory5.Text, lblIA5.Text, lblAW5.Text);


                    lblTheory6.Text = getMarksifPresent(dr["f_Theory"].ToString());
                    lblIA6.Text = getMarksifPresent(dr["f_IA"].ToString());
                    lblAW6.Text = getMarksifPresent(dr["f_AW"].ToString());
                    lblTotal6.Text = Convert.ToString(getMarks(dr["f_Theory"].ToString()) + getMarks(dr["f_IA"].ToString()) + getMarks(dr["f_AW"].ToString()));
                    lblGrade6.Text = findGrade(lblTotal6.Text);
                    lblStatus6.Text = findStatus(lblTheory6.Text, lblIA6.Text, lblAW6.Text);


                    lblTheory7.Text = getMarksifPresent(dr["g_Theory"].ToString());
                    lblIA7.Text = getMarksifPresent(dr["g_IA"].ToString());
                    lblAW7.Text = getMarksifPresent(dr["g_AW"].ToString());
                    lblTotal7.Text = Convert.ToString(getMarks(dr["g_Theory"].ToString()) + getMarks(dr["g_IA"].ToString()) + getMarks(dr["g_AW"].ToString()));
                    lblGrade7.Text = findGrade(lblTotal7.Text);
                    lblStatus7.Text = findStatus(lblTheory7.Text, lblIA7.Text, lblAW7.Text);
                   


                }

                else if (tsub == 8)
                {
                    lblTheory5.Text = getMarksifPresent(dr["e_Theory"].ToString());
                    lblIA5.Text = getMarksifPresent(dr["e_IA"].ToString());
                    lblAW5.Text = getMarksifPresent(dr["e_AW"].ToString());
                    lblTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                    lblGrade5.Text = findGrade(lblTotal5.Text);
                    lblStatus5.Text = findStatus(lblTheory5.Text, lblIA5.Text, lblAW5.Text);

                    lblTheory6.Text = getMarksifPresent(dr["f_Theory"].ToString());
                    lblIA6.Text = getMarksifPresent(dr["f_IA"].ToString());
                    lblAW6.Text = getMarksifPresent(dr["f_AW"].ToString());
                    lblTotal6.Text = Convert.ToString(getMarks(dr["f_Theory"].ToString()) + getMarks(dr["f_IA"].ToString()) + getMarks(dr["f_AW"].ToString()));
                    lblGrade6.Text = findGrade(lblTotal6.Text);
                    lblStatus6.Text = findStatus(lblTheory6.Text, lblIA6.Text, lblAW6.Text);


                    lblTheory7.Text =getMarksifPresent( dr["g_Theory"].ToString());
                    lblIA7.Text = getMarksifPresent(dr["g_IA"].ToString());
                    lblAW7.Text = getMarksifPresent(dr["g_AW"].ToString());
                    lblTotal7.Text = Convert.ToString(getMarks(dr["g_Theory"].ToString()) + getMarks(dr["g_IA"].ToString()) + getMarks(dr["g_AW"].ToString()));
                    lblGrade7.Text = findGrade(lblTotal7.Text);
                    lblStatus7.Text = findStatus(lblTheory7.Text, lblIA7.Text, lblAW7.Text);


                    lblTheory8.Text = getMarksifPresent(dr["h_Theory"].ToString());
                    lblIA8.Text = getMarksifPresent(dr["h_IA"].ToString());
                    lblAW8.Text = getMarksifPresent(dr["h_AW"].ToString());
                    lblTotal8.Text = Convert.ToString(getMarks(dr["h_Theory"].ToString()) + getMarks(dr["h_IA"].ToString()) + getMarks(dr["h_AW"].ToString()));
                    lblGrade8.Text = findGrade(lblTotal8.Text);
                    lblStatus8.Text = findStatus(lblTheory8.Text, lblIA8.Text, lblAW8.Text);
                   


                }

                if (tprac == 1)
                {
                    lblPracMaksObtained1.Visible = false;
                    lblPracMaksObtained2.Text =getMarksifPresent(dr["Prac_Marks1"].ToString());
                    lblGrade10.Text = findPracGrade(lblPracMaksObtained2.Text, lblMaxPracMarks2.Text);
                    lblStatus10.Text = findPracStatus(lblPracMaksObtained2.Text, lblMaxPracMarks2.Text);

                }

                else if (tprac == 2)
                {
                    lblPracMaksObtained1.Text = getMarksifPresent(dr["Prac_Marks1"].ToString());
                    lblPracMaksObtained2.Text = getMarksifPresent(dr["Prac_Marks2"].ToString());

                    lblGrade9.Text = findPracGrade(lblPracMaksObtained1.Text, lblMaxPracMarks1.Text);
                    lblStatus9.Text = findPracStatus(lblPracMaksObtained1.Text, lblMaxPracMarks1.Text);

                    lblGrade10.Text = findPracGrade(lblPracMaksObtained2.Text, lblMaxPracMarks2.Text);
                    lblStatus10.Text = findPracStatus(lblPracMaksObtained2.Text, lblMaxPracMarks2.Text);


                }

               
            }

            lblGTMMarks.Text = Convert.ToString(tsub * 100 + getMarks(lblMaxPracMarks1.Text) + getMarks(lblMaxPracMarks2.Text));
            lblGrandTotal.Text = Convert.ToString(getMarks(lblTotal1.Text) + getMarks(lblTotal2.Text) + getMarks(lblTotal3.Text) + getMarks(lblTotal4.Text) + getMarks(lblTotal5.Text) + getMarks(lblTotal6.Text) + getMarks(lblTotal7.Text) + getMarks(lblTotal8.Text) + getMarks(lblPracMaksObtained1.Text) + getMarks(lblPracMaksObtained2.Text));


            lblGrade11.Text = findPracGrade(lblGrandTotal.Text, lblGTMMarks.Text);
            lblStatus11.Text = findFinalStatus();
            lblResult.Text = findResultStatus(Convert.ToInt32(lblGTMMarks.Text) , Convert.ToInt32(lblGrandTotal.Text) );
            lblDivision.Text = findDivision(Convert.ToInt32(lblGTMMarks.Text), Convert.ToInt32(lblGrandTotal.Text));


            con.Close();

            //catch
            //{
            //    Response.Write("<script>alert('Sorry !! no record found')</script>");
            //}
        }

        private string getMarksifPresent(string marks)
        {
            if (marks == "")
            {
                return "AB";
            }
            else if (marks == "A")
            {
                return "AB";
            }

            else return marks;
          
        }

        private string findFinalStatus()
        {
            if (lblStatus1.Text == "NC" || lblStatus2.Text == "NC" || lblStatus3.Text == "NC" || lblStatus4.Text == "NC" || lblStatus4.Text == "NC" || lblStatus5.Text == "NC" || lblStatus6.Text == "NC" || lblStatus7.Text == "NC" || lblStatus8.Text == "NC" || lblStatus9.Text == "NC" || lblStatus10.Text == "NC")
          {
              return "NC";

          }

            else
          {
              return "CC";
          }
             
        }

        private string findDivision(int maxmarks, int marksobtained)
        {
            string div = "";


            int percent = (marksobtained * 100) / maxmarks;

            if (lblStatus11.Text == "CC")
            {


                if (percent >= 60)
                {
                    div = "First";

                }

                else if (percent < 60 && percent >= 45)
                {


                    div = "Second";

                }

                else if (percent < 45)
                {


                    div = "Thired";

                }
            }

            else if (lblStatus11.Text == "NC")
            {
                div = "XX";


            }


            return div;

            
        }

        private string findResultStatus(int  maxmarks, int marksobtained)
        {
            string status = "";


            int percent = (marksobtained * 100 ) / maxmarks;

            if (lblStatus11.Text == "CC")
            {
                status = "Pass";
                 
            }

            else if (lblStatus11.Text == "NC")
            {


                status = "Not Cleared";

            }


            return status;
        }



        private Int32 getMarks(string marks)
        {
            if (marks == "" || marks == "-" || marks == "A" || marks == "AB" || marks == "NF" || marks == "*")
            {
                return 0;
            }    

            else return Convert.ToInt32(marks);


        }


        private string findCourse(string courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select CourseShortName,Specialization from DDECourse where CourseID='" + courseid + "'", con);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            string course = "";

            while (dr.Read())
            {
                if (dr[1].ToString() == "")
                {

                    course = dr[0].ToString();
                }

                else
                {

                    course = dr[0].ToString() + "_" + findSpecialisation(dr[1].ToString());
                }




            }
            con.Close();

            return course;



        }

        private string findSpecialisation(string sp)
        {
            string oldstr = sp.Substring(0, 1);
            string newstr = oldstr;
            int i = 1;
            while (oldstr != "")
            {
                try
                {
                    oldstr = sp.Substring(i, 1);
                    newstr = newstr + findchar(oldstr);
                    i++;
                }
                catch
                {
                    return newstr;
                }


            }

            return newstr;

        }

        private string findchar(string oldstr)
        {
            if (oldstr == "&")
            {
                return "_";
            }

            else if (oldstr == ".")
            {
                return "";
            }
            else if (oldstr == " ")
            {
                return "";
            }

            else return oldstr;
        }

        private string findCourseShortName(string courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select CourseShortName from DDECourse where CourseID='" + courseid + "'", con);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            string course = "";

            while (dr.Read())
            {
                course = dr[0].ToString();



            }
            con.Close();

            return course;
            
        }

        private string findCourseFullName(string courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select CourseFullName from DDECourse where CourseID='" + courseid + "'", con);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            string course = "";

            while (dr.Read())
            {
                course = dr[0].ToString();



            }
            con.Close();

            return course;

        }


        private string findStatus(string tee, string ia, string aw)
        {

            string status = "";

            int teepercent = (getMarks(tee) * 100) / 60;
            int iapercent = (getMarks(ia) * 100) / 20;
            int awpercent = (getMarks(aw) * 100) / 20;

            if (teepercent < 40 || iapercent < 40 || awpercent < 40)
            {
                status = "NC";

            }

            else
            {
                status = "CC";
            }

            return status;



        }


        private string findPracStatus(string pracmarksobtained, string maxpracmarks)
        {
            string status = "";


            int pracpercent = (getMarks(pracmarksobtained) *100  /getMarks(maxpracmarks));

            if (pracpercent < 40)
            {
               status = "NC";
            }

            else
            {
               status = "CC";
            }

            return status;


        }



        private string findGrade(string total)
        {
            string grade = "";

            int percent = (getMarks(total) * 100) / 100;

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

        private string findPracGrade(string pracmarksobtained, string maxpracmarks)
        {
            string grade = "";

            int percent = (getMarks(pracmarksobtained) * 100) / getMarks(maxpracmarks);

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

        
    }
    }

