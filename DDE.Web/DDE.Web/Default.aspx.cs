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
namespace DDE.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
        }
       
        protected void lnkbtnUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin/CPFirstTime.aspx?LoginType=" + "14");
        }

        protected void lnkbtnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin/CPFirstTime.aspx?LoginType=" + "15");
        }

        protected void lnkbtnSC_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Admin/CPFirstTime.aspx?LoginType=" + "16");
        }

        protected void lnkbtnStudents_Click(object sender, EventArgs e)
        {

            //Response.Redirect("Admin/CPFirstTime.aspx?LoginType=" + "17");
            
        }

        

        
    }
            
    
}
