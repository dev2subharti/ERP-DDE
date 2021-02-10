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
    public partial class SearchSLMIssueStatausByENo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 95) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 99))
            {
                pnlData.Visible = true;
                pnlSearch.Visible = true;
                pnlMSG.Visible = false;
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
            if (FindInfo.isENoExist(tbENo.Text))
            {
                int srid = FindInfo.findSRIDByENo(tbENo.Text);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Select DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.StudyCentreCode,DDESLMIssueRecord.Year,DDESLMIssueRecord.LNo from DDESLMIssueRecord inner join DDEStudentRecord on DDEStudentRecord.SRID=DDESLMIssueRecord.SRID where DDESLMIssueRecord.SRID='" + srid + "' order by DDESLMIssueRecord.Year";
               
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                DataTable dt = new DataTable();
                DataColumn dtcol1 = new DataColumn("SNo");
                DataColumn dtcol2 = new DataColumn("EnrollmentNo");
                DataColumn dtcol3 = new DataColumn("StudentName");
                DataColumn dtcol4 = new DataColumn("Year");
                DataColumn dtcol5 = new DataColumn("SCCode");
                DataColumn dtcol6 = new DataColumn("LID");
                DataColumn dtcol7 = new DataColumn("LG");
                DataColumn dtcol8 = new DataColumn("LGTime");
                DataColumn dtcol9 = new DataColumn("LP");
                DataColumn dtcol10 = new DataColumn("LPTime");
                DataColumn dtcol11 = new DataColumn("DType");
                DataColumn dtcol12 = new DataColumn("DokNo");

                dt.Columns.Add(dtcol1);
                dt.Columns.Add(dtcol2);
                dt.Columns.Add(dtcol3);
                dt.Columns.Add(dtcol4);
                dt.Columns.Add(dtcol5);
                dt.Columns.Add(dtcol6);
                dt.Columns.Add(dtcol7);
                dt.Columns.Add(dtcol8);
                dt.Columns.Add(dtcol9);
                dt.Columns.Add(dtcol10);
                dt.Columns.Add(dtcol11);
                dt.Columns.Add(dtcol12);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = (i + 1).ToString();
                    drow["EnrollmentNo"] = ds.Tables[0].Rows[i]["EnrollmentNo"].ToString();
                    drow["StudentName"] = ds.Tables[0].Rows[i]["StudentName"].ToString();
                    drow["Year"] = ds.Tables[0].Rows[i]["Year"].ToString();
                    drow["SCCode"] = ds.Tables[0].Rows[i]["StudyCentreCode"].ToString();

                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["LNo"]) == 0)
                    {
                        drow["LID"] = "-";
                        drow["LG"] = "No";
                        drow["LGTime"] = "-";
                        drow["LP"] = "No";
                        drow["LPTime"] = "-";
                        drow["DType"] = "-";
                        drow["DokNo"] = "-";
                       
                    }
                    else
                    {
                        string[] str = SLM.findSLMLetterDetails(Convert.ToInt32(ds.Tables[0].Rows[i]["LNo"]));
                        drow["LID"] = ds.Tables[0].Rows[i]["LNo"].ToString();
                        drow["LG"] = "Yes";
                        drow["LGTime"] = str[2];
                        if (str[3].ToString() == "01-01-1900")
                        {
                            drow["LP"] = "No";
                            drow["LPTime"] = "-";
                            drow["DType"] = "-";
                            drow["DokNo"] = "-";
                        }
                        else
                        {
                            drow["LP"] = "Yes";
                            drow["LPTime"] = str[3];
                            if (str[7].ToString() == "0")
                            {
                                drow["DType"] = "-";
                                drow["DokNo"] = "-";
                            }
                            else if (str[7].ToString() == "1")
                            {
                                drow["DType"] = "COURIER";
                                drow["DokNo"] = str[9].ToString();
                            }
                            else if (str[7].ToString() == "2")
                            {
                                drow["DType"] = "BY HAND";
                                drow["DokNo"] = str[9].ToString();
                            }
                        }
                        
                    }
                    
                    
                    dt.Rows.Add(drow);

                }


                dtlistShowStudents.DataSource = dt;
                dtlistShowStudents.DataBind();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtlistShowStudents.Visible = true;
                    tbENo.Enabled = false;
                    setcolor();
                }
                else
                {
                    dtlistShowStudents.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found";
                    pnlMSG.Visible = true;
                }

            }
            else
            {
                dtlistShowStudents.Visible = false;
                lblMSG.Text = "Sorry !! Invalid Enrollment No.";
                pnlMSG.Visible = true;
            }
        }

        private void setcolor()
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                Label lg = (Label)dli.FindControl("lblLG");
                Label lp = (Label)dli.FindControl("lblLP");
             


                if (lg.Text=="Yes")
                {
                    lg.BackColor = System.Drawing.Color.Green;
                    lg.ForeColor = System.Drawing.Color.White;


                }
                else if (lg.Text == "No")
                {
                    lg.BackColor = System.Drawing.Color.Orange;


                }
                if (lp.Text == "Yes")
                {
                    lp.BackColor = System.Drawing.Color.Green;
                    lp.ForeColor = System.Drawing.Color.White;


                }
                else if (lp.Text == "No")
                {
                    lp.BackColor = System.Drawing.Color.Orange;


                }


            }
        }
    }
}