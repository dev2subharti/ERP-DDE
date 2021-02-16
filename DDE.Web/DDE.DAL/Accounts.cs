using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;

namespace DDE.DAL
{
    public class Accounts
    {
        public static bool validFee(int srid,int cid, int fhid, string asession, int paymentmode, string dcno, string dcday, string dcmonth, string dcyear, string ibn, int amount, string amountinwords, int totalamount, int year, string exam,int totalbpsub,int erid,string batch,string frdate, int et,int count,int iid,string sccode, out string error)
        {
            bool valid = false;
            error = "";
            string dcdate = "";

            if (fhid == 6 || fhid == 8 || fhid == 9 || fhid == 10 || fhid == 11 || fhid == 12 || fhid == 14 || fhid == 18 || fhid == 23 || fhid == 27 || fhid == 28 || fhid == 29 || fhid == 30 || fhid == 34 || fhid == 36 || fhid == 38 || fhid == 39 || fhid == 43 || fhid == 44 || fhid == 45 || fhid == 46 || fhid == 47 || fhid == 48 || fhid == 49 || fhid == 50 || fhid == 53 || fhid == 54 || fhid == 55 || fhid == 56 || fhid == 57 || fhid == 58 || fhid == 59 || fhid == 60 || fhid == 61 || fhid == 64 || fhid == 65)
            {
                exam = "NA";
            }
            
            dcdate = dcyear + "-" + dcmonth + "-" + dcday;
           
            if (!feeAlreadyExist(srid,cid, asession, fhid, year, exam,totalbpsub,batch,frdate))
            {
                if (validAmount(srid, cid, fhid, amount, year, exam, totalbpsub, erid, batch, frdate,dcno,dcdate,ibn,paymentmode, et,count,iid, out error))
                {
                    if (validDCDetail(paymentmode, asession,amount, dcno, dcdate, ibn, totalamount,et,count,iid, out error))
                    {
                        if (et == 1 || et == 3)
                        {
                            string[] stu = FindInfo.findStudentSCDetails(srid);

                            if (Accounts.validSCCode(iid, sccode, Convert.ToBoolean(stu[0]), stu[1], out error))
                            {
                                valid = true;
                            }
                            else
                            {
                                valid = false;

                            }
                        }
                        else
                        {
                            valid = true;
                        }
                       
                    } 
                       
                }
                else
                {
                    valid = false;
                   
                }


            }
            else
            {
                valid = false;
                if (fhid == 2 || fhid == 3)
                {
                    string rollno="NF";
                    string moe="NF";
                    int ercounter=0;
                    if (fhid == 2)
                    {
                        moe = "R";
                    }
                    else if (fhid == 3)
                    {
                        moe = "B";
                    }
                    findAllotedRollNoAndCounter(srid, exam, moe, out rollno,out ercounter);
                    error = "This fee is already entered <br/> Allotted Roll is : "+rollno+"<br/> Alloted Counter is : "+ercounter.ToString();
                }
                else
                {
                    error = "Sorry !! this fee is already entered";
                }
            }

            return valid;
                  

        }

        private static bool validSCCode(int iid, string currentsccode, bool trans, string presccode, out string error)
        {
            bool valid = false;
            error = "NF";
           
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where IID='" + iid + "'", con);

            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    if (trans == true)
                    {
                        string[] str = dr["SCCode"].ToString().Split(',');
                        int pos = Array.IndexOf(str, presccode);
                        if (pos > -1 && (isSCEligibleForTransfer(currentsccode)))
                        {
                            if (dr["Received"].ToString() == "True")
                            {
                                if (dr["Verified"].ToString() == "True")
                                {
                                    if (dr["AmountAlloted"].ToString() == "True")
                                    {
                                        valid = true;
                                        iid = Convert.ToInt32(dr["IID"]);
                                       
                                    }
                                    else if (dr["AmountAlloted"].ToString() == "False")
                                    {
                                        error = "Sorry !!The amount of instrument is not distributed till yet.";
                                    }
                                }
                                else if (dr["Verified"].ToString() == "False")
                                {
                                    error = "Sorry !! This instrument is not verified.";
                                }
                            }
                            else if (dr["Received"].ToString() == "False")
                            {
                                error = "Sorry !! This instrument is not received.";
                            }


                        }
                        else
                        {

                            error = "Sorry !! The SC Code of Student does not match to the SC Code of instrument";

                        }
                    }
                    else
                    {
                        string[] str = dr["SCCode"].ToString().Split(',');
                        int pos = Array.IndexOf(str, currentsccode);
                        if (pos > -1)
                        {
                            if (dr["Received"].ToString() == "True")
                            {
                                if (dr["Verified"].ToString() == "True")
                                {
                                    if (dr["AmountAlloted"].ToString() == "True")
                                    {
                                        valid = true;
                                        iid = Convert.ToInt32(dr["IID"]);
                                      
                                    }
                                    else if (dr["AmountAlloted"].ToString() == "False")
                                    {
                                        error = "Sorry !!The amount of instrument is not distributed till yet.";
                                    }
                                }
                                else if (dr["Verified"].ToString() == "False")
                                {
                                    error = "Sorry !! This instrument is not verified.";
                                }
                            }
                            else if (dr["Received"].ToString() == "False")
                            {
                                error = "Sorry !! This instrument is not received.";
                            }


                        }
                        else
                        {

                            error = "Sorry !! The SC Code of Student does not match to the SC Code of instrument";

                        }
                    }
                }
            }
            else
            {

                error = "Sorry !! There is no instrument with this no.";
            }


            con.Close();


