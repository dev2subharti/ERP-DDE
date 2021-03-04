using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class VerifyDraft : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IID"] != null)
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78))
                {
                   
                    pnlSearch.Visible = false;
                    populateDCDetails(Convert.ToInt32(Request.QueryString["IID"]));             
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
            else
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 74))
                {
                    
                    pnlSearch.Visible = true;                
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
        }
    
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            populateTotalInstruments();
           
        }
      
        private void populateTotalInstruments()
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("select * from DDEFeeInstruments where INo='" + tbDCNo.Text + "'", con1);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("IID");
            DataColumn dtcol3 = new DataColumn("Type");
            DataColumn dtcol4 = new DataColumn("TypeNo");
            DataColumn dtcol5 = new DataColumn("No");
            DataColumn dtcol6 = new DataColumn("Date");
            DataColumn dtcol7 = new DataColumn("TotalAmount");
            DataColumn dtcol8 = new DataColumn("IBN");
            DataColumn dtcol9 = new DataColumn("Status");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            con1.Open();
            dr1 = cmd1.ExecuteReader();
            int i = 1;
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["IID"] = Convert.ToString(dr1["IID"]);
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr1["IType"]));                   
                    drow["No"] = Convert.ToString(dr1["INo"]);
                    drow["Date"] = Convert.ToDateTime(dr1["IDate"]).ToString("dd MMMM yyyy");
                    drow["TotalAmount"] = Convert.ToInt32(dr1["TotalAmount"]);
                    drow["IBN"] = Convert.ToString(dr1["IBN"]);
                    drow["Status"] = Convert.ToString(dr1["Verified"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }
            con1.Close();

            dtlistTotalInstruments.DataSource = dt;
            dtlistTotalInstruments.DataBind();

            if (i > 1)
            {
                dtlistTotalInstruments.Visible = true;
                pnlData.Visible = true;              
                pnlMSG.Visible = false;              
                btnOK.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Insrument exist with this no.";
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
            }
        }

        private void populateDCDetails(int iid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
          
            cmd.CommandText = "select * from DDEFeeInstruments where IID='"+iid+"'";

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tbDType.Text = Accounts.findMOPByID(Convert.ToInt32(dr["IType"]));
                tbDNo.Text = dr["INo"].ToString();
                lblIID.Text = Convert.ToInt32(dr["IID"]).ToString();
                tbDCDate.Text = Convert.ToDateTime(dr["IDate"]).ToString("dd MMMM yyyy").ToUpper();
                tbIBN.Text = dr["IBN"].ToString().ToUpper();
                tbTotalAmount.Text = Convert.ToInt32(dr["TotalAmount"]).ToString();
                tbSCCode.Text = dr["SCCode"].ToString();

                ddlistDDDay.SelectedItem.Selected = false;
                ddlistDDMonth.SelectedItem.Selected = false;
                ddlistDDYear.SelectedItem.Selected = false;
                
                if (dr["Verified"].ToString() == "True")
                {
                    if (dr["AmountReceivedOn"].ToString() != "")
                    {
                        ddlistDDDay.Items.FindByText(Convert.ToDateTime(dr["AmountReceivedOn"]).ToString("dd")).Selected = true;
                        ddlistDDMonth.Items.FindByValue(Convert.ToDateTime(dr["AmountReceivedOn"]).ToString("MM")).Selected = true;
                        ddlistDDYear.Items.FindByText(Convert.ToDateTime(dr["AmountReceivedOn"]).ToString("yyyy")).Selected = true;
                    }
                    else
                    {
                        ddlistDDDay.Items.FindByText(Convert.ToDateTime(dr["IDate"]).ToString("dd")).Selected = true;
                        ddlistDDMonth.Items.FindByValue(Convert.ToDateTime(dr["IDate"]).ToString("MM")).Selected = true;
                        ddlistDDYear.Items.FindByText(Convert.ToDateTime(dr["IDate"]).ToString("yyyy")).Selected = true;

                    }
                    lblSumOn.Text = "Verified On :" + Convert.ToDateTime(dr["VerifiedOn"]).ToString("dd-MM-yyyy");
                    lblSumOn.Visible = true;
                    ddlistDDDay.Enabled = false;
                    ddlistDDMonth.Enabled = false;
                    ddlistDDYear.Enabled = false;

                }
                else
                {
                    ddlistDDDay.Items.FindByText(Convert.ToDateTime(dr["IDate"]).ToString("dd")).Selected = true;
                    ddlistDDMonth.Items.FindByValue(Convert.ToDateTime(dr["IDate"]).ToString("MM")).Selected = true;
                    ddlistDDYear.Items.FindByText(Convert.ToDateTime(dr["IDate"]).ToString("yyyy")).Selected = true;

                    lblSumOn.Text = "";
                    lblSumOn.Visible = false;
                    ddlistDDDay.Enabled = true;
                    ddlistDDMonth.Enabled = true;
                    ddlistDDYear.Enabled = true;
                }
            }


            con.Close();


            pnlDCDetail.Visible = true;
            pnlSearch.Visible = false;

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "Verify More Instruments")
            {
                Response.Redirect("VerifyDraft.aspx");
            }
            else if (btnOK.Text == "OK")
            {
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlDCDetail.Visible = false;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
        } 

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            if (valisSCCodes())
            {
                if (!(Accounts.isInstrumentLocked(Convert.ToInt32(lblIID.Text))))
                {
                    if (btnVerify.Text == "Verify Instrument")
                    {
                        string aro = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEFeeInstruments set Verified=@Verified,VerifiedOn=@VerifiedOn,AmountReceivedOn=@AmountReceivedOn,VerifiedBy=@VerifiedBy where IID='" + lblIID.Text + "'", con);

                        cmd.Parameters.AddWithValue("@Verified", "True");
                        cmd.Parameters.AddWithValue("@VerifiedOn", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@AmountReceivedOn", aro);
                        cmd.Parameters.AddWithValue("@VerifiedBy", Convert.ToInt32(Session["ERID"]));



                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Verified Instruments", "Verified '" + tbDType.Text + "' with No. '" + tbDCNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                        pnlData.Visible = false;
                        lblMSG.Text = "Instruments has been verified successfully";
                        pnlMSG.Visible = true;
                        btnOK.Text = "Verify More Instruments";
                        btnOK.Visible = true;
                    }
                    else if (btnVerify.Text == "Set Verification Pending")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEFeeInstruments set Verified=@Verified,VerifiedOn=@VerifiedOn,AmountReceivedOn=@AmountReceivedOn,VerifiedBy=@VerifiedBy where IID='" + lblIID.Text + "'", con);

                        cmd.Parameters.AddWithValue("@Verified", "False");
                        cmd.Parameters.AddWithValue("@VerifiedOn", "");
                        cmd.Parameters.AddWithValue("@AmountReceivedOn", "");
                        cmd.Parameters.AddWithValue("@VerifiedBy", "");



                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Update", "Set Verification Pending for '" + tbDType.Text + "' with No. '" + tbDNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                        pnlData.Visible = false;
                        lblMSG.Text = "Instruments has been set on 'Verification Pending' mode successfully";
                        pnlMSG.Visible = true;

                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! This Instrument is Locked.Please contact Accounts Department";
                    pnlMSG.Visible = true;
                    btnOK.Text = "OK";
                    btnOK.Visible = true;
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry ! Invalid SC Code.";
                pnlMSG.Visible = true;
            }
        }

        private bool valisSCCodes()
        {
            bool val = false;


            //string[] str = tbSCCode.Text.Split(',');



            //bool isspecafcode = false;

            //string[] specafcode = FindInfo.findSpecAFCodes();

            //for (int s = 0; s < str.Length; s++)
            //{
            //    int pos = Array.IndexOf(specafcode, str[s]);

            //    if ((pos > -1))
            //    {
            //        isspecafcode = true;
            //    }
            //}


            //if (isspecafcode == true)
            //{
            //    if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78))
            //    {
            //        val = true;
            //    }
            //    else
            //    {
            //        val = false;
            //    }
            //}
            //else
            //{
            //    val = true;
            //}

            //return val;

            return true;
        }

        protected void dtlistTotalInstruments_ItemCommand(object source, DataListCommandEventArgs e)
        {
            populateDCDetails(Convert.ToInt32(e.CommandArgument));
            dtlistTotalInstruments.Visible = false;
            if (e.CommandName == "True")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 74))
                {

                    btnVerify.Text = "Set Verification Pending";
                    btnVerify.Enabled = true;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78))
                {
                    btnVerify.Text = "Set Verification Pending";
                    btnVerify.Enabled = true;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 74))
                {
                    btnVerify.Text = "Already Verified";
                    btnVerify.Enabled = false;
                }
            }
            else if (e.CommandName == "False")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 74))
                {
                    btnVerify.Text = "Verify Instrument";
                    btnVerify.Enabled = true;
                    btnVerify.Visible = true;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 74))
                {
                    btnVerify.Text = "Verify Instrument";
                    btnVerify.Enabled = true;
                    btnVerify.Visible = true;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78))
                {
                    
                    btnVerify.Visible = false;
                }
            }
        }

       

       
    }
}
