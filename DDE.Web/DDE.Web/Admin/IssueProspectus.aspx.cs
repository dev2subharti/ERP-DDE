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
    public partial class IssueProspectus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 81))
            {
                if (!IsPostBack)
                {
                    ViewState["ErrorType"] = "";               
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;

                }

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (validEntry())
            {
                string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;

                btnFind.Visible = false;
                fillFeePanelDetails();
                setTodayDate();
                
                tbReqFee.Text = (Convert.ToInt32(tbTotalPP.Text) * Accounts.findRequiredFeeByFHID(Convert.ToInt32(ddlistFeeHead.SelectedItem.Value),frdate)).ToString();
                tbStudentAmount.Text = tbReqFee.Text;

                PopulateDDList.populateAccountSession(ddlistAcountsSession);

                ddlistFeeHead.Enabled = false;
                ddlistPaymentMode.Enabled = false;
              
                tbTotalPP.Enabled = false;

                pnlDDFee.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
            }
        }

        private void setTodayDate()
        {
            ddlistFRDDay.SelectedItem.Selected = false;
            ddlistFRDMonth.SelectedItem.Selected = false;
            ddlistFRDYear.SelectedItem.Selected = false;
            ddlistFRDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistFRDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistFRDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        private void fillFeePanelDetails()
        {
            if (ddlistPaymentMode.SelectedItem.Value != "0")
            {
                if (ddlistPaymentMode.SelectedItem.Value == "1")
                {


                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "2")
                {


                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "3")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;


                }
                else if (ddlistPaymentMode.SelectedItem.Value == "4")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;

                }
                else if (ddlistPaymentMode.SelectedItem.Value == "5")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = true;
                    tbIBN.Visible = true;


                }
                else if (ddlistPaymentMode.SelectedItem.Value == "6")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;

                }

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Please select any one payment mode";
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
            }
        }

        protected void ddlistPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            fillFeePanelDetails();

        }      

        private bool validSCCode()
        {
            bool val = false;

            if (tbSCCode.Text != "" && tbSCCode.Text != "NA")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select SCCode from DDEStudyCentres where SCCode='"+tbSCCode.Text+"' and Mode='True'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count>0)
                {
                    val = true;

                }
                else
                {
                    val = false;
                }

            }

            
            return val;
        }

        private bool validEntry()
        {
            if (ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--")
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "OK")
            {
                btnFind.Visible = false;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else if (btnOK.Text == "Issue Another Prospectus")
            {
                Response.Redirect("IssueProspectus.aspx");
            }

        }

        protected void lnkbtnFDCDetails_Click(object sender, EventArgs e)
        {

            string error;
            int iid;
            string scmode;
            int count;
            string ardate;
            if (Accounts.validInstrument(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value, tbSCCode.Text, false, "", out iid, out scmode, out count, out ardate, out error))
            {
                lblIID.Text = iid.ToString();

                if (count == 1)
                {

                    string[] dcdetail = Accounts.findInstrumentsDetailsNew(tbDDNumber.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value));

                    if (scmode == "0")
                    {
                        pnlDDFee.BackColor = System.Drawing.Color.Orange;
                    }


                    lblNewDD.Visible = false;
                    ddlistDDDay.SelectedItem.Selected = false;
                    ddlistDDMonth.SelectedItem.Selected = false;
                    ddlistDDYear.SelectedItem.Selected = false;

                    ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
                    ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
                    ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

                    tbTotalAmount.Text = dcdetail[1].ToString();
                    tbIBN.Text = dcdetail[2].ToString();

                    lblNewDD.Visible = false;
                    btnIP.Visible = true;

                    tbDDNumber.Enabled = false;
                    ddlistDDDay.Enabled = false;
                    ddlistDDMonth.Enabled = false;
                    ddlistDDYear.Enabled = false;
                    if (ardate != "")
                    {
                        lblARDate.Text = "Amount. Rec. On : " + Convert.ToDateTime(ardate).ToString("dd-MM-yyyy");
                        lblARPDate.Text = Convert.ToDateTime(ardate).ToString("yyyy-MM-dd"); ;
                    }
                    else
                    {
                        lblARDate.Text = "";
                        lblARPDate.Text = "";
                    }
                    tbTotalAmount.Enabled = false;
                    tbIBN.Enabled = false;
                    tbStudentAmount.Enabled = true;


                }
                else if (count > 1)
                {
                    tbDDNumber.Visible = false;
                    lnkbtnFDCDetails.Visible = false;
                    PopulateDDList.populateSameInsByIno(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value, ddlistIns);
                    ddlistIns.Visible = true;
                    lblNewDD.Visible = false;

                    ddlistDDDay.Enabled = false;
                    ddlistDDMonth.Enabled = false;
                    ddlistDDYear.Enabled = false;
                    tbTotalAmount.Enabled = false;
                    tbIBN.Enabled = false;
                    tbStudentAmount.Enabled = false;

                }
                setAccountSession();
            }
            else
            {

                lblNewDD.Text = error;
                lblNewDD.Visible = true;
                btnIP.Visible = false;
            }

        }

        private void setAccountSession()
        {
            if (lblARPDate.Text == "" || lblARPDate.Text == "1900-01-01")
            {
                ddlistAcountsSession.Enabled = true;
            }
            else
            {
                ddlistAcountsSession.SelectedItem.Selected = false;
                string asess = Accounts.findAccountSessionByARDate(lblARPDate.Text);
                ddlistAcountsSession.Items.FindByText(asess).Selected = true;
                ddlistAcountsSession.Enabled = false;
            }
        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            if (lnkbtnEdit.Text == "Edit")
            {
                ddlistFRDDay.Enabled = true;
                ddlistFRDMonth.Enabled = true;
                ddlistFRDYear.Enabled = true;
                lnkbtnEdit.Text = "Update";
            }
            else if (lnkbtnEdit.Text == "Update")
            {
                ddlistFRDDay.Enabled = false;
                ddlistFRDMonth.Enabled = false;
                ddlistFRDYear.Enabled = false;
                lnkbtnEdit.Text = "Edit";
                string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                tbReqFee.Text = (Convert.ToInt32(tbTotalPP.Text) * Accounts.findRequiredFeeByFHID(Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), frdate)).ToString();
              
            }
        }

        protected void ddlistFeeHead_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlistFeeHead.SelectedItem.Value == "11")
            {
                tbSCCode.Text = "NA";
               
               
            }
            else if (ddlistFeeHead.SelectedItem.Value == "31")
            {
                tbSCCode.Text = "";
              
             
            }


        }

        protected void ddlistIns_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] dcdetail = Accounts.findInstrumentsDetailsNewByIID(Convert.ToInt32(ddlistIns.SelectedItem.Value));

            lblIID.Text = ddlistIns.SelectedItem.Value;
            lblNewDD.Visible = false;
            ddlistDDDay.SelectedItem.Selected = false;
            ddlistDDMonth.SelectedItem.Selected = false;
            ddlistDDYear.SelectedItem.Selected = false;

            ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
            ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
            ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

            tbTotalAmount.Text = dcdetail[1].ToString();
            tbIBN.Text = dcdetail[2].ToString();
            if (dcdetail[2].ToString() != "")
            {
                lblARDate.Text = "Amount Rec. On : " + Convert.ToDateTime(dcdetail[3]).ToString("dd-MM-yyyy");
                lblARPDate.Text = Convert.ToDateTime(dcdetail[3]).ToString("yyyy-MM-dd");
            }
            else
            {
                lblARDate.Text = "";
                lblARPDate.Text = "";
            }

            tbStudentAmount.Enabled = true;
            btnIP.Visible = true;
        }

        protected void btnIP_Click(object sender, EventArgs e)
        {

           
         try
         {
            string error;
            if (validFee(out error))
            {
                if (validEntry())
                {
                  
                        if (ddlistFeeHead.SelectedItem.Value == "31")
                        {
                            if (validSCCode())
                            {

                                string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                                string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;

                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("insert into DDESCFeeRecord values(@AccountSession,@SCOFRID,@SCID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@Concession,@TotalDCAmount,@ForYear,@FRDate,@TOFS,@EntryType)", con);

                                cmd.Parameters.AddWithValue("@AccountSession", ddlistAcountsSession.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@SCOFRID", 0);
                                cmd.Parameters.AddWithValue("@SCID", FindInfo.findSCIDBySCCode(tbSCCode.Text));
                                cmd.Parameters.AddWithValue("@FeeHead", 31);
                                cmd.Parameters.AddWithValue("@PaymentMode", ddlistPaymentMode.SelectedItem.Value);
                                cmd.Parameters.AddWithValue("@DCNumber", tbDDNumber.Text);
                                cmd.Parameters.AddWithValue("@DCDate", dcdate);
                                cmd.Parameters.AddWithValue("@IBN", tbIBN.Text.ToUpper());
                                cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(tbStudentAmount.Text));
                                cmd.Parameters.AddWithValue("@AmountInWords", Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)).ToUpper());
                                cmd.Parameters.AddWithValue("@Concession", 0);
                                cmd.Parameters.AddWithValue("@TotalDCAmount", Convert.ToInt32(tbTotalAmount.Text));
                                cmd.Parameters.AddWithValue("@ForYear", 0);
                                cmd.Parameters.AddWithValue("@FRDate", frdate);
                                cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                                cmd.Parameters.AddWithValue("@EntryType", 1);

                                cmd.Connection = con;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                Log.createLogNow("SC Fee Submit", "Filled" + ddlistFeeHead.SelectedItem.Text + " Fee of SC Code '" + tbSCCode.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                string er;
                                string range = FindInfo.findAndAllotProspectusRange(Convert.ToInt32(tbTotalPP.Text), tbSCCode.Text, Convert.ToInt32(lblIID.Text), out er);
                                if (er == "")
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = "Fee has been submitted successfully !! <br/> Alloted Prospectus Range is " + range;
                                    btnOK.Text = "Issue Another Prospectus";
                                    pnlMSG.Visible = true;
                                }
                                else
                                {
                                    lblMSG.Text = er;
                                    pnlData.Visible = false;
                                    lblMSG.Text = er;
                                    pnlMSG.Visible = true;


                                }
                            }
                            else
                            {

                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! You did not select any of given entries";
                                pnlMSG.Visible = true;
                                btnOK.Text = "OK";
                                btnOK.Visible = true;
                            }

                        }
                        else if (ddlistFeeHead.SelectedItem.Value == "11")
                        {

                            string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;

                            string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;


                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into DDEOtherFeeRecord values(@AccountSession,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@Concession,@TotalDCAmount,@FRDate,@TOFS,@EntryType)", con);

                            cmd.Parameters.AddWithValue("@AccountSession", ddlistAcountsSession.SelectedItem.Text);
                            cmd.Parameters.AddWithValue("@FeeHead", 11);
                            cmd.Parameters.AddWithValue("@PaymentMode", ddlistPaymentMode.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@DCNumber", tbDDNumber.Text);
                            cmd.Parameters.AddWithValue("@DCDate", dcdate);
                            cmd.Parameters.AddWithValue("@IBN", tbIBN.Text.ToUpper());
                            cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(tbStudentAmount.Text));
                            cmd.Parameters.AddWithValue("@AmountInWords", Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)).ToUpper());
                            cmd.Parameters.AddWithValue("@Concession", 0);
                            cmd.Parameters.AddWithValue("@TotalDCAmount", Convert.ToInt32(tbTotalAmount.Text));
                            cmd.Parameters.AddWithValue("@FRDate", frdate);
                            cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@EntryType", 1);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();


                            Log.createLogNow("Prospectus Issued", "Issued '" + tbTotalPP.Text + "' Prospectus on Instrument No : " + tbDDNumber.Text, Convert.ToInt32(Session["ERID"].ToString()));
                            string er;
                            string range = FindInfo.findAndAllotProspectusRange(Convert.ToInt32(tbTotalPP.Text), tbSCCode.Text, Convert.ToInt32(lblIID.Text), out er);
                            if (er == "")
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Fee has been submitted successfully !! <br/> Alloted Prospectus Range is " + range;
                                btnOK.Text = "Issue Another Prospectus";
                                btnOK.Visible = true;
                                pnlMSG.Visible = true;
                            }
                            else
                            {
                                lblMSG.Text = er;
                                pnlData.Visible = false;
                                lblMSG.Text = er;
                                pnlMSG.Visible = true;


                            }
                        }


                  
                }

                else
                {

                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! You did not select any of given entries";
                    pnlMSG.Visible = true;
                    btnOK.Text = "OK";
                    btnOK.Visible = true;

                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = error;
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
            }

          }

          catch (FormatException ex)
          {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;

          }
        }

        private bool validFee(out string error)
        {
            error = "";
            bool valid = false;
            string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;
            if (Convert.ToInt32(tbStudentAmount.Text) == Convert.ToInt32(tbReqFee.Text))
            {
                
                int  fhfee =Accounts.findTotalFHFeeByIID(Convert.ToInt32(ddlistFeeHead.SelectedItem.Value),Convert.ToInt32(lblIID.Text));
               
                int fhusedfee =Accounts.findUsedAmountOfDraftByFH(Convert.ToInt32(ddlistFeeHead.SelectedItem.Value),Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value),tbDDNumber.Text,dcdate,tbIBN.Text);
                            
                int reaminfhfee = (fhfee - fhusedfee);

                if (reaminfhfee >= Convert.ToInt32(tbStudentAmount.Text))
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                    error = "Sorry !! Filled amount is " + tbStudentAmount.Text + " but the remaining amount on this fee head of this instrument is " + reaminfhfee.ToString();
                }

            }
            else
            {
                valid = false;
                error = "Sorry !! 'Paying Amount' is not equal to 'Required Fee'";
            }

            return valid;
        }
    
    }
}