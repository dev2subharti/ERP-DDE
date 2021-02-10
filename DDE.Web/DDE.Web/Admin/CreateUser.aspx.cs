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
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 2))
            {
                if (!IsPostBack)
                {
                    populateEmployee();
                   
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

        private void populateEmployee()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select ERID,Name from SVSUEmployeeRecord where Department='41'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlistEmployee.Items.Add(new ListItem(dr[1].ToString(),dr[0].ToString()));
               
            }

            con.Close();
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            if (Authorisation.isUser(Convert.ToInt32(ddlistEmployee.SelectedItem.Value)) == false)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into Users values(@ERID,@Unit,@CreatedBy,@LoginTypeID,@TimeOfCreation)", con);

                cmd.Parameters.AddWithValue("@ERID", ddlistEmployee.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@Unit", 1);
                cmd.Parameters.AddWithValue("@CreatedBy", Session["ERID"].ToString());
                cmd.Parameters.AddWithValue("@LoginTypeID", 14);
                cmd.Parameters.AddWithValue("@TimeOfCreation", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Create", "Created a user '" +FindInfo.findLinearEmployeeDetailByERID(Convert.ToInt32(ddlistEmployee.SelectedItem.Value)), Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "User has been created successfully !!";
                pnlMSG.Visible = true;
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! This User is already exist";
                pnlMSG.Visible = true;

            }
        }
    }
}
