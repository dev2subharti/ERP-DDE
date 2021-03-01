using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class adminlogin : System.Web.UI.Page
    {

       
        Log log = new Log();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["LoginType"] == "14")
            {
                lblRole.Text = "User Login";
            }
            else if (Request.QueryString["LoginType"] == "15")
            {
                lblRole.Text = "Admin Login";
            }          
            else if (Request.QueryString["LoginType"] == "16")
            {
                lblRole.Text = "Study Centre Login";
            }
            else if (Request.QueryString["LoginType"] == "17")
            {
                lblRole.Text = "Student Login";
            }
            else
            {             
                Response.Redirect("../Default.aspx");            
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string st;

                try
                {
                    if (Request.QueryString["LoginType"] == "15")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());

                        SqlCommand cmd = new SqlCommand("select EmployeePassword,NoTimesLoggedIn from SVSUEmployeeRecord where EmployeeID='" + txtUserName.Text + "'  and Department='41' and Designation='19' and RecordStatus='True'", con);

                        SqlDataReader dr;

                        findEmployeeInfo(txtUserName.Text);


                        if (validUser(Convert.ToInt32(Session["ERID"])))
                        {

                            con.Open();
                            dr = cmd.ExecuteReader();
                            dr.Read();
                            Session["NoTimesLoggedIn"] = dr[1].ToString();

                            st = dr[1].ToString();
                            Security s1 = new Security();
                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                            if (dr[1].ToString() == "0")
                            {


                                if ((dr[0].ToString() == txtPwd.Text))
                                {

                                    Log.IncreaseLoggedIntimes(st, Convert.ToInt32(Session["ERID"]));
                                    Session["LoginType"] = Request.QueryString["LoginType"].ToString();
                                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                                }
                                else
                                {
                                    lblError.Text = "Please enter a currect password";
                                    lblError.Visible = true;

                                }


                            }
                            else
                            {
                                if (dr[0].ToString() == s1.EncryptOneWay(txtPwd.Text))
                                {


                                    Log.IncreaseLoggedIntimes(st, Convert.ToInt32(Session["ERID"]));
                                    Session["LoginType"] = Request.QueryString["LoginType"].ToString();
                                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                                }
                                else
                                {
                                    lblError.Text = "Please enter a currect password";
                                    lblError.Visible = true;

                                }


                            }


                            con.Close();
                        }

                        else
                        {
                            lblError.Text = "Sorry !! You are not authorised to login here";
                            lblError.Visible = true;

                        }
                    }
                    else if (Request.QueryString["LoginType"] == "14")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());

                        SqlCommand cmd = new SqlCommand("select EmployeePassword,NoTimesLoggedIn from SVSUEmployeeRecord where EmployeeID='" + txtUserName.Text + "' and Department='41' and RecordStatus='True'", con);

                        SqlDataReader dr;

                        findEmployeeInfo(txtUserName.Text);


                        if (validUser(Convert.ToInt32(Session["ERID"])))
                        {

                            con.Open();
                            dr = cmd.ExecuteReader();
                            dr.Read();
                            Session["NoTimesLoggedIn"] = dr[1].ToString();

                            st = dr[1].ToString();
                            Security s1 = new Security();
                            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                            if (dr[1].ToString() == "0")
                            {


                                if ((dr[0].ToString() == txtPwd.Text))
                                {

                                    Log.IncreaseLoggedIntimes(st, Convert.ToInt32(Session["ERID"]));
                                    Session["LoginType"] = Request.QueryString["LoginType"].ToString();
                                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                                }
                                else
                                {
                                    lblError.Text = "Please enter a currect password";
                                    lblError.Visible = true;

                                }


                            }
                            else
                            {
                                if (dr[0].ToString() == s1.EncryptOneWay(txtPwd.Text))
                                {
                                    Log.IncreaseLoggedIntimes(st, Convert.ToInt32(Session["ERID"]));
                                    Session["LoginType"] = Request.QueryString["LoginType"].ToString();
                                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                                }
                                else
                                {
                                    lblError.Text = "Please enter a currect password";
                                    lblError.Visible = true;

                                }


                            }


                            con.Close();
                        }

                        else
                        {
                            lblError.Text = "Sorry !! You are not authorised to login here";
                            lblError.Visible = true;

                        }
                    }

                   
                }

                catch(Exception ex)
                {
                    lblError.Text = "Please enter a valid user name";
                    lblError.Visible = true;
                }
                 
        }



        private bool validUser(int erid)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand scmd = new SqlCommand("Select LoginTypeID from Users where ERID='" + erid + "' ", con);
            SqlDataReader dr;


            bool vuser = false;
            con.Open();
            dr = scmd.ExecuteReader();
            dr.Read();
            if (dr[0].ToString() == Request.QueryString["LoginType"])
            {
                vuser = true;
            }
            con.Close();

            return vuser;

        }

        private void findEmployeeInfo(string empid)
        {
            if (Request.QueryString["LoginType"] == "14" || Request.QueryString["LoginType"] == "15")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                SqlCommand scmd = new SqlCommand("Select ERID,Name,CollegeOrUnit,Department,EmployeeType,Designation from SVSUEmployeeRecord where EmployeeID ='" + empid + "' and RecordStatus='True'", con);
                SqlDataReader dr;
                con.Open();
                dr = scmd.ExecuteReader();
                while (dr.Read())
                {
                    Session["ERID"] = dr["ERID"].ToString();
                    Session["EmployeeID"] = empid.ToString();
                    Session["Name"] = dr["Name"].ToString();
                    Session["UnitID"] = Convert.ToInt32(dr["CollegeOrUnit"]);
                    Session["UnitName"] = FindInfo.findUnitNameByUnitID(Convert.ToInt32(dr["CollegeOrUnit"]));
                    Session["DepartmentName"] = FindInfo.findDepartmentNameByID(Convert.ToInt32(dr["Department"]));
                    Session["EmployeeType"] = dr["EmployeeType"].ToString();
                    Session["DesignationName"] = FindInfo.findDesignationNameByID(Convert.ToInt32(dr["Designation"]));

                }
                con.Close();

                string lastlogouttime = log.getLastLogoutTime(Session["ERID"].ToString());
                Session["LastLogoutTime"] = lastlogouttime;
            }

          
                
        }





        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPwd.Text = "";

        }



    }
}
