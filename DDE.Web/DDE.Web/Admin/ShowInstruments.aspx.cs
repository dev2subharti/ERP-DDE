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
    public partial class ShowInstruments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 57) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 73) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 74) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 75) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 77) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79))
            {
                if (!IsPostBack)
                {
                    ddlistSCCode.Items.Add("ALL");
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateProStudyCentre(ddlistSCCode);
                    setCurrentDate();
                    
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

        private void setCurrentDate()
        {
            ddlistDayFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDayTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            polulateIDetails();
            setStatus();
            setAccessibility();
        }

        private void setStatus()
        {
            foreach (DataListItem dli in dtlistShowDrafts.Items)
            {
                Label lb = (Label)dli.FindControl("lblVerification");
                Label ld = (Label)dli.FindControl("lblDistribution");

                if (lb.Text == "OK")
                {
                    lb.Visible = false;
                }
                else if (lb.Text == "V")
                {
                    lb.Visible = true;
                }

                if (ld.Text == "OK")
                {
                    ld.Visible = false;
                }
                else if (ld.Text == "D")
                {
                    ld.Visible = true;
                }
               
            }
        }

        private void setAccessibility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 73))
            {
                foreach (DataListItem dli in dtlistShowDrafts.Items)
                {
                    CheckBox cb = (CheckBox)dli.FindControl("cbChecked");
                    cb.Visible = true;
                }

                if (Convert.ToInt32(Session["TI"]) >0)
                {
                    btnPublish.Visible = true;
                }
            }
            else
            {
                btnPublish.Visible = false;
            }

            if (ddlistIType.SelectedItem.Text == "RECEIVED")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 77))
                {

                    foreach (DataListItem dli in dtlistShowDrafts.Items)
                    {
                        LinkButton lb = (LinkButton)dli.FindControl("lnkbtnEdit");

                        lb.Visible = true;
                    }

                }
            }
            else if (ddlistIType.SelectedItem.Text == "VERIFIED")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 78))
                {

                    foreach (DataListItem dli in dtlistShowDrafts.Items)
                    {
                        LinkButton lb = (LinkButton)dli.FindControl("lnkbtnEdit");

                        lb.Visible = true;
                    }

                }
            }
            else if (ddlistIType.SelectedItem.Text == "DISTRIBUTED")
            {
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79))
                {

                    foreach (DataListItem dli in dtlistShowDrafts.Items)
                    {
                        LinkButton lb = (LinkButton)dli.FindControl("lnkbtnEdit");

                        lb.Visible = true;
                    }

                }
            }
           
        }

        private void polulateIDetails()
        {
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 11:59:59 PM";

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcoliid = new DataColumn("IID");
            DataColumn dtcol2 = new DataColumn("Type");
            DataColumn dtcolRG = new DataColumn("RG");
            DataColumn dtcol3 = new DataColumn("DCNumber");
            DataColumn dtcol4 = new DataColumn("DCDate");
            DataColumn dtcol5 = new DataColumn("IBN");
            DataColumn dtcol6 = new DataColumn("DCAmount");
            DataColumn dtcol7 = new DataColumn("Balance");
            DataColumn dtcol8 = new DataColumn("VOn");
            DataColumn dtcol9 = new DataColumn("VBy");
            DataColumn dtcol10 = new DataColumn("VStatus");
            DataColumn dtcol11 = new DataColumn("DStatus");
            DataColumn dtcol12 = new DataColumn("SCCode");
            DataColumn dtcol13 = new DataColumn("SCMode");
            DataColumn dtcol14 = new DataColumn("LockRemark");
           

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcoliid);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcolRG);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);
            dt.Columns.Add(dtcol12);
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {

                if (ddlistIType.SelectedItem.Text == "RECEIVED")
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where Received='True' and ReceivedOn>='" + from + "' and ReceivedOn<='" + to + "' order by IID";
                }
                else if (ddlistIType.SelectedItem.Text == "VERIFIED")
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where Verified='True' and VerifiedOn>='" + from + "' and VerifiedOn<='" + to + "' order by IID";
                }
                else if (ddlistIType.SelectedItem.Text == "DISTRIBUTED")
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where AmountAlloted='True' and AllotedOn>='" + from + "' and AllotedOn<='" + to + "' order by IID";
                }
            }
            else
            {
                if (ddlistIType.SelectedItem.Text == "RECEIVED")
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where Received='True' and SCCode='" + ddlistSCCode.SelectedItem.Text + "' and ReceivedOn>='" + from + "' and ReceivedOn<='" + to + "' order by IID";
                }
                else if (ddlistIType.SelectedItem.Text == "VERIFIED")
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where Verified='True' and SCCode='" + ddlistSCCode.SelectedItem.Text + "' and VerifiedOn>='" + from + "' and VerifiedOn<='" + to + "' order by IID";
                }
                else if (ddlistIType.SelectedItem.Text == "DISTRIBUTED")
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where AmountAlloted='True' and SCCode='" + ddlistSCCode.SelectedItem.Text + "' and AllotedOn>='" + from + "' and AllotedOn<='" + to + "' order by IID";
                }
            }

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
                    drow["IID"] = Convert.ToInt32(dr["IID"]);
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr["IType"]));
                    if (Convert.ToString(dr["RG"]) == "True")
                    {
                        drow["RG"] = "RG";
                    }
                    else if (Convert.ToString(dr["RG"]) == "False")
                    {
                        drow["RG"] = "";
                    }
                    drow["DCNumber"] = Convert.ToString(dr["INo"]);
                    drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                    drow["SCMode"] = Convert.ToString(dr["SCMode"]);
                    drow["DCDate"] = Convert.ToDateTime(dr["IDate"]).ToString("dd/MM/yyyy").ToUpper();
                    drow["IBN"] = Convert.ToString(dr["IBN"]);
                    drow["DCAmount"] = Convert.ToString(dr["TotalAmount"]);
                    drow["Balance"] = (Convert.ToInt32(drow["DCAmount"]) - Accounts.findUsedAmountOfDraft(Convert.ToInt32(dr["IType"]), Convert.ToString(dr["INo"]), Convert.ToDateTime(dr["IDate"]).ToString("yyyy-MM-dd"), Convert.ToString(dr["IBN"])));
                    if (ddlistIType.SelectedItem.Text == "RECEIVED")
                    {
                        drow["VOn"] =Convert.ToDateTime(dr["ReceivedOn"]).ToString("dd/MM/yyyy");
                        drow["VBy"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["ReceivedBy"]));
                    }
                    else if (ddlistIType.SelectedItem.Text == "VERIFIED")
                    {
                        drow["VOn"] = Convert.ToDateTime(dr["VerifiedOn"]).ToString("dd/MM/yyyy");
                        drow["VBy"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["VerifiedBy"]));
                    }
                    else if (ddlistIType.SelectedItem.Text == "DISTRIBUTED")
                    {
                        drow["VOn"] = Convert.ToDateTime(dr["AllotedOn"]).ToString("dd/MM/yyyy");
                        drow["VBy"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["AllotedBy"]));
                    }
                    if (dr["Verified"].ToString() == "True")
                    {
                        drow["VStatus"] = "OK";
                      
                    }
                    else if (dr["Verified"].ToString() == "False")
                    {
                        drow["VStatus"] = "V";
                    }
                    if (dr["AmountAlloted"].ToString() == "True")
                    {
                        drow["DStatus"] = "OK";

                    }
                    else if (dr["AmountAlloted"].ToString() == "False")
                    {
                        drow["DStatus"] = "D";
                    }

                    drow["LockRemark"] = Convert.ToString(dr["LockRemark"]);

                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }



            con.Close();
            int j=1;

            if (ddlistSCCode.SelectedItem.Text != "ALL")
            {
                fillIfInMultiple(dt,i, out j);
            }

            dtlistShowDrafts.DataSource = dt;
            dtlistShowDrafts.DataBind();

            con.Close();

            if (i > 1 || j>1)
            {

                dtlistShowDrafts.Visible = true;
                btnPublish.Visible = true;
                cbAdjustment.Visible = true;
                pnlMSG.Visible = false;
                Session["TI"] = i+j;
            }
            else
            {
                dtlistShowDrafts.Visible = false;
                btnPublish.Visible = false;
                cbAdjustment.Visible=false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
                Session["TI"] = 0;

            }
        }

        private void fillIfInMultiple(DataTable dt,int k, out int j)
        {
            j = 1;
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 11:59:59 PM";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (ddlistIType.SelectedItem.Text == "RECEIVED")
            {
                cmd.CommandText = "select * from DDEFeeInstruments where Received='True' and SCMode='False' and ReceivedOn>='" + from + "' and ReceivedOn<='" + to + "' order by IID";
            }
            else if (ddlistIType.SelectedItem.Text == "VERIFIED")
            {
                cmd.CommandText = "select * from DDEFeeInstruments where Verified='True' and SCMode='False' and VerifiedOn>='" + from + "' and VerifiedOn<='" + to + "' order by IID";
            }
            else if (ddlistIType.SelectedItem.Text == "DISTRIBUTED")
            {
                cmd.CommandText = "select * from DDEFeeInstruments where AmountAlloted='True' and SCMode='False' and AllotedOn>='" + from + "' and AllotedOn<='" + to + "' order by IID";
            }
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string[] str = (ds.Tables[0].Rows[i]["SCCode"]).ToString().Split(',');
                int pos = Array.IndexOf(str, ddlistSCCode.SelectedItem.Text);
                if (pos>-1)
                {
                   DataRow drow = dt.NewRow();
                    drow["SNo"] = k;
                    drow["IID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["IID"]);
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(ds.Tables[0].Rows[i]["IType"]));
                    if (Convert.ToString(ds.Tables[0].Rows[i]["RG"]) == "True")
                    {
                        drow["RG"] = "RG";
                    }
                    else if (Convert.ToString(ds.Tables[0].Rows[i]["RG"]) == "False")
                    {
                        drow["RG"] = "";
                    }
                    drow["DCNumber"] = Convert.ToString(ds.Tables[0].Rows[i]["INo"]);
                    drow["SCCode"] = Convert.ToString(ds.Tables[0].Rows[i]["SCCode"]);
                    drow["SCMode"] = Convert.ToString(ds.Tables[0].Rows[i]["SCMode"]);
                    drow["DCDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["IDate"]).ToString("dd/MM/yyyy").ToUpper();
                    drow["IBN"] = Convert.ToString(ds.Tables[0].Rows[i]["IBN"]);
                    drow["DCAmount"] = Convert.ToString(ds.Tables[0].Rows[i]["TotalAmount"]);
                    drow["Balance"] = (Convert.ToInt32(drow["DCAmount"]) - Accounts.findUsedAmountOfDraft(Convert.ToInt32(ds.Tables[0].Rows[i]["IType"]), Convert.ToString(ds.Tables[0].Rows[i]["INo"]), Convert.ToDateTime(ds.Tables[0].Rows[i]["IDate"]).ToString("yyyy-MM-dd"), Convert.ToString(ds.Tables[0].Rows[i]["IBN"])));
                    if (ddlistIType.SelectedItem.Text == "RECEIVED")
                    {
                        drow["VOn"] =Convert.ToDateTime(ds.Tables[0].Rows[i]["ReceivedOn"]).ToString("dd/MM/yyyy");
                        drow["VBy"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(ds.Tables[0].Rows[i]["ReceivedBy"]));
                    }
                    else if (ddlistIType.SelectedItem.Text == "VERIFIED")
                    {
                        drow["VOn"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["VerifiedOn"]).ToString("dd/MM/yyyy");
                        drow["VBy"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(ds.Tables[0].Rows[i]["VerifiedBy"]));
                    }
                    else if (ddlistIType.SelectedItem.Text == "DISTRIBUTED")
                    {
                        drow["VOn"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["AllotedOn"]).ToString("dd/MM/yyyy");
                        drow["VBy"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(ds.Tables[0].Rows[i]["AllotedBy"]));
                    }

                    if (ds.Tables[0].Rows[i]["Verified"].ToString() == "True")
                    {
                        drow["VStatus"] = "OK";
                      
                    }
                    else if (ds.Tables[0].Rows[i]["Verified"].ToString() == "False")
                    {
                        drow["VStatus"] = "V";
                    }

                    if (ds.Tables[0].Rows[i]["AmountAlloted"].ToString() == "True")
                    {
                        drow["DStatus"] = "OK";

                    }
                    else if (ds.Tables[0].Rows[i]["AmountAlloted"].ToString() == "False")
                    {
                        drow["DStatus"] = "D";
                    }

                    drow["LockRemark"] = Convert.ToString(ds.Tables[0].Rows[i]["LockRemark"]);

                    dt.Rows.Add(drow);
                    j = j + 1;
                    k = k + 1;
                }
                
            }
            
        }

        protected void dtlistShowDrafts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (ddlistSCCode.SelectedItem.Text != "ALL")
            {
                string scmode;
                string ins = calculateInstruments(out scmode);
                int times;
                string lastprinted;
                int vlno;
                if (FindInfo.isVLPrintedByIID(ins, out times, out lastprinted, out vlno))
                {

                    pnlData.Visible = false;
                    lblMSG.Text = "This Instrument Verification Letter has already printed '" + times.ToString() + "' times.<br/>Last printed on '" + lastprinted + "'<br/> with Letter No. '" + vlno + "'";
                    btnOK.Visible = true;
                    pnlMSG.Visible = true;
                    Session["printcounter"] = times;

                }
                else
                {


                    if (ins != "")
                    {
                        Session["VLSCCode"] = ddlistSCCode.SelectedItem.Text;
                        Session["ins"] = ins;
                        if (cbAdjustment.Checked)
                        {
                            Session["AdjustmentExist"] = "True";
                            Session["AdjLNo"] = tbLNo.Text;
                            Session["AdjDate"] = tbDate.Text;
                            Session["AdjAmount"] = tbAmount.Text;
                            Session["Adjustment"] = "Amount to be adjusted from Letter No. : " + tbLNo.Text + " Dated : " + tbDate.Text + " Amount : " + tbAmount.Text;
                        }
                        else
                        {
                            Session["AdjustmentExist"] = "False";
                            Session["AdjLNo"] = "";
                            Session["AdjDate"] = "";
                            Session["AdjAmount"] = "";
                            Session["Adjustment"] = "";
                        }
                        Response.Redirect("VerificationLetter.aspx");
                    }

                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Please select any instrument for publish";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }

                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Please select any one study centre";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
           
        }

        private string  calculateInstruments(out string scmode)
        {
            string ins = "";
            scmode = "1";
           
                foreach (DataListItem dli in dtlistShowDrafts.Items)
                {
                    CheckBox cb = (CheckBox)dli.FindControl("cbChecked");
                    Label lbl = (Label)dli.FindControl("lblIID");
                    Label lblsc = (Label)dli.FindControl("lblSCMode");

                    if (cb.Checked)
                    {
                        if (ins == "")
                        {
                            ins = lbl.Text;
                        }
                        else
                        {
                            ins = ins + "," + lbl.Text;
                        }
                        if (lblsc.Text == "False")
                        {
                            scmode = "0";
                        }
                        
                    }
                }
                Session["SCMode"] = scmode;
                
          

            return ins;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }

        protected void cbAdjustment_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAdjustment.Checked)
            {
                pnlAdjustment.Visible = true;
            }
            else
            {
                pnlAdjustment.Visible = false;
            }
        }

        protected void dtlistShowDrafts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (ddlistIType.SelectedItem.Text == "RECEIVED")
                {
                    Response.Redirect("CreateInstrument.aspx?IID="+e.CommandArgument);
                }
                else if (ddlistIType.SelectedItem.Text == "VERIFIED")
                {
                    Response.Redirect("VerifyDraft.aspx?IID=" + e.CommandArgument);
                }
                else if (ddlistIType.SelectedItem.Text == "DISTRIBUTED")
                {
                    Response.Redirect("DistributeAmount.aspx?IID=" + e.CommandArgument);
                }
            }
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDrafts.Visible = false;
            btnPublish.Visible = false;
            cbAdjustment.Visible = false;
            pnlAdjustment.Visible = false;
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowDrafts.Items)
            {
                CheckBox cb = (CheckBox)dli.FindControl("cbChecked");
                cb.Checked = true;
            }
        }
    }
}