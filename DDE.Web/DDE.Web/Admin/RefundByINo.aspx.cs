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
    public partial class RefundByINo : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 82))
            {
                if (!IsPostBack)
                {
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
      
        private void populateDCDetails(int iid)
        {
            foreach (DataListItem dli in dtlistTotalInstruments.Items)
            {

                Label sn = (Label)dli.FindControl("lblSNo");
                Label liid = (Label)dli.FindControl("lbIID");
                Label itype = (Label)dli.FindControl("lblType");
                Label itypeno = (Label)dli.FindControl("lblTypeNo");
                Label ino = (Label)dli.FindControl("lblNo");
                Label idate = (Label)dli.FindControl("lblDate");
                Label itotalamount = (Label)dli.FindControl("lblTotalAmount");
                Label ibn = (Label)dli.FindControl("lblIBN");

                if (iid == Convert.ToInt32(liid.Text))
                {
                    lblIID.Text = iid.ToString();

                    ddlistPaymentMode.SelectedItem.Selected = false;
                    ddlistPaymentMode.Items.FindByValue(itypeno.Text).Selected = true;

                    tbDNo.Text = ino.Text;
                   

                    string dcdate = Convert.ToDateTime(idate.Text).ToString("yyyy-MM-dd");

                    ddlistDDDay.SelectedItem.Selected = false;
                    ddlistDDMonth.SelectedItem.Selected = false;
                    ddlistDDYear.SelectedItem.Selected = false;

                    ddlistDDDay.Items.FindByText(dcdate.Substring(8, 2)).Selected = true;
                    ddlistDDMonth.Items.FindByValue(dcdate.Substring(5, 2)).Selected = true;
                    ddlistDDYear.Items.FindByText(dcdate.Substring(0, 4)).Selected = true;


                    tbIBN.Text = ibn.Text;
                    tbTotalAmount.Text = itotalamount.Text;
                    tbUsedAmount.Text = Accounts.findUsedAmountOfDraft(Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDNo.Text, dcdate, tbIBN.Text).ToString();
                    tbBalance.Text = (Convert.ToInt32(tbTotalAmount.Text) - Convert.ToInt32(tbUsedAmount.Text)).ToString();

                    break;
                }
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
           
            SqlConnection con= new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where INo='"+tbDCNo.Text+"'", con);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("IID");
            DataColumn dtcol3 = new DataColumn("Type");
            DataColumn dtcol4 = new DataColumn("TypeNo");
            DataColumn dtcol5 = new DataColumn("No");
            DataColumn dtcol6 = new DataColumn("Date");
            DataColumn dtcol7 = new DataColumn("TotalAmount");
            DataColumn dtcol8 = new DataColumn("IBN");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            con.Open();
            dr = cmd.ExecuteReader();
            int i = 1;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["IID"] = Convert.ToInt32(dr["IID"]);
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr["IType"]));
                    drow["TypeNo"] = Convert.ToInt32(dr["IType"]);
                    drow["No"] = Convert.ToString(dr["INo"]);
                    drow["Date"] = Convert.ToDateTime(dr["IDate"]).ToString("dd MMMM yyyy");
                    drow["TotalAmount"] = Convert.ToInt32(dr["TotalAmount"]);
                    drow["IBN"] = Convert.ToString(dr["IBN"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }
            con.Close();


            dtlistTotalInstruments.DataSource = dt;
            dtlistTotalInstruments.DataBind();


        }

     

        private void populateTransactions()
        {
           

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FRID"); 
            DataColumn dtcol3 = new DataColumn("SRID");  
    
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");     
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("CourseID");  
            DataColumn dtcol8 = new DataColumn("Course");
            DataColumn dtcol9 = new DataColumn("Batch");
            DataColumn dtcol10 = new DataColumn("SCCode");

            DataColumn dtcol11 = new DataColumn("Year");
            DataColumn dtcol12 = new DataColumn("ReqFee");
            DataColumn dtcol13 = new DataColumn("Trans");
            DataColumn dtcol14 = new DataColumn("PaidFee");
            DataColumn dtcol15 = new DataColumn("FeePer");
            DataColumn dtcol16 = new DataColumn("Refund");
            DataColumn dtcol17 = new DataColumn("Col");
           
         
            dt.Columns.Add(dtcol1);
         
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

            int i = 1;
           

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                cmd.CommandText =findCommanad(); 


                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader();


                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int pfee;
                        string insdetails;
                        bool sameins;
                        bool sameltins;

                        DataRow drow = dt.NewRow();

                        drow["SNo"] = i;  
                        drow["SRID"]=dr["SRID"].ToString();
                        fillStudentInfo(Convert.ToInt32(dr["SRID"]),Convert.ToInt32(dr["ForYear"]), drow);
                      
                        drow["Year"] = dr["ForYear"].ToString();
                        drow["ReqFee"] = Accounts.findRequiredFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(drow["CourseID"]), 1, 0, drow["Batch"].ToString(), "");
                        findFeeDetails(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["ForYear"]), out pfee,out sameins, out insdetails,out sameltins);
                        
                       
                        //drow["InsDetails"] = insdetails;
                        if (sameins == true)
                        {
                            drow["Trans"] = pfee;
                            drow["PaidFee"] = pfee;
                            drow["FeePer"] = findFeePercent(Convert.ToInt32(drow["ReqFee"]), pfee);
                            if (!FindInfo.isRefundGenerated(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["ForYear"])))
                            {
                                drow["Refund"] = findRefund(Convert.ToInt32(drow["ReqFee"]), pfee);
                                drow["Col"] = "";
                            }
                            else
                            {
                                drow["Refund"] = "0";
                                drow["Col"] = "AG";
                            }
                        }
                        else
                        {
                            drow["Trans"] = insdetails+"</br><b>"+pfee+"</b>";
                            drow["PaidFee"] = pfee;
                            drow["FeePer"] = findFeePercent(Convert.ToInt32(drow["ReqFee"]), pfee);
                            if (sameltins == true)
                            {
                                if (!FindInfo.isRefundGenerated(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["ForYear"])))
                                {
                                    drow["Refund"] = findRefund(Convert.ToInt32(drow["ReqFee"]), pfee);
                                    drow["Col"] = "";
                                }
                                else
                                {
                                    drow["Refund"] = "0";
                                    drow["Col"] = "AG";
                                }
                            }
                            else
                            {
                                if (!FindInfo.isRefundGenerated(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["ForYear"])))
                                {
                                    drow["Refund"] = "0";
                                    drow["Col"] = "";
                                }
                                else
                                {
                                    drow["Refund"] = "0";
                                    drow["Col"] = "AG";
                                }
                               
                            }
                           
                        }
                       
                              
                      
                     
                      
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }

                  
                    
                }





                con.Close();
           

            int sno = 1;
            if (i == 1)
            {
                sno = 1;
            }
            else if (i > 1)
            {
                sno = i - 1;
            }

            var batch = dt.AsEnumerable().Select(s => new
            {
                id = s.Field<string>("Batch"),
            }).Distinct().ToList();

            if (batch.Count > 1)
            {
                ddlistBatch.Items.Add("ALL");
            }
            for (int a = 0; a < batch.Count; a++)
            {
                ddlistBatch.Items.Add(batch[a].id);
            }



            var sccode = dt.AsEnumerable().Select(s => new
            {
                id = s.Field<string>("SCCode"),
            }).Distinct().ToList();

            if (sccode.Count > 1)
            {
                ddlistSCCode.Items.Add("ALL");
            }
           
            for (int a = 0; a < sccode.Count; a++)
            {
                ddlistSCCode.Items.Add(sccode[a].id);
            }




            var course = dt.AsEnumerable().Select(s => new
            {
                id = s.Field<string>("Course"),
            }).Distinct().ToList();

            if (course.Count > 1)
            {
                ddlistCourse.Items.Add("ALL");
            }

            for (int a = 0; a < course.Count; a++)
            {
                ddlistCourse.Items.Add(course[a].id);
            }


            if (dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "EnrollmentNo";
                DataView dv = dt.DefaultView;

                int j = 1;
                foreach (DataRowView dvr in dv)
                {
                    dvr[0] = j;
                    j++;
                }

                dtlistShowTransactions.DataSource = dt;
                dtlistShowTransactions.DataBind();
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No 'Course Fee' entered with this instrument.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }

           
            ViewState["StuList"] = dt;
            Session["FinalList"] = dt;
         
        }     

        private void findFeeDetails(int srid, int year, out int pfee,out bool sameins, out string insdetails, out bool sameltins)
        {
            string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;

            pfee = 0;
            insdetails = "";
            sameins = false;
            sameltins = false;
            int valid = 1;

           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from [DDEFeeRecord_2009-10] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2010-11] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2011-12] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2012-13] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2014-15] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2015-16] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2016-17] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2017-18] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2018-19] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2019-20] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "' union select * from [DDEFeeRecord_2020-21] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

           
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pfee = pfee + Convert.ToInt32(ds.Tables[0].Rows[i]["Amount"]);
                    if (insdetails == "")
                    {
                        insdetails = Convert.ToInt32(ds.Tables[0].Rows[i]["Amount"]) + " - " + ds.Tables[0].Rows[i]["DCNumber"].ToString();
                    }
                    else
                    {
                        insdetails = insdetails + "<br/>" + Convert.ToInt32(ds.Tables[0].Rows[i]["Amount"]) + " - " + ds.Tables[0].Rows[i]["DCNumber"].ToString();
                    }

                    if (valid == 1)
                    {

                        if ((ds.Tables[0].Rows[i]["DCNumber"].ToString() == tbDCNo.Text.ToUpper()) && (ds.Tables[0].Rows[i]["PaymentMode"].ToString() == ddlistPaymentMode.SelectedItem.Value) && (Convert.ToDateTime(ds.Tables[0].Rows[i]["DCDate"]).ToString("yyyy-MM-dd") == dcdate) && (ds.Tables[0].Rows[i]["IBN"].ToString() == tbIBN.Text))
                        {
                            sameins = true;
                        }
                        else
                        {
                            sameins = false;
                            valid = 0;
                        }

                        
                    }
                    if (i == (ds.Tables[0].Rows.Count - 1))
                    {
                        if ((ds.Tables[0].Rows[i]["DCNumber"].ToString() == tbDCNo.Text.ToUpper()) && (ds.Tables[0].Rows[i]["PaymentMode"].ToString() == ddlistPaymentMode.SelectedItem.Value) && (Convert.ToDateTime(ds.Tables[0].Rows[i]["DCDate"]).ToString("yyyy-MM-dd") == dcdate) && (ds.Tables[0].Rows[i]["IBN"].ToString() == tbIBN.Text))
                        {
                            sameltins = true;
                        }
                        else
                        {
                            sameltins = false;

                        }
                    }

                }
            }

            con.Close();


        }

        private float findRefund(float rfee, float pfee)
        {
            if (pfee != 0)
            {
              
                if (pfee == rfee)
                {
                    return ((40 * pfee) / 100);
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

        private float findFeePercent(float rfee, float pfee)
        {
            if (pfee != 0)
            {
                return ((pfee / rfee) * 100);
            }
            else
            {
                return 0;
            }
        }

        private string findCommanad()
        {
            string dcdate = ddlistDDYear.SelectedItem.Text + "-" + ddlistDDMonth.SelectedItem.Value + "-" + ddlistDDDay.SelectedItem.Text;

            string command = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select AcountSession from DDEAcountSession", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (command == "")
                {
                    command = "select distinct SRID,ForYear from [DDEFeeRecord_" + dr[0].ToString() + "] where FeeHead='1' and PaymentMode='" + ddlistPaymentMode.SelectedItem.Value + "' and DCNumber='" + tbDNo.Text.ToUpper() + "' and DCDate='" + dcdate + "' and IBN='" + tbIBN.Text + "'";
                }
                else
                {
                    command = command + " union " + "select distinct SRID,ForYear from [DDEFeeRecord_" + dr[0].ToString() + "] where FeeHead='1' and PaymentMode='" + ddlistPaymentMode.SelectedItem.Value + "' and DCNumber='" + tbDNo.Text.ToUpper() + "' and DCDate='" + dcdate + "' and IBN='" + tbIBN.Text + "'";
                }

            }

            con.Close();
            return command;
        }

        private void fillStudentInfo(int srid,int year, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select ApplicationNo,EnrollmentNo,Session,StudentName,FatherName,StudyCentreCode,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                if (FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"])) == "MBA")
                {
                    if (year == 1)
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                        drow["CourseID"] = Convert.ToInt32(dr["Course"]);
                    }
                    else if (year == 2)
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course2Year"]));
                        drow["CourseID"] = Convert.ToInt32(dr["Course2Year"]);
                    }
                    else if (year == 3)
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course3Year"]));
                        drow["CourseID"] = Convert.ToInt32(dr["Course3Year"]);
                    }

                }
                else
                {
                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                    drow["CourseID"] = Convert.ToInt32(dr["Course"]);
                }
               
                drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);           
                drow["Batch"] = Convert.ToString(dr["Session"]);
               

            }

            con.Close();

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
           
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {           
                Session["IID"] = lblIID.Text;
                Session["INo"] = tbDNo.Text;
                Session["IType"] = ddlistPaymentMode.SelectedItem.Text;
                Session["IDate"] = ddlistDDDay.SelectedItem.Text + " " + ddlistDDMonth.SelectedItem.Text + " " + ddlistDDYear.SelectedItem.Text;
                Session["IBN"] = tbIBN.Text;
                Session["TotalAmount"] = tbTotalAmount.Text;
                Session["Batch"] = ddlistBatch.SelectedItem.Text;
                Session["SCCode"] = ddlistSCCode.SelectedItem.Text;
                Session["Course"] = ddlistCourse.SelectedItem.Text;

                Session["TotalRefund"] = tbTotalRefund.Text;
                Session["Extra"] = tbBalanceExtra.Text;
                Session["Short"] = tbBalanceShort.Text;
                Session["NetRefund"] = tbNetRefund.Text;

                Response.Redirect("PublishRefundByINo.aspx");     
           
        }

        protected void dtlistTotalInstruments_ItemCommand(object source, DataListCommandEventArgs e)
        {
          string lno = "";
          if (!FindInfo.isInsAlreadyPublished(Convert.ToInt32(e.CommandArgument), out lno))
          {
              populateDCDetails(Convert.ToInt32(e.CommandArgument));
              dtlistTotalInstruments.Visible = false;
              pnlDCDetail.Visible = true;
              populateTransactions();
              pnlTransactions.Visible = true;
              calculateRefund();
              pnlSearch.Visible = false;
          }
          else
          {
              pnlData.Visible = false;
              lblMSG.Text = "Sorry! Refund is already generated for this instrument.The Letter No. is : " + lno;
              pnlMSG.Visible = true;
              btnOK.Visible = true;
          }
           
        }

        protected void ddlistBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
            calculateRefund();
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
            calculateRefund();
        }

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterData();
            calculateRefund();
        }

        private void calculateRefund()
        {
            double totalrefund = 0;
            foreach (DataListItem dli in dtlistShowTransactions.Items)
            {
                Label ram = (Label)dli.FindControl("lblRefund");

                totalrefund = totalrefund + Convert.ToDouble(ram.Text);
            }
            tbTotalRefund.Text = totalrefund.ToString();
        }

        private void filterData()
        {
            DataTable dt = (DataTable)ViewState["StuList"];
            if (ddlistBatch.SelectedItem.Text == "ALL")
            {
                if (ddlistSCCode.SelectedItem.Text == "ALL")
                {
                    if (ddlistCourse.SelectedItem.Text == "ALL")
                    {
                        DataView dv = dt.DefaultView;


                        int j = 1;
                        foreach (DataRowView dvr in dv)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dt;
                        dtlistShowTransactions.DataBind();

                         ViewState["FinalList"] =dt;
                    }
                    else
                    {
                        DataView dv = new DataView(dt, "Course = '" + ddlistCourse.SelectedItem.Text + "' ", "Course Desc", DataViewRowState.CurrentRows);

                        int j = 1;
                        foreach (DataRowView dvr in dv)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dv;
                        dtlistShowTransactions.DataBind();

                        Session["FinalList"] = dv.ToTable();
                    }
                }
                else
                {
                    if (ddlistCourse.SelectedItem.Text == "ALL")
                    {
                        DataView dv = new DataView(dt, "SCCode = '" + ddlistSCCode.SelectedItem.Text + "' ", "SCCode Desc", DataViewRowState.CurrentRows);
                        int j = 1;
                        foreach (DataRowView dvr in dv)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dv;
                        dtlistShowTransactions.DataBind();

                        Session["FinalList"] = dv.ToTable();
                    }
                    else
                    {
                        DataView dv = new DataView(dt, "SCCode = '" + ddlistSCCode.SelectedItem.Text + "' ", "SCCode Desc", DataViewRowState.CurrentRows);

                        DataTable dt1 = dv.ToTable();

                        DataView dv1 = new DataView(dt1, "Course = '" + ddlistCourse.SelectedItem.Text + "' ", "Course Desc", DataViewRowState.CurrentRows);

                        int j = 1;
                        foreach (DataRowView dvr in dv1)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dv1;
                        dtlistShowTransactions.DataBind();

                        Session["FinalList"] = dv1.ToTable();
                    }
                }

            }
            else
            {
                if (ddlistSCCode.SelectedItem.Text == "ALL")
                {
                    if (ddlistCourse.SelectedItem.Text == "ALL")
                    {
                        DataView dv = new DataView(dt, "Batch = '" + ddlistBatch.SelectedItem.Text + "' ", "Batch Desc", DataViewRowState.CurrentRows);

                        int j = 1;
                        foreach (DataRowView dvr in dv)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dv;
                        dtlistShowTransactions.DataBind();

                        Session["FinalList"] = dv.ToTable(); 
                    }
                    else
                    {
                        DataView dv = new DataView(dt, "Batch = '" + ddlistBatch.SelectedItem.Text + "' ", "Batch Desc", DataViewRowState.CurrentRows);

                        DataTable dt1 = dv.ToTable();

                        DataView dv1 = new DataView(dt1, "Course = '" + ddlistCourse.SelectedItem.Text + "' ", "Course Desc", DataViewRowState.CurrentRows);

                        int j = 1;
                        foreach (DataRowView dvr in dv1)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dv1;
                        dtlistShowTransactions.DataBind();

                        Session["FinalList"] = dv1.ToTable();
                    }
                }
                else
                {
                    if (ddlistCourse.SelectedItem.Text == "ALL")
                    {
                        DataView dv = new DataView(dt, "Batch = '" + ddlistBatch.SelectedItem.Text + "' ", "Batch Desc", DataViewRowState.CurrentRows);

                        DataTable dt1 = dv.ToTable();

                        DataView dv1 = new DataView(dt1, "SCCode = '" + ddlistSCCode.SelectedItem.Text + "' ", "SCCode Desc", DataViewRowState.CurrentRows);

                        int j = 1;
                        foreach (DataRowView dvr in dv1)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dv1;
                        dtlistShowTransactions.DataBind();

                        Session["FinalList"] = dv1.ToTable();
                    }
                    else
                    {
                        DataView dv = new DataView(dt, "Batch = '" + ddlistBatch.SelectedItem.Text + "' ", "Batch Desc", DataViewRowState.CurrentRows);

                        DataTable dt1 = dv.ToTable();

                        DataView dv1 = new DataView(dt1, "SCCode = '" + ddlistSCCode.SelectedItem.Text + "' ", "SCCode Desc", DataViewRowState.CurrentRows);

                        DataTable dt2 = dv1.ToTable();

                        DataView dv2 = new DataView(dt2, "Course = '" + ddlistCourse.SelectedItem.Text + "' ", "Course Desc", DataViewRowState.CurrentRows);

                        int j = 1;
                        foreach (DataRowView dvr in dv2)
                        {
                            dvr[0] = j;
                            j++;
                        }
                        dtlistShowTransactions.DataSource = dv2;
                        dtlistShowTransactions.DataBind();

                        Session["FinalList"] = dv.ToTable();
                    }
                }

            }

           
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            int extra = 0;
            int shorta = 0;
            if (tbBalanceExtra.Text == "")
            {
                extra = 0;
            }
            else
            {
                extra = Convert.ToInt32(tbBalanceExtra.Text);
            }
            if (tbBalanceShort.Text == "")
            {
                shorta = 0;
            }
            else
            {
                shorta = Convert.ToInt32(tbBalanceShort.Text);
            }
            tbNetRefund.Text = ((Convert.ToInt32(tbTotalRefund.Text) + extra) - (shorta)).ToString();
            tbBalanceExtra.Enabled = false;
            tbBalanceShort.Enabled = false;
            btnPublish.Visible = true;
        }
    }
}