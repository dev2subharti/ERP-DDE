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
using System.Net.Mail;
using System.Text;

namespace DDE.Web.Admin
{
    public partial class SendASMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 65))
            {

                if (!IsPostBack)
                {

                    PopulateDDList.populateBatch(ddlistBatch);
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
            ddlistDayFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDayTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateStudents();
        }

        private void populateStudents()
        {
           
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 11:59:59 PM";

            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

           
            cmd.CommandText = "select SCCode,Email from DDEStudyCentres order by SCCode";
           

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("EmailID");
            DataColumn dtcol4 = new DataColumn("TStudents");
            DataColumn dtcol5 = new DataColumn("Confirmed");
            DataColumn dtcol6 = new DataColumn("Pending");
            DataColumn dtcol7 = new DataColumn("SRIDS");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);


            int i = 1;
            int totalstudents = 0;

            string tsrid = findTSRID();

            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                drow["EmailID"] = Convert.ToString(dr["Email"]);

                int tstu, conf, pend;
                string fsrids;

                findStudents(ddlistBatch.SelectedItem.Text, dr["SCCode"].ToString(),from,to, out tstu,out conf,out pend, out fsrids, tsrid);

                drow["TStudents"] = tstu;
                drow["Confirmed"] = conf;
                drow["Pending"] = pend;
                drow["SRIDS"] = fsrids;
                if (tstu != 0)
                {
                    dt.Rows.Add(drow);
                    totalstudents = totalstudents + tstu;

                    i = i + 1;
                }

            }

            dtlistStudentList.DataSource = dt;
            dtlistStudentList.DataBind();

            con.Close();
            lblTotal.Text = "Total : " + totalstudents.ToString();

            if (i > 1)
            {

                pnlReport.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                pnlReport.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        private string findTSRID()
        {
            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand();

            if (ddlistBatch.SelectedItem.Text == "ALL")
            {
                cmd1.CommandText = "select SRID from DDEStudentRecord where SCStatus='T'";
            }
            else
            {
                cmd1.CommandText = "select SRID from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and SCStatus='T'";
            }

            cmd1.Connection = con1;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);

            string tsrid = "";

            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {                
                        if (tsrid == "")
                        {

                            tsrid = (ds1.Tables[0].Rows[i]["SRID"]).ToString();

                        }
                        else
                        {
                            tsrid = tsrid + "," + (ds1.Tables[0].Rows[i]["SRID"]).ToString();
                        }              

                }
            }

            return tsrid;
        }

        private void findStudents(string batch,string sccode,string from,string to, out int tstu, out int conf, out int pend, out string fsrids, string tsrid)
        {
            tstu = 0;
            conf = 0;
            pend = 0;
            fsrids = "";

            string srid = findAllSRID(batch, sccode, tsrid);

            if (srid != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand();

                if (ddlistYear.SelectedItem.Text == "ALL")
                {
                    cmd.CommandText = "select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srid + ") and FeeHead='1' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2'))a";
                }
                else
                {
                    cmd.CommandText = "select distinct SRID from (select distinct SRID from [DDEFeeRecord_2013-14] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2014-15] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2015-16] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2')  union select distinct SRID from [DDEFeeRecord_2016-17] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2017-18] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2018-19] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2019-20] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2') union select distinct SRID from [DDEFeeRecord_2020-21] where SRID in (" + srid + ") and FeeHead='1' and ForYear='" + ddlistYear.SelectedItem.Value + "' and CONVERT(datetime,TOFS,105)>='" + from + "' and CONVERT(datetime,TOFS,105)<='" + to + "' and (EntryType='1' or EntryType='2'))a";
                }

                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                tstu = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (fsrids == "")
                    {
                        fsrids = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }
                    else
                    {
                        fsrids = fsrids + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    }

                }

                conf = findTotalStudentByAdmissionStatus(fsrids, 1);
                pend = findTotalStudentByAdmissionStatus(fsrids, 2);
            }

            
            


        }

        private int findTotalStudentByAdmissionStatus(string fsrid, int astatus)
        {
            if (fsrid != "")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

                SqlCommand cmd = new SqlCommand();


                cmd.CommandText = "select SRID from DDEStudentRecord where SRID in (" + fsrid + ") and AdmissionStatus='" + astatus + "'";

                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);



                return ds.Tables[0].Rows.Count;
            }
            else return 0;
        }

        private string findAllSRID(string batch, string sccode, string tsrid)
        {
            string srid = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (batch == "ALL")
            {
                cmd.CommandText = "select SRID from DDEStudentRecord where (SCStatus='O' or SCStatus='C') and StudyCentreCode='" + sccode + "'";
            }
            else
            {
                cmd.CommandText = "select SRID from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and (SCStatus='O' or SCStatus='C') and StudyCentreCode='" + sccode + "'";
            }

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

           

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (srid == "")
                {
                    srid = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                }
                else
                {
                    srid = srid + "," + (ds.Tables[0].Rows[i]["SRID"]).ToString();
                }

            }


            if (tsrid != "")
            {

                SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd2 = new SqlCommand();


                cmd2.CommandText = "select distinct SRID from DDEChangeSCRecord where PreviousSC='" + sccode + "' and SRID in (" + tsrid + ")";



                cmd2.Connection = con2;
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);



                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {

                        if (srid == "")
                        {

                            srid = (ds2.Tables[0].Rows[i]["SRID"]).ToString();

                        }
                        else
                        {
                            srid = srid + "," + (ds2.Tables[0].Rows[i]["SRID"]).ToString();
                        }


                    }
                }
            }

            return srid;
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            string dfrom = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text;
            string dto = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text;

            foreach (DataListItem dli in dtlistStudentList.Items)
            {
                Label sccode = (Label)dli.FindControl("lblSCCode");
                Label emailid = (Label)dli.FindControl("lblEmailID");
                LinkButton srids = (LinkButton)dli.FindControl("lnkbtnTStudents");

                SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com");
                mySmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new
                System.Net.NetworkCredential("ddesvsu@gmail.com", "subhartidebsvsudec30");
                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress from = new MailAddress("ddesvsu@gmail.com", "DDE, SVSU Meerut");
                MailAddress to = new MailAddress(emailid.Text, "Study Centre : "+sccode.Text);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


                myMail.Subject = "Admission Status of '"+ddlistYear.SelectedItem.Text+"' Students of Study Centre Code : "+ sccode.Text;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;


                myMail.Body = findMailBody(srids.CommandArgument,dfrom,dto);
                myMail.BodyEncoding = System.Text.Encoding.UTF8;

                myMail.IsBodyHtml = true;

                mySmtpClient.Port = 587;
                mySmtpClient.EnableSsl = true;
                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                mySmtpClient.Send(myMail);

                pnlData.Visible = false;
                lblMSG.Text = "Mails has been sent successfully !!";
                pnlMSG.Visible = true;
            }
        }

        private string findMailBody(string srids, string from,string to)
        {
            string period;
            if (from == to)
            {
                period = "of "+DateTime.Now.ToString("dd MMMM yyyy") + " till " + DateTime.Now.ToString("hh:mm tt");
            }
            else
            {
                period = "From " + Convert.ToDateTime(from).ToString("dd MMMM yyyy") + " To " + Convert.ToDateTime(to).ToString("dd MMMM yyyy");
            }
            StringBuilder mb = new StringBuilder();
            mb.Append("<div style='fontfamily:TimesNewRoman; fontsize:12px; color:#003f6f' >");
            mb.Append("Good evening Sir/Madam, <br/><br/> This is to inform you that we have received the forms of students, sent by you to DDE, SVSU, Meerut. <br/>We are sending you the Admission Status of your students <br/><br/><u> <b>" + period + " </b></u><br/><br/>Please check the following list carefully and mail us on <b>ddesvsu@gmail.com</b> if you have any query or problem regarding the status of any student.").AppendLine();
            mb.Append("<br/><br/><b>The status of your "+ddlistYear.SelectedItem.Text+" students are as follow : </b><br/><br/>").AppendLine();
            mb.Append("<table border='1px' cellspacing='0px' cellpading='5px' >");
            mb.Append("<tr>");
            mb.Append("<td align='Center'><b>S.No.</b></td>");
            mb.Append("<td align='Center'><b>Enrollment No</b></td>");
            mb.Append("<td align='Center'><b>Batch</b></td>");
            mb.Append("<td align='Center'><b>Student Name</b></td>");
            mb.Append("<td align='Center'><b>Father's Name</b></td>");
            mb.Append("<td align='Center'><b>Course</b></td>");
            mb.Append("<td align='Center'><b>Admission<br/> Status</b></td>");
            mb.Append("<td align='Center'><b>Remark</b></td>");
            mb.Append("</tr>");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

            if (ddlistBatch.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,Fathername,Session,CYear,AdmissionStatus,ReasonIfPending from DDEStudentRecord where SRID in ("+srids+") and RecordStatus='True' order by EnrollmentNo";
            }
            else
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,Fathername,Session,CYear,AdmissionStatus,ReasonIfPending from DDEStudentRecord where SRID in (" + srids + ") and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True' order by EnrollmentNo";
            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                mb.Append("<tr>");
                mb.Append("<td>" + i.ToString() + "</td>");
                mb.Append("<td>"+dr["EnrollmentNo"].ToString()+"</td>");
                mb.Append("<td>" + dr["Session"].ToString() + "</td>");
                mb.Append("<td>" + dr["StudentName"].ToString() + "</td>");
                mb.Append("<td>" + dr["FatherName"].ToString() + "</td>");
                mb.Append("<td>" + FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["CYear"])) + "</td>");
                if (dr["AdmissionStatus"].ToString() == "1")
                {
                    mb.Append("<td>CONFIRMED</td>");
                    mb.Append("<td>NA</td>");
                   
                }
                else if (dr["AdmissionStatus"].ToString() == "2")
                {
                    mb.Append("<td>PENDING</td>");             
                    mb.Append("<td>" + dr["ReasonIfPending"].ToString() + "</td>");
                }
                else if (dr["AdmissionStatus"].ToString() == "3")
                {
                    mb.Append("<td>PROVISIONAL</td>");
                    mb.Append("<td>" + dr["ReasonIfPending"].ToString() + "</td>");
                }
              
                mb.Append("</tr>");
                i = i + 1;
            }


            con.Close();
            mb.Append("</table>").AppendLine();
            mb.Append("<br/><b>Thank You <br/>Admission Cell<br/> DDE, SVSU, Meerut</b>");
            mb.Append("<br/><br/><b>Disclaimer : The admission status of the student is provisional and final admission status is subject to the verification of fee,eligibility and supportive documents.</b>");
            mb.Append("</div>");
            

            return mb.ToString();
        }

        protected void ddlistBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlReport.Visible = false;
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlReport.Visible = false;
        }

        protected void dtlistStudentList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Session["SRIDS"] = e.CommandArgument;
            Response.Redirect("ShowMaildStudents.aspx");
        }
    }
}
