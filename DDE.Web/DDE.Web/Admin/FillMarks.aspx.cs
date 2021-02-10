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
    public partial class FillMarks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 25) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 29))
            {
                if (!IsPostBack)
                { 
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("A13").Selected = true;
                    PopulateDDList.populateCourses(ddlistCourse);
                    PopulateDDList.populateSySession(ddlistSySession);
                  
                    PopulateDDList.populateStudyCentre(ddlistStudyCentre);
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
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 25) && !Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {


                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
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

                   
                }


            }
            else if (!Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 25) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {


                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");

                    if (ia.Text == "")
                    {
                        ia.Enabled = false;
                    }

                    else
                    {
                        ia.Enabled = true;
                    }
                    if (aw.Text == "")
                    {
                        aw.Enabled =false;
                    }

                    else
                    {
                        aw.Enabled = true;
                    }


                }


            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 25) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {


                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");
                    ia.Enabled = true;
                    aw.Enabled = true;
                }


            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 29))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {
                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");
                    ia.Enabled = false;
                    aw.Enabled = false;
                }


            }

       
        }

        private void populateMarks(DataRow drow, int srid, int subid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select IA,AW from DDEMarkSheet_"+ddlistExam.SelectedItem.Value+" where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='"+ddlistMOE.SelectedItem.Value+"'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            drow["IAMarks"]=dr[0].ToString();
            drow["AWMarks"]=dr[1].ToString();
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
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "2")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "3")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                }
               
            }
            else
            {
                cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
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

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            int i = 1;
            while (dr.Read())
            {
                if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10" || ddlistExam.SelectedItem.Value == "A11" || ddlistExam.SelectedItem.Value == "B11" || ddlistExam.SelectedItem.Value == "A12" || ddlistExam.SelectedItem.Value == "B12")
                {
                    if (FindInfo.examFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]),ddlistYear.SelectedItem.Value, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
                    {

                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i;
                        drow["SRID"] = Convert.ToString(dr["SRID"]);
                        drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
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
                            cmd.Parameters.AddWithValue("@StudyCentreCode", ddlistStudyCentre.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@Theory", "");
                            cmd.Parameters.AddWithValue("@IA", ia.Text);
                            cmd.Parameters.AddWithValue("@AW", aw.Text);
                            cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Log.createLogNow("Marks Filling", "Filled '" + ia.Text + "' IA & '" + aw.Text + "' AW internal marks of a student with Enrollment No '" + eno.Text + "' of subject '" + ddlistSubject.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
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

                            Log.createLogNow("Marks Updation", "Updated internal marks IA from '" + lia.Text + "' to '" + ia.Text + "' & AW from '" + law.Text + "' to '"+aw.Text+"' of a student with Enrollment No '" + eno.Text + "' of subject '" + ddlistSubject.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
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
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Invalid Marks are filled at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check marks should be between 0-16 and should be in numeric";
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }

        }

        private bool validMarks()
        {

            bool validmarks=false;
            int SNo = 0;
            bool allowedforfullmarks = false;
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 68))
            {
                allowedforfullmarks = true;
            }
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                try
                {
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    TextBox ia = (TextBox)dli.FindControl("tbIA");
                    TextBox aw = (TextBox)dli.FindControl("tbAW");

                    SNo=Convert.ToInt32(sno.Text);

                    int iamarks;
                    int awmarks;
                    if (ia.Text == "")
                    {
                         iamarks=0;
                    }
                    else if (ia.Text == "AB")
                    {
                         iamarks = 0;
                    }
                    else
                    {
                      iamarks=Convert.ToInt32(ia.Text);
                    }


                    if (aw.Text == "")
                    {
                         awmarks=0;
                    }
                    else if (aw.Text == "AB")
                    {
                         awmarks = 0;
                    }
                    else
                    {
                      awmarks=Convert.ToInt32(aw.Text);
                    }

                    //if (allowedforfullmarks)
                    //{
                        if (iamarks >= 0 && iamarks <= 20 && awmarks >= 0 && awmarks <= 20)
                        {
                            validmarks = true;

                        }

                        else
                        {
                            validmarks = false;
                            Session["SNo"] = SNo;
                            break;
                        }
                    //}
                    //else
                    //{
                    //    if (iamarks >= 0 && iamarks <= 16 && awmarks >= 0 && awmarks <= 16)
                    //    {
                    //        validmarks = true;

                    //    }

                    //    else
                    //    {
                    //        validmarks = false;
                    //        Session["SNo"] = SNo;
                    //        break;
                    //    }
                    //}
                    
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

        protected void ddlistStudyCentre_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }
    }
}
