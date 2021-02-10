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
    public partial class ShowMigrationRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]),120) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 121))
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateList();
            setColor();
        }

        private void setColor()
        {
            bool valid = false;
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 121))
            {
                valid = true;
            }

            foreach (DataListItem dli in dtlistShowMigrationInfo.Items)
            {
                Button edit = (Button)dli.FindControl("btnEdit");
                Label pub = (Label)dli.FindControl("lblPublished");
                Label rec = (Label)dli.FindControl("lblReceived");
                Label post = (Label)dli.FindControl("lblPosted");

                Image pubY = (Image)dli.FindControl("imgPubY");
                Image pubN = (Image)dli.FindControl("imgPubN");
                Image recY = (Image)dli.FindControl("imgRecY");
                Image recN = (Image)dli.FindControl("imgRecN");
                Image postY = (Image)dli.FindControl("imgPostY");
                Image postN = (Image)dli.FindControl("imgPostN");


                if (pub.Text == "Yes")
                {
                    pubY.Visible = true;
                }
                else if (pub.Text == "No")
                {
                    pubN.Visible = true;
                }
                if (rec.Text == "Yes")
                {
                    recY.Visible = true;
                }
                else if (rec.Text == "No")
                {
                    recN.Visible = true;
                }
                if (post.Text == "Yes")
                {
                    postY.Visible = true;
                }
                else if (post.Text == "No")
                {
                    postN.Visible = true;
                }

                if(valid==true)
                {
                    edit.Visible = true;
                }
                else
                {
                    edit.Visible = false;
                }
            }
        }

        private void populateList()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataReader dr;

            if (ddlistType.SelectedItem.Value == "1")
            {
                cmd.CommandText = "Select  DDEMigrationInfo.MID,DDEMigrationInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEMigrationInfo.LetterPublished,DDEMigrationInfo.MigrationReceived,DDEMigrationInfo.MigrationPosted from DDEMigrationInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEMigrationInfo.SRID where DDEMigrationInfo.LetterPublished='False' order by DDEMigrationInfo.MID";
            }
            else if (ddlistType.SelectedItem.Value == "2")
            {
                cmd.CommandText = "Select  DDEMigrationInfo.MID,DDEMigrationInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEMigrationInfo.LetterPublished,DDEMigrationInfo.MigrationReceived,DDEMigrationInfo.MigrationPosted from DDEMigrationInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEMigrationInfo.SRID where DDEMigrationInfo.LetterPublished='True' order by DDEMigrationInfo.MID";
            }
            else if (ddlistType.SelectedItem.Value == "3")
            {
                cmd.CommandText = "Select DDEMigrationInfo.MID,DDEMigrationInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEMigrationInfo.LetterPublished,DDEMigrationInfo.MigrationReceived,DDEMigrationInfo.MigrationPosted from DDEMigrationInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEMigrationInfo.SRID where DDEMigrationInfo.MigrationReceived='True' order by DDEMigrationInfo.MID";
            }
            else if (ddlistType.SelectedItem.Value == "4")
            {
                cmd.CommandText = "Select DDEMigrationInfo.MID,DDEMigrationInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEMigrationInfo.LetterPublished,DDEMigrationInfo.MigrationReceived,DDEMigrationInfo.MigrationPosted from DDEMigrationInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEMigrationInfo.SRID where DDEMigrationInfo.MigrationPosted='True' order by DDEMigrationInfo.MID";
            }
            else if (ddlistType.SelectedItem.Value == "0")
            {
                cmd.CommandText = "Select DDEMigrationInfo.MID,DDEMigrationInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEMigrationInfo.LetterPublished,DDEMigrationInfo.MigrationReceived,DDEMigrationInfo.MigrationPosted from DDEMigrationInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEMigrationInfo.SRID order by DDEMigrationInfo.MID";
            }

            cmd.Connection = con;

            dr = cmd.ExecuteReader();



            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("SName");
            DataColumn dtcol5 = new DataColumn("Published");
            DataColumn dtcol6 = new DataColumn("Received");
            DataColumn dtcol7 = new DataColumn("Posted");
            DataColumn dtcol8 = new DataColumn("MID");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["MID"] = Convert.ToInt32(dr["MID"]);
                drow["SRID"] = Convert.ToInt32(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["SName"] = Convert.ToString(dr["StudentName"]);
                if (Convert.ToString(dr["LetterPublished"]) == "True")
                {
                    drow["Published"] = "Yes";
                }
                else
                {
                    drow["Published"] = "No";
                }
                if (Convert.ToString(dr["MigrationReceived"]) == "True")
                {
                    drow["Received"] = "Yes";
                }
                else
                {
                    drow["Received"] = "No";
                }
                if (Convert.ToString(dr["MigrationPosted"]) == "True")
                {
                    drow["Posted"] = "Yes";
                }
                else
                {
                    drow["Posted"] = "No";
                }


                dt.Rows.Add(drow);
                i = i + 1;
            }

            con.Close();

            if (i > 1)
            {
                dtlistShowMigrationInfo.DataSource = dt;
                dtlistShowMigrationInfo.DataBind();
                dtlistShowMigrationInfo.Visible = true;
                btnPublish.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowMigrationInfo.Visible = false;
                btnPublish.Visible = false;
                lblMSG.Text = "Sorry!! No Record Found.";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowMigrationInfo_ItemCommand(object source, DataListCommandEventArgs e)
        {
           
            if (e.CommandName == "Edit")
            {
                Response.Redirect("RequestForMigration.aspx?MID=" + e.CommandArgument);
            }
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            Session["MIDS"] = findAllSelected();
            Response.Redirect("MigrationLetter.aspx");
        }

        private string findAllSelected()
        {
            string selected = "";

            foreach (DataListItem dli in dtlistShowMigrationInfo.Items)
            {
                
                Label mid = (Label)dli.FindControl("lblMID");
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");
               

                if (cb.Checked==true)
                {
                    if (selected == "")
                    {
                        selected = mid.Text;
                    }
                    else
                    {
                        selected = selected + "," + mid.Text;
                    }
                }
                

            }

            return selected;


        }
    }
}