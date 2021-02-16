using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DDE.DAL;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class ResultCalcuate : System.Web.UI.Page
    {
        public SqlConnection CSOE = new SqlConnection();
        public SqlConnection CSCO = new SqlConnection();
        string MOE = "";
        public string NewSRID, NewSubjectID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            //{
            pnlSearch.Visible = true;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
            Fillddl();
            //}
            //else
            //{
            //    pnlSearch.Visible = true;
            //    pnlData.Visible = true;
            //    pnlMSG.Visible = false;
            //    btnOK.Visible = false;
            //    //pnlData.Visible = false;
            //    //lblMSG.Text = "Sorry !! You are not authorised for this control";
            //    //pnlMSG.Visible = true;
            //}
        }

        void Fillddl()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct Folder from [SHEET DATA 2447002]", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlCenterCode.Items.Add(dr["Folder"].ToString());
            }

            con.Close();


        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            if (btnSearch.Text == "Search")
            {
                int srid = 0;

                if (rblSearchType.SelectedItem.Value == "4")
                {
                    //populateStudents();

                }
                else
                {
                    if (rblSearchType.SelectedItem.Value == "1")
                    {
                        //srid = FindInfo.findSRIDByOANo(Convert.ToInt32(tbID.Text));                  
                    }
                    else if (rblSearchType.SelectedItem.Value == "2")
                    {
                        //srid = FindInfo.findSRIDByANo(tbID.Text);

                    }
                    else if (rblSearchType.SelectedItem.Value == "3")
                    {
                        srid = FindInfo.findSRIDByENo(tbID.Text);

                        populateStudents();


                    }
                    //if (srid != 0)
                    //{
                    //    Session["RecordType"] = "Show";
                    //    Response.Redirect("DStudentRegistration.aspx?SRID=" + srid);
                    //}

                    else if (srid == 0)
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! No Record Found.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }



            }
            else if (btnSearch.Text == "Search Another")
            {
                tbID.Enabled = true;
                rblSearchType.Enabled = true;
                dtlistShowStudents.Visible = false;
                btnSearch.Text = "Search";
            }

        }

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEStudentRecord where EnrollmentNo = '" + tbID.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("Course");
            DataColumn dtcol7 = new DataColumn("SCCode");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            da.SelectCommand.Connection = con;
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i + 1;
                    drow["SRID"] = ds.Tables[0].Rows[i]["SRID"];
                    drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"];
                    drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"];
                    drow["FatherName"] = ds.Tables[0].Rows[i]["FatherName"];
                    drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                    drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"];

                    dt.Rows.Add(drow);
                }
            }

            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();


            if (ds.Tables[0].Rows.Count > 0)
            {

                dtlistShowStudents.Visible = true;
                pnlMSG.Visible = false;
                rblSearchType.Enabled = false;
                tbID.Enabled = false;
                btnSearch.Text = "Search Another";

            }

            else
            {
                dtlistShowStudents.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
                rblSearchType.Enabled = true;
                tbID.Enabled = true;

            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {

            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }



        protected void dtlistShowStudents_ItemCommand(object source, DataListCommandEventArgs e)
        {
            //Session["RecordType"] = "Show";
            //Response.Redirect("DStudentRegistration.aspx?SRID=" +e.CommandArgument);
        }

        protected void rblSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }

        protected void btncalculate_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("srid");
            dt.Columns.Add("EnrollmentNo");
            dt.Columns.Add("remarks");
            String sqlResult = "select z.RN1 ,(z.p1+'-'+z.p2) papercd,a.EnrollmentNo,a.srid from[dbo].[SHEET DATA 2447002] z";
            sqlResult += " inner join[ddedbweb].[dbo].[DDEStudentRecord] a on a.EnrollmentNo=z.ENO where z.FOLDER ='"+ ddlCenterCode.SelectedValue +"'";
            SqlConnectionOpen();
            SqlCommand AllrecordOf = new SqlCommand(sqlResult + " order By DTEXID asc", CSOE);
            SqlDataAdapter Allda = new SqlDataAdapter(AllrecordOf);
            DataTable Alldt = new DataTable();

            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = Alldt.DefaultView.ToTable(true, "srid", "EnrollmentNo", "papercd", "RN1");

            Allda.SelectCommand.Connection = CSOE;
            Allda.Fill(Alldt);
            if (dtUniqRecords.Rows.Count > 0)
            {
                String varMarks = "", varInsertUpdate = "";
                SqlCommand ExiquitQry;
                for (int l = 0; l <= dtUniqRecords.Rows.Count - 1; l++)
                {
                    varMarks = ResultCalc(dtUniqRecords.Rows[l]["EnrollmentNo"].ToString(), dtUniqRecords.Rows[l]["papercd"].ToString());

                    string[] EnormentPCod = varMarks.Split(',');
                    if (EnormentPCod.Length > 0)
                    {
                        runSqlQueryStr("select* from[dbo].[ResultCalculate] where SRID = '" + dtUniqRecords.Rows[l]["srid"].ToString() + "' and PaperCode='" + dtUniqRecords.Rows[l]["papercd"].ToString() + "'", out string chk);
                        if (dtUniqRecords.Rows[l]["RN1"].ToString() == "W")
                        { MOE = "R"; }
                        else if (dtUniqRecords.Rows[l]["RN1"].ToString() == "XW")
                        { MOE = "B"; }
                        else { MOE = ""; }
                        try
                        {
                            if (chk == "")
                            {
                                varInsertUpdate = "INSERT INTO ResultCalculate ([SRID],[EnrolmentNo],[PaperCode],[SubjectID],[MaxMarks],[MinMarks],[CreatedAt],[CenterCode],[MOE],[ModifyAt])VALUES(";
                                varInsertUpdate += "'" + dtUniqRecords.Rows[l]["srid"].ToString() + "','" + dtUniqRecords.Rows[l]["EnrollmentNo"].ToString() + "',";
                                varInsertUpdate += "'" + dtUniqRecords.Rows[l]["papercd"].ToString() + "','" + EnormentPCod[1].ToString() + "','60',";
                                varInsertUpdate += "'" + EnormentPCod[0].ToString() + "', getdate(),'" + ddlCenterCode.SelectedValue + "','" + MOE + "','')";
                            }
                            else
                            {
                                varInsertUpdate = "update [ResultCalculate] set MinMarks = '" + EnormentPCod[0].ToString() + "' ,";
                                varInsertUpdate += "[CenterCode] = '" + ddlCenterCode.SelectedValue + "',[EnrolmentNo] ='" + dtUniqRecords.Rows[l]["EnrollmentNo"].ToString() + "',";
                                varInsertUpdate += "[ModifyAt] = GetDate() Where SRID = '" + dtUniqRecords.Rows[l]["srid"].ToString() + "'";
                                varInsertUpdate += " and PaperCode ='" + dtUniqRecords.Rows[l]["papercd"].ToString() + "'";
                            }

                            ExiquitQry = new SqlCommand(varInsertUpdate, CSCO);
                            SqlConnectionOnlineOpen();
                            ExiquitQry.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            DataRow dr = dt.NewRow();
                            dr["srid"] = dtUniqRecords.Rows[l]["srid"].ToString();
                            dr["EnrollmentNo"] = dtUniqRecords.Rows[l]["EnrollmentNo"].ToString();
                            dr["remarks"] = "Fail to Insert/Update record";
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["srid"] = dtUniqRecords.Rows[l]["srid"].ToString();
                        dr["EnrollmentNo"] = dtUniqRecords.Rows[l]["EnrollmentNo"].ToString();
                        dr["remarks"] = "Record not found";
                        dt.Rows.Add(dr);
                    }
                }
            }
            dtaLstError.DataSource = new Int32[0];
            dtaLstError.DataBind();

            if (dt.Rows.Count > 0)
            {
                dtaLstError.DataSource = dt;
                dtaLstError.DataBind();
            }
        }

        public static string ResultCalc(string varEnrol, string varPaperCD)
        {
            string varMarks = "0";
            String sqlResultCal = "select sum((case when StudentAnswer = Ans then 1 else 0 end)) a,SubjectID from(";
            sqlResultCal += " (SELECT eno, isnull(StudentAnswer,0)StudentAnswer ,ROW_NUMBER() OVER(PARTITION BY frmid order by frmid) as f FROM[SHEET DATA 2447002] UNPIVOT";
            sqlResultCal += " (StudentAnswer FOR ColName in ([F001],[F002],[F003],[F004],[F005],[F006],[F007],[F008],[F009],[F010],[F011],[F012],[F013],[F014],[F015],";
            sqlResultCal += " [F016],[F017],[F018],[F019],[F020],[F021],[F022],[F023],[F024],[F025],[F026],[F027],[F028],[F029],[F030],[F031],[F032],[F033],[F034],";
            sqlResultCal += " [F035],[F036],[F037],[F038],[F039],[F040],[F041],[F042],[F043],[F044],[F045],[F046],[F047],[F048],[F049],[F050],[F051],[F052],[F053],";
            sqlResultCal += " [F054],[F055],[F056],[F057],[F058],[F059],[F060]))AS[SHEET DATA 2447002]";
            sqlResultCal += " where eno = '" + varEnrol + "' and (p1+'-'+p2) = '" + varPaperCD + "' ) z ";
            sqlResultCal += " inner join(select Ans, qsno, SubjectID  from[dbo].[QuestionBank]";
            sqlResultCal += " inner join QuestionPapers on QuestionBank.QID = QuestionPapers.QID";

            sqlResultCal += " inner join(select z.SubjectID, z.PaperCode from ddedbweb.dbo.DDESubject z";
            sqlResultCal += " right join ddedbweb.dbo.DDEStudentRecord a on ";
            sqlResultCal += " (case z.nyear when 1 then a.Course when 2 then a.course2year when 3 then a.course3year end) = z.courseid ";
            sqlResultCal += " and a.EnrollmentNo = '" + varEnrol + "' and a.SyllabusSession = z.SyllabusSession) w on w.PaperCode = [QuestionBank].PaperCode)";

            sqlResultCal += " where QuestionBank.PaperCode = '" + varPaperCD + "' and ExamCode = 'Z11') a on a.qsno = z.f) group by SubjectID";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlCommand AllrecordOf = new SqlCommand(sqlResultCal, con);
            SqlDataAdapter Allda = new SqlDataAdapter(AllrecordOf);
            DataSet Alldt = new DataSet();

            if (Alldt.Tables[0].Rows.Count > 0)
                varMarks = Alldt.Tables[0].Rows[0]["a"].ToString() + "," + Alldt.Tables[0].Rows[0]["SubjectID"].ToString();

            con.Close();
            return varMarks;
        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            int result = 0;
            string ColumnName = "";
            try
            {
                SqlConnectionOpen();
                SqlCommand cmd = new SqlCommand("select *,(P1 + '-' + P2) as subjectCode from[SHEET DATA 2447002] where ENO  = '" + tbID.Text + "'", CSOE);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.SelectCommand.Connection = CSOE;
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k <= dt.Rows.Count - 1; k++)
                    {
                        //SqlCommand cmd1 = new SqlCommand("select  Top 60 Ans from [dbo].[QuestionBank] where PaperCode ='" + dt.Rows[k]["subjectCode"] + "' order by QID asc", CSOE);
                        SqlCommand cmd1 = new SqlCommand("select top 60 Ans from[dbo].[QuestionBank] inner join QuestionPapers on QuestionBank.QID = QuestionPapers.QID  where QuestionBank.PaperCode = '" + dt.Rows[k]["subjectCode"] + "' and ExamCode = 'Z11' order by QuestionBank.QID asc", CSOE);
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.SelectCommand.Connection = CSOE;
                        da1.Fill(dt1);
                        for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                        {
                            ColumnName = "F" + string.Format("{0:000}", i + 1);
                            if (dt.Rows[0][ColumnName].ToString() == dt1.Rows[i]["Ans"].ToString().ToUpper())
                            { result++; }
                        }
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd2 = new SqlCommand("select SubjectID, SriD from DDEStudentRecord with (nolock) inner join DDESubject with (nolock) on DDEStudentRecord.syllabussession = DDESubject.SyllabusSession " +
                        "where DDEStudentRecord.EnrollmentNo = '" + tbID.Text + "' and DDESubject.PaperCode = '" + dt.Rows[k]["subjectCode"] + "' and " +
                        "CourseID = (Select case when Course = 76 and Course2Year != '' then Course2Year else Course END)", con1);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                        da2.SelectCommand.Connection = con1;
                        da2.Fill(dt2);

                        runSqlQueryStr("select* from[dbo].[ResultCalculate] with (nolock) where SRID = '" + dt2.Rows[0]["SRID"] + "' and PaperCode='" + dt.Rows[k]["subjectCode"] + "'", out string chk);
                        if (dt.Rows[k]["RN1"].ToString() == "W")
                        { MOE = "R"; }
                        else if (dt.Rows[k]["RN1"].ToString() == "XW")
                        { MOE = "B"; }
                        else { MOE = ""; }
                        if (chk == "")
                        {
                            SqlCommand ExiquitQry = new SqlCommand("INSERT INTO [dbo].[ResultCalculate]([SRID],[EnrolmentNo],[PaperCode],[SubjectID],[MaxMarks],[MinMarks],[CreatedAt],[CenterCode],[MOE],[ModifyAt])VALUES ('" + dt2.Rows[0]["SRID"] + "','" + tbID.Text + "','" + dt.Rows[k]["subjectCode"] + "','" + dt2.Rows[0]["SubjectID"] + "','60','" + result + "',GetDate(),'001Z11','" + MOE + "','')", CSCO);

                            SqlConnectionOnlineOpen();
                            ExiquitQry.ExecuteNonQuery();
                            result = 0;
                            MsgBox("Result Save Succfully", this);
                        }
                        else
                        {
                            SqlCommand ExiquitQry = new SqlCommand("update [dbo].[ResultCalculate] set MinMarks = '" + result + "' ,[CenterCode] = '001Z11',[EnrolmentNo] ='" + tbID.Text + "',  ModifyAt = GetDate() Where SRID = '" + dt2.Rows[0]["SRID"] + "' and PaperCode ='" + dt.Rows[k]["subjectCode"] + "'", CSCO);
                            SqlConnectionOnlineOpen();
                            ExiquitQry.ExecuteNonQuery();
                            result = 0;
                            MsgBox("Result Update  Succfully", this);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void runSqlQueryStr(String SqlQry, out string j)
        {
            CSCO.Close();
            SqlConnectionOnlineOpen();
            SqlCommand cmd = new SqlCommand(SqlQry, CSCO);
            try
            {
                j = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                j = "FALSE";
            }
            CSCO.Close();
        }

        public void SqlConnectionOpen()
        {
            CSOE.ConnectionString = ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString();

            if (CSOE.State == ConnectionState.Closed)
            {
                CSOE.Open();
            }
            else
            {
                CSOE.Close();
                CSOE.Open();
            }
        }
        public void SqlConnectionOnlineOpen()
        {
            CSCO.ConnectionString = ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString();

            if (CSCO.State == ConnectionState.Closed)
            {
                CSCO.Open();
            }
            else
            {
                CSCO.Close();
                CSCO.Open();
            }
        }

        protected void btnCalculate_Click1(object sender, EventArgs e)
        {
            //SqlConnectionOpen();
            //SqlCommand AllrecordOf = new SqlCommand("select * from [dbo].[SHEET DATA 2447002] order By ENO asc", CSOE);
            //SqlDataAdapter Allda = new SqlDataAdapter(AllrecordOf);
            //DataTable Alldt = new DataTable();
            //Allda.SelectCommand.Connection = CSOE;
            //Allda.Fill(Alldt);
            //if (Alldt.Rows.Count > 0)
            //{
            //    for (int l = 0; l <= Alldt.Rows.Count - 1; l++)
            //    {
            //        SqlCommand ResultQry = new SqlCommand("select sum((case when StudentAnswer = Ans then 1 else 0 end)) a from(" +
            //        "(SELECT isnull(StudentAnswer,0)StudentAnswer ,ROW_NUMBER() OVER(PARTITION BY frmid order by frmid) as f FROM[SHEET DATA 2447002] UNPIVOT" +
            //        "(StudentAnswer FOR ColName in ([F001],[F002],[F003],[F004],[F005],[F006],[F007],[F008],[F009],[F010],[F011],[F012],[F013],[F014],[F015]," +
            //        "[F016],[F017],[F018],[F019],[F020],[F021],[F022],[F023],[F024],[F025],[F026],[F027],[F028],[F029],[F030],[F031],[F032],[F033],[F034]," +
            //        "[F035],[F036],[F037],[F038],[F039],[F040],[F041],[F042],[F043],[F044],[F045],[F046],[F047],[F048],[F049],[F050],[F051],[F052],[F053]," +
            //        "[F054],[F055],[F056],[F057],[F058],[F059],[F060]))AS[SHEET DATA 2447002] where eno = '" + Alldt.Rows + "' and PCODE = 'DMC218') z" +
            //        "inner join(select top 60 Ans, qsno from[dbo].[QuestionBank] inner join QuestionPapers on QuestionBank.QID = QuestionPapers.QID" +
            //        "where QuestionBank.PaperCode = 'DMC-218' and ExamCode = 'Z11' order by QuestionBank.QID asc) a on a.qsno = z.f) ");
            //    }
            //}
        }
        public void MsgBox(string msg, Page refP)
        {
            Label lbl = new Label();
            string lb = "window.alert('" + msg + "')";
            ScriptManager.RegisterClientScriptBlock(refP, GetType(), "UniqueKey", lb, true);
            refP.Controls.Add(lbl);
        }

    }
}

