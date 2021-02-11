using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;

namespace DDE.DAL
{
    public class FindInfo
    {

        public static int findUnitIDByERID(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CollegeOrUnit from SVSUEmployeeRecord where ERID ='" + erid + "' ", con);
            SqlDataReader dr;
            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            int unitid = Convert.ToInt32(dr[0]);
            con.Close();
            return unitid;

        }

        public static int findExamCentreBySCCode(string sccode, string exam)
        {
            int ecid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select ECID from DDEExaminationCentres_"+exam+" where SCCodes like '%" +sccode  + "%'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
               
                dr.Read();
                ecid = Convert.ToInt32(dr[0]);
            }
            con.Close();
            return ecid;
        }

      

        public static int findUnitIDByUnitName(string unitname)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select UnitID from Units where UnitShortName ='" + unitname + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            int unitid = Convert.ToInt32(dr[0]);
            con.Close();
            return unitid;


        }

        public static bool isExamCentreSetBySRID(int srid, string exam, string moe)
        {
            bool set = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select ExamRecordID from DDEExamRecord_" + exam + " where SRID='" + srid+ "' and MOE='"+moe+"' and VECID!=0", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                set = true;
            }
            con.Close();
            return set;
        }

       
        public static int findTotalFormsAllotted(string session, int exid)
        {
            int tf = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select Count(PSRID) as TF from DDEPendingStudentRecord where [Session]='" + session + "' and ExID='" + exid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tf = Convert.ToInt32(dr["TF"]);
            }
            con.Close();
            return tf;
        }

        public static int findTotalFormsChecked(string session, int exid)
        {
            int tf = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select Count(PSRID) as TF from DDEPendingStudentRecord where [Session]='" + session + "' and ExID='" + exid + "' and Eligible!=''", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tf = Convert.ToInt32(dr["TF"]);
            }
            con.Close();
            return tf;
        }

        public static bool isAssignmentUploaded(int srid, string exam)
        {
            bool exist = false;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select * from DDEStudentAssignments where SRID='" + srid + "'", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();

            if (dr1.HasRows)
            {

                exist = true;


            }

            con1.Close();
            return exist;
        }

        public static string findRoleNameByRollID(int Roleid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select RoleName from Roles where RoleID='" + Roleid + "'", con);
            SqlDataReader dr;

            string Rolename = "";

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Rolename = dr[0].ToString();
            }

            con.Close();
            return Rolename;

        }

        public static string findExamNameHindi(string examname)
        {
            string examhindi = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select ExamNameHindi from DDEExaminations where ExamName='" + examname + "'", con);
            SqlDataReader dr;



            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                examhindi = dr[0].ToString();
            }
            con.Close();
            return examhindi;
        }

      

        public static string findLoginTypeByLTID(int ltid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select LoginType from LoginType where LoginTypeID='" + ltid + "'", con);
            SqlDataReader dr;



            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string logintype = dr[0].ToString();
            con.Close();
            return logintype;
        }

        public static string findDivHindi(string div)
        {
            if(div=="FIRST")
            {
                return "प्रथम";
            }
            else if (div == "SECOND")
            {
                return "द्वितीय";
            }
            else if (div == "THIRD")
            {
                return "तृतीय";
            }
            else
            {
                return "NF";
            }
        }

        
        public static string findEmployeeDetailByERID(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select EmployeeID,Name,Designation,CollegeOrUnit,Department from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string empdetail = "<b>" + dr["Name"].ToString() + "</b><br />" + findDesignationNameByID(Convert.ToInt32(dr["Designation"])) + ",<br />" + findDepartmentNameByID(Convert.ToInt32(dr["Department"])) + ",<br />" + findUnitNameByUnitID(Convert.ToInt32(dr["CollegeOrUnit"])) + ".";
            con.Close();
            return empdetail;
        }

        public static string findSMSSenderID(out int smsbalance)
        {
            smsbalance = 10000;
            return "SVSUDE";
        }

        public static string findEmployeeNameByERID(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select Name,EmployeeID from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            SqlDataReader dr;


            string empdetail = "";
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                empdetail = dr["Name"].ToString() + " (" + dr["EmployeeID"].ToString() + ")";
            }
            con.Close();
            return empdetail;
        }

      

        public static int updateSMSCounter(int v1, int v2)
        {
            throw new NotImplementedException();
        }

        public static int findUnReadMsgByGID(int gid)
        {
            int newmsg = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Count(GTID) from DDEGrievanceTransactions where GID='" + gid + "' and QueryRead='False'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                newmsg = Convert.ToInt32(dr[0]);
            }

            con.Close();
            return newmsg;
        }

        public static string findLinearEmployeeDetailByERID(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select EmployeeID,Name,Designation,CollegeOrUnit,Department from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            SqlDataReader dr;



            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string empdetail = "<b>" + dr["Name"].ToString() + "</b>, " + dr["Designation"].ToString() + ", " + dr["Department"].ToString() + ", " + dr["CollegeOrUnit"].ToString() + ".";
            con.Close();
            return empdetail;
        }

        public static int findExamIDByExamCode(string examcode)
        {
            int examid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamID from DDEExaminations where ExamCode='" + examcode+ "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                examid = Convert.ToInt32(dr[0]);
            }

            con.Close();
            return examid;
        }

        public static string findVExamCentreBySRID(int srid, string exam, string moe)
        {
            string ec = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select DDEVExamCenters.ExamCenterName,DDEVExamCenters.City from DDEExamRecord_"+exam+ " inner join DDEVExamCenters on DDEExamRecord_" + exam + ".VECID=DDEVExamCenters.VECID where DDEExamRecord_" + exam + ".SRID='" + srid + "' and DDEExamRecord_" + exam + ".MOE='" + moe + "' and DDEExamRecord_" + exam + ".VECID!=0", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ec = dr["ExamCenterName"].ToString()+", "+ dr["City"].ToString();
            }

            con.Close();      

            return ec;
        }

        public static int findDomainIDByRollID(int rollid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select DomainID from Roles where RoleID='" + rollid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            int domainid = Convert.ToInt32(dr[0].ToString());


            con.Close();

            return domainid;


        }

        public static string findLastLogoutTimeByERID(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select LastLogoutTime from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string lastlogouttime = dr[0].ToString();


            con.Close();

            return lastlogouttime;


        }

        public static string findMonthByMonthNo(int monthno)
        {


            if (monthno == 1)
            {
                return "January";

            }

            else if (monthno == 2)
            {

                return "February";
            }
            else if (monthno == 3)
            {

                return "March";
            }
            else if (monthno == 4)
            {

                return "April";
            }
            else if (monthno == 5)
            {

                return "May";
            }
            else if (monthno == 6)
            {

                return "June";
            }
            else if (monthno == 7)
            {

                return "July";
            }
            else if (monthno == 8)
            {

                return "August";
            }
            else if (monthno == 9)
            {

                return "September";
            }
            else if (monthno == 10)
            {

                return "October";
            }
            else if (monthno == 11)
            {

                return "November";
            }
            else if (monthno == 12)
            {

                return "December";
            }

            else return "";

        }



        public static int noOfLoggedIn(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select NoTimesLoggedIn  from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            int noofloggedin = Convert.ToInt32(dr[0]);
            con.Close();

            return noofloggedin;

        }


        public static string findCategoryIDbyCategoryName(string categoryName)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CATID from Category where CategoryName='" + categoryName + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string catname = dr[0].ToString();
            con.Close();

            return catname;

        }

        public static string findCategoryNameByID(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CategoryName from Category where CATID ='" + id + "' ", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string Catname = dr[0].ToString();
            con.Close();

            return Catname;
        }

        public static string findUnitNameByUnitID(int unitid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select UnitShortName from Units where UnitID ='" + unitid + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string unitname = dr[0].ToString();
            con.Close();

            return unitname;
        }

        public static string findDepartmentNameByID(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select DepartmentName from Departments where DepartmentID ='" + id + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string depname = dr[0].ToString();
            con.Close();

            return depname;

        }

        public static string findIDByDepartmentName(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select DepartmentID from Departments where DepartmentName  ='" + id + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string depname = dr[0].ToString();
            con.Close();

            return depname;

        }
        public static string findDesignationNameByID(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select DesignationName from EmpDesignation where DesignationID ='" + id + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string desname = dr[0].ToString();
            con.Close();

            return desname;

        }
        public static string findClassNameByID(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select ClassName from EmpClass where CLASSID ='" + id + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string ctsname = dr[0].ToString();
            con.Close();

            return ctsname;

        }
        public static string findQualificationNameByID(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select QualiName from EmpQualification where QUALIID ='" + id + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string Qualiname = dr[0].ToString();
            con.Close();

            return Qualiname;

        }
        public static int findIDByDesignationName(string Name)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select DesignationID from EmpDesignation where DesignationName ='" + Name + "' ", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            int desid = Convert.ToInt32(dr[0].ToString());
            con.Close();

            return desid;

        }

        public static string findClassTypecodeByid(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select ClassCode from EmpClass where CLASSID ='" + id + "' ", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string code = dr[0].ToString();
            con.Close();

            return code;

        }
        public static string findDesignationcodeByid(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select DesignationCode from EmpDesignation where DesignationID ='" + id + "' ", con);
            SqlDataReader dr;


            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string code = dr[0].ToString();
            con.Close();

            return code;

        }
        public static string findunitcodeByid(string id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select UnitCode from Units where UnitID ='" + id + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string code = dr[0].ToString();
            con.Close();

            return code;

        }


        public static string findCourseNameByID(int courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseShortName,Specialization from DDECourse where CourseID ='" + courseid + "' ", con);
            SqlDataReader dr;


            string course = "";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[1].ToString() == "")
                {
                    course = dr[0].ToString();
                }

                else
                {
                    course = dr[0].ToString() + " (" + dr[1].ToString() + ")";
                }
            }
            con.Close();

            return course;
        }

        public static string findCourseFullNameByID(int courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseFullName from DDECourse where CourseID ='" + courseid + "' ", con);
            SqlDataReader dr;


            string course = "";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                course = dr[0].ToString();

            }
            con.Close();

            return course;
        }

        public static void findCourseDetailsForDegreeByCourseID(int courseid, out string cne, out string cnh, out string spe, out string sph)
        {
            cne = "X";
            cnh = "X";
            spe = "X";
            sph = "X";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseFullNameDegree,CourseFullNameHindi,SpecializationDegree,SpecializationHindi from DDECourse where CourseID ='" + courseid + "'", con);
            SqlDataReader dr;

            string course = "";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                cne = dr["CourseFullNameDegree"].ToString();
                cnh = dr["CourseFullNameHindi"].ToString();

                if(dr["SpecializationDegree"].ToString()=="")
                {
                    spe = "X";
                    sph = "X";
                }
                else
                {
                    spe = dr["SpecializationDegree"].ToString();
                    sph = dr["SpecializationHindi"].ToString();
                }
                             

            }
            con.Close();
           

        }

        public static string findRelativeWorkPlaceByID(int id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select RelatWorkName from EmpRelativeWork where RELATID ='" + id + "' ", con);
            SqlDataReader dr;


            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string rwplace = dr[0].ToString();
            con.Close();

            return rwplace;

        }

        public static int findLTIDByERID(int erid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select LoginTypeID from Users where ERID='" + erid + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            int ltid = Convert.ToInt32(dr[0]);
            con.Close();

            return ltid;

        }



        public static string findCourseIDByName(string coursename)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseID from Courses where CourseName ='" + coursename + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string courseid = dr[0].ToString();
            con.Close();

            return courseid;
        }

        public static string findENoByID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select EnrollmentNo from DDEStudentRecord where SRID ='" + srid + "' ", con);
            SqlDataReader dr;


            string eno = "NA";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                eno = dr[0].ToString();
            }
            con.Close();

            return eno;
        }



        public static string findPracticalDetailByID(int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select PracticalName,CourseName from DDEPractical where PracticalID ='" + pracid + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string pracdetail = dr[0].ToString() + " in " + dr[1].ToString();
            con.Close();

            return pracdetail;

        }

        public static string findSubjectDetailByID(int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select SubjectName,CourseName from DDESubject where SubjectID ='" + subid + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            string pracdetail = dr[0].ToString() + " in " + dr[1].ToString();
            con.Close();

            return pracdetail;
        }


        public static int findYearOfSubject(int subid)
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

        public static int findYearOfStudent(int srid)
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

        public static string findAlphaYearOfStudent(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CYear from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();


            dr.Read();

            int year = Convert.ToInt32(dr[0]);

            con.Close();

            if (year == 1)
            {
                return "1st Year";
            }
            else if (year == 2)
            {
                return "2nd Year";
            }

            else if (year == 3)
            {
                return "3rd Year";
            }

            else
            {
                return "";
            }
        }

        public static string findDOABySRID(int srid)
        {
            string doa = "";
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select VDOA from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                doa = Convert.ToDateTime(dr[0]).ToString("dd-MM-yyyy");
               
                
            }

            con.Close();
  
            return doa;
        }

        public static string findDOCByExam(string examcode)
        {
            string doe = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ResultDeclaredOn from DDEExaminations where ExamCode='" + examcode + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if(dr.HasRows)
            {
                dr.Read();
                doe=Convert.ToDateTime(dr[0]).ToString("dd-MM-yyyy");
            }
           
            con.Close();

            return doe;
        }

        public static string findQStatusByID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select QualifyingStatus from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();


            dr.Read();

            string qs = dr[0].ToString();

            con.Close();

            return qs;
        }

        public static int findYearOfPractical(int pracid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Year from DDEPractical where PracticalID='" + pracid + "' ", con);
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

        public static bool oneYearCourse(int cid)
        {

            if (cid == 22 || cid == 9 || cid == 14 || cid == 34 || cid == 95 || cid == 25 || cid == 23 || cid == 6 || cid == 24 || cid == 21)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string removeSpace(string sp)
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

        private static string findchar(string oldstr)
        {

            if (oldstr == " ")
            {
                return "_";
            }

            else return oldstr;
        }

        public static string findCityByID(int cityid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select City from CityList where CityID='" + cityid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();


            dr.Read();

            string city = dr[0].ToString();

            con.Close();

            return city;
        }

        public static int findCourseIDBySRID(int srid)
        {
            int cid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Course from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                cid=  Convert.ToInt32(dr[0]);
            }

            con.Close();


            return cid;
        }

        public static int findCourse2YearIDBySRID(int srid)
        {
            int cid = 0;
            try
            {
               
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select Course2Year from DDEStudentRecord where SRID='" + srid + "' ", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                    cid = Convert.ToInt32(dr[0]);
                }

                con.Close();
            }
            catch
            {
                cid = 0;
            }


            return cid;
        }
        public static int findRollNoCounter(int courseid, string exam, string moe)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            string col = "";
            if (exam == "B13" || exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
            {
                col = "RollNoCounter_" + exam;
            }
            else
            {
                if (moe == "R")
                {
                    col = "RollNoCounter_" + exam;
                }

                else if (moe == "B")
                {
                    col = "BPRollNoCounter_" + exam;
                }
            }

            SqlCommand cmd = new SqlCommand("Select " + col + " from DDERollNoCounters where CourseID='" + courseid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();

            int counter = Convert.ToInt32(dr[0]);

            con.Close();

            return counter + 1;
        }

        public static string findCourseCodeByID(int courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseCode from DDECourse where CourseID='" + courseid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            string ccode = dr[0].ToString();
            con.Close();

            return ccode;
        }

        public static void updateRollNoCounter(int courseid, int newcounter, string exam)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDERollNoCounters set RollNoCounter_" + exam + "=@RollNoCounter_" + exam + " where CourseID='" + courseid + "' ", con);
            cmd.Parameters.AddWithValue("@RollNoCounter_" + exam + "", newcounter);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static string findRollNoBySRID(int srid, string examcode, string moe)
        {
            string rollno = "";
            if (examcode == "B11")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP from DDEStudentRecord where SRID='" + srid + "'", con);


                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    if (moe == "R")
                    {
                        if (dr["RollNoIYear"].ToString() != "")
                        {
                            rollno = dr["RollNoIYear"].ToString();
                        }

                        else if (dr["RollNoIIYear"].ToString() != "")
                        {
                            rollno = dr["RollNoIIYear"].ToString();
                        }
                        else if (dr["RollNoIIIYear"].ToString() != "")
                        {
                            rollno = dr["RollNoIIIYear"].ToString();
                        }
                    }

                    else if (moe == "B")
                    {
                        rollno = dr["RollNoBP"].ToString();
                    }


                }
                con.Close();

            }
            else if (examcode == "A12")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select RollNo_" + examcode + " from DDEExamRecord where SRID='" + srid + "' ", con);


                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    if (moe == "R")
                    {
                        rollno = dr[0].ToString();
                    }

                    else if (moe == "B")
                    {
                        rollno = "X" + dr[0].ToString();
                    }

                }
                con.Close();

            }
            else if (examcode == "A13")
            {
                if (moe == "R")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select RollNo from ExamRecord_June13 where SRID='" + srid + "'", con);
                    con.Open();
                    SqlDataReader dr;


                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        rollno = dr[0].ToString();

                    }

                    con.Close();

                }

                else if (moe == "B")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_A13 where SRID='" + srid + "'", con);
                    con.Open();
                    SqlDataReader dr;


                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        rollno = dr[0].ToString();

                    }

                    con.Close();
                }

            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_" + examcode + " where SRID='" + srid + "' and MOE='" + moe + "'", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    rollno = dr[0].ToString();

                }

                con.Close();

            }


            return rollno;

        }

        public static string findRollNoBySRID1(int srid, int year, string examcode, string moe)
        {
            string rollno = "";

            if (examcode == "A15" || examcode == "B15" || examcode == "A16" || examcode == "B16" || examcode == "A17" || examcode == "B17" || examcode == "A18" || examcode == "B18" || examcode == "A19" || examcode == "W10" || examcode == "Z10" || examcode == "W11" || examcode == "Z11" || examcode == "W12" || examcode == "H10" || examcode == "G10" || examcode == "H11")
            {
                if (moe == "R")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_" + examcode + " where SRID='" + srid + "' and Year='" + year + "' and MOE='R'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rollno = (ds.Tables[0].Rows[0]["RollNo"]).ToString();
                    }
                }
                else if (moe == "B")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_" + examcode + " where SRID='" + srid + "' and MOE='B'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rollno = (ds.Tables[0].Rows[0]["RollNo"]).ToString();
                    }
                }
            }

            return rollno;

        }

        public static bool alreadyExist(string srid, string table)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from " + table + " where SRID='" + srid + "' and MOE='R'", con);
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

        public static bool alreadyExistInTable(string srid, string table)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from " + table + " where SRID='" + srid + "'", con);
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

        public static bool isPCPStudent(int srid, DropDownList ddlist)
        {
            bool pcp = false;
            int totalexam = ddlist.Items.Count;

            for (int i = 1; i < totalexam - 1; i++)
            {
                if (isPCP(srid, ddlist.Items[i].Value))
                {
                    pcp = true;
                    break;
                }

            }

            return pcp;


        }

        private static bool isPCP(int srid, string examcode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEExamRecord_" + examcode + " where SRID='" + srid + "' and QualifyingStatus='PCP'", con);
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

        public static int findSRIDByENo(string eno)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select SRID from DDEStudentRecord where EnrollmentNo='" + eno + "' and RecordStatus='True'", con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                int srid = Convert.ToInt32(dr[0]);
                con.Close();
                return srid;
            }

            catch
            {

                return 0;

            }

        }

        public static int findPSRIDByENo(string eno)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select PSRID from DDEStudentRecord where EnrollmentNo='" + eno + "' and RecordStatus='True'", con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                int srid = Convert.ToInt32(dr[0]);
                con.Close();
                return srid;
            }

            catch
            {

                return 0;

            }

        }
        public static object findBPSubjectCodesBySRID(int srid, string examcode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord where SRID='" + srid + "' ", con);
            string subjects = "";
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subjects = "1Y : " + findSubjects(dr["BPSubjects1_" + examcode].ToString()) + "</br>2Y : " + findSubjects(dr["BPSubjects2_" + examcode].ToString()) + "</br>3Y : " + findSubjects(dr["BPSubjects3_" + examcode].ToString());
            }
            con.Close();

            return subjects;
        }

        private static string findSubjects(string sub)
        {
            string subjects = "";
            if (sub != "")
            {

                string[] sublist = sub.Split(',');

                for (int i = 0; i < sublist.Length; i++)
                {
                    if (subjects == "")
                    {
                        subjects = findSubjectSNo(Convert.ToInt32(sublist[i]));
                    }

                    else
                    {
                        subjects = subjects + "," + findSubjectSNo(Convert.ToInt32(sublist[i]));
                    }
                }
            }


            return subjects;
        }

        private static string findSubjectSNo(int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectSNo from DDESubject where SubjectID='" + subid + "' ", con);
            string sno = "";
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sno = dr[0].ToString();
            }
            con.Close();

            return sno;
        }

        public static void changeCourseBySRID(int srid, string course)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudentRecord set Course=@Course where SRID='" + srid + "' ", con);
            cmd.Parameters.AddWithValue("@Course", course);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public static string findExamCentreBySRID(int srid, string examcode)
        {
            string ecity;
            string ezone;

            findExamCityZoneBySRID(srid, examcode, out ecity, out ezone);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCentreAddress from DDEExaminationCentres where ExamCentreCity='" + ecity + "' and ExamCentreZone='" + ezone + "' ", con);
            string ecAddress = "";
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ecAddress = dr[0].ToString();
            }
            con.Close();

            return ecAddress;
        }



        private static void findExamCityZoneBySRID(int srid, string examcode, out string ecity, out string ezone)
        {
            ecity = "NONE";
            ezone = "A";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCentreCity_" + examcode + ",ExamCentreJone_" + examcode + " from DDEExamRecord where SRID='" + srid + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ecity = dr[0].ToString();
                ezone = dr[1].ToString();
            }
            con.Close();

        }

        public static string findAddressBySRID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CAddress from DDEStudentRecord where SRID='" + srid + "' ", con);
            string address = "";
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                address = dr[0].ToString();
            }
            con.Close();

            return address;

        }

        public static bool examFeeSubmittedBySRID(int srid, string year, string examcode, string cardtype)
        {
            bool exist = false;
            if (examcode == "A10" || examcode == "B10" || examcode == "A11" || examcode == "B11")
            {
                exist = true;
            }
            else if (examcode == "A12")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                if (cardtype == "R")
                {
                    cmd.CommandText = "select * from DDEFeeRecord where SRID='" + srid + "' and ExamFee_" + examcode + "='1000'";
                }

                else if (cardtype == "B")
                {
                    cmd.CommandText = "select * from DDEFeeRecord where SRID='" + srid + "' and BPExamFee_" + examcode + "!='' and BPExamFee_" + examcode + "!='NULL'";
                }


                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    exist = true;
                }

                con.Close();

            }
            else if (examcode == "B12")
            {
                string query = "";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (cardtype == "R")
                        {
                            if (query == "")
                            {
                                query = "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and ForYear='" + year + "' and FeeHead='2' and ForExam='B12'";
                            }
                            else
                            {
                                query = query + " union " + "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and ForYear='" + year + "' and FeeHead='2' and ForExam='B12'";
                            }
                        }
                        else if (cardtype == "B")
                        {
                            if (query == "")
                            {
                                query = "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='3' and ForExam='B12'";
                            }
                            else
                            {
                                query = query + " union " + "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='3' and ForExam='B12'";
                            }

                        }
                    }

                }
                con.Close();

                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader dr1;

                cmd1.CommandText = query;

                cmd1.Connection = con1;
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    exist = true;
                }

                con1.Close();

            }
            else if (examcode == "A13")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                if (cardtype == "R")
                {
                    cmd.CommandText = "select * from ExamRecord_June13 where SRID='" + srid + "'";
                }

                else if (cardtype == "B")
                {
                    cmd.CommandText = "select * from DDEExamRecord_A13 where SRID='" + srid + "'";
                }


                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (cardtype == "R")
                    {
                        if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True") || (dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True") || (dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
                        {
                            exist = true;
                        }

                    }

                    else if (cardtype == "B")
                    {
                        exist = true;
                    }

                }

                con.Close();

            }
            else
            {
                string query = "";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (cardtype == "R")
                        {
                            if (query == "")
                            {
                                query = "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and ForYear='" + year + "' and FeeHead='2' and ForExam='" + examcode + "'";
                            }
                            else
                            {
                                query = query + " union " + "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and ForYear='" + year + "' and FeeHead='2' and ForExam='" + examcode + "'";
                            }
                        }
                        else if (cardtype == "B")
                        {
                            if (query == "")
                            {
                                query = "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + examcode + "'";
                            }
                            else
                            {
                                query = query + " union " + "select SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + examcode + "'";
                            }

                        }
                    }

                }
                con.Close();

                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader dr1;

                cmd1.CommandText = query;

                cmd1.Connection = con1;
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    exist = true;
                }

                con1.Close();

            }


            return exist;
        }

        public static bool StudyCentreExist(string sccode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEStudyCentres where SCCode='" + sccode + "'", con);
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

        public static string findSCCodeByID(int scid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCCode from DDEStudyCentres where SCID='" + scid + "' ", con);
            string sccode = "";
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sccode = dr[0].ToString();
            }
            con.Close();

            return sccode;

        }

        public static bool ExamCentreExist(string examcode, string eccode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEExaminationCentres_" + examcode + " where ExamCentreCode='" + eccode + "'", con);
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

        public static string findECCodeByID(int ecid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCCode from DDEStudyCentres where SCID='" + ecid + "' ", con);
            string eccode = "";
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                eccode = dr[0].ToString();
            }
            con.Close();

            return eccode;
        }

        public static string[] findSubjectInfoByID(int subid)
        {
            string[] subinfo = { "", "", "", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectCode,SubjectName,SubjectSNo,PaperCode from DDESubject where SubjectID='" + subid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subinfo[0] = dr[0].ToString();
                subinfo[1] = dr[1].ToString();
                subinfo[2] = dr[2].ToString();
                subinfo[3] = dr[3].ToString();
            }
            con.Close();

            return subinfo;

        }


        public static string[] findSubjectInfoByID2(int subid)
        {
            string[] subinfo = { "", "", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PaperCode,SubjectName,Year from DDESubject where SubjectID='" + subid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subinfo[0] = dr[0].ToString();
                subinfo[1] = dr[1].ToString();
                if (dr[2].ToString() == "1st Year")
                {
                    subinfo[2] = "1";
                }
                else if (dr[2].ToString() == "2nd Year")
                {
                    subinfo[2] = "2";
                }
                else if (dr[2].ToString() == "3rd Year")
                {
                    subinfo[2] = "3";
                }
            }
            con.Close();

            return subinfo;

        }

        public static string[] findPracticalInfoByID2(int pid)
        {
            string[] subinfo = { "", "", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalCode,PracticalName,Year from DDEPractical where PracticalID='" + pid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subinfo[0] = dr[0].ToString();
                subinfo[1] = dr[1].ToString();
                if (dr[2].ToString() == "1st Year")
                {
                    subinfo[2] = "1";
                }
                else if (dr[2].ToString() == "2nd Year")
                {
                    subinfo[2] = "2";
                }
                else if (dr[2].ToString() == "3rd Year")
                {
                    subinfo[2] = "3";
                }
            }
            con.Close();

            return subinfo;

        }
        public static bool filterByECentre(int srid, string ecode, string ecentre, string zone)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select ExamCentreCity_" + ecode + ",ExamCentreJone_" + ecode + " from DDEExamRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            bool exist = false;

            if (ecentre != "ALL" && zone != "ALL")
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr[0].ToString() == ecentre && dr[1].ToString() == zone)
                    {
                        exist = true;
                    }
                }

                con.Close();
            }

            else if (ecentre == "ALL" && zone != "ALL")
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr[1].ToString() == zone)
                    {
                        exist = true;
                    }
                }

                con.Close();
            }

            else if (ecentre != "ALL" && zone == "ALL")
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr[0].ToString() == ecentre)
                    {
                        exist = true;
                    }
                }

                con.Close();
            }

            else if (ecentre == "ALL" && zone == "ALL")
            {
                exist = true;
            }



            return exist;

        }

        public static string findAlphaYear(string year)
        {
            if (year == "1")
            {
                return "1st Year";
            }

            else if (year == "2")
            {
                return "2nd Year";
            }
            else if (year == "3")
            {
                return "3rd Year";
            }
            else if (year == "4")
            {
                return "4th Year";
            }
            else return "";
        }

        public static string findAlphaYear1(string year)
        {
            if (year == "1")
            {
                return "First Year";
            }

            else if (year == "2")
            {
                return "Second Year";
            }
            else if (year == "3")
            {
                return "Third Year";
            }

            else return "";
        }

        public static int findProgrammeCode(string cid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ProgramCode from DDECourse where CourseID='" + cid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            int pcode = Convert.ToInt32(dr[0]);
            con.Close();

            return pcode;

        }

        public static int findCounter(string batch)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select EnrollmentCounter from DDESession where Session='" + batch + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            int counter = Convert.ToInt32(dr[0]);
            con.Close();

            return counter + 1;
        }

        public static void updateEnrollmentCounter(int batchid, int counter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (batchid>=23)
            {
               int examid = FindInfo.findExamIDByExamCode(findApplicableExamByBatchID(batchid));
               cmd.CommandText= "update DDEExaminations set EnrollmentCounter=@EnrollmentCounter where ExamID='" + examid + "' ";
            }
            else
            {
                cmd.CommandText = "update DDESession set EnrollmentCounter=@EnrollmentCounter where SessionID='" + batchid + "' ";
            }
           
            cmd.Parameters.AddWithValue("EnrollmentCounter", counter);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static bool validExamForAugust2011(string exam, string year, string course)
        {


            bool valid = false;


            if (exam == "August 2011" && year == "1st Year")
            {
                if (course == "BBA" || course == "BCA" || course == "BCOM" || course == "BSC_PCM" || course == "BSC_ZBC" || course == "MA_HINDI" || course == "MA_ENGLISH" || course == "MA_HISTORY" || course == "MA_POLITICALSCIENCE" || course == "MA_SOCIOLOGY" || course == "MBA" || course == "MCA" || course == "MLIB" || course == "PGDCA" || course == "PGDFQSM" || course == "PGDGM")
                {
                    valid = true;
                }

                else
                {
                    valid = false;
                }
            }

            else
            {
                valid = false;
            }



            return valid;

        }

        public static string findTodayDate()
        {
            return DateTime.Now.ToString("yyyy-MMMM-dd");
        }

        public static string findCourseShortNameByID(int courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseShortName from DDECourse where CourseID ='" + courseid + "' ", con);
            SqlDataReader dr;

            string course = "";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                course = dr[0].ToString();
            }
            con.Close();

            return course;
        }

        public static string findFeeHeadNameByID(int fhid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select FeeHead from DDEFeeHead where FHID ='" + fhid + "' ", con);
            SqlDataReader dr;

            string fhname = "";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                fhname = dr[0].ToString();
            }
            con.Close();

            return fhname;
        }



        public static string[] findStudentDetailBySRID(int srid)
        {
            string[] stinfo = { "", "", "", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select EnrollmentNo,StudentName,FatherName,StudyCentreCode from DDEStudentRecord where SRID='" + srid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                stinfo[0] = dr[0].ToString();
                stinfo[1] = dr[1].ToString();
                stinfo[2] = dr[2].ToString();
                stinfo[3] = dr[3].ToString();
            }
            con.Close();

            return stinfo;
        }



        public static int findCourseDuration(int cid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseDuration from DDECourse where CourseID ='" + cid + "' ", con);
            SqlDataReader dr;


            int dur = 0;
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() != "")
                {
                    dur = Convert.ToInt32(dr[0]);
                }
            }
            con.Close();

            return dur;
        }



        public static bool resultlistExist(int cid, int year, string exam, string moe)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEMakeResultOnline where CourseID='" + cid + "' and Year='" + year + "' and Examination='" + exam + "' and MOE='" + moe + "'", con);
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

        public static int findTotalPracMarksByID(int pid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select PracticalMaxMarks from DDEPractical where PracticalID ='" + pid + "' ", con);
            SqlDataReader dr;


            int tm = 0;
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                tm = Convert.ToInt32(dr[0]);

            }
            con.Close();

            return tm;
        }

        internal static int findExamCounterByCode(string exam)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCounter from DDEExaminations where ExamCode='" + exam + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();


            dr.Read();

            int counter = Convert.ToInt32(dr[0]);

            con.Close();

            return counter;
        }



        public static bool alreadyDetaind(int srid, string exam, string moe, out string dstatus)
        {
            dstatus = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEDetainedStudents where SRID='" + srid + "' and Examination='" + exam + "' and MOE='" + moe + "'", con);
            SqlDataReader dr;

            bool exist = false;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exist = true;
                dstatus = dr["DetainedStatus"].ToString();
            }

            con.Close();

            return exist;
        }


        public static bool rollnoAlltted(int srid, string examcode, string moe, out string rollno)
        {
            rollno = "";
            bool exist = false;
            if (examcode == "A10" || examcode == "B10" || examcode == "A11" || examcode == "B12")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_" + examcode + " where SRID='" + srid + "' and MOE='" + moe + "'", con);


                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    exist = true;
                    rollno = dr[0].ToString();

                }
                con.Close();
            }
            else if (examcode == "B11")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select RollNoIYear,RollNoIIYear,RollNoIIIYear,RollNoBP from DDEStudentRecord where SRID='" + srid + "'", con);


                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    exist = true;
                    if (moe == "R")
                    {
                        if (dr["RollNoIYear"].ToString() != "")
                        {
                            rollno = dr["RollNoIYear"].ToString();
                        }

                        else if (dr["RollNoIIYear"].ToString() != "")
                        {
                            rollno = dr["RollNoIIYear"].ToString();
                        }
                        else if (dr["RollNoIIIYear"].ToString() != "")
                        {
                            rollno = dr["RollNoIIIYear"].ToString();
                        }
                    }

                    else if (moe == "B")
                    {
                        rollno = dr["RollNoBP"].ToString();
                    }


                }
                con.Close();

            }
            else if (examcode == "A12")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select RollNo_" + examcode + " from DDEExamRecord where SRID='" + srid + "' ", con);


                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    exist = true;
                    if (moe == "R")
                    {
                        rollno = dr[0].ToString();
                    }

                    else if (moe == "B")
                    {
                        rollno = "X" + dr[0].ToString();
                    }

                }
                con.Close();

            }
            else if (examcode == "A13")
            {
                if (moe == "R")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select RollNo from ExamRecord_June13 where SRID='" + srid + "'", con);
                    con.Open();
                    SqlDataReader dr;


                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        exist = true;
                        rollno = dr[0].ToString();

                    }

                    con.Close();


                }

                else if (moe == "B")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_A13 where SRID='" + srid + "'", con);
                    con.Open();
                    SqlDataReader dr;


                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        exist = true;
                        rollno = dr[0].ToString();

                    }

                    con.Close();
                }

            }
            else if (examcode == "B13")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_B13 where SRID='" + srid + "' and MOE='" + moe + "'", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    exist = true;
                    rollno = dr[0].ToString();

                }

                con.Close();


            }


            return exist;
        }




        public static bool validENo(string eno)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;


            cmd.CommandText = "select SRID from DDEStudentRecord where EnrollmentNo='" + eno + "'";


            bool exist = false;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                exist = true;

            }

            con.Close();

            return exist;
        }

        public static string findAdmissionThrough(int atype)
        {
            string at = "NA";

            if (atype == 1)
            {
                at = "DIRECT";
            }
            else if (atype == 2)
            {
                at = "WEM";
            }
            return at;
        }
        public static string findAdmissionType(int atype)
        {
            string at = "NA";

            if (atype == 1)
            {
                at = "REGULAR";
            }
            else if (atype == 2)
            {
                at = "CT";
            }
            return at;
        }

        public static int findTotalAmountBySessionID(int sid, int fhid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select Amount from [DDEOLFeeRecord_2012-13] where SessionID='" + sid + "' and FeeHead='" + fhid + "' ", con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int amount = 0;
            while (dr.Read())
            {
                amount = amount + Convert.ToInt32(dr["Amount"]);
            }

            return amount;

        }

        public static object findSCNameByID(int p)
        {
            throw new NotImplementedException();
        }

        public static string findSCNameByCode(string code)
        {
            string scname = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Location,City from DDEStudyCentres where SCCode='" + code + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                scname = dr["Location"].ToString() + "," + dr["City"].ToString();

            }

            con.Close();

            return scname;
        }

        public static string findCurrentSCCodeBySRID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID ='" + srid + "' ", con);
            SqlDataReader dr;


            string sccode = "NA";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
                {
                    sccode = Convert.ToString(dr["StudyCentreCode"]);
                }
                else if (dr["SCStatus"].ToString() == "T")
                {
                    sccode = findCurrentSCCode(srid);
                }
            }
            con.Close();

            return sccode;
        }


        private static string findCurrentSCCode(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PreviousSC,CurrentSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sccode = dr["CurrentSC"].ToString() + " (" + dr["PreviousSC"].ToString() + ")";
                }

            }

            con.Close();

            return sccode;
        }


        public static string findPreviousSCCodeBySRID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID ='" + srid + "' ", con);
            SqlDataReader dr;


            string sccode = "NA";
            con.Open();
            dr = scmd.ExecuteReader();
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

        public static DateTime findDOAByBatch(int batchid)
        {
            Random gen = new Random();
            DateTime da = new DateTime();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select StartDOA,EndDOA from DDESession where SessionID='" + batchid+ "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                string[] sd = Convert.ToDateTime(dr[0]).ToString("dd-MM-yyyy").Split('-');
                string[] ed = Convert.ToDateTime(dr[1]).ToString("dd-MM-yyyy").Split('-');
                DateTime start = new DateTime(Convert.ToInt32(sd[2]), Convert.ToInt32(sd[1]), Convert.ToInt32(sd[0]));
                DateTime end = new DateTime(Convert.ToInt32(ed[2]), Convert.ToInt32(ed[1]), Convert.ToInt32(ed[0]));
                int range = (end - start).Days;
                da = start.AddDays(gen.Next(range));

                if (da.DayOfWeek == DayOfWeek.Sunday)
                {
                    da = da.AddDays(-1);
                }
            }

            return da;
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
                while (dr.Read())
                {
                    sccode = dr[0].ToString();
                }

            }

            con.Close();

            return sccode;
        }

        public static string findSCCodeBySRID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select StudyCentreCode from DDEStudentRecord where SRID ='" + srid + "' ", con);
            SqlDataReader dr;


            string sccode = "NA";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                sccode = Convert.ToString(dr["StudyCentreCode"]);

            }
            con.Close();

            return sccode;
        }
        public static void findExamCentreDetailByECID_A13(int ecid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres1 where ECID='" + ecid + "'", con);
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    drow["Exam City"] = Convert.ToString(dr["City"]);
                    drow["Exam Centre"] = Convert.ToString(dr["CentreName"]);
                    drow["Exam Code"] = Convert.ToString(dr["ExamCentreCode"]);

                }

            }

            con.Close();
        }



        private static int findECIDBySRID(int srid, string examcode, string moe)
        {
            int ecid = 0;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (moe == "R" && examcode == "A13")
            {
                cmd.CommandText = "Select ECID from ExamRecord_June13 where SRID='" + srid + "'";
            }
            else
            {
                cmd.CommandText = "Select ExamCentreCode from DDEExamRecord_" + examcode + " where SRID='" + srid + "' and MOE='" + moe + "'";
            }
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ecid = Convert.ToInt32(dr[0]);


                }

            }

            con.Close();

            return ecid;
        }

        public static string findSubjectNameByID(int subid)
        {
            string sub = "NOT FOUND";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectCode from DDESubject where SubjectID='" + subid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sub = dr[0].ToString();
            }
            con.Close();

            return sub;
        }
        public static string findProperSubjectNameByID(int subid)
        {
            string sub = "NOT FOUND";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectName from DDESubject where SubjectID='" + subid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sub = dr[0].ToString();
            }
            con.Close();

            return sub;
        }

        public static string findBatchBySRID(int srid)
        {
            string batch = "NOT FOUND";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Session from DDEStudentRecord where SRID='" + srid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                batch = dr[0].ToString();
            }
            con.Close();

            return batch;
        }

        public static string findENoByECID(int ecid)
        {
            string eno = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from ExamRecord_June13 where ECID='" + ecid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                eno = findENoByID(Convert.ToInt32(dr[0]));
            }
            con.Close();

            return eno;
        }

        public static string findENoByERID(int erid)
        {
            string eno = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from ExamRecord_June13 where ExamRecordID='" + erid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                eno = findENoByID(Convert.ToInt32(dr[0]));
            }
            con.Close();

            return eno;
        }

        public static string findSubjectCodeByID(int subid)
        {
            string sc = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectCode from DDESubject where SubjectID='" + subid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sc = dr[0].ToString();
            }
            con.Close();

            return sc;
        }

        public static int findENoCounterByExamCode(string examcode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select EnrollmentCounter from DDEExaminations where ExamCode='" + examcode + "' or SemesterExamCode='"+examcode+"' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            int counter = Convert.ToInt32(dr[0]);
            con.Close();

            return counter + 1;
        }

        public static int findSubjectYearByID(int subid)
        {
            string subyear = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Year from DDESubject where SubjectID='" + subid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subyear = dr[0].ToString();
            }
            con.Close();


            if (subyear == "1st Year")
            {
                return 1;
            }
            else if (subyear == "2nd Year")
            {
                return 2;
            }
            else if (subyear == "3rd Year")
            {
                return 3;
            }
            else return 0;


        }

        public static string findCourseNameBySRID(int srid, int year)
        {
            string course = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();

                int cid = Convert.ToInt32(dr[0]);
                if (cid == 76)
                {
                    if (year == 1)
                    {
                        if (dr[0].ToString() != "")
                        {
                            course = findCourseNameByID(Convert.ToInt32(dr[0]));
                        }
                    }
                    else if (year == 2)
                    {
                        if (dr[1].ToString() != "")
                        {
                            course = findCourseNameByID(Convert.ToInt32(dr[1]));
                        }
                    }
                    else if (year == 3)
                    {
                        if (dr[2].ToString() != "")
                        {
                            course = findCourseNameByID(Convert.ToInt32(dr[2]));
                        }
                    }

                }
                else
                {
                    if (dr[0].ToString() != "")
                    {
                        course = findCourseNameByID(Convert.ToInt32(dr[0]));
                    }
                }
            }

            con.Close();

            return course;
        }
        public static string findCourseFullNameBySRID(int srid, int year)
        {
            string course = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();

                int cid = Convert.ToInt32(dr[0]);
                if (cid == 76)
                {
                    if (year == 1)
                    {
                        course = findCourseFullNameByID(Convert.ToInt32(dr[0]));
                    }
                    else if (year == 2)
                    {
                        if (dr[1].ToString() != "")
                        {
                            course = findCourseFullNameByID(Convert.ToInt32(dr[1]));
                        }
                    }
                    else if (year == 3)
                    {
                        if (dr[2].ToString() != "")
                        {
                            course = findCourseFullNameByID(Convert.ToInt32(dr[2]));
                        }
                    }

                }
                else
                {
                    course = findCourseFullNameByID(Convert.ToInt32(dr[0]));
                }
            }

            con.Close();

            return course;
        }



        public static string findSubjectNameByPaperCode(string pcode, string sysession)
        {
            string sname = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectName from DDESubject where PaperCode='" + pcode + "' and SyllabusSession='" + sysession + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                sname = dr["SubjectName"].ToString();

            }

            con.Close();

            return sname;
        }

      

        public static string findAllSubjectNameByPaperCode(string pcode)
        {
            string sname = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct SubjectName from DDESubject where PaperCode='" + pcode + "' and SyllabusSession!='A 2009-10'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (sname == "NF")
                    {
                        sname = dr["SubjectName"].ToString().Trim();

                    }
                    else
                    {
                        //string[] str = sname.Split(',');

                        //int pos = Array.IndexOf(str, dr["SubjectName"].ToString().Trim());
                        //if(!(pos>-1))
                        //{
                        sname = sname + ",<br/>" + dr["SubjectName"].ToString().Trim();
                        //}
                    }
                }

            }

            con.Close();

            return sname;
        }

        public static string findPaperCodeByID(int subid)
        {
            string pc = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PaperCode from DDESubject where SubjectID='" + subid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                pc = dr["PaperCode"].ToString();

            }

            con.Close();

            return pc;
        }

        public static bool marksheetPrinted(int srid, string moe, out int time)
        {
            bool printed = false;
            time = 0;
            if (moe == "B")
            {
                printed = false;
            }
            else if (moe == "R")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select MSPrinted,Times from ExamRecord_June13 where SRID='" + srid + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["MSPrinted"].ToString() == "True")
                    {
                        printed = true;
                        time = Convert.ToInt32(dr["Times"]);
                    }

                }

                con.Close();
            }

            return printed;
        }

        public static string findAllExamYear(int srid, string exam, string moe)
        {
            string year = "0";
           
            if (moe == "R")
            {
                if (exam == "A12")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlDataReader dr;
                    SqlCommand cmd = new SqlCommand("Select * from DDEFeeRecord where SRID='" + srid + "' and ExamFee_A12='1000'", con);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        year = "";
                    }
                    con.Close();
                }
                else if (exam == "B12")
                {

                    string ey = "";

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.SelectCommand.Connection = con1;
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("Select * from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='2' and ForExam='B12'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.SelectCommand.Connection = con;
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {


                                    if (ey != "")
                                    {
                                        string[] saey = ey.Split(',');
                                        int pos = Array.IndexOf(saey, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                        if (!(pos > -1))
                                        {
                                            if (ey == "")
                                            {
                                                ey = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                            }
                                            else
                                            {
                                                ey = ey + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                            }

                                        }
                                    }
                                    else
                                    {
                                        ey = ds.Tables[0].Rows[i]["ForYear"].ToString();

                                    }
                                }
                            }
                        }
                    }
                    year = ey;
                }

                else if (exam == "A13")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlDataReader dr;
                    SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "' and Online='True'", con);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
                        {
                            year = "1";

                            if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
                            {
                                year = "1,2";
                            }
                            else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
                            {
                                year = "1,3";
                            }


                        }
                        else if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
                        {
                            year = "2";

                            if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
                            {
                                year = "1,2";
                            }
                            else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
                            {
                                year = "2,3";
                            }


                        }
                        else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
                        {
                            year = "3";

                            if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
                            {
                                year = "1,3";
                            }
                            else if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
                            {
                                year = "2,3";
                            }
                        }
                    }
                    con.Close();
                }
                else if (exam == "B13")
                {
                    string cy = "";
                    string ey = "";

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.SelectCommand.Connection = con1;
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("Select * from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and (FeeHead='1' or FeeHead='2')", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.SelectCommand.Connection = con;
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 1)
                                    {

                                        if (cy != "")
                                        {
                                            string[] sacy = cy.Split(',');
                                            int pos = Array.IndexOf(sacy, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (cy == "")
                                                {
                                                    cy = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    cy = cy + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                            }
                                        }
                                        else
                                        {

                                            cy = ds.Tables[0].Rows[i]["ForYear"].ToString(); ;

                                        }
                                    }
                                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 2 && ds.Tables[0].Rows[i]["ForExam"].ToString() == exam)
                                    {

                                        if (ey != "")
                                        {
                                            string[] saey = ey.Split(',');
                                            int pos = Array.IndexOf(saey, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (ey == "")
                                                {
                                                    ey = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    ey = ey + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }

                                            }
                                        }
                                        else
                                        {
                                            ey = ds.Tables[0].Rows[i]["ForYear"].ToString();

                                        }
                                    }




                                }


                            }


                        }
                    }

                    if (cy == ey)
                    {
                        year = ey;
                    }
                    else
                    {
                        if (cy == "1")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1";
                            }

                        }
                        else if (cy == "2")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2";
                            }

                        }
                        else if (cy == "3")
                        {
                            if (ey == "1,3" || ey == "3,1")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "3";
                            }

                        }

                        else if ((cy == "1,2" || cy == "2,1"))
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2";
                            }
                        }
                        else if (cy == "2,3" || cy == "3,2")
                        {
                            if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2,3";
                            }
                        }
                        else if (cy == "1,3" || cy == "3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,3";
                            }

                        }
                        else if (cy == "1,2,3" || cy == "1,3,2" || cy == "3,2,1" || cy == "3,1,2" || cy == "2,1,3" || cy == "2,3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2,3";
                            }
                        }

                    }
                }
                else if (exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
                {
                    string cy = "";
                    string ey = "";

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.SelectCommand.Connection = con1;
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("Select * from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and (FeeHead='1' or FeeHead='2') and ForExam='" + exam + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.SelectCommand.Connection = con;
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 1)
                                    {

                                        if (cy != "")
                                        {
                                            string[] sacy = cy.Split(',');
                                            int pos = Array.IndexOf(sacy, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (cy == "")
                                                {
                                                    cy = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    cy = cy + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                            }
                                        }
                                        else
                                        {

                                            cy = ds.Tables[0].Rows[i]["ForYear"].ToString(); ;

                                        }
                                    }
                                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 2)
                                    {

                                        if (ey != "")
                                        {
                                            string[] saey = ey.Split(',');
                                            int pos = Array.IndexOf(saey, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (ey == "")
                                                {
                                                    ey = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    ey = ey + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }

                                            }
                                        }
                                        else
                                        {
                                            ey = ds.Tables[0].Rows[i]["ForYear"].ToString();

                                        }
                                    }




                                }


                            }


                        }
                    }

                    if (cy == ey)
                    {
                        year = ey;
                    }
                    else
                    {
                        if (cy == "1")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1";
                            }

                        }
                        else if (cy == "2")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2";
                            }

                        }
                        else if (cy == "3")
                        {
                            if (ey == "1,3" || ey == "3,1")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "3";
                            }

                        }

                        else if ((cy == "1,2" || cy == "2,1"))
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2";
                            }
                        }
                        else if (cy == "2,3" || cy == "3,2")
                        {
                            if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2,3";
                            }
                        }
                        else if (cy == "1,3" || cy == "3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,3";
                            }

                        }
                        else if (cy == "1,2,3" || cy == "1,3,2" || cy == "3,2,1" || cy == "3,1,2" || cy == "2,1,3" || cy == "2,3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2,3";
                            }
                        }

                    }
                }


            }

            else if (moe == "B")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_" + exam + " where SRID='" + srid + "' and MOE='B'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if ((dr["BPSubjects1"].ToString() != "") || (dr["BPPracticals1"].ToString() != ""))
                    {

                        if ((dr["BPSubjects2"].ToString() != "") || (dr["BPPracticals2"].ToString() != ""))
                        {
                            if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                            {
                                year = "1,2,3";
                            }
                            else
                            {
                                year = "1,2";
                            }
                        }
                        else if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                        {
                            year = "1,3";
                        }
                        else
                        {
                            year = "1";
                        }
                    }
                    else if ((dr["BPSubjects2"].ToString() != "") || (dr["BPPracticals2"].ToString() != ""))
                    {


                        if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                        {
                            year = "2,3";
                        }
                        else
                        {
                            year = "2";
                        }
                    }
                    else if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                    {
                        year = "3";

                    }
                }
                con.Close();
            }

            return year;
        }

        public static string findAllExamYear1(int srid, string exam, string moe, out string cy, out string ey)
        {
            string year = "0";
            cy = "";
            ey = "";
            if (moe == "R")
            {
                if (exam == "A12")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlDataReader dr;
                    SqlCommand cmd = new SqlCommand("Select * from DDEFeeRecord where SRID='" + srid + "' and ExamFee_A12='1000'", con);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        year = "";
                    }
                    con.Close();
                }
                else if (exam == "B12")
                {

                  

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.SelectCommand.Connection = con1;
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("Select * from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='2' and ForExam='B12'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.SelectCommand.Connection = con;
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {


                                    if (ey != "")
                                    {
                                        string[] saey = ey.Split(',');
                                        int pos = Array.IndexOf(saey, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                        if (!(pos > -1))
                                        {
                                            if (ey == "")
                                            {
                                                ey = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                            }
                                            else
                                            {
                                                ey = ey + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                            }

                                        }
                                    }
                                    else
                                    {
                                        ey = ds.Tables[0].Rows[i]["ForYear"].ToString();

                                    }
                                }
                            }
                        }
                    }
                    year = ey;
                }

                else if (exam == "A13")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlDataReader dr;
                    SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "' and Online='True'", con);
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();

                        if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
                        {
                            year = "1";

                            if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
                            {
                                year = "1,2";
                            }
                            else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
                            {
                                year = "1,3";
                            }


                        }
                        else if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
                        {
                            year = "2";

                            if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
                            {
                                year = "1,2";
                            }
                            else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
                            {
                                year = "2,3";
                            }


                        }
                        else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
                        {
                            year = "3";

                            if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
                            {
                                year = "1,3";
                            }
                            else if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
                            {
                                year = "2,3";
                            }
                        }
                    }
                    con.Close();
                }
                else if (exam == "B13")
                {
                    

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.SelectCommand.Connection = con1;
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("Select * from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and (FeeHead='1' or FeeHead='2')", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.SelectCommand.Connection = con;
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 1)
                                    {

                                        if (cy != "")
                                        {
                                            string[] sacy = cy.Split(',');
                                            int pos = Array.IndexOf(sacy, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (cy == "")
                                                {
                                                    cy = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    cy = cy + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                            }
                                        }
                                        else
                                        {

                                            cy = ds.Tables[0].Rows[i]["ForYear"].ToString(); ;

                                        }
                                    }
                                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 2 && ds.Tables[0].Rows[i]["ForExam"].ToString() == exam)
                                    {

                                        if (ey != "")
                                        {
                                            string[] saey = ey.Split(',');
                                            int pos = Array.IndexOf(saey, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (ey == "")
                                                {
                                                    ey = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    ey = ey + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }

                                            }
                                        }
                                        else
                                        {
                                            ey = ds.Tables[0].Rows[i]["ForYear"].ToString();

                                        }
                                    }




                                }


                            }


                        }
                    }

                    if (cy == ey)
                    {
                        year = ey;
                    }
                    else
                    {
                        if (cy == "1")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1";
                            }

                        }
                        else if (cy == "2")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2";
                            }

                        }
                        else if (cy == "3")
                        {
                            if (ey == "1,3" || ey == "3,1")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "3";
                            }

                        }

                        else if ((cy == "1,2" || cy == "2,1"))
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2";
                            }
                        }
                        else if (cy == "2,3" || cy == "3,2")
                        {
                            if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2,3";
                            }
                        }
                        else if (cy == "1,3" || cy == "3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,3";
                            }

                        }
                        else if (cy == "1,2,3" || cy == "1,3,2" || cy == "3,2,1" || cy == "3,1,2" || cy == "2,1,3" || cy == "2,3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2,3";
                            }
                        }

                    }
                }
                else if (exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
                {
                  

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.SelectCommand.Connection = con1;
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("Select * from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and (FeeHead='1' or FeeHead='2') and ForExam='" + exam + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.SelectCommand.Connection = con;
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 1)
                                    {

                                        if (cy != "")
                                        {
                                            string[] sacy = cy.Split(',');
                                            int pos = Array.IndexOf(sacy, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (cy == "")
                                                {
                                                    cy = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    cy = cy + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                            }
                                        }
                                        else
                                        {

                                            cy = ds.Tables[0].Rows[i]["ForYear"].ToString(); ;

                                        }
                                    }
                                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["FeeHead"]) == 2)
                                    {

                                        if (ey != "")
                                        {
                                            string[] saey = ey.Split(',');
                                            int pos = Array.IndexOf(saey, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                            if (!(pos > -1))
                                            {
                                                if (ey == "")
                                                {
                                                    ey = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }
                                                else
                                                {
                                                    ey = ey + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                                }

                                            }
                                        }
                                        else
                                        {
                                            ey = ds.Tables[0].Rows[i]["ForYear"].ToString();

                                        }
                                    }




                                }


                            }


                        }
                    }

                    if (cy == ey)
                    {
                        year = ey;
                    }
                    else
                    {
                        if (cy == "1")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1";
                            }

                        }
                        else if (cy == "2")
                        {
                            if (ey == "1,2" || ey == "2,1")
                            {
                                year = "2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2";
                            }

                        }
                        else if (cy == "3")
                        {
                            if (ey == "1,3" || ey == "3,1")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "3";
                            }

                        }

                        else if ((cy == "1,2" || cy == "2,1"))
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2";
                            }
                        }
                        else if (cy == "2,3" || cy == "3,2")
                        {
                            if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "2,3";
                            }
                        }
                        else if (cy == "1,3" || cy == "3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,3";
                            }

                        }
                        else if (cy == "1,2,3" || cy == "1,3,2" || cy == "3,2,1" || cy == "3,1,2" || cy == "2,1,3" || cy == "2,3,1")
                        {
                            if (ey == "1")
                            {
                                year = "1";
                            }
                            else if (ey == "2")
                            {
                                year = "2";
                            }
                            else if (ey == "3")
                            {
                                year = "3";
                            }
                            else if (ey == "1,2" || ey == "2,1")
                            {
                                year = "1,2";
                            }
                            else if (ey == "2,3" || ey == "3,2")
                            {
                                year = "2,3";
                            }
                            else if (ey == "1,3" || ey == "3,1")
                            {
                                year = "1,3";
                            }
                            else if (ey == "1,2,3" || ey == "1,3,2" || ey == "3,2,1" || ey == "3,1,2" || ey == "2,1,3" || ey == "2,3,1")
                            {
                                year = "1,2,3";
                            }
                        }

                    }
                }


            }

            else if (moe == "B")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_" + exam + " where SRID='" + srid + "' and MOE='B'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if ((dr["BPSubjects1"].ToString() != "") || (dr["BPPracticals1"].ToString() != ""))
                    {

                        if ((dr["BPSubjects2"].ToString() != "") || (dr["BPPracticals2"].ToString() != ""))
                        {
                            if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                            {
                                year = "1,2,3";
                            }
                            else
                            {
                                year = "1,2";
                            }
                        }
                        else if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                        {
                            year = "1,3";
                        }
                        else
                        {
                            year = "1";
                        }
                    }
                    else if ((dr["BPSubjects2"].ToString() != "") || (dr["BPPracticals2"].ToString() != ""))
                    {


                        if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                        {
                            year = "2,3";
                        }
                        else
                        {
                            year = "2";
                        }
                    }
                    else if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                    {
                        year = "3";

                    }
                }
                con.Close();
            }

            return year;
        }

        public static int findCSID(int v1, string v2)
        {
            throw new NotImplementedException();
        }

        public static string findEmailIDBySCCode(string sccode)
        {
            string email = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Email from DDEStudyCentres where SCCode='" + sccode + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                email = dr["Email"].ToString();

            }

            con.Close();
            return email;
        }

        public static string findEmailIDBySCID(int scid)
        {
            string email = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Email from DDEStudyCentres where SCID='" + scid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                email = dr["Email"].ToString();

            }

            con.Close();
            return email;
        }
        public static string findEmailIDByECID(int ecid, string exam)
        {
            string email = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Email from DDEExaminationCentres_" + exam + " where ECID='" + ecid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                email = dr["Email"].ToString();

            }

            con.Close();
            return email;
        }

        public static int findSRIDByANo(string ano)
        {
            int srid = 0;
            if (ano != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where ApplicationNo='" + ano + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                    srid = Convert.ToInt32(dr["SRID"]);

                }

                con.Close();
            }
            return srid;
        }

        public static string findANoBySRID(int srid)
        {
            string ano = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ApplicationNo from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                ano = dr["ApplicationNo"].ToString();

            }

            con.Close();
            return ano;
        }

        public static string findMarkSheetStatus(int srid, string eno, int cid, int year, string exam)
        {
            string sess = "";
            if (exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
            {
                sess = FindInfo.findSySessionBySRID(srid);
            }
            else
            {
                if ((eno.Substring(0, 3) == "A13" || eno.Substring(0, 3) == "C14") && (cid == 12 || cid == 27))
                {
                    sess = "A 2013-14";
                }
                else
                {
                    sess = "A 2010-11";
                }
            }

            string status = "NF";
            string stcourse = findCourseNameBySRID(srid, year);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SubjectID from DDESubject where SyllabusSession='" + sess + "' and CourseName='" + stcourse + "' and Year='" + findAlphaYear(year.ToString()) + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (subjectmarksFilled(srid, Convert.ToInt32(dr[0]), exam))
                    {
                        if (status != "Pending")
                        {
                            status = "OK";
                        }
                    }
                    else
                    {
                        status = "Pending";
                    }
                }


            }

            con.Close();

            string sysession = "";
            if (exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
            {
                sysession = FindInfo.findSySessionBySRID(srid);
            }
            else
            {
                if ((eno.Substring(0, 3) == "C13") && (year == 2) && (FindInfo.findCourseShortNameByID(cid) == "MBA"))
                {
                    sysession = "A 2013-14";
                }
                else if ((eno.Substring(0, 3) == "A13" || eno.Substring(0, 3) == "C14") && (cid == 12 || cid == 27))
                {
                    sysession = "A 2013-14";
                }
                else
                {
                    sysession = "A 2010-11";
                }
            }

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select PracticalID from DDEPractical where SyllabusSession='" + sysession + "' and CourseName='" + stcourse + "' and Year='" + findAlphaYear(year.ToString()) + "'", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();

            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    if (pracmarksFilled(srid, Convert.ToInt32(dr1[0]), exam))
                    {
                        if (status != "Pending")
                        {
                            status = "OK";
                        }
                    }
                    else
                    {
                        status = "Pending";
                    }
                }


            }

            con1.Close();



            return status;

        }

        private static bool isMotherNameExist(int srid)
        {
            bool exist = false;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select MotherName from DDEStudentRecord where SRID='" + srid + "' and RecordStatus='True'", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();

            if (dr1.HasRows)
            {
                dr1.Read();
                if ((dr1[0].ToString() != ""))
                {
                    exist = true;
                }

            }

            con1.Close();
            return exist;
        }

        private static bool pracmarksFilled(int srid, int subid, string exam)
        {
            bool filled = false;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select * from DDEPracticalMarks_" + exam + " where SRID='" + srid + "' and PracticalID='" + subid + "' and MOE='R'", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();

            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    if ((dr1["PracticalMarks"].ToString() != ""))
                    {
                        filled = true;
                    }
                }



            }

            con1.Close();
            return filled;
        }

        private static bool subjectmarksFilled(int srid, int subid, string exam)
        {
            bool filled = false;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select * from DDEMarkSheet_" + exam + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='R'", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();

            if (dr1.HasRows)
            {
                dr1.Read();

                if ((dr1["Theory"].ToString() != "") && (dr1["IA"].ToString() != "") && (dr1["AW"].ToString() != ""))
                {
                    filled = true;
                }

            }

            con1.Close();
            return filled;
        }

        public static string findSCCodeForMarkSheetBySRID(int srid)
        {
            string sccode = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O")
                {
                    sccode = dr["StudyCentreCode"].ToString();
                }
                else if (dr["SCStatus"].ToString() == "C" || dr["SCStatus"].ToString() == "T")
                {
                    sccode = findCurrentSCBySRID(srid);
                }



            }

            con.Close();


            return sccode;
        }

        private static string findCurrentSCBySRID(int srid)
        {
            string sccode = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CurrentSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sccode = dr["CurrentSC"].ToString();
                }

            }

            con.Close();


            return sccode;
        }

        public static object findPrintStatus(int srid, string exam, string moe)
        {
            string printed = "No";
            if (exam == "A13")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select MSPrinted from ExamRecord_June13 where SRID='" + srid + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["MSPrinted"].ToString() == "True")
                    {
                        printed = "Yes";
                    }

                }

                con.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select MSPrinted from DDEExamRecord_" + exam + " where SRID='" + srid + "' and MOE='" + moe + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["MSPrinted"].ToString() == "True")
                    {
                        printed = "Yes";
                    }

                }

                con.Close();
            }


            return printed;
        }

        public static string findCourseAndYearForMS(int srid, int year)
        {
            string course = "NF";
            string finalcourse = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "' ", con);
            int courseid = 0;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();

                int cid = Convert.ToInt32(dr[0]);

                if (cid == 76)
                {
                    if (year == 1)
                    {
                        course = findCourseFullNameByID(Convert.ToInt32(dr[0]));
                        courseid = Convert.ToInt32(dr[0]);
                    }
                    else if (year == 2)
                    {
                        course = findCourseFullNameByID(Convert.ToInt32(dr[1]));
                        courseid = Convert.ToInt32(dr[1]);
                    }
                    else if (year == 3)
                    {
                        course = findCourseFullNameByID(Convert.ToInt32(dr[2]));
                        courseid = Convert.ToInt32(dr[2]);
                    }

                }
                else
                {
                    course = findCourseFullNameByID(Convert.ToInt32(dr[0]));
                    courseid = Convert.ToInt32(dr[0]);
                }
            }

            con.Close();

            if (findCourseDuration(courseid) == 1)
            {

                finalcourse = course;

            }

            else if (findCourseDuration(courseid) == 2)
            {
                if (year == 1)
                {
                    finalcourse = course + " - FIRST YEAR";
                }
                else if (year == 2)
                {
                    finalcourse = course + " - FINAL YEAR";
                }

            }
            else if (findCourseDuration(courseid) == 3)
            {
                if (year == 1)
                {
                    finalcourse = course + " - FIRST YEAR";
                }
                else if (year == 2)
                {
                    finalcourse = course + " - SECOND YEAR";
                }
                else if (year == 3)
                {
                    finalcourse = course + " - FINAL YEAR";
                }


            }

            return finalcourse;
        }

        public static string findCourseAndYearForNewMS(int srid, int year)
        {
            string course = "NF";
            string finalcourse = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "' ", con);
            int courseid = 0;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();

                int cid = Convert.ToInt32(dr[0]);

                if (cid == 76)
                {
                    if (year == 1)
                    {
                        course = findCourseFullNameByID(Convert.ToInt32(dr[0]));
                        courseid = Convert.ToInt32(dr[0]);
                    }
                    else if (year == 2)
                    {
                        course = findCourseFullNameByID(Convert.ToInt32(dr[1]));
                        courseid = Convert.ToInt32(dr[1]);
                    }
                    else if (year == 3)
                    {
                        course = findCourseFullNameByID(Convert.ToInt32(dr[2]));
                        courseid = Convert.ToInt32(dr[2]);
                    }

                }
                else
                {
                    course = findCourseFullNameByID(Convert.ToInt32(dr[0]));
                    courseid = Convert.ToInt32(dr[0]);
                }
            }

            con.Close();

            int cd = findCourseDuration(courseid);

            if (cd == 1)
            {

                finalcourse = course;

            }

            else if (cd == 2)
            {
                if (year == 1)
                {
                    finalcourse = course + " - FIRST YEAR";
                }
                else if (year == 2)
                {
                    finalcourse = course + " - FINAL YEAR";
                }

            }
            else if (cd == 3)
            {
                if (year == 1)
                {
                    finalcourse = course + " - FIRST YEAR";
                }
                else if (year == 2)
                {
                    finalcourse = course + " - SECOND YEAR";
                }
                else if (year == 3)
                {
                    finalcourse = course + " - FINAL YEAR";
                }


            }

            return finalcourse;
        }

        public static bool isDetained(int srid, string exam, string moe, out string remark)
        {
            bool detained = false;
            remark = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

            if (exam == "ALL")
            {
                if (moe == "ALL")
                {
                    cmd.CommandText = "Select DetainedStatus,Remark from DDEDetainedStudents where SRID='" + srid + "'";
                }
                else
                {
                    cmd.CommandText = "Select DetainedStatus,Remark from DDEDetainedStudents where SRID='" + srid + "' and MOE='" + moe + "'";
                }
            }
            else
            {
                if (moe == "ALL")
                {
                    cmd.CommandText = "Select DetainedStatus,Remark from DDEDetainedStudents where SRID='" + srid + "' and Examination='" + exam + "'";
                }
                else
                {
                    cmd.CommandText = "Select DetainedStatus,Remark from DDEDetainedStudents where SRID='" + srid + "' and Examination='" + exam + "' and MOE='" + moe + "'";
                }
            }

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["DetainedStatus"].ToString() == "True")
                {
                    detained = true;
                    remark = ds.Tables[0].Rows[i]["Remark"].ToString();
                }

            }



            return detained;
        }

        public static bool courseEligibleForGrace(int srid)
        {
            int cid = findCourseIDBySRID(srid);

            if (cid == 19 || cid == 76 || cid == 20 || cid == 22)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool eligibleForGrace(int srid, string exam, string year, out int premarks, out string subname)
        {
            bool eligible = false;
            subname = "NF";
            premarks = 0;
            string ss = "";

            if (exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
            {
                ss = FindInfo.findSySessionBySRID(srid);

            }
            else
            {
                ss = "A 2010-11";
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SubjectID from DDESubject where SyllabusSession='" + ss + "' and CourseName='" + FindInfo.findCourseNameBySRID(srid, Convert.ToInt32(year)) + "' and Year='" + FindInfo.findAlphaYear(year) + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            int counter = 0;
            while (dr.Read())
            {
                string tm = findTheoryMarks(srid, exam, Convert.ToInt32(dr[0]));

                if (tm == "" || tm == "-" || tm == "AB")
                {
                    counter = counter + 10;
                    break;
                }
                else
                {
                    if (tm != "" && tm != "-" && tm != "AB")
                    {
                        int theory = ((Convert.ToInt32(tm) * 60) / 100);
                        if (theory < 24)
                        {
                            if (theory >= 19 && theory <= 23)
                            {
                                counter = counter + 1;
                                if (counter == 1)
                                {
                                    premarks = theory;
                                    subname = findProperSubjectNameByID(Convert.ToInt32(dr[0]));
                                }
                            }
                            else
                            {
                                counter = counter + 10;
                                break;
                            }
                        }
                    }
                }



            }

            con.Close();

            if (counter == 1)
            {
                eligible = true;
            }

            return eligible;
        }

        public static bool eligibleForGrace1(int srid, string exam, string year, out int premarks, out string subname)
        {
            bool eligible = false;
            subname = "NF";
            premarks = 0;

           

            string ss = FindInfo.findSySessionBySRID(srid);

           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SubjectID from DDESubject where SyllabusSession='" + ss + "' and CourseName='" + FindInfo.findCourseNameBySRID(srid, Convert.ToInt32(year)) + "' and Year='" + FindInfo.findAlphaYear(year) + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            int counter = 0;
            while (dr.Read())
            {
                string tm = findTheoryMarks(srid, exam, Convert.ToInt32(dr[0]));

                if (tm == "" || tm == "-" || tm == "AB")
                {
                    counter = counter + 10;
                    break;
                }
                else
                {
                    if (tm != "" && tm != "-" && tm != "AB")
                    {
                        int theory = ((Convert.ToInt32(tm) * 70) / 100);
                        if (theory < 28)
                        {
                            if (theory >= 23 && theory <= 27)
                            {
                                counter = counter + 1;
                                if (counter == 1)
                                {
                                    premarks = theory;
                                    subname = findProperSubjectNameByID(Convert.ToInt32(dr[0]));
                                }
                            }
                            else
                            {
                                counter = counter + 10;
                                break;
                            }
                        }
                    }
                }



            }

            con.Close();

            if (counter == 1)
            {
                eligible = true;
            }

            return eligible;
        }
        private static string findTheoryMarks(int srid, string examcode, int subid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select Theory from DDEMarkSheet_" + examcode + " where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='R'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            string tm = "";
            while (dr.Read())
            {
                tm = dr[0].ToString();
            }

            con.Close();
            return tm;
        }

        public static int findCYearBySRID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select CYear from DDEStudentRecord where SRID='" + srid + "' and RecordStatus='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            int cyear = 0;
            while (dr.Read())
            {
                cyear = Convert.ToInt32(dr[0]);
            }

            con.Close();
            return cyear;
        }

        public static string findSCCodeForAdmitCardBySRID(int srid)
        {
            string sccode = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCStatus,StudyCentreCode,PreviousSCCode from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
                {
                    sccode = dr["StudyCentreCode"].ToString();
                }
                else if (dr["SCStatus"].ToString() == "T")
                {
                    sccode = dr["PreviousSCCode"].ToString();
                }



            }

            con.Close();


            return sccode;
        }





        public static object findCourseShortNameBySRID(int srid, int year)
        {
            string course = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "' ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();

                int cid = Convert.ToInt32(dr[0]);
                if (cid == 76)
                {
                    if (year == 1)
                    {
                        course = findCourseNameByID(Convert.ToInt32(dr[0]));
                    }
                    else if (year == 2)
                    {
                        if (dr[1].ToString() != "")
                        {
                            course = findCourseNameByID(Convert.ToInt32(dr[1]));
                        }
                    }
                    else if (year == 3)
                    {
                        if (dr[2].ToString() != "")
                        {
                            course = findCourseNameByID(Convert.ToInt32(dr[2]));
                        }
                    }

                }
                else
                {
                    course = findCourseNameByID(Convert.ToInt32(dr[0]));
                }
            }

            con.Close();

            return course;
        }


        public static bool findRecordStatusBySRID(int srid)
        {
            bool rstatus = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select RecordStatus from DDEStudentrecord where SRID='" + srid + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if ((ds.Tables[0].Rows[i]["RecordStatus"]).ToString() == "True")
                {
                    rstatus = true;
                }
            }

            return rstatus;
        }

        public static string findPaymentModeByID(int pm)
        {
            if (pm == 1)
            {
                return "DD";
            }
            else if (pm == 2)
            {
                return "CHEQUE";
            }
            else if (pm == 3)
            {
                return "CASH";
            }
            else if (pm == 4)
            {
                return "RTGS";
            }
            else if (pm == 5)
            {
                return "DEDUCT FROM REFUND";
            }
            else if (pm == 6)
            {
                return "DIRECT CASH TRANSFER";
            }
            else if (pm == 7)
            {
                return "ADJUSTMENT AGAINST DISCOUNT";
            }
            else
            {
                return "NF";
            }
        }

        public static string isFeePaid(int srid, int fhid, string exam)
        {
            string paid = "NP";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("select distinct ForYear from [DDEFeeRecord_" + (ds.Tables[0].Rows[i]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForExam='" + exam + "'", con1);


                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {

                    if (paid == "NP")
                    {
                        paid = (ds1.Tables[0].Rows[j]["ForYear"]).ToString().ToString();
                    }
                    else
                    {
                        paid = paid + "," + (ds1.Tables[0].Rows[j]["ForYear"]).ToString().ToString();
                    }
                }


            }

            return paid;
        }

        public static string findBothTCSCCodeBySRID(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID ='" + srid + "' ", con);
            SqlDataReader dr;


            string sccode = "NF";
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O")
                {
                    sccode = Convert.ToString(dr["StudyCentreCode"]);
                }
                else if (dr["SCStatus"].ToString() == "T" || dr["SCStatus"].ToString() == "C")
                {
                    sccode = findBothSCCode(srid);
                }
            }
            con.Close();

            return sccode;
        }

        private static string findBothSCCode(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PreviousSC,CurrentSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sccode = dr["CurrentSC"].ToString() + " (" + dr["PreviousSC"].ToString() + ")";
                }

            }

            con.Close();

            return sccode;
        }

        public static string findFRDateBySRID(int srid, int fhid)
        {
            throw new NotImplementedException();
        }

        public static bool isExamAndCourseFeeSubmittedBySRID(int srid, int year, string examcode, string moe, out string error)
        {
            error = "";
            bool examfeepaid = false;
            bool coursefeepaid = false;

            if (moe == "R")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;


                cmd.CommandText = "select * from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "'";


                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    coursefeepaid = true;
                }
                else
                {
                    error = "Course";
                }

                con.Close();
            }
            else if (moe == "B")
            {
                coursefeepaid = true;
            }


            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();
            SqlDataReader dr1;

            if (moe == "R")
            {
                cmd1.CommandText = "select * from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='2' and ForYear='" + year + "' and ForExam='" + examcode + "'";
            }
            else if (moe == "B")
            {
                cmd1.CommandText = "select * from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + examcode + "'";
            }

            cmd1.Connection = con1;
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                examfeepaid = true;
            }
            else
            {
                if (error == "")
                {
                    error = "Exam";
                }
                else
                {
                    error = error + " and Exam";
                }

            }

            con1.Close();


            if (coursefeepaid == true && examfeepaid == true)
            {
                error = "Course and Exam fee paid";
                return true;
            }
            else
            {
                error = error + " Fee not paid";
                return false;
            }
        }

        public static string findRemarkForDetained(int srid, string examcode, string moe)
        {
            string remark = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Remark from DDEDetainedStudents where SRID='" + srid + "' and Examination='" + examcode + "' and MOE='" + moe + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    remark = dr[0].ToString();
                }

            }

            con.Close();

            return remark;
        }

        public static string findSCAllotedStreams(int scid)
        {
            string streams = "0";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select AllotedStreams from DDESCAllotedStreams where SCID='" + scid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr[0].ToString() != "")
                    {
                        streams = dr[0].ToString();
                    }
                }

            }
            else
            {
                streams = "0";
            }

            con.Close();


            return streams;
        }

        public static bool streamRecordEntered(int scid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDESCAllotedStreams where SCID='" + scid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                exist = true;

            }

            con.Close();


            return exist;
        }

        public static bool muToSCAllowed(int pracid)
        {
            bool allowed = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select MUAllowedForSC from DDEPractical where PracticalID='" + pracid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() == "True")
                {

                    allowed = true;
                }

            }

            con.Close();


            return allowed;
        }



        public static bool isCourseFeeSubmittedBySRID(int srid, int year)
        {

            bool coursefeepaid = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "'";



            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                coursefeepaid = true;
            }


            con.Close();

            return coursefeepaid;
        }

        public static bool isExamFeeSubmittedBySRID(int srid, int year, string examcode, string moe)
        {
            bool examfeepaid = false;

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();
            SqlDataReader dr1;

            if (moe == "R")
            {
                cmd1.CommandText = "select * from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='2' and ForYear='" + year + "' and ForExam='" + examcode + "'";
            }

            cmd1.Connection = con1;
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                examfeepaid = true;
            }


            con1.Close();

            return examfeepaid;

        }

        public static string findExamEnrolledBySRIDandYear(int srid, int year, string moe)
        {
            string exam = "NA";
            if (moe == "R")
            {
                string adexam = findAdmissionEnrolledBySRIDandYear(srid, year);

                if (adexam != "NA")
                {

                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.SelectCommand.Connection = con1;
                    da1.Fill(ds1);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("Select ForExam from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='2' and ForYear='" + year + "' and ForExam='" + adexam + "'", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.SelectCommand.Connection = con;
                            da.Fill(ds);
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                exam = (ds.Tables[0].Rows[0]["ForExam"]).ToString();
                                break;


                            }
                        }
                    }
                }
            }

            return exam;

        }

        private static string findAdmissionEnrolledBySRIDandYear(int srid, int year)
        {
            string exam = "NA";

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.SelectCommand.Connection = con1;
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select ForExam from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.SelectCommand.Connection = con;
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        exam = (ds.Tables[0].Rows[0]["ForExam"]).ToString();
                        break;


                    }
                }
            }


            return exam;
        }

        public static string findProANo()
        {
            string ano = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ProANo from DDEProANoCounter", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            dr.Read();

            int counter = Convert.ToInt32(dr[0]);

            ano = "PR-" + string.Format("{0:00000}", counter);


            con.Close();

            return ano;
        }

        public static void updateProANo(string ano)
        {
            int counter = Convert.ToInt32(ano.Substring(3, 5));

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEProANoCounter set ProANo=@ProANo", con);
            cmd.Parameters.AddWithValue("@ProANo", counter + 1);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static string findSCCodeBySCID(int scid)
        {
            string sc = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCCode from DDEStudyCentres where SCID='" + scid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                sc = dr["SCCode"].ToString();

            }

            con.Close();
            return sc;
        }

        public static string findPasswordBySCID(int scid)
        {
            string pass = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Password from DDEStudyCentres where SCID='" + scid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                pass = dr["Password"].ToString();

            }

            con.Close();
            return pass;
        }

        public static int calculateECIDBySRID(int srid, string examcode)
        {

            int ecid = 0;
            string sccode = findSCCodeForAdmitCardBySRID(srid);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ECID,SCCodes from DDEExaminationCentres_" + examcode + "", con);
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

        public static string findExamCentreForAdmitCard(int srid, string examcode, string moe, out string city)
        {
            int ecid = findECIDBySRID(srid, examcode, moe);
            city = "NF";


            string ecdetail = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (examcode == "A13")
            {
                cmd.CommandText = "Select * from DDEExaminationCentres1 where ECID='" + ecid + "'";
            }
            else
            {
                cmd.CommandText = "Select * from DDEExaminationCentres_" + examcode + " where ECID='" + ecid + "'";
            }
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ecdetail = dr["CentreName"].ToString() + "<br/>" + dr["Location"].ToString();
                    city = dr["City"].ToString();

                }

            }

            con.Close();
            return ecdetail;
        }

        public static bool isCTPaperAlloted(int srid, string exam)
        {
            bool cta = false;

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();
            SqlDataReader dr1;


            cmd1.CommandText = "select * from DDECTPaperRecord where SRID='" + srid + "' and Exam='" + exam + "'";


            cmd1.Connection = con1;
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                cta = true;
            }


            con1.Close();

            return cta;
        }

        public static object findFormCounter(int srid, int year, string exam, string moe)
        {
            string fc = "NF";
            if (moe == "B")
            {
                year = 0;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ExamRecordID from DDEExamRecord_" + exam + " where SRID='" + srid + "' and Year='" + year + "' and MOE='" + moe + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                fc = dr[0].ToString();

            }

            con.Close();

            return fc;
        }

        public static int findECIDBySRID(int srid, string exam)
        {
            int ecid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ExamCentreCode from DDEExamRecord_" + exam + " where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                ecid = Convert.ToInt32(dr[0]);

            }

            con.Close();

            return ecid;
        }

        public static int findSRIDByFRID(int frid, string asession)
        {
            int srid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID from [DDEFeeRecord_" + asession + "] where FRID='" + frid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                srid = Convert.ToInt32(dr[0]);

            }

            con.Close();

            return srid;
        }

        public static string[] findSubjectDetailByASPRID(int asprid, string exam)
        {
            string[] sub = { "", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PaperCode from DDEASPrintRecord_" + exam + " where ASPRID='" + asprid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                sub[0] = findAllSubjectNameByPaperCode(dr[0].ToString());
                sub[1] = dr[0].ToString();

            }

            con.Close();


            return sub;
        }

        public static bool awardSheetNoExist(int asprid, string exam)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEASPrintRecord_" + exam + " where ASPRID='" + asprid + "'", con);
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

        public static bool isASPrintedByASNo(int asprid, string exam, out int times)
        {
            bool exist = false;
            times = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEASPrintRecord_" + exam + " where ASPRID='" + asprid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                times = Convert.ToInt32(dr["Times"]);
                exist = true;

            }

            con.Close();

            return exist;
        }



        public static int findTotalStudentsInASByPaperCode(string pcode, bool printed, string exam, int rblmode, out int pr, out int ab)
        {
            string subid = findAllSubjectIDByPaperCode(pcode);

            pr = 0;
            ab = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (printed == true)
            {
                cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + exam + " where SubjectID in (" + subid + ") and ASPRID!='0'";
            }
            else if (printed == false)
            {
                if (exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
                {
                    if (rblmode == 1)
                    {
                        cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + exam + " where SubjectID in (" + subid + ") and ASPRID='0' and (DDEAnswerSheetRecord_" + exam + ".ReceivedBy!='2470' and DDEAnswerSheetRecord_" + exam + ".ReceivedBy!='2552' and DDEAnswerSheetRecord_" + exam + ".ReceivedBy!='2563' and DDEAnswerSheetRecord_" + exam + ".ReceivedBy!='2566'and DDEAnswerSheetRecord_" + exam + ".ReceivedBy!='2572')";
                    }
                    else if (rblmode == 2)
                    {
                        cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + exam + " where SubjectID in (" + subid + ") and ASPRID='0' and (DDEAnswerSheetRecord_" + exam + ".ReceivedBy='2470' or DDEAnswerSheetRecord_" + exam + ".ReceivedBy='2552' or DDEAnswerSheetRecord_" + exam + ".ReceivedBy='2563' or DDEAnswerSheetRecord_" + exam + ".ReceivedBy='2566' or DDEAnswerSheetRecord_" + exam + ".ReceivedBy='2572')";
                    }
                }
                else
                {
                    cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + exam + " where SubjectID in (" + subid + ") and ASPRID='0'";
                }
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Received"].ToString() == "True")
                {
                    pr = pr + 1;
                }
                else if (ds.Tables[0].Rows[i]["Received"].ToString() == "False")
                {
                    ab = ab + 1;
                }
            }



            return ds.Tables[0].Rows.Count;
        }

        public static int findTotalStudentsInASByPCandERID(string pcode, int erid, string exam, out int pr, out int ab)
        {
            string subid = findAllSubjectIDByPaperCode(pcode);

            pr = 0;
            ab = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();


            cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + exam + " where SubjectID in (" + subid + ") and ASPRID='0' and ReceivedBy='" + erid + "'";





            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Received"].ToString() == "True")
                {
                    pr = pr + 1;
                }
                else if (ds.Tables[0].Rows[i]["Received"].ToString() == "False")
                {
                    ab = ab + 1;
                }
            }



            return ds.Tables[0].Rows.Count;
        }

        private static string findAllSubjectIDByPaperCode(string pcode)
        {
            string subid = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();


            cmd.CommandText = "Select * from DDESubject where SyllabusSession!='A 2009-10' and PaperCode='" + pcode + "'";


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);



            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (subid == "")
                {
                    subid = ds.Tables[0].Rows[i]["SubjectID"].ToString();
                }
                else
                {
                    subid = subid + "," + ds.Tables[0].Rows[i]["SubjectID"].ToString();
                }
            }

            return subid;

        }

        public static string findProgrammeCodeNameByNo(int pc)
        {
            if (pc == 1)
            {
                return "DIPLOMA";
            }
            else if (pc == 2)
            {
                return "ADVANCED DIPLOMA";
            }
            else if (pc == 3)
            {
                return "POST GRADUATE DIPLOMA";
            }
            else if (pc == 5)
            {
                return "GRADUATION";
            }
            else if (pc == 6)
            {
                return "POST GRADUATION";
            }
            else
            {
                return "NOT FOUND";
            }
        }

        public static int findTotalStudentsPCandGenderWise(int pc, string batch, string gender)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where Course in (" + findCourseByPC(pc) + ") and  Session='" + batch + "' and Gender='" + gender + "' and RecordStatus='True'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);

            return ds.Tables[0].Rows.Count;
        }

        private static string findCourseByPC(int pc)
        {
            string cids = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();


            cmd.CommandText = "Select CourseID from DDECourse where ProgramCode='" + pc + "'";


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);



            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (cids == "")
                {
                    cids = ds.Tables[0].Rows[i]["CourseID"].ToString();
                }
                else
                {
                    cids = cids + "," + ds.Tables[0].Rows[i]["CourseID"].ToString();
                }
            }

            return cids;
        }

        public static string findGenderBySRID(int srid)
        {
            string gender = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Gender from DDEStudentRecord where SRID='" + srid + "' and RecordStatus='True'", con);
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                gender = dr[0].ToString();

            }

            con.Close();

            return gender;
        }

        public static int findTotalStudentsBySC(string sccode, string batch, out string srids)
        {
            int ts = 0;
            srids = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SRID from DDEStudentRecord where (SCStatus='O' or SCStatus='C') and StudyCentreCode='" + sccode + "' and Session='" + batch + "' and RecordStatus='True' ", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ts = ds.Tables[0].Rows.Count;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (srids == "")
                {
                    srids = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                }
                else
                {
                    srids = srids + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                }
            }


            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select distinct SRID from DDEStudentRecord where SCStatus='T' and Session='" + batch + "' and RecordStatus='True' ", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);

            if (ds1.Tables[0].Rows.Count > 0)
            {

                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    if (findTranferedSCCode(Convert.ToInt32(ds1.Tables[0].Rows[j]["SRID"])) == sccode)
                    {
                        ts = ts + 1;
                        if (srids == "")
                        {
                            srids = (ds1.Tables[0].Rows[j]["SRID"]).ToString();
                        }
                        else
                        {
                            srids = srids + "," + (ds1.Tables[0].Rows[j]["SRID"]).ToString();
                        }
                    }

                }
            }

            return ts;


        }

        public static bool papercodeExistInDateSheet(string papercode, string exam, string sysession, string moe)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where PaperCode='" + papercode + "' and ExaminationCode='" + exam + "' and SyllabusSession='" + sysession + "' and MOE='"+moe+"'", con);
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

        public static string findSubjectCodesByPaperCode(string pc)
        {
            string sc = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SubjectCode from DDESubject where PaperCode='" + pc + "' and SyllabusSession!='A 2009-10'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (sc == "")
                {
                    sc = (ds.Tables[0].Rows[i]["SubjectCode"]).ToString();
                }
                else
                {
                    sc = sc + ", " + (ds.Tables[0].Rows[i]["SubjectCode"]).ToString();
                }
            }

            return sc;
        }


        public static string findSubjectCodesByPaperCode(string pc, string ss)
        {
            string sc = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SubjectCode from DDESubject where PaperCode='" + pc + "' and SyllabusSession='"+ss+"'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (sc == "")
                {
                    sc = (ds.Tables[0].Rows[i]["SubjectCode"]).ToString();
                }
                else
                {
                    sc = sc + ", " + (ds.Tables[0].Rows[i]["SubjectCode"]).ToString();
                }
            }

            return sc;
        }




        public static void findAllByDSID(int dsid, out string pc, out int year, out string date, out string tf, out string tt, out string sysession, out string dstype)
        {
            pc = "NF";
            year = 0;
            date = "NF";
            tf = "NF";
            tt = "NF";
            sysession = "NF";
            dstype = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where DSID='" + dsid + "'", con);
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                pc = dr["PaperCode"].ToString();
                year = Convert.ToInt32(dr["Year"]);
                date = Convert.ToDateTime(dr["Date"]).ToString("dd-MM-yyyy");
                tf = dr["TimeFrom"].ToString();
                tt = dr["TimeTo"].ToString();
                sysession = dr["SyllabusSession"].ToString();
                dstype = dr["DSType"].ToString();

            }

            con.Close();


        }



        public static string findCoursesByPaperCode(string pc)
        {
            string courses = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct CourseName from DDESubject where PaperCode='" + pc + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (courses == "NF")
                {
                    courses = (ds.Tables[0].Rows[i]["CourseName"]).ToString() + "<br/>";
                }
                else
                {
                    courses = courses + (ds.Tables[0].Rows[i]["CourseName"]).ToString() + "<br/>";
                }

            }

            return courses;

        }

        public static string[] findPracticalInfoByID(int pracid)
        {
            string[] pracinfo = { "", "", "", };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalCode,PracticalName,PracticalSNo from DDEPractical where PracticalID='" + pracid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                pracinfo[0] = dr[0].ToString();
                pracinfo[1] = dr[1].ToString();
                pracinfo[2] = dr[2].ToString();
                //pracinfo[3] = dr[3].ToString();
            }
            con.Close();

            return pracinfo;

        }



        public static bool isRollNoAlreadyExist(string rollno, string exam)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_" + exam + " where RollNo='" + rollno + "'", con);
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

        public static bool QPFileExist(string filename, string examcode)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where QPFileName='" + filename + "' and ExaminationCode='" + examcode + "'", con);
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



        public static string findQPOfTheDay(string exam, string selected, int ecid, string date, out string validpc)
        {
            string qp = "NF";
            validpc = "";
            string srid = findAllSRIDByECID(ecid, exam);
            if (srid != "")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select SRID,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SRID in (" + srid + ")", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string course = "";
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]) == 76)
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 1)
                            {
                                if (course == "")
                                {
                                    course = "MBA";
                                }
                                else
                                {
                                    course = course + ",MBA";
                                }
                            }
                            else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 2)
                            {
                                if (course == "")
                                {
                                    course = findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                }
                                else
                                {
                                    if (ds.Tables[0].Rows[i]["Course2Year"].ToString() != "")
                                    {
                                        course = course + "," + findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                    }
                                }
                            }
                            else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 3)
                            {
                                if (course == "")
                                {
                                    course = findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                }
                                else
                                {
                                    if (ds.Tables[0].Rows[i]["Course3Year"].ToString() != "")
                                    {
                                        course = course + "," + findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (course == "")
                            {
                                course = findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]));
                            }
                            else
                            {
                                course = course + "," + findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"])); ;
                            }
                        }

                    }
                }

                string allpc = findAllPCByCourseName(course);


                string pcs = findAllPCOfTheDay(exam, selected, allpc, date, out qp, out validpc);


            }




            return qp;
        }


        private static string findAllPCByCourseName(string course)
        {
            string allpc = "";

            string[] cr = course.Split(',');

            string query = "";

            for (int i = 0; i < cr.Length; i++)
            {
                if (query == "")
                {
                    query = "CourseName='" + cr[i].ToString() + "'";
                }
                else
                {
                    query = query + " or CourseName='" + cr[i].ToString() + "'";
                }
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PaperCode from DDESubject where (" + query + ") and PaperCode!=''", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if ((ds.Tables[0].Rows[i]["PaperCode"]).ToString() != "")
                    {
                        if (allpc == "")
                        {
                            allpc = (ds.Tables[0].Rows[i]["PaperCode"]).ToString();

                        }
                        else
                        {
                            allpc = allpc + "," + (ds.Tables[0].Rows[i]["PaperCode"]).ToString();

                        }
                    }

                }
            }

            return allpc;
        }

        private static string findAllPCOfTheDay(string exam, string selected, string allpc, string date, out string qp, out string validpc)
        {
            string allvalidpc = "";
            validpc = "NF";
            qp = "NF";

            string[] cr = allpc.Split(',');

            string query = "";

            for (int i = 0; i < cr.Length; i++)
            {
                if (query == "")
                {
                    query = "PaperCode='" + cr[i].ToString() + "'";
                }
                else
                {
                    query = query + " or PaperCode='" + cr[i].ToString() + "'";
                }
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PaperCode,QPFileName from DDEExaminationSchedules where (" + query + ") and Date='" + date + "' and ExaminationCode='" + exam + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string[] sqp = selected.Split(',');



            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (allvalidpc == "")
                    {
                        allvalidpc = (ds.Tables[0].Rows[i]["PaperCode"]).ToString();
                        if ((ds.Tables[0].Rows[i]["QPFileName"]).ToString() != "")
                        {
                            int pos = Array.IndexOf(sqp, ds.Tables[0].Rows[i]["PaperCode"].ToString());
                            if ((pos > -1))
                            {
                                if (qp == "NF")
                                {
                                    qp = (ds.Tables[0].Rows[i]["QPFileName"]).ToString();
                                    validpc = (ds.Tables[0].Rows[i]["PaperCode"]).ToString();
                                }
                                else
                                {
                                    qp = qp + "," + (ds.Tables[0].Rows[i]["QPFileName"]).ToString();
                                    validpc = validpc + "," + (ds.Tables[0].Rows[i]["PaperCode"]).ToString();
                                }
                            }
                        }

                    }
                    else
                    {
                        allvalidpc = allvalidpc + "," + (ds.Tables[0].Rows[i]["PaperCode"]).ToString();
                        if ((ds.Tables[0].Rows[i]["QPFileName"]).ToString() != "")
                        {
                            int pos = Array.IndexOf(sqp, ds.Tables[0].Rows[i]["PaperCode"].ToString());
                            if ((pos > -1))
                            {
                                if (qp == "NF")
                                {
                                    qp = (ds.Tables[0].Rows[i]["QPFileName"]).ToString();
                                    validpc = (ds.Tables[0].Rows[i]["PaperCode"]).ToString();
                                }
                                else
                                {
                                    qp = qp + "," + (ds.Tables[0].Rows[i]["QPFileName"]).ToString();
                                    validpc = validpc + "," + (ds.Tables[0].Rows[i]["PaperCode"]).ToString();
                                }
                            }
                        }
                    }

                }
            }

            return allvalidpc;
        }

        public static string findAllSRIDByECID(int ecid, string exam)
        {
            string srid = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SRID from DDEExamRecord_" + exam + " where ExamCentreCode='" + ecid + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (srid == "")
                    {
                        srid = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }
                    else
                    {
                        srid = srid + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }

                }
            }

            return srid;
        }

        public static string findPracticalNameByID(int pid)
        {
            string sub = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalCode from DDEPractical where PracticalID='" + pid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sub = dr[0].ToString();
            }
            con.Close();

            return sub;
        }

        public static string findTotalSRIDByPCandECID(string tsrid, string pc, string exam, string moe)
        {
            string srid = "";

            string[] str = tsrid.Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                if (srid == "")
                {
                    if (papercodeexistBySRID(Convert.ToInt32(str[i]), pc, exam, moe))
                    {
                        srid = str[i];
                    }
                }
                else
                {
                    if (papercodeexistBySRID(Convert.ToInt32(str[i]), pc, exam, moe))
                    {
                        srid = srid + "," + str[i];
                    }
                }

            }

            return srid;
        }

        public static string findTotalSRIDByPCandECID1(string tsrid, string pc, string exam)
        {
            string srid = "";

            string[] str = tsrid.Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                if (srid == "")
                {
                    if (papercodeexistBySRID1(Convert.ToInt32(str[i]), pc, exam))
                    {
                        srid = str[i];
                    }
                }
                else
                {
                    if (papercodeexistBySRID1(Convert.ToInt32(str[i]), pc, exam))
                    {
                        srid = srid + "," + str[i];
                    }
                }

            }

            return srid;
        }

        private static bool papercodeexistBySRID(int srid, string pc, string exam, string moe)
        {
            bool exist = false;
            if (moe == "R")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select SRID,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string course = "";
                if (ds.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) == 76)
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 1)
                            {

                                course = "MBA";

                            }
                            else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 2)
                            {

                                course = findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));

                            }
                            else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 3)
                            {

                                course = findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));

                            }
                        }
                        else
                        {

                            course = findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]));

                        }

                    }
                }

                if (papaercodeexistByCourse(course, Convert.ToInt32(ds.Tables[0].Rows[0]["CYear"]), pc))
                {
                    exist = true;
                }
            }
            else if (moe == "B")
            {
                string[] str = findAllBackPaperPCBySRID(srid, exam);

                int pos = Array.IndexOf(str, pc);
                if (pos > -1)
                {
                    exist = true;
                }

            }

            return exist;

        }

        private static bool papercodeexistBySRID1(int srid, string pc, string exam)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID,Year,MOE from DDEExamRecord_" + exam + " where SRID='" + srid + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[0]["MOE"] == "R")
                    {
                        if (papaercodeexistByCourse(FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[0]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[0]["Year"])), Convert.ToInt32(ds.Tables[0].Rows[0]["Year"]), pc))
                        {
                            exist = true;
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["MOE"] == "B")
                    {
                        string[] str = findAllBackPaperPCBySRID(srid, exam);

                        int pos = Array.IndexOf(str, pc);
                        if (pos > -1)
                        {
                            exist = true;
                        }
                    }
                }
            }


            return exist;

        }

        private static string[] findAllBackPaperPCBySRID(int srid, string exam)
        {
            string[] str = { };
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_" + exam + " where SRID='" + srid + "' and MOE='B' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                if (dr["BPSubjects1"].ToString() != "")
                {
                    string[] sub = dr["BPSubjects1"].ToString().Split(',');
                    for (int i = 0; i < sub.Length; i++)
                    {
                        Array.Resize(ref str, str.Length + 1);
                        str[str.Length - 1] = findPaperCodeByID(Convert.ToInt32(sub[i]));
                    }

                }
                if (dr["BPSubjects2"].ToString() != "")
                {
                    string[] sub = dr["BPSubjects2"].ToString().Split(',');
                    for (int i = 0; i < sub.Length; i++)
                    {
                        Array.Resize(ref str, str.Length + 1);
                        str[str.Length - 1] = findPaperCodeByID(Convert.ToInt32(sub[i]));
                    }

                }

                if (dr["BPSubjects3"].ToString() != "")
                {
                    string[] sub = dr["BPSubjects3"].ToString().Split(',');
                    for (int i = 0; i < sub.Length; i++)
                    {
                        Array.Resize(ref str, str.Length + 1);
                        str[str.Length - 1] = findPaperCodeByID(Convert.ToInt32(sub[i]));
                    }

                }
            }
            con.Close();



            return str;
        }

        private static bool papaercodeexistByCourse(string course, int year, string pc)
        {
            bool exist = false;

            string fyear = findAlphaYear(year.ToString());

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESubject where CourseName='" + course + "' and Year='" + fyear + "' and PaperCode='" + pc + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                exist = true;
            }

            return exist;

        }

        public static string findAllPCByTSRIDS(string tssrid)
        {
            throw new NotImplementedException();
        }

        public static int findTotalEntryofAS(string asno, string exam)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ASRID from DDEASPrintRecord_" + exam + " where ASPRID='" + Convert.ToInt32(asno) + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            string total = "";
            if (dr.HasRows)
            {
                dr.Read();
                total = dr[0].ToString();
            }
            con.Close();

            string[] str = total.Split(',');

            return str.Length;
        }

        public static string findQPFileNameByDSID(int dsid)
        {
            string fn = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select QPFileName from DDEExaminationSchedules where DSID='" + dsid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                fn = dr[0].ToString();
            }
            con.Close();

            return fn;
        }

        public static string findExamNameByCode(string ecode)
        {
            string exam = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamName from DDEExaminations where ExamCode='" + ecode + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exam = dr[0].ToString();
            }
            con.Close();

            return exam;
        }

        public static int findStudentsAttendenceByASNo(int asno, string exam, out int pr, out int ab)
        {
            pr = 0;
            ab = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();


            cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + exam + " where ASPRID='" + asno + "'";





            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["Received"].ToString() == "True")
                {
                    pr = pr + 1;
                }
                else if (ds.Tables[0].Rows[i]["Received"].ToString() == "False")
                {
                    ab = ab + 1;
                }
            }



            return ds.Tables[0].Rows.Count;
        }

        public static bool isEligibleExamForGrace(string exam)
        {
            if (exam == "A13" || exam == "B13" || exam == "A14" || exam == "B14" || exam == "A15" || exam == "B15" || exam == "A16" || exam == "B16" || exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "A19" || exam == "W10" || exam == "Z10" || exam == "W11" || exam == "Z11" || exam == "W12" || exam == "H10" || exam == "G10" || exam == "H11")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string findDFRNo()
        {
            string counter = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CounterValue from DDECounters where CounterName='DRCounter' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                counter = "DR-" + Convert.ToInt32(dr[0]).ToString();
            }
            con.Close();

            return counter;
        }

        public static string findDCTNo()
        {
            string counter = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CounterValue from DDECounters where CounterName='DCTCounter' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                counter = "CT-" + Convert.ToInt32(dr[0]).ToString();
            }
            con.Close();

            return counter;
        }

        public static void updateDRCounter(int counter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDECounters set CounterValue=@CounterValue where CounterName='DRCounter' ", con);
            cmd.Parameters.AddWithValue("@CounterValue", counter + 1);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void updateCTCounter(int counter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDECounters set CounterValue=@CounterValue where CounterName='DCTCounter' ", con);
            cmd.Parameters.AddWithValue("@CounterValue", counter + 1);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static bool isInstrumentVerified(string dcno, string dctype)
        {
            bool exist = false;



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEFeeInstruments where INo='" + dcno + "' and IType='" + dctype + "' and Verified='True' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                exist = true;
            }

            return exist;
        }

        public static bool isInstrumentDistrubted(string dcno, string dctype)
        {
            bool exist = false;



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEFeeInstruments where INo='" + dcno + "' and IType='" + dctype + "' and AmountAlloted='True' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                exist = true;
            }

            return exist;
        }

        public static string findVLNo()
        {
            string counter = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CounterValue from DDECounters where CounterName='VLCounter' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                counter = (Convert.ToInt32(dr[0]) + 1).ToString();
            }
            con.Close();

            return counter;
        }



        public static bool isVLPrintedByNo(int vlno, out int times, out string lastprinted)
        {
            times = 0;
            lastprinted = "NF";
            bool printed = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEVerificationLetters where VLNo='" + vlno + "' and Printed='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                printed = true;
                times = Convert.ToInt32(dr["Times"]);
                lastprinted = dr["LastTimeOfPrint"].ToString();
            }
            con.Close();

            return printed;

        }

        public static bool isVLPrintedByIID(string iids, out int times, out string lastprinted, out int vlno)
        {
            times = 0;
            lastprinted = "NF";
            vlno = 0;
            bool printed = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEVerificationLetters where IIDS='" + iids + "' and Printed='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                printed = true;
                times = Convert.ToInt32(dr["Times"]);
                lastprinted = dr["LastTimeOfPrint"].ToString();
                vlno = Convert.ToInt32(dr["VLNo"]);
            }
            con.Close();

            return printed;
        }

        public static bool isVLNoAlreadyExist(int vlno)
        {

            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEVerificationLetters where VLNo='" + vlno + "'", con);
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

        public static string findApplicableExamByBatch(string batch)
        {
            string exam = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ApplicableExam from DDESession where Session='" + batch + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exam = dr[0].ToString();
            }
            con.Close();

            return exam;
        }

        public static string findApplicableExamByBatchID(int batchid)
        {
            string exam = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ApplicableExam from DDESession where SessionID='" + batchid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exam = dr[0].ToString();
            }
            con.Close();

            return exam;
        }
        public static object findRemark(int rm)
        {
            if (rm == 1)
            {
                return "60% (Only for Course fee)";

            }
            else if (rm == 2)
            {
                return "100% (Only for Course fee)";

            }
            else if (rm == 3)
            {
                return "BOTH (Only for Course fee)";

            }
            else if (rm == 4)
            {
                return "CONCESSION";

            }
            else if (rm == 5)
            {
                return "NA";

            }
            else if (rm == 6)
            {
                return "REMAINING COURSE FEE";

            }
            else
            {
                return "NF";
            }
        }

        public static object findInstrumentDetailsIIDS(string iids)
        {
            string idetails = "NF";


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEFeeInstruments where IID in (" + iids + ") ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    if (idetails == "NF")
                    {
                        idetails = dr["INo"].ToString() + " (" + findPaymentModeByID(Convert.ToInt32(dr["IType"])) + ")";
                    }
                    else
                    {

                        idetails = idetails + "<br/>" + dr["INo"].ToString() + " (" + findPaymentModeByID(Convert.ToInt32(dr["IType"])) + ")";

                    }
                }
            }
            con.Close();

            return idetails;
        }

        public static string findSubjectIDsByPaperCode(string pc)
        {
            string sc = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SubjectID from DDESubject where PaperCode='" + pc + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (sc == "")
                {
                    sc = (ds.Tables[0].Rows[i]["SubjectID"]).ToString();
                }
                else
                {
                    sc = sc + ", " + (ds.Tables[0].Rows[i]["SubjectID"]).ToString();
                }
            }

            return sc;
        }

        public static int findSCIDBySCCode(string sccode)
        {
            int scid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCID from DDEStudyCentres where SCCode='" + sccode + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                scid = (Convert.ToInt32(dr[0]));
            }
            con.Close();

            return scid;
        }

        public static string findAndAllotProspectusRange(int tp, string sccode, int iid, out string error)
        {
            int counter = 0;
            int tr = 2;
            error = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select CounterValue from DDECounters where CounterName='ProspectusCounter'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                counter = Convert.ToInt32(dr[0].ToString());
            }
            con.Close();


            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet dataset = new DataSet();


            command.Connection = connection;
            command.CommandText = "SELECT * FROM DDEProspectusRecord";
            adapter.SelectCommand = command;
            adapter.Fill(dataset, "ProspectusRecord");

            for (int i = (counter + 1); i <= (counter + tp); i++)
            {

                if (!isProspectusNoAlreadyExist(i))
                {
                    DataRow row = dataset.Tables["ProspectusRecord"].NewRow();

                    row["PNo"] = i;
                    row["SRID"] = 0;
                    row["SCCode"] = sccode;
                    row["IID"] = iid;

                    dataset.Tables["ProspectusRecord"].Rows.Add(row);
                    if (tr != 0 || tr == 2)
                    {
                        tr = 1;
                    }
                }
                else
                {
                    tr = 0;
                    error = "Sorry !! Counter Matched. Please contact to administrator.";
                    break;

                }

            }

            try
            {

                if (tr == 1)
                {
                    int result = adapter.Update(dataset, "ProspectusRecord");

                }

            }
            catch (SqlException ex)
            {
                error = ex.Message;
                tr = 0;
            }



            string range = "";

            if (tr == 1)
            {

                updateProsPectusCounter(counter + tp);
                range = (counter + 1).ToString() + "-" + (counter + tp).ToString();

            }
            else
            {
                range = "NA";
            }



            return range;


        }

        private static void updateProsPectusCounter(int pcounter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDECounters set CounterValue=@CounterValue where CounterName='ProspectusCounter'", con);

            cmd.Parameters.AddWithValue("@CounterValue", pcounter);



            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private static bool isProspectusNoAlreadyExist(int pno)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEProspectusRecord where PNo='" + pno + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;

            }
            con.Close();

            return exist;
        }



        internal static bool isTransferedStudent(int srid)
        {
            bool tr = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCStatus from DDEStudentRecord where SRID='" + srid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "T" && dr["StudyCentreCode"].ToString() == "001")
                {
                    tr = true;
                }

            }
            con.Close();

            return tr;
        }

        public static string[] findStudentSCDetails(int srid)
        {
            string[] str = { "False", "" };
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCStatus from DDEStudentRecord where SRID='" + srid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "T")
                {
                    str[0] = "True";
                    str[1] = findPreviousSCCodeBySRID(srid);
                }


            }
            con.Close();

            return str;
        }

        public static string findPreviousExamForBP(int srid, string ec, string year, out string subjectids, out string pracid)
        {
            string str = "";


            subjectids = findBPSubjectID(srid, ec, year);
            pracid = findBPPracticalID(srid, ec, year);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCode from DDEExaminations where Online='True'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr[0].ToString() != ec)
                    {
                        if (subjectids != "")
                        {

                            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlDataReader dr1;
                            SqlCommand cmd1 = new SqlCommand("select * from DDEMarkSheet_" + dr[0].ToString() + " where SRID='" + srid + "' and SubjectID in (" + subjectids + ") and MOE='R'", con1);

                            con1.Open();
                            dr1 = cmd1.ExecuteReader();
                            if (dr1.HasRows)
                            {
                                dr1.Read();

                                int marks = 0;
                                if (dr1["Theory"].ToString() == "" || dr1["Theory"].ToString() == "-" || dr1["Theory"].ToString() == "AB")
                                {
                                    marks = 0;
                                }
                                else
                                {
                                    marks = Convert.ToInt32(dr1["Theory"]);
                                }

                                if (marks < 40)
                                {
                                    if (str == "")
                                    {
                                        str = dr[0].ToString();
                                    }
                                    else
                                    {
                                        str = str + "," + dr[0].ToString();
                                    }

                                }


                            }
                            con1.Close();
                        }

                        if (pracid != "")
                        {

                            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlDataReader dr2;
                            SqlCommand cmd2 = new SqlCommand("select * from DDEPracticalMarks_" + dr[0].ToString() + " where SRID='" + srid + "' and PracticalID in (" + pracid + ") and MOE='R'", con2);

                            con2.Open();
                            dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                dr2.Read();

                                int marks = 0;
                                if (dr2["PracticalMarks"].ToString() == "" || dr2["PracticalMarks"].ToString() == "-" || dr2["PracticalMarks"].ToString() == "AB")
                                {
                                    marks = 0;
                                }
                                else
                                {
                                    marks = Convert.ToInt32(dr2["PracticalMarks"]);
                                }
                                int passingmarks = (((FindInfo.findMAXPracMarksByID(Convert.ToInt32(dr2["PracticalID"]))) * 40) / 100);

                                if (marks < passingmarks)
                                {
                                    string[] sacy = str.Split(',');
                                    int pos = Array.IndexOf(sacy, dr[0].ToString());
                                    if (!(pos > -1))
                                    {
                                        if (str == "")
                                        {
                                            str = dr[0].ToString();
                                        }
                                        else
                                        {
                                            str = str + "," + dr[0].ToString();
                                        }
                                    }

                                }


                            }
                            con2.Close();
                        }
                    }

                }
            }
            con.Close();



            return str;
        }

        private static int findMAXPracMarksByID(int pid)
        {
            int mm = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalMaxMarks from DDEPractical where PracticalID='" + pid + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                mm = Convert.ToInt32(dr[0]);
            }
            con.Close();

            return mm;
        }

        public static string findBPPracticalID(int srid, string ec, string year)
        {
            string pracid = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select BPPracticals" + year + " from DDEExamRecord_" + ec + " where SRID='" + srid + "' and MOE='B'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                pracid = dr[0].ToString();
            }
            con.Close();

            return pracid;
        }

        public static string findBPSubjectID(int srid, string ec, string year)
        {
            string subid = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select BPSubjects" + year + " from DDEExamRecord_" + ec + " where SRID='" + srid + "' and MOE='B'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subid = dr[0].ToString();
            }
            con.Close();

            return subid;
        }


        public static string findSRIDSBySCCode(string sccode)
        {
            string srids = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where StudyCentreCode='" + sccode + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (srids == "")
                    {
                        srids = dr[0].ToString();
                    }
                    else
                    {
                        srids = srids + "," + dr[0].ToString();
                    }
                }

            }
            con.Close();

            return srids;
        }

        public static string findSRIDSBySCCodeandBatch(string sccode, string batch)
        {
            string srids = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where StudyCentreCode='" + sccode + "' and Session='" + batch + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (srids == "")
                    {
                        srids = dr[0].ToString();
                    }
                    else
                    {
                        srids = srids + "," + dr[0].ToString();
                    }
                }

            }
            con.Close();

            return srids;
        }
        public static string findAllSRIDSBySCCode(string sccode)
        {
            string srids = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select SRID from DDEStudentRecord where ((SCStatus='O' or SCStatus='C') and StudyCentreCode='" + sccode + "') or (SCStatus='T' and PreviousSCCode='" + sccode + "') and RecordStatus='True' ";


            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (srids == "")
                    {
                        srids = dr[0].ToString();
                    }
                    else
                    {
                        srids = srids + "," + dr[0].ToString();
                    }
                }

            }
            con.Close();

            return srids;
        }

        public static string findOandCSRIDSBySCCode(string sccode)
        {
            string srids = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select SRID from DDEStudentRecord where (SCStatus='O' or SCStatus='C') and StudyCentreCode='" + sccode + "' and RecordStatus='True' ";

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (srids == "")
                    {
                        srids = dr[0].ToString();
                    }
                    else
                    {
                        srids = srids + "," + dr[0].ToString();
                    }
                }

            }
            con.Close();


            return srids;
        }



        public static int findTotalStudentsByCourseID(int cid, string batch, int year, out string srids)
        {
            srids = "";
            int ts = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (year == 0)
            {
                if (mbagroup(cid))
                {
                    cmd.CommandText = "Select SRID,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + batch + "' and Course2Year='" + cid + "' and RecordStatus='True' ";
                }
                else
                {
                    cmd.CommandText = "Select SRID,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + batch + "' and Course='" + cid + "' and RecordStatus='True' ";
                }

            }
            else
            {
                if (mbagroup(cid))
                {
                    cmd.CommandText = "Select SRID,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + batch + "' and CYear='" + year + "' and Course2Year='" + cid + "' and RecordStatus='True' ";
                }
                else
                {
                    cmd.CommandText = "Select SRID,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + batch + "' and CYear='" + year + "' and Course='" + cid + "' and RecordStatus='True' ";
                }
            }

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);



            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (year == 0)
                {
                    if (cid == 76)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 1)
                        {
                            if (srids == "")
                            {
                                srids = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                            }
                            else
                            {
                                srids = srids + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                            }
                        }

                    }
                    else
                    {
                        if (ds.Tables[0].Rows[i]["Course"].ToString() == "76")
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 2 || Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 3)
                            {
                                if (srids == "")
                                {
                                    srids = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                                }
                                else
                                {
                                    srids = srids + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                                }
                            }

                        }
                        else
                        {
                            if (srids == "")
                            {
                                srids = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                            }
                            else
                            {
                                srids = srids + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                            }
                        }
                    }

                }
                else
                {
                    if (srids == "")
                    {
                        srids = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }
                    else
                    {
                        srids = srids + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }
                }

            }
            if (srids == "")
            {
                ts = 0;
            }
            else
            {
                string[] str = srids.Split(',');

                ts = str.Length;
            }

            return ts;

        }

        private static bool mbagroup(int cid)
        {
            bool exist = false;
            if (cid == 57 || cid == 67 || cid == 78 || cid == 79 || cid == 82 || cid == 85 || cid == 91 || cid == 93 || cid == 96 || cid == 97 || cid == 98 || cid == 119 || cid == 120 || cid == 121 || cid == 124)
            {
                exist = true;
            }
            return exist;
        }

        public static string findSCCodeByVLNo(int vlno)
        {
            string sccode = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCCode from DDEVerificationLetters where VLNo='" + vlno + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sccode = dr[0].ToString();


            }
            con.Close();

            return sccode;
        }

        public static string findSCCodeByInsNo(int ins)
        {
            string sccode = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCCode from DDEFeeInstruments where IID='" + ins + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sccode = dr[0].ToString();


            }
            con.Close();

            return sccode;
        }

        public static bool isGraced(int srid, int year, string exam)
        {
            bool graced = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEGracedStudents where SRID='" + srid + "' and Year='" + year + "' and Exam='" + exam + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                graced = true;
            }
            con.Close();

            return graced;
        }

        public static int findSCProNo()
        {
            int counter = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CounterValue from DDECounters where CounterName='ProSCCodeCounter'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                counter = Convert.ToInt32(dr[0]);
            }
            con.Close();

            return (counter + 1);
        }

        public static int findRefundLNoCounter()
        {
            int counter = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CounterValue from DDECounters where CounterName='RefundLNoCounter'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                counter = Convert.ToInt32(dr[0]);
            }
            con.Close();

            return (counter + 1);
        }

        public static string filterCourseFeePaidBySRIDS(string srids, string exam)
        {
            string query = "";
            string esrid = "";

            if (srids != "")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (query == "")
                        {
                            query = "select distinct SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID in (" + srids + ") and FeeHead='1' and ForExam='" + exam + "'";
                        }
                        else
                        {
                            query = query + " union " + "select distinct SRID from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID in (" + srids + ") and FeeHead='1' and ForExam='" + exam + "'";
                        }
                    }

                }
                con.Close();


                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr1;
                SqlCommand cmd1 = new SqlCommand(query, con1);

                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        if (esrid == "")
                        {
                            esrid = dr1[0].ToString();
                        }
                        else
                        {
                            esrid = esrid + "," + dr1[0].ToString();
                        }
                    }

                }
                con1.Close();
            }

            return esrid;


        }

        public static string findAllFeeHeadID()
        {
            string fhid = "";

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("select FHID from DDEFeeHead", con1);

            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    if (fhid == "")
                    {
                        fhid = dr1[0].ToString();
                    }
                    else
                    {
                        fhid = fhid + "," + dr1[0].ToString();
                    }
                }

            }
            con1.Close();

            return fhid;
        }

        public static string findAllCourseYear(int srid, string exam)
        {

            string year = "";


            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.SelectCommand.Connection = con1;
            da1.Fill(ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select * from [DDEFeeRecord_" + (ds1.Tables[0].Rows[j]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='1' and ForExam='" + exam + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.SelectCommand.Connection = con;
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            if (year != "")
                            {
                                string[] sacy = year.Split(',');
                                int pos = Array.IndexOf(sacy, ds.Tables[0].Rows[i]["ForYear"].ToString());
                                if (!(pos > -1))
                                {
                                    if (year == "")
                                    {
                                        year = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                    }
                                    else
                                    {
                                        year = year + "," + ds.Tables[0].Rows[i]["ForYear"].ToString();
                                    }
                                }
                            }
                            else
                            {

                                year = ds.Tables[0].Rows[i]["ForYear"].ToString(); ;

                            }
                        }
                    }
                }
            }

            return year;
        }

        public static string findSySessionByDSID(int dsid)
        {
            string sysession = "";

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("select SyllabusSession from DDEExaminationSchedules where DSID='" + dsid + "'", con1);

            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {

                    sysession = dr1[0].ToString();

                }

            }
            con1.Close();

            return sysession;
        }

        public static bool isDegreeInfoExist(int srid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEDegreeInfo where SRID='" + srid + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static bool isPracticalExist(string sysession, string course, string praccode)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEPractical where SyllabusSession='" + sysession + "' and CourseName='" + course + "' and PracticalCode='" + praccode + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static bool isCourseExist(string ccode)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDECourse where CourseCode='" + ccode + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static bool isSubjectExist(string sysession, string course, string subcode)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDESubject where SyllabusSession='" + sysession + "' and CourseName='" + course + "' and SubjectCode='" + subcode + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static bool isCityExist(string city)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from CityList where City='" + city + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static bool isENoAlreadyExist(string eno)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where EnrollmentNo='" + eno + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static bool isENoExist(string eno)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where EnrollmentNo='" + eno + "' and RecordStatus='True'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static string[] findDetainedStudents(string exam, string moe)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (moe == "ALL")
            {
                cmd.CommandText = "Select distinct SRID from DDEDetainedStudents where Examination='" + exam + "' and DetainedStatus='1'";
            }
            else
            {
                cmd.CommandText = "Select distinct SRID from DDEDetainedStudents where Examination='" + exam + "' and MOE='" + moe + "' and DetainedStatus='1'";
            }
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string[] det = new string[ds.Tables[0].Rows.Count];

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    det[i] = ds.Tables[0].Rows[i]["SRID"].ToString();
                }
            }

            return det;
        }

        public static string[] findAllDetainedStudentsForSLM()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Select distinct SRID from DDEDetainedStudents where DetainedStatus='1' and MOE='R' and (Examination='A16' or Examination='B16' or Examination='A17' or Examination='B17' or Examination='A18' or Examination='B18' or Examination='A19' or Examination='W10' or Examination='Z10' or Examination='W11' or Examination='Z11' or Examination='W12' or Examination='H10' or Examination='G10' or Examination='H11')";

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            string[] det = new string[ds.Tables[0].Rows.Count];

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    det[i] = ds.Tables[0].Rows[i]["SRID"].ToString();
                }
            }

            return det;
        }

        public static string[] findSubjectInfoWithExamDateTimeBySubID(int subid, string examcode)
        {
            string[] subinfo = { "", "", "", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select DDESubject.PaperCode,DDESubject.SubjectName,DDEExaminationSchedules.Date,DDEExaminationSchedules.TimeFrom,DDEExaminationSchedules.TimeTo from DDESubject inner join DDEExaminationSchedules on DDEExaminationSchedules.PaperCode=DDESubject.PaperCode where DDESubject.SubjectID='" + subid + "' and  DDEExaminationSchedules.ExaminationCode='" + examcode + "' order by DDESubject.SubjectSNo", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subinfo[0] = dr["PaperCode"].ToString();
                subinfo[1] = dr["SubjectName"].ToString();
                subinfo[2] = Convert.ToDateTime(dr["Date"]).ToString("dd-MM-yyyy");
                subinfo[3] = dr["TimeFrom"].ToString().Substring(0, 6) + findSection(dr["TimeFrom"].ToString().Substring(6, 1)) + " - " + dr["TimeTo"].ToString().Substring(0, 6) + findSection(dr["TimeTo"].ToString().Substring(6, 1));

            }
            con.Close();

            return subinfo;

        }

        private static string findSection(string sec)
        {
            if (sec == "1")
            {
                return "AM";
            }
            else if (sec == "2")
            {
                return "PM";
            }
            else if (sec == "3")
            {
                return "NOON";
            }
            else
            {
                return "NF";
            }
        }

        public static int findCourseIDByCourseName(string cname)
        {
            int cid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID from DDECourse where CourseName='" + cname + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                cid = Convert.ToInt32(dr[0]);
            }
            con.Close();

            return cid;
        }

        public static bool isInsAlreadyPublished(int iid, out string lno)
        {
            lno = "";
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select RLID from DDERefundLetterRecord where IID='" + iid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lno = dr["RLID"].ToString();
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static bool refundLetterExistByNo(string lno)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select RLID from DDERefundLetterRecord where RLID='" + lno + "'", con);
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

        public static string findSySessionBySRID(int srid)
        {
            string sysession = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SyllabusSession from DDEStudentRecord where SRID='" + srid + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sysession = dr[0].ToString();
            }
            con.Close();

            return sysession;
        }

        public static int findRIIDByRLID(int rlid, out string rrids)
        {
            int riid = 0;
            rrids = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select RRIDS,RIID from DDERefundLetterRecord where RLID='" + rlid + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                riid = Convert.ToInt32(dr["RIID"]);
                rrids = dr["RRIDS"].ToString();
            }
            con.Close();

            return riid;
        }

        public static bool isRefundGenerated(int srid, int year)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select RRID from DDERefundRecord where SRID='" + srid + "' and Year='" + year + "' and Refund!='0'", con);
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

        public static bool isDLPrintedByID(int diid, out int times, out string lastprinted)
        {
            times = 0;
            lastprinted = "";
            bool printed = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEDegreeInfo where DIID='" + diid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["LetterPublished"].ToString() == "True")
                {
                    printed = true;
                }


            }
            con.Close();

            return printed;
        }

        public static bool isMLPrintedByID(int mlid, out int times, out string lastprinted)
        {
            times = 0;
            lastprinted = "";
            bool printed = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEMigrationLetters where MLID='" + mlid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {               
                printed = true;               
            }
            con.Close();

            return printed;
        }

        public static bool degreeLetterExistByNo(string lno)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DIID from DDEDegreeInfo where DIID='" + lno + "'", con);
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

        public static bool CLDegreeExistByNo(string lno)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select CLDID from DDECoveringLettersDegree where CLDID='" + lno + "'", con);
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
        public static string findASPrintedBy(int asno, string exam)
        {
            string eid = "NF";
            int erid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PrintedBy from DDEASPrintRecord_" + exam + " where ASPRID='" + asno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                erid = Convert.ToInt32(dr["PrintedBy"]);
            }
            con.Close();

            eid = findEmployeeName1ByERID(erid);


            return eid;
        }

        private static string findEmployeeIDByERID(int erid)
        {
            string eid = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select EmployeeID  from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                eid = dr[0].ToString();

            }
            con.Close();

            return eid;
        }

        private static string findEmployeeName1ByERID(int erid)
        {
            string ename = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Name  from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ename = dr[0].ToString();

            }
            con.Close();

            return ename;
        }

        public static int findSubjectYearByPaperCode(string pc)
        {
            string subyear = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Year from DDESubject where PaperCode='" + pc + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subyear = dr[0].ToString();
            }
            con.Close();


            if (subyear == "1st Year")
            {
                return 1;
            }
            else if (subyear == "2nd Year")
            {
                return 2;
            }
            else if (subyear == "3rd Year")
            {
                return 3;
            }
            else return 0;
        }

        public static string[] findDegreeDetails(int p)
        {
            throw new NotImplementedException();
        }

        public static bool degreeLetterExistBySRID(int srid, out int diid, out string error)
        {
            bool exist = false;
            error = "";
            diid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DIID,LetterPublished from DDEDegreeInfo where SRID='" + srid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
               
                exist = true;
                diid = Convert.ToInt32(dr["DIID"]);
               
            }
            else
            {
                error = "Sorry !! There is no Degree application with this Enrollment No.";
            }
            con.Close();

            return exist;
        }

        public static bool CLDegreeExistBySRID(int srid, out int clno, out string error)
        {
            bool exist = false;
            error = "";
            clno = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select CLPublished,CLNo from DDEDegreeInfo where SRID='" + srid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["CLPublished"].ToString() == "True")
                {
                    exist = true;
                    clno = Convert.ToInt32(dr["CLNo"]);
                }
                else if (dr["LetterPublished"].ToString() == "False")
                {
                    error = "Sorry !! Covering Letter is not published till yet for this student";
                }
            }
            else
            {
                error = "Sorry !! Degree Request does not exist with this Enrollment No.";
            }
            con.Close();

            return exist;
        }

        public static bool isEligibleForCMS(int srid)
        {
            bool eligible = false;
            int cd = findCourseDuration(findCourseIDBySRID(srid));

            if (isMBACourse(FindInfo.findCourseIDBySRID(srid)))
            {
                if (FindInfo.findSessionIDBySession(FindInfo.findSessionBySRID(srid)) >= 9)
                {
                    cd = 2;
                }
                else
                {
                    cd = 3;
                }
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("select SRID from [DDEFeeRecord_" + (ds.Tables[0].Rows[i]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + cd + "'", con1);


                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    eligible = true;
                    break;
                }

            }


            return eligible;
        }

        public static bool isEligibleForTS(int srid)
        {
            bool eligible = false;
            int cd = findCourseDuration(findCourseIDBySRID(srid));

            if (isMBACourse(FindInfo.findCourseIDBySRID(srid)))
            {
                if (FindInfo.findSessionIDBySession(FindInfo.findSessionBySRID(srid)) >= 9)
                {
                    cd = 2;
                }
                else
                {
                    cd = 3;
                }
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("select SRID from [DDEFeeRecord_" + (ds.Tables[0].Rows[i]["AcountSession"]).ToString() + "] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + cd + "'", con1);


                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    eligible = true;
                    break;
                }

            }


            return eligible;
        }
        private static int findSessionIDBySession(string session)
        {
            int sid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SessionID from DDESession where Session='" + session + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sid = Convert.ToInt32(dr[0]);
            }

            con.Close();

            return sid;
        }

        private static string findSessionBySRID(int srid)
        {
            string sess = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select [Session] from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sess = dr[0].ToString();
            }

            con.Close();

            return sess;
        }

        public static bool isEligibleForTwoYearMBA(string eno)
        {
            bool el = false;
            string batch = eno.Substring(0, 3);

            if (batch == "C13" || batch == "A13" || batch == "C14" || batch == "A14" || batch == "C15" || batch == "A15" || batch == "C16" || batch == "A16" || batch == "C17" || batch == "A17" || batch == "C18" || batch == "A18" || batch == "C19" || batch == "A19")
            {
                el = true;
            }

            return el;
        }

        public static bool isSLMExist(string slmcode)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SLMCode from DDESLMMaster where SLMCode='" + slmcode + "'", con);
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

        public static bool isRegisteredForSLM(int srid, int year)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMIssueRecord where SRID='" + srid + "' and Year='" + year + "'", con);
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

        public static bool isMBASpecialazation(int cid)
        {
            if (cid == 57 || cid == 67 || cid == 78 || cid == 79 || cid == 82 || cid == 85 || cid == 91 || cid == 93 || cid == 96 || cid == 97 || cid == 98 || cid == 119 || cid == 120 || cid == 121 || cid == 124 || cid == 125)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isMBACourse(int cid)
        {
            if (cid == 57 || cid == 67 || cid == 76 || cid == 78 || cid == 79 || cid == 82 || cid == 85 || cid == 91 || cid == 93 || cid == 96 || cid == 97 || cid == 98 || cid == 119 || cid == 120 || cid == 121 || cid == 124 || cid == 125)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isExamCentreSet(string exam)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_" + exam + "", con);
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

        public static string[] findExamCentreDetailByECID(int srid, string exam)
        {
            string[] ec = { "", "", "" };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEExamRecord_" + exam + ".ExamCentreCode as ECID,DDEExaminationCentres_" + exam + ".City,DDEExaminationCentres_" + exam + ".ExamCentreCode,DDEExaminationCentres_" + exam + ".CentreName from DDEExamRecord_" + exam + " inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode where DDEExamRecord_" + exam + ".SRID='" + srid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ec[0] = dr["City"].ToString();
                ec[1] = dr["ExamCentreCode"].ToString();
                ec[2] = dr["CentreName"].ToString();
            }

            con.Close();

            return ec;
        }

        public static int findCourseFeeByBatch(int cid, int batchid)
        {
            int rfee = 0;
            string bt = findSessionCodeByID(batchid);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + bt + " from DDECourse where CourseID='" + cid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                rfee = Convert.ToInt32(dr[0]);

            }

            con.Close();
            return rfee;
        }

        public static string findECCodeByECID(int ecid, string exam)
        {
            string ec = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCentreCode from DDEExaminationCentres_" + exam + " where ECID='" + ecid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                ec = dr["ExamCentreCode"].ToString();

            }

            con.Close();
            return ec;
        }

        public static string findPasswordByECID(int ecid, string exam)
        {
            string pass = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Password from DDEExaminationCentres_" + exam + " where ECID='" + ecid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                pass = dr["Password"].ToString();

            }

            con.Close();
            return pass;
        }

        public static bool isApplicationEligible(int psrid, out string error)
        {
            bool eligible = false;
            error = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Eligible,OriginalsVerified,AdmissionStatus from DDEPendingStudentRecord where PSRID='" + psrid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                if (dr["Eligible"].ToString() == "YES" && dr["OriginalsVerified"].ToString() == "YES" && dr["AdmissionStatus"].ToString() == "1")
                {
                    eligible = true;
                }
                else
                {
                    error = "Sorry !! This Student is not marked 'Eligible' yet.";
                }

            }
            else
            {
                error = "Sorry !! No Record found with this ID.";
            }

            con.Close();
            return eligible;
        }

        public static int findBatchID(string batch)
        {
            int bid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SessionID from DDESession where [Session]='" + batch + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                bid = Convert.ToInt32(dr[0]);

            }

            con.Close();
            return bid;
        }

        public static string findAllSRIDByECIDandMOE(int ecid, string exam, string moe)
        {
            string srid = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SRID from DDEExamRecord_" + exam + " where ExamCentreCode='" + ecid + "' and MOE='" + moe + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (srid == "")
                    {
                        srid = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }
                    else
                    {
                        srid = srid + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }

                }
            }

            return srid;
        }

        public static string findAllSRIDByECIDandMOEandSCCode(int ecid, string exam, string moe, string sccode)
        {
            string srid = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct DDEExamRecord_" + exam + ".SRID from DDEExamRecord_" + exam + " inner join DDEStudentRecord on DDEExamRecord_" + exam + ".SRID=DDEStudentRecord.SRID where DDEExamRecord_" + exam + ".ExamCentreCode='" + ecid + "' and DDEExamRecord_" + exam + ".MOE='" + moe + "' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + sccode + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + sccode + "' ))", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (srid == "")
                    {
                        srid = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }
                    else
                    {
                        srid = srid + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }

                }
            }

            return srid;
        }

        public static int findBPYear(int srid, string exam)
        {
            string year = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_" + exam + " where SRID='" + srid + "' and MOE='B'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if ((dr["BPSubjects1"].ToString() != "") || (dr["BPPracticals1"].ToString() != ""))
                {
                    if ((dr["BPSubjects2"].ToString() != "") || (dr["BPPracticals2"].ToString() != ""))
                    {
                        if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                        {
                            year = "1,2,3";
                        }
                        else
                        {
                            year = "1,2";
                        }
                    }
                    else if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                    {
                        year = "1,3";
                    }
                    else
                    {
                        year = "1";
                    }
                }
                else if ((dr["BPSubjects2"].ToString() != "") || (dr["BPPracticals2"].ToString() != ""))
                {
                    if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                    {
                        year = "2,3";
                    }
                    else
                    {
                        year = "2";
                    }
                }
                else if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                {
                    year = "3";

                }
            }
            con.Close();

            int y = Convert.ToInt32(year.Substring((year.Length - 1), 1));
            return y;
        }

        public static string findSCAddressingDetails(string sccode)
        {
            string scname = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Administrator,Location,Address,City from DDEStudyCentres where SCCode='" + sccode + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                scname = dr["Administrator"].ToString() + "</br>" + dr["Location"].ToString() + " (AF Code-" + sccode + ")" + "</br>" + dr["Address"].ToString() + "</br>" + dr["City"].ToString();

            }

            con.Close();

            return scname;
        }

        public static string findSCAddress(string sccode)
        {
            string scname = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Address,City from DDEStudyCentres where SCCode='" + sccode + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                scname = dr["Address"].ToString() + "," + dr["City"].ToString();

            }

            con.Close();

            return scname;
        }

        public static double findDispatchPartyRateByDPID(int dpid)
        {
            double rate = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Rate from DDEDispatchParty where DPID='" + dpid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                rate = Convert.ToDouble(dr[0]);

            }

            con.Close();
            return rate;
        }

        public static object findPendingStudentPhoto(int psrid)
        {
            object sp = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select StudentPhoto from DDEPendingStudentRecord where PSRID='" + psrid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                sp = dr[0];
            }

            con.Close();
            return sp;
        }

        public static int findECIDByECCode(string eccode, string exam)
        {
            int ecid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ECID from DDEExaminationCentres_" + exam + " where ExamCentreCode='" + eccode + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                ecid = Convert.ToInt32(dr[0]);

            }

            con.Close();
            return ecid;
        }

        public static bool isValidDate(string idate)
        {
            bool valid = false;

            int year = Convert.ToInt32(idate.Substring(0, 4));
            int month = Convert.ToInt32(idate.Substring(5, 2));
            int day = Convert.ToInt32(idate.Substring(8, 2));
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day >= 1 && day <= 31)
                {
                    valid = true;
                }
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day >= 1 && day <= 30)
                {
                    valid = true;
                }
            }
            else if (month == 2)
            {
                int leap = year % 4;
                if (leap == 0)
                {
                    if (day >= 1 && day <= 29)
                    {
                        valid = true;
                    }
                }
                else
                {
                    if (day >= 1 && day <= 28)
                    {
                        valid = true;
                    }

                }
            }

            return valid;
        }

        public static int findSRIDByOURID(int ourid)
        {
            int srid = 0;


            return srid;
        }

        public static int findPSRIDBySCFormCounter(int scformcounter, string sccode)
        {
            int psrid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PSRID from DDEPendingStudentRecord where SCFormCounter='" + scformcounter + "' and StudyCentreCode='" + sccode + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                psrid = Convert.ToInt32(dr[0]);

            }

            con.Close();
            return psrid;
        }

        public static object findExaminerByID(int exid)
        {
            string examiner = "NA";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Name,Type from DDEExaminers where ExID='" + exid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                examiner = dr[0].ToString() + "(" + dr[1].ToString().Substring(0, 1) + ")";

            }

            con.Close();

            return examiner;
        }

        public static object findPracticalNameByPracticalCode(string pcode, string sysession)
        {
            string pname = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalName from DDEPractical where PracticalCode='" + pcode + "' and SyllabusSession='" + sysession + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                pname = dr["PracticalName"].ToString();

            }

            con.Close();

            return pname;
        }

        public static string findQPFileName(string examcode, string papercode)
        {
            string file = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select QPFileName from DDEExaminationSchedules where PaperCode='" + papercode + "' and ExaminationCode='" + examcode + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (file == "")
                    {
                        file = dr["QPFileName"].ToString();
                    }
                    else
                    {
                        file = file + "<br/>" + dr["QPFileName"].ToString();
                    }
                }

            }

            con.Close();

            return file;
        }

        public static int findPracMaxMarksByID(int pracid)
        {
            int maxmarks = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalMaxMarks from DDEPractical where PracticalID='" + pracid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                maxmarks = Convert.ToInt32(dr[0]);

            }

            con.Close();
            return maxmarks;
        }

        public static bool IsStudentPhotoExist(int srid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select StudentPhoto from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                if (dr[0].ToString() != "")
                {
                    exist = true;
                }
                else
                {
                    exist = false;
                }

            }

            con.Close();
            return exist;
        }

        public static int findFHIDByFRID(int frid, string asession)
        {
            int fhid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select FeeHead from [DDEFeeRecord_" + asession + "] where FRID='" + frid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                fhid = Convert.ToInt32(dr[0]);

            }

            con.Close();

            return fhid;

        }

        public static string findForExamByFRID(int frid, string asession)
        {
            string exam = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ForExam from [DDEFeeRecord_" + asession + "] where FRID='" + frid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                exam = Convert.ToString(dr[0]);

            }

            con.Close();

            return exam;

        }

        public static int findSRIDByOANo(int oano)
        {
            int srid = 0;
            if (oano != 0)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where PSRID='" + oano + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();

                    srid = Convert.ToInt32(dr["SRID"]);

                }

                con.Close();
            }
            return srid;
        }

        public static bool IsMSAlreadyPrinted(int srid, int year, string exam, string moe, out int counter, out int times, out string lastprinted)
        {
            bool printed = false;
            counter = 0;
            times = 0;
            lastprinted = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select MSPrinted,Times,LastPrintTime,MSCounter from DDEExamRecord_" + exam + " where SRID='" + srid + "' and Year='" + year + "' and MOE='" + moe + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                if (dr["MSPrinted"].ToString() == "True")
                {
                    printed = true;
                    times = Convert.ToInt32(dr["Times"]);
                    lastprinted = dr["LastPrintTime"].ToString();
                    counter = Convert.ToInt32(dr["MSCounter"]);

                }


            }

            con.Close();

            return printed;
        }

        public static string findAllottedToByASNo(int asno, string exam)
        {
            object facname = "NA";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AllottedTo from DDEASPrintRecord_" + exam + " where ASPRID='" + asno + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                facname = findExaminerByID(Convert.ToInt32(dr[0]));

            }

            con.Close();

            return facname.ToString();
        }

        public static bool isMigrationInfoExist(int srid, string doctype)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEMigrationInfo where SRID='" + srid + "' and DocumentType='"+doctype+"'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        public static string findMigrationLetterPubDate(int lno)
        {
            string date = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PublishedOn from DDEMigrationLetters where MLID='" + lno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                date = dr[0].ToString();
            }
            con.Close();

            return date;
        }

        public static string findCLDegreePubDate(int lno)
        {
            string date = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PublishedOn from DDECoveringLettersDegree where CLDID='" + lno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                date = dr[0].ToString();
            }
            con.Close();

            return date;
        }

        public static bool migrationLetterExistBySRID(int srid,out int mid, out int LNo, out string error)
        {
            bool exist = false;
            error = "";
            LNo = 0;
            mid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select MID,LNo,LetterPublished from DDEMigrationInfo where SRID='" + srid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["LetterPublished"].ToString() == "True")
                {
                    exist = true;
                    LNo = Convert.ToInt32(dr["LNo"]);
                    mid = Convert.ToInt32(dr["MID"]);
                }
                else if (dr["LetterPublished"].ToString() == "False")
                {
                    error = "Sorry !! Migration requested but letter is not published till yet for this student";
                }
            }
            else
            {
                error = "Sorry !! There is no Migration request with this Enrollment No.";
            }
            con.Close();

            return exist;
        }

        public static bool migrationLetterExistByNo(int lno, out string mids)
        {
            bool exist = false;
            mids = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select MIDS from DDEMigrationLetters where MLID='" + lno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exist = true;
                mids = dr[0].ToString();
            }
            con.Close();

            return exist;
        }

        public static string findMigrationEntriesByMLNo(int lno)
        {
          
            string  mids = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select MIDS from DDEMigrationLetters where MLID='" + lno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
              
                mids = dr[0].ToString();
            }
            con.Close();

            return mids;
        }

        public static string findAllSubjectCodeByPaperCode(string pcode)
        {
            string sname = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct SubjectCode from DDESubject where PaperCode='" + pcode + "' and SyllabusSession!='A 2009-10'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (sname == "NF")
                    {
                        sname = dr["SubjectCode"].ToString().Trim();

                    }
                    else
                    {
                        //string[] str = sname.Split(',');

                        //int pos = Array.IndexOf(str, dr["SubjectName"].ToString().Trim());
                        //if(!(pos>-1))
                        //{
                        sname = sname + "," + dr["SubjectCode"].ToString().Trim();
                        //}
                    }
                }

            }

            con.Close();

            return sname;
        }

        public static string findYearByPaperCode(string pcode)
        {
            string year = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct Year from DDESubject where PaperCode='" + pcode + "' and SyllabusSession!='A 2009-10'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (year == "NF")
                    {
                        year = dr["Year"].ToString().Trim();

                    }
                    else
                    {
                        //string[] str = sname.Split(',');

                        //int pos = Array.IndexOf(str, dr["SubjectName"].ToString().Trim());
                        //if(!(pos>-1))
                        //{
                             year = year + "," + dr["Year"].ToString().Trim();
                        //}
                    }
                }
            }


            con.Close();

            return year;
        }

       

        public static int findNOTPrintedASByPC(string pcode, string exam, int erid)
        {
            int count = 0;

            if (erid != 2470 && erid != 2552 && erid != 2563 && erid != 2566 && erid != 2572)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Select DDEAnswerSheetRecord_" + exam + ".ASRID,DDESubject.PaperCode from DDEAnswerSheetRecord_" + exam + " inner join DDESubject on DDEAnswerSheetRecord_" + exam + ".SubjectID=DDESubject.SubjectID where DDESubject.PaperCode='" + pcode + "' and DDEAnswerSheetRecord_" + exam + ".ReceivedBy='" + erid + "' and DDEAnswerSheetRecord_" + exam + ".ASPRID='0' ";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.SelectCommand.Connection = con;
                da.Fill(ds);

                count = ds.Tables[0].Rows.Count;
            }


            return count;
        }

        public static int findAllottedFIDByASNo(int asno, string exam)
        {
            int fid=0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AllottedTo from DDEASPrintRecord_" + exam + " where ASPRID='" + asno + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                fid = Convert.ToInt32(dr[0]);

            }

            con.Close();

            return fid;
        }

        public static string findSessionCodeByID(int sessionid)
        {
            string sessioncode = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SessionCode from DDESession where SessionID='" +sessionid + "' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sessioncode= dr["SessionCode"].ToString();
            }
            con.Close();

            return sessioncode;
        }

        public static string findNextDegreeSNo(int batchid)
        {
            int dc = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select DegreeCounter from DDESession where SessionID='" + batchid+"' ", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                dc = Convert.ToInt32(dr[0]);
            }
            con.Close();

            return String.Format("{0:000000}",dc + 1) ;
        }

        public static int updateDegreeCounter(int batchid, int degreecounter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDESession set DegreeCounter=@DegreeCounter where SessionID='" + batchid+"' ", con);
            cmd.Parameters.AddWithValue("@DegreeCounter", degreecounter);

            con.Open();
            int i=cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public static int findSRIDByDIID(int diid)
        {
            int srid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEDegreeInfo where DIID='" + diid + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
               
                srid = Convert.ToInt32(dr["SRID"]);
            }
            con.Close();

            return srid;
        }
        public static bool isDegreePrinted(int srid, out string date, out int not)
        {
            bool exist = false;
            date = "";
            not = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEDegreeInfo where SRID='" + srid + "' and DPStatus='True'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exist = true;
                date = Convert.ToDateTime(dr["DPDoneOn"]).ToString("dd-MM-yyyy");
                not = Convert.ToInt32(dr["NOTPrinted"]);
            }
            con.Close();

            return exist;
        }

        public static bool isDegreePrinted(int srid, out string sno)
        {
            bool exist = false;
            sno = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEDegreeInfo where SRID='" + srid + "' and SNo!='0'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exist = true;             
                sno= Convert.ToString(dr["SNo"]);
            }
            con.Close();

            return exist;
        }

        public static int findTotalQuestionByPC(string papercode)
        {
            int tq = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Count(QID) as TQ from QuestionBank where PaperCode='" + papercode + "'", con);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                tq = Convert.ToInt32(dr["TQ"]);
            }
            con.Close();

            return tq;
        }

    }

}
