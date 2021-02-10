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

namespace DDE.Web.Admin.MasterPages
{
    public partial class StudyCentre : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblWelcome.Text = "<b>WELCOME : " + Session["Admin"].ToString();

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
                        lblLastLogoutTime.Text = "<b>LAST LOGOUT TIME WAS : </b>" + dt;
                    }



                }
            }
        }
    }
}
