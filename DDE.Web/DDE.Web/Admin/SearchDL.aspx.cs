using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class SearchDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85))
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
          

            if (rblLetter.SelectedItem.Value == "1")
            {
                int diid;
                string error;

                if (ddlistSearchType.SelectedItem.Value == "1")
                {
                    if (FindInfo.degreeLetterExistBySRID(FindInfo.findSRIDByENo(tbLNo.Text), out diid, out error))
                    {
                        Session["DIID"] = diid;
                        Session["CF"] = "Old";
                        Response.Redirect("DegreeLetter.aspx");
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = error;
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }
                else if (ddlistSearchType.SelectedItem.Value == "2")
                {
                    if (FindInfo.degreeLetterExistByNo(tbLNo.Text))
                    {
                        Session["DIID"] = tbLNo.Text;
                        Session["CF"] = "Old";
                        Response.Redirect("DegreeLetter.aspx");
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! There is no Degree Letter with this no.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }
            }
            else if (rblLetter.SelectedItem.Value == "2")
            {
                int cldid;
                string error;

                if (ddlistSearchType.SelectedItem.Value == "1")
                {
                    if (FindInfo.CLDegreeExistBySRID(FindInfo.findSRIDByENo(tbLNo.Text), out cldid, out error))
                    {
                        Session["CLDID"] = cldid;
                        Session["CF"] = "Old";
                        Response.Redirect("CoveringLetterDegree.aspx?CLDID=" + cldid);
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = error;
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }
                else if (ddlistSearchType.SelectedItem.Value == "2")
                {
                    if (FindInfo.CLDegreeExistByNo(tbLNo.Text))
                    {
                        Session["CLDID"] = tbLNo.Text;
                        Session["CF"] = "Old";
                        Response.Redirect("CoveringLetterDegree.aspx?CLDID=" + tbLNo.Text);
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! There is no Covering Degree Letter with this no.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }
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