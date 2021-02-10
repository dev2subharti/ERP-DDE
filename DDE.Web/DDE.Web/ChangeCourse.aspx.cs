using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDE.Web
{
    public partial class ChangeCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateDDList.populateCourses(ddlistCurrentCourse);
            PopulateDDList.populateCourses(ddlistNewCourse);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select CYear,Course,Course2Year,Course3Year from DDEStudentRecord where EnrollmentNo='" + tbENo.Text + "' and RecordStatus='True' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                if(Convert.ToInt32(dr["CYear"])==2 && Convert.ToInt32(dr["Course"])==76)
                {
                    ddlistCurrentCourse.Items.FindByValue(dr["Course2Year"].ToString()).Selected = true;
                }
                else
                {
                    ddlistCurrentCourse.Items.FindByValue(dr["Course"].ToString()).Selected = true;
                }
               
              

             
            }


            con.Close();
        }
    }
}