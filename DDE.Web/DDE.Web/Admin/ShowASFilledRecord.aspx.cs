using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DDE.Web.Admin
{
    public partial class ShowASFilledRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 70))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExaminers(ddlistExaminer, ddlistExam.SelectedItem.Value);
                    ddlistExaminer.Items.Add(new ListItem("ALL", "0"));
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            populateReport();    

        }

        private void populateReport()
        {
            
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr;

                if (ddlistExaminer.SelectedItem.Text == "ALL")
                {
                    cmd.CommandText = "Select * from DDEASPrintRecord_" + ddlistExam.SelectedItem.Value + " where Uploaded='" + ddlistType.SelectedItem.Value + "'";
                }
                else
                {
                    cmd.CommandText = "Select * from DDEASPrintRecord_" + ddlistExam.SelectedItem.Value + " where Uploaded='" + ddlistType.SelectedItem.Value + "' and CheckedBy='"+ddlistExaminer.SelectedItem.Value+"'";
                }

                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("ASNo");
                DataColumn dtcol3 = new DataColumn("PaperCode");
                DataColumn dtcol4 = new DataColumn("SubjectName");
                DataColumn dtcol5 = new DataColumn("TotalStudents");
                DataColumn dtcol6 = new DataColumn("TotalUploaded");
                DataColumn dtcol7 = new DataColumn("Examiner");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);

                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader();

                int i = 1;
                int ts = 0;
                int tu = 0;
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i;
                    drow["ASNo"] = String.Format("{0:0000}", Convert.ToInt32(dr["ASPRID"]));
                    drow["PaperCode"] = dr["PaperCode"].ToString();
                    drow["SubjectName"] = FindInfo.findSubjectNameByPaperCode(dr["PaperCode"].ToString(),"A 2010-11");
                    string[] students = dr["ASRID"].ToString().Split(',');
                    drow["TotalStudents"] = students.Length;
                    drow["TotalUploaded"] = dr["FE"].ToString();
                    drow["Examiner"] = FindInfo.findExaminerByID(Convert.ToInt32(dr["CheckedBy"]));
                    ts = ts + Convert.ToInt32(drow["TotalStudents"]);
                    tu = tu + Convert.ToInt32(dr["FE"]);
                    dt.Rows.Add(drow);
                    i = i + 1;
                }

                con.Close();

                dtlistASReport.DataSource = dt;
                dtlistASReport.DataBind();


                if (i > 1)
                {
                    lblTS.Text ="Total Students : " +ts.ToString();
                    lblTU.Text ="Total Uploaded : "+ tu.ToString();
                    lblTU.Visible = true;
                    lblTS.Visible = true;
                    dtlistASReport.Visible = true;
                    pnlMSG.Visible = false;

                }
                else
                {
                    dtlistASReport.Visible = false;
                    lblMSG.Text = "Sorry !! No record found";
                    pnlMSG.Visible = true;
                    lblTU.Visible = false;
                    lblTS.Visible = false;

                }

        }

        protected void ddlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistASReport.Visible = false;
            lblTS.Visible = false;
            lblTU.Visible = false;
        }

        protected void ddlistExaminer_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistASReport.Visible = false;
            lblTS.Visible = false;
            lblTU.Visible = false;
        }
    }
}