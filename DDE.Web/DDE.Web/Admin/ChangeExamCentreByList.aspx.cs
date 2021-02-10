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
    public partial class ChangeExamCentreByList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
                    ddlistExam.Items.FindByValue("Z11").Selected = true;
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                   

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

            SqlCommand cmd = new SqlCommand("Select DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".ExamCentreCode from DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID inner join DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + " on DDEExaminationCentres_" + ddlistExam.SelectedItem.Value + ".ECID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".ExamCentreCode where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='"+ddlistMOE.SelectedItem.Value+"' and (DDEStudentRecord.StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "' or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + ddlistSCCode.SelectedItem.Text + "'))", con);


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
            DataColumn dtcol8 = new DataColumn("ECCode");
           


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);



            int j = 1;
            while (dr.Read())
            {
                             
                DataRow drow = dt.NewRow();
                drow["SNo"] = j; 
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["ECCode"] = Convert.ToString(dr["ExamCentreCode"]);

                dt.Rows.Add(drow);
                j = j + 1;
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

        private string findECCode(int srid)
        {
            string eccode = "NOT SET";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select * from DDEExamRecord_"+ddlistExam.SelectedItem.Value+" where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                eccode = findExamCodeByECID(Convert.ToInt32(dr["ExamCentreCode"]), ddlistExam.SelectedItem.Value);

            }
           



            con.Close();
            return eccode;
        }

        private string findExamCodeByECID(int ecid, string exam)
        {
            string eccode = "NOT SET";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCentreCode from DDEExaminationCentres_"+exam+" where ECID='" + ecid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                eccode = dr[0].ToString();

            }

            con.Close();
            return eccode;
        }

     

        protected void btnFind_Click(object sender, EventArgs e)
        {
            PopulateDStudent();         
        }

        
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
                foreach (DataListItem dli in dtlistShowRegistration.Items)
                {
                    
                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label eno = (Label)dli.FindControl("lblENo");
                    Label eccode = (Label)dli.FindControl("lblECCode");
                    TextBox nec = (TextBox)dli.FindControl("tbNewECCode");

                    if (nec.Text != "" && nec.Text != eccode.Text)
                    {
                        int ecid=FindInfo.findECIDByECCode(nec.Text, ddlistExam.SelectedItem.Value);
                        if (ecid!=0)
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                            SqlCommand cmd = new SqlCommand();

                            cmd.CommandText = "update DDEExamRecord_" + ddlistExam.SelectedItem.Value + " set ExamCentreCode=@ExamCentreCode where SRID='" + srid.Text + "' ";
                            cmd.Parameters.AddWithValue("@ExamCentreCode", ecid);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Log.createLogNow("Update Exam Centre", "Exam Centre Changed for "+ddlistExam.SelectedItem.Text+" from '" + eccode.Text + "' to '" + nec.Text + "' with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        }
                        else
                        {
                           
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry!! Invalid Exam Centre Code at SNo. : "+sno.Text;
                            btnOK.Visible = true;
                            pnlMSG.Visible = true;
                            break;
                        }


                    }

                }

                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully";
                btnOK.Visible = false;
                pnlMSG.Visible = true;
        }
      
        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }
    }
}
