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
    public partial class UpdateDNoOnSLMLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateStudyCentreForSLMletters(ddlistSCCode);
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


        private void ValidateData()
        {
            foreach (DataListItem dli in dtlistShowLetters.Items)
            {

                TextBox dno = (TextBox)dli.FindControl("tbDNo");
               
                if (dno.Text != "")
                {
                    dno.Enabled = false;

                }
                

              
            }
        }


        private void populateLetter()
        {
            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text + " 11:59:59 PM";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' order by DDESLMLetters.LID";
            }
            else
            {
                cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' and DDESLMLetters.SCCode='" + ddlistSCCode.SelectedItem.Text + "' order by DDESLMLetters.LID";
            }
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("LID");
            DataColumn dtcol3 = new DataColumn("LDate");
            DataColumn dtcol4 = new DataColumn("DDate");
            DataColumn dtcol5 = new DataColumn("SCCode");         
            DataColumn dtcol9 = new DataColumn("NetWeight");       
            DataColumn dtcol12 = new DataColumn("NetAmount");
            DataColumn dtcol13 = new DataColumn("DocketNo");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);         
            dt.Columns.Add(dtcol9);      
            dt.Columns.Add(dtcol12);
            dt.Columns.Add(dtcol13);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = (i + 1).ToString();
                drow["LID"] = ds.Tables[0].Rows[i]["LID"].ToString();
                drow["LDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["LetterPublishedOn"]).ToString("dd-MM-yyyy");
                drow["DDate"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["LetterProcessedOn"]).ToString("dd-MM-yyyy");
                drow["SCCode"] = ds.Tables[0].Rows[i]["SCCode"].ToString();
                drow["NetWeight"] = ds.Tables[0].Rows[i]["TotalPktWeight"].ToString();
                drow["NetAmount"] = ds.Tables[0].Rows[i]["TotalDispatchCharge"].ToString();
                drow["DocketNo"] = ds.Tables[0].Rows[i]["DocketNo"].ToString();

                dt.Rows.Add(drow);
            }

            dtlistShowLetters.DataSource = dt;
            dtlistShowLetters.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowLetters.Visible = true;
                btnSubmit.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowLetters.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (DataListItem dli in dtlistShowLetters.Items)
            {
                Label lbllid = (Label)dli.FindControl("lblLID");
                TextBox tbdno = (TextBox)dli.FindControl("tbDNo");
                Label lbldno = (Label)dli.FindControl("lblDNo");
                if (tbdno.Text != lbldno.Text)
                {
                   
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDESLMLetters set DocketNo=@DocketNo where LID='" + lbllid.Text + "' ", con);

                    cmd.Parameters.AddWithValue("@DocketNo", tbdno.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    i = i + 1;
                }



            }

            if (i > 0)
            {
                pnlData.Visible = false;
                lblMSG.Text = "'"+i.ToString()+ "' Docket Numbers have been saved successfully!!";
                pnlMSG.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not change any Docket No.";
                pnlMSG.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateLetter();
            ValidateData();                      
        }
        
    }
}