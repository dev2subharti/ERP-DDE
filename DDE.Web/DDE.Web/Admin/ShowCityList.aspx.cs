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
    public partial class ShowCityList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 89))
            {

                if (!IsPostBack)
                {

                    populateCities();


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

        private void populateCities()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from CityList order by City", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("CityID");
            DataColumn dtcol3 = new DataColumn("City");

            DataColumn dtcol4 = new DataColumn("State");
            DataColumn dtcol5 = new DataColumn("Country");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);



            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["CityID"] = Convert.ToString(dr["CityID"]);
                drow["City"] = Convert.ToString(dr["City"]);

                drow["State"] = Convert.ToString(dr["State"]);
                drow["Country"] = Convert.ToString(dr["Country"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowCity.DataSource = dt;
            dtlistShowCity.DataBind();

            con.Close();
        }

        protected void dtlistShowCity_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                Response.Redirect("AddCity.aspx?CityID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Delete")
            {
                string city = FindInfo.findCityByID(Convert.ToInt32(e.CommandArgument));
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from CityList where CityID ='" + Convert.ToString(e.CommandArgument) + "'", con);

                Log.createLogNow("Delete", "Delete a city '" +city  + "'", Convert.ToInt32(Session["ERID"].ToString()));
                con.Open();
                cmd.ExecuteReader();
                con.Close();
                populateCities();


            }
        }
    }
}
