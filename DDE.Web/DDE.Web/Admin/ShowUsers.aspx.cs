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
    public partial class ShowUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 2))
            {
                if (!IsPostBack)
                {


                    populateUsers();


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

        private void populateUsers()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from Users where CreatedBy='" + Session["ERID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ERID");
            DataColumn dtcol3 = new DataColumn("PhotoID");
            DataColumn dtcol4 = new DataColumn("UserDetail");





            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);




            int i = 1;
            while (dr.Read())
            {


                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["ERID"] = dr["ERID"].ToString();
                addEmpInfo(drow, dr["ERID"].ToString());
                dt.Rows.Add(drow);
                i = i + 1;

            }


            dtlistShowUser.DataSource = dt;
            dtlistShowUser.DataBind();

            con.Close();

        }

        private void addEmpInfo(DataRow drow, string erid)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select PhotoID,Name,Designation,CollegeOrUnit,Department from SVSUEmployeeRecord where ERID='" + erid + "'", con);
            SqlDataReader dr;



            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                drow["PhotoID"] = "../EmployeePhotos/" + dr["PhotoID"].ToString();
                drow["UserDetail"] = FindInfo.findEmployeeDetailByERID(Convert.ToInt32(erid));



            }

            con.Close();



        }



        protected void dtlistShowUser_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Assign Role")
            {
                Response.Redirect("AssignRole.aspx?ERID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Show Assigned Role")
            {
                Response.Redirect("ShowAssignedRoles.aspx?ERID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Publish Password")
            {
                Response.Redirect("PublishPassword.aspx?ERID=" + Convert.ToString(e.CommandArgument));
            }
            else if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from Users where ERID ='" + Convert.ToInt32(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                deleteAllRoles(Convert.ToInt32(e.CommandArgument));

                Log.createLogNow("Delete", "Delete a user '" + FindInfo.findLinearEmployeeDetailByERID(Convert.ToInt32(e.CommandArgument)) + "'", Convert.ToInt32(Session["ERID"].ToString()));
               
                populateUsers();

            }


        }

        private void deleteAllRoles(int ERID)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("delete from AssignedRoles where ERID ='"+ERID+"'", con);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
