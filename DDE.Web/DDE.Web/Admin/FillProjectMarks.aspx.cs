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
    public partial class FillProjectMarks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("W10").Selected = true;
                    pnlSearch.Visible = true;
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int srid = 0;
            if (rblMode.SelectedItem.Value == "1")
            {
                srid = FindInfo.findSRIDByENo(tbENo.Text);
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                srid = findSRIDByRollNo(tbENo.Text, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
            }

            //string remark;

            //if (!FindInfo.isDetained(srid, ddlistExamination.SelectedItem.Value,ddlistMOE.SelectedItem.Value, out remark)|| FindInfo.findSCCodeForAdmitCardBySRID(srid)=="261")
            //{

            if (srid != 0)
            {
                string year = FindInfo.findAllExamYear(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);

                if (year != "0" && year != "")
                {

                    if (year.Length > 1)
                    {
                        polulateStudentInfo(srid, year.Substring(year.Length - 1, 1));
                        populatePracticalInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                      

                        rblMode.Visible = false;
                        pnlSearch.Visible = false;
                        pnlASRecord.Visible = true;
                        btnSubmit.Visible = true;


                    }
                    else
                    {
                        polulateStudentInfo(srid, year);
                        populatePracticalInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                      

                        rblMode.Visible = false;
                        pnlSearch.Visible = false;
                        pnlASRecord.Visible = true;
                        btnSubmit.Visible = true;

                    }
                }
                else
                {

                    string exam = "NA";
                    int cid = Convert.ToInt32(FindInfo.findCourseIDBySRID(srid));
                    if (FindInfo.isMBACourse(cid))
                    {
                        if (lblBatch.Text == "A 2009-10" || lblBatch.Text == "C 2010" || lblBatch.Text == "A 2010-11" || lblBatch.Text == "C 2011" || lblBatch.Text == "A 2011-12" || lblBatch.Text == "C 2012" || lblBatch.Text == "A 2012-13")
                        {
                            exam = FindInfo.findExamEnrolledBySRIDandYear(srid, 3, ddlistMOE.SelectedItem.Value);
                        }
                        else
                        {
                            exam = FindInfo.findExamEnrolledBySRIDandYear(srid, 2, ddlistMOE.SelectedItem.Value);
                        }
                    }
                    else
                    {
                        int cd = FindInfo.findCourseDuration(cid);
                        exam = FindInfo.findExamEnrolledBySRIDandYear(srid, cd, ddlistMOE.SelectedItem.Value);
                    }

                    if (exam != "NA")
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry ! Course or Exam or Both fee is paid for Exam : " + exam;
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                        ddlistExamination.SelectedItem.Selected = false;
                        ddlistExamination.Items.FindByValue(exam).Selected = true;
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry ! Course or Exam or Both fee not paid.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }
                //}
                //else
                //{
                //    pnlData.Visible = false;
                //    lblMSG.Text = lblMSG.Text = "Sorry ! student is detained.<br/> Reason for deatined is : " + remark;
                //    pnlMSG.Visible = true;
                //    btnOK.Visible = true;
                //}

               
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry ! This Enrollment No. does not not exist";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }

        }

        private int findSRIDByRollNo(string rollno, string exam, string moe)
        {
            int srid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_" + exam + " where RollNo='" + rollno + "' and MOE='" + moe + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                srid = Convert.ToInt32(dr[0]);

            }

            con.Close();


            return srid;
        }

        private void populatePracticalInfo(int srid, string exam, string moe)
        {
            string ssession = "";

            ssession = lblSSession.Text;


            if (moe == "R")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEPractical where SyllabusSession='" + ssession + "' and CourseName='" + lblCourse.Text + "' and Year='" + lblYear.Text + "' and (PracticalName like '%project%' or PracticalName like '%dissertation%' or PracticalName like '%case%') order by PracticalSNo", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();


                DataTable dt = new DataTable();


                DataColumn dtcol1 = new DataColumn("ProjectSNo");
                DataColumn dtcol2 = new DataColumn("ProjectCode");
                DataColumn dtcol3 = new DataColumn("ProjectID");
                DataColumn dtcol4 = new DataColumn("ProjectName");
                DataColumn dtcol5 = new DataColumn("MaxMarks");
                DataColumn dtcol6 = new DataColumn("MarksObt");
                DataColumn dtcol7 = new DataColumn("MarksFilled");
               


                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);
              

                int i = 1;
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["ProjectSNo"] = i.ToString();
                    drow["ProjectCode"] = Convert.ToString(dr["PracticalCode"]);
                    drow["ProjectID"] = Convert.ToString(dr["PracticalID"]);
                    drow["ProjectName"] = Convert.ToString(dr["PracticalName"]);
                    drow["MaxMarks"] = FindInfo.findPracMaxMarksByID(Convert.ToInt32(dr["PracticalID"]));
                    string pmarks;
                    if (Exam.isPracMarksFilled(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(dr["PracticalID"]), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out pmarks))
                    {
                        drow["MarksObt"] = pmarks;
                        drow["MarksFilled"] = "True";
                    }
                    else
                    {
                        drow["MarksObt"] = "";
                        drow["MarksFilled"] = "False";
                    }

                   
                    dt.Rows.Add(drow);
                    i++;

                }

                dtlistRecAS.DataSource = dt;
                dtlistRecAS.DataBind();

                con.Close();
            }
            else if (moe == "B")
            {
                string[] sub1 = { };
                string[] sub2 = { };
                string[] sub3 = { };

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select BPPracticals1,BPPracticals2,BPPracticals3 from DDEExamRecord_" + exam + " where SRID='" + srid + "' and MOE='B'", con);
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    sub1 = dr[0].ToString().Split(',');
                    sub2 = dr[1].ToString().Split(',');
                    sub3 = dr[2].ToString().Split(',');
                }
                con.Close();



                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("ProjectSNo");
                DataColumn dtcol2 = new DataColumn("ProjectCode");
                DataColumn dtcol4 = new DataColumn("ProjectID");
                DataColumn dtcol5 = new DataColumn("ProjectName");



                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);

                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);

                int i = 1;
                if (sub1.Length != 0)
                {
                    int j = 0;
                    while (j < sub1.Length)
                    {

                        if (sub1[j] != "")
                        {

                            string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(sub1[j]));
                            DataRow drow = dt.NewRow();
                            drow["ProjectSNo"] = i.ToString();
                            drow["ProjectCode"] = subinfo[0];
                            drow["ProjectID"] = sub1[j].ToString();
                            drow["ProjectName"] = subinfo[1];
                            dt.Rows.Add(drow);

                            i++;
                        }

                        j++;

                    }
                }
                if (sub2.Length != 0)
                {

                    int j = 0;
                    while (j < sub2.Length)
                    {



                        if (sub2[j] != "")
                        {

                            string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(sub2[j]));

                            DataRow drow = dt.NewRow();
                            drow["ProjectSNo"] = i.ToString();
                            drow["ProjectCode"] = subinfo[0];
                            drow["ProjectID"] = sub2[j].ToString();
                            drow["ProjectName"] = subinfo[1];
                            dt.Rows.Add(drow);

                            i++;

                        }

                        j++;

                    }
                }
                if (sub3.Length != 0)
                {

                    int j = 0;
                    while (j < sub3.Length)
                    {

                        if (sub3[j] != "")
                        {

                            string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(sub3[j]));
                            DataRow drow = dt.NewRow();
                            drow["ProjectSNo"] = i.ToString();
                            drow["ProjectCode"] = subinfo[0];
                            drow["ProjectID"] = sub3[j].ToString();
                            drow["ProjectName"] = subinfo[1];
                            dt.Rows.Add(drow);
                            i++;
                        }


                        j++;

                    }
                }

                dtlistRecAS.DataSource = dt;
                dtlistRecAS.DataBind();
            }
        }

        private void polulateStudentInfo(int srid, string year)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,StudyCentreCode,CYear,Session,SyllabusSession,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "' and RecordStatus='True'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                lblENo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = srid.ToString();
                lblSName.Text = dr["StudentName"].ToString();
                lblFName.Text = dr["FatherName"].ToString();
                lblSCCode.Text = dr["StudyCentreCode"].ToString();
                lblBatch.Text = dr["Session"].ToString();
                lblSSession.Text = dr["SyllabusSession"].ToString();
                if (year == "1")
                {
                    lblYear.Text = "1ST YEAR";
                }
                else if (year == "2")
                {
                    lblYear.Text = "2ND YEAR";
                }
                else if (year == "3")
                {
                    lblYear.Text = "3RD YEAR";
                }

                lblRollNo.Text = FindInfo.findRollNoBySRID1(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(year), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                string ecity;
                lblEC.Text = FindInfo.findExamCentreForAdmitCard(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out ecity);
                lblCourse.Text = FindInfo.findCourseNameBySRID(srid, Convert.ToInt32(year));
                lblCourseID.Text = dr["Course"].ToString();

            }


            con.Close();



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (validMarks())
                {

                    submitMarks();
                    pnlData.Visible = false;
                    lblMSG.Text = "Marks has been updated successfully";
                    pnlMSG.Visible = true;
                    btnOK.Text = "Upload for next Student";
                    btnOK.Visible = true;

                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Invalid Marks are filled at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check marks should be valid and in numeric";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;

                }
            }
            catch (Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void submitMarks()
        {

            foreach (DataListItem dli in dtlistRecAS.Items)
            {
                Label lblpracid = (Label)dli.FindControl("lblProjectID");
                Label lblpraccode = (Label)dli.FindControl("lblProjectCode");  
                Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                TextBox tpracmarks = (TextBox)dli.FindControl("tbMO");
                Label lpracmarks = (Label)dli.FindControl("lblMO");


                if (lblmf.Text == "False")
                {

                    if (tpracmarks.Text != "")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDEPracticalMarks_" + ddlistExamination.SelectedItem.Value + " values(@SRID,@PracticalID,@StudyCentreCode,@PracticalMarks,@MOE)", con);

                        cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
                        cmd.Parameters.AddWithValue("@PracticalID", Convert.ToInt32(lblpracid.Text));
                        cmd.Parameters.AddWithValue("@StudyCentreCode", lblSCCode.Text);
                        cmd.Parameters.AddWithValue("@PracticalMarks", tpracmarks.Text);
                        cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        Log.createLogNow("Marks Filling", "Filled '" + tpracmarks.Text + "' practical marks of a student with Enrollment No '" + lblENo.Text + "' of practical code '" + lblpraccode.Text + "' for '" + ddlistExamination.SelectedItem.Text + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));


                    }

                }
                else if (lblmf.Text == "True")
                {

                    if (lpracmarks.Text != tpracmarks.Text)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEPracticalMarks_" + ddlistExamination.SelectedItem.Value + " set PracticalMarks=@PracticalMarks where SRID='" + lblSRID.Text + "' and PracticalID='" + lblpracid.Text + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);

                        cmd.Parameters.AddWithValue("@PracticalMarks", tpracmarks.Text);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Marks Updation", "Updated practical marks from '" + lpracmarks.Text + "' to '" + tpracmarks.Text + "' of a student with Enrollment No '" + lblENo.Text + "' of practical code '" + lblpraccode.Text + "' for '" + ddlistExamination.SelectedItem.Text + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                    }


                }


            }

        }

        private bool validMarks()
        {

            bool validmarks = false;
            int SNo = 0;
            bool allowedforfullmarks = false;
           
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 68))
            {
                allowedforfullmarks = true;
            }
            foreach (DataListItem dli in dtlistRecAS.Items)
            {
                try
                {
                    Label sno = (Label)dli.FindControl("lblProjectSNo");
                    Label pracid = (Label)dli.FindControl("lblProjectID");
                    TextBox pracmarks = (TextBox)dli.FindControl("tbMO");

                    SNo = Convert.ToInt32(sno.Text);
                    int maxpracmarks = FindInfo.findPracMaxMarksByID(Convert.ToInt32(pracid.Text));
                    int pmarks;
                    if (pracmarks.Text == "" || pracmarks.Text == "-" || pracmarks.Text == "AB")
                    {
                        pmarks = 0;
                    }
                    else
                    {
                        pmarks = Convert.ToInt32(pracmarks.Text);
                    }


                    if (allowedforfullmarks)
                    {
                        if (pmarks >= 0 && pmarks <= maxpracmarks)
                        {
                            validmarks = true;

                        }

                        else
                        {
                            validmarks = false;
                            Session["SNo"] = SNo;
                            break;
                        }
                    }

                    else
                    {

                        if (pmarks >= 0 && pmarks <= ((maxpracmarks * 80) / 100))
                        {
                            validmarks = true;

                        }

                        else
                        {
                            validmarks = false;
                            Session["SNo"] = SNo;
                            break;
                        }

                    }

                }

                catch
                {
                    validmarks = false;
                    Session["SNo"] = SNo;
                    break;
                }


            }

            return validmarks;

        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (btnOK.Text == "Upload for next Student")
            {
                Response.Redirect("FillProjectMarks.aspx");
            }
            else
            {
                pnlData.Visible = true;

                pnlMSG.Visible = false;
            }
        }
    
    }
}