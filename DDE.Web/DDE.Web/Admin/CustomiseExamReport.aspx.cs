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
using System.Text;
using System.IO;

namespace DDE.Web.Admin
{
    public partial class CustomiseExamReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {

                if (!IsPostBack)
                {
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
            populateList();
            setValidation();
        }

        private void setValidation()
        {
           
                foreach (GridViewRow dli in gvShowStudent.Rows)
                {

                    LinkButton fillef = (LinkButton)dli.FindControl("lnkbtnFillEF");
                    LinkButton showac = (LinkButton)dli.FindControl("lnkbtnShowAC");
                    string roll = dli.Cells[2].Text;
                    if (roll == "&nbsp;")
                    {
                        fillef.Visible = true;
                        showac.Visible = false;
                    }
                    else
                    {
                       
                        fillef.Visible = false;
                        showac.Visible = true;
                    }
                 

                }
           
        }

        private void populateList()
        {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand(findCommand());             
                cmd.Connection = con;
                                   
                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("SRID");
                DataColumn dtcol3 = new DataColumn("EnrollmentNo");
                DataColumn dtcol4 = new DataColumn("RollNo");
                DataColumn dtcol5 = new DataColumn("StudentName");
                DataColumn dtcol6 = new DataColumn("FatherName");
                DataColumn dtcol7 = new DataColumn("SCCode");
                DataColumn dtcol8 = new DataColumn("Batch");
                DataColumn dtcol9 = new DataColumn("AT");
                DataColumn dtcol10 = new DataColumn("Course");
                DataColumn dtcol11 = new DataColumn("CID");
                DataColumn dtcol12 = new DataColumn("Year");
                DataColumn dtcol13 = new DataColumn("ExFormFeeded");
              

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

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                int ex = 0;
            
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                     DataRow drow = dt.NewRow();
                     drow["SNo"] = i+1;
                     drow["SRID"] = ds.Tables[0].Rows[i]["SRID"].ToString();
                     drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                     drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                     drow["FatherName"] = ds.Tables[0].Rows[i]["FatherName"].ToString();
                     if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "O" || ds.Tables[0].Rows[i]["SCStatus"].ToString() == "C")
                     {
                         drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                     }
                     else if (ds.Tables[0].Rows[i]["SCStatus"].ToString() == "T")
                     {
                         drow["SCCode"] =ds.Tables[0].Rows[i]["StudyCentreCode"].ToString()+"("+ ds.Tables[0].Rows[i]["PreviousSCCode"].ToString()+")";
                     }
                    
