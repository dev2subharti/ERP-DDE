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
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Security s1 = new Security();

           
            
            if (s1.EncryptOneWay(tbPriviousPassword.Text) == getPrePassword())
            {       
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                SqlCommand cmd = new SqlCommand("update SVSUEmployeeRecord set EmployeePassword=@EmployeePassword where ERID ='" + Session["ERID"].ToString() + "'", con);

                cmd.Parameters.AddWithValue("@EmployeePassword", s1.EncryptOneWay(tbNewPassword.Text));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

              
               pnlData.Visible = false;
               lblMSG.Text = "Password has been changed successfully";
               pnlMSG.Visible = true;

            }

            else
            {

                lblMSG.Text = "Sorry !! Current password is not correct";
                pnlMSG.Visible = true;

            }
        }

        private string getPrePassword()
        {
            string pass = "";

           
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                SqlCommand cmd = new SqlCommand("select EmployeePassword from SVSUEmployeeRecord  where ERID ='" + Session["ERID"].ToString() + "'", con);

                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pass = dr[0].ToString();


                }

                con.Close();
          
            

            return pass;
        }



    }
}
