using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Net.Mime;

namespace DDE.Web.Admin
{
    public partial class SendQuestionPapers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 72))
            {
                if (!IsPostBack)
                {

                    setCurrentDate();

                }

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

        private void setCurrentDate()
        {
            ddlistDay.SelectedItem.Selected = false;
            ddlistMonth.SelectedItem.Selected = false;
            ddlistYear.SelectedItem.Selected = false;
            ddlistDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateDateSheet();
            populateSubjects();
          
        }

        private void populateExamCentres()
        {
            string date = ddlistYear.SelectedItem.Text + "-" + ddlistMonth.SelectedItem.Value + "-" + ddlistDay.SelectedItem.Text;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_B15 order by ExamCentreCode", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            DataColumn dtcol = new DataColumn("SNo");
            DataColumn dtcol1 = new DataColumn("ECID");
            DataColumn dtcol2 = new DataColumn("ECCode");
            DataColumn dtcol4 = new DataColumn("ECName");
            DataColumn dtcol5 = new DataColumn("Email");
            DataColumn dtcol6 = new DataColumn("QP");


            dt.Columns.Add(dtcol);
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);

            int j = 1;

            string selected = findSelectedPapers();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = j;
                drow["ECID"] = ds.Tables[0].Rows[i]["ECID"].ToString();
                drow["ECCode"] = ds.Tables[0].Rows[i]["ExamCentreCode"].ToString();
                drow["ECName"] = ds.Tables[0].Rows[i]["CentreName"].ToString();
                drow["Email"] = ds.Tables[0].Rows[i]["Email"].ToString();
                string validpc;
                drow["QP"] = FindInfo.findQPOfTheDay("B15",selected, Convert.ToInt32(drow["ECID"]), date, out validpc);
                if (drow["QP"].ToString() != "NF" && drow["QP"].ToString() != "")
                {
                    dt.Rows.Add(drow);
                    j++;
                }
                
            }

            dtlistEC.DataSource = dt;
            dtlistEC.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistEC.Visible = true;
                btnSendMail.Visible = true;
                btnSelectAll.Visible = true;
                btnClearAll.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistEC.Visible = false;
                btnSendMail.Visible = false;
                btnSelectAll.Visible = false;
                btnClearAll.Visible = false;
                lblMSG.Text = "Sorry !! No Examination Centre Found.";
                pnlMSG.Visible = true;
            }
        }

        private string findSelectedPapers()
        {
            string selected = "";
            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                foreach (DataListItem dl in subjects.Items)
                {
                    Label pc = (Label)dl.FindControl("lblPaperCode");
                    CheckBox sel = (CheckBox)dl.FindControl("cbSelect");

                    if (sel.Checked)
                    {
                        if (selected == "")
                        {
                            selected = pc.Text;
                        }
                        else
                        {
                            selected =selected+","+ pc.Text;
                        }
                    }
                }

            }
            return selected;
        }

       

        private void populateSubjects()
        {
            string timefrom = ddlistHourFrom.SelectedItem.Text + ":" + ddlistMinutesFrom.SelectedItem.Text + " " + ddlistSectionFrom.SelectedItem.Value;
            string timeto = ddlistHourTo.SelectedItem.Text + ":" + ddlistMinutesTo.SelectedItem.Text + " " + ddlistSectionTo.SelectedItem.Value;
            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                Label date = (Label)dli.FindControl("lblDate");

                string fd = Convert.ToInt32(date.Text.Substring(3, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(0, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(6, 4)).ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where ExaminationCode='B15' and Date='" + Convert.ToDateTime(fd).ToString("yyyy-MM-dd") + "' and TimeFrom='" + timefrom + "' and TimeTo='" + timeto + "' order by Year", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("DSID");
                DataColumn dtcol2 = new DataColumn("Time");             
                DataColumn dtcol4 = new DataColumn("SubjectCode");
                DataColumn dtcol5 = new DataColumn("PaperCode");
                DataColumn dtcol6 = new DataColumn("SubjectName");
                DataColumn dtcol7 = new DataColumn("Year");
                DataColumn dtcol8 = new DataColumn("QPFileName");
                DataColumn dtcol9 = new DataColumn("QPFileURL");
               

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);           
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);
                dt.Columns.Add(dtcol8);
                dt.Columns.Add(dtcol9);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["DSID"] = ds.Tables[0].Rows[i]["DSID"].ToString();
                    drow["Time"] = ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(6, 1)) + " - " + ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(6, 1));
                    drow["SubjectCode"] = FindInfo.findSubjectCodesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    drow["PaperCode"] = ds.Tables[0].Rows[i]["PaperCode"].ToString();
                    drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString(), ds.Tables[0].Rows[i]["SyllabusSession"].ToString());
                    drow["Year"] = ds.Tables[0].Rows[i]["Year"].ToString();
                    drow["QPFileName"] = ds.Tables[0].Rows[i]["QPFileName"].ToString();
                    drow["QPFileURL"] = "QuestionPapers/B15/" + ds.Tables[0].Rows[i]["QPFileName"].ToString();
           
                    if (!alreadyexits(drow["PaperCode"].ToString(), drow["SubjectName"].ToString(), dt))
                    {
                        dt.Rows.Add(drow);
                    }
                }

                subjects.DataSource = dt;
                subjects.DataBind();



            }
        }

        private bool alreadyexits(string pc, string sn, DataTable dt)
        {
            bool exist = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["PaperCode"].ToString() == pc && row["SubjectName"].ToString() == sn)
                {
                    exist = true;
                    break;
                }
            }

            return exist;
        }

        private string findSection(string sec)
        {
            if (sec == "1")
            {
                return "AM";
            }
            else if (sec == "2")
            {
                return "PM";
            }
            else if (sec == "3")
            {
                return "NOON";
            }
            else
            {
                return "NF";
            }
        }

        private void populateDateSheet()
        {
            string date = ddlistYear.SelectedItem.Text + "-" + ddlistMonth.SelectedItem.Value + "-" + ddlistDay.SelectedItem.Text;
            string timefrom = ddlistHourFrom.SelectedItem.Text + ":" + ddlistMinutesFrom.SelectedItem.Text + " " + ddlistSectionFrom.SelectedItem.Value;
            string timeto = ddlistHourTo.SelectedItem.Text + ":" + ddlistMinutesTo.SelectedItem.Text + " " + ddlistSectionTo.SelectedItem.Value;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where ExaminationCode='B15' and Date='" + date + "' and TimeFrom='" + timefrom + "' and TimeTo='" + timeto + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("Date");
          

            dt.Columns.Add(dtcol1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drow = dt.NewRow();
                drow["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"]).ToString("dd-MM-yyyy");
                dt.Rows.Add(drow);
            }

            dtlistShowDS.DataSource = dt;
            dtlistShowDS.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowDS.Visible = true;
                btnSearchEC.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowDS.Visible = false;
                btnSearchEC.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }


        }

       
        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            btnOK.Visible = false;
            dtlistShowDS.Visible = true;
          
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            string date = ddlistDay.SelectedItem.Text+" "+ddlistMonth.SelectedItem.Text+" "+ddlistYear.SelectedItem.Text;

            int j = 1;

            foreach (DataListItem dli in dtlistEC.Items)
            {
                Label eccode = (Label)dli.FindControl("lblECCode");
                Label emailid = (Label)dli.FindControl("lblEmail");
                Label qp = (Label)dli.FindControl("lblQP");
                CheckBox selected = (CheckBox)dli.FindControl("cbESelect");

                if (selected.Checked)
                {

                    if (qp.Text != "NF" && qp.Text != "")
                    {

                        if (j >= 1 && j <= 50)
                        {
                            SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                            mySmtpClient.UseDefaultCredentials = false;

                            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("academic.dde@gmail.com","svsudeb32");
                            MailAddress from = new MailAddress("academic.dde@gmail.com", "DDE, SVSU Meerut");


                            mySmtpClient.Credentials = basicAuthenticationInfo;
                            MailAddress to = new MailAddress(emailid.Text, "Examination Centre : " + eccode.Text);
                            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                            myMail.Subject = "EC Code-'" + eccode.Text + "' Question Papers for '" + date + "' for December 2015 Examination";
                            myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                            myMail.Body = "Please find the attached Question Papers";
                            myMail.BodyEncoding = System.Text.Encoding.UTF8;

                            string[] at = qp.Text.Split(',');

                            for (int i = 0; i < at.Length; i++)
                            {
                                string file = Server.MapPath("QuestionPapers/B15/" + at[i]);
                                Attachment a = new Attachment(file, MediaTypeNames.Application.Octet);
                                myMail.Attachments.Add(a);
                            }

                            myMail.IsBodyHtml = true;

                            mySmtpClient.Port = 587;
                            mySmtpClient.EnableSsl = true;
                            mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                            mySmtpClient.Send(myMail);
                        }
                        else if (j >= 51 && j <= 100)
                        {
                            SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                            mySmtpClient.UseDefaultCredentials = false; 

                            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("confidential.dde@gmail.com","32svsudeb");
                            MailAddress from = new MailAddress("confidential.dde@gmail.com", "DDE, SVSU Meerut");


                            mySmtpClient.Credentials = basicAuthenticationInfo;
                            MailAddress to = new MailAddress(emailid.Text, "Examination Centre : " + eccode.Text);
                            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                            myMail.Subject = "EC Code-'" + eccode.Text + "' Question Papers for '" + date + "' for December 2015 Examination";
                            myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                            myMail.Body = "Please find the attached Question Papers";
                            myMail.BodyEncoding = System.Text.Encoding.UTF8;

                            string[] at = qp.Text.Split(',');

                            for (int i = 0; i < at.Length; i++)
                            {
                                string file = Server.MapPath("QuestionPapers/B15/" + at[i]);
                                Attachment a = new Attachment(file, MediaTypeNames.Application.Octet);
                                myMail.Attachments.Add(a);
                            }

                            myMail.IsBodyHtml = true;

                            mySmtpClient.Port = 587;
                            mySmtpClient.EnableSsl = true;
                            mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                            mySmtpClient.Send(myMail);
                        }
                        else if (j >= 101 && j <= 150)
                        {
                            SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                            mySmtpClient.UseDefaultCredentials = false;

                            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("examcell.dde@gmail.com","distanceeducation");
                            MailAddress from = new MailAddress("examcell.dde@gmail.com", "DDE, SVSU Meerut");


                            mySmtpClient.Credentials = basicAuthenticationInfo;
                            MailAddress to = new MailAddress(emailid.Text, "Examination Centre : " + eccode.Text);
                            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                            myMail.Subject = "EC Code-'" + eccode.Text + "' Question Papers for '" + date + "' for December 2015 Examination";
                            myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                            myMail.Body = "Please find the attached Question Papers";
                            myMail.BodyEncoding = System.Text.Encoding.UTF8;

                            string[] at = qp.Text.Split(',');

                            for (int i = 0; i < at.Length; i++)
                            {
                                string file = Server.MapPath("QuestionPapers/B15/" + at[i]);
                                Attachment a = new Attachment(file, MediaTypeNames.Application.Octet);
                                myMail.Attachments.Add(a);
                            }

                            myMail.IsBodyHtml = true;

                            mySmtpClient.Port = 587;
                            mySmtpClient.EnableSsl = true;
                            mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                            mySmtpClient.Send(myMail);
                        }
                        else if (j >= 151)
                        {
                            SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                            mySmtpClient.UseDefaultCredentials = false;

                            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("ddesvsu@gmail.com", "subhartidebsvsudec30");
                            MailAddress from = new MailAddress("ddesvsu@gmail.com", "DDE, SVSU Meerut");


                            mySmtpClient.Credentials = basicAuthenticationInfo;
                            MailAddress to = new MailAddress(emailid.Text, "Examination Centre : " + eccode.Text);
                            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                            myMail.Subject = "EC Code-'" + eccode.Text + "' Question Papers for '" + date + "' for December 2015 Examination";
                            myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                            myMail.Body = "Please find the attached Question Papers";
                            myMail.BodyEncoding = System.Text.Encoding.UTF8;

                            string[] at = qp.Text.Split(',');

                            for (int i = 0; i < at.Length; i++)
                            {
                                string file = Server.MapPath("QuestionPapers/B15/" + at[i]);
                                Attachment a = new Attachment(file, MediaTypeNames.Application.Octet);
                                myMail.Attachments.Add(a);
                            }

                            myMail.IsBodyHtml = true;

                            mySmtpClient.Port = 587;
                            mySmtpClient.EnableSsl = true;
                            mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                            mySmtpClient.Send(myMail);
                        }


                        j = j + 1;
                       
                      
                    }
                }

               
            }

            pnlData.Visible = false;
            lblMSG.Text = "Question Papers has been sent by mails successfully !!";
            pnlMSG.Visible = true;
        }

        protected void btnSearchEC_Click(object sender, EventArgs e)
        {
            populateExamCentres();
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistEC.Items)
            {
               
                CheckBox selected = (CheckBox)dli.FindControl("cbESelect");

                selected.Checked = true;

            }
        }

        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistEC.Items)
            {

                CheckBox selected = (CheckBox)dli.FindControl("cbESelect");

                selected.Checked = false;

            }
        }
    }
}