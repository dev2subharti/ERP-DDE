using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class PublishListsOfSLMLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
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

        protected void btnPublishStudentList_Click(object sender, EventArgs e)
        {
            try
            {
                if (SLM.slmLetterExist(Convert.ToInt32(tbLNo.Text)))
                {
                    Response.Redirect("PublishStudentListofSLMLetter.aspx?LNo=" + tbLNo.Text);
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! No Letter exist with this No.";
                    pnlMSG.Visible = true;
                }
            }
            catch (Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }

        }

        //protected void btnPublishSLMSetList_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (SLM.slmLetterExist(Convert.ToInt32(tbLNo.Text)))
        //        {
        //            Response.Redirect("SLMLetter.aspx?LNo=" + tbLNo.Text);
        //        }
        //        else
        //        {
                   
        //            lblMSG.Text = "Sorry !! No Letter exist with this No.";
        //            pnlMSG.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMSG.Text = ex.Message;
        //        pnlMSG.Visible = true;
        //    }
        //}
    }
}