using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class SearchSLMSellletterByLNo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 100))
            {
                pnlData.Visible = true;
                pnlSearch.Visible = true;
                pnlMSG.Visible = false;
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
            try
            {
                if (SLM.slmSaleLetterExist(Convert.ToInt32(tbLNo.Text)))
                {
                    Response.Redirect("SLMSellLetter.aspx?LNo=" + tbLNo.Text);
                }
                else
                {
                   
                    lblMSG.Text = "Sorry !! No Letter exist with this No.";
                    pnlMSG.Visible = true;
                }
            }
            catch (Exception ex)
            {
               
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }
        }
    }
}