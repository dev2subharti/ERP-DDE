using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class ShowExaminationCentreReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 64))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("Z11").Selected = true;
                    ddlistExam.Enabled = false;
                    PopulateDDList.populateExamCentreByExam(ddlistEC,"Z11");
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    setCurrentDate();

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

        private void setCurrentDate()
        {
            ddlistDay.SelectedItem.Selected = false;
            ddlistMonth.SelectedItem.Selected = false;
            ddlistYear.SelectedItem.Selected = false;
            ddlistDay.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonth.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYear.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateDateSheet();
            populateSubjects();
            //populateExamCentres();

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
             LinkButton show=(LinkButton)sender;
             if (show.Text != "0")
             {
                 string date = ddlistDay.SelectedItem.Text + "-" + ddlistMonth.SelectedItem.Value + "-" + ddlistYear.SelectedItem.Text;
                 string timefrom = "";
                 string timeto = "";
                
                    if (rblShift.SelectedItem.Value == "1")
                    {
                        timefrom = "10:00 1";
                        timeto = "11:00 1";
                    }
                    else if (rblShift.SelectedItem.Value == "2")
                    {
                        timefrom = "12:00 3";
                        timeto = "01:00 2";
                    }
                    else if (rblShift.SelectedItem.Value == "3")
                    {
                       timefrom = "02:00 2";
                      timeto = "03:00 2";
                    }


                 Session["SRIDS"] = show.CommandArgument;
                 Session["ExamCode"] = ddlistExam.SelectedItem.Value;
                 Session["MOE"] = ddlistMOE.SelectedItem.Value;
                 Session["Heading"] = "Examination Centre : " + ddlistEC.SelectedItem.Text + "<br/> Paper Code : " + show.CommandName + "<br/> Date : " + date + "<br/> Time : " + timefrom + " To " + timeto;
                 Response.Redirect("ShowECStudents.aspx");
             }
        }

        //private void populateExamCentres()
        //{
        //    string date = ddlistYear.SelectedItem.Text + "-" + ddlistMonth.SelectedItem.Value + "-" + ddlistDay.SelectedItem.Text;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
        //    SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_A14 where ECID='"+ddlistEC.SelectedItem.Value+"'", con);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);

        //    DataTable dt = new DataTable();
        //    DataColumn dtcol = new DataColumn("SNo");
        //    DataColumn dtcol1 = new DataColumn("ECID");
        //    DataColumn dtcol2 = new DataColumn("ECCode");
        //    DataColumn dtcol4 = new DataColumn("ECName");
        //    DataColumn dtcol5 = new DataColumn("Email");
        //    DataColumn dtcol6 = new DataColumn("QP");
        //    DataColumn dtcol7 = new DataColumn("TS");
        //    DataColumn dtcol8 = new DataColumn("SRIDS");


        //    dt.Columns.Add(dtcol);
        //    dt.Columns.Add(dtcol1);
        //    dt.Columns.Add(dtcol2);
        //    dt.Columns.Add(dtcol4);
        //    dt.Columns.Add(dtcol5);
        //    dt.Columns.Add(dtcol6);
        //    dt.Columns.Add(dtcol7);
        //    dt.Columns.Add(dtcol8);

        //    int j = 1;

        //    string selected = findSelectedPapers();
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        DataRow drow = dt.NewRow();
        //        drow["SNo"] = j;
        //        drow["ECID"] = ds.Tables[0].Rows[i]["ECID"].ToString();
        //        drow["ECCode"] = ds.Tables[0].Rows[i]["ExamCentreCode"].ToString();
        //        drow["ECName"] = ds.Tables[0].Rows[i]["CentreName"].ToString();
        //        drow["Email"] = ds.Tables[0].Rows[i]["Email"].ToString();
        //        string validpc;
        //        string qps = FindInfo.findQPOfTheDay(selected, Convert.ToInt32(drow["ECID"]), date, out validpc);
        //        drow["QP"]=findwithbreak(validpc) ;
              


        //        if (drow["QP"].ToString() != "NF" && drow["QP"].ToString() != "")
        //        {
        //            string[] qp = validpc.ToString().Split(',');
                   
        //            string ts = "";
        //            string anchor = "";
        //            for (int k = 0; k < qp.Length; k++)
        //            {
        //                string sr=FindInfo.findTotalSRIDByPCandECID(Convert.ToInt32(drow["ECID"]), qp[k]);
        //                if (sr != "")
        //                {
        //                    string[] tsrid = sr.Split(',');
        //                    ts = tsrid.Length.ToString();

        //                    if (anchor == "")
        //                    {
        //                        anchor = "<a href='ShowECStudents.aspx?SRIDS="+sr+"'>"+ts+"<a/>";
        //                    }
        //                    else
        //                    {
        //                        anchor = anchor + "<br/><a href='ShowECStudents.aspx?SRIDS=" + sr + "'>" + ts + "<a/>";
        //                    }
                           
        //                }
        //                else
        //                {
        //                    if (anchor == "")
        //                    {
        //                        anchor = "<a href='#'>0<a/>";
        //                    }
        //                    else
        //                    {
        //                        anchor =anchor+ "<br/><a href='#'>0<a/>";
        //                    }
        //                }


        //            }

                   
        //            drow["SRIDS"] = anchor;

        //            dt.Rows.Add(drow);
        //            j++;
        //        }


        //    }

        //    dtlistEC.DataSource = dt;
        //    dtlistEC.DataBind();

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        dtlistEC.Visible = true;
               
        //        pnlData.Visible = true;
        //        pnlMSG.Visible = false;
        //    }
        //    else
        //    {
        //        dtlistEC.Visible = false;
                
        //        lblMSG.Text = "Sorry !! No Examination Centre Found.";
        //        pnlMSG.Visible = true;
        //    }
        //}

        private string findwithbreak(string validpc)
        {
            string[] str = validpc.Split(',');

            string withbr = "";

            for (int i = 0; i < str.Length; i++ )
            {
                if (withbr == "")
                {
                    withbr = str[i];
                }
                else
                {
                    withbr =withbr +"<br/>"+ str[i];
                }
            }

            return withbr;
        }

        private string findSelectedPapers()
        {
            string selected = "";
            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                foreach (DataListItem dl in subjects.Items)
                {
                    Label pc = (Label)dl.FindControl("lblPaperCode");
                   

                 
                        if (selected == "")
                        {
                            selected = pc.Text;
                        }
                        else
                        {
                            selected = selected + "," + pc.Text;
                        }
                   
                }

            }
            return selected;
        }

        private void populateSubjects()
        {
            string timefrom = "";
            string timeto = "";

            if (rblShift.SelectedItem.Value == "1")
            {
                timefrom = "10:00 1";
                timeto = "11:00 1";
            }
            else if (rblShift.SelectedItem.Value == "2")
            {
                timefrom = "12:00 3";
                timeto = "01:00 2";
            }
            else if (rblShift.SelectedItem.Value == "3")
            {
                timefrom = "02:00 2";
                timeto = "03:00 2";
            }

            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                Label date = (Label)dli.FindControl("lblDate");

                string fd = Convert.ToInt32(date.Text.Substring(3, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(0, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(6, 4)).ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where ExaminationCode='"+ddlistExam.SelectedItem.Value+"' and Date='" + Convert.ToDateTime(fd).ToString("yyyy-MM-dd") + "' and TimeFrom='" + timefrom + "' and TimeTo='" + timeto + "' order by Year", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("DSID");
                DataColumn dtcol2 = new DataColumn("Time");
                DataColumn dtcol4 = new DataColumn("SubjectCode");
                DataColumn dtcol5 = new DataColumn("PaperCode");
                DataColumn dtcol6 = new DataColumn("SubjectName");
                DataColumn dtcol7 = new DataColumn("Year");
                DataColumn dtcol8 = new DataColumn("TS");
                DataColumn dtcol9 = new DataColumn("SRIDS");


                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);
                dt.Columns.Add(dtcol8);
                dt.Columns.Add(dtcol9);
                //string tssrid = FindInfo.findAllSRIDByECID(Convert.ToInt32(ddlistEC.SelectedItem.Value),ddlistExam.SelectedItem.Value);
                string tssrid = FindInfo.findAllSRIDByECIDandMOE(Convert.ToInt32(ddlistEC.SelectedItem.Value), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value);
                int total = 0;
                string tsr="";
              

                int counter = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["DSID"] = ds.Tables[0].Rows[i]["DSID"].ToString();
                    drow["Time"] = ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(6, 1)) + " - " + ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(6, 1));
                    drow["SubjectCode"] = FindInfo.findSubjectCodesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    drow["PaperCode"] = ds.Tables[0].Rows[i]["PaperCode"].ToString();
                    drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString(), ds.Tables[0].Rows[i]["SyllabusSession"].ToString());
                    drow["Year"] = ds.Tables[0].Rows[i]["Year"].ToString();

                    
                    string sr = FindInfo.findTotalSRIDByPCandECID(tssrid, drow["PaperCode"].ToString(),ddlistExam.SelectedItem.Value,ddlistMOE.SelectedItem.Value);
                   
                    if (sr != "")
                    {
                         if(tsr=="")
                         {
                            tsr=sr;
                         }
                         else
                         {
                             tsr =tsr+","+sr;
                         }
                        string[] tsrid = sr.Split(',');
                        drow["TS"] = tsrid.Length.ToString();
                        drow["SRIDS"] = sr;
                        counter = counter + 1;
                        total = total + Convert.ToInt32(drow["TS"]);
                       
                    }
                    else
                    {
                        drow["TS"] = "0";
                        drow["SRIDS"] = null;           
                       
                    }

                  

                    if (!alreadyexits(drow["PaperCode"].ToString(), drow["SubjectName"].ToString(), dt))
                    {
                        dt.Rows.Add(drow);
                    }
                }

                Session["STR"] = dt;
                subjects.DataSource = dt;
                subjects.DataBind();

                lnkbtnTotal.Text = "Total : " + total.ToString();
                lnkbtnTotal.CommandArgument = tsr;



            }
        }

        private bool alreadyexits(string pc, string sn, DataTable dt)
        {
            bool exist = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["PaperCode"].ToString() == pc && row["SubjectName"].ToString() == sn)
                {
                    exist = true;
                    break;
                }
            }

            return exist;
        }

        private string findSection(string sec)
        {
            if (sec == "1")
            {
                return "AM";
            }
            else if (sec == "2")
            {
                return "PM";
            }
            else if (sec == "3")
            {
                return "NOON";
            }
            else
            {
                return "NF";
            }
        }

        private void populateDateSheet()
        {
            string date = ddlistYear.SelectedItem.Text + "-" + ddlistMonth.SelectedItem.Value + "-" + ddlistDay.SelectedItem.Text;
            string timefrom = "";
            string timeto = "";

            if (rblShift.SelectedItem.Value == "1")
            {
                timefrom = "10:00 1";
                timeto = "11:00 1";
            }
            else if (rblShift.SelectedItem.Value == "2")
            {
                timefrom = "12:00 3";
                timeto = "01:00 2";
            }
            else if (rblShift.SelectedItem.Value == "3")
            {
                timefrom = "02:00 2";
                timeto = "03:00 2";
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where ExaminationCode='"+ddlistExam.SelectedItem.Value+"' and Date='" + date + "' and TimeFrom='" + timefrom + "' and TimeTo='" + timeto + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("Date");


            dt.Columns.Add(dtcol1);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drow = dt.NewRow();
                drow["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"]).ToString("dd-MM-yyyy");
                dt.Rows.Add(drow);
            }

            dtlistShowDS.DataSource = dt;
            dtlistShowDS.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowDS.Visible = true;
              
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowDS.Visible = false;
               
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }


        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            btnOK.Visible = false;
            dtlistShowDS.Visible = true;

        }

        protected void lnkbtnTotal_Click(object sender, EventArgs e)
        {
            string date = ddlistDay.SelectedItem.Text + "-" + ddlistMonth.SelectedItem.Value + "-" + ddlistYear.SelectedItem.Text;
            string timefrom = "";
            string timeto = "";

            if (rblShift.SelectedItem.Value == "1")
            {
                timefrom = "10:00 1";
                timeto = "11:00 1";
            }
            else if (rblShift.SelectedItem.Value == "2")
            {
                timefrom = "12:00 3";
                timeto = "01:00 2";
            }
            else if (rblShift.SelectedItem.Value == "3")
            {
                timefrom = "02:00 2";
                timeto = "03:00 2";
            }




            Session["SRIDS"] = lnkbtnTotal.CommandArgument;
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["MOE"] = ddlistMOE.SelectedItem.Value;
            Session["Heading"] = "Examination Centre : " + ddlistEC.SelectedItem.Text + "<br/> Date : " + date + "<br/> Time : " + timefrom + " To " + timeto;
            Response.Redirect("ShowECStudents.aspx");
        }

         
    }
}