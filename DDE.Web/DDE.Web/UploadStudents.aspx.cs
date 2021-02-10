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
using System.Data.SqlClient;
using System.Data.OleDb;
using DDE.DAL;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Drawing;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace DDE.Web
{
    public partial class UploadStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    PopulateDDList.populateExam(ddlistExam);

            //    //populateCourses();


            //}

        }



        //protected void btnUploadExaminer_Click(object sender, EventArgs e)
        //{
        //    int counter = 0;
        //    string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\data\Exam data\Examiners_A18.xls;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
        //    string queryString = "SELECT * FROM [Internal$]";

        //    using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    {

        //        OleDbCommand command = new OleDbCommand(queryString, connection);
        //        OleDbDataAdapter da = new OleDbDataAdapter(command);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);


        //        int j = 0;
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {


        //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd = new SqlCommand("insert into DDEExaminers (A18,ExCode_A18,Type,PreFix,Name,Address,ContactNo,Qualification,WStatus,DetailsIfWorking,Specialization,AccountNo,BankName,BankIFSC,BranchName,RefferredBy,PAN,Resume,Remarks) values (@A18,@ExCode_A18,@Type,@PreFix,@Name,@Address,@ContactNo,@Qualification,@WStatus,@DetailsIfWorking,@Specialization,@AccountNo,@BankName,@BankIFSC,@BranchName,@RefferredBy,@PAN,@Resume,@Remarks)", con);


        //            cmd.Parameters.AddWithValue("@A18", "True");
        //            cmd.Parameters.AddWithValue("@ExCode_A18", ds.Tables[0].Rows[i]["ExCode"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@Type", ds.Tables[0].Rows[i]["Type"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@PreFix", ds.Tables[0].Rows[i]["PreFix"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@Name", ds.Tables[0].Rows[i]["Name"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@Address", ds.Tables[0].Rows[i]["Address"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@ContactNo", ds.Tables[0].Rows[i]["ContactNo"].ToString());
        //            cmd.Parameters.AddWithValue("@Qualification", ds.Tables[0].Rows[i]["Qualification"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@WStatus", ds.Tables[0].Rows[i]["WStatus"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@DetailsIfWorking", ds.Tables[0].Rows[i]["DetailsIfWorking"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@Specialization", ds.Tables[0].Rows[i]["Specialization"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@AccountNo", ds.Tables[0].Rows[i]["AccountNo"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@BankName", ds.Tables[0].Rows[i]["BankName"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@BankIFSC", ds.Tables[0].Rows[i]["BankIFSC"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@BranchName", ds.Tables[0].Rows[i]["BranchName"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@RefferredBy", ds.Tables[0].Rows[i]["RefferredBy"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@PAN", ds.Tables[0].Rows[i]["PAN"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@Resume", ds.Tables[0].Rows[i]["Resume"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@Remarks", ds.Tables[0].Rows[i]["Remarks"].ToString().ToUpper());



        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();

        //            counter = counter + 1;

        //            OleDbConnection.ReleaseObjectPool();


        //        }

        //        Response.Write("<br/>" + counter.ToString() + " Examiner uploaded");
        //    }

        //}

        //protected void btnLock_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select * from DDERefundLetterRecord", con);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);


        //    int j = 0;
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {

        //            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd2 = new SqlCommand("update DDEFeeInstruments set RG=@RG,Lock=@Lock where IID='" + ds.Tables[0].Rows[i]["IID"].ToString() + "'", con2);

        //            cmd2.Parameters.AddWithValue("@RG", "True");
        //            cmd2.Parameters.AddWithValue("@Lock", "True");

        //            con2.Open();
        //            cmd2.ExecuteNonQuery();
        //            con2.Close();

        //            j = j + 1;




        //    }
        //    Response.Write(j + "rows updated");
        //}



        //protected void btnPEReport_Click(object sender, EventArgs e)
        //{
        //    populateExamReport();
        //}

        //private void populateExamReport()
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand(findERQuery(), con);
        //    SqlDataReader dr;

        //    DataTable dt = new DataTable();

        //    DataColumn dtcol1 = new DataColumn("SNo");
        //    DataColumn dtcol2 = new DataColumn("EnrollmentNo");
        //    DataColumn dtcol3 = new DataColumn("RollNo");
        //    DataColumn dtcol4 = new DataColumn("StudentName");
        //    DataColumn dtcol5 = new DataColumn("FatherName");
        //    DataColumn dtcol6 = new DataColumn("Course");
        //    DataColumn dtcol7 = new DataColumn("Year");
        //    DataColumn dtcol8 = new DataColumn("Gender");
        //    DataColumn dtcol9 = new DataColumn("Category");
        //    DataColumn dtcol10 = new DataColumn("MaxMarks");
        //    DataColumn dtcol11 = new DataColumn("ObtainedMarks");
        //    DataColumn dtcol12 = new DataColumn("Percentage");
        //    DataColumn dtcol13 = new DataColumn("Filter");
        //    DataColumn dtcol14 = new DataColumn("Result");

        //    dt.Columns.Add(dtcol1);
        //    dt.Columns.Add(dtcol2);
        //    dt.Columns.Add(dtcol3);
        //    dt.Columns.Add(dtcol4);
        //    dt.Columns.Add(dtcol5);
        //    dt.Columns.Add(dtcol6);
        //    dt.Columns.Add(dtcol7);
        //    dt.Columns.Add(dtcol8);
        //    dt.Columns.Add(dtcol9);
        //    dt.Columns.Add(dtcol10);
        //    dt.Columns.Add(dtcol11);
        //    dt.Columns.Add(dtcol12);
        //    dt.Columns.Add(dtcol13);
        //    dt.Columns.Add(dtcol14);

        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    int i = 1;
        //    while (dr.Read())
        //    {
        //        DataRow drow = dt.NewRow();
        //        drow["SNo"] = i;
        //        drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
        //        drow["RollNo"] = Convert.ToString(dr["RollNo"]);
        //        drow["StudentName"] = Convert.ToString(dr["StudentName"]);
        //        drow["FatherName"] = Convert.ToString(dr["FatherName"]);
        //        if (Convert.ToString(dr["MOE"]) == "R")
        //        {

        //            drow["Year"] = Convert.ToString(dr["Year"]);
        //            int year=Convert.ToInt32(drow["Year"]);
        //            if (Convert.ToString(dr["Course"]) == "MBA")
        //            {
        //                if (year == 1)
        //                {
        //                    drow["Course"] = Convert.ToString(dr["Course"]);
        //                }
        //                else if (year == 2)
        //                {
        //                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course2Year"])); ;
        //                }
        //                else if (year == 3)
        //                {
        //                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course3Year"])); ;
        //                }
        //            }
        //            else
        //            {
        //                drow["Course"] = Convert.ToString(dr["Course"]);
        //            }
        //        }
        //        else if (Convert.ToString(dr["MOE"]) == "B")
        //        {

        //            drow["Year"] = FindInfo.findBPYear(Convert.ToInt32(dr["SRID"]),ddlistExam.SelectedItem.Value);
        //            drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(drow["Year"]));
        //        }
        //        drow["Gender"] = Convert.ToString(dr["Gender"]);
        //        drow["Category"] = Convert.ToString(dr["Category"]);
        //        drow["MaxMarks"] = Convert.ToString(dr["MaxMarks"]);
        //        drow["ObtainedMarks"] = Convert.ToString(dr["ObtMarks"]);
        //        if (Convert.ToInt32(dr["MaxMarks"]) != 0)
        //        {
        //            drow["Percentage"] =((Convert.ToDouble(dr["ObtMarks"])/Convert.ToDouble(dr["MaxMarks"])) * 100).ToString("00.00");
        //            if (Convert.ToDouble(drow["Percentage"]) < 60.00)
        //            {
        //                drow["Filter"] = "BELOW 60";
        //            }
        //            else
        //            {
        //                drow["Filter"] = "60 AND ABOVE";
        //            }
        //        }
        //        else
        //        {
        //            drow["Percentage"] = "0";                
        //            drow["Filter"] = "NA";

        //        }
        //        if (Convert.ToString(dr["QualifyingStatus"]) == "")
        //        {
        //            drow["Result"] = "NA";
        //        }
        //        else
        //        {
        //            drow["Result"] = Convert.ToString(dr["QualifyingStatus"]);
        //        }

        //        dt.Rows.Add(drow);
        //        i = i + 1;
        //    }

        //    gvPER.DataSource = dt;
        //    gvPER.DataBind();
        //    con.Close();
        //}

        //private string findERQuery()
        //{
        //    string exam = ddlistExam.SelectedItem.Value;
        //    StringBuilder sb = new StringBuilder();


        //    sb.Append("select distinct DDEExamRecord_"+exam+".SRID,");        
        //    sb.Append("DDEStudentRecord.EnrollmentNo,");
        //    sb.Append("DDEExamRecord_" + exam + ".RollNo,");
        //    sb.Append("DDEStudentRecord.StudentName,");
        //    sb.Append("DDEStudentRecord.FatherName,");
        //    sb.Append("DDECourse.CourseName as Course,");
        //    sb.Append("DDEStudentRecord.Course2Year,");
        //    sb.Append("DDEStudentRecord.Course3Year,");
        //    sb.Append("DDEExamRecord_" + exam + ".Year,");
        //    sb.Append("DDEStudentRecord.Gender,");
        //    sb.Append("DDEStudentRecord.Category,");
        //    sb.Append("DDEExamRecord_" + exam + ".MaxMarks,");
        //    sb.Append("DDEExamRecord_" + exam + ".ObtMarks,");
        //    sb.Append("DDEExamRecord_" + exam + ".QualifyingStatus,");
        //    sb.Append("DDEExamRecord_" + exam + ".MOE");

        //    sb.Append(" from DDEExamRecord_" + exam);
        //    sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + exam + ".SRID");
        //    sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course order by DDEStudentRecord.EnrollmentNo");


        //    return sb.ToString();


        //}

        //protected void btnShowPhoto_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select top 58 PSRID,EnrollmentNo from DDEStudentRecord where PSRID!='0' order by PSRID", con);
        //    SqlDataReader dr;

        //    DataTable dt = new DataTable();

        //    DataColumn dtcol1 = new DataColumn("SNo");
        //    DataColumn dtcol2 = new DataColumn("PSRID");
        //    DataColumn dtcol3 = new DataColumn("Photo");



        //    dt.Columns.Add(dtcol1);
        //    dt.Columns.Add(dtcol2);
        //    dt.Columns.Add(dtcol3);

        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    int i = 1;
        //    while (dr.Read())
        //    {
        //        DataRow drow = dt.NewRow();
        //        drow["SNo"] = i;
        //        drow["PSRID"] = Convert.ToString(dr["PSRID"]);
        //        drow["Photo"] = "<a href='OA/" + dr["EnrollmentNo"].ToString() + ".jpg'>" + dr["EnrollmentNo"].ToString() + "</a>";

        //        dt.Rows.Add(drow);
        //        i = i + 1;
        //    }

        //    gvPhoto.DataSource = dt;
        //    gvPhoto.DataBind();
        //    con.Close();
        //}



        //protected void Unnamed2_Click(object sender, EventArgs e)
        //{
        //    tb.Font.Name = new Font("krutidev", 10).ToString();

        //}

        //protected void btnUploadSLM_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select * from DDESLMLinking", con);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);


        //    int j = 0;
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        int slmid = findSLMID(ds.Tables[0].Rows[i]["SLMCode"].ToString());


        //        if (slmid != 0)
        //        {
        //            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd2 = new SqlCommand("update DDESLMLinking set SLMCode=@SLMCode where SLMCode='" + ds.Tables[0].Rows[i]["SLMCode"].ToString() + "'", con2);

        //            cmd2.Parameters.AddWithValue("@SLMCode", slmid);


        //            con2.Open();
        //            cmd2.ExecuteNonQuery();
        //            con2.Close();

        //            j = j + 1;
        //        }



        //    }
        //    Response.Write(j+"rows updated");


        //}

        //private int findSLMID(string slmcode)
        //{
        //    int slmid = 0;

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select SLMID from DDESLMMaster where SLMCode='" + slmcode + "'", con);
        //    SqlDataReader dr;
        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        slmid =Convert.ToInt32(dr["SLMID"]) ;
        //    }

        //    con.Close();

        //    return slmid;
        //}

        //private bool slmAlreadyExist(string slmcode)
        //{
        //    bool exist = false;

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select * from DDESLMMaster where SLMCode='" + slmcode + "'", con);
        //    SqlDataReader dr;
        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        exist = true;
        //    }

        //    con.Close();

        //    return exist;
        //}



        //protected void btnSetForExam_Click(object sender, EventArgs e)
        //{
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd = new SqlCommand("Select SRID,ApplicationNo,EnrollmentNo from DDEStudentRecord where [Session]='A 2013-14' order by SRID", con);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);


        //        int j = 0;
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);

        //            if (!examsetOnAdmissionFee(srid))
        //            {
        //                //int srids = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
        //                //SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //                //SqlCommand cmd2 = new SqlCommand("update [DDEFeeRecord_2013-14] set ForExam=@ForExam where SRID='" + srids + "' and FeeHead='1' ", con2);

        //                //cmd2.Parameters.AddWithValue("@ForExam", "A14");

        //                //con2.Open();
        //                //cmd2.ExecuteNonQuery();
        //                //con2.Close();
        //                j = j + 1;
        //                Response.Write("<br/>" + j + "-" + ds.Tables[0].Rows[i]["ApplicationNo"].ToString() + "-" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
        //            }
        //        }


        //}

        //private bool examsetOnAdmissionFee(int srid)
        //{
        //    bool set = false;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select distinct SRID from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='1' and ForExam='A14' union Select distinct SRID from [DDEFeeRecord_2014-15] where SRID='" + srid + "' and FeeHead='1' and ForExam='A14' union Select distinct SRID from [DDEFeeRecord_2015-16] where SRID='" + srid + "' and FeeHead='1' and ForExam='A14'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();


        //    if (dr.HasRows)
        //    {

        //                set = true;

        //    }

        //    con.Close();
        //    return set;
        //}

        //protected void btnUpdtaeDuplicateRollNo_Click(object sender, EventArgs e)
        //{

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select distinct RollNo from DDEExamRecord_A14 where MOE='R'", con);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);



        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd1 = new SqlCommand("Select * from DDEExamRecord_A14 where RollNo='X" + ds.Tables[0].Rows[i]["RollNo"].ToString() + "' ", con1);

        //        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //        DataSet ds1 = new DataSet();
        //        da1.Fill(ds1);

        //        int bs = ds1.Tables[0].Rows.Count;

        //        if (bs > 1)
        //        {
        //            //for (int j = 0; j < (bs - 1); j++)
        //            //{
        //            //    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            //    SqlCommand cmd2 = new SqlCommand("update DDEExamRecord_A14 set RollNo=@RollNo where ExamRecordID='" + ds1.Tables[0].Rows[j]["ExamRecordID"].ToString() + "' ", con2);
        //            //    int srid = Convert.ToInt32(ds1.Tables[0].Rows[j]["SRID"]);
        //            //    int cid = FindInfo.findCourseIDBySRID(srid);
        //            //    int counter;

        //            //    string rollno = Exam.allotRollNo(srid, FindInfo.findENoByID(srid), cid, "A14", "R", out counter);



        //            //    cmd2.Parameters.AddWithValue("@RollNo", rollno);

        //            //    con2.Open();
        //            //    cmd2.ExecuteNonQuery();
        //            //    con2.Close();

        //            //    if (counter != 0)
        //            //    {
        //            //        FindInfo.updateRollNoCounter(cid, counter, "A14");
        //            //    }


        //            //}
        //            string srid = "";
        //            for (int j = 0; j < (bs - 1); j++)
        //            {
        //                srid =srid+" - "+ ds.Tables[0].Rows[i]["SRID"].ToString();
        //            }

        //            Response.Write("<br/>" + ds.Tables[0].Rows[i]["RollNo"].ToString() + " - " + bs+"<br/>"+srid);

        //        }



        //    }
        //}





        //protected void btnUpdateYear_Click(object sender, EventArgs e)
        //{
        //        Session["misseno"] = "";
        //        Session["cn"] = 0;
        //        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\10-10-2013\C-2012_2Year.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //        string queryString = "SELECT * FROM [Sheet1$]";
        //        OleDbDataReader dr;
        //        string eno = "";
        //        try
        //        {

        //            using (OleDbConnection connection = new OleDbConnection(connectionString))
        //            {

        //                OleDbCommand command = new OleDbCommand(queryString, connection);
        //                connection.Open();
        //                dr = command.ExecuteReader();
        //                while (dr.Read())
        //                {

        //                    eno = dr["EnrollmentNo"].ToString();
        //                    int srid = FindInfo.findSRIDByENo(eno);
        //                    if (srid == 0)
        //                    {
        //                        Session["misseno"] = Session["misseno"] + "<br/>" + eno;
        //                    }
        //                    else
        //                    {
        //                        updateYear(srid, Convert.ToInt32(dr["Year"]));
        //                    }

        //                }
        //                dr.Close();
        //                connection.Close();
        //                OleDbConnection.ReleaseObjectPool();
        //                Response.Write("<br/>Missed Enrollment No are : " + Session["eno"].ToString());
        //                Response.Write("<br/>"+Session["cn"].ToString()+" Students updated");
        //            }
        //        }
        //        catch (Exception er)
        //        {
        //            Response.Write("Error occured with Enrollment No : " + eno);
        //            Response.Write("<br/>Error description : " + er.Message);
        //            Response.Write("<br/>Missed Enrollment No are : " + Session["misseno"].ToString());
        //            Response.Write("<br/>" + Session["cn"].ToString() + " Students updated");
        //        }

        //}

        //private void updateYear(int srid, int year)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEStudentRecord set CYear=@CYear where SRID='" + srid + "'", con);

        //    cmd.Parameters.AddWithValue("@CYear", year);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    Session["cn"] = Convert.ToInt32(Session["cn"]) + 1;

        //}

        //protected void btnUpdateRecord_Click(object sender, EventArgs e)
        //{
        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\AdmissionData\21-11-2013\A2012-13.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet1$]";
        //    OleDbDataReader dr;
        //    string eno = "";
        //    try
        //    {

        //        using (OleDbConnection connection = new OleDbConnection(connectionString))
        //        {

        //            OleDbCommand command = new OleDbCommand(queryString, connection);
        //            connection.Open();
        //            dr = command.ExecuteReader();
        //            while (dr.Read())
        //            {

        //                eno = dr["EnrollmentNo"].ToString();
        //                int srid = FindInfo.findSRIDByENo(eno);
        //                if (srid == 0)
        //                {
        //                    Session["misseno"] = Session["misseno"] + "<br/>" + eno;
        //                }
        //                else
        //                {
        //                    updateRecord(srid, dr["Gender"].ToString(), dr["Category"].ToString());
        //                }

        //            }
        //            dr.Close();
        //            connection.Close();
        //            OleDbConnection.ReleaseObjectPool();
        //            Response.Write("<br/>Missed Enrollment No are : " + Session["eno"].ToString());
        //            Response.Write("<br/>" + Session["cn"].ToString() + " Students updated");
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        Response.Write("Error occured with Enrollment No : " + eno);
        //        Response.Write("<br/>Error description : " + er.Message);
        //        Response.Write("<br/>Missed Enrollment No are : " + Session["misseno"].ToString());
        //        Response.Write("<br/>" + Session["cn"].ToString() + " Students updated");
        //    }

        //}

        //private void updateRecord(int srid, string Gender, string cat)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEStudentRecord set Gender=@Gender,Category=@Category where SRID='" + srid + "'", con);

        //    cmd.Parameters.AddWithValue("@Gender", Gender);
        //    cmd.Parameters.AddWithValue("@Category", cat);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    Session["cn"] = Convert.ToInt32(Session["cn"]) + 1;
        //}





        //    con.Close();
        //}

        //private bool isPasswordExist(int pass)
        //{
        //    bool exist = true;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select Password from DDEExaminationCentres_A15 where Password='" + pass + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();


        //    if (dr.HasRows)
        //    {
        //        exist = true;
        //    }
        //    else
        //    {
        //        exist = false;
        //    }

        //    con.Close();
        //    return exist;
        //}

        //private void updatePassword(int ecid, string pass)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEExaminationCentres_A15 set Password=@Password,NoTimesLoggedIn=@NoTimesLoggedIn where ECID='" + ecid + "'", con);

        //    cmd.Parameters.AddWithValue("@Password", pass);
        //    cmd.Parameters.AddWithValue("@NoTimesLoggedIn", 0);
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //protected void btnAssignICardNo_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select SRID,EnrollmentNo,StudyCentreCode from DDEStudentRecord where Session='C 2013' and AdmissionStatus='1'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();


        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            updateICardNo(Convert.ToInt32(dr[0]),dr[1].ToString(),dr[2].ToString());
        //        }
        //    }

        //    con.Close();
        //}

        //private void updateICardNo(int srid, string eno, string sccode)
        //{
        //    string icardno = eno.Substring(0, 3) + "-" + sccode + eno.Substring(6,5);
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEStudentRecord set ICardNo=@ICardNo where SRID='" + srid + "'", con);

        //    cmd.Parameters.AddWithValue("@ICardNo", icardno);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //private void populateCourses()
        //{

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse order by CourseShortName", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        if (dr[2].ToString() == "")
        //        {
        //            ddlistCourse.Items.Add(dr[1].ToString());
        //            ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
        //        }

        //        else
        //        {
        //            ddlistCourse.Items.Add(dr[1].ToString() + " (" + dr[2].ToString()+")");
        //            ddlistCourse.Items.FindByText(dr[1].ToString() + " (" + dr[2].ToString() + ")").Value = dr[0].ToString();

        //        }


        //    }
        //    con.Close();


        //}

        //protected void btnUploadStudents_Click(object sender, EventArgs e)
        //{

        //   string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\AdmissionData\new 1\A-2012-13 (I Year) S.No-8760 to 8761.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //   string queryString = "SELECT * FROM [Sheet1$]";
        //   OleDbDataReader dr;
        //   string eno = "";
        //   int i = 0;
        //   try
        //   {


        //    using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    {

        //        OleDbCommand command = new OleDbCommand(queryString, connection);
        //        connection.Open();
        //        dr = command.ExecuteReader();

        //        while (dr.Read())
        //        {
        //        if (validStudent(dr["EnrollmentNo"].ToString()))
        //        {

        //            eno = dr["EnrollmentNo"].ToString();
        //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd = new SqlCommand("insert into DDEStudentRecord values(@ApplicationNo,@SCStatus,@StudyCentreCode,@Session,@EnrollmentNo,@AdmissionThrough,@AdmissionType,@PreviousInstitute,@PreviousCourse,@RollNoIYear,@RollNoIIYear,@RollNoIIIYear,@RollNoBP,@CYear,@Course,@Course2Year,@Course3Year,@StudentName,@FatherName,@Gender,@DOBDay,@DOBMonth,@DOBYear,@CAddress,@City,@District,@State,@PinCode,@PhoneNo,@MobileNo,@Email,@DOA,@DDNumber,@DDDay,@DDMonth,@DDYear,@IssuingBankName,@DDAmount,@DDAmountInwords,@Nationality,@Category,@Employmentstatus,@examname1,@examname2,@examname3,@examname4,@subject1,@subject2,@subject3,@subject4,@YearPass1,@YearPass2,@YearPass3,@YearPass4,@UniversityBoard1,@UniversityBoard2,@UniversityBoard3,@UniversityBoard4,@Divisiongrade1,@Divisiongrade2,@Divisiongrade3,@Divisiongrade4,@Eligible,@CourseFeePaid,@FeeRecIssued,@OriginalsVerified,@QualifyingStatus,@RecordStatus)", con);

        //            cmd.Parameters.AddWithValue("@ApplicationNo", dr["ApplicationNo"].ToString());
        //            cmd.Parameters.AddWithValue("@SCStatus", "O");
        //            cmd.Parameters.AddWithValue("@StudyCentreCode", dr["StudyCentreCode"].ToString());
        //            cmd.Parameters.AddWithValue("@Session", "A 2012-13");
        //            cmd.Parameters.AddWithValue("@EnrollmentNo", dr["EnrollmentNo"].ToString());
        //            cmd.Parameters.AddWithValue("@AdmissionThrough", findAdmissionThrough(dr["AdmissionThrough"].ToString()));

        //            //if (dr["AdmissionType"].ToString()=="RG")
        //            //{
        //                cmd.Parameters.AddWithValue("@AdmissionType", 1);
        //                cmd.Parameters.AddWithValue("@PreviousInstitute", 0);
        //                cmd.Parameters.AddWithValue("@PreviousCourse", 0);

        //            //}
        //            //else if (dr["AdmissionType"].ToString() == "CT")
        //            //{
        //            //    cmd.Parameters.AddWithValue("@AdmissionType", 2);
        //            //    cmd.Parameters.AddWithValue("@PreviousInstitute", findPreviousInstitute(dr["PreviousInstitute"].ToString()));
        //            //    cmd.Parameters.AddWithValue("@PreviousCourse", findCourseID(dr["PreviousCourse"].ToString().ToUpper()));

        //            //}

        //            cmd.Parameters.AddWithValue("@RollNoIYear", "");
        //            cmd.Parameters.AddWithValue("@RollNoIIYear", "");
        //            cmd.Parameters.AddWithValue("@RollNoIIIYear", "");
        //            cmd.Parameters.AddWithValue("@RollNoBP", "");
        //            cmd.Parameters.AddWithValue("@CYear", 1);                   
        //            cmd.Parameters.AddWithValue("@Course", findCourseID(dr["Course"].ToString().ToUpper()));
        //            cmd.Parameters.AddWithValue("@Course2Year", "");
        //            cmd.Parameters.AddWithValue("@Course3Year", "");
        //            cmd.Parameters.AddWithValue("@StudentName", dr["StudentName"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@FatherName", dr["FatherName"].ToString().ToUpper());
        //            cmd.Parameters.AddWithValue("@Gender", "");
        //            cmd.Parameters.AddWithValue("@DOBDay", "");
        //            cmd.Parameters.AddWithValue("@DOBMonth", "");
        //            cmd.Parameters.AddWithValue("@DOBYear", "");
        //            cmd.Parameters.AddWithValue("@CAddress", "");
        //            cmd.Parameters.AddWithValue("@City", "");
        //            cmd.Parameters.AddWithValue("@District", "");
        //            cmd.Parameters.AddWithValue("@State", "");
        //            cmd.Parameters.AddWithValue("@PinCode", "");
        //            cmd.Parameters.AddWithValue("@PhoneNo", "");
        //            cmd.Parameters.AddWithValue("@MobileNo", "");
        //            cmd.Parameters.AddWithValue("@Email", "");
        //            cmd.Parameters.AddWithValue("@DOA", "");
        //            cmd.Parameters.AddWithValue("@DDNumber", "");
        //            cmd.Parameters.AddWithValue("@DDDay", "");
        //            cmd.Parameters.AddWithValue("@DDMonth", "");
        //            cmd.Parameters.AddWithValue("@DDYear", "");
        //            cmd.Parameters.AddWithValue("@IssuingBankName", "");
        //            cmd.Parameters.AddWithValue("@DDAmount", "");
        //            cmd.Parameters.AddWithValue("@DDAmountInwords", "");
        //            cmd.Parameters.AddWithValue("@Nationality", "");
        //            cmd.Parameters.AddWithValue("@Category", "");
        //            cmd.Parameters.AddWithValue("@Employmentstatus", "");
        //            cmd.Parameters.AddWithValue("@examname1", "");
        //            cmd.Parameters.AddWithValue("@examname2", "");
        //            cmd.Parameters.AddWithValue("@examname3", "");
        //            cmd.Parameters.AddWithValue("@examname4", "");
        //            cmd.Parameters.AddWithValue("@subject1", "");
        //            cmd.Parameters.AddWithValue("@subject2", "");
        //            cmd.Parameters.AddWithValue("@subject3", "");
        //            cmd.Parameters.AddWithValue("@subject4", "");
        //            cmd.Parameters.AddWithValue("@YearPass1", "");
        //            cmd.Parameters.AddWithValue("@YearPass2", "");
        //            cmd.Parameters.AddWithValue("@YearPass3", "");
        //            cmd.Parameters.AddWithValue("@YearPass4", "");
        //            cmd.Parameters.AddWithValue("@UniversityBoard1", "");
        //            cmd.Parameters.AddWithValue("@UniversityBoard2", "");
        //            cmd.Parameters.AddWithValue("@UniversityBoard3", "");
        //            cmd.Parameters.AddWithValue("@UniversityBoard4", "");
        //            cmd.Parameters.AddWithValue("@Divisiongrade1", "");
        //            cmd.Parameters.AddWithValue("@Divisiongrade2", "");
        //            cmd.Parameters.AddWithValue("@Divisiongrade3", "");
        //            cmd.Parameters.AddWithValue("@Divisiongrade4", "");
        //            cmd.Parameters.AddWithValue("@Eligible", "");
        //            cmd.Parameters.AddWithValue("@CourseFeePaid", "");
        //            cmd.Parameters.AddWithValue("@FeeRecIssued", "");
        //            cmd.Parameters.AddWithValue("@OriginalsVerified", "");
        //            cmd.Parameters.AddWithValue("@QualifyingStatus", "AC");
        //            cmd.Parameters.AddWithValue("@RecordStatus", "True");


        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();

        //            i = i+1;
        //          }
        //         }
        //        dr.Close();
        //        connection.Close();
        //        OleDbConnection.ReleaseObjectPool();

        //        Response.Write(i.ToString()+" Students Uploaded");
        //      }
        //   }
        //   catch (Exception er)
        //   {
        //       Response.Write(i.ToString() + " Students Uploaded");
        //       Response.Write("<br/>Error occured with Enrollment No : " + eno);
        //       Response.Write("Error Msg : "+er.Message);


        //   }
        //}

        //private object findPreviousInstitute(string pi)
        //{
        //    if (pi == "SVSU")
        //    {
        //        return 1;
        //    }
        //    else if (pi == "OTHER")
        //    {
        //        return 2;
        //    }


        //    else
        //    {
        //        return 0;
        //    }
        //}



        //private int findAdmissionThrough(string at)
        //{
        //    if (at == "DIRECT" || at=="D")
        //    {
        //        return 1;
        //    }
        //    else if (at == "WEM" || at == "W")
        //    {
        //        return 2;
        //    }
        //    else if (at == "WEM to DIRECT" || at == "W to D") 
        //    {
        //        return 3;
        //    }

        //    else
        //    {
        //        return 0; 
        //    }
        //}



        //private bool validStudent(string eno)
        //{
        //    bool newstudent = false;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where EnrollmentNo='"+eno+"'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();


        //    if (dr.HasRows)
        //    {
        //        newstudent = false;
        //    }
        //    else
        //    {
        //        newstudent = true;
        //    }


        //    con.Close();
        //    return newstudent;

        //}

        //private string findGender(string gen)
        //{
        //    if(gen=="M")
        //    {
        //        return "MALE";
        //    }

        //    else if(gen=="F")
        //    {
        //        return "FEMALE";
        //    }

        //    else
        //    {
        //        return "";
        //    }
        //}

        //private string findMonthName(string monthno)
        //{
        //    if (monthno == "01")
        //    {
        //        return "JANUARY";
        //    }
        //    else if (monthno == "02")
        //    {
        //        return "FEBRUARY";
        //    }
        //    else if (monthno == "03")
        //    {
        //        return "MARCH";
        //    }
        //    else if (monthno == "04")
        //    {
        //        return "APRIL";
        //    }
        //    else if (monthno == "05")
        //    {
        //        return "MAY";
        //    }
        //    else if (monthno == "06")
        //    {
        //        return "JUNE";
        //    }
        //    else if (monthno == "07")
        //    {
        //        return "JULY";
        //    }
        //    else if (monthno == "08")
        //    {
        //        return "AUGUST";
        //    }
        //    else if (monthno == "09")
        //    {
        //        return "SEPTEMBER";
        //    }
        //    else if (monthno == "10")
        //    {
        //        return "OCTOBER";
        //    }
        //    else if (monthno == "11")
        //    {
        //        return "NOVEMBER";
        //    }
        //    else if (monthno == "12")
        //    {
        //        return "DECEMBER";
        //    }

        //    else return "";


        //}

        //private int findCourseID(string course)
        //{
        //    char[] ch = {' '};

        //    int cid = Convert.ToInt32(ddlistCourse.Items.FindByText(course.TrimEnd(ch)).Value);
        //    return cid;
        //}

        //protected void btnUploadAData_Click(object sender, EventArgs e)
        //{
        //    Session["counter"] = 0;
        //    Session["eno"] = "";
        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\Exam data\amit ji.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet3$]";
        //    OleDbDataReader dr;
        //    string eno = "";
        //    try
        //    {

        //    using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    {

        //        OleDbCommand command = new OleDbCommand(queryString, connection);
        //        connection.Open();
        //        dr = command.ExecuteReader();
        //        while (dr.Read())
        //        {


        //                eno = dr["EnrollmentNo"].ToString();
        //                int srid = FindInfo.findSRIDByENo(eno);
        //                if (srid == 0)
        //                {
        //                    Session["eno"] = Session["eno"] + "<br/>" + eno;
        //                }
        //                else
        //                {
        //                    markFeeofStudent(srid, FindInfo.findBatchBySRID(srid), 3);
        //                }

        //        }
        //        dr.Close();
        //        connection.Close();
        //        OleDbConnection.ReleaseObjectPool();

        //        Response.Write("Missed Enrollment No are : " + Session["eno"].ToString());
        //        Response.Write("<br/>"+Session["Counter"].ToString()+" Students Uploaded");
        //    }
        //    }
        //    catch (Exception er)
        //    {
        //        Response.Write("Error occured with Enrollment No : " + eno);
        //        Response.Write("<br/>Error description : " + er.Message);
        //        Response.Write("<br/>Missed Enrollment No are : " + Session["eno"].ToString());
        //    }
        //}

        //private void markFeeofStudent(int srid,string batch, int year)
        //{

        //    if (validSRID(srid))
        //    {
        //        int courseid = FindInfo.findCourseIDBySRID(srid);
        //        int counter = findCounter(courseid);
        //        string rollno = "A13" + FindInfo.findCourseCodeByID(courseid) + string.Format("{0:0000}", counter);
        //        int ecid = findExamCentre(findStudyCentre(Convert.ToInt32(srid)));
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd = new SqlCommand("insert into ExamRecord_June13 values(@SRID,@Batch,@MOA,@RollNo,@AFP1Year,@AFP2Year,@AFP3Year,@EFP1Year,@EFP2Year,@EFP3Year,@CTPaperCode1,@CTPaperCode2,@ECID,@DCase,@Online)", con);


        //        cmd.Parameters.AddWithValue("@SRID", srid);
        //        cmd.Parameters.AddWithValue("@Batch", batch);
        //        cmd.Parameters.AddWithValue("@MOA", "");
        //        cmd.Parameters.AddWithValue("@RollNo", rollno);

        //        if (year == 1)
        //        {
        //            cmd.Parameters.AddWithValue("@AFP1Year","True");
        //            cmd.Parameters.AddWithValue("@AFP2Year", "False");
        //            cmd.Parameters.AddWithValue("@AFP3Year", "False");

        //            cmd.Parameters.AddWithValue("@EFP1Year", "True");
        //            cmd.Parameters.AddWithValue("@EFP2Year", "False");
        //            cmd.Parameters.AddWithValue("@EFP3Year", "False");
        //        }

        //        else if (year == 2)
        //        {

        //            cmd.Parameters.AddWithValue("@AFP1Year", "False");
        //            cmd.Parameters.AddWithValue("@AFP2Year", "True");
        //            cmd.Parameters.AddWithValue("@AFP3Year", "False");

        //            cmd.Parameters.AddWithValue("@EFP1Year", "False");
        //            cmd.Parameters.AddWithValue("@EFP2Year", "True");
        //            cmd.Parameters.AddWithValue("@EFP3Year", "False");
        //        }

        //        else if (year == 3)
        //        {
        //            cmd.Parameters.AddWithValue("@AFP1Year", "False");
        //            cmd.Parameters.AddWithValue("@AFP2Year", "False");
        //            cmd.Parameters.AddWithValue("@AFP3Year", "True");

        //            cmd.Parameters.AddWithValue("@EFP1Year", "False");
        //            cmd.Parameters.AddWithValue("@EFP2Year", "False");
        //            cmd.Parameters.AddWithValue("@EFP3Year", "True");
        //        }


        //        cmd.Parameters.AddWithValue("@CTPaperCode1", "");
        //        cmd.Parameters.AddWithValue("@CTPaperCode2", "");
        //        cmd.Parameters.AddWithValue("@ECID", ecid);
        //        cmd.Parameters.AddWithValue("@DCase", "False");
        //        cmd.Parameters.AddWithValue("@Online", "True");

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        updateCounter(courseid, counter);
        //        Session["counter"] = Convert.ToInt32(Session["counter"]) + 1;
        //    }
        //}

        //private bool validSRID(int srid)
        //{
        //    bool newstudent = false;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        newstudent = false;
        //    }
        //    else
        //    {
        //        newstudent = true;
        //    }


        //    con.Close();
        //    return newstudent;
        //}

        //protected void btnCheckExamData_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    DataTable dt = new DataTable();

        //    DataColumn dtcol1 = new DataColumn("SNo");
        //    DataColumn dtcol2 = new DataColumn("EnrollmentNo");
        //    DataColumn dtcol3 = new DataColumn("Batch");
        //    DataColumn dtcol4 = new DataColumn("SCCode");
        //    DataColumn dtcol5 = new DataColumn("AFP1Year");
        //    DataColumn dtcol6 = new DataColumn("AFP2Year");
        //    DataColumn dtcol7 = new DataColumn("AFP3Year");
        //    DataColumn dtcol8 = new DataColumn("EFP1Year");
        //    DataColumn dtcol9 = new DataColumn("EFP2Year");
        //    DataColumn dtcol10 = new DataColumn("EFP3Year");




        //    dt.Columns.Add(dtcol1);
        //    dt.Columns.Add(dtcol2);
        //    dt.Columns.Add(dtcol3);
        //    dt.Columns.Add(dtcol4);
        //    dt.Columns.Add(dtcol5);
        //    dt.Columns.Add(dtcol6);
        //    dt.Columns.Add(dtcol7);
        //    dt.Columns.Add(dtcol8);
        //    dt.Columns.Add(dtcol9);
        //    dt.Columns.Add(dtcol10);

        //    int i = 1;

        //    while (dr.Read())
        //    {

        //        if ((dr["AFP1Year"].ToString() == "True") && (dr["EFP2Year"].ToString()=="True"|| dr["EFP2Year"].ToString() =="True"))
        //        {
        //            DataRow drow = dt.NewRow();
        //            drow["SNo"] = i;
        //            drow["EnrollmentNo"] = FindInfo.findENoByID(Convert.ToInt32(dr["SRID"]));
        //            drow["Batch"] = Convert.ToString(dr["Batch"]);
        //            drow["SCCode"] = FindInfo.findSCCodeBySRID(Convert.ToInt32(dr["SRID"]));
        //            drow["AFP1Year"] = Convert.ToString(dr["AFP1Year"]);
        //            drow["AFP2Year"] = Convert.ToString(dr["AFP2Year"]);
        //            drow["AFP3Year"] = Convert.ToString(dr["AFP3Year"]);
        //            drow["EFP1Year"] = Convert.ToString(dr["EFP1Year"]);
        //            drow["EFP2Year"] = Convert.ToString(dr["EFP2Year"]);
        //            drow["EFP3Year"] = Convert.ToString(dr["EFP3Year"]);
        //            dt.Rows.Add(drow);
        //            i = i + 1;
        //        }

        //        else if ((dr["AFP2Year"].ToString() == "True") && (dr["EFP1Year"].ToString() == "True" || dr["EFP3Year"].ToString() == "True"))
        //        {
        //            DataRow drow = dt.NewRow();
        //            drow["SNo"] = i;
        //            drow["EnrollmentNo"] = FindInfo.findENoByID(Convert.ToInt32(dr["SRID"]));
        //            drow["Batch"] = Convert.ToString(dr["Batch"]);
        //            drow["SCCode"] = FindInfo.findSCCodeBySRID(Convert.ToInt32(dr["SRID"]));
        //            drow["AFP1Year"] = Convert.ToString(dr["AFP1Year"]);
        //            drow["AFP2Year"] = Convert.ToString(dr["AFP2Year"]);
        //            drow["AFP3Year"] = Convert.ToString(dr["AFP3Year"]);
        //            drow["EFP1Year"] = Convert.ToString(dr["EFP1Year"]);
        //            drow["EFP2Year"] = Convert.ToString(dr["EFP2Year"]);
        //            drow["EFP3Year"] = Convert.ToString(dr["EFP3Year"]);
        //            dt.Rows.Add(drow);
        //            i = i + 1;
        //        }

        //        else if ((dr["AFP3Year"].ToString() == "True") && (dr["EFP1Year"].ToString() == "True" || dr["EFP2Year"].ToString() == "True"))
        //        {
        //            DataRow drow = dt.NewRow();
        //            drow["SNo"] = i;
        //            drow["EnrollmentNo"] = FindInfo.findENoByID(Convert.ToInt32(dr["SRID"]));
        //            drow["Batch"] = Convert.ToString(dr["Batch"]);
        //            drow["SCCode"] = FindInfo.findSCCodeBySRID(Convert.ToInt32(dr["SRID"]));
        //            drow["AFP1Year"] = Convert.ToString(dr["AFP1Year"]);
        //            drow["AFP2Year"] = Convert.ToString(dr["AFP2Year"]);
        //            drow["AFP3Year"] = Convert.ToString(dr["AFP3Year"]);
        //            drow["EFP1Year"] = Convert.ToString(dr["EFP1Year"]);
        //            drow["EFP2Year"] = Convert.ToString(dr["EFP2Year"]);
        //            drow["EFP3Year"] = Convert.ToString(dr["EFP3Year"]);
        //            dt.Rows.Add(drow);
        //            i = i + 1;
        //        }


        //    }

        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();

        //    con.Close();
        //}



        //protected void btnUploadExamData_Click(object sender, EventArgs e)
        //{
        //    Session["eno"] = "";
        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\Exam data\368.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet1$]";
        //    OleDbDataReader dr;
        //    string eno = "";
        //    Session["TotalUpdated"] = 0;
        //    try
        //    {

        //        using (OleDbConnection connection = new OleDbConnection(connectionString))
        //        {

        //            OleDbCommand command = new OleDbCommand(queryString, connection);
        //            connection.Open();
        //            dr = command.ExecuteReader();
        //            int i = 0;
        //            while (dr.Read())
        //            {
        //                if (dr["EnrollmentNo"].ToString() != "")
        //                {
        //                    eno = dr["EnrollmentNo"].ToString();
        //                    uploadExamData(eno, 1);
        //                    i = i + 1;
        //                    Session["TS"] = i.ToString();
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //            dr.Close();
        //            connection.Close();
        //            OleDbConnection.ReleaseObjectPool();

        //            Response.Write(Session["TotalUpdated"].ToString() + " Students Updated");
        //            Response.Write(i.ToString()+ " Students Uploaded");
        //            Response.Write("<br/> Missed Enrollment No are : " + Session["missedeno"].ToString());
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        Response.Write(Session["TotalUpdated"].ToString() + " Students Updated");
        //        Response.Write("<br/>Error occured with Enrollment No : " + eno);
        //        Response.Write(Session["TS"].ToString() + " Students Uploaded");
        //        Response.Write("<br/>Error description : " + er.Message);
        //        Response.Write("<br/>Missed Enrollment No are : " + Session["eno"].ToString());
        //    }
        //}

        //private void uploadExamData(string eno, int year)
        //{
        //    int srid = FindInfo.findSRIDByENo(eno);

        //    if (srid == 0)
        //    {
        //        Session["missedeno"] = Session["missedeno"] + "<br/>" + eno;
        //    }

        //   // string[] eyr = findExamYearRecord(srid);

        //    if (!examRecordUpdated(srid,year))
        //    {
        //        Session["TotalUpdated"] =Convert.ToInt32(Session["TotalUpdated"])  + 1;

        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;

        //        if (year == 1)
        //        {
        //            cmd.CommandText = "update ExamRecord_June13 set EFP1Year=@EFP1Year where SRID='" + srid + "' ";
        //            cmd.Parameters.AddWithValue("@EFP1Year", "True");

        //        }
        //        else if (year == 2)
        //        {
        //            cmd.CommandText = "update ExamRecord_June13 set EFP2Year=@EFP2Year where SRID='" + srid + "' ";
        //            cmd.Parameters.AddWithValue("@EFP2Year", "True");

        //        }

        //        else if (year == 3)
        //        {
        //            cmd.CommandText = "update ExamRecord_June13 set EFP3Year=@EFP3Year where SRID='" + srid + "' ";
        //            cmd.Parameters.AddWithValue("@EFP3Year", "True");

        //        }


        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }

        //}

        //private bool examRecordUpdated(int srid, int year)
        //{
        //    bool updated = false;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        if (year == 1)
        //        {
        //            if (dr["EFP1Year"].ToString() == "True")
        //            {
        //                updated = true;
        //            }

        //        }
        //        else if (year == 2)
        //        {
        //            if (dr["EFP2Year"].ToString() == "True")
        //            {
        //                updated = true;
        //            }

        //        }
        //        else if (year == 3)
        //        {
        //            if (dr["EFP3Year"].ToString() == "True")
        //            {
        //                updated = true;
        //            }

        //        }

        //    }


        //    con.Close();
        //    return updated;

        //}

        //private string[] findExamYearRecord(int srid)
        //{
        //    string[] eyr = {"","",""};
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select EFP1Year,EFP2Year,EFP3Year from ExamRecord_June13 where SRID='" + srid + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        eyr[0] = dr[1].ToString();
        //        eyr[1] = dr[2].ToString();
        //        eyr[2] = dr[3].ToString();
        //    }

        //    con.Close();
        //    return eyr;
        //}

        //protected void btnUpdateExamData_Click(object sender, EventArgs e)
        //{
        //    Session["eno"] = "";
        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\AdmissionData\Credit.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet1$]";
        //    OleDbDataReader dr;
        //    string eno = "";
        //    try
        //    {

        //        using (OleDbConnection connection = new OleDbConnection(connectionString))
        //        {

        //            OleDbCommand command = new OleDbCommand(queryString, connection);
        //            connection.Open();
        //            dr = command.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                eno = dr["EnrollmentNo"].ToString();
        //                updateExamData(eno, Convert.ToInt32(dr["Year"]));

        //            }
        //            dr.Close();
        //            connection.Close();
        //            OleDbConnection.ReleaseObjectPool();


        //            Response.Write("<br/> Missed Enrollment No are : " + Session["missedeno"].ToString());
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        Response.Write("Error occured with Enrollment No : " + eno);
        //        Response.Write("<br/>Error description : " + er.Message);
        //        Response.Write("<br/>Missed Enrollment No are : " + Session["eno"].ToString());
        //    }
        //}

        //private void updateExamData(string eno, int year)
        //{
        //    int srid = FindInfo.findSRIDByENo(eno);

        //    if (srid == 0)
        //    {
        //        Session["missedeno"] = Session["missedeno"] + "<br/>" + eno;
        //    }
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;


        //    if (year == 3)
        //    {
        //        cmd.CommandText = "update ExamRecord_June13 set AFP2Year=@AFP2Year,AFP3Year=@AFP3Year where SRID='" + srid + "' ";
        //        cmd.Parameters.AddWithValue("@AFP2Year", "False");
        //        cmd.Parameters.AddWithValue("@AFP3Year", "True");

        //    }


        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //}

        protected void btnUploadExamCentre_Click(object sender, EventArgs e)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\DDE\Data\data\Exam data\ExamCentres_W10.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString = "SELECT * FROM [self$]";
            OleDbDataReader dr;

            int i = 0;
            try
            {

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {

                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    connection.Open();
                    dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        if (dr["City"].ToString() != "")
                        {
                            if (!ecExist(dr["ExamCentreCode"].ToString(), "W10"))
                            {
                                Random rd = new Random();
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("insert into DDEExaminationCentres_W10 values(@City,@ExamCentreCode,@Password,@PasswordMailSent,@ContactPerson,@ContactNo,@CentreName,@Location,@Email,@SCCodes,@Remark,@NoTimesLoggedIn,@LastLogoutTime)", con);

                                cmd.Parameters.AddWithValue("@City", dr["City"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@ExamCentreCode", dr["ExamCentreCode"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Password", rd.Next(100000, 999999));
                                cmd.Parameters.AddWithValue("@PasswordMailSent", "False");
                                cmd.Parameters.AddWithValue("@ContactPerson", dr["ContactPerson"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@ContactNo", dr["ContactNo"].ToString());
                                cmd.Parameters.AddWithValue("@CentreName", dr["CentreName"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Location", dr["Location"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@Email", dr["Email"].ToString());
                                cmd.Parameters.AddWithValue("@SCCodes", dr["SCCodes"].ToString());
                                cmd.Parameters.AddWithValue("@Remark", dr["Remark"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@NoTimesLoggedIn", 0);
                                cmd.Parameters.AddWithValue("@LastLogoutTime", "");

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                i = i + 1;
                            }
                        }
                        else
                        {
                            break;
                        }

                    }
                    dr.Close();
                    connection.Close();
                    OleDbConnection.ReleaseObjectPool();


                    Response.Write(i.ToString() + " entries uploaded");
                }
            }
            catch (Exception er)
            {
                Response.Write(er.Message);
            }
        }

        private bool ecExist(string eccode, string exam)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_" + exam + " where ExamCentreCode='" + eccode + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }

            con.Close();


            return exist;
        }




        //protected void btnCancelStudents_Click(object sender, EventArgs e)
        //{
        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\AdmissionData\Cancelled list of A-2012-13 (I Year).xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet1$]";
        //    OleDbDataReader dr;

        //    int i = 0;
        //    try
        //    {

        //        using (OleDbConnection connection = new OleDbConnection(connectionString))
        //        {

        //            OleDbCommand command = new OleDbCommand(queryString, connection);
        //            connection.Open();
        //            dr = command.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                int srid = FindInfo.findSRIDByENo(dr["EnrollmentNo"].ToString());
        //                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //                SqlCommand cmd = new SqlCommand("update ExamRecord_June13 set Online=@Online where SRID='" + srid + "' ", con);
        //                cmd.Parameters.AddWithValue("@Online", "False");

        //                con.Open();
        //                cmd.ExecuteNonQuery();
        //                con.Close();

        //                i = i + 1;
        //            }
        //            dr.Close();
        //            connection.Close();
        //            OleDbConnection.ReleaseObjectPool();


        //            Response.Write(i.ToString() + " students Canceled");
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        Response.Write(er.Message);
        //    }
        //}

        //protected void btnARollNo_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select SRID from ExamRecord_June13 where RollNo=''", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            allotRollNo(Convert.ToInt32(dr["SRID"]));
        //        }

        //    }

        //    con.Close();
        //}

        //private void allotRollNo(int srid)
        //{
        //    int courseid = FindInfo.findCourseIDBySRID(srid);
        //    int counter = findCounter(courseid);
        //    string rollno = "A13" + FindInfo.findCourseCodeByID(courseid) + string.Format("{0:0000}", counter);

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update ExamRecord_June13 set RollNo=@RollNo where SRID='" + srid + "' ", con);
        //    cmd.Parameters.AddWithValue("@RollNo", rollno);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    updateCounter(courseid, counter);
        //}

        //private void updateCounter(int courseid, int counter)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDERollNoCounters set RollNoCounter_A13=@RollNoCounter_A13 where CourseID='" + courseid + "'", con);
        //    cmd.Parameters.AddWithValue("@RollNoCounter_A13", counter);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //}

        //private int findCounter(int courseid)
        //{
        //    string counter = "NA";

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select RollNoCounter_A13 from DDERollNoCounters where CourseID='" + courseid + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        counter = Convert.ToString(dr[0]);

        //    }

        //    con.Close();

        //    return Convert.ToInt32(counter) + 1;
        //}

        //protected void btnABPRollNo_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_A13 where RollNo=''", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            allotBPRollNo(Convert.ToInt32(dr["SRID"]));
        //        }

        //    }

        //    con.Close();
        //}

        //private void allotBPRollNo(int srid)
        //{
        //    string alotedrollno = findRollNo(srid);

        //    if (alotedrollno != "")
        //    {
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd = new SqlCommand("update DDEExamRecord_A13 set RollNo=@RollNo where SRID='" + srid + "' ", con);
        //        cmd.Parameters.AddWithValue("@RollNo", "X" + alotedrollno);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    else
        //    {
        //        int courseid = FindInfo.findCourseIDBySRID(srid);
        //        int counter = findBPCounter(courseid);
        //        string rollno = "XA13" + FindInfo.findCourseCodeByID(courseid) + string.Format("{0:0000}", counter);
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd = new SqlCommand("update DDEExamRecord_A13 set RollNo=@RollNo where SRID='" + srid + "' ", con);
        //        cmd.Parameters.AddWithValue("@RollNo", rollno);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        con.Close();

        //        updateBPCounter(courseid, counter);

        //    }

        //}

        //private void updateBPCounter(int courseid, int counter)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDERollNoCounters set BPRollNoCounter_A13=@BPRollNoCounter_A13 where CourseID='" + courseid + "'", con);
        //    cmd.Parameters.AddWithValue("@BPRollNoCounter_A13", counter);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //private int findBPCounter(int courseid)
        //{
        //    string counter = "NA";

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select BPRollNoCounter_A13 from DDERollNoCounters where CourseID='" + courseid + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        counter = Convert.ToString(dr[0]);

        //    }

        //    con.Close();

        //    return Convert.ToInt32(counter) + 1;
        //}

        //private string findRollNo(int srid)
        //{
        //    string rollno = "";
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select RollNo from ExamRecord_June13 where SRID='" + srid + "'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        rollno = dr[0].ToString();



        //    }

        //    con.Close();

        //    return rollno;
        //}

        protected void btnAssignExamCentres_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct SRID from DDEExamRecord_W11 where ExamCentreCode='0'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int ecid = findExamCentre(FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(dr[0])));
                    if (ecid != 0)
                    {
                        updateExamCentre(Convert.ToInt32(dr[0]), ecid);
                        i++;
                    }

                }

            }

            con.Close();

            Response.Write(i.ToString() + " Students alloted");

        }

        private void updateExamCentre(int srid, int ecid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEExamRecord_W11 set ExamCentreCode=@ExamCentreCode where SRID='" + srid + "'", con);
            cmd.Parameters.AddWithValue("@ExamCentreCode", ecid.ToString());

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //private void updateBPExamCentre(int srid, int ecid)
        //{

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEExamRecord_A13 set ExamCentreCode=@ExamCentreCode where SRID='" + srid + "'", con);
        //    cmd.Parameters.AddWithValue("@ExamCentreCode", ecid);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        private int findExamCentre(string sccode)
        {
            int ecid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ECID,SCCodes from DDEExaminationCentres_W11", con);
            con.Open();
            SqlDataReader dr;
            string[] sc = { };
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sc = dr["SCCodes"].ToString().Split(',');
                    for (int i = 0; i < sc.Length; i++)
                    {
                        if (sc[i].ToString() == sccode)
                        {

                            ecid = Convert.ToInt32(dr["ECID"]);
                            break;
                        }
                    }
                }

            }

            con.Close();
            return ecid;
        }

        //protected void btnShiftRollNo_Click(object sender, EventArgs e)
        //{

        //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd1 = new SqlCommand("Select * from DDEExamRecord_A19", con1);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);


        //    int j = 0;
        //    for (int i = 77; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        if (!(Exam.examRecordExist(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]), "W10", Convert.ToString(ds.Tables[0].Rows[i]["MOE"]))))
        //        {
        //            int counter;
        //            int cid = FindInfo.findCourseIDBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
        //            if (cid != 0)
        //            {
        //                string rollno = Exam.allotRollNo(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), "", cid, "W10", (ds.Tables[0].Rows[i]["MOE"]).ToString(), out counter);

        //                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //                SqlCommand cmd = new SqlCommand("insert into DDEExamRecord_W10 values(@SRID,@Year,@RollNo,@BPSubjects1,@BPSubjects2,@BPSubjects3,@BPPracticals1,@BPPracticals2,@BPPracticals3,@ExamCentreCode,@ExamCentreCity,@ExamCentreZone,@MaxMarks,@ObtMarks,@QualifyingStatus,@MOE,@MSPrinted,@Times,@LastPrintTime,@MSCounter)", con);

        //                cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
        //                cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]));
        //                cmd.Parameters.AddWithValue("@RollNo", rollno);
        //                cmd.Parameters.AddWithValue("@BPSubjects1", (ds.Tables[0].Rows[i]["BPSubjects1"]).ToString());
        //                cmd.Parameters.AddWithValue("@BPSubjects2", (ds.Tables[0].Rows[i]["BPSubjects2"]).ToString());
        //                cmd.Parameters.AddWithValue("@BPSubjects3", (ds.Tables[0].Rows[i]["BPSubjects3"]).ToString());
        //                cmd.Parameters.AddWithValue("@BPPracticals1", (ds.Tables[0].Rows[i]["BPPracticals1"]).ToString());
        //                cmd.Parameters.AddWithValue("@BPPracticals2", (ds.Tables[0].Rows[i]["BPPracticals2"]).ToString());
        //                cmd.Parameters.AddWithValue("@BPPracticals3", (ds.Tables[0].Rows[i]["BPPracticals3"]).ToString());
        //                cmd.Parameters.AddWithValue("@ExamCentreCode", 0);
        //                cmd.Parameters.AddWithValue("@ExamCentreCity", "");
        //                cmd.Parameters.AddWithValue("@ExamCentreZone", "");
        //                cmd.Parameters.AddWithValue("@MaxMarks", 0);
        //                cmd.Parameters.AddWithValue("@ObtMarks", 0);
        //                cmd.Parameters.AddWithValue("@QualifyingStatus", "");
        //                cmd.Parameters.AddWithValue("@MOE", (ds.Tables[0].Rows[i]["MOE"]).ToString());
        //                cmd.Parameters.AddWithValue("@MSPrinted", "False");
        //                cmd.Parameters.AddWithValue("@Times", 0);
        //                cmd.Parameters.AddWithValue("@LastPrintTime", "");
        //                cmd.Parameters.AddWithValue("@MSCounter", "");


        //                cmd.Connection = con;
        //                con.Open();
        //                int k = cmd.ExecuteNonQuery();
        //                con.Close();
        //                j = j + k;

        //                if (counter != 0)
        //                {
        //                    FindInfo.updateRollNoCounter(cid, counter, "W10");
        //                }
        //            }
        //        }


        //    }

        //    Response.Write(j + "rows updated");

        //}





        //protected void btnDeleteSLMRecord_Click(object sender, EventArgs e)
        //{
        //    string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Offline Applications\999\Cont\A18\Delete2.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
        //    string queryString = "SELECT * FROM [Applications$]";

        //    using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    {


        //        OleDbCommand command = new OleDbCommand(queryString, connection);
        //        OleDbDataAdapter da = new OleDbDataAdapter(command);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            int srid = FindInfo.findSRIDByENo(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
        //            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd1 = new SqlCommand("delete from DDESLMIssueRecord  where SRID ='" + srid + "' and Year='" + ds.Tables[0].Rows[i]["ForYear"].ToString() + "' and LNo='0'", con1);


        //            con1.Open();
        //            int j= cmd1.ExecuteNonQuery();
        //            con1.Close();

        //            if (j != 1)
        //            {
        //                Response.Write("<br/>" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
        //            }
        //        }

        //    }
        //}



        //protected void btnCENo_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select SRID,EnrollmentNo from DDEStudentRecord where SRID>125837", con);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);


        //    int j = 0;
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {

        //        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd2 = new SqlCommand("update DDEStudentRecord set EnrollmentNo=@EnrollmentNo,ICardNo=@ICardNo where SRID='" + ds.Tables[0].Rows[i]["SRID"].ToString() + "'", con2);


        //        cmd2.Parameters.AddWithValue("@EnrollmentNo", ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(0, 11) + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(12, 3));
        //        cmd2.Parameters.AddWithValue("@ICardNo", ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(0, 3) + "-" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(5, 3) + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(9, 2) + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(12, 3));

        //        con2.Open();
        //        cmd2.ExecuteNonQuery();
        //        con2.Close();

        //        j = j + 1;

        //    }

        //    Response.Write(j + "rows updated");
        //}

        //protected void btnInsertCity_Click(object sender, EventArgs e)
        //{
        //    int ts = 0;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("SELECT  * FROM DDECity where State!='UTTAR PRADESH'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            if (dr["CityName"].ToString() != "")
        //            {
        //                string[] str = dr["CityName"].ToString().Split(',');
        //                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //                SqlCommand cmd1 = new SqlCommand("insert into CityList values(@City,@State,@Country)", con1);

        //                cmd1.Parameters.AddWithValue("@City", str[0].ToString().Trim());
        //                cmd1.Parameters.AddWithValue("@State", dr["State"].ToString().Trim());
        //                cmd1.Parameters.AddWithValue("@Country", "INDIA");


        //                con1.Open();
        //                cmd1.ExecuteNonQuery();
        //                con1.Close();
        //                ts = ts + 1;
        //            }

        //        }

        //    }

        //    con.Close();
        //}

        //protected void btnCorrectExamCentre_Click(object sender, EventArgs e)
        //{
        //    int ts = 0;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("SELECT  [DDEExamRecord_A15].[SRID] ,[DDEExamRecord_A15].[Year] ,[DDEExamRecord_A15].[RollNo],[DDEExamRecord_A15].[ExamCentreCode], DDEStudentRecord.StudyCentreCode FROM [DDEExamRecord_A15] inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_A15.SRID where DDEStudentRecord.StudyCentreCode='009'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            updateExamCentre(Convert.ToInt32(dr["SRID"]), 190);
        //            ts = ts + 1;

        //        }

        //    }

        //    con.Close();

        //    Response.Write(ts.ToString());
        //}

        //private void updateExamCentre(int srid, int ecid)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEExamRecord_A15 set ExamCentreCode=@ExamCentreCode where SRID='" + srid + "'", con);
        //    cmd.Parameters.AddWithValue("@ExamCentreCode", ecid.ToString());

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //protected void btnSetPreSCCode_Click(object sender, EventArgs e)
        //{

        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where SCStatus='C'", con);
        //    con.Open();
        //    SqlDataReader dr;


        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            updatePreSCCode(Convert.ToInt32(dr[0]), findPreSCCode(Convert.ToInt32(dr[0])));
        //        }

        //    }

        //    con.Close();


        //}

        //private void updatePreSCCode(int srid, string presccode)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEStudentRecord set PreviousSCCode=@PreviousSCCode where SRID='" + srid + "'", con);
        //    cmd.Parameters.AddWithValue("@PreviousSCCode", presccode);

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //private string findPreSCCode(int srid)
        //{
        //    string psc = "";
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select PreviousSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
        //    con.Open();
        //    SqlDataReader dr;
        //    string[] sc = { };
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            psc = dr[0].ToString();

        //        }

        //    }

        //    con.Close();
        //    return psc;
        //}

        //private string findStudyCentre(int srid)
        //{
        //    string sccode = "NF";
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID='" + srid + "'", con);
        //    con.Open();
        //    SqlDataReader dr;


        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
        //        {
        //            sccode = Convert.ToString(dr["StudyCentreCode"]);
        //        }
        //        else if (dr["SCStatus"].ToString() == "T")
        //        {
        //            sccode = findTranferedSCCode(srid);
        //        }

        //    }

        //    con.Close();

        //    return sccode;
        //}


        //private string findTranferedSCCode(int srid)
        //{
        //    string sccode = "NF";
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select PreviousSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
        //    con.Open();
        //    SqlDataReader dr;


        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            sccode = dr[0].ToString();
        //        }

        //    }

        //    con.Close();

        //    return sccode;
        //}

        //protected void btnSetExam_Click(object sender, EventArgs e)
        //{

        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\AdmissionData\28-12-2013\A-2011-12 (III Year) S.No-01 to 786.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet1$]";
        //    OleDbDataReader dr;

        //    int i = 0;
        //    try
        //    {

        //        using (OleDbConnection connection = new OleDbConnection(connectionString))
        //        {

        //            OleDbCommand command = new OleDbCommand(queryString, connection);
        //            connection.Open();
        //            dr = command.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                if (dr["EnrollmentNo"].ToString()!="")
        //                {
        //                    int srid = FindInfo.findSRIDByENo(dr["EnrollmentNo"].ToString());
        //                    updateExam(srid);
        //                    i = i + 1;
        //                }
        //            }
        //            connection.Close();
        //        }


        //        Response.Write(i.ToString() + " Students updated");
        //    }
        //    catch
        //    {
        //        Response.Write(i.ToString() + " Students updated");
        //    }


        //}

        //private void updateExam(int srid)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update [DDEFeeRecord_2013-14] set ForExam=@ForExam where SRID='" + srid + "' and ForYear='3' and FeeHead='1'", con);
        //    cmd.Parameters.AddWithValue("@ForExam", "A14");

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}



        //protected void btnAECBP_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_A13 where ExamCentreCode='' and MOE='B'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            int ecid = findExamCentre(findStudyCentre(Convert.ToInt32(dr[0])));

        //            updateBPExamCentre(Convert.ToInt32(dr[0]), ecid);

        //        }

        //    }

        //    con.Close();


        //}

        //protected void btnSendMail_Click(object sender, EventArgs e)
        //{
        //    SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
        //    mySmtpClient.UseDefaultCredentials = false;
        //    System.Net.NetworkCredential basicAuthenticationInfo = new
        //    System.Net.NetworkCredential("ak1781@gmail.com", "712141986");
        //    mySmtpClient.Credentials = basicAuthenticationInfo;


        //    MailAddress from = new MailAddress("ak1781@gmail.com", "TestFromName");
        //    MailAddress to = new MailAddress("ak1781@gmail.com", "TestToName");
        //    MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


        //    MailAddress replyto = new MailAddress("ak1781@gmail.com");
        //    myMail.ReplyTo = replyto;


        //    myMail.Subject = "Test message";
        //    myMail.SubjectEncoding = System.Text.Encoding.UTF8;


        //    myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
        //    myMail.BodyEncoding = System.Text.Encoding.UTF8;

        //    myMail.IsBodyHtml = true;

        //    mySmtpClient.Port = 587;
        //    mySmtpClient.EnableSsl = true;
        //    mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

        //    mySmtpClient.Send(myMail);
        //}

        //protected void btnSetQStatus_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord_A13 where MOE='B'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    int i = 0;
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {


        //            updateQStatus(Convert.ToInt32(dr[0]));
        //            i = i + 1;
        //        }

        //    }
        //    Response.Write(i.ToString() + "Students Updated");

        //    con.Close();
        //}

        //private void updateQStatus(int srid)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEStudentRecord set QualifyingStatus=@QualifyingStatus where SRID='" + srid + "'", con);
        //    cmd.Parameters.AddWithValue("@QualifyingStatus", "PCP");

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //protected void findTotalCopies_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select SRID from ExamRecord_June13", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();

        //    int total = 0;

        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {

        //            string year = findAllExamYear(Convert.ToInt32(dr[0]), "R");
        //            if (year != "0")
        //            {
        //                if (year.Length == 1)
        //                {
        //                    total = total + countcopies(Convert.ToInt32(dr[0]), Convert.ToInt32(year));
        //                }
        //                else
        //                {
        //                    string[] yr = year.Split(',');
        //                    for (int i = 0; i < yr.Length; i++)
        //                    {
        //                        total = total + countcopies(Convert.ToInt32(dr[0]), Convert.ToInt32(yr[i]));
        //                    }

        //                }
        //            }


        //        }

        //    }
        //    Response.Write("Total Regular Copies are : " +total.ToString());

        //    con.Close();

        //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr1;
        //    SqlCommand cmd1 = new SqlCommand("Select * from DDEExamRecord_A13 where MOE='B'", con1);
        //    con1.Open();
        //    dr1 = cmd1.ExecuteReader();
        //    int j = 0;
        //    if (dr1.HasRows)
        //    {
        //        while (dr1.Read())
        //        {
        //            if (dr1["BPSubjects1"].ToString() != "")
        //            {
        //                string[] sub = dr1["BPSubjects1"].ToString().Split(',');
        //                j = j + sub.Length;
        //            }
        //            if (dr1["BPSubjects2"].ToString() != "")
        //            {
        //                string[] sub = dr1["BPSubjects2"].ToString().Split(',');
        //                j = j + sub.Length;
        //            }
        //            if (dr1["BPSubjects3"].ToString() != "")
        //            {
        //                string[] sub = dr1["BPSubjects3"].ToString().Split(',');
        //                j = j + sub.Length;
        //            }



        //        }

        //    }
        //    Response.Write(" <br/> Total Back Paper Copies are : " +j.ToString());

        //    con1.Close();

        //}

        //private int countcopies(int srid,int year)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select PracticalID from DDEPractical where SyllabusSession='A 2010-11' and CourseName='"+FindInfo.findCourseNameBySRID(srid, year)+"' and Year='"+FindInfo.findAlphaYear(year.ToString())+"'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    int i = 0;
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {


        //            i=i+1;

        //        }

        //    }


        //    con.Close();
        //    return i;
        //}

        //private string findAllExamYear(int srid, string moe)
        //{
        //    string year = "0";

        //    if (moe == "R")
        //    {
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlDataReader dr;
        //        SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "' and Online='True'", con);
        //        con.Open();
        //        dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            dr.Read();

        //            if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
        //            {
        //                year = "1";

        //                if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
        //                {
        //                    year = "1,2";
        //                }
        //                else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
        //                {
        //                    year = "1,3";
        //                }


        //            }
        //            else if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
        //            {
        //                year = "2";

        //                if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
        //                {
        //                    year = "1,2";
        //                }
        //                else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
        //                {
        //                    year = "2,3";
        //                }


        //            }
        //            else if ((dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True"))
        //            {
        //                year = "3";

        //                if ((dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True"))
        //                {
        //                    year = "1,3";
        //                }
        //                else if ((dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True"))
        //                {
        //                    year = "2,3";
        //                }
        //            }
        //        }
        //        con.Close();


        //    }

        //    else if (moe == "B")
        //    {
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlDataReader dr;
        //        SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_A13 where SRID='" + srid + "'", con);
        //        con.Open();
        //        dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            dr.Read();
        //            if ((dr["BPSubjects1"].ToString() != ""))
        //            {
        //                year = "1";
        //                if ((dr["BPSubjects2"].ToString() != ""))
        //                {
        //                    year = "1,2";
        //                }
        //                else if ((dr["BPSubjects3"].ToString() != ""))
        //                {
        //                    year = "1,3";
        //                }
        //            }
        //            else if ((dr["BPSubjects2"].ToString() != ""))
        //            {
        //                year = "2";
        //                if ((dr["BPSubjects1"].ToString() != ""))
        //                {
        //                    year = "1,2";
        //                }
        //                else if ((dr["BPSubjects3"].ToString() != ""))
        //                {
        //                    year = "2,3";
        //                }
        //            }
        //            else if ((dr["BPSubjects3"].ToString() != ""))
        //            {
        //                year = "3";
        //                if ((dr["BPSubjects1"].ToString() != ""))
        //                {
        //                    year = "1,3";
        //                }
        //                else if ((dr["BPSubjects2"].ToString() != ""))
        //                {
        //                    year = "2,3";
        //                }
        //            }
        //        }
        //        con.Close();
        //    }

        //    return year;
        //}

        //protected void btnSetYear_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlDataReader dr;
        //    SqlCommand cmd = new SqlCommand("Select SRID,Course from DDEStudentRecord where [Session]='A 2009-10'", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    int i = 0;
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            int dur = FindInfo.findCourseDuration(Convert.ToInt32(dr[1]));
        //            updateYearRecord(Convert.ToInt32(dr[0]),dur);
        //            i = i + 1;
        //        }

        //    }

        //    con.Close();
        //    Response.Write((i-1).ToString()+" Students Record updated");
        //}

        //private void updateYearRecord(int srid, int dur)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("update DDEStudentRecord set FirstYear=@FirstYear,SecondYear=@SecondYear,ThirdYear=@ThirdYear where SRID='" + srid + "'", con);

        //    if (dur == 1)
        //    {
        //        cmd.Parameters.AddWithValue("@FirstYear", "True");
        //        cmd.Parameters.AddWithValue("@SecondYear", "False");
        //        cmd.Parameters.AddWithValue("@ThirdYear", "False");
        //    }
        //    else if (dur == 2)
        //    {
        //        cmd.Parameters.AddWithValue("@FirstYear", "True");
        //        cmd.Parameters.AddWithValue("@SecondYear", "True");
        //        cmd.Parameters.AddWithValue("@ThirdYear", "False");
        //    }
        //    else if (dur == 3)
        //    {
        //        cmd.Parameters.AddWithValue("@FirstYear", "True");
        //        cmd.Parameters.AddWithValue("@SecondYear", "True");
        //        cmd.Parameters.AddWithValue("@ThirdYear", "True");
        //    }

        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //protected void btnSetYear_Click(object sender, EventArgs e)
        //{
        //    Session["misseno"] = "";
        //    Session["cn"] = 0;
        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=G:\data\Year wise data\C-2012 (I, II & III Year) List.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [III Year$]";
        //    OleDbDataReader dr;
        //    string eno = "";
        //    try
        //    {

        //        using (OleDbConnection connection = new OleDbConnection(connectionString))
        //        {

        //            OleDbCommand command = new OleDbCommand(queryString, connection);
        //            connection.Open();
        //            dr = command.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                if (dr["EnrollmentNo"].ToString() != "")
        //                {

        //                    eno = dr["EnrollmentNo"].ToString();
        //                    int srid = FindInfo.findSRIDByENo(eno);
        //                    if (srid == 0)
        //                    {
        //                        Session["misseno"] = Session["misseno"] + "<br/>" + eno;
        //                    }
        //                    else
        //                    {
        //                        updateYear(srid, Convert.ToInt32(dr["Year"]));
        //                        Session["cn"] = Convert.ToInt32(Session["cn"]) + 1;
        //                    }
        //                }
        //                else
        //                {
        //                    break;
        //                }

        //            }
        //            dr.Close();
        //            connection.Close();
        //            OleDbConnection.ReleaseObjectPool();
        //            Response.Write("<br/>Missed Enrollment No are : " + Session["eno"].ToString());
        //            Response.Write("<br/>" + Session["cn"].ToString() + " Students updated");
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        Response.Write("Error occured with Enrollment No : " + eno);
        //        Response.Write("<br/>Error description : " + er.Message);
        //        Response.Write("<br/>Missed Enrollment No are : " + Session["misseno"].ToString());
        //        Response.Write("<br/>" + Session["cn"].ToString() + " Students updated");
        //    }

        //}

        //private void updateYear(int srid, int year)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand();

        //    if (year == 1)
        //    {
        //        cmd.CommandText = "update DDEStudentRecord set FirstYear=@FirstYear where SRID='" + srid + "'";
        //        cmd.Parameters.AddWithValue("@FirstYear", "True");
        //    }
        //    else if (year == 2)
        //    {
        //        cmd.CommandText = "update DDEStudentRecord set SecondYear=@SecondYear where SRID='" + srid + "'";
        //        cmd.Parameters.AddWithValue("@SecondYear", "True");
        //    }
        //    else if (year == 3)
        //    {
        //        cmd.CommandText = "update DDEStudentRecord set ThirdYear=@ThirdYear where SRID='" + srid + "'";
        //        cmd.Parameters.AddWithValue("@ThirdYear", "True");
        //    }


        //    cmd.Connection = con;
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //protected void btnChangeExam_Click(object sender, EventArgs e)
        //{
        //    Session["misseno"] = "";
        //    Session["cn"] = 0;
        //    string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\data\Exam data\DeleteFromDec13.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet1$]";
        //    OleDbDataReader dr;
        //    string eno = "";
        //    try
        //    {

        //        using (OleDbConnection connection = new OleDbConnection(connectionString))
        //        {

        //            OleDbCommand command = new OleDbCommand(queryString, connection);
        //            connection.Open();
        //            dr = command.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                if (dr["EnrollmentNo"].ToString() != "")
        //                {

        //                    eno = dr["EnrollmentNo"].ToString();
        //                    int srid = FindInfo.findSRIDByENo(eno);
        //                    if (srid == 0)
        //                    {
        //                        Session["misseno"] = Session["misseno"] + "<br/>" + eno;
        //                    }
        //                    else
        //                    {
        //                        DeleteExamandFeeData(srid);
        //                        Session["cn"] = Convert.ToInt32(Session["cn"]) + 1;
        //                    }
        //                }
        //                else
        //                {
        //                    break;
        //                }

        //            }
        //            dr.Close();
        //            connection.Close();
        //            OleDbConnection.ReleaseObjectPool();
        //            Response.Write("<br/>Missed Enrollment No are : " + Session["eno"].ToString());
        //            Response.Write("<br/>" + Session["cn"].ToString() + " Students updated");
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //        Response.Write("Error occured with Enrollment No : " + eno);
        //        Response.Write("<br/>Error description : " + er.Message);
        //        Response.Write("<br/>Missed Enrollment No are : " + Session["misseno"].ToString());
        //        Response.Write("<br/>" + Session["cn"].ToString() + " Students updated");
        //    }
        //}

        //private void DeleteExamandFeeData(int srid)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand();


        //    cmd.CommandText = "update DDEExamRecord_B13 set SRID=@SRID,Year=@Year,RollNo=@RollNo,BPSubjects1=@BPSubjects1,BPSubjects2=@BPSubjects2,BPSubjects3=@BPSubjects3,ExamCentreCode=@ExamCentreCode,ExamCentreCity=@ExamCentreCity,ExamCentreZone=@ExamCentreZone,MaxMarks=@MaxMarks,ObtMarks=@ObtMarks,QualifyingStatus=@QualifyingStatus where SRID='" + srid + "'";

        //    cmd.Parameters.AddWithValue("@SRID", 0);
        //    cmd.Parameters.AddWithValue("@Year", 0);
        //    cmd.Parameters.AddWithValue("@RollNo", "ShiftedtoJ14");
        //    cmd.Parameters.AddWithValue("@BPSubjects1", "");
        //    cmd.Parameters.AddWithValue("@BPSubjects2", "");
        //    cmd.Parameters.AddWithValue("@BPSubjects3", "");
        //    cmd.Parameters.AddWithValue("@ExamCentreCode", "");
        //    cmd.Parameters.AddWithValue("@ExamCentreCity", "");
        //    cmd.Parameters.AddWithValue("@ExamCentreZone", "");
        //    cmd.Parameters.AddWithValue("@MaxMarks", 0);
        //    cmd.Parameters.AddWithValue("@ObtMarks", 0);
        //    cmd.Parameters.AddWithValue("@QualifyingStatus", "");




        //    cmd.Connection = con;
        //    con.Open();
        //    cmd.ExecuteNonQuery();
        //    con.Close();

        //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd1 = new SqlCommand();


        //    cmd1.CommandText = "Delete from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='2' and ForExam='B13'";





        //    cmd1.Connection = con1;
        //    con1.Open();
        //    cmd1.ExecuteNonQuery();
        //    con1.Close();

        //}

        //protected void btnCSS_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd1 = new SqlCommand("Select SLMRID,SRID from DDESLMIssueRecord where CID='76' AND [Year]='2'", con1);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);


        //    int j = 0;
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {


        //        int cid = FindInfo.findCourse2YearIDBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
        //        if (cid != 0)
        //        {

        //            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd2 = new SqlCommand("update DDESLMIssueRecord set CID=@CID where SLMRID='" + ds.Tables[0].Rows[i]["SLMRID"].ToString() + "'", con2);

        //            cmd2.Parameters.AddWithValue("@CID", cid);


        //            con2.Open();
        //            cmd2.ExecuteNonQuery();
        //            con2.Close();
        //            j = j + 1;


        //        }



        //    }

        //    Response.Write(j + "rows updated");

        //}

        //protected void btnFindGender_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = new DataTable();
        //    DataColumn dtcol1 = new DataColumn("SNo");
        //    DataColumn dtcol2 = new DataColumn("OANo");
        //    DataColumn dtcol3 = new DataColumn("EnrollmentNo");
        //    DataColumn dtcol4 = new DataColumn("StudentName");
        //    DataColumn dtcol5 = new DataColumn("DOA");
        //    DataColumn dtcol6 = new DataColumn("Category");
        //    DataColumn dtcol7 = new DataColumn("MobileNo");
        //    DataColumn dtcol8 = new DataColumn("AadhaarNo");
        //    DataColumn dtcol9 = new DataColumn("Course");
        //    DataColumn dtcol10 = new DataColumn("Gender");



        //    dt.Columns.Add(dtcol1);
        //    dt.Columns.Add(dtcol2);
        //    dt.Columns.Add(dtcol3);
        //    dt.Columns.Add(dtcol4);
        //    dt.Columns.Add(dtcol5);
        //    dt.Columns.Add(dtcol6);
        //    dt.Columns.Add(dtcol7);
        //    dt.Columns.Add(dtcol8);
        //    dt.Columns.Add(dtcol9);
        //    dt.Columns.Add(dtcol10);


        //    int counter = 0;
        //    string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\2019.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
        //    string queryString = "SELECT * FROM [Sheet1$]";

        //    using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    {

        //        OleDbCommand command = new OleDbCommand(queryString, connection);
        //        OleDbDataAdapter da = new OleDbDataAdapter(command);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);


        //        int j = 1;
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd = new SqlCommand();
        //            if (ds.Tables[0].Rows[i]["EnrollmentNo"].ToString() != "" )
        //            {
        //                cmd.CommandText = "select PSRID,EnrollmentNo,StudentName,DOA,Category,MobileNo,AadhaarNo,CourseName,Gender from DDEStudentRecord  inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course where EnrollmentNo='" + Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]) + "'";

        //            }
        //            else
        //            {
        //                cmd.CommandText = "select PSRID,StudentName,DOA,Category,MobileNo,AadhaarNo,CourseName,Gender from DDEPendingStudentRecord inner join DDECourse on DDECourse.CourseID=DDEPendingStudentRecord.Course where PSRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["OANo"]) + "'";
        //            }

        //            cmd.Connection = con;
        //            con.Open();
        //            SqlDataReader dr;
        //            dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    DataRow drow = dt.NewRow();
        //                    drow["SNo"] = j.ToString();
        //                    drow["OANo"] = Convert.ToString(dr["PSRID"]);
        //                    drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
        //                    drow["StudentName"] = Convert.ToString(dr["StudentName"]);
        //                    drow["DOA"] = Convert.ToDateTime(dr["DOA"]).ToString("dd-MM-yyyy");
        //                    drow["Category"] = Convert.ToString(dr["Category"]);
        //                    drow["MobileNo"] = Convert.ToString(dr["MobileNo"]);
        //                    drow["AadhaarNo"] = Convert.ToString(dr["AadhaarNo"]);
        //                    drow["Course"] = ds.Tables[0].Rows[i]["Course"].ToString();
        //                    drow["Gender"] = Convert.ToString(dr["Gender"]);
        //                    dt.Rows.Add(drow);
        //                    j = j + 1;
        //                }
        //            }
        //            else
        //            {

        //                Response.Write(ds.Tables[0].Rows[i]["SNo"].ToString()+", "+ ds.Tables[0].Rows[i]["OANo"].ToString()+", "+ds.Tables[0].Rows[i]["EnrollmentNo"].ToString()+"</br>");
        //            }

        //           con.Close();

        //           counter = counter + 1;

        //          OleDbConnection.ReleaseObjectPool();


        //        }


        //    }

        //    gvStudents.DataSource = dt;
        //    gvStudents.DataBind();
        //}

        //protected void SetInternalMarks_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd1 = new SqlCommand("Select RID,IA,AW from DDEMarkSheet_W10 order by RID", con1);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);


        //    int j = 0;
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {

        //        string ia = findMarks(ds.Tables[0].Rows[i]["IA"].ToString());
        //        string aw = findMarks(ds.Tables[0].Rows[i]["AW"].ToString());
        //        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W10 set IA=@IA,AW=@AW where RID='" + ds.Tables[0].Rows[i]["RID"].ToString() + "'", con2);

        //        cmd2.Parameters.AddWithValue("@IA", ia);
        //        cmd2.Parameters.AddWithValue("@AW", aw);
        //        cmd2.Parameters.AddWithValue("@Updated", "True");

        //        con2.Open();
        //        cmd2.ExecuteNonQuery();
        //        con2.Close();
        //        j = j + 1;

        //    }

        //    Response.Write(j + "rows updated");


        //    //Response.Write("Result-" + findMarks(tbInput.Text));

        //}

        //private string findMarks(string marks)
        //{
        //    int result = 0;

        //   if(marks=="")
        //   {
        //        return "";
        //   }
        //   else
        //   {
        //        double mar= (Convert.ToInt32(marks) * 75) / 100;
        //        double mar1 = (Convert.ToInt32(marks) * 75) % 100;
        //        if (mar1 < 50)
        //        {
        //            result =Convert.ToInt32(mar);
        //        }
        //        else if (mar1 >= 50)
        //        {
        //            result = Convert.ToInt32(mar) + 1;
        //        }
        //        return result.ToString();
        //    }

        //}

        //protected void btnUpdateSpec_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd1 = new SqlCommand("Select SLMRID,SRID from DDESLMIssueRecord where CID='76' and [Year]='2' and LNo='0' order by SLMRID", con1);

        //    SqlDataAdapter da = new SqlDataAdapter(cmd1);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);


        //    int j = 0;
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {


        //        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlCommand cmd2 = new SqlCommand("update DDESLMIssueRecord set CID=@CID where SLMRID='" + ds.Tables[0].Rows[i]["SLMRID"].ToString() + "'", con2);

        //        cmd2.Parameters.AddWithValue("@CID", findSpec(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"])));


        //        con2.Open();
        //        cmd2.ExecuteNonQuery();
        //        con2.Close();
        //        j = j + 1;

        //    }

        //    Response.Write(j + "rows updated");
        //}

        //private int findSpec(int srid)
        //{
        //    int cid = 0;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand scmd = new SqlCommand("Select Course2Year from DDEStudentRecord where SRID ='" + srid + "' ", con);
        //    SqlDataReader dr;
        //    con.Open();
        //    dr = scmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        dr.Read();
        //        cid = Convert.ToInt32(dr[0]);
        //    }
        //    con.Close();

        //    return cid;
        //}

        //protected void btnUpdateDOA_Click(object sender, EventArgs e)
        //{
        //    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd1 = new SqlCommand("Select SRID,[Session] from DDEStudentRecord where [Session]='C 2020' order by SRID", con1);

        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //    DataSet ds = new DataSet();
        //    da1.Fill(ds);
        //    int counter = 0;
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {

        //        Random gen = new Random();
        //        DateTime da = new DateTime();
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //        SqlDataReader dr;
        //        SqlCommand cmd = new SqlCommand("Select StartDOA,EndDOA from DDESession where [Session]='" + Convert.ToString(ds.Tables[0].Rows[i]["Session"]) + "' ", con);
        //        con.Open();
        //        dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            dr.Read();


        //            string[] sd = Convert.ToDateTime(dr[0]).ToString("dd-MM-yyyy").Split('-');
        //            string[] ed = Convert.ToDateTime(dr[1]).ToString("dd-MM-yyyy").Split('-');
        //            DateTime start = new DateTime(Convert.ToInt32(sd[2]), Convert.ToInt32(sd[1]), Convert.ToInt32(sd[0]));
        //            DateTime end = new DateTime(Convert.ToInt32(ed[2]), Convert.ToInt32(ed[1]), Convert.ToInt32(ed[0]));
        //            int range = (end - start).Days;
        //            da = start.AddDays(gen.Next(range));

        //            if (da.DayOfWeek == DayOfWeek.Sunday)
        //            {
        //                da = da.AddDays(-1);
        //            }

        //            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //            SqlCommand cmd2 = new SqlCommand("update DDEStudentRecord set VDOA=@VDOA where SRID='" + ds.Tables[0].Rows[i]["SRID"].ToString() + "'", con2);

        //            cmd2.Parameters.AddWithValue("@VDOA", da);


        //            con2.Open();
        //            int j=cmd2.ExecuteNonQuery();
        //            con2.Close();
        //            counter = counter + j;
        //        }

        //        con.Close();
        //    }

        //    Response.Write(counter.ToString()+" : Student Updated");
        //}

        protected void btnUpdateCourseID_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select distinct CourseName from DDEPractical", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int courseid = findCourseIDByName(ds.Tables[0].Rows[i]["CourseName"].ToString());
                if (courseid != 0)
                {
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd2 = new SqlCommand("update DDEPractical set CourseID=@CourseID where CourseName='" + ds.Tables[0].Rows[i]["CourseName"].ToString() + "'", con2);

                    cmd2.Parameters.AddWithValue("@CourseID", courseid);


                    con2.Open();
                    int j = cmd2.ExecuteNonQuery();
                    con2.Close();
                }

            }
        }

        public static int findCourseIDByName(string coursename)
        {
            int courseid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseID from DDECourse where CourseName ='" + coursename + "' ", con);
            SqlDataReader dr;



            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                courseid = Convert.ToInt32(dr[0]);
            }
            con.Close();

            return courseid;
        }

        protected void btnSetAB_Click(object sender, EventArgs e)
        {
            int counter = 0;
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Offline Applications\998\JUNE19_ABSENT_LIST.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString = "SELECT * FROM [Sheet1$]";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {

                OleDbCommand command = new OleDbCommand(queryString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);


                int j = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W10 set Theory=@Theory where SRID='" + FindInfo.findSRIDByENo(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString()) + "' and MOE='" + ds.Tables[0].Rows[i]["MOE"].ToString() + "'", con2);

                    cmd2.Parameters.AddWithValue("@Theory", "AB");


                    con2.Open();
                    int k = cmd2.ExecuteNonQuery();
                    con2.Close();


                    counter = counter + k;

                    OleDbConnection.ReleaseObjectPool();


                }

                Response.Write("<br/>" + counter.ToString() + " Absent uploaded");
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender1, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                HttpFileCollection uploadedFiles = Request.Files;

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];
                    string filename = userPostedFile.FileName;
                    string ftpfullpath = @"ftp://192.168.61.99/" + i.ToString() + "_" + filename;
                    FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                    ftp.Credentials = new NetworkCredential("ddeftp", "dde@789");

                    ftp.EnableSsl = true;
                    ftp.KeepAlive = true;
                    ftp.UseBinary = true;
                    ftp.Method = WebRequestMethods.Ftp.UploadFile;

                    //using (FileStream fs = File.OpenWrite("file.dat"))
                    //{
                    //    userPostedFile.InputStream.CopyTo(fs);
                    //    byte[] buffer = new byte[fs.Length];
                    //    fs.Read(buffer, 0, buffer.Length);
                    //    fs.Close();

                    //    Stream ftpstream = ftp.GetRequestStream();
                    //    ftpstream.Write(buffer, 0, buffer.Length);
                    //    ftpstream.Close();

                    //    //fs.Flush();
                    //}

                    int FileLen = userPostedFile.ContentLength;
                    byte[] buffer = new byte[FileLen];


                    Stream MyStream = userPostedFile.InputStream;

                    // Read the file into the byte array.
                    MyStream.Read(buffer, 0, FileLen);

                    //byte[] buffer = new byte[fs.Length];
                    //fs.Read(buffer, 0, buffer.Length);
                    //fs.Close();

                    Stream ftpstream = ftp.GetRequestStream();
                    ftpstream.Write(buffer, 0, buffer.Length);
                    ftpstream.Close();





                    //FileStream fs = userPostedFile.InputStream.ReadByte();
                    //byte[] buffer = new byte[fs.Length];
                    //fs.Read(buffer, 0, buffer.Length);
                    //fs.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAloowResult_Click(object sender, EventArgs e)
        {
            int counter = 0;
            //string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Offline Applications\999\Allow for result\2.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            //string queryString = "SELECT * FROM [Sheet1$]";

            //using (OleDbConnection connection = new OleDbConnection(connectionString))
            //{
            //    OleDbCommand command = new OleDbCommand(queryString, connection);
            //    OleDbDataAdapter da = new OleDbDataAdapter(command);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select DDEExamRecord_W11.SRID,DDEExamRecord_W11.MOE from DDEExamRecord_W11 inner join DDEStudentRecord on DDEExamRecord_W11.SRID=DDEStudentRecord.SRID where StudyCentreCode='" + tbSCCode.Text + "' and DDEExamRecord_W11.SRID not in (select DDEResultAllowedList.SRID from DDEResultAllowedList inner join DDEStudentRecord on DDEResultAllowedList.SRID=DDEStudentRecord.SRID where StudyCentreCode='" + tbSCCode.Text + "' and ExamCode='W11')", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);


            int j = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);

                if (srid > 0)
                {
                    if (!(isAllowResultExist(srid, "W11")))
                    {

                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd1 = new SqlCommand("insert into DDEResultAllowedList values(@SRID,@MOE,@ExamCode)");


                        cmd1.Parameters.AddWithValue("@SRID", srid);
                        cmd1.Parameters.AddWithValue("@MOE", ds.Tables[0].Rows[i]["MOE"].ToString());
                        cmd1.Parameters.AddWithValue("@ExamCode", "W11");

                        cmd1.Connection = con1;
                        con1.Open();
                        j = cmd1.ExecuteNonQuery();
                        con1.Close();
                        counter = counter + j;
                    }
                }
                else
                {
                    Response.Write(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString() + " Not Found</br>");
                }



                OleDbConnection.ReleaseObjectPool();


            }


            //}

            Response.Write("<br/>" + counter.ToString() + "Allow Result uploaded");
        }

        private bool isAllowResultExist(int srid, string exam)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEResultAllowedList where SRID='" + srid + "' and ExamCode='" + exam + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                exist = true;

            }
            con.Close();

            return exist;
        }

        protected void btnAllotPassword_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select ExID from DDEExaminers", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Random rd = new Random();

                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd2 = new SqlCommand("update DDEExaminers set Password=@Password where ExID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["ExID"]) + "'", con2);

                cmd2.Parameters.AddWithValue("@Password", rd.Next(111111, 999999));


                con2.Open();
                int j = cmd2.ExecuteNonQuery();
                con2.Close();


            }
        }

        protected void btnCorrectENo_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select SRID,EnrollmentNo from DDEStudentRecord where EnrollmentNo=''", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Random rd = new Random();

                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd2 = new SqlCommand("update DDEExaminers set Password=@Password where ExID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["ExID"]) + "'", con2);

                cmd2.Parameters.AddWithValue("@Password", rd.Next(111111, 999999));


                con2.Open();
                int j = cmd2.ExecuteNonQuery();
                con2.Close();


            }
        }

        protected void btnAllowOE_Click(object sender, EventArgs e)
        {
            int counter = 0;
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\DDE\June2020.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString = "SELECT * FROM [Sheet11$]";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);



                DataSet ds = new DataSet();
                da.Fill(ds);


                int j = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    if (!(isAllowOEExist(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString())))
                    {

                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd1 = new SqlCommand("insert into DDEStudentAllowedForOE values(@EnrollmentNo)");


                        cmd1.Parameters.AddWithValue("@EnrollmentNo", ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());


                        cmd1.Connection = con1;
                        con1.Open();
                        j = cmd1.ExecuteNonQuery();
                        con1.Close();
                        counter = counter + j;
                    }

                }





            }


            OleDbConnection.ReleaseObjectPool();

            Response.Write("<br/>" + counter.ToString() + "Allow OE uploaded");
        }

        private bool isAllowOEExist(string eno)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentAllowedForOE where EnrollmentNo='" + eno + "'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {

                exist = true;

            }
            con.Close();

            return exist;
        }

        protected void btnChangeSpec_Click(object sender, EventArgs e)
        {
           
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select * from DDEExaminations", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_"+ ds.Tables[0].Rows[i]["ExamCode"] + " set SubjectID=@SubjectID where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["ExID"]) + "' and SubjectID='1'", con2);

                cmd2.Parameters.AddWithValue("@SubjectID", "");


                con2.Open();
                int j = cmd2.ExecuteNonQuery();
                con2.Close();


            }
        }

        protected void uploadResult_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlCommand cmd1 = new SqlCommand("SELECT distinct DDEStudentAssignments.SRID,DDEStudentRecord.Course,DDEExamRecord_W11.[Year],DDECourse.CourseDuration FROM DDEStudentAssignments inner join DDEStudentRecord on DDEStudentAssignments.SRID = DDEStudentRecord.SRID inner join DDEExamRecord_W11 on DDEStudentAssignments.SRID = DDEExamRecord_W11.SRID inner join DDECourse on DDEStudentRecord.Course = DDECourse.CourseID where DDEExamRecord_W11.MOE = 'R'", con1);
            SqlCommand cmd1 = new SqlCommand("SELECT distinct SRID,SubjectID FROM ResultCalculate", con1);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {

                int counter = 0;

                if (Convert.ToInt32(ds1.Tables[0].Rows[i]["Year"]) < Convert.ToInt32(ds1.Tables[0].Rows[i]["CourseDuration"]))
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = findTHCommand(Convert.ToInt32(ds1.Tables[0].Rows[i]["SRID"]));

                    cmd.Connection = con;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string thmarks;

                        if (isTHEntryExist(Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[j]["SubjectID"]), out thmarks))
                        {
                            if (thmarks == "")
                            {
                                Random rd = new Random();
                                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W11 set Theory=@Theory where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]) + "' and SubjectID='" + Convert.ToInt32(ds.Tables[0].Rows[j]["SubjectID"]) + "'", con2);

                                cmd2.Parameters.AddWithValue("@Theory", rd.Next(55, 70));

                                con2.Open();
                                int l = cmd2.ExecuteNonQuery();
                                con2.Close();
                                counter = counter + l;
                            }

                        }
                        else
                        {
                            Random rd = new Random();
                            SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd3 = new SqlCommand("insert into DDEMarkSheet_W11 values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con3);

                            cmd3.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]));
                            cmd3.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ds.Tables[0].Rows[j]["SubjectID"]));
                            cmd3.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[j]["StudyCentreCode"].ToString());
                            cmd3.Parameters.AddWithValue("@Theory", rd.Next(55, 70));
                            cmd3.Parameters.AddWithValue("@IA", "");
                            cmd3.Parameters.AddWithValue("@AW", "");
                            cmd3.Parameters.AddWithValue("@MOE", "R");

                            cmd3.Connection = con3;
                            con3.Open();
                            int k = cmd3.ExecuteNonQuery();
                            con3.Close();
                            counter = counter + k;

                        }

                    }

                }


            }
        }

        private bool isTHEntryExist(int srid, int subjectid, out string thmarks)
        {
            bool exist = false;
            thmarks = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select RID,Theory from DDEMarkSheet_W11 where SRID ='" + srid + "' and SubjectID='" + subjectid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                thmarks = dr["Theory"].ToString();
                exist = true;
            }
            con.Close();

            return exist;

        }

        private string findTHCommand(int srid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDESubject.SubjectID");
            sb.Append(",DDESubject.SubjectName");

            sb.Append(" FROM[dbo].[DDEExamRecord_W11]");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDESubject on DDEStudentRecord.Course=DDESubject.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='"+srid+"' and DDEStudentRecord.Course!='76' and ([DDEExamRecord_W11].Year= DDESubject.NYear)");

            sb.Append(" and(DDEStudentRecord.SyllabusSession= DDESubject.SyllabusSession)");

            sb.Append(" union");

            sb.Append(" SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDESubject.SubjectID");
            sb.Append(",DDESubject.SubjectName");

            sb.Append(" FROM[dbo].[DDEExamRecord_W11]");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDESubject on DDEStudentRecord.Course=DDESubject.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course= '76' and [DDEExamRecord_W11].Year= '1' and ([DDEExamRecord_W11].Year= DDESubject.NYear)");

            sb.Append(" and(DDEStudentRecord.SyllabusSession= DDESubject.SyllabusSession)");

            sb.Append(" union");

            sb.Append(" SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDESubject.SubjectID");
            sb.Append(",DDESubject.SubjectName");

            sb.Append(" FROM[dbo].[DDEExamRecord_W11]");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDESubject on DDEStudentRecord.Course2Year=DDESubject.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course= '76' and [DDEExamRecord_W11].Year= '2' and ([DDEExamRecord_W11].Year= DDESubject.NYear)");

            sb.Append(" and(DDEStudentRecord.SyllabusSession= DDESubject.SyllabusSession)");

            sb.Append(" order by SRID,SubjectID");

            return sb.ToString();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select EnrollmentNo,Session,count(*) as cnt from DDEStudentRecord group by EnrollmentNo,Session having count(*) > 1 and[Session] = 'Q 2019-20'", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);

            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                int srid = FindInfo.findSRIDByENo(ds1.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                int psrid = FindInfo.findPSRIDByENo(ds1.Tables[0].Rows[i]["EnrollmentNo"].ToString());

                if (sameEntry(ds1.Tables[0].Rows[i]["EnrollmentNo"].ToString()))
                {

                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd2 = new SqlCommand("delete from DDEStudentRecord where SRID='" + srid + "'", con2);

                    con2.Open();
                    int j = cmd2.ExecuteNonQuery();
                    con2.Close();

                    SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd3 = new SqlCommand("delete from DDEExamRecord_W11 where SRID='" + srid + "'", con3);

                    con3.Open();
                    int k = cmd3.ExecuteNonQuery();
                    con3.Close();

                    SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd4 = new SqlCommand("delete from DDESLMIssueRecord where SRID='" + srid + "' and LNo='0'", con4);

                    con4.Open();
                    int l = cmd4.ExecuteNonQuery();
                    con4.Close();
                }
                else
                {

                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd2 = new SqlCommand("delete from DDEStudentRecord where SRID='" + srid + "'", con2);

                    con2.Open();
                    int j = cmd2.ExecuteNonQuery();
                    con2.Close();

                    SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd3 = new SqlCommand("delete from DDEExamRecord_W11 where SRID='" + srid + "'", con3);

                    con3.Open();
                    int k = cmd3.ExecuteNonQuery();
                    con3.Close();

                    SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd4 = new SqlCommand("delete from DDESLMIssueRecord where SRID='" + srid + "' and LNo='0'", con4);

                    con4.Open();
                    int l = cmd4.ExecuteNonQuery();
                    con4.Close();

                    SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd5 = new SqlCommand("update DDEPendingStudentRecord set Enrolled=@Enrolled where PSRID='" + psrid + "'", con5);

                    cmd5.Parameters.AddWithValue("@Enrolled", "False");

                    con5.Open();
                    int m = cmd5.ExecuteNonQuery();
                    con5.Close();
                }
               
            }
        }

        private bool sameEntry(string eno)
        {
            bool valid = false;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select * from DDEStudentRecord where EnrollmentNo='"+eno+"'", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            if((ds.Tables[0].Rows[0]["StudentName"].ToString()== ds.Tables[0].Rows[1]["StudentName"].ToString())&&(ds.Tables[0].Rows[0]["FatherName"].ToString() == ds.Tables[0].Rows[1]["FatherName"].ToString()))
            {
                valid = true;
            }
          
            return valid;

           
        }
      
        protected void btnUploadFYResult_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());

            SqlCommand cmd = new SqlCommand("SELECT distinct SRID,SubjectID FROM ResultCalculate where SDID>15074 order by SRID,SubjectID", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                string thmarks;

                if (isTHEntryExist(Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[j]["SubjectID"]), out thmarks))
                {
                    if (thmarks == "")
                    {
                        Random rd = new Random();
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W11 set Theory=@Theory where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]) + "' and SubjectID='" + Convert.ToInt32(ds.Tables[0].Rows[j]["SubjectID"]) + "'", con2);

                        cmd2.Parameters.AddWithValue("@Theory", rd.Next(55, 70));

                        con2.Open();
                        int l = cmd2.ExecuteNonQuery();
                        con2.Close();

                    }

                }
                else
                {
                    Random rd = new Random();
                    SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd3 = new SqlCommand("insert into DDEMarkSheet_W11 values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con3);

                    cmd3.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]));
                    cmd3.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ds.Tables[0].Rows[j]["SubjectID"]));
                    cmd3.Parameters.AddWithValue("@StudyCentreCode", FindInfo.findSCCodeForMarkSheetBySRID(Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"])));
                    cmd3.Parameters.AddWithValue("@Theory", rd.Next(55, 70));
                    cmd3.Parameters.AddWithValue("@IA", "");
                    cmd3.Parameters.AddWithValue("@AW", "");
                    cmd3.Parameters.AddWithValue("@MOE", "R");

                    cmd3.Connection = con3;
                    con3.Open();
                    int k = cmd3.ExecuteNonQuery();
                    con3.Close();


                }

            }
        }

        protected void btnCorrectSySession_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand(findCommand(), con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                int subjectid = findCorrectSubjectID(Convert.ToInt32(ds.Tables[0].Rows[j]["CourseID"]), Convert.ToString(ds.Tables[0].Rows[j]["PaperCode"]));
                if(subjectid!=0)
                {
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W11 set SubjectID=@SubjectID where RID='" + Convert.ToInt32(ds.Tables[0].Rows[j]["RID"]) + "'", con2);

                    cmd2.Parameters.AddWithValue("@SubjectID", subjectid);

                    con2.Open();
                    int l = cmd2.ExecuteNonQuery();
                    con2.Close();
                }
                 
            }
        }

        private int findCorrectSubjectID(int courseid, string papercode)
        {
            int csubid = 0;
           
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select SubjectID from DDESubject where CourseID='" + courseid + "' and PaperCode='"+papercode+ "' and SyllabusSession='A 2013-14'", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            if(ds.Tables[0].Rows.Count>0)
            {
                csubid = Convert.ToInt32(ds.Tables[0].Rows[0]["SubjectID"]);
            }

            return csubid;
        }

        private string findCommand()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT [RID]");
            sb.Append(",[DDEMarkSheet_W11].[SRID]");
            sb.Append(",DDESubject.[SubjectID]");
            sb.Append(",DDESubject.SyllabusSession");
            sb.Append(",DDESubject.CourseID");
            sb.Append(",DDESubject.PaperCode");
            sb.Append(",[DDEMarkSheet_W11].[StudyCentreCode]");
            sb.Append(",[Theory]");
            sb.Append(",[IA]");
            sb.Append(",[AW]");
            sb.Append(",[MOE]");
            sb.Append(" FROM [DDEMarkSheet_W11]");
            sb.Append(" inner join DDEStudentRecord on [DDEMarkSheet_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDESubject on DDESubject.SubjectID=[DDEMarkSheet_W11].SubjectID");
            sb.Append(" where Course2Year!='' and DDESubject.SyllabusSession= 'A 2010-11'  order by SRID");

            return sb.ToString();

        }

        protected void btnDeleteDuplicate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select SRID,SubjectID,count(*) as cnt from [DDEMarkSheet_W11] group by SRID,SubjectID having count(*) > 1 order by SRID", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                int rid = findRID(Convert.ToInt32(ds.Tables[0].Rows[j]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[j]["SubjectID"]));
                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd2 = new SqlCommand("delete from DDEMarkSheet_W11 where RID='" + rid + "'", con2);

                con2.Open();
                int l = cmd2.ExecuteNonQuery();
                con2.Close();               
            }

        }

        private int findRID(int srid, int subid)
        {
            int rid = 0;

            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select RID from DDEMarkSheet_W11 where SRID='" + srid + "' and SubjectID='" + subid + "'", con1);

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rid = Convert.ToInt32(ds.Tables[0].Rows[0]["RID"]);
            }

            return rid;
        }

        protected void btnUploadAss_Click(object sender, EventArgs e)
        {
            int counter = 0;
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Offline Applications\271\1.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString = "SELECT * FROM [Sheet1$]";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(queryString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);



                DataSet ds = new DataSet();
                da.Fill(ds);


                int j = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int srid = FindInfo.findSRIDByENo(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                    int year = findExamYear(srid,"W11");
                    int cd = FindInfo.findCourseDuration(FindInfo.findCourseIDBySRID(srid));
                    if (!(FindInfo.isAssignmentUploaded(srid,"W11")))
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDEStudentAssignments OUTPUT INSERTED.AssID values(@SRID,@FileName,@FileType,@FileSize,@TOE)", con);

                        cmd.Parameters.AddWithValue("@SRID", srid);
                        cmd.Parameters.AddWithValue("@FileName", ds.Tables[0].Rows[i]["EnrollmentNo"].ToString()+".pdf");
                        cmd.Parameters.AddWithValue("@FileType", ".pdf");
                        cmd.Parameters.AddWithValue("@FileSize", 0);
                        cmd.Parameters.AddWithValue("@TOE", DateTime.Now.ToString());

                        cmd.Connection = con;
                        con.Open();
                        object assid = cmd.ExecuteScalar();
                        con.Close();

                        if (year < cd)
                        {
                            updateTHMarks(srid);
                            updatePracMarks(srid);
                        }
                        else if (year== cd)
                        {
                            updatePracMarks(srid);
                        }
                    }

                    counter = counter + 1;

                }

            }


            OleDbConnection.ReleaseObjectPool();

            Response.Write("<br/>" + counter.ToString() + " Students Uploaded.");
        }

        private int findExamYear(int srid, string exam)
        {
            int year = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select Year from DDEExamRecord_W11 where SRID ='" + srid + "' and MOE='R'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                year = Convert.ToInt32(dr["Year"]);
            }
            con.Close();

            return year;
        }

        private int updatePracMarks(int srid)
        {
            int counter = 0;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();


            cmd1.CommandText = findPracCommand(srid);

            cmd1.Connection = con1;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (!(isPracEntryExist(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalID"]))))
                {
                    Random rd = new Random();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEPracticalMarks_W11 values(@SRID,@PracticalID,@StudyCentreCode,@PracticalMarks,@MOE)", con);

                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                    cmd.Parameters.AddWithValue("@PracticalID", Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalID"]));
                    cmd.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                    int minpm = (Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalMaxMarks"]) * 60) / 100;
                    int maxpm = (Convert.ToInt32(ds.Tables[0].Rows[i]["PracticalMaxMarks"]) * 75) / 100;
                    cmd.Parameters.AddWithValue("@PracticalMarks", rd.Next(minpm, maxpm));
                    cmd.Parameters.AddWithValue("@MOE", "R");

                    cmd.Connection = con;
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    counter = counter + j;

                }

            }



            return counter;
        }

        private bool isPracEntryExist(int srid, int pracid)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select SRID from DDEPracticalMarks_W11 where SRID ='" + srid + "' and PracticalID='" + pracid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }
            con.Close();

            return exist;
        }

        private string findPracCommand(int srid)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDEPractical.PracticalID");
            sb.Append(",DDEPractical.PracticalName");
            sb.Append(",DDEPractical.PracticalMaxMarks");

            sb.Append(" FROM DDEExamRecord_W11");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDEPractical on DDEStudentRecord.Course=DDEPractical.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course!='76' and ([DDEExamRecord_W11].Year= DDEPractical.NYear)");

            sb.Append(" and (DDEStudentRecord.SyllabusSession= DDEPractical.SyllabusSession)");

            sb.Append(" union");

            sb.Append(" SELECT");
            sb.Append(" DDEExamRecord_W11.SRID");
            sb.Append(",DDEExamRecord_W11.Year");
            sb.Append(",DDEStudentRecord.StudentName");
            sb.Append(",DDEStudentRecord.EnrollmentNo");
            sb.Append(",DDEStudentRecord.SCStatus");
            sb.Append(",DDEStudentRecord.StudyCentreCode");
            sb.Append(",DDEStudentRecord.PreviousSCCode");
            sb.Append(",DDEStudentRecord.Course");
            sb.Append(",DDEStudentRecord.Course2Year");
            sb.Append(",DDEPractical.PracticalID");
            sb.Append(",DDEPractical.PracticalName");
            sb.Append(",DDEPractical.PracticalMaxMarks");

            sb.Append(" FROM DDEExamRecord_W11");
            sb.Append(" inner join DDEStudentRecord on[DDEExamRecord_W11].SRID=DDEStudentRecord.SRID");
            sb.Append(" inner join DDEPractical on DDEStudentRecord.Course2Year=DDEPractical.CourseID");

            sb.Append(" where [DDEExamRecord_W11].MOE= 'R' and [DDEExamRecord_W11].SRID='" + srid + "' and DDEStudentRecord.Course= '76' and DDEStudentRecord.CYear= '2' and ([DDEExamRecord_W11].Year= DDEPractical.NYear)");

            sb.Append(" and (DDEStudentRecord.SyllabusSession= DDEPractical.SyllabusSession)");

            sb.Append(" order by SRID,PracticalID");


            return sb.ToString();
        }

        private int updateTHMarks(int srid)
        {
            int counter = 0;
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();

            cmd1.CommandText = findTHCommand(srid);


            cmd1.Connection = con1;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string thmarks;

                if (isTHEntryExist(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"]), out thmarks))
                {
                    if (thmarks == "")
                    {
                        Random rd = new Random();
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W11 set Theory=@Theory where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) + "' and SubjectID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"]) + "'", con2);

                        cmd2.Parameters.AddWithValue("@Theory", rd.Next(55, 70));

                        con2.Open();
                        int j = cmd2.ExecuteNonQuery();
                        con2.Close();
                        counter = counter + j;
                    }

                }
                else
                {
                    Random rd = new Random();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_W11 values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                    cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                    cmd.Parameters.AddWithValue("@SubjectID", Convert.ToInt32(ds.Tables[0].Rows[i]["SubjectID"]));
                    cmd.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                    cmd.Parameters.AddWithValue("@Theory", rd.Next(55, 70));
                    cmd.Parameters.AddWithValue("@IA", "");
                    cmd.Parameters.AddWithValue("@AW", "");
                    cmd.Parameters.AddWithValue("@MOE", "R");

                    cmd.Connection = con;
                    con.Open();
                    int j = cmd.ExecuteNonQuery();
                    con.Close();
                    counter = counter + j;

                }

            }

            return counter;
        }

        protected void btnUploadOR_Click(object sender, EventArgs e)
        {
            int counter = 0;


            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlCommand cmd1 = new SqlCommand("SELECT distinct SRID,PaperCode,MOE FROM Result order by SRID");

            cmd1.Connection = con1;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();
            da1.Fill(ds);

        
                int j = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
                    int year = findExamYear(srid, "W11");
                    int cid = FindInfo.findCourseIDBySRID(srid);
                    int cd = FindInfo.findCourseDuration(cid);
                    int subid = findSubjectID(year,cid, Convert.ToString(ds.Tables[0].Rows[i]["PaperCode"]));
                    string sccode = FindInfo.findSCCodeBySRID(srid);
                   
                    string thmarks;
                    if (isTHEntryExist(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), subid, out thmarks))
                    {
                          if (thmarks == "")
                          {
                            Random rd = new Random();
                            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd2 = new SqlCommand("update DDEMarkSheet_W11 set Theory=@Theory where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) + "' and SubjectID='" + subid + "' and MOE='"+ Convert.ToString(ds.Tables[0].Rows[i]["MOE"]) + "'", con2);

                            cmd2.Parameters.AddWithValue("@Theory", rd.Next(55, 70));

                            con2.Open();
                            int k = cmd2.ExecuteNonQuery();
                            con2.Close();
                            counter = counter + k;
                          }

                    }
                    else
                    {
                          Random rd = new Random();
                          SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                          SqlCommand cmd = new SqlCommand("insert into DDEMarkSheet_W11 values(@SRID,@SubjectID,@StudyCentreCode,@Theory,@IA,@AW,@MOE)", con);

                          cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                          cmd.Parameters.AddWithValue("@SubjectID", subid);
                          cmd.Parameters.AddWithValue("@StudyCentreCode", sccode);
                          cmd.Parameters.AddWithValue("@Theory", rd.Next(55, 70));
                          cmd.Parameters.AddWithValue("@IA", "");
                          cmd.Parameters.AddWithValue("@AW", "");
                          cmd.Parameters.AddWithValue("@MOE", Convert.ToString(ds.Tables[0].Rows[i]["MOE"]));

                          cmd.Connection = con;
                          con.Open();
                          int k = cmd.ExecuteNonQuery();
                          con.Close();
                          counter = counter + k;

                    }

                       updatePracMarks(srid);

                }


            Response.Write("<br/>" + counter.ToString() + " Students Uploaded.");
        }

        private int findSubjectID(int year, int cid, string papercode)
        {
            int subid = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select SubjectID from DDESubject where CourseID ='" + cid + "' and NYear='" + year + "' and PaperCode='"+papercode+"'", con);
            SqlDataReader dr;

            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                subid = Convert.ToInt32(dr["SubjectID"]);
            }
            con.Close();

            return subid;
        }

        protected void btnUploadPracMarks_Click(object sender, EventArgs e)
        {
            int counter = 0;


            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("SELECT distinct SRID FROM DDEStudentAssignments order by SRID");

            cmd1.Connection = con1;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds = new DataSet();

            da1.Fill(ds);
            int j = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int srid = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
                //int year = findExamYear(srid, "W11");
                //int cid = FindInfo.findCourseIDBySRID(srid);
                //int cd = FindInfo.findCourseDuration(cid);
                //int subid = findSubjectID(year, cid, Convert.ToString(ds.Tables[0].Rows[i]["PaperCode"]));
                //string sccode = FindInfo.findSCCodeBySRID(srid);
             
                updatePracMarks(srid);

            }


            Response.Write("<br/>" + counter.ToString() + " Students Uploaded.");
        }
    }
}
