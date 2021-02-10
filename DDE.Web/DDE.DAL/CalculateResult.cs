using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.DAL
{
   public class CalculateResult
    {

       int fototal=0;
       int fmtotal=0;
       string result="";
       string fstatus="CC";
        public string [] findResult(int srid,string moe)
        {

            string [] rdetail={"","",""};
            populateSubjectMarks(srid, moe);
            populatePracticalMarks(srid, moe);
            findResultStatus();

            rdetail[0] = fmtotal.ToString();
            rdetail[1] = fototal.ToString();
            rdetail[2] = result;

            return rdetail;
        }



        private void populateSubjectMarks(int srid, string moe)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEMarkSheet where SRID='" + srid + "' and MOE='" + moe + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();


            int styear = findYearOfStudent(srid);

            while (dr.Read())
            {
                int subyear = findYearOfSubject(Convert.ToInt32(dr["SubjectID"]));
                if (moe == "R")
                {
                    if (styear == subyear)
                    {

                        int total = getMarks(dr["Theory"].ToString()) + getMarks(dr["IA"].ToString()) + getMarks(dr["AW"].ToString());
                        findStatus(dr["Theory"].ToString(), dr["IA"].ToString(), dr["AW"].ToString());
                        fototal = fototal + total;
                        fmtotal = fmtotal + 100;
       
                    }
                }

                else if (moe == "B")
                {
                        int total = getMarks(dr["Theory"].ToString()) + getMarks(dr["IA"].ToString()) + getMarks(dr["AW"].ToString());
                        findStatus(dr["Theory"].ToString(), dr["IA"].ToString(), dr["AW"].ToString());
                        fototal = fototal + total;
                        fmtotal = fmtotal + 100;
                  
                }


            }
            
            con.Close();

        }

        private int findYearOfSubject(int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Year from DDESubject where SubjectID='" + subid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();


            dr.Read();

            string year = dr[0].ToString();

            con.Close();

            if (year == "1st Year")
            {
                return 1;
            }

            else if (year == "2nd Year")
            {
                return 2;
            }

            else if (year == "3rd Year")
            {
                return 3;
            }

            else if (year == "4th Year")
            {
                return 4;
            }

            else return 0;
        }

        private int findYearOfStudent(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CYear from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();


            dr.Read();

            int year = Convert.ToInt32(dr[0]);

            con.Close();

            return year;
        }


       
       

        private void populatePracticalMarks(int srid, string moe)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPracticalMarks where SRID='" + srid + "' and MOE='" + moe + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

    
            while (dr.Read())
            {

                fototal = fototal + getMarks(dr["PracticalMarks"].ToString());
                int mpm=findPracMaxMarks(Convert.ToInt32(dr["PracticalID"]));
                fmtotal = fmtotal +mpm ;
                findPracStatus(dr["PracticalMarks"].ToString(),mpm.ToString());
            }

         

            con.Close();

        }

        private int findPracMaxMarks(int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select PracticalMaxMarks from DDEPractical where PracticalID='" + pracid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            int pmm =Convert.ToInt32(dr[0]);
            con.Close();
            return pmm;
        }

        private void findPracStatus(string pracmarksobtained, string maxpracmarks)
        {
           
            int pracpercent = (getMarks(pracmarksobtained) * 100 / getMarks(maxpracmarks));

            if (pracpercent < 40)
            {
                fstatus = "NC";
            }

        }

        private void findResultStatus()
        {
           
           

            if (fstatus == "CC")
            {
                result = "AC";
            }

            else if (fstatus == "NC")
            {
                result = "PCP";
            }

        }

        private void findStatus(string tee, string ia, string aw)
        {

 
            int teepercent = (getMarks(tee) * 100) / 60;
            int iapercent = (getMarks(ia) * 100) / 20;
            int awpercent = (getMarks(aw) * 100) / 20;

            if (teepercent < 40 || iapercent < 40 || awpercent < 40)
            {
               
                fstatus = "NC";

            }

        }



        public static string findPracRemark(string pmarks,int totalmarks)
        {
            if (pmarks == "")
            {
                return "NC";
            }

            else if (pmarks == "AB")
            {
                return "NC";
            }
            else
            {
                if (getPercent(Convert.ToInt32(pmarks),totalmarks)>=40)
                {
                    return "CC";
                }

                else
                {
                    return "NC";
                }

            }
        }

        private static int getPercent(int marks, int totalmarks)
        {
            return (marks * 100) / totalmarks;
        }

        public static string findSubRemark(string tee, string ia, string aw)
        {

            string status = "";

            int teepercent = (getMarks(tee) * 100) / 100;
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

        private static int getMarks(string marks)
        {
            if (marks == "")
            {
                return 0;
            }
            else if (marks == "*")
            {
                return 0;
            }

            else if (marks == "AB")
            {
                return 0;
            }

            else return Convert.ToInt32(marks);
        }

        public static void calculateFullResult(int srid, string course, int year, string exam, out int tmarks, out int omarks, out string result, out string grade, out string division)
        {
            tmarks = 0;
            omarks = 0;
            result = "Not Set";
            grade = "";
            division = "";

            string sub = "";
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SubjectID from DDESubject where SyllabusSession='A 2010-11' and CourseName='" + course + "' and Year='" + FindInfo.findAlphaYear(year.ToString()) + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if(dr.HasRows)
            {
                while (dr.Read())
                {
                    tmarks = tmarks + 100;
                    if (sub == "")
                    {
                        sub = dr[0].ToString();
                    }
                    else
                    {
                        sub = sub + "," + dr[0].ToString();
                    }
               
                }
            }



            con.Close();


            if (sub != "")
            {
                omarks = findSubObtainedMarks(srid, sub, exam, out result);
            }

            string prac = "";
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("select PracticalID,PracticalMaxMarks from DDEPractical where SyllabusSession='A 2010-11' and CourseName='" + course + "' and Year='" + FindInfo.findAlphaYear(year.ToString()) + "'", con1);
            SqlDataReader dr1;

            con1.Open();
            dr1 = cmd1.ExecuteReader();

            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    tmarks = tmarks + Convert.ToInt32(dr1[1]);
                    if (prac == "")
                    {
                        prac = dr1[0].ToString();
                    }
                    else
                    {
                        prac = prac + "," + dr1[0].ToString();
                    }

                }
            }

            con1.Close();

            string presult = result;
           
            if (prac != "")
            {
                omarks =omarks+findPracObtainedMarks(srid, prac, exam, presult, out result);
            }

            grade = findGrade(tmarks,omarks);

          


            if (result == "Pass")
            {
                division = findDivision(tmarks, omarks);
            }
            else
            {
                division = "XX";
            }

        }

        private static string findGrade(int tmarks, int omarks)
        {
            string grade = "";
            int percent = 0;

            if (tmarks != 0)
            {
                percent = ((omarks * 100) / tmarks);
            }
           

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

            else if (percent < 40 && percent > 0)
            {
                grade = "D";
            }
            else if (percent == 0)
            {
                grade = "XX";
            }

            return grade;
        }

        private static string findDivision(int tmarks, int omarks)
        {
            string div = "";

            int percent = 0;

            if (tmarks != 0)
            {
                percent = (omarks * 100) / tmarks;
            }
         
           
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


                div = "Third";

            }
                

            return div;
        }

        private static int findPracObtainedMarks(int srid, string prac, string exam, string presult, out string result)
        {
            int omarks = 0;
            result = presult;
          

           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPracticalMarks_" + exam + " where SRID='" + srid + "' and PracticalID in (" + prac + ") and MOE='R'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int marks = getMarks(dr["PracticalMarks"].ToString());


                    omarks = omarks + marks;

                    if (presult == "Pass" || presult == "Not Found" || presult == "Not Set")
                    {

                        if (dr["PracticalMarks"].ToString() == "")
                        {
                            if (presult == "Fail" || presult == "Pass" || presult == "NotSet")
                            {
                                result = "Incomplete";
                            }
                        }
                        else if (marks < ((Exam.findMaximumPracticalMarks(Convert.ToInt32(dr["PracticalID"])) * 40) / 100))
                        {
                            if (presult != "Incomplete" && (presult == "Pass" || presult == "NotSet"))
                            {
                                result = "Fail";
                            }
                        }
                        else
                        {
                            if ((presult != "Incomplete" && presult != "Fail") || presult == "NotSet")
                            {
                                result = "Pass";
                            }
                        }
                    }
                  
                }
            }
            else
            {
                if (presult == "Not Found")
                {
                    result = "Not Found";
                }
                else
                {
                    result = "Incomplete";
                }
            }


            con.Close();

            return omarks;
        }

        private static int findSubObtainedMarks(int srid, string sub, string exam, out string result)
        {
            int omarks = 0;
            result = "NotSet";
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEMarkSheet_"+exam+" where SRID='"+srid+"' and SubjectID in ("+sub+") and MOE='R'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int th = getMarks(dr["Theory"].ToString());
                    int ia = getMarks(dr["IA"].ToString());
                    int aw = getMarks(dr["AW"].ToString());

                    omarks = omarks + ((th * 60) / 100) + ia + aw;

                    if (dr["Theory"].ToString() == "" || dr["IA"].ToString() == "" || dr["AW"].ToString() == "")
                    {
                        if (result == "Fail" || result == "Pass" || result == "NotSet")
                        {
                            result = "Incomplete";
                        }
                    }
                    else if (th < 40 || ia == 0 || aw == 0)
                    {
                        if (result != "Incomplete" && ( result == "Pass" || result == "NotSet"))
                        {
                            result = "Fail";
                        }
                    }
                    else
                    {
                        if ((result != "Fail" && result != "Incomplete") || result == "NotSet")
                        {
                            result = "Pass";
                        }
                    }
                   
                }
            }
            else
            {
                result = "Not Found";
              
            }



            con.Close();

            return omarks;
        }
    }
}
