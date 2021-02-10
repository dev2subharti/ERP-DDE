using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DDE.DAL;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class FillBPExamFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 38))
            {
                if (!IsPostBack)
                {
                    populateExam();
                    populateCourses();
                    populateSessions();
                    populateSySessions();
                    
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

        private void populateExam()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCode,ExamName from DDEExaminations ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistExam.Items.Add(dr["ExamName"].ToString());
                ddlistExam.Items.FindByText(dr["ExamName"].ToString()).Value = dr["ExamCode"].ToString();
              

            }

            con.Close();


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

            populateStudents();
            findFee();

        }

        private void fillSetExamCentres(DataTable dt1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select City from CityList order by City ", con);
            SqlDataReader dr;

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("City");
            dt.Columns.Add(dtcol1);


            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();

                drow["City"] = Convert.ToString(dr["City"]);
                dt.Rows.Add(drow);
            }


            con.Close();

            int j = 0;
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {

                Label srid = (Label)dli.FindControl("lblSRID");
                DropDownList ec = (DropDownList)dli.FindControl("ddlistExamCentre");
                DropDownList jone = (DropDownList)dli.FindControl("ddlistJone");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ec.Items.Add(dt.Rows[i]["City"].ToString());

                }
                if (dt1.Rows[j]["ExamCity"].ToString() != "")
                {
                    ec.Items.FindByText(dt1.Rows[j]["ExamCity"].ToString()).Selected = true;
                }

                if (dt1.Rows[j]["ExamJone"].ToString() != "")
                {

                    jone.Items.FindByText(dt1.Rows[j]["ExamJone"].ToString()).Selected = true;
                }
                j++;
            }


        }



        private void popuLateExamFee(DataRow drow, int srid)
        {
            string colEF = "BPExamFee_" + ddlistExam.SelectedItem.Value;
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + colEF + " from DDEFeeRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            drow["BPExamFee"] = dr[0].ToString();
           
            con.Close();

        }

        private bool feeFilled(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from DDEFeeRecord", con);
            con.Open();
            dr = cmd.ExecuteReader();

            bool feefilled = false;
            while (dr.Read())
            {

                if (Convert.ToInt32(dr[0]) == srid)
                {

                    feefilled = true;
                    break;

                }

            }

            con.Close();

            return feefilled;

        }



        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ", con);
           
            SqlDataReader dr;

            if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
            {
                if (ddlistYear.SelectedItem.Value == "1")
                {
                    cmd.CommandText = "Select SRID from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "2")
                {
                    cmd.CommandText = "Select SRID from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "'and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "3")
                {
                    cmd.CommandText = "Select SRID from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "'and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
                }

            }
            else
            {
                cmd.CommandText = "Select SRID from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "'and QualifyingStatus='PCP' and RecordStatus='True' order by StudentName ";
            }

            con.Open();
            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("Batch");
            DataColumn dtcol9 = new DataColumn("BPExamFee");
            DataColumn dtcol10 = new DataColumn("LateExamFee");
            DataColumn dtcol11 = new DataColumn("ExamCity");
            DataColumn dtcol12 = new DataColumn("ExamJone");
            DataColumn dtcol13 = new DataColumn("FeeFilled");
            DataColumn dtcol14 = new DataColumn("Subjects1");
            DataColumn dtcol15 = new DataColumn("Subjects2");
            DataColumn dtcol16 = new DataColumn("Subjects3");


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
            dt.Columns.Add(dtcol16);

           
            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                populateStudentDetail(drow, Convert.ToInt32(dr["SRID"]));
                if (drow["Course"].ToString() == ddlistCourse.SelectedItem.Value)
                {
                    if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Value)
                    {
                       
                            drow["SNo"] = i;
                            drow["SRID"] = Convert.ToString(dr["SRID"]);

                            if (feeFilled(Convert.ToInt32(dr["SRID"])))
                            {
                                drow["FeeFilled"] = "True";
                                popuLateExamFee(drow, Convert.ToInt32(dr["SRID"]));
                            }

                            else
                            {
                                drow["FeeFilled"] = "False";
                                drow["BPExamFee"] = "";
                                drow["LateExamFee"] = "";

                            }
                            popuLateExamCentre(drow, Convert.ToInt32(dr["SRID"]));
                            dt.Rows.Add(drow);
                            i = i + 1;
                      
                    }
                }
            }



            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            

            con.Close();

            fillSetSubjects(dt);
            fillSetExamCentres(dt);

            if (i > 1)
            {
                dtlistShowStudents.Visible = true;
                btnSubmit.Visible = true;
                btnOK.Visible = false;
                pnlMSG.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

        }

        private void populateStudentDetail(DataRow drow, int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select StudyCentreCode,EnrollmentNo,StudentName,FatherName,Course,CYear,Session from DDEStudentRecord where SRID='" + srid + "' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["Course"] = Convert.ToInt32(dr["Course"]);
                drow["Batch"] = Convert.ToString(dr["Session"]);
            }
            con.Close();
        }

        private void fillSetSubjects(DataTable dto)
        {
            DataTable dt1 = fillSubjects("1st Year");
            DataTable dt2 = fillSubjects("2nd Year");
            DataTable dt3 = fillSubjects("3rd Year");
            

            int j = 0;
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {

                Label srid = (Label)dli.FindControl("lblSRID");
                CheckBoxList sub1 = (CheckBoxList)dli.FindControl("CBLSubjects1");
                CheckBoxList sub2 = (CheckBoxList)dli.FindControl("CBLSubjects2");
                CheckBoxList sub3 = (CheckBoxList)dli.FindControl("CBLSubjects3");
               
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    sub1.Items.Add(dt1.Rows[i]["SubjectSNo"].ToString());
                    sub1.Items.FindByText(dt1.Rows[i]["SubjectSNo"].ToString()).Value = dt1.Rows[i]["SubjectID"].ToString();
                }
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sub2.Items.Add(dt2.Rows[i]["SubjectSNo"].ToString());
                    sub2.Items.FindByText(dt2.Rows[i]["SubjectSNo"].ToString()).Value = dt2.Rows[i]["SubjectID"].ToString();
                }
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    sub3.Items.Add(dt3.Rows[i]["SubjectSNo"].ToString());
                    sub3.Items.FindByText(dt3.Rows[i]["SubjectSNo"].ToString()).Value = dt3.Rows[i]["SubjectID"].ToString();
                }


                if (dto.Rows[j]["Subjects1"].ToString() != "")
                {
                    string[] sublist = dto.Rows[j]["Subjects1"].ToString().Split(',');
                    for (int k = 0; k <sublist.Length;k++ )
                    {
                        sub1.Items.FindByValue(sublist[k]).Selected = true;
                    }
                }
                if (dto.Rows[j]["Subjects2"].ToString() != "")
                {
                    string[] sublist = dto.Rows[j]["Subjects2"].ToString().Split(',');
                    for (int k = 0; k < sublist.Length; k++)
                    {
                        sub2.Items.FindByValue(sublist[k]).Selected = true;
                    }
                }
                if (dto.Rows[j]["Subjects3"].ToString() != "")
                {
                    string[] sublist = dto.Rows[j]["Subjects3"].ToString().Split(',');
                    for (int k = 0; k < sublist.Length; k++)
                    {
                        sub3.Items.FindByValue(sublist[k]).Selected = true;
                    }
                }
     
                j++;
            }

            
        }

        private DataTable fillSubjects(string year)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            cmd.Connection = con;
            if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA" && year == "1st Year")
            {
                cmd.CommandText="Select SubjectSNo,SubjectID from DDESubject where CourseName='MBA' and Year='" + year + "' and SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' order by SubjectSNo";
            }
            else
            {
                cmd.CommandText = "Select SubjectSNo,SubjectID from DDESubject where CourseName='" + ddlistCourse.SelectedItem.Text + "' and Year='" + year + "' and SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' order by SubjectSNo";
            }
           

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SubjectSNo");
            DataColumn dtcol2 = new DataColumn("SubjectID");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);


            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                dt.Rows.Add(drow);
            }


            con.Close();

            return dt;
        }

      

        private void popuLateExamCentre(DataRow drow, int srid)
        {
            string colECity = "ExamCentreCity_" + ddlistExam.SelectedItem.Value;
            string colEJone = "ExamCentreJone_" + ddlistExam.SelectedItem.Value;
            string colSubjects1 = "BPSubjects1_" + ddlistExam.SelectedItem.Value;
            string colSubjects2 = "BPSubjects2_" + ddlistExam.SelectedItem.Value;
            string colSubjects3 = "BPSubjects3_" + ddlistExam.SelectedItem.Value;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + colECity + "," + colEJone + "," + colSubjects1 + "," + colSubjects2 + "," + colSubjects3 + " from DDEExamRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                drow["ExamCity"] = dr[0].ToString();
                drow["ExamJone"] = dr[1].ToString();
                drow["Subjects1"] = dr[2].ToString();
                drow["Subjects2"] = dr[3].ToString();
                drow["Subjects3"] = dr[4].ToString();
            }
            con.Close();


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string colEF = "ExamFee_" + ddlistExam.SelectedItem.Value;
            string colLEF = "LateExamFee_" + ddlistExam.SelectedItem.Value;
            string colBPEF = "BPExamFee_" + ddlistExam.SelectedItem.Value;

            string colRollNo = "RollNo_" + ddlistExam.SelectedItem.Value;
            string colBPRollNo = "BPRollNo_" + ddlistExam.SelectedItem.Value;
            string colECID = "ExamCentreID_" + ddlistExam.SelectedItem.Value;
            string colECity = "ExamCentreCity_" + ddlistExam.SelectedItem.Value;
            string colEJone = "ExamCentreJone_" + ddlistExam.SelectedItem.Value;
            string colSubjects1 = "BPSubjects1_" + ddlistExam.SelectedItem.Value;
            string colSubjects2 = "BPSubjects2_" + ddlistExam.SelectedItem.Value;
            string colSubjects3 = "BPSubjects3_" + ddlistExam.SelectedItem.Value;

            if (validAmount())
            {
                foreach (DataListItem dli in dtlistShowStudents.Items)
                {

                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label eno = (Label)dli.FindControl("lblENo");

                    Label lblff = (Label)dli.FindControl("lblFeeFilled");
                    TextBox bpef = (TextBox)dli.FindControl("tbBPExamFee");
                   
                    Label bplef = (Label)dli.FindControl("lblBPExamFee");
                    CheckBoxList sub1 = (CheckBoxList)dli.FindControl("CBLSubjects1");
                    CheckBoxList sub2 = (CheckBoxList)dli.FindControl("CBLSubjects2");
                    CheckBoxList sub3 = (CheckBoxList)dli.FindControl("CBLSubjects3");
                    DropDownList ec = (DropDownList)dli.FindControl("ddlistExamCentre");
                    DropDownList jone = (DropDownList)dli.FindControl("ddlistJone");

                    string subjectlist1 = "";
                    string subjectlist2 = "";
                    string subjectlist3 = "";

                    subjectlist1 = findSubjectList(subjectlist1,sub1);
                    subjectlist2 = findSubjectList(subjectlist2, sub2);
                    subjectlist3 = findSubjectList(subjectlist3, sub3);

                    

                    if (lblff.Text == "False")
                    {


                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDEFeeRecord values(@SRID,@" + colEF + ",@" + colLEF + ",@" + colBPEF +")", con);


                        cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));

                        cmd.Parameters.AddWithValue("@" + colEF, "");
                        cmd.Parameters.AddWithValue("@" + colLEF, "");
                        cmd.Parameters.AddWithValue("@" + colBPEF, bpef.Text);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        int courseid = FindInfo.findCourseIDBySRID(Convert.ToInt32(srid.Text));
                        int counter = FindInfo.findRollNoCounter(courseid,ddlistExam.SelectedItem.Value,"B");
                        int newcounter = counter + 1;
                        string sno = string.Format("{0:0000}", newcounter);
                        string rollno = "XA12" + FindInfo.findCourseCodeByID(courseid) + sno;

                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd1 = new SqlCommand("insert into DDEExamRecord values(@SRID,@" + colRollNo + ",@" + colECID + ",@" + colECity + ",@" + colEJone + ",@" + colBPRollNo + ",@" + colSubjects1 + ",@" + colSubjects2 + ",@" + colSubjects3 + ")", con1);

                        cmd1.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));
                        cmd1.Parameters.AddWithValue("@" + colRollNo, "");
                        cmd1.Parameters.AddWithValue("@" + colECID, "");
                        cmd1.Parameters.AddWithValue("@" + colECity, ec.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@" + colEJone, jone.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@" + colBPRollNo, rollno);
                        cmd1.Parameters.AddWithValue("@" + colSubjects1,subjectlist1);
                        cmd1.Parameters.AddWithValue("@" + colSubjects2, subjectlist2);
                        cmd1.Parameters.AddWithValue("@" + colSubjects3, subjectlist3);

                        cmd1.Connection = con1;
                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();

                        FindInfo.updateRollNoCounter(courseid, newcounter,ddlistExam.SelectedItem.Value);

                        if (bpef.Text != "")
                        {

                            Log.createLogNow("Fee Filling", "Filled Back Paper Fee of a student with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        }
                    }

                    else if (lblff.Text == "True")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEFeeRecord set " + colBPEF + "=@" + colBPEF + " where SRID='" + srid.Text + "'", con);
                        cmd.Parameters.AddWithValue("@" + colBPEF, bpef.Text);
                  


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd1 = new SqlCommand("update DDEExamRecord set " + colECity + "=@" + colECity + "," + colEJone + "=@" + colEJone + "," + colBPRollNo + "=@" + colBPRollNo + "," + colSubjects1 + "=@" + colSubjects1 + "," + colSubjects2 + "=@" + colSubjects2 + "," + colSubjects3 + "=@" + colSubjects3 + " where SRID='" + srid.Text + "'", con1);

                        cmd1.Parameters.AddWithValue("@" + colECity, ec.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@" + colEJone, jone.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@" + colBPRollNo, "X" + FindInfo.findRollNoBySRID(Convert.ToInt32(srid.Text), ddlistExam.SelectedItem.Value,"R"));
                        cmd1.Parameters.AddWithValue("@" + colSubjects1, subjectlist1);
                        cmd1.Parameters.AddWithValue("@" + colSubjects2, subjectlist2);
                        cmd1.Parameters.AddWithValue("@" + colSubjects3, subjectlist3);
                       


                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();

                        if (bplef.Text != bpef.Text)
                        {
                            Log.createLogNow("Fee Updation", "Updated fee of a student with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        }
                    }

                }

                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Fee have been submitted successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Invalid Amount is filled at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check Back Paper Exam Fee should be " + Session["BPExamFee"].ToString();
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }
        }

        private string findSubjectList(string subjectlist, CheckBoxList sub)
        {
           
            foreach (ListItem sublist in sub.Items)
            {

                if (sublist.Selected)
                {
                    if (subjectlist == "")
                    {
                        subjectlist = sublist.Value;
                    }
                    else if (subjectlist != "")
                    {
                        subjectlist = subjectlist + "," + sublist.Value;
                    }
                  
                }
            }
            return subjectlist;
        }


        private void findFee()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select FeeAmount from DDEFee where FeeHead='BACK PAPER EXAM FEE' ", con);
          
            SqlDataReader dr;
           

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            Session["BPExamFee"] = Convert.ToInt32(dr[0]);
            con.Close();

        }

        private bool validAmount()
        {

            bool validamount = false;
            int SNo = 0;
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                try
                {
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    TextBox bpef = (TextBox)dli.FindControl("tbBPExamFee");
                    CheckBoxList sub1 = (CheckBoxList)dli.FindControl("CBLSubjects1");
                    CheckBoxList sub2 = (CheckBoxList)dli.FindControl("CBLSubjects2");
                    CheckBoxList sub3 = (CheckBoxList)dli.FindControl("CBLSubjects3");

                    SNo = Convert.ToInt32(sno.Text);

                    int examfee;

                   
                    int totalsub = findTotalSelectedSub(sub1,sub2,sub3);
                   

                    int totalamount = totalsub * Convert.ToInt32(Session["BPExamFee"]);

                    if (bpef.Text == "")
                    {
                        examfee = 0;
                    }

                    else
                    {
                        examfee = Convert.ToInt32(bpef.Text);
                    }


                    if (totalsub != 0)
                    {

                        if (examfee == totalamount)
                        {
                            validamount = true;

                        }
                        else
                        {
                            validamount = false;
                            Session["SNo"] = SNo;
                            break;
                        }
                    }
                    else if (totalsub == 0)
                    {
                        if (examfee == 0)
                        {
                            validamount = true;

                        }
                        else
                        {
                            validamount = false;
                            Session["SNo"] = SNo;
                            break;
                        }
                    }

                    
                }

                catch
                {
                    validamount = false;
                    Session["SNo"] = SNo;
                    break;
                }


            }

            return validamount;

        }

        private int findTotalSelectedSub(CheckBoxList sub1, CheckBoxList sub2, CheckBoxList sub3)
        {
            int totalsub = 0;
            foreach (ListItem sublist in sub1.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }
            foreach (ListItem sublist in sub2.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }
            foreach (ListItem sublist in sub3.Items)
            {

                if (sublist.Selected)
                {
                    totalsub = totalsub + 1;
                }
            }

            return totalsub;
        }






        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            dtlistShowStudents.Visible = true;
            btnSubmit.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;

        }


        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistSySession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistYear_SelectedIndexChanged1(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistPreviousExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }
    }
}
