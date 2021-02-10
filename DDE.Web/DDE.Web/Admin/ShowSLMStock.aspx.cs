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
    public partial class ShowSLMStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 93) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {
                if (!IsPostBack)
                {
                    lblDateTime.Text = "As on : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                    populateSLMStock();
                    setColors();
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

        private void setColors()
        {
            foreach (DataListItem dli in dtlistShowSLM.Items)
            {
               
                Label ps = (Label)dli.FindControl("lblPS");
               

              
                if ((Convert.ToInt32(ps.Text) >= 100) && (Convert.ToInt32(ps.Text) < 300))
                {
                    ps.BackColor = System.Drawing.Color.Orange;
                }
                else if (Convert.ToInt32(ps.Text) < 100)
                {
                    ps.BackColor = System.Drawing.Color.Red;
                    ps.ForeColor = System.Drawing.Color.White;
                }

            }
        }

        private void populateSLMStock()
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
            int counter = 0;
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
                counter = counter + Convert.ToInt32(drow["PS"]);
            }

            dtlistShowSLM.DataSource = dt;
            dtlistShowSLM.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowSLM.Visible = true;
                lblTotalSLM.Text = "Total SLM : " + counter.ToString();
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                dtlistShowSLM.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }

        }
    }
}