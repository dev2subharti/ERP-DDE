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
    public partial class AllotApplications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 124))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExaminers(ddlistExaminers,"Z10");
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
                SqlCommand cmd = new SqlCommand("Select distinct StudyCentreCode from DDEPendingStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and SCStatus='O' and AdmissionStatus='0' and Enrolled='False' and Eligible='' and DocUploaded='True' and ExID='0' union Select distinct PreviousSCCode as StudyCentreCode from DDEPendingStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and SCStatus='T' and AdmissionStatus='0' and Enrolled='False' and Eligible='' and DocUploaded='True' and ExID='0' order by StudyCentreCode", con);
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
            SqlCommand cmd = new SqlCommand("Select DDEPendingStudentRecord.PSRID,DDEPendingStudentRecord.StudentName,DDEPendingStudentRecord.FatherName,DDEPendingStudentRecord.Session,DDECourse.CourseName,DDEPendingStudentRecord.CYear from DDEPendingStudentRecord inner join DDECourse on DDEPendingStudentRecord.Course=DDECourse.CourseID where DDEPendingStudentRecord.AdmissionStatus='0' and DDEPendingStudentRecord.Session='" + ddlistBatch.SelectedItem.Text + "' and ((SCStatus='O' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "') or (SCStatus='T' and PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "')) and Enrolled='False' and Eligible='' and DocUploaded='True' and ExID='0'  order by PSRID", con);
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
                btnSelectAll.Visible = true;
                dtlistShowPending.Visible = true;
                pnlAllot.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;


            }

            else
            {
                btnSelectAll.Visible = false;
                dtlistShowPending.Visible = false;
                pnlAllot.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
         
               if (ddlistBatch.SelectedItem.Text != "--SELECT ONE--" && ddlistSCCode.SelectedItem.Text != "--SELECT ONE--")
                {
                        populateStudents();
                       
                 }
                 else
                 {
                     btnSelectAll.Visible = false;
                     dtlistShowPending.Visible = false;
                     pnlAllot.Visible = false;
                     lblMSG.Text = "Please select any Batch and SCCode.";
                     pnlMSG.Visible = true;
                  }

               
           

        }

        protected void ddlistBatch_SelectedIndexChanged(object sender, EventArgs e)
        {

            populateSCCodes();

            btnSelectAll.Visible = false;
            dtlistShowPending.Visible = false;
            pnlAllot.Visible = false;
            pnlMSG.Visible = false;


        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectAll.Visible = false;
            dtlistShowPending.Visible = false;
            pnlAllot.Visible = false;
            pnlMSG.Visible = false;
        }

        protected void btnAllot_Click(object sender, EventArgs e)
        {
            int counter = 0;
            if (ddlistExaminers.SelectedItem.Text != "--SELECT ONE--")
            {
                foreach (DataListItem dli in dtlistShowPending.Items)
                {
                    Label psrid = (Label)dli.FindControl("lblPSRID");
                    CheckBox cb = (CheckBox)dli.FindControl("cbSelect");

                    if (cb.Checked == true)
                    {
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd2 = new SqlCommand("update DDEPendingStudentRecord set ExID=@ExID where PSRID='" + Convert.ToInt32(psrid.Text) + "'", con2);

                        cmd2.Parameters.AddWithValue("@ExID", Convert.ToInt32(ddlistExaminers.SelectedItem.Value));

                        con2.Open();
                        int j = cmd2.ExecuteNonQuery();

                        con2.Close();

                        counter = counter + j;
                    }


                }

                btnSelectAll.Visible = false;
                dtlistShowPending.Visible = false;
                pnlAllot.Visible = false;
                lblMSG.Text = counter.ToString() + " Applications Allotted Successfully.";
                pnlMSG.Visible = true;
            }
            else
            {
                btnSelectAll.Visible = false;
                dtlistShowPending.Visible = false;
                pnlAllot.Visible = false;
                lblMSG.Text ="Sorry you did not select any faculty";
                pnlMSG.Visible = true;
            }
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowPending.Items)
            {
               
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");

                cb.Checked = true;
            }
        }
    }
}