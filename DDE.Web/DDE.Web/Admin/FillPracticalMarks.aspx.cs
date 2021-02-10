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
    public partial class FillPracticalMarks : System.Web.UI.Page
    {
        int maxpracmarks = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("B13").Selected = true;
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

      


        private void populatePractical()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalID,PracticalName from DDEPractical where SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' and CourseName='" + ddlistCourse.SelectedItem.Text + "' and Year='" + ddlistYear.SelectedItem.Text + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistPractical.Items.Add(dr[1].ToString());
                ddlistPractical.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();


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
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) && !Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {


                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox prac = (TextBox)dli.FindControl("tbPracticalMarks");


                    if (prac.Text == "")
                    {
                        prac.Enabled = true;
                    }

                    else
                    {
                        prac.Enabled = false;
                    }


                }


            }
            else if (!Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {


                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox prac = (TextBox)dli.FindControl("tbPracticalMarks");


                    if (prac.Text == "")
                    {
                        prac.Enabled = false;
                    }

                    else
                    {
                        prac.Enabled = true;
                    }

                }


            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {


                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox prac = (TextBox)dli.FindControl("tbPracticalMarks");
                    prac.Enabled = true;

             
                }


            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 29))
            {

                foreach (DataListItem dli in dtlistShowStudents.Items)
                {
                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                    TextBox prac = (TextBox)dli.FindControl("tbPracticalMarks");
                    prac.Enabled = false;
                }


            }


        }
       
        private void findMaxPracMarks(int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalMaxMarks from DDEPractical where PracticalID='"+pracid+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            maxpracmarks= Convert.ToInt32(dr[0]); 
            con.Close();
       
        }

        private void populateMarks(DataRow drow, int srid, int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select PracticalMarks from DDEPracticalMarks_"+ddlistExam.SelectedItem.Value+" where SRID='" + srid + "' and PracticalID='" + pracid + "' and MOE='"+ddlistMOE.SelectedItem.Value+"'", con);
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


          
                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
                {
                    if (ddlistYear.SelectedItem.Value == "1")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by EnrollmentNo";
                    }
                    else if (ddlistYear.SelectedItem.Value == "2")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by EnrollmentNo";
                    }
                    else if (ddlistYear.SelectedItem.Value == "3")
                    {
                        cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by EnrollmentNo";
                    }

                }
                else
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP,CYear,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "' and RecordStatus='True' order by EnrollmentNo";
                }


            SqlDataReader dr;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
           
            DataColumn dtcol5 = new DataColumn("RollNo");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("SCCode");
            DataColumn dtcol8 = new DataColumn("MarksFilled");
            DataColumn dtcol9 = new DataColumn("PracticalMarks");
            DataColumn dtcol10 = new DataColumn("CYear");
          



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
           
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);

            int i = 1;
            while (dr.Read())
            {
                
                //if (ddlistExam.SelectedItem.Value == "A10" || ddlistExam.SelectedItem.Value == "B10" || ddlistExam.SelectedItem.Value == "A11" || ddlistExam.SelectedItem.Value == "B11" || ddlistExam.SelectedItem.Value == "A12")
                //{
                    if (FindInfo.examFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]),ddlistYear.SelectedItem.Value, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i;
                        drow["SRID"] = Convert.ToString(dr["SRID"]);
                        drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                        drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                        drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);

                        drow["CYear"] = Convert.ToString(dr["CYear"]);


                        drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]),ddlistExam.SelectedItem.Value,ddlistMOE.SelectedItem.Value);
                       

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

                        dt.Rows.Add(drow);
                        i = i + 1;
                    }

                //}
                //else
                //{
                //    if (!FindInfo.isDetained(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value,ddlistMOE.SelectedItem.Value, out error))
                //    {
                //        string year = FindInfo.findAllExamYear(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);

                //        string[] cyear = year.Split(',');

                //        int pos = Array.IndexOf(cyear, ddlistYear.SelectedItem.Value);
                //        if (pos > -1)
                //        {
                //            DataRow drow = dt.NewRow();
                //            drow["SNo"] = i;
                //            drow["SRID"] = Convert.ToString(dr["SRID"]);
                //            drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                //            drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                //            drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);

                //            drow["CYear"] = Convert.ToString(dr["CYear"]);

                //            if (ddlistMOE.SelectedItem.Value == "R")
                //            {

                //                if (Convert.ToInt32(dr["CYear"]) == 1)
                //                {

                //                    drow["RollNo"] = Convert.ToString(dr["RollNoIYear"]);
                //                }

                //                else if (Convert.ToInt32(dr["CYear"]) == 2)
                //                {
                //                    drow["RollNo"] = Convert.ToString(dr["RollNoIIYear"]);
                //                }

                //                else if (Convert.ToInt32(dr["CYear"]) == 3)
                //                {
                //                    drow["RollNo"] = Convert.ToString(dr["RollNoIIIYear"]);
                //                }
                //            }

                //            else if (ddlistMOE.SelectedItem.Value == "B")
                //            {
                //                drow["RollNo"] = Convert.ToString(dr["RollNoBP"]);
                //            }

                //            if (marksFilled(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistPractical.SelectedItem.Value)))
                //            {
                //                drow["MarksFilled"] = "True";
                //                populateMarks(drow, Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistPractical.SelectedItem.Value));
                //            }

                //            else
                //            {
                //                drow["MarksFilled"] = "False";
                //                drow["PracticalMarks"] = "";


                //            }

                //            dt.Rows.Add(drow);
                //            i = i + 1;
                       //}
                    //}
                //}
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

            findMaxPracMarks(Convert.ToInt32(ddlistPractical.SelectedItem.Value));

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
                            cmd.Parameters.AddWithValue("@StudyCentreCode", ddlistStudyCentre.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@PracticalMarks", pracmarks.Text);
                            cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);




                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();


                            Log.createLogNow("Marks Filling", "Filled " + pracmarks.Text + " practical marks of a student with Enrollment No " + eno.Text + " of practical " + ddlistPractical.SelectedItem.Text + " in course " + ddlistCourse.SelectedItem.Text + "", Convert.ToInt32(Session["ERID"].ToString()));
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


                            Log.createLogNow("Marks Updation", "Updated practical marks from '" + lpracmarks.Text + "' to '" + pracmarks.Text + "' of a student with Enrollment No '" + eno.Text + "' of practical '" + ddlistPractical.SelectedItem.Text + "' in course '" + ddlistCourse.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
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
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Invalid Marks are filled at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check marks should be between 0-" + ((maxpracmarks*80)/100) + " and should be in numeric";
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }
        }

        private bool validMarks()
        {

            bool validmarks = false;
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

                    if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14")
                    {

                        if (allowedforfullmarks)
                        {
                            if (pmarks >= 0 && pmarks <= maxpracmarks)
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

                        else
                        {

                            if (pmarks >= 0 && pmarks <= ((maxpracmarks * 80) / 100))
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
                    }
                    else
                    {
                        if (pmarks >= 0 && pmarks <= maxpracmarks)
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
            ddlistPractical.Items.Clear();
            populatePractical();

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;

        }

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistSySession.SelectedItem.Text != "--Select Here--")
            {
                ddlistPractical.Items.Clear();
                populatePractical();
            }

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistPractical_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistSySession.SelectedItem.Text != "--Select Here--")
            {
                ddlistPractical.Items.Clear();
                populatePractical();
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
