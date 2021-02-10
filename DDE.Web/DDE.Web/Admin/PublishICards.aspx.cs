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
using System.IO;

namespace DDE.Web.Admin
{
    public partial class PublishICards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateBatch(ddlistBatch);
                    PopulateDDList.populateStudyCentre(ddlistSCCode);
                   
   

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

       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateStudents();
            //populateStudentPhotos();
            
        }

        private void populateStudentPhotos()
        {
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {
                Label srid = (Label)dli.FindControl("lblSRID");
                Image img = (Image)dli.FindControl("imgPhoto");

                img.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.Text;
               
            }
        }

        private void populateStudents()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

            if (ddlistSCCode.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,FatherName from DDEStudentRecord where Session='"+ddlistBatch.SelectedItem.Text+"' and StudentPhoto is not null and RecordStatus='True'";
            }
            else
            {
                cmd.CommandText = "select SRID,EnrollmentNo,StudentName,FatherName from DDEStudentRecord where Session='" + ddlistBatch.SelectedItem.Text + "' and StudyCentreCode='" + ddlistSCCode.SelectedItem.Text + "' and StudentPhoto is not null and RecordStatus='True'";
            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            //DataColumn dtcol2 = new DataColumn("StudentPhoto");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("EC");
            DataColumn dtcol6 = new DataColumn("StudentName");
            DataColumn dtcol7 = new DataColumn("FatherName");


            dt.Columns.Add(dtcol1);
            //dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);



            while (dr.Read())
            {
                DataRow drow = dt.NewRow();

                //drow["StudentPhoto"] = "StudentPhotos/" + dr["EnrollmentNo"].ToString() + ".jpg";
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                if (dr["EnrollmentNo"].ToString().Length == 10)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(5, 5);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 11)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 5);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 12)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(6, 6);
                }
                else if (dr["EnrollmentNo"].ToString().Length == 14)
                {
                    drow["EC"] = dr["EnrollmentNo"].ToString().Substring(9, 5);
                }
                else
                {
                    drow["EC"] = "";
                }


                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);

                dt.Rows.Add(drow);

            }




            dt.DefaultView.Sort = "EC ASC";
            DataView dv = dt.DefaultView;


            int j = 1;
            foreach (DataRowView dvr in dv)
            {
                dvr[0] = j;
                j++;
            }


            dtlistShowRegistration.DataSource = dt;
            dtlistShowRegistration.DataBind();

            con.Close();

            if (j > 1)
            {

                pnlStudentList1.Visible = true;
                pnlMSG.Visible = false;

            }

            else
            {
                pnlStudentList1.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        protected void btnPublish1_Click(object sender, EventArgs e)
        {
            Session["Students"] = calculateAllSelected();
            Response.Redirect("ICard.aspx");
        }


        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Session["Students"] = calculateAllSelected();
            Response.Redirect("ICard.aspx");
        }

        private string calculateAllSelected()
        {
            string srids = "";
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {


                Label srid = (Label)dli.FindControl("lblSRID");
                CheckBox selected = (CheckBox)dli.FindControl("cbSelect");

                if (selected.Checked==true)
                {
                    if (srids == "")
                    {
                        srids = srid.Text;
                    }
                    else
                    {
                        srids =srids+ ","+ srid.Text;
                    }
                }  

            }

            return srids;
        }

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {             
                CheckBox selected = (CheckBox)dli.FindControl("cbSelect");
                Image img = (Image)dli.FindControl("imgPhoto");        
                selected.Checked = true;                           
            }
        }

        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowRegistration.Items)
            {

                CheckBox selected = (CheckBox)dli.FindControl("cbSelect");

                selected.Checked = false;

            }
        }

        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        
    }
}
