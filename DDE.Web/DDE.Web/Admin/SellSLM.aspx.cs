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
    public partial class SellSLM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 100))
            {

                if (!IsPostBack)
                {
                    PopulateDDList.populateSLMSaleParty(ddlistParty);
                    PopulateDDList.populateSLMCodes(ddlistSLM);
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if(ddlistParty.SelectedItem.Text!="--SELECT ONE--")
            {
           
                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("SLMID");
                DataColumn dtcol3 = new DataColumn("SLMCode");
                DataColumn dtcol4 = new DataColumn("Title");
                DataColumn dtcol5 = new DataColumn("Quantity");
                DataColumn dtcol6 = new DataColumn("Rate");
                DataColumn dtcol7 = new DataColumn("Amount");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);

                int i = 1;



                foreach (DataListItem dli in dtlistSLM.Items)
                {
                    Label slmid = (Label)dli.FindControl("lblSLMID");
                    Label slmcode = (Label)dli.FindControl("lblSLMCode");
                    Label title = (Label)dli.FindControl("lblTitle");
                    TextBox quantity = (TextBox)dli.FindControl("tbQuantity");
                    Label rate = (Label)dli.FindControl("lblRate");
                    Label amount = (Label)dli.FindControl("lblAmount");


                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["SLMID"] = Convert.ToString(slmid.Text);
                    drow["SLMCode"] = Convert.ToString(slmcode.Text);
                    drow["Title"] = Convert.ToString(title.Text);
                    drow["Quantity"] = Convert.ToString(quantity.Text);
                    drow["Rate"] = Convert.ToString(rate.Text);
                    drow["Amount"] = Convert.ToString(amount.Text);

                    dt.Rows.Add(drow);

                    i = i + 1;

                }
                if (dt.Rows.Count > 0)
                {
                    if (!(dt.AsEnumerable().Where(c => c.Field<string>("SLMID").Equals(ddlistSLM.SelectedItem.Value)).Count() > 0))
                    {

                        DataRow drow1 = dt.NewRow();

                        drow1["SNo"] = i;
                        string[] slmdetails = SLM.findSLMDetails(Convert.ToInt32(ddlistSLM.SelectedItem.Value));
                        drow1["SLMID"] = ddlistSLM.SelectedItem.Value;
                        drow1["SLMCode"] = slmdetails[0];
                        drow1["Title"] = slmdetails[4];
                        drow1["Quantity"] = 0;
                        drow1["Rate"] = slmdetails[5];
                        drow1["Amount"] = 0;
                        dt.Rows.Add(drow1);
                    }
                }
                else
                {
                    DataRow drow1 = dt.NewRow();

                    drow1["SNo"] = i;
                    string[] slmdetails = SLM.findSLMDetails(Convert.ToInt32(ddlistSLM.SelectedItem.Value));
                    drow1["SLMID"] = ddlistSLM.SelectedItem.Value;
                    drow1["SLMCode"] = slmdetails[0];
                    drow1["Title"] = slmdetails[4];
                    drow1["Quantity"] = 0;
                    drow1["Rate"] = slmdetails[5];
                    drow1["Amount"] = 0;
                    dt.Rows.Add(drow1);
                }


                dtlistSLM.DataSource = dt;
                dtlistSLM.DataBind();

                pnlBill.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Please select any one party!!";
                pnlMSG.Visible = true;
            }
              

          
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            if (btnLock.Text == "Lock Details")
            {
                int totalquan = 0;
                double totalamount = 0;
                string error = "";
                foreach (DataListItem dli in dtlistSLM.Items)
                {

                    TextBox quantity = (TextBox)dli.FindControl("tbQuantity");
                    Label rate = (Label)dli.FindControl("lblRate");
                    Label amount = (Label)dli.FindControl("lblAmount");
                    ImageButton del = (ImageButton)dli.FindControl("imgbtnDelete");
                    if (Convert.ToDouble(quantity.Text) == 0)
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry!! You can not fill Quantity '0'.Please Check";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        error = "Error";
                        break;

                    }
                    else
                    {

                        amount.Text = (Convert.ToDouble(quantity.Text) * Convert.ToDouble(rate.Text)).ToString();
                        amount.Enabled = false;
                        rate.Enabled = false;
                        del.Visible = false; ;
                        quantity.Enabled = false;

                        totalquan = totalquan + Convert.ToInt32(quantity.Text);
                        totalamount = totalamount + Convert.ToDouble(amount.Text);
                    }

                    btnAdd.Visible = false;

                }
                if (error == "")
                {
                    lblTotalSLM.Text = totalquan.ToString();
                    lblTotalAmount.Text = totalamount.ToString();
                    btnLock.Text = "Edit";
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    DataTable dt=findTable();
                    Session["FinalTable"] = dt;
                }
            }
            else if (btnLock.Text == "Edit")
            {

                foreach (DataListItem dli in dtlistSLM.Items)
                {

                    TextBox quantity = (TextBox)dli.FindControl("tbQuantity");

                    ImageButton del = (ImageButton)dli.FindControl("imgbtnDelete");

                    quantity.Enabled = true;

                    del.Visible = true;

                }

                lblTotalSLM.Text = "";
                lblTotalAmount.Text = "";
                btnAdd.Visible = true;
                btnLock.Text = "Lock Details";
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
            }
        }

        private DataTable findTable()
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMID");
            DataColumn dtcol3 = new DataColumn("SLMCode");
            DataColumn dtcol4 = new DataColumn("Title");
            DataColumn dtcol5 = new DataColumn("Quantity");
            DataColumn dtcol6 = new DataColumn("Rate");
            DataColumn dtcol7 = new DataColumn("Amount");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);


            int i = 1;



            foreach (DataListItem dli in dtlistSLM.Items)
            {
                Label slmid = (Label)dli.FindControl("lblSLMID");
                Label slmcode = (Label)dli.FindControl("lblSLMCode");
                Label title = (Label)dli.FindControl("lblTitle");
                TextBox quantity = (TextBox)dli.FindControl("tbQuantity");
                Label rate = (Label)dli.FindControl("lblRate");
                Label amount = (Label)dli.FindControl("lblAmount");


                DataRow drow = dt.NewRow();

                drow["SNo"] = i;
                drow["SLMID"] = Convert.ToString(slmid.Text);
                drow["SLMCode"] = Convert.ToString(slmcode.Text);
                drow["Title"] = Convert.ToString(title.Text);
                drow["Quantity"] = Convert.ToString(quantity.Text);
                drow["Rate"] = Convert.ToString(rate.Text);
                drow["Amount"] = Convert.ToString(amount.Text);

                dt.Rows.Add(drow);

                i = i + 1;

            }

            return dt;
        }

        protected void dtlistSLM_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("SLMID");
                DataColumn dtcol3 = new DataColumn("SLMCode");
                DataColumn dtcol4 = new DataColumn("Title");
                DataColumn dtcol5 = new DataColumn("Quantity");
                DataColumn dtcol6 = new DataColumn("Rate");
                DataColumn dtcol7 = new DataColumn("Amount");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);


                int i = 1;



                foreach (DataListItem dli in dtlistSLM.Items)
                {
                    Label slmid = (Label)dli.FindControl("lblSLMID");
                    Label slmcode = (Label)dli.FindControl("lblSLMCode");
                    Label title = (Label)dli.FindControl("lblTitle");
                    TextBox quantity = (TextBox)dli.FindControl("tbQuantity");
                    Label rate = (Label)dli.FindControl("lblRate");
                    Label amount = (Label)dli.FindControl("lblAmount");


                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["SLMID"] = Convert.ToString(slmid.Text);
                    drow["SLMCode"] = Convert.ToString(slmcode.Text);
                    drow["Title"] = Convert.ToString(title.Text);
                    drow["Quantity"] = Convert.ToString(quantity.Text);
                    drow["Rate"] = Convert.ToString(rate.Text);
                    drow["Amount"] = Convert.ToString(amount.Text);

                    dt.Rows.Add(drow);

                    i = i + 1;

                }

                int index = Convert.ToInt32(e.Item.ItemIndex);


                dt.Rows[index].Delete();
                dt.AcceptChanges();


                dtlistSLM.EditItemIndex = -1;
                dtlistSLM.DataSource = dt;
                dtlistSLM.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["PartyID"] = ddlistParty.SelectedItem.Value;
            Session["PartyName"] = ddlistParty.SelectedItem.Text;
            Session["TotalSLM"] = lblTotalSLM.Text;
            Session["TotalAmount"] = lblTotalAmount.Text;
            Response.Redirect("SLMSellLetter.aspx");        
            
        }

      
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SellSLM.aspx");
        }

       

        

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
           
            pnlMSG.Visible = false;
        }

        protected void ddlistParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistParty.SelectedItem.Text != "--SELECT ONE--")
            {
                ddlistParty.Enabled = false;
            }
        }

      
    }
}