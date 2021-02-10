using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class CreateQuestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 108))
            {
                if (!IsPostBack)
                {
                   
                    if (Request.QueryString["QID"] != null)
                    {
                        populateQuestionID();
                        btnSubmit.Text = "Update";

                    }
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }




        }

        private void populateQuestionID()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineExam"].ToString());
            SqlCommand cmd = new SqlCommand("select * from QuestionBank where QID='" + Request.QueryString["QID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbPaperCode.Text = dr["PaperCode"].ToString();
                tbQuestion.Text = dr["Question"].ToString();
                tbA.Text = dr["A"].ToString();
                tbB.Text = dr["B"].ToString();
                tbC.Text = dr["C"].ToString();
                tbD.Text = dr["D"].ToString();
                tbAns.Text = dr["Ans"].ToString();
            }

            con.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["QID"] == null)
            {
                if (!isQuestionExist(tbQuestion.Text, tbPaperCode.Text))
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "insert into QuestionBank (PaperCode,QT,Question,A,B,C,D,Ans) OUTPUT INSERTED.QID values (@PaperCode,@QT,@Question,@A,@B,@C,@D,@Ans)";

                    cmd.Parameters.AddWithValue("@PaperCode", tbPaperCode.Text);
                    cmd.Parameters.AddWithValue("@QT", 0);
                    cmd.Parameters.AddWithValue("@Question", tbQuestion.Text);

                    cmd.Parameters.AddWithValue("@A", tbA.Text);
                    cmd.Parameters.AddWithValue("@B", tbB.Text);
                    cmd.Parameters.AddWithValue("@C", tbC.Text);
                    cmd.Parameters.AddWithValue("@D", tbD.Text);

                    cmd.Parameters.AddWithValue("@Ans", tbAns.Text);

                    cmd.Connection = con;

                    con.Open();
                    object qid = cmd.ExecuteScalar();
                    con.Close();



                    pnlData.Visible = false;
                    lblMSG.Text = "Question has been created successfully";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! this question is already exist";
                    pnlMSG.Visible = true;
                }
            }

            else if (Request.QueryString["QID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                SqlCommand cmd = new SqlCommand("update QuestionBank set PaperCode=@PaperCode,Question=@Question,A=@A,B=@B,C=@C,D=@D,Ans=@Ans where QID='" + Request.QueryString["QID"] + "' ", con);

                cmd.Parameters.AddWithValue("@PaperCode", tbPaperCode.Text);
         
                cmd.Parameters.AddWithValue("@Question", tbQuestion.Text);

                cmd.Parameters.AddWithValue("@A", tbA.Text);
                cmd.Parameters.AddWithValue("@B", tbB.Text);
                cmd.Parameters.AddWithValue("@C", tbC.Text);
                cmd.Parameters.AddWithValue("@D", tbD.Text);

                cmd.Parameters.AddWithValue("@Ans", tbAns.Text);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
              
                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = true;


            }

        }

        private bool isQuestionExist(string question, string papercode)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlCommand scmd = new SqlCommand("Select QID from QuestionBank where Question='" + question + "' and PaperCode='" + papercode + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();
            return exist;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateQuestion.aspx");
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
           
        }

       
    }
}