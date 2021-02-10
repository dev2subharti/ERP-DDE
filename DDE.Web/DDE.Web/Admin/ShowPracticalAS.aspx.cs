using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class ShowPracticalAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 70))
            {
                PopulateDDList.populateStudyCentre(ddlistSCCode);
                PopulateDDList.populatePracticalCodeForAS(ddlistPracCode);
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                pnlSearch.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["ExamName"] = ddlistExam.SelectedItem.Text;
            Session["PracticalID"] = ddlistPracCode.SelectedItem.Value;
            Session["SCCode"] = ddlistSCCode.SelectedItem.Text;
            Response.Redirect("AwardSheetPrac.aspx");
        }
    }
}