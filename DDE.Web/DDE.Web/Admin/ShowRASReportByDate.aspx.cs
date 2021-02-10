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
    public partial class ShowRASReportByDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 64))
            {

                if (!IsPostBack)
                {             
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


        protected void btnPublish_Click(object sender, EventArgs e)
        {
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 23:59:59";

            Session["RecFrom"] = from;
            Session["RecTo"] = to;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct ReceivedBy from DDEAnswerSheetRecord_"+ddlistExamination.SelectedItem.Value+" where TOR>='" + from + "' and TOR<='" + to + "' order by ReceivedBy", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ERID");
            DataColumn dtcol3 = new DataColumn("Employee");
            DataColumn dtcol4 = new DataColumn("TASReceived");
           


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);

            int total = 0;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
               
                drow["ERID"] = Convert.ToInt32(dr["ReceivedBy"]);
                drow["Employee"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["ReceivedBy"]));

                drow["TASReceived"] =findTASR(Convert.ToInt32(dr["ReceivedBy"]), from, to);

                total = total + Convert.ToInt32(drow["TASReceived"]);

                dt.Rows.Add(drow);
               
            }

            dt.DefaultView.Sort = "TASReceived DESC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }

           
            dtlistTASR.DataSource = dt;
            dtlistTASR.DataBind();

            con.Close();
            lblTotal.Text = "Total : " + total.ToString();

            if (j <= 1)
            {
                pnlReport.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
            }
            else
            {
                pnlReport.Visible = true;
                pnlMSG.Visible = false;
            }
        }

        private int findTASR(int erid, string from, string to)
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEAnswerSheetRecord_"+ddlistExamination.SelectedItem.Value+" where ReceivedBy='" + erid + "' and TOR>='" + from + "' and TOR<='" + to + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


           
            int i = 0;
            while (dr.Read())
            {
                
                i = i + 1;
            }

           

            con.Close();

            return i;
        }

        protected void dtlistTASR_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "TASR")
            {
                Session["ExamCode"] = ddlistExamination.SelectedItem.Value;
                Session["ReceivedBy"] = Convert.ToInt32(e.CommandArgument);
                Session["PaperCode"] = "ALL";
                Session["Filter"] = "ALL";
                Response.Redirect("ShowASReceivedByEmployee.aspx");
            }
        }
    }
}
