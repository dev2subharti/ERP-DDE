using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class ReceivePPFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 103))
            {
                setTodayDate();
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

        private void setTodayDate()
        {
            ddlistDDDay.SelectedItem.Selected = false;
            ddlistDDMonth.SelectedItem.Selected = false;
            ddlistDDYear.SelectedItem.Selected = false;
            ddlistDDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "Receive More Instrument")
            {
                Response.Redirect("ReceivePPFee.aspx");
            }
            else if (btnOK.Text == "OK")
            {
                btnOK.Visible = false;
                pnlMSG.Visible = false;
                pnlData.Visible = true;
            }
        }

      

        protected void btnReceive_Click(object sender, EventArgs e)
        {
           
                if (tbDNo.Text != "" && tbTotalAmount.Text != "")
                {
                    if (ddlistFeeHead.SelectedItem.Text != "--SELECT ONE--")
                    {
                       
                            string idate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;

                            if (!Accounts.instrumentExist(tbDNo.Text,Convert.ToInt32(ddlistDraftType.SelectedItem.Value),"NA", idate))
                            {
                                try
                                {                                  
                                        if (valisSCCodes())
                                        {

                                            if (FindInfo.isValidDate(idate))
                                            {

                                                string sc = "";
                                                if (tbSCCode.Text == "" && tbProSCCode.Text != "")
                                                {
                                                    sc = tbProSCCode.Text;
                                                }
                                                else if (tbSCCode.Text != "" && tbProSCCode.Text == "")
                                                {
                                                    sc = tbSCCode.Text;
                                                }
                                                else if (tbSCCode.Text != "" && tbProSCCode.Text != "")
                                                {
                                                    sc = tbSCCode.Text + "," + tbProSCCode.Text;
                                                }
                                                else if (tbSCCode.Text == "NA")
                                                {
                                                    sc = "NA";
                                                }
                                             
                                                string[] str = sc.Split(',');

                                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                                SqlCommand cmd = new SqlCommand("insert into DDEFeeInstruments values(@RG,@Lock,@LockRemark,@IType,@INo,@IDate,@IBN,@TotalAmount,@SCMode,@SCCode,@Received,@ReceivedOn,@ReceivedBy,@Verified,@VerifiedOn,@AmountReceivedOn,@VerifiedBy,@AmountAlloted,@AllotedOn,@AllotedBy,@AllotedFeeHeads,@Remark,@Balance,@FH1,@FH2,@FH3,@FH4,@FH5,@FH5,@FH7,@FH8,@FH9,@FH10,@FH11,@FH12,@FH13,@FH14,@FH15,@FH16,@FH17,@FH18,@FH19,@FH20,@FH21,@FH22,@FH23,@FH24,@FH25,@FH26,@FH27,@FH28,@FH29,@FH30,@FH31,@FH32,@FH33,@FH34,@FH35,@FH36,@FH37,@FH38,@FH39,@FH40,@FH41,@FH42,@FH43,@FH44,@FH45,@FH46,@FH47,@FH48,@FH49,@FH50,@FH51,@FH52,@FH53,@FH54,@FH55,@FH56,@FH57,@FH58,@FH59,@FH60,@FH61,@FH62,@FH63,@FH64,@FH65,@FH66,@FH67)", con);

                                                cmd.Parameters.AddWithValue("@RG", "False");
                                                cmd.Parameters.AddWithValue("@Lock", "False");
                                                cmd.Parameters.AddWithValue("@LockRemark", "");
                                                cmd.Parameters.AddWithValue("@IType", ddlistDraftType.SelectedItem.Value);
                                                cmd.Parameters.AddWithValue("@INo", tbDNo.Text);
                                                cmd.Parameters.AddWithValue("@IDate", idate);
                                                cmd.Parameters.AddWithValue("@IBN", "NA");
                                                cmd.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(tbTotalAmount.Text));
                                                if (str.Length == 1)
                                                {
                                                    cmd.Parameters.AddWithValue("@SCMode", "True");
                                                }
                                                else if (str.Length > 1)
                                                {
                                                    cmd.Parameters.AddWithValue("@SCMode", "False");
                                                }

                                                cmd.Parameters.AddWithValue("@SCCode", sc);
                                                cmd.Parameters.AddWithValue("@Received", "True");
                                                cmd.Parameters.AddWithValue("@ReceivedOn", DateTime.Now.ToString());
                                                cmd.Parameters.AddWithValue("@ReceivedBy", Convert.ToInt32(Session["ERID"]));
                                                cmd.Parameters.AddWithValue("@Verified", "True");
                                                cmd.Parameters.AddWithValue("@VerifiedOn", DateTime.Now.ToString());
                                                cmd.Parameters.AddWithValue("@AmountReceivedOn", DateTime.Now.ToString());
                                                cmd.Parameters.AddWithValue("@VerifiedBy", Convert.ToInt32(Session["ERID"]));
                                                cmd.Parameters.AddWithValue("@AmountAlloted", "True");
                                                cmd.Parameters.AddWithValue("@AllotedOn", DateTime.Now.ToString());
                                                cmd.Parameters.AddWithValue("@AllotedBy", Convert.ToInt32(Session["ERID"]));
                                                cmd.Parameters.AddWithValue("@AllotedFeeHeads", ddlistFeeHead.SelectedItem.Value);
                                                cmd.Parameters.AddWithValue("@Remark", 5);
                                                cmd.Parameters.AddWithValue("@Balance", 0);
                                              
                                                for (int i = 1; i <= 10; i++)
                                                {
                                                    cmd.Parameters.AddWithValue("@FH" + i.ToString(), 0);
                                                }

                                                if (ddlistFeeHead.SelectedItem.Value == "11")
                                                {
                                                    cmd.Parameters.AddWithValue("@FH11", Convert.ToInt32(tbTotalAmount.Text));
                                                }
                                                else
                                                {
                                                    cmd.Parameters.AddWithValue("@FH11",0);
                                                }

                                               
                                                for (int i = 12; i <= 30; i++)
                                                {
                                                    cmd.Parameters.AddWithValue("@FH" + i.ToString(), 0);
                                                }


                                                if (ddlistFeeHead.SelectedItem.Value == "31")
                                                {
                                                    cmd.Parameters.AddWithValue("@FH31", Convert.ToInt32(tbTotalAmount.Text));
                                                }
                                                else
                                                {
                                                    cmd.Parameters.AddWithValue("@FH31", 0);
                                                }


                                                for (int i = 32; i <= 67; i++)
                                                {
                                                    cmd.Parameters.AddWithValue("@FH" + i.ToString(), 0);
                                                }

                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                             
                                                Log.createLogNow("Create", "Created a Fee Instrument '" + ddlistDraftType.SelectedItem.Text + " with No. '" + tbDNo.Text + "'", Convert.ToInt32(Session["ERID"]));
                                                pnlData.Visible = false;
                                                lblMSG.Text = "Instrument has been received successfully";
                                                pnlMSG.Visible = true;
                                                btnOK.Text = "Receive More Instrument";
                                                btnOK.Visible = true;
                                            }
                                            else
                                            {
                                                pnlData.Visible = false;
                                                lblMSG.Text = "Sorry ! Invalid date.";
                                                pnlMSG.Visible = true;
                                                btnOK.Text = "OK";
                                                btnOK.Visible = true;
                                            }
                                        }
                                        else
                                        {
                                            pnlData.Visible = false;
                                            lblMSG.Text = "Sorry ! Invalid SC Code. Please fill correct SC Code";
                                            pnlMSG.Visible = true;
                                            btnOK.Text = "OK";
                                            btnOK.Visible = true;
                                        }
                                    
                                }

                                catch (Exception er)
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = er.Message;
                                    pnlMSG.Visible = true;
                                    btnOK.Text = "OK";
                                    btnOK.Visible = true;
                                }


                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! this Instrument is already exist";
                                pnlMSG.Visible = true;
                                btnOK.Text = "OK";
                                btnOK.Visible = true;
                            }                   

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Please select any 'PROSPECTUS TYPE'";
                        pnlMSG.Visible = true;
                        btnOK.Text = "OK";
                        btnOK.Visible = true;
                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please check !! you have missed any entry";
                    pnlMSG.Visible = true;
                    btnOK.Text = "OK";
                    btnOK.Visible = true;
                }
        
           
           
            
        }

        private bool valisSCCodes()
        {
            bool val = false;
            string valid = "NA";

            if (ddlistFeeHead.SelectedItem.Value == "31")
            {

                string[] str = tbSCCode.Text.Split(',');
                string[] pstr = tbProSCCode.Text.Split(',');

                if (tbSCCode.Text != "")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select SCCode from DDEStudyCentres where SCCode in (" + tbSCCode.Text + ") and Mode='True'", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count == str.Length)
                    {
                        valid = "Yes";
                    }
                    else
                    {
                        valid = "No";
                    }

                }

                if (valid != "No")
                {

                    if (tbProSCCode.Text != "")
                    {
                        for (int i = 0; i < pstr.Length; i++)
                        {
                            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd1 = new SqlCommand("Select ProSCCode from DDEStudyCentres where ProSCCode ='" + pstr[i] + "' and Mode='False'", con1);

                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);

                            if (ds1.Tables[0].Rows.Count == str.Length)
                            {
                                if (valid != "No")
                                {
                                    valid = "Yes";
                                }
                            }
                            else
                            {
                                valid = "No";
                            }
                        }
                    }
                }

                if (tbSCCode.Text != "")
                {
                    if (valid == "Yes")
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i].Length != 3)
                            {
                                valid = "No";
                                break;
                            }
                        }
                    }
                }

                if (valid == "Yes")
                {
                    val = true;
                }
            }
            else if (ddlistFeeHead.SelectedItem.Value == "11")
            {

                val = true;
            }

            return val;

        }

       
        protected void ddlistFeeHead_SelectedIndexChanged(object sender, EventArgs e)
        {
             if (ddlistFeeHead.SelectedItem.Value == "11")
             {
                 tbSCCode.Text = "NA";
                 tbSCCode.Enabled = false;

               
                 tbProSCCode.Enabled = false;
             }

             else if (ddlistFeeHead.SelectedItem.Value == "31")
             {
                 tbSCCode.Text = "";
                 tbSCCode.Enabled = true;

                 tbProSCCode.Text = "";
                 tbProSCCode.Enabled = true;
             }
        }
    }
}