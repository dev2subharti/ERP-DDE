using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DDE.DAL;
using System.IO;

namespace DDE.Web
{
    public partial class UploadExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
          
            int counter = 0;
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Offline Applications\999\C 2020\Excel Sheets\11.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
            string queryString = "SELECT * FROM [Applications$]";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {

                OleDbCommand command = new OleDbCommand(queryString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);

                string scf = "No";
                int j = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        if (!(isSCFormExist(Convert.ToInt32(ds.Tables[0].Rows[i]["SCFormCounter"]), ds.Tables[0].Rows[i]["AFCode"].ToString())))
                        {
                            if (validEntry(ds, i))
                            {
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("insert into DDEPendingStudentRecord (ApplicationNo,SCFormCounter,StudyCentreCode,SCStatus,PreviousSCCode,Session,SyllabusSession,AdmissionType,PreviousInstitute,PreviousCourse,CYear,Course,StudentName,FatherName,MotherName,Gender,DOBDay,DOBMonth,DOBYear,CAddress,City,District,State,PinCode,PhoneNo,MobileNo,Email,DOA,Nationality,Category,Employmentstatus,examname1,examname2,examname3,examname4,examname5,subject1,subject2,subject3,subject4,subject5,YearPass1,YearPass2,YearPass3,YearPass4,YearPass5,UniversityBoard1,UniversityBoard2,UniversityBoard3,UniversityBoard4,UniversityBoard5,Divisiongrade1,Divisiongrade2,Divisiongrade3,Divisiongrade4,Divisiongrade5,Eligible,OriginalsVerified,AdmissionStatus,ReasonIfPending,Enrolled) values (@ApplicationNo,@SCFormCounter,@StudyCentreCode,@SCStatus,@PreviousSCCode,@Session,@SyllabusSession,@AdmissionType,@PreviousInstitute,@PreviousCourse,@CYear,@Course,@StudentName,@FatherName,@MotherName,@Gender,@DOBDay,@DOBMonth,@DOBYear,@CAddress,@City,@District,@State,@PinCode,@PhoneNo,@MobileNo,@Email,@DOA,@Nationality,@Category,@Employmentstatus,@examname1,@examname2,@examname3,@examname4,@examname5,@subject1,@subject2,@subject3,@subject4,@subject5,@YearPass1,@YearPass2,@YearPass3,@YearPass4,@YearPass5,@UniversityBoard1,@UniversityBoard2,@UniversityBoard3,@UniversityBoard4,@UniversityBoard5,@Divisiongrade1,@Divisiongrade2,@Divisiongrade3,@Divisiongrade4,@Divisiongrade5,@Eligible,@OriginalsVerified,@AdmissionStatus,@ReasonIfPending,@Enrolled)", con);

                                cmd.Parameters.AddWithValue("@ApplicationNo", ds.Tables[0].Rows[i]["ApplicationNo"].ToString());
                                cmd.Parameters.AddWithValue("@SCFormCounter",Convert.ToInt32(ds.Tables[0].Rows[i]["SCFormCounter"]));

                                if (ds.Tables[0].Rows[i]["AFStatus"].ToString() == "TRANSFER TO 001")
                                {
                                    cmd.Parameters.AddWithValue("@StudyCentreCode", "001");
                                    cmd.Parameters.AddWithValue("@SCStatus", "T");
                                    cmd.Parameters.AddWithValue("@PreviousSCCode", ds.Tables[0].Rows[i]["AFCode"].ToString());

                                }
                                else if (ds.Tables[0].Rows[i]["AFStatus"].ToString() == "SELF")
                                {
                                    cmd.Parameters.AddWithValue("@StudyCentreCode", ds.Tables[0].Rows[i]["AFCode"].ToString());
                                    cmd.Parameters.AddWithValue("@SCStatus", "O");
                                    cmd.Parameters.AddWithValue("@PreviousSCCode", "");

                                }

                                cmd.Parameters.AddWithValue("@Session", ds.Tables[0].Rows[i]["Session"].ToString());
                                cmd.Parameters.AddWithValue("@SyllabusSession", findSyllabusSession(findCourseID(ds.Tables[0].Rows[i]["Course (Applying For)"].ToString())));

                                if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "REGULAR")
                                {
                                    cmd.Parameters.AddWithValue("@AdmissionType", 1);
                                    cmd.Parameters.AddWithValue("@PreviousInstitute", "");
                                    cmd.Parameters.AddWithValue("@PreviousCourse", "");
                                }

                                else if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "CREDIT TRANSFER" || ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "LATERAL ENTRY")
                                {
                                    cmd.Parameters.AddWithValue("@AdmissionType", findAdmissionType(ds.Tables[0].Rows[i]["AdmissionType"].ToString()));
                                    cmd.Parameters.AddWithValue("@PreviousInstitute", findPreviousInst(ds.Tables[0].Rows[i]["PreviousInstitute"].ToString()));
                                    cmd.Parameters.AddWithValue("@PreviousCourse", findCourseID(ds.Tables[0].Rows[i]["Previous Course"].ToString()));
                                }

                                cmd.Parameters.AddWithValue("@CYear", Convert.ToInt32(ds.Tables[0].Rows[i]["Year"].ToString()));
                                cmd.Parameters.AddWithValue("@Course", findCourseID(ds.Tables[0].Rows[i]["Course (Applying For)"].ToString()));

                                cmd.Parameters.AddWithValue("@StudentName", ds.Tables[0].Rows[i]["StudentName"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@FatherName", ds.Tables[0].Rows[i]["FatherName"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@MotherName", ds.Tables[0].Rows[i]["MotherName"].ToString().ToUpper());

                                cmd.Parameters.AddWithValue("@Gender", ds.Tables[0].Rows[i]["Gender"].ToString().ToUpper());

                                //string dob = ds.Tables[0].Rows[i]["DOB (DD-MM-YYYY)"].ToString();
                                string dob = Convert.ToDateTime(ds.Tables[0].Rows[i]["DOB (DD-MM-YYYY)"]).ToString("dd-MM-yyyy");
                                cmd.Parameters.AddWithValue("@DOBDay", dob.Substring(0, 2));
                                cmd.Parameters.AddWithValue("@DOBMonth", FindInfo.findMonthByMonthNo(Convert.ToInt32(dob.Substring(3, 2))).ToUpper());
                                cmd.Parameters.AddWithValue("@DOBYear", dob.Substring(6, 4));
                                cmd.Parameters.AddWithValue("@CAddress", ds.Tables[0].Rows[i]["CAddress"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@City", ds.Tables[0].Rows[i]["City"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@District", ds.Tables[0].Rows[i]["City"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@State", ds.Tables[0].Rows[i]["State"].ToString().ToUpper());
                                cmd.Parameters.AddWithValue("@PinCode", ds.Tables[0].Rows[i]["PinCode"].ToString());
                                cmd.Parameters.AddWithValue("@PhoneNo", ds.Tables[0].Rows[i]["PhoneNo"].ToString());
                                cmd.Parameters.AddWithValue("@MobileNo", ds.Tables[0].Rows[i]["MobileNo"].ToString());
                                cmd.Parameters.AddWithValue("@Email", ds.Tables[0].Rows[i]["Email"].ToString());
                                cmd.Parameters.AddWithValue("@DOA", DateTime.Now);

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

                                cmd.Parameters.AddWithValue("@Eligible", "");

                                cmd.Parameters.AddWithValue("@OriginalsVerified", "");
                                cmd.Parameters.AddWithValue("@AdmissionStatus", "0");
                                cmd.Parameters.AddWithValue("@ReasonIfPending", "");
                                cmd.Parameters.AddWithValue("@Enrolled", "False");

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                counter = counter + 1;

                            }
                            else
                            {
                                Response.Write("<br/>Error on SCFormCounter : " + ds.Tables[0].Rows[i]["SCFormCounter"].ToString() + ".Error Message : Invalid Entry.");
                                goto outer;
                            }
                        }
                        else
                        {
                            if (scf == "No")
                            {
                                scf = ds.Tables[0].Rows[i]["SCFormCounter"].ToString();
                            }
                            else
                            {
                                scf = scf + ", " + ds.Tables[0].Rows[i]["SCFormCounter"].ToString();
                            }
                        }

                        OleDbConnection.ReleaseObjectPool();

                       
                    }
                    catch (Exception er)
                    {

                        Response.Write("<br/>Error on SCFormCounter : " +ds.Tables[0].Rows[i]["SCFormCounter"].ToString()+".Error Message : "+er.Message);
                        goto outer;

                    }

                    outer:
                    continue;
                }

                Response.Write("<br/>" + counter.ToString() + " Students uploaded");
                Response.Write("<br/>" + scf + " Students already exist");
            }        
           
        }

        private bool isSCFormExist(int scformcounter, string sccode)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select PSRID from DDEPendingStudentRecord where SCFormCounter='" + scformcounter + "' and StudyCentreCode='"+sccode+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {              
                exist = true;
            }

            con.Close();
            return exist;
        }

        private int findAdmissionType(string at)
        {
            int adty = 0;
            if (at == "REGULAR")
            {
                adty = 1;
            }
            else if (at == "CREDIT TRANSFER")
            {
                adty = 2;
            }
            else if (at == "LATERAL ENTRY")
            {
                adty = 3;
            }

            return adty;
        }

        private int findPreviousInst(string PI)
        {
            int p = 0;
            if (PI == "SVSU")
            {
                p=1;
            }
            else if (PI == "OTHER")
            {
                p=2;
            }
            return p;
        }

        private int findCourseID(string course)
        {
            int cid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select CourseID from DDECourse where CourseName ='" + course + "' ", con);
            SqlDataReader dr;
         
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                cid =Convert.ToInt32(dr[0].ToString());
               
            }
            con.Close();

            return cid;
        }

        private string findSyllabusSession(int cid)
        {
            string ss = "";
            if (cid == 12 || cid == 27 || (FindInfo.findCourseShortNameByID(cid) == "MBA"))
            {
                ss = "A 2013-14";
            }
            else
            {
                ss = "A 2010-11";
            }
            return ss;
        }

        private bool validEntry(DataSet ds, int i)
        {
            return true;
        }

        protected void btnUploadStPhoto_Click(object sender, EventArgs e)
        {
            //string error = "";
            HttpFileCollection uploadedFiles = Request.Files;
            int counter = 0;

            if (uploadedFiles.Count >= 0)
            {

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];

                    try
                    {
                        if (userPostedFile.ContentLength > 0)
                        {
                        
                                //if (!(File.Exists(Server.MapPath("OAPhotos/" + Path.GetFileName(userPostedFile.FileName)))))
                                //{
                                    string scformcounter = "";
                                    string fileExt = System.IO.Path.GetExtension(userPostedFile.FileName);

                                    if (fileExt == ".jpeg" || fileExt == ".JPEG")
                                    {
                                        scformcounter = userPostedFile.FileName.Substring(0, (userPostedFile.FileName.Length - 5));
                                    }
                                    else if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".gif" || fileExt == ".JPG" || fileExt == ".PNG" || fileExt == ".GIF")
                                    {
                                        scformcounter = userPostedFile.FileName.Substring(0, (userPostedFile.FileName.Length - 4));
                                    }
                                   
                                    byte[] sp = ImageToByteArray(userPostedFile);
                                    
                                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                    SqlCommand cmd1 = new SqlCommand("update DDEPendingStudentRecord set StudentPhoto=@StudentPhoto where SCFormCounter='" + scformcounter + "' and StudyCentreCode='999'", con1);
                                    //SqlCommand cmd1 = new SqlCommand("update DDEPendingStudentRecord set StudentPhoto=@StudentPhoto where PSRID='" + scformcounter + "' and StudyCentreCode='999'", con1);
                                    //SqlCommand cmd1 = new SqlCommand("update DDEStudentRecord set StudentPhoto=@StudentPhoto where EnrollmentNo='" + scformcounter + "'", con1);
                                    cmd1.Parameters.AddWithValue("@StudentPhoto", sp);

                                    con1.Open();
                                    int count = cmd1.ExecuteNonQuery();
                                    con1.Close();
                                   
                                    counter = counter + count;                                      

                                //}
                                                  

                        }
                    }
                    catch (Exception Ex)
                    {

                        //error = lblMSG.Text = "Some error in file : " + Path.GetFileName(userPostedFile.FileName) + "<br/>Error message : " + Ex.Message;
                        //pnlMSG.Visible = true;
                        break;
                    }
                }

               
                    //lblMSG.Text = "'" + counter + "'  Photos Uploaded";
                    //pnlMSG.Visible = true;
               

            }
            else
            {

                //lblMSG.Text = "Sorry !! You did not select any photo. Please select any photo.";
                //pnlMSG.Visible = true;
            }

            Response.Write(counter.ToString()+"Photos Uploaded");
        }

        public byte[] ImageToByteArray(HttpPostedFile userpostedfile)
        {
            byte[] data = null;
           
            long numBytes = userpostedfile.ContentLength;
            //FileStream fStream = new FileStream(userpostedfile.FileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(userpostedfile.InputStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    int counter = 0;
        //    string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Offline Applications\999\Excel Sheets\13.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
        //    string queryString = "SELECT * FROM [Applications$]";

        //    using (OleDbConnection connection = new OleDbConnection(connectionString))
        //    {
        //        OleDbCommand command = new OleDbCommand(queryString, connection);
        //        OleDbDataAdapter da = new OleDbDataAdapter(command);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
             
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (!(enrolled(Convert.ToInt32(ds.Tables[0].Rows[i]["SCFormCounter"]))))
        //            {

        //                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //                SqlCommand cmd = new SqlCommand("delete from DDEPendingStudentRecord where SCFormCounter ='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SCFormCounter"]) + "' and StudyCentreCode='999'", con);

        //                con.Open();
        //                cmd.ExecuteReader();
        //                con.Close();

        //                counter = counter + 1;
        //            }
        //            else
        //            {
        //                Response.Write(ds.Tables[0].Rows[i]["SCFormCounter"].ToString() + "<br/>");
        //            }
        //        }

        //        Response.Write(counter + " Entries deleted");

        //    }
        //}

        private bool enrolled(int scfc)
        {
            bool exist = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Enrolled from DDEPendingStudentRecord where SCFormCounter='" + scfc + "' and StudyCentreCode='999'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                if (dr[0].ToString() == "True")
                {
                    exist = true;
                }

            }

            con.Close();
            return exist;
        }

        protected void btnUploadQP_Click(object sender, EventArgs e)
        {

            if(rblType.SelectedItem.Value=="0")
            {
                int counter = 0;
                string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\QP\" + fuQuestion.FileName + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
                string queryString = "SELECT * FROM [Sheet1$]";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(queryString, connection);
                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (!(isQuestionExist(ds.Tables[0].Rows[i]["Question"].ToString(), tbPaperCode.Text)))
                        {

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "insert into QuestionBank (PaperCode,QT,Question,A,B,C,D,Ans) OUTPUT INSERTED.QID values (@PaperCode,@QT,@Question,@A,@B,@C,@D,@Ans)";

                            cmd.Parameters.AddWithValue("@PaperCode", tbPaperCode.Text);
                            cmd.Parameters.AddWithValue("@QT", 0);
                            cmd.Parameters.AddWithValue("@Question", ds.Tables[0].Rows[i]["Question"].ToString());

                            cmd.Parameters.AddWithValue("@A", ds.Tables[0].Rows[i]["Option A"].ToString());
                            cmd.Parameters.AddWithValue("@B", ds.Tables[0].Rows[i]["Option B"].ToString());
                            cmd.Parameters.AddWithValue("@C", ds.Tables[0].Rows[i]["Option C"].ToString());
                            cmd.Parameters.AddWithValue("@D", ds.Tables[0].Rows[i]["Option D"].ToString());

                            cmd.Parameters.AddWithValue("@Ans", ds.Tables[0].Rows[i]["Key"].ToString());

                            cmd.Connection = con;

                            con.Open();
                            object qid = cmd.ExecuteScalar();
                            con.Close();

                            if (Convert.ToInt32(qid) > 0)
                            {
                                counter = counter + 1;
                            }

                        }
                        else
                        {
                            Response.Write(ds.Tables[0].Rows[i]["Question"].ToString() + " already exist.<br/>");
                        }
                    }
                }

                Response.Write(counter + " Question Uploaded");
            }
            else if (rblType.SelectedItem.Value == "1")
            {
                HttpFileCollection uploadedFiles = Request.Files;

                int counter = 0;

                if (uploadedFiles.Count >= 0)
                {
                    for (int i = 0; i < uploadedFiles.Count; i++)
                    {
                        HttpPostedFile userPostedFile = uploadedFiles[i];

                        try
                        {
                            if (userPostedFile.ContentLength > 0)
                            {
                                string[] str = userPostedFile.FileName.Split('.');
                                string ans = str[1];

                                byte[] sp = ImageToByteArray(userPostedFile);
                             
                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = "insert into QuestionBank (PaperCode,QT,QImage,A,B,C,D,Ans) OUTPUT INSERTED.QID values (@PaperCode,@QT,@QImage,@A,@B,@C,@D,@Ans)";

                                cmd.Parameters.AddWithValue("@PaperCode", tbPaperCode.Text);
                                cmd.Parameters.AddWithValue("@QT", 1);
                                cmd.Parameters.AddWithValue("@QImage", sp);

                                cmd.Parameters.AddWithValue("@A", "A");
                                cmd.Parameters.AddWithValue("@B", "B");
                                cmd.Parameters.AddWithValue("@C", "C");
                                cmd.Parameters.AddWithValue("@D", "D");

                                cmd.Parameters.AddWithValue("@Ans", ans);

                                cmd.Connection = con;

                                con.Open();
                                object qid = cmd.ExecuteScalar();
                                con.Close();

                                if (Convert.ToInt32(qid) > 0)
                                {
                                    counter = counter + 1;
                                }

                            }
                        }
                        catch (Exception Ex)
                        {

                            //error = lblMSG.Text = "Some error in file : " + Path.GetFileName(userPostedFile.FileName) + "<br/>Error message : " + Ex.Message;
                            //pnlMSG.Visible = true;
                            break;
                        }
                    }

                }
                else
                {

                    //lblMSG.Text = "Sorry !! You did not select any photo. Please select any photo.";
                    //pnlMSG.Visible = true;
                }

                Response.Write(counter + " Question Uploaded");
            }
        }

        private bool isQuestionExist(string question, string papercode)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlCommand scmd = new SqlCommand("Select QID from QuestionBank where Question='" + question + "' and PaperCode='" + papercode + "'", con);
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
    }

       
}

        
    