                     drow["Batch"] = ds.Tables[0].Rows[i]["Session"].ToString();
                     if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "1")
                     {
                         drow["AT"] = "R";
                     }
                     else if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "2")
                     {
                         drow["AT"] = "CT";
                     }
                     else if (ds.Tables[0].Rows[i]["AdmissionType"].ToString() == "3")
                     {
                         drow["AT"] = "LE";
                     }
                   
                     drow["Year"] = ds.Tables[0].Rows[i]["ForYear"].ToString();
                     if (ds.Tables[0].Rows[i]["CourseShortName"].ToString() == "MBA")
                     {
                         try
                         {
                             if (ds.Tables[0].Rows[i]["ForYear"].ToString() == "1")
                             {
                                 drow["Course"] = "MBA";
                                 drow["CID"] = "76";
                             }
                             else if (ds.Tables[0].Rows[i]["ForYear"].ToString() == "2")
                             {
                                 drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                                 drow["CID"] = ds.Tables[0].Rows[i]["Course2Year"].ToString();
                             }
                             else if (ds.Tables[0].Rows[i]["ForYear"].ToString() == "3")
                             {
                                 drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                                 drow["CID"] = ds.Tables[0].Rows[i]["Course3Year"].ToString();
                             }
                         }
                         catch
                         {
                             drow["Course"] = "NOT FOUND";
                         }
                     }
                     else
                     {
                         if (ds.Tables[0].Rows[i]["Specialization"].ToString() == "")
                         {
                             drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString();
                             drow["CID"] = ds.Tables[0].Rows[i]["Course"].ToString();
                         }
                         else
                         {
                             drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString() + " (" + ds.Tables[0].Rows[i]["Specialization"].ToString() + ")";
                             drow["CID"] = ds.Tables[0].Rows[i]["Course"].ToString();
                         }
                     }

                     drow["RollNo"] = FindInfo.findRollNoBySRID1(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]),Convert.ToInt32(drow["Year"]), ddlistExam.SelectedItem.Value,"R");

                    
                     if (drow["RollNo"].ToString() != "")
                     {
                         drow["ExFormFeeded"] = "Yes";
                         ex = ex + 1;
                     }
                     else
                     {
                         drow["ExFormFeeded"] = "No";
                     }
                        
                     dt.Rows.Add(drow);
                        
                }

                gvShowStudent.DataSource = dt;
                gvShowStudent.DataBind();

                      
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvShowStudent.Visible = true;
                    btnExport.Visible = true;
                    pnlMSG.Visible = false;
                    lblAd.Text = "Admission Form Entered : " + ds.Tables[0].Rows.Count;
                    lblExam.Text = "Examination Form Entered : " + ex;
                    lblRem.Text = "Remaining Exam Form : " + (Convert.ToInt32(ds.Tables[0].Rows.Count) - ex);
                    lblAd.Visible = true;
                    lblExam.Visible = true;
                    lblRem.Visible = true;


                }
                else
                {
                    gvShowStudent.Visible = false;
                    btnExport.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found";
                    pnlMSG.Visible = true;
                    lblAd.Text = "";
                    lblExam.Text = "";
                    lblRem.Text = "";
                    lblAd.Visible = false;
                    lblExam.Visible = false;
                    lblRem.Visible = false;
                }
           
           

        }

        private string findCommand()
        {
            StringBuilder sb = new StringBuilder();

           
                sb.Append("select distinct [DDEFeeRecord_2013-14].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2013-14].[ForYear],");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode,");
                sb.Append("DDEStudentRecord.AdmissionType");

                sb.Append(" from [DDEFeeRecord_2013-14] ");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2013-14].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
              
                sb.Append(" where [DDEFeeRecord_2013-14].FeeHead='1' and [DDEFeeRecord_2013-14].ForExam='"+ddlistExam.SelectedItem.Value+"'");
                sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2014-15].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2014-15].[ForYear],");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode, ");
                sb.Append("DDEStudentRecord.PreviousSCCode,");
                sb.Append("DDEStudentRecord.AdmissionType");

                sb.Append(" from [DDEFeeRecord_2014-15]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2014-15].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                sb.Append(" where [DDEFeeRecord_2014-15].FeeHead='1' and [DDEFeeRecord_2014-15].ForExam='"+ddlistExam.SelectedItem.Value+"'");
                sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2015-16].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2015-16].[ForYear],");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode,");
                sb.Append("DDEStudentRecord.AdmissionType");

                sb.Append(" from [DDEFeeRecord_2015-16]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2015-16].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");
               
                sb.Append(" where [DDEFeeRecord_2015-16].FeeHead='1' and [DDEFeeRecord_2015-16].ForExam='"+ddlistExam.SelectedItem.Value+"'");
                sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");
          
                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2016-17].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2016-17].[ForYear],");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode,");
                sb.Append("DDEStudentRecord.AdmissionType");

                sb.Append(" from [DDEFeeRecord_2016-17]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2016-17].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                sb.Append(" where [DDEFeeRecord_2016-17].FeeHead='1' and [DDEFeeRecord_2016-17].ForExam='" + ddlistExam.SelectedItem.Value + "'");
                sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2017-18].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2017-18].[ForYear],");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode,");
                sb.Append("DDEStudentRecord.AdmissionType");

                sb.Append(" from [DDEFeeRecord_2017-18]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2017-18].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                sb.Append(" where [DDEFeeRecord_2017-18].FeeHead='1' and [DDEFeeRecord_2017-18].ForExam='" + ddlistExam.SelectedItem.Value + "'");
                sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");

                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2018-19].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2018-19].[ForYear],");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode,");
                sb.Append("DDEStudentRecord.AdmissionType");

                sb.Append(" from [DDEFeeRecord_2018-19]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2018-19].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                sb.Append(" where [DDEFeeRecord_2018-19].FeeHead='1' and [DDEFeeRecord_2018-19].ForExam='" + ddlistExam.SelectedItem.Value + "'");
                sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");


                sb.Append(" union");

                sb.Append(" select distinct [DDEFeeRecord_2019-20].SRID,");
                sb.Append("DDEStudentRecord.[Session],");
                sb.Append("DDEStudentRecord.EnrollmentNo,");
                sb.Append("DDEStudentRecord.StudentName,");
                sb.Append("DDEStudentRecord.FatherName,");
                sb.Append("[DDEFeeRecord_2019-20].[ForYear],");
                sb.Append("DDEStudentRecord.Course,");
                sb.Append("DDEStudentRecord.Course2Year,");
                sb.Append("DDEStudentRecord.Course3Year,");
                sb.Append("DDECourse.CourseShortName,");
                sb.Append("DDECourse.Specialization,");
                sb.Append("DDEStudentRecord.SCStatus,");
                sb.Append("DDEStudentRecord.StudyCentreCode,");
                sb.Append("DDEStudentRecord.PreviousSCCode,");
                sb.Append("DDEStudentRecord.AdmissionType");

                sb.Append(" from [DDEFeeRecord_2019-20]");
                sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2019-20].SRID");
                sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

                sb.Append(" where [DDEFeeRecord_2019-20].FeeHead='1' and [DDEFeeRecord_2019-20].ForExam='" + ddlistExam.SelectedItem.Value + "'");
                sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");

            sb.Append(" union");

            sb.Append(" select distinct [DDEFeeRecord_2020-21].SRID,");
            sb.Append("DDEStudentRecord.[Session],");
            sb.Append("DDEStudentRecord.EnrollmentNo,");
            sb.Append("DDEStudentRecord.StudentName,");
            sb.Append("DDEStudentRecord.FatherName,");
            sb.Append("[DDEFeeRecord_2020-21].[ForYear],");
            sb.Append("DDEStudentRecord.Course,");
            sb.Append("DDEStudentRecord.Course2Year,");
            sb.Append("DDEStudentRecord.Course3Year,");
            sb.Append("DDECourse.CourseShortName,");
            sb.Append("DDECourse.Specialization,");
            sb.Append("DDEStudentRecord.SCStatus,");
            sb.Append("DDEStudentRecord.StudyCentreCode,");
            sb.Append("DDEStudentRecord.PreviousSCCode,");
            sb.Append("DDEStudentRecord.AdmissionType");

            sb.Append(" from [DDEFeeRecord_2020-21]");
            sb.Append(" inner join DDEStudentRecord on DDEStudentRecord.SRID=[DDEFeeRecord_2020-21].SRID");
            sb.Append(" inner join DDECourse on DDECourse.CourseID=DDEStudentRecord.Course");

            sb.Append(" where [DDEFeeRecord_2020-21].FeeHead='1' and [DDEFeeRecord_2020-21].ForExam='" + ddlistExam.SelectedItem.Value + "'");
            sb.Append(" and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "' )) ");


            sb.Append(" order by DDEStudentRecord.EnrollmentNo");

            return sb.ToString();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=Form_Entries_"+ddlistExam.SelectedItem.Value+"_"+ ddlistSCCode.SelectedItem.Text +".xls";
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
            lblAd.Visible = false;
            lblExam.Visible = false;
            lblRem.Visible = false;
            btnExport.Visible = false;
            gvShowStudent.Visible = false;
            pnlMSG.Visible = false;
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAd.Visible = false;
            lblExam.Visible = false;
            lblRem.Visible = false;
            btnExport.Visible = false;
            gvShowStudent.Visible = false;
            pnlMSG.Visible = false;
        }

        protected void gvShowStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvShowStudent.Rows[rowIndex];

            Label srid = (Label)row.FindControl("lblSRID");
            Label cid = (Label)row.FindControl("lblCID");

            if (e.CommandName == "FillEF")
            {
                LinkButton lnkbtnFillEF = (LinkButton)row.FindControl("lnkbtnFillEF");
                lnkbtnFillEF.Visible = false;
                Session["EFSRID"] = srid.Text;
                Session["EFENo"] = row.Cells[1].Text;
                Session["EFCID"] = cid.Text;
                Session["EFYear"] = row.Cells[9].Text;
                Session["EFExam"] = ddlistExam.SelectedItem.Value;
                Response.Redirect("FillFee.aspx?EFSRID=" + srid.Text);
            }
            else if (e.CommandName == "ShowAC")
            {
                Session["SRID"] = srid.Text;
                Session["CardType"] = "R";
                Session["Exam"] = ddlistExam.SelectedItem.Text;
                Session["ExamCode"] = ddlistExam.SelectedItem.Value;             
                Session["ACExamCode"] = ddlistExam.SelectedItem.Value;
                Response.Redirect("AdmitCardList2.aspx");
            }
        }
    }
}