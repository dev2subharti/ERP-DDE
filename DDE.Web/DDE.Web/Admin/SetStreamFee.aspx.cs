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
    public partial class SetStreamFee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 39))
            {

                if (!IsPostBack)
                {

                    populateCourses();
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

        private void setAccessibility()
        {
            foreach (DataListItem dli in dtlistShowCourses.Items)
            {


                TextBox tb = (TextBox)dli.FindControl("tbStreamFee");

                if (tb.Text != "")
                {
                    tb.Enabled = false;
                }



            }
        }

        private void populateCourses()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDECourse order by CourseShortName", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("CourseID");
            DataColumn dtcol3 = new DataColumn("CourseCode");
            DataColumn dtcol4 = new DataColumn("CourseShortName");
           
            DataColumn dtcol6 = new DataColumn("StreamFee");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
          
            dt.Columns.Add(dtcol6);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["CourseID"] = Convert.ToString(dr["CourseID"]);
                drow["CourseCode"] = Convert.ToString(dr["CourseCode"]);
                if (Convert.ToString(dr["Specialization"]) != "")
                {
                    drow["CourseShortName"] = Convert.ToString(dr["CourseShortName"])+"("+Convert.ToString(dr["Specialization"])+")";
                }
                else
                {
                    drow["CourseShortName"] = Convert.ToString(dr["CourseShortName"]);
                }
              
                drow["StreamFee"] = Convert.ToString(dr["StreamFee"]);

                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowCourses.DataSource = dt;
            dtlistShowCourses.DataBind();

            con.Close();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowCourses.Items)
            {


                TextBox sf = (TextBox)dli.FindControl("tbStreamFee");
                Label cid = (Label)dli.FindControl("lblCourseID");
                Label cn = (Label)dli.FindControl("lblCourseName");


                if (sf.Text != "" && sf.Text!="0")
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDECourse set StreamFee=@StreamFee where CourseID='" + cid.Text + "' ", con);

                    cmd.Parameters.AddWithValue("@StreamFee",Convert.ToInt32(sf.Text));
               


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Set Stream Fee", "Set stream fee Rs. '" + sf.Text + " for  '" + cn.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                  
                }



            }

            pnlData.Visible = false;
            lblMSG.Text = "Fee has been updated successfully";
            pnlMSG.Visible = true;
        }

        

    }
}