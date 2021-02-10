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
    public partial class ShowRefundLettersByList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
            {
                if (!IsPostBack)
                {
                    ddlistSCCode.Items.Add("ALL");
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
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
            populateLetters();

        }

        private void populateLetters()
        {
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 11:59:59 PM";

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("RLID");
            DataColumn dtcol3 = new DataColumn("Instruments");
            DataColumn dtcol4 = new DataColumn("SCCode");
            DataColumn dtcol5 = new DataColumn("NetRefund");
            DataColumn dtcol6 = new DataColumn("PublishedOn");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "select * from DDERefundLetterRecord where  RGTime>='" + from + "' and RGTime<='" + to + "' order by RLID";
            }
            else
            {
                cmd.CommandText = "select * from DDERefundLetterRecord where  SCCode='" + ddlistSCCode.SelectedItem.Text + "' and RGTime>='" + from + "' and RGTime<='" + to + "' order by RLID";
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
                    drow["RLID"] = Convert.ToInt32(dr["RLID"]);
                    drow["Instruments"] = FindInfo.findInstrumentDetailsIIDS(Convert.ToString(dr["IID"]));
                    drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                    drow["NetRefund"] = Convert.ToString(dr["NetRefund"]);
                    drow["PublishedOn"] = Convert.ToDateTime(dr["RGTime"]).ToString("dd/MM/yyyy").ToUpper();
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }
            

            con.Close();

            dtlistShowDrafts.DataSource = dt;
            dtlistShowDrafts.DataBind();

            con.Close();

            if (i > 1)
            {
                dtlistShowDrafts.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowDrafts.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;

            }
        }


      

        protected void btnOK_Click(object sender, EventArgs e)
        {

        }

        protected void dtlistShowDrafts_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Response.Redirect("RefundLetter.aspx?LNo=" + e.CommandArgument);
        }
    }
}