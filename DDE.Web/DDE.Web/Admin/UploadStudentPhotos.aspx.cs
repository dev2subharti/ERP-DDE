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

namespace DDE.Web.Admin
{
    public partial class UploadStudentPhotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 98))
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

        protected void btnUploadSPhotos_Click(object sender, EventArgs e)
        {
            string error = "";
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
                                string eno = "";
                                string fileExt = System.IO.Path.GetExtension(userPostedFile.FileName);

                                if (fileExt == ".jpeg" || fileExt == ".JPEG")
                                {
                                    eno = userPostedFile.FileName.Substring(0, (userPostedFile.FileName.Length - 5));
                                }
                                else if (fileExt == ".jpg" || fileExt == ".png" || fileExt == ".JPG" || fileExt == ".PNG")
                                {
                                    eno = userPostedFile.FileName.Substring(0, (userPostedFile.FileName.Length - 4));
                                }

                                int srid = FindInfo.findSRIDByENo(eno);

                                bool exist = false;
                                if ((FindInfo.IsStudentPhotoExist(srid)))
                                {
                                    exist = true;
                                } 
                                        
                                    byte[] sp = ImageToByteArray(eno, userPostedFile);
                                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                    SqlCommand cmd1 = new SqlCommand("update DDEStudentRecord set StudentPhoto=@StudentPhoto where SRID='" + srid + "'", con1);
                                    cmd1.Parameters.AddWithValue("@StudentPhoto", sp);

                                    con1.Open();
                                    int count = cmd1.ExecuteNonQuery();
                                    con1.Close();
                                    if (count == 1)
                                    {
                                        counter = counter + 1;

                                        if (exist == true)
                                        {
                                            Log.createLogNow("Change Student Photo", "Photo Changed of Student with ENo. " + eno, Convert.ToInt32(Session["ERID"]));
                                        }
                                        else if(exist == false)
                                        {
                                            Log.createLogNow("Upload Student Photo", "Photo uploaded of Student with ENo. " + eno, Convert.ToInt32(Session["ERID"]));
                                        }
                                    }          
                                                                  
                            }
                        }
                        catch (Exception Ex)
                        {

                            error = lblMSG.Text = "Some error in file : " + Path.GetFileName(userPostedFile.FileName) + "<br/>Error message : " + Ex.Message;
                            pnlMSG.Visible = true;
                            break;
                        }
                    }

                    //if (error == "")
                    //{
                    //    if (rblMode.SelectedItem.Value == "1")
                    //    {
                    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    //        SqlCommand cmd = new SqlCommand("Select SRID,EnrollmentNo from DDEStudentRecord where StudentPhoto is null", con);
                    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //        DataSet ds = new DataSet();
                    //        da.Fill(ds);
                    //        int counter = 0;
                    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //        {
                    //            if (File.Exists(Server.MapPath("StudentPhotos/" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString() + ".jpg")))
                    //            {
                    //                byte[] sp = ImageToByteArray(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                    //                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    //                SqlCommand cmd1 = new SqlCommand("update DDEStudentRecord set StudentPhoto=@StudentPhoto where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) + "' ", con1);
                    //                cmd1.Parameters.AddWithValue("@StudentPhoto", sp);

                    //                con1.Open();
                    //                cmd1.ExecuteNonQuery();
                    //                con1.Close();
                    //                counter = counter + 1;
                    //                Log.createLogNow("Upload Student Photo", "Photo uploaded of Student with ENo. " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString(), Convert.ToInt32(Session["ERID"]));

                    //            }


                    //        }


                    //        lblMSG.Text = "'" + counter + "'  Photos Uploaded";
                    //        pnlMSG.Visible = true;
                    //    }
                    //    else if (rblMode.SelectedItem.Value == "2")
                    //    {
                    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    //        SqlCommand cmd = new SqlCommand("Select SRID,EnrollmentNo from DDEStudentRecord where SRID in", con);
                    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //        DataSet ds = new DataSet();
                    //        da.Fill(ds);
                    //        int counter = 0;
                    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //        {
                    //            if (File.Exists(Server.MapPath("StudentPhotos/" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString() + ".jpg")))
                    //            {
                    //                byte[] sp = ImageToByteArray(ds.Tables[0].Rows[i]["EnrollmentNo"].ToString());
                    //                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    //                SqlCommand cmd1 = new SqlCommand("update DDEStudentRecord set StudentPhoto=@StudentPhoto where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) + "' ", con1);
                    //                cmd1.Parameters.AddWithValue("@StudentPhoto", sp);

                    //                con1.Open();
                    //                cmd1.ExecuteNonQuery();
                    //                con1.Close();
                    //                counter = counter + 1;
                    //                Log.createLogNow("Upload Student Photo", "Photo uploaded of Student with ENo. " + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString(), Convert.ToInt32(Session["ERID"]));

                    //            }


                    //        }


                    //        lblMSG.Text = "'" + counter + "'  Photos Uploaded";
                    //        pnlMSG.Visible = true;
                    //    }
                    //}
                    //else
                    //{
                    //    lblMSG.Text = error;
                    //    pnlMSG.Visible = true;
                    //}
                 
                        lblMSG.Text = "'" + counter + "'  Photos Uploaded";
                        pnlMSG.Visible = true;
                   
               
            }
            else
            {

                lblMSG.Text = "Sorry !! You did not select any photo. Please select any photo.";
                pnlMSG.Visible = true;
            }
        }

        public byte[] ImageToByteArray(string eno, HttpPostedFile userpostedfile)
        {

            byte[] data = null;
            long numBytes = userpostedfile.ContentLength;
            BinaryReader br = new BinaryReader(userpostedfile.InputStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }


        
    }
}