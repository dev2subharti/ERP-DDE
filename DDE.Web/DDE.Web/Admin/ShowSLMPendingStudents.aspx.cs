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
    public partial class ShowSLMPendingStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95))
            {

                if (!IsPostBack)
                {
                    populateEligibleSC();
                    PopulateDDList.populateCourses(ddlistCourse);
                    ddlistCourse.Items.Add("ALL");
                    setTodayDate();

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

        private void setTodayDate()
        {
            ddlistDOADayFrom.SelectedItem.Selected = false;
            ddlistDOAMonthFrom.SelectedItem.Selected = false;
            ddlistDOAYearFrom.SelectedItem.Selected = false;


            ddlistDOADayFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDOAMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDOAYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDOADayTo.SelectedItem.Selected = false;
            ddlistDOAMonthTo.SelectedItem.Selected = false;
            ddlistDOAYearTo.SelectedItem.Selected = false;


            ddlistDOADayTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistDOAMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistDOAYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        private void populateEligibleSC()
        {
            ddlistSCCode.Items.Clear();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;


            cmd.CommandText = "Select distinct DDEStudentRecord.StudyCentreCode from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID where DDESLMIssueRecord.LNo='0' and DDESLMIssueRecord.SLMRID>='11087' and (DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') union select distinct DDEStudentRecord.PreviousSCCode as StudyCentreCode from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID where DDESLMIssueRecord.LNo='0' and DDESLMIssueRecord.SLMRID>='11087' and (DDEStudentRecord.SCStatus='T') order by StudyCentreCode";
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListItem li=new ListItem();
                li.Text = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();
                if (!(ddlistSCCode.Items.Contains(li)))
                {
                    ddlistSCCode.Items.Add(ds.Tables[0].Rows[i]["StudyCentreCode"].ToString());
                }
             
            }

            ddlistSCCode.Items.Remove("995");
            ddlistSCCode.Items.Remove("997");
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateSLMPendingStudents1();
            
        }
        
        private void populateSLMPendingStudents1()
        {
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            if (rblMode.SelectedItem.Value == "1")
            {
                cmd.CommandText = "Select DDESLMIssueRecord.SLMRID,DDESLMIssueRecord.CID,DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNO,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseName,DDESLMIssueRecord.Year,DDESLMLinking.SLMID,DDESLMMaster.SLMCode,DDESLMMaster.Lang,DDESLMMaster.Title,DDESLMMaster.PresentStock from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID inner join DDESLMLinking on DDESLMIssueRecord.CID=DDESLMLinking.CID and DDESLMIssueRecord.Year=DDESLMLinking.Year inner join DDESLMMaster on DDESLMMaster.SLMID=DDESLMLinking.SLMID inner join DDECourse on DDECourse.CourseID=DDESLMIssueRecord.CID where DDESLMIssueRecord.LNo='0' and DDESLMIssueRecord.SLMRID>='11087' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "')) order by DDEStudentRecord.EnrollmentNo";
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text + " 00:00:01 AM";
                string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text + " 11:59:59 PM";

                cmd.CommandText = "Select DDESLMIssueRecord.SLMRID,DDESLMIssueRecord.CID,DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNO,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseName,DDESLMIssueRecord.Year,DDESLMLinking.SLMID,DDESLMMaster.SLMCode,DDESLMMaster.Lang,DDESLMMaster.Title,DDESLMMaster.PresentStock from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID inner join DDESLMLinking on DDESLMIssueRecord.CID=DDESLMLinking.CID and DDESLMIssueRecord.Year=DDESLMLinking.Year inner join DDESLMMaster on DDESLMMaster.SLMID=DDESLMLinking.SLMID inner join DDECourse on DDECourse.CourseID=DDESLMIssueRecord.CID where DDESLMIssueRecord.LNo='0' and DDESLMIssueRecord.SLMRID>='11087' and DDESLMIssueRecord.TOR>='" + from + "' and DDESLMIssueRecord.TOR<='" + to + "' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.Text + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "')) order by DDEStudentRecord.EnrollmentNo";
            }
           

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMRID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("CourseID");
            DataColumn dtcol9 = new DataColumn("Year");
            DataColumn dtcol10 = new DataColumn("SLMID");
            DataColumn dtcol11 = new DataColumn("SLMCode");
            DataColumn dtcol12 = new DataColumn("Lang");
            DataColumn dtcol13 = new DataColumn("Title");
            DataColumn dtcol14 = new DataColumn("PresentStock");

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


            int sn = 1;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string[] detained = FindInfo.findAllDetainedStudentsForSLM();
                int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                 if (!(pos > -1))
                 {
                     DataRow drow = dt.NewRow();
                     drow["SNo"] = sn;
                     drow["SLMRID"] = Convert.ToString(ds.Tables[0].Rows[i]["SLMRID"]);
                     drow["SRID"] = Convert.ToString(ds.Tables[0].Rows[i]["SRID"]);
                     drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                     drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                     drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                     drow["CourseID"] = Convert.ToString(ds.Tables[0].Rows[i]["CID"]);
                     drow["Course"] = Convert.ToString(ds.Tables[0].Rows[i]["CourseName"]);
                     drow["Year"] = Convert.ToString(ds.Tables[0].Rows[i]["Year"]);
                     drow["SLMID"] = Convert.ToString(ds.Tables[0].Rows[i]["SLMID"]);
                     drow["SLMCode"] = Convert.ToString(ds.Tables[0].Rows[i]["SLMCode"]);
                     drow["Lang"] = Convert.ToString(ds.Tables[0].Rows[i]["Lang"]);
                     drow["Title"] = Convert.ToString(ds.Tables[0].Rows[i]["Title"]);
                     drow["PresentStock"] = Convert.ToString(ds.Tables[0].Rows[i]["PresentStock"]);
                     dt.Rows.Add(drow);
                     sn = sn + 1;
                 }
            }

            DataTable stlist = findStudentList(dt);

            dtlistShowSLM.DataSource = stlist;
            dtlistShowSLM.DataBind();



            if (ds.Tables[0].Rows.Count > 1)
            {
                dtlistShowSLM.Visible = true;
                btnPublish.Visible = true;
                lblMSG.Text = "";
                pnlMSG.Visible = false;
                pnlSelect.Visible = true;

               
            }
            else
            {
                dtlistShowSLM.Visible = false;
                btnPublish.Visible = false;
                lblMSG.Text = "Sorry !! No Student is pending for SLM";
                pnlMSG.Visible = true;
                pnlSelect.Visible = false;
            }

        }

        private string findSLMRIDS(DataList dlist)
        {
            string slmrids = "";
          
          
           
            foreach (DataListItem  dli in dlist.Items)
            {
                Label slmrid = (Label)dli.FindControl("lblSLMRID");
                CheckBox select = (CheckBox)dli.FindControl("cbSelect");

                if (select.Checked == true)
                {
                    if (slmrids == "")
                    {
                        slmrids = slmrid.Text;
                    }
                    else
                    {
                        slmrids = slmrids + "," + slmrid.Text;
                    }
                }
            }

            return slmrids;
        }

        private DataTable findSLMList(DataTable dt)
        {
            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "SLMID");
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMCode");
            DataColumn dtcol3 = new DataColumn("Dual");
            DataColumn dtcol4 = new DataColumn("GroupID");
            DataColumn dtcol5 = new DataColumn("Title");
            DataColumn dtcol6 = new DataColumn("Lang");
            DataColumn dtcol7 = new DataColumn("PresentStock");
            DataColumn dtcol8 = new DataColumn("Quantity");

            distinctValues.Columns.Add(dtcol1);
            distinctValues.Columns.Add(dtcol2);
            distinctValues.Columns.Add(dtcol3);
            distinctValues.Columns.Add(dtcol4);
            distinctValues.Columns.Add(dtcol5);
            distinctValues.Columns.Add(dtcol6);
            distinctValues.Columns.Add(dtcol7);
            distinctValues.Columns.Add(dtcol8);


            distinctValues.Columns["SNo"].SetOrdinal(0);
            int total = 0;
            foreach (DataRow row in distinctValues.Rows)
            {
                int numberOfRecords = 0;
                DataRow[] rows;
                findSLMDetails(Convert.ToInt32(row["SLMID"]), row);
                rows = dt.Select("SLMID = '" + row["SLMID"].ToString()+"'");
                numberOfRecords = rows.Length;
                if (row["Dual"].ToString() == "True")
                {
                    row["Quantity"] = "0";
                    
                }
                else
                {
                    row["Quantity"] = numberOfRecords.ToString();
                }
               
            }


            distinctValues.DefaultView.Sort = "SLMCode ASC";
            DataView dv = distinctValues.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
                total = total + Convert.ToInt32(dvr[8]);
            }
            Session["TotalSLM"] = total.ToString();
            return distinctValues;
        }

        private void findSLMDetails(int slmid, DataRow row)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDESLMMaster where SLMID='" + slmid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {

                dr.Read();
                row["SLMCode"] = dr["SLMCode"].ToString();
                row["Dual"] = dr["Dual"].ToString();
                row["GroupID"] = dr["GroupID"].ToString();
                row["Title"] = dr["Title"].ToString();
                row["Lang"] = dr["Lang"].ToString();
                row["PresentStock"] = dr["PresentStock"].ToString();

            }

            con.Close();
        }

        private DataTable findSLMSetList(DataTable studentlist)
        {
            DataView view = new DataView(studentlist);
            DataTable distinctValues = view.ToTable(true, "CourseID", "Course", "Year");
            DataColumn sn = new DataColumn("SNo");
            DataColumn ts = new DataColumn("TotalStudents");
            distinctValues.Columns.Add(sn);
            distinctValues.Columns.Add(ts);
            distinctValues.Columns["SNo"].SetOrdinal(0);
            int k = 1;
            foreach (DataRow row in distinctValues.Rows)
            {
                int numberOfRecords = 0;
                DataRow[] rows;

                rows = studentlist.Select("Course = '" + row["Course"].ToString() + "' and Year='" + row["Year"].ToString() + "'");
                numberOfRecords = rows.Length;
                row["TotalStudents"] = numberOfRecords.ToString();

            }


            distinctValues.DefaultView.Sort = "Year ASC";
            DataView dv = distinctValues.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }
            
            return distinctValues;
        }

        private DataTable findStudentList(DataTable dt)
        {
            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true,"SLMRID", "SRID", "EnrollmentNo", "StudentName", "FatherName","CourseID","Course", "Year");
            DataColumn sn = new DataColumn("SNo");
           
            distinctValues.Columns.Add(sn);        
            distinctValues.Columns["SNo"].SetOrdinal(0);

          
            distinctValues.DefaultView.Sort = "EnrollmentNo ASC";
            DataView dv = distinctValues.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }

            return distinctValues;
        }

        private void populateSLMPendingStudents()
        {
            string slmrids = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (rblMode.SelectedItem.Value == "1")
            {
                cmd.CommandText = "Select DDESLMIssueRecord.SLMRID,DDESLMIssueRecord.CID,DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNO,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDECourse.CourseName,DDESLMIssueRecord.Year from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID inner join DDECourse on DDECourse.CourseID=DDESLMIssueRecord.CID where DDESLMIssueRecord.SCCode='" + ddlistSCCode.SelectedItem.Text + "' and DDESLMIssueRecord.LetterPublished='False' and DDESLMIssueRecord.SLMRID>='11087'";
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text + " 00:00:01 AM";
                string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text + " 11:59:59 PM";

                cmd.CommandText = "Select DDESLMIssueRecord.SLMRID,DDESLMIssueRecord.CID,DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNO,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDECourse.CourseName,DDESLMIssueRecord.Year from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID inner join DDECourse on DDECourse.CourseID=DDESLMIssueRecord.CID where DDESLMIssueRecord.SCCode='" + ddlistSCCode.SelectedItem.Text + "' and DDESLMIssueRecord.TOR>='" + from + "' and DDESLMIssueRecord.TOR<='" + to + "'  and DDESLMIssueRecord.LetterPublished='False' and DDESLMIssueRecord.SLMRID>='11087'";
            }
            
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMRID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("CourseID");
            DataColumn dtcol9 = new DataColumn("Year");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SLMRID"] = Convert.ToString(dr["SLMRID"]);
                if (slmrids == "")
                {
                    slmrids = Convert.ToString(dr["SLMRID"]);
                }
                else
                {
                    slmrids = slmrids + "," + Convert.ToString(dr["SLMRID"]);
                }
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["CourseID"] = Convert.ToString(dr["CID"]);
                drow["Course"] = Convert.ToString(dr["CourseName"]);
                
                drow["Year"] = Convert.ToString(dr["Year"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowSLM.DataSource = dt;
            dtlistShowSLM.DataBind();

            con.Close();


            if (i > 1)
            {
                dtlistShowSLM.Visible = true;
                btnPublish.Visible = true;
                lblMSG.Text = "";
                pnlMSG.Visible = false;

                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true,"CourseID", "Course", "Year");
                DataColumn sn = new DataColumn("SNo");
                DataColumn ts = new DataColumn("TotalStudents");
                distinctValues.Columns.Add(sn);
                distinctValues.Columns.Add(ts);
                distinctValues.Columns["SNo"].SetOrdinal(0);
                int k = 1;
                foreach (DataRow row in distinctValues.Rows)
                {
                    int numberOfRecords = 0;
                    DataRow[] rows;

                    rows = dt.Select("Course = '" + row["Course"].ToString() + "' and Year='" + row["Year"].ToString() + "'");
                    numberOfRecords = rows.Length;
                    row["TotalStudents"] = numberOfRecords.ToString();

                }


                distinctValues.DefaultView.Sort = "Year ASC";
                DataView dv = distinctValues.DefaultView;


                int j = 1;
                foreach (DataRowView dvr in dv)
                {
                    dvr[0] = j;
                    j++;
                }

                Session["SLMSet"] = distinctValues;
                Session["SLMRIDS"] = slmrids;
            }
            else
            {
                dtlistShowSLM.Visible = false;
                btnPublish.Visible = false;
                lblMSG.Text = "Sorry !! No Student is pending for SLM";
                pnlMSG.Visible = true;
            }

           
        

        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Session["SCCode"] = ddlistSCCode.SelectedItem.Text;

            string slmrids=findSLMRIDS(dtlistShowSLM);
           

            DataTable dt = findFinalSelectedDataTable(slmrids);
            
            DataTable slmlist = findSLMList(dt);
            Session["SLMList"] = slmlist;


            Session["SLMRIDS"] = slmrids;

            Response.Redirect("SLMLetter.aspx");
           
        }

        private DataTable findFinalSelectedDataTable(string slmrids)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDESLMIssueRecord.SLMRID,DDESLMIssueRecord.CID,DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNO,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseName,DDESLMIssueRecord.Year,DDESLMLinking.SLMID,DDESLMMaster.SLMCode,DDESLMMaster.Lang,DDESLMMaster.Title,DDESLMMaster.PresentStock from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID inner join DDESLMLinking on DDESLMIssueRecord.CID=DDESLMLinking.CID and DDESLMIssueRecord.Year=DDESLMLinking.Year inner join DDESLMMaster on DDESLMMaster.SLMID=DDESLMLinking.SLMID inner join DDECourse on DDECourse.CourseID=DDESLMIssueRecord.CID where DDESLMIssueRecord.SLMRID in ("+slmrids+") order by DDEStudentRecord.EnrollmentNo", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMRID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("CourseID");
            DataColumn dtcol9 = new DataColumn("Year");
            DataColumn dtcol10 = new DataColumn("SLMID");
            DataColumn dtcol11 = new DataColumn("SLMCode");
            DataColumn dtcol12 = new DataColumn("Lang");
            DataColumn dtcol13 = new DataColumn("Title");
            DataColumn dtcol14 = new DataColumn("PresentStock");

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
                string[] detained = FindInfo.findAllDetainedStudentsForSLM();
                int pos = Array.IndexOf(detained, dr["SRID"].ToString());

                if (!(pos > -1))
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["SLMRID"] = Convert.ToString(dr["SLMRID"]);
                    drow["SRID"] = Convert.ToString(dr["SRID"]);
                    drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                    drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                    drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                    drow["CourseID"] = Convert.ToString(dr["CID"]);
                    drow["Course"] = Convert.ToString(dr["CourseName"]);
                    drow["Year"] = Convert.ToString(dr["Year"]);
                    drow["SLMID"] = Convert.ToString(dr["SLMID"]);
                    drow["SLMCode"] = Convert.ToString(dr["SLMCode"]);
                    drow["Lang"] = Convert.ToString(dr["Lang"]);
                    drow["Title"] = Convert.ToString(dr["Title"]);
                    drow["PresentStock"] = Convert.ToString(dr["PresentStock"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }
            }

            DataTable stlist = findStudentList(dt);

            dtlistShowSLM.DataSource = stlist;
            dtlistShowSLM.DataBind();

            con.Close();

            return dt;
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rblMode.SelectedItem.Value == "1")
            {
                pnlDate.Visible = false;
                dtlistShowSLM.Visible = false;
                pnlSelect.Visible = false;
                btnPublish.Visible = false;
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                pnlDate.Visible = true;
                dtlistShowSLM.Visible = false;
                pnlSelect.Visible = false;
                btnPublish.Visible = false;
            }
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowSLM.Visible = false;
            pnlSelect.Visible = false;
            btnPublish.Visible = false;
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowSLM.Items)
            {
                Label cid = (Label)dli.FindControl("lblCourseID");
                CheckBox select = (CheckBox)dli.FindControl("cbSelect");

                if (ddlistCourse.SelectedItem.Text == "ALL")
                {

                    select.Checked = false;
                }
                else
                {
                    if (ddlistCourse.SelectedItem.Value == cid.Text)
                    {
                        select.Checked = false;
                    }
                }
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowSLM.Items)
            {
                Label cid = (Label)dli.FindControl("lblCourseID");
                CheckBox select = (CheckBox)dli.FindControl("cbSelect");

                if (ddlistCourse.SelectedItem.Text == "ALL")
                {

                    select.Checked = true;
                }
                else
                {
                    if (ddlistCourse.SelectedItem.Value == cid.Text)
                    {
                        select.Checked = true;
                    }
                }
            }
        }

        protected void btnRemoveYear_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowSLM.Items)
            {
                Label year = (Label)dli.FindControl("lblYear");
                CheckBox select = (CheckBox)dli.FindControl("cbSelect");

                if (year.Text == ddlistYear.SelectedItem.Value)
                {

                    select.Checked = false;
                }
               
            }
        }

        protected void btnSelectYear_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowSLM.Items)
            {
                Label year = (Label)dli.FindControl("lblYear");
                CheckBox select = (CheckBox)dli.FindControl("cbSelect");

                if (year.Text == ddlistYear.SelectedItem.Value)
                {

                    select.Checked = true;
                }
               
            }
        }
    }
}