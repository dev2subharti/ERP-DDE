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
    public partial class SetYearRecord : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistBatch);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateCourses(ddlistCourse);


                    Session["Role"] = "Edit";

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
            DataColumn dtcol8 = new DataColumn("CourseID");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

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
                drow["CourseID"] = Convert.ToString(dr["Course"]);
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


            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            con.Close();

            if (j > 1)
            {

                dtlistShowStudents.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                dtlistShowStudents.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
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
            setYears();
           

        }

        private void setYears()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {

                Image img = (Image)dli.FindControl("imgPhoto");
                Label srid = (Label)dli.FindControl("lblSRID");
                Label cid = (Label)dli.FindControl("lblCourseID");
                DataList dtlist = (DataList)dli.FindControl("dtlistYearStatus");

                int cduration = FindInfo.findCourseDuration(Convert.ToInt32(cid.Text));

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select Session from DDESession order by SessionID", con);
                con.Open();
                SqlDataReader dr;

                dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();

                DataColumn dtcol1 = new DataColumn("Session");

                dt.Columns.Add(dtcol1);


                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["Session"] = dr[0].ToString();
                    dt.Rows.Add(drow);

                }

                dtlist.DataSource = dt;
                dtlist.DataBind();

                con.Close();

                foreach (DataListItem dli2 in dtlist.Items)
                {
                    
                    Label session = (Label)dli2.FindControl("lblSession");
                    DropDownList cyear = (DropDownList)dli2.FindControl("ddlistCYear");

                    if (cduration == 1)
                    {
                        cyear.Items.Add("1ST YEAR");
                        cyear.Items.FindByText("1ST YEAR").Value = "1";

                        cyear.Items.Add("PASSED OUT");
                        cyear.Items.FindByText("PASSED OUT").Value = "5";
                    }

                    else if (cduration == 2)
                    {
                        cyear.Items.Add("1ST YEAR");
                        cyear.Items.FindByText("1ST YEAR").Value = "1";

                        cyear.Items.Add("2ND YEAR");
                        cyear.Items.FindByText("2ND YEAR").Value = "2";

                        cyear.Items.Add("PASSED OUT");
                        cyear.Items.FindByText("PASSED OUT").Value = "5";
                    }

                    else if (cduration == 3)
                    {
                        cyear.Items.Add("1ST YEAR");
                        cyear.Items.FindByText("1ST YEAR").Value = "1";

                        cyear.Items.Add("2ND YEAR");
                        cyear.Items.FindByText("2ND YEAR").Value = "2";

                        cyear.Items.Add("3RD YEAR");
                        cyear.Items.FindByText("3RD YEAR").Value = "3";

                        cyear.Items.Add("PASSED OUT");
                        cyear.Items.FindByText("PASSED OUT").Value = "5";
                    }

                    else if (cduration == 4)
                    {
                        cyear.Items.Add("1ST YEAR");
                        cyear.Items.FindByText("1ST YEAR").Value = "1";

                        cyear.Items.Add("2ND YEAR");
                        cyear.Items.FindByText("2ND YEAR").Value = "2";

                        cyear.Items.Add("3RD YEAR");
                        cyear.Items.FindByText("3RD YEAR").Value = "3";

                        cyear.Items.Add("4TH YEAR");
                        cyear.Items.FindByText("4TH YEAR").Value = "4";

                        cyear.Items.Add("PASSED OUT");
                        cyear.Items.FindByText("PASSED OUT").Value = "5";
                    }


                }

              

            }
        }

        protected void dtlistShowRegistration_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Session["RecordType"] = "Edit";
                Response.Redirect("DStudentRegistration.aspx?SRID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudentRecord set RecordStatus=@RecordStatus where SRID ='" + Convert.ToString(e.CommandArgument) + "'", con);
                cmd.Parameters.AddWithValue("@RecordStatus", "False");

                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Delete", "Delete a student with Enrollment No '" + FindInfo.findENoByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));

                PopulateDStudent();


            }
            else if (e.CommandName == "ShowFullRecord")
            {
                Session["RecordType"] = "Show";
                Response.Redirect("DStudentRegistration.aspx?SRID=" + Convert.ToString(e.CommandArgument));

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
