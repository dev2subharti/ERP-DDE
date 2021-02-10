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
    public partial class DetainStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 62))
            {
                PopulateDDList.populateExam(ddlistExam);
               
                pnlSearch.Visible = true;
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
             int srid = FindInfo.findSRIDByENo(tbENo.Text);
             polulateStudentInfo(srid);
             
             setDetainedRecord();
             btnDetain.Visible = true;
             pnlSearch.Visible = false;
             pnlStudentDetails.Visible = true;
            
        }

        private void setDetainedRecord()
        {
            string dstatus;
            if (FindInfo.alreadyDetaind(Convert.ToInt32(lblSRID.Text), ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value, out dstatus))
            {

                if (dstatus == "True")
                {
                    tbcs.Text = "DETAINED";
                    btnDetain.Text = "Allow";
                   
                }
                else if (dstatus == "False")
                {
                    tbcs.Text = "ALLOWED";
                    btnDetain.Text = "Detain";
                }



            }
            else
            {
                tbcs.Text = "NOT SET";
            }
        }

       

        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,StudyCentreCode,Course,CYear from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                tbEnNo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = srid.ToString();
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();
                tbSCCode.Text = dr["StudyCentreCode"].ToString();
                tbCourse.Text = FindInfo.findCourseNameBySRID(srid, Convert.ToInt32(dr["CYear"]));
                tbRemark.Text = FindInfo.findRemarkForDetained(srid, ddlistExam.SelectedItem.Value,ddlistMOE.SelectedItem.Value);
                
            }

            con.Close();

          
        }


        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }

        protected void btnDetain_Click(object sender, EventArgs e)
        {
            string dstatus;
            if (FindInfo.alreadyDetaind(Convert.ToInt32(lblSRID.Text),ddlistExam.SelectedItem.Value,ddlistMOE.SelectedItem.Value,out dstatus))
            {
               
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEDetainedStudents set DetainedStatus=@DetainedStatus,Remark=@Remark where SRID='" + lblSRID.Text + "' and Examination='" + ddlistExam.SelectedItem.Value + "' and MOE='" + ddlistMOE.SelectedItem.Value + "' ", con);

                if (dstatus == "True")
                {
                    cmd.Parameters.AddWithValue("@DetainedStatus", "False");                   
                }
                else if (dstatus == "False")
                {
                    cmd.Parameters.AddWithValue("@DetainedStatus", "True");                   
                }

                cmd.Parameters.AddWithValue("@Remark", tbRemark.Text.ToUpper());

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Update Detained Record", btnDetain.Text+" Student in '"+ddlistMOE.SelectedItem.Text+"'"+ddlistExam.SelectedItem.Text+" Examination with Enrollment No '"+tbENo.Text, Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                pnlMSG.Visible = true;
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDEDetainedStudents values(@SRID,@Examination,@MOE,@DetainedStatus,@Remark)", con);


                cmd.Parameters.AddWithValue("@SRID", lblSRID.Text);
                cmd.Parameters.AddWithValue("@Examination", ddlistExam.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@DetainedStatus", "True");
                cmd.Parameters.AddWithValue("@Remark", tbRemark.Text.ToUpper());
             
               
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Detain Student", "Detain Student in '"+ddlistMOE.SelectedItem.Text+"'"+ddlistExam.SelectedItem.Text+" Examination with Enrollment No '"+tbENo.Text, Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Student has been detained successfully in </br>'" + ddlistMOE.SelectedItem.Text + "'" + ddlistExam.SelectedItem.Text + " Examination";
                pnlMSG.Visible = true;
            }
        }

       
    }
}
