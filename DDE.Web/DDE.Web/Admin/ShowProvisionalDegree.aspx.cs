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
using System.IO;
using System.Text;

namespace DDE.Web.Admin
{
    public partial class ShowProvisionalDegree : System.Web.UI.Page
    {
        StringBuilder SB = new StringBuilder(90000000);
        StringBuilder SB1 = new StringBuilder(90000000);
        StringBuilder SB2 = new StringBuilder(90000000);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 113))
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;

                if (!IsPostBack)
                {
                    //setCurrentDate();
                    ExamRecord();
                }
            }


            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        void ExamRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (con.State == ConnectionState.Closed)
                con.Open();

            String varServer = string.Empty;
            SqlDataAdapter adp = new SqlDataAdapter("select getdate()", con);
            if (adp.SelectCommand.ExecuteScalar() != null)
                varServer = adp.SelectCommand.ExecuteScalar().ToString();


            cmd = new SqlCommand("select * from DDEExaminations order by examcode desc", con);
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsExam = new DataSet();

            if (con.State == ConnectionState.Closed)
                con.Open();

            da.Fill(dsExam);

            ddlistExam.DataTextField = "examname";
            ddlistExam.DataValueField = "examcode";

            ddlistExam.DataSource = dsExam;
            ddlistExam.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            String varInsert = "", varSrlno = "";
            Int32 varNos = 0;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (con.State == ConnectionState.Closed)
                con.Open();

            String varServer = string.Empty;
            SqlDataAdapter adp = new SqlDataAdapter("select getdate()", con);
            if (adp.SelectCommand.ExecuteScalar() != null)
                varServer = adp.SelectCommand.ExecuteScalar().ToString();

            try
            {
                SqlCommand cmdExam = new SqlCommand();
                cmdExam = new SqlCommand("select ResultDeclaredOn from DDEExaminations where Examcode='" + ddlistExam.SelectedValue.ToString() + "' ", con);
                cmdExam.CommandTimeout = 0;
                SqlDataAdapter daExam = new SqlDataAdapter(cmdExam);
                DataSet dsExam = new DataSet();
                daExam.Fill(dsExam);
                if (dsExam.Tables[0].Rows.Count <= 0)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Result Declaration Date not Record found..!!";
                    pnlMSG.Visible = true;
                    return;
                }

                SqlCommand cmdExamRec = new SqlCommand();
                cmdExamRec = new SqlCommand("select rollno from DDEExamRecord_" + ddlistExam.SelectedValue.ToString() + " where srid=(select srid from DDEStudentRecord where srid='" + Session["srid"].ToString() + "')", con);
                cmdExamRec.CommandTimeout = 0;
                SqlDataAdapter da1 = new SqlDataAdapter(cmdExamRec);
                DataSet dsExamRec = new DataSet();
                da1.Fill(dsExamRec);
                if (dsExamRec.Tables[0].Rows.Count <= 0)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "No Record found...!!";
                    pnlMSG.Visible = true;
                    return;
                }

                cmd = new SqlCommand("select * from ddeProvisionalDegree where srid=" + Session["srid"].ToString(), con);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsCheck = new DataSet();
                da.Fill(dsCheck);

                if (dsCheck.Tables[0].Rows.Count <= 0)
                {
                    SqlDataAdapter adNos = new SqlDataAdapter("select max(Nos) from ddeProvisionalDegree ", con);
                    if (adNos.SelectCommand.ExecuteScalar() != null)
                        varNos = Convert.ToInt32(adp.SelectCommand.ExecuteScalar().ToString());

                    varSrlno = tbENo.Text.Substring(0, 1) + "/" + Convert.ToDateTime(varServer).ToString("yyyy") + "/" + (1000000 + 1 + varNos).ToString().Substring(3, 7);
                    varInsert = "insert into ddeProvisionalDegree (SRID,CourseID,CompletionDT,Exam,RollNo,MM,Obtain,Grade,Status,SrlNo)values (";
                    varInsert += "'" + Session["srid"].ToString() + "'," + Session["Courseid"].ToString() + ",";
                    varInsert += "'" + Convert.ToDateTime(dsExam.Tables[0].Rows[0]["ResultDeclaredOn"].ToString()).ToString("yyyy-MM-dd") + "',";
                    varInsert += "'" + ddlistExam.SelectedItem.Text + "','" + dsExamRec.Tables[0].Rows[0]["rollno"].ToString() + "'," + txtMM.Text + "," + txtObtMarks.Text + ",";
                    varInsert += "'" + txtGrade.Text + "','Active','" + varSrlno.ToString() + "')";
                }
                else
                {
                    varInsert = "update ddeProvisionalDegree set CourseID=" + dsCheck.Tables[0].Rows[0]["courseid"].ToString();
                    varInsert += ",CompletionDT='" + Convert.ToDateTime(dsExam.Tables[0].Rows[0]["ResultDeclaredOn"].ToString()).ToString("yyyy-MM-dd") + "'";
                    varInsert += ",Exam='" + ddlistExam.SelectedItem.Text + "',MM=" + txtMM.Text + ",Obtain=" + txtObtMarks.Text + ",Grade='" + txtGrade.Text + "'";
                    varInsert += " where srid=" + Session["srid"].ToString();
                }

                SqlCommand cmdInsrt = new SqlCommand(varInsert, con);
                cmdInsrt.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();

                pnlData.Visible = false;
                lblMSG.Text = "Fail to Save Record...!!";
                pnlMSG.Visible = true;
                return;
            }

            Response.Redirect("ProvisionalDegree.aspx");
        }



        protected void tbENo_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            if (con.State == ConnectionState.Closed)
                con.Open();

            String sqlProvDegree = "select z.*,a.CourseFullName,b.EnrollmentNo,b.StudentName,b.FatherName,b.gender,b.StudentPhoto from ddeProvisionalDegree z";
            sqlProvDegree += " inner join DDECourse a on a.CourseID=z.courseid";
            sqlProvDegree += " inner join DDEStudentRecord b on b.srid=z.srid";
            sqlProvDegree += " where b.EnrollmentNo='" + tbENo.Text + "'";

            SqlCommand cmdProDegree = new SqlCommand();
            cmdProDegree = new SqlCommand(sqlProvDegree, con);
            cmdProDegree.CommandTimeout = 0;
            SqlDataAdapter daProDegree = new SqlDataAdapter(cmdProDegree);
            DataSet dsProDegree = new DataSet();
            daProDegree.Fill(dsProDegree);

            DataSet dsStdRecord = new DataSet();
            if (dsProDegree.Tables[0].Rows.Count <= 0)
            {
                String sqlEnrol = "select a.CourseFullName,a.SpecializationDegree,z.StudentName,z.FatherName,z.VDOA,z.Gender,z.srid,a.courseid from DDEStudentRecord z";
                sqlEnrol += " inner join DDECourse a on a.CourseID in (";
                sqlEnrol += " select isnull((case cyear when 1 then course when 2 then course2year when 3 then course3year end),0)asd from DDEStudentRecord";
                sqlEnrol += " where EnrollmentNo = z.EnrollmentNo ) and EnrollmentNo = z.EnrollmentNo";
                sqlEnrol += " where EnrollmentNo='" + tbENo.Text + "'";

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand(sqlEnrol, con);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsStdRecord);

                if (dsStdRecord.Tables[0].Rows.Count > 0)
                {
                    Session["SRID"] = dsStdRecord.Tables[0].Rows[0]["srid"].ToString();
                    Session["ExamLst"] = ddlistExam.SelectedValue.ToString();
                    Session["Exam"] = ddlistExam.SelectedItem.Text;
                    Session["Courseid"] = dsStdRecord.Tables[0].Rows[0]["courseid"].ToString();

                    txtStudentName.Text = dsStdRecord.Tables[0].Rows[0]["studentname"].ToString();
                    txtFName.Text = dsStdRecord.Tables[0].Rows[0]["studentname"].ToString();
                    txtAdminDate.Text = Convert.ToDateTime(dsStdRecord.Tables[0].Rows[0]["vdoa"].ToString()).ToString("dd-MM-yyyy");
                    txtCourse.Text = dsStdRecord.Tables[0].Rows[0]["CourseFullName"].ToString();

                    txtMM.Text = dsStdRecord.Tables[0].Rows[0]["mm"].ToString();
                    txtObtMarks.Text = dsStdRecord.Tables[0].Rows[0]["obtain"].ToString();
                    txtGrade.Text = dsStdRecord.Tables[0].Rows[0]["grade"].ToString();
                }
                else
                {
                    lblMSG.Text = "Enrolment No not found  !!";
                    pnlMSG.Visible = true;
                }
            }
            else
            {
                Session["SRID"] = dsProDegree.Tables[0].Rows[0]["srid"].ToString();
                //Session["ExamLst"] = ddlistExam.SelectedValue.ToString();
                //Session["Exam"] = ddlistExam.SelectedItem.Text;
                //Session["Courseid"] = dsProDegree.Tables[0].Rows[0]["courseid"].ToString();

                txtStudentName.Text = dsProDegree.Tables[0].Rows[0]["studentname"].ToString();
                txtFName.Text = dsProDegree.Tables[0].Rows[0]["studentname"].ToString();
                //txtAdminDate.Text = Convert.ToDateTime(dsStdRecord.Tables[0].Rows[0]["vdoa"].ToString()).ToString("dd-MM-yyyy");
                txtCourse.Text = dsProDegree.Tables[0].Rows[0]["CourseFullName"].ToString();

                txtMM.Text = dsProDegree.Tables[0].Rows[0]["mm"].ToString();
                txtObtMarks.Text = dsProDegree.Tables[0].Rows[0]["obtain"].ToString();
                txtGrade.Text = dsProDegree.Tables[0].Rows[0]["grade"].ToString();
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            lblMSG.Text = string.Empty;
            pnlMSG.Visible = false;
            pnlData.Visible = true;
        }
    }
}