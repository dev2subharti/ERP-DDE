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
    public partial class AssignRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 2))
            {
                if (!IsPostBack)
                {
                    populateEmployeeInfo();
                    populateRoles();

                    if (AllreadyAssigned(Request.QueryString["ERID"].ToString()))
                    {
                        populateAssignedRoles(Request.QueryString["ERID"].ToString());
                        btnAssignRoles.Text = "Update Roles";
                    }

                    else
                    {
                        btnAssignRoles.Text = "Assign Roles";
                    }


                    pnlData.Visible = true;
                    pnlMSG.Visible = false;

                }
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        private void populateAssignedRoles(string erid)
        {
            foreach (DataListItem dli in dtlistAssignRoles.Items)
            {


                Label Roleid = (Label)dli.FindControl("lblRoleID");
                CheckBox cb = (CheckBox)dli.FindControl("cbAssign");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                SqlCommand cmd = new SqlCommand("select AssignedRoleID from AssignedRoles where ERID='" + erid + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (Roleid.Text == dr[0].ToString())
                    {
                        cb.Checked = true;
                    }


                }

                con.Close();



            }
        }

        private bool AllreadyAssigned(string erid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct ERID from AssignedRoles", con);
            con.Open();
            dr = cmd.ExecuteReader();

            bool assigned = false;
            while (dr.Read())
            {

                if (dr[0].ToString() == erid)
                {

                    assigned = true;
                    break;

                }



            }

            con.Close();

            return assigned;
        }

        private void populateRoles()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from AssignedRoles where ERID='" + Session["ERID"].ToString() + "' order by DomainID", con);
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


            dtlistAssignRoles.DataSource = dt;
            dtlistAssignRoles.DataBind();

            con.Close();


        }



        private void populateEmployeeInfo()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select EmployeeID,Name,CollegeOrUnit,Department,Designation from SVSUEmployeeRecord where ERID='" + Request.QueryString["ERID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgEmployee.ImageUrl = "~/EmployeePhotos/" + dr["EmployeeID"].ToString() + ".jpg";
                lblEmpID.Text = dr["EmployeeID"].ToString();
                lblEName.Text = dr["Name"].ToString();
                lblDesignation.Text = FindInfo.findDesignationNameByID(Convert.ToInt32(dr["Designation"]));
                lblUnit.Text = FindInfo.findUnitNameByUnitID(Convert.ToInt32(dr["CollegeOrUnit"]));
                lblEmpDepartment.Text = FindInfo.findDepartmentNameByID(Convert.ToInt32(dr["Department"]));



            }

            con.Close();

        }


        protected void btnAssignRoles_Click(object sender, EventArgs e)
        {
            if (btnAssignRoles.Text == "Assign Roles")
            {

                foreach (DataListItem dli in dtlistAssignRoles.Items)
                {


                    Label Roleid = (Label)dli.FindControl("lblRoleID");
                    CheckBox cb = (CheckBox)dli.FindControl("cbAssign");

                    if (cb.Checked)
                    {


                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into AssignedRoles values(@ERID,@AssignedRoleID,@UnitID,@DomainID,@AssignedBy,@TimeOfAssignment)", con);


                        cmd.Parameters.AddWithValue("@ERID", Convert.ToInt32(Request.QueryString["ERID"]));
                        cmd.Parameters.AddWithValue("@AssignedRoleID", Convert.ToInt32(Roleid.Text));
                        cmd.Parameters.AddWithValue("@UnitID", FindInfo.findUnitIDByUnitName(lblUnit.Text));
                        cmd.Parameters.AddWithValue("@DomainID", FindInfo.findDomainIDByRollID(Convert.ToInt32(Roleid.Text)));
                        cmd.Parameters.AddWithValue("@AssignedBy", Convert.ToInt32(Session["ERID"]));
                        cmd.Parameters.AddWithValue("@TimeOfAssignment", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss tt"));



                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        Log.createLogNow("Assign", "Assigned roles to '"+lblEName.Text+" ("+lblEmpID.Text+"), "+lblDesignation.Text+", "+lblEmpDepartment.Text , Convert.ToInt32(Session["ERID"].ToString()));
                        
                    }
                }


                pnlData.Visible = false;
                lblMSG.Text = "Roles has been assigned successfully !!";
                pnlMSG.Visible = true;

            }


            else if (btnAssignRoles.Text == "Update Roles")
            {
                foreach (DataListItem dli in dtlistAssignRoles.Items)
                {


                    Label Roleid = (Label)dli.FindControl("lblRoleID");
                    CheckBox cb = (CheckBox)dli.FindControl("cbAssign");

                    if (RoleAssigned(Roleid.Text) == true)
                    {
                        if (cb.Checked == false)
                        {
                            deleteAssignedRole(Roleid.Text);

                        }

                    }

                    else
                    {
                        if (cb.Checked)
                        {

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
                            SqlCommand cmd = new SqlCommand("insert into AssignedRoles values(@ERID,@AssignedRoleID,@UnitID,@DomainID,@AssignedBy,@TimeOfAssignment)", con);


                            cmd.Parameters.AddWithValue("@ERID", Convert.ToInt32(Request.QueryString["ERID"]));
                            cmd.Parameters.AddWithValue("@AssignedRoleID", Convert.ToInt32(Roleid.Text));
                            cmd.Parameters.AddWithValue("@UnitID", FindInfo.findUnitIDByUnitName(lblUnit.Text));
                            cmd.Parameters.AddWithValue("@DomainID", FindInfo.findDomainIDByRollID(Convert.ToInt32(Roleid.Text)));
                            cmd.Parameters.AddWithValue("@AssignedBy", Convert.ToInt32(Session["ERID"]));
                            cmd.Parameters.AddWithValue("@TimeOfAssignment", DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss tt"));



                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            Log.createLogNow("Update", "Updated roles to '" + lblEName.Text + " (" + lblEmpID.Text + "), " + lblDesignation.Text + ", " + lblEmpDepartment.Text, Convert.ToInt32(Session["ERID"].ToString()));
                        }

                    }









                }


                pnlData.Visible = false;
                lblMSG.Text = "Roles has been updated successfully !!";
                pnlMSG.Visible = true;


            }


        }

        private bool RoleAssigned(string Roleid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select distinct ERID,AssignedRoleID from AssignedRoles", con);
            con.Open();
            dr = cmd.ExecuteReader();

            bool assigned = false;
            while (dr.Read())
            {

                if (dr[0].ToString() == Request.QueryString["ERID"] && dr[1].ToString() == Roleid)
                {

                    assigned = true;
                    break;

                }



            }

            con.Close();

            return assigned;

        }

        private void deleteAssignedRole(string Roleid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("delete from AssignedRoles where ERID='" + Request.QueryString["ERID"].ToString() + "' and AssignedRoleID='" + Roleid + "'", con);



            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
