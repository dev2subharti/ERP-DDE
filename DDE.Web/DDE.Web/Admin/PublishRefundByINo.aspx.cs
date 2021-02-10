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
using System.Data.SqlClient;
using System.IO;

namespace DDE.Web.Admin
{
    public partial class PublishRefundByINo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
            {
                populateData();
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

        private void populateData()
        {
            lblIID.Text = Session["IID"].ToString();
            tbINo.Text = Session["INo"].ToString();
            tbIType.Text = Session["IType"].ToString();
            tbIDate.Text = Session["IDate"].ToString();
            tbIBN.Text = Session["IBN"].ToString();
            tbTotalAmount.Text = Session["TotalAmount"].ToString();
            tbBatch.Text = Session["Batch"].ToString();
            tbSCCode.Text = Session["SCCode"].ToString();
            tbCourse.Text = Session["Course"].ToString();

            tbTotalRefund.Text=Session["TotalRefund"].ToString();
            tbBalanceExtra.Text = Session["Extra"].ToString();
            tbBalanceShort.Text = Session["Short"].ToString();
            tbNetRefund.Text = Session["NetRefund"].ToString();

            DataTable dt = (DataTable)Session["FinalList"];
            dt.DefaultView.Sort = "EnrollmentNo";
            DataView dv = dt.DefaultView;

            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }

            dtlistShowTransactions.DataSource = dt;
            dtlistShowTransactions.DataBind();

            
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
               string lno = "";
               if (!FindInfo.isInsAlreadyPublished(Convert.ToInt32(lblIID.Text), out lno))
               {

                   SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                   SqlCommand cmd = new SqlCommand("insert into DDERefundLetterRecord output Inserted.RLID values(@IID,@Batch,@SCCode,@Course,@RRIDS,@TotalRefund,@Extra,@Short,@NetRefund,@RGBy,@RGTime,@RP,@RIID,@RPBy,@RPTime)", con);

                   cmd.Parameters.AddWithValue("@IID", Convert.ToInt32(lblIID.Text));
                   cmd.Parameters.AddWithValue("@Batch", tbBatch.Text);
                   cmd.Parameters.AddWithValue("@SCCode", tbSCCode.Text);
                   cmd.Parameters.AddWithValue("@Course", tbCourse.Text);
                   cmd.Parameters.AddWithValue("@RRIDS", "");
                   cmd.Parameters.AddWithValue("@TotalRefund", Convert.ToInt32(tbTotalRefund.Text));
                   cmd.Parameters.AddWithValue("@Extra", Convert.ToInt32(tbBalanceExtra.Text));
                   cmd.Parameters.AddWithValue("@Short", Convert.ToInt32(tbBalanceShort.Text));
                   cmd.Parameters.AddWithValue("@NetRefund", Convert.ToInt32(tbNetRefund.Text));
                   cmd.Parameters.AddWithValue("@RGBy", Convert.ToInt32(Session["ERID"]));
                   cmd.Parameters.AddWithValue("@RGTime", DateTime.Now.ToString());
                   cmd.Parameters.AddWithValue("@RP", "False");
                   cmd.Parameters.AddWithValue("@RIID", 1);
                   cmd.Parameters.AddWithValue("@RPBy", 0);
                   cmd.Parameters.AddWithValue("@RPTime", "");


                   con.Open();
                   object rlno = cmd.ExecuteScalar();
                   con.Close();


                   string rrids = insertRefundRecord(Convert.ToInt32(rlno));

                   int res = updateRRIDS(rlno, rrids);
                   int rres = updateInsStatus(Convert.ToInt32(lblIID.Text));
                   if (res == 1 && rres == 1)
                   {
                       btnPrint.Visible = false;
                       hl.Visible = false;
                       lblLNo.Text = "Ref No. : DDE/Accounts/" + DateTime.Now.ToString("yyyy") + "/" + rlno;
                       lblDate.Text = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");
                       Log.createLogNow("Refund Generated", "Generated refund for '" + tbIType.Text + " with No. '" + tbINo.Text + "'", Convert.ToInt32(Session["ERID"]));
                       ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                   }
                   else
                   {
                       Log.createErrorLog("Publish Refund", "Sorry! Some error occured during execution of command.Please contact to ERP Developer.", Convert.ToInt32(Session["ERID"]));
                       pnlData.Visible = false;
                       lblMSG.Text = "Sorry! Some error occured during execution of command.Please contact to ERP Developer.";
                       pnlMSG.Visible = true;
                   }
               }
               else
               {
                   pnlData.Visible = false;
                   lblMSG.Text = "Sorry! Refund is already generated for this instrument.The Letter No. is : " + lno;
                   pnlMSG.Visible = true;
                  
               }
            }
            catch(Exception ex)
            {

                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }

