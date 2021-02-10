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
    public partial class ShowMyRecProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 69))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExamination);
                    ddlistExamination.Items.FindByValue("W10").Selected = true;
                    setCurrentDate();
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;


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

        private void populateTotalReceivedCopy()
        {
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 23:59:59";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();


            cmd.CommandText = "Select * from DDEProjectRecRecord where ReceivedBy='" + Convert.ToInt32(Session["ERID"]) + "' and TOR>='" + Convert.ToDateTime(from) + "' and TOR<='" + Convert.ToDateTime(to) + "' order by TOR";
           
           

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;

            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("EnrollmentNo");       
            DataColumn dtcol3 = new DataColumn("PracticalCode");         
            DataColumn dtcol4 = new DataColumn("Course");
            DataColumn dtcol5 = new DataColumn("ProjectName");
            DataColumn dtcol6 = new DataColumn("MO");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["EnrollmentNo"] = FindInfo.findENoByID(Convert.ToInt32(dr["SRID"]));               
                string[] subinfo = FindInfo.findPracticalInfoByID2(Convert.ToInt32(dr["PracticalID"]));
                drow["PracticalCode"] = subinfo[0];            
                int subyear = Convert.ToInt32(subinfo[2]);
                drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(dr["SRID"]), subyear);
                drow["ProjectName"] = subinfo[1];
                drow["MO"] = "";
                dt.Rows.Add(drow);
                i = i + 1;
            }



            gvAwarsSheet.DataSource = dt;
            gvAwarsSheet.DataBind();

            con.Close();

            if (i <= 1)
            {
                gvAwarsSheet.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;

            }
            else
            {
                gvAwarsSheet.Visible = true;
                pnlMSG.Visible = false;
            }

        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            populateTotalReceivedCopy();
        }
      
    }
}