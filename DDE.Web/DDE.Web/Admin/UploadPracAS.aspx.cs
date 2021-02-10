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
    public partial class UploadPracAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "id", "ValidatePassKey()", true);

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populatePracticalCodeForAS(ddlistPracCode);
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

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            populatePracticalDetails();
            pnlAS.Visible = true;
            populateAwardSheet();
            setAccessibility();
        }

        private void setAccessibility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) && !Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
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
            else if (!Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
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
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 27) && Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 28))
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".RollNo,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.Course,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID where DDEStudentRecord.Course='" + Convert.ToInt32(lblCourseID.Text) + "' and ((DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year='" + lblYear.Text.Substring(0, 1) + "') or (DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".BPPracticals" + lblYear.Text.Substring(0, 1) + " like '%" + ddlistPracCode.SelectedItem.Value + "%')) and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) order by DDEStudentrecord.EnrollmentNo", con);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("RollNo");
            DataColumn dtcol5 = new DataColumn("MarksObt");
            DataColumn dtcol6 = new DataColumn("MarksFilled");
            DataColumn dtcol7 = new DataColumn("MOE");
                

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);



            if (ds.Tables[0].Rows.Count > 0)
            {
                string[] detained = FindInfo.findDetainedStudents(ddlistExam.SelectedItem.Value, "ALL");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                    if (!(pos > -1))
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i + 1;
                        drow["SRID"] = ds.Tables[0].Rows[i]["SRID"].ToString();
                        drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                        drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();

                        string pmarks;
                        if (Exam.isPracMarksFilled(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ddlistPracCode.SelectedItem.Value), ddlistExam.SelectedItem.Value, ds.Tables[0].Rows[i]["MOE"].ToString(), out pmarks))
                        {
                            drow["MarksObt"] = pmarks;
                            drow["MarksFilled"] = "True";
                        }
                        else
                        {
                            drow["MarksObt"] = "";
                            drow["MarksFilled"] = "False";
                        }

                        drow["MOE"] = ds.Tables[0].Rows[i]["MOE"].ToString();
                        dt.Rows.Add(drow);
                    }
                }

            }

            dtlistAS.DataSource = dt;
            dtlistAS.DataBind();




            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlAS.Visible = true;
                dtlistAS.Visible = true;
                btnUploadMarks.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlAS.Visible = false;
                dtlistAS.Visible = false;
                btnUploadMarks.Visible = false;
                lblMSG.Text = "Sorry No Record Found !!";
                pnlMSG.Visible = true;
            }

        }

        private void populatePracticalDetails()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical where PracticalID='" + ddlistPracCode.SelectedItem.Value + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lblPracticalName.Text = dr["PracticalName"].ToString();
                lblPracticalCode.Text = dr["PracticalCode"].ToString();
                lblCourse.Text = dr["CourseName"].ToString();
                lblCourseID.Text = FindInfo.findCourseIDByCourseName(lblCourse.Text).ToString();
                lblYear.Text = dr["Year"].ToString();
                lblSCCode.Text = ddlistSCCode.SelectedItem.Text;
                lblMMarks.Text = dr["PracticalMaxMarks"].ToString();

            }

            con.Close();
        }

        protected void btnUploadMarks_Click(object sender, EventArgs e)
        {
            try
            {
                if (validMarks())
                {
                    
                    submitMarks();
                    pnlData.Visible = false;
                    lblMSG.Text = "Marks has been updated successfully";
                    pnlMSG.Visible = true;
                    btnOK.Visible = false;
                  
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
          
            foreach (DataListItem dli in dtlistAS.Items)
            {

                Label srid = (Label)dli.FindControl("lblSRID");
              
                Label moe = (Label)dli.FindControl("lblMOE");
                Label eno = (Label)dli.FindControl("lblENo");                       
                Label lblmf = (Label)dli.FindControl("lblMarksFilled");
                TextBox tpracmarks = (TextBox)dli.FindControl("tbMO");
                Label lpracmarks = (Label)dli.FindControl("lblMO");


                if (lblmf.Text == "False")
                {

                    if (tpracmarks.Text != "")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDEPracticalMarks_" + ddlistExam.SelectedItem.Value + " values(@SRID,@PracticalID,@StudyCentreCode,@PracticalMarks,@MOE)", con);

                        cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));
                        cmd.Parameters.AddWithValue("@PracticalID", Convert.ToInt32(ddlistPracCode.SelectedItem.Value));
                        cmd.Parameters.AddWithValue("@StudyCentreCode", ddlistSCCode.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@PracticalMarks", tpracmarks.Text);
                        cmd.Parameters.AddWithValue("@MOE", moe.Text);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                      

                        Log.createLogNow("Marks Filling", "Filled '" + tpracmarks.Text + "' practical marks of a student with Enrollment No '" + eno.Text + "' of practical code '" + ddlistPracCode.SelectedItem.Text + "' for '" + ddlistExam.SelectedItem.Text + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                    }

                }
                else if (lblmf.Text == "True")
                {

                    if (lpracmarks.Text != tpracmarks.Text)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEPracticalMarks_" + ddlistExam.SelectedItem.Value + " set PracticalMarks=@PracticalMarks where SRID='" + srid.Text + "' and PracticalID='" + ddlistPracCode.SelectedItem.Value + "' and MOE='" + moe.Text + "'", con);

                        cmd.Parameters.AddWithValue("@PracticalMarks", tpracmarks.Text);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Marks Updation", "Updated practical marks from '" + lpracmarks.Text + "' to '" + tpracmarks.Text + "' of a student with Enrollment No '" + eno.Text + "' of practical code '" + ddlistPracCode.SelectedItem.Text + "' for '" + ddlistExam.SelectedItem.Text + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

                    }
                 

                }


            }

        }

        private bool validMarks()
        {

            bool validmarks = false;
            int SNo = 0;
            bool allowedforfullmarks = false;
            int maxpracmarks = FindInfo.findPracMaxMarksByID(Convert.ToInt32(ddlistPracCode.SelectedItem.Value));
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 68))
            {
                allowedforfullmarks = true;
            }
            foreach (DataListItem dli in dtlistAS.Items)
            {
                try
                {
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    TextBox pracmarks = (TextBox)dli.FindControl("tbMO");

                    SNo = Convert.ToInt32(sno.Text);

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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlAS.Visible = true;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAS.Visible = false;
            dtlistAS.Visible = false;
            btnUploadMarks.Visible = false;
            pnlMSG.Visible = false;
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAS.Visible = false;
            dtlistAS.Visible = false;
            btnUploadMarks.Visible = false;
            pnlMSG.Visible = false;
        }

        protected void ddlistPracCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAS.Visible = false;
            dtlistAS.Visible = false;
            btnUploadMarks.Visible = false;
            pnlMSG.Visible = false;
        }
    }
}