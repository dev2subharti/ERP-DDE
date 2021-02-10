using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class UpdateASAllotment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 70))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("W10").Selected = true;                  
                    pnlSearch.Visible = true;
                    pnlASDetails.Visible = false;
                    btnUpdate.Visible = false;
                    
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                int times;
                if (FindInfo.isASPrintedByASNo(Convert.ToInt32(tbASNo.Text), ddlistExam.SelectedItem.Value, out times))
                {
                    PopulateDDList.populateExaminers(ddlistCE, ddlistExam.SelectedItem.Value);
                    ddlistCE.Items.FindByValue(FindInfo.findAllottedFIDByASNo(Convert.ToInt32(tbASNo.Text), ddlistExam.SelectedItem.Value).ToString()).Selected = true;
                    ddlistCE.Enabled = false;
                    PopulateDDList.populateExaminers(ddlistNE, ddlistExam.SelectedItem.Value);
                    tbASNo.Enabled = false;
                    btnSearch.Visible = false;
                    pnlASDetails.Visible = true;
                    btnUpdate.Visible = true;
                }
                else
                {
                    pnlASDetails.Visible = false;
                    btnUpdate.Visible = false;
                    lblMSG.Text = "Sorry !! There in no Award Sheet with this no.";
                    pnlMSG.Visible = true;
                }
            }
            catch (FormatException ex)
            {
                pnlASDetails.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = "Sorry!! type in numeric format";
                pnlMSG.Visible = true;
            }
            catch (Exception ex)
            {
                pnlASDetails.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if ((ddlistCE.SelectedItem.Value != ddlistNE.SelectedItem.Value) && (ddlistNE.SelectedItem.Value != "1000") && (ddlistNE.SelectedItem.Value != "0"))
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("update DDEASPrintRecord_" + ddlistExam.SelectedItem.Value + " set AllottedTo=@AllottedTo where ASPRID='" + Convert.ToInt32(tbASNo.Text) + "'", con);

                cmd1.Parameters.AddWithValue("@AllottedTo", Convert.ToInt32(ddlistNE.SelectedItem.Value));
                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Update", "Updated Award Sheet Allotment of ASNo '" + tbASNo.Text + "' from '" + ddlistCE.SelectedItem.Text + "' to '" + ddlistNE.SelectedItem.Text + "' for  '" + ddlistExam.SelectedItem.Text + "' exam", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
                btnOK.Text = "Change Another";
                btnOK.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any new examiner on this Award Sheet";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "OK")
            {
                btnOK.Visible = false;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
              
            }
            else if (btnOK.Text == "Change Another")
            {
                Response.Redirect("UpdateASAllotment.aspx");
            }
        }
    }
}