            //DataTable dt = (DataTable)Session["FinalList"];

            //MemoryStream str = new MemoryStream();
            //dt.WriteXml(str, true);
            //str.Seek(0, SeekOrigin.Begin);
            //StreamReader sr = new StreamReader(str);
            //string xmlstr;
            //xmlstr = sr.ReadToEnd();

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlCommand cmd = new SqlCommand("insert into DDERefundLetters values(@List)", con);

            //cmd.Parameters.AddWithValue("@List", xmlstr);
          


            //con.Open();
            //cmd.ExecuteNonQuery();
            //con.Close();
        }

        private int updateInsStatus(int iid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEFeeInstruments set RG=@RG,Lock=@Lock where IID='" + iid + "' ", con);
            cmd.Parameters.AddWithValue("@RG", "True");
            cmd.Parameters.AddWithValue("@Lock", "True");
            con.Open();
            int r = cmd.ExecuteNonQuery();
            con.Close();

            return r;
        }

        private int updateRRIDS(object rlno, string rrids)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDERefundLetterRecord set RRIDS=@RRIDS where RLID='" + rlno + "' ", con);
            cmd.Parameters.AddWithValue("@RRIDS", rrids);

            con.Open();
            int r= cmd.ExecuteNonQuery();
            con.Close();

            return r;
        }

      

        private string insertRefundRecord(int rlno)
        {
            DataTable dt = (DataTable)Session["FinalList"];
            string rrids = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object sr = dt.Rows[0]["SRID"];

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDERefundRecord output Inserted.RRID values(@SRID,@Course,@Year,@SCCode,@RAmount,@Trans,@PAmount,@FeePer,@Refund,@LNo,@RG,@RP)", con);

                cmd.Parameters.AddWithValue("@SRID",Convert.ToInt32(dt.Rows[i]["SRID"]));
                cmd.Parameters.AddWithValue("@Course", Convert.ToInt32(dt.Rows[i]["CourseID"]));
                cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(dt.Rows[i]["Year"]));
                cmd.Parameters.AddWithValue("@SCCode", dt.Rows[i]["SCCode"].ToString());
                cmd.Parameters.AddWithValue("@RAmount", Convert.ToInt32(dt.Rows[i]["ReqFee"]));
                cmd.Parameters.AddWithValue("@Trans", Convert.ToString(dt.Rows[i]["Trans"]));
                cmd.Parameters.AddWithValue("@PAmount", Convert.ToInt32(dt.Rows[i]["Paidfee"]));
                cmd.Parameters.AddWithValue("@FeePer", Convert.ToDecimal(dt.Rows[i]["FeePer"]));
                cmd.Parameters.AddWithValue("@Refund", Convert.ToInt32(dt.Rows[i]["Refund"]));
                cmd.Parameters.AddWithValue("@LNo", rlno);
                cmd.Parameters.AddWithValue("@RG", "True");       
                cmd.Parameters.AddWithValue("@RP", "False");
               
               

                con.Open();
                object rr= cmd.ExecuteScalar();
                con.Close();

                if (rrids == "")
                {
                    rrids = rr.ToString();
                }
                else
                {
                    rrids = rrids + "," + rr.ToString();
                }
            }

            return rrids;
        }
    }
}