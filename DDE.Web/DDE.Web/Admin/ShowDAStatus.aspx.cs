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
    public partial class ShowDAStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 118) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 119))
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;

                if (!IsPostBack)
                {
                    setCurrentDate();
                }
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }
        private void setCurrentDate()
        {
            ddlistDayFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDayTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateList();

        }

        private void populateList()
        {
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 11:59:59 PM";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataReader dr;

            if (ddlistType.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "1")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.PRStatus='True' and DDEDegreeInfo.NDStatus='True' and DDEDegreeInfo.DPStatus='False' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "2")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.PRStatus='True' and DDEDegreeInfo.NDStatus='True' and DDEDegreeInfo.DPStatus='True' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "3")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.PRStatus='True' and DDEDegreeInfo.NDStatus='True' and DDEDegreeInfo.DPStatus='True' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "4")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.PRStatus='True' and DDEDegreeInfo.NDStatus='True' and DDEDegreeInfo.DPStatus='True' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "5")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.PRStatus='True' and DDEDegreeInfo.NDStatus='True' and DDEDegreeInfo.DPStatus='True' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "6")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.PRStatus='True' and DDEDegreeInfo.NDStatus='True' and DDEDegreeInfo.DPStatus='True' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "6")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEDegreeInfo.EntryTime,DDEDegreeInfo.PRDoneOn,DDEDegreeInfo.NDDoneOn,DDEDegreeInfo.DPDoneOn from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.PRStatus='True' and DDEDegreeInfo.NDStatus='True' and DDEDegreeInfo.DPStatus='True' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }


            cmd.Connection = con;

            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("DIID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("SName");
            DataColumn dtcol6 = new DataColumn("FName");
            DataColumn dtcol7 = new DataColumn("EntryTime");
            DataColumn dtcol8 = new DataColumn("PRDoneOn");
            DataColumn dtcol9 = new DataColumn("NDDoneOn");
            DataColumn dtcol10 = new DataColumn("DPDoneOn");

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


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["DIID"] = Convert.ToInt32(dr["DIID"]);
                drow["SRID"] = Convert.ToInt32(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["SName"] = Convert.ToString(dr["StudentName"]);
                drow["FName"] = Convert.ToString(dr["FatherName"]);
                drow["EntryTime"] = Convert.ToDateTime(dr["EntryTime"]).ToString("dd-MM-yyyy");
                if (Convert.ToString(dr["PRDoneOn"]) == "1/1/1900 12:00:00 AM")
                {
                    drow["PRDoneOn"] = "-";
                }
                else
                {
                    drow["PRDoneOn"] = Convert.ToDateTime(dr["PRDoneOn"]).ToString("dd-MM-yyyy");
                }
                if (Convert.ToString(dr["NDDoneOn"]) == "1/1/1900 12:00:00 AM")
                {
                    drow["NDDoneOn"] = "-";
                }
                else
                {
                    drow["NDDoneOn"] = Convert.ToDateTime(dr["NDDoneOn"]).ToString("dd-MM-yyyy");
                }
                if (Convert.ToString(dr["DPDoneOn"]) == "1/1/1900 12:00:00 AM")
                {
                    drow["DPDoneOn"] = "-";
                }
                else
                {
                    drow["DPDoneOn"] = Convert.ToDateTime(dr["DPDoneOn"]).ToString("dd-MM-yyyy");
                }
                dt.Rows.Add(drow);
                i = i + 1;
            }

            con.Close();

            if (i > 1)
            {
                dtlistShowDegreeInfo.DataSource = dt;
                dtlistShowDegreeInfo.DataBind();
                dtlistShowDegreeInfo.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowDegreeInfo.Visible = false;
                lblMSG.Text = "Sorry!! No Record Found.";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowDegreeInfo_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label srid = (Label)e.Item.FindControl("lblSRID");

            if (e.CommandName == "Show")
            {
                Response.Redirect("Degree.aspx?DIID=" + Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void ddlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDegreeInfo.Visible = false;

        }
    }
}