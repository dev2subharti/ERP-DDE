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
    public partial class ShowMyReceivedAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 69))
            {

                if (!IsPostBack)
                {
                    setCurrentDate();

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

        private void setCurrentDate()
        {
            ddlistDayFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDayTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }


        protected void btnPublish_Click(object sender, EventArgs e)
        {
            
            if (ddlistFilter.SelectedItem.Text == "NOT PUBLISHED")
            {
                populatePendings();

            }
            else if (ddlistFilter.SelectedItem.Text == "ALL")
            {
                string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01";
                string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 23:59:59";

                Session["RecFrom"] = from;
                Session["RecTo"] = to;

                Session["ExamCode"] = ddlistExamination.SelectedItem.Value;
                Session["ExamName"] = ddlistExamination.SelectedItem.Text;
                Session["ReceivedBy"] = Convert.ToInt32(Session["ERID"]);
                Session["PaperCode"] = tbPC.Text.ToUpper();
                Session["Filter"] = ddlistFilter.SelectedItem.Text;
                Response.Redirect("ShowASReceivedByEmployee.aspx");
            }
          
        }

        private void populatePendings()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;


            cmd.CommandText = "Select distinct DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + ".SubjectID,DDESubject.PaperCode from DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + " inner join DDESubject on DDESubject.SubjectID=DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + ".SubjectID where DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + ".ASPRID='0' and DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + ".ReceivedBy='"+Convert.ToInt32(Session["ERID"]) +"'";



            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SubjectID");
            DataColumn dtcol3 = new DataColumn("PaperCode");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);

            cmd.Connection = con;

            con.Open();

            dr = cmd.ExecuteReader();

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SubjectID"] = Convert.ToInt32(dr["SubjectID"]);
                drow["PaperCode"] = dr["PaperCode"].ToString();
                dt.Rows.Add(drow);
                i = i + 1;
            }

            con.Close();

            DataView view = new DataView(dt);
            DataTable pcodes = view.ToTable(true, "PaperCode");

            populateASReport(pcodes);
        }

        private void populateASReport(DataTable pcodes)
        {

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");         
            DataColumn dtcol3 = new DataColumn("PaperCode");
            DataColumn dtcol4 = new DataColumn("SubjectName");
            DataColumn dtcol5 = new DataColumn("TotalStudents");
            DataColumn dtcol6 = new DataColumn("Present");
            DataColumn dtcol7 = new DataColumn("Absent");
           


            dt.Columns.Add(dtcol1);
           
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
          

            for (int i = 0; i < pcodes.Rows.Count; i++)
            {

                DataRow drow = dt.NewRow();
               
                drow["PaperCode"] = pcodes.Rows[i]["PaperCode"];

                if (ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17"|| ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10")
                {
                    drow["SubjectName"] = FindInfo.findAllSubjectNameByPaperCode(drow["PaperCode"].ToString());

                    int pr;
                    int ab;
                    drow["TotalStudents"] = FindInfo.findTotalStudentsInASByPCandERID(drow["PaperCode"].ToString(),Convert.ToInt32(Session["ERID"]),ddlistExamination.SelectedItem.Value, out pr, out ab);
                    drow["Present"] = pr.ToString();
                    drow["Absent"] = ab.ToString();
                 
                    dt.Rows.Add(drow);
                }


            }

            dt.DefaultView.Sort = "PaperCode";
            DataView dv = dt.DefaultView;

            int k = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = k;
                k++;
            }

            dtlistASReport.DataSource = dt;
            dtlistASReport.DataBind();

            if (k > 1)
            {

                dtlistASReport.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                dtlistASReport.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

        }

        protected void ddlistFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlistFilter.SelectedItem.Text == "NOT PUBLISHED")
            {
                pnlCalender.Visible = false;
                lblPC.Visible = false;
                tbPC.Visible = false;
                
            }
            else if (ddlistFilter.SelectedItem.Text == "ALL")
            {
                pnlCalender.Visible = true;
                lblPC.Visible = true;
                tbPC.Visible = true;
                dtlistASReport.Visible = false;
            }
        }

        

        protected void dtlistASReport_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Session["ExamCode"] = ddlistExamination.SelectedItem.Value;
            Session["ExamName"] = ddlistExamination.SelectedItem.Text;
            Session["ReceivedBy"] = Convert.ToInt32(Session["ERID"]);
            Session["SubjectCode"] = e.CommandArgument;
            Session["SubjectName"] = e.CommandName;
            Session["SubjectID"] = FindInfo.findSubjectIDsByPaperCode(e.CommandArgument.ToString());
            Session["Filter"] = "NOT PUBLISHED";
            Session["CF"] = "IAS";
            Session["ASPrinted"] = "NO";
            Response.Redirect("AwardSheet.aspx");
        }

      
               
           
       
    }
}