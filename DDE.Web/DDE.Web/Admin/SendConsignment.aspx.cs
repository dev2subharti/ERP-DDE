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
    public partial class SendConsignment : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 93))
        //    {
        //        if (!IsPostBack)
        //        {
        //            PopulateDDList.populateDispatchParties(ddlistParty);

        //        }

        //        pnlData.Visible = true;
        //        pnlMSG.Visible = false;
        //    }


        //    else
        //    {
        //        pnlData.Visible = false;
        //        lblMSG.Text = "Sorry !! You are not authorised for this control";
        //        pnlMSG.Visible = true;
        //    }
        //}

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    string pktentryerror;
        //    string pktupdateerror;
        //    if (cbByHand.Checked == true)
        //    {
        //        int updatepkt = updatePacketDetails(out pktupdateerror);
        //        if (updatepkt == 1)
        //        {
        //            pnlData.Visible = false;
        //            lblMSG.Text = "Letter has been pcocessed successfully !!";
        //            pnlMSG.Visible = true;
        //            btnOK.Visible = false;
        //        }
        //        else
        //        {
        //            pnlData.Visible = false;
        //            lblMSG.Text = "Sorry !! Letter could not be processed. Please contact to ERP Developer";
        //            pnlMSG.Visible = true;
        //            btnOK.Visible = false;
        //        }
        //    }
        //    else
        //    {
        //        int enterpktdetails = enterPacketDetails(out pktentryerror);
        //        int updatepkt = updatePacketDetails(out pktupdateerror);
        //        if (pktentryerror == "" && pktupdateerror == "" && enterpktdetails == dtlistSLM.Items.Count && updatepkt == 1)
        //        {
        //            pnlData.Visible = false;
        //            lblMSG.Text = "Letter has been pcocessed successfully !!";
        //            pnlMSG.Visible = true;
        //            btnOK.Visible = false;
        //        }
        //        else
        //        {
        //            pnlData.Visible = false;
        //            lblMSG.Text = "Sorry !! something went wrong. Please contact to ERP Developer.Error : " + pktentryerror + "," + pktupdateerror;
        //            pnlMSG.Visible = true;
        //            btnOK.Visible = false;
        //        }
        //    }


        //}

        //private int updatePacketDetails(out string pktupdateerror)
        //{
        //    pktupdateerror = "";
        //    int updated = 0;
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd = new SqlCommand("update DDESLMLetters set LetterProcessedOn=@LetterProcessedOn,DispatchDate=@DispatchDate,TotalPktWeight=@TotalPktWeight,TotalDispatchCharge=@TotalDispatchCharge,DType=@DType,DPID=@DPID,DocketNo=@DocketNo where LID='" + tbLetterNo.Text + "' ", con);

        //        cmd.Parameters.AddWithValue("@LetterProcessedOn", DateTime.Now.ToString());
        //        cmd.Parameters.AddWithValue("@DispatchDate", DateTime.Now.ToString("yyyy-MM-dd"));


        //        if (cbByHand.Checked == false)
        //        {
        //            cmd.Parameters.AddWithValue("@TotalPktWeight", lblTotalWt.Text);
        //            cmd.Parameters.AddWithValue("@TotalDispatchCharge", lblTotalAmount.Text);
        //            cmd.Parameters.AddWithValue("@DType", 1);
        //            cmd.Parameters.AddWithValue("@DPID", ddlistParty.SelectedItem.Value);
        //            cmd.Parameters.AddWithValue("@DocketNo", "");
        //        }
        //        else if (cbByHand.Checked == true)
        //        {
        //            cmd.Parameters.AddWithValue("@TotalPktWeight", 0);
        //            cmd.Parameters.AddWithValue("@TotalDispatchCharge", 0);
        //            cmd.Parameters.AddWithValue("@DType", 2);
        //            cmd.Parameters.AddWithValue("@DPID", 0);
        //            cmd.Parameters.AddWithValue("@DocketNo", "NA");
        //        }



        //        con.Open();
        //        updated = cmd.ExecuteNonQuery();
        //        con.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        pktupdateerror = ex.Message;
        //    }


        //    return updated;
        //}

        //private int enterPacketDetails(out string pktentryerror)
        //{
        //    pktentryerror = "";
        //    int result = 0;
        //    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand command = new SqlCommand();
        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        //    DataSet dataset = new DataSet();


        //    command.Connection = connection;
        //    command.CommandText = "SELECT * FROM DDESLMPackets";
        //    adapter.SelectCommand = command;
        //    adapter.Fill(dataset, "SLMPK");

        //    int counter = 0;

        //    foreach (DataListItem dli in dtlistSLM.Items)
        //    {


        //        TextBox wt = (TextBox)dli.FindControl("tbWeight");
        //        TextBox rate = (TextBox)dli.FindControl("tbRate");

        //        if (Convert.ToDouble(wt.Text) != 0)
        //        {
        //            DataRow row = dataset.Tables["SLMPK"].NewRow();
        //            row["LID"] = Convert.ToInt32(tbLetterNo.Text);
        //            row["PWeight"] = wt.Text;
        //            row["Rate"] = rate.Text;
        //            dataset.Tables["SLMPK"].Rows.Add(row);

        //            counter = counter + 1;


        //        }

        //    }

        //    try
        //    {
        //        result = adapter.Update(dataset, "SLMPK");
        //        if (result == counter)
        //        {
        //            pktentryerror = "";
        //        }

        //    }
        //    catch (SqlException ex)
        //    {
        //        pktentryerror = ex.Message;

        //    }

        //    return result;
        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("SendConsignment.aspx");
        //}

        //protected void ddlistConsType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(ddlistConsType.SelectedItem.Value=="1" || ddlistConsType.SelectedItem.Value == "2")
        //    {
        //        lblRecID.Text = "Enrollmemt No.";

        //    }
        //}
    }
}