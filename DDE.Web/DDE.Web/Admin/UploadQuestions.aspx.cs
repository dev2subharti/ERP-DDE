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
using System.IO;
using System.Data.OleDb;

namespace DDE.Web.Admin
{
    public partial class UploadQuestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 108))
            {
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


        
        public void readfiles()
        {
            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];
            string fname = userPostedFile.FileName;
            string path = Server.MapPath("/Admin/ExcelSheet/" + fname);
            System.IO.Stream inStream = userPostedFile.InputStream; byte[] fileData = new byte[userPostedFile.ContentLength];
            inStream.Read(fileData, 0, userPostedFile.ContentLength);
            userPostedFile.SaveAs(path);
        }
      
        protected void btnUploadQP_Click(object sender, EventArgs e)
        {
            try
            {
                readfiles();
                if (rblType.SelectedItem.Value == "0")
                {
                    int counter = 0; string path = Server.MapPath("/Admin/ExcelSheet/" + fuQuestion.FileName);
                    string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
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
                                cmd.CommandText = "insert into QuestionBank_Semester (PaperCode,QT,Question,A,B,C,D,Ans) OUTPUT INSERTED.QID values (@PaperCode,@QT,@Question,@A,@B,@C,@D,@Ans)";

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
                                    cmd.CommandText = "insert into QuestionBank_Semester (PaperCode,QT,QImage,A,B,C,D,Ans) OUTPUT INSERTED.QID values (@PaperCode,@QT,@QImage,@A,@B,@C,@D,@Ans)";

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

                                lblMSG.Text = "Some error in file : " + Path.GetFileName(userPostedFile.FileName) + "<br/>Error message : " + Ex.Message;
                                pnlMSG.Visible = true;
                                break;
                            }
                        }

                    }
                    else
                    {

                        lblMSG.Text = "Sorry !! You did not select any photo. Please select any photo.";
                        pnlMSG.Visible = true;
                    }


                }
            }
            catch (Exception ex)
            {
                pnlData.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }
        }

        private bool isQuestionExist(string question, string papercode)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSonlineexam"].ToString());
            SqlCommand scmd = new SqlCommand("Select QID from QuestionBank_Semester where Question='" + question + "' and PaperCode='" + papercode + "'", con);
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

        public byte[] ImageToByteArray(HttpPostedFile userpostedfile)
        {
            byte[] data = null;

            long numBytes = userpostedfile.ContentLength;
            BinaryReader br = new BinaryReader(userpostedfile.InputStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }
    }
}