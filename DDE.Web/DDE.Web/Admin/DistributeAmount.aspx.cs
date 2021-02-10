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

namespace DDE.Web.Admin
{
    public partial class DistributeAmount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["IID"] != null)
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79))
                {
                    if (!IsPostBack)
                    {
                        
                        pnlSearch.Visible = false;
                        populateDCDetails(Convert.ToInt32(Request.QueryString["IID"]));
                     
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
            else
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 75))
                {
                    if (!IsPostBack)
                    {
                       
                        pnlSearch.Visible = true;
                       
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
        }   

        private void populateFeeHeads(string fhs)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FHID");
            DataColumn dtcol3 = new DataColumn("FeeHead");
            DataColumn dtcol4 = new DataColumn("Amount");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from DDEFeeHead where FHID in ("+fhs+") order by FHID";


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
                    drow["Amount"] = "";


                    dt.Rows.Add(drow);
                    i = i + 1;
                }
            }
            


            dtlistFeeHeads.DataSource = dt;
            dtlistFeeHeads.DataBind();

            con.Close();





        }

        protected void btnDistribute_Click(object sender, EventArgs e)
        {
            if (!(Accounts.isInstrumentLocked(Convert.ToInt32(lblIID.Text))))
            {
                if (Request.QueryString["IID"] != null)
                {
                    try
                    {
                        int amount = 0;

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand();



                        if (tbBalance.Text == "")
                        {
                            cmd.Parameters.AddWithValue("@Balance", 0);
                            amount = amount + 0;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Balance", tbBalance.Text);
                            amount = amount + Convert.ToInt32(tbBalance.Text);
                        }

                        string fhs = "";
                        bool ok = false;
                        foreach (DataListItem dli in dtlistFeeHeads.Items)
                        {
                            TextBox am = (TextBox)dli.FindControl("tbAmount");
                            Label fhid = (Label)dli.FindControl("lblFHID");

                            if (am.Text != "")
                            {

                                amount = amount + Convert.ToInt32(am.Text);
                                cmd.Parameters.AddWithValue("@FH" + fhid.Text, Convert.ToInt32(am.Text));

                                if (fhs == "")
                                {
                                    fhs = "FH" + fhid.Text + "=" + "@FH" + fhid.Text;
                                }
                                else
                                {
                                    fhs = fhs + "," + "FH" + fhid.Text + "=" + "@FH" + fhid.Text;

                                }

                                

                                ok = true;
                            }
                            else
                            {
                                ok = false;
                                break;
                            }


                        }

                        cmd.CommandText = "update DDEFeeInstruments set Balance=@Balance," + fhs + " where IID='" + lblIID.Text + "'";
                        cmd.Connection = con;



                        if (amount == Convert.ToInt32(tbTotalAmount.Text))
                        {
                            if (ok == true)
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                Log.createLogNow("Update", "Updated the distribution of amount of  '" + tbDType.Text + "' with No. '" + tbDNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                                pnlData.Visible = false;
                                lblMSG.Text = "Amount distribution has been updated successfully !!";
                                pnlMSG.Visible = true;

                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Please fill amount in all fee heads.";
                                pnlMSG.Visible = true;
                                btnOK.Text = "OK";
                                btnOK.Visible = true;
                            }


                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! Distributed amount is not equal to 'Total Amount' of Instrument";
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
                    try
                    {
                        int amount = 0;

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand();


                        cmd.Parameters.AddWithValue("@AmountAlloted", "True");
                        cmd.Parameters.AddWithValue("@AllotedOn", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@AllotedBy", Convert.ToInt32(Session["ERID"]));
                        if (tbBalance.Text == "")
                        {
                            cmd.Parameters.AddWithValue("@Balance", 0);
                            amount = amount + 0;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Balance", tbBalance.Text);
                            amount = amount + Convert.ToInt32(tbBalance.Text);
                        }

                        string fhs = "";
                        bool ok = false;
                        foreach (DataListItem dli in dtlistFeeHeads.Items)
                        {
                            TextBox am = (TextBox)dli.FindControl("tbAmount");
                            Label fhid = (Label)dli.FindControl("lblFHID");

                            if (am.Text != "")
                            {

                                amount = amount + Convert.ToInt32(am.Text);
                                cmd.Parameters.AddWithValue("@FH" + fhid.Text, Convert.ToInt32(am.Text));

                                if (fhs == "")
                                {
                                    fhs = "FH" + fhid.Text + "=" + "@FH" + fhid.Text;
                                }
                                else
                                {
                                    fhs = fhs + "," + "FH" + fhid.Text + "=" + "@FH" + fhid.Text;

                                }

                                ok = true;
                            }
                            else
                            {
                                ok = false;
                                break;
                            }

                        }

                        cmd.CommandText = "update DDEFeeInstruments set AmountAlloted=@AmountAlloted,AllotedOn=@AllotedOn,AllotedBy=@AllotedBy,Balance=@Balance," + fhs + " where IID='" + lblIID.Text + "'";
                        cmd.Connection = con;



                        if (amount == Convert.ToInt32(tbTotalAmount.Text))
                        {
                            if (ok == true)
                            {

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                Log.createLogNow("Distributed Amount", "Distributed the amount of  '" + tbDType.Text + "' with No. '" + tbDCNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                                pnlData.Visible = false;
                                lblMSG.Text = "Amount has been distributed successfully !!";
                                pnlMSG.Visible = true;
                                btnOK.Text = "Distribute More Instruments";
                                btnOK.Visible = true;
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Please fill amount in all fee heads.";
                                pnlMSG.Visible = true;
                                btnOK.Text = "OK";
                                btnOK.Visible = true;
                            }


                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! Distributed amount is not equal to 'Total Amount' of Instrument";
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "OK")
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;              
                btnOK.Visible = false;
            }

            else if(btnOK.Text == "Distribute More Instruments")
            {
                Response.Redirect("DistributeAmount.aspx");
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {


            populateTotalInstruments();
            setStatus();
               
           
        }

        private void setStatus()
        {
            foreach (DataListItem dli in dtlistTotalInstruments.Items)
            {
                Label ver = (Label)dli.FindControl("lblVerified");
                LinkButton dis = (LinkButton)dli.FindControl("lnkbtnShow");
                if (ver.Text == "True")
                {
                    dis.Enabled = true;
                }
                else if (ver.Text == "True")
                {
                    dis.Enabled = false;
                    dis.Text = "Not Verified";
                }
                else
                {
                    dis.Enabled = false;
                    dis.Text = "Not Verified";
                }
            }
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
            DataColumn dtcol9 = new DataColumn("Verified");
            DataColumn dtcol10 = new DataColumn("Distributed");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);

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
                    drow["Verified"] = Convert.ToString(dr1["Verified"]);
                    drow["Distributed"] = Convert.ToString(dr1["AmountAlloted"]);
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
            string fhs = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where IID='"+iid+"'", con);
            SqlDataReader dr;

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
                fhs = dr["AllotedFeeHeads"].ToString();
                populateFeeHeads(fhs);
                        
                foreach (DataListItem dli in dtlistFeeHeads.Items)
                {
                    TextBox am = (TextBox)dli.FindControl("tbAmount");
                    Label fhid = (Label)dli.FindControl("lblFHID");

                    am.Text = dr["FH" + fhid.Text].ToString();
                    if (Convert.ToInt32(fhid.Text) == 31 && Convert.ToInt32(am.Text)!=0)
                    {
                        am.Enabled = false;
                    }
                }
            }


            con.Close();

            dtlistFeeHeads.Visible = true;
            btnDistribute.Visible = true;
            pnlBalance.Visible = true;
            pnlDCDetail.Visible = true;             
          
        }    

        protected void lnkbtnBalance_Click(object sender, EventArgs e)
        {
            int amount = 0;

            try
            {

                foreach (DataListItem dli in dtlistFeeHeads.Items)
                {
                    TextBox am = (TextBox)dli.FindControl("tbAmount");
                    if (am.Text != "")
                    {
                        amount = amount + Convert.ToInt32(am.Text);
                    }
                }

                tbBalance.Text = (Convert.ToInt32(tbTotalAmount.Text) - amount).ToString();

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

        protected void dtlistTotalInstruments_ItemCommand(object source, DataListCommandEventArgs e)
        {
            populateDCDetails(Convert.ToInt32(e.CommandArgument));
            dtlistTotalInstruments.Visible = false;
            pnlSearch.Visible = false;
            if (e.CommandName == "True")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 75))
                {
                    btnDistribute.Text = "Update Distribution";
                    btnDistribute.Enabled = true;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79))
                {
                    btnDistribute.Text = "Update Distribution";
                    btnDistribute.Enabled = true;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 75))
                {
                    btnDistribute.Text = "Already Distributed";
                    btnDistribute.Enabled = false;
                }
            }
            else if (e.CommandName == "False")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 75))
                {
                    btnDistribute.Text = "Distribute";
                    btnDistribute.Enabled = true;
                    btnDistribute.Visible = true;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79))
                {
                   
                    btnDistribute.Visible = false;
                }
                else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 75))
                {

                    btnDistribute.Text = "Distribute";
                    btnDistribute.Enabled = true;
                    btnDistribute.Visible = true;
                }
            }
        }
    }
}