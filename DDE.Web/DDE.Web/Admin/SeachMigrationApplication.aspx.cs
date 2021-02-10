using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class SeachMigrationApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 122))
            {
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int lno;
            int mid;
            string error;


            if (FindInfo.migrationLetterExistBySRID(FindInfo.findSRIDByENo(tbENo.Text), out mid,out lno, out error))
            {             
                Response.Redirect("RequestForMigration.aspx?MID=" + mid);
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = error;
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }


        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }
    }
}