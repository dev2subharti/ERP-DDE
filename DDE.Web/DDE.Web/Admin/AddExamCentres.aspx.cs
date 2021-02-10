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
    public partial class AddExamCentres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
            {

                if (!IsPostBack)
                {              
                    PopulateDDList.populateCity(ddlistCity, "UTTAR PRADESH");                   
                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("W11").Selected = true;

                    if (Request.QueryString["ECID"] != null)
                    {
                        populateECRecord();
                        btnSubmit.Text = "Update";
                    }

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





        private void populateECRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEExaminationCentres_"+Session["ExamCode"].ToString()+" where ECID='" + Request.QueryString["ECID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ddlistExamination.SelectedItem.Selected = false;
                ddlistExamination.Items.FindByValue(Session["ExamCode"].ToString()).Selected = true;
                ddlistCity.Items.FindByValue(dr["City"].ToString().ToUpper()).Selected = true;
                tbECCode.Text = dr["ExamCentreCode"].ToString();
                tbContactPerson.Text = dr["ContactPerson"].ToString();
                tbCNo.Text = dr["ContactNo"].ToString();
                tbCentreName.Text = dr["CentreName"].ToString();
                tbAddress.Text = dr["Location"].ToString();
                tbEAddress.Text = dr["Email"].ToString();
                tbExamSCCodes.Text = dr["SCCodes"].ToString();
                lblExamSCCodes.Text = dr["SCCodes"].ToString();
                ddlistRemark.Items.FindByValue(dr["Remark"].ToString().ToUpper()).Selected = true;
               
            }

            con.Close();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["ECID"] == null)
            {
                if (!FindInfo.ExamCentreExist(ddlistExamination.SelectedItem.Value, tbECCode.Text))
                {

                    Random rd = new Random();
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEExaminationCentres_" + ddlistExamination.SelectedItem.Value + " output Inserted.ECID values(@City,@ExamCentreCode,@Password,@PasswordMailSent,@ContactPerson,@ContactNo,@CentreName,@Location,@Email,@SCCodes,@Remark,@NoTimesLoggedIn,@LastLogoutTime)", con);

                    cmd.Parameters.AddWithValue("@City", ddlistCity.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@ExamCentreCode", tbECCode.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Password", rd.Next(100000,999999));
                    cmd.Parameters.AddWithValue("@PasswordMailSent", "False");
                    cmd.Parameters.AddWithValue("@ContactPerson", tbContactPerson.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ContactNo", tbCNo.Text);
                    cmd.Parameters.AddWithValue("@CentreName", tbCentreName.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Location", tbAddress.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Email", tbEAddress.Text);
                    cmd.Parameters.AddWithValue("@SCCodes", tbExamSCCodes.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Remark", ddlistRemark.SelectedItem.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@NoTimesLoggedIn",0);
                    cmd.Parameters.AddWithValue("@LastLogoutTime", "");
                 
                       
                    cmd.Connection = con;
                    con.Open();
                    object ecid = cmd.ExecuteScalar();
                    con.Close();

                    int ts= allotThisEC(Convert.ToInt32(ecid), tbExamSCCodes.Text);
                    Log.createLogNow("Create", "Register Examination Centre with code '" + tbECCode.Text + "' for " + ddlistExamination.SelectedItem.Text + " exam", Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "Examination Centre has been registered and alloted to '"+ts.ToString()+"' students successfully!!";
                    pnlMSG.Visible = true;
                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! this Examination Centre is already exist";
                    pnlMSG.Visible = true;

                }

            }

            else if (Request.QueryString["ECID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEExaminationCentres_" + ddlistExamination.SelectedItem.Value + " set City=@City,ExamCentreCode=@ExamCentreCode,ContactPerson=@ContactPerson,ContactNo=@ContactNo,CentreName=@CentreName,Location=@Location,Email=@Email,SCCodes=@SCCodes,Remark=@Remark where ECID='" + Request.QueryString["ECID"] + "'", con);

                cmd.Parameters.AddWithValue("@City", ddlistCity.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@ExamCentreCode", tbECCode.Text.ToUpper());
                cmd.Parameters.AddWithValue("@ContactPerson", tbContactPerson.Text.ToUpper());
                cmd.Parameters.AddWithValue("@ContactNo", tbCNo.Text);
                cmd.Parameters.AddWithValue("@CentreName", tbCentreName.Text.ToUpper());
                cmd.Parameters.AddWithValue("@Location", tbAddress.Text.ToUpper());
                cmd.Parameters.AddWithValue("@Email", tbEAddress.Text);
                cmd.Parameters.AddWithValue("@SCCodes", tbExamSCCodes.Text.ToUpper());
                cmd.Parameters.AddWithValue("@Remark", ddlistRemark.SelectedItem.Text.ToUpper());

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                int ts = 0;
                if (lblExamSCCodes.Text != tbExamSCCodes.Text)
                {
                    ts = allotThisEC(Convert.ToInt32(Request.QueryString["ECID"]), tbExamSCCodes.Text);
                }
                Log.createLogNow("Update", "Updated Examination Centre with code '" + tbECCode.Text + "' for " + ddlistExamination.SelectedItem.Value + " exam", Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                if (lblExamSCCodes.Text != tbExamSCCodes.Text)
                {
                    lblMSG.Text = "SC Codes has been updated and alloted to '" + ts.ToString() + "' students successfully!!";
                }
                else
                {
                    lblMSG.Text = "Record has been updated successfully !!";
                }
                pnlMSG.Visible = true;

            }

        }

        private int allotThisEC(int ecid, string sccodes)
        {
            int ts = 0;
            string[] sc = sccodes.Split(',');
            for (int i = 0; i < sc.Length; i++)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select distinct DDEExamRecord_" + ddlistExamination.SelectedItem.Value + ".SRID, DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode from DDEExamRecord_" + ddlistExamination.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExamination.SelectedItem.Value + ".SRID where DDEExamRecord_" + ddlistExamination.SelectedItem.Value + ".ExamCentreCode='0' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and DDEStudentRecord.StudyCentreCode='" + sc[i].ToString() + "') or (DDEStudentRecord.SCStatus='T' and DDEStudentRecord.PreviousSCCode='" + sc[i].ToString() + "'))", con);
                con.Open();
                dr = cmd.ExecuteReader();
              
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        updateExamCentre(Convert.ToInt32(dr["SRID"]), ecid);
                        ts = ts + 1;

                    }

                }

                con.Close();
            }

            return ts;
        }

        private void updateExamCentre(int srid, int ecid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEExamRecord_" + ddlistExamination.SelectedItem.Value + " set ExamCentreCode=@ExamCentreCode where SRID='" + srid + "'", con);
            cmd.Parameters.AddWithValue("@ExamCentreCode", ecid.ToString());

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddExamCentres.aspx");
        }
    }
}
