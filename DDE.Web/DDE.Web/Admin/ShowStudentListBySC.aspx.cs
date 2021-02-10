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
    public partial class ShowStudentListBySC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {
                if (!IsPostBack)
                {
                    populateRecord();

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
            SqlCommand cmd = new SqlCommand("Select SRID,EnrollmentNo,StudentName,FatherName,CYear from DDEStudentRecord where SRID in ("+Session["SRIDS"].ToString()+") order by EnrollmentNo", con);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("StudentName");
            DataColumn dtcol4 = new DataColumn("FatherName");
            DataColumn dtcol5 = new DataColumn("SCCode");
            DataColumn dtcol6 = new DataColumn("Course");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i + 1;
                    drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                    drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                    drow["FatherName"] = ds.Tables[0].Rows[i]["FatherName"].ToString();
                    drow["SCCode"] = FindInfo.findBothTCSCCodeBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                    drow["Course"]=FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]),Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                    dt.Rows.Add(drow);

                }

                dtlistRecord.DataSource = dt;
                dtlistRecord.DataBind();
                dtlistRecord.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistRecord.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }

        }

        

    }
}
