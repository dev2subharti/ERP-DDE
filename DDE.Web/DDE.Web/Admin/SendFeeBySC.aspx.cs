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

namespace DDE.Web.Admin
{
    public partial class SendFeeBySC : System.Web.UI.Page
    {
        int counter = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["LoginType"].ToString() == "16")
            {

                if (!IsPostBack)
                {
                    Random r = new Random();
                    Session["SessionID"] = r.Next();
                    Session["CurrentFH"] = "";
                    if (ddlistFeeHead.Items.Count == 1)
                    {
                        PopulateDDList.populateSTFeeHead(ddlistFeeHead);
                    }
                    PopulateDDList.populateExam(ddlistExamination);
                    PopulateDDList.populateBatch(ddlistBatch);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateCourses(ddlistCourse);
                    
                    ddlistSCCode.Items.FindByText(Session["SCCode"].ToString()).Selected = true;
                    ddlistSCCode.Enabled = false;
                   
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
           
            PopulateDStudent();
            setAmountStatus();

            foreach (DataListItem dli in dtlistShowStudents.Items)
            {

                Image img = (Image)dli.FindControl("imgPhoto");

                if (rblPhoto.SelectedItem.Text == "Without Photo")
                {
                    img.Visible = false;
                }

            }
            
        }

        private void setAmountStatus()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {

                TextBox amount = (TextBox)dli.FindControl("tbAmount");
                Label dueamount = (Label)dli.FindControl("lblDueAmount");

                if (dueamount.Text=="0")
                {
                    amount.Text = "PAID";
                    amount.Enabled = false;
                }

            }
        }


