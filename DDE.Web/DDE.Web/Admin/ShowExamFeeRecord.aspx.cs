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
    public partial class ShowExamFeeRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 38))
            {
                if (!IsPostBack)
                {
                 
                    PopulateDDList.populateCourses(ddlistCourse);
                    PopulateDDList.populateExam(ddlistExam);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateBatch(ddlistSession);
                    PopulateDDList.populateExamCentre(ddlistExamCentre);
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

            populateStudents();
            dtlistShowStudents.Visible = true;
            btnOK.Visible = false;
           
        }

  
        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (ddlistExam.SelectedItem.Value == "B12")
            {
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    cmd.CommandText = "Select SRID from DDEExamRecord_B12 where ExamFee_A12='1000'";
                }

                else if (ddlistMOE.SelectedItem.Value == "B")
                {
                    cmd.CommandText = "Select distinct SRID from DDEFeeRecord where BPExamFee_A12!='' and BPExamFee_A12!='NULL' ";
                }
            }
            else
            {
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    cmd.CommandText = "Select distinct SRID from DDEFeeRecord where ExamFee_A12='1000'";
                }

                else if (ddlistMOE.SelectedItem.Value == "B")
                {
                    cmd.CommandText = "Select distinct SRID from DDEFeeRecord where BPExamFee_A12!='' and BPExamFee_A12!='NULL' ";
                }

            }

           

            cmd.Connection=con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();
            
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("RollNo");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("FatherName");
            DataColumn dtcol8 = new DataColumn("Course");
            DataColumn dtcol9 = new DataColumn("CourseID");
            DataColumn dtcol10 = new DataColumn("CYear");
            DataColumn dtcol11 = new DataColumn("Batch");
            DataColumn dtcol12 = new DataColumn("Subjects");
            DataColumn dtcol13 = new DataColumn("ExamCity");
            DataColumn dtcol14 = new DataColumn("ExamJone");
           
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
                DataRow drow = dt.NewRow();
                //drow["SNo"] = i;
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                populateStudentDetail(drow, Convert.ToInt32(drow["SRID"]));
                populateExamCentre(drow, Convert.ToInt32(drow["SRID"]));
                if (ddlistJone.SelectedItem.Text == "ALL")
                {
                    if (ddlistExamCentre.SelectedItem.Text == "ALL")
                    {

                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSession.SelectedItem.Text == "ALL")
                                {
                                    dt.Rows.Add(drow);
                                    i = i + 1;
                                }

                                else
                                {
                                    if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }
                                }
                            }

                            else
                            {
                                if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                {
                                    if (ddlistSession.SelectedItem.Text == "ALL")
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }

                                    else
                                    {
                                        if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }
                                    }
                                }

                            }


                        }

                        else
                        {
                            if (drow["SCCode"].ToString() == ddlistSCCode.SelectedItem.Text)
                            {
                                if (ddlistCourse.SelectedItem.Text == "ALL")
                                {
                                    if (ddlistSession.SelectedItem.Text == "ALL")
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }

                                    else
                                    {
                                        if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }
                                    }
                                }

                                else
                                {
                                    if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                    {
                                        if (ddlistSession.SelectedItem.Text == "ALL")
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }

                                        else
                                        {
                                            if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }
                                        }
                                    }

                                }

                            }

                        }
                    }


                    else
                    {
                        if (drow["ExamCity"].ToString() == ddlistExamCentre.SelectedItem.Text)
                        {
                            if (ddlistSCCode.SelectedItem.Text == "ALL")
                            {
                                if (ddlistCourse.SelectedItem.Text == "ALL")
                                {
                                    if (ddlistSession.SelectedItem.Text == "ALL")
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }

                                    else
                                    {
                                        if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }
                                    }
                                }

                                else
                                {
                                    if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                    {
                                        if (ddlistSession.SelectedItem.Text == "ALL")
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }

                                        else
                                        {
                                            if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }
                                        }
                                    }

                                }


                            }

                            else
                            {
                                if (drow["SCCode"].ToString() == ddlistSCCode.SelectedItem.Text)
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistSession.SelectedItem.Text == "ALL")
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }

                                        else
                                        {
                                            if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                        {
                                            if (ddlistSession.SelectedItem.Text == "ALL")
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }

                                            else
                                            {
                                                if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                                {
                                                    dt.Rows.Add(drow);
                                                    i = i + 1;
                                                }
                                            }
                                        }

                                    }

                                }

                            }
                        }


                    }
                }

                else if (ddlistJone.SelectedItem.Text == drow["ExamJone"].ToString())
                {
                    if (ddlistExamCentre.SelectedItem.Text == "ALL")
                    {

                        if (ddlistSCCode.SelectedItem.Text == "ALL")
                        {
                            if (ddlistCourse.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSession.SelectedItem.Text == "ALL")
                                {
                                    dt.Rows.Add(drow);
                                    i = i + 1;
                                }

                                else
                                {
                                    if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }
                                }
                            }

                            else
                            {
                                if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                {
                                    if (ddlistSession.SelectedItem.Text == "ALL")
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }

                                    else
                                    {
                                        if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }
                                    }
                                }

                            }


                        }

                        else
                        {
                            if (drow["SCCode"].ToString() == ddlistSCCode.SelectedItem.Text)
                            {
                                if (ddlistCourse.SelectedItem.Text == "ALL")
                                {
                                    if (ddlistSession.SelectedItem.Text == "ALL")
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }

                                    else
                                    {
                                        if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }
                                    }
                                }

                                else
                                {
                                    if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                    {
                                        if (ddlistSession.SelectedItem.Text == "ALL")
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }

                                        else
                                        {
                                            if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }
                                        }
                                    }

                                }

                            }

                        }
                    }


                    else
                    {
                        if (drow["ExamCity"].ToString() == ddlistExamCentre.SelectedItem.Text)
                        {
                            if (ddlistSCCode.SelectedItem.Text == "ALL")
                            {
                                if (ddlistCourse.SelectedItem.Text == "ALL")
                                {
                                    if (ddlistSession.SelectedItem.Text == "ALL")
                                    {
                                        dt.Rows.Add(drow);
                                        i = i + 1;
                                    }

                                    else
                                    {
                                        if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }
                                    }
                                }

                                else
                                {
                                    if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                    {
                                        if (ddlistSession.SelectedItem.Text == "ALL")
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }

                                        else
                                        {
                                            if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }
                                        }
                                    }

                                }


                            }

                            else
                            {
                                if (drow["SCCode"].ToString() == ddlistSCCode.SelectedItem.Text)
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistSession.SelectedItem.Text == "ALL")
                                        {
                                            dt.Rows.Add(drow);
                                            i = i + 1;
                                        }

                                        else
                                        {
                                            if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }
                                        }
                                    }

                                    else
                                    {
                                        if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                                        {
                                            if (ddlistSession.SelectedItem.Text == "ALL")
                                            {
                                                dt.Rows.Add(drow);
                                                i = i + 1;
                                            }

                                            else
                                            {
                                                if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                                                {
                                                    dt.Rows.Add(drow);
                                                    i = i + 1;
                                                }
                                            }
                                        }

                                    }

                                }

                            }
                        }


                    }

                }

              
            }

            dt.DefaultView.Sort = "SCCode ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;           
                j++;
            }

            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            if (i > 1)
            {
                dtlistShowStudents.Visible = true;             
                btnOK.Visible = false;
                pnlMSG.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;            
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
     

        }


        private void populateStudentDetail(DataRow drow, int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select StudyCentreCode,EnrollmentNo,StudentName,FatherName,Course,CYear,Session from DDEStudentRecord where SRID='"+srid+"' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(dr["Course"]));
                drow["CourseID"] = Convert.ToInt32(dr["Course"]);

                if (Convert.ToInt32(dr["CYear"])==1)
                {
                    drow["CYear"] = "1st Year";
                }
                else if (Convert.ToInt32(dr["CYear"]) == 2)
                {
                    drow["CYear"] = "2nd Year";

                }
                else if (Convert.ToInt32(dr["CYear"]) == 3)
                {
                    drow["CYear"] = "3rd Year";

                }
                else if (Convert.ToInt32(dr["CYear"]) == 4)
                {
                    drow["CYear"] = "4th Year";

                }
                else if (Convert.ToInt32(dr["CYear"]) == 5)
                {
                    drow["CYear"] = "PASSED OUT";

                }
                
                drow["Batch"] = Convert.ToString(dr["Session"]);
            }
            con.Close();
        }

        private void populateExamCentre(DataRow drow, int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEExamRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (ddlistMOE.SelectedItem.Value == "R")
                {
                    drow["RollNo"] = dr["RollNo_"+ddlistExam.SelectedItem.Value].ToString();
                    drow["Subjects"] = "ALL";
                }
                else if (ddlistMOE.SelectedItem.Value == "B")
                {
                    drow["RollNo"] = dr["BPRollNo_" + ddlistExam.SelectedItem.Value].ToString();
                    drow["Subjects"] = FindInfo.findBPSubjectCodesBySRID(srid, ddlistExam.SelectedItem.Value);
                }

                drow["ExamCity"] = dr["ExamCentreCity_"+ddlistExam.SelectedItem.Value].ToString();
                drow["ExamJone"] = dr["ExamCentreJone_"+ddlistExam.SelectedItem.Value].ToString();

            }
            con.Close();

        }



        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {


            dtlistShowStudents.Visible = false;
           
        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }
            
        protected void btnOK_Click(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = true;
            
            pnlMSG.Visible = false;
            btnOK.Visible = false;

        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            dtlistShowStudents.Visible = false;
           
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
           
        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
           

        }

        protected void ddlistExamCentre_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            
        }

        protected void ddlistJone_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }

        protected void ddlistMOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }

    
    }
}
