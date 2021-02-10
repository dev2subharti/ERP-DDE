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
    public partial class ProcessSLMLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95))
            {              
                pnlData.Visible = true;
                pnlSearch.Visible = true;
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
            if ((cbByHand.Checked == false && ddlistParty.SelectedItem.Text != "--SELECT ONE--") || (cbByHand.Checked == true))
            {

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("PID");
                DataColumn dtcol3 = new DataColumn("PWeight");
                DataColumn dtcol4 = new DataColumn("Rate");
                DataColumn dtcol5 = new DataColumn("Amount");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);


                int i = 1;



                foreach (DataListItem dli in dtlistSLM.Items)
                {
                    Label pid = (Label)dli.FindControl("lblPID");
                    TextBox wt = (TextBox)dli.FindControl("tbWeight");
                    TextBox rate = (TextBox)dli.FindControl("tbRate");
                    Label amount = (Label)dli.FindControl("lblAmount");


                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["PID"] = Convert.ToString(pid.Text);
                    drow["PWeight"] = Convert.ToString(wt.Text);
                    drow["Rate"] = Convert.ToString(rate.Text);
                    drow["Amount"] = Convert.ToString(amount.Text);

                    dt.Rows.Add(drow);

                    i = i + 1;

                }


                DataRow drow1 = dt.NewRow();

                drow1["SNo"] = i;
                drow1["PID"] = 0;

                drow1["PWeight"] = 0;
                drow1["Rate"] = lblPRate.Text;
                drow1["Amount"] = 0;
                dt.Rows.Add(drow1);


                dtlistSLM.DataSource = dt;
                dtlistSLM.DataBind();

                pnlBill.Visible = true;
                ddlistParty.Enabled = false;
                cbByHand.Enabled = false;

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Please select any one 'Party'";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
          
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            if (btnLock.Text == "Lock Details")
            {
                double totalwt = 0;
                double totalamount = 0;
                string error = "";
                foreach (DataListItem dli in dtlistSLM.Items)
                {
                   
                    TextBox wt = (TextBox)dli.FindControl("tbWeight");
                    TextBox rate = (TextBox)dli.FindControl("tbRate");
                    Label amount = (Label)dli.FindControl("lblAmount");
                    ImageButton del = (ImageButton)dli.FindControl("imgbtnDelete");
                    if (Convert.ToDouble(wt.Text) == 0)
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry!! You can not fill Weight '0'.Please Check";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        error = "Error";
                        break;

                    }
                    else
                    {

                        amount.Text = (Convert.ToDouble(wt.Text) * Convert.ToDouble(rate.Text)).ToString();
                        amount.Enabled = false;
                        rate.Enabled = false;
                        del.Visible = false; ;
                        wt.Enabled = false;
                            
                        totalwt = totalwt + Convert.ToDouble(wt.Text);
                        totalamount = totalamount + Convert.ToDouble(amount.Text);
                    }

                    btnAdd.Visible = false;

                }
                if (error == "")
                {
                    lblTotalWt.Text = totalwt.ToString();
                    lblTotalAmount.Text = totalamount.ToString();               
                    btnLock.Text = "Edit";
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
            }
            else if (btnLock.Text == "Edit")
            {

                foreach (DataListItem dli in dtlistSLM.Items)
                {

                    TextBox wt = (TextBox)dli.FindControl("tbWeight");
                  
                    ImageButton del = (ImageButton)dli.FindControl("imgbtnDelete");

                    wt.Enabled = true;
                  
                    del.Visible = true;

                }

                lblTotalWt.Text = "";
                lblTotalAmount.Text = "";
                btnAdd.Visible = true;
                btnLock.Text = "Lock Details";
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
                DataColumn dtcol2 = new DataColumn("PID"); 
                DataColumn dtcol3 = new DataColumn("PWeight");
                DataColumn dtcol4= new DataColumn("Rate");
                DataColumn dtcol5 = new DataColumn("Amount");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);           
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);

                int i = 1;



                foreach (DataListItem dli in dtlistSLM.Items)
                {
                    Label pid = (Label)dli.FindControl("lblPID");
                    TextBox wt = (TextBox)dli.FindControl("tbWeight");
                    TextBox rate = (TextBox)dli.FindControl("tbRate");
                    Label amount = (Label)dli.FindControl("lblAmount");


                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i;
                    drow["PID"] = Convert.ToString(pid.Text);
                    drow["PWeight"] = Convert.ToString(wt.Text);
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
           
            string pktentryerror;
            string pktupdateerror;
            if (cbByHand.Checked == true)
            {
                int updatepkt = updatePacketDetails(out pktupdateerror);
                if (updatepkt==1)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Letter has been pcocessed successfully !!";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Letter could not be processed. Please contact to ERP Developer";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }
            }
            else
            {
                int enterpktdetails = enterPacketDetails(out pktentryerror);
                int updatepkt = updatePacketDetails(out pktupdateerror);
                if (pktentryerror == "" && pktupdateerror == "" && enterpktdetails == dtlistSLM.Items.Count && updatepkt == 1)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Letter has been pcocessed successfully !!";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! something went wrong. Please contact to ERP Developer.Error : " + pktentryerror + "," + pktupdateerror;
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                }
            }


                
           
        }

        private int updatePacketDetails(out string pktupdateerror)
        {
            pktupdateerror = "";
            int updated = 0;          
            try
            {                      
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDESLMLetters set LetterProcessedOn=@LetterProcessedOn,DispatchDate=@DispatchDate,TotalPktWeight=@TotalPktWeight,TotalDispatchCharge=@TotalDispatchCharge,DType=@DType,DPID=@DPID,DocketNo=@DocketNo where LID='" + tbLetterNo.Text + "' ", con);

                cmd.Parameters.AddWithValue("@LetterProcessedOn", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@DispatchDate", DateTime.Now.ToString("yyyy-MM-dd"));
               
               
                if (cbByHand.Checked == false)
                {
                    cmd.Parameters.AddWithValue("@TotalPktWeight", lblTotalWt.Text);
                    cmd.Parameters.AddWithValue("@TotalDispatchCharge", lblTotalAmount.Text);
                    cmd.Parameters.AddWithValue("@DType", 1);
                    cmd.Parameters.AddWithValue("@DPID", ddlistParty.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@DocketNo", "");
                }
                else if (cbByHand.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@TotalPktWeight", 0);
                    cmd.Parameters.AddWithValue("@TotalDispatchCharge", 0);
                    cmd.Parameters.AddWithValue("@DType", 2);
                    cmd.Parameters.AddWithValue("@DPID", 0);
                    cmd.Parameters.AddWithValue("@DocketNo", "NA");
                }
               
              

                con.Open();
                updated = cmd.ExecuteNonQuery();
                con.Close();
                    
            }
            catch (Exception ex)
            {
                pktupdateerror = ex.Message;
            }


            return updated;
        }

        private int enterPacketDetails(out string pktentryerror)
        {
            pktentryerror = "";
            int result = 0;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet dataset = new DataSet();


            command.Connection = connection;
            command.CommandText = "SELECT * FROM DDESLMPackets";
            adapter.SelectCommand = command;
            adapter.Fill(dataset, "SLMPK");

            int counter = 0;

            foreach (DataListItem dli in dtlistSLM.Items)
            {
               

                TextBox wt = (TextBox)dli.FindControl("tbWeight");
                TextBox rate = (TextBox)dli.FindControl("tbRate");

                if (Convert.ToDouble(wt.Text) != 0)
                {
                    DataRow row = dataset.Tables["SLMPK"].NewRow();
                    row["LID"] = Convert.ToInt32(tbLetterNo.Text);
                    row["PWeight"] = wt.Text;
                    row["Rate"] = rate.Text;
                    dataset.Tables["SLMPK"].Rows.Add(row);

                    counter = counter + 1;


                }

            }

            try
            {
                result = adapter.Update(dataset, "SLMPK");
                if (result == counter)
                {
                    pktentryerror = "";
                }

            }
            catch (SqlException ex)
            {
                pktentryerror = ex.Message;

            }

            return result;
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProcessSLMLetter.aspx");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {        
            populateLetterDetails();
            ddlistParty.Items.Clear();
            ddlistParty.Items.Add("--SELECT ONE--");
            PopulateDDList.populateDispatchParties(ddlistParty);
        }

        private void populateLetterDetails()
        {
            string error = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESLMLetters where LID='" + tbLNo.Text + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            int i = 0;

            if (dr.HasRows)
            {
                dr.Read();
                if (Convert.ToDateTime(dr["LetterProcessedOn"]).ToString("dd-MM-yyyy") == "01-01-1900")
                {
                    i = 1;
                    tbLetterNo.Text = dr["LID"].ToString();
                    tbLetterDate.Text = Convert.ToDateTime(dr["LetterPublishedOn"]).ToString("dd-MM-yyyy");
                    tbSCCode.Text = dr["SCCode"].ToString();
                    tbTotalSLM.Text = SLM.findTotalSLMByLID(Convert.ToInt32(dr["LID"])).ToString();
                    
                }
                else
                {
                    error = "Sorry !! This Letter No. is already processed.";
                }

            }
            else
            {
                error = "Sorry !! Invalid Letter No.";
            }

            con.Close();

            if (i == 1)
            {
                pnlAllDetails.Visible = true;
                pnlSearch.Visible = false;
                tbLNo.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                pnlAllDetails.Visible = false;
                pnlSearch.Visible = true;           
                lblMSG.Text = error;
                pnlMSG.Visible = true;
                btnOK.Visible = false;
            }
        }

        protected void ddlistParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPRate.Text = FindInfo.findDispatchPartyRateByDPID(Convert.ToInt32(ddlistParty.SelectedItem.Value)).ToString();
            ddlistParty.Enabled = false;
            cbByHand.Enabled = false;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlSearch.Visible = false;
            pnlMSG.Visible = false;
        }

        protected void cbByHand_CheckedChanged(object sender, EventArgs e)
        {
            if (cbByHand.Checked == true)
            {
                pnlDispatchDetails.Visible = false;
                ddlistParty.Enabled = false;
                btnSubmit.Visible = true;
            }
            else if (cbByHand.Checked == false)
            {
                pnlDispatchDetails.Visible = true;
                ddlistParty.Enabled = true;
                btnSubmit.Visible = false;
            }
           
        }
    }
}