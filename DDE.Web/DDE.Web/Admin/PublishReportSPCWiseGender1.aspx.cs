using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class PublishReportSPCWiseGender1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 35))
            {
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
            SqlCommand cmd = new SqlCommand("Select distinct SRID from [DDEFeeRecord_2013-14] where FeeHead='1' and ForExam='A14' and ForYear='" + ddlistYear.SelectedItem.Value + "'", con);
           
          
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("Programme");
            DataColumn dtcol3 = new DataColumn("Male");
            DataColumn dtcol4 = new DataColumn("Female");
            DataColumn dtcol5 = new DataColumn("Not Found");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.SelectCommand.Connection = con;
            da.Fill(ds);

            int male1=0, female1 = 0, nf1=0;
            int male2 = 0, female2 = 0, nf2 = 0;
            int male3 = 0, female3 = 0, nf3 = 0;
            int male5 = 0, female5 = 0, nf5 = 0;
            int male6 = 0, female6 = 0, nf6 = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
               int  pc=FindInfo.findProgrammeCode(FindInfo.findCourseIDBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"])).ToString());
              
               if (pc == 1)
               {
                   string gender = FindInfo.findGenderBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                   if (gender == "MALE")
                   {
                       male1 = male1 + 1;
                   }
                   else if (gender == "FEMALE")
                   {
                       female1 = female1 + 1;
                   }
                   else
                   {
                       nf1 = nf1 + 1;
                   }

               }
               else if (pc == 2)
               {
                   string gender = FindInfo.findGenderBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                   if (gender == "MALE")
                   {
                       male2 = male2 + 1;
                   }
                   else if (gender == "FEMALE")
                   {
                       female2 = female2 + 1;
                   }
                   else
                   {
                       nf2 = nf2 + 1;
                   }

               }
               else if (pc == 3)
               {
                   string gender = FindInfo.findGenderBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                   if (gender == "MALE")
                   {
                       male3 = male3 + 1;
                   }
                   else if (gender == "FEMALE")
                   {
                       female3 = female3 + 1;
                   }
                   else
                   {
                       nf3 = nf3 + 1;
                   }

               }
               else if (pc == 5)
               {
                   string gender = FindInfo.findGenderBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                   if (gender == "MALE")
                   {
                       male5 = male5 + 1;
                   }
                   else if (gender == "FEMALE")
                   {
                       female5 = female5 + 1;
                   }
                   else
                   {
                       nf5 = nf5 + 1;
                   }

               }
               else if (pc == 6)
               {
                   string gender = FindInfo.findGenderBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]));
                   if (gender == "MALE")
                   {
                       male6 = male6 + 1;
                   }
                   else if (gender == "FEMALE")
                   {
                       female6 = female6 + 1;
                   }
                   else
                   {
                       nf6 = nf6 + 1;
                   }

               }
               
            }
            int k = 1;
            for (int j = 1; j <= 6; j++)
            {
               
                if (j != 4)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = k;
                    drow["Programme"] =FindInfo.findProgrammeCodeNameByNo(j);
                    if (j == 1)
                    {
                        drow["Male"] = male1.ToString();
                        drow["Female"] = female1.ToString();
                        drow["Not Found"] = nf1.ToString();
                    }
                    else if (j == 2)
                    {
                        drow["Male"] = male2.ToString();
                        drow["Female"] = female2.ToString();
                        drow["Not Found"] = nf2.ToString();
                    }
                    else if (j == 3)
                    {
                        drow["Male"] = male3.ToString();
                        drow["Female"] = female3.ToString();
                        drow["Not Found"] = nf3.ToString();
                    }
                    else if (j == 5)
                    {
                        drow["Male"] = male5.ToString();
                        drow["Female"] = female5.ToString();
                        drow["Not Found"] = nf5.ToString();
                    }
                    else if (j == 6)
                    {
                        drow["Male"] = male6.ToString();
                        drow["Female"] = female6.ToString();
                        drow["Not Found"] = nf6.ToString();
                    }
                    dt.Rows.Add(drow);
                    k = k + 1;
                   
                }
            }



            gvReport.DataSource = dt;
            gvReport.DataBind();


        }
    }
}
