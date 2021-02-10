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
    public partial class ShowCTStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("W10").Selected = true;
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            populateList();
        }

        private void populateList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select DDEStudentRecord.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.CYear,DDEStudentRecord.StudentName,DDEStudentRecord.AdmissionType,DDEStudentRecord.SCStatus,DDEStudentRecord.StudyCentreCode,DDEStudentRecord.PreviousSCCode,DDEStudentRecord.PreviousInstitute,DDECTPaperRecord.Paper1,DDECTPaperRecord.Paper2 from DDECTPaperRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDECTPaperRecord.SRID where DDECTPaperRecord.Exam='" + ddlistExamination.SelectedItem.Value + "' order by DDEStudentRecord.StudyCentreCode", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol8 = new DataColumn("SCCode");
            DataColumn dtcol9 = new DataColumn("Course");
            DataColumn dtcol10 = new DataColumn("Year");
            DataColumn dtcol5 = new DataColumn("PreIns");
            DataColumn dtcol6 = new DataColumn("Paper1");
            DataColumn dtcol7 = new DataColumn("Paper2");


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
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["Year"] = dr["CYear"].ToString();
                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), Convert.ToInt32(dr["CYear"]));
                if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
                {
                     drow["SCCode"]=dr["StudyCentreCode"].ToString();
                }
                else if (dr["SCStatus"].ToString() == "T")
                {
                    drow["SCCode"] = dr["PreviousSCCode"].ToString();
                }
                if (Convert.ToString(dr["PreviousInstitute"]) == "1")
                {
                    drow["PreIns"] = "DDE (SVSU)";
                }
                else if (Convert.ToString(dr["PreviousInstitute"]) == "2")
                {
                    drow["PreIns"] = "OTHER";
                }
                drow["Paper1"] = Convert.ToString(dr["Paper1"]);
                drow["Paper2"] = Convert.ToString(dr["Paper2"]);
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowList.DataSource = dt;
            dtlistShowList.DataBind();

            con.Close();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}