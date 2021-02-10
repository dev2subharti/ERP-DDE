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
    public partial class ShowGrievances : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 97))
            {
                if(!IsPostBack)
                {
                    PopulateDDList.populateGCategory(ddlistGC);
                   
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
            populateQueries();
            //setBtnStatus();
        }

        private void setBtnStatus()
        {
            foreach (DataListItem dli in dtlistShowQueries.Items)
            {
                TextBox remark = (TextBox)dli.FindControl("tbRemark");
                Button btn = (Button)dli.FindControl("btnSolved");

                if (ddlistStatus.SelectedItem.Value == "1")
                {
                    remark.Enabled = true;
                    btn.Text = "Make Solved";
                    btn.CommandName = "Make Solved";
                }
                else if (ddlistStatus.SelectedItem.Value == "2")
                {
                    remark.Enabled = false;
                    btn.Text = "Make Pending";
                    btn.CommandName = "Make Pending";
                }
            }
        }

        private void populateQueries()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if(cbNewMsg.Checked==true)
            {
                if (ddlistStatus.SelectedItem.Value == "0")
                {
                    if (ddlistGC.SelectedItem.Text == "ALL")
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='False' order by DDEGrievances.GID";
                    }
                    else
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='False' and DDEGrievances.GCID='" + ddlistGC.SelectedItem.Value + "' order by DDEGrievances.GID";
                    }

                }
                else if (ddlistStatus.SelectedItem.Value == "1")
                {
                    if (ddlistGC.SelectedItem.Text == "ALL")
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='True' order by DDEGrievances.GID";
                    }
                    else
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='True' and DDEGrievances.GCID='" + ddlistGC.SelectedItem.Value + "' order by DDEGrievances.GID";
                    }
                }
            }
            else
            {
                if (ddlistStatus.SelectedItem.Value == "0")
                {
                    if (ddlistGC.SelectedItem.Text == "ALL")
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='False' order by DDEGrievances.GID";
                    }
                    else
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='False' and DDEGrievances.GCID='" + ddlistGC.SelectedItem.Value + "' order by DDEGrievances.GID";
                    }

                }
                else if (ddlistStatus.SelectedItem.Value == "1")
                {
                    if (ddlistGC.SelectedItem.Text == "ALL")
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='True' order by DDEGrievances.GID";
                    }
                    else
                    {
                        cmd.CommandText = "Select DDEGrievanceCategory.GCategory,DDEGrievances.GID,DDEGrievances.SRID,DDEGrievances.MNo,DDEGrievances.EmailID,DDEGrievances.RegisteredOn,DDEGrievances.Status,DDEGrievances.ClosedOn,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName from DDEGrievances inner join DDEStudentRecord on DDEGrievances.SRID=DDEStudentRecord.SRID inner join DDEGrievanceCategory on DDEGrievances.GCID=DDEGrievanceCategory.GCID where DDEGrievances.Status='True' and DDEGrievances.GCID='" + ddlistGC.SelectedItem.Value + "' order by DDEGrievances.GID";
                    }
                }
            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("GID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("Subject");
            DataColumn dtcol5 = new DataColumn("ENo");
            DataColumn dtcol6 = new DataColumn("Name");
            DataColumn dtcol7 = new DataColumn("ROn");
            DataColumn dtcol8 = new DataColumn("COn");
            DataColumn dtcol9 = new DataColumn("NewMsg");
          


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);
            dt.Columns.Add(dtcol8);
            dt.Columns.Add(dtcol9);


            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["GID"] = Convert.ToInt32(dr["GID"]);
                drow["SRID"] = Convert.ToInt32(dr["SRID"]);
                drow["Subject"] = Convert.ToString(dr["GCategory"]);
                drow["ENo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["Name"] = Convert.ToString(dr["StudentName"]);
                drow["ROn"] = Convert.ToDateTime(dr["RegisteredOn"]).ToString("dd-MM-yyyy");
                if(Convert.ToDateTime(dr["ClosedOn"]).ToString("dd-MM-yyyy")=="01-01-1900")
                {
                    drow["COn"] = "-";
                }
                else
                {
                    drow["COn"] = Convert.ToDateTime(dr["ClosedOn"]).ToString("dd-MM-yyyy");
                }

                int newmsg = FindInfo.findUnReadMsgByGID(Convert.ToInt32(dr["GID"]));

                if (cbNewMsg.Checked==true)
                {
                   
                    if(newmsg>0)
                    {
                        drow["NewMsg"] = newmsg;
                        dt.Rows.Add(drow);
                        i = i + 1;
                    }                  

                }
                else
                {
                    drow["NewMsg"] = newmsg;
                    dt.Rows.Add(drow);
                    i = i + 1;

                }
                
               
               
            }



            dtlistShowQueries.DataSource = dt;
            dtlistShowQueries.DataBind();
            con.Close();

            if (i > 1)
            {
                dtlistShowQueries.Visible = true;
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowQueries.Visible = false;
                lblMSG.Text = "Sorry!! No Record Found.";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowQueries_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "Show")
            {
                Label eno = (Label)e.Item.FindControl("lblENo");
                Label subject = (Label)e.Item.FindControl("lblSubject");

                populateGrievanceStatus(Convert.ToInt32(e.CommandArgument),subject.Text, eno.Text);
                populateGrievanceTransactions(Convert.ToInt32(e.CommandArgument));
                setValidation();
                updateQueryReadStuatus(Convert.ToInt32(e.CommandArgument));
                populateQueries();
                ModalPopupExtender1.Show();
                pnlGT.Visible = true;

            }
          

           
        }

        private void updateQueryReadStuatus(int gid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update DDEGrievanceTransactions set QueryRead=@QueryRead where GID='" + gid + "' and QueryRead='False' ", con);
            cmd.Parameters.AddWithValue("@QueryRead", "True");

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void populateGrievanceStatus(int gid,string subject, string eno)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEGrievances where GID='" + gid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();


            if (dr.HasRows)
            {
                dr.Read();

                lblSubject.Text = subject;
                lblENo.Text = eno;
                lblSRID.Text = Convert.ToInt32(dr["SRID"]).ToString();
                lblGID.Text = String.Format("{0:000000}", gid);
                if (dr["Status"].ToString() == "True")
                {
                    rblStatus.Items.FindByValue("1").Selected = true;
                }
                else if (dr["Status"].ToString() == "False")
                {
                    rblStatus.Items.FindByValue("0").Selected = true;
                }

            }



            con.Close();
        }

        private void setValidation()
        {
            foreach (DataListItem dli in dtlist.Items)
            {
                Label tt = (Label)dli.FindControl("lblTT");
                Label query = (Label)dli.FindControl("lblQuery");
                Label reply = (Label)dli.FindControl("lblReply");
                Image left = (Image)dli.FindControl("imgLeft");
                Image right = (Image)dli.FindControl("imgRight");

                if (tt.Text == "Q")
                {
                    right.Visible = false;
                    query.Visible = false;

                    left.Visible = true;
                    reply.Visible = true;

                   
                }
                else if (tt.Text == "R")
                {
                    right.Visible = true;
                    query.Visible = true;

                    left.Visible = false;
                    reply.Visible = false;
                }
            }
        }

        private void populateGrievanceTransactions(int gid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEGrievanceTransactions where GID='" + gid + "' order by GTID", con);
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("GTID");
            DataColumn dtcol2 = new DataColumn("TT");
            DataColumn dtcol3 = new DataColumn("Query");
            DataColumn dtcol4 = new DataColumn("EntryOn");
            DataColumn dtcol5 = new DataColumn("Reply");
            DataColumn dtcol6 = new DataColumn("ReplyOn");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);


            int i = 0;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();

                drow["GTID"] = Convert.ToString(dr["GTID"]);
                drow["TT"] = Convert.ToString(dr["TT"]);



                if (Convert.ToString(dr["TT"]) == "Q")
                {
                    drow["Query"] = Convert.ToString(dr["Query"]);
                    drow["EntryOn"] = Convert.ToDateTime(dr["EntryOn"]).ToString("dd-MM-yyyy");
                    drow["Reply"] = "";
                    drow["ReplyOn"] = "";
                }
                else if (Convert.ToString(dr["TT"]) == "R")
                {
                    drow["Query"] = "";
                    drow["EntryOn"] = "";
                    drow["Reply"] = Convert.ToString(dr["Reply"]);
                    drow["ReplyOn"] = Convert.ToDateTime(dr["ReplyOn"]).ToString("dd-MM-yyyy");
                }


                dt.Rows.Add(drow);
                i = i + 1;


            }

            dtlist.DataSource = dt;
            dtlist.DataBind();

            con.Close();

         
        }

        protected void ddlistStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowQueries.Visible = false;
        }

        protected void ddlistGC_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowQueries.Visible = false;
        }

        protected void imgbtnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (tbQuery.Text != "")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("insert into DDEGrievanceTransactions OUTPUT INSERTED.GTID values(@GID,@TT,@Query,@EntryOn,@QueryBy,@QueryRead,@Reply,@ReplyOn,@ReplyBy,@ReplyRead)", con);

                cmd.Parameters.AddWithValue("@GID", Convert.ToInt32(lblGID.Text));
                cmd.Parameters.AddWithValue("@TT", "R");
                cmd.Parameters.AddWithValue("@Query", "");
                cmd.Parameters.AddWithValue("@EntryOn", "");
                cmd.Parameters.AddWithValue("@QueryBy", 0);
                cmd.Parameters.AddWithValue("@QueryRead", "False");
                cmd.Parameters.AddWithValue("@Reply", tbQuery.Text);
                cmd.Parameters.AddWithValue("@ReplyOn", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@ReplyBy", Convert.ToInt32(Session["ERID"]));
                cmd.Parameters.AddWithValue("@ReplyRead", "False");

                cmd.Connection = con;
                con.Open();
                object gtid = cmd.ExecuteScalar();
                con.Close();
                tbQuery.Text = "";
                populateGrievanceTransactions(Convert.ToInt32(lblGID.Text));
                setValidation();        
                ModalPopupExtender1.Show();
                pnlGT.Visible = true;

            }
        }

        protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rblStatus.SelectedItem.Value=="0")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEGrievances set Status=@Status where GID='" + Convert.ToInt32(lblGID.Text) + "'", con);
                cmd.Parameters.AddWithValue("@Status", "False");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                populateQueries();
            }
            else if (rblStatus.SelectedItem.Value == "1")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEGrievances set Status=@Status where GID='" + Convert.ToInt32(lblGID.Text) + "'", con);
                cmd.Parameters.AddWithValue("@Status", "True");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                populateQueries();
            }
        }

        protected void cbNewMsg_CheckedChanged(object sender, EventArgs e)
        {
            dtlistShowQueries.Visible = false;
        }
    }
}