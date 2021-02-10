using DDE.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDE.Web.Admin
{
    public partial class SendSMSToStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateBatch(ddlistBatch);

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

      

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateStudent();
        }

        private void PopulateStudent()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand(findCommand(), con);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("OANo");
            DataColumn dtcol4 = new DataColumn("ENo");
            DataColumn dtcol5 = new DataColumn("SubCenterCode");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("FatherName");
            DataColumn dtcol8 = new DataColumn("Course");
            DataColumn dtcol9 = new DataColumn("MNo");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i + 1;
                drow["SRID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]);
                drow["OANo"] = Convert.ToString(ds.Tables[0].Rows[i]["PSRID"]);
                drow["ENo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                drow["SubCenterCode"] = Convert.ToString(ds.Tables[0].Rows[i]["StudyCentreCode"]);
                drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                drow["Course"] = Convert.ToString(ds.Tables[0].Rows[i]["CourseShortName"]);
                drow["MNo"] = Convert.ToString(ds.Tables[0].Rows[i]["MobileNo"]);
                dt.Rows.Add(drow);

            }

            dtlistShowRegistration.DataSource = dt;
            dtlistShowRegistration.DataBind();



            if (ds.Tables[0].Rows.Count > 0)
            {
                int smsbalance;
                string senderid = FindInfo.findSMSSenderID(out smsbalance);
                lblSMSBalance.Text = smsbalance.ToString();
                
                pnlStudentList.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {

                pnlStudentList.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        private string findCommand()
        {
            string cmnd = "";
          
            
                    
            cmnd = "select SRID,PSRID,EnrollmentNo,StudentName,FatherName,StudyCentreCode,DDECourse.CourseShortName,MobileNo from DDEStudentRecord inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where [Session]='" + ddlistBatch.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "' order by EnrollmentNo";
                    
           
            return cmnd;
        }



     

        protected void ddlistBatch_SelectedIndexChanged(object sender, EventArgs e)
        {

            pnlStudentList.Visible = false;
        }

     

      
        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlStudentList.Visible = false;

        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            int counter;
            string mno = findTotalMNo(out counter);

            int totalmsg = findTotalMsg(counter);

            if (counter > 0)
            {
                int smsbalance;
                string senderid = FindInfo.findSMSSenderID(out smsbalance);

                if (counter <= smsbalance)
                {
                    if (senderid != "NF")
                    {
                        string message = "";

                        if (rblMsgType.SelectedItem.Value == "unicode")
                        {
                            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                            var utf16Bytes = Encoding.Unicode.GetBytes(tbMSGH.Text);
                            var utf8Bytes = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, utf16Bytes);
                            var utf8string = iso_8859_1.GetString(utf8Bytes);
                            message = System.Web.HttpUtility.UrlEncode(utf8string, Encoding.UTF8);
                        }
                        else
                        {
                            message = HttpUtility.UrlPathEncode(tbMSG.Text);
                        }

                        string result = sendMessage(senderid, mno, message);

                        if (result.Substring(0, 3) == "SMS")
                        {
                            int i = FindInfo.updateSMSCounter((smsbalance - totalmsg), Convert.ToInt32(Session["ClientID"]));
                            if (i > 0)
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = totalmsg + " SMS have been sent successfully !!";
                                pnlMSG.Visible = true;
                            }
                            else
                            {
                                pnlData.Visible = false;
                                lblMSG.Text = totalmsg + " SMS have been sent successfully but SMS balance could not be updated. Please contact Admin.";
                                pnlMSG.Visible = true;
                            }


                        }
                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = result;
                            pnlMSG.Visible = true;
                        }
                    }
                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry!! Sender ID could not be found. Please contact to DIPL Admin";
                        pnlMSG.Visible = true;

                    }
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! You have selected '" + counter + "' Mobile No's while your SMS Balance is '" + smsbalance + "'";
                    pnlMSG.Visible = true;

                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry!! You did not select any Mobile No.";
                pnlMSG.Visible = true;

            }

        }

        private int findTotalMsg(int counter)
        {
            int totalmsg = 0;
            int msglength = 0;

            if (rblMsgType.SelectedItem.Value == "unicode")
            {
                msglength = tbMSGH.Text.Length;
            }
            else if (rblMsgType.SelectedItem.Value == "text")
            {
                msglength = tbMSG.Text.Length;
            }

            if (msglength >= 0 && msglength <= 160)
            {
                totalmsg = counter;
            }
            else if (msglength >= 161 && msglength <= 320)
            {
                totalmsg = 2 * counter;
            }
            else if (msglength >= 321 && msglength <= 480)
            {
                totalmsg = 3 * counter;
            }
            else if (msglength >= 481 && msglength <= 640)
            {
                totalmsg = 4 * counter;
            }
            else if (msglength >= 641)
            {
                totalmsg = 5 * counter;
            }

            return totalmsg;
        }
        private string findTotalMNo(out int counter)
        {
            string mno = "";
            counter = 0;
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {

                Label lblmno = (Label)dli.FindControl("lblMNo");
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");

                if (cb.Checked == true)
                {

                    if (lblmno.Text != "")
                    {
                        if (mno == "")
                        {
                            mno = lblmno.Text;
                            counter = counter + 1;
                        }
                        else
                        {
                            mno = mno + "," + lblmno.Text;
                            counter = counter + 1;
                        }
                    }

                }

            }

            return mno;



        }

        public string sendMessage(string senderid, string phoneNo, string message)
        {

            string url = "http://sms.textmysms.com/app/smsapi/index.php";

            string result = "";


            String strPost = "?key=" + HttpUtility.UrlPathEncode("35D3D8307B2F57") + "&campaign=0&routeid=18&type=" + rblMsgType.SelectedItem.Value + "&contacts=" + HttpUtility.UrlPathEncode(phoneNo) + "&senderid=" + HttpUtility.UrlPathEncode(senderid) + "&msg=" + message;

            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url + strPost);
            objRequest.ProtocolVersion = HttpVersion.Version10;
            objRequest.Method = "POST";
            objRequest.ContentLength = Encoding.UTF8.GetByteCount(strPost);
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                // Close and clean up the StreamReader sr.Close();
            }
            return result;
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");
                cb.Checked = true;
            }
        }

        protected void rblMsgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMsgType.SelectedItem.Value == "text")
            {
                tbMSG.Visible = true;
                lblDisplay.Visible = true;
                tbMSGH.Visible = false;
                lblDisplayH.Visible = false;
            }
            else if (rblMsgType.SelectedItem.Value == "unicode")
            {
                tbMSG.Visible = false;
                lblDisplay.Visible = false;
                tbMSGH.Visible = true;
                lblDisplayH.Visible = true;
            }
        }

       
    }
}