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
    public partial class ShowListofAwardSheets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 70))
            {

               
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

        private void populateReport_A13()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            cmd.CommandText = "Select distinct SubjectID from DDEAnswerSheetRecord_A13";

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SubjectID");
            DataColumn dtcol3 = new DataColumn("PaperCode");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);

            cmd.Connection = con;

            con.Open();

            dr = cmd.ExecuteReader();

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SubjectID"] = Convert.ToInt32(dr["SubjectID"]);
                string[] subinfo = FindInfo.findSubjectInfoByID2(Convert.ToInt32(dr["SubjectID"]));
                drow["PaperCode"] = subinfo[0].ToString();
                //if (drow["PaperCode"].ToString() == "")
                //{
                //    Response.Write(drow["SubjectID"].ToString());
                //}         
                dt.Rows.Add(drow);
                i = i + 1;
            }



            con.Close();

            DataView view = new DataView(dt);
            DataTable pcodes = view.ToTable(true, "PaperCode");

            populateASReport(pcodes);
            
                
        }
           
        private void populateASReport(DataTable pcodes)
        {

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ASNo");
            DataColumn dtcol3 = new DataColumn("PaperCode");
            DataColumn dtcol4 = new DataColumn("SubjectName");
            DataColumn dtcol5 = new DataColumn("TotalStudents");
            DataColumn dtcol6 = new DataColumn("Present");
            DataColumn dtcol7 = new DataColumn("Absent");
            DataColumn dtcol8 = new DataColumn("NOP");
            DataColumn dtcol9 = new DataColumn("Period");
            DataColumn dtcol10 = new DataColumn("PrintMode");
            DataColumn dtcol11 = new DataColumn("PrintedBy");
            DataColumn dtcol12 = new DataColumn("AT");


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

            int total = 0;
           
            for (int i = 0; i < pcodes.Rows.Count; i++)
            {

                DataRow drow = dt.NewRow();
                drow["ASNo"] = "";
                drow["PaperCode"] = pcodes.Rows[i]["PaperCode"];
               

                if (ddlistExam.SelectedItem.Value == "A13")
                {
                    if (drow["PaperCode"].ToString() != "")
                    {
                        drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(drow["PaperCode"].ToString(), "A 2010-11");
                        findPrintInfo_A13(drow["PaperCode"].ToString(), drow);
                    }
                    else
                    {
                        drow["SubjectName"] = "Not Found";
                        findPrintInfo_A13(drow["PaperCode"].ToString(), drow);

                    }
                    if (ddlistType.SelectedItem.Text == "ALL")
                    {
                        dt.Rows.Add(drow);

                    }
                    else if (ddlistType.SelectedItem.Text == "PRINTED")
                    {
                        if (Convert.ToInt32(drow["NOP"]) > 0)
                        {
                            dt.Rows.Add(drow);

                        }
                    }
                    else if (ddlistType.SelectedItem.Text == "NOT PRINTED")
                    {
                        if (Convert.ToInt32(drow["NOP"]) == 0)
                        {
                            dt.Rows.Add(drow);

                        }
                    }
                }
                else if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10")
                {
                    drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(drow["PaperCode"].ToString(),"A 2010-11");

                    int pr;
                    int ab;
                    drow["TotalStudents"] = FindInfo.findTotalStudentsInASByPaperCode(drow["PaperCode"].ToString(), false, ddlistExam.SelectedItem.Value,Convert.ToInt32(rblMode.SelectedItem.Value), out pr,out ab);
                    total = total + pr;
                    drow["Present"]=pr.ToString();
                    drow["Absent"] = ab.ToString();
                    drow["NOP"] = "0";
                    drow["Period"] = "";
                    drow["PrintMode"] = "";
                    drow["PrintedBy"] = "";
                    drow["AT"] = "";
                    dt.Rows.Add(drow);
                }
                
               
            }

            dt.DefaultView.Sort = "PaperCode";
            DataView dv = dt.DefaultView;

            int k = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = k;
                k++;
            }

            dtlistASReport.DataSource=dt;
            dtlistASReport.DataBind();

            if (k > 1)
            {

                dtlistASReport.Visible = true;
                pnlMSG.Visible = false;
                lblTotal.Text = "Total Answer Sheets : " + total.ToString();
                lblTotal.Visible = true;

            }

            else
            {
                dtlistASReport.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
                lblTotal.Text = "";
                lblTotal.Visible = false;
            }

        }

        private void findPrintInfo_A13(string pcode, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEASPrintRecord_"+ddlistExam.SelectedItem.Value+" where PaperCode='" + pcode + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if(dr.HasRows)
            {
                dr.Read();
                drow["NOP"] =dr["Times"].ToString();
                drow["Period"] =dr["Period"].ToString();
            
            }
            else
            {
                drow["NOP"] = "0";
                drow["Period"] = "NA";
            }

               
            con.Close();
            
        }

        protected void dtlistASReport_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;

            if (ddlistType.SelectedItem.Text == "PRINTED")
            {
                Response.Redirect("PublishAwardSheet.aspx?ASNo=" + e.CommandName);
               
            }
            else if (ddlistType.SelectedItem.Text == "NOT PRINTED")
            {
                Session["ASFilter"] = rblMode.SelectedItem.Value;
                Response.Redirect("PublishAwardSheet.aspx?PaperCode=" + e.CommandArgument);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlistExam.SelectedItem.Value == "A13")
            {

               populateReport_A13();
            }
            else if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14" || ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10")
            {

                populateReport();
              
            }      

        }

        private void populateReport()
        {
            try
            {
                if (ddlistType.SelectedItem.Text == "PRINTED")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;


                    cmd.CommandText = "Select * from DDEASPrintRecord_" + ddlistExam.SelectedItem.Value + " where ASPRID>='" + tbFrom.Text + "' and ASPRID<='" + tbTo.Text + "'";

                    DataTable dt = new DataTable();
                    DataColumn dtcol1 = new DataColumn("SNo");
                    DataColumn dtcol2 = new DataColumn("ASNo");
                    DataColumn dtcol3 = new DataColumn("PaperCode");
                    DataColumn dtcol4 = new DataColumn("SubjectName");
                    DataColumn dtcol5 = new DataColumn("TotalStudents");
                    DataColumn dtcol6 = new DataColumn("Present");
                    DataColumn dtcol7 = new DataColumn("Absent");
                    DataColumn dtcol8 = new DataColumn("NOP");
                    DataColumn dtcol9 = new DataColumn("Period");
                    DataColumn dtcol10 = new DataColumn("PrintMode");
                    DataColumn dtcol11 = new DataColumn("PrintedBy");
                    DataColumn dtcol12 = new DataColumn("AT");


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

                    cmd.Connection = con;
                    con.Open();

                    dr = cmd.ExecuteReader();

                    int i = 1;
                    int total = 0;
                    while (dr.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i;
                        drow["ASNo"] = String.Format("{0:0000}", Convert.ToInt32(dr["ASPRID"]));
                        drow["PaperCode"] = dr["PaperCode"].ToString();
                        drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(dr["PaperCode"].ToString(), "A 2010-11");
                        int pr;
                        int ab;
                        drow["TotalStudents"] = FindInfo.findStudentsAttendenceByASNo(Convert.ToInt32(drow["ASNo"]), ddlistExam.SelectedItem.Value, out pr, out ab);
                        //if (drow["PaperCode"].ToString().Substring(0, 3) == "DCS" || drow["PaperCode"].ToString() == "DBA-202")
                        //{
                            total = total + pr;
                        //}
                        drow["Present"] = pr.ToString();
                        drow["Absent"] = ab.ToString();
                        drow["NOP"] = dr["Times"].ToString();
                        drow["Period"] =Convert.ToDateTime(dr["Period"]).ToString("dd-MM-yyyy");
                        if (ddlistExam.SelectedItem.Value == "A15" || ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17"|| ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18" || ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10")
                        {
                            drow["PrintMode"] = dr["PrintMode"].ToString();
                            drow["PrintedBy"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["PrintedBy"]));
                            drow["AT"] = FindInfo.findExaminerByID(Convert.ToInt32(dr["AllottedTo"]));
                        }
                        else
                        {
                            drow["PrintMode"] = "";
                            drow["PrintedBy"] = "";
                            drow["AT"] = "";
                        }

                       
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }

                    con.Close();

                    dtlistASReport.DataSource = dt;
                    dtlistASReport.DataBind();


                    if (i > 1)
                    {

                        dtlistASReport.Visible = true;
                        pnlMSG.Visible = false;
                        setColor();
                        lblTotal.Text = "Total Answer Sheets : " + total;
                        lblTotal.Visible = true;
                       

                    }

                    else
                    {
                        dtlistASReport.Visible = false;
                        lblMSG.Text = "Sorry !! No record found";
                        pnlMSG.Visible = true;
                        lblTotal.Visible = false;
                    }

                }

                else if (ddlistType.SelectedItem.Text == "NOT PRINTED")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;

                    if (rblMode.SelectedItem.Value == "1")
                    {

                        cmd.CommandText = "Select distinct DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".SubjectID,DDESubject.PaperCode from DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + " inner join DDESubject on DDESubject.SubjectID=DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".SubjectID where DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ASPRID='0' and (DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy!='2470' and DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy!='2552' and DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy!='2563' and DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy!='2566' and DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy!='2572')";

                    }
                  else if (rblMode.SelectedItem.Value == "2")
                    {

                        cmd.CommandText = "Select distinct DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".SubjectID,DDESubject.PaperCode from DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + " inner join DDESubject on DDESubject.SubjectID=DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".SubjectID where DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ASPRID='0' and (DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy='2470' or DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy='2552' or DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy='2563' or DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy='2566' or DDEAnswerSheetRecord_" + ddlistExam.SelectedItem.Value + ".ReceivedBy='2572' )";

                    }

                    DataTable dt = new DataTable();

                    DataColumn dtcol1 = new DataColumn("SNo");
                    DataColumn dtcol2 = new DataColumn("SubjectID");
                    DataColumn dtcol3 = new DataColumn("PaperCode");

                    dt.Columns.Add(dtcol1);
                    dt.Columns.Add(dtcol2);
                    dt.Columns.Add(dtcol3);

                    cmd.Connection = con;

                    con.Open();

                    dr = cmd.ExecuteReader();

                    int i = 1;
                    while (dr.Read())
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = i;
                        drow["SubjectID"] = Convert.ToInt32(dr["SubjectID"]);
                        drow["PaperCode"] = dr["PaperCode"].ToString();
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }

                    con.Close();

                    DataView view = new DataView(dt);
                    DataTable pcodes = view.ToTable(true, "PaperCode");

                    populateASReport(pcodes);
                    setColor();

                }
            }
            catch (Exception ex)
            {
                dtlistASReport.Visible = false;
                lblMSG.Text = ex.Message;
                pnlMSG.Visible = true;
            }
           
                      
       }

        private void setColor()
        {
            foreach (DataListItem dli in dtlistASReport.Items)
            {
                Label ts = (Label)dli.FindControl("lblTS");
                Label asno = (Label)dli.FindControl("lblASNo");
                Label pm = (Label)dli.FindControl("lblPM");

                if (ddlistType.SelectedItem.Text == "PRINTED")
                {
                    if (pm.Text == "2")
                    {
                        asno.BackColor = System.Drawing.Color.Orange;
                    }
                }

                else if (ddlistType.SelectedItem.Text == "NOT PRINTED")
                {

                    if (Convert.ToInt32(ts.Text) >= 100)
                    {
                        ts.BackColor = System.Drawing.Color.Red;
                        ts.ForeColor = System.Drawing.Color.White;
                    }
                  
                }
            }
        }

        protected void ddlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistASReport.Visible = false;
            lblTotal.Visible = false;

            if (ddlistType.SelectedItem.Text == "PRINTED")
            {
                pnlRange.Visible=true;
                pnlFilter.Visible = false;
            }
            else if (ddlistType.SelectedItem.Text == "NOT PRINTED")
            {
                pnlRange.Visible = false;
                pnlFilter.Visible = true;

            }

           
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistASReport.Visible = false;
            lblTotal.Visible = false;
        }

        
    }
}
