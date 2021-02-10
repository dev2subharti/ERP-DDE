using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DDE.DAL;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class ShowDCDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 57))
            {

                if (!IsPostBack)
                {

                    //if (Request.QueryString["DCNo"] != null)
                    //{

                    //    populateDCDetails();

                    //}
                    //else
                    //{
                        pnlSearch.Visible = true;
                    //}

                   
                }
                //if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 56))
                //{
                //    btnUpdateDD.Visible = true;
                //}
                //else
                //{
                //    btnUpdateDD.Visible = false;
                //}
               

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

        private void setAccessibility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 56))
            {
                foreach (DataListItem dli in dtlistShowTransactions.Items)
                {


                    LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");
                    LinkButton delete = (LinkButton)dli.FindControl("lnkbtnDelete");


                    edit.Visible = true;
                    delete.Visible = true;



                }
            }

        }

        private void populateDCDetails(int sno)
        {
            foreach (DataListItem dli in dtlistTotalInstruments.Items)
            {

                Label sn = (Label)dli.FindControl("lblSNo");
                Label itype = (Label)dli.FindControl("lblType");
                Label itypeno = (Label)dli.FindControl("lblTypeNo");
                Label ino = (Label)dli.FindControl("lblNo");
                Label idate = (Label)dli.FindControl("lblDate");
                Label itotalamount = (Label)dli.FindControl("lblTotalAmount");
                Label ibn = (Label)dli.FindControl("lblIBN");

                if (sno.ToString() == sn.Text)
                {

                    ddlistPaymentMode.SelectedItem.Selected = false;

                    ddlistPaymentMode.Items.FindByValue(itypeno.Text).Selected = true;

                    tbDNo.Text = ino.Text;
                    lblDNo.Text = ino.Text;
                   
                    string dcdate = Convert.ToDateTime(idate.Text).ToString("yyyy-MM-dd");
                   
                    ddlistDDDay.SelectedItem.Selected = false;
                    ddlistDDMonth.SelectedItem.Selected = false;
                    ddlistDDYear.SelectedItem.Selected = false;

                    ddlistDDDay.Items.FindByText(dcdate.Substring(8, 2)).Selected = true;
                    ddlistDDMonth.Items.FindByValue(dcdate.Substring(5, 2)).Selected = true;
                    ddlistDDYear.Items.FindByText(dcdate.Substring(0, 4)).Selected = true;

                    tbIBN.Text =ibn.Text;
                    lblIID.Text = Accounts.findIIDByInstrumentDetails(Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value),lblDNo.Text, dcdate, ibn.Text).ToString();
                              
                    tbTotalAmount.Text = itotalamount.Text;
                    tbUsedAmount.Text = Accounts.findUsedAmountOfDraft(Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDNo.Text, dcdate, tbIBN.Text).ToString();
                    tbBalance.Text = (Convert.ToInt32(tbTotalAmount.Text) - Convert.ToInt32(tbUsedAmount.Text)).ToString();

                    break;
                }
            }            

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 56))
            {
               
                tbDNo.Enabled = true;
                ddlistPaymentMode.Enabled = true;
                ddlistDDDay.Enabled = true;
                ddlistDDMonth.Enabled = true;
                ddlistDDYear.Enabled = true;
                tbIBN.Enabled = true;
                tbTotalAmount.Enabled = true;
               
            }

            pnlDCDetail.Visible = true;
            pnlSearch.Visible = false;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (Accounts.draftnoExist(tbDCNo.Text))
            {
                populateTotalInstruments();
                dtlistTotalInstruments.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record found with this draft no.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void populateTotalInstruments()
        {
            string query = "";
           

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (query == "")
                        {
                            query = "select distinct PaymentMode,DCNumber,DCDate,IBN,TotalDCAmount from [DDEFeeRecord_" + dr[0].ToString() + "] where DCNumber='"+tbDCNo.Text+"' ";
                        }
                        else
                        {
                            query = query + " union " + "select distinct PaymentMode,DCNumber,DCDate,IBN,TotalDCAmount from [DDEFeeRecord_" + dr[0].ToString() + "] where DCNumber='"+tbDCNo.Text+"'";
                        }
                    }

                }
                con.Close();

                query = query + " union select distinct PaymentMode,DCNumber,DCDate,IBN,TotalDCAmount from DDESCFeeRecord where DCNumber='" + tbDCNo.Text + "'";
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr1;
                SqlCommand cmd1 = new SqlCommand(query , con1);

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("Type");
                DataColumn dtcol3 = new DataColumn("TypeNo");
                DataColumn dtcol4 = new DataColumn("No");
                DataColumn dtcol5 = new DataColumn("Date");
                DataColumn dtcol6 = new DataColumn("TotalAmount");
                DataColumn dtcol7 = new DataColumn("IBN");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);

                con1.Open();
                dr1 = cmd1.ExecuteReader();
                int i = 1;
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i;
                        drow["Type"] =FindInfo.findPaymentModeByID(Convert.ToInt32(dr1["PaymentMode"]));
                        drow["TypeNo"] = Convert.ToInt32(dr1["PaymentMode"]);
                        drow["No"] = Convert.ToString(dr1["DCNumber"]);
                        drow["Date"] = Convert.ToDateTime(dr1["DCDate"]).ToString("dd MMMM yyyy");
                        drow["TotalAmount"] = Convert.ToInt32(dr1["TotalDCAmount"]);
                        drow["IBN"] = Convert.ToString(dr1["IBN"]);
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }

                }
                con1.Close();

                dtlistTotalInstruments.DataSource = dt;
                dtlistTotalInstruments.DataBind();
           

        }

        protected void btnFindTransactions_Click(object sender, EventArgs e)
        {
            populateTransactions();
            
            pnlTransactions.Visible = true;
        }

        private void populateTransactions()
        {
            string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Text + "-" + ddlistDDDay.SelectedItem.Text;

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FRID");
            DataColumn dtcol3 = new DataColumn("FormNo");
            DataColumn dtcol4 = new DataColumn("SRID");
            DataColumn dtcol5 = new DataColumn("SCCode");
            DataColumn dtcol6 = new DataColumn("EnrollmentNo");
            DataColumn dtcol7 = new DataColumn("Batch");
            DataColumn dtcol8 = new DataColumn("StudentName");
            DataColumn dtcol9 = new DataColumn("FatherName");
            DataColumn dtcol10 = new DataColumn("Course");
            DataColumn dtcol11 = new DataColumn("Year");
            DataColumn dtcol12 = new DataColumn("Exam");
            DataColumn dtcol13 = new DataColumn("FeeHead");
            DataColumn dtcol14 = new DataColumn("Amount");
            DataColumn dtcol15 = new DataColumn("ASession");
            DataColumn dtcol16 = new DataColumn("Discription_E");
            DataColumn dtcol17 = new DataColumn("Discription_D");
            DataColumn dtcol18 = new DataColumn("TOFS");
           
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
            dt.Columns.Add(dtcol14);
            dt.Columns.Add(dtcol15);
            dt.Columns.Add(dtcol16);
            dt.Columns.Add(dtcol17);
            dt.Columns.Add(dtcol18);

            int i = 1;
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

                cmd.CommandText = "select FRID,SRID,FeeHead,PaymentMode,DCNumber,DCDate,TotalDCAmount,Amount,ForYear,ForExam,CONVERT(datetime,[TOFS],105) as TOFS from [DDEFeeRecord_" + dr1["AcountSession"].ToString() + "] where PaymentMode='" + ddlistPaymentMode.SelectedItem.Value + "' and DCNumber='" + tbDNo.Text + "' and DCDate='" + dcdate + "' and IBN='" + tbIBN.Text + "'";
               
                cmd.Connection = con;
                con.Open();
                
                dr = cmd.ExecuteReader();
               
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        DataRow drow = dt.NewRow();

                        drow["SNo"] = i;
                        drow["FRID"] = dr["FRID"].ToString();
                        fillStudentInfo(Convert.ToInt32(dr["SRID"]), drow);
                        if (Convert.ToInt32(dr["FeeHead"]) == 3)
                        {
                            drow["Year"] = findBPYear(Convert.ToInt32(dr["SRID"]),dr["ForExam"].ToString());
                        }
                        else
                        {
                            drow["Year"] = dr["ForYear"].ToString();
                        }
                        drow["Exam"] = (dr["ForExam"]).ToString();
                        drow["FeeHead"] = Accounts.findFeeHeadNameByID(Convert.ToInt32(dr["FeeHead"]));
                        drow["Amount"] = Convert.ToInt32(dr["Amount"]).ToString();
                        drow["ASession"] = dr1["AcountSession"].ToString();
                        drow["Discription_E"] = "E_" + drow["ASession"]+"_" + drow["FeeHead"].ToString() + " of " + drow["EnrollmentNo"].ToString() + " for Year '" + drow["Year"].ToString() + " for Exam '"+drow["Exam"].ToString();
                        drow["Discription_D"] = "D_" + drow["ASession"]+ "_" + drow["FeeHead"].ToString() + " of " + drow["EnrollmentNo"].ToString() + " for Year '" + drow["Year"].ToString() + " for Exam '" + drow["Exam"].ToString();
                        drow["TOFS"] = Convert.ToDateTime(dr["TOFS"]).ToString("yyyy-MM-dd hh:mm:ss tt");
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }
                }                

                con.Close();
            }
            con1.Close();

            int sno = 1;
            if (i == 1)
            {
                sno = 1;
            }
            else if(i>1)
            {
                sno = i - 1;
            }

            populateSCFee(dt, sno);

            dt.DefaultView.Sort = "TOFS ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }
           

            dtlistShowTransactions.DataSource = dt;
            dtlistShowTransactions.DataBind();

            setAccessibility();
        }

        private void populateSCFee(DataTable dt, int sno)
        {
            string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Text + "-" + ddlistDDDay.SelectedItem.Text;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from DDESCFeeRecord where PaymentMode='" + ddlistPaymentMode.SelectedItem.Value + "' and DCNumber='" + tbDNo.Text + "' and DCDate='" + dcdate + "' and IBN='" + tbIBN.Text + "'";


            cmd.Connection = con;
            con.Open();

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    DataRow drow = dt.NewRow();

                    drow["SNo"] = sno;
                    drow["FRID"] = dr["SCFRID"].ToString();
                    fillSCInfo(Convert.ToInt32(dr["SCID"]), drow);

                    drow["Year"] = dr["ForYear"].ToString();

                    drow["Exam"] = "NA";
                    drow["FeeHead"] = Accounts.findFeeHeadNameByID(Convert.ToInt32(dr["FeeHead"]));
                    drow["Amount"] = Convert.ToInt32(dr["Amount"]).ToString();
                    drow["ASession"] = "AccountSession";
                    drow["Discription_E"] = "";
                    drow["Discription_D"] = "";

                    dt.Rows.Add(drow);
                    sno = sno + 1;
                }
            }

            con.Close();
        }

        private void fillSCInfo(int scid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SCCode from DDEStudyCentres where SCID='" + scid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drow["FormNo"] = "NA";
                drow["SRID"] = scid.ToString();
                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                drow["EnrollmentNo"] = "NA";
                drow["Batch"] = "NA";
                drow["StudentName"] = "NA";
                drow["FatherName"] = "NA";
                drow["Course"] = "NA";

            }

            con.Close();
        }

        private string  findBPYear(int srid, string exam)
        {
            string year = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_"+exam+" where SRID='" + srid + "'", con);
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = true;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }

        protected void dtlistShowTransactions_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string cmmd = e.CommandName.Substring(0, 1);
            string asession = e.CommandName.Substring(2, 7);
            Session["ASession"] = asession;

            if (cmmd == "E")
            {
                
                Response.Redirect("EditFeeDetail.aspx?FRID=" + Convert.ToInt32(e.CommandArgument));
            }
            else if (cmmd == "D")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from [DDEFeeRecord_" + asession + "] where FRID ='" + Convert.ToString(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteReader();
                con.Close();

                Log.createLogNow("Delete", "Delete " + e.CommandName.Substring(10, (e.CommandName.Length-10)), Convert.ToInt32(Session["ERID"].ToString()));

                
                populateTransactions();
            }
           

        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Response.Redirect("PublishDCDetails.aspx?DCNo="+tbDCNo.Text);
        }

        //protected void btnUpdateDD_Click(object sender, EventArgs e)
        //{
        //    string dcdate = ddlistDDYear.SelectedItem.Value + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Value;

        //        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlDataReader dr1;
        //        SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession ", con1);
        //        con1.Open();
        //        dr1 = cmd1.ExecuteReader();
        //        while (dr1.Read())
        //        {

        //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd = new SqlCommand("update [DDEFeeRecord_" + dr1["AcountSession"].ToString() + "] set PaymentMode=@PaymentMode,DCNumber=@DCNumber,DCDate=@DCDate,IBN=@IBN,TotalDCAmount=@TotalDCAmount where DCNumber='" + lblDNo.Text + "' and DCDate='"+dcdate+"' and IBN='"+tbIBN.Text+"' ", con);

        //            cmd.Parameters.AddWithValue("@PaymentMode", ddlistPaymentMode.SelectedItem.Value);
        //            cmd.Parameters.AddWithValue("@DCNumber", tbDNo.Text);
        //            cmd.Parameters.AddWithValue("@DCDate", dcdate);
        //            cmd.Parameters.AddWithValue("@IBN", tbIBN.Text);
        //            cmd.Parameters.AddWithValue("@TotalDCAmount", tbTotalAmount.Text);

        //            cmd.Connection = con;
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //        con1.Close();

        //    Log.createLogNow("Update", "Update Draft Detail of Draft No. '" + tbDNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

        //    pnlData.Visible = false;
        //    lblMSG.Text = "Record has been updated successfully";
        //    pnlMSG.Visible = true;
           
        //}

        protected void dtlistTotalInstruments_ItemCommand(object source, DataListCommandEventArgs e)
        {
            populateDCDetails(Convert.ToInt32(e.CommandArgument));
            dtlistTotalInstruments.Visible = false;
            pnlDCDetail.Visible = true;
           
        }

      
    }
}
