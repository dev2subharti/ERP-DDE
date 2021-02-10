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
    public partial class UpdateASAllotted : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
            {
                PopulateDDList.populateExam(ddlistExam);
          
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pupulateandSetExaminer();
        }

        private void pupulateandSetExaminer()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ECID,City,ExamCentreCode,CentreName from DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + " order by ExamCentreCode", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistCEC.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString());
                ddlistCEC.Items.FindByText("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString()).Value = dr[0].ToString();

                ddlistNEC.Items.Add("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString());
                ddlistNEC.Items.FindByText("(" + dr[2].ToString() + ") " + dr[3].ToString() + ", " + dr[1].ToString()).Value = dr[0].ToString();

            }
            con.Close();



            ddlistCEC.Items.Add("NOT FOUND");
            ddlistCEC.Items.FindByText("NOT FOUND").Value = "0";
           
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd1 = new SqlCommand("update DDEExamRecord_" + ddlistExam.SelectedItem.Value + " set ExamCentreCode=@ExamCentreCode where Exam='" + ddlistExam.SelectedItem.Value + "'", con);


            cmd1.Parameters.AddWithValue("@ExamCentreCode", ddlistNEC.SelectedItem.Value);


            con.Open();

            cmd1.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Update Exam Centre", "Updated Exam Centre for " + ddlistExam.SelectedItem.Text + " exam from '" + ddlistCEC.SelectedItem.Value + "' to '" + ddlistNEC.SelectedItem.Value + "' with Enrollment No '" + tbASNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

            pnlData.Visible = false;
            lblMSG.Text = "Record has been updated successfully";
            pnlMSG.Visible = true;
        }
    }
}