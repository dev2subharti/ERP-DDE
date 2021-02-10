using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class SCRefunds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 58))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistBatch);
                  
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (ddlistSCType.SelectedItem.Value == "1")
            {
                populateDirectRefunds();
                
            }

            else if (ddlistSCType.SelectedItem.Value == "2")
            {
                 populateWemRefunds();
              
            }

        }

        private void populateWemRefunds()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("select SCCode from DDEStudyCentres where RecommendedBy='" + ddlistSCType.SelectedItem.Text + "'", con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("ReqFee");
            DataColumn dtcol4 = new DataColumn("LateFee");
            DataColumn dtcol5 = new DataColumn("PaidFee");
            DataColumn dtcol6 = new DataColumn("DueFee");
            DataColumn dtcol7 = new DataColumn("University");
            DataColumn dtcol8 = new DataColumn("WEM");
            DataColumn dtcol9 = new DataColumn("Refund");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;

                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                int reqfee = Accounts.findReqFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 1);
                drow["ReqFee"] = reqfee.ToString();
                int latefee = Accounts.findReqFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 12);
                drow["LateFee"] = latefee.ToString();
                int paidfee = Accounts.findPaidFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 1) + Accounts.findPaidFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 12);
                drow["PaidFee"] = paidfee.ToString();
                int duefee = ((reqfee+latefee) - paidfee);
                drow["DueFee"] = duefee.ToString();

              
                int onlypf = Accounts.findPaidFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 1);

                float fifty = Accounts.findPercentageOf(reqfee, 50);
                float ten = Accounts.findPercentageOf(reqfee, 10);
                if (onlypf >= fifty)
                {
                    drow["University"] = fifty.ToString();
                    if ((onlypf - fifty) >= ten)
                    {
                        drow["WEM"] = ten.ToString();
                        drow["Refund"] = (onlypf - (fifty + ten)).ToString();
                    }
                    else if ((onlypf - fifty) < ten)
                    {
                        drow["WEM"] = (onlypf - fifty).ToString();
                        drow["Refund"] = 0;
                    }
                    
                }

                else if (onlypf < fifty)
                {
                    drow["University"] = onlypf.ToString();
                    drow["WEM"] = 0;
                    drow["Refund"] = 0;
                }


                dt.Rows.Add(drow);
                i = i + 1;
            }


            if (i > 1)
            {

                dtlistDirectSC.Visible = false;
                dtlistWEMSC.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                dtlistDirectSC.Visible = false;
                dtlistWEMSC.Visible = true;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

            dtlistWEMSC.DataSource = dt;
            dtlistWEMSC.DataBind();

            con.Close();
           
            
        }

        private void populateDirectRefunds()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("select SCCode from DDEStudyCentres where RecommendedBy='"+ddlistSCType.SelectedItem.Text+"'", con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("ReqFee");
            DataColumn dtcol4 = new DataColumn("LateFee");
            DataColumn dtcol5 = new DataColumn("PaidFee");
            DataColumn dtcol6 = new DataColumn("DueFee");
            DataColumn dtcol7 = new DataColumn("University");
            DataColumn dtcol8 = new DataColumn("Refund");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;

                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                int reqfee = Accounts.findReqFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 1);
                drow["ReqFee"] = reqfee.ToString();
                int latefee = Accounts.findReqFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 12);
                drow["LateFee"] = latefee.ToString();
                int paidfee = Accounts.findPaidFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 1) + Accounts.findPaidFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 12);
                drow["PaidFee"] = paidfee.ToString();
                int duefee = ((reqfee + latefee) - paidfee);
                drow["DueFee"] = duefee.ToString();

               
                int onlypf = Accounts.findPaidFeeofSC(dr["SCCode"].ToString(), ddlistBatch.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), 1);

                float sixty = Accounts.findPercentageOf(reqfee, 60);
                if (onlypf >= sixty)
                {
                    drow["University"] = sixty.ToString();
                    drow["Refund"] = (onlypf - sixty).ToString();
                }

                else if (onlypf < sixty)
                {
                    drow["University"] = onlypf.ToString();
                    drow["Refund"] = 0;
                }
                
               
                dt.Rows.Add(drow);
                i = i + 1;
            }


            if (i > 1)
            {

                dtlistDirectSC.Visible = true;
                dtlistWEMSC.Visible = false;
                pnlMSG.Visible = false;

            }

            else
            {
                dtlistDirectSC.Visible = false;
                dtlistWEMSC.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

            dtlistDirectSC.DataSource = dt;
            dtlistDirectSC.DataBind();

            con.Close();
           
        }
    }
}
