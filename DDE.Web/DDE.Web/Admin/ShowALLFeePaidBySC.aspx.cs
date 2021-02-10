using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class ShowALLFeePaidBySC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {
                if (!IsPostBack)
                {
                    populateFeeHeads();
                    PopulateDDList.populateExam(ddlistExamination);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    ddlistExamination.Items.Add("NA");
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

        private void populateFeeHeads()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeHead where FeePayer='STUDENT' order by FHID", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cblistFeeHead.Items.Add(dr[1].ToString());
                cblistFeeHead.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();

            }

            con.Close();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateStudents();
        }

        private void populateStudents()
        {
            string selectedfeeheads = findSelectedFeeHeads();
        }

        private string findSelectedFeeHeads()
        {
            string selectedValue = "";
            foreach (ListItem item in cblistFeeHead.Items)
            {
                if (item.Selected)
                {
                    if (selectedValue == "")
                    {
                        selectedValue = item.Value;
                    }
                    else
                    {
                        selectedValue = selectedValue + "," + item.Value;
                    }
                }
            }
            return selectedValue;
        }
        protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelectAll.Checked)
            {
                foreach (ListItem item in cblistFeeHead.Items)
                {
                    item.Selected = true;
                }
            }
            else if (!cbSelectAll.Checked)
            {
                foreach (ListItem item in cblistFeeHead.Items)
                {
                    item.Selected = false;
                }
            }
        }

        protected void cblistFeeHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cblistFeeHead.SelectedItem.Value == "1" || cblistFeeHead.SelectedItem.Value == "2" || cblistFeeHead.SelectedItem.Value == "3")
            {
                lblExamination.Visible = true;
                ddlistExamination.Visible = true;
                ddlistExamination.Enabled = true;
            }
            else
            {
                lblExamination.Visible = false;
                ddlistExamination.Visible = false;
                ddlistExamination.Enabled = false;
            }
        }
     
    }
}