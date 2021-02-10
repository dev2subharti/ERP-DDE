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
    public partial class ShowPaidFeeByENo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 57))
            {
                if (Request.QueryString["SRID"] != null)
                {
                    polulateStudentInfo(Convert.ToInt32(Request.QueryString["SRID"]));
                    populateFeePaid();
                    pnlSearch.Visible = false;
                    pnlStudentDetails.Visible = true;
                 
                }
                else
                {
                    pnlSearch.Visible = true;
                    pnlStudentDetails.Visible = false;
                   
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
      
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            populateFeePanel();
            
        }

        private void populateFeePanel()
        {
            int srid = FindInfo.findSRIDByENo(tbENo.Text);
            if (srid != 0)
            {

                polulateStudentInfo(srid);
                populateFeePaid();
                if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 56))
                {
                    gvShowStudent.Columns[11].Visible = true;
                    gvShowStudent.Columns[12].Visible = true;
                }
                else
                {
                    gvShowStudent.Columns[11].Visible = false;
                    gvShowStudent.Columns[12].Visible = false;
                }


            }

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! not a valid Enrollment No.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void populateFeePaid()
        {
            int i = 1;
            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("FeeHead");
            DataColumn dtcol3 = new DataColumn("Year");
            DataColumn dtcol4 = new DataColumn("Exam");
            DataColumn dtcol5 = new DataColumn("FormNo");
            DataColumn dtcol6 = new DataColumn("Amount");
            DataColumn dtcol7 = new DataColumn("Payment Mode");
            DataColumn dtcol8 = new DataColumn("D/C No.");
            DataColumn dtcol9 = new DataColumn("D/C Date");
            DataColumn dtcol10 = new DataColumn("Total D/C Amount");
            DataColumn dtcol11 = new DataColumn("Discription_E");
            DataColumn dtcol12 = new DataColumn("Discription_D");
            DataColumn dtcol13 = new DataColumn("SubmittedOn");
            DataColumn dtcol14 = new DataColumn("TOFS");
            DataColumn dtcol15 = new DataColumn("FRID");


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
            dt.Columns.Add(dtcol13);
            dt.Columns.Add(dtcol14);
            dt.Columns.Add(dtcol15);
          
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("Select AcountSession from DDEAcountSession", con);
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("select FRID,FeeHead,PaymentMode,DCNumber,DCDate,TotalDCAmount,Amount,ForYear,ForExam,CONVERT(datetime,[TOFS],105) as TOFS from [DDEFeeRecord_" + dr["AcountSession"].ToString() + "] where SRID='" + lblSRID.Text + "' order by TOFS ASC", con1);

                    SqlDataReader dr1;

                    con1.Open();
                    dr1 = cmd1.ExecuteReader();
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            DataRow drow = dt.NewRow();
                            drow["SNo"] = i;
                            drow["FeeHead"] = FindInfo.findFeeHeadNameByID(Convert.ToInt32(dr1["FeeHead"]));
                            drow["Year"] = Convert.ToString(dr1["ForYear"]);
                           
                            if (Convert.ToString(dr1["ForExam"]) != "NA")
                            {
                                drow["Exam"] = FindInfo.findExamNameByCode(Convert.ToString(dr1["ForExam"]));                                                       
                            }
                            else
                            {
                                drow["Exam"] = Convert.ToString(dr1["ForExam"]);
                            }
                            if (Convert.ToInt32(dr1["FeeHead"]) == 2)
                            {
                                drow["FormNo"] = FindInfo.findFormCounter(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(drow["Year"]), dr1["ForExam"].ToString(), "R");
                            }
                            else if (Convert.ToInt32(dr1["FeeHead"]) == 3)
                            {
                                drow["FormNo"] = FindInfo.findFormCounter(Convert.ToInt32(lblSRID.Text), Convert.ToInt32(drow["Year"]), dr1["ForExam"].ToString(), "B");
                            }
                            else
                            {
                                drow["FormNo"] = "NA";
                            }
                            drow["Payment Mode"] = FindInfo.findPaymentModeByID(Convert.ToInt32(dr1["PaymentMode"]));
                            drow["Amount"] = Convert.ToString(dr1["Amount"]);
                            drow["D/C No."] = Convert.ToString(dr1["DCNumber"]);
                            drow["D/C Date"] = Convert.ToDateTime(dr1["DCDate"]).ToString("dd MMMM yyyy").ToUpper();
                            drow["Total D/C Amount"] = Convert.ToString(dr1["TotalDCAmount"]);
                            drow["Discription_E"] = "E_" + dr["AcountSession"] + "_" + drow["FeeHead"].ToString() + " of " + tbENo.Text + " for Year '" + drow["Year"].ToString() + " for Exam '" + drow["Exam"].ToString();
                            drow["Discription_D"] = "D_" + dr["AcountSession"] + "_" + drow["FeeHead"].ToString() + " of " + tbENo.Text + " for Year '" + drow["Year"].ToString() + " for Exam '" + drow["Exam"].ToString();
                            drow["SubmittedOn"] =Convert.ToDateTime(dr1["TOFS"]).ToString("dd-MM-yyyy hh:mm tt");
                            drow["TOFS"] =Convert.ToDateTime(dr1["TOFS"]).ToString("yyyy-MM-dd hh:mm:ss tt");
                            drow["FRID"] = dr1["FRID"].ToString();
                            dt.Rows.Add(drow);
                            i = i + 1;
                        }

                    }
                    con1.Close();
                }
                con.Close();


             dt.DefaultView.Sort = "TOFS ASC";
             DataView dv = dt.DefaultView;


             int j = 1;
             foreach (DataRowView dvr in dv)
             {
                 dvr[0] = j;
                 j++;
             }

            gvShowStudent.DataSource = dt;
            gvShowStudent.DataBind();

            con.Close();
           

            if (i > 1)
            {
                pnlSearch.Visible = false;
                pnlStudentDetails.Visible = true;
            }
            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! No Record Found.";
                pnlMSG.Visible = true;
                btnOK.Visible = true;
            }
        }

        private void polulateStudentInfo(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select EnrollmentNo,StudentName,FatherName,StudyCentreCode,CYear,Course,Course2Year,Course3Year from DDEStudentRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                imgStudent.ImageUrl = "StudentImgHandler.ashx?SRID=" + srid.ToString();
                tbEnNo.Text = dr["EnrollmentNo"].ToString();
                lblSRID.Text = srid.ToString();
                tbSName.Text = dr["StudentName"].ToString();
                tbFName.Text = dr["FatherName"].ToString();
                tbSCCode.Text = FindInfo.findBothTCSCCodeBySRID(srid);
                tbCourse.Text = FindInfo.findCourseNameBySRID(srid,Convert.ToInt32(dr["CYear"]));
                         
            }

            con.Close();
        }
        
        protected void btnOK_Click(object sender, EventArgs e)
        {
            btnOK.Visible = false;
            pnlMSG.Visible = false;
            pnlSearch.Visible = true;
        }
             
        protected void gvShowStudent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmmd = e.CommandName.Substring(0, 1);
          
            string asession = e.CommandName.Substring(2, 7);
            if (cmmd == "E")
            {
                Session["ASession"] = asession;
                Response.Redirect("EditFeeDetail.aspx?FRID=" + Convert.ToInt32(e.CommandArgument));
            }
            else if (cmmd == "D")
            {
                int srid = FindInfo.findSRIDByFRID(Convert.ToInt32(e.CommandArgument),asession);

                int fhid = FindInfo.findFHIDByFRID(Convert.ToInt32(e.CommandArgument), asession);

                string exam = FindInfo.findForExamByFRID(Convert.ToInt32(e.CommandArgument), asession);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("delete from [DDEFeeRecord_" + asession + "] where FRID ='" + Convert.ToString(e.CommandArgument) + "'", con);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

               

                if (fhid == 2)
                {
                    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd1 = new SqlCommand("delete from DDEExamRecord_" + exam + " where SRID ='" + srid+ "' and MOE='R'", con1);


                    con1.Open();
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                }
                else if (fhid == 3)
                {
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd2 = new SqlCommand("delete from DDEExamRecord_" + exam + " where SRID ='" + srid + "' and MOE='B'", con2);


                    con2.Open();
                    cmd2.ExecuteNonQuery();
                    con2.Close();
                }

                Log.createLogNow("Delete", "Delete " + e.CommandName.Substring(10, (e.CommandName.Length - 10)), Convert.ToInt32(Session["ERID"].ToString()));

                populateFeePanel();
            }
        }

       
    }
}
