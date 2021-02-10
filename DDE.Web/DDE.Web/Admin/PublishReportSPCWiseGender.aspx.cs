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
    public partial class PublishReportSPCWiseGender : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 35))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistBatch);
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='1' and ForExam='A14' and ForYear='"+ddlistBatch+"'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("Programme");
            DataColumn dtcol3 = new DataColumn("Male");
            DataColumn dtcol4 = new DataColumn("Female");
         


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
         


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["Programme"] = FindInfo.findProgrammeCodeNameByNo(Convert.ToInt32(dr[0]));
                drow["Male"] =FindInfo.findTotalStudentsPCandGenderWise(Convert.ToInt32(dr[0]),ddlistBatch.SelectedItem.Text,"MALE");
                drow["Female"] = FindInfo.findTotalStudentsPCandGenderWise(Convert.ToInt32(dr[0]), ddlistBatch.SelectedItem.Text,"FEMALE");
              

                dt.Rows.Add(drow);
                i = i + 1;
            }       

            con.Close();

            gvReport.DataSource = dt;
            gvReport.DataBind();

           
        }
    }
}
