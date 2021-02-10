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
    public partial class AddStudyCentre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 39))
            {

                if (!IsPostBack)
                {
                    
                    PopulateDDList.populateCity(ddlistCity, "UTTAR PRADESH");
                    PopulateDDList.populateCity(ddlistDistrict, "UTTAR PRADESH");

                    if (Request.QueryString["SCID"] != null)
                    {
                        populateSCRecord();
                        setValidation();
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



       

        private void populateSCRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEStudyCentres where SCID='" + Request.QueryString["SCID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                if (dr["Mode"].ToString() == "True")
                {
                    tbSCCode.Text = dr["SCCode"].ToString();
                    ddlistMode.Items.FindByValue("1").Selected = true;

                }
                else if (dr["Mode"].ToString() == "False")
                {
                    tbSCCode.Text = dr["ProSCCode"].ToString();
                    ddlistMode.Items.FindByValue("0").Selected = true;
                }
                ddlistMode.Enabled = false;
                tbSCCode.Enabled = false;
                tbSCName.Text = dr["Location"].ToString();
                ddlistRBy.Items.FindByText(dr["RecommendedBy"].ToString()).Selected = true;
                tbAdmin.Text = dr["Administrator"].ToString();
                //ddlistAllowed.Items.FindByText(dr["AdminAllowed"].ToString()).Selected = true;
                tbAddress.Text = dr["Address"].ToString();
                ddlistCity.Items.FindByText(dr["City"].ToString()).Selected = true;
                ddlistDistrict.Items.FindByText(dr["District"].ToString()).Selected = true;
                ddlistArea.Items.FindByValue(dr["Area"].ToString()).Selected = true;
                tbMNo.Text = dr["MobileNo"].ToString();
                tbPNo.Text = dr["LandlineNo"].ToString();
                tbEAddress.Text = dr["Email"].ToString();
                tbDate.Text = dr["DOI"].ToString();
                tbANo.Text = dr["ANo"].ToString();
                tbBankName.Text = dr["BankName"].ToString();
                tbIFSC.Text = dr["IFSCCode"].ToString();
            }

            con.Close();
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            if (Request.QueryString["SCID"] == null)
            {
                if (!FindInfo.StudyCentreExist(tbSCCode.Text))
                {

                    Random rd = new Random();

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEStudyCentres values(@Mode,@ProSCCode,@SCCode,@Password,@Location,@RecommendedBy,@Administrator,@AdminAllowed,@Address,@City,@District,@Area,@MobileNo,@LandlineNo,@Email,@DOI,@NoTimesLoggedIn,@LastLogoutTime,@PasswordMailSent,@ANo,@BankName,@IFSCCode,@REFor,@RFrom,@RTo,@Online,@Authorised)", con);



                    if (ddlistMode.SelectedItem.Value == "1")
                    {
                        cmd.Parameters.AddWithValue("@Mode", "True");
                        cmd.Parameters.AddWithValue("@ProSCCode", "");
                        cmd.Parameters.AddWithValue("@SCCode", tbSCCode.Text.ToUpper());
                    }
                    else if (ddlistMode.SelectedItem.Value == "0")
                    {
                        cmd.Parameters.AddWithValue("@Mode", "False");
                        cmd.Parameters.AddWithValue("@ProSCCode", tbSCCode.Text.ToUpper());
                        cmd.Parameters.AddWithValue("@SCCode", tbSCCode.Text.ToUpper());
                    }
                    cmd.Parameters.AddWithValue("@Password", rd.Next(100000, 999999));
                    cmd.Parameters.AddWithValue("@Location", tbSCName.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@RecommendedBy", ddlistRBy.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Administrator", tbAdmin.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@AdminAllowed", ddlistAllowed.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Address", tbAddress.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@City", ddlistCity.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@District", ddlistDistrict.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Area", ddlistArea.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@MobileNo", tbMNo.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@LandlineNo", tbPNo.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@Email", tbEAddress.Text);
                    cmd.Parameters.AddWithValue("@DOI", tbDate.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@NoTimesLoggedIn", "0");
                    cmd.Parameters.AddWithValue("@LastLogoutTime", "");
                    cmd.Parameters.AddWithValue("@PasswordMailSent", "False");
                    cmd.Parameters.AddWithValue("@ANo", tbANo.Text);
                    cmd.Parameters.AddWithValue("@BankName", tbBankName.Text);
                    cmd.Parameters.AddWithValue("@IFSCCode", tbIFSC.Text);
                   
                    cmd.Parameters.AddWithValue("@REFor", "");
                    cmd.Parameters.AddWithValue("@RFrom", "");
                    cmd.Parameters.AddWithValue("@RTo", "");
                    cmd.Parameters.AddWithValue("@Online", "OFFLINE");
                    cmd.Parameters.AddWithValue("@Authorised", "True");

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Log.createLogNow("Create", "Register Study Centre with SC Code '" + tbSCCode.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));

                    if (ddlistMode.SelectedItem.Value == "0")
                    {
                        updateProSCCodeCounter(Convert.ToInt32(ViewState["ProSCCodeCounter"]));
                    }
                    pnlData.Visible = false;
                    lblMSG.Text = "Study Centre has been registered successfully !!";
                    pnlMSG.Visible = true;
                }

                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! this Study Centre is already exist";
                    pnlMSG.Visible = true;

                }

            }

            else if (Request.QueryString["SCID"] != null)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudyCentres set Location=@Location,RecommendedBy=@RecommendedBy,Administrator=@Administrator,AdminAllowed=@AdminAllowed,Address=@Address,City=@City,District=@District,Area=@Area,MobileNo=@MobileNo,LandlineNo=@LandlineNo,Email=@Email,DOI=@DOI,ANo=@ANo,BankName=@BankName,IFSCCode=@IFSCCode where SCID='" + Request.QueryString["SCID"] + "'", con);


                cmd.Parameters.AddWithValue("@Location", tbSCName.Text.ToUpper());
                cmd.Parameters.AddWithValue("@RecommendedBy", ddlistRBy.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Administrator", tbAdmin.Text.ToUpper());
                cmd.Parameters.AddWithValue("@AdminAllowed", ddlistAllowed.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Address", tbAddress.Text.ToUpper());
                cmd.Parameters.AddWithValue("@City", ddlistCity.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@District", ddlistDistrict.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Area", ddlistArea.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@MobileNo", tbMNo.Text.ToUpper());
                cmd.Parameters.AddWithValue("@LandlineNo", tbPNo.Text.ToUpper());
                cmd.Parameters.AddWithValue("@Email", tbEAddress.Text);
                cmd.Parameters.AddWithValue("@DOI", tbDate.Text.ToUpper());
                cmd.Parameters.AddWithValue("@ANo", tbANo.Text);
                cmd.Parameters.AddWithValue("@BankName", tbBankName.Text);
                cmd.Parameters.AddWithValue("@IFSCCode", tbIFSC.Text);


                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Updated Study Centre Record with SC Code '" + tbSCCode.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Record has been updated successfully !!";
                pnlMSG.Visible = true;


            }

        }

        private void updateProSCCodeCounter(int counter)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDECounters set CounterValue=@CounterValue where CounterName='ProSCCodeCounter' ", con);
            cmd.Parameters.AddWithValue("@CounterValue", counter);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void ddlistMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            setValidation();
            if (ddlistMode.SelectedItem.Value == "1")
            {
                tbSCCode.Enabled = true;
                tbSCCode.Text = "";
               
            }
            else if (ddlistMode.SelectedItem.Value == "0")
            {
                int counter = FindInfo.findSCProNo();
                ViewState["ProSCCodeCounter"] = counter;
                if (counter != 0)
                {
                    tbSCCode.Text = "PR-" + (counter).ToString();
                }
                tbSCCode.Enabled = false;
               
            }
           
        }

        private void setValidation()
        {
            if (ddlistMode.SelectedItem.Value == "1")
            {               
                rfvANo.Enabled = true;
                rfvBankName.Enabled = true;
                rfvIFSC.Enabled = true;
            }
            else if (ddlistMode.SelectedItem.Value == "0")
            {               
                rfvANo.Enabled = false;
                rfvBankName.Enabled = false;
                rfvIFSC.Enabled = false;
            }
        }

      
    }
}
