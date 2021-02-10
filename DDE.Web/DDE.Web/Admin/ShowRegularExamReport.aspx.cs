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
using System.Text;

namespace DDE.Web.Admin
{
    public partial class ShowRegularExamReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
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

        private void populateRecord()
        {
            if (ddlistExam.SelectedItem.Value == "A13")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where Online='True'", con);
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
                    if (dr["AFP1Year"].ToString() == "True")
                    {
                        drow["Ad. Year Paid"] = "1";

                        if (dr["AFP2Year"].ToString() == "True")
                        {
                            drow["Ad. Year Paid"] = "1,2";

                            if (dr["AFP3Year"].ToString() == "True")
                            {
                                drow["Ad. Year Paid"] = "1,2,3";
                            }

                        }

                        else if (dr["AFP3Year"].ToString() == "True")
                        {
                            drow["Ad. Year Paid"] = "1,3";
                        }

                    }
                    else if (dr["AFP2Year"].ToString() == "True")
                    {
                        drow["Ad. Year Paid"] = "2";

                        if (dr["AFP1Year"].ToString() == "True")
                        {
                            drow["Ad. Year Paid"] = "1,2";

                            if (dr["AFP3Year"].ToString() == "True")
                            {
                                drow["Ad. Year Paid"] = "1,3";
                            }

                        }

                        else if (dr["AFP3Year"].ToString() == "True")
                        {
                            drow["Ad. Year Paid"] = "2,3";
                        }

                    }
                    else if (dr["AFP3Year"].ToString() == "True")
                    {
                        drow["Ad. Year Paid"] = "3";

                        if (dr["AFP1Year"].ToString() == "True")
                        {
                            drow["Ad. Year Paid"] = "1,3";

                            if (dr["AFP2Year"].ToString() == "True")
                            {
                                drow["Ad. Year Paid"] = "1,2,3";
                            }

                        }

                        else if (dr["AFP2Year"].ToString() == "True")
                        {
                            drow["Ad. Year Paid"] = "2,3";
                        }

                    }





                    if (dr["EFP1Year"].ToString() == "True")
                    {
                        drow["Exam Year Paid"] = "1";

                        if (dr["EFP2Year"].ToString() == "True")
                        {
                            drow["Exam Year Paid"] = "1,2";

                            if (dr["EFP3Year"].ToString() == "True")
                            {
                                drow["Exam Year Paid"] = "1,2,3";
                            }

                        }

                        else if (dr["EFP3Year"].ToString() == "True")
                        {
                            drow["Exam Year Paid"] = "1,3";
                        }

                    }
                    else if (dr["EFP2Year"].ToString() == "True")
                    {
                        drow["Exam Year Paid"] = "2";

                        if (dr["EFP1Year"].ToString() == "True")
                        {
                            drow["Exam Year Paid"] = "1,2";

                            if (dr["EFP3Year"].ToString() == "True")
                            {
                                drow["Exam Year Paid"] = "1,3";
                            }

                        }

                        else if (dr["EFP3Year"].ToString() == "True")
                        {
                            drow["Exam Year Paid"] = "2,3";
                        }

                    }
                    else if (dr["EFP3Year"].ToString() == "True")
                    {
                        drow["Exam Year Paid"] = "3";

                        if (dr["EFP1Year"].ToString() == "True")
                        {
                            drow["Exam Year Paid"] = "1,3";

                            if (dr["EFP2Year"].ToString() == "True")
                            {
                                drow["Exam Year Paid"] = "1,2,3";
                            }

                        }

                        else if (dr["EFP2Year"].ToString() == "True")
                        {
                            drow["Exam Year Paid"] = "2,3";
                        }

                    }

                    if (drow["Exam Year Paid"].ToString() != "")
                    {
                        drow["Amount"] = "1000";
                    }
                    if (dr["ECID"].ToString().ToString() != "0")
                    {
                        if (drow["Ad. Year Paid"].ToString() != "" && drow["Exam Year Paid"].ToString() != "")
                        {
                            //findExamCentreRecord(drow["SC Code"].ToString(), drow);
                            if (dr["ECID"].ToString() != "")
                            {
                                FindInfo.findExamCentreDetailByECID_A13(Convert.ToInt32(dr["ECID"]), drow);
                            }

                        }
                        else
                        {
                            drow["Exam City"] = "NA";
                            drow["Exam Centre"] = "NA";
                            drow["Exam Code"] = "NA";
                        }
                    }
                    else
                    {
                        drow["Exam City"] = "NOT SET";
                        drow["Exam Centre"] = "NOT SET";
                        drow["Exam Code"] = "NOT SET";
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
            else if (ddlistExam.SelectedItem.Value == "B13")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='2' and ForExam='B13'", con);
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

                    //if (populateStudentinfo(Convert.ToInt32(dr["SRID"]), drow))
                    //{

                        drow["Roll No."] = FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]), ddlistExam.SelectedItem.Value, "R");
                        drow["Ad. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(dr["SRID"]), 1, "NA");
                        drow["Cont. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(dr["SRID"]), 5, "NA");
                        drow["Exam Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(dr["SRID"]), 2, ddlistExam.SelectedItem.Value);

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

                        populateExamCentreRecord(Convert.ToInt32(dr["SRID"]),"B13", drow);

                        

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
            else if (ddlistExam.SelectedItem.Value == "A14")
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("Select * from DDEStudentRecord where RecordStatus='True'");
                cmd1.Connection = con1;
                DataSet ds1 = new DataSet();


                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(ds1, "StudentRecord");
                DataColumn[] col = new DataColumn[1];
                col[0] = ds1.Tables["StudentRecord"].Columns["SRID"];
                ds1.Tables["StudentRecord"].PrimaryKey = col;

                //cmd1.CommandText = "select * from DDEExamRecord_A14";
                //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                //da2.Fill(ds1, "ExamRecord");
                //DataColumn[] col2 = new DataColumn[1];
                //col2[0] = ds1.Tables["ExamRecord"].Columns["ExamRecordID"];
                //ds1.Tables["ExamRecord"].PrimaryKey = col2;

                cmd1.CommandText = "select * from DDEExaminationCentres_A14";
                SqlDataAdapter da3 = new SqlDataAdapter(cmd1);
                da3.Fill(ds1, "ExamCentreRecord");
                DataColumn[] col3 = new DataColumn[1];
                col3[0] = ds1.Tables["ExamCentreRecord"].Columns["ECID"];
                ds1.Tables["ExamCentreRecord"].PrimaryKey = col3;




                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();

                if (ddlistYear.SelectedItem.Text == "ALL")
                {
                    cmd.CommandText = "Select distinct SRID from ( select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='1' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead='1' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead='1' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead='1' and ForExam='A14' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead='1' and ForExam='A14')a";
                }
                else
                {
                    cmd.CommandText = "Select distinct SRID from ( select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='1' and ForExam='A14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead='1' and ForExam='A14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead='1' and ForExam='A14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead='1' and ForExam='A14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead='1' and ForExam='A14' and ForYear='" + ddlistYear.SelectedItem.Value + "')a";
                }

                cmd.Connection = con;
               
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

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
               
                da.Fill(ds);

             

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i+1;

                   DataRow rowFound = ds1.Tables["StudentRecord"].Rows.Find(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));

                   if (rowFound != null)
                   {
                     drow["Enrollment No."] = Convert.ToString(rowFound["EnrollmentNo"]);
                     drow["Student Name"] = Convert.ToString(rowFound["StudentName"]);
                     drow["Father's Name"] = Convert.ToString(rowFound["FatherName"]);
                     drow["SC Code"] = FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(rowFound["SRID"]));
                     drow["Batch"] = Convert.ToString(rowFound["Session"]);
                     drow["Course"] = FindInfo.findCourseShortNameBySRID(Convert.ToInt32(rowFound["SRID"]), Convert.ToInt32(rowFound["CYear"]));
                   }
             
                   
                   

                    drow["Roll No."] = FindInfo.findRollNoBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value, "R");

                    drow["Ad. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), 1, "A14");
                    drow["Cont. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), 5, "NA");
                    drow["Exam Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), 2, ddlistExam.SelectedItem.Value);

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


                       


                     DataRow rowFound1 = ds1.Tables["ExamCentreRecord"].Rows.Find(FindInfo.findECIDBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]),"A14"));

                     if (rowFound1 != null)
                     {
                         drow["Exam City"] = Convert.ToString(rowFound1["City"]);
                         drow["Exam Centre"] = Convert.ToString(rowFound1["CentreName"]);
                         drow["Exam Code"] = Convert.ToString(rowFound1["ExamCentreCode"]);
                     }



                      
                     dt.Rows.Add(drow);
                       
                    
                }

                gvShowStudent.DataSource = dt;
                gvShowStudent.DataBind();

                con.Close();

                if (ds.Tables[0].Rows.Count > 1)
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
            else if (ddlistExam.SelectedItem.Value == "B14")
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand("Select * from DDEStudentRecord where RecordStatus='True'");
                cmd1.Connection = con1;
                DataSet ds1 = new DataSet();


                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(ds1, "StudentRecord");
                DataColumn[] col = new DataColumn[1];
                col[0] = ds1.Tables["StudentRecord"].Columns["SRID"];
                ds1.Tables["StudentRecord"].PrimaryKey = col;

                //cmd1.CommandText = "select * from DDEExamRecord_A14";
                //SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
                //da2.Fill(ds1, "ExamRecord");
                //DataColumn[] col2 = new DataColumn[1];
                //col2[0] = ds1.Tables["ExamRecord"].Columns["ExamRecordID"];
                //ds1.Tables["ExamRecord"].PrimaryKey = col2;

                cmd1.CommandText = "select * from DDEExaminationCentres_B14";
                SqlDataAdapter da3 = new SqlDataAdapter(cmd1);
                da3.Fill(ds1, "ExamCentreRecord");
                DataColumn[] col3 = new DataColumn[1];
                col3[0] = ds1.Tables["ExamCentreRecord"].Columns["ECID"];
                ds1.Tables["ExamCentreRecord"].PrimaryKey = col3;




                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();

                if (ddlistYear.SelectedItem.Text == "ALL")
                {
                    cmd.CommandText = "Select distinct SRID from ( select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='1' and ForExam='B14' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead='1' and ForExam='B14' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead='1' and ForExam='B14' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead='1' and ForExam='B14' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead='1' and ForExam='B14')a";
                }
                else
                {
                    cmd.CommandText = "Select distinct SRID from ( select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='1' and ForExam='B14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2014-15] where FeeHead='1' and ForExam='B14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2015-16] where FeeHead='1' and ForExam='B14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2016-17] where FeeHead='1' and ForExam='B14' and ForYear='" + ddlistYear.SelectedItem.Value + "' union select distinct SRID from [DDEFeeRecord_2017-18] where FeeHead='1' and ForExam='B14' and ForYear='" + ddlistYear.SelectedItem.Value + "')a";
                }

                cmd.Connection = con;
               
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

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
               
                da.Fill(ds);

             

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i+1;

                   DataRow rowFound = ds1.Tables["StudentRecord"].Rows.Find(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));

                   if (rowFound != null)
                   {
                     drow["Enrollment No."] = Convert.ToString(rowFound["EnrollmentNo"]);
                     drow["Student Name"] = Convert.ToString(rowFound["StudentName"]);
                     drow["Father's Name"] = Convert.ToString(rowFound["FatherName"]);
                     drow["SC Code"] = FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(rowFound["SRID"]));
                     drow["Batch"] = Convert.ToString(rowFound["Session"]);
                     drow["Course"] = FindInfo.findCourseShortNameBySRID(Convert.ToInt32(rowFound["SRID"]), Convert.ToInt32(rowFound["CYear"]));
                   }
             
                   
                   

                    drow["Roll No."] = FindInfo.findRollNoBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value, "R");

                    drow["Ad. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), 1, "B14");
                    drow["Cont. Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), 5, "NA");
                    drow["Exam Year Paid"] = FindInfo.isFeePaid(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), 2, ddlistExam.SelectedItem.Value);

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


                       


                     DataRow rowFound1 = ds1.Tables["ExamCentreRecord"].Rows.Find(FindInfo.findECIDBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]),"B14"));

                     if (rowFound1 != null)
                     {
                         drow["Exam City"] = Convert.ToString(rowFound1["City"]);
                         drow["Exam Centre"] = Convert.ToString(rowFound1["CentreName"]);
                         drow["Exam Code"] = Convert.ToString(rowFound1["ExamCentreCode"]);
                     }



                      
                     dt.Rows.Add(drow);
                       
                    
                }

                gvShowStudent.DataSource = dt;
                gvShowStudent.DataBind();

                con.Close();

                if (ds.Tables[0].Rows.Count > 1)
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

            else if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
            {
                bool examcentreset = FindInfo.isExamCentreSet(ddlistExam.SelectedItem.Value);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand(findCommand(ddlistExam.SelectedItem.Value,ddlistYear.SelectedItem.Value, examcentreset));             
                cmd.Connection = con;

               
                                       
                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("Enrollment No.");
                DataColumn dtcol3 = new DataColumn("Roll No.");
                DataColumn dtcol4 = new DataColumn("Student Name");
                DataColumn dtcol5 = new DataColumn("Father's Name");
                DataColumn dtcol6 = new DataColumn("SC Code");
                DataColumn dtcol7 = new DataColumn("Batch");
                DataColumn dtcol8 = new DataColumn("Course");
                DataColumn dtcol9 = new DataColumn("Year");     
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
                dt.Columns.Add(dtcol12);
                dt.Columns.Add(dtcol13);
                dt.Columns.Add(dtcol14);
                dt.Columns.Add(dtcol15);
                //dt.Columns.Add(dtcol16);

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                     DataRow drow = dt.NewRow();
                     drow["SNo"] = i+1;
                     drow["Enrollment No."] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                     drow["Student Name"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                     drow["Father's Name"] = ds.Tables[0].Rows[i]["FatherName"].ToString();
                     if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "O" || ds.Tables[0].Rows[i]["SCStatus"].ToString() == "C")
                     {
                         drow["SC Code"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                     }
                     else if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                     {
                         drow["SC Code"] = ds.Tables[0].Rows[i]["PreviousSCCode"].ToString();
                     }
                    
                     drow["Batch"] = ds.Tables[0].Rows[i]["Session"].ToString();
                     if (ds.Tables[0].Rows[i]["CourseShortName"].ToString() == "MBA")
                     {
                         if (ds.Tables[0].Rows[i]["ForYear"].ToString() == "1")
                         {
                             drow["Course"] = "MBA";
                         }
                         else if (ds.Tables[0].Rows[i]["ForYear"].ToString() == "2")
                         {
                             try
                             {
                                 drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                             }
                             catch
                             {
                                 drow["Course"] = "NA";
                             }
                         }
                         else if (ds.Tables[0].Rows[i]["ForYear"].ToString() == "3")
                         {
                             try
                             {
                                 drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                             }
                             catch
                             {
                                 drow["Course"] = "NA";
                             }
                         }
                     }
                     else
                     {
                         if (ds.Tables[0].Rows[i]["Specialization"].ToString() == "")
                         {
                             drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString();
                         }
                         else
                         {
                             drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString() + " (" + ds.Tables[0].Rows[i]["Specialization"].ToString() + ")";
                         }
                     }
                     drow["Year"] = ds.Tables[0].Rows[i]["ForYear"].ToString();
                     drow["Roll No."] = FindInfo.findRollNoBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value, "R");
                     if (drow["Roll No."].ToString() == "")
                     {
                         drow["Eligible For Exam"] = "NO";
                     }
                     else
                     {
                         drow["Eligible For Exam"] = "YES";
                     }

                     string[] ec = FindInfo.findExamCentreDetailByECID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value);

                     drow["Exam City"] = ec[0].ToString();
                     drow["Exam Centre"] = ec[1].ToString();
                     drow["Exam Code"] = ec[2].ToString();

                     //drow["Form Counter"] = "";  
                     dt.Rows.Add(drow);
                        
                }

                gvShowStudent.DataSource = dt;
                gvShowStudent.DataBind();

                      
                if (ds.Tables[0].Rows.Count > 1)
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

        private string findCommand(string exam,string year, bool examcentreset)
        {
            StringBuilder sb = new StringBuilder();

          
                sb.Append("select distinct [DDEFeeRecord_2013-14].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2013-14].ForYear,");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode");
                //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
                //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
                //sb.Append("DDEExaminationCentres_" + exam + ".City,");
                //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
                //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");

                sb.Append(" from [DDEFeeRecord_2013-14] ");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2013-14].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2013-14].SRID");
                //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");
                
                sb.Append(" where [DDEFeeRecord_2013-14].FeeHead='1' and [DDEFeeRecord_2013-14].ForExam='"+exam+"'");
                if (year != "0")
                {
                    sb.Append(" and [DDEFeeRecord_2013-14].ForYear='"+year+"'");
                }

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2014-15].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2014-15].ForYear,");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode");
                //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
                //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
                //sb.Append("DDEExaminationCentres_" + exam + ".City,");
                //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
                //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");
                               
                sb.Append(" from [DDEFeeRecord_2014-15]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2014-15].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2014-15].SRID");
                //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");
               
                sb.Append(" where [DDEFeeRecord_2014-15].FeeHead='1' and [DDEFeeRecord_2014-15].ForExam='"+exam+"'");             
                if (year != "0")
                {
                    sb.Append(" and [DDEFeeRecord_2014-15].ForYear='" + year + "'");
                }
                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2015-16].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append(" DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2015-16].ForYear,");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode");
                //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
                //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
                //sb.Append("DDEExaminationCentres_" + exam + ".City,");
                //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
                //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");

                sb.Append(" from [DDEFeeRecord_2015-16]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2015-16].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2015-16].SRID");
                //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");
               
                sb.Append(" where [DDEFeeRecord_2015-16].FeeHead='1' and [DDEFeeRecord_2015-16].ForExam='"+exam+"'");
                if (year != "0")
                {
                    sb.Append(" and [DDEFeeRecord_2015-16].ForYear='" + year + "'");
                }

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2016-17].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append(" DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2016-17].ForYear,");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode");
                //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
                //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
                //sb.Append("DDEExaminationCentres_" + exam + ".City,");
                //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
                //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");

                sb.Append(" from [DDEFeeRecord_2016-17]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2016-17].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2015-16].SRID");
                //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");

                sb.Append(" where [DDEFeeRecord_2016-17].FeeHead='1' and [DDEFeeRecord_2016-17].ForExam='" + exam + "'");
                if (year != "0")
                {
                    sb.Append(" and [DDEFeeRecord_2016-17].ForYear='" + year + "'");
                }

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2017-18].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append(" DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2017-18].ForYear,");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode");
                //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
                //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
                //sb.Append("DDEExaminationCentres_" + exam + ".City,");
                //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
                //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");

                sb.Append(" from [DDEFeeRecord_2017-18]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2017-18].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2017-18].SRID");
                //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");

                sb.Append(" where [DDEFeeRecord_2017-18].FeeHead='1' and [DDEFeeRecord_2017-18].ForExam='" + exam + "'");
                if (year != "0")
                {
                    sb.Append(" and [DDEFeeRecord_2017-18].ForYear='" + year + "'");
                }

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2018-19].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append(" DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2018-19].ForYear,");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode");
                //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
                //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
                //sb.Append("DDEExaminationCentres_" + exam + ".City,");
                //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
                //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");

                sb.Append(" from [DDEFeeRecord_2018-19]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2018-19].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2018-19].SRID");
                //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");

                sb.Append(" where [DDEFeeRecord_2018-19].FeeHead='1' and [DDEFeeRecord_2018-19].ForExam='" + exam + "'");
                if (year != "0")
                {
                    sb.Append(" and [DDEFeeRecord_2018-19].ForYear='" + year + "'");
                }

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2019-20].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append(" DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2019-20].ForYear,");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode");
                //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
                //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
                //sb.Append("DDEExaminationCentres_" + exam + ".City,");
                //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
                //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");

                sb.Append(" from [DDEFeeRecord_2019-20]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2019-20].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
                //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2019-20].SRID");
                //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");

                sb.Append(" where [DDEFeeRecord_2019-20].FeeHead='1' and [DDEFeeRecord_2019-20].ForExam='" + exam + "'");
                if (year != "0")
                {
                    sb.Append(" and [DDEFeeRecord_2019-20].ForYear='" + year + "'");
                }

            sb.Append(" union");

            sb.Append(" select distinct [DDEFeeRecord_2020-21].SRID,");
            sb.Append("DDEStudentRecord.[Session],");
            sb.Append("DDEStudentRecord.EnrollmentNo,");
            sb.Append(" DDEStudentRecord.StudentName,");
            sb.Append("DDEStudentRecord.FatherName,");
            sb.Append("[DDEFeeRecord_2020-21].ForYear,");
            sb.Append("DDEStudentRecord.Course,");
            sb.Append("DDEStudentRecord.Course2Year,");
            sb.Append("DDEStudentRecord.Course3Year,");
            sb.Append("DDECourse.CourseShortName,");
            sb.Append("DDECourse.Specialization,");
            sb.Append("DDEStudentRecord.SCStatus,");
            sb.Append("DDEStudentRecord.StudyCentreCode,");
            sb.Append("DDEStudentRecord.PreviousSCCode");
            //sb.Append("DDEExamRecord_"+exam+".ExamRecordID,");
            //sb.Append("DDEExamRecord_" + exam + ".RollNo,");
            //sb.Append("DDEExaminationCentres_" + exam + ".City,");
            //sb.Append("DDEExaminationCentres_" + exam + ".CentreName,");
            //sb.Append("DDEExaminationCentres_" + exam + ".ExamCentreCode");

            sb.Append(" from [DDEFeeRecord_2020-21]");
            sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2020-21].SRID");
            sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
            //sb.Append(" inner join DDEExamRecord_"+exam+" on DDEExamRecord_"+exam+".SRID=[DDEFeeRecord_2020-21].SRID");
            //sb.Append(" inner join DDEExaminationCentres_" + exam + " on DDEExaminationCentres_" + exam + ".ECID=DDEExamRecord_" + exam + ".ExamCentreCode");

            sb.Append(" where [DDEFeeRecord_2020-21].FeeHead='1' and [DDEFeeRecord_2020-21].ForExam='" + exam + "'");
            if (year != "0")
            {
                sb.Append(" and [DDEFeeRecord_2020-21].ForYear='" + year + "'");
            }

            return sb.ToString();
        }

        private void populateExamCentreRecord(int srid, string exam, DataRow drow)
        {
            int ecid = FindInfo.findECIDBySRID(srid,exam);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_"+exam+" where ECID='" + ecid + "'", con);
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

       

        private void findExamCentreRecord(string  sccode, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres1", con); 
            string[] sc = { };

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sc = ds.Tables[0].Rows[i]["SCCodes"].ToString().Split(',');
                    for (int j= 0; j <sc.Length; j++)
                    {
                        if (sc[j].ToString() == sccode)
                        {
                            drow["Exam City"] = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                            drow["Exam Centre"] = Convert.ToString(ds.Tables[0].Rows[i]["CentreName"]);
                            drow["Exam Code"] = Convert.ToString(ds.Tables[0].Rows[i]["ExamCentreCode"]);
                        }
                    }
                }

            }

          
            
        }

        

        private void populateStudentinfo(int srid, DataRow drow)
        {
           
                      
                   
          
        }

      

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateRecord();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=ExamData_Regular_Till_"+DateTime.Now.ToString("dd-MM-yyyy_(hh:mm:ss:tt)")+".xls";
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

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        
    }
}
