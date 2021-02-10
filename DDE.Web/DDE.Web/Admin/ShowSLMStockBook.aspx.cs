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
    public partial class ShowSLMStockBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 93) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateAllSLMCodes(ddlistSLMCode);
                    ddlistSLMCode.Items.Add("ALL");
                    setTodayDate();
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

        private void setTodayDate()
        {
            ddlistDOADayFrom.SelectedItem.Selected = false;
            ddlistDOAMonthFrom.SelectedItem.Selected = false;
            ddlistDOAYearFrom.SelectedItem.Selected = false;
            ddlistDOADayFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDOAMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDOAYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDOADayTo.SelectedItem.Selected = false;
            ddlistDOAMonthTo.SelectedItem.Selected = false;
            ddlistDOAYearTo.SelectedItem.Selected = false;
            ddlistDOADayTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDOAMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDOAYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            polulateStockBook();
           
        }

        private void polulateStockBook()
        {
            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text + " 11:59:59 PM";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (ddlistSLMCode.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "Select DDESLMMaster.SLMCode,DDESLMStockRegister.BillID,DDESLMStockRegister.LID,DDESLMStockRegister.Description,DDESLMStockRegister.TOT,DDESLMStockRegister.Credit,DDESLMStockRegister.Debit,DDESLMStockRegister.CurrentTotal from DDESLMStockRegister inner join DDESLMMaster on DDESLMMaster.SLMID=DDESLMStockRegister.SLMID where CONVERT(datetime,DDESLMStockRegister.TOT,105)>='" + from + "' and CONVERT(datetime,DDESLMStockRegister.TOT,105)<='" + to + "' order by DDESLMStockRegister.SSID";
            }
            else
            {
                cmd.CommandText = "Select DDESLMMaster.SLMCode,DDESLMStockRegister.BillID,DDESLMStockRegister.LID,DDESLMStockRegister.Description,DDESLMStockRegister.TOT,DDESLMStockRegister.Credit,DDESLMStockRegister.Debit,DDESLMStockRegister.CurrentTotal from DDESLMStockRegister inner join DDESLMMaster on DDESLMMaster.SLMID=DDESLMStockRegister.SLMID where DDESLMStockRegister.SLMID='" + ddlistSLMCode.SelectedItem.Value + "' and CONVERT(datetime,DDESLMStockRegister.TOT,105)>='" + from + "' and CONVERT(datetime,DDESLMStockRegister.TOT,105)<='" + to + "' order by DDESLMStockRegister.SSID";
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLM Code");
            DataColumn dtcol3 = new DataColumn("Bill No");
            DataColumn dtcol4 = new DataColumn("Letter No");
            DataColumn dtcol5 = new DataColumn("Description");
            DataColumn dtcol6 = new DataColumn("Time");
            DataColumn dtcol7 = new DataColumn("Credit");
            DataColumn dtcol8 = new DataColumn("Debit");
            DataColumn dtcol9 = new DataColumn("Balance");
            
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            int credit = 0;
            int debit = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = (i + 1).ToString();
                drow["SLM Code"] = ds.Tables[0].Rows[i]["SLMCode"].ToString();
                if (ds.Tables[0].Rows[i]["BillID"].ToString() == "0")
                {
                    drow["Bill No"] = "-";
                }
                else
                {
                    string[] bd = SLM.findBillDetailsByBillID(Convert.ToInt32(ds.Tables[0].Rows[i]["BillID"]));
                    drow["Bill No"] =bd[0].ToString();
                }
                if (ds.Tables[0].Rows[i]["LID"].ToString() == "0")
                {
                    drow["Letter No"] = "-";
                }
                else
                {
                    drow["Letter No"] = ds.Tables[0].Rows[i]["LID"].ToString();
                }
               
                drow["Description"] = ds.Tables[0].Rows[i]["Description"].ToString();
                drow["Time"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["TOT"].ToString()).ToString("dd-MM-yyy hh:mm:ss tt"); 
                drow["Credit"] = ds.Tables[0].Rows[i]["Credit"].ToString();
                credit = credit + Convert.ToInt32(drow["Credit"]);
                drow["Debit"] = ds.Tables[0].Rows[i]["Debit"].ToString();
                debit = debit + Convert.ToInt32(drow["Debit"]);
                drow["Balance"] = ds.Tables[0].Rows[i]["CurrentTotal"].ToString();
              
                dt.Rows.Add(drow);

            }
           
            gvSLMSR.DataSource = dt;
            gvSLMSR.DataBind();

            lblTotalCr.Text = credit.ToString();
            lblTotalDt.Text = debit.ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlTrans.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlTrans.Visible = false;
                lblMSG.Text = "Sorry !! No record found.";
                pnlMSG.Visible = true;
            }
            
        }

        protected void ddlistSLMCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlTrans.Visible = false;
        }
    }
}