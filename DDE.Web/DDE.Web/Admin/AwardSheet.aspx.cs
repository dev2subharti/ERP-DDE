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
    public partial class AwardSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 70) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 69))
            {
                if (!IsPostBack)
                {               
                    lblExamName.Text = "( Examination : "+Session["ExamName"].ToString()+" )";
                    lblSubjectName.Text = Session["SubjectName"].ToString();
                    lblSubjectCode.Text = Session["SubjectCode"].ToString();
                    hlinkQPFile.NavigateUrl= "QuestionPapers/" + Session["ExamCode"].ToString() +"/"+ FindInfo.findQPFileName(Session["ExamCode"].ToString(), Session["SubjectCode"].ToString());

                    ddlistAT.Items.Add(new ListItem("NA", "0"));    
                    PopulateDDList.populateExaminers(ddlistAT, Session["ExamCode"].ToString());
                  
                    populateAwardSheet();

                    if (Request.QueryString["ASNo"] != null)
                    {
                        if (Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                        {
                            ddlistAT.Items.FindByValue(FindInfo.findAllottedFIDByASNo(Convert.ToInt32(Request.QueryString["ASNo"]), Session["ExamCode"].ToString()).ToString()).Selected = true;
                            lblAT.Visible = true;
                            ddlistAT.Visible = true;
                            ddlistAT.Enabled = false;
                        }
                        else
                        {
                            lblAT.Visible = false;
                            ddlistAT.Visible = false;
                        }
                    }
                   
                }                

            }         
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private void populateAwardSheet()
        {
            int year = FindInfo.findSubjectYearByPaperCode(lblSubjectCode.Text);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (Session["ExamCode"].ToString()=="A13")
            {
                cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where SubjectID in (" + Session["SubjectID"].ToString() + ") and TOR>='" + Convert.ToDateTime(Session["From"]) + "' and TOR<='" + Convert.ToDateTime(Session["To"]) + "'";
            }

            else if (Session["ExamCode"].ToString() == "B13" || Session["ExamCode"].ToString() == "A14" || Session["ExamCode"].ToString() == "B14" || Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17" || Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
            {
                if (Request.QueryString["ASNo"] != null)
                {
                    cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where ASPRID='" + Convert.ToInt32(Request.QueryString["ASNo"]) + "'";
                }
                else
                {
                    if (Session["CF"].ToString() == "AAS")
                    {
                        if (Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                        {
                            if (Session["ASFilter"].ToString() == "1")
                            {
                                cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where SubjectID in (" + Session["SubjectID"].ToString() + ") and ASPRID='0' and (DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy!='2470' and DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy!='2552' and DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy!='2563' and DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy!='2566' and DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy!='2572')";
                            }
                            else if (Session["ASFilter"].ToString() == "2")
                            {
                                cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where SubjectID in (" + Session["SubjectID"].ToString() + ") and ASPRID='0' and (DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy='2470' or DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy='2552' or DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy='2563' or DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy='2566' or DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + ".ReceivedBy='2572')";
                            }
                        }
                        else
                        {
                            cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where SubjectID in (" + Session["SubjectID"].ToString() + ") and ASPRID='0'";
                        }
                    }
                    else if (Session["CF"].ToString() == "IAS")
                    {
                        cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where SubjectID in (" + Session["SubjectID"].ToString() + ") and ReceivedBy='" + Convert.ToInt32(Session["ERID"]) + "' and ASPRID='0'";
                    }

                }
            }
           

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("EC");
            DataColumn dtcol4 = new DataColumn("RollNo");
            DataColumn dtcol5 = new DataColumn("Course");
            DataColumn dtcol6 = new DataColumn("MarksObt");
            DataColumn dtcol7 = new DataColumn("MarksObt(In Words)");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            string asrid = "";

            int tc = 0;
            
            while (dr.Read())
            {
                if (asrid == "")
                {
                    asrid = (dr["ASRID"]).ToString();
                }
                else
                {
                    asrid =asrid+","+ (dr["ASRID"]).ToString();
                }
                DataRow drow = dt.NewRow();
               
                drow["EnrollmentNo"] = FindInfo.findENoByID(Convert.ToInt32(dr["SRID"]));
                
                if (drow["EnrollmentNo"].ToString() != "NA")
                {
                    drow["EC"] = drow["EnrollmentNo"].ToString().Substring(drow["EnrollmentNo"].ToString().Length - 5, 5);
                }
                else
                {
                    drow["EC"] = 0;
                }
                if (Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                {
                    drow["RollNo"] = FindInfo.findRollNoBySRID1(Convert.ToInt32(dr["SRID"]), year, Session["ExamCode"].ToString(), dr["MOE"].ToString());
                }
                else
                {
                    drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]),Session["ExamCode"].ToString(), dr["MOE"].ToString());
                }
              
                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]),year);
                if (dr["Received"].ToString() == "True")
                {
                    drow["MarksObt"] = "";
                    drow["MarksObt(In Words)"] = "";
                    tc = tc + 1;
                }
                else if (dr["Received"].ToString() == "False")
                {
                    drow["MarksObt"] = "AB";
                    drow["MarksObt(In Words)"] = "Absent";
                }
                dt.Rows.Add(drow);
              
            }

            if (Session["ExamCode"].ToString() == "A16")
            {
                if (Request.QueryString["ASNo"] != null)
                {
                    if (Convert.ToInt32(Request.QueryString["ASNo"]) <= 367)
                    {
                        dt.DefaultView.Sort = "EnrollmentNo";

                    }
                    else if (Convert.ToInt32(Request.QueryString["ASNo"]) > 367)
                    {
                        dt.DefaultView.Sort = "EC";

                    }
                }
                else
                {
                    dt.DefaultView.Sort = "EC";
                }
            }
            else if (Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
            {              
                dt.DefaultView.Sort = "EC";             
            }
            else
            {
                dt.DefaultView.Sort = "EnrollmentNo";
            }

            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }


            gvAwarsSheet.DataSource = dt;
            gvAwarsSheet.DataBind();

            con.Close();

            lblTC.Text = tc.ToString();

            if (j <= 1)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
                btnYes.Visible = false;
                btnNo.Visible = false;
            }
            else
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }

           

            ViewState["ASRID"] = asrid;
           
        }

        private void populateASlip()
        {
            if (Request.QueryString["ASNo"] != null)
            {
                int ascounter=Convert.ToInt32( Request.QueryString["ASNo"]);
                lblASNoAS.Text = String.Format("{0:0000}",ascounter);
            }
            else
            {
                lblASNoAS.Text = ViewState["ascount"].ToString();
            }
            lblTCAS.Text = lblTC.Text;
            lblPaperCodeAS.Text = lblPaperCode.Text;
            lblTDAS.Text = "...........";
            lblENAS.Text = ddlistAT.SelectedItem.Text;
         
            lblDOIAS.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblDOSAS.Text = "..........";

            pnlASlip.Visible = true;
        }

        private void populateQP()
        {

            lblPaperCode.Text = lblSubjectCode.Text;
            lblCourseCode.Text = FindInfo.findSubjectCodesByPaperCode(lblPaperCode.Text);
            lblExamination.Text = lblExamName.Text;
            lblYear.Text = FindInfo.findYearByPaperCode(lblPaperCode.Text);
            lblSubName.Text = lblSubjectName.Text;
            populateSNo();
            populateQuestions();
        }

        private void populateSNo()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEQuestionPapers where ExamCode='" + Session["ExamCode"].ToString() + "' and PaperCode='" + Session["SubjectCode"].ToString() + "' and Status='True' order by QSNo", con);

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

                dtlistShowQP.DataSource = dt;
                dtlistShowQP.DataBind();
                pnlQP.Visible = true;
                
            }

           
        }

        private void populateQuestions()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEQuestionPapers where ExamCode='" + Session["ExamCode"].ToString() + "' and PaperCode='" + Session["SubjectCode"].ToString() + "' and Status='True' order by QSNo", con);

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int times = 0;
               

                if (Request.QueryString["ASNo"] != null)
                {
                    if (FindInfo.isASPrintedByASNo(Convert.ToInt32(Request.QueryString["ASNo"]), Session["ExamCode"].ToString(), out times))
                    {

                        pnlData.Visible = false;
                        lblMSG.Text = "This Award sheet has already printed '" + times.ToString() + "' times.<br/> Do you want to print this Award Sheet again ?";
                        btnYes.Visible = true;
                        btnNo.Visible = true;
                        pnlMSG.Visible = true;
                        Session["printcounter"] = times;

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Error : 001. Please contact IT Cell";
                        pnlMSG.Visible = true;
                    }

                }
                else
                {
                    Session["Period"] = Convert.ToDateTime(Session["From"]).ToString("dd-MM-yyyy").Substring(0, 10) + " To " + Convert.ToDateTime(Session["To"]).ToString("dd-MM-yyyy").Substring(0, 10);

                    string preperiod = "";
                    if (alreadyPrinted(Session["SubjectCode"].ToString(), out times, out preperiod))
                    {

                        pnlData.Visible = false;
                        if (Session["ExamCode"].ToString() == "A13")
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Award sheet with this paper code has already printed '" + times.ToString() + "' times. Last print period was </br> '" + preperiod + "' <br/> Do you want to print this Award Sheet with current period again ?";
                            btnYes.Visible = true;
                            btnNo.Visible = true;
                            pnlMSG.Visible = true;
                            Session["printcounter"] = times;
                        }
                        else if (Session["ExamCode"].ToString() == "B13" || Session["ExamCode"].ToString() == "A14" || Session["ExamCode"].ToString() == "B14" || Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry you can not print it again. Please go to intial page";
                            btnYes.Visible = false;
                            btnNo.Visible = false;
                            pnlMSG.Visible = true;
                        }


                    }
                    else
                    {
                        if (Session["ExamCode"].ToString() == "A13")
                        {

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into DDEASPrintRecord_A13 values(@SubjectID,@PaperCode,@Period,@Times)", con);

                            cmd.Parameters.AddWithValue("@SubjectID", Session["SubjectID"].ToString());
                            cmd.Parameters.AddWithValue("@PaperCode", Session["SubjectCode"].ToString());
                            cmd.Parameters.AddWithValue("@Period", Session["Period"].ToString());
                            cmd.Parameters.AddWithValue("@Times", 1);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Log.createLogNow("Award Sheet Printed", "Award Sheet with Paper Code : " + lblSubjectCode.Text + " Printed  For Period '" + Session["Period"].ToString() + "' For '" + Session["ExamName"].ToString() + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                            btnPrint.Visible = false;
                            hlinkQPFile.Visible = false;

                            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                           
                        }
                        else if (Session["ExamCode"].ToString() == "B13" || Session["ExamCode"].ToString() == "A14" || Session["ExamCode"].ToString() == "B14")
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into DDEASPrintRecord_" + Session["ExamCode"].ToString() + " OUTPUT INSERTED.ASPRID values(@SubjectID,@ASRID,@PaperCode,@Period,@Times,@TE,@FE,@Uploaded)", con);

                            cmd.Parameters.AddWithValue("@SubjectID", Session["SubjectID"].ToString());
                            cmd.Parameters.AddWithValue("@ASRID", ViewState["ASRID"].ToString());
                            cmd.Parameters.AddWithValue("@PaperCode", Session["SubjectCode"].ToString());
                            cmd.Parameters.AddWithValue("@Period", DateTime.Now.ToString());
                            cmd.Parameters.AddWithValue("@Times", 1);
                            string[] str = ViewState["ASRID"].ToString().Split(',');
                            cmd.Parameters.AddWithValue("@TE", str.Length);
                            cmd.Parameters.AddWithValue("@FE", 0);
                            cmd.Parameters.AddWithValue("@Uploaded", "False");
                           
                            con.Open();
                            int ascounter = (Int32)cmd.ExecuteScalar();
                            con.Close();

                            object res = setAwardSheetNo(ViewState["ASRID"].ToString(), ascounter);
                            if (Convert.ToInt32(res) == str.Length)
                            {
                                Session["ASPrinted"] = "YES";

                                lblASCounter.Text = "Award Sheet No :" + String.Format("{0:0000}", ascounter);
                                lblASCounter.Visible = true;

                                Log.createLogNow("Award Sheet Printed", lblASCounter.Text + "' Printed of '" + Session["SubjectCode"].ToString() + "' For Period '" + Session["Period"].ToString() + "' For '" + Session["ExamName"].ToString() + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                                btnPrint.Visible = false;
                                hlinkQPFile.Visible = false;
                               
                                ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! Some error occured.Please contact to ERP Developer.";
                                btnYes.Visible = false;
                                btnNo.Visible = false;
                                pnlMSG.Visible = true;
                            }
                        }
                        else if (Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16")
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into DDEASPrintRecord_" + Session["ExamCode"].ToString() + " OUTPUT INSERTED.ASPRID values(@SubjectID,@ASRID,@PaperCode,@Period,@Times,@TE,@FE,@Uploaded,@PrintMode,@PrintedBy)", con);

                            cmd.Parameters.AddWithValue("@SubjectID", Session["SubjectID"].ToString());
                            cmd.Parameters.AddWithValue("@ASRID", ViewState["ASRID"].ToString());
                            cmd.Parameters.AddWithValue("@PaperCode", Session["SubjectCode"].ToString());
                            cmd.Parameters.AddWithValue("@Period", DateTime.Now.ToString());
                            cmd.Parameters.AddWithValue("@Times", 1);
                            string[] str = ViewState["ASRID"].ToString().Split(',');
                            cmd.Parameters.AddWithValue("@TE", str.Length);
                            cmd.Parameters.AddWithValue("@FE", 0);
                            cmd.Parameters.AddWithValue("@Uploaded", "False");

                          
                            if(Session["CF"].ToString()=="AAS")
                            {
                               cmd.Parameters.AddWithValue("@PrintMode",1 );
                            }
                            else if(Session["CF"].ToString()=="IAS")
                            {
                                cmd.Parameters.AddWithValue("@PrintMode", 2);
                            }

                            cmd.Parameters.AddWithValue("@PrintedBy",Convert.ToInt32(Session["ERID"]));
                          
                            con.Open();
                            int ascounter = (Int32)cmd.ExecuteScalar();
                            con.Close();

                            object res = setAwardSheetNo(ViewState["ASRID"].ToString(), ascounter);
                            if (Convert.ToInt32(res) == str.Length)
                            {
                                Session["ASPrinted"] = "YES";

                                lblASCounter.Text = "Award Sheet No :" + String.Format("{0:0000}", ascounter);
                                lblASCounter.Visible = true;

                                Log.createLogNow("Award Sheet Printed", lblASCounter.Text + "' Printed of '" + Session["SubjectCode"].ToString() + "' For Period '" + Session["Period"].ToString() + "' For '" + Session["ExamName"].ToString() + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                                btnPrint.Visible = false;
                                hlinkQPFile.Visible = false;

                                lblPrintedBy.Text = "Published By : " + Session["Name"].ToString();
                                lblPrintedBy.Visible = true;

                                ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! Some error occured.Please contact to ERP Developer.";
                                btnYes.Visible = false;
                                btnNo.Visible = false;
                                pnlMSG.Visible = true;
                            }
                        }
                        else if (Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into DDEASPrintRecord_" + Session["ExamCode"].ToString() + " OUTPUT INSERTED.ASPRID values(@SubjectID,@ASRID,@PaperCode,@Period,@Times,@TE,@FE,@Uploaded,@PrintMode,@PrintedBy,@CheckedBy,@AllottedTo)", con);

                            cmd.Parameters.AddWithValue("@SubjectID", Session["SubjectID"].ToString());
                            cmd.Parameters.AddWithValue("@ASRID", ViewState["ASRID"].ToString());
                            cmd.Parameters.AddWithValue("@PaperCode", Session["SubjectCode"].ToString());
                            cmd.Parameters.AddWithValue("@Period", DateTime.Now.ToString());
                            cmd.Parameters.AddWithValue("@Times", 1);
                            string[] str = ViewState["ASRID"].ToString().Split(',');
                            cmd.Parameters.AddWithValue("@TE", str.Length);
                            cmd.Parameters.AddWithValue("@FE", 0);
                            cmd.Parameters.AddWithValue("@Uploaded", "False");

                            if (Session["CF"].ToString() == "AAS")
                            {
                                cmd.Parameters.AddWithValue("@PrintMode", 1);
                            }
                            else if (Session["CF"].ToString() == "IAS")
                            {
                                cmd.Parameters.AddWithValue("@PrintMode", 2);
                            }

                            cmd.Parameters.AddWithValue("@PrintedBy", Convert.ToInt32(Session["ERID"]));
                            cmd.Parameters.AddWithValue("@CheckedBy", 0);
                            cmd.Parameters.AddWithValue("@AllottedTo", Convert.ToInt32(ddlistAT.SelectedItem.Value));

                            con.Open();
                            int ascounter = (Int32)cmd.ExecuteScalar();
                            con.Close();


                            ViewState["ascount"] = String.Format("{0:0000}", ascounter); ;
                            object res = setAwardSheetNo(ViewState["ASRID"].ToString(), ascounter);
                            if (Convert.ToInt32(res) == str.Length)
                            {
                                Session["ASPrinted"] = "YES";

                                lblASCounter.Text = "Award Sheet No :" + String.Format("{0:0000}", ascounter);
                                lblASCounter.Visible = true;

                                lblAT.Text = " Alloted To : "+ddlistAT.SelectedItem.Text;
                                lblAT.Visible = true;
                              
                                Log.createLogNow("Award Sheet Printed", lblASCounter.Text + "' Printed of '" + Session["SubjectCode"].ToString() + "' For Period '" + Session["Period"].ToString() + "' For '" + Session["ExamName"].ToString() + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                                btnPrint.Visible = false;
                                hlinkQPFile.Visible = false;

                                lblPrintedBy.Text = "Published By : " + Session["Name"].ToString();
                                lblPrintedBy.Visible = true;

                                if (Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                                {
                                    populateQP();
                                    populateASlip();
                                }

                                ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = "Sorry !! Some error occured.Please contact to ERP Developer.";
                                btnYes.Visible = false;
                                btnNo.Visible = false;
                                pnlMSG.Visible = true;
                            }
                        }
                   
                    }

                    ddlistAT.Visible = false;
               }
            }
            catch (Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                btnYes.Visible = false;
                btnNo.Visible = false;
                pnlMSG.Visible = true;
            }

           
    
        }

        private int setAwardSheetNo(string asrids, int ascounter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " set ASPRID=@ASPRID where ASRID in (" + asrids + ")", con);

            cmd.Parameters.AddWithValue("@ASPRID", ascounter);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();

            return res;   
               
        }

        private bool alreadyPrinted(string pcode,out int times, out string preperiod)
        {
            bool printed = false;
            times = 0;
            preperiod = "";

            if (Session["ExamCode"].ToString() == "A13")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDEASPrintRecord_A13 where PaperCode='" + pcode + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    times = Convert.ToInt32(dr["Times"]);
                    preperiod = dr["Period"].ToString();
                    printed = true;

                }
                con.Close();
            }
            else if (Session["ExamCode"].ToString() == "B13" || Session["ExamCode"].ToString() == "A14" || Session["ExamCode"].ToString() == "B14" || Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
            {
                if (Session["ASPrinted"].ToString() == "NO")
                {
                    printed = false;
                }
                else if (Session["ASPrinted"].ToString() == "YES")
                {
                    printed = true;
                }
            }
           
            return printed;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnPrint.Visible = false;

            if (Request.QueryString["ASNo"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEASPrintRecord_" + Session["ExamCode"].ToString() + " set Period=@Period,Times=@Times where ASPRID='" + Convert.ToInt32(Request.QueryString["ASNo"]) + "' ", con);

                cmd.Parameters.AddWithValue("@Period", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@Times", (Convert.ToInt32(Session["printcounter"]) + 1));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                lblASCounter.Text = "Award Sheet No :" + Request.QueryString["ASNo"];
                lblASCounter.Visible = true;

                lblAT.Text = "Allotted To :" + FindInfo.findAllottedToByASNo(Convert.ToInt32(Request.QueryString["ASNo"]), Session["ExamCode"].ToString());
                lblAT.Visible = true;

                if (Session["ExamCode"].ToString() == "A15" || Session["ExamCode"].ToString() == "B15" || Session["ExamCode"].ToString() == "A16" || Session["ExamCode"].ToString() == "B16" || Session["ExamCode"].ToString() == "A17" || Session["ExamCode"].ToString() == "B17"|| Session["ExamCode"].ToString() == "A18" || Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                {
                    lblPrintedBy.Text = "Published By : " + FindInfo.findASPrintedBy(Convert.ToInt32(Request.QueryString["ASNo"]), Session["ExamCode"].ToString());
                    lblPrintedBy.Visible = true;
                }

                if (Session["ExamCode"].ToString() == "B18" || Session["ExamCode"].ToString() == "W10" || Session["ExamCode"].ToString() == "Z10")
                {
                    populateQP();
                    populateASlip();
                }
                Log.createLogNow("Award Sheet Printed", lblASCounter.Text + "' Printed of '" + Session["SubjectCode"].ToString() + "' For '" + Session["ExamName"].ToString() + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
            }

            else
            {
                if (Session["ExamName"].ToString() == "A13")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDEASPrintRecord_A13 set Period=@Period,Times=@Times where PaperCode='" + Session["SubjectCode"].ToString() + "' ", con);

                    cmd.Parameters.AddWithValue("@Period", Session["Period"].ToString());
                    cmd.Parameters.AddWithValue("@Times", (Convert.ToInt32(Session["printcounter"]) + 1));



                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    Log.createLogNow("Award Sheet Printed", "Award Sheet with Paper Code : " + lblSubjectCode.Text + " Printed  For Period '" + Session["Period"].ToString() + "' For '" + Session["ExamName"].ToString() + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                    ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Error : 002. Please contact IT Cell";
                    pnlMSG.Visible = true;
                }

                
            }
                                        
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;          
            pnlMSG.Visible = false;
        }
    }
}
