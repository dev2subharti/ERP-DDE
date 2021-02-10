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
    public partial class ShowQuestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 108))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populatePaperCodeFromQB(ddlistPaperCode);
                    if (Request.QueryString["PaperCode"] != null)
                    {
                        ddlistPaperCode.SelectedItem.Selected = false;
                        ddlistPaperCode.Items.FindByText(Request.QueryString["PaperCode"]).Selected = true;
                        ddlistPaperCode.Enabled = false;
                        populateQuestions();
                    }
                                
                }

                if (rblType.SelectedItem.Value == "0")
                {
                    dtlistShowQuestions.CssClass = "ef";
                }
                else if (rblType.SelectedItem.Value == "1")
                {
                    dtlistShowQuestions.CssClass = "hf";
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateQuestions();
        }

        private void populateQuestions()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from QuestionBank where PaperCode='" + ddlistPaperCode.SelectedItem.Value + "' order by QID", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("QID");
            DataColumn dtcol3 = new DataColumn("Question");
            DataColumn dtcol4 = new DataColumn("A");
            DataColumn dtcol5 = new DataColumn("B");
            DataColumn dtcol6 = new DataColumn("C");
            DataColumn dtcol7 = new DataColumn("D");
            DataColumn dtcol8 = new DataColumn("Key");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i + 1;
                drow["QID"] = ds.Tables[0].Rows[i]["QID"].ToString();
                drow["Question"] = ds.Tables[0].Rows[i]["Question"].ToString();
                drow["A"] = ds.Tables[0].Rows[i]["A"].ToString();
                drow["B"] = ds.Tables[0].Rows[i]["B"].ToString();
                drow["C"] = ds.Tables[0].Rows[i]["C"].ToString();
                drow["D"] = ds.Tables[0].Rows[i]["D"].ToString();
                drow["Key"] = ds.Tables[0].Rows[i]["Ans"].ToString();
                dt.Rows.Add(drow);
                
            }

            dtlistShowQuestions.DataSource = dt;
            dtlistShowQuestions.DataBind();
        }

        protected void dtlistShowQuestions_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName=="Delete")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                SqlCommand cmd = new SqlCommand("delete from QuestionBank where QID ='" + Convert.ToString(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Delete", "Deleted a Question from Paper Code '" + ddlistPaperCode.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                populateQuestions();
            }
            else if(e.CommandName == "Edit")
            {
                Response.Redirect("CreateQuestion.aspx?QID="+e.CommandArgument);
            }
        }

        protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rblType.SelectedItem.Value=="0")
            {
                dtlistShowQuestions.CssClass = "ef";
            }
            else if (rblType.SelectedItem.Value == "1")
            {
                dtlistShowQuestions.CssClass = "hf";
            }
        }
    }
}