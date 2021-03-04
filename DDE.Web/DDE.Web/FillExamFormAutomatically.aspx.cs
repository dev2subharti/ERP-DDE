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
using System.Text;
using System.Data.OleDb;


namespace DDE.Web
{
    public partial class FillExamFormAutomatically : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAutoFeed_Click(object sender, EventArgs e)
        {
            string sccode = "996";
            string exam="Z11";
            int ecid = FindInfo.findExamCentreBySCCode(sccode,exam);

            int itype = 4;
            string ino = "COMB016USCDEC20-011";
            string idate = "2021-03-03";
            string ibn = "NA";
            int iamount = 539145;

            string asession = "2020-21";

            
            //int amount = 1400;
            //string amountinwords = "ONE THOUSAND FOUR HUNDRED";

            int amount = 700;
            string amountinwords = "SEVEN HUNDRED";

            if (ecid != 0)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                //SqlCommand cmd = new SqlCommand("Select SRID,CYear as ForYear,Course from DDEStudentRecord where [Session]='Q 2018-19' and StudyCentreCode='"+sccode+"' and RecordStatus='True' order by SRID", con);
                //SqlCommand cmd = new SqlCommand("Select DDEOnlineContinuationRecord.SRID,DDEOnlineContinuationRecord.ForYear,DDEStudentRecord.Course from DDEOnlineContinuationRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEOnlineContinuationRecord.SRID where DDEOnlineContinuationRecord.SCCode='" + sccode + "' and  DDEOnlineContinuationRecord.ForExam='"+exam+"' and DDEOnlineContinuationRecord.Enrolled='1' order by SRID", con);
                //SqlCommand cmd = new SqlCommand("Select DDEOnlineExamRecord.OERID,DDEOnlineExamRecord.SRID,DDEOnlineExamRecord.Year as ForYear,DDEStudentRecord.Course from DDEOnlineExamRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEOnlineExamRecord.SRID where DDEOnlineExamRecord.SCCode='" + sccode + "' and DDEOnlineExamRecord.Enrolled='0' and DDEOnlineExamRecord.MOE='R' and DDEOnlineExamRecord.Examination='" + exam + "' order by SRID", con);
                SqlCommand cmd = new SqlCommand(findCommand2(sccode, exam),con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int j = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; j < ds.Tables[0].Rows.Count; i++)
                    {
                        int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
                        int feeoutput = 0;
                        int examoutput = 0;
                        if (!isExamFormEntered(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), exam))
                        {
                            feeoutput = fillFee(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), exam, asession, ino, itype, idate, ibn, iamount, amount, amountinwords);
                            if (feeoutput == 1)
                            {
                                examoutput = fillExamForm(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), exam, ecid, "R");
                            }

                        }
                        if (feeoutput != examoutput)
                        {
                            Response.Write("Operation failed @ : " + srid.ToString());
                            break;
                        }
                        else if (feeoutput == 1 && examoutput == 1)
                        {
                            //updateEnrolled(Convert.ToInt32(ds.Tables[0].Rows[i]["OERID"]));
                            j = j + 1;
                        }
                    }
                }

            Response.Write("Total Form Feeded : " + j.ToString());
            }
            else
            {
                Response.Write("Sorry !! Exam Center is not allotted till yet");
            }
        }
      
        private void updateEnrolled(int OERID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEOnlineExamRecord set Enrolled=@Enrolled,EnrolledOn=@EnrolledOn where OERID='" + OERID + "' ", con);
            cmd.Parameters.AddWithValue("@Enrolled", "True");
            cmd.Parameters.AddWithValue("@EnrolledOn", DateTime.Now.ToString());

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private string findCommand(string sccode, string exam)
        {
             StringBuilder cmd = new StringBuilder();
                           
               
                cmd.Append("select distinct [DDEFeeRecord_2016-17].SRID,");             
                cmd.Append(" DDEStudentRecord.Course,");                            
                cmd.Append(" [DDEFeeRecord_2016-17].ForYear");
                                                           
                cmd.Append(" from [DDEFeeRecord_2016-17]");
                cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2016-17].SRID"); 
                cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                                      
                cmd.Append(" where [DDEFeeRecord_2016-17].FeeHead='1' and [DDEFeeRecord_2016-17].ForExam='"+exam+"'"); 
                cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"'");
                
                cmd.Append(" union"); 
                
                cmd.Append(" select distinct [DDEFeeRecord_2017-18].SRID,");               
                cmd.Append(" DDEStudentRecord.Course,");               
                cmd.Append(" [DDEFeeRecord_2017-18].ForYear");
                                                           
                cmd.Append(" from [DDEFeeRecord_2017-18]");
                cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2017-18].SRID"); 
                cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                                      
                cmd.Append(" where [DDEFeeRecord_2017-18].FeeHead='1' and [DDEFeeRecord_2017-18].ForExam='"+exam+"'");
                cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"'");

                cmd.Append(" union");

                cmd.Append(" select distinct [DDEFeeRecord_2018-19].SRID,");
                cmd.Append(" DDEStudentRecord.Course,");
                cmd.Append(" [DDEFeeRecord_2018-19].ForYear");

                cmd.Append(" from [DDEFeeRecord_2018-19]");
                cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2018-19].SRID");
                cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                cmd.Append(" where [DDEFeeRecord_2018-19].FeeHead='1' and [DDEFeeRecord_2018-19].ForExam='"+exam+"'");
                cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"' order by SRID");

                cmd.Append(" union");

                cmd.Append(" select distinct [DDEFeeRecord_2019-20].SRID,");
                cmd.Append(" DDEStudentRecord.Course,");
                cmd.Append(" [DDEFeeRecord_2019-20].ForYear");

                cmd.Append(" from [DDEFeeRecord_2019-20]");
                cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2019-20].SRID");
                cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                cmd.Append(" where [DDEFeeRecord_2019-20].FeeHead='1' and [DDEFeeRecord_2019-20].ForExam='"+exam+"'");
                cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"' order by SRID");

                cmd.Append(" union");

                cmd.Append(" select distinct [DDEFeeRecord_2020-21].SRID,");
                cmd.Append(" DDEStudentRecord.Course,");
                cmd.Append(" [DDEFeeRecord_2020-21].ForYear");

                cmd.Append(" from [DDEFeeRecord_2020-21]");
                cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2020-21].SRID");
                cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                cmd.Append(" where [DDEFeeRecord_2020-21].FeeHead='1' and [DDEFeeRecord_2020-21].ForExam='" + exam + "'");
                cmd.Append(" and DDEStudentRecord.StudyCentreCode='" + sccode + "' order by SRID");

                return cmd.ToString();
        }

        private string findCommand1(string sccode, string exam)
        {
                 StringBuilder cmd = new StringBuilder();

                 cmd.Append("select [DDEFeeRecord_2016-17].SRID,[DDEFeeRecord_2016-17].ForYear,DDEStudentRecord.Course"); 
                                                           
                 cmd.Append(" from [DDEFeeRecord_2016-17]"); 
                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2016-17].SRID");  
                 cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");                                       
                 cmd.Append(" where [DDEFeeRecord_2016-17].FeeHead='1' and [DDEFeeRecord_2016-17].ForExam='"+exam+"'");  
                 cmd.Append(" and StudyCentreCode='"+sccode+"' and [DDEFeeRecord_2016-17].SRID not in (SELECT"); 
                 cmd.Append(" [DDEExamRecord_"+exam+"].[SRID]"); 
          
                 cmd.Append(" FROM [ddedbweb].[dbo].[DDEExamRecord_"+exam+"]"); 
  
                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEExamRecord_"+exam+"].SRID"); 
  
                 cmd.Append(" where StudyCentreCode='"+sccode+"' and MOE='R')"); 
                
                 cmd.Append(" union");

                 cmd.Append(" select [DDEFeeRecord_2017-18].SRID,[DDEFeeRecord_2017-18].ForYear,DDEStudentRecord.Course"); 
                                                                
                 cmd.Append(" from [DDEFeeRecord_2017-18]"); 
                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2017-18].SRID");  
                 cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course"); 
                                      
                 cmd.Append(" where [DDEFeeRecord_2017-18].FeeHead='1' and [DDEFeeRecord_2017-18].ForExam='"+exam+"'"); 
                 cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"' and [DDEFeeRecord_2017-18].SRID not in (SELECT"); 
                 cmd.Append(" [DDEExamRecord_"+exam+"].[SRID]"); 
      
                 cmd.Append(" FROM [ddedbweb].[dbo].[DDEExamRecord_"+exam+"]"); 
  
                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEExamRecord_"+exam+"].SRID");

                 cmd.Append(" where StudyCentreCode='"+sccode+"' and MOE='R')");

                 cmd.Append(" union");

                 cmd.Append(" select [DDEFeeRecord_2018-19].SRID,[DDEFeeRecord_2018-19].ForYear,DDEStudentRecord.Course");

                 cmd.Append(" from [DDEFeeRecord_2018-19]");
                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2018-19].SRID");
                 cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                 cmd.Append(" where [DDEFeeRecord_2018-19].FeeHead='1' and [DDEFeeRecord_2018-19].ForExam='"+exam+"'");
                 cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"' and [DDEFeeRecord_2018-19].SRID not in (SELECT");
                 cmd.Append(" [DDEExamRecord_"+exam+"].[SRID]");

                 cmd.Append(" FROM [ddedbweb].[dbo].[DDEExamRecord_"+exam+"]");

                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEExamRecord_"+exam+"].SRID");

                 cmd.Append(" where StudyCentreCode='"+sccode+"' and MOE='R')");

                 cmd.Append(" union");

                 cmd.Append(" select [DDEFeeRecord_2019-20].SRID,[DDEFeeRecord_2019-20].ForYear,DDEStudentRecord.Course");

                 cmd.Append(" from [DDEFeeRecord_2019-20]");
                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2019-20].SRID");
                 cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                 cmd.Append(" where [DDEFeeRecord_2019-20].FeeHead='1' and [DDEFeeRecord_2019-20].ForExam='"+exam+"'");
                 cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"' and [DDEFeeRecord_2019-20].SRID not in (SELECT");
                 cmd.Append(" [DDEExamRecord_"+exam+"].[SRID]");

                 cmd.Append(" FROM [ddedbweb].[dbo].[DDEExamRecord_"+exam+"]");

                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEExamRecord_"+exam+"].SRID");

                 cmd.Append(" where StudyCentreCode='"+sccode+"' and MOE='R')");

                 cmd.Append(" union");

                 cmd.Append(" select [DDEFeeRecord_2020-21].SRID,[DDEFeeRecord_2020-21].ForYear,DDEStudentRecord.Course");

                 cmd.Append(" from [DDEFeeRecord_2020-21]");
                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2020-21].SRID");
                 cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                 cmd.Append(" where [DDEFeeRecord_2020-21].FeeHead='1' and [DDEFeeRecord_2020-21].ForExam='" + exam + "'");
                 cmd.Append(" and DDEStudentRecord.StudyCentreCode='" + sccode + "' and [DDEFeeRecord_2020-21].SRID not in (SELECT");
                 cmd.Append(" [DDEExamRecord_" + exam + "].[SRID]");

                 cmd.Append(" FROM [ddedbweb].[dbo].[DDEExamRecord_" + exam + "]");

                 cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEExamRecord_" + exam + "].SRID");

                 cmd.Append(" where StudyCentreCode='" + sccode + "' and MOE='R')");

            return cmd.ToString();
        }

        private string findCommand2(string sccode, string exam)
        {
            StringBuilder cmd = new StringBuilder();

            cmd.Append(" select [DDEFeeRecord_2020-21].SRID,[DDEFeeRecord_2020-21].ForYear,DDEStudentRecord.Course");

            cmd.Append(" from [DDEFeeRecord_2020-21]");
            cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2020-21].SRID");
            cmd.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

            cmd.Append(" where [DDEFeeRecord_2020-21].FeeHead='1' and [DDEFeeRecord_2020-21].ForExam='" + exam+"'");
            cmd.Append(" and DDEStudentRecord.StudyCentreCode='"+sccode+"' and [DDEFeeRecord_2020-21].SRID not in (SELECT");
            cmd.Append(" [DDEExamRecord_"+exam+"].[SRID]");

            cmd.Append(" FROM [ddedbweb].[dbo].[DDEExamRecord_"+exam+"]");

            cmd.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEExamRecord_"+exam+"].SRID");

            cmd.Append(" where StudyCentreCode='"+sccode+"' and MOE='R')");

            return cmd.ToString();
        }

        private int fillExamForm(int srid,int year,int cid,string exam, int ecid, string moe)
        {
            start:
            int examoutput=0;
            
            int counter = 0;
                       
            string rollno = allotRollNo(srid,cid, exam, moe, out counter);
                              
            if (!FindInfo.isRollNoAlreadyExist(rollno, exam))
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDEExamRecord_" + exam + " values(@SRID,@Year,@RollNo,@BPSubjects1,@BPSubjects2,@BPSubjects3,@BPPracticals1,@BPPracticals2,@BPPracticals3,@VECID,@ExamCentreCode,@ExamCentreCity,@ExamCentreZone,@MaxMarks,@ObtMarks,@QualifyingStatus,@MOE,@MSPrinted,@Times,@LastPrintTime,@MSCounter)", con);

                cmd.Parameters.AddWithValue("@SRID", srid);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@RollNo", rollno);
                cmd.Parameters.AddWithValue("@BPSubjects1", "");
                cmd.Parameters.AddWithValue("@BPSubjects2", "");
                cmd.Parameters.AddWithValue("@BPSubjects3", "");
                cmd.Parameters.AddWithValue("@BPPracticals1", "");
                cmd.Parameters.AddWithValue("@BPPracticals2", "");
                cmd.Parameters.AddWithValue("@BPPracticals3", "");
                cmd.Parameters.AddWithValue("@VECID", 0);
                cmd.Parameters.AddWithValue("@ExamCentreCode", ecid);
                cmd.Parameters.AddWithValue("@ExamCentreCity", "");
                cmd.Parameters.AddWithValue("@ExamCentreZone", "");
                cmd.Parameters.AddWithValue("@MaxMarks", 0);
                cmd.Parameters.AddWithValue("@ObtMarks", 0);
                cmd.Parameters.AddWithValue("@QualifyingStatus", "");
                cmd.Parameters.AddWithValue("@MOE", moe);
                cmd.Parameters.AddWithValue("@MSPrinted", "False");
                cmd.Parameters.AddWithValue("@Times", 0);
                cmd.Parameters.AddWithValue("@LastPrintTime", "");
                cmd.Parameters.AddWithValue("@MSCounter", 0);
               
                cmd.Connection = con;
                con.Open();
                examoutput= cmd.ExecuteNonQuery();
                con.Close();

                if (examoutput==1 && counter != 0)
                {
                    FindInfo.updateRollNoCounter(cid, counter, exam);
                }                     
                   
            }
            //else
            //{
            //    FindInfo.updateRollNoCounter(cid, counter, exam);
            //    goto start;

            //}

            return examoutput;
           
        }

        private string allotRollNo(int srid, int cid, string exam, string moe, out int counter)
        {
            counter = FindInfo.findRollNoCounter(cid, exam, moe);
            string rollno = exam + FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
            return rollno;
        }

        private int fillFee(int srid, int year, string exam, string asession, string ino, int itype,string idate,string ibn,int iamount,int amount,string amountinwords)
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into [DDEFeeRecord_"+asession+"] values(@OFRID,@SRID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@TotalDCAmount,@ForYear,@ForExam,@FRDate,@TOFS,@Verified,@VerifiedOn,@VerifiedBy,@EntryType)", con);

            cmd.Parameters.AddWithValue("@OFRID", 0);
            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@FeeHead", 2);
            cmd.Parameters.AddWithValue("@PaymentMode", itype);
            cmd.Parameters.AddWithValue("@DCNumber", ino);
            cmd.Parameters.AddWithValue("@DCDate", idate);
            cmd.Parameters.AddWithValue("@IBN", ibn);
            cmd.Parameters.AddWithValue("@Amount",amount);
            cmd.Parameters.AddWithValue("@AmountInWords",amountinwords.ToUpper());
            cmd.Parameters.AddWithValue("@TotalDCAmount",iamount);
            cmd.Parameters.AddWithValue("@ForYear", year);
            cmd.Parameters.AddWithValue("@ForExam", exam);
            cmd.Parameters.AddWithValue("@FRDate", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Verified", "False");
            cmd.Parameters.AddWithValue("@VerifiedOn", "");
            cmd.Parameters.AddWithValue("@VerifiedBy", 0);
            cmd.Parameters.AddWithValue("@EntryType", 1);

            cmd.Connection = con;
            con.Open();
            int output= cmd.ExecuteNonQuery();
            con.Close();

            return output;
        }

        private bool isExamFormEntered(int srid, int year, string exam)
        {

            bool exist = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_"+exam+" where SRID='" + srid + "' and Year='"+year+"' and MOE='R'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                exist = true;
            }
            else
            {
                exist = false;
            }

            con.Close();
            return exist;
        }

        protected void btnLateFee_Click(object sender, EventArgs e)
        {
            int count = 0;
            string sccode = "999";
            string exam = "Z11";

            string asession = "2020-21";
            int itype = 4;
            string ino = "";
            string iday = "14";
            string imonth = "01";
            string iyear = "2021";
            string idate = iyear + "-" + imonth + "-" + iday;
            string ibn = "NA";
            int totalamount = 3779590;
            int fhfee = 2743940;



            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\LateFee\June 2019\998.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString = "SELECT * FROM [Fresh$]";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {

                OleDbCommand command = new OleDbCommand(queryString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);

                for (int i = 5789; i < ds.Tables[0].Rows.Count; i++)
                {
                    string[] s = findStudentInfo(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());

                    int srid = Convert.ToInt32(s[0]);
                    int cid = Convert.ToInt32(s[1]);
                    string session = s[2].ToString();
                    //int batchid = FindInfo.findBatchID(session);
                    if (srid != 0)
                    {
                        string afexam;
                        if (!(feeAlreadyExist(srid, 12, Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]), out afexam)))
                        {
                            int latefee = Convert.ToInt32(ds.Tables[0].Rows[i]["LATE FEE"]);
                            string error;
                            if (validINo(fhfee, itype, ino, idate, ibn, latefee, out error))
                            {
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("insert into [DDEFeeRecord_" + asession + "] values(@OFRID,@SRID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@TotalDCAmount,@ForYear,@ForExam,@FRDate,@TOFS,@Verified,@VerifiedOn,@VerifiedBy,@EntryType)", con);

                                cmd.Parameters.AddWithValue("@OFRID", 0);
                                cmd.Parameters.AddWithValue("@SRID", srid);
                                cmd.Parameters.AddWithValue("@FeeHead", 12);
                                cmd.Parameters.AddWithValue("@PaymentMode", itype);
                                cmd.Parameters.AddWithValue("@DCNumber", ino);
                                cmd.Parameters.AddWithValue("@DCDate", idate);
                                cmd.Parameters.AddWithValue("@IBN", ibn);
                                cmd.Parameters.AddWithValue("@Amount", latefee);
                                cmd.Parameters.AddWithValue("@AmountInWords", Accounts.IntegerToWords(latefee));
                                cmd.Parameters.AddWithValue("@TotalDCAmount", totalamount);
                                cmd.Parameters.AddWithValue("@ForYear", Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                                cmd.Parameters.AddWithValue("@ForExam", exam);
                                cmd.Parameters.AddWithValue("@FRDate", DateTime.Now.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                                cmd.Parameters.AddWithValue("@Verified", "False");
                                cmd.Parameters.AddWithValue("@VerifiedOn", "");
                                cmd.Parameters.AddWithValue("@VerifiedBy", 0);
                                cmd.Parameters.AddWithValue("@EntryType", 1);

                                cmd.Connection = con;
                                con.Open();
                                int output = cmd.ExecuteNonQuery();
                                con.Close();

                                Log.createLogNow("Fee Submit", "Filled Late Course Fee of a student with Enrollment No '" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString(), 0);

                             

                                count = count + output;

                                //Response.Write("<br/>" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                            }
                            else
                            {
                                Response.Write("<br/> Sorry!! Remaining amount is less than required course fee");
                                break;
                            }

                        }
                        else
                        {
                            Response.Write("<br/>Late Course Fee is already exist for Exam : '" + exam + "' for ENo : " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                        }
                    }
                    else
                    {
                        Response.Write("<br/>SRID not found for ENo : " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                    }
                }

            }

            Response.Write("<br/>Total : " + (count).ToString() + " Students uploaded.");
        }

        private bool validINo(int fhfee, int itype, string ino, string idate, string ibn, int cfee, out string error)
        {
            error = "";
            bool valid = false;
            int fhusedfee = Accounts.findUsedAmountOfDraftByFH(12, itype, ino, idate, ibn);

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

        private string[] findStudentInfo(string eno)
        {

            string[] s = { "0", "0", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID,Course,Session from DDEStudentRecord where EnrollmentNo='" + eno + "' ", con);

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
                exam = dr["ForExam"].ToString();
                exist = true;
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

    }
}