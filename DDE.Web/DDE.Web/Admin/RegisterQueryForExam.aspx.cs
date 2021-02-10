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
    public partial class RegisterQueryForExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                pnlSearch.Visible = true;
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int srid = FindInfo.findSRIDByENo(tbENo.Text);
            if (validENo(srid))
            {

                polulateStudentInfo(srid);
              
                pnlSearch.Visible = false;
                pnlStudentDetails.Visible = true;
                btnRegister.Visible = true;
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! not a valid Enrollment No.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

       
     


        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,StudyCentreCode,CYear,Session,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                 imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                tbEnNo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = srid.ToString();
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();
                tbSCCode.Text = dr["StudyCentreCode"].ToString();
                tbBatch.Text = dr["Session"].ToString();

                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                {

                    if (dr["CYear"].ToString() == "1")
                    {
                        if (dr["Course"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                        }
                    }
                    else if (dr["CYear"].ToString() == "2")
                    {
                        if (dr["Course2Year"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course2Year"]));
                        }

                    }
                    else if (dr["CYear"].ToString() == "3")
                    {
                        if (dr["Course"].ToString() == "")
                        {
                            tbCourse.Text = "Not Set";
                        }
                        else
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course3Year"]));
                        }

                    }
                }
                else
                {
                    tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                }


            }

            con.Close();



        }

        private bool validENo(int srid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select Course from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;

            }

            con.Close();

            return exist;
        }

       

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
            pnlStudentDetails.Visible = false;
            btnRegister.Visible = false;
            tbENo.Text = "";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                int year = Convert.ToInt32(ddlistExamYear.SelectedItem.Value);
                int srid = FindInfo.findSRIDByENo(tbENo.Text);
                if (validSRID(srid))
                {

                    int courseid = FindInfo.findCourseIDBySRID(srid);
                    int counter = findCounter(courseid);
                    string rollno = "A13" + FindInfo.findCourseCodeByID(courseid) + string.Format("{0:0000}", counter);
                    int ecid = findExamCentre(findStudyCentre(Convert.ToInt32(srid)));
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into ExamRecord_June13 values(@SRID,@Batch,@MOA,@RollNo,@AFP1Year,@AFP2Year,@AFP3Year,@EFP1Year,@EFP2Year,@EFP3Year,@CTPaperCode1,@CTPaperCode2,@ECID,@DCase,@FYear,@QStatus,@Online,@MSPrinted,@Times)", con);


                    cmd.Parameters.AddWithValue("@SRID", srid);
                    cmd.Parameters.AddWithValue("@Batch", tbBatch.Text);
                    cmd.Parameters.AddWithValue("@MOA", "");
                    cmd.Parameters.AddWithValue("@RollNo", rollno);

                    cmd.Parameters.AddWithValue("@AFP1Year", "False");
                    cmd.Parameters.AddWithValue("@AFP2Year", "False");
                    cmd.Parameters.AddWithValue("@AFP3Year", "False");

                    if (year == 1)
                    {

                        cmd.Parameters.AddWithValue("@EFP1Year", "True");
                        cmd.Parameters.AddWithValue("@EFP2Year", "False");
                        cmd.Parameters.AddWithValue("@EFP3Year", "False");
                    }

                    else if (year == 2)
                    {
                        cmd.Parameters.AddWithValue("@EFP1Year", "False");
                        cmd.Parameters.AddWithValue("@EFP2Year", "True");
                        cmd.Parameters.AddWithValue("@EFP3Year", "False");
                    }

                    else if (year == 3)
                    {


                        cmd.Parameters.AddWithValue("@EFP1Year", "False");
                        cmd.Parameters.AddWithValue("@EFP2Year", "False");
                        cmd.Parameters.AddWithValue("@EFP3Year", "True");
                    }


                    cmd.Parameters.AddWithValue("@CTPaperCode1", "");
                    cmd.Parameters.AddWithValue("@CTPaperCode2", "");
                    cmd.Parameters.AddWithValue("@ECID", ecid);
                    cmd.Parameters.AddWithValue("@DCase", "True");
                    cmd.Parameters.AddWithValue("@FYear", "");
                    cmd.Parameters.AddWithValue("@QStatus", "False");
                    cmd.Parameters.AddWithValue("@Online", "True");
                    cmd.Parameters.AddWithValue("@MSPrinted", "False");
                    cmd.Parameters.AddWithValue("@Times", 0);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    updateCounter(courseid, counter);
                    Session["counter"] = Convert.ToInt32(Session["counter"]) + 1;

                    Log.createLogNow("Filled Fee", "Filled Regular exam fee for June 2013 exam with Enrollment No. '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                    pnlData.Visible = false;
                    lblMSG.Text = "Query has been registered successfully";
                    pnlMSG.Visible = true;
                }
                else
                {
                    if (registeredForYear(srid,year))
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry this student is already registered  'for this Year' <br/> for June 2013 Exam";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand();

                        

                        if (year == 1)
                        {
                            cmd.CommandText = "update ExamRecord_June13 set EFP1Year=@EFP1Year,DCase=@DCase,FYear=@FYear,QStatus=@QStatus where SRID='" + srid + "' ";
                            cmd.Parameters.AddWithValue("@EFP1Year", "True");
                            cmd.Parameters.AddWithValue("@DCase", "True");
                            cmd.Parameters.AddWithValue("@FYear", year.ToString());
                            cmd.Parameters.AddWithValue("@QStatus", "False");
                        }
                        else if (year == 2)
                        {
                            cmd.CommandText = "update ExamRecord_June13 set EFP2Year=@EFP2Year,DCase=@DCase,FYear=@FYear,QStatus=@QStatus where SRID='" + srid + "' ";
                            cmd.Parameters.AddWithValue("@EFP2Year", "True");
                            cmd.Parameters.AddWithValue("@DCase", "True");
                            cmd.Parameters.AddWithValue("@FYear", year.ToString());
                            cmd.Parameters.AddWithValue("@QStatus", "False");
                        }
                        else if (year == 3)
                        {
                            cmd.CommandText = "update ExamRecord_June13 set EFP3Year=@EFP3Year,DCase=@DCase,FYear=@FYear,QStatus=@QStatus where SRID='" + srid + "' ";
                            cmd.Parameters.AddWithValue("@EFP3Year", "True");
                            cmd.Parameters.AddWithValue("@DCase", "True");
                            cmd.Parameters.AddWithValue("@FYear", year.ToString());
                            cmd.Parameters.AddWithValue("@QStatus", "False");

                        }
                       

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Filled Fee", "Filled Regular exam fee for '"+year+"' Year for June 2013 exam with Enrollment No. '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                        pnlData.Visible = false;
                        lblMSG.Text = "Query has been registered successfully";
                        pnlMSG.Visible = true;
                    }

                }
            }
            catch(Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = "There is some error. Please cross check all entries.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private bool registeredForYear(int srid, int year)
        {
            bool registered = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (year.ToString() == dr["FYear"].ToString())
                {
                    
                        registered = true;
                   
                }
                
                
            }
            


            con.Close();
            return registered;
        }

        private void updateCounter(int courseid, int counter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDERollNoCounters set RollNoCounter_A13=@RollNoCounter_A13 where CourseID='" + courseid + "'", con);
            cmd.Parameters.AddWithValue("@RollNoCounter_A13", counter);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }


        private int findExamCentre(string sccode)
        {
            int ecid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ECID,SCCodes from DDEExaminationCentres1", con);
            con.Open();
            SqlDataReader dr;
            string[] sc = { };
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sc = dr["SCCodes"].ToString().Split(',');
                    for (int i = 0; i < sc.Length; i++)
                    {
                        if (sc[i].ToString() == sccode)
                        {

                            ecid = Convert.ToInt32(dr["ECID"]);
                        }
                    }
                }

            }

            con.Close();
            return ecid;
        }

        private string findStudyCentre(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
                {
                    sccode = Convert.ToString(dr["StudyCentreCode"]);
                }
                else if (dr["SCStatus"].ToString() == "T")
                {
                    sccode = findTranferedSCCode(srid);
                }

            }

            con.Close();

            return sccode;
        }
        private string findTranferedSCCode(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PreviousSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                sccode = dr[0].ToString();

            }

            con.Close();

            return sccode;
        }


        private int findCounter(int courseid)
        {
            string counter = "NA";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select RollNoCounter_A13 from DDERollNoCounters where CourseID='" + courseid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                counter = Convert.ToString(dr[0]);

            }

            con.Close();

            return Convert.ToInt32(counter) + 1;
        }

        private bool validSRID(int srid)
        {
            bool newstudent = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                newstudent = false;
            }
            else
            {
                newstudent = true;
            }


            con.Close();
            return newstudent;
        }
    }
}
