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
    public partial class ShowMaildStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 65))
            {

                if (!IsPostBack)
                {

                    populateStudents();


                }

               
            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

    

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where SRID in ("+Session["SRIDS"].ToString()+") order by EnrollmentNo", con);
           


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("StudentName");
            DataColumn dtcol4 = new DataColumn("FatherName");
            DataColumn dtcol5 = new DataColumn("SCCode");
            DataColumn dtcol6 = new DataColumn("Year");
            DataColumn dtcol7 = new DataColumn("AdmissionStatus");
            DataColumn dtcol8 = new DataColumn("Remark");
         


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

          
         
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i+1;
                drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                drow["SCCode"] = Convert.ToString(ds.Tables[0].Rows[i]["StudyCentreCode"]);
                drow["Year"] = Convert.ToString(ds.Tables[0].Rows[i]["CYear"]);
                if ((ds.Tables[0].Rows[i]["AdmissionStatus"]).ToString() =="1")
                {
                    drow["AdmissionStatus"] = "Confirmed";
                    drow["Remark"] = "";
                }
                else if ((ds.Tables[0].Rows[i]["AdmissionStatus"]).ToString() == "2")
                {
                    drow["AdmissionStatus"] = "Pending";
                    drow["Remark"] = Convert.ToString(ds.Tables[0].Rows[i]["ReasonIfPending"]);
                }
                dt.Rows.Add(drow);
               
            }

            gvShowMaildStudents.DataSource = dt;
            gvShowMaildStudents.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvShowMaildStudents.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
            }

            con.Close();
        }
    }
}