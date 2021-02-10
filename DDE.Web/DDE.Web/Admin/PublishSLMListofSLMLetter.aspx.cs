using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class PublishSLMListofSLMLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 93) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {
                if (!IsPostBack)
                {
                    populateSLMList();
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

        private void populateSLMList()
        {
            
        }
    }
}