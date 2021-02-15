using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace DDE.Web.Admin
{
    public partial class CreateInstrument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IID"] != null)
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 77))
                {

                    if (!IsPostBack)
                    {                       
                        populateFeeHeads();
                        PopulateDDList.populateBanks(ddlistIBN);
                        populateInstrumentDetails();
                        if (Accounts.isInstrumentVerified(Convert.ToInt32(Request.QueryString["IID"])))
                        {
                            frezeData();                                         
                        }
                        else
                        {                         
                            btnReceive.Visible = true;
                           
                        }                                          
                         
                    }

                    btnReceive.Text = "Update";
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
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 73))
                {

                    if (!IsPostBack)
                    {                     
                        populateFeeHeads();
                        PopulateDDList.populateBanks(ddlistIBN);
                        setTodayDate();
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

        private void frezeData()
        {
            ddlistDraftType.Enabled = false;
            tbDNo.Enabled = false;
            ddlistDDDay.Enabled = false;
            ddlistDDMonth.Enabled = false;
            ddlistDDYear.Enabled = false;
            ddlistIBN.Enabled = false;
            tbTotalAmount.Enabled = false;
        }

        private void populateInstrumentDetails()
        {
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where IID='"+Request.QueryString["IID"]+"'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ddlistDraftType.Items.FindByValue(dr["IType"].ToString()).Selected = true;
               

                tbDNo.Text = dr["INo"].ToString();
               

                lblIID.Text = Convert.ToInt32(dr["IID"]).ToString();

                ddlistDDDay.Items.FindByText(dr["IDate"].ToString().Substring(8,2)).Selected = true;
                ddlistDDMonth.Items.FindByValue(dr["IDate"].ToString().Substring(5,2)).Selected = true;
                ddlistDDYear.Items.FindByText(dr["IDate"].ToString().Substring(0,4)).Selected = true;


                ddlistIBN.Items.FindByText(dr["IBN"].ToString().ToUpper()).Selected = true;
              
                tbTotalAmount.Text = Convert.ToInt32(dr["TotalAmount"]).ToString();
                tbSCCode.Text= dr["SCCode"].ToString();
                ddlistRemark.Items.FindByValue(dr["Remark"].ToString()).Selected = true;
                
                populateFeeHeads();

                string[] fhs = dr["AllotedFeeHeads"].ToString().Split(',');

                foreach (DataListItem dli in dtlistFeeHeads.Items)
                {
                    CheckBox cb = (CheckBox)dli.FindControl("cbChecked");
                    Label fhid = (Label)dli.FindControl("lblFHID");

                    int pos = Array.IndexOf(fhs, fhid.Text);

                    if ((pos>-1))
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Checked = false;
                    }

                    if (Convert.ToInt32(fhid.Text) == 31)
                    {
                        cb.Enabled = false;
                    }
                }



            }


            con.Close();

      
        }

        private void populateFeeHeads()
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FHID");
            DataColumn dtcol3 = new DataColumn("FeeHead");
        

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
          

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from DDEFeeHead order by FHID";


            cmd.Connection = con;
            con.Open();

            dr = cmd.ExecuteReader();

            int i = 1;
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["FHID"] = dr["FHID"].ToString();
                    drow["FeeHead"] = dr["FeeHead"].ToString();
                   


                    dt.Rows.Add(drow);
                    i = i + 1;
                }
            }



            dtlistFeeHeads.DataSource = dt;
            dtlistFeeHeads.DataBind();

            con.Close();





        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "Receive More Instrument")
            {
                Response.Redirect("CreateInstrument.aspx");
            }
            else  if (btnOK.Text == "OK")
            {
                btnOK.Visible = false;
                pnlMSG.Visible = false;
                pnlData.Visible = true;
            }
        }

        protected void ddlistDraftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistDraftType.SelectedItem.Value == "1")
            {
                tbDNo.Text = "";
                tbDNo.Enabled = true;
             
               
                ddlistIBN.Enabled = true;

               

                ddlistDDDay.Enabled = true;
                ddlistDDMonth.Enabled = true;
                ddlistDDYear.Enabled = true;
            }
            else if (ddlistDraftType.SelectedItem.Value == "2")
            {
                tbDNo.Text = "";
                tbDNo.Enabled = true;

              
                ddlistIBN.Enabled = true;



                ddlistDDDay.Enabled = true;
                ddlistDDMonth.Enabled = true;
                ddlistDDYear.Enabled = true;
            }
            else if (ddlistDraftType.SelectedItem.Value == "3")
            {
                tbDNo.Text = "";
                tbDNo.Enabled = true;

                ddlistIBN.Items.FindByText("NA").Selected = true;
                ddlistIBN.Enabled = false;



                ddlistDDDay.Enabled = true;
                ddlistDDMonth.Enabled = true;
                ddlistDDYear.Enabled = true;
            }
            else if (ddlistDraftType.SelectedItem.Value == "4")
            {
                tbDNo.Text = "";
                tbDNo.Enabled = true;

              
                ddlistIBN.Enabled = true;



                ddlistDDDay.Enabled = true;
                ddlistDDMonth.Enabled = true;
                ddlistDDYear.Enabled = true;
            }
            else if (ddlistDraftType.SelectedItem.Value == "5")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 76))
                {
                    tbDNo.Text = FindInfo.findDFRNo();
                    tbDNo.Enabled = false;

                    ddlistIBN.Items.FindByText("NA").Selected = true;
                    ddlistIBN.Enabled = false; ;



                    ddlistDDDay.Enabled = true;
                    ddlistDDMonth.Enabled = true;
                    ddlistDDYear.Enabled = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! you are not authorised for receiving this instrument type.";
                    pnlMSG.Visible = true;
                    btnOK.Text = "OK";
                    btnOK.Visible = true;

                    ddlistDraftType.SelectedItem.Selected = false;
                    ddlistDraftType.Items.FindByText("--SELECT ONE--").Selected = true;
                    tbDNo.Text = "";
                }
            }
            else if (ddlistDraftType.SelectedItem.Value == "6")
            {
                tbDNo.Text = FindInfo.findDCTNo();
                tbDNo.Enabled = false;

               
                ddlistIBN.Enabled = true;



                ddlistDDDay.Enabled = true;
                ddlistDDMonth.Enabled = true;
                ddlistDDYear.Enabled = true;
            }
        }

        protected void btnReceive_Click(object sender, EventArgs e)
        {
            if (btnReceive.Text == "Receive")
            {
                if (tbDNo.Text != "" && tbTotalAmount.Text != "")
                {
                    if (ddlistDraftType.SelectedItem.Text != "--SELECT ONE--")
                    {
                        if (ddlistRemark.SelectedItem.Text != "--SELECT ONE--")
                        {
                             string idate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;

                            if (!Accounts.instrumentExist(tbDNo.Text,Convert.ToInt32(ddlistDraftType.SelectedItem.Value) ,ddlistIBN.SelectedItem.Text, idate))
                            {
                                try
                                {
                                    string fhs = calculateAFH();
                                    if (fhs != "")
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

                                                string[] str = sc.Split(',');

                                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                                SqlCommand cmd = new SqlCommand("insert into DDEFeeInstruments values(@RG,@Lock,@LockRemark,@IType,@INo,@IDate,@IBN,@TotalAmount,@SCMode,@SCCode,@Received,@ReceivedOn,@ReceivedBy,@Verified,@VerifiedOn,@AmountReceivedOn,@VerifiedBy,@AmountAlloted,@AllotedOn,@AllotedBy,@AllotedFeeHeads,@Remark,@Balance,@FH1,@FH2,@FH3,@FH4,@FH5,@FH5,@FH7,@FH8,@FH9,@FH10,@FH11,@FH12,@FH13,@FH14,@FH15,@FH16,@FH17,@FH18,@FH19,@FH20,@FH21,@FH22,@FH23,@FH24,@FH25,@FH26,@FH27,@FH28,@FH29,@FH30,@FH31,@FH32,@FH33,@FH34,@FH35,@FH36,@FH37,@FH38,@FH39,@FH40,@FH41,@FH42,@FH43,@FH44,@FH45,@FH46,@FH47,@FH48,@FH49,@FH50,@FH51,@FH52,@FH53,@FH54,@FH55,@FH56,@FH57,@FH58,@FH59,@FH60,@FH61,@FH62,@FH63,@FH64,@FH65,@FH66,@FH67)", con);

                                                cmd.Parameters.AddWithValue("@RG", "False");
                                                cmd.Parameters.AddWithValue("@Lock", "False");
                                                cmd.Parameters.AddWithValue("@LockRemark", "");
                                                cmd.Parameters.AddWithValue("@IType", ddlistDraftType.SelectedItem.Value);
                                                cmd.Parameters.AddWithValue("@INo", tbDNo.Text);
                                                cmd.Parameters.AddWithValue("@IDate", idate);
                                                cmd.Parameters.AddWithValue("@IBN", ddlistIBN.SelectedItem.Text.ToUpper());
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
                                                cmd.Parameters.AddWithValue("@Verified", "False");
                                                cmd.Parameters.AddWithValue("@VerifiedOn", "");
                                                cmd.Parameters.AddWithValue("@AmountReceivedOn", "");
                                                cmd.Parameters.AddWithValue("@VerifiedBy", 0);
                                                cmd.Parameters.AddWithValue("@AmountAlloted", "False");
                                                cmd.Parameters.AddWithValue("@AllotedOn", "");
                                                cmd.Parameters.AddWithValue("@AllotedBy", 0);
                                                cmd.Parameters.AddWithValue("@AllotedFeeHeads", fhs);
                                                cmd.Parameters.AddWithValue("@Remark", Convert.ToInt32(ddlistRemark.SelectedItem.Value));
                                                cmd.Parameters.AddWithValue("@Balance", 0);
                                                int ta = 0;


                                                for (int i = 1; i <= 67; i++)
                                                {

                                                    cmd.Parameters.AddWithValue("@FH" + i.ToString(), 0);

                                                }

                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();

                                                if (ddlistDraftType.SelectedItem.Value == "5")
                                                {

                                                    FindInfo.updateDRCounter(Convert.ToInt32(tbDNo.Text.Substring(3, (tbDNo.Text.Length - 3))));
                                                }
                                                else if (ddlistDraftType.SelectedItem.Value == "6")
                                                {

                                                    FindInfo.updateCTCounter(Convert.ToInt32(tbDNo.Text.Substring(3, (tbDNo.Text.Length - 3))));
                                                }

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
                                    else
                                    {
                                        pnlData.Visible = false;
                                        lblMSG.Text = "Please select any Fee Head.";
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
                            lblMSG.Text = "Please select any 'REMARK'";
                            pnlMSG.Visible = true;
                            btnOK.Text = "OK";
                            btnOK.Visible = true;
                        }

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Please select any 'INTRUMENT TYPE'";
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
            else if (btnReceive.Text == "Update")
            {
                if (ddlistDraftType.SelectedItem.Text != "--SELECT ONE--")
                {
                    if (ddlistRemark.SelectedItem.Text != "--SELECT ONE--")
                    {
                
                            try
                            {
                                string fhs = calculateAFH();
                                if (fhs != "")
                                {
                                    if (valisSCCodes())
                                    {
                                        string idate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;

                                        string sc = "";
                                        if (tbSCCode.Text == "" && tbProSCCode.Text != "")
                                        {
                                            sc=tbProSCCode.Text;
                                        }
                                        else if (tbSCCode.Text != "" && tbProSCCode.Text == "")
                                        {
                                            sc = tbSCCode.Text;
                                        }
                                        else if (tbSCCode.Text != "" && tbProSCCode.Text != "")
                                        {
                                            sc = tbSCCode.Text+","+tbProSCCode.Text;
                                        }

                                        string[] str = sc.Split(',');

                                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                        SqlCommand cmd = new SqlCommand("update DDEFeeInstruments set IType=@IType,INo=@INo,IDate=@IDate,IBN=@IBN,TotalAmount=@TotalAmount,SCMode=@SCMode,SCCode=@SCCode,AllotedFeeHeads=@AllotedFeeHeads,Remark=@Remark where IID='" + lblIID.Text + "'", con);

                                        cmd.Parameters.AddWithValue("@IType", ddlistDraftType.SelectedItem.Value);
                                        cmd.Parameters.AddWithValue("@INo", tbDNo.Text);
                                        cmd.Parameters.AddWithValue("@IDate", idate);
                                        cmd.Parameters.AddWithValue("@IBN", ddlistIBN.SelectedItem.Text.ToUpper());
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
                                        cmd.Parameters.AddWithValue("@AllotedFeeHeads", fhs);
                                        cmd.Parameters.AddWithValue("@Remark", Convert.ToInt32(ddlistRemark.SelectedItem.Value));

                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();


                                        string dcdate = ddlistDDYear.SelectedItem.Value + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Value;



                                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                        SqlDataReader dr1;
                                        SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession ", con1);
                                        con1.Open();
                                        dr1 = cmd1.ExecuteReader();
                                        while (dr1.Read())
                                        {

                                            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                            SqlCommand cmd2 = new SqlCommand("update [DDEFeeRecord_" + dr1["AcountSession"].ToString() + "] set PaymentMode=@PaymentMode,DCNumber=@DCNumber,DCDate=@DCDate,IBN=@IBN,TotalDCAmount=@TotalDCAmount where DCNumber='" + tbDNo.Text + "' and DCDate='" + dcdate + "' and IBN='" + ddlistIBN.SelectedItem.Text + "' ", con2);

                                            cmd2.Parameters.AddWithValue("@PaymentMode", ddlistDraftType.SelectedItem.Value);
                                            cmd2.Parameters.AddWithValue("@DCNumber", tbDNo.Text);
                                            cmd2.Parameters.AddWithValue("@DCDate", dcdate);
                                            cmd2.Parameters.AddWithValue("@IBN", ddlistIBN.SelectedItem.Text);
                                            cmd2.Parameters.AddWithValue("@TotalDCAmount", tbTotalAmount.Text);

                                            cmd2.Connection = con2;
                                            con2.Open();
                                            cmd2.ExecuteNonQuery();
                                            con2.Close();
                                        }
                                        con1.Close();
            



                                        Log.createLogNow("Update", "Updated a Fee Instrument '" + ddlistDraftType.SelectedItem.Text + " with No. '" + tbDNo.Text + "'", Convert.ToInt32(Session["ERID"]));
                                        pnlData.Visible = false;
                                        lblMSG.Text = "Instrument has been updated successfully";
                                        pnlMSG.Visible = true;
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
                                else
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = "Please select any Fee Head.";
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
                        lblMSG.Text = "Please select any 'REMARK'";
                        pnlMSG.Visible = true;
                        btnOK.Text = "OK";
                        btnOK.Visible = true;
                    }

                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Please select any 'INSTRUMENT TYPE'";
                    pnlMSG.Visible = true;
                    btnOK.Text = "OK";
                    btnOK.Visible = true;
                } 
            }
        }

        private bool valisSCCodes()
        {
            bool val = false;
            string valid = "NA";
           
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

            return val;

        }

        private string calculateAFH()
        {
            string fhs = "";

            
                foreach (DataListItem dli in dtlistFeeHeads.Items)
                {
                    CheckBox cb = (CheckBox)dli.FindControl("cbChecked");
                    Label lbl = (Label)dli.FindControl("lblFHID");

                    if (cb.Checked)
                    {
                        if (fhs == "")
                        {
                            fhs = lbl.Text;
                        }
                        else
                        {
                            fhs = fhs + "," + lbl.Text;
                        }
                    }
                }
                
           

            return fhs;
        }

       
    }
}