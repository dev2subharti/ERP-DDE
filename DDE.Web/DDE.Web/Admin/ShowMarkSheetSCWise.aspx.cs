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
using System.Text;

namespace DDE.Web.Admin
{
    public partial class ShowMarkSheetSCWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 112))
            {
                if (!IsPostBack)
                {
                  
                    PopulateDDList.populateStudyCentre(ddlistSC);
                  
                    ddlistMOE.Items.FindByValue("R").Selected = true;
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
            ddlistSelect.SelectedItem.Selected = false;
            ddlistSelect.Items.FindByText("--SELECT ONE--").Selected = true;
            populateStudents();
            setColor();
            

        }

        private void setColor()
        {
            int pending = 0;
            int ok = 0;
            int printedm = 0;
            int pendingprint = 0;

            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                Label ec = (Label)dli.FindControl("lblEC");
                Label msstatus = (Label)dli.FindControl("lblMSStatus");
                Label printed = (Label)dli.FindControl("lblPrinted");
                Label detained = (Label)dli.FindControl("lblDetained");
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");
                Label srid = (Label)dli.FindControl("lblSRID");
                Label year = (Label)dli.FindControl("lblYear");
                Label mname = (Label)dli.FindControl("lblMName");
                Label m = (Label)dli.FindControl("lblM");
                Label graced = (Label)dli.FindControl("lblGracedStudent");

                if (printed.Text == "Yes")
                {
                    printed.BackColor = System.Drawing.Color.FromName("#81FD84");
                }
                else if (printed.Text == "No")
                {
                    printed.BackColor = System.Drawing.Color.FromName("#F8A403");
                    
                }
                if (detained.Text == "Yes")
                {
                    cb.Checked = false;
                    cb.Enabled = false;
                    printed.BackColor = System.Drawing.Color.FromName("#B41104");
                }
                else if (detained.Text == "No")
                {
                    
                    cb.Enabled = true;
                }

                if (FindInfo.isGraced(Convert.ToInt32(srid.Text), Convert.ToInt32(year.Text), ddlistExam.SelectedItem.Value))
                {
                    cb.Checked=false;
                    cb.Enabled=false;
                    graced.Visible = true;
                    
                }
                if (msstatus.Text == "OK")
                {
                    if (mname.Text == "" || (mname.Text == "NA"))
                    {
                        cb.Checked = false;
                        cb.Enabled = false;
                        m.Visible = true;
                      
                        msstatus.Text = "Pending";
                        m.Text = "(MO)";

                    }
                }
                else if (msstatus.Text == "Pending")
                {
                    if (mname.Text == "" || (mname.Text == "NA"))
                    {
                        cb.Checked = false;
                        cb.Enabled = false;
                        m.Visible = true;
                        m.Text = "(MK, MO)";

                    }
                    else
                    {
                        cb.Checked = false;
                        cb.Enabled = false;
                        m.Visible = true;
                        m.Text = "(MK)";
                    }
                }

                if(ddlistExam.SelectedItem.Value=="W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
                {
                    if (Convert.ToInt32(ec.Text) == 0)
                    {
                        cb.Checked = false;
                        cb.Enabled = false;
                        ec.Visible = true;

                        msstatus.Text = "Pending";
                        ec.Text = "(EC)";

                    }

                }

                if (msstatus.Text == "Pending")
                {
                    pending = pending + 1;
                }
                else if (msstatus.Text == "OK")
                {
                    ok = ok + 1;
                    if (printed.Text == "No")
                    {
                        pendingprint = pendingprint + 1;
                    }
                }
                if (printed.Text == "Yes")
                {
                    printedm = printedm + 1;
                }
                lblPending.Text = "Pending MS: " + pending.ToString();
                lblOK.Text = "OK MS: " + ok.ToString();
                lblPrinted.Text = "Printed : " + printedm.ToString();
                lblPendingPrint.Text = "Pending Print : " + pendingprint.ToString();
            }
        }

        private void populateStudents()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();


            cmd.CommandText = "Select distinct DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".VECID,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSCounter,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.MotherName,DDEStudentRecord.Course,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSPrinted from  DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='R' and (((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and (DDEStudentRecord.StudyCentreCode='" + ddlistSC.SelectedItem.Value + "')) or (DDEStudentRecord.PreviousSCCode='" + ddlistSC.SelectedItem.Value + "' and DDEStudentRecord.SCStatus='T')) and DDEStudentRecord.StudentPhoto is not null and DDEStudentRecord.RecordStatus='True' order by EnrollmentNo";
           

            //cmd.CommandText = findQuery(); 

            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID"); 
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("FatherName");
            DataColumn dtcol6 = new DataColumn("MotherName");
            DataColumn dtcol7 = new DataColumn("MSStatus");
            DataColumn dtcol8 = new DataColumn("MSCounter");
            DataColumn dtcol9 = new DataColumn("Year");
            DataColumn dtcol10 = new DataColumn("Printed");
            DataColumn dtcol11 = new DataColumn("Detained");
            DataColumn dtcol12 = new DataColumn("VECID");



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


            int count = 1;
            string[] detained = FindInfo.findDetainedStudents(ddlistExam.SelectedItem.Value, "R");

            

            if (ds.Tables[0].Rows.Count > 0)
            {
                //if (ddlistSC.SelectedItem.Text != "001" && ddlistSC.SelectedItem.Text != "999" && ddlistSC.SelectedItem.Text != "998" && ddlistSC.SelectedItem.Text != "997" && ddlistSC.SelectedItem.Text != "996" && ddlistSC.SelectedItem.Text != "995" && ddlistSC.SelectedItem.Text != "994" && ddlistSC.SelectedItem.Text != "417" && ddlistSC.SelectedItem.Text != "084" && ddlistSC.SelectedItem.Text != "081" && ddlistSC.SelectedItem.Text != "029" && ddlistSC.SelectedItem.Text != "150" && ddlistSC.SelectedItem.Text != "449" && ddlistSC.SelectedItem.Text != "261" && ddlistSC.SelectedItem.Text != "440")
                //{
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        if (!(dt.AsEnumerable().Any(row => ds.Tables[0].Rows[i]["SRID"].ToString() == row.Field<String>("SRID"))))
                //        {

                //            string year = "";

                //            year = FindInfo.findAllExamYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value, "R");


                //            if (year != "NF" && year != "0" && year != "")
                //            {
                //                DataRow drow = dt.NewRow();
                //                int yr = 0;
                //                if (year.Length == 1)
                //                {
                //                    yr = Convert.ToInt32(year);

                //                    drow["Printed"] = findMSPrintedByYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), yr);
                //                }
                //                else if (year.Length == 3)
                //                {
                //                    yr = Convert.ToInt32(year.Substring(2, 1));

                //                    drow["Printed"] = findMSPrintedByYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]),yr);
                //                }
                //                else if (year.Length == 5)
                //                {
                //                    yr = Convert.ToInt32(year.Substring(4, 1));
                //                    drow["Printed"] = findMSPrintedByYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), yr);
                //                }

                //                drow["SNo"] = Convert.ToString(count);
                //                drow["SRID"] = Convert.ToString(ds.Tables[0].Rows[i]["SRID"]);
                //                drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                //                drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                //                drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                //                drow["MotherName"] = Convert.ToString(ds.Tables[0].Rows[i]["MotherName"]);
                //                drow["Year"] = yr;
                //                drow["MSStatus"] = FindInfo.findMarkSheetStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]), Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), yr, ddlistExam.SelectedItem.Value);
                //                drow["MSCounter"] = Convert.ToInt32(ds.Tables[0].Rows[i]["MSCounter"]);


                //                int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                //                if ((pos > -1))
                //                {
                //                    drow["Detained"] = "Yes";
                //                }
                //                else
                //                {
                //                    drow["Detained"] = "No";
                //                }

                //                drow["VECID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["VECID"]);
                //                dt.Rows.Add(drow);

                //                count = count + 1;

                //            }
                //        }
                //    }


                //}
                //else
                //{
                if (Convert.ToInt32(tbFrom.Text) > 0 && Convert.ToInt32(tbTo.Text) <= ds.Tables[0].Rows.Count)
                {
                    for (int i = (Convert.ToInt32(tbFrom.Text) - 1); i < (Convert.ToInt32(tbTo.Text)); i++)
                    {

                        if (!(dt.AsEnumerable().Any(row => ds.Tables[0].Rows[i]["SRID"].ToString() == row.Field<String>("SRID"))))
                        {

                            string year = "";

                            year = FindInfo.findAllExamYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), ddlistExam.SelectedItem.Value, "R");


                            if (year != "NF" && year != "0" && year != "")
                            {
                                DataRow drow = dt.NewRow();
                                int yr = 0;
                                if (year.Length == 1)
                                {
                                    yr = Convert.ToInt32(year);

                                    drow["Printed"] = findMSPrintedByYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), yr);
                                }
                                else if (year.Length == 3)
                                {
                                    yr = Convert.ToInt32(year.Substring(2, 1));

                                    drow["Printed"] = findMSPrintedByYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), yr);
                                }
                                else if (year.Length == 5)
                                {
                                    yr = Convert.ToInt32(year.Substring(4, 1));
                                    drow["Printed"] = findMSPrintedByYear(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), yr);
                                }
                                drow["SNo"] = Convert.ToString(count);
                                drow["SRID"] = Convert.ToString(ds.Tables[0].Rows[i]["SRID"]);
                                drow["EnrollmentNo"] = Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]);
                                drow["StudentName"] = Convert.ToString(ds.Tables[0].Rows[i]["StudentName"]);
                                drow["FatherName"] = Convert.ToString(ds.Tables[0].Rows[i]["FatherName"]);
                                drow["MotherName"] = Convert.ToString(ds.Tables[0].Rows[i]["MotherName"]);
                                drow["Year"] = yr;
                                drow["MSStatus"] = FindInfo.findMarkSheetStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["SRID"]), Convert.ToString(ds.Tables[0].Rows[i]["EnrollmentNo"]), Convert.ToInt32(ds.Tables[0].Rows[i]["Course"]), Convert.ToInt32(drow["Year"]), ddlistExam.SelectedItem.Value);
                                drow["MSCounter"] = Convert.ToInt32(ds.Tables[0].Rows[i]["MSCounter"]);
                                if (ds.Tables[0].Rows[i]["MSPrinted"].ToString() == "True")
                                {
                                    drow["Printed"] = "Yes";
                                }
                                else
                                {
                                    drow["Printed"] = "No";
                                }

                                int pos = Array.IndexOf(detained, ds.Tables[0].Rows[i]["SRID"].ToString());

                                if ((pos > -1))
                                {
                                    drow["Detained"] = "Yes";
                                }
                                else
                                {
                                    drow["Detained"] = "No";
                                }
                                drow["VECID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["VECID"]);
                                dt.Rows.Add(drow);
                                count = count + 1;

                            }
                        }
                    }


                }
                else
                {
                    dtlistShowStudents.Visible = false;
                    pnlTaskbar.Visible = false;
                    btnPublish.Visible = false;

                    lblMSG.Text = "Sorry !! Invalid Range";
                    pnlMSG.Visible = true;
                }


                //}

            }

         

            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();



            if (count > 1)
            {
                lblTotal.Text = "Total MS: " + (count-1).ToString();
               

                dtlistShowStudents.Visible = true;
                pnlTaskbar.Visible = true;
                btnPublish.Visible = true;
                pnlMSG.Visible = false;


            }

            else
            {
                dtlistShowStudents.Visible = false;
                pnlTaskbar.Visible = false;
                btnPublish.Visible = false;

                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }
        }

        private string findMSPrintedByYear(int srid, int year)
        {
            string msprinted = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select MSPrinted from DDEExamRecord_"+ddlistExam.SelectedItem.Value+" where SRID='" + srid + "' and Year='"+year+"'", con);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[0].ToString() == "True")
                {
                    msprinted = "Yes";
                }
                else
                {
                    msprinted = "No";
                }
            }
            con.Close();

            return msprinted;

        }
     
        protected void btnPublish_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SRID");
            DataColumn dtcol2 = new DataColumn("Year");
           
            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);

            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");
                Label status = (Label)dli.FindControl("lblMSStatus");
                Label srid = (Label)dli.FindControl("lblSRID");
                Label year = (Label)dli.FindControl("lblYear");

                if (cb.Checked == true)
                {
                    DataRow drow = dt.NewRow();
                    drow["SRID"] = Convert.ToInt32(srid.Text);
                    drow["Year"] = Convert.ToInt32(year.Text);
                   

                    dt.Rows.Add(drow);
                   
                   
                    if (ddlistExam.SelectedItem.Value == "A13")
                    {
                        Exam.updateMSPrintRecord_A13(Convert.ToInt32(srid.Text));
                    }
                }

            }

            Session["Students"] = dt;
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["ExamName"] = ddlistExam.SelectedItem.Text;
            Session["MOE"] = ddlistMOE.SelectedItem.Value;
            if (ddlistExam.SelectedItem.Value == "A13")
            {
                Response.Redirect("PublishMarkSheetBySC.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14")
            {
                Response.Redirect("PublishMarkSheetBySC1.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15")
            {
                Response.Redirect("PublishMarkSheetBySC2.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18")
            {
                Response.Redirect("PublishMarkSheetBySC3.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
            {
                Response.Redirect("PublishMarkSheetBySC5.aspx");
            }
        }

        protected void ddlistSC_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPublish.Visible = false;
            pnlTaskbar.Visible = false;
            dtlistShowStudents.Visible = false;

            //if (ddlistSC.SelectedItem.Text == "001" || ddlistSC.SelectedItem.Text == "999" || ddlistSC.SelectedItem.Text == "998" || ddlistSC.SelectedItem.Text == "997" || ddlistSC.SelectedItem.Text == "996" || ddlistSC.SelectedItem.Text == "995" || ddlistSC.SelectedItem.Text == "994" || ddlistSC.SelectedItem.Text == "417" || ddlistSC.SelectedItem.Text == "084" || ddlistSC.SelectedItem.Text == "081" || ddlistSC.SelectedItem.Text == "029" || ddlistSC.SelectedItem.Text == "150" || ddlistSC.SelectedItem.Text == "449" || ddlistSC.SelectedItem.Text == "261" || ddlistSC.SelectedItem.Text == "440")
            //{
                pnlRange.Visible = true;
                lblTotalMS.Text = "(of " + findTotalMS(ddlistSC.SelectedItem.Text) + ")";
            //}
            //else
            //{
            //    pnlRange.Visible = false;
            //}
        }

        private string findTotalMS(string sccode)
        {
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand();

           
            cmd.CommandText = "Select distinct DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEStudentRecord.FatherName,DDEStudentRecord.MotherName,DDEStudentRecord.Course,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".Year,DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MSPrinted from  DDEExamRecord_" + ddlistExam.SelectedItem.Value + " inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".SRID where DDEExamRecord_" + ddlistExam.SelectedItem.Value + ".MOE='R' and ((DDEStudentRecord.SCStatus='O' or DDEStudentRecord.SCStatus='C') and (DDEStudentRecord.StudyCentreCode='" + ddlistSC.SelectedItem.Value + "')) or (DDEStudentRecord.PreviousSCCode='" + ddlistSC.SelectedItem.Value + "' and DDEStudentRecord.SCStatus='T') and DDEStudentRecord.RecordStatus='True' order by EnrollmentNo";
           
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds.Tables[0].Rows.Count.ToString();
        }

        protected void btnPublish1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SRID");
            DataColumn dtcol2 = new DataColumn("Year");

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);

            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");
                Label status = (Label)dli.FindControl("lblMSStatus");
                Label srid = (Label)dli.FindControl("lblSRID");
                Label year = (Label)dli.FindControl("lblYear");

                if (cb.Checked == true)
                {
                    DataRow drow = dt.NewRow();
                    drow["SRID"] = Convert.ToInt32(srid.Text);
                    drow["Year"] = Convert.ToInt32(year.Text);


                    dt.Rows.Add(drow);


                    if (ddlistExam.SelectedItem.Value == "A13")
                    {
                        Exam.updateMSPrintRecord_A13(Convert.ToInt32(srid.Text));
                    }
                }

            }

            Session["Students"] = dt;
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["ExamName"] = ddlistExam.SelectedItem.Text;
            Session["MOE"] = ddlistMOE.SelectedItem.Value;

            if (ddlistExam.SelectedItem.Value == "A13")
            {
                Response.Redirect("PublishMarkSheetBySC.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "B13" || ddlistExam.SelectedItem.Value == "A14")
            {
                Response.Redirect("PublishMarkSheetBySC1.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "B14" || ddlistExam.SelectedItem.Value == "A15")
            {
                Response.Redirect("PublishMarkSheetBySC2.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "B15" || ddlistExam.SelectedItem.Value == "A16" || ddlistExam.SelectedItem.Value == "B16" || ddlistExam.SelectedItem.Value == "A17" || ddlistExam.SelectedItem.Value == "B17" || ddlistExam.SelectedItem.Value == "A18" || ddlistExam.SelectedItem.Value == "B18")
            {
                Response.Redirect("PublishMarkSheetBySC3.aspx");
            }
            else if (ddlistExam.SelectedItem.Value == "W10" || ddlistExam.SelectedItem.Value == "Z10" || ddlistExam.SelectedItem.Value == "W11")
            {
                Response.Redirect("PublishMarkSheetBySC5.aspx");
            }
        }

        protected void ddlistSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");
                Label status = (Label)dli.FindControl("lblMSStatus");
                Label printed = (Label)dli.FindControl("lblPrinted");
                Label detained = (Label)dli.FindControl("lblDetained");
                Label graced = (Label)dli.FindControl("lblGracedStudent");

                if (ddlistSelect.SelectedItem.Text == "TOTAL MS")
                {
                    cb.Checked = true;
                }
                else if (ddlistSelect.SelectedItem.Text == "PENDING MS")
                {
                    if (status.Text == "Pending")
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Enabled = false;
                        cb.Checked = false;
                    }
                }
                else if (ddlistSelect.SelectedItem.Text == "OK MS")
                {
                    if (status.Text == "OK")
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Enabled = false;
                        cb.Checked = false;
                    }

                }
                else if (ddlistSelect.SelectedItem.Text == "PRINTED")
                {
                    if (printed.Text == "Yes")
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Enabled = false;
                        cb.Checked = false;
                    }

                }
                else if (ddlistSelect.SelectedItem.Text == "PENDING PRINT")
                {
                    if (status.Text == "OK" && printed.Text == "No")
                    {
                        cb.Checked = true;
                    }
                    else
                    {
                        cb.Enabled = false;
                        cb.Checked = false;
                    }

                }

                if (printed.Text == "Yes")
                {
                    printed.BackColor = System.Drawing.Color.FromName("#81FD84");
                }
                else if (printed.Text == "No")
                {
                    printed.BackColor = System.Drawing.Color.FromName("#F8A403");

                }

                if (detained.Text == "Yes")
                {
                    cb.Checked = false;
                    cb.Enabled = false;
                    printed.BackColor = System.Drawing.Color.FromName("#B41104");
                }
               

                if (graced.Visible == true)
                {
                    cb.Checked = false;
                    cb.Enabled = false;
                }

            }
        }

        protected void btnPublishList_Click(object sender, EventArgs e)
        {
            string students = "";
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");
                Label srid = (Label)dli.FindControl("lblSRID");

                if (cb.Checked == true)
                {
                    if (students == "")
                    {
                        students = srid.Text;
                    }
                    else
                    {
                        students = students + "," + srid.Text;
                    }
                }

            }
            Session["StudentList"] = students;
            Session["ExamCode"] = ddlistExam.SelectedItem.Value;
            Session["ExamName"] = ddlistExam.SelectedItem.Text;
            Session["MOE"] = ddlistMOE.SelectedItem.Value;
            Response.Redirect("PublishStudentList.aspx");
        }

       
    }
}
