using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace DDE.Web.Admin.MasterPages
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //try
                //{

                    lblWelcome.Text = "<b>WELCOME : " + Session["Name"].ToString() + "</b>, " + Session["DesignationName"].ToString() + ", " + Session["DepartmentName"].ToString() + ", " + Session["UnitName"] + ".";

                    if (Session["LastLogoutTime"].ToString() == "0")
                    {
                        lblLastLogoutTime.Text = "<b>LAST LOGOUT TIME WAS : </b> YOU LOGGED IN FIRST TIME";

                    }
                    else
                    {

                        string lastlogouttime = Session["LastLogoutTime"].ToString();
                        if (lastlogouttime == "")
                        {
                            lblLastLogoutTime.Text = "You haven't Logged out Properly Last Time";
                        }
                        else
                        {
                            string dt = Convert.ToString(Session["LastLogoutTime"].ToString());
                            //DateTime dt = (DateTime)(TypeDescriptor.GetConverter(new DateTime(1990, 5, 6)).ConvertFrom(Session["LastLogoutTime"].ToString())); 
                            lblLastLogoutTime.Text = "<b>LAST LOGOUT TIME WAS : </b>" + dt;
                        }

                        //+lastlogouttime.Substring(0, 2) + " " + FindInfo.findMonthByMonthNo(Convert.ToInt32(lastlogouttime.Substring(3, 2))) + " " + lastlogouttime.Substring(6, 16);

                        //lblLastLogoutTime.Text = "<b>LAST LOGOUT TIME WAS : </b>" + Session["LastLogoutTime"].ToString();

                    }
                //}
                //catch
                //{
                //    Response.Write("javascript:alert('Sorry !! Session expired. Please login again.');");
                //    FormsAuthentication.SignOut();
                //    Session.Abandon();
                //    Response.Redirect("~/Default.aspx");
                //}
            }

        }
       
    }
}
