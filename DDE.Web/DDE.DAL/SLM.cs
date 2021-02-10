using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.DAL
{
    public class SLM
    {
        public static int makeSLMTransaction(int slmid,int billid,int lid,int slid,string description,int credit,int debit, out string error)
        {
            object updated = 0;
            error = "";
            try
            {
               
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDESLMStockRegister values(@SLMID,@BillID,@LID,@SLID,@Description,@TOT,@Rate,@Credit,@Debit,@CurrentTotal)", con);

                cmd.Parameters.AddWithValue("@SLMID", slmid);
                cmd.Parameters.AddWithValue("@BillID",billid);
                cmd.Parameters.AddWithValue("@LID", lid);
                cmd.Parameters.AddWithValue("@SLID", slid);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@TOT", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Rate", 0.00);
                cmd.Parameters.AddWithValue("@Credit", credit);
                cmd.Parameters.AddWithValue("@Debit", debit);
               
                int ct = findCurrentStock(slmid);
                int ft = 0;
                if (credit != 0)
                {
                    ft = ct + credit;
                }
                else if (debit != 0)
                {
                    ft = ct - debit;
                }

                cmd.Parameters.AddWithValue("@CurrentTotal", ft);

                cmd.Connection = con;
                con.Open();
                updated= cmd.ExecuteNonQuery();
                con.Close();

               int upd= updateSLMStock(slmid, ft);
               if (upd != 1)
               {
                   error = "Sorry !! Master SLM Stock is not updated!!";
               }
            }
            catch (Exception ex)
            {
                updated = 0;
                error = ex.Message;
            }

            return Convert.ToInt32(updated);
        }

        private static int updateSLMStock(int slmid, int ft)
        {
            int updated = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDESLMMaster set PresentStock=@PresentStock where SLMID='" + slmid + "' ", con);

            cmd.Parameters.AddWithValue("@PresentStock", ft);

            con.Open();
            updated= cmd.ExecuteNonQuery();
            con.Close();
            return updated;
        }

        public static int findCurrentStock(int slmid)
        {
            int cs = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PresentStock from DDESLMMaster where SLMID='" + slmid+ "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                cs = Convert.ToInt32(dr[0]);
            }

            con.Close();

            return cs;
        }

        public static bool isSLMEnterdonBill(int slmid, string billno,string date,out int quantity)
        {
            quantity = 0;
            bool entered = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Credit from DDESLMStockRegister where SLMID='" + slmid + "' and BillNo='"+billno+"' and Date='"+date+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();
                quantity = Convert.ToInt32(dr[0]);
                entered = true;

            }

            con.Close();

            return entered;

        }



        public static int generateLetterForIssuingSLM(string slmrids, string sccode)
        {
            object lid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESLMLetters OUTPUT INSERTED.LID values(@SCCode,@SLMRIDS,@LetterPublishedOn,@LetterProcessedOn,@DispatchDate,@TotalPktWeight,@TotalDispatchCharge,@DType,@DPID,@DocketNo)", con);


            cmd.Parameters.AddWithValue("@SCCode", sccode);
            cmd.Parameters.AddWithValue("@SLMRIDS", slmrids);
            cmd.Parameters.AddWithValue("@LetterPublishedOn", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@LetterProcessedOn", "");
            cmd.Parameters.AddWithValue("@DispatchDate", "");
            cmd.Parameters.AddWithValue("@TotalPktWeight", 0);
            cmd.Parameters.AddWithValue("@TotalDispatchCharge", 0);
            cmd.Parameters.AddWithValue("@DType", 0);
            cmd.Parameters.AddWithValue("@DPID", 0);
            cmd.Parameters.AddWithValue("@DocketNo", "");
            
    

            cmd.Connection = con;
            con.Open();
            lid = cmd.ExecuteScalar();    
            con.Close();

            return Convert.ToInt32(lid);
        }

        public static int updateLNoToSLMRIDS(int lid, string slmrids)
        {
            int updated = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDESLMIssueRecord set LNo=@LNo where SLMRID in (" + slmrids + ")", con);


          
            cmd.Parameters.AddWithValue("@LNo",lid);
          

            con.Open();
            updated = cmd.ExecuteNonQuery();
            con.Close();
            return updated;
        }

        public static bool isSLMLinkingRecordExist(int slmid, int cid, int year)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMLinking where SLMID='" + slmid + "' and CID='" + cid + "' and Year='" +year + "'", con);
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

        public static bool slmLetterExist(int lid)
        {

            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select LID from DDESLMLetters where LID='" + lid + "'", con);
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

        public static string findDateOnLetter(int lid)
        {
            string date = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select LetterPublishedOn from DDESLMLetters where LID='" + lid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                date = Convert.ToDateTime(dr["LetterPublishedOn"].ToString()).ToString("dd-MM-yyyy");
            }

            con.Close();

            return date;
        }

        public static string[] findSLMLetterDetails(int lid)
        {
            string[] sld = {"","","","","","","","","",""};

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMLetters where LID='" + lid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sld[0] = dr["SCCode"].ToString();
                sld[1] = dr["SLMRIDS"].ToString();
                sld[2] = Convert.ToDateTime(dr["LetterPublishedOn"].ToString()).ToString("dd-MM-yyyy");
                sld[3] = Convert.ToDateTime(dr["LetterProcessedOn"].ToString()).ToString("dd-MM-yyyy");
                if (dr["DispatchDate"].ToString() == "")
                {
                    sld[4] = "";
                }
                else
                {
                    sld[4] = Convert.ToDateTime(dr["DispatchDate"].ToString()).ToString("dd-MM-yyyy");
                }
                sld[5] = dr["TotalPktWeight"].ToString();
                sld[6] = dr["TotalDispatchCharge"].ToString();
                sld[7] = dr["DType"].ToString();
                sld[8] = dr["DPID"].ToString();
                sld[9] = dr["DocketNo"].ToString();
               
            }

            con.Close();

            return sld;
        }

        public static bool isSLMLetterGenerated(string slmrids, out int lno)
        {
            bool exist = false;
            lno = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select LNo from DDESLMIssueRecord where SLMRID in (" + slmrids + ")", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32((ds.Tables[0].Rows[i]["LNo"])) > 0)
                    {
                        exist = true;
                        lno = Convert.ToInt32((ds.Tables[0].Rows[i]["LNo"]));
                        break;
                    }
                    

                }
            }

            return exist;
        }

        public static string findTitleBySLMID(int slmid)
        {
            string title = "NF";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Title from DDESLMMaster where SLMID='" + slmid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                title = dr[0].ToString();
            }

            con.Close();

            return title;
        }

        public static int enterBillDetails(string billno, string billdate, string orderno, string orderdate, string challanno, string challandate, int partyid, double totalamount, double discount, double postalcharge, double netamount, int erid)
        {
            object lid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESLMBills OUTPUT INSERTED.BillID values(@BillNo,@BillDate,@ChallanNo,@ChallanDate,@OrderNo,@OrderDate,@PartyID,@TotalPaybleAmount,@Discount,@PostalCharge,@NetPaybleAmount,@ReceivedBy,@ReceivedOn,@Verified,@VerifiedBy,@VerifiedOn)", con);


            cmd.Parameters.AddWithValue("@BillNo", billno);
            cmd.Parameters.AddWithValue("@BillDate", billdate);
            cmd.Parameters.AddWithValue("@ChallanNo", challanno);
            cmd.Parameters.AddWithValue("@ChallanDate", challandate);
            cmd.Parameters.AddWithValue("@OrderNo", orderno);
            cmd.Parameters.AddWithValue("@OrderDate", orderdate);
            cmd.Parameters.AddWithValue("@PartyID", partyid);
            cmd.Parameters.AddWithValue("@TotalPaybleAmount", totalamount);
            cmd.Parameters.AddWithValue("@Discount",discount);
            cmd.Parameters.AddWithValue("@PostalCharge", postalcharge);
            cmd.Parameters.AddWithValue("@NetPaybleAmount",netamount);
            cmd.Parameters.AddWithValue("@ReceivedBy",erid );
            cmd.Parameters.AddWithValue("@ReceivedOn", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@Verified", "False");
            cmd.Parameters.AddWithValue("@VerifiedBy", 0);
            cmd.Parameters.AddWithValue("@VerifiedOn", "");

            cmd.Connection = con;
            con.Open();
            lid = cmd.ExecuteScalar();
            con.Close();

            return Convert.ToInt32(lid);
        }

        public static string [] findBillDetailsByBillID(int billid)
        {
            string[] bd = { ""};

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMBills where BillID='" + billid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                bd[0] = dr["BillNo"].ToString();
              
            }

            con.Close();

            return bd;
        }

        public static int findTotalSLMByLID(int lid)
        {
            int totalslm = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Debit from DDESLMStockRegister where LID='"+lid+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    totalslm = totalslm + Convert.ToInt32(ds.Tables[0].Rows[i]["Debit"]);
                }
            }
            return totalslm;
            
        }

        public static string[] findSLMDetails(int slmid)
        {
            string[] sld = { "", "", "", "", "", "", "", };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMMaster where SLMID='" +slmid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sld[0] = dr["SLMCode"].ToString();
                sld[1] = dr["Dual"].ToString();
                sld[2] = dr["GroupID"].ToString();
                sld[3] = dr["Lang"].ToString();
                sld[4] = dr["Title"].ToString();
                sld[5] = dr["Cost"].ToString();
                sld[6] = dr["PresentStock"].ToString();
            
              

            }

            con.Close();

            return sld;
        }

        public static string[] findSLMSaleLetterDetails(int slid)
        {
            string[] sld = { "", "",};

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMSaleLetters where SLID='" + slid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                sld[0] = dr["SPartyID"].ToString();          
                sld[1] = Convert.ToDateTime(dr["LetterPublishedOn"].ToString()).ToString("dd-MM-yyyy");
               

            }

            con.Close();

            return sld;
        }

        public static int generateLetterForSaleSLM(int partyid)
        {
            object slid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESLMSaleLetters OUTPUT INSERTED.SLID values(@PartyID,@LetterPublishedOn)", con);

            cmd.Parameters.AddWithValue("@PartyID", partyid);      
            cmd.Parameters.AddWithValue("@LetterPublishedOn", DateTime.Now.ToString());
          

            cmd.Connection = con;
            con.Open();
            slid = cmd.ExecuteScalar();
            con.Close();

            return Convert.ToInt32(slid);
        }

        public static bool slmSaleLetterExist(int slid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SLID from DDESLMSaleLetters where SLID='" + slid + "'", con);
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

        public static string findSLMSalePartyDetailsByID(int pid)
        {
            string party = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMSaleParty where SPartyID='" + pid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                party =dr["SPartyName"].ToString()+"<br/>"+ dr["Address"].ToString();
                
            }

            con.Close();

            return party;
        }
    }
}
