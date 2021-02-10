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
    public partial class ShowASReceivedByEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 64) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 69))
            {
                if (!IsPostBack)
                {
                    populateTotalReceivedCopy();
                    lblEmp.Text = "Employee Name : " + FindInfo.findEmployeeNameByERID(Convert.ToInt32(Session["ReceivedBy"]));              
                    lblPeriod.Text ="Time Period : " + Convert.ToDateTime(Session["RecFrom"]).ToString("dd-MM-yyyy").Substring(0, 10) + " To " + Convert.ToDateTime(Session["RecTo"]).ToString("dd-MM-yyyy").Substring(0, 10);
                                    
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }


        }

        private void populateTotalReceivedCopy()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

            if (Session["PaperCode"].ToString() == "ALL")
            {
                cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where ReceivedBy='" + Convert.ToInt32(Session["ReceivedBy"]) + "' and TOR>='" + Convert.ToDateTime(Session["RecFrom"]) + "' and TOR<='" + Convert.ToDateTime(Session["RecTo"]) + "' order by TOR";
            }
            else
            {

                string subids = FindInfo.findSubjectIDsByPaperCode(Session["PaperCode"].ToString());
                if (subids == "")
                {
                    Session["SubjectID"]= subids = "0";
                }
                else
                {
                    Session["SubjectID"] = subids;
                }
            
                cmd.CommandText = "Select * from DDEAnswerSheetRecord_" + Session["ExamCode"].ToString() + " where SubjectID in (" + subids + ") and ReceivedBy='" + Convert.ToInt32(Session["ReceivedBy"]) + "' and  TOR>='" + Convert.ToDateTime(Session["RecFrom"]) + "' and TOR<='" + Convert.ToDateTime(Session["RecTo"]) + "' order by TOR";
                
            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("RollNo");
            DataColumn dtcol4 = new DataColumn("PaperCode");
            DataColumn dtcol5 = new DataColumn("Subject");
            DataColumn dtcol6 = new DataColumn("Course");
            DataColumn dtcol7 = new DataColumn("TOR");
            
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["EnrollmentNo"] = FindInfo.findENoByID(Convert.ToInt32(dr["SRID"]));
                drow["RollNo"] =FindInfo.findRollNoBySRID(Convert.ToInt32(dr["SRID"]),Session["ExamCode"].ToString(), dr["MOE"].ToString());
                string[] subinfo = FindInfo.findSubjectInfoByID2(Convert.ToInt32(dr["SubjectID"]));
                drow["PaperCode"] = subinfo[0];
                drow["Subject"] = subinfo[1];
                int subyear =Convert.ToInt32(subinfo[2]);
                string course = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), subyear);

                if(subyear==1)
                {
                    drow["Course"] = course + " - 1ST YEAR";
                }
                else if (subyear == 2)
                {
                    drow["Course"] = course + " - 2ND YEAR";
                }
                else if (subyear == 3)
                {
                    drow["Course"] = course + " - 3RD YEAR";
                }
                drow["TOR"] = Convert.ToDateTime(dr["TOR"]).ToString("dd-MM-yyyy hh:mm:ss tt");
                dt.Rows.Add(drow);
                i = i + 1;
            }

           

            gvAwarsSheet.DataSource = dt;
            gvAwarsSheet.DataBind();

            con.Close();

            if (i <= 1)
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
              
            }
            else
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
           
        }

        private object findRollNoBySRID(int srid, string moe)
        {
            string rollno = "NF";
            if (moe == "R")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select RollNo from ExamRecord_June13 where SRID='" + srid + "'", con);
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    rollno = dr[0].ToString();

                }

                con.Close();
            }
            else if (moe == "B")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select RollNo from DDEExamRecord_A13 where SRID='" + srid + "'", con);
                con.Open();
                SqlDataReader dr;


                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    rollno = dr[0].ToString();

                }

                con.Close();
            }
            return rollno;
        }

    }
}
