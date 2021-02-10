using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DDE.DAL;

namespace DDE.Web.Admin.UserControls
{
    public partial class AdminHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkbtnAccountAdminSignout_Click(object sender, EventArgs e)
        {
            Log log = new Log();
            log.LoginTimingsLogout(Convert.ToString(Session["ERID"]));

            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}