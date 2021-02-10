using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.DAL
{
    public class Log
    {
        public static void IncreaseLoggedIntimes(string st,int erid)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand command = new SqlCommand("Update SVSUEmployeeRecord set NoTimesLoggedIn=@NoTimesLoggedIn where ERID='" + erid + "'", connection);
            connection.Open();
            command.Parameters.AddWithValue("@NoTimesLoggedIn", Convert.ToString((Convert.ToInt32(st) + 1)));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void InsertLoginActivity(String EmployeeID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Cssvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("Insert into LoginActivity values(@LoginStatus,@AccountStatus,@ERID,@TotalSucessfullLoginAttempt,@TotalUnsucessfullLoginAttempt,@TodayUnsucessfullLoginAttempt,@TodaySucessfullLoginAttempt)", con);
            cmd.Parameters.AddWithValue("@LoginStatus",0);
            cmd.Parameters.AddWithValue("@AccountStatus",1);
            cmd.Parameters.AddWithValue("@ERID",getERID(EmployeeID));
            cmd.Parameters.AddWithValue("@TotalSucessfullLoginAttempt",0);
            cmd.Parameters.AddWithValue("@TotalUnsucessfullLoginAttempt",0);
            cmd.Parameters.AddWithValue("@TodayUnsucessfullLoginAttempt",0);
            cmd.Parameters.AddWithValue("@TodaySucessfullLoginAttempt",0);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private string getERID(string EmployeeID)
        {
            string erid="";

            SqlConnection conic = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select ERID from SVSUEmployeeRecord where EmployeeId='"+ EmployeeID +"'",conic);
            SqlDataReader dr;
            conic.Open();
            dr = scmd.ExecuteReader();
            while(dr.Read())
            {
              erid=dr[0].ToString();
          
            }
            conic.Close();

            return erid;
        }


        public static void  createLogNow(string EventType, string EventDescription, int ERID)
        {
            //String[] empinfo = EmpInfo(Convert.ToInt32(ERID));
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Insert into DDELog values(@EventType,@EventDescription,@EventDateTime,@ERID)", con);
            cmd.Parameters.AddWithValue("@EventType", EventType);
            cmd.Parameters.AddWithValue("@EventDescription", EventDescription);
            cmd.Parameters.AddWithValue("@EventDateTime", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@ERID", ERID);
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void LoginTimingsLogin(string erid)
        {
             SqlConnection console = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Insert into Logintiming Values(@ERID,@LoginTime,@LogoutTime,@TodayDate)", console);
            cmd1.Parameters.AddWithValue("@ERID", erid);
            cmd1.Parameters.AddWithValue("@LoginTime", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            cmd1.Parameters.AddWithValue("@LogoutTime", "");
            cmd1.Parameters.AddWithValue("@TodayDate", DateTime.Now.ToString("dd / MM / yyyy"));
            console.Open();
            cmd1.ExecuteNonQuery();
            console.Close();

        }

        public void LoginTimingsLogout(string erid)
        {
            string dt="";
            SqlConnection cst = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmcst = new SqlCommand("Select LoginTime from LoginTiming", cst);
            SqlDataReader sdr;
            cst.Open();
            sdr = cmcst.ExecuteReader();
            while (sdr.Read())
            {
                dt = sdr[0].ToString();


            }
            cst.Close();


            SqlConnection console = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Insert into LoginTiming Values(@ERID,@LoginTime,@LogoutTime,@TodayDate)", console);
            cmd1.Parameters.AddWithValue("@ERID", erid);
            cmd1.Parameters.AddWithValue("@LoginTime", dt);
            cmd1.Parameters.AddWithValue("@LogoutTime", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            cmd1.Parameters.AddWithValue("@TodayDate", DateTime.Now.ToString("dd / MM / yyyy"));
            console.Open();
            cmd1.ExecuteNonQuery();
            console.Close();

        }

        public String getLastLogoutTime(string erid)
        {
            //string dt = DateTime.Now.ToString("dd/MM/yyyy");
            //int date = Convert.ToInt32(dt.Substring(0,2));
            //int month = Convert.ToInt32(dt.Substring(3, 2));
            //int year = Convert.ToInt32(dt.Substring(6,4));
            string dt = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());


            SqlCommand cmd = new SqlCommand("Select LogoutTime,TodayDate from Logintiming where ID=(select MAX(ID) from Logintiming where ERID='"+erid+"')", con);


            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dt = Convert.ToString(dr[0]);

            }

            //dr.Read();
            
            //string lastlogouttime = dr[0].ToString();

            con.Close();
            //if (dt == "1/1/1900 12:00:00 AM")
            //{
            //    dt = "You haven't Logged out Properly Last Time";
            //}

           

            return dt.ToString();

        }

        public String getLastLogoutDate(int erid)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            string dt = DateTime.Now.ToString("dd/MM/yyyy");


            SqlCommand cmd = new SqlCommand("Select LogoutTime from Logintiming where ERID='" + erid + "' and TodayDate='" + dt + "'", con);

            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dt = Convert.ToString(dr[0]);

            }
            con.Close();
            return dt.ToString();
        }

        //public static void setLastLoginTime(int erid)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update SVSUEmployeeRecord set LastLogoutTime=@LastLogoutTime where ERID='" + erid + "'", con);
        //    cmd.Parameters.AddWithValue("@LastLogoutTime", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();


        //}


        public static void IncreaseLoggedIntimesofSC(string st, string sccode)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand command = new SqlCommand("Update DDEStudyCentres set NoTimesLoggedIn=@NoTimesLoggedIn where SCCode='" + sccode + "'", connection);
            connection.Open();
            command.Parameters.AddWithValue("@NoTimesLoggedIn", Convert.ToString((Convert.ToInt32(st) + 1)));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string getLastLogoutTimeSC(string sccode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select LastLogoutTime from DDEStudyCentres where SCCode='" + sccode + "'", con);

            SqlDataReader dr;

            string llt = "";
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                llt = Convert.ToString(dr[0]);

            }
            con.Close();
            return llt;
        }



        public static void setLastLogoutTime(string sccode)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand command = new SqlCommand("Update DDEStudyCentres set LastLogoutTime=@LastLogoutTime where SCCode='" + sccode + "'", connection);
            connection.Open();
            command.Parameters.AddWithValue("@LastLogoutTime", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss tt"));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void createErrorLog(string eventname, string errormsg, int ERID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Insert into DDEErrorLog values(@EventName,@ErrorMessage,@OperatorID,@TOE)", con);
            cmd.Parameters.AddWithValue("@EventName", eventname);
            cmd.Parameters.AddWithValue("@ErrorMessage", errormsg);
            cmd.Parameters.AddWithValue("@TOE", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@OperatorID", ERID);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}