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
    /// Summary description for Question
    /// </summary>
    public class Question : IHttpHandler
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());


        public void ProcessRequest(HttpContext context)
        {


            SqlCommand cmd = new SqlCommand("select QImage from QuestionBank where QID='" + context.Request.QueryString["QID"] + "'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);




            if (ds.Tables[0].Rows.Count > 0)
            {
                context.Response.BinaryWrite((byte[])(ds.Tables[0].Rows[0]["QImage"]));
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