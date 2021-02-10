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
    public partial class UpdateSLMStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 94))
            {
                if (!IsPostBack)
                {
                    populateSLMCodes();
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

        private void populateSLMCodes()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMMaster order by SLMCode", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMID");
            DataColumn dtcol3 = new DataColumn("SLMCode");
            DataColumn dtcol4 = new DataColumn("Title");
            DataColumn dtcol5 = new DataColumn("Cost");
            DataColumn dtcol6 = new DataColumn("PS");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = (i + 1).ToString();
                drow["SLMID"] = ds.Tables[0].Rows[i]["SLMID"].ToString();
                drow["SLMCode"] = ds.Tables[0].Rows[i]["SLMCode"].ToString();
                drow["Title"] = ds.Tables[0].Rows[i]["Title"].ToString();
                drow["Cost"] = ds.Tables[0].Rows[i]["Cost"].ToString();
                drow["PS"] = ds.Tables[0].Rows[i]["PresentStock"].ToString();

                dt.Rows.Add(drow);
            }

            dtlistShowSLM.DataSource = dt;
            dtlistShowSLM.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowSLM.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowSLM.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }


        }

        protected void dtlistShowSLM_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    Label slmid = (Label)e.Item.FindControl("lblSLMID");
                    Label slmcode = (Label)e.Item.FindControl("lblSLMCode");
                    DropDownList mode = (DropDownList)e.Item.FindControl("ddlistCalc");
                    Label ps = (Label)e.Item.FindControl("lblPS");
                    TextBox ne = (TextBox)e.Item.FindControl("tbNewEntry");
                    TextBox remark = (TextBox)e.Item.FindControl("tbRemark");
                    Button btnupdate = (Button)e.Item.FindControl("btnUpdate");

                    string error = "";
                    int updated = 0;
                    if (mode.SelectedItem.Text == "+")
                    {
                        updated = SLM.makeSLMTransaction(Convert.ToInt32(slmid.Text), 0, 0,0, remark.Text.ToUpper(), Convert.ToInt32(ne.Text), 0, out error);
                    }
                    else if (mode.SelectedItem.Text == "-")
                    {
                        updated = SLM.makeSLMTransaction(Convert.ToInt32(slmid.Text), 0, 0,0, remark.Text.ToUpper(), 0, Convert.ToInt32(ne.Text), out error);
                    }
                   

                    if (updated == 0)
                    {
                        dtlistShowSLM.Visible = false;
                        lblMSG.Text = error;
                        pnlMSG.Visible = true;
                    }
                    else
                    {
                        string mod = "";
                        if (mode.SelectedItem.Text == "+")
                        {
                            mod = "Credit";
                        }
                        else if (mode.SelectedItem.Text == "-")
                        {
                            mod = "Debit";
                        }
                        Log.createLogNow("Update SLM Stock", mod+" SLM Code '" + slmcode.Text + "' with Quantity '"+ ne.Text + " in Stock Register with Remark '"+remark.Text+"'", Convert.ToInt32(Session["ERID"].ToString()));
                       
                        //populateSLMCodes();
                        ps.Text = SLM.findCurrentStock(Convert.ToInt32(slmid.Text)).ToString();
                        ne.Enabled = false;
                        ne.Text = "";
                        ne.BackColor = System.Drawing.Color.Orange;
                        remark.Enabled = false;
                        remark.Text = "";
                        remark.BackColor = System.Drawing.Color.Orange;
                        btnupdate.Visible= false;
                        
                    }


                }
            }
            catch (Exception ex)
            {
                dtlistShowSLM.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }
        }
       
    }
}