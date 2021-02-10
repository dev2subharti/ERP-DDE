using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace DDE.Web.Admin
{
    public partial class ShowAdmissionDataReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {
                if (!IsPostBack)
                {
                    populateFeeHeads();
                    PopulateDDList.populateExam(ddlistExam);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    ddlistExam.Items.Add("NA");
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

      

        

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            Session["FHIDS"] = findSelectedFeeHeads();
            Session["EntryType"] = ddlistEntryType.SelectedItem.Value;
            Session["From"]= ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text + " 00:00:01 AM";
            Session["To"] = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text + " 11:59:59 PM";
            Session["Year"] = ddlistYear.SelectedItem.Value;
            if (ddlistExam.Enabled == true)
            {
                Session["Exam"] = ddlistExam.SelectedItem.Value;
            }
            else
            {
                Session["Exam"] = "NA";
            }
            Session["ReportType"] = ddlistRT.SelectedItem.Value;
            Session["SCCode"] = ddlistSCCode.SelectedItem.Text;
            Response.Redirect("PublishStudentRecord.aspx");

            
        }

        private string findSelectedFeeHeads()
        {
           string  selectedValue = "";
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
                        selectedValue =selectedValue+","+ item.Value;
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
                ddlistExam.Enabled = true;
            }
            else
            {
                ddlistExam.Enabled = false;
            }
        }

        

    }
}
