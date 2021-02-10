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

namespace DDE.Web.Admin.UserControls
{
    public partial class StudyCentre : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkbtnAccountAdminSignout_Click(object sender, EventArgs e)
        {

            Log.setLastLogoutTime(Session["SCCode"].ToString());
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }
    }
}