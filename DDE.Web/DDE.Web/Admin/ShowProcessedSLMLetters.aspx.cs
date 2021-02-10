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
    public partial class ShowProcessedSLMLetters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
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


        private void populateLetter()
        {
            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text + " 11:59:59 PM";
            double totalwt = 0;
            double totalcharge = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {
                if (ddlistDType.SelectedItem.Value == "0")
                {
                    cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' order by DDESLMLetters.LID";
                }    
                else if (ddlistDType.SelectedItem.Value == "1")
                {
                    cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' and DDESLMLetters.DType='" + ddlistDType.SelectedItem.Value + "' and DDESLMLetters.DPID='"+ddlistDParty.SelectedItem.Value+"' order by DDESLMLetters.LID";
                }
                else if (ddlistDType.SelectedItem.Value == "2")
                {
                    cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' and DDESLMLetters.DType='" + ddlistDType.SelectedItem.Value + "' order by DDESLMLetters.LID";
                }              
                
            }
            else
            {
                if (ddlistDType.SelectedItem.Value == "0")
                {
                    cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' and DDESLMLetters.SCCode='" + ddlistSCCode.SelectedItem.Text + "' order by DDESLMLetters.LID";
                }  
                else  if (ddlistDType.SelectedItem.Value == "1")
                {
                    cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' and DDESLMLetters.SCCode='" + ddlistSCCode.SelectedItem.Text + "' and DDESLMLetters.DType='" + ddlistDType.SelectedItem.Value + "' and DDESLMLetters.DPID='" + ddlistDParty.SelectedItem.Value + "' order by DDESLMLetters.LID";
                }
                else if (ddlistDType.SelectedItem.Value == "2")
                {
                    cmd.CommandText = "Select DDESLMLetters.LID,DDESLMLetters.SCCode,DDEStudyCentres.Administrator,DDEStudyCentres.Address,DDEStudyCentres.City,DDESLMLetters.LetterpublishedOn,DDESLMLetters.LetterProcessedOn,DDESLMLetters.TotalPktWeight,DDESLMLetters.TotalDispatchCharge,DDESLMLetters.DocketNo from DDESLMLetters inner join DDEStudyCentres on DDEStudyCentres.SCCode=DDESLMLetters.SCCode where CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)>='" + from + "' and CONVERT(datetime,DDESLMLetters.LetterProcessedOn,105)<='" + to + "' and DDESLMLetters.SCCode='" + ddlistSCCode.SelectedItem.Text + "' and DDESLMLetters.DType='" + ddlistDType.SelectedItem.Value + "' order by DDESLMLetters.LID";
                }
               
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
            DataColumn dtcol6 = new DataColumn("PName");
            DataColumn dtcol7 = new DataColumn("Address");
            //DataColumn dtcol8 = new DataColumn("City");
            DataColumn dtcol9= new DataColumn("NetWeight");
            DataColumn dtcol10 = new DataColumn("PSNo");
            DataColumn dtcol11 = new DataColumn("PWeight");
            DataColumn dtcol12 = new DataColumn("NetAmount");
            DataColumn dtcol13 = new DataColumn("DocketNo");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            //dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);
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
                drow["PName"] = ds.Tables[0].Rows[i]["Administrator"].ToString();
                drow["Address"] = ds.Tables[0].Rows[i]["Address"].ToString() + "," + ds.Tables[0].Rows[i]["City"].ToString();
                //drow["City"] = ds.Tables[0].Rows[i]["City"].ToString();
                drow["PSNo"] = 0;
                drow["PWeight"] = 0;
                drow["NetWeight"] = ds.Tables[0].Rows[i]["TotalPktWeight"].ToString();
                totalwt = totalwt + Convert.ToDouble(drow["NetWeight"]);
                drow["NetAmount"] = ds.Tables[0].Rows[i]["TotalDispatchCharge"].ToString();
                totalcharge = totalcharge + Convert.ToDouble(drow["NetAmount"]);
                drow["DocketNo"] = ds.Tables[0].Rows[i]["DocketNo"].ToString();
              
                dt.Rows.Add(drow);
            }

           
            dtlistShowLetters.DataSource = dt;
            dtlistShowLetters.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {                        
                pnlProcessedLetters.Visible = true;
                pnlMSG.Visible = false;                            
            }
            else
            {
                pnlProcessedLetters.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }

            ViewState["TotalWt"] = totalwt.ToString();
            ViewState["TotalCharge"] = totalcharge.ToString();
        }

        private void populateAttachedPackets()
        {
            int pktcounter = 0;
            foreach (DataListItem dli in dtlistShowLetters.Items)
            {
                DataList packets= (DataList)dli.FindControl("dtlistShowPackets");
                Label lid = (Label)dli.FindControl("lblLID");



                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDESLMPackets where LID='" + lid.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("PSNo");
                DataColumn dtcol2 = new DataColumn("PWeight");
              


                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
              


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["PSNo"] = (i + 1).ToString();
                    drow["PWeight"] = ds.Tables[0].Rows[i]["PWeight"].ToString();
                  
                    dt.Rows.Add(drow);
                    pktcounter = pktcounter + 1;
                }

                packets.DataSource = dt;
                packets.DataBind();

            }

            ViewState["TotalPkt"] = pktcounter.ToString();
        }





       

        protected void btnSearch_Click(object sender, EventArgs e)
        {           
            populateLetter();
            populateAttachedPackets();
            lblTotalPkt.Text = ViewState["TotalPkt"].ToString();
            lblTotalWt.Text = ViewState["TotalWt"].ToString();
            lblTotalCharge.Text = ViewState["TotalCharge"].ToString();
      
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlProcessedLetters.Visible = false;
        }

        protected void ddlistDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlProcessedLetters.Visible = false;

            if (ddlistDType.SelectedItem.Value == "1")
            {
                lblDParty.Visible = true;
                ddlistDParty.Visible = true;
                ddlistDParty.Items.Clear();
                PopulateDDList.populateDParty(ddlistDParty);
            }
            else
            {
                lblDParty.Visible = false;
                ddlistDParty.Visible = false;

            }
        }

        protected void ddlistDParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlProcessedLetters.Visible = false;
        }

       




        
    }
}