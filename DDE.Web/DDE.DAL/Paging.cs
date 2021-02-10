using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DDE.DAL
{
   public class Paging
    {

        public static DataTable SelectStudentsByCB(string sp, DataTable colunms, int start,string course,string batch, out int totalrecords, out int noOfPages)
        {
            try
            {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlParameter[] par = new SqlParameter[5];
                    par[0] = new SqlParameter("@Start", start);
                    par[1] = new SqlParameter("@Course", course);
                    par[2] = new SqlParameter("@Batch", batch);
                    par[3] = new SqlParameter("@TotalRecords", totalrecords = 0);
                    par[3].Direction = ParameterDirection.Output;
                    par[4] = new SqlParameter("@NoOfPage", noOfPages = 0);
                    par[4].Direction = ParameterDirection.Output;
                   

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.Add(par[0]);
                    cmd.Parameters.Add(par[1]);
                    cmd.Parameters.Add(par[2]);
                    cmd.Parameters.Add(par[3]);
                    cmd.Parameters.Add(par[4]);
                    cmd.CommandText = sp;
                    cmd.CommandType = CommandType.StoredProcedure;

                    int totcol=colunms.Rows.Count;
                    DataTable dt = new DataTable();
                    DataColumn SNo = new DataColumn("SNo");
                    dt.Columns.Add(SNo);


                    for (int i = 0; i < totcol; i++)
                    {
                        DataColumn col = new DataColumn(colunms.Rows[0]["ColName"].ToString());
                        dt.Columns.Add(col);
                    }

                    

                    SqlDataReader dr;
                    cmd.Connection = con;
                    con.Open();
                    dr = cmd.ExecuteReader();

                    int j = 1;  
                    while (dr.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = j;
                        for (int k = 0; k <totcol; k++)
                        {

                            drow[colunms.Rows[k]["ColName"].ToString()] = dr[colunms.Rows[k]["ColName"].ToString()].ToString(); ;
                            dt.Rows.Add(drow);
                            
                        }
                        
                        j = j + 1;
                        
                    }
                    con.Close();
                    totalrecords = Convert.ToInt32(par[3].Value);
                    noOfPages = Convert.ToInt32(par[4].Value);
                    return dt;
             
            }
            catch (Exception)
            {
                throw;

            }

        }

        public static DataTable SelectStudentsBySCB(string sp, DataTable colunms, int start, string sccode, string course, string batch, out int totalrecords, out int noOfPages)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlParameter[] par = new SqlParameter[6];
                par[0] = new SqlParameter("@Start", start);
                par[1] = new SqlParameter("@SCCode", sccode);
                par[2] = new SqlParameter("@Course", course);
                par[3] = new SqlParameter("@Batch", batch);
                par[4] = new SqlParameter("@TotalRecords", totalrecords = 0);
                par[4].Direction = ParameterDirection.Output;
                par[5] = new SqlParameter("@NoOfPage", noOfPages = 0);
                par[5].Direction = ParameterDirection.Output;


                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(par[0]);
                cmd.Parameters.Add(par[1]);
                cmd.Parameters.Add(par[2]);
                cmd.Parameters.Add(par[3]);
                cmd.Parameters.Add(par[4]);
                cmd.Parameters.Add(par[5]);
                cmd.CommandText = sp;
                cmd.CommandType = CommandType.StoredProcedure;

                int totcol = colunms.Rows.Count;
                DataTable dt = new DataTable();
                DataColumn SNo = new DataColumn("SNo");
                dt.Columns.Add(SNo);


                for (int i = 0; i < totcol; i++)
                {
                    DataColumn col = new DataColumn(colunms.Rows[0]["ColName"].ToString());
                    dt.Columns.Add(col);
                }


                SqlDataReader dr;
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();

                int j = 1;
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = j;
                    for (int k = 0; k < totcol; k++)
                    {

                        drow[colunms.Rows[k]["ColName"].ToString()] = dr[colunms.Rows[k]["ColName"].ToString()].ToString(); ;
                        dt.Rows.Add(drow);

                    }

                    j = j + 1;

                }
                con.Close();
                totalrecords = Convert.ToInt32(par[4].Value);
                noOfPages = Convert.ToInt32(par[5].Value);
                return dt;

            }
            catch (Exception)
            {
                throw;

            }

        }
    }
}
