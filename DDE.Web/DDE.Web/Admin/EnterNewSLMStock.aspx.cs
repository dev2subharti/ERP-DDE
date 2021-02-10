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
    public partial class EnterNewSLMStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 93))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateSLMPublications(ddlistParty);
                    PopulateDDList.populateAllSLMCodes(ddlistSLMCode);
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
            if (ddlistSLMCode.SelectedItem.Value != "0")
            {
                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("SLMID");
                DataColumn dtcol3 = new DataColumn("SLMCode");
                DataColumn dtcol4 = new DataColumn("Title");
                DataColumn dtcol5 = new DataColumn("Qty");
                DataColumn dtcol6 = new DataColumn("Cost");
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
                    Label slmcode= (Label)dli.FindControl("lblSLMCode");
                    Label title = (Label)dli.FindControl("lblTitle");
                    TextBox qty = (TextBox)dli.FindControl("tbQty");
                    TextBox rate = (TextBox)dli.FindControl("tbRate");
                    Label amount = (Label)dli.FindControl("lblAmount");


                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["SLMID"] = Convert.ToString(slmid.Text);
                    drow["SLMCode"] = Convert.ToString(slmcode.Text);
                    drow["Title"] = Convert.ToString(title.Text);
                    drow["Qty"] = Convert.ToString(qty.Text);
                    drow["Cost"] = Convert.ToString(rate.Text);
                    drow["Amount"] = Convert.ToString(amount.Text);

                    dt.Rows.Add(drow);

                    i = i + 1;

                }
                if (dt.Rows.Count > 0)
                {
                    if (!(dt.AsEnumerable().Where(c => c.Field<string>("SLMID").Equals(ddlistSLMCode.SelectedItem.Value)).Count() > 0))
                    {
                        DataRow drow1 = dt.NewRow();

                        drow1["SNo"] = i;
                        drow1["SLMID"] = Convert.ToInt32(ddlistSLMCode.SelectedItem.Value);
                        drow1["SLMCode"] = Convert.ToString(ddlistSLMCode.SelectedItem.Text);
                        drow1["Title"] = Convert.ToString(SLM.findTitleBySLMID(Convert.ToInt32(ddlistSLMCode.SelectedItem.Value)));
                        drow1["Qty"] = 0;
                        drow1["Cost"] = 0;
                        drow1["Amount"] = 0;
                        dt.Rows.Add(drow1);

                    }

                }
                else
                {
                    DataRow drow1 = dt.NewRow();

                    drow1["SNo"] = i;
                    drow1["SLMID"] = Convert.ToInt32(ddlistSLMCode.SelectedItem.Value);
                    drow1["SLMCode"] = Convert.ToString(ddlistSLMCode.SelectedItem.Text);
                    drow1["Title"] = Convert.ToString(SLM.findTitleBySLMID(Convert.ToInt32(ddlistSLMCode.SelectedItem.Value)));
                    drow1["Qty"] = 0;
                    drow1["Cost"] = 0;
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
                pnlBill.Visible = false;
                lblMSG.Text = "Please select any SLM Code.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            if (btnLock.Text == "Lock Bill Details")
            {
                int totalqty = 0;
                double totalamount = 0;
                string error = "";
                foreach (DataListItem dli in dtlistSLM.Items)
                {
                    Label slmcode = (Label)dli.FindControl("lblSLMCode");
                    TextBox qty = (TextBox)dli.FindControl("tbQty");
                    TextBox rate = (TextBox)dli.FindControl("tbRate");
                    Label amount = (Label)dli.FindControl("lblAmount");
                    ImageButton del = (ImageButton)dli.FindControl("imgbtnDelete");
                    if (Convert.ToInt32(qty.Text) == 0)
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry!! You cant not fill Quantity '0' on SLM Code : "+slmcode.Text+". Please Check.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        error = "Error";
                        break;
                       
                    }
                    else
                    {

                        amount.Text = (Convert.ToDouble(qty.Text) * Convert.ToDouble(rate.Text)).ToString();
                        qty.Enabled = false;
                        rate.Enabled = false;
                        del.Visible = false; ;

                        totalqty = totalqty + Convert.ToInt32(qty.Text);
                        totalamount = totalamount + Convert.ToDouble(amount.Text);
                    }

                }
                if (error == "")
                {
                    lblTotalQty.Text = totalqty.ToString();
                    lblTotalAmount.Text = totalamount.ToString();
                    lblNetAmount.Text = (Convert.ToDouble(lblTotalAmount.Text) - ((Convert.ToDouble(lblTotalAmount.Text) * Convert.ToDouble(tbDiscount.Text))/100) + Convert.ToDouble(tbPostageCharge.Text)).ToString();

                    tbDiscount.Enabled = false;
                    tbPostageCharge.Enabled = false;
                    btnLock.Text = "Edit";
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
            }
            else if (btnLock.Text == "Edit")
            {
               
                foreach (DataListItem dli in dtlistSLM.Items)
                {

                    TextBox qty = (TextBox)dli.FindControl("tbQty");
                    TextBox rate = (TextBox)dli.FindControl("tbRate");
                    ImageButton del = (ImageButton)dli.FindControl("imgbtnDelete");
                  
                    qty.Enabled = true;
                    rate.Enabled = true;
                    del.Visible = true;

                }

                lblTotalQty.Text = "";
                lblTotalAmount.Text = "";
                lblNetAmount.Text = "";
                tbDiscount.Enabled = true;
                tbPostageCharge.Enabled = true;
                btnLock.Text = "Lock Bill Details";
                btnSubmit.Visible = false;
                btnCancel.Visible = false;            
            }
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
                DataColumn dtcol5 = new DataColumn("Qty");
                DataColumn dtcol6 = new DataColumn("Cost");
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
                    TextBox qty = (TextBox)dli.FindControl("tbQty");
                    TextBox rate = (TextBox)dli.FindControl("tbRate");
                    Label amount = (Label)dli.FindControl("lblAmount");


                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["SLMID"] = Convert.ToString(slmid.Text);
                    drow["SLMCode"] = Convert.ToString(slmcode.Text);
                    drow["Title"] = Convert.ToString(title.Text);
                    drow["Qty"] = Convert.ToString(qty.Text);
                    drow["Cost"] = Convert.ToString(rate.Text);
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
            string error;
            string billdate=ddlistBYear.SelectedItem.Text+"-"+ddlistBMonth.SelectedItem.Value+"-"+ddlistBDay.SelectedItem.Text;
            string orderdate = ddlistOYear.SelectedItem.Text + "-" + ddlistOMonth.SelectedItem.Value + "-" + ddlistODay.SelectedItem.Text;
            string challandate = ddlistCYear.SelectedItem.Text + "-" + ddlistCMonth.SelectedItem.Value + "-" + ddlistCDay.SelectedItem.Text;
            if (validEntries(out error))
            {
                int billid = SLM.enterBillDetails(tbBillNo.Text, billdate, tbONo.Text, orderdate, tbCNo.Text, challandate, Convert.ToInt32(ddlistParty.SelectedItem.Value), Convert.ToDouble(lblTotalAmount.Text), Convert.ToDouble(tbDiscount.Text), Convert.ToDouble(tbPostageCharge.Text), Convert.ToDouble(lblNetAmount.Text), Convert.ToInt32(Session["ERID"]));

                if (billid != 0)
                {
                    string slmentryerror;
                    string slmlistupdateerror;
                    int enterslmtrans = enterSLMEntryTransactions(billid, out slmentryerror);
                    int updateslmstock = updateslmst(out slmlistupdateerror);
                    if (slmentryerror == "" && slmlistupdateerror == "" && enterslmtrans == dtlistSLM.Items.Count && updateslmstock == dtlistSLM.Items.Count)
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Bill entered and the stock has been updated successfully !!";
                        pnlMSG.Visible = true;
                        btnOK.Visible = false;
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Bill entered but the stock could not be updated correctly. Please contact to ERP Developer.Error : " + slmentryerror + "," + slmlistupdateerror;
                        pnlMSG.Visible = true;
                        btnOK.Visible = false;
                    }
                    
                  
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Bill could not be entered. Please contact to ERP Developer.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = error;
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private int enterSLMEntryTransactions(int billid, out string slmentryerror)
        {
            slmentryerror = "";
            int result = 0;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet dataset = new DataSet();


            command.Connection = connection;
            command.CommandText = "SELECT * FROM DDESLMStockRegister";
            adapter.SelectCommand = command;
            adapter.Fill(dataset, "SLMSR");

            int counter = 0;

            foreach (DataListItem dli in dtlistSLM.Items)
            {
                Label slmid = (Label)dli.FindControl("lblSLMID");

                TextBox qty = (TextBox)dli.FindControl("tbQty");
                TextBox rate = (TextBox)dli.FindControl("tbRate");

                if (qty.Text != "0")
                {
                    DataRow row = dataset.Tables["SLMSR"].NewRow();
                    row["SLMID"] = Convert.ToInt32(slmid.Text);
                    row["BillID"] = billid;
                    row["LID"] = 0;
                    row["SLID"] = 0;
                    row["Description"] = "THROUGH BILL ENTRY";
                    row["TOT"] = DateTime.Now.ToString();
                    row["Rate"] = rate.Text;
                    row["Credit"] = Convert.ToInt32(qty.Text);
                    row["Debit"] = 0;
                    row["CurrentTotal"] = SLM.findCurrentStock(Convert.ToInt32(slmid.Text)) + Convert.ToInt32(qty.Text);

                    dataset.Tables["SLMSR"].Rows.Add(row);

                    counter = counter + 1;


                }

            }

            try
            {
                result = adapter.Update(dataset, "SLMSR");
                if (result == counter)
                {
                    slmentryerror = "";
                }

            }
            catch (SqlException ex)
            {
                slmentryerror = ex.Message;

            }

            return result;
        }

        private int updateslmst(out string slmlistupdateerror)
        {
            slmlistupdateerror = "";
            int counter = 0;
            try
            {
                foreach (DataListItem dli in dtlistSLM.Items)
                {
                    Label slmid = (Label)dli.FindControl("lblSLMID");

                    TextBox qty = (TextBox)dli.FindControl("tbQTY");
                    if (qty.Text != "0")
                    {
                        int updated = 0;
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDESLMMaster set PresentStock=@PresentStock where SLMID='" + slmid.Text + "' ", con);


                        cmd.Parameters.AddWithValue("@PresentStock", SLM.findCurrentStock(Convert.ToInt32(slmid.Text)) + Convert.ToInt32(qty.Text));


                        con.Open();
                        updated = cmd.ExecuteNonQuery();
                        con.Close();

                        counter = counter + 1;

                    }

                }
            }
            catch (Exception ex)
            {
                slmlistupdateerror = ex.Message;
            }


            return counter;
        }

        private bool validEntries(out string error)
        {
            error = "";
            bool valid = true;
            if (ddlistParty.SelectedItem.Text == "")
            {
                valid = false;
                error = "Please select any 'Party Name'";
            }
            return valid;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnterNewSLMStock.aspx");
        }

    

       

    }
}