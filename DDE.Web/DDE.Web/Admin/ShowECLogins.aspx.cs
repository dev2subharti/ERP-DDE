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
    public partial class ShowECLogins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
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

        private void populateExamCentres()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_" + ddlistExamination.SelectedItem.Value + " order by ECID", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ECID");
            DataColumn dtcol3 = new DataColumn("City");
            DataColumn dtcol4 = new DataColumn("ExamCentreCode");
            DataColumn dtcol5 = new DataColumn("Password");       
            DataColumn dtcol6 = new DataColumn("CentreName");
            DataColumn dtcol7 = new DataColumn("Email");
          
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
                drow["ECID"] = Convert.ToString(dr["ECID"]);
                drow["City"] = Convert.ToString(dr["City"]);
                drow["ExamCentreCode"] = Convert.ToString(dr["ExamCentreCode"]);
                drow["Password"] = Convert.ToString(dr["Password"]);
             
                drow["CentreName"] = Convert.ToString(dr["CentreName"]);
                drow["Email"] = Convert.ToString(dr["Email"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowExamCentres.DataSource = dt;
            dtlistShowExamCentres.DataBind();

            con.Close();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateExamCentres();
        }
    }
}