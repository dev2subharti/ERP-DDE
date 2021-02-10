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
    public partial class ShowMyRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 4))
            {
                if (!IsPostBack)
                {

                    populateRoles(Convert.ToInt32(Session["ERID"]));
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;

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

        private void populateRoles(int erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from AssignedRoles where ERID='" + erid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("RoleID");
            DataColumn dtcol3 = new DataColumn("RoleName");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["RoleID"] = Convert.ToString(dr["AssignedRoleID"]);
                drow["RoleName"] = FindInfo.findRoleNameByRollID(Convert.ToInt32(dr["AssignedRoleID"]));

                dt.Rows.Add(drow);
                i = i + 1;

            }


            dtlistMyRoles.DataSource = dt;
            dtlistMyRoles.DataBind();

            con.Close();
        }
    }
}
