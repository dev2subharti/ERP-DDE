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
    public partial class GenerateQuestionPaper1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 106))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("Z11").Selected = true;
                    ddlistExam.Enabled = false;
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
            ddlistDay.SelectedItem.Selected = false;
            ddlistMonth.SelectedItem.Selected = false;
            ddlistYear.SelectedItem.Selected = false;
            ddlistDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateDateSheet();
            populateSubjects();
          

        }
        
        private void populateDateSheet()
        {
            string date = ddlistYear.SelectedItem.Text + "-" + ddlistMonth.SelectedItem.Value + "-" + ddlistDay.SelectedItem.Text;
            string timefrom = "";
            string timeto = "";

            if (rblShift.SelectedItem.Value == "1")
            {
                timefrom = "10:00 1";
                timeto = "11:00 1";
            }
            else if (rblShift.SelectedItem.Value == "2")
            {
                timefrom = "12:00 3";
                timeto = "01:00 2";
            }
            else if (rblShift.SelectedItem.Value == "3")
            {
                timefrom = "02:00 2";
                timeto = "03:00 2";
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where ExaminationCode='" + ddlistExam.SelectedItem.Value + "' and Date='" + date + "' and TimeFrom='" + timefrom + "' and TimeTo='" + timeto + "' and MOE='"+ddlistMOE.SelectedItem.Value+"'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("Date");


            dt.Columns.Add(dtcol1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drow = dt.NewRow();
                drow["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"]).ToString("dd-MM-yyyy");
                dt.Rows.Add(drow);
            }

            dtlistShowDS.DataSource = dt;
            dtlistShowDS.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowDS.Visible = true;

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowDS.Visible = false;

                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }


        }

        private void populateSubjects()
        {
            string timefrom = "";
            string timeto = "";

            if (rblShift.SelectedItem.Value == "1")
            {
                timefrom = "10:00 1";
                timeto = "11:00 1";
            }
            else if (rblShift.SelectedItem.Value == "2")
            {
                timefrom = "12:00 3";
                timeto = "01:00 2";
            }
            else if (rblShift.SelectedItem.Value == "3")
            {
                timefrom = "02:00 2";
                timeto = "03:00 2";
            }

            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                Label date = (Label)dli.FindControl("lblDate");

                string fd = Convert.ToInt32(date.Text.Substring(3, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(0, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(6, 4)).ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where ExaminationCode='" + ddlistExam.SelectedItem.Value + "' and Date='" + Convert.ToDateTime(fd).ToString("yyyy-MM-dd") + "' and TimeFrom='" + timefrom + "' and TimeTo='" + timeto + "' and MOE='"+ddlistMOE.SelectedItem.Value+"' order by Year", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("DSID");
                DataColumn dtcol2 = new DataColumn("Time");
                DataColumn dtcol4 = new DataColumn("SubjectCode");
                DataColumn dtcol5 = new DataColumn("PaperCode");
                DataColumn dtcol6 = new DataColumn("SySession");
                DataColumn dtcol7 = new DataColumn("SubjectName");
                DataColumn dtcol8 = new DataColumn("Year");
                DataColumn dtcol9 = new DataColumn("SetQP");
                DataColumn dtcol10 = new DataColumn("Status");
                DataColumn dtcol11 = new DataColumn("TQ");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);
                dt.Columns.Add(dtcol8);
                dt.Columns.Add(dtcol9);
                dt.Columns.Add(dtcol10);
                dt.Columns.Add(dtcol11);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["DSID"] = ds.Tables[0].Rows[i]["DSID"].ToString();
                    drow["Time"] = ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(6, 1)) + " - " + ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(6, 1));
                    drow["SubjectCode"] = FindInfo.findSubjectCodesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString(), ds.Tables[0].Rows[i]["SyllabusSession"].ToString());
                    drow["PaperCode"] = ds.Tables[0].Rows[i]["PaperCode"].ToString();
                    drow["SySession"] = ds.Tables[0].Rows[i]["SyllabusSession"].ToString();
                    drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString(), ds.Tables[0].Rows[i]["SyllabusSession"].ToString());
                    drow["Year"] = ds.Tables[0].Rows[i]["Year"].ToString();

                    if (ds.Tables[0].Rows[i]["SetQP"].ToString() == "True")
                    {
                        drow["SetQP"] = "YES";
                    }
                    else
                    {
                        drow["SetQP"] = "NO";
                    }
                    if (ds.Tables[0].Rows[i]["Online"].ToString()=="True")
                    {
                        drow["Status"] = "ONLINE";
                    }
                    else
                    {
                        drow["Status"] = "OFFLINE";
                    }
                    drow["TQ"] = FindInfo.findTotalQuestionByPC(drow["PaperCode"].ToString().Trim());
                    if (!alreadyexits(drow["PaperCode"].ToString(), drow["SubjectName"].ToString(), dt))
                    {
                        dt.Rows.Add(drow);
                    }



                }

                subjects.DataSource = dt;
                subjects.DataBind();


                foreach (DataListItem dliss in subjects.Items)
                {
                    Label lsetqp = (Label)dliss.FindControl("lblSetQP");
                    Label tq = (Label)dliss.FindControl("lblTQ");
                    LinkButton bsetqp = (LinkButton)dliss.FindControl("lnkbtnSet");
                    LinkButton bshow = (LinkButton)dliss.FindControl("lnkbtnShow");
                    Button OLStatus = (Button)dliss.FindControl("btnStatus");

                    if(Convert.ToInt32(tq.Text)<120)
                    {
                        bsetqp.BackColor = System.Drawing.Color.Orange;
                        bsetqp.ForeColor = System.Drawing.Color.White;
                    }
                    else if(Convert.ToInt32(tq.Text) >= 120)
                    {
                        bsetqp.BackColor = System.Drawing.Color.Green;
                        bsetqp.ForeColor = System.Drawing.Color.White;
                    }

                    if(lsetqp.Text=="YES")
                    {
                        bsetqp.Visible = false;
                        if (OLStatus.Text == "ONLINE")
                        {
                            OLStatus.CommandName = "MAKE OFFLINE";
                            OLStatus.BackColor = System.Drawing.Color.Green;
                            OLStatus.ForeColor = System.Drawing.Color.White;
                        }

                        else if (OLStatus.Text == "OFFLINE")
                        {
                            OLStatus.CommandName = "MAKE ONLINE";
                            OLStatus.BackColor = System.Drawing.Color.Orange;
                            OLStatus.ForeColor = System.Drawing.Color.White;
                        }
                        bshow.Visible = true;
                        OLStatus.Visible = true;
                    }
                    else if(lsetqp.Text=="NO")
                    {
                        bsetqp.Visible = true;
                        bshow.Visible = false;
                        OLStatus.Visible =false;
                    }
                   

                }
            }
        }

        private string findwithbreak(string validpc)
        {
            string[] str = validpc.Split(',');

            string withbr = "";

            for (int i = 0; i < str.Length; i++)
            {
                if (withbr == "")
                {
                    withbr = str[i];
                }
                else
                {
                    withbr = withbr + "<br/>" + str[i];
                }
            }

            return withbr;
        }

        private string findSelectedPapers()
        {
            string selected = "";
            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                foreach (DataListItem dl in subjects.Items)
                {
                    Label pc = (Label)dl.FindControl("lblPaperCode");



                    if (selected == "")
                    {
                        selected = pc.Text;
                    }
                    else
                    {
                        selected = selected + "," + pc.Text;
                    }

                }

            }
            return selected;
        }

        private bool alreadyexits(string pc, string sn, DataTable dt)
        {
            bool exist = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["PaperCode"].ToString() == pc && row["SubjectName"].ToString() == sn)
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

        protected void lnkbtnSet_Click(object sender, EventArgs e)
        {
            LinkButton show = (LinkButton)sender;


            Session["PaperCode"] = show.ToolTip;
            Session["SySession"] = show.CommandArgument;
            Session["Year"] = show.CommandName;
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["ExamName"] = ddlistExam.SelectedItem.Text;
            Session["MOE"] = ddlistMOE.SelectedItem.Value;
            Session["MOEA"] = ddlistMOE.SelectedItem.Text;


            Response.Redirect("SetQP1.aspx");

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            LinkButton show = (LinkButton)sender;

            Session["PaperCode"] = show.ToolTip;
            Session["SySession"] = show.CommandArgument;
            Session["Year"] = show.CommandName;
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["ExamName"] = ddlistExam.SelectedItem.Text;
            Session["MOE"] = ddlistMOE.SelectedItem.Value;
            Session["MOEA"] = ddlistMOE.SelectedItem.Text;
            Response.Redirect("ShowQuestionPaper.aspx");

        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            Button status = (Button)sender;
            if (status.CommandName == "MAKE ONLINE")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEExaminationSchedules set Online=@Online where ExaminationCode='" + ddlistExam.SelectedItem.Value + "' and PaperCode='" + status.CommandArgument + "' ", con);
                cmd.Parameters.AddWithValue("@Online", "True");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Made Question Paper 'ONLINE' of Paper Code '" + status.CommandArgument + "' for Exam '" + ddlistExam.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateSubjects();
             


            }
            else if (status.CommandName == "MAKE OFFLINE")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEExaminationSchedules set Online=@Online where ExaminationCode='" + ddlistExam.SelectedItem.Value + "' and PaperCode='" + status.CommandArgument + "' ", con);
                cmd.Parameters.AddWithValue("@Online", "False");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Made Question Paper 'OFFLINE' of Paper Code '" + status.CommandArgument + "' for Exam '" + ddlistExam.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateSubjects();
               

            }

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            btnOK.Visible = false;
            dtlistShowDS.Visible = true;

        }
    }
}