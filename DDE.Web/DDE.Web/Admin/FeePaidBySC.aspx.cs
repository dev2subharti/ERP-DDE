using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DDE.DAL;
using COM;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class FeePaidBySC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {

                    COM.CheckSumResponseBean objCheckSumResponseBean = new COM.CheckSumResponseBean();
                    TPSLUtil1 objTPSLUtil1 = new TPSLUtil1();

                    Response.Write("Originally Generated String===========>" + Convert.ToString(Session["myString"]) + "End String===========>");
                    String strResponseMsg = Request["msg"] == null ? "" : Request["msg"].Trim();

                    Response.Write("strResponseMsg===========>" + strResponseMsg);

                    String[] token = strResponseMsg.Split('|');

                    if (token.Length == 26)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDETransactionRecord set MerchantID=@MerchantID,SubscriberID=@SubscriberID,TransactionRefNo=@TransactionRefNo,BankRefNo=@BankRefNo,TransactionAmount=@TransactionAmount,BankID=@BankID,BankMerchantID=@BankMerchantID,TxnType=@TxnType,CurrencyName=@CurrencyName,SecurityType=@SecurityType,SecurityID=@SecurityID,SecurityPassword=@SecurityPassword,TxnDate=@TxnDate,AuthStatus=@AuthStatus,SettlementType=@SettlementType,AdditionalInfo1=@AdditionalInfo1,AdditionalInfo2=@AdditionalInfo2,AdditionalInfo3=@AdditionalInfo3,AdditionalInfo4=@AdditionalInfo4,AdditionalInfo5=@AdditionalInfo5,AdditionalInfo6=@AdditionalInfo6,AdditionalInfo7=@AdditionalInfo7,ErrorStatus=@ErrorStatus,ErrorDescription=@ErrorDescription,CheckSum=@CheckSum where TID='" + Convert.ToInt32(Session["TID"]) + "'", con);

                        cmd.Parameters.AddWithValue("@MerchantID", token[0].ToString());
                        cmd.Parameters.AddWithValue("@SubscriberID", token[1].ToString());
                        cmd.Parameters.AddWithValue("@TransactionRefNo", token[2].ToString());
                        cmd.Parameters.AddWithValue("@BankRefNo", token[3].ToString());
                        cmd.Parameters.AddWithValue("@TransactionAmount", token[4].ToString());
                        cmd.Parameters.AddWithValue("@BankID", token[5].ToString());
                        cmd.Parameters.AddWithValue("@BankMerchantID", token[6].ToString());
                        cmd.Parameters.AddWithValue("@TxnType", token[7].ToString());
                        cmd.Parameters.AddWithValue("@CurrencyName", token[8].ToString());
                        cmd.Parameters.AddWithValue("@ItemCode", token[9].ToString());
                        cmd.Parameters.AddWithValue("@SecurityType", token[10].ToString());
                        cmd.Parameters.AddWithValue("@SecurityID", token[11].ToString());
                        cmd.Parameters.AddWithValue("@SecurityPassword", token[12].ToString());
                        cmd.Parameters.AddWithValue("@TxnDate",Convert.ToDateTime(token[13]));
                        cmd.Parameters.AddWithValue("@AuthStatus", token[14].ToString());
                        cmd.Parameters.AddWithValue("@SettlementType", token[15].ToString());
                        cmd.Parameters.AddWithValue("@AdditionalInfo1", token[16].ToString());
                        cmd.Parameters.AddWithValue("@AdditionalInfo2", token[17].ToString());
                        cmd.Parameters.AddWithValue("@AdditionalInfo3", token[18].ToString());
                        cmd.Parameters.AddWithValue("@AdditionalInfo4", token[19].ToString());
                        cmd.Parameters.AddWithValue("@AdditionalInfo5", token[20].ToString());
                        cmd.Parameters.AddWithValue("@AdditionalInfo6", token[21].ToString());
                        cmd.Parameters.AddWithValue("@AdditionalInfo7", token[22].ToString());
                        cmd.Parameters.AddWithValue("@ErrorStatus", token[23].ToString());
                        cmd.Parameters.AddWithValue("@ErrorDescription", token[24].ToString());
                        cmd.Parameters.AddWithValue("@CheckSum", token[25].ToString());

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        objCheckSumResponseBean.MSG = strResponseMsg;

                        objCheckSumResponseBean.PropertyPath = "C:\\ddeerp\\30-03-2012\\OnlineProperty\\MerchantDetails.property";

                        string strCheckSumValue = objTPSLUtil1.transactionResponseMessage(objCheckSumResponseBean);

                        Response.Write("strCheckSumValue***********" + strCheckSumValue);

                        if (!strCheckSumValue.Equals(""))
                        {
                            if (token[25].ToString() == strCheckSumValue)
                            {
                                if (token[14].ToString() == "0300")
                                {

                                    Accounts.setTransactionStatus(Convert.ToInt32(Session["TID"]), token[2].ToString(), token[5].ToString(), "True", "2012-13");
                                    lblMSG.Text = "<b>Your Payment Transaction has been completed successfully.</b></br></br>Your Transaction Reference No. is :" + token[2].ToString() + "</br></br> We Shall update your fee record as we receive this amount from your bank";
                                }
                                else
                                {
                                    Accounts.setTransactionStatus(Convert.ToInt32(Session["TID"]), token[2].ToString(), token[5].ToString(), "False", "2012-13");
                                    lblMSG.Text = "<b>Sorry!! Your Payment Transaction Failed.";

                                }
                            }
                            else
                            {
                                Accounts.setTransactionStatus(Convert.ToInt32(Session["TID"]), token[2].ToString(), token[5].ToString(), "False", "2012-13");
                                lblMSG.Text = "<b>Sorry!! Your Payment Transaction Failed Due to Checksum Mismatch.";
                            }
                        }
                        else
                        {
                            Accounts.setTransactionStatus(Convert.ToInt32(Session["TID"]), token[2].ToString(), token[5].ToString(), "False", "2012-13");
                            lblMSG.Text = "Transaction Failed due to invalid parameters";

                        }
                        
                   
                    }
                    else
                    {
                        Response.Write("Inside ELSE of Response***********");
                        return;
                    }

                    


                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }
        }

       
    }
}

