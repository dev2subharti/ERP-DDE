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

namespace DDE.Web.Admin
{
    public partial class PayAmountBySC : System.Web.UI.Page
    {
        COM.TPSLUtil1 objTPSLUtil1 = new COM.TPSLUtil1();
        COM.CheckSumRequestBean objCheckSumRequestBean = new COM.CheckSumRequestBean();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginType"].ToString() == "16")
            {
                if (!IsPostBack)
                {
                    lblTP.Text = Session["TransactionAmount"].ToString();

                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }


        }

       
        protected void btn_Request_Click(object sender, EventArgs e)
        {
            try
            {
               
                int tid=Convert.ToInt32(Session["TID"]);
                objCheckSumRequestBean.MerchantTranId = string.Format("{0:00000000}",tid);
                objCheckSumRequestBean.MarketCode = "L2504";
                objCheckSumRequestBean.AccountNo = "1";
                objCheckSumRequestBean.Amt = lblTP.Text;
                objCheckSumRequestBean.BankCode = "NA";
                objCheckSumRequestBean.PropertyPath = "e:\\ddeerp\\30-03-2012\\OnlineProperty\\MerchantDetails.property";


                //objCheckSumRequestBean.CustomerId = txtcustomerid.Text;

                string strMsg = objTPSLUtil1.transactionRequestMessage(objCheckSumRequestBean);
                //Response.Write(strMsg);


                //HtmlInputHidden msg = new HtmlInputHidden();
                //msg.ID = "msg";
                //msg.Value = strMsg;
                ////cph_TPSL.Controls.Add(msg);
                //Session["myString"] = msg.Value.ToString();   

                Session["myString"] = strMsg;


                //HtmlInputHidden msg = new HtmlInputHidden();
                //msg.ID = "msg";
                //msg.Value = strMsg;
                //form1.Controls.Add(msg);
                //Session["myString"] = msg.Value.ToString();   


                if (!strMsg.Equals(""))
                {

                    Response.Redirect("https://www.tpsl-india.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg);
                    //Response.Redirect("https://www.tpsl-india.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnPBChalan_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChalanForm.aspx");
        }
   
    }
}
