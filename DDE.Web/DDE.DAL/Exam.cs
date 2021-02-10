using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.DAL
{
   public class Exam
    {

       public static void fillExamRecord(int srid, int fhid, int year, string rno, string exam, string bpsub1, string bpsub2, string bpsub3, string bpprac1, string bpprac2, string bpprac3, string examcentrecode, int ecid, string moe, out string rollno, out int ercounter, out string examrecorderror)
       {
               rollno = "";
               ercounter =0;
               examrecorderror = "";
               if (moe == "B")
               {
                   year = 0;
               }
               if (!examRecordExist(srid,year,exam,moe))
               {
                   int counter = 0;
                   string eno = FindInfo.findENoByID(srid);
                   int cid = FindInfo.findCourseIDBySRID(srid);
                   if (exam == "B12" || exam == "B13" || exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
                   { 
                       rollno = allotRollNo(srid, eno, cid, exam, moe, out counter);
                       if (exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
                       {
                           if (fhid == 26)
                           {
                               rollno = "R" + rollno;
                           }
                       }
                      
                   }
                   else if (exam == "A13")
                   {
                       if (moe == "B")
                       {
                           rollno = allotBPRollNo(srid);
                       }
                       examcentrecode = Exam.findECIDBySRID(srid,exam).ToString();
                   }
                   else
                   {
                       rollno = rno;
                   }

                   if (!FindInfo.isRollNoAlreadyExist(rollno, exam))
                   {

                       SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                       SqlCommand cmd = new SqlCommand("insert into DDEExamRecord_" + exam + " values(@SRID,@Year,@RollNo,@BPSubjects1,@BPSubjects2,@BPSubjects3,@BPPracticals1,@BPPracticals2,@BPPracticals3,@VECID,@ExamCentreCode,@ExamCentreCity,@ExamCentreZone,@MaxMarks,@ObtMarks,@QualifyingStatus,@MOE,@MSPrinted,@Times,@LastPrintTime,@MSCounter)", con);

                       cmd.Parameters.AddWithValue("@SRID", srid);
                       if (moe == "R")
                       {
                           cmd.Parameters.AddWithValue("@Year", year);
                       }
                       else if (moe == "B")
                       {
                           cmd.Parameters.AddWithValue("@Year", 0);
                       }

                       cmd.Parameters.AddWithValue("@RollNo", rollno);
                       cmd.Parameters.AddWithValue("@BPSubjects1", bpsub1);
                       cmd.Parameters.AddWithValue("@BPSubjects2", bpsub2);
                       cmd.Parameters.AddWithValue("@BPSubjects3", bpsub3);
                       cmd.Parameters.AddWithValue("@BPPracticals1", bpprac1);
                       cmd.Parameters.AddWithValue("@BPPracticals2", bpprac2);
                       cmd.Parameters.AddWithValue("@BPPracticals3", bpprac3);
                       cmd.Parameters.AddWithValue("@VECID",0);                     
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
                       cmd.Parameters.AddWithValue("@MSCounter", "");


                       cmd.Connection = con;
                       con.Open();
                       cmd.ExecuteNonQuery();
                       con.Close();

                       ercounter = findExamRecordCounter(srid, year, exam, moe);

                       if (exam != "A13")
                       {

                           if (counter != 0)
                           {
                               FindInfo.updateRollNoCounter(cid, counter, exam);
                           }
                       }
                   }
                   else
                   {
                       examrecorderror = "Sorry !! alloted roll no is already exist. This is due to any rare networking problem.<br/>Please delete Examination Fee of this form and re enter the Examination Form.";
                   }

               } 
        
       }

       private static int findExamRecordCounter(int srid,int year, string exam, string moe)
       {
           int ercounter = 0;

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlDataReader dr;
           SqlCommand cmd = new SqlCommand("Select ExamRecordID from DDEExamRecord_" + exam + " where SRID='" + srid + "' and Year='"+year+"' and MOE='" + moe + "'", con);
           con.Open();
           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {

               dr.Read();
               ercounter= Convert.ToInt32(dr[0]);
               
           }

           con.Close();

           return ercounter;
       }

       public static string allotRollNo(int srid,string eno,int cid, string exam,string moe, out int counter)
       {
           counter = 0;
           string rollno =""; 
           if (exam == "B12" || exam == "A13" || exam == "B13" || exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
           {
               if (moe == "R")
               {
                   string alrollno = "";
                   if (rollnoAlloted(srid,exam,"B", out alrollno))
                   {                      
                       rollno = alrollno.Substring(1,9);

                       if (FindInfo.isRollNoAlreadyExist(rollno, exam))
                       {
                           counter = FindInfo.findRollNoCounter(cid, exam, moe);
                           rollno = exam + FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
                       }

                   }
                   else
                   {
                       counter = FindInfo.findRollNoCounter(cid, exam, moe);
                       rollno = exam + FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
                   }
               }
               else if (moe == "B")
               {
                   string alrollno = "";
                   if (rollnoAlloted(srid,exam,"R", out alrollno))
                   {

                       rollno = "X"+alrollno;
                       if (FindInfo.isRollNoAlreadyExist(rollno, exam))
                       {
                           counter = FindInfo.findRollNoCounter(cid, exam, moe);
                           rollno = "X" + exam + FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
                       }
                   }
                   else
                   {
                       counter = FindInfo.findRollNoCounter(cid, exam, moe);
                       rollno = "X"+exam+ FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
                   }
               }

           }
           else 
           {
               string enrollmentno = "";

               if (eno.Substring(0, 2) == "09")
               {
                   eno = "A" + eno;
               }

               counter = FindInfo.findRollNoCounter(cid, exam, moe);

               if (moe == "R")
               {
                   enrollmentno = eno.Substring(0, 3) + FindInfo.findExamCounterByCode(exam).ToString() + FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
               }

               else if (moe == "B")
               {
                   enrollmentno = "X" + eno.Substring(0, 3) + FindInfo.findExamCounterByCode(exam).ToString() + FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
               }

               enrollmentno = rollno;

               return enrollmentno;
           }
           
           
           return rollno;

           
       }

        public static int updateQPSet(string examcode, string papercode, string moe)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEExaminationSchedules set SetQP=@SetQP where ExaminationCode='" + examcode + "' and PaperCode='"+papercode+"' and MOE='"+moe+"'", con);
            cmd.Parameters.AddWithValue("@SetQP", "True");

            con.Open();
            int i= cmd.ExecuteNonQuery();
            con.Close();

            return i;
        }

        private static bool rollnoAlloted(int srid,string exam, string moe, out string alrollno)
       {
           bool alotted = false;
           alrollno = "NF";

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlDataReader dr;
           SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_"+exam+" where SRID='" + srid  + "' and MOE='"+moe+"'", con);
           con.Open();
           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
              
               dr.Read();
               alrollno  = Convert.ToString(dr[0]);
               alotted = true;
           }

           con.Close();


           return alotted;
       }

       private static string  allotBPRollNo(int srid)
       {
           string rollno = "";
           string alotedrollno = findRollNo(srid);

           if (alotedrollno != "")
           {
              rollno = "X" + alotedrollno;
           }
           else
           {
               int courseid = FindInfo.findCourseIDBySRID(srid);
               int counter = findBPCounter(courseid);
               rollno = "XA13" + FindInfo.findCourseCodeByID(courseid) + string.Format("{0:0000}", counter);
               
               updateBPCounter(courseid, counter);

           }

           return rollno;

       }

       private static void updateBPCounter(int courseid, int counter)
       {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("update DDERollNoCounters set BPRollNoCounter_A13=@BPRollNoCounter_A13 where CourseID='" + courseid + "'", con);
           cmd.Parameters.AddWithValue("@BPRollNoCounter_A13", counter);

           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }

       private static int findBPCounter(int courseid)
       {
           string counter = "NA";

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlDataReader dr;
           SqlCommand cmd = new SqlCommand("Select BPRollNoCounter_A13 from DDERollNoCounters where CourseID='" + courseid + "'", con);
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

       private static string findRollNo(int srid)
       {
           string rollno = "";
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlDataReader dr;
           SqlCommand cmd = new SqlCommand("Select RollNo from ExamRecord_June13 where SRID='" + srid + "'", con);
           con.Open();
           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
               dr.Read();
               rollno = dr[0].ToString();



           }

           con.Close();

           return rollno;
       }

       public static bool examRecordExist(int srid, int year, string exam,string moe)
       {
           if (moe == "B")
           {
               year = 0;
           }
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("select * from DDEExamRecord_" + exam + " where SRID='" + srid + "' and Year='"+year+"' and MOE='"+moe+"'", con);
           SqlDataReader dr;

           bool exist = false;
           con.Open();
           dr = cmd.ExecuteReader();
           if (dr.HasRows)
           {
               exist = true;
           }

           con.Close();

           return exist;
       }

       public static void updateRollNo(int srid, int year, string rollno, string exam, string moe)
       {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("update DDEExamRecord_"+exam+" set RollNo=@RollNo where SRID='" + srid + "' and Year='"+year+"' and MOE='"+moe+"' ", con);

           cmd.Parameters.AddWithValue("@RollNo", rollno);
     
           cmd.Connection = con;
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }

       public static void updateExamCentre(int srid, int year, string exam, int ecid, string moe)
       {

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("update DDEExamRecord_" + exam + " set ExamCentreCode=@ExamCentreCode where SRID='" + srid + "'", con);

          
           cmd.Parameters.AddWithValue("@ExamCentreCode", ecid);
          

           cmd.Connection = con;
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }


       public static void updatePerticularExamCentre(int srid, int year, string exam, string city, string zone, string moe)
       {

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("update DDEExamRecord_" + exam + " set ExamCentreCity=@ExamCentreCity,ExamCentreZone=@ExamCentreZone where SRID='" + srid + "' and Year='"+year+"' and MOE='"+moe+"'", con);

           
           cmd.Parameters.AddWithValue("@ExamCentreCity", city);
           cmd.Parameters.AddWithValue("@ExamCentreZone", zone);

           cmd.Connection = con;
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }


       public static void updateBPRecord(int srid, string exam, string sub1, string sub2, string sub3, string prac1, string prac2, string prac3, string examcentrecode, int ecid, string moe, out string rollno, out int fcounter)
       {
            rollno = "";
            fcounter = 0;
            int counter = 0;
            string eno = FindInfo.findENoByID(srid);
            int cid = FindInfo.findCourseIDBySRID(srid);
            if (exam == "A13")
            {
                rollno = allotRollNo(srid, eno, cid, exam, moe, out counter);
                ecid = findECIDBySRID(srid, exam);


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEExamRecord_" + exam + " set RollNo=@RollNo,BPSubjects1=@BPSubjects1,BPSubjects2=@BPSubjects2,BPSubjects3=@BPSubjects3,ExamCentreCode=@ExamCentreCode,ExamCity=@ExamCity,ExamZone=@ExamZone where SRID='" + srid + "'", con);

                cmd.Parameters.AddWithValue("@RollNo", rollno);
                cmd.Parameters.AddWithValue("@BPSubjects1", sub1);
                cmd.Parameters.AddWithValue("@BPSubjects2", sub2);
                cmd.Parameters.AddWithValue("@BPSubjects3", sub3);
                cmd.Parameters.AddWithValue("@ExamCentreCode", ecid);
                cmd.Parameters.AddWithValue("@ExamCity", "");
                cmd.Parameters.AddWithValue("@ExamZone", "");

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (counter != 0)
                {
                    FindInfo.updateRollNoCounter(cid, counter, exam);
                }
            }
            else if (exam == "B13" || exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
            {
               
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand("update DDEExamRecord_" + exam + " set BPSubjects1=@BPSubjects1,BPSubjects2=@BPSubjects2,BPSubjects3=@BPSubjects3,BPPracticals1=@BPPracticals1,BPPracticals2=@BPPracticals2,BPPracticals3=@BPPracticals3 where SRID='" + srid + "' and MOE='B'", con);

                cmd.Parameters.AddWithValue("@BPSubjects1", sub1);
                cmd.Parameters.AddWithValue("@BPSubjects2", sub2);
                cmd.Parameters.AddWithValue("@BPSubjects3", sub3);
                cmd.Parameters.AddWithValue("@BPPracticals1", prac1);
                cmd.Parameters.AddWithValue("@BPPracticals2", prac2);
                cmd.Parameters.AddWithValue("@BPPracticals3", prac3);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                rollno = FindInfo.findRollNoBySRID(srid,exam,"B");
                //fcounter = FindInfo.findFormCounter(srid,0,exam,"B");
            }
       }

       private static int findECIDBySRID(int srid, string exam)
       {

            int ecid = findExamCentre(findStudyCentre(srid));

            return ecid;
        
       }

       private static int findExamCentre(string sccode)
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

       private static string findStudyCentre(int srid)
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

       private static string findTranferedSCCode(int srid)
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

       public static void updateMSPrintRecord_A13(int srid)
       {

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("update ExamRecord_June13 set MSPrinted=@MSPrinted,Times=@Times,LastPrintTime=@LastPrintTime where SRID='" + srid + "' ", con);

           cmd.Parameters.AddWithValue("@MSPrinted", "True");
           cmd.Parameters.AddWithValue("@Times", (findPreviousTimes(srid)+1));
           cmd.Parameters.AddWithValue("@LastPrintTime", DateTime.Now.ToString());
           
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }

       private static int findPreviousTimes(int srid)
       {
           int counter = 0;
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("Select Times from ExamRecord_June13 where SRID='" + srid + "'", con);
           con.Open();
           SqlDataReader dr;


           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
               dr.Read();
               counter = Convert.ToInt32(dr[0]);

           }

           con.Close();

           return counter;
       }

       public static void updateCurrentYear(int srid, int cyear)
       {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("update DDEStudentRecord set CYear=@CYear where SRID='" + srid + "' ", con);


           cmd.Parameters.AddWithValue("@CYear", cyear);
          

           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }

       public static bool isTheoryMarksFilled(int srid,int subid, string exam, string moe, out string  thmarks)
       {
           bool exist = false;
           thmarks = "";
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("select * from DDEMarkSheet_" + exam + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='" + moe + "'", con);
           SqlDataReader dr;

           
           con.Open();
           dr = cmd.ExecuteReader();
           if (dr.HasRows)
           {
               dr.Read();
               exist = true;
               thmarks = dr["Theory"].ToString();
           }

           con.Close();

           return exist;
       }

       public static bool isPracMarksFilled(int srid, int pracid, string exam, string moe, out string pmarks)
       {
           bool exist = false;
           pmarks = "";
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("select * from DDEPracticalMarks_" + exam + " where SRID='" + srid + "' and PracticalID='" + pracid + "' and MOE='" + moe + "'", con);
           SqlDataReader dr;


           con.Open();
           dr = cmd.ExecuteReader();
           if (dr.HasRows)
           {
               dr.Read();
               exist = true;
               pmarks = dr["PracticalMarks"].ToString();
           }

           con.Close();

           return exist;
       }

       public static void setYearStatus(int srid, int year)
       {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand();

           if (year == 1)
           {
               cmd.CommandText = "update DDEStudentRecord set FirstYear=@FirstYear where SRID='" + srid + "'";
               cmd.Parameters.AddWithValue("@FirstYear", "True");
           }
           else if (year == 2)
           {
               cmd.CommandText = "update DDEStudentRecord set SecondYear=@SecondYear where SRID='" + srid + "'";
               cmd.Parameters.AddWithValue("@SecondYear", "True");
           }
           else if (year == 3)
           {
               cmd.CommandText = "update DDEStudentRecord set ThirdYear=@ThirdYear where SRID='" + srid + "'";
               cmd.Parameters.AddWithValue("@ThirdYear", "True");
           }


           cmd.Connection = con;

           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }

       public static void updateMSPrintRecord(int srid,int year,int mm,int om, string qs, string exam,int counter,string printmode, string moe)
       {
                     
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand();
           if (printmode == "N")
           {
               if (moe == "R")
               {
                   cmd.CommandText = "update DDEExamRecord_" + exam + " set MaxMarks=@MaxMarks,ObtMarks=@ObtMarks,QualifyingStatus=@QualifyingStatus,MSPrinted=@MSPrinted,Times=@Times,LastPrintTime=@LastPrintTime where SRID='" + srid + "' and Year='" + year + "' and MOE='R'";
               }
               else if (moe == "B")
               {
                   cmd.CommandText = "update DDEExamRecord_" + exam + " set MaxMarks=@MaxMarks,ObtMarks=@ObtMarks,QualifyingStatus=@QualifyingStatus,MSPrinted=@MSPrinted,Times=@Times,LastPrintTime=@LastPrintTime where SRID='" + srid + "' and MOE='B'";
               }


               cmd.Parameters.AddWithValue("@MaxMarks", mm);
               cmd.Parameters.AddWithValue("@ObtMarks", om);
               cmd.Parameters.AddWithValue("@QualifyingStatus", qs);
               cmd.Parameters.AddWithValue("@MSPrinted", "True");
               cmd.Parameters.AddWithValue("@Times", (findPreviousTimes(srid, year, exam,counter,printmode, moe) + 1));
               cmd.Parameters.AddWithValue("@LastPrintTime", DateTime.Now.ToString());
           }
           else if (printmode == "C" || printmode == "D")
           {
               cmd.CommandText = "update DDEMSCountersDC set Times=@Times,LastPrintTime=@LastPrintTime where SRID='" + srid + "' and Year='" + year + "' and MSCounter='" + counter + "' and MOE='"+moe+"'";
               cmd.Parameters.AddWithValue("@Times", (findPreviousTimes(srid, year, exam,counter,printmode, moe) + 1));
               cmd.Parameters.AddWithValue("@LastPrintTime", DateTime.Now.ToString());
              
           }
          

           cmd.Connection = con;
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }
       public static void updateMSPrintRecordAndCounter(int srid, int year, int mm, int om, string qs, string exam, int counter,string printmode,string moe, int erid)
       {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand();

           if (printmode == "N")
           {

               if (moe == "R")
               {
                   cmd.CommandText = "update DDEExamRecord_" + exam + " set MaxMarks=@MaxMarks,ObtMarks=@ObtMarks,QualifyingStatus=@QualifyingStatus,MSPrinted=@MSPrinted,Times=@Times,LastPrintTime=@LastPrintTime,MSCounter=@MSCounter where SRID='" + srid + "' and Year='" + year + "' and MOE='R'";
               }
               else if (moe == "B")
               {
                   cmd.CommandText = "update DDEExamRecord_" + exam + " set MaxMarks=@MaxMarks,ObtMarks=@ObtMarks,QualifyingStatus=@QualifyingStatus,MSPrinted=@MSPrinted,Times=@Times,LastPrintTime=@LastPrintTime,MSCounter=@MSCounter where SRID='" + srid + "' and MOE='B'";
               }

               cmd.Parameters.AddWithValue("@MaxMarks", mm);
               cmd.Parameters.AddWithValue("@ObtMarks", om);
               cmd.Parameters.AddWithValue("@QualifyingStatus", qs);
               cmd.Parameters.AddWithValue("@MSPrinted", "True");
               cmd.Parameters.AddWithValue("@Times", (findPreviousTimes(srid, year, exam,counter,printmode, moe) + 1));
               cmd.Parameters.AddWithValue("@LastPrintTime", DateTime.Now.ToString());
               cmd.Parameters.AddWithValue("@MSCounter", counter);
           }
           if (printmode == "C" || printmode == "D")
           {

               cmd.CommandText = "insert into DDEMSCountersDC values(@MSCounter,@SRID,@Year,@Exam,@Mode,@Times,@LastPrintTime,@PrintedBy,@MOE)";

               cmd.Parameters.AddWithValue("@MSCounter",counter);
               cmd.Parameters.AddWithValue("@SRID", srid);
               cmd.Parameters.AddWithValue("@Year", year);
               cmd.Parameters.AddWithValue("@Exam", exam);            
               cmd.Parameters.AddWithValue("@Mode", printmode);
               cmd.Parameters.AddWithValue("@Times",1);
               cmd.Parameters.AddWithValue("@LastPrintTime", DateTime.Now.ToString());
               cmd.Parameters.AddWithValue("@PrintedBy", erid);
               cmd.Parameters.AddWithValue("@MOE", moe);
           }

          

           cmd.Connection = con;
           con.Open();
           cmd.ExecuteNonQuery();
           con.Close();
       }
       private static int findPreviousTimes(int srid,int year, string exam,int mscounter,string printmode, string moe)
       {
           int times = 0;
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand();
           if (printmode == "N")
           {
               cmd.CommandText = "Select Times from DDEExamRecord_" + exam + " where SRID='" + srid + "' and Year='" + year + "' and MOE='" + moe + "'";
           }
           else if (printmode == "C" || printmode == "D")
           {
               cmd.CommandText = "Select Times from DDEMSCountersDC where SRID='" + srid + "' and Year='" + year + "' and MSCounter='"+mscounter+"' and MOE='" + moe + "'";
           }

           cmd.Connection = con;
           con.Open();
           SqlDataReader dr;


           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
               dr.Read();
               times= Convert.ToInt32(dr[0]);

           }

           con.Close();

           return times;
       }

     

       public static void fillMarks(int srid, int subid, string exam, string moe,string sccode, string marks)
       {
           if (marksAlreadyFilled(srid, subid, exam, moe))
           {
               SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
               SqlCommand cmd = new SqlCommand("update DDEMarkSheet_" + exam + " set Theory=@Theory where SRID='" + srid + "' and SubjectID='"+subid+"' and MOE='" + moe + "'", con);

               cmd.Parameters.AddWithValue("@Theory", marks);
             
               con.Open();
               cmd.ExecuteNonQuery();
               con.Close();
           }
           else
           {
               SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
               SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_" + exam + " values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);


               cmd.Parameters.AddWithValue("@SRID", srid);
               cmd.Parameters.AddWithValue("@SubjectID",subid);
               cmd.Parameters.AddWithValue("@StudyCentreCode", sccode);
               cmd.Parameters.AddWithValue("@Theory", marks);
               cmd.Parameters.AddWithValue("@IA", "");
               cmd.Parameters.AddWithValue("@AW", "");
               cmd.Parameters.AddWithValue("@MOE", moe);


               cmd.Connection = con;
               con.Open();
               cmd.ExecuteNonQuery();
               con.Close();
           }
       }

       private static bool marksAlreadyFilled(int srid, int subid, string exam, string moe)
       {
           bool exist = false;
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("Select * from DDEMarkSheet_" + exam + " where SRID='" + srid + "' and SubjectID='"+subid+"' and MOE='" + moe + "'", con);
           con.Open();
           SqlDataReader dr;


           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
               exist = true;

           }

           con.Close();

           return exist;
       }

       public static string findNewTheoryMarks(int srid, int subid, string exam)
       {
           string marks = "";
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("Select Theory from DDEMarkSheet_" + exam + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='B'", con);
           con.Open();
           SqlDataReader dr;


           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
               dr.Read();
               if (dr["Theory"].ToString() == "")
               {
                   marks = "";
               }
               else if (dr["Theory"].ToString() == "-")
               {
                   marks = "-";
               }
               else if (dr["Theory"].ToString() == "AB")
               {
                   marks = "AB";
               }
               else
               {
                   marks = ((Convert.ToInt32(dr["Theory"]) * 60) / 100).ToString();
               }


           }

           con.Close();
           return marks;
       }

       public static string findNewPracticalMarks(int srid, int pracid, string exam)
       {
           string marks = "";
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("Select PracticalMarks from DDEPracticalMarks_" + exam + " where SRID='" + srid + "' and PracticalID='" + pracid + "' and MOE='B'", con);
           con.Open();
           SqlDataReader dr;


           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
               dr.Read();
               if (dr["PracticalMarks"].ToString() == "")
               {
                   marks = "";
               }
               else if (dr["PracticalMarks"].ToString() == "-")
               {
                   marks = "-";
               }
               else if (dr["PracticalMarks"].ToString() == "AB")
               {
                   marks = "AB";
               }
               else
               {
                   marks = (Convert.ToInt32(dr["PracticalMarks"])).ToString();
               }


           }

           con.Close();
           return marks;
       }

       public static string findPreviousMaximumTheoryMarks(int srid, int subid, out string mode, out string preexam)
       {
           string[,] str = { { "", "","" }, { "", "","" }, { "", "","" }, { "", "","" } };
           string mm = "";
           mode = "";
           preexam = "";
           int i = 0;
           int j = 0;
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlDataReader dr;
           SqlCommand cmd = new SqlCommand("Select ExamCode from DDEExaminations where Online='True'", con);

           con.Open();
           dr = cmd.ExecuteReader();
           if (dr.HasRows)
           {

               while (dr.Read())
               {

                   SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                   SqlDataReader dr1;
                   SqlCommand cmd1 = new SqlCommand("select Theory,MOE from DDEMarkSheet_" + dr[0].ToString() + " where SRID='" + srid + "' and SubjectID='" + subid + "'", con1);

                   con1.Open();
                   dr1 = cmd1.ExecuteReader();
                   if (dr1.HasRows)
                   {
                       while (dr1.Read())
                       {
                           str[i, j] = dr1[0].ToString();
                           str[i, (j + 1)] = dr1[1].ToString();
                           str[i, (j + 2)] = dr[0].ToString();
                           i = i + 1;

                       }
                   }
                   con1.Close();
               }
           }
           con.Close();

           int max = getMarks(str[0, 0]);
           mm = str[0, 0];
           mode = str[0, 1];
           preexam = str[0, 2];

           for (int k = 0; k < (i); k++)
           {
               if (getMarks(str[k, 0]) > max)
               {
                   max = getMarks(str[k, 0]);
                   mm = str[k, 0];
                   mode = str[k, 1];
                   preexam = str[k, 2];
               }
           }

           return mm;
       }

       private static int getMarks(string marks)
       {
           if (marks == "" || marks == "-" || marks == "AB" || marks == "NF" || marks == "*")
           {
               return 0;
           }

           else return Convert.ToInt32(marks);
          
       }

       public static string[] findPreviousInternalMarks(int srid, int subid, string moe)
       {
           string[] str = { "", "" };
           bool found = false;

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlDataReader dr;
           SqlCommand cmd = new SqlCommand("Select ExamCode from DDEExaminations where Online='True'", con);

           con.Open();
           dr = cmd.ExecuteReader();
           if (dr.HasRows)
           {


               while (dr.Read())
               {

                   SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                   SqlDataReader dr1;
                   SqlCommand cmd1 = new SqlCommand("select IA,AW from DDEMarkSheet_" + dr[0].ToString() + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='" + moe + "'", con1);

                   con1.Open();
                   dr1 = cmd1.ExecuteReader();
                   if (dr1.HasRows)
                   {
                       while (dr1.Read())
                       {
                           str[0] = dr1[0].ToString();
                           str[1] = dr1[1].ToString();
                           found = true;
                           break;


                       }
                   }
                   con1.Close();

                   if (found == true)
                   {
                       break;
                   }


               }
           }
           con.Close();

           return str;
       }


       public static string findPreviousMaximumPracticalMarks(int srid, int pracid, out string mode)
       {
           string[,] str = { { "", "" }, { "", "" }, { "", "" }, { "", "" } };
           string mm = "";
           mode = "";
           int i = 0;
           int j = 0;
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlDataReader dr;
           SqlCommand cmd = new SqlCommand("Select ExamCode from DDEExaminations where Online='True'", con);

           con.Open();
           dr = cmd.ExecuteReader();
           if (dr.HasRows)
           {

               while (dr.Read())
               {

                   SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                   SqlDataReader dr1;
                   SqlCommand cmd1 = new SqlCommand("select PracticalMarks,MOE from DDEPracticalMarks_" + dr[0].ToString() + " where SRID='" + srid + "' and PracticalID='" + pracid + "'", con1);

                   con1.Open();
                   dr1 = cmd1.ExecuteReader();
                   if (dr1.HasRows)
                   {
                       while (dr1.Read())
                       {
                           str[i, j] = dr1[0].ToString();
                           str[i, (j + 1)] = dr1[1].ToString();

                           i = i + 1;

                       }
                   }
                   con1.Close();
               }
           }
           con.Close();

           int max = getMarks(str[0, 0]);
           mm = str[0, 0];
           mode = str[0, 1];

           for (int k = 0; k < (i); k++)
           {
               if (getMarks(str[k, 0]) > max)
               {
                   max = getMarks(str[k, 0]);
                   mm = str[k, 0];
                   mode = str[k, 1];
               }
           }

           return mm;
       }

       internal static int findMaximumPracticalMarks(int pracid)
       {
           int maxmarks = 0;
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("Select PracticalMaxMarks from DDEPractical where PracticalID='" + pracid + "'", con);
           con.Open();
           SqlDataReader dr;


           dr = cmd.ExecuteReader();

           if (dr.HasRows)
           {
               dr.Read();
               maxmarks = Convert.ToInt32(dr[0]);

           }

           con.Close();
           return maxmarks;
       }

       public static void updateMarks(int p, int p_2, string p_3, string p_4, string p_5, string p_6)
       {
           throw new NotImplementedException();
       }

       public static bool isQuestionPaperAlreadySet(string examcode, string pcode)
       {
           bool exist = false;
          
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("Select * from DDEQuestionPapers where ExamCode='" + examcode + "' and PaperCode='"+pcode+"'", con);
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

        public static bool isQuestionPaperAlreadySet(string examcode, string pcode, string moe)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where ExaminationCode='" + examcode + "' and PaperCode='" + pcode + "' and MOE='"+moe+"' and SetQP='True'", con);
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

        public static bool isQuestionPaperOnline(string examcode, string pcode)
       {
           bool exist = false;

           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
           SqlCommand cmd = new SqlCommand("Select Status from DDEQuestionPapers where ExamCode='" + examcode + "' and PaperCode='" + pcode + "' and Status='True'", con);
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
    }
}
