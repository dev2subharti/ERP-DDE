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
    public partial class PublishDCDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 57))
            {

                if (!IsPostBack)
                {

                    if (Request.QueryString["DCNo"] != null)
                    {

                        populateDCDetails();
                        populateTransactions();

                    }
                    else
                    {
                        pnlData.Visible = false;

                        lblMSG.Text = "Please fill any draft no.";
                        pnlMSG.Visible = true;
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

        private void populateDCDetails()
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession ", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from [DDEFeeRecord_" + dr1["AcountSession"].ToString() + "] where DCNumber='" + Request.QueryString["DCNo"] + "'", con);
                SqlDataReader dr;



                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {

                    dr.Read();
                    tbDType.Text = Accounts.findMOPByID(Convert.ToInt32(dr["PaymentMode"]));
                    tbDNo.Text = dr["DCNumber"].ToString();
                    tbDCDate.Text = Convert.ToDateTime(dr["DCDate"]).ToString("dd MMMM yyyy");
                    tbIBN.Text = dr["IBN"].ToString();
                    tbTotalAmount.Text = Convert.ToInt32(dr["TotalDCAmount"]).ToString();
                    tbUsedAmount.Text = Accounts.findUsedAmountOfDraft(Convert.ToInt32(dr["PaymentMode"]),Request.QueryString["DCNo"],Convert.ToDateTime(dr["DCDate"]).ToString("yyyy-MM-dd"), tbIBN.Text).ToString();
                    tbBalance.Text = (Convert.ToInt32(tbTotalAmount.Text) - Convert.ToInt32(tbUsedAmount.Text)).ToString();

                }

                con.Close();
            }
            con1.Close();

            pnlDCDetail.Visible = true;
            
        }



       

        

        private void populateTransactions()
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FormNo");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("SCCode");
            DataColumn dtcol5 = new DataColumn("EnrollmentNo");
            DataColumn dtcol6 = new DataColumn("Batch");
            DataColumn dtcol7 = new DataColumn("StudentName");
            DataColumn dtcol8 = new DataColumn("FatherName");
            DataColumn dtcol9 = new DataColumn("Course");
            DataColumn dtcol10 = new DataColumn("Year");
            DataColumn dtcol11 = new DataColumn("FeeHead");
            DataColumn dtcol12 = new DataColumn("Amount");
            DataColumn dtcol13 = new DataColumn("ASession");




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
            dt.Columns.Add(dtcol12);
            dt.Columns.Add(dtcol13);

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession ", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;
             
                cmd.CommandText = "select * from [DDEFeeRecord_" + dr1["AcountSession"].ToString() + "] where DCNumber='" + Request.QueryString["DCNo"] + "'";
               

                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader();

                int i = 1;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        DataRow drow = dt.NewRow();

                        drow["SNo"] = i;
                        fillStudentInfo(Convert.ToInt32(dr["SRID"]), drow);
                        if (Convert.ToInt32(dr["FeeHead"]) == 3)
                        {
                            drow["Year"] = findBPYear(Convert.ToInt32(dr["SRID"]), dr["ForExam"].ToString());
                        }
                        else
                        {
                            drow["Year"] = dr["ForYear"].ToString();
                        }
                        drow["FeeHead"] = Accounts.findFeeHeadNameByID(Convert.ToInt32(dr["FeeHead"]));
                        drow["Amount"] = Convert.ToInt32(dr["Amount"]).ToString();
                        drow["ASession"] = dr1["AcountSession"].ToString();

                        dt.Rows.Add(drow);
                        i = i + 1;
                    }
                }



                dtlistShowTransactions.DataSource = dt;
                dtlistShowTransactions.DataBind();

                con.Close();
            }
            con1.Close();
        }
        private string findBPYear(int srid, string exam)
        {
            string year = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_" + exam + " where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if ((dr["BPSubjects1"].ToString() != ""))
                {
                    year = "1";
                    if ((dr["BPSubjects2"].ToString() != ""))
                    {
                        year = "1,2";
                    }
                    else if ((dr["BPSubjects3"].ToString() != ""))
                    {
                        year = "1,3";
                    }
                }
                else if ((dr["BPSubjects2"].ToString() != ""))
                {
                    year = "2";
                    if ((dr["BPSubjects1"].ToString() != ""))
                    {
                        year = "1,2";
                    }
                    else if ((dr["BPSubjects3"].ToString() != ""))
                    {
                        year = "2,3";
                    }
                }
                else if ((dr["BPSubjects3"].ToString() != ""))
                {
                    year = "3";
                    if ((dr["BPSubjects1"].ToString() != ""))
                    {
                        year = "1,3";
                    }
                    else if ((dr["BPSubjects2"].ToString() != ""))
                    {
                        year = "2,3";
                    }
                }
            }
            con.Close();
            return year;
        }

        private void fillStudentInfo(int srid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select ApplicationNo,EnrollmentNo,Session,StudentName,FatherName,StudyCentreCode,Course from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drow["FormNo"] = Convert.ToString(dr["ApplicationNo"]);
                drow["SRID"] = srid.ToString();
                drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["Batch"] = Convert.ToString(dr["Session"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));

            }

            con.Close();

        }
    }
}
