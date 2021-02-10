using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DDE.DAL;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class SetRequiredFee : System.Web.UI.Page
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateRequiredFee();
            setColor();
        }

        private void setColor()
        {
            foreach (DataListItem dli in dtlistShowReqAmount.Items)
            {
                Label reqfee = (Label)dli.FindControl("lblRequiredFee");

                if (reqfee.Text == "EXPIRED")
                {
                    reqfee.BackColor = System.Drawing.Color.FromName("#F37E01");
                }

            }
        }

        private void populateRequiredFee()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("select * from DDERequiredFeeRecord where Batch='" + ddlistBatch.SelectedItem.Text + "'", con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("RFID");
            DataColumn dtcol3 = new DataColumn("FHID");
            DataColumn dtcol4 = new DataColumn("FeeHead");
            DataColumn dtcol5 = new DataColumn("RequiredFee");
            DataColumn dtcol6 = new DataColumn("From");
            DataColumn dtcol7 = new DataColumn("To");
         

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["RFID"] = dr["RFID"].ToString();
                drow["FHID"] = dr["FHID"].ToString();
                drow["FeeHead"] = FindInfo.findFeeHeadNameByID(Convert.ToInt32(dr["FHID"]));
                if (Convert.ToDateTime(dr["TPFrom"]) <= Convert.ToDateTime(FindInfo.findTodayDate()) && Convert.ToDateTime(dr["TPTo"]) >= Convert.ToDateTime(FindInfo.findTodayDate()))
                {
                    drow["RequiredFee"] = dr["RequiredFee"].ToString();
                }

                else
                {
                    drow["RequiredFee"] = "EXPIRED";
                }
                drow["From"] =Convert.ToDateTime(dr["TPFrom"]).ToString("dd-MM-yyyy");
                drow["To"] = Convert.ToDateTime(dr["TPTo"]).ToString("dd-MM-yyyy");

                dt.Rows.Add(drow);
                i = i + 1;
            }


            

            dtlistShowReqAmount.DataSource = dt;
            dtlistShowReqAmount.DataBind();

            con.Close();

            if (i > 1)
            {

                dtlistShowReqAmount.Visible = true;

                pnlMSG.Visible = false;

            }

            else
            {
                dtlistShowReqAmount.Visible = false;

                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowReqAmount_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }
    }
}
