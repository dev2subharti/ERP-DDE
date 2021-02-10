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
    public partial class FillSCFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 80))
            {
                if (!IsPostBack)
                {
                    ViewState["ErrorType"] = "";
                   
                    PopulateDDList.populateSCFeeHead(ddlistFeeHead);
                    PopulateDDList.populateAccountSession(ddlistAcountsSession);              
                    ddlistAcountsSession.Items.FindByText("2016-17").Selected = true;
                   
                    pnlFeeHead.Visible = true;
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (validEntry())
            {

                btnFind.Visible = false;
                fillFeePanelDetails();
                setTodayDate();
                setFeeStatus();

                ddlistAcountsSession.AutoPostBack = true;
                ddlistPaymentMode.AutoPostBack = true;

                pnlDDFee.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
              
                if (ddlistFeeHead.SelectedItem.Value == "19")
                {
                    pnlRPeriod.Visible = true;
                    setRenewalPeriod();
                }
                else
                {
                    pnlRPeriod.Visible = false;
                }             

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void setRenewalPeriod()
        {
            ddlistRSession.SelectedItem.Selected = false;
            ddlistRSession.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            rfYear.SelectedItem.Selected = false;          
            rfYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            rtDay.SelectedItem.Selected = false;
            rtMonth.SelectedItem.Selected = false;
            rtYear.SelectedItem.Selected = false;
            rtDay.Items.FindByText(DateTime.Now.ToString("31")).Selected = true;
            rtMonth.Items.FindByValue(DateTime.Now.ToString("12").ToUpper()).Selected = true;
            rtYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        private void setTodayDate()
        {
            ddlistDDDay.SelectedItem.Selected = false;
            ddlistDDMonth.SelectedItem.Selected = false;
            ddlistDDYear.SelectedItem.Selected = false;
            ddlistDDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistFRDDay.SelectedItem.Selected = false;
            ddlistFRDMonth.SelectedItem.Selected = false;
            ddlistFRDYear.SelectedItem.Selected = false;
            ddlistFRDDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistFRDMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistFRDYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        private void setFeeStatus()
        {
                   
            if (validEntry())
            {
                string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                int tp = 0;
               
                int reqfee = Accounts.findRequiredSCFee(Convert.ToInt32(lblSCID.Text),Convert.ToInt32(ddlistFeeHead.SelectedItem.Value),tp,frdate);

                if (ddlistFeeHead.SelectedItem.Value == "22")
                {
                    reqfee = findSelectedStreamFee();
                }

                int paidfee = Accounts.findPreviousSCPaidFee(Convert.ToInt32(lblSCID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value),1);

               
                int duefee = (reqfee - paidfee);
                tbReqFee.Text = reqfee.ToString();
                tbPaidFee.Text = paidfee.ToString();
                tbDueFee.Text = duefee.ToString();
            }
        }

        private int findSelectedStreamFee()
        {
            int tfee = 0;
            foreach(DataListItem dli in dtlistStream.Items)
            {
                Label fee = (Label)dli.FindControl("lblSFee");
                CheckBox cb = (CheckBox)dli.FindControl("cbStream");


                if (cb.Checked && cb.Enabled)
                {
                    tfee = tfee + Convert.ToInt32(fee.Text);
                }

            }

            return tfee;
        }

        private void fillFeePanelDetails()
        {
            if (ddlistPaymentMode.SelectedItem.Value != "0")
            {
                if (ddlistPaymentMode.SelectedItem.Value == "1")
                {


                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "2")
                {


                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;



                }
                else if (ddlistPaymentMode.SelectedItem.Value == "3")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;


                }
                else if (ddlistPaymentMode.SelectedItem.Value == "4")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = false;
                    tbIBN.Visible = false;

                }
                else if (ddlistPaymentMode.SelectedItem.Value == "5")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;

                    lblIBN.Visible = true;
                    tbIBN.Visible = true;


                }
                else if (ddlistPaymentMode.SelectedItem.Value == "6")
                {

                    lblDCDate.Visible = true;
                    ddlistDDDay.Visible = true;
                    ddlistDDMonth.Visible = true;
                    ddlistDDYear.Visible = true;
                    lblIBN.Visible = true;
                    tbIBN.Visible = true;

                }

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Please select any one payment mode";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void populateSCInfo(int scid)
        {
           
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select SCCode,Location,City from DDEStudyCentres where SCID='" + scid + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                   
                    lblSCID.Text = scid.ToString();
                    tbSCCode1.Text = dr["SCCode"].ToString();
                    tbSName.Text = dr["Location"].ToString();
                    tbCity.Text = dr["City"].ToString();
                   
                }

                con.Close();          
           
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (tbSCCode.Text != "")
            {
                if (validSCCode(tbSCCode.Text))
                {
                  
                        pnlFeeHead.Visible = false;                     
                        populateSCInfo(FindInfo.findSCIDBySCCode(tbSCCode.Text));
                        pnlStudentDetail.Visible = true;
                        btnFind.Visible = true;
                   
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! this SC Code does not exist </br> Please fill valid SC Code first";
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry!! Yod did not fill SC Code.</br> Please fill SC Code first";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }


        }

        private bool validSCCode(string sccode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

          
            cmd.CommandText = "select * from DDEStudyCentres where SCCode='" + sccode+ "'";


            bool exist = false;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                exist = true;

            }

            con.Close();

            return exist;
        }

        protected void ddlistPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            fillFeePanelDetails();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string error;
           

            if (validEntry())
            {

                try
                {
                   
                    string frdate = ddlistFRDYear.SelectedItem.Value + "-" + ddlistFRDMonth.SelectedItem.Value + "-" + ddlistFRDDay.SelectedItem.Value;
                    int tp = 0;
                   
                    int cons = 0;
                    if (ddlistFeeHead.SelectedItem.Value == "22")
                    {
                        cons = Convert.ToInt32(tbCon.Text);
                    }
                    if (Accounts.validSCFee(Convert.ToInt32(lblSCID.Text),Convert.ToInt32(lblIID.Text),Convert.ToInt32(ddlistFeeHead.SelectedItem.Value),Convert.ToInt32(tbStudentAmount.Text),frdate,tp,Convert.ToInt32(tbReqFee.Text),cons, out error))
                    {
                        string foryear = "NA";

                        Accounts.fillSCFee(Convert.ToInt32(lblSCID.Text), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), ddlistAcountsSession.SelectedItem.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value), tbDDNumber.Text, ddlistDDDay.SelectedItem.Text, ddlistDDMonth.SelectedItem.Value, ddlistDDYear.SelectedItem.Text, tbIBN.Text, Convert.ToInt32(tbStudentAmount.Text), Accounts.IntegerToWords(Convert.ToInt32(tbStudentAmount.Text)), Convert.ToInt32(tbTotalAmount.Text),foryear,frdate,cons,1);
                        if (ddlistFeeHead.SelectedItem.Value == "19")
                        {
                            string rfrom = rfYear.SelectedItem.Text+"-"+rfMonth.SelectedItem.Value+"-"+rfDay.SelectedItem.Text;
                            string rto = rtYear.SelectedItem.Text + "-" + rtMonth.SelectedItem.Value + "-" + rtDay.SelectedItem.Text;

                            updateSCRenewalRecord(ddlistRSession.SelectedItem.Text, rfrom,rto);
                        }
                        if (ddlistFeeHead.SelectedItem.Value == "22")
                        {
                            string allotedcourse = findAllotedCourses();
                            updateSCStreamRecord(Convert.ToInt32(tbSCCode.Text),allotedcourse);
                        }

                        try
                        {
                            Log.createLogNow("SC Fee Submit", "Filled" + ddlistFeeHead.SelectedItem.Text + " Fee of SC Code '" + tbSCCode.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        }
                        catch
                        {
                            lblMSG.Text = "Fee has been submitted successfully !!";
                            pnlData.Visible = false;
                            pnlMSG.Visible = true;
                            btnOK.Visible = false;
                        }         

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = error;
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                      
                    }
                }

                catch (FormatException ex)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = ex.Message;
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                  
                }
            }

            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You did not select any of given entries";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
              
            }
        }

        private void updateSCStreamRecord(int scid, string allotedcourse)
        {

            if (alreadyExist(scid))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDESCAllotedStreams set AllotedStreams=@AllotedStreams where SCID='" + scid + "' ", con);

                cmd.Parameters.AddWithValue("@AllotedStreams", allotedcourse);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string astreams = FindInfo.findSCAllotedStreams(scid);
                Log.createLogNow("Streams Updated", "Updated Streams from '" + astreams + "' to '" + allotedcourse + "'", Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Streams have been updated successfully !!";
                pnlMSG.Visible = true;

            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDESCAllotedStreams values(@SCID,@AllotedStreams)", con);


                cmd.Parameters.AddWithValue("@SCID", scid);
                cmd.Parameters.AddWithValue("@AllotedStreams", allotedcourse);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Streams Alloted", "'" + allotedcourse + "' Streams Alloted to SC Code '" + tbSCCode.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Streams have been alloted successfully !!";
                pnlMSG.Visible = true;
            }
        }

        private bool alreadyExist(int scid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESCAllotedStreams where SCID='" + scid + "'", con);
            SqlDataReader dr;

            bool exist = false;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }

            con.Close();

            return exist;
        }

        private string findAllotedCourses()
        {
            string streams = "";
            foreach (DataListItem dli in dtlistStream.Items)
            {
                Label sid = (Label)dli.FindControl("lblSID");
                CheckBox cb = (CheckBox)dli.FindControl("cbStream");


                if (cb.Checked)
                {
                    if (streams == "")
                    {
                        streams = sid.Text;
                    }
                    else
                    {
                        streams = streams + "," + sid.Text;
                           
                    }
                }

            }

            string allotedcourse = "";


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select CourseID from DDECourse where StreamID in (" + streams + ") order by CourseID", con);
            SqlDataReader dr;


                 
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    if (allotedcourse == "")
                    {
                        allotedcourse = dr["CourseID"].ToString();
                    }
                    else
                    {
                        allotedcourse = allotedcourse+","+ dr["CourseID"].ToString();
                    }
                }

            }

            con.Close();

            return allotedcourse;
        }

        private void updateSCRenewalRecord(string currentrenewal, string rfrom, string rto)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudyCentres set REFor=@REFor,RFrom=@RFrom,RTo=@RTo where SCID='" + lblSCID.Text + "' ", con);

            cmd.Parameters.AddWithValue("@REFor", currentrenewal);
            cmd.Parameters.AddWithValue("@RFrom", rfrom);
            cmd.Parameters.AddWithValue("@RTo", rto);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private bool validEntry()
        {
            if (ddlistAcountsSession.SelectedItem.Text == "--SELECT ONE--" || ddlistFeeHead.SelectedItem.Text == "--SELECT ONE--" || ddlistPaymentMode.SelectedItem.Text == "--SELECT ONE--")
            {
                return false;
            }

            else
            {
                return true;
            }
                   
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
           
            pnlStudentDetail.Visible = true;
            btnFind.Visible = false;
            pnlFeeHead.Visible = false;
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
          
        }

        protected void lnkbtnFDCDetails_Click(object sender, EventArgs e)
        {
            
                string error;
                int iid;
                string scmode;
                int count;
                string ardate;
                if (Accounts.validInstrument(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value, tbSCCode1.Text,false,"", out iid,out scmode,out count,out ardate, out error))
                {
                    lblIID.Text = iid.ToString();

                    if (count == 1)
                    {

                        string[] dcdetail = Accounts.findInstrumentsDetailsNew(tbDDNumber.Text, Convert.ToInt32(ddlistPaymentMode.SelectedItem.Value));

                        if (scmode == "0")
                        {
                            pnlDDFee.BackColor = System.Drawing.Color.Orange;
                        }

                        
                        lblNewDD.Visible = false;
                        ddlistDDDay.SelectedItem.Selected = false;
                        ddlistDDMonth.SelectedItem.Selected = false;
                        ddlistDDYear.SelectedItem.Selected = false;

                        ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
                        ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
                        ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

                        tbTotalAmount.Text = dcdetail[1].ToString();
                        tbIBN.Text = dcdetail[2].ToString();
                       
                        lblNewDD.Visible = false;
                        btnSubmit.Visible = true;

                        tbDDNumber.Enabled = false;
                        ddlistDDDay.Enabled = false;
                        ddlistDDMonth.Enabled = false;
                        ddlistDDYear.Enabled = false;
                        if (ardate != "")
                        {
                            lblARDate.Text = "Amount. Rec. On : " + Convert.ToDateTime(ardate).ToString("dd-MM-yyyy");
                            lblARPDate.Text = Convert.ToDateTime(ardate).ToString("yyyy-MM-dd"); ;
                        }
                        else
                        {
                            lblARDate.Text = "";
                            lblARPDate.Text = "";
                        }
                        tbTotalAmount.Enabled = false;
                        tbIBN.Enabled = false;
                        tbStudentAmount.Enabled = true;


                    }
                    else if (count > 1)
                    {
                        tbDDNumber.Visible = false;
                        lnkbtnFDCDetails.Visible = false;
                        PopulateDDList.populateSameInsByIno(tbDDNumber.Text, ddlistPaymentMode.SelectedItem.Value, ddlistIns);
                        ddlistIns.Visible = true;
                        lblNewDD.Visible = false;

                        ddlistDDDay.Enabled = false;
                        ddlistDDMonth.Enabled = false;
                        ddlistDDYear.Enabled = false;
                        tbTotalAmount.Enabled = false;
                        tbIBN.Enabled = false;
                        tbStudentAmount.Enabled = false;

                    }
                    setAccountSession();
                }
                else
                {

                    lblNewDD.Text = error;
                    lblNewDD.Visible = true;
                    btnSubmit.Visible = false;
                }
           
        }

        private void setAccountSession()
        {
            if (lblARPDate.Text == "" || lblARPDate.Text == "1900-01-01")
            {
                ddlistAcountsSession.Enabled = true;
            }
            else
            {
                ddlistAcountsSession.SelectedItem.Selected = false;
                string asess = Accounts.findAccountSessionByARDate(lblARPDate.Text);
                ddlistAcountsSession.Items.FindByText(asess).Selected = true;
                ddlistAcountsSession.Enabled = false;
            }
        }
       
        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            if (lnkbtnEdit.Text == "Edit")
            {
                ddlistFRDDay.Enabled = true;
                ddlistFRDMonth.Enabled = true;
                ddlistFRDYear.Enabled = true;
                lnkbtnEdit.Text = "Update";
            }
            else if (lnkbtnEdit.Text == "Update")
            {
                ddlistFRDDay.Enabled = false;
                ddlistFRDMonth.Enabled = false;
                ddlistFRDYear.Enabled = false;
                lnkbtnEdit.Text = "Edit";
                setFeeStatus();
            }
        }

        protected void ddlistFeeHead_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlistFeeHead.SelectedItem.Value == "22")
            {
                populateStreams();
                setStreams();
                pnlStream.Visible = true;
                btnFind.Visible = true;
                pnlRPeriod.Visible = false;
                pnlDDFee.Visible = false;
                tbCon.Text = "0";
                lblCon.Visible = true;
                tbCon.Visible = true;

            }
            else
            {
                pnlStream.Visible = false;
                lblCon.Visible = false;
                tbCon.Visible = false;

            }


            if (ddlistFeeHead.SelectedItem.Value == "19" && btnFind.Visible == false)
            {
                pnlRPeriod.Visible = true;
                setRenewalPeriod();

            }
            else
            {
                pnlRPeriod.Visible = false;

            }

            setFeeStatus();

               
           
        }

        private void setStreams()
        {
            string allotedstream = "";
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select distinct StreamID from DDECourse where CourseID in (" + FindInfo.findSCAllotedStreams(Convert.ToInt32(tbSCCode.Text)) + ") order by StreamID", con);
            SqlDataReader dr;



            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    if (allotedstream == "")
                    {
                        allotedstream = dr["StreamID"].ToString();
                    }
                    else
                    {
                        allotedstream = allotedstream + "," + dr["StreamID"].ToString();
                    }
                }

            }

            con.Close();

            string[] str = allotedstream.Split(',');

            foreach (DataListItem dli in dtlistStream.Items)
            {
                Label sid = (Label)dli.FindControl("lblSID");
                CheckBox cb = (CheckBox)dli.FindControl("cbStream");

                int pos = Array.IndexOf(str, sid.Text);
                if ((pos > -1))
                {
                    cb.Checked = true;
                    cb.Enabled = false;
                }
                
               

            }
        }

        private void populateStreams()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStreams", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("StreamID");
            DataColumn dtcol3 = new DataColumn("StreamName");
            DataColumn dtcol4 = new DataColumn("StreamFee");
          

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
         

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["StreamID"] = Convert.ToString(dr["StreamID"]);
                drow["StreamName"] = Convert.ToString(dr["StreamName"]);
                drow["StreamFee"] = Convert.ToString(dr["StreamFee"]);
                
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistStream.DataSource = dt;
            dtlistStream.DataBind();

            con.Close();

        }

        protected void ddlistIns_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] dcdetail = Accounts.findInstrumentsDetailsNewByIID(Convert.ToInt32(ddlistIns.SelectedItem.Value));


            lblNewDD.Visible = false;
            ddlistDDDay.SelectedItem.Selected = false;
            ddlistDDMonth.SelectedItem.Selected = false;
            ddlistDDYear.SelectedItem.Selected = false;

            ddlistDDDay.Items.FindByText(dcdetail[0].Substring(8, 2)).Selected = true;
            ddlistDDMonth.Items.FindByValue(dcdetail[0].Substring(5, 2)).Selected = true;
            ddlistDDYear.Items.FindByText(dcdetail[0].Substring(0, 4)).Selected = true;

            tbTotalAmount.Text = dcdetail[1].ToString();
            tbIBN.Text = dcdetail[2].ToString();
            if (dcdetail[2].ToString() != "")
            {
                lblARDate.Text = "Amount Rec. On : " + Convert.ToDateTime(dcdetail[3]).ToString("dd-MM-yyyy");
                lblARPDate.Text = Convert.ToDateTime(dcdetail[3]).ToString("yyyy-MM-dd");
            }
            else
            {
                lblARDate.Text = "";
                lblARPDate.Text = "";
            }

            tbStudentAmount.Enabled = true;
            btnSubmit.Visible = true;
        }

        protected void linkbtnFillCon_Click(object sender, EventArgs e)
        {
            linkbtnFillCon.Visible = false;
            pnlUnlockCon.Visible = true;
            
        }

        protected void btnUnlock_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from SVSUEmployeeRecord where EmployeeID='" + tbUName.Text + "'", con);
            SqlDataReader dr;

          
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                Security s1 = new Security();
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                if (dr["EmployeePassword"].ToString() == s1.EncryptOneWay(tbPass.Text))
                {
                    if (Authorisation.authorised(Convert.ToInt32(dr["ERID"]), 84))
                    {
                        tbCon.Enabled = true;
                        pnlUnlockCon.Visible = false;
                    }
                    else
                    {
                        lblMSGCon.Text = "Sorry !! You are not authorised for this control.";
                    }
                   
                }
                else
                {
                    lblMSGCon.Text = "Sorry !! invalid Password.";
                }
                               

            }
            else
            {
                lblMSGCon.Text = "Sorry !! invalid User Name.";
            }


            con.Close();
           
        }      

    }
}
