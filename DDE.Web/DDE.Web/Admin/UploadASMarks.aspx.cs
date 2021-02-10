using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class UploadASMarks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "id", "ValidatePassKey()", true);

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                if (!IsPostBack)
                {
                    pnlSearch.Visible = true;
                    pnlAS.Visible = false;
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
            lblASCounter.Text = String.Format("{0:0000}",Convert.ToInt32(tbASNo.Text));
            lblASCounter.Visible = true;
            string[] subdetail = FindInfo.findSubjectDetailByASPRID(Convert.ToInt32(tbASNo.Text),ddlistExam.SelectedItem.Value);
            lblSubjectName.Text = subdetail[0].ToString();
            lblSubjectCode.Text = subdetail[1].ToString();
            populateAwardSheet();
            setAccessibility();
        }

        private void setAccessibility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) && !Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                foreach (DataListItem dli in dtlistAS.Items)
                {
                    TextBox theory = (TextBox)dli.FindControl("tbMO");

                    if (theory.Text == "")
                    {
                        theory.Enabled = true;
                    }
                    else
                    {
                        theory.Enabled = false;
                    }

                }

            }
            else if (!Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                foreach (DataListItem dli in dtlistAS.Items)
                {
                    TextBox theory = (TextBox)dli.FindControl("tbMO");

                    if (theory.Text == "")
                    {
                        theory.Enabled = false;
                    }
                    else
                    {
                        theory.Enabled = true;
                    }

                }

            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 26) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                foreach (DataListItem dli in dtlistAS.Items)
                {           
                    TextBox theory = (TextBox)dli.FindControl("tbMO");
                    theory.Enabled = true;
                }
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 29))
            {
                foreach (DataListItem dli in dtlistAS.Items)
                {

                    TextBox theory = (TextBox)dli.FindControl("tbMO");
                    theory.Enabled = false;

                }
            }

        }
        private void populateAwardSheet()
        {
            int year = FindInfo.findSubjectYearByPaperCode(lblSubjectCode.Text);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + " where ASPRID='" + Convert.ToInt32(tbASNo.Text) + "'", con);
            SqlDataReader dr;
         
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("SubjectID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("RollNo");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("SCCode");
            DataColumn dtcol9 = new DataColumn("MarksObt");
            DataColumn dtcol10 = new DataColumn("MarksFilled");
            DataColumn dtcol11 = new DataColumn("MOE");
         


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);
      
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while(dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SRID"] = Convert.ToInt32(dr["SRID"]);
                    drow["SubjectID"] = Convert.ToInt32(dr["SubjectID"]);
                    drow["EnrollmentNo"] = FindInfo.findENoByID(Convert.ToInt32(dr["SRID"]));
                    if (drow["EnrollmentNo"].ToString() != "NA")
                    {
                        drow["EC"] = drow["EnrollmentNo"].ToString().Substring(drow["EnrollmentNo"].ToString().Length - 5, 5);
                    }
                    else
                    {
                        drow["EC"] = 0;
                    }
                    if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10")
                    {
                        drow["RollNo"] = FindInfo.findRollNoBySRID1(Convert.ToInt32(dr["SRID"]), year, ddlistExam.SelectedItem.Value, dr["MOE"].ToString());
                    }
                    else
                    {
                        drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, dr["MOE"].ToString());
                    }
                    int subyear = FindInfo.findSubjectYearByID(Convert.ToInt32(dr["SubjectID"]));
                    drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), subyear);
                    drow["SCCode"] = FindInfo.findSCCodeForMarkSheetBySRID(Convert.ToInt32(dr["SRID"]));

                    string thmarks;
                    if (Exam.isTheoryMarksFilled(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["SubjectID"]), ddlistExam.SelectedItem.Value, dr["MOE"].ToString(), out thmarks))
                    {
                        drow["MarksObt"] = thmarks;
                        drow["MarksFilled"] = "True";
                    }
                    else
                    {
                        drow["MarksObt"] = "";
                        drow["MarksFilled"] = "False";
                    }

                    drow["MOE"] = Convert.ToString(dr["MOE"]);
                   
                    dt.Rows.Add(drow);
                }
               
            }
            if (ddlistExam.SelectedItem.Value == "A16")
            {
                if (Convert.ToInt32(tbASNo.Text) <= 367)
                {
                    dt.DefaultView.Sort = "EnrollmentNo";
                   
                }
                else if (Convert.ToInt32(tbASNo.Text) > 367)
                {
                    dt.DefaultView.Sort = "EC";
                   
                }
            }
            else if (ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10")
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


            dtlistAS.DataSource = dt;
            dtlistAS.DataBind();

            con.Close();

            if (j <= 1)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
               
            }
            else
            {
                pnlSearch.Visible = false;
                pnlAS.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }

        }

        protected void btnUploadMarks_Click(object sender, EventArgs e)
        {
            try
            {
                if (validMarks())
                {
                    if (ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10")
                    {
                        if (allMarksFilled())
                        {
                            if (pnlExaminer.Visible == true)
                            {
                                if (ddlistExaminer.SelectedItem.Text != "--SELECT ONE--")
                                {
                                    submitMarks();
                                    insertExaminerDetails();
                                    pnlData.Visible = false;
                                    lblMSG.Text = "Marks has been updated successfully";
                                    pnlMSG.Visible = true;
                                    btnOK.Visible = false;
                                }
                                else
                                {
                                    pnlData.Visible = false;
                                    lblMSG.Text = "Please Select any one Examiner.";
                                    pnlMSG.Visible = true;
                                    btnOK.Visible = true;
                                }
                            }
                            else
                            {
                                PopulateDDList.populateExaminers(ddlistExaminer,ddlistExam.SelectedItem.Value);
                                pnlExaminer.Visible = true;
                            }

                        }
                        else
                        {
                            submitMarks();
                            pnlData.Visible = false;
                            lblMSG.Text = "Marks has been updated successfully";
                            pnlMSG.Visible = true;
                            btnOK.Visible = false;
                        }
                    }
                    else
                    {
                        submitMarks();
                        pnlData.Visible = false;
                        lblMSG.Text = "Marks has been updated successfully";
                        pnlMSG.Visible = true;
                        btnOK.Visible = false;
                    }


                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Invalid Marks are filled at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check marks should be between 0-100 and should be in numeric";
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

        private void insertExaminerDetails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEASPrintRecord_" + ddlistExam.SelectedItem.Value + " set CheckedBy=@CheckedBy where ASPRID ='" + tbASNo.Text + "'", con);

            cmd.Parameters.AddWithValue("@CheckedBy", ddlistExaminer.SelectedItem.Value);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
        }

        private bool allMarksFilled()
        {
            bool af = true;
            foreach (DataListItem dli in dtlistAS.Items)
            {

               
                TextBox theory = (TextBox)dli.FindControl("tbMO");
                if (theory.Text == "")
                {
                    af = false;
                    break;
                }
               
            }

            return af;
        }

        private void submitMarks()
        {
            int fe = 0;
            foreach (DataListItem dli in dtlistAS.Items)
            {

                Label srid = (Label)dli.FindControl("lblSRID");
                Label subid = (Label)dli.FindControl("lblSubjectID");
                Label moe = (Label)dli.FindControl("lblMOE");
                Label eno = (Label)dli.FindControl("lblENo");
                Label sccode = (Label)dli.FindControl("lblSCCode");
                Label course = (Label)dli.FindControl("lblCourse");
                Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                TextBox theory = (TextBox)dli.FindControl("tbMO");
                Label ltheory = (Label)dli.FindControl("lblMO");



                if (lblmf.Text == "False")
                {

                    if (theory.Text != "")
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                        cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));
                        cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(subid.Text));
                        cmd.Parameters.AddWithValue("@StudyCentreCode", sccode.Text);
                        cmd.Parameters.AddWithValue("@Theory", theory.Text);
                        cmd.Parameters.AddWithValue("@IA", "");
                        cmd.Parameters.AddWithValue("@AW", "");
                        cmd.Parameters.AddWithValue("@MOE", moe.Text);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        fe = fe + 1;

                        Log.createLogNow("Marks Filling", "Filled '" + theory.Text + "' theory marks of a student with Enrollment No '" + eno.Text + "' of paper code '" + lblSubjectCode.Text + "' in course '" + course.Text + "' for '" + ddlistExam.SelectedItem.Text + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                    }

                }
                else if (lblmf.Text == "True")
                {

                    if (ltheory.Text != theory.Text)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " set Theory=@Theory where SRID='" + srid.Text + "' and SubjectID='" + subid.Text + "' and MOE='" + moe.Text + "'", con);

                        cmd.Parameters.AddWithValue("@Theory", theory.Text);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Marks Updation", "Updated theory marks from '" + ltheory.Text + "' to '" + theory.Text + "' of a student with Enrollment No '" + eno.Text + "' of paper code '" + lblSubjectCode.Text + "' in course '" + course.Text + "' for '" + ddlistExam.SelectedItem.Text + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                    }
                    
                    fe = fe + 1;

                }



            }

            if (fe != 0 && (ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10"))
            {

                updateASFilledInfo(tbASNo.Text, fe);
            }

           
        }

        private void updateASFilledInfo(string asno, int fe)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEASPrintRecord_"+ddlistExam.SelectedItem.Value+" set FE=@FE,Uploaded=@Uploaded where ASPRID='" + Convert.ToInt32(tbASNo.Text) + "' ", con);
           
            cmd.Parameters.AddWithValue("@FE", fe);
            cmd.Parameters.AddWithValue("@Uploaded", "True");

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private bool validMarks()
        {

            bool validmarks = false;
            int SNo = 0;
            foreach (DataListItem dli in dtlistAS.Items)
            {
                try
                {
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    TextBox theorymarks = (TextBox)dli.FindControl("tbMO");

                    SNo = Convert.ToInt32(sno.Text);

                    if (theorymarks.Text.Trim().Length == 2 || theorymarks.Text.Trim() == "")
                    {

                        int tmarks;
                        if (theorymarks.Text == "")
                        {
                            tmarks = 0;
                        }
                        else if (theorymarks.Text == "AB")
                        {
                            tmarks = 0;
                        }
                        else
                        {
                            tmarks = Convert.ToInt32(theorymarks.Text);
                        }

                        if (tmarks >= 0 && tmarks <= 100)
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
                        validmarks = false;
                        Session["SNo"] = SNo;
                        break;
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            
            pnlAS.Visible = true;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }
       
    }


}
