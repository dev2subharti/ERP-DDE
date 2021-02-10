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
using System.Net.Mail;
using System.Text;
using System.Net.Mime;

namespace DDE.Web.Admin
{
    public partial class PublishPasswordEC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
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

        private void setMailStatus()
        {
            foreach (DataListItem dli in dtlistShowExamCentres.Items)
            {

                Label pass = (Label)dli.FindControl("lblPassword");
                Label pms = (Label)dli.FindControl("lblPassMailSent");
                Button sendmail = (Button)dli.FindControl("btnSendMail");

                if (pass.Text == "<b>Changed</b>")
                {
                    sendmail.Visible = false;
                }
                else
                {
                    if (pms.Text == "True")
                    {
                        sendmail.Visible = false;
                    }
                    else if (pms.Text == "False")
                    {
                        sendmail.Visible = true;
                    }
                }

            }
        }

        private void populateExamCentres()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_" + ddlistEC.SelectedItem.Value + " order by ExamCentreCode", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ECID");
            DataColumn dtcol3 = new DataColumn("SCCodes");
            DataColumn dtcol4 = new DataColumn("ExamCentreCode");
            DataColumn dtcol5 = new DataColumn("Password");
            DataColumn dtcol6 = new DataColumn("Email");
            DataColumn dtcol7 = new DataColumn("CentreName");
            DataColumn dtcol8 = new DataColumn("PassMailSent");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["ECID"] = Convert.ToString(dr["ECID"]);
                drow["SCCodes"] = Convert.ToString(dr["SCCodes"]);
                drow["ExamCentreCode"] = Convert.ToString(dr["ExamCentreCode"]);              
                drow["Password"] = Convert.ToString(dr["Password"]);
                drow["Email"] = Convert.ToString(dr["Email"]);
                drow["CentreName"] = Convert.ToString(dr["CentreName"]) + "," + Convert.ToString(dr["City"]);
                drow["PassMailSent"] = Convert.ToString(dr["PasswordMailSent"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowExamCentres.DataSource = dt;
            dtlistShowExamCentres.DataBind();

            con.Close();
        }

        protected void dtlistShowExamCentres_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string emailid = FindInfo.findEmailIDByECID(Convert.ToInt32(e.CommandArgument), ddlistEC.SelectedItem.Value);
            string eccode = FindInfo.findECCodeByECID(Convert.ToInt32(e.CommandArgument), ddlistEC.SelectedItem.Value);
            if (e.CommandName == "Send Mail")
            {
                SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                mySmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("ddesvsu@gmail.com", "subhartidebsvsudec30");

                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress from = new MailAddress("ddesvsu@gmail.com", "DDE, SVSU Meerut");
                MailAddress to = new MailAddress(emailid, "Question Papers : 04-09-2019, 1st Meeting");
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                myMail.Subject = "Question Papers : 04-09-2019, 1st Meeting";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                myMail.Body = findMailBody(Convert.ToInt32(e.CommandArgument));
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                string file = Server.MapPath("QuestionBank/QuestionPapers.zip");
                Attachment a = new Attachment(file, MediaTypeNames.Application.Octet);
                myMail.Attachments.Add(a);

                myMail.IsBodyHtml = true;

                mySmtpClient.Port = 587;
                mySmtpClient.EnableSsl = true;
                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;


                mySmtpClient.Send(myMail);

                updatePassMailSent(Convert.ToInt32(e.CommandArgument), "True");
                populateExamCentres();
                setMailStatus();

                


                //SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                //mySmtpClient.UseDefaultCredentials = false;
                //System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("ddesvsu@gmail.com", "subhartidebsvsudec30");

                //mySmtpClient.Credentials = basicAuthenticationInfo;

                //MailAddress from = new MailAddress("ddesvsu@gmail.com", "DDE, SVSU Meerut");
                //MailAddress to = new MailAddress(emailid, "Examination Centre : " + eccode);
                //MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                //myMail.Subject = "Confidential : Username and Password of Exam Centre Login of Exam Centre Code : " + eccode;
                //myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                //myMail.Body = findMailBody(Convert.ToInt32(e.CommandArgument));
                //myMail.BodyEncoding = System.Text.Encoding.UTF8;

                //string file = Server.MapPath("Images/General Rules for Exam.pdf");
                //Attachment a = new Attachment(file, MediaTypeNames.Application.Octet);
                //myMail.Attachments.Add(a);

                //myMail.IsBodyHtml = true;

                //mySmtpClient.Port = 587;
                //mySmtpClient.EnableSsl = true;
                //mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;


                //mySmtpClient.Send(myMail);

                //updatePassMailSent(Convert.ToInt32(e.CommandArgument), "True");
                //populateExamCentres();
                //setMailStatus();

                //Log.createLogNow("Mail Sent", "Sent Password Mail to Exam Centre Code '" + eccode + "' for '"+ddlistEC.SelectedItem.Text+"' Exam", Convert.ToInt32(Session["ERID"].ToString()));
            }
            else if (e.CommandName == "Reset Password")
            {
               
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEExaminationCentres_" + ddlistEC.SelectedItem.Value + " set PasswordMailSent=@PasswordMailSent where ECID='" + e.CommandArgument + "'", con);

                cmd.Parameters.AddWithValue("@PasswordMailSent", "False");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                populateExamCentres();
                setMailStatus();

               
                
                //Random rd = new Random();
                //string password = rd.Next(100000, 999999).ToString();
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                //SqlCommand cmd = new SqlCommand("update DDEExaminationCentres_" + ddlistEC.SelectedItem.Value + " set Password=@Password,NoTimesLoggedIn=@NoTimesLoggedIn,PasswordMailSent=@PasswordMailSent where ECID='" + e.CommandArgument + "'", con);

                //cmd.Parameters.AddWithValue("@Password", password);
                //cmd.Parameters.AddWithValue("@NoTimesLoggedIn", 0);
                //cmd.Parameters.AddWithValue("@PasswordMailSent", "False");

                //con.Open();
                //cmd.ExecuteNonQuery();
                //con.Close();

                //populateExamCentres();
                //setMailStatus();

                //Log.createLogNow("Reset Password", "Reset Password of Exam Centre Code '" + eccode + "' for '" + ddlistEC.SelectedItem.Text + "' Exam", Convert.ToInt32(Session["ERID"].ToString()));

            }

        }

        private void updatePassMailSent(int ecid, string status)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEExaminationCentres_" + ddlistEC.SelectedItem.Value + " set PasswordMailSent=@PasswordMailSent where ECID='" + ecid + "'", con);
            cmd.Parameters.AddWithValue("@PasswordMailSent", status);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private string findMailBody(int ecid)
        {
            string eccode = FindInfo.findECCodeByECID(ecid, ddlistEC.SelectedItem.Value);
            string pass = FindInfo.findPasswordByECID(ecid, ddlistEC.SelectedItem.Value);


            StringBuilder mb = new StringBuilder();
            mb.Append("<div style='fontfamily:TimesNewRoman; fontsize:12px; color:#003f6f' >");
            mb.Append("Dear Sir/Madam <br/><br/> This is to inform you that we are sending you the user name and password for your examination centre login on website of DDE (SVSU), Meerut. Now onwards the question papers, for "+ddlistEC.SelectedItem.Text+" examination, will be uploaded to your login account. So please download question papers from your login account").AppendLine();
            mb.Append("<br/><br/><b>Guide lines for using Examination Center Login : </b><br/><br/>").AppendLine();
            mb.Append("<ol>");
            mb.Append("<li>Open our website <a href='http://www.subhartidde.com' >www.subhartidde.com</a></li>");
            mb.Append("<li>Now click on link 'Login' on top right most of website page</li>");
            mb.Append("<li>Fill your Username and Password which are given below in this email and click on Login button</li>");
            mb.Append("<li>If you are loging in first time after receiving your new password then change your password</li>");
            mb.Append("<li>Now you are ready to use your Examination Center Login.</li>");
            mb.Append("<li>Click on 'Download Question Papers'.</li>");
            mb.Append("<li>Fill the desired date on calendar and choose shift of exam and click on 'Search Papers'.</li>");
            mb.Append("<li>Now you will find a list of Question Papers.Click on 'Download' button for each question paper.</li>");
            mb.Append("</ol>");

            mb.Append("<br/><br/><b>Note : The Question Papers will be uploaded before half an hour from time of examination.</b>General instructions for examination are enclosed with this mail.");
            
            mb.Append("<br/><br/>Your Username and Password are as follow :");
            mb.Append("<br/><br/><b>Username : " + eccode + "<b/>");
            mb.Append("<br/><b>Password : " + pass + "<b/>");
            mb.Append("<br/><br/>Thank You <br/>DDE, SVSU, Meerut");
            mb.Append("<br/><br/>This is a auto generated mail.");

            //return mb.ToString();
            return "Please find the attached Question Papers";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            populateExamCentres();
            setMailStatus();

        }
    }
}