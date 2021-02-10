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
    public partial class PublishRefundLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
            {
                if (!IsPostBack)
                {
                   
                    lblDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
                    lblSCCode.Text = Session["SCCode"].ToString();
                    lblSCName.Text = FindInfo.findSCNameByCode(Session["SCCode"].ToString());

                    populateRefunds();
                   

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

       

        private void setColor()
        {
            foreach (DataListItem dli in dtlistDirectSC.Items)
            {
                Label per = (Label)dli.FindControl("lblFeePer");
                Label refund = (Label)dli.FindControl("lblRefund");
                CheckBox cb = (CheckBox)dli.FindControl("cbRefund");
                char[] ch = { '%' };
                string str = per.Text.Trim(ch);
                if (Convert.ToDecimal(str) == 100)
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
                if (Convert.ToDecimal(refund.Text) > 0)
                {
                    cb.Checked = true;
                    cb.Enabled = true;
                }
                else
                {
                    cb.Checked = false;
                    cb.Enabled = false;
                }
            }
        }



        private void populateRefunds()
        {
        
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("select SRID,EnrollmentNo,StudentName,FatherName,Course,Course2Year,Course3Year,CYear,Session from DDEStudentRecord where SRID in (" + Session["SRIDS"].ToString() + ")", con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn sr = new DataColumn("SRID");
            DataColumn dtcolen = new DataColumn("EnrollmentNo");
            DataColumn dtcol2 = new DataColumn("SName");
            DataColumn dtcol3 = new DataColumn("FName");
            DataColumn dtcol4 = new DataColumn("Course");
            DataColumn dtcol5 = new DataColumn("CYear");
            DataColumn dtcol6 = new DataColumn("FPYear");
            DataColumn dtcol7 = new DataColumn("RFee");
            DataColumn dtcol8 = new DataColumn("PFee");
            DataColumn dtcol9 = new DataColumn("FeePer");
            DataColumn dtcol10 = new DataColumn("Refund");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(sr);
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

            decimal totalrefund = 0;
            int i = 1;
            while (dr.Read())
            {
                int pfee;
                string insdetails;

                string fpyear = FindInfo.findAllCourseYear(Convert.ToInt32(dr["SRID"]), Session["Exam"].ToString());

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
                                findFeeDetails(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(str[j]), Session["Exam"].ToString(), out pfee, out insdetails);
                                drow["PFee"] = pfee;
                                drow["FeePer"] = findFeePercent(Convert.ToInt32(drow["RFee"]), pfee);
                               
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
                            findFeeDetails(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(fpyear), Session["Exam"].ToString(), out pfee, out insdetails);
                            drow["PFee"] = pfee;
                            drow["FeePer"] = findFeePercent(Convert.ToInt32(drow["RFee"]), pfee);
                           
                            drow["Refund"] = findRefund(Convert.ToInt32(drow["RFee"]), pfee);
                            totalrefund = totalrefund + Convert.ToDecimal(drow["Refund"]);
                            dt.Rows.Add(drow);
                            i = i + 1;
                        }

                    }
                }
               
                    
            }


            if (i > 1)
            {

                dtlistDirectSC.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                dtlistDirectSC.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

            dtlistDirectSC.DataSource = dt;
            dtlistDirectSC.DataBind();

            con.Close();

            CultureInfo cultureInfo = new CultureInfo("hi-IN");
            //var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
            //numberFormatInfo.CurrencySymbol = "";

            lblTotalRefund.Text = "Total Refund : " + Convert.ToDecimal(totalrefund).ToString("c", cultureInfo);
            ViewState["TR"] = totalrefund;

            ViewState["DR"] = Session["DRAmount"];
            lblDRAmount.Text = "Amount Deducted : " + Convert.ToDecimal(Session["DRAmount"]).ToString("c", cultureInfo);
            ViewState["FR"] = Convert.ToInt32(totalrefund) - Convert.ToInt32(Session["DRAmount"]);
            lblFinalAmount.Text = "Final Refund : " + (Convert.ToDecimal(totalrefund) - Convert.ToDecimal(Session["DRAmount"])).ToString("c", cultureInfo);
            lblDRAmount.Visible = true;
            lblFinalAmount.Visible = true;
           
            
           


        }

        private void findFeeDetails(int srid, int year, string exam, out int pfee, out string insdetails)
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
                float feeper = ((pfee / rfee) * 100);
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
                return ((pfee / rfee) * 100).ToString() + "%";
            }
            else
            {
                return "0%";
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand command = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            DataSet dataset = new DataSet();


            command.Connection = connection;
            command.CommandText = "SELECT * FROM DDERefundRecord";
            adapter.SelectCommand = command;
            adapter.Fill(dataset, "RefundRecord");

            foreach (DataListItem dli in dtlistDirectSC.Items)
            {
                Label srid = (Label)dli.FindControl("lblSRID");
                Label fpyear = (Label)dli.FindControl("lblFPYear");
                Label rfee = (Label)dli.FindControl("lblRFee");
                Label pfee = (Label)dli.FindControl("lblPFee");
                Label refund = (Label)dli.FindControl("lblRefund");             

                DataRow row = dataset.Tables["RefundRecord"].NewRow();
                row["SRID"] = srid.Text;
                row["Year"] = fpyear.Text;
                row["SCCode"] = lblSCCode.Text;
                row["RAmount"] = Convert.ToInt32(rfee.Text);
                row["PAmount"] = Convert.ToInt32(pfee.Text);
                row["Refund"] = Convert.ToInt32(refund.Text);
                row["LNo"] = Convert.ToInt32(lblLNo.Text.Substring(18,(lblLNo.Text.Length-18)));
                row["TotalRefund"] = Convert.ToInt32(ViewState["TR"]);
                if (Session["DR"].ToString() == "Yes")
                {
                    row["RefundDeducted"] = Convert.ToInt32(ViewState["DR"]);
                    row["DRInsNo"] = Session["DRInsNo"].ToString();
                    row["FinalRefund"] = Convert.ToInt32(ViewState["FR"]);
                }
                else
                {
                    row["RefundDeducted"] = 0;
                    row["DRInsNo"] = "NA";
                    row["FinalRefund"] = Convert.ToInt32(ViewState["TR"]);
                }
                row["RG"] = "True";
                row["RGBy"] = Convert.ToInt32(Session["ERID"]);
                row["TORG"] = DateTime.Now.ToString();
                row["RP"] = DBNull.Value;
                row["PIID"] = DBNull.Value;
                row["RPBy"] = DBNull.Value;
                row["TORP"] = DBNull.Value;
                

                dataset.Tables["RefundRecord"].Rows.Add(row);              
               
            }

            try
            {
                int result = adapter.Update(dataset, "RefundRecord");
                int counter = FindInfo.findRefundLNoCounter();
                lblLNo.Text = "DDE/Accounts/"+Session["Batch"]+"/" + counter;
                lblLNo.Visible = true;
                btnPrint.Visible = false;
                updateRLNoCounter(counter);

                ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
            }
            catch (SqlException ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }

        

        }

        private void updateRLNoCounter(int counter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDECounters set CounterValue=@CounterValue where CounterName='RefundLNoCounter' ", con);
            cmd.Parameters.AddWithValue("@CounterValue", counter);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}