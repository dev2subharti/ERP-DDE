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
    public partial class ShowSLMLetters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95)||Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {
                if (!IsPostBack)
                {

                    PopulateDDList.populateStudyCentreForSLMletters(ddlistSCCode);
                    ddlistSCCode.Items.Add("ALL");
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            polulateLetters();
        }

        private void polulateLetters()
        {
            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text + " 11:59:59 PM";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "Select * from DDESLMLetters where LetterPublishedOn>='" + from + "' and LetterPublishedOn<='" + to + "'  order by LID";
            }
            else
            {
                cmd.CommandText = "Select * from DDESLMLetters where SCCode='" + ddlistSCCode.SelectedItem.Text + "' and LetterPublishedOn>='" + from + "' and LetterPublishedOn<='" + to + "' order by LID";
            }
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("LID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("TotalSLM");
            DataColumn dtcol5 = new DataColumn("PublishedOn");
            DataColumn dtcol6 = new DataColumn("ProcessedOn");
            DataColumn dtcol7 = new DataColumn("By");
            DataColumn dtcol8 = new DataColumn("DocketNo");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = (i + 1).ToString();
                drow["LID"] = ds.Tables[0].Rows[i]["LID"].ToString();
                drow["SCCode"] = ds.Tables[0].Rows[i]["SCCode"].ToString();
                drow["TotalSLM"] = SLM.findTotalSLMByLID(Convert.ToInt32(drow["LID"]));
                drow["PublishedOn"] =Convert.ToDateTime(ds.Tables[0].Rows[i]["LetterPublishedOn"]).ToString("dd-MM-yyyy hh:mm:ss tt");
                if (Convert.ToDateTime(ds.Tables[0].Rows[i]["LetterProcessedOn"]).ToString("dd-MM-yyyy hh:mm:ss tt") == "01-01-1900 12:00:00 AM")
                {
                    drow["ProcessedOn"] = "Pending";
                    drow["By"] = "-";
                    drow["DocketNo"] = "-";
                }
                else
                {
                    drow["ProcessedOn"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["LetterProcessedOn"]).ToString("dd-MM-yyyy hh:mm:ss tt");
                    if (ds.Tables[0].Rows[i]["DType"].ToString() == "1")
                    {
                        drow["By"] = "Courier"; 
                        drow["DocketNo"] =ds.Tables[0].Rows[i]["DocketNo"].ToString();
                    }
                    else if (ds.Tables[0].Rows[i]["DType"].ToString() == "2")
                    {
                        drow["By"] = "By Hand";
                        drow["DocketNo"] = "NA";
                    }
                }
                dt.Rows.Add(drow);

            }

            dtlistShowSLMLetters.DataSource = dt;
            dtlistShowSLMLetters.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowSLMLetters.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowSLMLetters.Visible =false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowSLMLetters_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Show")
            {
                Response.Redirect("SLMLetter.aspx?LNo=" + e.CommandArgument);
            }
            else
            {
                Response.Redirect("PublishStudentListofSLMLetter.aspx?LNo=" + e.CommandArgument);
            }
        }
    }
}