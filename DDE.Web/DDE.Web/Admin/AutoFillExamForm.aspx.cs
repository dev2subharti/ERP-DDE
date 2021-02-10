using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;
using System.Data;
using System.Text;

namespace DDE.Web.Admin
{
    public partial class AutoFillExamForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
           if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 111))
           {
                pnlSearch.Visible = true;                
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            populateTotalInstruments();

        }

        private void populateTotalInstruments()
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("select * from DDEFeeInstruments where INo='" + tbDCNo.Text + "'", con1);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("IID");
            DataColumn dtcol3 = new DataColumn("Type");
            DataColumn dtcol4 = new DataColumn("TypeNo");
            DataColumn dtcol5 = new DataColumn("No");
            DataColumn dtcol6 = new DataColumn("Date");
            DataColumn dtcol7 = new DataColumn("TotalAmount");
            DataColumn dtcol8 = new DataColumn("IBN");
            DataColumn dtcol9 = new DataColumn("Status");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            con1.Open();
            dr1 = cmd1.ExecuteReader();
            int i = 1;
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["IID"] = Convert.ToString(dr1["IID"]);
                    drow["Type"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr1["IType"]));
                    drow["No"] = Convert.ToString(dr1["INo"]);
                    drow["Date"] = Convert.ToDateTime(dr1["IDate"]).ToString("dd MMMM yyyy");
                    drow["TotalAmount"] = Convert.ToInt32(dr1["TotalAmount"]);
                    drow["IBN"] = Convert.ToString(dr1["IBN"]);
                    drow["Status"] = Convert.ToString(dr1["Verified"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

            }
            con1.Close();

            dtlistTotalInstruments.DataSource = dt;
            dtlistTotalInstruments.DataBind();

            if (i > 1)
            {
                dtlistTotalInstruments.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Insrument exist with this no.";
                pnlMSG.Visible = true;
                btnOK.Text = "OK";
                btnOK.Visible = true;
            }
        }

        private void populateDCDetails(int iid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select * from DDEFeeInstruments where IID='" + iid + "'";

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tbDType.Text = Accounts.findMOPByID(Convert.ToInt32(dr["IType"]));
                lblIT.Text = dr["IType"].ToString();
                tbDNo.Text = dr["INo"].ToString();
                lblIID.Text = Convert.ToInt32(dr["IID"]).ToString();
                tbDCDate.Text = Convert.ToDateTime(dr["IDate"]).ToString("dd MMMM yyyy").ToUpper();
                tbIBN.Text = dr["IBN"].ToString().ToUpper();
                tbTotalAmount.Text = Convert.ToInt32(dr["TotalAmount"]).ToString();
                tbSCCode.Text = dr["SCCode"].ToString();

             
            }


            con.Close();


            pnlDCDetail.Visible = true;
            pnlSearch.Visible = false;

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
           
                pnlSearch.Visible = true;
                pnlData.Visible = true;
                pnlDCDetail.Visible = false;
                pnlMSG.Visible = false;
                btnOK.Visible = false;
        }
        

        protected void dtlistTotalInstruments_ItemCommand(object source, DataListCommandEventArgs e)
        {
            populateDCDetails(Convert.ToInt32(e.CommandArgument));
            dtlistTotalInstruments.Visible = false;
           
        }

        protected void btnAutoFillExamForm_Click(object sender, EventArgs e)
        {
                       
            int ecid = FindInfo.findExamCentreBySCCode(tbSCCode.Text, ddlistExam.SelectedItem.Value);
            string amountinwords = Accounts.IntegerToWords(Convert.ToInt32(tbExamFee.Text));
            string asession = "2020-21";
            string error;
            int fhfee = Accounts.findTotalAmountOnFeeHead(2, Convert.ToInt32(lblIT.Text), tbDNo.Text, Convert.ToDateTime(tbDCDate.Text).ToString("yyyy-MM-dd"), tbIBN.Text);
            if (ecid != 0)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand("Select DDEOnlineExamRecord.OERID,DDEOnlineExamRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEOnlineExamRecord.Year as ForYear,DDEStudentRecord.Course from DDEOnlineExamRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEOnlineExamRecord.SRID where DDEOnlineExamRecord.SCCode='" + tbSCCode.Text + "' and DDEOnlineExamRecord.Enrolled='0' and DDEOnlineExamRecord.MOE='R' and DDEOnlineExamRecord.Examination='" + ddlistExam.SelectedItem.Value + "' order by SRID", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                int j = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                  for (int i = 0; j < ds.Tables[0].Rows.Count; i++)
                  {
                    int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
                    int feeoutput = 0;
                    int examoutput = 0;
                    if (validINo(fhfee, Convert.ToInt32(lblIT.Text), tbDNo.Text, Convert.ToDateTime(tbDCDate.Text).ToString("yyyy-MM-dd"), tbIBN.Text, Convert.ToInt32(tbExamFee.Text), out error))
                    {
                        if (!isExamFormEntered(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), ddlistExam.SelectedItem.Value))
                        {
                            feeoutput = fillFee(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), ddlistExam.SelectedItem.Value, asession, tbDNo.Text, Convert.ToInt32(lblIT.Text), Convert.ToDateTime(tbDCDate.Text).ToString("yyyy-MM-dd"), tbIBN.Text, Convert.ToInt32(tbTotalAmount.Text), Convert.ToInt32(tbExamFee.Text), amountinwords, ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                            if (feeoutput == 1)
                            {
                                examoutput = fillExamForm(srid, Convert.ToInt32(ds.Tables[0].Rows[i]["ForYear"]), Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), ddlistExam.SelectedItem.Value, ecid, "R");
                            }

                        }
                        if (feeoutput != examoutput)
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Operation failed @ : " + srid.ToString();
                            pnlMSG.Visible = true;
                            btnOK.Text = "OK";
                            btnOK.Visible = true;
                            break;
                            
                        }
                        else if (feeoutput == 1 && examoutput == 1)
                        {
                            updateEnrolled(Convert.ToInt32(ds.Tables[0].Rows[i]["OERID"]));
                            j = j + 1;
                        }
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Total Form Feeded : " + j.ToString()+ "<br/> Sorry!! remaining amount on this fee head is less than required amount";
                        pnlMSG.Visible = true;
                        btnOK.Text = "OK";
                        btnOK.Visible = true;
                        break;
                    }
                  }
                    

                    pnlData.Visible = false;
                    lblMSG.Text = "Total Form Feeded : " + j.ToString();
                    pnlMSG.Visible = true;
                    btnOK.Text = "OK";
                    btnOK.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! No Pending Exam Form Found";
                    pnlMSG.Visible = true;
                    btnOK.Text = "OK";
                    btnOK.Visible = true;
                }



            }
            else
            {
                Response.Write("Sorry !! Exam Center is not allotted till yet");
            }
        }

        private bool validINo(int fhfee, int itype, string ino, string idate, string ibn, int cfee, out string error)
        {
            error = "";
            bool valid = false;
            int fhusedfee = Accounts.findUsedAmountOfDraftByFH(2, itype, ino, idate, ibn);

            int reaminfhfee = (fhfee - fhusedfee);

            if (reaminfhfee >= cfee)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }


            return valid;
        }
        private void updateEnrolled(int OERID)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEOnlineExamRecord set Enrolled=@Enrolled,EnrolledOn=@EnrolledOn where OERID='" + OERID + "' ", con);
            cmd.Parameters.AddWithValue("@Enrolled", "True");
            cmd.Parameters.AddWithValue("@EnrolledOn", DateTime.Now.ToString());

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

     

        private int fillExamForm(int srid, int year, int cid, string exam, int ecid, string moe)
        {
        start:
            int examoutput = 0;

            int counter = 0;

            string rollno = allotRollNo(srid, cid, exam, moe, out counter);

            if (!FindInfo.isRollNoAlreadyExist(rollno, exam))
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDEExamRecord_" + exam + " values(@SRID,@Year,@RollNo,@BPSubjects1,@BPSubjects2,@BPSubjects3,@BPPracticals1,@BPPracticals2,@BPPracticals3,@VECID,@ExamCentreCode,@ExamCentreCity,@ExamCentreZone,@MaxMarks,@ObtMarks,@QualifyingStatus,@MOE,@MSPrinted,@Times,@LastPrintTime,@MSCounter)", con);

                cmd.Parameters.AddWithValue("@SRID", srid);
                cmd.Parameters.AddWithValue("@Year", year);
                cmd.Parameters.AddWithValue("@RollNo", rollno);
                cmd.Parameters.AddWithValue("@BPSubjects1", "");
                cmd.Parameters.AddWithValue("@BPSubjects2", "");
                cmd.Parameters.AddWithValue("@BPSubjects3", "");
                cmd.Parameters.AddWithValue("@BPPracticals1", "");
                cmd.Parameters.AddWithValue("@BPPracticals2", "");
                cmd.Parameters.AddWithValue("@BPPracticals3", "");
                cmd.Parameters.AddWithValue("@VECID", 0);              
                cmd.Parameters.AddWithValue("@ExamCentreCode", ecid);
                cmd.Parameters.AddWithValue("@ExamCentreCity", "");
                cmd.Parameters.AddWithValue("@ExamCentreZone", "");
                cmd.Parameters.AddWithValue("@MaxMarks", 0);
                cmd.Parameters.AddWithValue("@ObtMarks", 0);
                cmd.Parameters.AddWithValue("@QualifyingStatus", "");
                cmd.Parameters.AddWithValue("@MOE", moe);
                cmd.Parameters.AddWithValue("@MSPrinted", "False");
                cmd.Parameters.AddWithValue("@Times", 0);
                cmd.Parameters.AddWithValue("@LastPrintTime", "");
                cmd.Parameters.AddWithValue("@MSCounter", 0);

                cmd.Connection = con;
                con.Open();
                examoutput = cmd.ExecuteNonQuery();
                con.Close();

                if (examoutput == 1 && counter != 0)
                {
                    FindInfo.updateRollNoCounter(cid, counter, exam);
                }

            }
            else
            {
                FindInfo.updateRollNoCounter(cid, counter, exam);
                goto start;

            }

            return examoutput;

        }

        private string allotRollNo(int srid, int cid, string exam, string moe, out int counter)
        {
            counter = FindInfo.findRollNoCounter(cid, exam, moe);
            string rollno = exam + FindInfo.findCourseCodeByID(cid) + string.Format("{0:0000}", counter);
            return rollno;
        }

        private int fillFee(int srid, int year, string exam, string asession, string ino, int itype, string idate, string ibn, int iamount, int amount, string amountinwords, string eno)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into [DDEFeeRecord_" + asession + "] values(@OFRID,@SRID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@TotalDCAmount,@ForYear,@ForExam,@FRDate,@TOFS,@Verified,@VerifiedOn,@VerifiedBy,@EntryType)", con);

            cmd.Parameters.AddWithValue("@OFRID", 0);
            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@FeeHead", 2);
            cmd.Parameters.AddWithValue("@PaymentMode", itype);
            cmd.Parameters.AddWithValue("@DCNumber", ino);
            cmd.Parameters.AddWithValue("@DCDate", idate);
            cmd.Parameters.AddWithValue("@IBN", ibn);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@AmountInWords", amountinwords.ToUpper());
            cmd.Parameters.AddWithValue("@TotalDCAmount", iamount);
            cmd.Parameters.AddWithValue("@ForYear", year);
            cmd.Parameters.AddWithValue("@ForExam", exam);
            cmd.Parameters.AddWithValue("@FRDate", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@Verified", "False");
            cmd.Parameters.AddWithValue("@VerifiedOn", "");
            cmd.Parameters.AddWithValue("@VerifiedBy", 0);
            cmd.Parameters.AddWithValue("@EntryType", 1);

            cmd.Connection = con;
            con.Open();
            int output = cmd.ExecuteNonQuery();
            con.Close();
            if(output>0)
            {
                Log.createLogNow("Fee Submit", "Filled Main Exam Fee of a student with Enrollment No '" + eno + "' for Exam '"+ddlistExam.SelectedItem.Text+"'", Convert.ToInt32(Session["ERID"].ToString()));
            }
            return output;
        }

        private bool isExamFormEntered(int srid, int year, string exam)
        {

            bool exist = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_" + exam + " where SRID='" + srid + "' and Year='" + year + "' and MOE='R'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                exist = true;
            }
            else
            {
                exist = false;
            }

            con.Close();
            return exist;
        }
    }
}