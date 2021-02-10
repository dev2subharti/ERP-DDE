using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class ShowCompleteExamReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                if(!IsPostBack)
                {
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("Z11").Selected = true;
                }
              
                pnlData.Visible = true;
                pnlMSG.Visible =false;

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

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            
           
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    if (ddlistSCCode.SelectedItem.Text == "ALL")
                    {
                        if (ddlistExam.SelectedItem.Value == "A13")
                        {
                            cmd.CommandText = "Select * from ExamRecord_June13 where Online='True'";
                        }
                        else if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14")
                        {
                            if (ddlistYear.SelectedItem.Text == "ALL")
                            {
                                cmd.CommandText = "Select distinct SRID,ForYear from [DDEFeeRecord_2013-14] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2014-15] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2015-16] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2016-17] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2017-18] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2018-19] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2019-20] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2020-21] where FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "'";
                            }
                            else
                            {
                                cmd.CommandText = "Select distinct SRID,ForYear from [DDEFeeRecord_2013-14] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2014-15] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2015-16] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "'  union Select distinct SRID,ForYear from [DDEFeeRecord_2016-17] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2017-18] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2018-19] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2019-20] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2020-21] where FeeHead='2' and ForYear='" + ddlistYear.SelectedItem.Value + "' and ForExam='" + ddlistExam.SelectedItem.Value + "'";
                            }
                        }
                        else 
                        {
                            if (ddlistYear.SelectedItem.Text == "ALL")
                            {
                                cmd.CommandText = "Select DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".SRID,DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".RollNo,DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".Year,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDECourse.CourseShortName,DDECourse.Specialization from DDEExamRecord_A15 inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".SRID inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course where DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".MOE='R'";
                            }
                            else
                            {
                                cmd.CommandText = "Select DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".SRID,DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".RollNo,DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".Year,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDECourse.CourseShortName,DDECourse.Specialization from DDEExamRecord_A15 inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".SRID inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course where DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".MOE='R' and DDEExamRecord_"+ ddlistExam.SelectedItem.Value+".Year='" + ddlistYear.SelectedItem.Value + "'";
                            }
                        }
                        
                       
                    }
                    else
                    {                       
                       
                        if (ddlistYear.SelectedItem.Text == "ALL")
                        {
                            cmd.CommandText = "Select DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".RollNo,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSPrinted,DDEStudentRecord.SyllabusSession,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDECourse.CourseShortName,DDECourse.Specialization from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='R' and DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSPrinted='False' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and (DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "')) or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "'))";
                        }
                        else
                        {
                            cmd.CommandText = "Select DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".RollNo,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year,,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSPrinted,DDEStudentRecord.SyllabusSession,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEStudentRecord.Course,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDECourse.CourseShortName,DDECourse.Specialization from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='R' and DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSPrinted='False' and DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year='" + ddlistYear.SelectedItem.Value + "' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and (DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "')) or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "'))";
                        }


                    //else
                    //{
                    //    string srids = FindInfo.findAllSRIDSBySCCode(ddlistSCCode.SelectedItem.Text);
                    //    string[] str = srids.Split(',');
                    //    cmd.CommandText = "Select distinct SRID,ForYear from [DDEFeeRecord_2013-14] where SRID in (" + srids + ") and FeeHead='2'  and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2014-15] where SRID in (" + srids + ") and FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2015-16] where SRID in (" + srids + ") and FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2016-17] where SRID in (" + srids + ") and FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2017-18] where SRID in (" + srids + ") and FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2018-19] where SRID in (" + srids + ") and FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2019-20] where SRID in (" + srids + ") and FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "' union Select distinct SRID,ForYear from [DDEFeeRecord_2020-21] where SRID in (" + srids + ") and FeeHead='2' and ForExam='" + ddlistExam.SelectedItem.Value + "'";
                    //}
                }

            }
            else if (ddlistMOE.SelectedItem.Value == "B")
            {
                if (ddlistSCCode.SelectedItem.Text == "ALL")
                {
                    cmd.CommandText = "Select * from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " where MOE='B' and MSPrinted='False'";
                }
                else
                {
                    //string srids = FindInfo.findAllSRIDSBySCCode(ddlistSCCode.SelectedItem.Text);
                    //string[] str = srids.Split(',');
                    cmd.CommandText = "Select * from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID=DDEStudentRecord.SRID where DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text+ "' and MOE='B' and DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSPrinted='False'";
                }
            }

            
          
            cmd.Connection = con;
      
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);              

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("RollNo"); 
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("SCCode");    
            DataColumn dtcol6 = new DataColumn("Course");
            DataColumn dtcol7 = new DataColumn("AFP");
            DataColumn dtcol8 = new DataColumn("EFP");
            DataColumn dtcol9 = new DataColumn("Eligible");
            DataColumn dtcol10 = new DataColumn("MSStatus");
            DataColumn dtcol11 = new DataColumn("Remark");
            DataColumn dtcol12 = new DataColumn("Printed");
            DataColumn dtcol13 = new DataColumn("Attendence");


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



            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i + 1;
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                    {
                        drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                        drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();
                        drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                        if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "O" || ds.Tables[0].Rows[i]["SCStatus"].ToString() == "C")
                        {
                            drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                        }
                        else if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                        {
                            drow["SCCode"] = ds.Tables[0].Rows[i]["PreviousSCCode"].ToString();
                        }

                        if (ds.Tables[0].Rows[i]["CourseShortName"].ToString() == "MBA")
                        {
                            try
                            {
                                if (ds.Tables[0].Rows[i]["Year"].ToString() == "1")
                                {
                                    drow["Course"] = "MBA";
                                }
                                else if (ds.Tables[0].Rows[i]["Year"].ToString() == "2")
                                {
                                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                }
                                else if (ds.Tables[0].Rows[i]["Year"].ToString() == "3")
                                {
                                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                }
                            }
                            catch
                            {
                                drow["Course"] = "NF";
                                goto step1;
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

                        step1:
                        drow["AFP"] = "Yes";
                        drow["EFP"] = "Yes";
                        drow["Eligible"] = "Yes";
                        string remark;
                        string pre;
                        drow["MSStatus"] = findMarkSheetStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ds.Tables[0].Rows[i]["SyllabusSession"].ToString(), drow, Convert.ToString(ds.Tables[0].Rows[i]["Year"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, "", "", "", out remark, out pre);
                        drow["Remark"] = remark;

                        if (ds.Tables[0].Rows[i]["MSPrinted"].ToString() == "True")
                        {
                            drow["Printed"] = "Yes";
                        }
                        else if (ds.Tables[0].Rows[i]["MSPrinted"].ToString() == "False")
                        {
                            drow["Printed"] = "No";
                        }
                        drow["Attendence"] = pre;

                    }
                    else
                    {

                        populateStudentinfo(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), drow);

                        if (ddlistExam.SelectedItem.Value == "A13")
                        {
                            drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();
                        }
                        else if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18")
                        {
                            drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                        }

                        string year = "No";
                        string remark;
                        if (ddlistMOE.SelectedItem.Value == "R")
                        {
                            if (ddlistExam.SelectedItem.Value == "A13")
                            {
                                if (ds.Tables[0].Rows[i]["AFP1Year"].ToString() == "True")
                                {
                                    drow["AFP"] = "1";

                                    if (ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True")
                                    {
                                        drow["AFP"] = "1,2";

                                        if (ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True")
                                        {
                                            drow["AFP"] = "1,2,3";
                                        }

                                    }

                                    else if (ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True")
                                    {
                                        drow["AFP"] = "1,3";
                                    }

                                }
                                else if (ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True")
                                {
                                    drow["AFP"] = "2";

                                    if (ds.Tables[0].Rows[i]["AFP1Year"].ToString() == "True")
                                    {
                                        drow["AFP"] = "1,2";

                                        if (ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True")
                                        {
                                            drow["AFP"] = "1,3";
                                        }

                                    }

                                    else if (ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True")
                                    {
                                        drow["AFP"] = "2,3";
                                    }

                                }
                                else if (ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True")
                                {
                                    drow["AFP"] = "3";

                                    if (ds.Tables[0].Rows[i]["AFP1Year"].ToString() == "True")
                                    {
                                        drow["AFP"] = "1,3";

                                        if (ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True")
                                        {
                                            drow["AFP"] = "1,2,3";
                                        }

                                    }

                                    else if (ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True")
                                    {
                                        drow["AFP"] = "2,3";
                                    }

                                }

                                if (ds.Tables[0].Rows[i]["EFP1Year"].ToString() == "True")
                                {
                                    drow["EFP"] = "1";

                                    if (ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True")
                                    {
                                        drow["EFP"] = "1,2";

                                        if (ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True")
                                        {
                                            drow["EFP"] = "1,2,3";
                                        }

                                    }

                                    else if (ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True")
                                    {
                                        drow["EFP"] = "1,3";
                                    }

                                }
                                else if (ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True")
                                {
                                    drow["EFP"] = "2";

                                    if (ds.Tables[0].Rows[i]["EFP1Year"].ToString() == "True")
                                    {
                                        drow["EFP"] = "1,2";

                                        if (ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True")
                                        {
                                            drow["EFP"] = "1,3";
                                        }

                                    }

                                    else if (ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True")
                                    {
                                        drow["EFP"] = "2,3";
                                    }

                                }
                                else if (ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True")
                                {
                                    drow["EFP"] = "3";

                                    if (ds.Tables[0].Rows[i]["EFP1Year"].ToString() == "True")
                                    {
                                        drow["EFP"] = "1,3";

                                        if (ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True")
                                        {
                                            drow["EFP"] = "1,2,3";
                                        }

                                    }

                                    else if (ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True")
                                    {
                                        drow["EFP"] = "2,3";
                                    }

                                }



                                if ((ds.Tables[0].Rows[i]["AFP1Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP1Year"].ToString() == "True"))
                                {
                                    year = "1";

                                    if ((ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True"))
                                    {
                                        year = "1,2";
                                    }
                                    else if ((ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True"))
                                    {
                                        year = "1,3";
                                    }


                                }
                                else if ((ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True"))
                                {
                                    year = "2";

                                    if ((ds.Tables[0].Rows[i]["AFP1Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP1Year"].ToString() == "True"))
                                    {
                                        year = "1,2";
                                    }
                                    else if ((ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True"))
                                    {
                                        year = "2,3";
                                    }


                                }
                                else if ((ds.Tables[0].Rows[i]["AFP3Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP3Year"].ToString() == "True"))
                                {
                                    year = "3";

                                    if ((ds.Tables[0].Rows[i]["AFP1Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP1Year"].ToString() == "True"))
                                    {
                                        year = "1,3";
                                    }
                                    else if ((ds.Tables[0].Rows[i]["AFP2Year"].ToString() == "True" && ds.Tables[0].Rows[i]["EFP2Year"].ToString() == "True"))
                                    {
                                        year = "2,3";
                                    }
                                }



                                if (year == "No")
                                {
                                    drow["Eligible"] = "No";
                                }
                                else
                                {
                                    drow["Eligible"] = "Yes <br/> For : " + year + " Year";
                                }




                                if (year.Length == 1 && year != "No")
                                {
                                    drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(year));
                                }
                                else if (year.Length == 2 && year != "No")
                                {
                                    drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(year.Substring(2, 1)));
                                }
                                else if (year == "No")
                                {
                                    if (drow["AFP"].ToString().Length == 1)
                                    {
                                        drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["AFP"].ToString()));
                                    }
                                    else if (drow["AFP"].ToString().Length == 2)
                                    {
                                        drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["AFP"].ToString().Substring(2, 1)));
                                    }
                                    else if (drow["AFP"].ToString().Length == 0)
                                    {
                                        if (drow["EFP"].ToString().Length == 1)
                                        {
                                            drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["EFP"].ToString()));
                                        }
                                        else if (drow["EFP"].ToString().Length == 2)
                                        {
                                            drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["EFP"].ToString().Substring(2, 1)));
                                        }


                                    }
                                }
                            }

                            else if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18")
                            {
                                year = ds.Tables[0].Rows[i]["ForYear"].ToString();
                                drow["EFP"] = year;
                                drow["AFP"] = isCourseFeePaid(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["EFP"]));
                                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["EFP"]));

                                if (drow["EFP"].ToString() == drow["AFP"].ToString())
                                {
                                    drow["Eligible"] = "Yes";
                                }
                                else
                                {
                                    drow["Eligible"] = "No";
                                }

                            }



                            string pre = "NF";

                            drow["MSStatus"] = findMarkSheetStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ds.Tables[0].Rows[i]["SyllabusSession"].ToString(), drow, year, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, "", "", "", out remark, out pre);
                            drow["Remark"] = remark;

                            if (ds.Tables[0].Rows[i]["MSPrinted"].ToString() == "True")
                            {
                                drow["Printed"] = "Yes <br/>" + ds.Tables[0].Rows[i]["Times"].ToString() + " Times";
                            }
                            else if (ds.Tables[0].Rows[i]["MSPrinted"].ToString() == "False")
                            {
                                drow["Printed"] = "";
                            }

                            if (drow["MSStatus"].ToString() == "NA")
                            {
                                drow["Attendence"] = "Present";
                            }
                            else
                            {
                                drow["Attendence"] = pre;
                            }
                           

                        }

                    }
                }
                else if (ddlistMOE.SelectedItem.Value == "B")
                {
                    populateStudentinfo(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), drow);
                    string year = "No";
                    string remark;
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlDataReader dr1;
                    SqlCommand cmd1 = new SqlCommand("Select * from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " where SRID='" + Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]) + "' and MOE='B'", con1);
                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        dr1.Read();
                        if ((dr1["BPSubjects1"].ToString() != ""))
                        {
                            year = "1";
                            if ((dr1["BPSubjects2"].ToString() != ""))
                            {
                                year = "1,2";
                            }
                            else if ((dr1["BPSubjects3"].ToString() != ""))
                            {
                                year = "1,3";
                            }
                        }
                        else if ((dr1["BPSubjects2"].ToString() != ""))
                        {
                            year = "2";
                            if ((dr1["BPSubjects1"].ToString() != ""))
                            {
                                year = "1,2";
                            }
                            else if ((dr1["BPSubjects3"].ToString() != ""))
                            {
                                year = "2,3";
                            }
                        }
                        else if ((dr1["BPSubjects3"].ToString() != ""))
                        {
                            year = "3";
                            if ((dr1["BPSubjects1"].ToString() != ""))
                            {
                                year = "1,3";
                            }
                            else if ((dr1["BPSubjects2"].ToString() != ""))
                            {
                                year = "2,3";
                            }
                        }
                    }
                    con1.Close();

                    if (year.Length == 1 && year != "No")
                    {
                        drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(year));
                    }
                    else if (year.Length == 2 && year != "No")
                    {
                        drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(year.Substring(2, 1)));
                    }
                    else if (year == "No")
                    {
                        if (drow["AFP"].ToString().Length == 1)
                        {
                            drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["AFP"].ToString()));
                        }
                        else if (drow["AFP"].ToString().Length == 2)
                        {
                            drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["AFP"].ToString().Substring(2, 1)));
                        }
                        else if (drow["AFP"].ToString().Length == 0)
                        {
                            if (drow["EFP"].ToString().Length == 1)
                            {
                                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["EFP"].ToString()));
                            }
                            else if (drow["EFP"].ToString().Length == 2)
                            {
                                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(drow["EFP"].ToString().Substring(2, 1)));
                            }


                        }
                    }




                    drow["AFP"] = year;
                    drow["EFP"] = year;
                    drow["Eligible"] = "Yes <br/> For : " + year + " Year";
                    string pre = "NF";
                    drow["MSStatus"] = findMarkSheetStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), FindInfo.findSySessionBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"])), drow, year, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, ds.Tables[0].Rows[i]["BPSubjects1"].ToString(), ds.Tables[0].Rows[i]["BPSubjects2"].ToString(), ds.Tables[0].Rows[i]["BPSubjects3"].ToString(), out remark, out pre);
                    drow["Remark"] = remark;
                    if (ddlistExam.SelectedItem.Value == "A13")
                    {
                        drow["Printed"] = "No";
                        if (drow["MSStatus"].ToString() == "NA")
                        {
                            drow["Attendence"] = "NA";
                        }
                        else
                        {
                            drow["Attendence"] = pre;
                        }
                    }
                }




                    dt.Rows.Add(drow);




                }

                gvShowReport.DataSource = dt;
                gvShowReport.DataBind();

                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvShowReport.Visible = true;
                    pnlMSG.Visible = false;
                }
                else
                {
                    gvShowReport.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found.";
                    pnlMSG.Visible = true;
                }
           
        }

        private object isCourseFeePaid(int srid, int year)
        {
            string paid = "NP";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd1 = new SqlCommand();
                SqlDataReader dr1;


                cmd1.CommandText = "select distinct ForYear from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + srid + "' and FeeHead='1' and ForYear='" + year + "'";


                cmd1.Connection = con1;
                con1.Open();
                dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    paid = year.ToString();                  
                    break;
                }
                con1.Close();

            }

            con.Close();


            return paid;
        }

        private int findYear(SqlDataReader dr)
        {
            int year = 0;
            if (dr["AFP1Year"].ToString() == dr["EFP1Year"].ToString())
            {
                year = 1;
            }
            else if (dr["AFP2Year"].ToString() == dr["EFP2Year"].ToString())
            {
                year = 1;
            }
            else if (dr["AFP3Year"].ToString() == dr["EFP3Year"].ToString())
            {
                year = 1;
            }
            return year;
        }

        private string findMarkSheetStatus(int srid,string sys, DataRow drow,string year,string exam, string moe,string sub1,string sub2,string sub3,out string remark,out string pre)
        {
            string status="NA";
            remark="NA";
            pre = "AB";
            if (moe == "R")
            {
                if (year != "No" && year.Length > 1)
                {
                    string[] sy = year.Split(',');
                    for (int j = 0; j < sy.Length; j++)
                    {
                        int intyear = Convert.ToInt32(sy[j]);
                        string ayear = FindInfo.findAlphaYear(sy[j]);
                        string sysession = "";
                        if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                        {
                            sysession = sys;
                        }
                        else
                        {
                            sysession = "A 2010-11";
                        }
                       

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("Select SubjectID,PaperCode from DDESubject where SyllabusSession='"+sysession+"' and CourseName='" + FindInfo.findCourseNameBySRID(srid, intyear) + "' and Year='" + ayear + "' order by SubjectSNo", con);
                        con.Open();
                        SqlDataReader dr;

                        dr = cmd.ExecuteReader();
                        
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (!asReceived(srid, Convert.ToInt32(dr["SubjectID"]),exam, moe))
                                {
                                    if (remark == "NA")
                                    {
                                        remark = "Answer Sheet Not Received for Paper Code : " + dr["PaperCode"];
                                    }
                                    else
                                    {
                                        remark = remark + ", Answer Sheet Not Received for Paper Code : " + dr["PaperCode"];
                                    }
                                }
                                else
                                {
                                    pre = "Present";
                                }
                                string rm = "";
                                if (!marksNotFeeded(srid, Convert.ToInt32(dr["SubjectID"]),exam, moe, out rm))
                                {
                                    if (remark == "NA")
                                    {
                                        remark = rm + " Marks are Not Filled for Paper Code : " + dr["PaperCode"];
                                    }
                                    else
                                    {
                                        remark = remark + ", " + rm + " Marks are Not Filled for Paper Code : " + dr["PaperCode"];
                                    }

                                }

                            }

                        }

                        con.Close();

                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd2 = new SqlCommand();

                        if (ddlistFor.SelectedItem.Value == "1")
                        {
                            cmd2.CommandText = "Select PracticalID,PracticalCode,PracticalName from DDEPractical where SyllabusSession='"+sysession+"' and CourseName='" + FindInfo.findCourseNameBySRID(srid, intyear) + "' and Year='" + ayear + "' order by PracticalSNo";
                        }
                        else if (ddlistFor.SelectedItem.Value == "2")
                        {
                            cmd2.CommandText = "Select PracticalID,PracticalCode,PracticalName from DDEPractical where SyllabusSession='" + sysession + "' and CourseName='" + FindInfo.findCourseNameBySRID(srid, intyear) + "' and Year='" + ayear + "' and MUAllowedForSC='True' order by PracticalSNo";
                        }

                        cmd2.Connection = con2;

                        con2.Open();
                        SqlDataReader dr2;

                        dr2 = cmd2.ExecuteReader();

                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                               
                                string rm = "";
                                if (!marksNotFeeded_P(srid, Convert.ToInt32(dr2["PracticalID"]),exam, moe, out rm))
                                {
                                    if (remark == "NA")
                                    {
                                        remark = rm + "Marks are Not Filled for : " + dr2["PracticalCode"] + "-" + dr2["PracticalName"];
                                    }
                                    else
                                    {
                                        remark = remark + ", " + rm + "Marks are Not Filled for : " + dr2["PracticalCode"] + "-" + dr2["PracticalName"];
                                    }

                                }

                            }

                        }

                        con2.Close();
                    }
                }
                else
                {
                    if (year != "No")
                    {
                        int intyear = Convert.ToInt32(year);
                        string ayear = FindInfo.findAlphaYear(year);
                        string sysession = "";
                        if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11" || ddlistExam.SelectedItem.Value == "Z11")
                        {
                            sysession = sys;
                        }
                        else
                        {
                            sysession = "A 2010-11";
                        }
                       
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("Select SubjectID,PaperCode from DDESubject where SyllabusSession='" + sysession + "' and CourseName='" + FindInfo.findCourseNameBySRID(srid, intyear) + "' and Year='" + ayear + "' order by SubjectSNo", con);
                        con.Open();
                        SqlDataReader dr;

                        dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (!asReceived(srid, Convert.ToInt32(dr["SubjectID"]),exam, moe))
                                {
                                    if (remark == "NA")
                                    {
                                        remark = "Answer Sheet Not Received for Paper Code : " + dr["PaperCode"];
                                    }
                                    else
                                    {
                                        remark = remark + ", Answer Sheet Not Received for Paper Code : " + dr["PaperCode"];
                                    }
                                }
                                else
                                {
                                    pre = "Present";
                                }
                                string rm = "";
                                if (!marksNotFeeded(srid, Convert.ToInt32(dr["SubjectID"]),exam, moe,out rm))
                                {
                                    if (remark == "NA")
                                    {
                                        remark = rm + " Marks are Not Filled for Paper Code : " + dr["PaperCode"];
                                    }
                                    else
                                    {
                                        remark = remark + ", " + rm + " Marks are Not Filled for Paper Code : " + dr["PaperCode"];
                                    }

                                }

                            }

                        }

                        con.Close();

                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd2 = new SqlCommand();

                        if (ddlistFor.SelectedItem.Value == "1")
                        {
                            cmd2.CommandText = "Select PracticalID,PracticalCode,PracticalName from DDEPractical where SyllabusSession='" + sysession + "' and CourseName='" + FindInfo.findCourseNameBySRID(srid, intyear) + "' and Year='" + ayear + "' order by PracticalSNo";
                        }
                        else if (ddlistFor.SelectedItem.Value == "2")
                        {
                            cmd2.CommandText = "Select PracticalID,PracticalCode,PracticalName from DDEPractical where SyllabusSession='" + sysession + "' and CourseName='" + FindInfo.findCourseNameBySRID(srid, intyear) + "' and Year='" + ayear + "' and MUAllowedForSC='True' order by PracticalSNo";
                        }

                        cmd2.Connection = con2;

                        con2.Open();
                        SqlDataReader dr2;

                        dr2 = cmd2.ExecuteReader();

                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {

                                string rm = "";
                                if (!marksNotFeeded_P(srid, Convert.ToInt32(dr2["PracticalID"]),exam, moe, out rm))
                                {
                                    if (remark == "NA")
                                    {
                                        remark = rm + "Marks are Not Filled for : " + dr2["PracticalCode"] + "-" + dr2["PracticalName"];
                                    }
                                    else
                                    {
                                        remark = remark + ", " + rm + "Marks are Not Filled for : " + dr2["PracticalCode"] + "-" + dr2["PracticalName"];
                                    }

                                }

                            }

                        }

                        con2.Close();
                    }

                    if (year != "No")
                    {

                        if (remark != "NA")
                        {
                            status = "Pending";
                        }
                        else if (remark == "NA")
                        {
                            status = "OK";
                        }
                    }
                    else
                    {

                        status = "NA";
                    }
                }
            }
            else if(moe=="B")
            {
                string[] sasub1 = sub1.Split(',');
                string[] sasub2 = sub2.Split(',');
                string[] sasub3 = sub3.Split(',');

                if (sasub1.Length != 0)
                {
                    int j = 0;
                    while (j < sasub1.Length)
                    {
                        if (sasub1[j].ToString() != "")
                        {
                            string papercode = FindInfo.findPaperCodeByID(Convert.ToInt32(sasub1[j]));

                            if (!asReceived(srid, Convert.ToInt32(sasub1[j]),exam, moe))
                            {
                                if (remark == "NA")
                                {
                                    remark = "Answer Sheet Not Received for Paper Code : " + papercode;
                                }
                                else
                                {
                                    remark = remark + ", Answer Sheet Not Received for Paper Code : " + papercode;
                                }
                            }


                            string rm = "";
                            if (!marksNotFeeded(srid, Convert.ToInt32(sasub1[j]),exam, moe, out rm))
                            {
                                if (remark == "NA")
                                {
                                    remark = rm + " Marks are Not Filled for Paper Code : " + papercode;
                                }
                                else
                                {
                                    remark = remark + ", " + rm + " Marks are Not Filled for Paper Code : " + papercode;
                                }
                            }
                        }
                        j = j + 1;
                    }
                }
                if (sasub2.Length != 0)
                {
                    int j = 0;
                    while (j < sasub2.Length)
                    {
                        if (sasub2[j].ToString() != "")
                        {
                            string papercode = FindInfo.findPaperCodeByID(Convert.ToInt32(sasub2[j]));

                            if (!asReceived(srid, Convert.ToInt32(sasub2[j]),exam, moe))
                            {
                                if (remark == "NA")
                                {
                                    remark = "Answer Sheet Not Received for Paper Code : " + papercode;
                                }
                                else
                                {
                                    remark = remark + ", Answer Sheet Not Received for Paper Code : " + papercode;
                                }
                            }

                            string rm = "";
                            if (!marksNotFeeded(srid, Convert.ToInt32(sasub2[j]),exam, moe, out rm))
                            {
                                if (remark == "NA")
                                {
                                    remark = rm + " Marks are Not Filled for Paper Code : " + papercode;
                                }
                                else
                                {
                                    remark = remark + ", " + rm + " Marks are Not Filled for Paper Code : " + papercode;
                                }
                            }
                        }
                        j = j + 1;
                    }
                }
                if (sasub3.Length != 0)
                {
                    int j = 0;
                    while (j < sasub3.Length)
                    {
                        if (sasub3[j].ToString() != "")
                        {
                            string papercode = FindInfo.findPaperCodeByID(Convert.ToInt32(sasub3[j]));

                            if (!asReceived(srid, Convert.ToInt32(sasub3[j]),exam, moe))
                            {
                                if (remark == "NA")
                                {
                                    remark = "Answer Sheet Not Received for Paper Code : " + papercode;
                                }
                                else
                                {
                                    remark = remark + ", Answer Sheet Not Received for Paper Code : " + papercode;
                                }
                            }

                            string rm = "";
                            if (!marksNotFeeded(srid, Convert.ToInt32(sasub3[j]),ddlistExam.SelectedItem.Value, moe, out rm))
                            {
                                if (remark == "NA")
                                {
                                    remark = rm + " Marks are Not Filled for Paper Code : " + papercode;
                                }
                                else
                                {
                                    remark = remark + ", " + rm + " Marks are Not Filled for Paper Code : " + papercode;
                                }
                            }
                        }
                        j = j + 1;
                    }
                }


                if (year != "No")
                {

                    if (remark != "NA")
                    {
                        status = "Pending";
                    }
                    else if (remark == "NA")
                    {
                        status = "OK";
                    }
                }
                else
                {

                    status = "NA";
                }
                                            
                   
              }

            return status;
 	
        }

        private bool marksNotFeeded_P(int srid, int pid,string exam, string moe, out string rm)
        {
            rm = "";
            bool rec = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEPracticalMarks_"+exam+" where SRID='" + srid + "' and PracticalID='" + pid + "' and MOE='" + moe + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["PracticalMarks"].ToString() != "")
                {
                    rec = true;
                }      

            }
            else
            {
                rm = "";
            }

            con.Close();
            return rec;
            
        }

        private bool marksNotFeeded(int srid, int subid,string exam, string moe, out string rm)
        {
            rm = "";
            bool rec = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEMarkSheet_"+exam+" where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='"+moe+"'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (moe == "R")
                {
                    if (dr["Theory"].ToString() != "" && dr["IA"].ToString() != "" && dr["AW"].ToString() != "")
                    {
                        rec = true;
                    }
                    else
                    {
                        if (dr["Theory"].ToString() == "")
                        {
                            rm = "Theory";
                        }
                        if (dr["IA"].ToString() == "")
                        {
                            rm = rm + ",IA";
                        }
                        if (dr["AW"].ToString() == "")
                        {
                            rm = rm + ",AW";
                        }

                    }
                }
                else if (moe == "B")
                {
                    if (dr["Theory"].ToString() != "")
                    {
                        rec = true;
                    }
                    else
                    {
                        if (dr["Theory"].ToString() == "")
                        {
                            rm = "Theory";
                        }
                        
                    }

                }

            }
            else
            {
                if (moe == "R")
                {
                    rm = "Theory,IA,AW";
                }
                else if (moe == "B")
                {
                    rm = "Theory";
                }
            }

            con.Close();
            return rec;
        }

        private bool asReceived(int srid, int subid,string exam, string moe)
        {
            bool rec = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEAnswerSheetRecord_"+exam+" where SRID='" + srid + "' and SubjectID='" + subid + "' and MOE='"+moe+"'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["Received"].ToString() == "True")
                {
                    rec = true;
                }

            }

            con.Close();
            return rec;
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
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);            
                drow["SCCode"] = findSCCode(srid);
               

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
                   sccode=findTranferedSCCode(srid);
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
           
            populateRecord();
            
        }
    }
    
}
