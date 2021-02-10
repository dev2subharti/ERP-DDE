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
    public partial class CreateDateSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 71))
            {
                if (!IsPostBack)
                {
                    
                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("Z11").Selected = true;
                    ddlistExamination.Enabled = false;

                    if (Request.QueryString["DSID"] != null)
                    {
                        string pc;
                        string date;
                        int year;
                        string tf;
                        string tt;
                        string dstype;
                        string sysession;
                        FindInfo.findAllByDSID(Convert.ToInt32(Request.QueryString["DSID"]), out pc, out year, out date, out tf, out tt,out sysession, out dstype);
                        
                        rblMode.SelectedItem.Selected = false;
                        rblMode.Items.FindByValue(dstype).Selected = true;

                        ddlistSySession.SelectedItem.Selected = false;
                        ddlistSySession.Items.FindByText(sysession).Selected = true;

                        ddlistYear.Items.FindByValue(year.ToString()).Selected = true;
                                            
                        populateAllPaperCodes(sysession, rblMode.SelectedItem.Value);
                        ddlistPaperCode.Items.FindByText(pc).Selected = true;
                        lblPC.Text = pc;

                        if (rblMode.SelectedItem.Value == "T")
                        {
                            populateSubjects();
                        }
                        else if (rblMode.SelectedItem.Value == "P")
                        {
                            populatePracticals();
                        }

                        SetDateTime(date, tf, tt);
                        pnlDateTime.Visible = true;
                        btnAdd.Text = "Update";
                        btnFind.Visible = false;

                    }
                    else
                    {
                        btnFind.Visible = false;
                        btnAdd.Text = "Add";
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

        private void populateAllPaperCodes(string sysession, string dstype)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (dstype == "T")
            {
                cmd.CommandText = "Select distinct PaperCode from DDESubject where SyllabusSession='" + sysession + "' and Year='" + ddlistYear.SelectedItem.Text + "'";
            }
            else if (dstype == "P")
            {
                cmd.CommandText = "Select distinct PracticalCode as PaperCode from DDEPractical where SyllabusSession='" + sysession + "' and Year='" + ddlistYear.SelectedItem.Text + "' and PType='1'";
            }
            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {               
                ddlistPaperCode.Items.Add(ds.Tables[0].Rows[i]["PaperCode"].ToString());              
            }
        }

        private void SetDateTime(string date, string tf, string tt)
        {
            ddlistDOADay.Items.FindByText(date.Substring(0,2)).Selected = true;
            ddlistDOAMonth.Items.FindByValue(date.Substring(3, 2)).Selected = true;
            ddlistDOAYear.SelectedItem.Selected = false;
            ddlistDOAYear.Items.FindByText(date.Substring(6, 4)).Selected = true;

            ddlistHourFrom.Items.FindByText(tf.Substring(0, 2)).Selected = true;
            ddlistMinutesFrom.Items.FindByText(tf.Substring(3, 2)).Selected = true;
            ddlistSectionFrom.Items.FindByValue(tf.Substring(6, 1)).Selected = true;

            ddlistHourTo.Items.FindByText(tt.Substring(0, 2)).Selected = true;
            ddlistMinutesTo.Items.FindByText(tt.Substring(3, 2)).Selected = true;
            ddlistSectionTo.Items.FindByValue(tt.Substring(6, 1)).Selected = true;
        }      

        private void populatePaperCodes(string sysession, string dstype)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (dstype == "T")
            {
                cmd.CommandText = "Select distinct PaperCode from DDESubject where SyllabusSession='" + sysession + "' and Year='" + ddlistYear.SelectedItem.Text + "'";
            }
            else if (dstype == "P")
            {
                cmd.CommandText = "Select distinct PracticalCode as PaperCode from DDEPractical where SyllabusSession='" + sysession + "' and Year='" + ddlistYear.SelectedItem.Text + "' and PType='1'";
            }

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);         

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (!FindInfo.papercodeExistInDateSheet(ds.Tables[0].Rows[i]["PaperCode"].ToString(), ddlistExamination.SelectedItem.Value,ddlistSySession.SelectedItem.Text,ddlistMOE.SelectedItem.Value))
                {
                    if (ds.Tables[0].Rows[i]["PaperCode"].ToString() != "")
                    {
                        ddlistPaperCode.Items.Add(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    }
                }
               
            }

        }
     
        private void populateSubjects()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESubject where SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' and PaperCode='" + ddlistPaperCode.SelectedItem.Text + "' order by SubjectSNo ", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SubjectID");
            DataColumn dtcol3 = new DataColumn("PaperCode");
            DataColumn dtcol4 = new DataColumn("SubjectSNo");
            DataColumn dtcol5 = new DataColumn("SubjectCode");
            DataColumn dtcol6 = new DataColumn("SubjectName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("Year");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                drow["PaperCode"] = Convert.ToString(dr["PaperCode"]);
                drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);
                drow["Course"] = Convert.ToString(dr["CourseName"]);
                drow["Year"] = Convert.ToString(dr["Year"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowSubjects.DataSource = dt;
            dtlistShowSubjects.DataBind();

            con.Close();

            if (i > 1)
            {
                dtlistShowSubjects.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowSubjects.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }

        }

        private void populatePracticals()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEPractical where SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' and PracticalCode='" + ddlistPaperCode.SelectedItem.Text + "' order by PracticalSNo", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SubjectID");
            DataColumn dtcol3 = new DataColumn("PaperCode");
            DataColumn dtcol4 = new DataColumn("SubjectSNo");
            DataColumn dtcol5 = new DataColumn("SubjectCode");
            DataColumn dtcol6 = new DataColumn("SubjectName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("Year");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SubjectID"] = Convert.ToString(dr["PracticalID"]);
                drow["PaperCode"] = "-";
                drow["SubjectSNo"] = Convert.ToString(dr["PracticalSNo"]);
                drow["SubjectCode"] = Convert.ToString(dr["PracticalCode"]);
                drow["SubjectName"] = Convert.ToString(dr["PracticalName"]);
                drow["Course"] = Convert.ToString(dr["CourseName"]);
                drow["Year"] = Convert.ToString(dr["Year"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowSubjects.DataSource = dt;
            dtlistShowSubjects.DataBind();

            con.Close();

            if (i > 1)
            {
                dtlistShowSubjects.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowSubjects.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "T")
            {
                populateSubjects();
            }
            else if (rblMode.SelectedItem.Value == "P")
            {
                populatePracticals();
            }

            pnlDateTime.Visible = true;

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                if (!FindInfo.papercodeExistInDateSheet(ddlistPaperCode.SelectedItem.Text, ddlistExamination.SelectedItem.Value, ddlistSySession.SelectedItem.Text,ddlistMOE.SelectedItem.Value))
                {
                    string timefrom = ddlistHourFrom.SelectedItem.Text + ":" + ddlistMinutesFrom.SelectedItem.Text + " " + ddlistSectionFrom.SelectedItem.Value;
                    string timeto = ddlistHourTo.SelectedItem.Text + ":" + ddlistMinutesTo.SelectedItem.Text + " " + ddlistSectionTo.SelectedItem.Value;
                    string date = ddlistDOAYear.SelectedItem.Value + "-" + ddlistDOAMonth.SelectedItem.Value + "-" + ddlistDOADay.SelectedItem.Value;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEExaminationSchedules values(@ExaminationCode,@DSType,@SyllabusSession,@PaperCode,@Year,@Date,@TimeFrom,@TimeTo,@QPFileName,@MOE,@SetQP,@Online)", con);


                    cmd.Parameters.AddWithValue("@ExaminationCode", ddlistExamination.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@DSType", rblMode.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@SyllabusSession", ddlistSySession.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@PaperCode", ddlistPaperCode.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Year", ddlistYear.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@TimeFrom", timefrom);
                    cmd.Parameters.AddWithValue("@TimeTo", timeto);
                    cmd.Parameters.AddWithValue("@QPFileName", "");
                    cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@SetQP", "False");
                    cmd.Parameters.AddWithValue("@Online", "False");
                  


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Create Date Sheet", "Add a Paper Code '" + ddlistPaperCode.SelectedItem.Text + " with Date '" + date + "' and with time '" + timefrom + " to " + timeto + "' for '" + ddlistExamination.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "This paper code has added to Date Sheet successfully !!";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! This paper code is already exist in Date Sheet !!";
                    pnlMSG.Visible = true;
                }
            }
            else if (btnAdd.Text == "Update")
            {
                if (ddlistPaperCode.SelectedItem.Text != lblPC.Text)
                {
                    if (!FindInfo.papercodeExistInDateSheet(ddlistPaperCode.SelectedItem.Text, ddlistExamination.SelectedItem.Value, ddlistSySession.SelectedItem.Text,ddlistMOE.SelectedItem.Value))
                    {
                        string timefrom = ddlistHourFrom.SelectedItem.Text + ":" + ddlistMinutesFrom.SelectedItem.Text + " " + ddlistSectionFrom.SelectedItem.Value;
                        string timeto = ddlistHourTo.SelectedItem.Text + ":" + ddlistMinutesTo.SelectedItem.Text + " " + ddlistSectionTo.SelectedItem.Value;
                        string date = ddlistDOAYear.SelectedItem.Value + "-" + ddlistDOAMonth.SelectedItem.Value + "-" + ddlistDOADay.SelectedItem.Value;

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEExaminationSchedules set PaperCode=@PaperCode,Year=@Year,Date=@Date,TimeFrom=@TimeFrom,TimeTo=@TimeTo where DSID='" + Request.QueryString["DSID"] + "'", con);

                        cmd.Parameters.AddWithValue("@PaperCode", ddlistPaperCode.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Year", ddlistYear.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@Date", date);
                        cmd.Parameters.AddWithValue("@TimeFrom", timefrom);
                        cmd.Parameters.AddWithValue("@TimeTo", timeto);



                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Upadte Date Sheet", "Updated a Paper Code from '"+lblPC.Text+"' to '" + ddlistPaperCode.SelectedItem.Text + " with Date '" + date + "' and with time '" + timefrom + " to " + timeto + "' for '" + ddlistExamination.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        pnlData.Visible = false;
                        lblMSG.Text = "The Record has been upadted to Date Sheet successfully !!";
                        pnlMSG.Visible = true;
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! This paper code is already exist in Date Sheet !!";
                        pnlMSG.Visible = true;
                    }
                }
                else
                {

                    string timefrom = ddlistHourFrom.SelectedItem.Text + ":" + ddlistMinutesFrom.SelectedItem.Text + " " + ddlistSectionFrom.SelectedItem.Value;
                    string timeto = ddlistHourTo.SelectedItem.Text + ":" + ddlistMinutesTo.SelectedItem.Text + " " + ddlistSectionTo.SelectedItem.Value;
                    string date = ddlistDOAYear.SelectedItem.Value + "-" + ddlistDOAMonth.SelectedItem.Value + "-" + ddlistDOADay.SelectedItem.Value;

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDEExaminationSchedules set Date=@Date,TimeFrom=@TimeFrom,TimeTo=@TimeTo where DSID='" + Request.QueryString["DSID"] + "'", con);



                   
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@TimeFrom", timefrom);
                    cmd.Parameters.AddWithValue("@TimeTo", timeto);



                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Upadte Date Sheet", "Updated a Paper Code '" + ddlistPaperCode.SelectedItem.Text + " with Date '" + date + "' and with time '" + timefrom + " to " + timeto + "' for '" + ddlistExamination.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "The Record has been upadted to Date Sheet successfully !!";
                    pnlMSG.Visible = true;
                }
            }
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistPaperCode.Items.Clear();
            populatePaperCodes(ddlistSySession.SelectedItem.Text, rblMode.SelectedItem.Value);
            dtlistShowSubjects.Visible = false;
            pnlDateTime.Visible = false;
            btnFind.Visible = true;
        }

        protected void ddlistPaperCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowSubjects.Visible = false;
            pnlDateTime.Visible = false;
            btnFind.Visible = true;
           
        }

        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateDateSheet.aspx");
        }

        protected void btnShowDateSheet_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("ShowDateSheet.aspx?Year="+ddlistYear.SelectedItem.Value+"&MOE="+ddlistMOE.SelectedItem.Value);
        }

        protected void ddlistSySession_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistPaperCode.Items.Clear();
            populatePaperCodes(ddlistSySession.SelectedItem.Text,rblMode.SelectedItem.Value);
            dtlistShowSubjects.Visible = false;
            pnlDateTime.Visible = false;
            btnFind.Visible = true;
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistPaperCode.Items.Clear();
            populatePaperCodes(ddlistSySession.SelectedItem.Text, rblMode.SelectedItem.Value);
            dtlistShowSubjects.Visible = false;
            pnlDateTime.Visible = false;
            btnFind.Visible = true;
        }
       
    }
}