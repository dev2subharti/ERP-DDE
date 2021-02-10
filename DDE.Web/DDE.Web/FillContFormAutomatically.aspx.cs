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
using System.Data.OleDb;

namespace DDE.Web
{
    public partial class FillContFormAutomatically : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int count = 0;
            string sccode = "996";         
            string exam = "Z11";

            string accsess = "2020-21";
            int itype = 4;
            string ino = "COMB012USCDEC20-008";
            string iday = "05";
            string imonth = "02";
            string iyear = "2021";
            string idate = iyear + "-" + imonth + "-" + iday;
            string ibn = "NA";
            int totalamount = 371245;
            int fhfee = 371245;

            string frdate = "2021-02-08";

            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Offline Applications\996\Cont\Z11\3.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString = "SELECT * FROM [Sheet1$]";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string[] s = findStudentInfo(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());                   
                    int srid =Convert.ToInt32(s[0]);
                    int cid = Convert.ToInt32(s[1]);
                    string session = s[2].ToString();
                    int batchid = FindInfo.findBatchID(session);
                    if (srid != 0)
                    {
                        //if (s[3].ToString() == "462")                            
                        //{
                            string afexam;
                            if (!(feeAlreadyExist(srid, 1, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]), out afexam)))
                            {
                              if (s[3].ToString() == "996")
                              {
                                int coursefee = FindInfo.findCourseFeeByBatch(cid, batchid) / 2;
                                string error;
                                if (validINo(fhfee, itype, ino, idate, ibn, coursefee, out error))
                                {
                                    Accounts.fillFee(Convert.ToInt32(srid), 1, accsess, itype, ino, iday, imonth, iyear, ibn, coursefee, Accounts.IntegerToWords(coursefee), totalamount, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]), exam, frdate, 1);

                                    upadteCYear(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]));
                                    if (cbTransfer.Checked == true)
                                    {
                                        string csccode = FindInfo.findSCCodeBySRID(srid);
                                        transferTo001(srid, csccode, sccode);
                                    }

                                    Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString(), 0);
                                    if (cid == 76)
                                    {
                                        int specid = FindInfo.findCourseIDByCourseName(ds.Tables[0].Rows[i]["Specialization"].ToString());
                                        setSpecialization(srid, specid);

                                        if (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"])))
                                        {
                                            int res = registerForSLM(Convert.ToInt32(srid), s[3].ToString(), specid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]));
                                        }
                                    }
                                    else
                                    {
                                        if (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"])))
                                        {
                                            int res = registerForSLM(Convert.ToInt32(srid), s[3].ToString(), cid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]));
                                        }
                                    }


                                    count = count + 1;

                                    Response.Write("<br/>" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                                }
                                else
                                {
                                    Response.Write("<br/> Sorry!! Remaining amount is less than required course fee");
                                    break;
                                }
                              }
                              else
                              {
                                  Response.Write("<br/>SC Code did not matched for ENo : " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                                int coursefee = FindInfo.findCourseFeeByBatch(cid, batchid) / 2;
                                string error;
                                if (validINo(fhfee, itype, ino, idate, ibn, coursefee, out error))
                                {
                                    Accounts.fillFee(Convert.ToInt32(srid), 1, accsess, itype, ino, iday, imonth, iyear, ibn, coursefee, Accounts.IntegerToWords(coursefee), totalamount, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]), exam, frdate, 1);

                                    upadteCYear(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]));
                                   
                                    string csccode = FindInfo.findSCCodeBySRID(srid);
                                    transferAFCode(srid, csccode, sccode);
                                   

                                    Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString(), 0);
                                    if (cid == 76)
                                    {
                                        int specid = FindInfo.findCourseIDByCourseName(ds.Tables[0].Rows[i]["Specialization"].ToString());
                                        setSpecialization(srid, specid);

                                        if (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"])))
                                        {
                                            int res = registerForSLM(Convert.ToInt32(srid), s[3].ToString(), specid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]));
                                        }
                                    }
                                    else
                                    {
                                        if (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"])))
                                        {
                                            int res = registerForSLM(Convert.ToInt32(srid), s[3].ToString(), cid, Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]));
                                        }
                                    }


                                    count = count + 1;

                                    Response.Write("<br/>" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                                }
                                else
                                {
                                    Response.Write("<br/> Sorry!! Remaining amount is less than required course fee");
                                    break;
                                }
                            }

                            }
                            else
                            {
                                Response.Write("<br/>Course Fee is already exist for Exam : '" + exam + "' for ENo : " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                            }
                        //}
                        //else
                        //{
                        //    Response.Write("<br/>SC Code did not matched for ENo : " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                        //}
                    }
                    else
                    {
                        Response.Write("<br/>SRID not found for ENo : " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                    }
                }
               
            }

            Response.Write("<br/>Total : " + (count).ToString() + " Students uploaded.");
        }

        private void transferAFCode(int srid, string currentsccode, string newsccode)
        {
            insertNewRecord(srid, currentsccode, newsccode);          
            updateCentralRecord(srid, newsccode, currentsccode);
        }

        private void transferTo001(int srid, string presccode, string currentsccode)
        {
            insertNewRecord(srid, presccode, currentsccode);
            insertNewRecord(srid, currentsccode, "001");
            updateCentralRecord(srid,"001", currentsccode);
        }

        private void insertNewRecord(int srid, string presccode, string currentsccode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDEChangeSCRecord values(@SRID,@PreviousSC,@CurrentSC,@TimeOfChange)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@PreviousSC", presccode);
            cmd.Parameters.AddWithValue("@CurrentSC", currentsccode);
            cmd.Parameters.AddWithValue("@TimeOfChange", DateTime.Now.ToString());


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Changed Study Centre", "Changed Study centre of student with Enrollment No '" + FindInfo.findENoByID(srid) + "'", 0);


        }

        private void updateCentralRecord(int srid,string newsccode, string previouscode)
        {
          

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set SCStatus=@SCStatus,StudyCentreCode=@StudyCentreCode,PreviousSCCode=@PreviousSCCode where SRID='" + srid + "'", con);

            cmd.Parameters.AddWithValue("@SCStatus", "C");
            cmd.Parameters.AddWithValue("@StudyCentreCode", newsccode);
            cmd.Parameters.AddWithValue("@PreviousSCCode", previouscode);



            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }
        private bool validINo(int fhfee, int itype, string ino, string idate, string ibn, int cfee, out string error)
        {
            error = "";
            bool valid = false;
            int fhusedfee = Accounts.findUsedAmountOfDraftByFH(1, itype, ino, idate, ibn);

            int reaminfhfee = (fhfee - fhusedfee);

            if (reaminfhfee >= cfee)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }


            return valid;
        }

        private void upadteCYear(int srid, int year)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set CYear=@CYear where SRID='" + srid + "' ", con);
            cmd.Parameters.AddWithValue("@CYear", year);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private string[] findStudentInfo(string eno)
        {

            string[] s = { "0","0","",""};

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID,Course,Session,StudyCentreCode from DDEStudentRecord where EnrollmentNo='" + eno + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                s[0] = dr[0].ToString();
                s[1] = dr[1].ToString();
                s[2] = dr[2].ToString();
                s[3] = dr[3].ToString();

            }
            con.Close();

            return s;
        }

        public static bool feeAlreadyExist(int srid, int feehead, int year, out string exam)
        {
            exam = "NA";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand(findCommand(srid, feehead, year), con);
            SqlDataReader dr;

            bool exist = false;
            
            con.Open();
            dr = cmd.ExecuteReader();
        
            if (dr.HasRows)
            {
                dr.Read();
                exam=dr["ForExam"].ToString();
                exist=true;
            }

            con.Close();

            return exist;
        }

        private static string findCommand(int srid, int feehead, int year)
        {
            string cm = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEAcountSession", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (cm == "")
                {
                    cm = "select * from [DDEFeeRecord_" + ds.Tables[0].Rows[i]["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForYear='" + year + "'";
                }
                else
                {
                    cm = cm + " union " + "select * from [DDEFeeRecord_" + ds.Tables[0].Rows[i]["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForYear='" + year + "'";
                }

            }

            con.Close();

            return cm;

        }
  
        private int registerForSLM(int srid, string sccode, int cid, int year)
        {
            int res = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESLMIssueRecord values(@SRID,@SCCode,@CID,@Year,@TOR,@LNo)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@SCCode", sccode);
            cmd.Parameters.AddWithValue("@CID", cid);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@LNo", 0);


            con.Open();
            res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        protected void btnSubmitOnlineCont_Click(object sender, EventArgs e)
        {
            int count = 0;
            string sccode = "996";
            string exam = "Z11";

            string accsess = "2020-21";
            int itype = 4;
            string ino = "COMB032USCDEC20-004";
            string iday = "14";
            string imonth = "01";
            string iyear = "2021";
            string idate = iyear + "-" + imonth + "-" + iday;
            string ibn = "NA";
            int totalamount = 3779590;
            int fhfee = 2611640;

            string frdate = "2021-01-21";
           

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEOnlineContinuationRecord where SCCode='"+sccode+"' and ForExam='"+exam+ "' and Enrolled='0'", con);
            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string[] s = findStudentInfoBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));

                    int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);

                    string eno = s[0].ToString();
                    int cid = Convert.ToInt32(s[1]);
                    string session = s[2].ToString();
                    int batchid = FindInfo.findBatchID(session);
                    if (srid != 0)
                    {
                        string afexam;
                        if (!(feeAlreadyExist(srid, 1, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), out afexam)))
                        {

                            int coursefee = FindInfo.findCourseFeeByBatch(cid, batchid) / 2;
                            string error;
                            if (validINo(fhfee, itype, ino, idate, ibn, coursefee, out error))
                            {

                                Accounts.fillFee(Convert.ToInt32(srid), 1, accsess, itype, ino, iday, imonth, iyear, ibn, coursefee, Accounts.IntegerToWords(coursefee), totalamount, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), exam, frdate, 1);

                                upadteCYear(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]));

                                upadteEnrolled(Convert.ToInt32(ds.Tables[0].Rows[i]["OURID"]));

                                if (cbTransfer.Checked == true)
                                {
                                   string csccode = FindInfo.findSCCodeBySRID(srid);
                                    transferTo001(srid, csccode, sccode);
                                }

                               Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + eno, 0);

                               if (Convert.ToString(ds.Tables[0].Rows[i]["IsMBACourse"]) == "True")
                               {
                                  setSpecialization(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["MBASpecialization"]));

                                  if (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"])))
                                  {
                                    int res = registerForSLM(Convert.ToInt32(srid), sccode, Convert.ToInt32(ds.Tables[0].Rows[i]["MBASpecialization"]), Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]));
                                  }
                               }
                               else
                               {
                                  if (!FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"])))
                                  {
                                    int res = registerForSLM(Convert.ToInt32(srid), sccode, cid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]));
                                  }
                               }
                            

                                count = count + 1;
                            }
                            else
                            {
                                Response.Write("<br/> Sorry!! Remaining amount is less than required course fee");
                                break;
                            }
                            

                        }
                        else
                        {
                            Response.Write("<br/>Course Fee is already exist for Exam : '"+afexam+"' for ENo : " + eno);
                        }
                    }
                    else
                    {
                        Response.Write("<br/>SRID not found for ENo : " + eno);
                    }
                }

           
            Response.Write("<br/>Total : " + (count).ToString() + " Students uploaded.");
        }

        private void upadteEnrolled(int ourid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEOnlineContinuationRecord set Enrolled=@Enrolled,EnrolledOn=@EnrolledOn where OURID='" + ourid + "' ", con);
            cmd.Parameters.AddWithValue("@Enrolled", "True");
            cmd.Parameters.AddWithValue("@EnrolledOn", DateTime.Now.ToString());
          

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void setSpecialization(int srid, int spcial)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set Course2Year=@Course2Year,Course3Year=@Course3Year where SRID='" + srid + "' ", con);
            cmd.Parameters.AddWithValue("@Course2Year", spcial);
            cmd.Parameters.AddWithValue("@Course3Year", spcial);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private string[] findStudentInfoBySRID(int srid)
        {
            string[] s = { "0", "0", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select EnrollmentNo,Course,Session from DDEStudentRecord where SRID='" + srid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                s[0] = dr[0].ToString();
                s[1] = dr[1].ToString();
                s[2] = dr[2].ToString();

            }
            con.Close();

            return s;
        }

        protected void btnCutSpFee_Click(object sender, EventArgs e)
        {
            int count = 0;
            string sccode = "995";
            string session = "C 2016";
            string exam = "B17";

            string accsess = "2018-19";
            int itype = 3;
            string ino = "EW-003";
            string iday = "20";
            string imonth = "12";
            string iyear = "2017";
            string idate = iyear + "-" + imonth + "-" + iday;
            string ibn = "NA";
            int totalamount = 2500000;
            int fhfee = 1930200;

            string frdate = "2018-06-25";


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SRID,EnrollmentNo,CYear from DDEStudentRecord where StudyCentreCode='" + sccode + "' and [Session]='" + session + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                             
                int coursefee = 1000;
                string error;
                if (validINo(fhfee, itype, ino, idate, ibn, coursefee, out error))
                {
                    Accounts.fillFee(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), 1, accsess, itype, ino, iday, imonth, iyear, ibn, coursefee, Accounts.IntegerToWords(coursefee), totalamount, Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]), exam, frdate, 1);

                    Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + ds.Tables[0].Rows[i]["EnrollmentNo"], 0);
                            
                    count = count + 1;
                }
                else
                {
                    Response.Write("<br/> Sorry!! Remaining amount is less than required course fee");
                    break;
                }                   
               
            }

            Response.Write("<br/>Total : " + (count).ToString() + " Students uploaded.");
        }
      
       
    }
}