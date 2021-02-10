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
using System.Data.SqlClient;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class FillTheoryMarks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 29))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("A13").Selected = true;
                    PopulateDDList.populateCourses(ddlistCourse);
                    PopulateDDList.populateSySession(ddlistSySession);
                  
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectID,SubjectName from DDESubject where SyllabusSession='"+ddlistSySession.SelectedItem.Text+"' and CourseName='"+ddlistCourse.SelectedItem.Text+"' and Year='"+ddlistYear.SelectedItem.Text+"'", con);
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
            populateStudents();
            setAccessibility();
           
        }

        private void setAccessibility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) && !Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {

                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox theory = (TextBox)dli.FindControl("tbTheory");


                    if (theory.Text == "")
                    {

                        theory.Enabled = true;

                    }

                    else
                    {
                        theory.Enabled = false;
                    }

                }


            }
            else if (!Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {

                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox theory = (TextBox)dli.FindControl("tbTheory");


                    if (theory.Text == "")
                    {
                        theory.Enabled = false;

                    }

                    else
                    {
                        theory.Enabled = true;
                    }

                }


            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {

                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox theory = (TextBox)dli.FindControl("tbTheory");
                    theory.Enabled = true;

                }

            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 29))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {
                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox theory = (TextBox)dli.FindControl("tbTheory");
                    theory.Enabled = false;
                  
                }

            }

        }
       
        private string populateMarks(int srid, int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select Theory from DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string marks =fillMarks(dr[0].ToString());
            con.Close();
            return marks;       
                     
        }

        private string fillMarks(string tmarks)
        {
            string marks = "";
            if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10")
            {
                if (tmarks == "")
                {
                    marks = "";
                }
                else if (tmarks == "AB")
                {
                    marks = "AB";
                }
                else
                {
                    int fmarks = ((Convert.ToInt32(tmarks) * 60) % 100);
                    {
                        if (fmarks >= 5)
                        {
                            marks = (((Convert.ToInt32(tmarks) * 60) / 100) + 1).ToString();
                        }

                        else
                        {
                            marks = ((Convert.ToInt32(tmarks) * 60) / 100).ToString();
                        }
                    }
                   
                }
            }
            else
            {
                marks = tmarks;
            }

            return marks;
        }

        private bool marksFilled(int srid, int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " where SRID='"+srid+"' and SubjectID='"+subid+"' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            bool marksfilled=false;
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
            SqlCommand cmd =new SqlCommand();

           

                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                {
                    if (ddlistYear.SelectedItem.Value == "1")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                    }
                    else if (ddlistYear.SelectedItem.Value == "2")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                    }
                    else if (ddlistYear.SelectedItem.Value == "3")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                    }

                }
                else
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
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
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("SCCode");
            DataColumn dtcol7 = new DataColumn("MarksFilled");
            DataColumn dtcol8 = new DataColumn("RollNo");
            DataColumn dtcol9 = new DataColumn("RNAllotted");        
            DataColumn dtcol10 = new DataColumn("TheoryMarks");
           


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
                if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10" || ddlistExam.SelectedItem.Value == "A11" || ddlistExam.SelectedItem.Value == "B11" || ddlistExam.SelectedItem.Value == "A12" || ddlistExam.SelectedItem.Value=="B12")
                {
                    if (FindInfo.examFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]),ddlistYear.SelectedItem.Value, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
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

                        string rollno;
                        if (FindInfo.rollnoAlltted(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out rollno))
                        {
                            drow["RNAllotted"] = "True";
                            drow["RollNo"] = rollno;

                        }
                        else
                        {
                            drow["RNAllotted"] = "False";
                            drow["RollNo"] = "";
                        }

                        if (marksFilled(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistSubject.SelectedItem.Value)))
                        {
                            drow["MarksFilled"] = "True";
                            drow["TheoryMarks"] = populateMarks(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistSubject.SelectedItem.Value)).ToString();
                        }

                        else
                        {
                            drow["MarksFilled"] = "False";
                            drow["TheoryMarks"] = "";

                        }

                        dt.Rows.Add(drow);
                        i = i + 1;
                    }

                }
                else
                {

                    string remark;
                    if (!FindInfo.isDetained(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value,ddlistMOE.SelectedItem.Value, out remark))
                    {
                        string year = FindInfo.findAllExamYear(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);

                        string[] cyear = year.Split(',');

                        int pos = Array.IndexOf(cyear, ddlistYear.SelectedItem.Value);
                        if (pos > -1)
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

                            string rollno;
                            if (FindInfo.rollnoAlltted(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out rollno))
                            {
                                drow["RNAllotted"] = "True";
                                drow["RollNo"] = rollno;

                            }
                            else
                            {
                                drow["RNAllotted"] = "False";
                                drow["RollNo"] = "";
                            }

                            if (marksFilled(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistSubject.SelectedItem.Value)))
                            {
                                drow["MarksFilled"] = "True";
                                drow["TheoryMarks"] = populateMarks(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistSubject.SelectedItem.Value)).ToString();
                            }

                            else
                            {
                                drow["MarksFilled"] = "False";
                                drow["TheoryMarks"] = "";

                            }

                            dt.Rows.Add(drow);
                            i = i + 1;
                        }
                    }
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
                lblMSG.Text = "Sorry !! No Record found";
                pnlMSG.Visible = true;
                btnOK.Visible = false;
                
            }

            con.Close();
           

           
        }       

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validMarks())
            {
                foreach (DataListItem dli in dtlistShowStudents.Items)
                {

                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label eno = (Label)dli.FindControl("lblENo");
                    Label sccode = (Label)dli.FindControl("lblSCCode");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    Label cyear = (Label)dli.FindControl("lblCYear");
                    Label lblrna = (Label)dli.FindControl("lblRNAllotted");
                    TextBox rollno = (TextBox)dli.FindControl("tbRollNo");
                    Label lrollno = (Label)dli.FindControl("lblRollNo");
                    TextBox theory = (TextBox)dli.FindControl("tbTheory");
                    Label ltheory = (Label)dli.FindControl("lblTheory");

                   

                    if (lblmf.Text == "False")
                    {
                        if (lblrna.Text == "False")
                        {
                            if (rollno.Text != "")
                            {
                                string rno;
                                int ercounter;
                                string examrecorderror;
                                if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10" || ddlistExam.SelectedItem.Value == "A11" || ddlistExam.SelectedItem.Value == "B11")
                                {
                                    Exam.fillExamRecord(Convert.ToInt32(srid.Text), 2, Convert.ToInt32(ddlistYear.SelectedItem.Value), rollno.Text, ddlistExam.SelectedItem.Value, "", "", "", "", "", "", "", 0, ddlistMOE.SelectedItem.Value, out rno, out ercounter, out examrecorderror);
                                }
                            }
                        }
                        else if (lblrna.Text == "True")
                        {
                            if (lrollno.Text != rollno.Text)
                            {
                                if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10" || ddlistExam.SelectedItem.Value == "A11" || ddlistExam.SelectedItem.Value == "B11")
                                {
                                    Exam.updateRollNo(Convert.ToInt32(srid.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), rollno.Text, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                                }
                            }
                        }
                        if (theory.Text != "")
                        {
                            
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);


                            cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));
                            cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ddlistSubject.SelectedItem.Value));
                            cmd.Parameters.AddWithValue("@StudyCentreCode", sccode.Text);
                            cmd.Parameters.AddWithValue("@Theory",getMarks(theory.Text));
                            cmd.Parameters.AddWithValue("@IA", "");
                            cmd.Parameters.AddWithValue("@AW", "");
                            cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);


                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Log.createLogNow("Marks Filling", "Filled '"+theory.Text+"' theory marks of a student with Enrollment No '" + eno.Text + "' of subject '" + ddlistSubject.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                           
                        }
                 
                    }

                    else if (lblmf.Text == "True")
                    {
                        if (lblrna.Text == "False")
                        {
                            if (rollno.Text != "")
                            {
                                string rno;
                                int ercounter;
                                string examrecorderror;
                                if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10" || ddlistExam.SelectedItem.Value == "A11" || ddlistExam.SelectedItem.Value == "B11")
                                {
                                    Exam.fillExamRecord(Convert.ToInt32(srid.Text), 2, Convert.ToInt32(ddlistYear.SelectedItem.Value), rollno.Text, ddlistExam.SelectedItem.Value, "", "", "", "", "", "", "", 0, ddlistMOE.SelectedItem.Value, out rno, out ercounter, out examrecorderror);
                                }
                            }
                        }
                        else if (lblrna.Text == "True")
                        {
                            if (lrollno.Text != rollno.Text)
                            {
                                if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10" || ddlistExam.SelectedItem.Value == "A11" || ddlistExam.SelectedItem.Value == "B11")
                                {
                                    Exam.updateRollNo(Convert.ToInt32(srid.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value), rollno.Text, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                                }
                            }
                        }
                        if (ltheory.Text != theory.Text)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("update DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " set Theory=@Theory where SRID='" + srid.Text + "' and SubjectID='" + ddlistSubject.SelectedItem.Value + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);
                            cmd.Parameters.AddWithValue("@Theory", getMarks(theory.Text));

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                           
                             Log.createLogNow("Marks Updation", "Updated theory marks from '" + ltheory.Text + "' to '" + theory.Text + "' of a student with Enrollment No '" + eno.Text + "' of subject '" + ddlistSubject.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                           
                        }

                    }

                   

              }

                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Marks has been updated successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Invalid Marks are filled at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check marks should be between 0-100 and should be in numeric";
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }

        }

        private string getMarks(string tmarks)
        {
            string marks = "";


            if (tmarks == "")
            {
                marks = "";
            }
            else if (tmarks == "AB")
            {
                marks = "AB";
            }
            else
            {
                marks = tmarks;
            }
               
               
          

            return marks;
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
                    TextBox theorymarks = (TextBox)dli.FindControl("tbTheory");
              
                    SNo = Convert.ToInt32(sno.Text);
                    int tmarks;
                    if (theorymarks.Text == "")
                    {
                        tmarks = 0;
                    }
                    else if (theorymarks.Text == "AB")
                    {
                        tmarks = 0;
                    }
                    else
                    {
                        tmarks = Convert.ToInt32(theorymarks.Text);
                    }

                  

                    if (tmarks >= 0 && tmarks <= 100 )
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

        protected void ddlistSySession_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistSubject.Items.Clear();
            populateSubjects();

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;

        }

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistSySession.SelectedItem.Text != "--Select One--")
            {
                ddlistSubject.Items.Clear();
                populateSubjects();
            }

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistSubject_SelectedIndexChanged(object sender, EventArgs e)
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
            pnlMSG.Visible = true;
            btnOK.Visible = false;
        }

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;

        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistSySession.SelectedItem.Text != "--Select One--")
            {
                ddlistSubject.Items.Clear();
                populateSubjects();
            }
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistExam.SelectedItem.Value == "A13")
            {
                ddlistSySession.SelectedItem.Selected = false;
                ddlistSySession.Items.FindByText("A 2010-11").Selected = true;
                ddlistSySession.Enabled = false;
            }
            else
            {
                ddlistSySession.Enabled = true;
            }

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }
    
    }
}