        private void PopulateDStudent()
        {
            string exam = "";

            if (ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3")
            {
                exam = ddlistExamination.SelectedItem.Value;
            }
            else
            {
                exam = "NA";
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand(findCommand(), con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("StudentPhoto");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("FatherName");
            DataColumn dtcol8 = new DataColumn("RequiredAmount");
            DataColumn dtcol9 = new DataColumn("PaidAmount");
            DataColumn dtcol10 = new DataColumn("DueAmount");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);


            while (dr.Read())
            {
                DataRow drow = dt.NewRow();

                drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                if (dr["EnrollmentNo"].ToString().Length == 10)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(5, 5);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 11)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 5);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 12)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 6);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 14)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(9, 5);
                }
                else
                {
                    drow["EC"] = "";
                }


                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);

                string frdate = FindInfo.findFRDateBySRID(Convert.ToInt32(dr["SRID"]), 1);

                drow["RequiredAmount"] = Accounts.findRequiredFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), 0, dr["Session"].ToString(), frdate);

                if (ddlistFeeHead.SelectedItem.Value == "3")
                {
                    drow["PaidAmount"] = "NA";
                }
                else
                {
                    drow["PaidAmount"] = Accounts.findPreviousPaidFeeVerified(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), exam);
                }

                if (ddlistFeeHead.SelectedItem.Value == "3")
                {
                    drow["DueAmount"] = "NA";
                }
                else
                {
                    drow["DueAmount"] = Accounts.findDueAmountVerified(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), exam, 0, dr["Session"].ToString(), frdate);
                }
               
                dt.Rows.Add(drow);

            }




            dt.DefaultView.Sort = "EC ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }


            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            con.Close();

            if (j > 1)
            {

                pnlStList.Visible = true;
                btnConfirm.Visible = true;
        
                pnlChart.Visible = false;
                pnlMSG.Visible = false;

            }

            else
            {
                btnConfirm.Visible = true;
                pnlChart.Visible = false;
                pnlStList.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        private string findCommand()
        {
            string cmnd = "";

                    if (rblAdmissionType.SelectedItem.Value == "0")
                    {                       
                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                          
                    }
                    else if (rblAdmissionType.SelectedItem.Value == "1")
                    {
                      
                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }

                    }
                        
                        

                    else if (rblAdmissionType.SelectedItem.Value == "2")
                    {
                       
                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
              
            }


            return cmnd;
        }


       

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            Session["TransactionAmount"] = lbltamount.Text;
            Response.Redirect("PayAmountBySC.aspx");
        }

        private void fillOfflineFeerecord()
        {
            int tid = Accounts.insertAndGetTransactionEntry(Session["SCCode"].ToString());
            Session["TID"] = tid;
            

            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                TextBox tbamt = (TextBox)dli.FindControl("tbAmount");
                Label srid = (Label)dli.FindControl("lblSRID");

                if (tbamt.Text != "" && tbamt.Text != "0" && tbamt.Text!="PAID")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into [DDEOLFeeRecord_2012-13] values(@SRID,@SessionID,@FeeHead,@PaymentMode,@DCNumber,@DCDate,@IBN,@Amount,@AmountInWords,@TotalDCAmount,@ForYear,@ForExam,@TOFS,@TNo,@TransactionStatus,@Verified)", con);


                    cmd.Parameters.AddWithValue("@SRID", srid.Text);
                    cmd.Parameters.AddWithValue("@SessionID", Convert.ToInt32(Session["SessionID"]));
                    cmd.Parameters.AddWithValue("@FeeHead", ddlistFeeHead.SelectedItem.Value);                    
                    cmd.Parameters.AddWithValue("@PaymentMode", 4);
                    cmd.Parameters.AddWithValue("@DCNumber", "");
                    cmd.Parameters.AddWithValue("@DCDate", "");
                    cmd.Parameters.AddWithValue("@IBN", "");
                    cmd.Parameters.AddWithValue("@Amount", tbamt.Text);
                    cmd.Parameters.AddWithValue("@AmountInWords", Accounts.IntegerToWords(Convert.ToInt32(tbamt.Text)).ToUpper());
                    cmd.Parameters.AddWithValue("@TotalDCAmount", "");
                    cmd.Parameters.AddWithValue("@ForYear", ddlistYear.SelectedItem.Value);

                    if (ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3")
                    {
                        cmd.Parameters.AddWithValue("@ForExam", ddlistExamination.SelectedItem.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ForExam", "NA");
                    }
                 
                    cmd.Parameters.AddWithValue("@TOFS", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@TNo", tid);
                    cmd.Parameters.AddWithValue("@TransactionStatus", "False");
                    cmd.Parameters.AddWithValue("@Verified", "False");

                  
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        private void unfreezeAmount()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                TextBox tbamt = (TextBox)dli.FindControl("tbAmount");               
                tbamt.Enabled = true;
            }

        }

        

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
          
            if (btnConfirm.Text == "Confirm")
            {
                fillOfflineFeerecord();
                fillChart();
                pnlChart.Visible = true;
                btnConfirm.Text = "Add More Fee";
                pnlTotal.Visible = true;
                btnSubmit.Visible = true;
                pnlFilters.Visible = false;
                btnSearch.Visible = false;
                pnlStList.Visible = false;
            }

           else  if (btnConfirm.Text =="Add More Fee")
            {
                pnlChart.Visible = false;
                btnConfirm.Visible = false;
                pnlTotal.Visible = false;
                btnSubmit.Visible =false;
                pnlFilters.Visible = true;
                btnSearch.Visible = true;
                btnConfirm.Text = "Confirm";
            }
            
            
       
        }

        private void fillChart()
        {
            int total = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select distinct FeeHead from [DDEOLFeeRecord_2012-13] where SessionID='" + Convert.ToInt32(Session["SessionID"]) + "' ", con);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FHID");
            DataColumn dtcol3 = new DataColumn("FeeHead");
            DataColumn dtcol4 = new DataColumn("Amount");
           
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);

            Session["FH1"] = "";
            Session["FH2"] = "";
            Session["FH3"] = "";
            Session["FH4"] = "";
            Session["FH5"] = "";
            Session["FH6"] = "";
            Session["FH7"] = "";
            Session["FH8"] = "";

            Session["Amount1"] = "";
            Session["Amount2"] = "";
            Session["Amount3"] = "";
            Session["Amount4"] = "";
            Session["Amount5"] = "";
            Session["Amount6"] = "";
            Session["Amount7"] = "";
            Session["Amount8"] = "";
       
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                string fh="FH"+i.ToString();
                string am = "Amount" + i.ToString();
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["FHID"] = Convert.ToInt32(dr["FeeHead"]);
                Session[fh]= drow["FeeHead"] = FindInfo.findFeeHeadNameByID(Convert.ToInt32(dr["FeeHead"]));
                Session[am]= drow["Amount"] = FindInfo.findTotalAmountBySessionID(Convert.ToInt32(Session["SessionID"]), Convert.ToInt32(dr["FeeHead"]));
                total = total + Convert.ToInt32(drow["Amount"]);
                i = i + 1;

                dt.Rows.Add(drow);
                    
            }

            dtlistChart.DataSource = dt;
            dtlistChart.DataBind();

            lbltamount.Text = total.ToString();
         

        }

        protected void ddlistFeeHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["cfh"] = ddlistFeeHead.SelectedItem.Text;
            if (ddlistFeeHead.SelectedItem.Value == "2")
            {
                lblExamination.Visible = true;
                ddlistExamination.Visible = true;
            }

            else if (ddlistFeeHead.SelectedItem.Value == "3")
            {
                lblExamination.Visible = true;
                ddlistExamination.Visible = true;
            }

            else
            {
                lblExamination.Visible = false;
                ddlistExamination.Visible = false;
            }
        }

        protected void btnAddMore_Click(object sender, EventArgs e)
        {
            pnlFilters.Visible = true;
            btnSearch.Visible = true;
            btnSubmit.Visible = false;
        }

       
        protected void dtlistChart_ItemCommand(object source, DataListCommandEventArgs e)
        {
            fillFeeInfo(Convert.ToInt32(e.CommandArgument));

        }

        private void fillFeeInfo(int fhid)
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                Label srid = (Label)dli.FindControl("lblSRID");
                TextBox tbamt = (TextBox)dli.FindControl("tbAmount");
                tbamt.Enabled = true;
                tbamt.Text = findPaidAmount(Convert.ToInt32(srid.Text), fhid, Convert.ToInt32(Session["SessionID"])).ToString();
            }
            ddlistFeeHead.SelectedItem.Selected = false;
            ddlistYear.SelectedItem.Selected = false;
            ddlistFeeHead.Items.FindByValue(fhid.ToString()).Selected = true;
            ddlistYear.Items.FindByValue("1").Selected = true;
            pnlFilters.Visible = true;
            pnlStList.Visible = true;
            btnSearch.Visible = true;
            pnlChart.Visible = false;
            pnlTotal.Visible = false;
            btnSubmit.Visible = false;
            btnConfirm.Visible = true;
            btnConfirm.Text = "Confirm";
           
        }

        private int findPaidAmount(int srid, int fhid, int sessionid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand scmd = new SqlCommand("Select Amount from [DDEOLFeeRecord_2012-13] where SessionID ='" + sessionid + "' and FeeHead='" + fhid + "' and SRID='" + srid + "' ", con);
            SqlDataReader dr;
            int amount=0;
            con.Open();
            dr = scmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                amount=Convert.ToInt32(dr[0]);
            }
            else
            {
                amount = 0;
            }
            con.Close();
            return amount;

        }

        

    }
}
