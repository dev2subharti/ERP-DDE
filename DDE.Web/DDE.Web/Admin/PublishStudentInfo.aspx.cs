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
    public partial class PublishStudentInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDDList.populateBatch(ddlistBatch);
                PopulateDDList.populateStudyCentre(ddlistSCCode);
                PopulateDDList.populateCourses(ddlistCourse);
                populateCoursesGroups();
                
            }

        }

        private void populateCoursesGroups()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse order by CourseShortName", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("CourseID");
            DataColumn dtcol2 = new DataColumn("Course");
          


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
          


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
               
                drow["CourseID"] = Convert.ToString(dr["CourseID"]);
                if (dr[2].ToString() == "")
                {
                    drow["Course"] = Convert.ToString(dr[1].ToString());
                }

                else
                {
                    drow["Course"] = Convert.ToString(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");

                }
               
             
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistGroup.DataSource = dt;
            dtlistGroup.DataBind();

            con.Close();
           
            
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            countInfoFields(dt);
            populateStudents(dt);

        }

        private void countInfoFields(DataTable dt)
        {
            DataColumn sn = new DataColumn("SNo");
            dt.Columns.Add(sn);

            DataColumn ec = new DataColumn("EC");
            dt.Columns.Add(ec);

            if (cbANo.Checked)
            {
                DataColumn an = new DataColumn("App.No");
                dt.Columns.Add(an);
            }
            if (cbAdmissionThrough.Checked)
            {
                DataColumn at = new DataColumn("AdmissionThrough");
                dt.Columns.Add(at);
            }
            if (cbENo.Checked)
            {
                DataColumn dtcol3 = new DataColumn("EnrollmentNo");
                dt.Columns.Add(dtcol3);
            }
            if (cbICNo.Checked)
            {
                DataColumn dtcolicno = new DataColumn("ICardNo");
                dt.Columns.Add(dtcolicno);
            }
            if (cbAT.Checked)
            {
                DataColumn at = new DataColumn("AdmissionType");
                dt.Columns.Add(at);
            }
            if (cbSName.Checked)
            {
                DataColumn dtcol1 = new DataColumn("Student Name");
                dt.Columns.Add(dtcol1);
            }
            if (cbFName.Checked)
            {
                DataColumn dtcol2 = new DataColumn("Father Name");
                dt.Columns.Add(dtcol2);
            }
            if (cbBatch.Checked)
            {
                DataColumn dtcol9 = new DataColumn("Batch");
                dt.Columns.Add(dtcol9);
            }
            if (cbSCCode.Checked)
            {
                DataColumn dtcol7 = new DataColumn("SCCode");
                dt.Columns.Add(dtcol7);
            }
            if (cbCourse.Checked)
            {
                DataColumn dtcol8 = new DataColumn("Course");
                dt.Columns.Add(dtcol8);
            }
            if (cbYear.Checked)
            {
                DataColumn dtcol16 = new DataColumn("Year");
                dt.Columns.Add(dtcol16);
            }
            if (cbDOB.Checked)
            {
                DataColumn dtcol4 = new DataColumn("DOB");
                dt.Columns.Add(dtcol4);
            }
            if (cbGender.Checked)
            {
                DataColumn dtcol5 = new DataColumn("Gender");
                dt.Columns.Add(dtcol5);
            }
            
            if (cbNationality.Checked)
            {
                DataColumn dtcol6 = new DataColumn("Nationality");
                dt.Columns.Add(dtcol6);
            }

            if (cbCategory.Checked)
            {
                DataColumn dtcol10 = new DataColumn("Category");
                dt.Columns.Add(dtcol10);

            }

            if (cbAddress.Checked)
            {
                DataColumn dtcol11 = new DataColumn("Address");
                dt.Columns.Add(dtcol11);
            }
           
            if (cbPincode.Checked)
            {
                DataColumn dtcol12 = new DataColumn("Pincode");
                dt.Columns.Add(dtcol12);
            }
           
            
            if (cbPhoneNo.Checked)
            {
                DataColumn dtcol13 = new DataColumn("Phone No.");
                dt.Columns.Add(dtcol13);
            }
            if (cbMNo.Checked)
            {
                DataColumn dtcol14 = new DataColumn("Mob. No.");
                dt.Columns.Add(dtcol14);
            }
            if (cbEAddress.Checked)
            {
                DataColumn dtcol15 = new DataColumn("Email Address");
                dt.Columns.Add(dtcol15);
            }
           
            
        }

        private void populateStudents(DataTable dt)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand(findCommand(), con);

            SqlDataReader dr;
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();



            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;

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

                if (cbSName.Checked)
                {
                    drow["Student Name"] = dr["StudentName"].ToString();

                }
                if (cbFName.Checked)
                {
                    drow["Father Name"] = dr["FatherName"].ToString();

                }
                if (cbANo.Checked)
                {
                    drow["App.No"] = dr["ApplicationNo"].ToString();

                }
                if (cbAdmissionThrough.Checked)
                {
                    if (dr["AdmissionThrough"].ToString() != "")
                    {
                        drow["AdmissionThrough"] = FindInfo.findAdmissionThrough(Convert.ToInt32(dr["AdmissionThrough"]));
                    }
                    else
                    {
                        drow["AdmissionThrough"] = "NF";
                    }

                }
                if (cbENo.Checked)
                {
                    drow["EnrollmentNo"] = dr["EnrollmentNo"].ToString();

                }
                if (cbICNo.Checked)
                {
                    drow["ICardNo"] = dr["ICardNo"].ToString();

                }
                if (cbAT.Checked)
                {
                    if (dr["AdmissionType"].ToString() != "")
                    {
                        
                        drow["AdmissionType"] = FindInfo.findAdmissionType(Convert.ToInt32(dr["AdmissionType"]));
                    }
                    else
                    {
                        drow["AdmissionType"] = "NF";
                    }

                }
                if (cbDOB.Checked)
                {
                    drow["DOB"] = dr["DOBDay"].ToString() + " " + dr["DOBMonth"].ToString() + " " + dr["DOBYear"].ToString();

                }
                if (cbGender.Checked)
                {
                    drow["Gender"] = dr["Gender"].ToString();

                }
               
                if (cbNationality.Checked)
                {
                    drow["Nationality"] = dr["Nationality"].ToString();

                }
                if (cbSCCode.Checked)
                {
                    drow["SCCode"] = FindInfo.findCurrentSCCodeBySRID(Convert.ToInt32(dr["SRID"]));

                }

                if (cbCourse.Checked)
                {
                    drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));

                }
                
                if (cbBatch.Checked)
                {
                    drow["Batch"] = dr["Session"].ToString();

                }
               
                if (cbCategory.Checked)
                {
                    drow["Category"] = dr["Category"].ToString();

                }

                if (cbAddress.Checked)
                {
                    drow["Address"] = dr["CAddress"].ToString();

                }
               
                if (cbPincode.Checked)
                {
                    drow["Pincode"] = dr["Pincode"].ToString();

                }
                
                if (cbPhoneNo.Checked)
                {
                    drow["Phone No."] = dr["PhoneNo"].ToString();

                }
                if (cbMNo.Checked)
                {
                    drow["Mob. No."] = (dr["MobileNo"]).ToString();

                }
                if (cbEAddress.Checked)
                {
                    drow["Email Address"] = dr["Email"].ToString();

                }
               
                if (cbYear.Checked)
                {


                    drow["Year"] = FindInfo.findAlphaYear(dr["CYear"].ToString());

                }

                dt.Rows.Add(drow);
                i = i + 1;
            }

            if (i > 1)
            {
                gvShowStudent.Visible = true;
                Publish.Visible = true;
                lblMSG.Visible = false;

            }


            else
            {
                gvShowStudent.Visible = false;
                Publish.Visible = false;
                lblMSG.Visible = true;

            }

            dt.DefaultView.Sort = "EC ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }
         
            gvShowStudent.DataSource = dt;
            gvShowStudent.DataBind();
            //gvShowStudent.Columns[0].Visible = false;
          
            con.Close();

           
            
        }

        private string findCommand()
        {
            string cmnd = "";

            if (rblFilterByCourse.SelectedItem.Value == "1")
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
                                    cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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

                                    cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "'and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
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
                                    cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            }
                        }


                    }
                }
            }

            else if (rblFilterByCourse.SelectedItem.Value == "2")
            {
                if (rblMode.SelectedItem.Value == "1")
                {

                    if (ddlistBatch.SelectedItem.Text == "ALL")
                    {
                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {

                            
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            
                        }
                        else
                        {
                            
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                           
                        }

                    }

                    else
                    {
                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {

                           
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                           
                        }
                        else
                        {
                            
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
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

                           
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            
                        }
                        else
                        {
                           
                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                            
                        }

                    }

                    else
                    {
                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {

                                if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                           
                        }
                        else
                        {
                             if (ddlistYear.SelectedItem.Text == "ALL")
                                {

                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                }

                                else
                                {
                                    cmnd = "select * from DDEStudentRecord where Course in (" + findCourseList() + ") and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                }

                        }


                    }
                }
            }

            return cmnd;
        }

        private string findCourseList()
        {
            string course = "";
            foreach (DataListItem dli in dtlistGroup.Items)
            {

                CheckBox cn = (CheckBox)dli.FindControl("cbCourse");
                Label cid = (Label)dli.FindControl("lblCourse");

                if (course == "")
                {
                    if (cn.Checked)
                    {
                        course = cid.Text;
                    }
                }

                else
                {
                    if (cn.Checked)
                    {
                        course = course + "," + cid.Text;
                    }

                }
               

                
            }

            return course;
        }

      

        protected void Publish_Click(object sender, EventArgs e)
        {

            pnlInfoFields.Visible = false;
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

        protected void rblFilterByCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblFilterByCourse.SelectedItem.Value == "1")
            {
                pnlGroups.Visible = false;
                ddlistCourse.Enabled = true; 
            }

            else if (rblFilterByCourse.SelectedItem.Value == "2")
            {
                pnlGroups.Visible = true;
                ddlistCourse.Enabled = false;
            }
        }
    }
}
