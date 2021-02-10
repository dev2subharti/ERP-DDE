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
    public partial class ShowMyLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 4))
            {

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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateLogs();
        }

        private void populateLogs()
        {
            string from = ddlistDOAYearFrom.SelectedItem.Text + "-" + ddlistDOAMonthFrom.SelectedItem.Value + "-" + ddlistDOADayFrom.SelectedItem.Text;
            string to = ddlistDOAYearTo.SelectedItem.Text + "-" + ddlistDOAMonthTo.SelectedItem.Value + "-" + ddlistDOADayTo.SelectedItem.Text;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

         
            cmd.CommandText = "select * from DDELog where ERID='" +Convert.ToInt32(Session["ERID"]) + "' and cast(CONVERT(datetime, EventDateTime, 104) AS date)>='" + from + "' and cast(CONVERT(datetime, EventDateTime, 104) AS date)<='" + to + "' order by EventID";
               
            cmd.Connection = con;
            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("Event");
            DataColumn dtcol3 = new DataColumn("Description");

            DataColumn dtcol4 = new DataColumn("Time");
            DataColumn dtcol5 = new DataColumn("Done By");


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
                drow["Event"] = Convert.ToString(dr["EventType"]);
                drow["Description"] = Convert.ToString(dr["EventDescription"]);
                drow["Time"] = Convert.ToString(dr["EventDateTime"]);
                drow["Done By"] = FindInfo.findEmployeeNameByERID(Convert.ToInt32(dr["ERID"]));
                dt.Rows.Add(drow);
                i = i + 1;
            }

            gvLog.DataSource = dt;
            gvLog.DataBind();


            con.Close();

            if (i > 1)
            {
                gvLog.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                gvLog.Visible = false;
                lblMSG.Text = "Sorry! No Record Found.";
                pnlMSG.Visible = true;
            }
        }
    }
}