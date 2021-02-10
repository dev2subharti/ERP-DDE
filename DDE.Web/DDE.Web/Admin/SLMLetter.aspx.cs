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
    public partial class SLMLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95))
            {
                if (!IsPostBack)
                {
                   
                    if (Request.QueryString["LNo"] != null)
                    {
                        string[] slmletter = SLM.findSLMLetterDetails(Convert.ToInt32(Request.QueryString["LNo"]));
                        lblRefNo.Text = "Ref.No. DDE/SVSU/" +slmletter[2].ToString().Substring(6,4) + "/SLM/" + Request.QueryString["LNo"];
                       
                        lblDate.Text = slmletter[2].ToString();
                        lblTo.Text = FindInfo.findSCAddressingDetails(slmletter[0].ToString());
                        populateSLMListFromTransactions(Convert.ToInt32(Request.QueryString["LNo"]));
                        btnPrint.Visible = false;
                       
                    }
                    else 
                    {

                        lblTo.Text = FindInfo.findSCAddressingDetails(Session["SCCode"].ToString());

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

        private void populateSLMListFromTransactions(int lid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDESLMStockRegister.SLMID,DDESLMStockRegister.Debit,DDESLMMaster.SLMCode,DDESLMMaster.Title,DDESLMMaster.Lang from DDESLMStockRegister inner join DDESLMMaster on DDESLMStockRegister.SLMID=DDESLMMaster.SLMID where DDESLMStockRegister.LID='" + lid + "' order by DDESLMMaster.SLMCode", con);
            SqlDataReader dr;
           


            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMID");
            DataColumn dtcol3 = new DataColumn("SLMCode");
            DataColumn dtcol4 = new DataColumn("Dual");
            DataColumn dtcol5 = new DataColumn("GroupID");
            DataColumn dtcol6 = new DataColumn("Title");
            DataColumn dtcol7 = new DataColumn("Lang");
            DataColumn dtcol8 = new DataColumn("PresentStock");
            DataColumn dtcol9 = new DataColumn("Quantity");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            int i=1;
            int counter = 0;
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
                    drow["Dual"] = "";
                    drow["GroupID"] = "";
                    drow["Title"] = Convert.ToString(dr["Title"]);
                    drow["Lang"] = Convert.ToString(dr["Lang"]);
                    drow["PresentStock"] = "";
                    drow["Quantity"] = Convert.ToString(dr["Debit"]);
                   

                    dt.Rows.Add(drow);
                    i = i + 1;
                    counter = counter + Convert.ToInt32(drow["Quantity"]);

                }
                
            }

            con.Close();
            dtlistSLM.DataSource = dt;
            dtlistSLM.DataBind();

            lblTotalSLM.Text = counter.ToString();


        }

        private void populateSet()
        {
            //DataTable dt = (DataTable)Session["SLMSet"];
            //dtlistShowSet.DataSource = dt;
            //dtlistShowSet.DataBind();

            DataTable slmlist = (DataTable)Session["SLMList"];
            dtlistSLM.DataSource = slmlist;
            dtlistSLM.DataBind();
            setValidation();
            lblTotalSLM.Text = Session["TotalSLM"].ToString();
        }

        private void setValidation()
        {
            foreach (DataListItem dli in dtlistSLM.Items)
            {
                Label slmid = (Label)dli.FindControl("lblSLMID");
                Label groupid = (Label)dli.FindControl("lblGroupID");
                Label dual = (Label)dli.FindControl("lblDual");
                Label ps = (Label)dli.FindControl("lblPresentStock");
                TextBox qty = (TextBox)dli.FindControl("tbQTY");


                if (Convert.ToInt32(qty.Text) >Convert.ToInt32(ps.Text))
                {
                    qty.BackColor = System.Drawing.Color.Red;
                   
                   
                }
                if (dual.Text == "True")
                {
                    qty.BackColor = System.Drawing.Color.Yellow;
                }
                if (Convert.ToInt32(groupid.Text) > 0)
                {
                    qty.Enabled = true;
                }
               

            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
                int lno;
        
                if (!(SLM.isSLMLetterGenerated(Session["SLMRIDS"].ToString(), out lno)))
                {                                  
                    if (findCurrentTotalSLM() == Convert.ToInt32(Session["TotalSLM"]))
                    {
                        string er;
                        if (correctEntries(out er))
                        {
                            populateNonZeroSLM();
                            int lid = SLM.generateLetterForIssuingSLM(Session["SLMRIDS"].ToString(), Session["SCCode"].ToString());
                            int updatedrecord = SLM.updateLNoToSLMRIDS(lid, Session["SLMRIDS"].ToString());
                            string[] str = Session["SLMRIDS"].ToString().Split(',');
                            int count = str.Length;
                            string error;

                            if ((updatedrecord == count) && (lid != 0))
                            {
                                int enterslmtrans = enterSLMIssueTransactions(lid, out error);
                                int updateslmstock = updateslmst();
                                if (error == "" && enterslmtrans == dtlistSLM.Items.Count && updateslmstock == dtlistSLM.Items.Count)
                                {
                                    lblLNo.Text = lid.ToString();
                                    lblRefNo.Text = "Ref.No. DDE/SVSU/" + DateTime.Now.ToString("yyyy") + "/SLM/" + lblLNo.Text;
                                    lblDate.Text = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");

                                    Log.createLogNow("Generate SLM Letter", "Generated SLM Issuing Letter with No : " + lblLNo.Text + " for '" + count.ToString() + "' students of SC Code : " + Session["SCCode"].ToString(), Convert.ToInt32(Session["ERID"].ToString()));

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
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = er;
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;
                        }
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! You can not change the total SLM to be issued on this letter.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Letter is already generated with these students with Letter No : "+lno.ToString();
                pnlMSG.Visible = true;
             
               
            }
        }

        private bool correctEntries(out string er)
        {
            er = "";
            bool correct = true;
            foreach (DataListItem dli in dtlistSLM.Items)
            {

                Label slmcode = (Label)dli.FindControl("lblSLMCode");
               
                Label ps = (Label)dli.FindControl("lblPresentStock");
                TextBox qty = (TextBox)dli.FindControl("tbQTY");


                if (Convert.ToInt32(qty.Text) > Convert.ToInt32(ps.Text))
                {
                    er = "Sorry !! Quantity is more than available stock on SLM Code : "+slmcode.Text;
                    correct = false;
                    break;

                }
                

            }

            return correct;
        }

        private int findCurrentTotalSLM()
        {
            int totalslm = 0;
            foreach (DataListItem dli in dtlistSLM.Items)
            {
                
                TextBox qty = (TextBox)dli.FindControl("tbQTY");

                totalslm = totalslm + Convert.ToInt32(qty.Text);

            }

            return totalslm;
        }

        private void populateNonZeroSLM()
        {

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMID");
            DataColumn dtcol3 = new DataColumn("SLMCode");
            DataColumn dtcol4 = new DataColumn("Dual");
            DataColumn dtcol5 = new DataColumn("GroupID");
            DataColumn dtcol6 = new DataColumn("Title");
            DataColumn dtcol7 = new DataColumn("Lang");
            DataColumn dtcol8 = new DataColumn("PresentStock");
            DataColumn dtcol9 = new DataColumn("Quantity");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            int i = 1;
            int counter = 0;
            foreach (DataListItem dli in dtlistSLM.Items)
            {
                Label slmid = (Label)dli.FindControl("lblSLMID");
                Label code = (Label)dli.FindControl("lblSLMCode");
                Label dual = (Label)dli.FindControl("lblSLMCode");
                Label groupid = (Label)dli.FindControl("lblSLMCode");
                Label title = (Label)dli.FindControl("lblTitle");
                Label lang = (Label)dli.FindControl("lblLang");
                Label ps = (Label)dli.FindControl("lblPresentStock");
                TextBox qty = (TextBox)dli.FindControl("tbQTY");

               
                if (qty.Text != "0")
                {

                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["SLMID"] = slmid.Text;
                    drow["SLMCode"] = code.Text;
                    drow["Dual"] = "";
                    drow["GroupID"] = "";
                    drow["Title"] = title.Text;
                    drow["Lang"] = lang.Text;
                    drow["Quantity"] = qty.Text;
                    drow["PresentStock"] = "";
                    dt.Rows.Add(drow);
                    i = i + 1;
                    counter = counter + Convert.ToInt32(drow["Quantity"]);
                }
               
            }

            dtlistSLM.DataSource = dt;
            dtlistSLM.DataBind();
            lblTotalSLM.Text = counter.ToString();
           
        }

        private int updateslmst()
        {
           
            int counter = 0;
            foreach (DataListItem dli in dtlistSLM.Items)
            {
                Label slmid = (Label)dli.FindControl("lblSLMID");

                TextBox qty = (TextBox)dli.FindControl("tbQTY");
                if (qty.Text != "0")
                {
                    int updated = 0;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDESLMMaster set PresentStock=@PresentStock where SLMID='" + slmid.Text + "' ", con);


                    cmd.Parameters.AddWithValue("@PresentStock", SLM.findCurrentStock(Convert.ToInt32(slmid.Text))-Convert.ToInt32(qty.Text));


                    con.Open();
                    updated = cmd.ExecuteNonQuery();
                    con.Close();

                    counter = counter + 1;

                }

            }

            return counter;
        }

        private int enterSLMIssueTransactions(int lid,out string error)
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

                TextBox qty = (TextBox)dli.FindControl("tbQty");
                if (qty.Text != "0")
                {
                    DataRow row = dataset.Tables["SLMSR"].NewRow();
                    row["SLMID"] = Convert.ToInt32(slmid.Text);
                    row["BillID"] = 0;
                    row["LID"] = lid;
                    row["SLID"] = 0;
                    row["Description"] = "ISSUED ON LETTER";
                    row["TOT"] = DateTime.Now.ToString();
                    row["Rate"] = 0.00;
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
            setValidation();
        }
    }
}