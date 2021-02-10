using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class VerificationLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 73))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["VLNo"] != null)
                    {
                        populateVL(Request.QueryString["VLNo"]);
                        populateSC();
                       
                    }
                    else
                    {
                        lblVLNo.Text = FindInfo.findVLNo();
                        lblRefNo.Text = "Ref.No. DDE/SVSU/" + DateTime.Now.ToString("yyyy") + "/02/" + lblVLNo.Text;
                        lblDate.Text = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");

                        populateSC();

                        string fhs;


                        populateIDetails(Session["ins"].ToString(),out fhs);
                        populateFeeHeads(fhs);

                        if (Session["AdjustmentExist"].ToString() == "False")
                        {
                            lblAdjustment.Visible = false;
                        }
                        else if (Session["AdjustmentExist"].ToString() == "True")
                        {

                            lblAdjustment.Text = Session["Adjustment"].ToString();
                        }
                    }
                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 101))
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["VLNo"] != null)
                    {
                        populateVL(Request.QueryString["VLNo"]);
                        populateSC();
                       

                    }
                    
                }

                btnPrint.Visible = false;
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

        private void populateSC()
        {
            string sccode = "0";
            if (Request.QueryString["VLNo"] != null)
            {
                sccode = FindInfo.findSCCodeByVLNo(Convert.ToInt32(Request.QueryString["VLNo"]));

            }
            else
            {
                if (Session["SCMode"].ToString() == "1")
                {
                    sccode = Session["VLSCCode"].ToString();
                }
                else if (Session["SCMode"].ToString() == "0")
                {
                    sccode = FindInfo.findSCCodeByInsNo(Convert.ToInt32(Session["ins"]));
                }

            }
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("SCName");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);

            string[] str = sccode.Split(',');
            int j = 1;
            string auth = "other";

            for (int i = 0; i < str.Length; i++)
            {
                if(str[i]=="001")
                {
                    auth = "001";
                }
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                cmd.CommandText = "select * from DDEStudyCentres where SCCode='"+str[i]+"' order by SCCode";



                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader();

             
                if (dr.HasRows)
                {
                    dr.Read();
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = j;
                    drow["SCCode"] = dr["SCCode"].ToString();
                    drow["SCName"] = dr["Location"].ToString();
                    dt.Rows.Add(drow);
                    j = j + 1;

                      
                   
                }
              
                con.Close();

                

            }

            dtlistSC.DataSource = dt;
            dtlistSC.DataBind();

           
        }

        private void populateVL(string vlno)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from DDEVerificationLetters where VLNo='"+vlno+"'";


            cmd.Connection = con;
            con.Open();

            dr = cmd.ExecuteReader();

            int i = 1;
            if (dr.HasRows)
            {
                dr.Read();
                lblVLNo.Text =vlno;
                lblRefNo.Text = "Ref.No. DDE/SVSU/" + Convert.ToDateTime(dr["FirstTimeOfPrint"]).ToString("yyyy") + "/02/" + lblVLNo.Text;
                lblDate.Text = "Date : " +Convert.ToDateTime(dr["FirstTimeOfPrint"]).ToString("dd/MM/yyyy");

               

                string fhs;


                populateIDetails(dr["IIDS"].ToString(),out fhs);
                populateFeeHeads(fhs);
                if (dr["AdjustmentExist"].ToString() == "True")
                {
                    lblAdjustment.Text = "Amount to be adjusted from Letter No. : " + dr["AdjLNo"].ToString() + " Dated : " + dr["AdjDate"].ToString() + " Amount : " + dr["AdjAmount"].ToString();
                }
                else if (dr["AdjustmentExist"].ToString() == "False")
                {
                    lblAdjustment.Visible = false;
                }
              
               
            }



            con.Close();
        }

      

        private void populateFeeHeads(string fhs)
        {
            
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FHID");
            DataColumn dtcol3 = new DataColumn("FeeHead");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from DDEFeeHead where FHID in ("+fhs+") order by FHID";


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
                    drow["FHID"] = dr["FHID"].ToString();
                    drow["FeeHead"] = dr["FeeHead"].ToString();



                    dt.Rows.Add(drow);
                    i = i + 1;
                }
            }



            dtlistFeeType.DataSource = dt;
            dtlistFeeType.DataBind();

            con.Close();

        }

     

        private void populateIDetails(string iids, out string fhs)
        {
            fhs = "";

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("S.No.");
            DataColumn dtcol2 = new DataColumn("Instrument Type");
            DataColumn dtcol3 = new DataColumn("Instrument No.");
            DataColumn dtcol4 = new DataColumn("Date");
            DataColumn dtcol5 = new DataColumn("Amount");
            DataColumn dtcol6 = new DataColumn("Bank");
            DataColumn dtcol7 = new DataColumn("Remark");

          
          


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

           
            cmd.CommandText = "select * from DDEFeeInstruments where IID in ("+iids+") order by IID";
               

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            int i = 1;
            int total = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["S.No."] = i;
                    drow["Instrument Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr["IType"]));
                    drow["Instrument No."] = Convert.ToString(dr["INo"]);
                    drow["Date"] = Convert.ToDateTime(dr["IDate"]).ToString("dd/MM/yyyy").ToUpper();                  
                    drow["Amount"] = Convert.ToInt32(dr["TotalAmount"])+"/-";
                    total = total + Convert.ToInt32(dr["TotalAmount"]);
                    drow["Bank"] = Convert.ToString(dr["IBN"]);
                    if (Convert.ToInt32(dr["FH11"]) != 0 || Convert.ToInt32(dr["FH31"]) != 0)
                    {
                        drow["Remark"] = FindInfo.findRemark(Convert.ToInt32(dr["Remark"]))+", P";
                    }
                    else
                    {
                        drow["Remark"] = FindInfo.findRemark(Convert.ToInt32(dr["Remark"]));
                    }
                    if (fhs == "")
                    {
                        fhs = Convert.ToString(dr["AllotedFeeHeads"]);
                    }
                    else
                    {
                        string [] sf=fhs.Split(',');
                        string[] str = Convert.ToString(dr["AllotedFeeHeads"]).Split(',');

                        for (int j = 0; j < str.Length; j++)
                        {
                            int pos = Array.IndexOf(sf, str[j]);
                            if (!(pos > -1))
                            {
                                fhs = fhs + "," + str[j];
                            }

                        }

                                          
                    }


                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }



            con.Close();

            gvIDetails.DataSource = dt;
            gvIDetails.DataBind();

            con.Close();

            lblTotal.Text =total.ToString() + "/-";
            //lbTotalInWords.Text = Accounts.IntegerToWords(total).ToUpper();

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (btnPrint.Visible == true)
            {
                int times;
                string lastprinted;
                if (Request.QueryString["VLNo"] != null)
                {
                    if (FindInfo.isVLPrintedByNo(Convert.ToInt32(Request.QueryString["VLNo"]), out times, out lastprinted))
                    {

                        pnlData.Visible = false;
                        lblMSG.Text = "This Instrument Verification Letter has already printed '" + times.ToString() + "' times.<br/>Last printed on '" + lastprinted + "'<br/> Do you want to print this Letter again ?";
                        btnYes.Visible = true;
                        btnNo.Visible = true;
                        pnlMSG.Visible = true;
                        Session["printcounter"] = times;

                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Error : 001. Please contact IT Cell";
                        pnlMSG.Visible = true;
                    }

                }
                else
                {
                    if (!FindInfo.isVLNoAlreadyExist(Convert.ToInt32(lblVLNo.Text)))
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDEVerificationLetters values(@VLNo,@IIDS,@SCMode,@SCCode,@AdjestmentExist,@AdjLNo,@AdjDate,@AdjAmount,@Printed,@FirstTimeOfPrint,@LastTimeOfPrint,@Times)", con);

                        cmd.Parameters.AddWithValue("@VLNo", lblVLNo.Text);
                        cmd.Parameters.AddWithValue("@IIDS", Session["ins"].ToString());
                        if (Session["SCMode"].ToString()=="1")
                        {
                            cmd.Parameters.AddWithValue("@SCMode","True");
                        }
                        else if (Session["SCMode"].ToString() == "0")
                        {
                            cmd.Parameters.AddWithValue("@SCMode", "False");
                        }
                        cmd.Parameters.AddWithValue("@SCCode", findSCCodes());
                        cmd.Parameters.AddWithValue("@AdjestmentExist", Session["AdjustmentExist"].ToString());
                        cmd.Parameters.AddWithValue("@AdjLNo", Session["AdjLNo"].ToString());
                        cmd.Parameters.AddWithValue("@AdjDate", Session["AdjDate"].ToString());
                        if (Session["AdjAmount"].ToString() == "")
                        {
                            cmd.Parameters.AddWithValue("@AdjAmount", "0");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@AdjAmount", Session["AdjAmount"].ToString());
                        }
                        cmd.Parameters.AddWithValue("@Printed", "True");
                        cmd.Parameters.AddWithValue("@FirstTimeOfPrint", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@LastTimeOfPrint", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@Times", 1);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Verification Letter Printed", "Instrument Verification Letter printed with No : " + lblVLNo.Text + " with times : 1", Convert.ToInt32(Session["ERID"].ToString()));

                        btnPrint.Visible = false;

                        updateVLCounter(Convert.ToInt32(lblVLNo.Text));
                        lblPNo.Text = "P-1";
                        lblPNo.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! A Letter is already exist with this no.";
                        btnYes.Visible = false;
                        btnNo.Visible = false;
                        pnlMSG.Visible = true;
                    }
                }
            }
            

        }

        private string findSCCodes()
        {
            string sccode = "";
            foreach (DataListItem dli in dtlistSC.Items)
            {

                
                Label sc = (Label)dli.FindControl("lblSCCode");

                if (sccode == "")
                {

                    sccode = sc.Text;
                }
                else
                {
                    sccode = sccode + "," + sc.Text;
                }

               


            }

            return sccode;
        }

        private void updateVLCounter(int counter)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDECounters set CounterValue=@CounterValue where CounterName='VLCounter' ", con);
            cmd.Parameters.AddWithValue("@CounterValue", counter);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        protected void btnNo_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlMSG.Visible = false;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnPrint.Visible = false;
            lblDate.Text = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");

            string sc=findSCCodes();
            string [] str=sc.Split(',');

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEVerificationLetters set SCMode=@SCMode,SCCode=@SCCode,LastTimeOfPrint=@LastTimeOfPrint,Times=@Times where VLNo='" + lblVLNo.Text + "' ", con);

            if (str.Length ==1)
            {
                cmd.Parameters.AddWithValue("@SCMode", "True");
            }
            else if (str.Length>1)
            {
                cmd.Parameters.AddWithValue("@SCMode", "False");
            }
            cmd.Parameters.AddWithValue("@SCCode",sc);
            cmd.Parameters.AddWithValue("@LastTimeOfPrint", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@Times", (Convert.ToInt32(Session["printcounter"]) + 1));



            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Verification Letter Printed", "Instrument Verification Letter printed with No : " + lblVLNo.Text + "with times : " + (Convert.ToInt32(Session["printcounter"]) + 1).ToString(), Convert.ToInt32(Session["ERID"].ToString()));


            lblPNo.Text = "P-"+(Convert.ToInt32(Session["printcounter"]) + 1).ToString();
            lblPNo.Visible = true;
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                      

           
          }

        
       

       
    }
}