using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class RequestForMigration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 120) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 121) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 122))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    if (Request.QueryString["MID"] != null)
                    {
                        populateExistRecord(Convert.ToInt32(Request.QueryString["MID"]));
                        pnlSearch.Visible = true;
                        btnSearch.Visible = false;
                        pnlStudentDetails.Visible = true;

                        if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 121))
                        {
                            btnSubmit.Visible = true;
                            btnSubmit.Text = "Update";
                        }
                        else
                        {
                            btnSubmit.Visible = false;
                            btnSubmit.Text = "";
                        }

                        pnlData.Visible = true;
                        pnlMSG.Visible = false;
                    }
                    else
                    {
                        if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 120))
                        {
                            pnlSearch.Visible = true;
                            pnlStudentDetails.Visible = false;
                            btnSubmit.Visible = false;
                            btnSubmit.Text = "Submit";
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
                  
                   
                }

               
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private void populateExistRecord(int mid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.MotherName,DDEStudentRecord.CYear,DDEMigrationInfo.PassingYear,DDEMigrationInfo.RollNo,DDEMigrationInfo.MailingAddress,DDEMigrationInfo.PinCode,DDEMigrationInfo.MobileNo from DDEMigrationInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEMigrationInfo.SRID where DDEMigrationInfo.MID='" + mid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + dr["SRID"].ToString();
                tbENo.Text = tbEnNo.Text = dr["EnrollmentNo"].ToString();
                ddlistExam.Items.FindByText(dr["PassingYear"].ToString()).Selected = true;
                lblSRID.Text = dr["SRID"].ToString();
                tbSNameE.Text = dr["StudentName"].ToString();
                tbFNameE.Text = dr["FatherName"].ToString();
                tbMNameE.Text = dr["MotherName"].ToString();
                tbCourse.Text = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["CYear"]));
                tbRollNo.Text = dr["RollNo"].ToString();
                tbRollNo.Enabled = true;
                tbAddress.Text = dr["MailingAddress"].ToString();
                tbPin.Text = dr["PinCode"].ToString();
                tbMNo.Text = dr["MobileNo"].ToString();

            }

            con.Close();


        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            int srid = FindInfo.findSRIDByENo(tbENo.Text);
            if (rblMode.SelectedItem.Value == "O")
            {
                if (Accounts.singlefeePaid(srid, 30) || Accounts.singlefeePaid(srid, 55)|| Accounts.singlefeePaid(srid, 56))
                {
                    polulateStudentInfo(srid);

                    if (FindInfo.isMigrationInfoExist(srid,rblMode.SelectedItem.Value))
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("select * from DDEMigrationInfo where SRID='" + srid + "'", con);
                        SqlDataReader dr;

                        con.Open();
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        tbRollNo.Text = dr["RollNo"].ToString();
                        tbRollNo.Enabled = false;
                        ddlistExam.Enabled = false;
                        con.Close();
                        btnSubmit.Visible = false;

                    }
                    else
                    {
                        btnSubmit.Visible = true;
                    }

                    pnlStudentDetails.Visible = true;

                }
                else
                {
                    pnlData.Visible = false;

                    lblMSG.Text = "Sorry !! Migration fee is not paid by the Student";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
            else if (rblMode.SelectedItem.Value == "D")
            {
                if (Accounts.singlefeePaid(srid, 34))
                {
                    polulateStudentInfo(srid);

                    if (FindInfo.isMigrationInfoExist(srid, rblMode.SelectedItem.Value))
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("select * from DDEMigrationInfo where SRID='" + srid + "'", con);
                        SqlDataReader dr;

                        con.Open();
                        dr = cmd.ExecuteReader();
                        dr.Read();
                        tbRollNo.Text = dr["RollNo"].ToString();
                        tbRollNo.Enabled = false;
                        ddlistExam.Enabled = false;
                        con.Close();
                        btnSubmit.Visible = false;

                    }
                    else
                    {
                        btnSubmit.Visible = true;
                    }

                    pnlStudentDetails.Visible = true;

                }
                else
                {
                    pnlData.Visible = false;

                    lblMSG.Text = "Sorry !! Duplicate Migration fee is not paid by the Student";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }

            rblMode.Enabled = false;

        }

        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,MotherName,StudyCentreCode,CYear,CAddress,PinCode,MobileNo,AadhaarNo from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                tbEnNo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = srid.ToString();
                tbSNameE.Text = dr["StudentName"].ToString();
                tbFNameE.Text = dr["FatherName"].ToString();
                tbMNameE.Text = dr["MotherName"].ToString();
                tbRollNo.Text = FindInfo.findRollNoBySRID(srid, ddlistExam.SelectedItem.Value, "R");
                tbCourse.Text = FindInfo.findCourseNameBySRID(srid, Convert.ToInt32(dr["CYear"]));
                if (tbRollNo.Text == "")
                {
                    tbRollNo.Enabled = true;
                }

                tbAddress.Text = dr["CAddress"].ToString();
                tbPin.Text = dr["PinCode"].ToString();
                tbMNo.Text = dr["MobileNo"].ToString();
              
            }

            con.Close();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (btnSubmit.Text == "Submit")
            {
                if (!FindInfo.isMigrationInfoExist(Convert.ToInt32(lblSRID.Text),rblMode.SelectedItem.Value))
                {


                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEMigrationInfo values(@SRID,@PassingYear,@RollNo,@DocumentType,@EntryTime,@LetterPublished,@LetterPublishedOn,@LNo,@MigrationReceived,@SNo,@ReceivedOn,@MigrationPosted,@PostedOn,@PostingMode,@DocketNo,@MailingAddress,@PinCode,@MobileNo)", con);

                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(lblSRID.Text));
                    cmd.Parameters.AddWithValue("@PassingYear", ddlistExam.SelectedItem.Text);                 
                    cmd.Parameters.AddWithValue("@RollNo", tbRollNo.Text); 
                    if(rblMode.SelectedItem.Value=="O")
                    {
                        cmd.Parameters.AddWithValue("@DocumentType", "O");
                    }
                    else if (rblMode.SelectedItem.Value == "D")
                    {
                        cmd.Parameters.AddWithValue("@DocumentType", "D");
                    }
                    cmd.Parameters.AddWithValue("@EntryTime", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@LetterPublished", "False");                 
                    cmd.Parameters.AddWithValue("@LetterPublishedOn", "");
                    cmd.Parameters.AddWithValue("@LNo", 0);
                    cmd.Parameters.AddWithValue("@MigrationReceived", "False");
                    cmd.Parameters.AddWithValue("@SNo", 0);
                    cmd.Parameters.AddWithValue("@ReceivedOn", "");
                    cmd.Parameters.AddWithValue("@MigrationPosted", "False");
                    cmd.Parameters.AddWithValue("@PostedOn", "");
                    cmd.Parameters.AddWithValue("@PostingMode", "");
                    cmd.Parameters.AddWithValue("@DocketNo", "");
                    cmd.Parameters.AddWithValue("@MailingAddress", tbAddress.Text);
                    cmd.Parameters.AddWithValue("@PinCode", tbPin.Text);
                    cmd.Parameters.AddWithValue("@MobileNo", tbMNo.Text);
                  
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Migration Info", "Requested For Migration for Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                    pnlData.Visible = false;
                    lblMSG.Text = "Record has been inserted successfully";
                    pnlMSG.Visible = true;

                }
                else
                {
                    pnlData.Visible = false;

                    lblMSG.Text = "Sorry !! Record is already exist for this student.";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEMigrationInfo set PassingYear=@PassingYear,SNameH=@SNameH,FNameH=@FNameH,MNameH=@MNameH,RollNo=@RollNo,FinalDiv=@FinalDiv,MailingAddress=@MailingAddress,PinCode=@PinCode,MobileNo=@MobileNo,DegreeType=@DegreeType where MID='" + Request.QueryString["MID"] + "'", con);

                cmd.Parameters.AddWithValue("@PassingYear", ddlistExam.SelectedItem.Text);              
                cmd.Parameters.AddWithValue("@RollNo", tbRollNo.Text);              
                cmd.Parameters.AddWithValue("@MailingAddress", tbAddress.Text);
                cmd.Parameters.AddWithValue("@PinCode", tbPin.Text);
                cmd.Parameters.AddWithValue("@MobileNo", tbMNo.Text);
               
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Migration Info", "Updated Migration Record for Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
            }

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {

            tbRollNo.Text = FindInfo.findRollNoBySRID(FindInfo.findSRIDByENo(tbENo.Text), ddlistExam.SelectedItem.Value, "R");
        }
    }
}