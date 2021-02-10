using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web.Admin
{
    public partial class ShowStudentsCourseWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistSession);

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






        private void populateRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDECourse order by CourseShortName", con);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("CourseID");
            DataColumn dtcol3 = new DataColumn("Course");
            DataColumn dtcol4 = new DataColumn("TS");
            DataColumn dtcol5 = new DataColumn("SRIDS");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            int j = 1;
            int ts = 0;

           
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = j;
                    drow["CourseID"] = ds.Tables[0].Rows[i]["CourseID"].ToString();
                    if (ds.Tables[0].Rows[i]["Specialization"].ToString() == "")
                    {
                        drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString();
                    }

                    else
                    {
                        drow["Course"] = ds.Tables[0].Rows[i]["CourseShortName"].ToString() + "(" + ds.Tables[0].Rows[i]["Specialization"].ToString() + ")";

                    }
                    string srids;
                    drow["TS"] = FindInfo.findTotalStudentsByCourseID(Convert.ToInt32(ds.Tables[0].Rows[i]["CourseID"]), ddlistSession.SelectedItem.Text, Convert.ToInt32(ddlistYear.SelectedItem.Value), out srids);
                    drow["SRIDS"] = srids;
                    if (Convert.ToInt32(drow["TS"]) != 0)
                    {
                        dt.Rows.Add(drow);
                        j = j + 1;
                        ts = ts + Convert.ToInt32(drow["TS"]);
                    }


                }
                dtlistRecord.DataSource = dt;
                dtlistRecord.DataBind();
                lblTotal.Text = "Total Students : " + ts.ToString();

                if (ts > 0)
                {

                    
                    pnlRecord.Visible = true;
                    pnlMSG.Visible = false;
                }
                else
                {
                    pnlRecord.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found";
                    pnlMSG.Visible = true;

                }
            

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateRecord();

        }



        protected void dtlistRecord_ItemCommand(object source, DataListCommandEventArgs e)
        {

            //Session["SRIDS"] = e.CommandArgument;
            //Response.Redirect("ShowStudentListBySC.aspx?SCCode=" + Convert.ToString(e.CommandName));

        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlRecord.Visible = false;
            pnlMSG.Visible = false;
        }

        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlRecord.Visible = false;
            pnlMSG.Visible = false;
        }
    }
}