using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace DDE.DAL
{
   public class PopulateDDList : System.Web.UI.Page
    {

        public  void populateddlist(DropDownList ddlist, String dbtable,String valuecol, String textcol, String orderby)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select "+valuecol+","+textcol+" from "+dbtable+" order by "+orderby+" ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[1].ToString());
                ddlist.Items.FindByText(dr[1].ToString()).Value=dr[0].ToString();

            }

            con.Close();

        }

        public static void populatePaperCodeFromQB(DropDownList ddlistPaperCode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct PaperCode from QuestionBank order by PaperCode", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistPaperCode.Items.Add(dr["PaperCode"].ToString());
            }
           
            con.Close();
        }

        public static void populateGCategory(DropDownList ddlistGC)
        {
            ddlistGC.Items.Add(new ListItem("ALL", "0"));
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select GCID,GCategory from DDEGrievanceCategory where GCID!='1' order by GCategory", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistGC.Items.Add(new ListItem(dr["GCategory"].ToString(), dr["GCID"].ToString()));
            }
            ddlistGC.Items.Add(new ListItem("OTHER", "1"));
            con.Close();
        }

        public void populateddlist(DropDownList ddlist, String dbtable, String valuecol, String textcol)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + valuecol + "," + textcol + " from " + dbtable+" ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[1].ToString());
                ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();

        }

        public void populateddlistByWhere(DropDownList ddlist, String dbtable, String valuecol, String textcol, String wherecol, String wherevalue, String orderby)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + valuecol + "," + textcol + " from " + dbtable + " where " + wherecol + "='" + wherevalue + "' order by " + orderby + " ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[1].ToString());
                ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();

        }
        
       
       public void populateddlistByWhere(DropDownList ddlist, String dbtable, String valuecol, String textcol, String wherecol, int wherevalue, String orderby)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + valuecol + "," + textcol + " from " + dbtable + " where " + wherecol + "='" + wherevalue + "' order by " + orderby + " ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[1].ToString());
                ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();

        }
        public void populateddlistByWhere(DropDownList ddlist, String dbtable, String valuecol, String textcol, String wherecol, String wherevalue)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + valuecol + "," + textcol + " from " + dbtable + " where " + wherecol + "='" + wherevalue +  "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[1].ToString());
                ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();

        }

        
        public void populateddlistBy2Where(DropDownList ddlist, String dbtable, String valuecol, String textcol, String wherecol1, String wherevalue1, String wherecol2, String wherevalue2, String orderby)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + valuecol + "," + textcol + " from " + dbtable + " where " + wherecol1 + "='" + wherevalue1 + "' and " + wherecol2 + "='" + wherevalue2 + "' order by " + orderby + " ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[1].ToString());
                ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();

        }
        public void populateddlist2(DropDownList ddlist, String dbtable, String valuecol,String wherecol,String wherecolvalue, String textcol, String orderby)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + valuecol + "," + textcol + " from " + dbtable+ " where "+wherecol+"='"+ wherecolvalue +"' " + " order by " + orderby + " ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[1].ToString());
                ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();

        }

        public void populateCYear(DropDownList ddlist)
        {
            int i;
            int cy=Convert.ToInt32(DateTime.Now.ToString("yyyy"));

            for (i = 1950; i <= cy; i++)
            {
                ddlist.Items.Add(i.ToString());
                ddlist.Items.FindByText(i.ToString()).Value = i.ToString();
            }         

        }

        public void populateRollNo(DropDownList ddlist)
        {
            int i;
           

            for (i = 1; i <= 200; i++)
            {
                
                ddlist.Items.Add(string.Format("{0:000}", i));
                ddlist.Items.FindByText(string.Format("{0:000}", i)).Value = string.Format("{0:000}", i);
            }


            
        }

        public void populateBatchYear(DropDownList ddlist)
        {
            int i;
            int cy = Convert.ToInt32(DateTime.Now.ToString("yyyy"));

            for (i = 1996; i <= cy; i++)
            {
                ddlist.Items.Add(i.ToString());
                ddlist.Items.FindByText(i.ToString()).Value = i.ToString();
            }

        }

        public static void populateCourses(DropDownList ddlist)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse where Online='True' order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[2].ToString() == "")
                {
                    ddlist.Items.Add(dr[1].ToString());
                    ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlist.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlist.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }

            }
            con.Close();
        }

        public static void populatePreviousCourses(DropDownList ddlist)
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
                    ddlist.Items.Add(dr[1].ToString());
                    ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlist.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlist.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }

            }
            con.Close();
        }

        public static void populateExam(DropDownList ddlist)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCode,ExamName from DDEExaminations ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlist.Items.Add(dr["ExamName"].ToString());
                ddlist.Items.FindByText(dr["ExamName"].ToString()).Value = dr["ExamCode"].ToString();


            }

            con.Close();


        }

        public static void populateBatch(DropDownList ddlist)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SessionID,Session from DDESession order by SessionID DESC", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlist.Items.Add(dr["Session"].ToString());
                ddlist.Items.FindByText(dr["Session"].ToString()).Value = dr["SessionID"].ToString();

            }

            con.Close();

        }

        public static void populateBatchByAdmissionOpen(DropDownList ddlist)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SessionID,Session from DDESession where AdmissionOpen='True' order by SessionID", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlist.Items.Add(dr["Session"].ToString());
                ddlist.Items.FindByText(dr["Session"].ToString()).Value = dr["SessionID"].ToString();

            }

            con.Close();

        }

        public static void populateStudyCentre(DropDownList ddlist)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCCode from DDEStudyCentres where Mode='True' order by SCCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0].ToString() != "")
                {
                    ddlist.Items.Add(dr[0].ToString());
                }
               
            }
            con.Close();
        }

        public static void populateStudyCentre1(DropDownList ddlist)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCID,SCCode from DDEStudyCentres where Mode='True' order by SCCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[1].ToString() != "")
                {
                    ddlist.Items.Add(dr[1].ToString());
                    ddlist.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }
            }
            con.Close();
        }


        public static void populateExamCentre(DropDownList ddlist)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct City from CityList order by City", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlist.Items.Add(dr[0].ToString());

            }
            con.Close();
        }

        public static void populateSySession(DropDownList ddlistSySession)
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

        

        public static void populateCity(DropDownList ddlistCity, string state)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select City from CityList where State='"+state+"' order by City", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistCity.Items.Add(dr["City"].ToString());
            }

            con.Close();
        }

        public static void populateAccountSession(DropDownList ddlistASession)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistASession.Items.Add(dr["AcountSession"].ToString());

            }

            con.Close();
        }

        public static void populateSTFeeHead(DropDownList ddlistFeeHead)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select FHID,FeeHead from DDEFeeHead where FeePayer='STUDENT' ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistFeeHead.Items.Add(dr["FeeHead"].ToString());
                ddlistFeeHead.Items.FindByText(dr["FeeHead"].ToString()).Value = dr["FHID"].ToString();
            }

            ddlistFeeHead.Items.Add("ANY OTHER FEE");
            ddlistFeeHead.Items.FindByText("ANY OTHER FEE").Value = "23";

            con.Close();
        }

        public static void populateSCFeeHead(DropDownList ddlistFeeHead)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select FHID,FeeHead from DDEFeeHead where FeePayer='STUDY CENTRE' and FHID!='31'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistFeeHead.Items.Add(dr["FeeHead"].ToString());
                ddlistFeeHead.Items.FindByText(dr["FeeHead"].ToString()).Value = dr["FHID"].ToString();
            }

            con.Close();
        }



        public static void populateStudyCentreByAT(DropDownList ddlistStudyCentre, string at)
        {
            string rb = "";
            if (at=="1")
            {
                rb = "UNIVERSITY";
            }

            else if (at == "2")
            {
                rb = "WEM";
            }
            ddlistStudyCentre.Items.Clear();
            ddlistStudyCentre.Items.Add("--SELECT ONE--");
            ddlistStudyCentre.Items.FindByText("--SELECT ONE--").Value = "0";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SCID,SCCode from DDEStudyCentres where RecommendedBy='" + rb + "' and Mode='True' and Authorised='True' order by SCCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[1].ToString() != "")
                {
                    ddlistStudyCentre.Items.Add(dr[1].ToString());
                }
                
            }
            con.Close();
        }

       
        public static void populateEvents(DropDownList ddlistEvent)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct EventType from DDELog", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistEvent.Items.Add(dr["EventType"].ToString());
               
            }

            con.Close();
        }

        public static void populateDDEEmpFromLog(DropDownList ddlistEmployee)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select ERID from SVSUEmployeeRecord where Department='41'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string emp = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr[0]));
                ddlistEmployee.Items.Add(emp);
                ddlistEmployee.Items.FindByText(emp).Value = dr[0].ToString();

            }

            con.Close();
        }

        public static void populateMBACourses(DropDownList ddlistCourse)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse where CourseShortName='MBA' and CourseID!='76' and Online='True' order by CourseShortName", con);
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

        public static void populateCoursesBySCStreamsandAT(DropDownList ddlistCourse,int aType, int sccode)
        {
            string[] streams = FindInfo.findSCAllotedStreams(sccode).Split(',');

            ddlistCourse.Items.Clear();

            ddlistCourse.Items.Add("--SELECT ONE--");
           

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse where Online='True' order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (sccode == 2)
                {                   
                        if (dr[2].ToString() == "")
                        {
                            if (aType == 1)
                            {
                                if (!(FindInfo.isMBASpecialazation(Convert.ToInt32(dr[0]))))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString());
                                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                                }
                            }
                            else if (aType == 2 || aType == 3)
                            {
                                if (!(Convert.ToInt32(dr[0]) == 76))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString());
                                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                                }
                            }
                        }

                        else
                        {
                            if (aType == 1)
                            {
                                if (!(FindInfo.isMBASpecialazation(Convert.ToInt32(dr[0]))))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();
                                }
                            }
                            else if (aType == 2 || aType == 3)
                            {
                                if (!(Convert.ToInt32(dr[0]) == 76))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();
                                }

                            }

                        }
                   
                }
                else
                {
                    int pos = Array.IndexOf(streams, dr["CourseID"].ToString());
                    if (pos > -1)
                    {
                        if (dr[2].ToString() == "")
                        {
                            if (aType == 1)
                            {
                                if (!(FindInfo.isMBASpecialazation(Convert.ToInt32(dr[0]))))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString());
                                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                                }

                            }
                            else if (aType == 2 || aType == 3)
                            {
                                if (!(Convert.ToInt32(dr[0]) == 76))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString());
                                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                                }

                            }
                        }

                        else
                        {
                            if (aType == 1)
                            {
                                if (!(FindInfo.isMBASpecialazation(Convert.ToInt32(dr[0]))))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();
                                }

                            }
                            else if (aType == 2 || aType == 3)
                            {
                                if (!(Convert.ToInt32(dr[0]) == 76))
                                {
                                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();
                                }

                            }

                        }
                    }
                }

            }
            con.Close();
        }
        public static void populateCoursesBySCStreams(DropDownList ddlistCourse, int sccode)
        {
            string[] streams = FindInfo.findSCAllotedStreams(sccode).Split(',');

            ddlistCourse.Items.Clear();

            ddlistCourse.Items.Add("--SELECT ONE--");


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse where Online='True' order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (sccode == 2)
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
                else
                {
                    int pos = Array.IndexOf(streams, dr["CourseID"].ToString());
                    if (pos > -1)
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
                }

            }
            con.Close();
        }
        public static void populateAllPaperCode(DropDownList ddlistPaperCode)
        {
            throw new NotImplementedException();
        }

        public static void populateExamCentreByExam(DropDownList ddlistEC, string exam)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ECID,City,ExamCentreCode,CentreName from DDEExaminationCentres_"+exam+" order by ECID", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistEC.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString());
                ddlistEC.Items.FindByText("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString()).Value = dr[0].ToString();

               

            }
            con.Close();
        }

        public static void populateProStudyCentre(DropDownList ddlist)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ProSCCode from DDEStudyCentres where Mode='False' order by ProSCCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0].ToString() != "")
                {
                    ddlist.Items.Add(dr[0].ToString());
                }

            }
            con.Close();
        }


        public static void populateSameInsByIno(string ino, string pm, DropDownList ddlist)
        {
            ddlist.Items.Clear();
            ddlist.Items.Add("--SELECT ONE--");     
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select IID,INo,IBN,IDate from DDEFeeInstruments where INo='"+ino+"' and IType='"+pm+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlist.Items.Add(dr[1].ToString() + "-" + dr[2].ToString() + "-" + dr[3].ToString());
                    ddlist.Items.FindByText(dr[1].ToString() + "-" + dr[2].ToString() + "-" + dr[3].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateStreamNames(DropDownList ddlistStream)
        {
            ddlistStream.Items.Clear();
            ddlistStream.Items.Add("--SELECT ONE--");  
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select StreamID,StreamName from DDEStreams", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistStream.Items.Add(dr[1].ToString());
                    ddlistStream.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populatePracticalCodeForAS(DropDownList ddlistPracCode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalID,PracticalCode from DDEPractical where AllowedForAS='True' order by PracticalCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                  
                    ddlistPracCode.Items.Add(dr[1].ToString());                
                    ddlistPracCode.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populatePracticalCodeForALL(DropDownList ddlistPracCode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PracticalID,PracticalCode from DDEPractical order by PracticalCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistPracCode.Items.Add(dr[1].ToString());
                    ddlistPracCode.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateSLMGroups(DropDownList ddlistGroup)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select GID,GroupName from DDEGroups order by GroupName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistGroup.Items.Add(dr[1].ToString());
                    ddlistGroup.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateState(DropDownList ddlistState)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct State from CityList order by State", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistState.Items.Add(dr["State"].ToString());
            }

            con.Close();
        }

       

        public static void populateAllSLMCodes(DropDownList ddlistSLMCode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SLMID,SLMCode from DDESLMMaster order by SLMCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistSLMCode.Items.Add(dr[1].ToString());
                    ddlistSLMCode.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateSLMPublications(DropDownList ddlistParty)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PublicationID,PublicationName from DDESLMPublications order by PublicationName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistParty.Items.Add(dr[1].ToString());
                    ddlistParty.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateDispatchParties(DropDownList ddlistParty)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select DPID,PartyName from DDEDispatchParty order by PartyName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistParty.Items.Add(dr[1].ToString());
                    ddlistParty.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateStudyCentreForSLMletters(DropDownList ddlistSCCode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct SCCode from DDESLMLetters order by SCCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistSCCode.Items.Add(dr[0].ToString());
                   

                }
            }
            con.Close();
        }

        public static void populateSLMCodes(DropDownList ddlistSLM)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SLMID,SLMCode from DDESLMMaster order by SLMCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistSLM.Items.Add(dr[1].ToString());
                    ddlistSLM.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateSLMSaleParty(DropDownList ddlistParty)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SPartyID,SPartyName from DDESLMSaleParty order by SPartyID", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistParty.Items.Add(dr[1].ToString());
                    ddlistParty.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateDParty(DropDownList ddlistDParty)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select DPID,PartyName from DDEDispatchParty order by DPID", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    ddlistDParty.Items.Add(dr[1].ToString());
                    ddlistDParty.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

                }
            }
            con.Close();
        }

        public static void populateExaminers(DropDownList ddlistExaminers, string exam)
        {
            if (exam == "A17" || exam == "B17" || exam == "A18" || exam == "B18" || exam == "W10" || exam == "Z10")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select ExID,Name,Type from DDEExaminers where " + exam + "='True' order by Name", con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    ddlistExaminers.Items.Add(new ListItem("--SELECT ONE--", "1000"));

                    while (dr.Read())
                    {
                        ddlistExaminers.Items.Add(new ListItem(dr[1].ToString() + "(" + dr[2].ToString().Substring(0, 1) + ")", dr[0].ToString()));
                    }
                }
                con.Close();
                ddlistExaminers.Items.Add(new ListItem("NF", "0"));
            }
            else
            {
                ddlistExaminers.Items.Add(new ListItem("NF","0"));
            }
        }
    }
}
