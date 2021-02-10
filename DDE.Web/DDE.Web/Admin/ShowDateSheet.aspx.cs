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
    public partial class ShowDateSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 72))
            {
                if (!IsPostBack)
                {               
                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("Z11").Selected = true;
                    //ddlistExamination.Enabled = false;

                    if (Request.QueryString["Year"] != null)
                    {
                        ddlistYear.Items.FindByValue(Request.QueryString["Year"]).Selected = true;
                        populateDateSheet();
                        populateSubjects();
                    }                

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
            populateDateSheet();       
           
        }

        protected void Edit_DateSheet(object sender, EventArgs e)
        {
            LinkButton edit = sender as LinkButton;
            Response.Redirect("CreateDateSheet.aspx?DSID=" + edit.CommandArgument);
        }

        protected void Delete_DateSheetRow(object sender, EventArgs e)
        {
            LinkButton del = sender as LinkButton;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("delete from DDEExaminationSchedules where DSID ='" + Convert.ToString(del.CommandArgument) + "'", con);

            Log.createLogNow("Delete", "Deleted a PaperCode '" + del.CommandName + "' from Date Sheet of " + ddlistExamination.SelectedItem.Text + " Exam", Convert.ToInt32(Session["ERID"].ToString()));
            con.Open();
            cmd.ExecuteReader();
            con.Close();

            populateDateSheet();
            if (rblMode.SelectedItem.Value == "T")
            {
                populateSubjects();
            }
            else if (rblMode.SelectedItem.Value == "P")
            {
                populatePracticals();
            }
        }

        private void populateSubjects()
        {
            foreach (DataListItem dli in dtlistShowTheoryDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                Label date = (Label)dli.FindControl("lblDate");

                string fd = Convert.ToInt32(date.Text.Substring(3, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(0, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(6, 4)).ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where Date='" + Convert.ToDateTime(fd).ToString("yyyy-MM-dd") + "' and Year='" + ddlistYear.SelectedItem.Value + "' and ExaminationCode='" + ddlistExamination.SelectedItem.Value + "' and DSType='" + rblMode.SelectedItem.Value + "' and MOE='"+ddlistMOE.SelectedItem.Value+"' order by Date", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("DSID");
                DataColumn dtcol2 = new DataColumn("Time");
                //DataColumn dtcol3 = new DataColumn("Course");
                DataColumn dtcol4 = new DataColumn("SubjectCode");
                DataColumn dtcol5 = new DataColumn("PaperCode");             
                DataColumn dtcol6 = new DataColumn("SubjectName");
             

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                //dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["DSID"] =ds.Tables[0].Rows[i]["DSID"].ToString();
                    drow["Time"] = ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(6, 1)) + " - " + ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(6, 1));
                    //drow["Course"] = FindInfo.findCoursesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    drow["SubjectCode"] = FindInfo.findSubjectCodesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    drow["PaperCode"] = ds.Tables[0].Rows[i]["PaperCode"].ToString();
                    drow["SubjectName"] =FindInfo.findSubjectNameByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString(),ds.Tables[0].Rows[i]["SyllabusSession"].ToString());
                    if (!subalreadyexits(drow["PaperCode"].ToString(), drow["SubjectName"].ToString(),dt))
                    {
                        dt.Rows.Add(drow);
                    }
                }

                subjects.DataSource = dt;
                subjects.DataBind();

               

            }
        }

        private void populatePracticals()
        {
            foreach (DataListItem dli in dtlistShowPracDS.Items)
            {
                DataList practicals = (DataList)dli.FindControl("dtlistShowPracticals");
                Label date = (Label)dli.FindControl("lblDate");

                string fd = Convert.ToInt32(date.Text.Substring(3, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(0, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(6, 4)).ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where Date='" + Convert.ToDateTime(fd).ToString("yyyy-MM-dd") + "' and Year='" + ddlistYear.SelectedItem.Value + "' and ExaminationCode='" + ddlistExamination.SelectedItem.Value + "' and DSType='" + rblMode.SelectedItem.Value + "' and MOE='"+ddlistMOE.SelectedItem.Value+"' order by Date", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("DSID");
                DataColumn dtcol2 = new DataColumn("Time");
                //DataColumn dtcol3 = new DataColumn("Course");
                DataColumn dtcol4 = new DataColumn("PracticalCode");
                DataColumn dtcol5 = new DataColumn("PracticalName");


                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                //dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
            

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["DSID"] = ds.Tables[0].Rows[i]["DSID"].ToString();
                    drow["Time"] = ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(6, 1)) + " - " + ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(6, 1));
                    //drow["Course"] = FindInfo.findCoursesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    drow["PracticalCode"] = ds.Tables[0].Rows[i]["PaperCode"].ToString();
                    drow["PracticalName"] = FindInfo.findPracticalNameByPracticalCode(ds.Tables[0].Rows[i]["PaperCode"].ToString(), ds.Tables[0].Rows[i]["SyllabusSession"].ToString());
                    if (!pracalreadyexits(drow["PracticalCode"].ToString(), drow["PracticalName"].ToString(), dt))
                    {
                        dt.Rows.Add(drow);
                    }
                }

                practicals.DataSource = dt;
                practicals.DataBind();



            }
        }

        private bool subalreadyexits(string pc, string sn, DataTable dt)
        {
            bool exist = false;

            foreach(DataRow row in dt.Rows)
            {
                if (row["PaperCode"].ToString() == pc && row["SubjectName"].ToString() == sn)
                {
                    exist = true;
                    break;
                }
            }

            return exist;
        }

        private bool pracalreadyexits(string pc, string sn, DataTable dt)
        {
            bool exist = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["PracticalCode"].ToString() == pc && row["PracticalName"].ToString() == sn)
                {
                    exist = true;
                    break;
                }
            }

            return exist;
        }

        private string findSection(string sec)
        {
            if (sec == "1")
            {
                return "AM";
            }
            else if (sec == "2")
            {
                return "PM";
            }
            else if (sec == "3")
            {
                return "NOON";
            }
            else
            {
                return "NF";
            }
        }

        private void populateDateSheet()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct Date from DDEExaminationSchedules where Year='" + ddlistYear.SelectedItem.Value + "' and ExaminationCode='" + ddlistExamination.SelectedItem.Value + "' and DSType='"+rblMode.SelectedItem.Value+"' and MOE='"+ddlistMOE.SelectedItem.Value+"' order by Date", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("Date");
            DataColumn dtcol2 = new DataColumn("Day");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]).ToString("dd-MM-yyyy");
                drow["Day"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]).DayOfWeek;
                dt.Rows.Add(drow);
            }

            if (rblMode.SelectedItem.Value == "T")
            {
                dtlistShowTheoryDS.DataSource = dt;
                dtlistShowTheoryDS.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtlistShowTheoryDS.Visible = true;
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;
                    btnPublish.Visible = true;
                    populateSubjects();
                }
                else
                {
                    dtlistShowTheoryDS.Visible = false;
                    btnPublish.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found.";
                    pnlMSG.Visible = true;
                }

                dtlistShowPracDS.Visible = false;
            }
            else if (rblMode.SelectedItem.Value == "P")
            {
                dtlistShowPracDS.DataSource = dt;
                dtlistShowPracDS.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtlistShowPracDS.Visible = true;
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;
                    btnPublish.Visible = true;
                    populatePracticals();
                }
                else
                {
                    dtlistShowPracDS.Visible = false;
                    btnPublish.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found.";
                    pnlMSG.Visible = true;
                }

                dtlistShowTheoryDS.Visible = false;
            }

           
        }

        protected void dtlistShowTheoryDS_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Session["DSType"] = rblMode.SelectedItem.Value;
            Session["DSExamName"] = ddlistExamination.SelectedItem.Text;
            Session["DSYearName"] = ddlistYear.SelectedItem.Text;
            Session["DSExam"] = ddlistExamination.SelectedItem.Value;
            Session["DSYear"] = ddlistYear.SelectedItem.Value;
            Session["DSMOE"] = ddlistMOE.SelectedItem.Value;
            Response.Redirect("PublishDateSheet.aspx");
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowTheoryDS.Visible = false;
            dtlistShowPracDS.Visible = false;
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowTheoryDS.Visible = false;
            dtlistShowPracDS.Visible = false;
        }

        protected void dtlistShowPracDS_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }
    }
}