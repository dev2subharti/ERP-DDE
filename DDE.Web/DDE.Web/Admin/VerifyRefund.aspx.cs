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
    public partial class VerifyRefund : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 88))
            {

                if (!IsPostBack)
                {

                    populateList();


                }

                
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private void populateList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDERefundLetterRecord.RLID,DDERefundLetterRecord.NetRefund,DDERefundInstruments.IType,DDERefundInstruments.INo,DDERefundInstruments.IDate,DDERefundInstruments.IAmount,DDERefundInstruments.IBN from DDERefundLetterRecord inner join DDERefundInstruments on DDERefundInstruments.RIID=DDERefundLetterRecord.RIID where DDERefundLetterRecord.RP='False' and DDERefundLetterRecord.RIID!='1' order by DDERefundLetterRecord.RLID", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("RLID");
            DataColumn dtcol3 = new DataColumn("NetRefund");
            DataColumn dtcol4 = new DataColumn("IAmount");
            DataColumn dtcol5 = new DataColumn("IType");
            DataColumn dtcol6 = new DataColumn("INo");
            DataColumn dtcol7 = new DataColumn("IDate");
            DataColumn dtcol8 = new DataColumn("IBN");

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
                drow["RLID"] = Convert.ToString(dr["RLID"]);
                drow["NetRefund"] = Convert.ToInt32(dr["NetRefund"]);
                drow["IAmount"] = Convert.ToInt32(dr["IAmount"]);
                drow["IType"] =FindInfo.findPaymentModeByID(Convert.ToInt32(dr["IType"]));
                drow["INo"] = Convert.ToString(dr["INo"]);
                drow["IDate"] =Convert.ToDateTime(dr["IDate"]).ToString("dd/MM/yyyy");
                drow["IBN"] = Convert.ToString(dr["IBN"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

           

            con.Close();

            if (i > 1)
            {
                dtlistRL.DataSource = dt;
                dtlistRL.DataBind();
                pnlData.Visible = true;             
                pnlMSG.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! There is no pending 'Reimbursement Letter' for verification";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistRL_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Verify")
                {
                    string rrids = "";
                    int riid = FindInfo.findRIIDByRLID(Convert.ToInt32(e.CommandArgument), out rrids);

                    int rins = updateInstrumnetDetails(riid);
                    int rrl = updateRLDetails(Convert.ToInt32(e.CommandArgument));
                    int rrrid = updateRRecords(rrids);

                    string[] str = rrids.Split(',');
                    if (rins == 1 && rrl == 1 && rrrid == str.Length)
                    {
                        populateList();
                    }
                    else
                    {
                        Log.createErrorLog("Refund Verification","Sorry !! Some error occured.Please contact to ERP Developer",Convert.ToInt32(Session["ERID"]));
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Some error occured.Please contact to ERP Developer";
                        pnlMSG.Visible = true;
                    }


                }
            }
            catch(Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }
        }

        private int updateInstrumnetDetails(int riid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDERefundInstruments set Verified=@Verified,VerifiedBy=@VerifiedBy,VerifiedTime=@VerifiedTime where RIID='" + riid + "' ", con);

            cmd.Parameters.AddWithValue("@Verified", "True");
            cmd.Parameters.AddWithValue("@VerifiedBy", Convert.ToInt32(Session["ERID"]));
            cmd.Parameters.AddWithValue("@VerifiedTime", DateTime.Now.ToString());

            con.Open();
            int rins = cmd.ExecuteNonQuery();
            con.Close();

            return rins;
        }

        private int updateRLDetails(int rlid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDERefundLetterRecord set RP=@RP,RPBy=@RPBy,RPTime=@RPTime where RLID='" + rlid + "' ", con);

            cmd.Parameters.AddWithValue("@RP", "True");
            cmd.Parameters.AddWithValue("@RPBy", Convert.ToInt32(Session["ERID"]));
            cmd.Parameters.AddWithValue("@RPTime", DateTime.Now.ToString());

            con.Open();
            int rrl = cmd.ExecuteNonQuery();
            con.Close();

            return rrl;
        }

        private int updateRRecords(string rrids)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDERefundRecord set RP=@RP where RRID in (" + rrids + ") ", con);

            cmd.Parameters.AddWithValue("@RP", "True");
           
            con.Open();
            int rrrid = cmd.ExecuteNonQuery();
            con.Close();

            return rrrid;
        }

       

       
    }
}