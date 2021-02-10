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
    public partial class ShowPaidFeeBySC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 57))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateAccountSession(ddlistASession);
                    ddlistASession.Items.FindByText("2012-13").Selected = true;
                    PopulateDDList.populateSTFeeHead(ddlistFeeHead);
                    PopulateDDList.populateExam(ddlistExamination);
                    PopulateDDList.populateBatch(ddlistBatch);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateCourses(ddlistCourse);


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


        private void PopulateDStudent()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand(findCommand(), con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FormNo");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("OFRID");
            DataColumn dtcol9 = new DataColumn("Amount");
            DataColumn dtcol10 = new DataColumn("TNo");
            DataColumn dtcol11 = new DataColumn("TRefNo");
            DataColumn dtcol12 = new DataColumn("Verified");
           

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
            dt.Columns.Add(dtcol11);
            dt.Columns.Add(dtcol12);

            int tar = 0;


            while (dr.Read())
            {
                int amount = 0;
                string  tno = "";
                string trefno = "";
                string verified = "";
                int ofrid = 0;
                if (Accounts.feePaidBySC(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value,"2012-13",out ofrid, out amount,out tno,out trefno, out verified))
                {
                    DataRow drow = dt.NewRow();

                    drow["FormNo"] = Convert.ToString(dr["ApplicationNo"]);
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
                    drow["Course"] = FindInfo.findCourseShortNameByID(Convert.ToInt32(dr["Course"]));
                    drow["OFRID"] = ofrid;
                    drow["Amount"] = amount.ToString();
                    int tid=Convert.ToInt32(tno);
                    drow["TNo"] =string.Format("{0:00000000}",tid); 
                    drow["TRefNo"] = trefno;
                    drow["Verified"] = verified;
                    tar = tar + Convert.ToInt32(drow["Amount"]);
                    dt.Rows.Add(drow);
                }

            }


            dt.DefaultView.Sort = "EC ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }

            dtlistShowRegistration.DataSource = dt;
            dtlistShowRegistration.DataBind();

            con.Close();

            if (j > 1)
            {

                pnlRecord.Visible = true;
                btnUpdate.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                pnlRecord.Visible = false;
                btnUpdate.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

            lblTotalAR.Text = tar.ToString();
           
        }

        private string findCommand()
        {
            string cmnd = "";

            if (rblAdmissionType.SelectedItem.Value == "0")
            {

                if (rblMode.SelectedItem.Value == "1")
                {

                    if (ddlistBatch.SelectedItem.Text == "ALL")
                    {
                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {

                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {

                                cmnd = "select * from DDEStudentRecord where RecordStatus='True'  order by EnrollmentNo";

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }


                    }
                }

                else if (rblMode.SelectedItem.Value == "2")
                {
                    string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                    string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                    if (ddlistBatch.SelectedItem.Text == "ALL")
                    {
                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {

                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }
                        else
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }
                        else
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }


                    }
                }
            }
            else if (rblAdmissionType.SelectedItem.Value == "1")
            {
                if (rblMode.SelectedItem.Value == "1")
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
                                    cmnd = "select * from DDEStudentRecord where  AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }


                    }
                }

                else if (rblMode.SelectedItem.Value == "2")
                {
                    string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                    string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                    if (ddlistBatch.SelectedItem.Text == "ALL")
                    {
                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {

                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }
                        else
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }
                        else
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }


                    }
                }

            }

            else if (rblAdmissionType.SelectedItem.Value == "2")
            {
                if (rblMode.SelectedItem.Value == "1")
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
                                    cmnd = "select * from DDEStudentRecord where  AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }


                    }
                }

                else if (rblMode.SelectedItem.Value == "2")
                {
                    string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                    string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                    if (ddlistBatch.SelectedItem.Text == "ALL")
                    {
                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {

                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }
                        else
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where  Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }
                        else
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }

                            else
                            {
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and  StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }


                    }
                }

            }


            return cmnd;
        }


        protected void btnFind_Click(object sender, EventArgs e)
        {
           
            PopulateDStudent();
            setVerificationStatus();

        }

        private void setVerificationStatus()
        {
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {
              
                CheckBox cbamt = (CheckBox)dli.FindControl("cbVerify");
                Label verified = (Label)dli.FindControl("lblVerify");

                if (verified.Text == "True")
                {
                    cbamt.Checked = true;
                }
                else
                {
                    cbamt.Checked = false;
                }

            }

        }


        protected void dtlistShowRegistration_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ENo")
            {
                Session["RecordType"] = "Show";
                Response.Redirect("DStudentRegistration.aspx?SRID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "DCNo")
            {
                Response.Redirect("ShowDCDetails.aspx?DCNo=" + Convert.ToString(e.CommandArgument));
            }



        }

        protected void dtlistdtlistPayDetail_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ENo")
            {
                Session["RecordType"] = "Show";
                Response.Redirect("DStudentRegistration.aspx?SRID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "DCNo")
            {
                Response.Redirect("ShowDCDetails.aspx?DCNo=" + Convert.ToString(e.CommandArgument));
            }



        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "1")
            {
                pnlDOA.Visible = false;
            }

            else if (rblMode.SelectedItem.Value == "2")
            {
                pnlDOA.Visible = true;
            }
        }

        protected void ddlistFeeHead_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3")
            {
                try
                {
                    if (ddlistExamination.Items.FindByText("NA").Selected)
                    {
                        ddlistExamination.Items.Remove("NA");
                    }


                    lblExamination.Visible = true;
                    ddlistExamination.Visible = true;
                }
                catch
                {

                    lblExamination.Visible = true;
                    ddlistExamination.Visible = true;
                }


            }

            else
            {
                ddlistExamination.SelectedItem.Text = "NA";
                ddlistExamination.SelectedItem.Value = "NA";
                lblExamination.Visible = false;
                ddlistExamination.Visible = false;

            }



        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string asession = ddlistASession.SelectedItem.Text;
            string exam = "";

            if (ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3")
            {
                exam = ddlistExamination.SelectedItem.Text;
            }
            else
            {
                exam = "NA";
            }
            

            foreach(DataListItem dli in dtlistShowRegistration.Items)
            {
                
                Label srid = (Label)dli.FindControl("lblSRID");
                Label ofrid = (Label)dli.FindControl("lblOFRID");
                CheckBox cbamt = (CheckBox)dli.FindControl("cbVerify");
                Label verified = (Label)dli.FindControl("lblVerify");

                if ((cbamt.Checked && verified.Text == "False") || (!cbamt.Checked && verified.Text == "True"))
                {
                    string status;

                    if (cbamt.Checked)
                    {
                        status = "True";
                    }
                    else 
                    {
                        status = "False";
                    }
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update [DDEOLFeeRecord_" + asession + "] set Verified=@Verified where OFRID='"+ofrid.Text+"'", con);
                    
                    cmd.Parameters.AddWithValue("@Verified", status);
                  
          
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();



                    Accounts.setOFFFeeRecordToONFeeRecord(Convert.ToInt32(ofrid.Text), ddlistASession.SelectedItem.Text,status);

                    if (cbamt.Checked)
                    {
                        Log.createLogNow("Verify Fee", "Verified '" + ddlistFeeHead.SelectedItem.Text + "' fee for Year '" + ddlistYear.SelectedItem.Value + "' and for Exam '" + exam + "' of a student with Enrollment No '" + FindInfo.findENoByID(Convert.ToInt32(srid.Text)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    }
                    else if (!cbamt.Checked)
                    {
                        Log.createLogNow("Verify Fee", "Disabled '" + ddlistFeeHead.SelectedItem.Text + "' fee for Year '" + ddlistYear.SelectedItem.Value + "' and for Exam '" + exam + "' of a student with Enrollment No '" + FindInfo.findENoByID(Convert.ToInt32(srid.Text)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    }
                    
                }

            }

            pnlData.Visible = false;
            lblMSG.Text = "Record has been updated successfully";
            pnlMSG.Visible = true;
        }
    }
}
