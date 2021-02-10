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
    public partial class ShowAmountDistribution : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 75)||Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 79)|| Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 102))
                {
                    if (!IsPostBack)
                    {

                        pnlSearch.Visible = true;

                        pnlData.Visible = true;

                        pnlMSG.Visible = false;
                    }

                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! You are not authorised for this control";
                    pnlMSG.Visible = true;
                }
           
        }

        private void populateFeeHeads(string fhs)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FHID");
            DataColumn dtcol3 = new DataColumn("FeeHead");
            DataColumn dtcol4 = new DataColumn("Amount");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from DDEFeeHead where FHID in (" + fhs + ") order by FHID";


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
                    drow["FHID"] = dr["FHID"].ToString();
                    drow["FeeHead"] = dr["FeeHead"].ToString();
                    drow["Amount"] = "";


                    dt.Rows.Add(drow);
                    i = i + 1;
                }
            }



            dtlistFeeHeads.DataSource = dt;
            dtlistFeeHeads.DataBind();

            con.Close();

        }
      
        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "OK")
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }

            else if (btnOK.Text == "Distribute More Instruments")
            {
                Response.Redirect("DistributeAmount.aspx");
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {


            populateTotalInstruments();
            setStatus();


        }

        private void setStatus()
        {
            foreach (DataListItem dli in dtlistTotalInstruments.Items)
            {
                Label ver = (Label)dli.FindControl("lblVerified");
                LinkButton dis = (LinkButton)dli.FindControl("lnkbtnShow");
                if (ver.Text == "True")
                {
                    dis.Enabled = true;
                }
                else if (ver.Text == "True")
                {
                    dis.Enabled = false;
                    dis.Text = "Not Verified";
                }
                else
                {
                    dis.Enabled = false;
                    dis.Text = "Not Verified";
                }
            }
        }

        private void populateTotalInstruments()
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("select * from DDEFeeInstruments where INo='" + tbDCNo.Text + "'", con1);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("IID");
            DataColumn dtcol3 = new DataColumn("Type");
            DataColumn dtcol4 = new DataColumn("TypeNo");
            DataColumn dtcol5 = new DataColumn("No");
            DataColumn dtcol6 = new DataColumn("Date");
            DataColumn dtcol7 = new DataColumn("TotalAmount");
            DataColumn dtcol8 = new DataColumn("IBN");
            DataColumn dtcol9 = new DataColumn("Verified");
            DataColumn dtcol10 = new DataColumn("Distributed");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);

            con1.Open();
            dr1 = cmd1.ExecuteReader();
            int i = 1;
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["IID"] = Convert.ToString(dr1["IID"]);
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr1["IType"]));
                    drow["No"] = Convert.ToString(dr1["INo"]);
                    drow["Date"] = Convert.ToDateTime(dr1["IDate"]).ToString("dd MMMM yyyy");
                    drow["TotalAmount"] = Convert.ToInt32(dr1["TotalAmount"]);
                    drow["IBN"] = Convert.ToString(dr1["IBN"]);
                    drow["Verified"] = Convert.ToString(dr1["Verified"]);
                    drow["Distributed"] = Convert.ToString(dr1["AmountAlloted"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }
            con1.Close();




            dtlistTotalInstruments.DataSource = dt;
            dtlistTotalInstruments.DataBind();

            if (i > 1)
            {
                dtlistTotalInstruments.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Insrument exist with this no.";
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
            }
        }

        private void populateDCDetails(int iid)
        {
            string fhs = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where IID='" + iid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tbDType.Text = Accounts.findMOPByID(Convert.ToInt32(dr["IType"]));
                tbDNo.Text = dr["INo"].ToString();
                lblIID.Text = Convert.ToInt32(dr["IID"]).ToString();
                tbDCDate.Text = Convert.ToDateTime(dr["IDate"]).ToString("dd MMMM yyyy").ToUpper();
                tbIBN.Text = dr["IBN"].ToString().ToUpper();
                tbTotalAmount.Text = Convert.ToInt32(dr["TotalAmount"]).ToString();
                tbSCCode.Text = dr["SCCode"].ToString();
                fhs = dr["AllotedFeeHeads"].ToString();
                populateFeeHeads(fhs);

                foreach (DataListItem dli in dtlistFeeHeads.Items)
                {
                    TextBox am = (TextBox)dli.FindControl("tbAmount");
                    Label fhid = (Label)dli.FindControl("lblFHID");

                    am.Text = dr["FH" + fhid.Text].ToString();

                }

                tbBalance.Text = Convert.ToInt32(dr["Balance"]).ToString();
            }


            con.Close();

            dtlistFeeHeads.Visible = true;
          
            pnlBalance.Visible = true;
            pnlDCDetail.Visible = true;

        }

        protected void dtlistTotalInstruments_ItemCommand(object source, DataListCommandEventArgs e)
        {
            populateDCDetails(Convert.ToInt32(e.CommandArgument));
            dtlistTotalInstruments.Visible = false;
            pnlSearch.Visible = false;
            
        }
    }
}