using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class LinkRLToInstrument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
            {
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;


            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (FindInfo.refundLetterExistByNo(tbLNo.Text))
            {
                int riid= populateLetterDetails(Convert.ToInt32(tbLNo.Text));
                if (riid > 1)
                {
                    populatePaidInsDetails(riid);
                    btnSubmit.Visible = false;
                }
                else
                {
                    btnSubmit.Visible = true;
                }
                tbLNo.Enabled = false;
                btnSearch.Visible = false;
                pnlLAndIDetails.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! There is no 'Reimbursement Letter' with this no.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void populatePaidInsDetails(int riid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDERefundInstruments where RIID='" + riid + "'", con);
            SqlDataReader dr;

          
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                ddlistDraftType.Items.FindByValue(dr["IType"].ToString()).Selected = true;
                ddlistDraftType.Enabled = false;

                tbDNo.Text = dr["INo"].ToString();
                tbDNo.Enabled = false;

                lblIID.Text = Convert.ToInt32(dr["IID"]).ToString();

                ddlistDDDay.Items.FindByText(dr["IDate"].ToString().Substring(8, 2)).Selected = true;
                ddlistDDMonth.Items.FindByValue(dr["IDate"].ToString().Substring(5, 2)).Selected = true;
                ddlistDDYear.Items.FindByText(dr["IDate"].ToString().Substring(0, 4)).Selected = true;

                ddlistDDDay.Enabled = false;
                ddlistDDMonth.Enabled = false;
                ddlistDDYear.Enabled = false;

                tbIBN.Text = dr["IBN"].ToString().ToUpper();
                tbIBN.Enabled = false;

                tbTotalAmount.Text = Convert.ToInt32(dr["TotalAmount"]).ToString();
                tbTotalAmount.Enabled = false;
              
               
            }


            con.Close();


          

        }

        private int populateLetterDetails(int lno)
        {
            int riid = 1;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDERefundLetterRecord where RLID='" + lno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                tbTotalRefund.Text = Convert.ToInt32(dr["TotalRefund"]).ToString();
                tbBalanceExtra.Text = Convert.ToInt32(dr["Extra"]).ToString();
                tbBalanceShort.Text = Convert.ToInt32(dr["Short"]).ToString();
                tbNetRefund.Text = Convert.ToInt32(dr["NetRefund"]).ToString();
                riid = Convert.ToInt32(dr["RIID"]);

            }

            return riid;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tbNetRefund.Text) == Convert.ToInt32(tbTotalAmount.Text))
                {

                    string idate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDERefundInstruments output Inserted.RIID values(@IType,@INo,@IDate,@IAmount,@IBN,@EnteredBy,@EntryTime,@Verified,@VerifiedBy,@VerifiedTime)", con);


                    cmd.Parameters.AddWithValue("@IType", ddlistDraftType.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@INo", tbDNo.Text);
                    cmd.Parameters.AddWithValue("@IDate", idate);
                    cmd.Parameters.AddWithValue("@IAmount", Convert.ToInt32(tbTotalAmount.Text));
                    cmd.Parameters.AddWithValue("@IBN", tbIBN.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@EnteredBy", Convert.ToInt32(Session["ERID"]));
                    cmd.Parameters.AddWithValue("@EntryTime", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@Verified", "False");
                    cmd.Parameters.AddWithValue("@VerifiedBy", 0);
                    cmd.Parameters.AddWithValue("@VerifiedTime", "");

                    con.Open();
                    object riid = cmd.ExecuteScalar();
                    con.Close();

                    int ures = updateRLDetails(Convert.ToInt32(tbLNo.Text), Convert.ToInt32(riid));

                    if (ures == 1)
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Reimbursement for letter no. '" + tbLNo.Text + "' has been paid with this instrument details";
                        pnlMSG.Visible = true;
                    }
                    else
                    {
                        Log.createErrorLog("Pay Refund", "Sorry !! Some error occured.Please contact to ERP Developer.", Convert.ToInt32(Session["ERID"]));
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Some error occured.Please contact to ERP Developer.";
                        pnlMSG.Visible = true;
                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Amount of Instrument is not equal to 'Net Payable to Centre'.";
                    pnlMSG.Visible = true;

                }
            }
            catch (Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }
        }

        private int updateRLDetails(int rlid, int riid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDERefundLetterRecord set RIID=@RIID where RLID='" + rlid + "' ", con);
            cmd.Parameters.AddWithValue("@RIID", riid);

            con.Open();
            int r = cmd.ExecuteNonQuery();
            con.Close();

            return r;
        }
    }
}