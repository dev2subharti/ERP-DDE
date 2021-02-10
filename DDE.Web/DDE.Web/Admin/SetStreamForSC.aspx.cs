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
    public partial class SetStreamForSC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 66))
            {
                if (!IsPostBack)
                {
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

        protected void ddlistSCCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowCourses.Visible = false;
            btnUpdate.Visible = false;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            populateCourses();
            populateAllotedStreams();
        }

        private void populateAllotedStreams()
        {
            string astreams = FindInfo.findSCAllotedStreams(Convert.ToInt32(ddlistSCCode.SelectedItem.Value));

            string[] streams = astreams.Split(',');

            if (astreams != "")
            {

                foreach (DataListItem dli in dtlistShowCourses.Items)
                {
                    Label cid = (Label)dli.FindControl("lblCourseID");
                    CheckBox alloted = (CheckBox)dli.FindControl("cbAllot");


                    for (int i = 0; i < streams.Length; i++)
                    {
                        if (Convert.ToInt32(cid.Text) == Convert.ToInt32(streams[i]))
                        {
                            alloted.Checked = true;
                            break;
                        }
                        
                    }


                }
            }
        }

        private void populateCourses()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDECourse where Online='True' order by CourseShortName", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("CourseID");
            DataColumn dtcol3 = new DataColumn("CourseName"); 
            
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
                      
            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["CourseID"] = Convert.ToString(dr["CourseID"]);
                if (dr["Specialization"].ToString() == "")
                {
                    drow["CourseName"] = Convert.ToString(dr["CourseShortName"]);
                }
                else
                {
                    drow["CourseName"] = Convert.ToString(dr["CourseShortName"]) + " (" + Convert.ToString(dr["Specialization"]) + ")";
                }
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistShowCourses.DataSource = dt;
            dtlistShowCourses.DataBind();

            con.Close();

            dtlistShowCourses.Visible = true;
            btnUpdate.Visible = true;
           
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string sstreams=findSelectedStreams();
            string astreams = FindInfo.findSCAllotedStreams(Convert.ToInt32(ddlistSCCode.SelectedItem.Value));

            if (FindInfo.streamRecordEntered(Convert.ToInt32(ddlistSCCode.SelectedItem.Text)))
            {
                if (sstreams != astreams)
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("update DDESCAllotedStreams set AllotedStreams=@AllotedStreams where SCID='" + ddlistSCCode.SelectedItem.Value + "' ", con);

                    cmd.Parameters.AddWithValue("@AllotedStreams", sstreams);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Log.createLogNow("Streams Updated", "Updated Streams of '"+ddlistSCCode.SelectedItem.Text+"' from '" + astreams + "' to '" + sstreams + "'", Convert.ToInt32(Session["ERID"].ToString()));
                    pnlData.Visible = false;
                    lblMSG.Text = "Streams have been updated successfully !!";
                    pnlMSG.Visible = true;
                }
                else
                {
                    pnlData.Visible = false;
                    lblMSG.Text = "Sorry !! you did not select any different or new Stream !!";
                    pnlMSG.Visible = true;
                }
            }
            else
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDESCAllotedStreams values(@SCID,@AllotedStreams)", con);


                cmd.Parameters.AddWithValue("@SCID", ddlistSCCode.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@AllotedStreams", sstreams);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Log.createLogNow("Streams Alloted", "'" + sstreams + "' Streams Alloted to SC Code '" + ddlistSCCode.SelectedItem.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                pnlData.Visible = false;
                lblMSG.Text = "Streams have been alloted successfully !!";
                pnlMSG.Visible = true;
            }
        }

        private string  findSelectedStreams()
        {
            string streams = "";
            foreach (DataListItem dli in dtlistShowCourses.Items)
            {



                Label cid = (Label)dli.FindControl("lblCourseID");
                CheckBox alloted = (CheckBox)dli.FindControl("cbAllot");



                    if (alloted.Checked == true)
                    {
                        if (streams == "")
                        {

                            streams = cid.Text;
                        }
                        else
                        {
                            streams =streams+","+ cid.Text;
                        }
                    }
                 
                


            }

            return streams;
        }
    }
}
