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

namespace DDE.Web.Admin
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgEmployee.ImageUrl = "../EmployeePhotos/" + Session["EmployeeID"].ToString()+".jpg";
            lblName.Text = Session["Name"].ToString() + ", " + Session["DesignationName"].ToString() + ", " + Session["DepartmentName"].ToString() + ", " + Session["UnitName"] + ".";
        }
    }
}
