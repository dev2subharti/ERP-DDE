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
using System.Data.SqlClient;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class SetQualifyingStatus : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 36))
            {
                if (!IsPostBack)
                {
                   
                    populateCourses();
                    populateSessions();


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


        private void populateSessions()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Session from DDESession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistSession.Items.Add(dr["Session"].ToString());


            }

            con.Close();

        }

        

        private void populateCourses()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[2].ToString() == "")
                {
                    ddlistCourse.Items.Add(dr[1].ToString());
                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }

            }

            con.Close();

        }



       
        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateStudents();
            setQualifyingStatus();     
        }

        private void setQualifyingStatus()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {

                Label srid = (Label)dli.FindControl("lblSRID");
                Label qs = (Label)dli.FindControl("lblQStatus");
                DropDownList ddlistQS = (DropDownList)dli.FindControl("ddlistQStatus");
                ddlistQS.Items.FindByValue(qs.Text).Selected = true;
                          
            }
            
        }

        
        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (FindInfo.findCourseShortNameByID(Convert.ToInt32(ddlistCourse.SelectedItem.Value)) == "MBA")
            {
                if (ddlistYear.SelectedItem.Value == "1")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "2")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,QualifyingStatus from DDEStudentRecord where Course2Year='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
                }
                else if (ddlistYear.SelectedItem.Value == "3")
                {
                    cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,QualifyingStatus from DDEStudentRecord where Course3Year='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";               
                    
                }

            }
            else
            {
                cmd.CommandText = "Select SRID,EnrollmentNo,StudentName,QualifyingStatus from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and CYear='" + ddlistYear.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ";
            }




            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("EC");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("QStatus");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);

          
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
               
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
                drow["QStatus"] = Convert.ToString(dr["QualifyingStatus"]);
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
                btnSubmit.Visible = true;
                pnlMSG.Visible = false;
                btnSQS.Visible = false;
            }

            else
            {
                ddlistSession.Enabled = false;
                btnFind.Visible = true;
                btnSQS.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
    
                foreach (DataListItem dli in dtlistShowStudents.Items)
                {

                    Label srid = (Label)dli.FindControl("lblSRID");
                    DropDownList ddlistQS = (DropDownList)dli.FindControl("ddlistQStatus");
                    Label qs = (Label)dli.FindControl("lblQStatus");

                    if(ddlistQS.SelectedItem.Value!=qs.Text)
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEStudentRecord set QualifyingStatus=@QualifyingStatus where SRID='" + srid.Text + "'", con);
                        cmd.Parameters.AddWithValue("@QualifyingStatus", ddlistQS.SelectedItem.Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    
                    }

   
                }

                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Record has been update successfully";
                pnlMSG.Visible = true;
        }

       


      

        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

       

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

       

        protected void btnSQS_Click(object sender, EventArgs e)
        {    
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlCommand cmd = new SqlCommand("Select SRID from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and RecordStatus='True' order by StudentName ", con);
            //SqlDataReader dr;
            
            //con.Open();
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    CalculateResult rcr = new CalculateResult();
            //    string[] rrdetail = rcr.findResult(Convert.ToInt32(dr["SRID"]), "R");
            //    if (Convert.ToInt32(rrdetail[1]) != 0)
            //    {
            //        FillResult(Convert.ToInt32(dr["SRID"]), rrdetail, ddlistExam.SelectedItem.Value, "R");
            //    }

            //    CalculateResult bcr = new CalculateResult();      
            //    string[] brdetail = bcr.findResult(Convert.ToInt32(dr["SRID"]), "B");
            //    if (Convert.ToInt32(brdetail[1]) != 0)
            //    {              
            //        FillResult(Convert.ToInt32(dr["SRID"]), brdetail, ddlistExam.SelectedItem.Value, "B");
            //    }
            //}
            //con.Close();

            // btnSQS.Visible = false;
            // btnOK.Visible = true;
            // lblMSG.Text = "Record has been update successfully";
            // pnlMSG.Visible = true;

           
        }

        private void FillResult(int srid,string [] rrdetail,string examcode,string moe)
        {
            if (!alreadyExist(srid, examcode, moe))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDEExamRecord_" + examcode + " values(@SRID,@MaxMarks,@ObtMarks,@QualifyingStatus,@MOE)", con);

                cmd.Parameters.AddWithValue("@SRID", srid);
                cmd.Parameters.AddWithValue("@MaxMarks", rrdetail[0]);
                cmd.Parameters.AddWithValue("@ObtMarks", rrdetail[1]);
                cmd.Parameters.AddWithValue("@QualifyingStatus", rrdetail[2]);
                cmd.Parameters.AddWithValue("@MOE", moe);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEExamRecord_" + examcode + " set @MaxMarks=@MaxMarks,ObtMarks=@ObtMarks,QualifyingStatus=@QualifyingStatus where SRID='"+srid+"' and MOE='"+moe+"'", con);
     
                cmd.Parameters.AddWithValue("@MaxMarks", rrdetail[0]);
                cmd.Parameters.AddWithValue("@ObtMarks", rrdetail[1]);
                cmd.Parameters.AddWithValue("@QualifyingStatus", rrdetail[2]);
               
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            
        }

        private bool alreadyExist(int srid, string examcode, string moe)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEExamRecord_"+examcode+" where SRID='" + srid+ "' and MOE='"+moe+"'", con);
            SqlDataReader dr;

            bool exist = false;
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                exist = true;
            }

            con.Close();

            return exist;
        }

        
            
        //protected void ddlistMode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlistMode.SelectedItem.Text == "MANUAL")
        //    {
        //        dtlistShowStudents.Visible = false;
        //        ddlistSession.Enabled = true;
        //        btnFind.Visible = true;
        //        btnSQS.Visible = false;
        //    }

        //    else if (ddlistMode.SelectedItem.Text == "AUTOMATIC")
        //    {
        //        dtlistShowStudents.Visible = false;
        //        ddlistSession.Enabled = false;
        //        btnSubmit.Visible = false;
        //        btnFind.Visible = false;
        //        btnSQS.Visible = true;
        //    }

        //}

        protected void btnOK_Click(object sender, EventArgs e)
        {
            ddlistSession.Enabled = false;
            btnSQS.Visible = true;
            btnOK.Visible = false;
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }


       
    }
}
