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
    public partial class SLMSellLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 100))
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["LNo"] != null)
                    {
                        string[] slmletter = SLM.findSLMSaleLetterDetails(Convert.ToInt32(Request.QueryString["LNo"]));
                        lblRefNo.Text = "Ref.No. DDE/SVSU/" + slmletter[1].ToString().Substring(6, 4) + "/IS/" + Request.QueryString["LNo"];
                        lblTo.Text =SLM.findSLMSalePartyDetailsByID(Convert.ToInt32(slmletter[0]));
                        lblDate.Text = slmletter[1].ToString();                     
                        populateSLMListFromTransactions(Convert.ToInt32(Request.QueryString["LNo"]));
                        btnPrint.Visible = false;

                    }
                    else
                    {
                        lblTo.Text = SLM.findSLMSalePartyDetailsByID(Convert.ToInt32(Session["PartyID"]));
                        lblSubject.Text = "Subject : Issue of SLMs for "+Session["PartyName"];
                        populateSet();
                    }

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

        private void populateSLMListFromTransactions(int slid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDESLMStockRegister.SLMID,DDESLMStockRegister.Rate,DDESLMStockRegister.Debit,DDESLMMaster.SLMCode,DDESLMMaster.Title,DDESLMMaster.Lang from DDESLMStockRegister inner join DDESLMMaster on DDESLMStockRegister.SLMID=DDESLMMaster.SLMID where DDESLMStockRegister.SLID='" + slid + "'", con);
            SqlDataReader dr;



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
            int counter = 0;
            double totalamount = 0;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["SLMID"] = Convert.ToString(dr["SLMID"]);
                    drow["SLMCode"] = Convert.ToString(dr["SLMCode"]);          
                    drow["Title"] = Convert.ToString(dr["Title"]);         
                    drow["Quantity"] = Convert.ToString(dr["Debit"]);
                    drow["Rate"] = Convert.ToDouble(dr["Rate"]);
                    drow["Amount"] = Convert.ToDouble(drow["Rate"]) * Convert.ToDouble(drow["Quantity"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                    counter = counter + Convert.ToInt32(drow["Quantity"]);
                    totalamount = totalamount + Convert.ToDouble(drow["Amount"]);

                }

            }

            con.Close();
            dtlistSLM.DataSource = dt;
            dtlistSLM.DataBind();

            lblTotalSLM.Text = counter.ToString();
            lblTotalAmount.Text = totalamount.ToString();


        }



        private void populateSet()
        {
            DataTable slmlist = (DataTable)Session["FinalTable"];
            dtlistSLM.DataSource = slmlist;
            dtlistSLM.DataBind();         
            lblTotalSLM.Text = Session["TotalSLM"].ToString();
            lblTotalAmount.Text = Session["TotalAmount"].ToString();
        }

     

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            int slid = SLM.generateLetterForSaleSLM(Convert.ToInt32(Session["PartyID"]));          
            if (slid != 0)
            {
                string error;
                int enterslmtrans = enterSLMSaleTransactions(slid, out error);
                int updateslmstock = updateslmst();
                if (error == "" && enterslmtrans == dtlistSLM.Items.Count && updateslmstock == dtlistSLM.Items.Count)
                {
                    lblLNo.Text = slid.ToString();
                    lblRefNo.Text = "Ref.No. DDE/SVSU/" + DateTime.Now.ToString("yyyy") + "/IS/" + lblLNo.Text;
                    lblDate.Text = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");
                    Log.createLogNow("Generate SLM Sale Letter", "Generated SLM Sale Letter with No : " + lblLNo.Text + " for '"+lblTo.Text+"'", Convert.ToInt32(Session["ERID"].ToString()));
                    btnPrint.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Letter generated but the SLM could not be deducted from SLM Stock Register.Please contact to ERP Developer.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }



            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Letter generated but the SLM could not be deducted from SLM Stock Register.Please contact to ERP Developer.";
                pnlMSG.Visible = true;
                btnOK.Visible = false;
            }
                   
               
           
        }

        private int updateslmst()
        {

            int counter = 0;
            foreach (DataListItem dli in dtlistSLM.Items)
            {
                Label slmid = (Label)dli.FindControl("lblSLMID");

                Label qty = (Label)dli.FindControl("lblQuantity");
                if (qty.Text != "0")
                {
                    int updated = 0;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDESLMMaster set PresentStock=@PresentStock where SLMID='" + slmid.Text + "' ", con);


                    cmd.Parameters.AddWithValue("@PresentStock", SLM.findCurrentStock(Convert.ToInt32(slmid.Text)) - Convert.ToInt32(qty.Text));


                    con.Open();
                    updated = cmd.ExecuteNonQuery();
                    con.Close();

                    counter = counter + 1;

                }

            }

            return counter;
        }
 

        private int enterSLMSaleTransactions(int slid, out string error)
        {

            error = "";
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
                Label rate = (Label)dli.FindControl("lblRate");
                Label qty = (Label)dli.FindControl("lblQuantity");
                if (qty.Text != "0")
                {
                    DataRow row = dataset.Tables["SLMSR"].NewRow();
                    row["SLMID"] = Convert.ToInt32(slmid.Text);
                    row["BillID"] = 0; ;
                    row["LID"] = 0;
                    row["SLID"] = slid;
                    row["Description"] = "SOLD ON LETTER";
                    row["TOT"] = DateTime.Now.ToString();
                    row["Rate"] = Convert.ToDouble(rate.Text);
                    row["Credit"] = 0;
                    row["Debit"] = Convert.ToInt32(qty.Text);
                    row["CurrentTotal"] = SLM.findCurrentStock(Convert.ToInt32(slmid.Text)) - Convert.ToInt32(qty.Text);
                    dataset.Tables["SLMSR"].Rows.Add(row);
                    counter = counter + 1;

                }

            }

            try
            {
                int result = adapter.Update(dataset, "SLMSR");
                if (result == counter)
                {
                    error = "";
                }

            }
            catch (SqlException ex)
            {
                pnlData.Visible = false;
                error = ex.Message;
                pnlMSG.Visible = true;
            }


            return counter;
        }



        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
           
        }
    }
}