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
    public partial class PublishPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 2))
            {
                if (!IsPostBack)
                {
                    if (Authorisation.isUser(Convert.ToInt32(Request.QueryString["ERID"])))
                    {
                        if (Authorisation.userCreatedByMe(Convert.ToInt32(Session["ERID"]), Convert.ToInt32(Request.QueryString["ERID"])))
                        {

                            populateEmployeeInfo();
                            pnlData.Visible = true;
                            pnlMSG.Visible = false;


                        }

                        else
                        {
                            pnlData.Visible = false;
                            lblMSG.Text = "Sorry !! You can not publish password of this Employee";
                            pnlMSG.Visible = true;

                        }
                       
                    }

                    else
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! this is not a user";
                        pnlMSG.Visible = true;

                    }

                    
                   

                }
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

           

        }

        private void populateEmployeeInfo()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSsvsudb"].ToString());
            SqlCommand cmd = new SqlCommand("select PhotoID,EmployeeID,Name,EmployeePassword,CollegeOrUnit,Department,Designation from SVSUEmployeeRecord where ERID='" + Request.QueryString["ERID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgEmployee.ImageUrl = "~/EmployeePhotos/" + dr["PhotoID"].ToString();

                lblEName.Text = dr["Name"].ToString();
                lblDesignation.Text = FindInfo.findDesignationNameByID(Convert.ToInt32(dr["Designation"]));
                lblUnit.Text = FindInfo.findUnitNameByUnitID(Convert.ToInt32(dr["CollegeOrUnit"]));
                lblEmpDepartment.Text = FindInfo.findDepartmentNameByID(Convert.ToInt32(dr["Department"]));
               
                lblGateway.Text = "Now click on <b>Users</b> button.";
                
                lblUserID.Text = dr["EmployeeID"].ToString();
                lblPassword.Text = dr["EmployeePassword"].ToString();

            }

            con.Close();

        }
    }
}
