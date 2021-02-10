using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DDE.DAL;
using System.IO;

namespace DDE.Web.Admin
{
    public partial class UploadQuestionPapers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 72))
            {
                if (!IsPostBack)
                {

                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("B15").Selected = true;
                    ddlistExamination.Enabled = false;

                    if (Request.QueryString["Year"] != null)
                    {
                        ddlistYear.Items.FindByValue(Request.QueryString["Year"]).Selected = true;
                        populateDateSheet();
                        populateSubjects();
                        populateQP();
                    }


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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateDateSheet();
            populateSubjects();
            populateQP();
        }

        private void populateQP()
        {
            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList dtlist = (DataList)dli.FindControl("dtlistShowSubjects");
                foreach (DataListItem dl in dtlist.Items)
                {
                    Label dsid = (Label)dl.FindControl("lblDSID");
                    Label fuploaded = (Label)dl.FindControl("lblfuploaded");
                    Label fname = (Label)dl.FindControl("lblfname");
                 
                    LinkButton delete = (LinkButton)dl.FindControl("lnkbtnDelete");
                    FileUpload fup = (FileUpload)dl.FindControl("fuQP");

                    if (fuploaded.Text == "True")
                    {
                        fup.Visible = false;
                    
                       
                        delete.Enabled = true;
                    }
                    else
                    {
                        fup.Visible = true;
                      
                        delete.Enabled =false;
                        delete.ForeColor = System.Drawing.Color.Gray;
                    }
                   
                   
                }
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton del = (LinkButton)sender;

            File.Delete(Server.MapPath("QuestionPapers/B15/" + FindInfo.findQPFileNameByDSID(Convert.ToInt32(del.CommandArgument))));

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEExaminationSchedules set QPFileName=@QPFileName where DSID ='" +del.CommandArgument+"'", con);
            cmd.Parameters.AddWithValue("@QPFileName", "");

            con.Open();
            cmd.ExecuteReader();
            con.Close();

           

            Log.createLogNow("Delete", "Deleted a Question Paper for Paper Code '" + del.CommandName + "' for June 2015 Examination", Convert.ToInt32(Session["ERID"].ToString()));

            populateSubjects();
            populateQP();
            
        } 

        private void populateSubjects()
        {
            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList subjects = (DataList)dli.FindControl("dtlistShowSubjects");
                Label date = (Label)dli.FindControl("lblDate");

                string fd = Convert.ToInt32(date.Text.Substring(3, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(0, 2)).ToString() + "-" + Convert.ToInt32(date.Text.Substring(6, 4)).ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDEExaminationSchedules where Date='" + Convert.ToDateTime(fd).ToString("yyyy-MM-dd") + "' and Year='" + ddlistYear.SelectedItem.Value + "' and ExaminationCode='B15' order by Date", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("DSID");
                DataColumn dtcol2 = new DataColumn("Time");
                //DataColumn dtcol3 = new DataColumn("Course");
                DataColumn dtcol4 = new DataColumn("SubjectCode");
                DataColumn dtcol5 = new DataColumn("PaperCode");
                DataColumn dtcol6 = new DataColumn("SubjectName");
                DataColumn dtcol7 = new DataColumn("QPFileName");
                DataColumn dtcol8 = new DataColumn("QPFileURL");
                DataColumn dtcol9 = new DataColumn("fuploaded");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                //dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);
                dt.Columns.Add(dtcol8);
                dt.Columns.Add(dtcol9);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["DSID"] = ds.Tables[0].Rows[i]["DSID"].ToString();
                    drow["Time"] = ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeFrom"].ToString().Substring(6, 1)) + " - " + ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(0, 6) + findSection(ds.Tables[0].Rows[i]["TimeTo"].ToString().Substring(6, 1));
                    //drow["Course"] = FindInfo.findCoursesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    drow["SubjectCode"] = FindInfo.findSubjectCodesByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString());
                    drow["PaperCode"] = ds.Tables[0].Rows[i]["PaperCode"].ToString();
                    drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(ds.Tables[0].Rows[i]["PaperCode"].ToString(), ds.Tables[0].Rows[i]["SyllabusSession"].ToString());
                    drow["QPFileName"] =ds.Tables[0].Rows[i]["QPFileName"].ToString();
                    drow["QPFileURL"] = "QuestionPapers/B15/" + ds.Tables[0].Rows[i]["QPFileName"].ToString();
                    if (ds.Tables[0].Rows[i]["QPFileName"].ToString() == "")
                    {
                        drow["fuploaded"] = "False";
                    }
                    else
                    {
                        drow["fuploaded"] = "True";
                    }

                    if (!alreadyexits(drow["PaperCode"].ToString(), drow["SubjectName"].ToString(), dt))
                    {
                        dt.Rows.Add(drow);
                    }
                }

                subjects.DataSource = dt;
                subjects.DataBind();



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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct Date from DDEExaminationSchedules where Year='" + ddlistYear.SelectedItem.Value + "' and ExaminationCode='B15' order by Date", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("Date");


            dt.Columns.Add(dtcol1);


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]).ToString("dd-MM-yyyy");
                dt.Rows.Add(drow);
            }

            dtlistShowDS.DataSource = dt;
            dtlistShowDS.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowDS.Visible = true;
                btnUpload.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowDS.Visible = false;
                btnUpload.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }


        }

        protected void dtlistShowDS_ItemCommand(object source, DataListCommandEventArgs e)
        {
          
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string error;
            if (validEntry(out error))
            {
                foreach (DataListItem dli in dtlistShowDS.Items)
                {
                    DataList dtlist = (DataList)dli.FindControl("dtlistShowSubjects");
                    foreach (DataListItem dl in dtlist.Items)
                    {
                        Label dsid = (Label)dl.FindControl("lblDSID");
                        Label pc = (Label)dl.FindControl("lblPaperCode");

                        FileUpload fup = (FileUpload)dl.FindControl("fuQP");

                        string filename = fup.FileName;

                        if (!File.Exists("QuestionPapers/B15/" + filename))
                        {
                            if (filename != "")
                            {

                                fup.SaveAs(Server.MapPath("QuestionPapers/B15/" + filename));

                                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                                SqlCommand cmd = new SqlCommand("update DDEExaminationSchedules set QPFileName=@QPFileName where DSID='" + dsid.Text + "'", con);
                                cmd.Parameters.AddWithValue("@QPFileName", filename);

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                Log.createLogNow("Upload", "Uploaded a Question Paper for Paper Code '" + pc.Text + "' for June 2015 Examination", Convert.ToInt32(Session["ERID"].ToString()));

                            }
                        }
                    }
                }
                dtlistShowDS.Visible = false;
                btnUpload.Visible = false;
                lblMSG.Text = "Question Papers has been uploaded successfully !!";
                pnlMSG.Visible = true;
            }
            else
            {
                dtlistShowDS.Visible = false;
                btnUpload.Visible = false;
                lblMSG.Text = error;
                btnOK.Visible = true;
                pnlMSG.Visible = true;
            }

            
        }

        private bool validEntry(out string error)
        {
            bool valid = true;
            error = "";
            foreach (DataListItem dli in dtlistShowDS.Items)
            {
                DataList dtlist = (DataList)dli.FindControl("dtlistShowSubjects");
                foreach (DataListItem dl in dtlist.Items)
                {
                    Label pc = (Label)dl.FindControl("lblPaperCode");
                    FileUpload fup = (FileUpload)dl.FindControl("fuQP");

                    string filename = fup.FileName;
                    if (filename != "")
                    {

                        if (File.Exists(Server.MapPath("QuestionPapers/B15/" + filename)))
                        {
                            valid = false;
                            error = "The file uploaded for Paper Code '" + pc.Text + "' with name '" + filename + "' is already exist.";
                            break;

                        }
                        else if (FindInfo.QPFileExist(filename,ddlistExamination.SelectedItem.Value))
                        {
                            valid = false;
                            error = "The file uploaded for Paper Code '" + pc.Text + "' with name '" + filename + "' is already exist.";
                            break;
                        }
                    }
                }
            }

            return valid;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            btnOK.Visible = false;
            dtlistShowDS.Visible = true;
            btnUpload.Visible = true;
        }
    }
}