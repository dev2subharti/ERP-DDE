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
    public partial class ShowReappearExamReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 64))
            {

                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("A14").Selected = true;

                    pnlData.Visible = true;
                    pnlMSG.Visible = false;
                }


            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;

            }



        }

        private void populateRecord()
        {           
            if (ddlistExam.SelectedItem.Value == "A14")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select distinct SRID from ( select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='26' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead='26' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead='26' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead='26' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead='26' and ForExam='A14')a", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();


                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("Enrollment No.");
                DataColumn dtcol3 = new DataColumn("Roll No.");
                DataColumn dtcol4 = new DataColumn("Student Name");
                DataColumn dtcol5 = new DataColumn("Father's Name");
                DataColumn dtcol6 = new DataColumn("SC Code");
                DataColumn dtcol7 = new DataColumn("Batch");
                DataColumn dtcol8 = new DataColumn("Course");
                DataColumn dtcol9 = new DataColumn("Ad. Year Paid");
                DataColumn dtcol10 = new DataColumn("Cont. Year Paid");
                DataColumn dtcol11 = new DataColumn("Exam Year Paid");
                DataColumn dtcol12 = new DataColumn("Eligible For Exam");
                DataColumn dtcol13 = new DataColumn("Exam City");
                DataColumn dtcol14 = new DataColumn("Exam Centre");
                DataColumn dtcol15 = new DataColumn("Exam Code");
                //DataColumn dtcol16 = new DataColumn("Form Counter");

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
                dt.Columns.Add(dtcol15);
                //dt.Columns.Add(dtcol16);

                int i = 1;
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;

                    if (populateStudentinfo(Convert.ToInt32(dr["SRID"]), drow))
                    {

                        drow["Roll No."] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, "R");

                        drow["Ad. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(dr["SRID"]), 1, "A14");
                        drow["Cont. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(dr["SRID"]), 5, "NA");
                        drow["Exam Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(dr["SRID"]), 26, ddlistExam.SelectedItem.Value);

                        if (((drow["Ad. Year Paid"].ToString() != "NP" && drow["Exam Year Paid"].ToString() != "NP")) || ((drow["Cont. Year Paid"].ToString() != "NP" && drow["Exam Year Paid"].ToString() != "NP") && (drow["Cont. Year Paid"].ToString() == drow["Exam Year Paid"].ToString())))
                        {
                            if (((drow["Cont. Year Paid"].ToString() != "NP" && drow["Exam Year Paid"].ToString() != "NP") && (drow["Cont. Year Paid"].ToString() == drow["Exam Year Paid"].ToString())))
                            {
                                drow["Eligible For Exam"] = "YES";
                            }
                            else
                            {
                                if (drow["Ad. Year Paid"].ToString() == drow["Exam Year Paid"].ToString())
                                {
                                    drow["Eligible For Exam"] = "YES";
                                }
                                else
                                {
                                    string[] ayear = drow["Ad. Year Paid"].ToString().Split(',');
                                    string[] eyear = drow["Exam Year Paid"].ToString().Split(',');

                                    int pos = Array.IndexOf(ayear, eyear[0]);
                                    if (pos > -1)
                                    {
                                        drow["Eligible For Exam"] = "YES";
                                    }
                                    else
                                    {
                                        drow["Eligible For Exam"] = "NO";
                                    }
                                }
                            }

                        }
                        else
                        {
                            drow["Eligible For Exam"] = "NO";
                        }

                        populateExamCentreRecord(Convert.ToInt32(dr["SRID"]), "A14", drow);



                        //if (drow["Exam Year Paid"].ToString() != "NP" && drow["Exam Year Paid"].ToString().Length==1)
                        //{
                        //    drow["Form Counter"] = FindInfo.findFormCounter(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(drow["Exam Year Paid"]), ddlistExam.SelectedItem.Value, "R");
                        //}
                        //else
                        //{
                        //    drow["Form Counter"] = "NA";
                        //}

                        dt.Rows.Add(drow);
                        i = i + 1;
                    }
                }

                gvShowStudent.DataSource = dt;
                gvShowStudent.DataBind();

                con.Close();

                if (i > 1)
                {
                    gvShowStudent.Visible = true;
                    btnExport.Visible = true;
                    pnlMSG.Visible = false;

                }
                else
                {
                    gvShowStudent.Visible = false;
                    btnExport.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found";
                    pnlMSG.Visible = true;
                }
            }
            else
            {
                gvShowStudent.Visible = false;
                btnExport.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
            }
        }

        private void populateExamCentreRecord(int srid, string exam, DataRow drow)
        {
            int ecid = FindInfo.findECIDBySRID(srid, exam);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_" + exam + " where ECID='" + ecid + "'", con);
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    drow["Exam City"] = Convert.ToString(dr["City"]);
                    drow["Exam Centre"] = Convert.ToString(dr["CentreName"]);
                    drow["Exam Code"] = Convert.ToString(dr["ExamCentreCode"]);

                }

            }
            else
            {
                drow["Exam City"] = "NOT SET";
                drow["Exam Centre"] = "NOT SET";
                drow["Exam Code"] = "NOT SET";
            }

            con.Close();


        }



        private void findExamCentreRecord(string sccode, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres1", con);
            con.Open();
            SqlDataReader dr;
            string[] sc = { };
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sc = dr["SCCodes"].ToString().Split(',');
                    for (int i = 0; i < sc.Length; i++)
                    {
                        if (sc[i].ToString() == sccode)
                        {
                            drow["Exam City"] = Convert.ToString(dr["City"]);
                            drow["Exam Centre"] = Convert.ToString(dr["CentreName"]);
                            drow["Exam Code"] = Convert.ToString(dr["ExamCentreCode"]);
                        }
                    }
                }

            }

            con.Close();

        }



        private bool populateStudentinfo(int srid, DataRow drow)
        {
            bool rstatus = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["RecordStatus"].ToString() == "True")
                {
                    rstatus = true;
                    drow["Enrollment No."] = Convert.ToString(dr["EnrollmentNo"]);
                    drow["Student Name"] = Convert.ToString(dr["StudentName"]);
                    drow["Father's Name"] = Convert.ToString(dr["FatherName"]);
                    drow["SC Code"] =FindInfo.findSCCodeForAdmitCardBySRID(srid);
                    drow["Batch"] = Convert.ToString(dr["Session"]);
                    drow["Course"] = FindInfo.findCourseShortNameBySRID(srid, Convert.ToInt32(dr["CYear"]));
                }

            }

            con.Close();
            return rstatus;
        }

       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateRecord();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=ExamData_Reappear_Till_" + DateTime.Now.ToString("dd-MM-yyyy_(hh:mm:ss:tt)") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            gvShowStudent.RenderBeginTag(hw);
            gvShowStudent.HeaderRow.RenderControl(hw);
            foreach (GridViewRow row in gvShowStudent.Rows)
            {
                row.RenderControl(hw);
            }
            gvShowStudent.FooterRow.RenderControl(hw);
            gvShowStudent.RenderEndTag(hw);

            //gvShowStudent.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}