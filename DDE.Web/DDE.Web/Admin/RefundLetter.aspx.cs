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
    public partial class RefundLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
            {
                if (!IsPostBack)
                {
                    if(Request.QueryString["LNo"]!=null)
                    {
                        populateLetterDetails(Convert.ToInt32(Request.QueryString["LNo"]));
                        populateTransactions(Convert.ToInt32(Request.QueryString["LNo"]));

                    }
                    
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

        private void populateLetterDetails(int lno)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDERefundLetterRecord where RLID='" + lno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                lblLNo.Text = "Ref No. : DDE/Accounts/" + Convert.ToDateTime(dr["RGTime"]).ToString("yyyy") + "/" + lno.ToString();
                lblDate.Text = "Date : " + Convert.ToDateTime(dr["RGTime"]).ToString("dd/MM/yyyy");
                populateInstrumentDetails(Convert.ToInt32(dr["IID"]));
              
                tbBatch.Text = dr["Batch"].ToString();
                tbSCCode.Text = dr["SCCode"].ToString();
                tbCourse.Text = dr["Course"].ToString();

                tbTotalRefund.Text =Convert.ToInt32(dr["TotalRefund"]).ToString() ;
                tbBalanceExtra.Text = Convert.ToInt32(dr["Extra"]).ToString();
                tbBalanceShort.Text = Convert.ToInt32(dr["Short"]).ToString();
                tbNetRefund.Text = Convert.ToInt32(dr["NetRefund"]).ToString();
            }

            con.Close();
        }

        private void populateInstrumentDetails(int iid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where IID='" + iid + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                lblIID.Text = dr["IID"].ToString();
                tbINo.Text = dr["INo"].ToString();
                tbIType.Text =FindInfo.findPaymentModeByID(Convert.ToInt32(dr["IType"]));
                tbIDate.Text =Convert.ToDateTime(dr["IDate"]).ToString("dd MMMM yyyy");
                tbIBN.Text = dr["IBN"].ToString();
                tbTotalAmount.Text = dr["TotalAmount"].ToString();
            }

            con.Close();
          
        }

        private void populateTransactions(int lno)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");     
            DataColumn dtcol8 = new DataColumn("Course");     
            DataColumn dtcol10 = new DataColumn("SCCode");
            DataColumn dtcol11 = new DataColumn("Year");
            DataColumn dtcol12 = new DataColumn("ReqFee");
            DataColumn dtcol13 = new DataColumn("Trans");
            DataColumn dtcol14 = new DataColumn("PaidFee");
            DataColumn dtcol15 = new DataColumn("FeePer");
            DataColumn dtcol16 = new DataColumn("Refund");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
         
            dt.Columns.Add(dtcol8);
          
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);
            dt.Columns.Add(dtcol12);
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);
            dt.Columns.Add(dtcol15);
            dt.Columns.Add(dtcol16);

            int i = 1;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("SELECT DDERefundRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDECourse.CourseName,DDERefundRecord.Year,DDERefundRecord.SCCode,DDERefundRecord.RAmount,DDERefundRecord.Trans,DDERefundRecord.PAmount,DDERefundRecord.FeePer,DDERefundRecord.Refund,DDERefundRecord.RLID,DDERefundRecord.RG,DDERefundRecord.RP FROM DDERefundRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDERefundRecord.SRID inner join DDECourse on DDECourse.CourseID=DDERefundRecord.Course where DDERefundRecord.RLID='" + lno + "' order by DDEStudentRecord.EnrollmentNo", con);
            SqlDataReader dr;

             
            con.Open();

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["SRID"] = dr["SRID"].ToString();
                    drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                    drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                    drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                    drow["Course"] = Convert.ToString(dr["CourseName"]);
                    drow["Year"] = Convert.ToString(dr["Year"]);
                    drow["ReqFee"] = Convert.ToString(dr["RAmount"]);
                    drow["Trans"] = Convert.ToString(dr["Trans"]);
                    drow["PaidFee"] = Convert.ToString(dr["PAmount"]);
                    drow["FeePer"] = Convert.ToString(dr["FeePer"]);
                    drow["Refund"] = Convert.ToString(dr["Refund"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }

            con.Close();

            dtlistShowTransactions.DataSource = dt;
            dtlistShowTransactions.DataBind();


        }

      
    }
}