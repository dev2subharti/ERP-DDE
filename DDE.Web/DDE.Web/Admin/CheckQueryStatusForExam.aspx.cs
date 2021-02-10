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
    public partial class CheckQueryStatusForExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 43))
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

        private void setQStatus()
        {
            foreach (DataListItem dli in dtlistQStatus.Items)
            {

                Label status = (Label)dli.FindControl("lblQStatus");
                Button conf = (Button)dli.FindControl("btnConfirm");

                if (status.Text == "OK")
                {

                    status.BackColor = System.Drawing.Color.FromName("#81FD84");
                    conf.Visible = false;
                }

                else if (status.Text == "PENDING")
                {

                    status.BackColor = System.Drawing.Color.FromName("#F8A403");
                    conf.Visible = true;
                }


                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 10))
                {
                    if (status.Text == "OK")
                    {
                        conf.Visible = false;
                    }
                    else if (status.Text == "PENDING")
                    {
                        conf.Visible = true;
                    }
                }
                else
                {
                    conf.Visible = false;
                }


            }
        }

        private void populateQStatus()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (ddlistFilter.SelectedItem.Text == "ALL")
            {
                cmd.CommandText = "Select * from ExamRecord_June13 where DCase='1'";
            }
            else if (ddlistFilter.SelectedItem.Text == "PENDING")
            {
                cmd.CommandText = "Select * from ExamRecord_June13 where DCase='1' and QStatus='0'";
            }

            else if (ddlistFilter.SelectedItem.Text == "OK")
            {
                cmd.CommandText = "Select * from ExamRecord_June13 where DCase='1' and QStatus='1'";
            }
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("ExamRecordID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("ForYear");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("SCCode");
            DataColumn dtcol7 = new DataColumn("Status");


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
                drow["ExamRecordID"] = dr["ExamRecordID"].ToString();
                populateStudentInfo(Convert.ToInt32(dr["SRID"]),drow);

                if (dr["FYear"].ToString() == "")
                {

                    if (dr["EFP1Year"].ToString() == "True")
                    {
                        drow["ForYear"] = "1";
                    }
                    else if (dr["EFP2Year"].ToString() == "True")
                    {
                        drow["ForYear"] = "2";
                    }
                    else if (dr["EFP3Year"].ToString() == "True")
                    {
                        drow["ForYear"] = "3";
                    }
                }
                else
                {
                    drow["ForYear"] = dr["FYear"].ToString();
                }

                

                if (drow["ForYear"].ToString() == "1")
                {
                    if (dr["AFP1Year"].ToString() == "True" && dr["EFP1Year"].ToString() == "True")
                    {
                        drow["Status"] = "OK";
                    }
                    else
                    {
                        drow["Status"] = "PENDING";
                    }

                }

                else if (drow["ForYear"].ToString() == "2")
                {
                    if (dr["AFP2Year"].ToString() == "True" && dr["EFP2Year"].ToString() == "True")
                    {
                        drow["Status"] = "OK";
                    }
                    else
                    {
                        drow["Status"] = "PENDING";
                    }

                }
                else if (drow["ForYear"].ToString() == "3")
                {
                    if (dr["AFP3Year"].ToString() == "True" && dr["EFP3Year"].ToString() == "True")
                    {
                        drow["Status"] = "OK";
                    }
                    else
                    {
                        drow["Status"] = "PENDING";
                    }

                }
               

               
                dt.Rows.Add(drow);
                i = i + 1;
            }

            dtlistQStatus.DataSource = dt;
            dtlistQStatus.DataBind();

           

            con.Close();

            if (i <= 1)
            {

                dtlistQStatus.Visible = false;

                lblMSG.Text = "Sorry !! No Record Found";
                pnlMSG.Visible = true;
            }
            else if (i >1)
            {
                dtlistQStatus.Visible = true;
                pnlData.Visible = true;

                pnlMSG.Visible = false;
            }
           
        }

        private void populateStudentInfo(int srid, DataRow drow)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select * from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["SCCode"] = findSCCode(srid);
               

            }

            con.Close();
        }

        private string findSCCode(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SCStatus,StudyCentreCode from DDEStudentRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                if (dr["SCStatus"].ToString() == "O" || dr["SCStatus"].ToString() == "C")
                {
                    sccode = Convert.ToString(dr["StudyCentreCode"]);
                }
                else if (dr["SCStatus"].ToString() == "T")
                {
                    sccode = findTranferedSCCode(srid);
                }

            }

            con.Close();

            return sccode;
        }

        private string findTranferedSCCode(int srid)
        {
            string sccode = "NF";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select PreviousSC from DDEChangeSCRecord where SRID='" + srid + "'", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                sccode = dr[0].ToString();

            }

            con.Close();

            return sccode;
        }

        
       

        protected void dtlistQStatus_ItemCommand(object source, DataListCommandEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();

            if (e.CommandName == "1")
            {
                cmd.CommandText = "update ExamRecord_June13 set AFP1Year=@AFP1Year,QStatus=@QStatus where ExamRecordID ='" + Convert.ToInt32(e.CommandArgument) + "'";
                cmd.Parameters.AddWithValue("@AFP1Year", "True");
                cmd.Parameters.AddWithValue("@QStatus", "True");
            }
            else if (e.CommandName == "2")
            {
                cmd.CommandText = "update ExamRecord_June13 set AFP2Year=@AFP2Year,QStatus=@QStatus where ExamRecordID ='" + Convert.ToInt32(e.CommandArgument) + "'";
                cmd.Parameters.AddWithValue("@AFP2Year", "True");
                cmd.Parameters.AddWithValue("@QStatus", "True");
            }
            else if (e.CommandName == "3")
            {
                cmd.CommandText = "update ExamRecord_June13 set AFP3Year=@AFP3Year,QStatus=@QStatus where ExamRecordID ='" + Convert.ToInt32(e.CommandArgument) + "'";
                cmd.Parameters.AddWithValue("@AFP3Year", "True");
                cmd.Parameters.AddWithValue("@QStatus", "True");
            }

            cmd.Connection = con;
            con.Open();
            cmd.ExecuteReader();
            con.Close();

            Log.createLogNow("Confirmed Admission Fee", "Confirmed Admission Fee for June 2013 exam for Enrollment No.'" + FindInfo.findENoByERID(Convert.ToInt32(e.CommandArgument)) + "'" + FindInfo.findENoByECID(Convert.ToInt32(e.CommandArgument)) + "' ", Convert.ToInt32(Session["ERID"].ToString()));
            populateQStatus();
            setQStatus();
                  
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateQStatus();
            setQStatus();          
        }
    }
}
