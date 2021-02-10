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
    public partial class BlockUnblockInstrument : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
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

        private bool populateDCDetails(int sno)
        {
            bool ver = false;
            foreach (DataListItem dli in dtlistTotalInstruments.Items)
            {

                Label sn = (Label)dli.FindControl("lblSNo");
                Label itype = (Label)dli.FindControl("lblType");
                Label itypeno = (Label)dli.FindControl("lblTypeNo");
                Label ino = (Label)dli.FindControl("lblNo");
                Label idate = (Label)dli.FindControl("lblDate");
                Label itotalamount = (Label)dli.FindControl("lblTotalAmount");
                Label ibn = (Label)dli.FindControl("lblIBN");
                Label remark = (Label)dli.FindControl("lblRemark");
                Label verified = (Label)dli.FindControl("lblVerified");

                if (sno.ToString() == sn.Text)
                {

                    ddlistPaymentMode.SelectedItem.Selected = false;

                    ddlistPaymentMode.Items.FindByValue(itypeno.Text).Selected = true;

                    tbDNo.Text = ino.Text;
                    lblDNo.Text = ino.Text;

                    string dcdate = Convert.ToDateTime(idate.Text).ToString("yyyy-MM-dd");

                    ddlistDDDay.SelectedItem.Selected = false;
                    ddlistDDMonth.SelectedItem.Selected = false;
                    ddlistDDYear.SelectedItem.Selected = false;

                    ddlistDDDay.Items.FindByText(dcdate.Substring(8, 2)).Selected = true;
                    ddlistDDMonth.Items.FindByValue(dcdate.Substring(5, 2)).Selected = true;
                    ddlistDDYear.Items.FindByText(dcdate.Substring(0, 4)).Selected = true;


                    tbIBN.Text = ibn.Text;
                    lblIID.Text = Accounts.findIIDByInstrumentDetails(Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), lblDNo.Text, dcdate, ibn.Text).ToString();
                    bool locked = Accounts.isInstrumentLocked(Convert.ToInt32(lblIID.Text));

                    if (locked == true)
                    {
                        btnLock.Text = "Unlock";

                    }
                    else if (locked == false)
                    {
                        btnLock.Text = "Lock";

                    }
                    tbTotalAmount.Text = itotalamount.Text;
                    tbUsedAmount.Text = Accounts.findUsedAmountOfDraft(Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDNo.Text, dcdate, tbIBN.Text).ToString();
                    tbBalance.Text = (Convert.ToInt32(tbTotalAmount.Text) - Convert.ToInt32(tbUsedAmount.Text)).ToString();
                    tbRemark.Text = remark.Text;

                    if (tbRemark.Text != "")
                    {
                        lnkbtnUR.Visible = true;
                    }
                    else
                    {
                        lnkbtnUR.Visible = false;
                    }

                    if (verified.Text == "True")
                    {
                        ver = true;
                    }
                    else if (verified.Text == "False")
                    {
                        ver= false;
                    }
                    break;
                }
            }

          

            return ver;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (Accounts.instrumentExistByINo(tbDCNo.Text))
            {
                populateTotalInstruments();
                dtlistTotalInstruments.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record found with this draft no.";
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
            }
        }

        private void populateTotalInstruments()
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select IType,INo,IDate,IBN,TotalAmount,LockRemark,Verified from DDEFeeInstruments where INo='" + tbDCNo.Text + "' ", con);


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("Type");
            DataColumn dtcol3 = new DataColumn("TypeNo");
            DataColumn dtcol4 = new DataColumn("No");
            DataColumn dtcol5 = new DataColumn("Date");
            DataColumn dtcol6 = new DataColumn("TotalAmount");
            DataColumn dtcol7 = new DataColumn("IBN");
            DataColumn dtcol8 = new DataColumn("LockRemark");
            DataColumn dtcol9 = new DataColumn("Verified");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            con.Open();
            dr = cmd.ExecuteReader();
            int i = 1;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr["IType"]));
                    drow["TypeNo"] = Convert.ToInt32(dr["IType"]);
                    drow["No"] = Convert.ToString(dr["INo"]);
                    drow["Date"] = Convert.ToDateTime(dr["IDate"]).ToString("dd MMMM yyyy");
                    drow["TotalAmount"] = Convert.ToInt32(dr["TotalAmount"]);
                    drow["IBN"] = Convert.ToString(dr["IBN"]);
                    drow["LockRemark"] = Convert.ToString(dr["LockRemark"]);
                    drow["Verified"] = Convert.ToString(dr["Verified"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }
            con.Close();




            dtlistTotalInstruments.DataSource = dt;
            dtlistTotalInstruments.DataBind();


        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "OK")
            {
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else
            {
                Response.Redirect("BlockUnblockInstrument.aspx");
            }
        } 

        protected void dtlistTotalInstruments_ItemCommand(object source, DataListCommandEventArgs e)
        {
            bool verified= populateDCDetails(Convert.ToInt32(e.CommandArgument));
            if (verified == true)
            {
                pnlDCDetail.Visible = true;
                pnlSearch.Visible = false;
                dtlistTotalInstruments.Visible = false;
               
            }
            else if (verified == false)
            {
                pnlDCDetail.Visible = false;
                pnlSearch.Visible = false;
                dtlistTotalInstruments.Visible = false;
                lblMSG.Text = "Sorry !! This instrument is not verified.";
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
               
            }
           
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            if (btnLock.Text == "Lock")
            {
                string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Text + "-" + ddlistDDDay.SelectedItem.Text;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEfeeInstruments set Lock=@Lock,LockRemark=@LockRemark where IID='" + lblIID.Text + "' ", con);
                cmd.Parameters.AddWithValue("@Lock", "True");
                cmd.Parameters.AddWithValue("@LockRemark", tbRemark.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Instrument Locked", "Instrument with details : '" + tbDCNo.Text + "'-'" + ddlistPaymentMode.SelectedItem.Text + "'-'" + dcdate + "' has been locked", Convert.ToInt32(Session["ERID"].ToString()));

                btnLock.Visible = false;

                pnlSearch.Visible = false;
                pnlDCDetail.Visible = false;
                lblMSG.Text = "Instrument has been locked successfully !!";
                pnlMSG.Visible = true;
                btnOK.Text = "Lock Another Instrument";
                btnOK.Visible = true;
            }
            else if (btnLock.Text == "Unlock")
            {
                string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Text + "-" + ddlistDDDay.SelectedItem.Text;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEfeeInstruments set Lock=@Lock,LockRemark=@LockRemark where IID='" + lblIID.Text + "' ", con);
                cmd.Parameters.AddWithValue("@Lock", "False");
                cmd.Parameters.AddWithValue("@LockRemark", tbRemark.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Instrument Unlocked", "Instrument with details : '" + tbDCNo.Text + "'-'" + ddlistPaymentMode.SelectedItem.Text + "'-'" + dcdate + "' has been unlocked", Convert.ToInt32(Session["ERID"].ToString()));

                btnLock.Visible = false;

                pnlSearch.Visible = false;
                pnlDCDetail.Visible = false;
                lblMSG.Text = "Instrument has been unlocked successfully !!";
                pnlMSG.Visible = true;
                btnOK.Text = "Unlock Another Instrument";
                btnOK.Visible = true;
            }
        }

        protected void lnkbtnUR_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEfeeInstruments set LockRemark=@LockRemark where IID='" + lblIID.Text + "' ", con);
          
            cmd.Parameters.AddWithValue("@LockRemark", tbRemark.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Update", "Updated Remark on Fee Instrument : '" + tbDCNo.Text + "'-'" + ddlistPaymentMode.SelectedItem.Text + "'-' with '"+tbRemark.Text+"'", Convert.ToInt32(Session["ERID"].ToString()));

            btnLock.Visible = false;

            pnlSearch.Visible = false;
            pnlDCDetail.Visible = false;
            lblMSG.Text = "Remark has been updated successfully !!";
            pnlMSG.Visible = true;
            btnOK.Visible = false;
        }
    }
}