            return valid;
        }
       
        public static bool feePaid(int srid, int year, int fhid, string exam, out string forexam)
        {
            bool fp=false;
            forexam = "NA";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr1;

                    if (fhid == 1)
                    {
                        cmd1.CommandText = "select ForExam from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForYear='" + year + "'";
                    }
                    else
                    {
                        cmd1.CommandText = "select ForExam from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForYear='" + year + "' and ForExam='" + exam + "'";
                    }
                   

                    cmd1.Connection = con1;
                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            fp=true;
                            forexam = dr1[0].ToString();
                            break;
                        }

                    }

                    con1.Close();

             }

            con.Close();
            
           
            return fp;   
        }

        private static void findAllotedRollNoAndCounter(int srid, string exam, string moe, out string rollno, out int ercounter)
        {
            rollno = "NF";
            ercounter = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select ExamRecordID,RollNo from DDEExamRecord_"+exam+" where SRID='" + srid+ "' and MOE='"+moe+"'", con);
            SqlDataReader dr;

           
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                ercounter = Convert.ToInt32(dr["ExamRecordID"]);
                rollno = dr["RollNo"].ToString();

            }

            con.Close();

        }

        public static void fillFee(int srid, int fhid, string asession, int paymentmode, string dcno, string dcday, string dcmonth, string dcyear, string ibn, int amount, string amountinwords, int totalamount, int year, string exam, string frdate, int et)
        {
            if (fhid == 6 || fhid == 8 || fhid == 9 || fhid == 10 || fhid == 11 || fhid == 12 || fhid == 14 || fhid == 18 || fhid == 23 || fhid == 27 || fhid == 28 || fhid == 29 || fhid == 30 || fhid == 34 || fhid == 36 || fhid == 38 || fhid == 39 || fhid == 43 || fhid == 44 || fhid == 45 || fhid == 46 || fhid == 47 || fhid == 48 || fhid == 49 || fhid == 50 || fhid == 53 || fhid == 54 || fhid == 55 || fhid == 56 || fhid == 57 || fhid == 58 || fhid == 59 || fhid == 60 || fhid == 61 || fhid == 64 || fhid == 65)
            {
                exam = "NA";
            }
            
            string dcdate = dcyear + "-" + dcmonth + "-" + dcday;

           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into [DDEFeeRecord_" + asession + "] values(@OFRID,@SRID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@TotalDCAmount,@ForYear,@ForExam,@FRDate,@TOFS,@Verified,@VerifiedOn,@VerifiedBy,@EntryType)", con);

            cmd.Parameters.AddWithValue("@OFRID", 0);
            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@FeeHead", fhid);
            cmd.Parameters.AddWithValue("@PaymentMode", paymentmode);
            cmd.Parameters.AddWithValue("@DCNumber", dcno.ToUpper().Trim());
            cmd.Parameters.AddWithValue("@DCDate", dcdate);
            cmd.Parameters.AddWithValue("@IBN", ibn.ToUpper());
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@AmountInWords", amountinwords.ToUpper());
            cmd.Parameters.AddWithValue("@TotalDCAmount", totalamount);
            cmd.Parameters.AddWithValue("@ForYear", year);
            cmd.Parameters.AddWithValue("@ForExam", exam);
            cmd.Parameters.AddWithValue("@FRDate", frdate);
            cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
          
           
            cmd.Parameters.AddWithValue("@Verified", "True");
            cmd.Parameters.AddWithValue("@VerifiedOn","");
            cmd.Parameters.AddWithValue("@VerifiedBy", 0);
           

            cmd.Parameters.AddWithValue("@EntryType", et);

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private static bool verifiedDraft(string dcno,out string vo, out int vb)
        {
            vo = "NF";
            vb = 0;
            bool ver = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader dr1;


                cmd1.CommandText = "select Verified,VerifiedOn,VerifiedBy from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where DCNumber='" + dcno + "'";


                cmd1.Connection = con1;
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    if (dr1["Verified"].ToString() == "True")
                    {
                        ver = true;
                        vo = dr1["VerifiedOn"].ToString();
                        vb = Convert.ToInt32(dr1["VerifiedBy"]);
                        break;
                    }                   

                }

                con1.Close();

            }

            con.Close();

            return ver;


        }

        public static bool feeAlreadyExist(int srid,int cid, string asession, int feehead, int year, string exam, int totalsub,string batch, string frdate)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand(findCommand(srid,feehead, year,exam), con);
            SqlDataReader dr;

            bool exist = false;
            int counter = 0;
            con.Open();
            dr = cmd.ExecuteReader();
            int totalinst;
            if (instalableFee(feehead,out totalinst))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        counter = counter + 1;
                    }

                }

                if (counter >= totalinst)
                {
                    exist = true;
                }
                else 
                {
                   
                    if(findPreviousPaidFee(srid,cid,feehead,year,exam)>=findRequiredFee(srid,cid, feehead,totalsub,batch, frdate))
                    {
                       exist = true;
                    }
                }
            }

            else
            {
                if (dr.HasRows)
                {
                    exist = true;
                }
            }
           

            con.Close();

            if (isMultipleRecFeeHead(feehead))
            {
                exist = false;
            }
            

            return exist;
        }

        private static bool isMultipleRecFeeHead(int feehead)
        {
            bool mr = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select MultipleRecAllTime from DDEFeeHead where FHID='" + feehead + "'", con);
            SqlDataReader dr;

           
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (Convert.ToString(dr["MultipleRecAllTime"]) == "True")
                {
                    mr = true;                  
                }

            }

            con.Close();

            return mr;
        }

        private static string findCommand(int srid,int feehead, int year, string exam)
        {
            string cm = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEAcountSession", con);
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            DataSet ds=new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (cm == "")
                {
                    cm = "select * from [DDEFeeRecord_" + ds.Tables[0].Rows[i]["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForYear='" + year + "' and ForExam='" + exam + "'";
                }
                else
                {
                    cm = cm + " union " + "select * from [DDEFeeRecord_" + ds.Tables[0].Rows[i]["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForYear='" + year + "' and ForExam='" + exam + "'";
                }
               
                
               
            }

            con.Close();
            
            return cm;

        }

        private static bool instalableFee(int feehead, out int totalinst)
        {
            totalinst=0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeHead where FHID='" + feehead + "'", con);
            SqlDataReader dr;

            bool ifee = false;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (Convert.ToInt32(dr["Instalable"]) == 1)
                {
                    ifee = true;
                    totalinst = Convert.ToInt32(dr["Installments"]);
                }
               
            }

            con.Close();

            return ifee;
            
        }

        private static int findNoOfInstallments(int fhid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select Installments from DDEFeeHead where FHID='" + fhid + "'", con);
            SqlDataReader dr;

            int inst = 0;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                inst = Convert.ToInt32(dr[0]);
            }

            con.Close();
            return inst;
        }

        public static int findPreviousPaidFee(int srid,int cid, int fhid, int year, string exam)
        {
            int feepaid = 0;
            if (fhid == 6 || fhid == 8 || fhid == 9 || fhid == 10 || fhid == 11 || fhid == 12 || fhid == 14 || fhid == 18 || fhid == 23 || fhid == 27 || fhid == 28 || fhid == 29 || fhid == 30 || fhid == 34 || fhid == 36 || fhid == 38 || fhid == 39 || fhid == 43 || fhid == 44 || fhid == 45 || fhid == 46 || fhid == 47 || fhid == 48 || fhid == 49 || fhid == 50 || fhid == 53 || fhid == 54 || fhid == 55 || fhid == 56 || fhid == 57 || fhid == 58 || fhid == 59 || fhid == 60 || fhid == 61 || fhid == 64 || fhid == 65)
            {
                exam = "NA";
            }
            if (srid != 0)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr1;

                    if (fhid == 1)
                    {
                        if (year == 5)
                        {
                            cmd1.CommandText = "select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "'";
                        }
                        else
                        {
                            cmd1.CommandText = "select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForYear='" + year + "'";
                        }
                    }
                    else
                    {
                        if (year == 5)
                        {
                            cmd1.CommandText = "select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForExam='" + exam + "'";
                        }
                        else
                        {
                            cmd1.CommandText = "select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForYear='" + year + "' and ForExam='" + exam + "'";
                        }

                    }

                    cmd1.Connection = con1;
                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            feepaid = feepaid + Convert.ToInt32(dr1["Amount"]);
                        }

                    }

                    con1.Close();

                }

                con.Close();
            }
           
            return feepaid;        
        }

        public static int findPreviousSCPaidFee(int scid,int fhid,int year)
        {
            int feepaid=0;
            
               
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();
            SqlDataReader dr1;

                   
            cmd1.CommandText = "select Amount from DDESCFeeRecord where SCID='" + scid + "' and FeeHead='" + fhid + "'";
                       

            cmd1.Connection = con1;
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    feepaid = feepaid + Convert.ToInt32(dr1["Amount"]);
                }

            }

            con1.Close();

            if (fhid == 31)
            {
                feepaid = 0;
            }

            return feepaid;
        }

        public static int findTotalDCAmount(string asession,int ptype, string dcno, string dcdate,string ibn)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select TotalDCAmount from [DDEFeeRecord_" + asession + "] where PaymentMode='" + ptype + "' and DCNumber='" + dcno + "' and DCDate='" + dcdate + "' and IBN='" + ibn + "'", con);
            SqlDataReader dr;

            int tdca = 0;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tdca = Convert.ToInt32(dr[0]);
            }

            con.Close();

            return tdca;
        }

        public static bool validDCDetail(int ptype, string asession,int amount, string dcnumber,string dcdate,string ibn,int totalamount, int et,int count,int iid, out string error)
        {
            bool valid = false;
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if(et==2)
            {
                cmd.CommandText="select distinct DCNumber,DCDate,IBN,TotalDCAmount from [DDEFeeRecord_" + asession + "] where PaymentMode='" + ptype + "' and DCNumber='" + dcnumber + "'";
            }
            else if (et == 1 || et == 3)
            {
                if (ptype == 3)
                {
                    ibn = "NA";
                }
                if (count == 1)
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where IType='" + ptype + "' and INo='" + dcnumber + "'";
                }
                else if (count > 1)
                {
                    cmd.CommandText = "select * from DDEFeeInstruments where IID='" + iid + "'";
                }
            }

            SqlDataReader dr;
            error = "";
          
            int counter = 0;
            string[] dcdetail = { "", "", "","" };

            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();
           
                if (et == 2)
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (ibn.ToUpper() == dr[2].ToString())
                            {
                                dcdetail[0] = dr[0].ToString();
                                dcdetail[1] = Convert.ToDateTime(dr[1].ToString()).ToString("yyyy-MM-dd");
                                dcdetail[2] = dr[2].ToString();
                                dcdetail[3] = Convert.ToInt32(dr[3]).ToString();

                            }
                            counter = counter + 1;

                        }
                    }
                   
                }
                else if (et == 1 || et == 3)
                {
                    if (dr.HasRows)
                    {
                        dr.Read();

                        dcdetail[0] = dr["INo"].ToString();
                        dcdetail[1] = Convert.ToDateTime(dr["IDate"].ToString()).ToString("yyyy-MM-dd");
                        dcdetail[2] = dr["IBN"].ToString();
                        dcdetail[3] = Convert.ToInt32(dr["TotalAmount"]).ToString();



                        if (dr["Received"].ToString() == "True")
                        {
                            if (dr["Verified"].ToString() == "True")
                            {
                                if (dr["AmountAlloted"].ToString() == "True")
                                {

                                }
                                else
                                {
                                    error = "Sorry !! Amount of this instrument is not distributed till yet. Please distribute the amout of instrument.";
                                }

                            }
                            else
                            {
                                error = "Sorry !! Instrument is not verified till yet. Please verify the instrument.";
                            }

                        }
                        else
                        {
                            error = "Sorry !! No Instrument received with this no.";
                        }
                    }
                    else
                    {
                        error = "Sorry !! No Instrument exist with this no.";
                    }

               
                
               
            }

            con.Close();


            //if (ptype == 1 || ptype == 2)
            //{
                
                int usedamount=findUsedAmountOfDraft(ptype,dcdate,dcnumber,ibn);
                int remamount = (totalamount - usedamount);

                if (et == 2)
                {
                    if (amount <= remamount)
                    {
                        if (counter == 0)
                        {
                            valid = true;
                        }
                        else
                        {
                            if (dcdate == dcdetail[1].ToString() && ibn.ToUpper() == dcdetail[2].ToString() && totalamount == Convert.ToInt32(dcdetail[3].ToString()))
                            {
                                valid = true;
                            }
                            else if (dcdetail[0] == "")
                            {
                                error = "Sorry !! invalid Instrument Details.Please Check Issuing Bank Name and other entries are correct or not";
                            }
                            else
                            {
                                error = "Sorry !! invalid Instrument Details. Previous Filled Details were </br>Date-" + Convert.ToDateTime(dcdetail[1].ToString()).ToString("dd MMMM yyyy") + "</br>Issuing Bank Name-" + dcdetail[2].ToString() + "</br>Total Amount-" + dcdetail[3].ToString();
                            }
                        }
                    }

                    else
                    {
                        error = "Sorry !! amount is more than remaining amount of Instrument </br></br> Total used amount is : " + usedamount.ToString() + "</br> Remaining amount is : " + remamount.ToString();
                    }
                    //}

                    //else
                    //{
                    //    if (dcnumber!=dcdetail[0].ToString())
                    //    {
                    //        valid = true;
                    //    }
                    //    else
                    //    {
                    //        error = "Sorry !! this Receipt No. is already exist";
                    //    }

                    //}

                    if (ptype == 5 || ptype == 6)
                    {
                        valid = true;
                    }
                }
                else if (et == 1 || et == 3)
                {
                    if (error == "")
                    {
                        if (amount <= remamount)
                        {
                           
                                if (dcdate == dcdetail[1].ToString() && ibn.ToUpper() == dcdetail[2].ToString() && totalamount == Convert.ToInt32(dcdetail[3].ToString()))
                                {
                                    valid = true;
                                }
                                else
                                {
                                    error = "Sorry !! invalid Instrument Details.Correct Details with this instrument no. are </br>Date-" + Convert.ToDateTime(dcdetail[1].ToString()).ToString("dd MMMM yyyy") + "</br>Issuing Bank Name-" + dcdetail[2].ToString() + "</br>Total Amount-" + dcdetail[3].ToString();
                                }
                           
                        }

                        else
                        {
                            error = "Sorry !! amount is more than remaining amount of Instrument </br></br> Total used amount is : " + usedamount.ToString() + "</br> Remaining amount is : " + remamount.ToString();
                        }
                    }
                    else
                    {
                        valid = false;
                    }

                    
                        
                }

           
            return valid;

        }

        private static bool validAmount(int srid,int cid, int fhid,int amount,int year, string exam, int totalbpsub,int erid, string batch, string frdate,string ino, string dcdate,string ibn, int pm, int et,int count,int iid, out string error)
        {
            error = "NF";
            bool valid = false;
            int reqfee = 0;
            int paidfee = 0;
            if (fhid == 19 || fhid == 20 || fhid == 21 || fhid == 22 || fhid == 28 || fhid == 31 || fhid == 39)
            {
                reqfee = findRequiredSCFee(srid,fhid,totalbpsub,frdate);
                paidfee = amount + findPreviousSCPaidFee(srid,fhid,year);
            }
            else
            {
                reqfee = findRequiredFee(srid, cid, fhid, totalbpsub, batch, frdate);
                paidfee = amount + findPreviousPaidFee(srid, cid, fhid, year, exam);
            }
            if (et == 2)
            {
                int totalinst;

                if (Authorisation.authorised(erid, 63))
                {

                    valid = true;

                }

                else if (instalableFee(fhid, out totalinst))
                {
                    if (fhid == 1)
                    {
                        if ((paidfee >= (reqfee / 2)) && (paidfee <= reqfee))
                        {
                            valid = true;
                        }
                        else
                        {
                            error = "Sorry !! Amount should be equal or more than " + (reqfee / 2).ToString() + " in first installment";
                        }
                    }
                    else
                    {
                        if (isMultipleRecFeeHead(fhid))
                        {
                            if ((paidfee >= 0) && (amount <= reqfee))
                            {
                                valid = true;
                            }
                            else
                            {
                                error = "Sorry !! Not a valid amount";
                            }
                        }
                        else
                        {
                            if ((paidfee >= 0) && (paidfee <= reqfee))
                            {
                                valid = true;
                            }
                            else
                            {
                                error = "Sorry !! Not a valid amount";
                            }
                        }
                    }
                }
                else
                {
                    if (amount == reqfee)
                    {
                        valid = true;
                    }
                    else
                    {
                        error = "Sorry !! Not a valid amount";
                    }

                }
                if (fhid == 16 || fhid == 23)
                {
                    valid = true;
                }
            }
            else if (et == 1 || et == 3)
            {
                int fhfee = 0;

                if (count == 1)
                {
                    fhfee = findTotalFHFee(fhid, pm, ino, dcdate, ibn);
                }
                else if (count > 1)
                {
                    fhfee = findTotalFHFeeByIID(fhid,iid);
                }


                int fhusedfee = findUsedAmountOfDraftByFH(fhid,pm,ino,dcdate,ibn); ;
                            
                int reaminfhfee = (fhfee - fhusedfee);
                int totalinst;
                if (reaminfhfee >= amount)
                {
                    if (Authorisation.authorised(erid, 63))
                    {

                        valid = true;

                    }

                    else if (instalableFee(fhid, out totalinst))
                    {
                        if (fhid == 1)
                        {
                            if ((paidfee >= (reqfee / 2)) && (paidfee <= reqfee))
                            {
                                valid = true;
                            }
                            else
                            {
                                error = "Sorry !! Amount should be equal or more than " + (reqfee / 2).ToString()+" in first installment";
                            }
                        }
                        else
                        {
                            if ((paidfee >= 0) && (amount <= reqfee))
                            {
                                valid = true;
                            }
                            else
                            {
                                error = "Sorry !! Not a valid amount";
                            }
                        }
                    }
                    else
                    {
                        if (amount == reqfee)
                        {
                            valid = true;
                        }
                        else
                        {
                            error = "Sorry !! Not a valid amount";
                        }

                    }

                    if (fhid == 16 || fhid == 23)
                    {
                        valid = true;
                    }

                   
     
                }
                else
                {
                    error = "Sorry !! Filled amount is " + amount + " but the remaining amount on this fee head of this instrument is " + reaminfhfee.ToString();
                }

            }

          
            return valid;
            
        }

        public static int findTotalFHFeeByIID(int fhid, int iid)
        {
            int tf = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select FH" + fhid + " from DDEFeeInstruments where IID='" + iid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tf = Convert.ToInt32(dr[0]);


            }

            con.Close();

            return tf;
        }

        public static int findTotalFHFee(int fhid,int pm,string ino,string idate,string ibn )
        {
            int tf = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select FH"+fhid+" from DDEFeeInstruments where IType='"+pm+"' and INo='" + ino + "' and IDate='" + idate + "' and IBN='"+ibn+"'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tf = Convert.ToInt32(dr[0]); 
                    
               
            }

            con.Close();

            return tf;
        }

        public static int findUsedAmountOfDraftByFH(int fhid,int pm,string ino,string dcdate,string ibn)
        {
            int usedamount=0;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr1;

                   
                    cmd1.CommandText = "select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where FeeHead='"+fhid+"' and DCNumber='" + ino + "' and DCDate='"+dcdate+"' and PaymentMode='" + pm + "' and IBN='"+ibn+"'";
                        
                    

                    cmd1.Connection = con1;
                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            usedamount = usedamount + Convert.ToInt32(dr1["Amount"]);
                        }

                    }

                    con1.Close();

                }

                con.Close();

                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr2;
                SqlCommand cmd2 = new SqlCommand("select Amount from DDESCFeeRecord where FeeHead='"+fhid+"' and DCNumber='" + ino + "' and DCDate='"+dcdate+"' and PaymentMode='" + pm + "' and IBN='"+ibn+"'", con2);
                con2.Open();
                dr2 = cmd2.ExecuteReader();

                if (dr2.HasRows)
                {
                    while (dr2.Read())
                    {

                        usedamount = usedamount + Convert.ToInt32(dr2["Amount"]);
                    }
                }

                con2.Close();


                SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr3;
                SqlCommand cmd3 = new SqlCommand("select Amount from DDEOtherFeeRecord where FeeHead='" + fhid + "' and DCNumber='" + ino + "' and DCDate='" + dcdate + "' and PaymentMode='" + pm + "' and IBN='" + ibn + "'", con3);
                con3.Open();
                dr3 = cmd3.ExecuteReader();

                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {

                        usedamount = usedamount + Convert.ToInt32(dr3["Amount"]);
                    }
                }

                con3.Close();
           
           
            return usedamount;       
        }
    
        public static int findRequiredFee(int srid,int cid, int fhid, int totalsub, string batch, string frdate)
        {
            int rfee = 0;
            string bt = FindInfo.findSessionCodeByID(FindInfo.findBatchID(batch));

            if (fhid == 1)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select "+bt+" from DDECourse where CourseID='" + cid + "'", con);
                SqlDataReader dr;

        
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();                   
                    rfee = Convert.ToInt32(dr[0]);
                     
                }

                con.Close();             

            }
            

            else
            {
                
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from DDERequiredFeeRecord where FHID='" + fhid + "' and TPFrom<='"+frdate+"' and TPTo>='"+frdate+"'", con);
                SqlDataReader dr;


                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["RequiredFee"].ToString() != "")
                    {
                        rfee = Convert.ToInt32(dr["RequiredFee"]);
                    }
                }

                con.Close();

            }

            if (fhid == 3)
            {
                rfee = rfee * totalsub;
            }
            
            if((fhid==2) && (batch=="A 2020-21" || batch=="Q 2020-21" || batch=="C 2021" || batch=="Q 2021"))
            {
                rfee = 750;
            }

            return rfee;
        }

        public static int findRequiredSCFee(int srid,int fhid, int tp,string frdate)
        {

            int rfee = 0;
         

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDERequiredFeeRecord where FHID='" + fhid + "' and TPFrom<='" + frdate + "' and TPTo>='" + frdate + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["RequiredFee"].ToString() != "")
                {
                    rfee = Convert.ToInt32(dr["RequiredFee"]);
                }
            }

            con.Close();

            if (fhid == 31)
            {
                rfee = rfee * tp;
            }
            return rfee;
        }

        public static int findDueAmount(int srid,int cid,int fhid,int year,string exam, int totalbpsub,string batch, string frdate)
        {
          return (findRequiredFee(srid,cid,fhid,totalbpsub,batch,frdate)-findPreviousPaidFee(srid,cid,fhid,year,exam));         
        }

        public static int findReqFeeofSC(string sccode, string batch,int year,int fhid)
        {
            int reqfee = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SRID,Session from DDEStudentRecord where StudyCentreCode='" + sccode + "' and Session='"+batch+"' and CYear='"+year+"' and RecordStatus='True'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string frdate = "";
                    reqfee = reqfee + findRequiredFee(Convert.ToInt32(dr[0]), FindInfo.findCourseIDBySRID(Convert.ToInt32(dr[0])), fhid,0,dr["Session"].ToString(),frdate);
                }
            }

            con.Close();

            return reqfee;
            
        }

        public static int findPaidFeeofSC(string sccode, string batch,int year, int fhid)
        {
            int paidfee = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SRID from DDEStudentRecord where StudyCentreCode='" + sccode + "' and Session='" + batch + "' and CYear='" + year + "' and RecordStatus='True'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    paidfee = paidfee + findPreviousPaidFee(Convert.ToInt32(dr[0]), FindInfo.findCourseIDBySRID(Convert.ToInt32(dr[0])), fhid, 1, "NA");
                }
            }

            con.Close();

            return paidfee;
        }

        public static string findMOPByID(int mop)
        {
            if (mop == 1)
            {
                return "DD";
            }

            else if (mop == 2)
            {
                return "CHEQUE";
            }
            else if (mop == 3)
            {
                return "CASH";
            }
            else if (mop == 4)
            {
                return "RTGS";
            }
            else if (mop == 5)
            {
                return "DEDUCT FROM REFUND";
            }
            else if (mop == 6)
            {
                return "DIRECT CASH TRANSFER";
            }
            else
            {
                return "";
            }
        }

        public static int findRequiredLateFee(int srid, int fhid)
        {
            int latefee = 0;
            int lateFHID=0;

            if (fhid == 12 || fhid == 24)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                if (fhid == 12)
                {
                    lateFHID = 12;
                    cmd.CommandText = "select LastDate from DDELastDateFeeRecord where FHID='" + 12 + "'";
                }

                else if (fhid == 24)
                {
                    lateFHID = 24;
                    cmd.CommandText = "select LastDate from DDELastDateFeeRecord where FHID='" + 24 + "'";
                }

                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (Convert.ToDateTime(dr[0]) < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        latefee = findLateFeeByID(lateFHID);

                    }

                }

                con.Close();
            }
            

            return latefee;
            
        }

        private static int findLateFeeByID(int fhid)
        {
            int lfee = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select RequiredFee from DDERequiredFeeRecord where FHID='" + fhid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                lfee = Convert.ToInt32(dr[0]);
            }

            con.Close();

            return lfee;
        }

        public static string findFeeHeadNameByID(int fhid)
        {
            string  feename = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select FeeHead from DDEFeeHead where FHID='" + fhid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                feename = dr[0].ToString();
            }

            con.Close();

            return feename;
        }

        public static float findPercentageOf(int amount, int per)
        {
            return (amount * per) / 100;
        }

        public static int findUsedAmountOfDraft(int ptype, string dcno, string dcdate, string ibn)
        {
            int usedamount = 0;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession ", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand("select * from [DDEFeeRecord_" + dr1["AcountSession"].ToString() + "] where PaymentMode='"+ptype+"' and DCNumber='" + dcno.Trim() + "' and DCDate='"+dcdate+"' and IBN='"+ibn.Trim()+"'",con);
                SqlDataReader dr;
               
                con.Open();

                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        usedamount = usedamount + Convert.ToInt32(dr["Amount"]);

                    }

                }

                con.Close();
            }
            con1.Close();

            usedamount = usedamount + findSCFeeUsedAmount(ptype,dcno,dcdate,ibn);

            return usedamount;
        }

        private static int findSCFeeUsedAmount(int ptype, string dcno, string dcdate, string ibn)
        {
            int uamount = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "select Amount from DDESCFeeRecord where PaymentMode='" + ptype + "' and DCNumber='" + dcno.Trim() + "' and DCDate='" + dcdate + "' and IBN='" + ibn.Trim() + "'";


            cmd.Connection = con;
            con.Open();

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    uamount = uamount + Convert.ToInt32(dr[0]); 
                    
                }
            }

            con.Close();
            return uamount;
        }

        public static string IntegerToWords(long inputNum)
        {
            int dig1, dig2, dig3, level = 0, lasttwo, threeDigits;

            string retval = "";
            string x = "";
            string[] ones ={
                "zero",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
                "ten",
                "eleven",
                "twelve",
                "thirteen",
                "fourteen",
                "fifteen",
                "sixteen",
                "seventeen",
                "eighteen",
                "nineteen"
              };
            string[] tens ={
                "zero",
                "ten",
                "twenty",
                "thirty",
                "forty",
                "fifty",
                "sixty",
                "seventy",
                "eighty",
                "ninety"
              };
            string[] thou ={
                "",
                "thousand",
                "million",
                "billion",
                "trillion",
                "quadrillion",
                "quintillion"
              };

            bool isNegative = false;
            if (inputNum < 0)
            {
                isNegative = true;
                inputNum *= -1;
            }

            if (inputNum == 0)
                return ("zero");

            string s = inputNum.ToString();

            while (s.Length > 0)
            {
                // Get the three rightmost characters
                x = (s.Length < 3) ? s : s.Substring(s.Length - 3, 3);

                // Separate the three digits
                threeDigits = int.Parse(x);
                lasttwo = threeDigits % 100;
                dig1 = threeDigits / 100;
                dig2 = lasttwo / 10;
                dig3 = (threeDigits % 10);

                // append a "thousand" where appropriate
                if (level > 0 && dig1 + dig2 + dig3 > 0)
                {
                    retval = thou[level] + " " + retval;
                    retval = retval.Trim();
                }

                // check that the last two digits is not a zero
                if (lasttwo > 0)
                {
                    if (lasttwo < 20) // if less than 20, use "ones" only
                        retval = ones[lasttwo] + " " + retval;
                    else // otherwise, use both "tens" and "ones" array
                        retval = tens[dig2] + " " + ones[dig3] + " " + retval;
                }

                // if a hundreds part is there, translate it
                if (dig1 > 0)
                    retval = ones[dig1] + " hundred " + retval;

                s = (s.Length - 3) > 0 ? s.Substring(0, s.Length - 3) : "";
                level++;
            }

            while (retval.IndexOf("  ") > 0)
                retval = retval.Replace("  ", " ");

            retval = retval.Trim();

            if (isNegative)
                retval = "negative " + retval;

            return (retval);
        }

        public static string [] findDCDetails(string dcno, int pm)
        {
       
            string[] dcdetail = { "", "", "", };
            
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession ", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select DCDate,TotalDCAmount,IBN from [DDEFeeRecord_" + dr1[0].ToString() + "] where DCNumber='" + dcno + "' and PaymentMode='"+pm+"'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    dcdetail[0] = Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd");
                    dcdetail[1] = Convert.ToInt32(dr[1]).ToString();
                    dcdetail[2] = dr[2].ToString();
                }

                con.Close();
            }

            con1.Close();
        
            return dcdetail;
        }

        public static string[] findInstrumentDetails(string ino)
        {

            string[] idetail = { "", "", "", };

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select IDate,TotalAmount,IBN from DDEFeeInstruments where INo='" + ino + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                idetail[0] = Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd");
                idetail[1] = Convert.ToInt32(dr[1]).ToString();
                idetail[2] = dr[2].ToString();
            }

            con.Close();
           
        

            return idetail;
        }

        public static int insertAndGetTransactionEntry(string sccode)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDETransactionRecord values(@SCCode,@MerchantID,@SubscriberID,@TransactionRefNo,@BankRefNo,@TransactionAmount,@BankID,@BankMerchantID,@TxnType,@CurrencyName,@ItemCode,@SecurityType,@SecurityID,@SecurityPassword,@TxnDate,@AuthStatus,@SettlementType,@AdditionalInfo1,@AdditionalInfo2,@AdditionalInfo3,@AdditionalInfo4,@AdditionalInfo5,@AdditionalInfo6,@AdditionalInfo7,@ErrorStatus,@ErrorDescription,@CheckSum)" + "select @TID=SCOPE_IDENTITY()", con);


            cmd.Parameters.AddWithValue("@SCCode", sccode);
            cmd.Parameters.AddWithValue("@MerchantID", "");
            cmd.Parameters.AddWithValue("@SubscriberID", "");
            cmd.Parameters.AddWithValue("@TransactionRefNo", "");
            cmd.Parameters.AddWithValue("@BankRefNo", "");
            cmd.Parameters.AddWithValue("@TransactionAmount", "");
            cmd.Parameters.AddWithValue("@BankID", "");
            cmd.Parameters.AddWithValue("@BankMerchantID", "");
            cmd.Parameters.AddWithValue("@TxnType", "");
            cmd.Parameters.AddWithValue("@CurrencyName", "");
            cmd.Parameters.AddWithValue("@ItemCode", "");
            cmd.Parameters.AddWithValue("@SecurityType", "");
            cmd.Parameters.AddWithValue("@SecurityID", "");
            cmd.Parameters.AddWithValue("@SecurityPassword", "");
            cmd.Parameters.AddWithValue("@TxnDate", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@AuthStatus", "");
            cmd.Parameters.AddWithValue("@SettlementType", "");
            cmd.Parameters.AddWithValue("@AdditionalInfo1","");
            cmd.Parameters.AddWithValue("@AdditionalInfo2", "");
            cmd.Parameters.AddWithValue("@AdditionalInfo3","");
            cmd.Parameters.AddWithValue("@AdditionalInfo4", "");
            cmd.Parameters.AddWithValue("@AdditionalInfo5", "");
            cmd.Parameters.AddWithValue("@AdditionalInfo6", "");
            cmd.Parameters.AddWithValue("@AdditionalInfo7", "");
            cmd.Parameters.AddWithValue("@ErrorStatus", "");
            cmd.Parameters.AddWithValue("@ErrorDescription", "");
            cmd.Parameters.AddWithValue("@CheckSum", "");


            SqlParameter p = cmd.Parameters.Add("@TID", SqlDbType.Int);
            p.Direction = ParameterDirection.Output;
            int   tid = 0;
           
            con.Open();
            cmd.ExecuteNonQuery();
            tid = (int)p.Value;
            con.Close();

            return tid;
        }

        public static void setTransactionStatus(int tid, string trefno,string bankid, string status, string asession)
        {
           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update [DDEOLFeeRecord_" + asession + "] set DCNumber=@DCNumber,DCDate=@DCDate,IBN=@IBN,TransactionStatus=@TransactionStatus where TNo='" + tid + "'", con);

            cmd.Parameters.AddWithValue("@DCNumber", trefno);
            cmd.Parameters.AddWithValue("@DCDate", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@IBN",Accounts.findBankNameByID(bankid));
            cmd.Parameters.AddWithValue("@TransactionStatus", status);
                   
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
                  
        }

        private static string findBankNameByID(string bankid)
        {
            string bankname = "Not Available";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select BankName from DDEBanks where BankID ='" +bankid+ "'", con);
            SqlDataReader dr;


            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                bankname= dr[0].ToString();
            }
            con.Close();

            return bankname;
        }

        public static bool feePaidBySC(int srid, int feehead, int year, string exam,string asession,out int ofrid, out int amount, out string tno,out string trefno, out string verified)
        {
            ofrid = 0;
            amount = 0;
            tno = "";
            trefno = "";
            verified = "";

            if (feehead == 1)
            {
                exam = "NA";
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select OFRID,DCNumber,Amount,TNo,Verified from [DDEOLFeeRecord_" + asession + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForYear='" + year + "' and ForExam='" + exam + "' and TransactionStatus='True'", con);
            SqlDataReader dr;

            bool exist = false;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                exist = true;
                ofrid = Convert.ToInt32(dr["OFRID"]);
                amount = Convert.ToInt32(dr["Amount"]);
                tno = dr["TNo"].ToString();
                trefno = dr["DCNumber"].ToString();
                verified = dr["Verified"].ToString();
            }

            con.Close();

            return exist;
        }

        public static void setOFFFeeRecordToONFeeRecord(int ofrid, string asession, string status)
        {
            if (!ofrAlreadyExist(ofrid,asession))
            {

                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr1;
                SqlCommand cmd1 = new SqlCommand("Select * from [DDEOLFeeRecord_" + asession + "] where OFRID='" + ofrid + "'", con1);
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into [DDEFeeRecord_" + asession + "] values(@OFRID,@SRID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@TotalDCAmount,@ForYear,@ForExam,@TOFS,@Verified)", con);

                    cmd.Parameters.AddWithValue("@OFRID", dr1["OFRID"].ToString());
                    cmd.Parameters.AddWithValue("@SRID", dr1["SRID"].ToString());
                    cmd.Parameters.AddWithValue("@FeeHead", dr1["FeeHead"].ToString());
                    cmd.Parameters.AddWithValue("@PaymentMode", dr1["PaymentMode"].ToString());
                    cmd.Parameters.AddWithValue("@DCNumber", dr1["DCNumber"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@DCDate", dr1["DCDate"].ToString());
                    cmd.Parameters.AddWithValue("@IBN", dr1["IBN"].ToString());
                    cmd.Parameters.AddWithValue("@Amount", dr1["Amount"].ToString());
                    cmd.Parameters.AddWithValue("@AmountInWords", dr1["AmountInWords"].ToString());
                    cmd.Parameters.AddWithValue("@TotalDCAmount", dr1["TotalDCAmount"].ToString());
                    cmd.Parameters.AddWithValue("@ForYear", dr1["ForYear"].ToString());
                    cmd.Parameters.AddWithValue("@ForExam", dr1["ForExam"].ToString());
                    cmd.Parameters.AddWithValue("@TOFS", dr1["TOFS"].ToString());
                    cmd.Parameters.AddWithValue("@Verified", status);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                con1.Close();
            }

            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update [DDEFeeRecord_" + asession + "] set Verified=@Verified where OFRID='" + ofrid + "'", con);

                cmd.Parameters.AddWithValue("@Verified", status);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
          
        }

        private static bool ofrAlreadyExist(int ofrid, string asession)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select FRID from [DDEFeeRecord_" + asession + "] where OFRID='" + ofrid + "'", con);
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
    
        public static bool isFeeRecordSubmitted(int srid, int feehead, int year, string exam)
        {
            bool exist = false;

            if (srid != 0)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr1;

                    if (year == 5)
                    {
                        cmd1.CommandText = "select * from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForExam='" + exam + "'";
                    }
                    else
                    {
                        cmd1.CommandText = "select * from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForYear='" + year + "' and ForExam='" + exam + "'";
                    }

                    cmd1.Connection = con1;
                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        exist = true;
                    }
        
                    con1.Close();


                }

                con.Close();
            }

            return exist; 
        }

        public static bool isFeeRecordVerified(int srid, int feehead, int year, string exam)
        {
            bool exist = false;

            if (srid != 0)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataReader dr1;

                    if (year == 5)
                    {
                        cmd1.CommandText = "select * from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForExam='" + exam + "' and Verified='True'";
                    }
                    else
                    {
                        cmd1.CommandText = "select * from [DDEFeeRecord_" + dr[0].ToString() + "] where SRID='" + srid + "' and FeeHead='" + feehead + "' and ForYear='" + year + "' and ForExam='" + exam + "' and Verified='True'";
                    }

                    cmd1.Connection = con1;
                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        exist = true;
                    }

                    con1.Close();


                }

                con.Close();
            }

            return exist; 
        }

        public static object findDueAmountVerified(int srid, int cid, int fhid, int year, string exam, int totalbpsub, string batch, string frdate)
        {
            return (findRequiredFee(srid, cid, fhid, totalbpsub,batch, frdate) - findPreviousPaidFeeVerified(srid, cid, fhid, year, exam));   
        }

        public static int findPreviousPaidFeeVerified(int srid, int cid, int fhid, int year, string exam)
        {
            int feepaid = 0;
            if (srid != 0)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForYear='" + year + "' and ForExam='" + exam + "' and Verified='True'", con1);

                    SqlDataReader dr1;

                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            feepaid = feepaid + Convert.ToInt32(dr1["Amount"]);
                        }

                    }

                    con1.Close();

                }

                con.Close();
            }

            return feepaid;
        }

        public static bool draftExist(string dcno, string pm)
        { 
                bool exist=false;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where DCNumber='" + dcno + "' and PaymentMode='"+pm+"'", con1);

                    SqlDataReader dr1;

                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        exist=true;
                        break;

                    }

                    con1.Close();

                }

                con.Close();



                return exist;
           

           
        }

        public static bool draftnoExist(string dcno)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where DCNumber='" + dcno + "'", con1);

                SqlDataReader dr1;

                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    exist = true;
                    break;

                }

                con1.Close();

            }

            con.Close();

            if (exist == false)
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("select Amount from DDESCFeeRecord where DCNumber='" + dcno + "'", con1);

                SqlDataReader dr1;

                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    exist = true;
                 

                }

                con1.Close();
            }



            return exist;



        }

        public static bool instrumentExistByINo(string dcno)
        {
            bool exist = false;
           
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select IID from DDEFeeInstruments where INo='" + dcno + "'", con2);

            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                exist = true;


            }

            con2.Close();



            return exist;
        }

        public static bool instrumentExist(string dcno, int pm,string ibn, string idate)
        {
            bool exist = false;
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlDataReader dr;
            //SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
            //con.Open();
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //    SqlCommand cmd1 = new SqlCommand("select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where DCNumber='" + dcno + "' and PaymentMode='" + pm + "'", con1);

            //    SqlDataReader dr1;

            //    con1.Open();
            //    dr1 = cmd1.ExecuteReader();
            //    if (dr1.HasRows)
            //    {
            //        exist = true;
            //        break;

            //    }

            //    con1.Close();

            //}

            //con.Close();

            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand();

            if(pm==4)
            {
                cmd2.CommandText = "select IID from DDEFeeInstruments where INo='" + dcno + "'";
            }
            else
            {
                cmd2.CommandText = "select IID from DDEFeeInstruments where INo='" + dcno + "' and IType='" + pm + "' and IBN='" + ibn + "' and IDate='" + idate + "'";
            }

            SqlDataReader dr2;
            cmd2.Connection = con2;
            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                exist = true;            
            }

            con2.Close();

            return exist;
        }

        public static bool instrumentVerified(string dcno, string pm)
        {
            bool exist = false;
           
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select IID from DDEFeeInstruments where INo='" + dcno + "' and IType='" + pm + "' and Verified='True'", con2);

            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                exist = true;


            }

            con2.Close();


            return exist;
        }

        public static string[] findInstrumentsDetails(string dcno, int pm)
        {
           
            string[] dcdetail = { "", "", "" }; 

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr1;
            SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession ", con1);
            con1.Open();
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select DCDate,TotalDCAmount,IBN from [DDEFeeRecord_" + dr1[0].ToString() + "] where DCNumber='" + dcno + "' and PaymentMode='" + pm + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    dcdetail[0] = Convert.ToDateTime(dr[0].ToString()).ToString("yyyy-MM-dd");
                    dcdetail[1] = Convert.ToInt32(dr[1]).ToString();
                    dcdetail[2] = dr[2].ToString();

                  

                    break;
                }

                con.Close();
            }

            con1.Close();

            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select IDate,TotalAmount,IBN from DDEFeeInstruments where INo='" + dcno + "' and IType='" + pm + "'", con2);
            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                dr2.Read();
                dcdetail[0] = Convert.ToDateTime(dr2[0].ToString()).ToString("yyyy-MM-dd");
                dcdetail[1] = Convert.ToInt32(dr2[1]).ToString();
                dcdetail[2] = dr2[2].ToString();
            }

            con2.Close();

            return dcdetail;
        }

        public static string[] findInstrumentsDetailsNew(string dcno, int pm)
        {

            string[] dcdetail = { "", "", "" };

            
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select IDate,TotalAmount,IBN from DDEFeeInstruments where INo='" + dcno + "' and IType='" + pm + "'", con2);
            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                dr2.Read();
                dcdetail[0] = Convert.ToDateTime(dr2[0].ToString()).ToString("yyyy-MM-dd");
                dcdetail[1] = Convert.ToInt32(dr2[1]).ToString();
                dcdetail[2] = dr2[2].ToString();
            }

            con2.Close();

            return dcdetail;
        }

        public static bool isValidInstrumentForVerification(string dcno, string pm, out string error)
        {
            bool valid = false;
            error = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where INo='" + dcno + "' and IType='" + pm + "'", con);

            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["Received"].ToString() == "True")
                {
                    if (dr["Verified"].ToString() == "False")
                    {
                        valid = true;
                    }
                    else if (dr["Verified"].ToString() == "True")
                    {
                        error = "Sorry !! This instrument is alreday verified";
                    }
                }
                else if (dr["Received"].ToString() == "False")
                {
                    error = "Sorry !! This instrument is not received";
                }


            }
            else
            {
                error = "Sorry !! There is no instrument with this no.<br/> Please enter a valid no.";
            }

            con.Close();


            return valid;
        }

        public static bool isValidInstrumentForDistribution(string dcno, string pm, out string error)
        {
            bool valid = false;
            error = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where INo='" + dcno + "' and IType='" + pm + "'", con);

            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["Received"].ToString() == "True")
                {
                    if (dr["Verified"].ToString() == "True")
                    {
                        if (dr["AmountAlloted"].ToString() == "False")
                        {
                            valid = true;
                        }
                        else if (dr["AmountAlloted"].ToString() == "True")
                        {
                            error = "Sorry !! This instrument is already distributed";
                        }
                    }
                    else if (dr["Verified"].ToString() == "False")
                    {
                        error = "Sorry !! This instrument is not verified.";
                    }
                }
                else if (dr["Received"].ToString() == "False")
                {
                    error = "Sorry !! This instrument is not received";
                }


            }
            else
            {
                error = "Sorry !! There is no instrument with this no.<br/> Please enter a valid no.";
            }

            con.Close();


            return valid;
        }
      
        public static bool validInstrument(string ino, string pm,string currentsccode,bool trans,string presccode, out int iid, out string scmode,out int count,out string ardate, out string error)
        {
            bool valid = false;
            error = "NF";
            iid = 0;
            scmode = "";
            count = 0;
            ardate = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where INo='" + ino + "' and IType='" + pm + "'", con);

            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    count = count + 1;
                    if (trans == true)
                    {
                        string[] str = dr["SCCode"].ToString().Split(',');
                        int pos = Array.IndexOf(str, presccode);
                        if (pos > -1 && (isSCEligibleForTransfer(currentsccode)))
                        {
                            //if (dr["RG"].ToString() == "False")
                            //{
                            if (dr["Lock"].ToString() == "False")
                            {
                                    if (dr["Received"].ToString() == "True")
                                    {
                                        if (dr["Verified"].ToString() == "True")
                                        {
                                            if (dr["AmountAlloted"].ToString() == "True")
                                            {
                                                valid = true;
                                                ardate = dr["AmountReceivedOn"].ToString();
                                                iid = Convert.ToInt32(dr["IID"]);
                                                if (dr["SCMode"].ToString() == "True")
                                                {
                                                    scmode = "1";
                                                }
                                                else if (dr["SCMode"].ToString() == "False")
                                                {
                                                    scmode = "0";
                                                }
                                            }
                                            else if (dr["AmountAlloted"].ToString() == "False")
                                            {
                                                error = "Sorry !!The amount of instrument is not distributed till yet.";
                                            }
                                        }
                                        else if (dr["Verified"].ToString() == "False")
                                        {
                                            error = "Sorry !! This instrument is not verified.";
                                        }
                                    }
                                    else if (dr["Received"].ToString() == "False")
                                    {
                                        error = "Sorry !! This instrument is not received.";
                                    }
                                }
                                else
                                {
                                    error = "Sorry !! This instrument is locked.";
                                }
                            //}
                            //else
                            //{
                            //    error = "Sorry !! Refund has been generated for this instrument.So it is locked.";
                            //}


                        }
                        else
                        {

                            error = "Sorry !! The SC Code of Student does not match to the SC Code of instrument";

                        }
                    }
                    else
                    {
                        string[] str = dr["SCCode"].ToString().Split(',');
                        int pos = Array.IndexOf(str, currentsccode);
                        if (pos > -1)
                        {
                            //if (dr["RG"].ToString() == "False")
                            //{
                            if (dr["Lock"].ToString() == "False")
                            {
                                    if (dr["Received"].ToString() == "True")
                                    {
                                        if (dr["Verified"].ToString() == "True")
                                        {
                                            if (dr["AmountAlloted"].ToString() == "True")
                                            {
                                                valid = true;
                                                ardate = dr["AmountReceivedOn"].ToString();
                                                iid = Convert.ToInt32(dr["IID"]);
                                                if (dr["SCMode"].ToString() == "True")
                                                {
                                                    scmode = "1";
                                                }
                                                else if (dr["SCMode"].ToString() == "False")
                                                {
                                                    scmode = "0";
                                                }
                                            }
                                            else if (dr["AmountAlloted"].ToString() == "False")
                                            {
                                                error = "Sorry !!The amount of instrument is not distributed till yet.";
                                            }
                                        }
                                        else if (dr["Verified"].ToString() == "False")
                                        {
                                            error = "Sorry !! This instrument is not verified.";
                                        }
                                    }
                                    else if (dr["Received"].ToString() == "False")
                                    {
                                        error = "Sorry !! This instrument is not received.";
                                    }
                                }
                                else
                                {
                                    error = "Sorry !! This instrument is locked.";
                                }
                            //}
                            //else
                            //{
                            //    error = "Sorry !! Refund has been generated for this instrument.So it is locked.";
                            //}


                        }
                        else
                        {

                            error = "Sorry !! The SC Code of Student does not match to the SC Code of instrument";

                        }
                    }
                }
            }
            else
            {
                error = "Sorry !! There is no instrument with this no.";
            }
           

            con.Close();


            return valid;
        }

        private static bool isSCEligibleForTransfer(string currentsccode)
        {
            bool exist = false;

            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select SCCode from DDESCEligibleForTransfer where SCCode='" + currentsccode + "'", con2);

            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                exist = true;


            }

            con2.Close();
            return exist;
        }

        public static bool validInstrumentByIID(int iid, string currentsccode, bool trans, string presccode, out string scmode,out string error)
        {
            bool valid = false;
            error = "NF";
            iid = 0;
            scmode = "";
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEFeeInstruments where IID='" + iid + "'", con);

            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    if (trans == true)
                    {
                        string[] str = dr["SCCode"].ToString().Split(',');
                        int pos = Array.IndexOf(str, presccode);
                        if (pos > -1 && (isSCEligibleForTransfer(currentsccode)))
                        {
                            if (dr["Received"].ToString() == "True")
                            {
                                if (dr["Verified"].ToString() == "True")
                                {
                                    if (dr["AmountAlloted"].ToString() == "True")
                                    {
                                        valid = true;
                                        iid = Convert.ToInt32(dr["IID"]);
                                        if (dr["SCMode"].ToString() == "True")
                                        {
                                            scmode = "1";
                                        }
                                        else if (dr["SCMode"].ToString() == "False")
                                        {
                                            scmode = "0";
                                        }
                                    }
                                    else if (dr["AmountAlloted"].ToString() == "False")
                                    {
                                        error = "Sorry !!The amount of instrument is not distributed till yet.";
                                    }
                                }
                                else if (dr["Verified"].ToString() == "False")
                                {
                                    error = "Sorry !! This instrument is not verified.";
                                }
                            }
                            else if (dr["Received"].ToString() == "False")
                            {
                                error = "Sorry !! This instrument is not received.";
                            }


                        }
                        else
                        {

                            error = "Sorry !! The SC Code of Student does not match to the SC Code of instrument";

                        }
                    }
                    else
                    {
                        string[] str = dr["SCCode"].ToString().Split(',');
                        int pos = Array.IndexOf(str, currentsccode);
                        if (pos > -1)
                        {
                            if (dr["Received"].ToString() == "True")
                            {
                                if (dr["Verified"].ToString() == "True")
                                {
                                    if (dr["AmountAlloted"].ToString() == "True")
                                    {
                                        valid = true;
                                        iid = Convert.ToInt32(dr["IID"]);
                                        if (dr["SCMode"].ToString() == "True")
                                        {
                                            scmode = "1";
                                        }
                                        else if (dr["SCMode"].ToString() == "False")
                                        {
                                            scmode = "0";
                                        }
                                    }
                                    else if (dr["AmountAlloted"].ToString() == "False")
                                    {
                                        error = "Sorry !!The amount of instrument is not distributed till yet.";
                                    }
                                }
                                else if (dr["Verified"].ToString() == "False")
                                {
                                    error = "Sorry !! This instrument is not verified.";
                                }
                            }
                            else if (dr["Received"].ToString() == "False")
                            {
                                error = "Sorry !! This instrument is not received.";
                            }


                        }
                        else
                        {

                            error = "Sorry !! The SC Code of Student does not match to the SC Code of instrument";

                        }
                    }
                }
            }
            else
            {

                error = "Sorry !! There is no instrument with this no.";
            }


            con.Close();


            return valid;
        }

        public static bool instrumentExistByNo(string vlno)
        {
            bool exist = false;

            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select VLID from DDEVerificationLetters where VLNo='" + vlno + "'", con2);

            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                exist = true;


            }

            con2.Close();


            return exist;
        }      

        public static bool isRefundedBySRID(int srid, int year)
        {

            bool exist = false;

            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select * from DDERefundRecord where SRID='" + srid + "' and Year='"+year+"' and RG='True'", con2);

            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                exist = true;

            }

            con2.Close();


            return exist;
        }

        public static bool validSCFee(int scid, int iid, int fhid, int amount, string frdate, int pros,int tbreqfee,int cons, out string error)
        {
            bool valid = false;
            error = "";
       
            int paidinst;
            int reqfee;
            int feepaid = findPaidFeeForSC(scid, fhid, out paidinst);

            if (fhid == 22)
            {
                reqfee = tbreqfee-((tbreqfee*cons)/100);
            }
            else
            {
               
                reqfee = findReqFeeForSC(fhid, frdate, pros);
            }
            int totalinst;
            if (instalableFee(fhid, out totalinst))
            {
                if (fhid == 31 || fhid == 11)
                {
                    if (amount == reqfee)
                    {
                        valid = true;
                    }
                    else
                    {
                        error = "Sorry !! Invalid Amount.";
                    }
                }
                else
                {
                    if (paidinst < totalinst)
                    {
                        if (fhid == 22)
                        {
                            if (amount <= reqfee)
                            {
                                valid = true;
                            }
                            else
                            {
                                error = "Sorry !! Fee paid is more than required fee.";
                            }
                        }
                        else
                        {
                            if (feepaid < reqfee)
                            {
                                if ((feepaid + amount) <= reqfee)
                                {
                                    valid = true;
                                }
                                else
                                {
                                    error = "Sorry !! Fee paid is more than required fee.";
                                }
                            }
                            else
                            {
                                error = "Sorry !! This fee is already exist.";
                            }
                        }

                    }
                    else
                    {
                        error = "Sorry !! all " + totalinst + " installments has already been submitted.";
                    }
                }

            }
            else
            {
                if (feepaid == reqfee)
                {
                    error = "Sorry !! This fee is already exist.";
                }
                else
                {
                   valid=true;
                }

            }         
            
            return valid;
        }

        private static int findReqFeeForSC(int fhid, string frdate,int pros)
        {
            int rfee=0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDERequiredFeeRecord where FHID='" + fhid + "' and TPFrom<='"+frdate+"' and TPTo>='"+frdate+"'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["RequiredFee"].ToString() != "")
                {
                    rfee = Convert.ToInt32(dr["RequiredFee"]);
                }
            }

            con.Close();

            if (fhid == 31)
            {
                rfee = rfee * pros;
            }

            return rfee;
           
        }

        private static int findPaidFeeForSC(int scid, int fhid, out int paidinst)
        {
            int feepaid=0;
            paidinst = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDESCFeeRecord where FeeHead='" + fhid + "' and SCID='" + scid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    feepaid = feepaid + Convert.ToInt32(dr["Amount"]);
                    paidinst = paidinst + 1;
                }
            }

            con.Close();

            return feepaid;
        }

        public static void fillSCFee(int scid, int fhid, string asession, int mop, string ino, string iday, string imonth, string iyear, string ibn, int amount, string amountinwords, int totalamount, string foryear, string frdate,int cons, int et)
        {
            string dcdate = iyear + "-" + imonth + "-" + iday;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESCFeeRecord values(@AccountSession,@SCOFRID,@SCID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@Concession,@TotalDCAmount,@ForYear,@FRDate,@TOFS,@EntryType)", con);

            cmd.Parameters.AddWithValue("@AccountSession", asession);
            cmd.Parameters.AddWithValue("@SCOFRID", 0);
            cmd.Parameters.AddWithValue("@SCID", scid);
            cmd.Parameters.AddWithValue("@FeeHead", fhid);
            cmd.Parameters.AddWithValue("@PaymentMode", mop);
            cmd.Parameters.AddWithValue("@DCNumber", ino.Trim());
            cmd.Parameters.AddWithValue("@DCDate", dcdate);
            cmd.Parameters.AddWithValue("@IBN", ibn.ToUpper());
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@AmountInWords", amountinwords.ToUpper());
            cmd.Parameters.AddWithValue("@Concession", cons);
            cmd.Parameters.AddWithValue("@TotalDCAmount", totalamount);
            cmd.Parameters.AddWithValue("@ForYear", foryear);
            cmd.Parameters.AddWithValue("@FRDate", frdate);
            cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            cmd.Parameters.AddWithValue("@EntryType",et);

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static string[] findInstrumentsDetailsNewByIID(int iid)
        {
            string[] dcdetail = { "", "", "","" };


            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select IDate,TotalAmount,IBN,AmountReceivedOn from DDEFeeInstruments where IID='" + iid + "'", con2);
            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                dr2.Read();
                dcdetail[0] = Convert.ToDateTime(dr2["IDate"].ToString()).ToString("yyyy-MM-dd");
                dcdetail[1] = Convert.ToInt32(dr2["TotalAmount"]).ToString();
                dcdetail[2] = dr2["IBN"].ToString();
                dcdetail[3] = dr2["AmountReceivedOn"].ToString();
            }

            con2.Close();

            return dcdetail;
        }

        public static bool singlefeePaid(int srid, int fhid)
        {
            bool fp = false; 
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader dr1;

              
                cmd1.CommandText = "select * from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='" + fhid + "'";
               


                cmd1.Connection = con1;
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        fp = true;
                       
                        break;
                    }

                }

                con1.Close();

            }

            con.Close();


            return fp;   
            
        }

        public static bool isInstrumentExist(string ino)
        {
            bool exist = false;
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select * from DDEFeeInstruments where INo='" + ino + "'", con2);
            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                exist = true;
            }

            con2.Close();

            return exist;
        }

        public static bool isInstrumentVerified(int iid)
        {
            bool verified = false;
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd2 = new SqlCommand("select * from DDEFeeInstruments where IID='" + iid + "'", con2);
            SqlDataReader dr2;

            con2.Open();
            dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                dr2.Read();
                if (dr2["Verified"].ToString() == "True")
                {
                    verified = true;
                }
            }

            con2.Close();

            return verified;
        }

        public static int findTotalAmountOnFeeHead(int fhid, int ptype, string ino, string idate, string ibn)
        {
            int tf = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select FH" + fhid + " from DDEFeeInstruments where IType='" + ptype + "' and INo='" + ino + "' and IDate='" + idate + "' and IBN='" + ibn + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                tf = Convert.ToInt32(dr[0]);


            }

            con.Close();

            return tf;
        }

        public static int findUsedAmountOnFeeHead(int fhid, int ptype, string ino, string idate, string ibn)
        {
            int usedamount = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader dr1;


                cmd1.CommandText = "select Amount from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where FeeHead='" + fhid + "' and DCNumber='" + ino + "' and DCDate='" + idate + "' and PaymentMode='" + ptype + "' and IBN='" + ibn + "'";



                cmd1.Connection = con1;
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        usedamount = usedamount + Convert.ToInt32(dr1["Amount"]);
                    }

                }

                con1.Close();

            }

            con.Close();


            return usedamount;     
        }

        public static string findAccountSessionByARDate(string ardate)
        {
            if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2009-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2010-03-31"))
            {
                return "2009-10";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2010-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2011-03-31"))
            {
                return "2010-11";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2011-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2012-03-31"))
            {
                return "2011-12";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2012-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2013-03-31"))
            {
                return "2012-13";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2013-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2014-03-31"))
            {
                return "2013-14";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2014-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2015-03-31"))
            {
                return "2014-15";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2015-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2016-03-31"))
            {
                return "2015-16";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2016-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2017-03-31"))
            {
                return "2016-17";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2017-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2018-03-31"))
            {
                return "2017-18";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2018-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2019-03-31"))
            {
                return "2018-19";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2019-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2020-03-31"))
            {
                return "2019-20";
            }
            else if (Convert.ToDateTime(ardate) >= Convert.ToDateTime("2020-04-01") && Convert.ToDateTime(ardate) <= Convert.ToDateTime("2021-03-31"))
            {
                return "2020-21";
            }
            else
            {
                return "NOT FOUND";
            }
        }

        public static bool isRefundGenerated(int iid)
        {
           bool rg= false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select RG from DDEFeeInstruments where IID='" + iid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() == "True")
                {
                    rg = true;
                }

            }

            con.Close();
            return rg;
        }

        public static int findIIDByInstrumentDetails(int itype,string ino, string idate, string ibn)
        {
           int iid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select IID from DDEFeeInstruments where IType='"+itype+"' and INo='" +ino + "' and IDate='"+idate+"' and IBN='"+ibn+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
               
                iid =Convert.ToInt32(dr[0]);
               

            }

            con.Close();
            return iid;
        }

        //public static bool isInstrumentLocked(int iid)
        //{
        //    bool locked = false;

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select Lock from DDEFeeInstruments where  IID='" + iid + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        if (dr["Lock"].ToString() == "True")
        //        {
        //            locked = true;
        //        }
        //        else
        //        {
        //            locked = false;
        //        }

               


        //    }

        //    con.Close();
        //    return locked;
        //}

        public static bool isFullCourseFeePaid(int samount, int cid, string session)
        {
            bool paid = false;
            if (samount > 0)
            {
                if (samount == findRequiredCourseFee(cid, session))
                {
                    paid = true;
                }
            }


            return paid;
        }

        private static int findRequiredCourseFee(int cid, string session)
        {
            int reqcfee = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + session + " from DDECourse where CourseID='" + cid + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                reqcfee = Convert.ToInt32(dr[0]);

            }

            con.Close();
            return reqcfee;
        }
      
        public static int findRequiredFeeByFHID(int fhid, string frdate)
        {
            int rfee = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDERequiredFeeRecord where FHID='" + fhid + "' and TPFrom<='" + frdate + "' and TPTo>='" + frdate + "'", con);
            SqlDataReader dr;


            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["RequiredFee"].ToString() != "")
                {
                    rfee = Convert.ToInt32(dr["RequiredFee"]);
                }
            }

            con.Close();

            return rfee;
        }

        public static bool isInstrumentLocked(int iid)
        {
            bool locked = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Lock from DDEFeeInstruments where IID='" + iid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() == "True")
                {
                    locked = true;
                }

            }

            con.Close();
            return locked;
        }
    }
    

}
