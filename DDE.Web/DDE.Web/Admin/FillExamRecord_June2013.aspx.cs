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
using System.Drawing;

namespace DDE.Web.Admin
{
    public partial class FillExamRecord_June2013 : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistBatch);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateCourses(ddlistCourse);

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


        private void PopulateDStudent()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand(findCommand(), con);


            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("StudentPhoto");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("FatherName");
            DataColumn dtcol8 = new DataColumn("ExamFee");
            DataColumn dtcol9 = new DataColumn("ExamCity");
            DataColumn dtcol10 = new DataColumn("ExamCentre");


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



            while (dr.Read())
            {
                DataRow drow = dt.NewRow();

                drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                if (dr["EnrollmentNo"].ToString().Length == 10)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(5, 5);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 11)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 5);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 12)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 6);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 14)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(9, 5);
                }
                else
                {
                    drow["EC"] = "";
                }

                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["ExamFee"] = findExamFee(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(ddlistExamYear.SelectedItem.Value));
                drow["ExamCity"] = "NA";
                drow["ExamCentre"] = "NA";
                dt.Rows.Add(drow);

            }

            dt.DefaultView.Sort = "EC ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }


            dtlistShowRegistration.DataSource = dt;
            dtlistShowRegistration.DataBind();

            con.Close();

            if (j > 1)
            {

                dtlistShowRegistration.Visible = true;
                pnlMSG.Visible = false;
                btnUpdate.Visible = true;

            }

            else
            {
                dtlistShowRegistration.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
                btnUpdate.Visible = false;
            }
        }

        private object findExamFee(int srid, int year)
        {
            string examfee = "0";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from ExamRecord_June13 where SRID='" + srid + "' and Online='True'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (year == 1 && dr["EFP1Year"].ToString() == "True")
                {
                    examfee = "1000";
                }

                else if (year == 2 && dr["EFP2Year"].ToString() == "True")
                {
                    examfee = "1000";
                }
                else if (year == 3 && dr["EFP3Year"].ToString() == "True")
                {
                    examfee = "1000";
                }

            }
            else
            {
                examfee = "NA";
            }
           


            con.Close();
            return examfee;
        }

        private string findCommand()
        {
            string cmnd = "";

            if (ddlistGender.SelectedItem.Text == "ALL")
            {

                if (ddlistCategory.SelectedItem.Text == "ALL")
                {

                    if (rblAdmissionType.SelectedItem.Value == "0")
                    {

                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "'and DOA>='" + from + "' and DOA<='" + to + "'  and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }
                    }
                    else if (rblAdmissionType.SelectedItem.Value == "1")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }

                    else if (rblAdmissionType.SelectedItem.Value == "2")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }
                }

                else
                {
                    if (rblAdmissionType.SelectedItem.Value == "1")
                    {

                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }
                    }
                    else if (rblAdmissionType.SelectedItem.Value == "1")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }

                    else if (rblAdmissionType.SelectedItem.Value == "2")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }


                }
            }
            else
            {
                if (ddlistCategory.SelectedItem.Text == "ALL")
                {

                    if (rblAdmissionType.SelectedItem.Value == "1")
                    {

                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }
                    }
                    else if (rblAdmissionType.SelectedItem.Value == "1")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }

                    else if (rblAdmissionType.SelectedItem.Value == "2")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }
                }

                else
                {
                    if (rblAdmissionType.SelectedItem.Value == "1")
                    {

                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }
                    }
                    else if (rblAdmissionType.SelectedItem.Value == "1")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }

                    else if (rblAdmissionType.SelectedItem.Value == "2")
                    {
                        if (rblMode.SelectedItem.Value == "1")
                        {

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                        else if (rblMode.SelectedItem.Value == "2")
                        {
                            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
                            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

                            if (ddlistBatch.SelectedItem.Text == "ALL")
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }

                            }

                            else
                            {
                                if (ddlistSCCode.SelectedItem.Text == "ALL")
                                {

                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            cmnd = "select * from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                        }

                                    }
                                }


                            }
                        }

                    }


                }
            }

            return cmnd;
        }


        protected void btnFind_Click(object sender, EventArgs e)
        {
            PopulateDStudent();

            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {

                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)dli.FindControl("imgPhoto");
                TextBox tb = (TextBox)dli.FindControl("tbExamFee");

                

                if (tb.Text == "0")
                {
                    tb.BackColor = Color.Orange;
                    tb.Enabled = true;
                }

                else if (tb.Text == "NA")
                {
                    tb.BackColor = Color.Orange;
                    tb.Enabled = false;
                }

                if (rblPhoto.SelectedItem.Text == "Without Photo")
                {
                    img.Visible = false;
                }


            }
           
        }

      

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "1")
            {
                pnlDOA.Visible = false;
            }

            else if (rblMode.SelectedItem.Value == "2")
            {
                pnlDOA.Visible = true;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int sno=validEntry();
            if (sno==0)
            {
                int year = Convert.ToInt32(ddlistExamYear.SelectedItem.Value);
                foreach (DataListItem dli in dtlistShowRegistration.Items)
                {
                    TextBox tb = (TextBox)dli.FindControl("tbExamFee");
                    Label lb = (Label)dli.FindControl("lblExamFee");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label lsno = (Label)dli.FindControl("lblSNo");
                    Label eno = (Label)dli.FindControl("lblENo");


                    if (tb.Text != lb.Text)
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand();

                        if (year == 1)
                        {
                            cmd.CommandText = "update ExamRecord_June13 set EFP1Year=@EFP1Year where SRID='" + srid.Text + "' ";
                            cmd.Parameters.AddWithValue("@EFP1Year", tb.Text);
                        }

                        else if (year == 2)
                        {
                            cmd.CommandText = "update ExamRecord_June13 set EFP2Year=@EFP2Year where SRID='" + srid.Text + "' ";
                            cmd.Parameters.AddWithValue("@EFP2Year", tb.Text);
                        }

                        else if (year == 3)
                        {
                            cmd.CommandText = "update ExamRecord_June13 set EFP3Year=@EFP3Year where SRID='" + srid.Text + "' ";
                            cmd.Parameters.AddWithValue("@EFP3Year", tb.Text);
                        }
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Log.createLogNow("Fee Filling", "Regular Exam Fee Filled '" + tb.Text + "' for June 2013 with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                       
                    }
  
              }

                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                btnOK.Visible = false;
                pnlMSG.Visible = true;
            }
            else
            {

                pnlData.Visible = false;
                lblMSG.Text = "Error at S.No. :" + sno;
                btnOK.Visible = true;
                pnlMSG.Visible = true;
               
            } 
                        

        }

        private int validEntry()
        {
                int sno = 0;
                foreach (DataListItem dli in dtlistShowRegistration.Items)
                {
                    TextBox tb = (TextBox)dli.FindControl("tbExamFee");
                    Label lsno = (Label)dli.FindControl("lblSNo");

                    if (tb.Text != "1000" && tb.Text != "0" && (tb.Text == "NA" && tb.Enabled == true))
                    {
                        sno =Convert.ToInt32(lsno.Text);
                        break;
                    }

                    
                   
                }

                return sno;
              
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            pnlData.Visible = true;

           

        }

       
    }
}
