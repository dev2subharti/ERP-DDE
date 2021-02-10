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
using System.Globalization;

namespace DDE.Web.Admin
{
    public partial class ShowRefundSCWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateStudyCentre(ddlistSC);
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            
           populateRefunds();
           setColor();
        }

        private void setColor()
        {
            foreach (DataListItem dli in dtlistDirectSC.Items)
            {
                Label per = (Label)dli.FindControl("lblFeePer");
                Label refund = (Label)dli.FindControl("lblRefund");
                CheckBox cb = (CheckBox)dli.FindControl("cbRefund");
                char [] ch={'%'};
                string str = per.Text.Trim(ch);
                if (Convert.ToDecimal(str)==100)
                {
                    per.BackColor = System.Drawing.Color.GreenYellow;
                   
                }
                else if (Convert.ToDecimal(str) >= 60 && Convert.ToDecimal(str) < 100)
                {
                    per.BackColor = System.Drawing.Color.Orange;
                }
                else if (Convert.ToDecimal(str) >= 0 && Convert.ToDecimal(str) < 60)
                {
                    per.BackColor = System.Drawing.Color.Red;
                    per.ForeColor = System.Drawing.Color.White;
                }
               
            }
        }

       

        private void populateRefunds()
        {
            string srids = FindInfo.findOandCSRIDSBySCCode(ddlistSC.SelectedItem.Value);
        
            string exam = "";
            if (ddlistBatch.SelectedItem.Text == "C 2014")
            {
                exam = "B14";
            }
            else if (ddlistBatch.SelectedItem.Text == "A 2014-15")
            {
                exam = "A15";
            }
            else if (ddlistBatch.SelectedItem.Text == "C 2015")
            {
                exam = "B15";
            }
            
            string esrids = FindInfo.filterCourseFeePaidBySRIDS(srids,exam);
            if (esrids != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand("select SRID,EnrollmentNo,StudentName,FatherName,Course,Course2Year,Course3Year,CYear,Session from DDEStudentRecord where SRID in (" + esrids + ")", con);


                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcolsrid = new DataColumn("SRID");
                DataColumn dtcolen = new DataColumn("EnrollmentNo");
                DataColumn dtcol2 = new DataColumn("SName");
                DataColumn dtcol3 = new DataColumn("FName");
                DataColumn dtcol4 = new DataColumn("Course");
                DataColumn dtcol5 = new DataColumn("CYear");
                DataColumn dtcol6 = new DataColumn("FPYear");
                DataColumn dtcol7 = new DataColumn("RFee");
                DataColumn dtcol8 = new DataColumn("PFee");
                DataColumn dtcol9 = new DataColumn("FeePer");
                DataColumn dtcol10 = new DataColumn("InsDetails");
                DataColumn dtcol11 = new DataColumn("Refund");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcolsrid);
                dt.Columns.Add(dtcolen);
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

                decimal totalrefund = 0;
                int i = 1;
                while (dr.Read())
                {
                    int pfee;
                    string insdetails;

                    string fpyear = FindInfo.findAllCourseYear(Convert.ToInt32(dr["SRID"]),exam);

                    if (fpyear != "" || fpyear != "0")
                    {

                        if (fpyear.Length > 1)
                        {
                            string[] str = fpyear.Split(',');
                            for (int j = 0; j < str.Length; j++)
                            {
                                if (!(Accounts.isRefundedBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(str[j]))))
                                {
                                    DataRow drow = dt.NewRow();
                                    drow["SNo"] = i;
                                    drow["SRID"] = Convert.ToInt32(dr["SRID"]);
                                    drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                                    drow["SName"] = Convert.ToString(dr["StudentName"]);
                                    drow["FName"] = Convert.ToString(dr["FatherName"]);
                                    drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["CYear"]));
                                    drow["CYear"] = dr["CYear"].ToString();
                                    drow["FPYear"] = Convert.ToInt32(str[j]);
                                    drow["RFee"] = Accounts.findRequiredFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 1, 0, dr["Session"].ToString(), "");
                                    findFeeDetails(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(str[j]), exam, out pfee, out insdetails);
                                    drow["PFee"] = pfee;
                                    drow["FeePer"] = findFeePercent(Convert.ToInt32(drow["RFee"]), pfee);
                                    drow["InsDetails"] = insdetails;
                                    drow["Refund"] = findRefund(Convert.ToInt32(drow["RFee"]), pfee);
                                    totalrefund = totalrefund + Convert.ToDecimal(drow["Refund"]);
                                    dt.Rows.Add(drow);
                                    i = i + 1;
                                }

                            }
                        }
                        else
                        {
                            if (!(Accounts.isRefundedBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(fpyear))))
                            {
                                DataRow drow = dt.NewRow();
                                drow["SNo"] = i;
                                drow["SRID"] = Convert.ToInt32(dr["SRID"]);
                                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                                drow["SName"] = Convert.ToString(dr["StudentName"]);
                                drow["FName"] = Convert.ToString(dr["FatherName"]);
                                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["CYear"]));
                                drow["CYear"] = dr["CYear"].ToString();
                                drow["FPYear"] = fpyear;
                                drow["RFee"] = Accounts.findRequiredFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 1, 0, dr["Session"].ToString(), "");
                                findFeeDetails(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(fpyear), exam, out pfee, out insdetails);
                                drow["PFee"] = pfee;
                                drow["FeePer"] = findFeePercent(Convert.ToInt32(drow["RFee"]), pfee);
                                drow["InsDetails"] = insdetails;
                                drow["Refund"] = findRefund(Convert.ToInt32(drow["RFee"]), pfee);
                                totalrefund = totalrefund + Convert.ToDecimal(drow["Refund"]);
                                dt.Rows.Add(drow);
                                i = i + 1;
                            }

                        }
                    }
                   
                }




                con.Close();

                if (i > 1)
                {

                    pnlRefundList.Visible = true;
                    pnlMSG.Visible = false;

                }

                else
                {
                    pnlRefundList.Visible = false;
                    lblMSG.Text = "Sorry !! No record found";
                    pnlMSG.Visible = true;
                }

                dtlistDirectSC.DataSource = dt;
                dtlistDirectSC.DataBind();

                ViewState["TotalAmount"] = totalrefund;

                CultureInfo cultureInfo = new CultureInfo("hi-IN");
                lblTotalRefund.Text = "Total Refund : " + Convert.ToDecimal(totalrefund).ToString("c", cultureInfo);

                if (totalrefund > 0)
                {
                    btnPublish.Visible = true;
                }
            }
            else
            {
                pnlRefundList.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
            

        }

        private void findFeeDetails(int srid,int year, string exam, out int pfee, out string insdetails)
        {
            pfee = 0;
            insdetails = "";

          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from [DDEFeeRecord_2009-10] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2010-11] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2011-12] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2012-13] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2014-15] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2015-16] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2016-17] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2017-18] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2018-19] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2019-20] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "' union select * from [DDEFeeRecord_2020-21] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' and ForExam='" + exam + "'", con);

            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pfee = pfee + Convert.ToInt32(dr["Amount"]);
                    if (insdetails == "")
                    {
                        insdetails = Convert.ToInt32(dr["Amount"]) + " - " + FindInfo.findPaymentModeByID(Convert.ToInt32(dr["PaymentMode"])) + " - " + dr["DCNumber"].ToString() + " - " + Convert.ToDateTime(dr["DCDate"]).ToString("dd/MM/yyyy") + " - " + dr["IBN"].ToString() + " - " + Convert.ToInt32(dr["TotalDCAmount"]);
                    }
                    else
                    {
                        insdetails = insdetails + "<br/>" + Convert.ToInt32(dr["Amount"]) + " - " + FindInfo.findPaymentModeByID(Convert.ToInt32(dr["PaymentMode"])) + " - " + dr["DCNumber"].ToString() + " - " + Convert.ToDateTime(dr["DCDate"]).ToString("dd/MM/yyyy") + " - " + dr["IBN"].ToString() + " - " + Convert.ToInt32(dr["TotalDCAmount"]);
                    }
                    
                }
            }

            con.Close();


        }

        private float findRefund(float rfee, float pfee)
        {
            if (pfee != 0)
            {
                float feeper = ((pfee/rfee) * 100);
                if (feeper > 60)
                {
                    return (((feeper - 60) * rfee) / 100);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private string findFeePercent(float rfee, float pfee)
        {
            if (pfee != 0)
            {
                return ((pfee/rfee) * 100).ToString() + "%";
            }
            else
            {
                return "0%";
            }
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (ddlistBatch.SelectedItem.Text == "C 2014")
            {
                Session["Exam"] = "B14";
                Session["Batch"] = "C 2014";
            }
            else if (ddlistBatch.SelectedItem.Text == "A 2014-15")
            {
                Session["Exam"] = "A15";
                Session["Batch"] = "A 2014-15";
            }
           else if (ddlistBatch.SelectedItem.Text == "C 2015")
            {
                Session["Exam"] = "B15";
                Session["Batch"] = "C 2015";
            }
            
            Session["SCCode"] = ddlistSC.SelectedItem.Text;
            Session["SRIDS"] = calculateSRIDS();
            if (tbDRAmount.Text != "")
            {
                Session["DR"] = "Yes";
                Session["DRAmount"] = tbDRAmount.Text;
            }
            else
            {
                Session["DR"] = "No";
                Session["DRAmount"] = 0;
            }
            Response.Redirect("PublishRefundLetter.aspx");
        }

        private string calculateSRIDS()
        {
            string srids = "";
            foreach (DataListItem dli in dtlistDirectSC.Items)
            {
                Label srid = (Label)dli.FindControl("lblSRID");
                CheckBox cb = (CheckBox)dli.FindControl("cbRefund");
                if (cb.Checked)
                {
                    if (srids == "")
                    {
                        srids = srid.Text;
                    }
                    else
                    {
                        srids =srids+","+ srid.Text;
                    }
                }
               
            }
            return srids;
        }

        protected void lnkDRefund_Click(object sender, EventArgs e)
        {
            if (lnkDRefund.Text == "Generate DFR Instrument")
            {
                pnlUP.Visible = true;
                lnkDRefund.Visible = false;
                btnPublish.Visible = false;
                             
            }
            else if (lnkDRefund.Text == "Confirm")
            {            
                generateDFRInstrument();
                lnkDRefund.Enabled = false;
                tbDRAmount.Visible = false;
                CultureInfo cultureInfo = new CultureInfo("hi-IN");
                lblDRAmount.Text = "Deducted Amount : " + Convert.ToDecimal(tbDRAmount.Text).ToString("c", cultureInfo);
                lblFinalAmount.Text = "Final Refund : " + Convert.ToDecimal(Convert.ToInt32(ViewState["TotalAmount"]) - Convert.ToInt32(tbDRAmount.Text)).ToString("c", cultureInfo);
                lblFinalAmount.Visible = true;
                btnPublish.Visible = true;
                      
            }
        }

        private bool validUser(int p)
        {
            throw new NotImplementedException();
        }

        private void generateDFRInstrument()
        {
            string ino = FindInfo.findDFRNo();
            Session["DRInsNo"] = ino;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDEFeeInstruments values(@IType,@INo,@IDate,@IBN,@TotalAmount,@SCMode,@SCCode,@Received,@ReceivedOn,@ReceivedBy,@Verified,@VerifiedOn,@VerifiedBy,@AmountAlloted,@AllotedOn,@AllotedBy,@AllotedFeeHeads,@Remark,@Balance,@FH1,@FH2,@FH3,@FH4,@FH5,@FH5,@FH7,@FH8,@FH9,@FH10,@FH11,@FH12,@FH13,@FH14,@FH15,@FH16,@FH17,@FH18,@FH19,@FH20,@FH21,@FH22,@FH23,@FH24,@FH25,@FH26,@FH27,@FH28,@FH29,@FH30,@FH31,@FH32,@FH33,@FH34,@FH35,@FH36,@FH37)", con);

            cmd.Parameters.AddWithValue("@IType", 5);
            cmd.Parameters.AddWithValue("@INo", ino);
            cmd.Parameters.AddWithValue("@IDate", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@IBN","NA");
            cmd.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(tbDRAmount.Text));        
            cmd.Parameters.AddWithValue("@SCMode", "True");           
            cmd.Parameters.AddWithValue("@SCCode", ddlistSC.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Received", "True");
            cmd.Parameters.AddWithValue("@ReceivedOn", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@ReceivedBy", Convert.ToInt32(Session["ERID"]));
            cmd.Parameters.AddWithValue("@Verified", "False");
            cmd.Parameters.AddWithValue("@VerifiedOn", "");
            cmd.Parameters.AddWithValue("@VerifiedBy", 0);
            cmd.Parameters.AddWithValue("@AmountAlloted", "False");
            cmd.Parameters.AddWithValue("@AllotedOn", "");
            cmd.Parameters.AddWithValue("@AllotedBy", 0);
            cmd.Parameters.AddWithValue("@AllotedFeeHeads", FindInfo.findAllFeeHeadID());
            cmd.Parameters.AddWithValue("@Remark", 5);
            cmd.Parameters.AddWithValue("@Balance", 0);
       


            for (int i = 1; i <= 33; i++)
            {

                cmd.Parameters.AddWithValue("@FH" + i.ToString(), 0);

            }

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

           

            FindInfo.updateDRCounter(Convert.ToInt32(ino.Substring(3, (ino.Length - 3))));
           

            Log.createLogNow("Create", "Created a Fee Instrument 'Deduct from Refund' with No. '" + ino + "'", Convert.ToInt32(Session["ERID"]));

            lnkDRefund.Text = "Instrument has been created with No. : "+ino;
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());

            SqlCommand cmd = new SqlCommand("select ERID,EmployeePassword from SVSUEmployeeRecord where EmployeeID='" + tbUserName.Text + "'", con);

            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();


                Security s1 = new Security();
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (dr[1].ToString() == s1.EncryptOneWay(tbPassword.Text))
                {
                    if (Authorisation.authorised(Convert.ToInt32(dr["ERID"]), 76))
                    {
                        pnlUP.Visible = false;
                        lnkDRefund.Text = "Confirm";
                        lnkDRefund.Visible = true;
                        lblDRAmount.Visible = true;
                        tbDRAmount.Visible = true;
                    }
                    else
                    {
                        lblError.Text = "Sorry !! You are not authorised for this role.";
                        lblError.Visible = true;

                    }
                }
                else
                {
                    lblError.Text = "Please enter a currect password";
                    lblError.Visible = true;

                }
            }
            else
            {
                lblError.Text = "Please enter a currect user name";
                lblError.Visible = true;

            }

            con.Close();     
           
        }
    }
}