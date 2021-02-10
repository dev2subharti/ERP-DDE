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
    public partial class ShowDStudent : System.Web.UI.Page
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
            else  if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistBatch);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                    PopulateDDList.populateCourses(ddlistCourse);
                    Session["Role"] = "Show";

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
            SqlCommand cmd = new SqlCommand(findCommand(),con);

           
            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            //DataColumn dtcol2 = new DataColumn("StudentPhoto");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("FatherName");
            DataColumn dtcol8 = new DataColumn("Course");

            dt.Columns.Add(dtcol1);
            //dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();

                    //drow["StudentPhoto"] = "StudentPhotos/" + ds.Tables[0].Rows[i]["EnrollmentNo"].ToString() + ".jpg";
                    drow["SRID"] = Convert.ToString(ds.Tables[0].Rows[i]["SRID"]);
                    drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                    if (ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Length == 10)
                    {
                        drow["EC"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(5, 5);
                    }
                    else if (ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Length == 11)
                    {
                        drow["EC"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(6, 5);
                    }
                    else if (ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Length == 12)
                    {
                        drow["EC"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(6, 6);
                    }
                    else if (ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Length == 14)
                    {
                        drow["EC"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString().Substring(9, 5);
                    }
                    else
                    {
                        drow["EC"] = "";
                    }


                    drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                    drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                    if (FindInfo.isMBACourse(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"])))
                    {
                        try
                        {
                            if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 1)
                            {
                                drow["Course"] = "MBA";
                            }
                            else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 2)
                            {
                                drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course2Year"]));
                            }
                            else if (Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]) == 3)
                            {
                                drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course3Year"]));
                            }
                        }
                        catch
                        {
                            drow["Course"] = "NOT FOUND";
                        }
                    }
                    else
                    {
                        drow["Course"] = FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]));
                    }

                    dt.Rows.Add(drow);

                }
            }


               

            if (ds.Tables[0].Rows.Count > 0)
            {
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
                dtlistShowRegistration.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                dtlistShowRegistration.Visible = false;
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo" ;

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }

                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }                                        
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "'and DOA>='" + from + "' and DOA<='" + to + "'  and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and RecordStatus='True'  order by EnrollmentNo";
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                          
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 1 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";

                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                           
                                        }

                                    }
                                }
                                else
                                {
                                    if (ddlistCourse.SelectedItem.Text == "ALL")
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {
                                            if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
                                        }

                                    }

                                    else
                                    {
                                        if (ddlistYear.SelectedItem.Text == "ALL")
                                        {

                                            cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='"+ddlistGender.SelectedItem.Text+"' and RecordStatus='True'  order by EnrollmentNo";

                                        }

                                        else
                                        {if (ddlistYear.SelectedItem.Value == "1")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and FirstYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "2")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and SecondYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            else if (ddlistYear.SelectedItem.Value == "3")
                                            {
                                                cmnd = "select SRID,EnrollmentNo,StudentName,FatherName,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and ThirdYear='True' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Value + "' and Session='" + ddlistBatch.SelectedItem.Text + "' and DOA>='" + from + "' and DOA<='" + to + "' and AdmissionType='" + 2 + "' and Category='" + ddlistCategory.SelectedItem.Text + "' Gender='" + ddlistGender.SelectedItem.Text + "' and RecordStatus='True'  order by EnrollmentNo";
                                            }
                                            
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

                Image img = (Image)dli.FindControl("imgPhoto");
                Label srid = (Label)dli.FindControl("lblSRID");
                LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");
                LinkButton delete = (LinkButton)dli.FindControl("lnkbtnDelete");
                LinkButton cancel = (LinkButton)dli.FindControl("lnkbtnCancel");
                LinkButton sfr = (LinkButton)dli.FindControl("lnkbtnSFR");

                if (Session["Role"].ToString() == "Show")
                {
                    edit.Visible = false;
                    delete.Visible = false;
                    cancel.Visible = false;
                    sfr.Visible = false;
                }

                if (rblPhoto.SelectedItem.Text == "Without Photo")
                {
                    img.Visible = false;
                }
                else if (rblPhoto.SelectedItem.Text == "With Photo")
                {
                    img.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.Text;
                    img.Visible = true;
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
                cmd.Parameters.AddWithValue("@RecordStatus","False");

                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Delete", "Delete a student with Enrollment No '" + FindInfo.findENoByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
              
                PopulateDStudent();
                

            }
            else if (e.CommandName == "Cancel")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudentRecord set RecordStatus=@RecordStatus,AdmissionStatus=@AdmissionStatus where SRID ='" + Convert.ToString(e.CommandArgument) + "'", con);
                cmd.Parameters.AddWithValue("@RecordStatus", "False");
                cmd.Parameters.AddWithValue("@AdmissionStatus", 4);

                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Cancel", "Cancel the admission of student with Enrollment No '" + FindInfo.findENoByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));

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

       
       
    }
}
