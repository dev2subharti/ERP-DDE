using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;


namespace DDE.Web.Admin
{
    public partial class ShowBPExamReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 64))
            {

                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 64))
                {

                    if (!IsPostBack)
                    {


                        PopulateDDList.populateExam(ddlistExam);
                        ddlistExam.Items.FindByValue("Z11").Selected = true;

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



        }

        private void populateRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_"+ddlistExam.SelectedItem.Value+" where MOE='B'", con);
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
            DataColumn dtcol9 = new DataColumn("BP Subjects");
            DataColumn dtcol10 = new DataColumn("Exam Year Paid");
            DataColumn dtcol11 = new DataColumn("Amount");
            DataColumn dtcol12 = new DataColumn("Exam City");
            DataColumn dtcol13 = new DataColumn("Exam Centre");
            DataColumn dtcol14 = new DataColumn("Exam Code");


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

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                populateStudentinfo(Convert.ToInt32(dr["SRID"]), drow);
                drow["Roll No."] = dr["RollNo"].ToString();
                
                drow["Amount"] = findAmountPaid(Convert.ToInt32(dr["SRID"]));

                drow["BP Subjects"] = "";
                drow["Exam Year Paid"] = "";

                if ((dr["BPSubjects1"].ToString() != "") || (dr["BPPracticals1"].ToString() != ""))
                {
                    if ((dr["BPSubjects1"].ToString() != ""))
                    {
                        drow["BP Subjects"] = findSubjects(dr["BPSubjects1"].ToString());
                    }
                    if ((dr["BPPracticals1"].ToString() != ""))
                    {
                        drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(dr["BPPracticals1"].ToString());
                    }

                    drow["Exam Year Paid"] = 1;
                }
                if ((dr["BPSubjects2"].ToString() != "") || (dr["BPPracticals2"].ToString() != ""))
                {
                    if (drow["BP Subjects"].ToString() != "")
                    {
                        if ((dr["BPSubjects2"].ToString() != ""))
                        {
                            drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findSubjects(dr["BPSubjects2"].ToString());
                        }
                        if ((dr["BPPracticals2"].ToString() != ""))
                        {
                            drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(dr["BPPracticals2"].ToString());
                        }
                    }
                    else
                    {
                        if ((dr["BPSubjects2"].ToString() != ""))
                        {
                            drow["BP Subjects"] = findSubjects(dr["BPSubjects2"].ToString());
                        }
                        if ((dr["BPPracticals2"].ToString() != ""))
                        {
                            drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(dr["BPPracticals2"].ToString());
                        }
                    }

                    if (drow["Exam Year Paid"].ToString() == "")
                    {
                        drow["Exam Year Paid"] = "2";
                    }
                    else if (drow["Exam Year Paid"].ToString() == "1")
                    {
                        drow["Exam Year Paid"] = "1,2";
                    }
                    
                }
                if ((dr["BPSubjects3"].ToString() != "") || (dr["BPPracticals3"].ToString() != ""))
                {
                    if (drow["BP Subjects"].ToString() != "")
                    {
                        if ((dr["BPSubjects3"].ToString() != ""))
                        {
                            drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findSubjects(dr["BPSubjects3"].ToString());
                        }
                        if ((dr["BPPracticals3"].ToString() != ""))
                        {
                            drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(dr["BPPracticals3"].ToString());
                        }
                    }
                    else
                    {
                        if ((dr["BPSubjects3"].ToString() != ""))
                        {
                            drow["BP Subjects"] = findSubjects(dr["BPSubjects3"].ToString());
                        }
                        if ((dr["BPPracticals3"].ToString() != ""))
                        {
                            drow["BP Subjects"] = drow["BP Subjects"].ToString() + "," + findPracticals(dr["BPPracticals3"].ToString());
                        }
                    }
                    if (drow["Exam Year Paid"].ToString() == "")
                    {
                        drow["Exam Year Paid"] = "3";
                    }
                    else if (drow["Exam Year Paid"].ToString() == "1")
                    {
                        drow["Exam Year Paid"] = "1,3";
                    }
                    else if (drow["Exam Year Paid"].ToString() == "2")
                    {
                        drow["Exam Year Paid"] = "2,3";
                    }
                    else if (drow["Exam Year Paid"].ToString() == "1,2")
                    {
                        drow["Exam Year Paid"] = "1,2,3";
                    }
                }
              
                if (drow["Exam Year Paid"].ToString() != "")
                {
                    //findExamCentreRecord(drow["SC Code"].ToString(), drow);
                    if (ddlistExam.SelectedItem.Value == "A13")
                    {
                        FindInfo.findExamCentreDetailByECID_A13(findExamCentreID(Convert.ToInt32(dr["SRID"])), drow);
                    }
                    else             
                    {
                        populateExamCentreRecord(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, drow);
                    }
                   
                }
                else
                {
                    drow["Exam City"] = "NA";
                    drow["Exam Centre"] = "NA";
                    drow["Exam Code"] = "NA";
                }


                dt.Rows.Add(drow);
                i = i + 1;
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

        private string findPracticals(string prac)
        {
            string[] s = { };
            string sub = "";
            s = prac.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                if (sub == "")
                {
                    sub = FindInfo.findPracticalNameByID(Convert.ToInt32(s[i])) + ",";
                }
                else
                {
                    sub = sub + FindInfo.findPracticalNameByID(Convert.ToInt32(s[i])) + ",";
                }


            }

            return sub;
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

        private int findExamCentreID(int srid)
        {
            int ecid = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select ExamCentreCode from DDEExamRecord_A13 where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                ecid = Convert.ToInt32(dr["ExamCentreCode"]);

            }

            con.Close();

            return ecid;
            
        }

        private object findSubjects(string subjects)
        {
           
           
            string[] s = { };
            string sub = "";
            s = subjects.Split(',');
            for (int i = 0; i < s.Length; i++)
            {
                if (sub == "")
                {
                    sub = FindInfo.findSubjectNameByID(Convert.ToInt32(s[i])) + ",";
                }
                else
                {
                    sub = sub + FindInfo.findSubjectNameByID(Convert.ToInt32(s[i])) + ",";
                }
                
                
            }
   
            return sub;
        }

        private object findAmountPaid(int srid)
        {
            int amount = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select Amount from [DDEFeeRecord_2013-14] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select Amount from [DDEFeeRecord_2014-15] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select Amount from [DDEFeeRecord_2015-16] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select Amount from [DDEFeeRecord_2016-17] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select Amount from [DDEFeeRecord_2017-18] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select Amount from [DDEFeeRecord_2018-19] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select Amount from [DDEFeeRecord_2019-20] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select Amount from [DDEFeeRecord_2020-21] where SRID='" + srid + "' and FeeHead='3' and ForExam='" + ddlistExam.SelectedItem.Value + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    amount = amount + Convert.ToInt32(dr["Amount"]);
                }
               

            }

            con.Close();

            return amount;
        }

       



        private void populateStudentinfo(int srid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                drow["Enrollment No."] = Convert.ToString(dr["EnrollmentNo"]);
                drow["Student Name"] = Convert.ToString(dr["StudentName"]);
                drow["Father's Name"] = Convert.ToString(dr["FatherName"]);
                drow["SC Code"] = findSCCode(srid);
                drow["Batch"] = Convert.ToString(dr["Session"]);
                drow["Course"] = FindInfo.findCourseShortNameBySRID(srid, Convert.ToInt32(dr["CYear"]));

            }

            con.Close();
        }

        private string findSCCode(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
                {
                    sccode = Convert.ToString(dr["StudyCentreCode"]);
                }
                else if (dr["SCStatus"].ToString() == "T")
                {
                    sccode = findTranferedSCCode(srid);
                }

            }

            con.Close();

            return sccode;
        }

        private string findTranferedSCCode(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PreviousSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                sccode = dr[0].ToString();

            }

            con.Close();

            return sccode;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateRecord();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=ExamData_BP_Till_" + DateTime.Now.ToString("dd-MM-yyyy_(hh:mm:ss:tt)") + ".xls";
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
