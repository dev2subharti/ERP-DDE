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
    public partial class ShowOnlineApplications : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 96) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {
                    
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

        private void populateSCCodes()
        {
            ddlistSCCode.Items.Clear();

            ddlistSCCode.Items.Add("--SELECT ONE--");

            if (ddlistBatch.SelectedItem.Text != "--SELECT ONE--")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select distinct StudyCentreCode from DDEPendingStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and SCStatus='O' and (AdmissionStatus='0' or AdmissionStatus='2') and Enrolled='False' union Select distinct PreviousSCCode as StudyCentreCode from DDEPendingStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and SCStatus='T' and AdmissionStatus='0' and Enrolled='False' order by StudyCentreCode", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr[0].ToString() != "")
                    {
                        ddlistSCCode.Items.Add(dr[0].ToString());
                    }

                }
                con.Close();
            }
        }

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEPendingStudentRecord.PSRID,DDEPendingStudentRecord.StudentName,DDEPendingStudentRecord.FatherName,DDEPendingStudentRecord.Session,DDECourse.CourseName,DDEPendingStudentRecord.CYear from DDEPendingStudentRecord inner join DDECourse on DDEPendingStudentRecord.Course=DDECourse.CourseID where DDEPendingStudentRecord.Session='" + ddlistBatch.SelectedItem.Text + "' and (DDEPendingStudentRecord.AdmissionStatus='0' or DDEPendingStudentRecord.AdmissionStatus='2') and ((SCStatus='O' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (SCStatus='T' and PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "'))  order by PSRID", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("PSRID");
           
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
           
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("Session");
         

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
           
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
          
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["PSRID"] = Convert.ToString(dr["PSRID"]);
            
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);

                drow["Course"] = Convert.ToString(dr["CourseName"]);
                drow["Session"] = Convert.ToString(dr["Session"]);
               
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowPending.DataSource = dt;
            dtlistShowPending.DataBind();

            con.Close();

            if (i > 1)
            {

                dtlistShowPending.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;


            }

            else
            {
                dtlistShowPending.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowPending_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Session["ComingFrom"] = "ALL";
            Response.Redirect("DStudentRegistration.aspx?PSRID="+e.CommandArgument);
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            if (ddlistSCCode.SelectedItem.Text == "995")
            {
                if (Convert.ToInt32(Session["ERID"]) == 2556)
                {
                    if (rblSearchType.SelectedItem.Value == "1")
                    {
                        Session["ComingFrom"] = "ALL";
                        Response.Redirect("DStudentRegistration.aspx?PSRID=" + tbPANo.Text);

                    }
                    if (rblSearchType.SelectedItem.Value == "3")
                    {
                        Session["ComingFrom"] = "ALL";
                        Response.Redirect("DStudentRegistration.aspx?PSRID=" + FindInfo.findPSRIDBySCFormCounter(Convert.ToInt32(tbPANo.Text), "999"));

                    }
                    else if (rblSearchType.SelectedItem.Value == "2")
                    {
                        if (ddlistBatch.SelectedItem.Text != "--SELECT ONE--" && ddlistSCCode.SelectedItem.Text != "--SELECT ONE--")
                        {
                            populateStudents();
                            setAccessibility();
                        }
                        else
                        {
                            dtlistShowPending.Visible = false;
                            lblMSG.Text = "Please select any Batch and SCCode.";
                            pnlMSG.Visible = true;
                        }

                    }
                }
                else
                {
                    dtlistShowPending.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found.";
                    pnlMSG.Visible = true;
                }

            }
            else
            {
                if (rblSearchType.SelectedItem.Value == "1")
                {
                    Session["ComingFrom"] = "ALL";
                    Response.Redirect("DStudentRegistration.aspx?PSRID=" + tbPANo.Text);

                }
                if (rblSearchType.SelectedItem.Value == "3")
                {
                    Session["ComingFrom"] = "ALL";
                    Response.Redirect("DStudentRegistration.aspx?PSRID=" + FindInfo.findPSRIDBySCFormCounter(Convert.ToInt32(tbPANo.Text), "NA"));

                }
                else if (rblSearchType.SelectedItem.Value == "2")
                {
                    if (ddlistBatch.SelectedItem.Text != "--SELECT ONE--" && ddlistSCCode.SelectedItem.Text != "--SELECT ONE--")
                    {
                        populateStudents();
                        setAccessibility();
                    }
                    else
                    {
                        dtlistShowPending.Visible = false;
                        lblMSG.Text = "Please select any Batch and SCCode.";
                        pnlMSG.Visible = true;
                    }

                }
            }
           
        }

        private void setAccessibility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 96))
            {
                foreach (DataListItem dli in dtlistShowPending.Items)
                {
                    LinkButton show = (LinkButton)dli.FindControl("lnkbtnShowDetails");
                    show.Visible = true;                 
                }
            }
        }

        protected void rblSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSearchType.SelectedItem.Value == "1")
            {
                lblPANo.Text = "Pro. ANo.";
                lblPANo.Visible = true;
                tbPANo.Visible = true;
                lblSCCode.Visible = false;
                ddlistSCCode.Visible = false;
                lblBatch.Visible = false;
                ddlistBatch.Visible = false;

            }
           else if (rblSearchType.SelectedItem.Value == "2")
            {
                lblPANo.Visible = false;
                tbPANo.Visible = false;
                lblSCCode.Visible = true;
                ddlistSCCode.Visible = true;
                lblBatch.Visible = true;
                ddlistBatch.Visible = true;

            }
            else if (rblSearchType.SelectedItem.Value == "3")
            {
                lblPANo.Text = "Form Counter";
                lblPANo.Visible = true;
                tbPANo.Visible = true;
                lblSCCode.Visible = false;
                ddlistSCCode.Visible = false;
                lblBatch.Visible = false;
                ddlistBatch.Visible = false;

            }

            dtlistShowPending.Visible = false;
        }

        protected void ddlistBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           populateSCCodes();

           dtlistShowPending.Visible = false;
           pnlMSG.Visible = false;
           
           
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowPending.Visible = false;
            pnlMSG.Visible = false;
        }
      
    }
}