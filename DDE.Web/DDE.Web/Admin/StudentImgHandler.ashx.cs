using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{
    /// <summary>
    /// Summary description for StudentImgHandler
    /// </summary>
    public class StudentImgHandler : IHttpHandler
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());


        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["PSRID"] != null)
            {

                SqlCommand cmd = new SqlCommand("select StudentPhoto from DDEPendingStudentRecord where PSRID='" + context.Request.QueryString["PSRID"] + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);




                if (ds.Tables[0].Rows.Count > 0)
                {
                    context.Response.BinaryWrite((byte[])(ds.Tables[0].Rows[0]["StudentPhoto"]));
                }
            }
            else if (context.Request.QueryString["SRID"] != null)
            {

                SqlCommand cmd = new SqlCommand("select StudentPhoto from DDEStudentRecord where SRID='" + context.Request.QueryString["SRID"] + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);




                if ((ds.Tables[0].Rows.Count > 0) && ((ds.Tables[0].Rows[0]["StudentPhoto"]).ToString()!=""))
                {
                    context.Response.BinaryWrite((byte[])(ds.Tables[0].Rows[0]["StudentPhoto"]));
                }
            }


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}