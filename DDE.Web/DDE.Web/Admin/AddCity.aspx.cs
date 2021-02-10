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
    public partial class AddCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 89))
            {
                PopulateDDList.populateState(ddlistState);
                if (Request.QueryString["CityID"] != null)
                {
                    populateCityByID();
                    btnSubmit.Text = "Update";
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

        private void populateCityByID()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from CityList where CityID='" + Request.QueryString["CityID"] + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbCity.Text = dr["City"].ToString().ToUpper();
                ddlistState.Items.FindByText(dr["State"].ToString()).Selected = true;
                ddlistCountry.Items.FindByText(dr["Country"].ToString()).Selected = true;                

            }

            con.Close();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CityID"] == null)
            {
                if (!FindInfo.isCityExist(tbCity.Text.ToUpper()))
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into CityList values(@City,@State,@Country)", con);

                    cmd.Parameters.AddWithValue("@City", tbCity.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@State", ddlistState.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlistCountry.SelectedItem.Text);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Create", "Created a City '" + tbCity.Text + " in State '" + ddlistState.SelectedItem.Text + "' in Country '" + ddlistCountry.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "City has been saved successfully";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry!! this city is already exist";
                    pnlMSG.Visible = true;
                }
            }

            else if (Request.QueryString["CityID"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update CityList set City=@City,State=@State,Country=@Country where CityID='" + Request.QueryString["CityID"] + "' ", con);

                cmd.Parameters.AddWithValue("@City", tbCity.Text.ToUpper());
                cmd.Parameters.AddWithValue("@State", ddlistState.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Country", ddlistCountry.SelectedItem.Text);

               
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Update", "Update a City '" + tbCity.Text + "in State '" + ddlistState.SelectedItem.Text + "' in Country '" + ddlistCountry.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "City has been updated successfully";
                pnlMSG.Visible = true;



            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
