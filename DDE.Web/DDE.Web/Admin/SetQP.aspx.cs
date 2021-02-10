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
    public partial class SetQP : System.Web.UI.Page
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

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("QN");
            DataColumn dtcol3 = new DataColumn("Question");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);


            Random rd = new Random();

            for (int i = 1; i <= 8; i++)
            {
            startagain:
                int qpn = rd.Next(Convert.ToInt32(Session["QPFrom"]), Convert.ToInt32(Session["QPTo"]));
                //int qpn = i+8;
                if (!(dt.AsEnumerable().Where(c => c.Field<string>("QN").Equals(qpn.ToString())).Count() > 0))
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["QN"] = qpn.ToString();
                    drow["Question"] = "QuestionBank1/" + Session["PaperCode"].ToString() + "/" + qpn.ToString() + ".jpg";
                    dt.Rows.Add(drow);
                }
                else
                {
                    goto startagain;
                }
            }



            dtlistShowQP.DataSource = dt;
            dtlistShowQP.DataBind();


        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            if (!(Exam.isQuestionPaperAlreadySet(lblExamCode.Text, lblPaperCode.Text)))
            {
                int counter = 0;
                foreach (DataListItem dli in dtlistShowQP.Items)
                {
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label qn = (Label)dli.FindControl("lblQN");
                    Image qimage = (Image)dli.FindControl("imgQuestion");

                    FileStream fs = File.Open(Server.MapPath(qimage.ImageUrl), FileMode.Open);               
                    System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                 

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEQuestionPapers values(@ExamCode,@PaperCode,@QSNo,@QImageNo,@QImage,@SetBy,@SetOn,@Status)", con);

                    cmd.Parameters.AddWithValue("@ExamCode", lblExamCode.Text);
                    cmd.Parameters.AddWithValue("@PaperCode", lblPaperCode.Text);
                    cmd.Parameters.AddWithValue("@QSNo", Convert.ToInt32(sno.Text));
                    cmd.Parameters.AddWithValue("@QImageNo", Convert.ToInt32(qn.Text));
                    cmd.Parameters.AddWithValue("@QImage", bytes);
                    cmd.Parameters.AddWithValue("@SetBy", Convert.ToInt32(Session["ERID"]));
                    cmd.Parameters.AddWithValue("@SetOn", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@Status", "False");
                    con.Open();
                    int i= cmd.ExecuteNonQuery();
                    con.Close();

                    counter = counter + i;
                }

                if (counter == 8)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Question Paper is set successfully.";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! Only '"+counter.ToString()+"' are set only. Please contact ERP Developer.";
                    pnlMSG.Visible = true;
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
            Response.Redirect("PublishQP.aspx");
        }
    }
}