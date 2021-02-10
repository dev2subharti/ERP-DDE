using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace DDE.Web.Admin
{
    public partial class SetQP1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 106))
            {

                if (!IsPostBack)
                {
                    lblPaperCode.Text = Session["PaperCode"].ToString();
                    lblSubjectCode.Text = FindInfo.findSubjectCodesByPaperCode(Session["PaperCode"].ToString(), Session["SySession"].ToString());
                    lblExamCode.Text = Session["ExamCode"].ToString();
                    lblExamName.Text = Session["ExamName"].ToString();
                    lblMOE.Text = Session["MOE"].ToString();
                    lblMOEA.Text = Session["MOEA"].ToString();
                    lblYear.Text = FindInfo.findAlphaYear(Session["Year"].ToString());
                    lblSubjectName.Text = FindInfo.findSubjectNameByPaperCode(Session["PaperCode"].ToString(), Session["SySession"].ToString());
                    populateQuestions();

                }

               
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private void populateQuestions()
        {
            try
            {
                if (lblPaperCode.Text != "DCS-111" && lblPaperCode.Text != "DSC-301" && lblPaperCode.Text != "DSC-302" && lblPaperCode.Text != "DCS-217" && lblPaperCode.Text != "DSC-201" && lblPaperCode.Text != "DSC-202" && lblPaperCode.Text != "DSC-101" && lblPaperCode.Text != "DSC-102")
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    if (lblMOE.Text == "R")
                    {
                        cmd.CommandText = "select * from QuestionBank where PaperCode='" + lblPaperCode.Text + "' order by QID";
                    }
                    else if (lblMOE.Text == "B")
                    {
                        cmd.CommandText = "select * from QuestionBank where PaperCode='" + lblPaperCode.Text + "' order by newid()";
                    }

                    DataTable dt = new DataTable();
                    DataColumn dtcol1 = new DataColumn("SNo");
                    DataColumn dtcol2 = new DataColumn("QID");
                    DataColumn dtcol3 = new DataColumn("QT");
                    DataColumn dtcol4 = new DataColumn("Question");
                    DataColumn dtcol5 = new DataColumn("A");
                    DataColumn dtcol6 = new DataColumn("B");
                    DataColumn dtcol7 = new DataColumn("C");
                    DataColumn dtcol8 = new DataColumn("D");



                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);
                    dt.Columns.Add(dtcol4);
                    dt.Columns.Add(dtcol5);
                    dt.Columns.Add(dtcol6);
                    dt.Columns.Add(dtcol7);
                    dt.Columns.Add(dtcol8);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.SelectCommand.Connection = con;
                    da.Fill(ds);
                    int i = 1;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 93; j < 153; j++)
                        {

                            DataRow drow = dt.NewRow();
                            drow["SNo"] = i;
                            drow["QID"] = Convert.ToInt32(ds.Tables[0].Rows[j]["QID"]);
                            drow["QT"] = Convert.ToInt32(ds.Tables[0].Rows[j]["QT"]);
                            drow["Question"] = Convert.ToString(ds.Tables[0].Rows[j]["Question"]);
                            drow["A"] =  Convert.ToString(ds.Tables[0].Rows[j]["A"]);
                            drow["B"] = Convert.ToString(ds.Tables[0].Rows[j]["B"]);
                            drow["C"] = Convert.ToString(ds.Tables[0].Rows[j]["C"]);
                            drow["D"] = Convert.ToString(ds.Tables[0].Rows[j]["D"]);

                            dt.Rows.Add(drow);
                            i++;

                        }

                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        dtlistShowQP.DataSource = dt;
                        dtlistShowQP.DataBind();
                        dtlistShowQP.Visible = true;
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;


                    }
                    else
                    {

                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! No Question Found.";
                        pnlMSG.Visible = true;
                    }
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    if (lblMOE.Text == "R")
                    {
                        cmd.CommandText = "select * from QuestionBank where PaperCode='" + lblPaperCode.Text + "' order by QID";
                    }
                    else if (lblMOE.Text == "B")
                    {
                        cmd.CommandText = "select * from QuestionBank where PaperCode='" + lblPaperCode.Text + "' order by newid()";
                    }

                    DataTable dt = new DataTable();
                    DataColumn dtcol1 = new DataColumn("SNo");
                    DataColumn dtcol2 = new DataColumn("QID");




                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.SelectCommand.Connection = con;
                    da.Fill(ds);
                    int i = 1;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 60; j < 120; j++)
                        {

                            DataRow drow = dt.NewRow();
                            drow["SNo"] = i;
                            drow["QID"] = Convert.ToInt32(ds.Tables[0].Rows[j]["QID"]);


                            dt.Rows.Add(drow);
                            i++;

                        }

                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        dtlistShowQPI.DataSource = dt;
                        dtlistShowQPI.DataBind();
                        dtlistShowQPI.Visible = true;

                        populateQImages();
                        pnlData.Visible = true;
                        pnlMSG.Visible = false;

                    }
                    else
                    {

                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! No Question Found.";
                        pnlMSG.Visible = true;
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Total Questions available are less than 60. Please upload more questions";
                pnlMSG.Visible = true;
                btnShow.Visible = false;
            }


        }

        private void populateQImages()
        {
            foreach (DataListItem dli in dtlistShowQPI.Items)
            {
                Label qid = (Label)dli.FindControl("lblQID");
                Image img = (Image)dli.FindControl("imgQues");

                img.ImageUrl = "Question.ashx?QID=" + Convert.ToInt32(qid.Text);
            }
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            if (!(Exam.isQuestionPaperAlreadySet(lblExamCode.Text, lblPaperCode.Text,lblMOE.Text)))
            {
                int counter = 0;
                if (lblPaperCode.Text != "DCS-111" && lblPaperCode.Text != "DSC-301" && lblPaperCode.Text != "DSC-302" && lblPaperCode.Text != "DCS-217" && lblPaperCode.Text != "DSC-201" && lblPaperCode.Text != "DSC-202" && lblPaperCode.Text != "DSC-101" && lblPaperCode.Text != "DSC-102")
                {
                    foreach (DataListItem dli in dtlistShowQP.Items)
                    {
                        Label sno = (Label)dli.FindControl("lblSNo");
                        Label qid = (Label)dli.FindControl("lblQID");


                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into QuestionPapers values(@ExamCode,@MOE,@PaperCode,@QSNo,@QID,@SetBy,@SetOn)", con);

                        cmd.Parameters.AddWithValue("@ExamCode", lblExamCode.Text);
                        cmd.Parameters.AddWithValue("@MOE", lblMOE.Text);
                        cmd.Parameters.AddWithValue("@PaperCode", lblPaperCode.Text);
                        cmd.Parameters.AddWithValue("@QSNo", Convert.ToInt32(sno.Text));
                        cmd.Parameters.AddWithValue("@QID", Convert.ToInt32(qid.Text));
                        cmd.Parameters.AddWithValue("@SetBy", Convert.ToInt32(Session["ERID"]));
                        cmd.Parameters.AddWithValue("@SetOn", DateTime.Now.ToString());

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();

                        counter = counter + i;
                    }
                    if (counter == 60)
                    {
                        int k = Exam.updateQPSet(lblExamCode.Text, lblPaperCode.Text, lblMOE.Text);
                        if (k > 0)
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Question Paper is set successfully.";
                            pnlMSG.Visible = true;
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Question Paper is set successfully, But QP Set Record is not updated";
                            pnlMSG.Visible = true;
                        }

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Only '" + counter.ToString() + "' questions are set only. Please contact ERP Developer.";
                        pnlMSG.Visible = true;
                    }
                }
                else
                {
                    foreach (DataListItem dli in dtlistShowQPI.Items)
                    {
                        Label sno = (Label)dli.FindControl("lblSNo");
                        Label qid = (Label)dli.FindControl("lblQID");


                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into QuestionPapers values(@ExamCode,@MOE,@PaperCode,@QSNo,@QID,@SetBy,@SetOn)", con);

                        cmd.Parameters.AddWithValue("@ExamCode", lblExamCode.Text);
                        cmd.Parameters.AddWithValue("@MOE", lblMOE.Text);
                        cmd.Parameters.AddWithValue("@PaperCode", lblPaperCode.Text);
                        cmd.Parameters.AddWithValue("@QSNo", Convert.ToInt32(sno.Text));
                        cmd.Parameters.AddWithValue("@QID", Convert.ToInt32(qid.Text));
                        cmd.Parameters.AddWithValue("@SetBy", Convert.ToInt32(Session["ERID"]));
                        cmd.Parameters.AddWithValue("@SetOn", DateTime.Now.ToString());

                        con.Open();
                        int i = cmd.ExecuteNonQuery();
                        con.Close();

                        counter = counter + i;
                    }
                    if (counter == 60)
                    {
                        int k = Exam.updateQPSet(lblExamCode.Text, lblPaperCode.Text, lblMOE.Text);
                        if (k > 0)
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Question Paper is set successfully.";
                            pnlMSG.Visible = true;
                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Question Paper is set successfully, But QP Set Record is not updated";
                            pnlMSG.Visible = true;
                        }

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! Only '" + counter.ToString() + "' questions are set only. Please contact ERP Developer.";
                        pnlMSG.Visible = true;
                    }
                }
            }
            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Question Paper for this paper code is already set.";
                pnlMSG.Visible = true;
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShowQuestionPaper.aspx");
        }
    }
}