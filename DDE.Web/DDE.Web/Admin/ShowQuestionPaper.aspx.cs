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
    public partial class ShowQuestionPaper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 106))
            {

              if (!IsPostBack)
              {
                lblPaperCode.Text = Session["PaperCode"].ToString();
                lblCourseCode.Text = FindInfo.findSubjectCodesByPaperCode(Session["PaperCode"].ToString(), Session["SySession"].ToString());
                lblExamCode.Text = Session["ExamCode"].ToString();
                lblMOE.Text = Session["MOE"].ToString();
                lblMOEA.Text = Session["MOEA"].ToString();
                lblExamination.Text = Session["ExamName"].ToString();
                lblYear.Text = FindInfo.findAlphaYear(Session["Year"].ToString());
                lblSubjectName.Text = FindInfo.findSubjectNameByPaperCode(Session["PaperCode"].ToString(), Session["SySession"].ToString());

                populateQuestions();

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
        private void populateQuestions()
        {
            if (lblPaperCode.Text != "DSC-301" && lblPaperCode.Text != "DSC-302" && lblPaperCode.Text != "DCS-118" && lblPaperCode.Text != "DSC-201" && lblPaperCode.Text != "DSC-202" && lblPaperCode.Text != "DSC-101" && lblPaperCode.Text != "DSC-102")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                SqlCommand cmd = new SqlCommand("select QuestionPapers.QSNo,QuestionPapers.QID,QuestionBank.QT,QuestionBank.Question,QuestionBank.A,QuestionBank.B,QuestionBank.C,QuestionBank.D from QuestionPapers inner join QuestionBank on QuestionPapers.QID=QuestionBank.QID where QuestionPapers.ExamCode='" + lblExamCode.Text + "' and QuestionPapers.PaperCode='" + lblPaperCode.Text + "' and QuestionPapers.MOE='" + lblMOE.Text + "' order by QuestionPapers.QSNo", con);
                //SqlCommand cmd = new SqlCommand("select top 60 * from QuestionBank where PaperCode='" + Session["PaperCode"].ToString() + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.SelectCommand.Connection = con;
                da.Fill(ds);

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

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {

                        DataRow drow = dt.NewRow();
                        //drow["SNo"] = Convert.ToInt32(ds.Tables[0].Rows[j]["QSNo"]);
                        drow["SNo"] = (j + 1).ToString();
                        drow["QID"] = Convert.ToInt32(ds.Tables[0].Rows[j]["QID"]);
                        drow["QT"] = Convert.ToInt32(ds.Tables[0].Rows[j]["QT"]);
                        drow["Question"] = Convert.ToString(ds.Tables[0].Rows[j]["Question"]);
                        drow["A"] = Convert.ToString(ds.Tables[0].Rows[j]["A"]);
                        drow["B"] = Convert.ToString(ds.Tables[0].Rows[j]["B"]);
                        drow["C"] = Convert.ToString(ds.Tables[0].Rows[j]["C"]);
                        drow["D"] = Convert.ToString(ds.Tables[0].Rows[j]["D"]);

                        dt.Rows.Add(drow);


                    }

                }


                if (ds.Tables[0].Rows.Count > 0)
                {

                    dtlistShowQP.DataSource = dt;
                    dtlistShowQP.DataBind();
                    dtlistShowQP.Visible = true;



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
                SqlCommand cmd = new SqlCommand("select QSNo,QID from QuestionPapers where ExamCode='" + lblExamCode.Text + "' and PaperCode='" + lblPaperCode.Text + "' and MOE='" + lblMOE.Text + "' order by QSNo", con);

                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("QID");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.SelectCommand.Connection = con;
                da.Fill(ds);
                int i = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {

                        DataRow drow = dt.NewRow();
                        drow["SNo"] = j + 1;
                        drow["QID"] = Convert.ToInt32(ds.Tables[0].Rows[j]["QID"]);


                        dt.Rows.Add(drow);


                    }

                }

                if (ds.Tables[0].Rows.Count > 0)
                {

                    dtlistShowQPI.DataSource = dt;
                    dtlistShowQPI.DataBind();
                    dtlistShowQPI.Visible = true;

                    populateQImages();

                }
                else
                {

                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! No Question Found.";
                    pnlMSG.Visible = true;
                }
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
    }
}