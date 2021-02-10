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
    public partial class ReceiveProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 69))
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
                        polulateStudentInfo(srid, year.Substring(year.Length-1,1));
                        populatePracticalInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                        setPracticalInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                        
                        rblMode.Visible = false;
                        pnlSearch.Visible = false;
                        pnlASRecord.Visible = true;
                        btnSubmit.Visible = true;


                    }
                    else
                    {
                        polulateStudentInfo(srid, year);
                        populatePracticalInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                        setPracticalInfo(srid, ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value);

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

                setValidation();
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry ! This Enrollment No. does not not exist";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }

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

       
        private void setPracticalInfo(int srid, string exam, string moe)
        {
            foreach (DataListItem dli in dtlistRecAS.Items)
            {
                Label lblSubID = (Label)dli.FindControl("lblSubjectID");
                Label lblCCode = (Label)dli.FindControl("lblCourseCode");
                Label lblAF = (Label)dli.FindControl("lblAF");
                Label lblASRID = (Label)dli.FindControl("lblASRID");
                RadioButtonList rblRec = (RadioButtonList)dli.FindControl("rblRec");
                RadioButtonList rblPRec = (RadioButtonList)dli.FindControl("rblPRec");
                Label lblRecBy = (Label)dli.FindControl("lblRecBy");
                Label lblRecTime = (Label)dli.FindControl("lblRecTime");
                Label lblASNo = (Label)dli.FindControl("lblASNo");
                ImageButton delete = (ImageButton)dli.FindControl("imgbtnDelete");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEProjectRecRecord where SRID='" + lblSRID.Text + "' and PracticalID='" + lblSubID.Text + "' and MOE='" + moe + "'", con);
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
                        if (lblCCode.Text != "")
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
                    lblASRID.Text = Convert.ToInt32(dr["PRID"]).ToString();
                    delete.CommandArgument = Convert.ToInt32(dr["PRID"]).ToString();
                }
                else
                {
                    lblAF.Text = "NO";
                    if (lblCCode.Text != "")
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

        private void populatePracticalInfo(int srid, string exam, string moe)
        {
            string ssession = "";
           
           ssession = lblSSession.Text;
           
           
            if (moe == "R")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEPractical where SyllabusSession='" + ssession + "' and CourseName='" + lblCourse.Text + "' and Year='" + lblYear.Text + "' and (PracticalName like '%project%' or PracticalName like '%dissertation%') order by PracticalSNo", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();


                DataTable dt = new DataTable();


                DataColumn dtcol1 = new DataColumn("SubjectSNo");
                DataColumn dtcol2 = new DataColumn("SubjectCode");
              
                DataColumn dtcol4 = new DataColumn("SubjectID");
                DataColumn dtcol5 = new DataColumn("SubjectName");



                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
               
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);




                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SubjectSNo"] = Convert.ToString(dr["PracticalSNo"]);
                    drow["SubjectCode"] = Convert.ToString(dr["PracticalCode"]);
                   
                    drow["SubjectID"] = Convert.ToString(dr["PracticalID"]);
                    drow["SubjectName"] = Convert.ToString(dr["PracticalName"]);
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
                DataColumn dtcol1 = new DataColumn("SubjectSNo");
                DataColumn dtcol2 = new DataColumn("SubjectCode");
               
                DataColumn dtcol4 = new DataColumn("SubjectID");
                DataColumn dtcol5 = new DataColumn("SubjectName");



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
                            drow["SubjectSNo"] = i.ToString();
                            drow["SubjectCode"] = subinfo[0];                         
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

                            string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(sub2[j]));

                            DataRow drow = dt.NewRow();
                            drow["SubjectSNo"] = i.ToString();
                            drow["SubjectCode"] = subinfo[0];                        
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

                            string[] subinfo = FindInfo.findPracticalInfoByID(Convert.ToInt32(sub3[j]));
                            DataRow drow = dt.NewRow();
                            drow["SubjectSNo"] = i.ToString();
                            drow["SubjectCode"] = subinfo[0];                         
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
                lblCourseID.Text = dr["Course"].ToString();

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


                if (lblAF.Text == "NO")
                {
                    if (rblRec.SelectedIndex != -1)
                    {
                        if (rblRec.SelectedItem.Value == "1" || rblRec.SelectedItem.Value == "0")
                        {
                            if (!alreadyFilled(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text), ddlistMOE.SelectedItem.Value))
                            {

                                string subject = FindInfo.findPracticalDetailByID(Convert.ToInt32(lblSubID.Text));

                                
                                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                    SqlCommand cmd = new SqlCommand("insert into DDEProjectRecRecord values(@SRID,@PracticalID,@MOA,@Examination,@Received,@ReceivedBy,@TOR,@ASPRID)", con);


                                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
                                    cmd.Parameters.AddWithValue("@PracticalID", Convert.ToInt32(lblSubID.Text));
                                    cmd.Parameters.AddWithValue("@MOA", ddlistMOE.SelectedItem.Value);
                                    cmd.Parameters.AddWithValue("@Examination", ddlistExamination.SelectedItem.Value);
                                    cmd.Parameters.AddWithValue("@Received", rblRec.SelectedItem.Value);
                                    cmd.Parameters.AddWithValue("@ReceivedBy", Convert.ToInt32(Session["ERID"]));
                                    cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());
                                    cmd.Parameters.AddWithValue("@ASPRID", 0);

                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();

                                   
                                    //if (rblRec.SelectedItem.Value == "0")
                                    //{
                                    //    Exam.fillMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, lblSCCode.Text, "AB");
                                    //}
                                   
                                


                                //Log.createLogNow("Project Received", "Marked Project Rec Status '" + rblRec.SelectedItem.Text + "' of '" + subject + "' for " + ddlistExamination.SelectedItem.Text + " exam with Enrollment No. '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                                pnlData.Visible = false;
                                lblMSG.Text = "Record has been updated successfully";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;
                            }
                            else
                            {

                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry! this Project is already received.";
                                pnlMSG.Visible = true;
                                btnOK.Visible = true;
                                break;
                            }
                        }
                    }

                }
                else if (lblAF.Text == "YES")
                {
                    if (rblRec.SelectedIndex != -1)
                    {
                        if (rblRec.SelectedItem.Value != rblPRec.SelectedItem.Value)
                        {

                            string subject = FindInfo.findPracticalDetailByID(Convert.ToInt32(lblSubID.Text));


                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("update DDEProjectRecRecord set  Received=@Received,ReceivedBy=@ReceivedBy,TOR=@TOR where SRID='" + lblSRID.Text + "' and PracticalID='" + lblSubID.Text + "'", con);

                            cmd.Parameters.AddWithValue("@Received", rblRec.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@ReceivedBy", Convert.ToInt32(Session["ERID"]));
                            cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                           
                            //if (rblRec.SelectedItem.Value == "0")
                            //{
                            //    Exam.fillMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, lblSCCode.Text, "AB");
                            //}
                            //if (rblRec.SelectedItem.Value == "1")
                            //{
                            //    Exam.fillMarks(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(lblSubID.Text), ddlistExamination.SelectedItem.Value, ddlistMOE.SelectedItem.Value, lblSCCode.Text, "");
                            //}

                           

                            Log.createLogNow("Updated Project Rec Record", "Marked Project Rec Status '" + rblRec.SelectedItem.Text + "' of '" + subject + "' for " + ddlistExamination.SelectedItem.Text + " exam with Enrollment No. '" + lblENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                            pnlData.Visible = false;
                            lblMSG.Text = "Record has been updated successfully";
                            pnlMSG.Visible = true;
                            btnOK.Visible = true;

                        }
                    }

                }

            }




        }

        private bool alreadyFilled(int srid, int subid, string moe)
        {
            bool rec = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEProjectRecRecord  where SRID='" + srid + "' and PracticalID='" + subid + "' and MOE='" + moe + "' and Examination='"+ddlistExamination.SelectedItem.Value+"'", con);
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
                


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEProjectRecRecord where PRID ='" + Convert.ToString(e.CommandArgument) + "'", con);

                con.Open();
                cmd.ExecuteReader();
                con.Close();


                Log.createLogNow("Delete", "Deleted a Project Receiving Record of '" + tbENo.Text + "' for '" + ddlistExamination.SelectedItem.Text + "' exam", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been deleted successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = true;

            }
        }
    }
}