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
    public partial class ShowOnlineQueries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 97))
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
            populateQueries();
            setBtnStatus();
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
            if (ddlistStatus.SelectedItem.Value == "1")
            {
                cmd.CommandText = "Select * from DDEOnlineQueries where Solved='0' and RecordStatus='1' order by QID";
            }
            else if (ddlistStatus.SelectedItem.Value == "2")
            {
                cmd.CommandText = "Select * from DDEOnlineQueries where Solved='1' and RecordStatus='1' order by QID";
            }

            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("QID");
            DataColumn dtcol3 = new DataColumn("Name");
            DataColumn dtcol4 = new DataColumn("EmailID");
            DataColumn dtcol5 = new DataColumn("ContactNo");
            DataColumn dtcol6 = new DataColumn("Query");
            DataColumn dtcol7 = new DataColumn("Remark");






            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);
            dt.Columns.Add(dtcol6);
            dt.Columns.Add(dtcol7);



            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["QID"] = Convert.ToString(dr["QID"]);
                drow["Name"] = Convert.ToString(dr["Name"]);
                drow["EmailID"] = Convert.ToString(dr["EmailID"]);
                drow["ContactNo"] = Convert.ToString(dr["ContactNo"]);
                drow["Query"] = Convert.ToString(dr["Query"]);
                drow["Remark"] = Convert.ToString(dr["Remark"]);
                dt.Rows.Add(drow);
                i = i + 1;
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
          
            if (e.CommandName == "Make Solved")
            {
                TextBox remark = (TextBox)e.Item.FindControl("tbRemark");

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEOnlineQueries set Remark=@Remark,Solved=@Solved,SolvedBy=@SolvedBy,TOSolving=@TOSolving  where QID='" + e.CommandArgument + "'", con);
                
                cmd.Parameters.AddWithValue("@Remark", remark.Text);
                cmd.Parameters.AddWithValue("@Solved", "True");
                cmd.Parameters.AddWithValue("@SolvedBy", Convert.ToInt32(Session["ERID"]));
                cmd.Parameters.AddWithValue("@TOSolving", DateTime.Now.ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

               
            }
            else if (e.CommandName == "Make Pending")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEOnlineQueries set Remark=@Remark,Solved=@Solved,SolvedBy=@SolvedBy,TOSolving=@TOSolving  where QID='" + e.CommandArgument + "'", con);

                cmd.Parameters.AddWithValue("@Remark", "");
                cmd.Parameters.AddWithValue("@Solved", "False");
                cmd.Parameters.AddWithValue("@SolvedBy", 0);
                cmd.Parameters.AddWithValue("@TOSolving", "");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

               
            }
            else if (e.CommandName == "Make Offline")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("update DDEOnlineQueries set RecordStatus=@RecordStatus where QID='" + e.CommandArgument + "'", con);


                cmd.Parameters.AddWithValue("@RecordStatus", "False");
             

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


            }

            populateQueries();
            setBtnStatus();
        }

        protected void ddlistStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowQueries.Visible = false;
        }
    }
}