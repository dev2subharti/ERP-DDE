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
    public partial class ShowStudyCentres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 39))
            {

                if (!IsPostBack)
                {

                    populateStudyCentres();

                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 40))
            {

                if (!IsPostBack)
                {

                    populateStudyCentres();
                    setAccessibility();

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

        private void setAccessibility()
        {
            foreach (DataListItem dli in dtlistShowStudyCentres.Items)
            {
                Button mode = (Button)dli.FindControl("btnMode");
                Button online = (Button)dli.FindControl("btnOnline");
                LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");
                LinkButton delete = (LinkButton)dli.FindControl("lnkbtnDelete");

                mode.Visible = false;
                online.Visible = false;
                edit.Visible = false;
                delete.Visible = false;


            }
        }

        private void populateStudyCentres()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudyCentres order by SCCode", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("City");
            DataColumn dtcol5 = new DataColumn("SCName");
            DataColumn dtcol6 = new DataColumn("Online");
            DataColumn dtcol7 = new DataColumn("Authorised");
          

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SCID"] = Convert.ToString(dr["SCID"]);
                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                drow["City"] = Convert.ToString(dr["City"]);
                drow["SCName"] = Convert.ToString(dr["Location"]);
                drow["Online"] = Convert.ToString(dr["Online"]);
                if (Convert.ToString(dr["Authorised"]) == "True")
                {
                    drow["Authorised"] = "OK";
                }
                else if (Convert.ToString(dr["Authorised"]) == "False")
                {
                    drow["Authorised"] = "Canceled";
                }
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowStudyCentres.DataSource = dt;
            dtlistShowStudyCentres.DataBind();

            con.Close();

            foreach (DataListItem dli in dtlistShowStudyCentres.Items)
            {

                Button Mode = (Button)dli.FindControl("btnMode");
                Button OLStatus = (Button)dli.FindControl("btnOnline");
                if (OLStatus.Text == "ONLINE")
                {
                    OLStatus.Text = "MAKE OFFLINE";
                    OLStatus.CommandName = "MAKE OFFLINE";
                    OLStatus.BackColor = System.Drawing.Color.Green;
                    OLStatus.ForeColor = System.Drawing.Color.White;
                }

                else if (OLStatus.Text == "OFFLINE")
                {
                    OLStatus.Text = "MAKE ONLINE";
                    OLStatus.CommandName = "MAKE ONLINE";
                    OLStatus.BackColor = System.Drawing.Color.Orange;
                    OLStatus.ForeColor = System.Drawing.Color.White;
                }

                if (Mode.Text == "OK")
                {
                    Mode.Text = "MAKE CANCEL";
                    Mode.CommandName = "MAKE CANCEL";
                    Mode.BackColor = System.Drawing.Color.Yellow;
                    Mode.ForeColor = System.Drawing.Color.Black;
                }

                else if (Mode.Text == "Canceled")
                {
                    Mode.Text = "MAKE OK";
                    Mode.CommandName = "MAKE OK";
                    Mode.BackColor = System.Drawing.Color.Red;
                    Mode.ForeColor = System.Drawing.Color.White;
                }

            }

        }

       

        protected void dtlistShowStudyCentres_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "MAKE OK")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudyCentres set Authorised=@Authorised where SCID='" + Convert.ToString(e.CommandArgument) + "' ", con);
                cmd.Parameters.AddWithValue("@Authorised", "True");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Made StudyCentre 'OK' with SC Code '" + FindInfo.findSCCodeByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateStudyCentres();


            }

            else if (e.CommandName == "MAKE CANCEL")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudyCentres set Authorised=@Authorised where SCID='" + Convert.ToString(e.CommandArgument) + "' ", con);
                cmd.Parameters.AddWithValue("@Authorised", "False");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Made StudyCentre 'CANCELED' with SC Code '" + FindInfo.findSCCodeByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateStudyCentres();


            }
            else if (e.CommandName == "MAKE ONLINE")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudyCentres set Online=@Online where SCID='" + Convert.ToString(e.CommandArgument) + "' ", con);
                cmd.Parameters.AddWithValue("@Online", "ONLINE");
            
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Made StudyCentre 'ONLINE' with SC Code '" + FindInfo.findSCCodeByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateStudyCentres();


            }

            else  if (e.CommandName == "MAKE OFFLINE")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEStudyCentres set Online=@Online where SCID='" + Convert.ToString(e.CommandArgument) + "' ", con);
                cmd.Parameters.AddWithValue("@Online", "OFFLINE");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Update", "Made StudyCentre 'OFFLINE' with SC Code '" + FindInfo.findSCCodeByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateStudyCentres();


            }
            else if (e.CommandName == "Edit")
            {

                Response.Redirect("AddStudyCentre.aspx?SCID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEStudyCentres where SCID ='" + Convert.ToString(e.CommandArgument) + "'", con);

                Log.createLogNow("Delete", "Delete a StudyCentre with SC Code '" + FindInfo.findSCCodeByID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                

                populateStudyCentres();


            }
        }
    }
}
