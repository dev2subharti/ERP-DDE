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

namespace DDE.Web.Admin
{
    public partial class ShowFeeRecord : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 57))
            {
                if (!IsPostBack)
                {
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
            SqlDataReader dr;

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FormNo");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("ReqFee");
            DataColumn dtcol9 = new DataColumn("LateFee");
            DataColumn dtcol10 = new DataColumn("PaidFee");
            DataColumn dtcol11 = new DataColumn("MOP");
            DataColumn dtcol12 = new DataColumn("DCNo");
            DataColumn dtcol13 = new DataColumn("TotalPaidFee");
            DataColumn dtcol14 = new DataColumn("DueFee");



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
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);

            con.Open();
            
            dr = cmd.ExecuteReader();
           
            int trf = 0, tlf = 0, tpf = 0, tdf = 0;

            while (dr.Read())
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


                if (ddlistRecordMode.SelectedItem.Text == "ALL")
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

                    string frdate = FindInfo.findFRDateBySRID(Convert.ToInt32(dr["SRID"]),1);

                    drow["ReqFee"] = Accounts.findRequiredFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), 0,dr["Session"].ToString(), frdate);
                    trf = trf + Convert.ToInt32(drow["ReqFee"]);

                    drow["LateFee"] = Accounts.findRequiredLateFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value));
                    tlf = tlf + Convert.ToInt32(drow["LateFee"]);

                    drow["PaidFee"] = "";
                    drow["MOP"] = "";
                    drow["DCNo"] = "";
                    if (ddlistFeeHead.SelectedItem.Value == "1")
                    {
                        drow["TotalPaidFee"] = (Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value) + Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 12, Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value));
                        tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                    }

                    else if (ddlistFeeHead.SelectedItem.Value == "2")
                    {
                        drow["TotalPaidFee"] = (Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value) + Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 24, Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value));
                        tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                    }

                    else
                    {
                        drow["TotalPaidFee"] = Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value);
                        tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                    }


                    drow["DueFee"] = ((Convert.ToInt32(drow["ReqFee"]) + Convert.ToInt32(drow["LateFee"])) - Convert.ToInt32(drow["TotalPaidFee"]));
                    tdf = tdf + Convert.ToInt32(drow["DueFee"]);

                    dt.Rows.Add(drow);

                }

                else if (ddlistRecordMode.SelectedItem.Text == "SUBMITTED")
                {
                    if (Accounts.isFeeRecordSubmitted(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), exam))
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

                        string frdate = FindInfo.findFRDateBySRID(Convert.ToInt32(dr["SRID"]), 1);

                        drow["ReqFee"] = Accounts.findRequiredFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), 0, dr["Session"].ToString(), frdate);
                        trf = trf + Convert.ToInt32(drow["ReqFee"]);

                        drow["LateFee"] = Accounts.findRequiredLateFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value));
                        tlf = tlf + Convert.ToInt32(drow["LateFee"]);

                        drow["PaidFee"] = "";
                        drow["MOP"] = "";
                        drow["DCNo"] = "";
                        if (ddlistFeeHead.SelectedItem.Value == "1")
                        {
                            drow["TotalPaidFee"] = (Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value) + Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 12, Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value));
                            tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                        }

                        else if (ddlistFeeHead.SelectedItem.Value == "2")
                        {
                            drow["TotalPaidFee"] = (Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value) + Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 24, Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value));
                            tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                        }

                        else
                        {
                            drow["TotalPaidFee"] = Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value);
                            tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                        }


                        drow["DueFee"] = ((Convert.ToInt32(drow["ReqFee"]) + Convert.ToInt32(drow["LateFee"])) - Convert.ToInt32(drow["TotalPaidFee"]));
                        tdf = tdf + Convert.ToInt32(drow["DueFee"]);

                        dt.Rows.Add(drow);
                    }
                }


                    else if (ddlistRecordMode.SelectedItem.Text == "VERIFIED")
                    {
                        if (Accounts.isFeeRecordVerified(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), exam))
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

                            string frdate = FindInfo.findFRDateBySRID(Convert.ToInt32(dr["SRID"]), 1);

                            drow["ReqFee"] = Accounts.findRequiredFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), 0, dr["Session"].ToString(),frdate);
                            trf = trf + Convert.ToInt32(drow["ReqFee"]);

                            drow["LateFee"] = Accounts.findRequiredLateFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value));
                            tlf = tlf + Convert.ToInt32(drow["LateFee"]);

                            drow["PaidFee"] = "";
                            drow["MOP"] = "";
                            drow["DCNo"] = "";
                            if (ddlistFeeHead.SelectedItem.Value == "1")
                            {
                                drow["TotalPaidFee"] = (Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value) + Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 12, Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value));
                                tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                            }

                            else if (ddlistFeeHead.SelectedItem.Value == "2")
                            {
                                drow["TotalPaidFee"] = (Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value) + Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), 24, Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value));
                                tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                            }

                            else
                            {
                                drow["TotalPaidFee"] = Accounts.findPreviousPaidFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["Course"]), Convert.ToInt32(ddlistFeeHead.SelectedItem.Value), Convert.ToInt32(ddlistYear.SelectedItem.Value), ddlistExamination.SelectedItem.Value);
                                tpf = tpf + Convert.ToInt32(drow["TotalPaidFee"]);
                            }


                            drow["DueFee"] = ((Convert.ToInt32(drow["ReqFee"]) + Convert.ToInt32(drow["LateFee"])) - Convert.ToInt32(drow["TotalPaidFee"]));
                            tdf = tdf + Convert.ToInt32(drow["DueFee"]);

                            dt.Rows.Add(drow);
                        }

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
                    pnlMSG.Visible = false;

                }

                else
                {
                    pnlRecord.Visible = false;
                    lblMSG.Text = "Sorry !! No record found";
                    pnlMSG.Visible = true;
                }

                lblTotalRF.Text = trf.ToString();
                lblTotalLF.Text = tlf.ToString();
                lblTotalFPF.Text = tpf.ToString();
                lblTotalDF.Text = tdf.ToString();
            
          
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
            setExamStatus();
            PopulateDStudent();
            PopulatePaymentDetails();
           

        }

        private void PopulatePaymentDetails()
        {
           
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {

                Label srid = (Label)dli.FindControl("lblSRID");
                DataList paydetail = (DataList)dli.FindControl("dtlistPayDetail");

                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("Select AcountSession from DDEAcountSession", con1);
                SqlDataReader dr1;
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    DataTable dt = new DataTable();

                    DataColumn dtcol1 = new DataColumn("PaidFee");
                    DataColumn dtcol2 = new DataColumn("MOP");
                    DataColumn dtcol3 = new DataColumn("URL");
                    DataColumn dtcol4 = new DataColumn("DCNo");



                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);
                    dt.Columns.Add(dtcol4);

                    fillPaymentDetails(Convert.ToInt32(srid.Text), dr1["AcountSession"].ToString(), dt);
                   

                    if (ddlistFeeHead.SelectedItem.Value == "1" )
                    {
                        fillLateFeeDetails(Convert.ToInt32(srid.Text),12,dr1["AcountSession"].ToString(), dt);

                    }

                    else if(ddlistFeeHead.SelectedItem.Value == "2")
                    {
                        fillLateFeeDetails(Convert.ToInt32(srid.Text), 24,dr1["AcountSession"].ToString(), dt);
                    }

                    paydetail.DataSource = dt;
                    paydetail.DataBind();

                }

                con1.Close();
            }

                       
        }

        private void fillLateFeeDetails(int srid, int fhid, string asession, DataTable dt)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("select PaymentMode,DCNumber,Amount from [DDEFeeRecord_" + asession + "] where SRID='" + srid + "' and FeeHead='" + fhid + "' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExamination.SelectedItem.Value + "'", con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["PaidFee"] = Convert.ToInt32(dr["Amount"]);
                    drow["MOP"] = Accounts.findMOPByID(Convert.ToInt32(dr["PaymentMode"]));
                    drow["URL"] = "ShowDCDetails.aspx?DCNo=" + Convert.ToString(dr["DCNumber"]);
                    drow["DCNo"] = Convert.ToString(dr["DCNumber"]);
                    dt.Rows.Add(drow);

                }
            }




            con.Close();
        }

        private void fillPaymentDetails(int srid,string asession, DataTable dt)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

            if (ddlistYear.SelectedItem.Value == "5")
            {
                cmd.CommandText = "select PaymentMode,DCNumber,Amount from [DDEFeeRecord_" + asession + "] where SRID='" + srid + "' and FeeHead='" + ddlistFeeHead.SelectedItem.Value + "' and ForExam='" + ddlistExamination.SelectedItem.Value + "'";
            }
            else
            {
                cmd.CommandText = "select PaymentMode,DCNumber,Amount from [DDEFeeRecord_" + asession + "] where SRID='" + srid + "' and FeeHead='" + ddlistFeeHead.SelectedItem.Value + "' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExamination.SelectedItem.Value + "'";
            }

            cmd.Connection = con;

            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["PaidFee"] = Convert.ToInt32(dr["Amount"]);
                    drow["MOP"] = Accounts.findMOPByID(Convert.ToInt32(dr["PaymentMode"]));
                    drow["URL"] = "ShowDCDetails.aspx?DCNo=" + Convert.ToString(dr["DCNumber"]);
                    drow["DCNo"] = Convert.ToString(dr["DCNumber"]);
                    dt.Rows.Add(drow);

                }
            }

            con.Close();
        }

        private void setExamStatus()
        {
            if (ddlistFeeHead.SelectedItem.Value == "2" || ddlistFeeHead.SelectedItem.Value == "3")
            {
                try
                {
                    if (ddlistExamination.Items.FindByText("NA").Selected)
                    {
                        ddlistExamination.Items.Remove("NA");
                    }

                   
                   
                }
                catch
                {
                    
                   
                }


            }

            else
            {
                ddlistExamination.SelectedItem.Text = "NA";
                ddlistExamination.SelectedItem.Value = "NA";
                

            }

            

        
           
        }

        protected void dtlistShowRegistration_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ENo")
            {
                Session["RecordType"] = "Show";
                Response.Redirect("DStudentRegistration.aspx?SRID=" +Convert.ToString(e.CommandArgument));
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

        protected void ddlistRecordMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
