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
    public partial class SetTMSCWiseDec12 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 86))
            {
                if (!IsPostBack)
                {
                    populateList();
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                }
                pnlData.Visible = true;
                pnlSet.Visible = true;
                pnlMSG.Visible = false;

            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            if (!sccodeAlreadyExist(ddlistSCCode.SelectedItem.Text))
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDEDec12MRecord values(@SCCode,@TM)", con);

                    cmd.Parameters.AddWithValue("@SCCode", ddlistSCCode.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@TM", Convert.ToInt32(tbTM.Text));



                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Set Dec 12 TMS", "Set '" + tbTM.Text + "' Total Mark Sheet for SCCode '" + ddlistSCCode.SelectedItem.Text + "' for December 2012 Examination", Convert.ToInt32(Session["ERID"].ToString()));

                    ddlistSCCode.SelectedItem.Selected = false;
                    ddlistSCCode.Items.FindByText("001").Selected = true;
                    tbTM.Text = "";
                    populateList();

                    
                }
                catch (Exception ex)
                {
                    pnlData.Visible = false;
                    lblMSG.Text = ex.Message;
                    pnlMSG.Visible = true;
                    btnOK.Visible = true;
                }
                    
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! This SC Code is already exist";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void populateList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEDec12MRecord order by SCCode", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SCCode");
            DataColumn dtcol3 = new DataColumn("TM");

          

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SCCode"] = Convert.ToString(dr["SCCode"]);
                drow["TM"] = Convert.ToString(dr["TM"]);

            
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowSC.DataSource = dt;
            dtlistShowSC.DataBind();

            con.Close();
        }

        private bool sccodeAlreadyExist(string sccode)
        {
            bool exist = false;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEDec12MRecord where SCCode='" + sccode + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                exist = true;
            }

            con.Close();

            return exist;
        }

        protected void dtlistShowSC_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Show")
            {
                Response.Redirect("ShowDec12StudentsBySC.aspx?SCCode=" + Convert.ToString(e.CommandArgument));
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            pnlData.Visible = true;
          
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }
    }
}