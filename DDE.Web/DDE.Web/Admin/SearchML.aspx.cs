using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class SearchML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 120) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 121))
            {
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
                if (!IsPostBack)
                {
                    lblID.Text = "Enrollment No.";
                }


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
            
            string mids;
            int mid;
            string error;
            int lno;
            if (rblMode.SelectedItem.Value == "1")
            {
                if (FindInfo.migrationLetterExistBySRID(FindInfo.findSRIDByENo(tbLNo.Text),out mid,out lno, out error))
                {

                    Session["MIDS"] =FindInfo.findMigrationEntriesByMLNo(lno);
                    Response.Redirect("MigrationLetter.aspx?MLID="+lno.ToString());
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = error;
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                if (FindInfo.migrationLetterExistByNo(Convert.ToInt32(tbLNo.Text) , out mids))
                {
                    Session["MIDS"] = mids;
                    Response.Redirect("MigrationLetter.aspx?MLID=" + tbLNo.Text);
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! There is no Migration Letter with this no.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
        }

       

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "1")
            {
                lblID.Text = "Enrollment No.";
                tbLNo.Text = "";

            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                lblID.Text = "Letter No.";
                tbLNo.Text = "";


            }
        }
    }
}