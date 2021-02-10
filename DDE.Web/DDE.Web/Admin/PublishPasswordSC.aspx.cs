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

namespace DDE.Web.Admin
{
    public partial class PublishPasswordSC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 39))
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
            foreach (DataListItem dli in dtlistShowStudyCentres.Items)
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

        private void populateStudyCentres()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudyCentres where Online='"+ddlistStatus.SelectedItem.Text+"' order by SCCode", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("Password");
            DataColumn dtcol5 = new DataColumn("SCName");
            DataColumn dtcol6 = new DataColumn("PassMailSent");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SCID"] = Convert.ToString(dr["SCID"]);
                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                if (dr["Password"].ToString().Length==6)
                {
                    drow["Password"] = Convert.ToString(dr["Password"]);
                }
                else
                {
                    drow["Password"] = "<b>Changed</b>";
                }
                drow["SCName"] = Convert.ToString(dr["Location"]);
                drow["PassMailSent"] = Convert.ToString(dr["PasswordMailSent"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowStudyCentres.DataSource = dt;
            dtlistShowStudyCentres.DataBind();

            con.Close();
        }

        protected void dtlistShowStudyCentres_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Send Mail")
            {

                string emailid = FindInfo.findEmailIDBySCID(Convert.ToInt32(e.CommandArgument));

                SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                mySmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("ddesvsu@gmail.com", "subhartidebsvsudec30");

                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress from = new MailAddress("ddesvsu@gmail.com", "DDE, SVSU Meerut");
                MailAddress to = new MailAddress(emailid, "Study Centre : " + e.CommandName);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                myMail.Subject = "Username and Password of Study Centre Login of SC Code : " + e.CommandName;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                myMail.Body = findMailBody(Convert.ToInt32(e.CommandArgument));
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                myMail.IsBodyHtml = true;

                mySmtpClient.Port = 587;
                mySmtpClient.EnableSsl = true;
                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                mySmtpClient.Send(myMail);



                updatePassMailSent(Convert.ToInt32(e.CommandArgument), "True");
                populateStudyCentres();
                setMailStatus();

                Log.createLogNow("Mail Sent", "Sent Password Mail to SCCode '"+FindInfo.findSCCodeByID(Convert.ToInt32(e.CommandArgument))+"'", Convert.ToInt32(Session["ERID"].ToString()));
            }
            else if (e.CommandName == "Reset Password")
            {
                Random rd = new Random();
                string password = rd.Next(100000,999999).ToString();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudyCentres set Password=@Password,NoTimesLoggedIn=@NoTimesLoggedIn,PasswordMailSent=@PasswordMailSent where SCID='" + e.CommandArgument + "'", con);
                
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@NoTimesLoggedIn", 0);
                cmd.Parameters.AddWithValue("@PasswordMailSent", "False");
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                populateStudyCentres();
                setMailStatus();

                Log.createLogNow("Reset Password", "Reset Password of SCCode '"+FindInfo.findSCCodeByID(Convert.ToInt32(e.CommandArgument))+"'", Convert.ToInt32(Session["ERID"].ToString()));

            }

        }

        private void updatePassMailSent(int scid, string status)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEStudyCentres set PasswordMailSent=@PasswordMailSent where SCID='"+scid+"'", con);
            cmd.Parameters.AddWithValue("@PasswordMailSent", status);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private string findMailBody(int scid)
        {
            string sccode = FindInfo.findSCCodeByID(scid);
            string pass = FindInfo.findPasswordBySCID(scid);
            

            StringBuilder mb = new StringBuilder();
            mb.Append("<div style='fontfamily:TimesNewRoman; fontsize:12px; color:#003f6f' >");
            mb.Append("Dear Sir/Madam, <br/><br/> This is to inform you that we have launched the Study Center Login. <br/>We are sending you the Username and Password of your login <br/><br/> Please use your Study Center Login as follow.").AppendLine();
            mb.Append("<br/><br/><b>Guide lines for using Study Center Login : </b><br/><br/>").AppendLine();
            mb.Append("<ol>");
            mb.Append("<li>Open our website <a href='http://www.subhartidde.com' >www.subhartidde.com</a></li>");
            mb.Append("<li>Now click on link 'Login' on top right most of website page</li>");
            mb.Append("<li>Now click on 'Study Centre' button</li>");
            mb.Append("<li>Fill your Username and Password which are given below in this email and click on Login button</li>");
            mb.Append("<li>If you are loging in first time after receiving your new password then change your password</li>");
            mb.Append("<li>Now you are ready to use your Study Center Login.</li>");
            mb.Append("</ol>");
            mb.Append("<br/><br/>Your Username and Password are as follow :");
            mb.Append("<br/><br/><b>Username : "+sccode+"<b/>");
            mb.Append("<br/><b>Password : " + pass + "<b/>");
            mb.Append("<br/><br/>Thank You <br/>DDE, SVSU, Meerut");
            mb.Append("<br/><br/>This is a auto generated mail.");

            return mb.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            populateStudyCentres();
            setMailStatus();          

        }



       
      
    }
}
