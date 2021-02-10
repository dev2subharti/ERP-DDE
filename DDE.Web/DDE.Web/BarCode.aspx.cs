using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DDE.DAL;

namespace DDE.Web
{
    public partial class BarCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //populateRecord();
        }

        private void populateRecord()
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("Batch");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("Address");
            DataColumn dtcol5 = new DataColumn("City/State");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Session,EnrollmentNo,CAddress from DDEStudentRecord where RecordStatus='True'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
      
                DataRow drow = dt.NewRow();
                drow["SNo"] = i+1;
                drow["Batch"] = ds.Tables[0].Rows[i]["Session"].ToString();
                drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                string s=ds.Tables[0].Rows[i]["CAddress"].ToString();
                string fs = s.Trim();
                string[] str = fs.Split(' ',',');
                drow["Address"] = fs;
                drow["City/State"]=str[str.Length-1].ToString();

                dt.Rows.Add(drow);
                
            }


            gv.DataSource = dt;
            gv.DataBind();
        }


        protected void Upload(object sender, EventArgs e)
        {
        //    foreach (HttpPostedFile postedFile in fupld.PostedFile)
        //    {
        //        string filename = Path.GetFileName(postedFile.FileName);
        //        string contentType = postedFile.ContentType;
        //        using (Stream fs = postedFile.InputStream)
        //        {
        //            using (BinaryReader br = new BinaryReader(fs))
        //            {
        //                byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //                using (SqlConnection con = new SqlConnection(constr))
        //                {
        //                    string query = "insert into tblFiles values (@Name, @ContentType, @Data)";
        //                    using (SqlCommand cmd = new SqlCommand(query))
        //                    {
        //                        cmd.Connection = con;
        //                        cmd.Parameters.AddWithValue("@Name", filename);
        //                        cmd.Parameters.AddWithValue("@ContentType", contentType);
        //                        cmd.Parameters.AddWithValue("@Data", bytes);
        //                        con.Open();
        //                        cmd.ExecuteNonQuery();
        //                        con.Close();
        //                    }
        //                }
        //            }
        //        }
        //    }
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into Try (name) values (@name)", con);

          
            cmd.Parameters.AddWithValue("@name", "ab");
            


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlCommand cmd = new SqlCommand("Select * from Try", con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    if ((ds.Tables[0].Rows[i]["photo"]).ToString() == "System.Byte[]")
            //    {
            //        Response.Write("<br/>" + (ds.Tables[0].Rows[i]["photo"]).ToString());
            //    }

            //}


          
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string code ="";
            //string code = "DDE," + Environment.NewLine + "Swami Vivekanand Subharti University, Meerut" + Environment.NewLine + "Name : PRAKASH GUPTA s/o UMA SHANKAR GUPTA" + Environment.NewLine + "Enrollment No : A1020507462" + Environment.NewLine + "Course : BCA" + Environment.NewLine + "Division : 1ST" + Environment.NewLine + "Passed Examination : JUNE 2013" + Environment.NewLine + "Link : http://www.subhartidde.com/Degree.aspx?EN=A1020507462";
            if (ddlistDoc.SelectedItem.Text == "CMS")
            {
                code = "For verification and more details please follow the link : www.subhartidde.com/CMS.aspx?EN=" + tbENo.Text;
            }
            else if (ddlistDoc.SelectedItem.Text == "DEGREE")
            {
                code = "For verification and more details please follow the link : www.subhartidde.com/Degree.aspx?EN="+tbENo.Text;
            }          
            else if (ddlistDoc.SelectedItem.Text == "TS")
            {
                code = "For verification and more details please follow the link : www.subhartidde.com/TS.aspx?EN=" + tbENo.Text;
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 70;
            imgBarCode.Width = 70;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);

                }
                pnlbc.Controls.Add(imgBarCode);

                //bitMap.Save(Server.MapPath("DI/" + TextBox1.Text + ".png"), System.Drawing.Imaging.ImageFormat.Png);

                //Response.Redirect("DI/" + TextBox1.Text + ".png");

            }
        }

    }
}