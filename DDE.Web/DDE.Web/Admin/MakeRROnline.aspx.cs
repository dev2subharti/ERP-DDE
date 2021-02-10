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
using System.Data.SqlClient;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class MakeRROnline : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 31))
            {
                if (!IsPostBack)
                {
                    PopulateDDList.populateExam(ddlistExam);
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

        private void populateCourses()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse where Online='True' order by CourseShortName", con);
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

                if (dr[2].ToString() == "")
                {
                    drow["CourseName"] = dr[1].ToString();
                    
                }

                else
                {
                    drow["CourseName"] = dr[1].ToString() + " " + "(" + dr[2].ToString() + ")";
                   

                }
               
               
                dt.Rows.Add(drow);
                i = i + 1;
            }



            dtlistShowCourses.DataSource = dt;
            dtlistShowCourses.DataBind();
            con.Close();

        }

        private void populateResultStatus()
        {
            foreach (DataListItem dli in dtlistShowCourses.Items)
            {

                Label cid = (Label)dli.FindControl("lblCourseID");
                Button b1 = (Button)dli.FindControl("btn1Year");
                Button b2 = (Button)dli.FindControl("btn2Year");
                Button b3 = (Button)dli.FindControl("btn3Year");
                Label r1 = (Label)dli.FindControl("R1Y");
                Label r2 = (Label)dli.FindControl("R2Y");
                Label r3 = (Label)dli.FindControl("R3Y");

              
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select *  from DDEMakeResultOnline where CourseID='" + cid.Text + "' and Examination='"+ddlistExam.SelectedItem.Value+"' and MOE='"+ddlistMOE.SelectedItem.Value+"'",con);
                SqlDataReader dr;
              
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (Convert.ToInt32(dr["Year"]) == 1)
                        {
                            if (dr["Status"].ToString() == "True")
                            {
                                b1.Text = "MAKE OFFLINE";
                                b1.BackColor = System.Drawing.Color.FromName("#81FD84");
                                r1.Text = "True";
                            }

                            else
                            {
                                b1.Text = "MAKE ONLINE";
                                b1.BackColor = System.Drawing.Color.FromName("#F8A403");
                                r1.Text = "False";
                            }

                        }
                        if (Convert.ToInt32(dr["Year"]) == 2)
                        {
                            if (dr["Status"].ToString() == "True")
                            {
                                b2.Text = "MAKE OFFLINE";
                                b2.BackColor = System.Drawing.Color.FromName("#81FD84"); 
                                r2.Text = "True";
                            }

                            else
                            {
                                b2.Text = "MAKE ONLINE";
                                b2.BackColor = System.Drawing.Color.FromName("#F8A403");
                                r2.Text = "False";
                            }
                        }
                        if (Convert.ToInt32(dr["Year"]) == 3)
                        {
                            if (dr["Status"].ToString() == "True")
                            {
                                b3.Text = "MAKE OFFLINE";
                                b3.BackColor = System.Drawing.Color.FromName("#81FD84");
                                r3.Text = "True";
                            }
                            else
                            {
                                b3.Text = "MAKE ONLINE";
                                b3.BackColor = System.Drawing.Color.FromName("#F8A403");

                                r3.Text = "False";
                            }
                        }
                    }

                }
              
                
                
                con.Close();


            }
            
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            populateData();
            
        }

        private void populateData()
        {
            populateCourses();
            setCourseYear();
            populateResultStatus();
            dtlistShowCourses.Visible = true;
            pnlMSG.Visible = false;
        }

        private void setCourseYear()
        {
            foreach (DataListItem dli in dtlistShowCourses.Items)
            {

                Label cid = (Label)dli.FindControl("lblCourseID");
                Button b1 = (Button)dli.FindControl("btn1Year");
                Button b2 = (Button)dli.FindControl("btn2Year");
                Button b3 = (Button)dli.FindControl("btn3Year");

             

                int cduration=FindInfo.findCourseDuration(Convert.ToInt32(cid.Text) );

                if (cduration==1)
                {
                    b1.Visible = true;
                    b2.Visible = false;
                    b3.Visible = false;                  
                }
                else if (cduration == 2)
                {
                    b1.Visible = true;
                    b2.Visible = true;
                    b3.Visible = false;
                }
                else if (cduration == 3)
                {
                    b1.Visible = true;
                    b2.Visible = true;
                    b3.Visible = true;
                }
               
            }
        }

        protected void dtlistShowCourses_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label cid = (Label)e.Item.FindControl("lblCourseID");
            Button b1 = (Button)e.Item.FindControl("btn1Year");
            Button b2 = (Button)e.Item.FindControl("btn2Year");
            Button b3 = (Button)e.Item.FindControl("btn3Year");

            Label r1 = (Label)e.Item.FindControl("R1Y");
            Label r2 = (Label)e.Item.FindControl("R2Y");
            Label r3 = (Label)e.Item.FindControl("R3Y");

            
            if(e.CommandArgument.ToString()=="1")
            {
                if (FindInfo.resultlistExist(Convert.ToInt32(cid.Text), 1, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
                {
                    if (b1.Text== "MAKE ONLINE")
                    {
                        updateResultList(Convert.ToInt32(cid.Text), 1, "True");
                    }
                    else if (b1.Text == "MAKE OFFLINE")
                    {
                        updateResultList(Convert.ToInt32(cid.Text), 1, "False");
                    }
                }
                else
                {
                    if (b1.Text == "MAKE ONLINE")
                    {
                        insertResultList(Convert.ToInt32(cid.Text), 1, "True");
                    }
                    else if (b1.Text == "MAKE OFFLINE")
                    {
                        insertResultList(Convert.ToInt32(cid.Text), 1, "False");
                    }                  
                }
                
            }
            else if (e.CommandArgument.ToString() == "2")
            {
                if (FindInfo.resultlistExist(Convert.ToInt32(cid.Text), 2, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
                {
                    if (b2.Text == "MAKE ONLINE")
                    {
                        updateResultList(Convert.ToInt32(cid.Text), 2, "True");
                    }
                    else if (b2.Text == "MAKE OFFLINE")
                    {
                        updateResultList(Convert.ToInt32(cid.Text), 2, "False");
                    }
                }
                else
                {
                    if (b2.Text == "MAKE ONLINE")
                    {
                        insertResultList(Convert.ToInt32(cid.Text), 2, "True");
                    }
                    else if (b2.Text == "MAKE OFFLINE")
                    {
                        insertResultList(Convert.ToInt32(cid.Text), 2, "False");
                    }
                }                
                
            }
            else if (e.CommandArgument.ToString() == "3")
            {
                if (FindInfo.resultlistExist(Convert.ToInt32(cid.Text), 3, ddlistExam.SelectedItem.Value, ddlistMOE.SelectedItem.Value))
                {
                    if (b3.Text == "MAKE ONLINE")
                    {
                        updateResultList(Convert.ToInt32(cid.Text), 3, "True");
                    }
                    else if (b3.Text == "MAKE OFFLINE")
                    {
                        updateResultList(Convert.ToInt32(cid.Text), 3, "False");
                    }
                }
                else
                {
                    if (b3.Text == "MAKE ONLINE")
                    {
                        insertResultList(Convert.ToInt32(cid.Text), 3, "True");
                    }
                    else if (b3.Text == "MAKE OFFLINE")
                    {
                        insertResultList(Convert.ToInt32(cid.Text), 3, "False");
                    }
                }                
            }

            populateResultStatus();
           
        }

        private void insertResultList(int cid, int year, string status)
        {
            string rs = "";
            if (status == "True")
            {
                rs = "ONLINE";
            }

            else if (status == "False")
            {
                rs = "OFFLINE";
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("insert into  DDEMakeResultOnline values(@CourseID,@Year,@Examination,@MOE,@Status)", con);
            
            cmd.Parameters.AddWithValue("@CourseID", cid);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Examination", ddlistExam.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@MOE", ddlistMOE.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@Status", status);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Make Result Online", "Made " + ddlistExam.SelectedItem.Text + " result " + rs + " of course '" + FindInfo.findCourseNameByID(cid), Convert.ToInt32(Session["ERID"].ToString()));           
        }

        private void updateResultList(int cid, int year, string status)
        {
            string rs = "";
            if (status == "True")
            {
                rs = "ONLINE";

            }

            else if (status == "False")
            {
                rs = "OFFLINE";
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("update  DDEMakeResultOnline set Status=@Status where CourseID='" + cid + "' and Year='" + year + "' and Examination='" + ddlistExam.SelectedItem.Value + "' and MOE='" + ddlistMOE.SelectedItem.Value + "'", con);

            cmd.Parameters.AddWithValue("@Status",status);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Log.createLogNow("Make Result Online", "Made " + ddlistExam.SelectedItem.Text + " result " + rs + " of course '" + FindInfo.findCourseNameByID(cid), Convert.ToInt32(Session["ERID"].ToString()));

          
        }

        protected void ddlistExam_SelectedIndexChanged1(object sender, EventArgs e)
        {
            dtlistShowCourses.Visible = false;

            pnlMSG.Visible = false;
        }

       

        protected void ddlistMOE_SelectedIndexChanged1(object sender, EventArgs e)
        {
            dtlistShowCourses.Visible = false;

            pnlMSG.Visible = false;
        }

      
       
    }
}
