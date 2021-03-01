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
using System.Data.OleDb;

namespace DDE.Web
{
    public partial class FillAdmissionFormAutomatically2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAutoFeed_Click(object sender, EventArgs e)
        {
            string sccode = "999";
            string session = "Q 2020";
            int batchid = 26;
            string exam = "G10";
            int ano = 473329;
            int lano = 473585;

            int sano = ano;
            string accsess = "2020-21";
            int itype = 1;
            string ino = "UTIBR520210224003";

            string iday = "24";
            string imonth = "02";
            string iyear = "2021";

            string idate = iyear + "-" + imonth + "-" + iday;
            string ibn = "ICICI BANK";
            int totalamount = 10000000;
            int fhfee = 10000000;

            string frdate = "2021-02-25";

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select * from DDEPendingStudentRecord where [Session]='" + session + "' and StudyCentreCode='" + sccode + "' and StudentPhoto is not null and Eligible='YES' and OriginalsVerified='YES' and AdmissionStatus='1' and Enrolled='False' order by PSRID", con1);

            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da.Fill(ds);


            int count = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if(ano<=lano)
                {
                step1:
                    if (newStudent(ano))
                    {
                        if (newApplication(Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"])))
                        {
                            string error;
                            int coursefee = FindInfo.findCourseFeeByBatch(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), FindInfo.findBatchID(ds.Tables[0].Rows[i]["Session"].ToString())) / 2;
                            if (validINo(fhfee, itype, ino, idate, ibn, coursefee, out error))
                            {
                                int encounter = 0;

                                string eno = "";
                                string icno = "";

                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand();


                                cmd.CommandText = "insert into DDEStudentRecord OUTPUT INSERTED.SRID values (@PSRID,@ProANo,@ApplicationNo,@SCStatus,@StudyCentreCode,@PreviousSCCode,@Session,@SyllabusSession,@EnrollmentNo,@ICardNo,@AdmissionThrough,@AdmissionType,@PreviousInstitute,@PreviousCourse,@RollNoIYear,@RollNoIIYear,@RollNoIIIYear,@RollNoBP,@CYear,@FirstYear,@SecondYear,@ThirdYear,@Course,@Course2Year,@Course3Year,@StudentName,@FatherName,@MotherName,@StudentPhoto,@Gender,@DOBDay,@DOBMonth,@DOBYear,@CAddress,@City,@District,@State,@PinCode,@PhoneNo,@MobileNo,@Email,@AadhaarNo,@DOA,@VDOA,@DDNumber,@DDDay,@DDMonth,@DDYear,@IssuingBankName,@DDAmount,@DDAmountInwords,@Nationality,@Category,@Employmentstatus,@examname1,@examname2,@examname3,@examname4,@examname5,@subject1,@subject2,@subject3,@subject4,@subject5,@YearPass1,@YearPass2,@YearPass3,@YearPass4,@YearPass5,@UniversityBoard1,@UniversityBoard2,@UniversityBoard3,@UniversityBoard4,@UniversityBoard5,@Divisiongrade1,@Divisiongrade2,@Divisiongrade3,@Divisiongrade4,@Divisiongrade5,@Eligible,@CourseFeePaid,@FeeRecIssued,@OriginalsVerified,@QualifyingStatus,@RecordStatus,@AdmissionStatus,@ReasonIfPending)";
                                cmd.Parameters.AddWithValue("@PSRID", Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"]));
                                cmd.Parameters.AddWithValue("@ProANo", "NA");
                                cmd.Parameters.AddWithValue("@ApplicationNo", ano);

                                cmd.Parameters.AddWithValue("@SCStatus", ds.Tables[0].Rows[i]["SCStatus"].ToString());
                                cmd.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                                cmd.Parameters.AddWithValue("@PreviousSCCode", ds.Tables[0].Rows[i]["PreviousSCCode"].ToString());


                                cmd.Parameters.AddWithValue("@Session", ds.Tables[0].Rows[i]["Session"].ToString());
                                cmd.Parameters.AddWithValue("@SyllabusSession", ds.Tables[0].Rows[i]["SyllabusSession"].ToString());

                                eno = allotEnrollmentNo(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), batchid, ds.Tables[0].Rows[i]["Session"].ToString(), ds.Tables[0].Rows[i]["StudyCentreCode"].ToString(), out encounter);

                                icno = eno.Substring(0, 3) + "-" + eno.Substring(6, 5);

                                cmd.Parameters.AddWithValue("@EnrollmentNo", eno);
                                cmd.Parameters.AddWithValue("@ICardNo", icno);
                                cmd.Parameters.AddWithValue("@AdmissionThrough", 1);

                                if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "1")
                                {
                                    cmd.Parameters.AddWithValue("@AdmissionType", Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionType"]));
                                    cmd.Parameters.AddWithValue("@PreviousInstitute", "");
                                    cmd.Parameters.AddWithValue("@PreviousCourse", "");
                                }

                                else if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "2" || ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "3")
                                {
                                    cmd.Parameters.AddWithValue("@AdmissionType", Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionType"]));
                                    cmd.Parameters.AddWithValue("@PreviousInstitute", Convert.ToInt32(ds.Tables[0].Rows[i]["PreviousInstitute"]));
                                    cmd.Parameters.AddWithValue("@PreviousCourse", Convert.ToInt32(ds.Tables[0].Rows[i]["PreviousCourse"]));
                                }

                                cmd.Parameters.AddWithValue("@RollNoIYear", "");
                                cmd.Parameters.AddWithValue("@RollNoIIYear", "");
                                cmd.Parameters.AddWithValue("@RollNoIIIYear", "");
                                cmd.Parameters.AddWithValue("@RollNoBP", "");
                                cmd.Parameters.AddWithValue("@CYear", Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                                if (ds.Tables[0].Rows[i]["CYear"].ToString() == "1")
                                {
                                    cmd.Parameters.AddWithValue("@FirstYear", "True");
                                    cmd.Parameters.AddWithValue("@SecondYear", "False");
                                    cmd.Parameters.AddWithValue("@ThirdYear", "False");
                                }
                                else if (ds.Tables[0].Rows[i]["CYear"].ToString() == "2")
                                {
                                    cmd.Parameters.AddWithValue("@FirstYear", "False");
                                    cmd.Parameters.AddWithValue("@SecondYear", "True");
                                    cmd.Parameters.AddWithValue("@ThirdYear", "False");
                                }
                                else if (ds.Tables[0].Rows[i]["CYear"].ToString() == "3")
                                {
                                    cmd.Parameters.AddWithValue("@FirstYear", "False");
                                    cmd.Parameters.AddWithValue("@SecondYear", "False");
                                    cmd.Parameters.AddWithValue("@ThirdYear", "True");
                                }
                                if (FindInfo.isMBACourse(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"])))
                                {
                                    if (ds.Tables[0].Rows[i]["CYear"].ToString() == "1" && ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "1")
                                    {
                                        cmd.Parameters.AddWithValue("@Course", 76);
                                        cmd.Parameters.AddWithValue("@Course2Year", "");
                                        cmd.Parameters.AddWithValue("@Course3Year", "");
                                    }
                                    else if (ds.Tables[0].Rows[i]["CYear"].ToString() == "2" && (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "2" || ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "3"))
                                    {
                                        cmd.Parameters.AddWithValue("@Course", 76);
                                        cmd.Parameters.AddWithValue("@Course2Year", ds.Tables[0].Rows[i]["Course"].ToString());
                                        cmd.Parameters.AddWithValue("@Course3Year", ds.Tables[0].Rows[i]["Course"].ToString());
                                    }


                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Course", ds.Tables[0].Rows[i]["Course"].ToString());
                                    cmd.Parameters.AddWithValue("@Course2Year", "");
                                    cmd.Parameters.AddWithValue("@Course3Year", "");
                                }
                                cmd.Parameters.AddWithValue("@StudentName", ds.Tables[0].Rows[i]["StudentName"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@FatherName", ds.Tables[0].Rows[i]["FatherName"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@MotherName", ds.Tables[0].Rows[i]["MotherName"].ToString().ToUpper());

                                //cmd.Parameters.AddWithValue("@StudentPhoto", FindInfo.findPendingStudentPhoto(Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"])));
                                cmd.Parameters.AddWithValue("@StudentPhoto", ds.Tables[0].Rows[i]["StudentPhoto"]);

                                cmd.Parameters.AddWithValue("@Gender", ds.Tables[0].Rows[i]["Gender"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@DOBDay", ds.Tables[0].Rows[i]["DOBDay"].ToString());
                                cmd.Parameters.AddWithValue("@DOBMonth", ds.Tables[0].Rows[i]["DOBMonth"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@DOBYear", ds.Tables[0].Rows[i]["DOBYear"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@CAddress", ds.Tables[0].Rows[i]["CAddress"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@City", ds.Tables[0].Rows[i]["City"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@District", ds.Tables[0].Rows[i]["District"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@State", ds.Tables[0].Rows[i]["State"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@PinCode", ds.Tables[0].Rows[i]["PinCode"].ToString());
                                cmd.Parameters.AddWithValue("@PhoneNo", ds.Tables[0].Rows[i]["PhoneNo"].ToString());
                                cmd.Parameters.AddWithValue("@MobileNo", ds.Tables[0].Rows[i]["MobileNo"].ToString());
                                cmd.Parameters.AddWithValue("@Email", ds.Tables[0].Rows[i]["Email"].ToString());
                                cmd.Parameters.AddWithValue("@AadhaarNo", ds.Tables[0].Rows[i]["AadhaarNo"].ToString());
                                cmd.Parameters.AddWithValue("@DOA", DateTime.Now.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@VDOA", FindInfo.findDOAByBatch(Convert.ToInt32(batchid)));
                                cmd.Parameters.AddWithValue("@DDNumber", "");
                                cmd.Parameters.AddWithValue("@DDDay", "");
                                cmd.Parameters.AddWithValue("@DDMonth", "");
                                cmd.Parameters.AddWithValue("@DDYear", "");
                                cmd.Parameters.AddWithValue("@IssuingBankName", "");
                                cmd.Parameters.AddWithValue("@DDAmount", "");
                                cmd.Parameters.AddWithValue("@DDAmountInwords", "");
                                cmd.Parameters.AddWithValue("@Nationality", ds.Tables[0].Rows[i]["Nationality"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Category", ds.Tables[0].Rows[i]["Category"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Employmentstatus", ds.Tables[0].Rows[i]["Employmentstatus"].ToString().ToUpper());

                                cmd.Parameters.AddWithValue("@examname1", ds.Tables[0].Rows[i]["examname1"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@examname2", ds.Tables[0].Rows[i]["examname2"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@examname3", ds.Tables[0].Rows[i]["examname3"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@examname4", ds.Tables[0].Rows[i]["examname4"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@examname5", ds.Tables[0].Rows[i]["examname5"].ToString().ToUpper());

                                cmd.Parameters.AddWithValue("@subject1", ds.Tables[0].Rows[i]["subject1"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@subject2", ds.Tables[0].Rows[i]["subject2"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@subject3", ds.Tables[0].Rows[i]["subject3"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@subject4", ds.Tables[0].Rows[i]["subject4"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@subject5", ds.Tables[0].Rows[i]["subject5"].ToString().ToUpper());

                                cmd.Parameters.AddWithValue("@YearPass1", ds.Tables[0].Rows[i]["YearPass1"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@YearPass2", ds.Tables[0].Rows[i]["YearPass2"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@YearPass3", ds.Tables[0].Rows[i]["YearPass3"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@YearPass4", ds.Tables[0].Rows[i]["YearPass4"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@YearPass5", ds.Tables[0].Rows[i]["YearPass5"].ToString().ToUpper());

                                cmd.Parameters.AddWithValue("@UniversityBoard1", ds.Tables[0].Rows[i]["UniversityBoard1"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@UniversityBoard2", ds.Tables[0].Rows[i]["UniversityBoard2"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@UniversityBoard3", ds.Tables[0].Rows[i]["UniversityBoard3"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@UniversityBoard4", ds.Tables[0].Rows[i]["UniversityBoard4"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@UniversityBoard5", ds.Tables[0].Rows[i]["UniversityBoard5"].ToString().ToUpper());

                                cmd.Parameters.AddWithValue("@Divisiongrade1", ds.Tables[0].Rows[i]["Divisiongrade1"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Divisiongrade2", ds.Tables[0].Rows[i]["Divisiongrade2"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Divisiongrade3", ds.Tables[0].Rows[i]["Divisiongrade3"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Divisiongrade4", ds.Tables[0].Rows[i]["Divisiongrade4"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Divisiongrade5", ds.Tables[0].Rows[i]["Divisiongrade5"].ToString().ToUpper());

                                cmd.Parameters.AddWithValue("@Eligible", ds.Tables[0].Rows[i]["Eligible"].ToString());
                                cmd.Parameters.AddWithValue("@CourseFeePaid", "");
                                cmd.Parameters.AddWithValue("@FeeRecIssued", "");
                                cmd.Parameters.AddWithValue("@OriginalsVerified", ds.Tables[0].Rows[i]["OriginalsVerified"].ToString());
                                cmd.Parameters.AddWithValue("@QualifyingStatus", "AC");

                                cmd.Parameters.AddWithValue("@RecordStatus", "True");

                                cmd.Parameters.AddWithValue("@AdmissionStatus", Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionStatus"]));

                                cmd.Parameters.AddWithValue("@ReasonIfPending", "NA");

                                cmd.Connection = con;

                                if (eno != "AE" && eno != "")
                                {
                                    con.Open();
                                    object srid = cmd.ExecuteScalar();
                                    con.Close();

                                    if (Convert.ToInt32(srid) > 0)
                                    {

                                    }
                                    FindInfo.updateEnrollmentCounter(batchid, encounter);

                                    updateEnrollStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"]));

                                    Log.createLogNow("Create", "Registered a student with Enrollment No'" + eno + "'", 0);

                                    Accounts.fillFee(Convert.ToInt32(srid), 1, accsess, itype, ino, iday, imonth, iyear, ibn, coursefee, Accounts.IntegerToWords(coursefee), totalamount, Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]), exam, frdate, 1);

                                    Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + eno + "'", 0);

                                    if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                                    {
                                        updateSCChangeRecord(Convert.ToInt32(srid), ds.Tables[0].Rows[i]["PreviousSCCode"].ToString(), ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                                    }
                                    if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "2" || ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "3")
                                    {
                                        insertCTMRecord(Convert.ToInt32(srid), ds.Tables[0].Rows[i]["PreviousInstitute"].ToString(), exam);
                                    }

                                    //detainStudent(Convert.ToInt32(srid), exam);
                                    //if(!FindInfo.isRegisteredForSLM(Convert.ToInt32(srid), Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"])))
                                    //{
                                    int res = registerForSLM(Convert.ToInt32(srid), ds.Tables[0].Rows[i]["StudyCentreCode"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                                    //}

                                }
                                else
                                {
                                    Response.Write("<br/> Sorry!! This Alloted Enrollment No. " + eno + " is already exist.");
                                }
                            }
                            else
                            {
                                Response.Write("<br/> Sorry!! Remaining amount is less than required course fee");
                                break;
                            }
                        }


                    }

                    else
                    {


                        Response.Write("<br/> Sorry !! this application no. : " + ano + " is already exist");
                        ano = ano + 1;
                        goto step1;

                    }

                    ano = ano + 1;
                    count = count + 1;
                }
                else
                {
                    Response.Write("<br/> Sorry !! Prospectus series ended");
                    break;
                }

            

            }

            Response.Write("<br/>Total : " + (count).ToString() + " Students uploaded.");
            Response.Write("<br/> ANo range alloted from " + (sano).ToString() + " To " + (ano - 1).ToString());
        }

        protected void btnAutoFeedFromExcel_Click(object sender, EventArgs e)
        {
            string sccode = "996";
            string session = "Q 2019-20";
            int batchid = 24;
            string exam = "Z11";
            int ano = 464049;
            int lano = 464224;

            int sano = ano;

            string accsess = "2020-21";
            int itype = 4;
            string ino = "COMB045USCJUNE20-004";

            string iday = "01";
            string imonth = "10";
            string iyear = "2020";

            string idate = iyear + "-" + imonth + "-" + iday;
            string ibn = "ICICI BANK";
            int totalamount = 4402655;
            int fhfee = 4085855;

            string frdate = "2020-11-07";

            string connectionString2 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Offline Applications\996\A 2019-20\7.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString2 = "SELECT * FROM [Sheet1$] order by OANo";

            int count = 0;

            using (OleDbConnection connection2 = new OleDbConnection(connectionString2))
            {

                OleDbCommand command2 = new OleDbCommand(queryString2, connection2);
                OleDbDataAdapter da2 = new OleDbDataAdapter(command2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                {
                   
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("Select * from DDEPendingStudentRecord where [Session]='" + session + "' and StudyCentreCode='" + sccode + "' and StudentPhoto is not null and Eligible='YES' and OriginalsVerified='YES' and AdmissionStatus='1' and Enrolled='False' and PSRID='"+ ds2.Tables[0].Rows[j]["OANo"]+ "'", con1);

                    SqlDataAdapter da = new SqlDataAdapter(cmd1);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ano <= lano)
                        {

                        step1:
                            if (newStudent(ano))
                            {
                                if (newApplication(Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"])))
                                {
                                    string error;
                                    int coursefee = FindInfo.findCourseFeeByBatch(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), FindInfo.findBatchID(ds.Tables[0].Rows[i]["Session"].ToString())) / 2;
                                    if (validINo(fhfee, itype, ino, idate, ibn, coursefee, out error))
                                    {
                                        int encounter = 0;

                                        string eno = "";
                                        string icno = "";

                                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                        SqlCommand cmd = new SqlCommand();


                                        cmd.CommandText = "insert into DDEStudentRecord OUTPUT INSERTED.SRID values (@PSRID,@ProANo,@ApplicationNo,@SCStatus,@StudyCentreCode,@PreviousSCCode,@Session,@SyllabusSession,@EnrollmentNo,@ICardNo,@AdmissionThrough,@AdmissionType,@PreviousInstitute,@PreviousCourse,@RollNoIYear,@RollNoIIYear,@RollNoIIIYear,@RollNoBP,@CYear,@FirstYear,@SecondYear,@ThirdYear,@Course,@Course2Year,@Course3Year,@StudentName,@FatherName,@MotherName,@StudentPhoto,@Gender,@DOBDay,@DOBMonth,@DOBYear,@CAddress,@City,@District,@State,@PinCode,@PhoneNo,@MobileNo,@Email,@AadhaarNo,@DOA,@VDOA,@DDNumber,@DDDay,@DDMonth,@DDYear,@IssuingBankName,@DDAmount,@DDAmountInwords,@Nationality,@Category,@Employmentstatus,@examname1,@examname2,@examname3,@examname4,@examname5,@subject1,@subject2,@subject3,@subject4,@subject5,@YearPass1,@YearPass2,@YearPass3,@YearPass4,@YearPass5,@UniversityBoard1,@UniversityBoard2,@UniversityBoard3,@UniversityBoard4,@UniversityBoard5,@Divisiongrade1,@Divisiongrade2,@Divisiongrade3,@Divisiongrade4,@Divisiongrade5,@Eligible,@CourseFeePaid,@FeeRecIssued,@OriginalsVerified,@QualifyingStatus,@RecordStatus,@AdmissionStatus,@ReasonIfPending)";
                                        cmd.Parameters.AddWithValue("@PSRID", Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"]));
                                        cmd.Parameters.AddWithValue("@ProANo", "NA");
                                        cmd.Parameters.AddWithValue("@ApplicationNo", ano);

                                        cmd.Parameters.AddWithValue("@SCStatus", ds.Tables[0].Rows[i]["SCStatus"].ToString());
                                        cmd.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                                        cmd.Parameters.AddWithValue("@PreviousSCCode", ds.Tables[0].Rows[i]["PreviousSCCode"].ToString());


                                        cmd.Parameters.AddWithValue("@Session", ds.Tables[0].Rows[i]["Session"].ToString());
                                        cmd.Parameters.AddWithValue("@SyllabusSession", ds.Tables[0].Rows[i]["SyllabusSession"].ToString());

                                        eno = allotEnrollmentNo(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), batchid, ds.Tables[0].Rows[i]["Session"].ToString(), ds.Tables[0].Rows[i]["StudyCentreCode"].ToString(), out encounter);

                                        icno = eno.Substring(0, 3) + "-" + eno.Substring(6, 5);

                                        cmd.Parameters.AddWithValue("@EnrollmentNo", eno);
                                        cmd.Parameters.AddWithValue("@ICardNo", icno);
                                        cmd.Parameters.AddWithValue("@AdmissionThrough", 1);

                                        if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "1")
                                        {
                                            cmd.Parameters.AddWithValue("@AdmissionType", Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionType"]));
                                            cmd.Parameters.AddWithValue("@PreviousInstitute", "");
                                            cmd.Parameters.AddWithValue("@PreviousCourse", "");
                                        }

                                        else if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "2" || ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "3")
                                        {
                                            cmd.Parameters.AddWithValue("@AdmissionType", Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionType"]));
                                            cmd.Parameters.AddWithValue("@PreviousInstitute", Convert.ToInt32(ds.Tables[0].Rows[i]["PreviousInstitute"]));
                                            cmd.Parameters.AddWithValue("@PreviousCourse", Convert.ToInt32(ds.Tables[0].Rows[i]["PreviousCourse"]));
                                        }

                                        cmd.Parameters.AddWithValue("@RollNoIYear", "");
                                        cmd.Parameters.AddWithValue("@RollNoIIYear", "");
                                        cmd.Parameters.AddWithValue("@RollNoIIIYear", "");
                                        cmd.Parameters.AddWithValue("@RollNoBP", "");
                                        cmd.Parameters.AddWithValue("@CYear", Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                                        if (ds.Tables[0].Rows[i]["CYear"].ToString() == "1")
                                        {
                                            cmd.Parameters.AddWithValue("@FirstYear", "True");
                                            cmd.Parameters.AddWithValue("@SecondYear", "False");
                                            cmd.Parameters.AddWithValue("@ThirdYear", "False");
                                        }
                                        else if (ds.Tables[0].Rows[i]["CYear"].ToString() == "2")
                                        {
                                            cmd.Parameters.AddWithValue("@FirstYear", "False");
                                            cmd.Parameters.AddWithValue("@SecondYear", "True");
                                            cmd.Parameters.AddWithValue("@ThirdYear", "False");
                                        }
                                        else if (ds.Tables[0].Rows[i]["CYear"].ToString() == "3")
                                        {
                                            cmd.Parameters.AddWithValue("@FirstYear", "False");
                                            cmd.Parameters.AddWithValue("@SecondYear", "False");
                                            cmd.Parameters.AddWithValue("@ThirdYear", "True");
                                        }
                                        if (FindInfo.isMBACourse(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"])))
                                        {
                                            if (ds.Tables[0].Rows[i]["CYear"].ToString() == "1" && ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "1")
                                            {
                                                cmd.Parameters.AddWithValue("@Course", 76);
                                                cmd.Parameters.AddWithValue("@Course2Year", "");
                                                cmd.Parameters.AddWithValue("@Course3Year", "");
                                            }
                                            else if (ds.Tables[0].Rows[i]["CYear"].ToString() == "2" && (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "2" || ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "3"))
                                            {
                                                cmd.Parameters.AddWithValue("@Course", 76);
                                                cmd.Parameters.AddWithValue("@Course2Year", ds.Tables[0].Rows[i]["Course"].ToString());
                                                cmd.Parameters.AddWithValue("@Course3Year", ds.Tables[0].Rows[i]["Course"].ToString());
                                            }


                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Course", ds.Tables[0].Rows[i]["Course"].ToString());
                                            cmd.Parameters.AddWithValue("@Course2Year", "");
                                            cmd.Parameters.AddWithValue("@Course3Year", "");
                                        }
                                        cmd.Parameters.AddWithValue("@StudentName", ds.Tables[0].Rows[i]["StudentName"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@FatherName", ds.Tables[0].Rows[i]["FatherName"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@MotherName", ds.Tables[0].Rows[i]["MotherName"].ToString().ToUpper());

                                        //cmd.Parameters.AddWithValue("@StudentPhoto", FindInfo.findPendingStudentPhoto(Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"])));
                                        cmd.Parameters.AddWithValue("@StudentPhoto", ds.Tables[0].Rows[i]["StudentPhoto"]);

                                        cmd.Parameters.AddWithValue("@Gender", ds.Tables[0].Rows[i]["Gender"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@DOBDay", ds.Tables[0].Rows[i]["DOBDay"].ToString());
                                        cmd.Parameters.AddWithValue("@DOBMonth", ds.Tables[0].Rows[i]["DOBMonth"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@DOBYear", ds.Tables[0].Rows[i]["DOBYear"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@CAddress", ds.Tables[0].Rows[i]["CAddress"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@City", ds.Tables[0].Rows[i]["City"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@District", ds.Tables[0].Rows[i]["District"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@State", ds.Tables[0].Rows[i]["State"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@PinCode", ds.Tables[0].Rows[i]["PinCode"].ToString());
                                        cmd.Parameters.AddWithValue("@PhoneNo", ds.Tables[0].Rows[i]["PhoneNo"].ToString());
                                        cmd.Parameters.AddWithValue("@MobileNo", ds.Tables[0].Rows[i]["MobileNo"].ToString());
                                        cmd.Parameters.AddWithValue("@Email", ds.Tables[0].Rows[i]["Email"].ToString());
                                        cmd.Parameters.AddWithValue("@AadhaarNo", ds.Tables[0].Rows[i]["AadhaarNo"].ToString());
                                        cmd.Parameters.AddWithValue("@DOA", DateTime.Now.ToString("yyyy-MM-dd"));
                                        cmd.Parameters.AddWithValue("@VDOA", FindInfo.findDOAByBatch(Convert.ToInt32(batchid)));
                                        cmd.Parameters.AddWithValue("@DDNumber", "");
                                        cmd.Parameters.AddWithValue("@DDDay", "");
                                        cmd.Parameters.AddWithValue("@DDMonth", "");
                                        cmd.Parameters.AddWithValue("@DDYear", "");
                                        cmd.Parameters.AddWithValue("@IssuingBankName", "");
                                        cmd.Parameters.AddWithValue("@DDAmount", "");
                                        cmd.Parameters.AddWithValue("@DDAmountInwords", "");
                                        cmd.Parameters.AddWithValue("@Nationality", ds.Tables[0].Rows[i]["Nationality"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@Category", ds.Tables[0].Rows[i]["Category"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@Employmentstatus", ds.Tables[0].Rows[i]["Employmentstatus"].ToString().ToUpper());

                                        cmd.Parameters.AddWithValue("@examname1", ds.Tables[0].Rows[i]["examname1"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@examname2", ds.Tables[0].Rows[i]["examname2"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@examname3", ds.Tables[0].Rows[i]["examname3"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@examname4", ds.Tables[0].Rows[i]["examname4"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@examname5", ds.Tables[0].Rows[i]["examname5"].ToString().ToUpper());

                                        cmd.Parameters.AddWithValue("@subject1", ds.Tables[0].Rows[i]["subject1"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@subject2", ds.Tables[0].Rows[i]["subject2"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@subject3", ds.Tables[0].Rows[i]["subject3"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@subject4", ds.Tables[0].Rows[i]["subject4"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@subject5", ds.Tables[0].Rows[i]["subject5"].ToString().ToUpper());

                                        cmd.Parameters.AddWithValue("@YearPass1", ds.Tables[0].Rows[i]["YearPass1"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@YearPass2", ds.Tables[0].Rows[i]["YearPass2"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@YearPass3", ds.Tables[0].Rows[i]["YearPass3"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@YearPass4", ds.Tables[0].Rows[i]["YearPass4"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@YearPass5", ds.Tables[0].Rows[i]["YearPass5"].ToString().ToUpper());

                                        cmd.Parameters.AddWithValue("@UniversityBoard1", ds.Tables[0].Rows[i]["UniversityBoard1"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@UniversityBoard2", ds.Tables[0].Rows[i]["UniversityBoard2"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@UniversityBoard3", ds.Tables[0].Rows[i]["UniversityBoard3"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@UniversityBoard4", ds.Tables[0].Rows[i]["UniversityBoard4"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@UniversityBoard5", ds.Tables[0].Rows[i]["UniversityBoard5"].ToString().ToUpper());

                                        cmd.Parameters.AddWithValue("@Divisiongrade1", ds.Tables[0].Rows[i]["Divisiongrade1"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@Divisiongrade2", ds.Tables[0].Rows[i]["Divisiongrade2"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@Divisiongrade3", ds.Tables[0].Rows[i]["Divisiongrade3"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@Divisiongrade4", ds.Tables[0].Rows[i]["Divisiongrade4"].ToString().ToUpper());
                                        cmd.Parameters.AddWithValue("@Divisiongrade5", ds.Tables[0].Rows[i]["Divisiongrade5"].ToString().ToUpper());

                                        cmd.Parameters.AddWithValue("@Eligible", ds.Tables[0].Rows[i]["Eligible"].ToString());
                                        cmd.Parameters.AddWithValue("@CourseFeePaid", "");
                                        cmd.Parameters.AddWithValue("@FeeRecIssued", "");
                                        cmd.Parameters.AddWithValue("@OriginalsVerified", ds.Tables[0].Rows[i]["OriginalsVerified"].ToString());
                                        cmd.Parameters.AddWithValue("@QualifyingStatus", "AC");

                                        cmd.Parameters.AddWithValue("@RecordStatus", "True");

                                        cmd.Parameters.AddWithValue("@AdmissionStatus", Convert.ToInt32(ds.Tables[0].Rows[i]["AdmissionStatus"]));

                                        cmd.Parameters.AddWithValue("@ReasonIfPending", "NA");

                                        cmd.Connection = con;

                                        if (eno != "AE" && eno != "")
                                        {
                                            con.Open();
                                            object srid = cmd.ExecuteScalar();
                                            con.Close();

                                            FindInfo.updateEnrollmentCounter(batchid, encounter);

                                            updateEnrollStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["PSRID"]));

                                            Log.createLogNow("Create", "Registered a student with Enrollment No'" + eno + "'", 0);

                                            Accounts.fillFee(Convert.ToInt32(srid), 1, accsess, itype, ino, iday, imonth, iyear, ibn, coursefee, Accounts.IntegerToWords(coursefee), totalamount, Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]), exam, frdate, 1);

                                            Log.createLogNow("Fee Submit", "Filled Course Fee of a student with Enrollment No '" + eno + "'", 0);

                                            if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                                            {
                                                updateSCChangeRecord(Convert.ToInt32(srid), ds.Tables[0].Rows[i]["PreviousSCCode"].ToString(), ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                                            }
                                            if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "2" || ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "3")
                                            {
                                                insertCTMRecord(Convert.ToInt32(srid), ds.Tables[0].Rows[i]["PreviousInstitute"].ToString(), exam);
                                            }

                                            //detainStudent(Convert.ToInt32(srid), exam);
                                            //if(!FindInfo.isRegisteredForSLM(Convert.ToInt32(srid), Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"])))
                                            //{
                                            int res = registerForSLM(Convert.ToInt32(srid), ds.Tables[0].Rows[i]["StudyCentreCode"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                                            //}

                                        }
                                        else
                                        {
                                            Response.Write("<br/> Sorry!! This Alloted Enrollment No. " + eno + " is already exist.");
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("<br/> Sorry!! Remaining amount is less than required course fee");
                                        break;
                                    }
                                }


                            }

                            else
                            {


                                Response.Write("<br/> Sorry !! this application no. : " + ano + " is already exist");
                                ano = ano + 1;
                                goto step1;

                            }

                            ano = ano + 1;
                            count = count + 1;
                        }
                        else
                        {
                            Response.Write("<br/> Sorry !! Prospectus series ended");
                            break;
                        }
                       

                    }
                }
            }

            Response.Write("<br/>Total : " + (count).ToString() + " Students uploaded.");
            Response.Write("<br/> ANo range alloted from " + (sano).ToString() + " To " + (ano - 1).ToString());
        }

        private void detainStudent(int srid, string exam)
        {
            string eno = FindInfo.findENoByID(srid);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDEDetainedStudents values(@SRID,@Examination,@MOE,@DetainedStatus,@Remark)", con);


            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@Examination", exam);
            cmd.Parameters.AddWithValue("@MOE", "R");
            cmd.Parameters.AddWithValue("@DetainedStatus", "True");
            cmd.Parameters.AddWithValue("@Remark", "DETAINED TILL DOCUMENT VERIFICATION");


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Log.createLogNow("Detain Student", "Detain Student in 'REGULAR'" + exam + " Examination with Enrollment No '" + eno, 0);

        }

        private bool validINo(int fhfee, int itype, string ino, string idate, string ibn, int cfee, out string error)
        {
            error = "";
            bool valid = false;
            int fhusedfee = Accounts.findUsedAmountOfDraftByFH(1, itype, ino, idate, ibn);

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

        private bool newApplication(int psrid)
        {
            bool newstudent = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PSRID from DDEStudentRecord where PSRID='" + psrid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                newstudent = false;
            }
            else
            {
                newstudent = true;
            }

            con.Close();

            return newstudent;
        }

        private int registerForSLM(int srid, string sccode, int cid, int year)
        {
            int res = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDESLMIssueRecord values(@SRID,@SCCode,@CID,@Year,@TOR,@LNo)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@SCCode", sccode);
            cmd.Parameters.AddWithValue("@CID", cid);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@TOR", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@LNo", 0);


            con.Open();
            res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        private void insertCTMRecord(int srid, string preins, string exam)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDECTPaperRecord values(@SRID,@Paper1,@Paper2,@Exam)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            if (preins == "1")
            {
                cmd.Parameters.AddWithValue("@Paper1", "CTM 1");
                cmd.Parameters.AddWithValue("@Paper2", "CTM 2");
            }
            else if (preins == "2")
            {
                cmd.Parameters.AddWithValue("@Paper1", "CTM 3");
                cmd.Parameters.AddWithValue("@Paper2", "CTM 4");
            }
            cmd.Parameters.AddWithValue("@Exam", exam);


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void updateSCChangeRecord(int srid, string presc, string currsc)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into DDEChangeSCRecord values(@SRID,@PreviousSC,@CurrentSC,@TimeOfChange)", con);

            cmd.Parameters.AddWithValue("@SRID", srid);
            cmd.Parameters.AddWithValue("@PreviousSC", presc);
            cmd.Parameters.AddWithValue("@CurrentSC", currsc);
            cmd.Parameters.AddWithValue("@TimeOfChange", DateTime.Now.ToString());


            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


        }

        private void updateEnrollStatus(int psrid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEPendingStudentRecord set Enrolled=@Enrolled where PSRID='" + psrid + "' ", con);

            cmd.Parameters.AddWithValue("@Enrolled", "True");

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private bool newStudent(int ano)
        {
            bool newstudent = true;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ApplicationNo from DDEStudentRecord where ApplicationNo='" + ano + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                newstudent = false;
            }

            con.Close();

            return newstudent;
        }

        private string allotEnrollmentNo(int cid,int batchid, string sess, string sccode, out int encounter)
        {
            string eno = "";
            encounter = 0;
            int pcode = FindInfo.findProgrammeCode(cid.ToString());
            if(batchid>=23)
            {
                encounter = FindInfo.findENoCounterByExamCode(FindInfo.findApplicableExamByBatch(sess));
            }
            else
            {
                encounter = FindInfo.findCounter(sess);
            }
           

            string finalcounter = string.Format("{0:00000}", encounter);

            eno = "Z1120" + pcode.ToString() + finalcounter;

            return eno;

        }

        protected void btnDetain_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where StudyCentreCode='999' and [Session]='Q 2018-19' and SRID>=313849", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
         
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                detainStudent(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]),"W10");
            }
         }

        //protected void btnUpdateANo_Click(object sender, EventArgs e)
        //{
        //int count = 229245;

        //SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //SqlCommand cmd1 = new SqlCommand("Select SRID from DDEStudentRecord where StudyCentreCode='998' and [Session]='A 2017-18' and DOA='2018-06-17' and ApplicationNo<227741", con1);

        //SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //DataSet ds = new DataSet();
        //da.Fill(ds);


        //for (int i = 0; i < 122; i++)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEStudentRecord set ApplicationNo=@ApplicationNo where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) + "' ", con);

        //    cmd.Parameters.AddWithValue("@ApplicationNo", count.ToString());

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    count = count + 1;
        //}
        //}
    }
}