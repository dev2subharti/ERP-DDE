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

namespace DDE.Web.Admin
{
    public partial class ShowSLMRequirements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {
                if (!IsPostBack)
                {
                    lblDateTime.Text ="As on : " +DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                    populateCurrentSLMRequirements();
                    setColors();
                }

               
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

       
        private void populateCurrentSLMRequirements()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

           
            cmd.CommandText = "Select DDESLMIssueRecord.SLMRID,DDESLMIssueRecord.CID,DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNO,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDECourse.CourseName,DDESLMIssueRecord.Year,DDESLMLinking.SLMID,DDESLMMaster.SLMCode,DDESLMMaster.Lang,DDESLMMaster.Title,DDESLMMaster.PresentStock from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID inner join DDESLMLinking on DDESLMIssueRecord.CID=DDESLMLinking.CID and DDESLMIssueRecord.Year=DDESLMLinking.Year inner join DDESLMMaster on DDESLMMaster.SLMID=DDESLMLinking.SLMID inner join DDECourse on DDECourse.CourseID=DDESLMIssueRecord.CID where DDESLMIssueRecord.LNo='0' and DDESLMIssueRecord.SLMRID>='11087' order by DDEStudentRecord.EnrollmentNo";
           

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
           



            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //string[] detained = FindInfo.findAllDetainedStudentsForSLM();
                //int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                //if (!(pos > -1))
                //{
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i+1;
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
                  
               //}
            }

           
            DataTable slmlist = findSLMList(dt);

            dtlistSLM.DataSource = slmlist;
            dtlistSLM.DataBind();



            if (ds.Tables[0].Rows.Count > 1)
            {
              
                pnlData.Visible = true;
                dtlistSLM.Visible = true;
                lblMSG.Text = "";
                pnlMSG.Visible = false;


            }
            else
            {
                pnlData.Visible = false;
                dtlistSLM.Visible = false;           
                lblMSG.Text = "Sorry !! No Student is pending for SLM";
                pnlMSG.Visible = true;
            }
        }

        private void setColors()
        {
            foreach (DataListItem dli in dtlistSLM.Items)
            {
                Label dual = (Label)dli.FindControl("lblDual");
                Label ps = (Label)dli.FindControl("lblPS");
                Label tr = (Label)dli.FindControl("lblTR");

                if (dual.Text == "True")
                {
                    tr.BackColor = System.Drawing.Color.Yellow;
                }
                if ((Convert.ToInt32(ps.Text) >= 100) && (Convert.ToInt32(ps.Text) < 300))
                {
                    ps.BackColor = System.Drawing.Color.Orange;
                }
                else if (Convert.ToInt32(ps.Text) < 100)
                {
                    ps.BackColor = System.Drawing.Color.Red;
                    ps.ForeColor = System.Drawing.Color.White;
                }

            }
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
          
            int totalreq = 0;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
               
                totalreq = totalreq + Convert.ToInt32(dvr[8]);
            }
            
            ViewState["TotalReq"] = totalreq.ToString();
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

        

         protected void lblTotalRequired_OnLoad(Object sender, EventArgs e)
         {
             Label tottalreq = (Label)sender;
             tottalreq.Text = ViewState["TotalReq"].ToString();
         }
    }
}