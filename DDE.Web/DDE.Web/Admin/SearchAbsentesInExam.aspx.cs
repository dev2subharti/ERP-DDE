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
    public partial class SearchAbsentesInExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 25))
            {
                if (!IsPostBack)
                {

                    PopulateDDList.populateExam(ddlistExam);
                    PopulateDDList.populateCourses(ddlistCourse);
                   
                    PopulateDDList.populateStudyCentre(ddlistStudyCentre);
                    PopulateDDList.populateBatch(ddlistSession);

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

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (ddlistCourse.SelectedItem.Text == "ALL")
            {
                if (ddlistSession.SelectedItem.Text == "ALL")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "'  and RecordStatus='True' order by StudentName ";
                }

                else
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "'  and RecordStatus='True' order by StudentName ";
                }

            }
            else
            {
                if (ddlistSession.SelectedItem.Text == "ALL")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "'  and RecordStatus='True' order by StudentName ";
                }

                else
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,StudyCentreCode from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and StudyCentreCode='" + ddlistStudyCentre.SelectedItem.Text + "'  and RecordStatus='True' order by StudentName ";
                }
            }



            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("SCCode");
            DataColumn dtcol6 = new DataColumn("SubjectCode");
            DataColumn dtcol7 = new DataColumn("SubjectName");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);


            Session["SNo"] = 1;
            int i=1;
            while (dr.Read())
            {
                if (FindInfo.examFeeSubmittedBySRID(Convert.ToInt32(dr["SRID"]),"", ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
                {
                    findAbsents(Convert.ToInt32(dr["SRID"]), dt, out i);
                }
                     
            }



            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();



            if (i > 1)
            {

                dtlistShowStudents.Visible = true;

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;

                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

            con.Close();

        }

        private void findAbsents(int srid,DataTable dt, out int i)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select distinct SRID from DDEMarkSheet_"+ddlistExam.SelectedItem.Value+" where SRID='" + srid + "' and Theory='AB' and MOE='"+ddlistMOE.SelectedItem.Value+"'", con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
           
            i =Convert.ToInt32(Session["SNo"]);
            if (dr.HasRows)
            {

                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["SRID"] = srid.ToString();
                    string[] sr = FindInfo.findStudentDetailBySRID(srid);
                    drow["EnrollmentNo"] = sr[0].ToString();
                    drow["StudentName"] = sr[1].ToString();
                    drow["SCCode"] = sr[3].ToString(); ;
                    drow["SubjectCode"] = "";
                    drow["SubjectName"] = "";
                    dt.Rows.Add(drow);
                    i = i + 1;
                }
            }

           
            con.Close();
            Session["SNo"] = i;
        }

       


        

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
           
        }

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateStudents();
            setSujects();
        }

        private void setSujects()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                              
               Label srid = (Label)dli.FindControl("lblSRID");
               DataList dl = (DataList)dli.FindControl("dtlistSubjects");

               SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

               SqlCommand cmd = new SqlCommand("Select SubjectID from DDEMarkSheet_" + ddlistExam.SelectedItem.Value + " where SRID='" + srid.Text + "' and Theory='AB' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);

               DataTable dt = new DataTable();

               DataColumn dtcol1 = new DataColumn("SubNo");
               DataColumn dtcol2 = new DataColumn("SubjectSNo");
               DataColumn dtcol3 = new DataColumn("SubjectCode");
               DataColumn dtcol4 = new DataColumn("SubjectName");



               dt.Columns.Add(dtcol1);
               dt.Columns.Add(dtcol2);
               dt.Columns.Add(dtcol3);
               dt.Columns.Add(dtcol4);

               con.Open();
               SqlDataReader dr;
               dr = cmd.ExecuteReader();

              
               if (dr.HasRows)
               {

                   while (dr.Read())
                   {
                       if (Convert.ToInt32(ddlistYear.SelectedItem.Value) == FindInfo.findYearOfSubject(Convert.ToInt32(dr["SubjectID"])))
                       {
                           DataRow drow = dt.NewRow();
                           string[] si = FindInfo.findSubjectInfoByID(Convert.ToInt32(dr["SubjectID"]));
                           
                           drow["SubjectCode"] = si[0].ToString();
                           drow["SubjectName"] = si[1].ToString();
                           drow["SubjectSNo"] = si[2].ToString();
                           dt.Rows.Add(drow);
                          
                       }
                       
                   }
               }
               dt.DefaultView.Sort = "SubjectSNo ASC";
               DataView dv = dt.DefaultView;


               int j = 1;
               foreach (DataRowView dvr in dv)
               {
                   dvr[0] = j;
                   j++;
               }
               dl.DataSource = dt;
               dl.DataBind();

               con.Close();
 
            }
        }

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }

        protected void ddlistStudyCentre_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }

        protected void ddlistYear_SelectedIndexChanged1(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }
    }
}
