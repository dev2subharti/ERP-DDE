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
using System.Data.SqlClient;
using DDE.DAL;


namespace DDE.Web.Admin
{
    public partial class FillReceivedCopy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 69))
            {
                if (!IsPostBack)
                {
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
                srid = findSRIDByRollNo(tbENo.Text,ddlistExamination.SelectedItem.Value,ddlistMOE.SelectedItem.Value);
            }

            //string remark;

            //if (!FindInfo.isDetained(srid, ddlistExamination.SelectedItem.Value,ddlistMOE.SelectedItem.Value, out remark)|| FindInfo.findSCCodeForAdmitCardBySRID(srid)=="261")
            //{

                string year = FindInfo.findAllExamYear(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);

                if (year != "0" && year != "")
                {

                    if (year.Length > 1)
                    {
                        populateYears(year);
                        lblEYear.Visible = true;
                        ddlistYear.Visible = true;
                        btnSearch.Visible = false;
                        btnSearchAgain.Visible = true;
                        ddlistMOE.Enabled = false;
                        rblMode.Visible = false;
                        tbENo.Enabled = false;
                        btnSearchAgain.CommandName = srid.ToString();

                    }
                    else
                    {
                        polulateStudentInfo(srid, year);
                        populateSubjectInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                        setSubjectInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);

                        rblMode.Visible = false;
                        pnlSearch.Visible = false;
                        pnlASRecord.Visible = true;
                        btnSubmit.Visible = true;
                       
                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry ! Course or Exam or Both fee is not paid.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            //}
            //else
            //{
            //    pnlData.Visible = false;
            //    lblMSG.Text = lblMSG.Text = "Sorry ! student is detained.<br/> Reason for deatined is : " + remark;
            //    pnlMSG.Visible = true;
            //    btnOK.Visible = true;
            //}

                setValidation();
           
        }

        private void setValidation()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 104))
            {
                foreach (DataListItem dli in dtlistRecAS.Items)
                {
                    RadioButtonList rblRec = (RadioButtonList)dli.FindControl("rblRec");
                    Label rt = (Label)dli.FindControl("lblRecTime");
                    ImageButton delete = (ImageButton)dli.FindControl("imgbtnDelete");

                    if (rt.Text != "")
                    {
                        delete.Visible = true;
                    }
                    else
                    {
                        delete.Visible = false;
                    }
                    rblRec.Enabled = true;
                }
            }
        }       

        private int findSRIDByRollNo(string rollno,string exam,string moe)
        {
            int srid = 0;

            if (exam == "A13")
            {
                if (moe == "R")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select SRID from ExamRecord_June13 where RollNo='" + rollno + "'", con);
                    con.Open();
                    SqlDataReader dr;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        srid = Convert.ToInt32(dr[0]);

                    }

                    con.Close();

                }

                else if (moe == "B")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_A13 where RollNo='" + rollno + "'", con);
                    con.Open();
                    SqlDataReader dr;


                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        srid = Convert.ToInt32(dr[0]);


                    }

                    con.Close();
                }
            }
            else 
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_"+exam+" where RollNo='" + rollno + "' and MOE='"+moe+"'", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    srid = Convert.ToInt32(dr[0]);

                }

                con.Close();
            }
           
            return srid;
        }

        private void populateYears(string year)
        {
            if (year == "1,2" || year == "2,1")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";
            }
            else if (year == "2,3" || year == "3,2")
            {
                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
            else if (year == "1,3" || year == "3,1")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
            else if (year == "1,2,3" || year == "3,2,1" || year == "3,1,2" || year == "2,1,3" || year == "2,3,1" || year == "1,3,2")
            {
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";
            }
            
        }

        private void setSubjectInfo(int srid,string exam,string moe)
        {
            foreach (DataListItem dli in dtlistRecAS.Items)
            {
                Label lblSubID = (Label)dli.FindControl("lblSubjectID");
                Label lblPCode = (Label)dli.FindControl("lblPaperCode");
                Label lblAF = (Label)dli.FindControl("lblAF");
                Label lblASRID = (Label)dli.FindControl("lblASRID");
                RadioButtonList rblRec = (RadioButtonList)dli.FindControl("rblRec");
                RadioButtonList rblPRec = (RadioButtonList)dli.FindControl("rblPRec");
                Label lblRecBy = (Label)dli.FindControl("lblRecBy");
                Label lblRecTime = (Label)dli.FindControl("lblRecTime");
                Label lblASNo = (Label)dli.FindControl("lblASNo");
                ImageButton delete = (ImageButton)dli.FindControl("imgbtnDelete");
                Image imgError = (Image)dli.FindControl("imgError");

                int notprintedPC = FindInfo.findNOTPrintedASByPC(lblPCode.Text,ddlistExamination.SelectedItem.Value,Convert.ToInt32(Session["ERID"]));
                if (notprintedPC >= 200)
                {
                    imgError.Visible = true;

                }
                
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select * from DDEAnswerSheetRecord_" + exam + " where SRID='" + lblSRID.Text + "' and SubjectID='" + lblSubID.Text + "' and MOE='" + moe + "'", con);
                    con.Open();
                    SqlDataReader dr;

                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (dr["Received"].ToString() == "True")
                        {
                            rblRec.Items.FindByValue("1").Selected = true;
                            rblPRec.Items.FindByValue("1").Selected = true;
                            rblRec.Enabled = false;

                            lblRecBy.Text = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["ReceivedBy"]));
                            lblRecTime.Text = Convert.ToDateTime(dr["TOR"].ToString()).ToString("dd MMMM yyyy");
                            lblASNo.Text = dr["ASPRID"].ToString();
                        }
                        else if (dr["Received"].ToString() == "False")
                        {
                            if (lblPCode.Text != "")
                            {
                                rblRec.Items.FindByValue("0").Selected = true;
                                rblPRec.Items.FindByValue("0").Selected = true;
                                rblRec.Enabled = false;

                                rblRec.Visible = true;
                                lblRecBy.Text = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["ReceivedBy"]));
                                lblRecTime.Text = Convert.ToDateTime(dr["TOR"].ToString()).ToString("dd MMMM yyyy");
                                lblASNo.Text = "NP";

                            }
                            else
                            {

                                rblRec.Visible = false;

                            }
                        }

                        lblAF.Text = "YES";
                        lblASRID.Text = Convert.ToInt32(dr["ASRID"]).ToString();
                        delete.CommandArgument = Convert.ToInt32(dr["ASRID"]).ToString();
                    }
                    else
                    {
                        lblAF.Text = "NO";
                        if (lblPCode.Text != "")
                        {
                            rblRec.Visible = true;
                        }
                        else
                        {
                            rblRec.Visible = false;


                        }

                    }

                    con.Close();

            }

        }

        private void populateSubjectInfo(int srid,string exam,string moe)
        {
            string ssession = "";
            if (ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17" || ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10" || ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11")
            {
                ssession = lblSSession.Text;
            }
            else
            {
                ssession = "A 2010-11";
            }
            if (moe == "R")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDESubject where SyllabusSession='"+ssession+"' and CourseName='" + lblCourse.Text + "' and Year='" + lblYear.Text + "' order by SubjectSNo", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();


                DataTable dt = new DataTable();


                DataColumn dtcol1 = new DataColumn("SubjectSNo");
                DataColumn dtcol2 = new DataColumn("SubjectCode");
                DataColumn dtcol3 = new DataColumn("PaperCode");
                DataColumn dtcol4 = new DataColumn("SubjectID");
                DataColumn dtcol5 = new DataColumn("SubjectName");



                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);




                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SubjectSNo"] = Convert.ToString(dr["SubjectSNo"]);
                    drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                    drow["PaperCode"] = Convert.ToString(dr["PaperCode"]);
                    drow["SubjectID"] = Convert.ToString(dr["SubjectID"]);
                    drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);
                    dt.Rows.Add(drow);

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
                SqlCommand cmd = new SqlCommand("Select BPSubjects1,BPSubjects2,BPSubjects3 from DDEExamRecord_"+exam+" where SRID='" + srid  + "' and MOE='B'", con);
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
                DataColumn dtcol1 = new DataColumn("SubjectSNo");
                DataColumn dtcol2 = new DataColumn("SubjectCode");
                DataColumn dtcol3 = new DataColumn("PaperCode");
                DataColumn dtcol4 = new DataColumn("SubjectID");
                DataColumn dtcol5 = new DataColumn("SubjectName");



                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
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

                            string[] subinfo = FindInfo.findSubjectInfoByID(Convert.ToInt32(sub1[j]));
                            DataRow drow = dt.NewRow();
                            drow["SubjectSNo"] = i.ToString();
                            drow["SubjectCode"] = subinfo[0];
                            drow["PaperCode"] = subinfo[3];
                            drow["SubjectID"] = sub1[j].ToString();
                            drow["SubjectName"] = subinfo[1];
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

                            string[] subinfo = FindInfo.findSubjectInfoByID(Convert.ToInt32(sub2[j]));
                           
                            DataRow drow = dt.NewRow();
                            drow["SubjectSNo"] = i.ToString();
                            drow["SubjectCode"] = subinfo[0];
                            drow["PaperCode"] = subinfo[3];
                            drow["SubjectID"] = sub2[j].ToString();
                            drow["SubjectName"] = subinfo[1];
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

                            string[] subinfo = FindInfo.findSubjectInfoByID(Convert.ToInt32(sub3[j]));
                            DataRow drow = dt.NewRow();
                            drow["SubjectSNo"] = i.ToString();
                            drow["SubjectCode"] = subinfo[0];
                            drow["PaperCode"] = subinfo[3];
                            drow["SubjectID"] = sub3[j].ToString();
                            drow["SubjectName"] = subinfo[1];
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
                lblSCCode.Text = findSCCodeBySRID(srid);
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
              
            }
            

            con.Close();



        }

        private string findSCCodeBySRID(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SCStatus,StudyCentreCode,PreviousSCCode from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
                {
                    sccode = Convert.ToString(dr["StudyCentreCode"]);
                }
                else if (dr["SCStatus"].ToString() == "T")
                {
                    sccode = Convert.ToString(dr["PreviousSCCode"]);
                }

            }

            con.Close();

            return sccode;
        }
      
        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;           
            pnlSearch.Visible = true;
            pnlASRecord.Visible = false;
            btnSubmit.Visible = false;
            rblMode.Visible = true;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistRecAS.Items)
            {
                Label lblSubID = (Label)dli.FindControl("lblSubjectID");
                Label lblAF = (Label)dli.FindControl("lblAF");
                RadioButtonList rblRec = (RadioButtonList)dli.FindControl("rblRec");
                RadioButtonList rblPRec = (RadioButtonList)dli.FindControl("rblPRec");

              
                    if (lblAF.Text=="NO")
                    {
                        if (rblRec.SelectedIndex != -1)
                        {
                            if (rblRec.SelectedItem.Value == "1" || rblRec.SelectedItem.Value == "0")
                            {
                                if (!alreadyFilled(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text),ddlistMOE.SelectedItem.Value))
                                {
                              
                                    string subject = FindInfo.findSubjectDetailByID(Convert.ToInt32(lblSubID.Text));

                                if (ddlistExamination.SelectedItem.Value == "A13")
                                {

                                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                    SqlCommand cmd = new SqlCommand("insert into DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + " values(@SRID,@SubjectID,@MOA,@Received,@ReceivedBy,@TOR)", con);


                                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
                                    cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(lblSubID.Text));
                                    cmd.Parameters.AddWithValue("@MOA", ddlistMOE.SelectedItem.Value);
                                    cmd.Parameters.AddWithValue("@Received", "True");
                                    cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());


                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                                else if (ddlistExamination.SelectedItem.Value == "B13" || ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17"|| ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10" || ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11")
                                {
                                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                    SqlCommand cmd = new SqlCommand("insert into DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + " values(@SRID,@SubjectID,@MOA,@Received,@ReceivedBy,@TOR,@ASPRID)", con);


                                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
                                    cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(lblSubID.Text));
                                    cmd.Parameters.AddWithValue("@MOA", ddlistMOE.SelectedItem.Value);
                                    cmd.Parameters.AddWithValue("@Received", rblRec.SelectedItem.Value);
                                    cmd.Parameters.AddWithValue("@ReceivedBy", Convert.ToInt32(Session["ERID"]));
                                    cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());
                                    cmd.Parameters.AddWithValue("@ASPRID", 0);

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                    if (ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17"|| ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10" || ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11")
                                    {
                                        if (rblRec.SelectedItem.Value == "0")
                                        {
                                            Exam.fillMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, lblSCCode.Text, "AB");
                                        }
                                    }

                                    if(ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11")
                                    {
                                        int thm = uploadTHMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text),lblSCCode.Text, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                                        //int pm = uploadPRMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text),lblSCCode.Text, ddlistMOE.SelectedItem.Value);
                                    }
                                }


                                Log.createLogNow("Answer Sheet Received", "Marked Answer Sheet Status '" + rblRec.SelectedItem.Text + "' of '" + subject + "' for " + ddlistExamination.SelectedItem.Text + " exam with Enrollment No. '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                pnlData.Visible = false;
                                lblMSG.Text = "Record has been updated successfully";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;
                                }
                                else
                                {

                                    pnlData.Visible = false;
                                    lblMSG.Text = "Sorry! this answer sheet is already received.";
                                    pnlMSG.Visible = true;
                                    btnOK.Visible = true;
                                    break;
                                }
                            }
                        }
                        
                    }
                    else if (lblAF.Text=="YES")
                    {
                        if (rblRec.SelectedIndex != -1)
                        {
                            if (rblRec.SelectedItem.Value != rblPRec.SelectedItem.Value)
                            {

                                string subject = FindInfo.findSubjectDetailByID(Convert.ToInt32(lblSubID.Text));


                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("update DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + " set  Received=@Received,ReceivedBy=@ReceivedBy,TOR=@TOR where SRID='" + lblSRID.Text + "' and SubjectID='" + lblSubID.Text + "'", con);

                                cmd.Parameters.AddWithValue("@Received", rblRec.SelectedItem.Value);
                                cmd.Parameters.AddWithValue("@ReceivedBy", Convert.ToInt32(Session["ERID"]));
                                cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                if (ddlistExamination.SelectedItem.Value == "A14" || ddlistExamination.SelectedItem.Value == "B14" || ddlistExamination.SelectedItem.Value == "A15" || ddlistExamination.SelectedItem.Value == "B15" || ddlistExamination.SelectedItem.Value == "A16" || ddlistExamination.SelectedItem.Value == "B16" || ddlistExamination.SelectedItem.Value == "A17" || ddlistExamination.SelectedItem.Value == "B17"|| ddlistExamination.SelectedItem.Value == "A18" || ddlistExamination.SelectedItem.Value == "B18" || ddlistExamination.SelectedItem.Value == "W10" || ddlistExamination.SelectedItem.Value == "Z10" || ddlistExamination.SelectedItem.Value == "W11" || ddlistExamination.SelectedItem.Value == "Z11")
                                {
                                    if (rblRec.SelectedItem.Value == "0")
                                    {
                                        Exam.fillMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, lblSCCode.Text, "AB");
                                    }
                                    if (rblRec.SelectedItem.Value == "1")
                                    {
                                        Exam.fillMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, lblSCCode.Text, "");
                                    }
                                  
                                }

                                Log.createLogNow("Updated Ans Sheet Record", "Marked Answer Sheet Status '" + rblRec.SelectedItem.Text + "' of '" + subject + "' for " + ddlistExamination.SelectedItem.Text + " exam with Enrollment No. '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                pnlData.Visible = false;
                                lblMSG.Text = "Record has been updated successfully";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;

                            }
                        }

                    }
                    
                }
               
            
        }     

        private int uploadTHMarks(int srid, int subjectid,string sccode, string exam, string moe)
        {
            string thmarks;
            int counter = 0;

            if (isTHEntryExist(srid, subjectid,exam, moe, out thmarks))
            {
                if (thmarks == "")
                {
                    Random rd = new Random();
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_"+exam+" set Theory=@Theory where SRID='" + srid + "' and SubjectID='" + subjectid + "'", con2);

                    cmd2.Parameters.AddWithValue("@Theory", rd.Next(55, 70));

                    con2.Open();
                    int j = cmd2.ExecuteNonQuery();
                    con2.Close();
                    counter = counter + j;
                }

            }
            else
            {
                Random rd = new Random();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_"+exam+" values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                cmd.Parameters.AddWithValue("@SRID", srid);
                cmd.Parameters.AddWithValue("@SubjectID", subjectid);
                cmd.Parameters.AddWithValue("@StudyCentreCode", sccode);
                cmd.Parameters.AddWithValue("@Theory", rd.Next(55, 70));
                cmd.Parameters.AddWithValue("@IA", "");
                cmd.Parameters.AddWithValue("@AW", "");
                cmd.Parameters.AddWithValue("@MOE", moe);

                cmd.Connection = con;
                con.Open();
                int j = cmd.ExecuteNonQuery();
                con.Close();
                counter = counter + j;

            }

            return counter;
        }

        private bool isTHEntryExist(int srid, int subjectid,string exam, string moe, out string thmarks)
        {
            bool exist = false;
            thmarks = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select RID,Theory from DDEMarkSheet_"+exam+" where SRID ='" + srid + "' and SubjectID='" + subjectid + "' and MOE='"+moe+"'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                thmarks = dr["Theory"].ToString();
                exist = true;
            }
            con.Close();

            return exist;

        }

        private bool alreadyFilled(int srid, int subid, string moe)
        {
            bool rec = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + "  where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='" + moe + "'", con);
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
               
                rec = true;
               
                
            }

            con.Close();
            return rec;

        }

        protected void btnSearchAgain_Click(object sender, EventArgs e)
        {
            int srid = Convert.ToInt32(btnSearchAgain.CommandName);
            polulateStudentInfo(srid, ddlistYear.SelectedItem.Value);
            populateSubjectInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
            setSubjectInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);

            rblMode.Visible = false;
            pnlSearch.Visible = false;
            pnlASRecord.Visible = true;
            btnSubmit.Visible = true;
           
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "1")
            {
                lblNo.Text = "Enrollment No.";
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                lblNo.Text = "Roll No.";
            }
        }

        protected void dtlistRecAS_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                Label pc=(Label)e.Item.FindControl("lblPaperCode");


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEAnswerSheetRecord_" + ddlistExamination.SelectedItem.Value + " where ASRID ='" + Convert.ToString(e.CommandArgument) + "'", con);

                con.Open();
                cmd.ExecuteReader();
                con.Close();


                Log.createLogNow("Delete", "Deleted a Answer Sheet Receiving Record of '" + pc.Text + "' of '" + tbENo.Text + "' for '"+ddlistExamination.SelectedItem.Text+"' exam", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been deleted successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
               
            }
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistRecAS.Items)
            {
                RadioButtonList rblRec = (RadioButtonList)dli.FindControl("rblRec");

                rblRec.Items.FindByValue("1").Selected = true;

                
            }
        }
    }
}
