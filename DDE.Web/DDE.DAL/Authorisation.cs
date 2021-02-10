using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;

namespace DDE.DAL
{
   public class Authorisation
    {
        public static bool authorised(int erid, int Roleid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from AssignedRoles Where ERID='" + erid.ToString() + "' and AssignedRoleID='" + Roleid.ToString() + "' ", con);
            SqlDataReader dr;

            bool auth = false;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                auth = true;

            }

            con.Close();

            return auth;

        }

        public static bool isUser(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from Users where ERID='" + erid.ToString() + "'", con);
            SqlDataReader dr;

            bool auth = false;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                auth = true;
            }


            con.Close();

            return auth;

        }


        public static bool userCreatedByMe(int erid, int userid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from Users where ERID='" + userid + "'", con);
            SqlDataReader dr;

            bool auth = false;
            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            if(Convert.ToInt32(dr["CreatedBy"])==erid )
            {
                auth = true;
            }


            con.Close();

            return auth;
        }

        public static bool authorisedSCFor(string  sccode, int roleid)
        {
            return true;
        }
    }
}
