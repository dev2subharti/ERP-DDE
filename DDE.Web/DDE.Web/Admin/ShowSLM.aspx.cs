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
    public partial class ShowSLM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 91) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 92) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {

                if (!IsPostBack)
                {
                    PopulateDDList.populateSySession(ddlistSS);

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

        private void setAccessbility()
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 92))
            {
                foreach (DataListItem dli in dtlistShowSLM.Items)
                {
                   
                    LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");
                    edit.Visible = true;

                }
            }

        }

       

        protected void Edit_SLM(object sender, EventArgs e)
        {
            LinkButton edit = sender as LinkButton;
            Response.Redirect("CreateSLM.aspx?SLMID=" + edit.CommandArgument);
        }

       
        private void populateAttachedCourses()
        {
            string role = "";

            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 91))
            {
                role = "Create";
            }
            else if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 92))
            {
                role = "Edit";
            }

            foreach (DataListItem dli in dtlistShowSLM.Items)
            {
                DataList courses = (DataList)dli.FindControl("dtlistShowCourses");
                Label slmid = (Label)dli.FindControl("lblSLMID");
                LinkButton edit = (LinkButton)dli.FindControl("lnkbtnEdit");

                if (role == "Edit")
                {
                    edit.Visible = true;
                }
                else
                {
                    edit.Visible = false;
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("Select * from DDESLMLinking where SLMID='" + slmid.Text + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("CSNo");
                DataColumn dtcol2 = new DataColumn("SLMLRID");
                DataColumn dtcol3 = new DataColumn("Course");
                DataColumn dtcol4 = new DataColumn("Year");


                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["CSNo"] = (i+1).ToString();
                    drow["SLMLRID"] = ds.Tables[0].Rows[i]["SLMLRID"].ToString();
                    drow["Course"] =FindInfo.findCourseNameByID(Convert.ToInt32(ds.Tables[0].Rows[i]["CID"].ToString())) ;
                    drow["Year"] =  ds.Tables[0].Rows[i]["Year"].ToString();
                  
                   dt.Rows.Add(drow);
                   
                }

                courses.DataSource = dt;
                courses.DataBind();



            }
        }

      

        

        private void populateSLMCodes()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDESLMMaster where SyllabusSession='"+ddlistSS.SelectedItem.Text+"' order by SLMCode", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SLMID");
            DataColumn dtcol3 = new DataColumn("SLMCode");
            DataColumn dtcol4 = new DataColumn("Title");
            DataColumn dtcol5 = new DataColumn("Cost");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = (i+1).ToString();
                drow["SLMID"] = ds.Tables[0].Rows[i]["SLMID"].ToString();
                drow["SLMCode"] = ds.Tables[0].Rows[i]["SLMCode"].ToString();
                drow["Title"] =ds.Tables[0].Rows[i]["Title"].ToString();
                drow["Cost"] = ds.Tables[0].Rows[i]["Cost"].ToString();

                dt.Rows.Add(drow);
            }

            dtlistShowSLM.DataSource = dt;
            dtlistShowSLM.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dtlistShowSLM.Visible = true;
                pnlData.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowSLM.Visible = false;

                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
            }


        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateSLMCodes();
            populateAttachedCourses();
            setAccessbility();
        }
    }
}