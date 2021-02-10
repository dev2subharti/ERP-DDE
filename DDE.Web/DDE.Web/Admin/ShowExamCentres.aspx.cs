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
    public partial class ShowExamCentres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 41))
            {

                if (!IsPostBack)
                {

                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("W10").Selected = true;

                }

                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 42))
            {

                if (!IsPostBack)
                {

                    populateExamCentres();
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

        private void populateExamCentres()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEExaminationCentres_"+ddlistExamination.SelectedItem.Value+" order by ECID", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ECID");
            DataColumn dtcol3 = new DataColumn("City");
            DataColumn dtcol4 = new DataColumn("ExamCentreCode");
            DataColumn dtcol5 = new DataColumn("ContactPerson");
            DataColumn dtcol6 = new DataColumn("ContactNo");
            DataColumn dtcol7 = new DataColumn("CentreName");
            DataColumn dtcol8 = new DataColumn("Location");
            DataColumn dtcol9 = new DataColumn("Email");
            DataColumn dtcol10 = new DataColumn("SCCodes");
            DataColumn dtcol11 = new DataColumn("Remark");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);
            dt.Columns.Add(dtcol10);
            dt.Columns.Add(dtcol11);
            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["ECID"] = Convert.ToString(dr["ECID"]);
                drow["City"] = Convert.ToString(dr["City"]);
                drow["ExamCentreCode"] = Convert.ToString(dr["ExamCentreCode"]);
                drow["ContactPerson"] = Convert.ToString(dr["ContactPerson"]);
                drow["ContactNo"] = Convert.ToString(dr["ContactNo"]);
                drow["CentreName"] = Convert.ToString(dr["CentreName"]);
                drow["Location"] = Convert.ToString(dr["Location"]);
                drow["Email"] = Convert.ToString(dr["Email"]);
                drow["SCCodes"] = Convert.ToString(dr["SCCodes"]);
                drow["Remark"] = Convert.ToString(dr["Remark"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowExamCentres.DataSource = dt;
            dtlistShowExamCentres.DataBind();

            con.Close();
        }

        private void setAccessibility()
        {
            foreach (DataListItem dli in dtlistShowExamCentres.Items)
            {


                LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");
                LinkButton delete = (LinkButton)dli.FindControl("lnkbtnDelete");
                edit.Visible = false;
                delete.Visible = false;

            }
        }

        protected void dtlistShowExamCentres_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Session["ExamCode"] = ddlistExamination.SelectedItem.Value;
                Response.Redirect("AddExamCentres.aspx?ECID=" + Convert.ToString(e.CommandArgument));
            }

            else if (e.CommandName == "Delete")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from DDEExaminationCentres_" + ddlistExamination.SelectedItem.Value + " where ECID ='" + Convert.ToString(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteReader();
                con.Close();
                Log.createLogNow("Delete", "Delete a Examination Centre with E.C. Code '" + FindInfo.findECCodeByID(Convert.ToInt32(e.CommandArgument)) + "' for " + ddlistExamination.SelectedItem.Text + " exam", Convert.ToInt32(Session["ERID"].ToString()));

                populateExamCentres();


            }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateExamCentres();
        }
    }
}
