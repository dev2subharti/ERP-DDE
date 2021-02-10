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

namespace DDE.Web
{
    public partial class FillIssueSLM_Record : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFISR_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where [Session]='C 2016' and StudyCentreCode='084' and RecordStatus='True' order by SRID", con);
            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            int j = 0;
            int k = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
                int cid = 0;

                if (FindInfo.isMBACourse(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"])))
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 1)
                    {
                        cid = 76;
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 2)
                    {
                        cid = Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]);
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 3)
                    {
                        cid = Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]);
                    }
                    
                }
                else
                {
                    cid = Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]);
                }

                int updated = 0;
                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd2 = new SqlCommand("update [DDEFeeRecord_2016-17] set EntryType=@EntryType where SRID='" + srid + "' and EntryType='2' ", con2);


                cmd2.Parameters.AddWithValue("@EntryType", 1);


                con2.Open();
                updated = cmd2.ExecuteNonQuery();
                con2.Close();

                if (updated == 1)
                {
                    k = k + 1;
                }
                    

                if (!(FindInfo.isRegisteredForSLM(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]))))
                {
                   

                    int res = 0;
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("insert into DDESLMIssueRecord values(@SRID,@SCCode,@CID,@Year,@TOR,@LNo)", con1);

                    cmd1.Parameters.AddWithValue("@SRID", srid);
                    cmd1.Parameters.AddWithValue("@SCCode", "084");
                    cmd1.Parameters.AddWithValue("@CID", cid);
                    cmd1.Parameters.AddWithValue("@Year", Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                    cmd1.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());
                    cmd1.Parameters.AddWithValue("@LNo", 0);


                    con1.Open();
                    res = cmd1.ExecuteNonQuery();
                    con1.Close();

                    if (res == 1)
                    {
                        j = j + 1;
                    }

                   
                }
            
               
            }

            Response.Write("Total Entry Type updated : " + k.ToString()+" Total SLM Issue Record filled : "+j.ToString());
        }

        protected void btnCorrect_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb1"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMPackets order by PID", con);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            int j = 0;
        
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                int res = 0;
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("update DDESLMPackets set LID=@LID where PID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["PID"]) + "'", con1);

                cmd1.Parameters.AddWithValue("@LID", Convert.ToInt32(ds.Tables[0].Rows[i]["LID"]));
              


                con1.Open();
                res = cmd1.ExecuteNonQuery();
                con1.Close();

                if (res == 1)
                {
                    j = j + 1;
                }
            }
        

            Response.Write("Total Record updated : " + j.ToString());
        }
    }
}