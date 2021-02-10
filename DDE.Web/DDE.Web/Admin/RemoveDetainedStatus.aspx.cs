using DDE.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDE.Web.Admin
{
    public partial class RemoveDetainedStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                pnlSearch.Visible = true;
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
            int srid = FindInfo.findSRIDByOANo(Convert.ToInt32(tbOANo.Text));
            if (srid != 0)
            {
                string remark;
                if (FindInfo.isDetained(srid, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out remark))
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDEDetainedStudents set DetainedStatus=@DetainedStatus where SRID='" + srid + "' and Examination='" + ddlistExam.SelectedItem.Value + "' and MOE='" + ddlistMOE.SelectedItem.Value + "' ", con);

                    cmd.Parameters.AddWithValue("@DetainedStatus", "False");

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Update Detained Record", "Removed a student from Detained List in '" + ddlistMOE.SelectedItem.Text + "'" + ddlistExam.SelectedItem.Text + " Examination with Enrollment No '" + FindInfo.findENoByID(srid), Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "Removed from Detained List Successfully.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Student is not in Detained List.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found with this OANo";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }


        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
            pnlData.Visible = true;
            tbOANo.Text = "";
        }

       
    }
}