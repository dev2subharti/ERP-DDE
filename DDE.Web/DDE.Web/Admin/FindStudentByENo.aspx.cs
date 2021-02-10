using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DDE.DAL;
using System.Data.SqlClient;

namespace DDE.Web.Admin
{
    public partial class FindStudentByENo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 11))
            {
                    pnlSearch.Visible = true;
                    pnlData.Visible = true;
                    pnlMSG.Visible = false;
                    btnOK.Visible = false;


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
            if (btnSearch.Text == "Search")
            {
                int srid = 0;

                if (rblSearchType.SelectedItem.Value == "4")
                {
                    populateStudents();

                }
                else
                {
                    if (rblSearchType.SelectedItem.Value == "1")
                    {
                         srid = FindInfo.findSRIDByOANo(Convert.ToInt32(tbID.Text));                  
                    }
                    else if (rblSearchType.SelectedItem.Value == "2")
                    {
                         srid = FindInfo.findSRIDByANo(tbID.Text);
                   
                    }
                    else if (rblSearchType.SelectedItem.Value == "3")
                    {
                        srid = FindInfo.findSRIDByENo(tbID.Text);

                    }
                    if (srid != 0)
                    {
                        Session["RecordType"] = "Show";
                        Response.Redirect("DStudentRegistration.aspx?SRID=" + srid);
                    }

                    else if (srid == 0)
                    {
                        pnlData.Visible = false;
                        lblMSG.Text = "Sorry !! No Record Found.";
                        pnlMSG.Visible = true;
                        btnOK.Visible = true;
                    }
                }
              
                
                
            }
            else if (btnSearch.Text == "Search Another")
            {
                tbID.Enabled = true;
                rblSearchType.Enabled = true;
                dtlistShowStudents.Visible = false;
                btnSearch.Text = "Search";
            }
          
        }

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEStudentRecord where StudentName like '"+tbID.Text+"%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
           
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");    
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");      
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("Course");
            DataColumn dtcol7 = new DataColumn("SCCode");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
     
            da.SelectCommand.Connection = con;
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();

                    drow["SNo"] = i + 1;
                    drow["SRID"] = ds.Tables[0].Rows[i]["SRID"];
                    drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"];
                    drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"];
                    drow["FatherName"] = ds.Tables[0].Rows[i]["FatherName"];
                    drow["Course"] = FindInfo.findCourseNameBySRID(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToInt32(ds.Tables[0].Rows[i]["CYear"]));
                    drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"];

                    dt.Rows.Add(drow);

                }
            }

            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();


            if (ds.Tables[0].Rows.Count > 0)
            {

                dtlistShowStudents.Visible = true;
                pnlMSG.Visible = false;
                rblSearchType.Enabled = false;
                tbID.Enabled = false;
                btnSearch.Text = "Search Another";

            }

            else
            {
                dtlistShowStudents.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
                rblSearchType.Enabled = true;
                tbID.Enabled = true;
               
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
           
            pnlData.Visible = true;
            pnlMSG.Visible = false;
            btnOK.Visible = false;
        }

     

        protected void dtlistShowStudents_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Session["RecordType"] = "Show";
            Response.Redirect("DStudentRegistration.aspx?SRID=" +e.CommandArgument);
        }

        protected void rblSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
        }
    }
}
