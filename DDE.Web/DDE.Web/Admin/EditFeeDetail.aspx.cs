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
    public partial class EditFeeDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 56))
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["FRID"] != null)
                    {
                        int srid =FindInfo.findSRIDByFRID(Convert.ToInt32(Request.QueryString["FRID"]),Session["ASession"].ToString());
                        populateStudentInfo(srid);
                        PopulateDDList.populateAccountSession(ddlistAcountsSession);
                        PopulateDDList.populateSTFeeHead(ddlistFeeHead);
                        PopulateDDList.populateExam(ddlistExamination);
                        ddlistExamination.Items.Add("NA");
                        
                        populateFeeDetails(Convert.ToInt32(Request.QueryString["FRID"]));

                       

                        if (ddlistFeeHead.SelectedItem.Value == "1")
                        {
                            ddlistExamination.Enabled = true;
                            tbAmount.Enabled = false;
                        }
                        else
                        {
                            ddlistExamination.Enabled = false;
                            tbAmount.Enabled = true;
                        }

                      

                        pnlData.Visible = true;
                        pnlMSG.Visible = false;

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! You can not open this page directly";
                        pnlMSG.Visible = true;
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

        

        private void populateStudentInfo(int srid)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select ApplicationNo,EnrollmentNo,StudentName,FatherName,StudyCentreCode,Course,Course2Year,Course3Year,CYear from DDEStudentRecord where SRID='" + srid + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();                
                    tbEnNo.Text = dr["EnrollmentNo"].ToString();
                    lblSRID.Text = srid.ToString();
                    tbSName.Text = dr["StudentName"].ToString();
                    tbFName.Text = dr["FatherName"].ToString();
                    tbSCCode.Text = dr["StudyCentreCode"].ToString();

                    string course = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));

                    if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                    {
                        if (Convert.ToInt32(dr["CYear"]) == 1)
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                            lblCID.Text = dr["Course"].ToString();
                        }
                        else if (Convert.ToInt32(dr["CYear"]) == 2)
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course2Year"]));
                            lblCID.Text = dr["Course2Year"].ToString();
                        }
                        else if (Convert.ToInt32(dr["CYear"]) == 3)
                        {
                            tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course3Year"]));
                            lblCID.Text = dr["Course3Year"].ToString();
                        }
                    }
                    else
                    {
                        tbCourse.Text = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                        lblCID.Text = dr["Course"].ToString();
                    }

                }

                con.Close();

                setYearList(Convert.ToInt32(lblCID.Text));
            }
            catch (FormatException fe)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Specialisation record of this student is not set. </br> Please set specialsation record of this student first and then try again";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["ErrorType"] = "Specialisation Not Set";
            }
            catch (InvalidCastException invc)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! Specialisation record of this student is not set. </br> Please set specialsation record of this student first and then try again";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                ViewState["ErrorType"] = "Specialisation Not Set";
            }
        }

        private void setYearList(int cid)
        {
            int duration = FindInfo.findCourseDuration(cid);
            ddlistYear.Items.Clear();

            if (duration == 1)
            {
                

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("NA");
                ddlistYear.Items.FindByText("NA").Value = "0";

            }
            else if (duration == 2)
            {
               

                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("NA");
                ddlistYear.Items.FindByText("NA").Value = "0";
            }
            else if (duration == 3)
            {
               
                ddlistYear.Items.Add("1ST YEAR");
                ddlistYear.Items.FindByText("1ST YEAR").Value = "1";

                ddlistYear.Items.Add("2ND YEAR");
                ddlistYear.Items.FindByText("2ND YEAR").Value = "2";

                ddlistYear.Items.Add("3RD YEAR");
                ddlistYear.Items.FindByText("3RD YEAR").Value = "3";

                ddlistYear.Items.Add("NA");
                ddlistYear.Items.FindByText("NA").Value = "0";
            }
        }

        private void populateFeeDetails(int frid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from [DDEFeeRecord_"+Session["ASession"]+"] where FRID='" + frid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistAcountsSession.Items.FindByText(Session["ASession"].ToString()).Selected = true;
                ddlistFeeHead.Items.FindByValue(dr["FeeHead"].ToString()).Selected = true;
                tbAmount.Text = Convert.ToInt32(dr["Amount"]).ToString();
                lblAmount.Text = Convert.ToInt32(dr["Amount"]).ToString();
                ddlistYear.Items.FindByValue(dr["ForYear"].ToString()).Selected = true;
                lblPYear.Text = dr["ForYear"].ToString();
                ddlistExamination.Items.FindByValue(dr["ForExam"].ToString()).Selected = true;
            }

            con.Close();
        }

        

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblAmount.Text != tbAmount.Text)
                {
                    string error="";
                    if (validAmountEditing(Convert.ToInt32(tbAmount.Text),Convert.ToInt32(lblAmount.Text), Convert.ToInt32(Request.QueryString["FRID"]), Session["ASession"].ToString(),out error))
                    {

                        if (ddlistFeeHead.SelectedItem.Value == "1")
                        {

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("update [DDEFeeRecord_" + Session["ASession"] + "] set Amount=@Amount,AmountInWords=@AmountInWords,ForYear=@ForYear,ForExam=@ForExam where FRID='" + Request.QueryString["FRID"] + "' ", con);

                            cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(tbAmount.Text));
                            cmd.Parameters.AddWithValue("@AmountInWords", Accounts.IntegerToWords(Convert.ToInt32(tbAmount.Text)).ToUpper());
                            cmd.Parameters.AddWithValue("@ForYear", ddlistYear.SelectedItem.Value);
                            cmd.Parameters.AddWithValue("@ForExam", ddlistExamination.SelectedItem.Value);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand("update [DDEFeeRecord_" + Session["ASession"] + "] set Amount=@Amount,AmountInWords=@AmountInWords,ForYear=@ForYear where FRID='" + Request.QueryString["FRID"] + "' ", con);



                            cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(tbAmount.Text));
                            cmd.Parameters.AddWithValue("@AmountInWords", Accounts.IntegerToWords(Convert.ToInt32(tbAmount.Text)).ToUpper());
                            cmd.Parameters.AddWithValue("@ForYear", ddlistYear.SelectedItem.Value);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }



                        if (ddlistFeeHead.SelectedItem.Value == "2")
                        {
                            if (lblPYear.Text != ddlistYear.SelectedItem.Value)
                            {
                                updateExamYear(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value, Convert.ToInt32(lblPYear.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value));
                            }
                        }

                        Log.createLogNow("Update", "Updated " + ddlistFeeHead.SelectedItem.Text + " for " + ddlistYear.SelectedItem.Text + " and for " + ddlistExamination.SelectedItem.Text + " Exam of '" + tbEnNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                        pnlData.Visible = false;
                        lblMSG.Text = "Record has been updated successfully";
                        pnlMSG.Visible = true;
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = error;
                        pnlMSG.Visible = true;
                    }
                }
                else
                {
                    if (ddlistFeeHead.SelectedItem.Value == "1")
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update [DDEFeeRecord_" + Session["ASession"] + "] set ForYear=@ForYear,ForExam=@ForExam where FRID='" + Request.QueryString["FRID"] + "' ", con);



                        cmd.Parameters.AddWithValue("@ForYear", ddlistYear.SelectedItem.Value);
                        cmd.Parameters.AddWithValue("@ForExam", ddlistExamination.SelectedItem.Value);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update [DDEFeeRecord_" + Session["ASession"] + "] set ForYear=@ForYear where FRID='" + Request.QueryString["FRID"] + "' ", con);



                      
                        cmd.Parameters.AddWithValue("@ForYear", ddlistYear.SelectedItem.Value);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }



                    if (ddlistFeeHead.SelectedItem.Value == "2")
                    {
                        if (lblPYear.Text != ddlistYear.SelectedItem.Value)
                        {
                            updateExamYear(Convert.ToInt32(lblSRID.Text), ddlistExamination.SelectedItem.Value, Convert.ToInt32(lblPYear.Text), Convert.ToInt32(ddlistYear.SelectedItem.Value));
                        }
                    }

                    Log.createLogNow("Update", "Updated " + ddlistFeeHead.SelectedItem.Text + " for " + ddlistYear.SelectedItem.Text + " and for " + ddlistExamination.SelectedItem.Text + " Exam of '" + tbEnNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                    pnlData.Visible = false;
                    lblMSG.Text = "Record has been updated successfully";
                    pnlMSG.Visible = true;
                }

               
            }
            catch (FormatException fe)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not entered amount in numeric form";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
           
        }

        private bool validAmountEditing(int updatedamount,int preamount, int frid, string asession, out string error)
        {
            bool valid = false;
            error = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from [DDEFeeRecord_"+asession+"] where FRID='" + frid + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string idate =Convert.ToDateTime(ds.Tables[0].Rows[0]["DCDate"]).ToString("yyyy-MM-dd");
            int usedamount = (Accounts.findUsedAmountOfDraft(Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentMode"]), ds.Tables[0].Rows[0]["DCNumber"].ToString(), idate, ds.Tables[0].Rows[0]["IBN"].ToString())) - (preamount);

            if ((usedamount + updatedamount) <= Accounts.findTotalDCAmount(asession, Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentMode"]), ds.Tables[0].Rows[0]["DCNumber"].ToString(), idate, ds.Tables[0].Rows[0]["IBN"].ToString()))
            {
                int totalfeeheadamount=Accounts.findTotalAmountOnFeeHead(Convert.ToInt32(ds.Tables[0].Rows[0]["FeeHead"]),Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentMode"]), ds.Tables[0].Rows[0]["DCNumber"].ToString(), idate, ds.Tables[0].Rows[0]["IBN"].ToString());
                int usedamountonfeehead=(Accounts.findUsedAmountOnFeeHead(Convert.ToInt32(ds.Tables[0].Rows[0]["FeeHead"]), Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentMode"]), ds.Tables[0].Rows[0]["DCNumber"].ToString(), idate, ds.Tables[0].Rows[0]["IBN"].ToString()))-(preamount);

                if (totalfeeheadamount != 0)
                {
                    if ((usedamountonfeehead + updatedamount) <= totalfeeheadamount)
                    {
                        valid = true;
                    }
                    else
                    {
                        error = "Sorry!! The updated amount is exceeding the total amount of allotted on this fee head";
                    }
                }
                else
                {
                    valid = true;
                }
                
            }
            else
            {
                error = "Sorry!! The updated amount is exceeding the total amount of Instrument";
            }
          

            return valid;
           
        }
       
        void updateExamYear(int srid, string exam, int pyear, int cyear)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update [DDEExamRecord_" + exam + "] set Year=@Year where SRID='" + lblSRID.Text + "'and Year='"+pyear+"' and MOE='R' ", con);
        
            cmd.Parameters.AddWithValue("@Year", cyear);

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
