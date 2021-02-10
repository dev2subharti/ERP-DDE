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
    public partial class ShowECPStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {

                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("Z11").Selected = true;
                    PopulateDDList.populateExamCentreByExam(ddlistEC, "Z11");
                    PopulateDDList.populateStudyCentre(ddlistSCCode);


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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            polulatePSList();

        }

        private void polulatePSList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "select DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".[Year],DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentPhoto,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".RollNo,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".BPSubjects1,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".BPSubjects2,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".BPSubjects3,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.[Session],DDEStudentRecord.[SyllabusSession],DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseShortName,DDECourse.Specialization,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".CentreName,DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".Location,DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".City from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID inner join DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + " on DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".ECID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".ExamCentreCode inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='" + ddlistMOE.SelectedItem.Value + "' and DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".ExamCentreCode='" + ddlistEC.SelectedItem.Value + "' order by DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year";
            }
            else
            {
                cmd.CommandText = "select DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".[Year],DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentPhoto,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".RollNo,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".BPSubjects1,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".BPSubjects2,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".BPSubjects3,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.[Session],DDEStudentRecord.[SyllabusSession],DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseShortName,DDECourse.Specialization,DDEStudentRecord.Course2Year,DDEStudentRecord.Course3Year,DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".CentreName,DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".Location,DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".City from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID inner join DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + " on DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".ECID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".ExamCentreCode inner join DDECourse on DDEStudentRecord.Course=DDECourse.CourseID where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='" + ddlistMOE.SelectedItem.Value + "' and DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".ExamCentreCode='" + ddlistEC.SelectedItem.Value + "' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "')) order by DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year";
            }



            DataTable dt = new DataTable();
            DataColumn dtsn = new DataColumn("SNo");
            DataColumn dtcol1 = new DataColumn("SRID");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("RollNo");
            DataColumn dtcol6 = new DataColumn("SName");
            DataColumn dtcol7 = new DataColumn("FName");
            DataColumn dtcol9 = new DataColumn("Course");
            DataColumn dtcol10 = new DataColumn("Year");


            dt.Columns.Add(dtsn);
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);


            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int count = 0;
            int sn = 1;
            if (ds.Tables[0].Rows.Count > 0)
            {
                string[] detained = FindInfo.findDetainedStudents(ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                   
                        int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                        if (!(pos > -1))
                        {

                            DataRow drow = dt.NewRow();

                            drow["SNo"] = sn.ToString();
                            drow["SRID"] = ds.Tables[0].Rows[i]["SRID"].ToString();


                            if ((ds.Tables[0].Rows[i]["SCStatus"].ToString() == "O") || (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "C"))
                            {
                                drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                            }
                            else if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                            {
                                drow["SCCode"] = ds.Tables[0].Rows[i]["PreviousSCCode"].ToString();
                            }
                            drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                            drow["RollNo"] = ds.Tables[0].Rows[i]["RollNo"].ToString();
                            drow["SName"] = ds.Tables[0].Rows[i]["StudentName"];
                            drow["FName"] = ds.Tables[0].Rows[i]["FatherName"];
                            if (ddlistMOE.SelectedItem.Value == "R")
                            {
                                drow["Year"] = FindInfo.findAlphaYear(ds.Tables[0].Rows[i]["Year"].ToString()).ToUpper();
                            }
                            else if (ddlistMOE.SelectedItem.Value == "B")
                            {
                                drow["Year"] = "NA";
                            }
                            if (ds.Tables[0].Rows[i]["CourseShortName"].ToString() == "MBA")
                            {
                                if (ddlistMOE.SelectedItem.Value == "R")
                                {
                                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 1)
                                    {
                                        drow["Course"] = "MBA";
                                    }
                                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 2)
                                    {
                                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                    }
                                    else if (Convert.ToInt32(ds.Tables[0].Rows[i]["Year"]) == 3)
                                    {
                                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                    }
                                }
                                else if (ddlistMOE.SelectedItem.Value == "B")
                                {

                                    if (ds.Tables[0].Rows[i]["BPSubjects1"].ToString() != "")
                                    {
                                        drow["Course"] = "MBA";
                                    }
                                    if (ds.Tables[0].Rows[i]["BPSubjects2"].ToString() != "")
                                    {
                                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                    }
                                    if (ds.Tables[0].Rows[i]["BPSubjects3"].ToString() != "")
                                    {
                                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
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

                            dt.Rows.Add(drow);
                            count = count + 1;
                            sn = sn + 1;
                        }
                   


                }

                dtlistShowDS.DataSource = dt;
                dtlistShowDS.DataBind();



                
                dtlistShowDS.Visible = true;
                pnlMSG.Visible = false;


            }
            else
            {
              
                dtlistShowDS.Visible = false;

                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

        }

       

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlistEC.Items.Clear();
            PopulateDDList.populateExamCentreByExam(ddlistEC, ddlistExam.SelectedItem.Value);
            dtlistShowDS.Visible = false;
        }

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDS.Visible = false;
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDS.Visible = false;
        }

        protected void ddlistEC_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDS.Visible = false;
        }
    }
}