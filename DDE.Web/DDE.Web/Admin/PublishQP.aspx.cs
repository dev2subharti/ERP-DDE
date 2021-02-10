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
    public partial class PublishQP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 106))
            {

                if (!IsPostBack)
                {
                   
                    lblPaperCode.Text = Session["PaperCode"].ToString();
                    lblCourseCode.Text = FindInfo.findSubjectCodesByPaperCode(Session["PaperCode"].ToString(),Session["SySession"].ToString());
                    lblExamination.Text = Session["ExamName"].ToString();
                    lblYear.Text = FindInfo.findAlphaYear(Session["Year"].ToString());
                    lblSubjectName.Text = FindInfo.findSubjectNameByPaperCode(Session["PaperCode"].ToString(), Session["SySession"].ToString());
                    populateSNo();
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

        private void populateSNo()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEQuestionPapers where ExamCode='" + Session["ExamCode"].ToString() + "' and PaperCode='" + Session["PaperCode"].ToString() + "' order by QSNo", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("QID");
            
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = Convert.ToInt32(ds.Tables[0].Rows[i]["QSNo"]);
                    drow["QID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["QID"]);
                    dt.Rows.Add(drow);
                }
            }

            dtlistShowQP.DataSource = dt;
            dtlistShowQP.DataBind();
        }

        private void populateQuestions()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEQuestionPapers where ExamCode='" + Session["ExamCode"].ToString() + "' and PaperCode='" + Session["PaperCode"].ToString() + "' order by QSNo", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataListItem dli in dtlistShowQP.Items)
            {
                Label sno = (Label)dli.FindControl("lblSNo");
                Label qid = (Label)dli.FindControl("lblQID");
                Image qimage = (Image)dli.FindControl("imgQuestion");

                qimage.ImageUrl = "QPImgHandler.ashx?QID=" + qid.Text;

            }

        }
          
  
    }
}