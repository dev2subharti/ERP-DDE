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
    public partial class ShowECStudents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 64))
            {

                if (!IsPostBack)
                {

                    populateStudents();
                    lblHeading.Text = Session["Heading"].ToString();


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
            DataTable st = (DataTable)Session["STR"];

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcolpc = new DataColumn("PaperCode");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");
            DataColumn dtcol3 = new DataColumn("RollNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("SCCode");
            DataColumn dtcol7 = new DataColumn("Course");
            DataColumn dtcol8 = new DataColumn("Year");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcolpc);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);

            int counter = 0;

            for(int j=0;j<st.Rows.Count;j++)
            {
                if (st.Rows[j]["SRIDS"].ToString() != "")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where SRID in (" + st.Rows[j]["SRIDS"].ToString() + ") order by EnrollmentNo", con);                 

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow drow = dt.NewRow();
                        drow["SNo"] = counter + 1;
                        drow["PaperCode"] = st.Rows[j]["PaperCode"].ToString();
                        drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                        drow["RollNo"] = FindInfo.findRollNoBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Session["ExamCode"].ToString(), Session["MOE"].ToString());
                        drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                        drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                        drow["SCCode"] = FindInfo.findSCCodeForAdmitCardBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                        drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                        drow["Year"] = Convert.ToString(ds.Tables[0].Rows[i]["CYear"]);

                        dt.Rows.Add(drow);

                        counter = counter + 1;

                    }
                }

            }

            gvShowMaildStudents.DataSource = dt;
            gvShowMaildStudents.DataBind();

            if (counter > 0)
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
       
           
        }
       
    }
}