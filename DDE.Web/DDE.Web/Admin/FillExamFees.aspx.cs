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
    public partial class FillExamFees : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 38))
            {
                if (!IsPostBack)
                {
                    populateExam();
                    populateCourses();
                    populateSessions();
                    Session["SNo"] = 1;
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

        private void populateExam()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select ExamCode,ExamName from DDEExaminations ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistExam.Items.Add(dr["ExamName"].ToString());
                ddlistExam.Items.FindByText(dr["ExamName"].ToString()).Value = dr["ExamCode"].ToString();

            }

            con.Close();
        }


        private void populateSessions()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select Session from DDESession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistSession.Items.Add(dr["Session"].ToString());

            }

            con.Close();

        }



        private void populateCourses()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select CourseID,CourseShortName,Specialization from DDECourse order by CourseShortName", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[2].ToString() == "")
                {
                    ddlistCourse.Items.Add(dr[1].ToString());
                    ddlistCourse.Items.FindByText(dr[1].ToString()).Value = dr[0].ToString();
                }

                else
                {
                    ddlistCourse.Items.Add(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")");
                    ddlistCourse.Items.FindByText(dr[1].ToString() + " " + "(" + dr[2].ToString() + ")").Value = dr[0].ToString();

                }

            }

            con.Close();

        }



        protected void btnFind_Click(object sender, EventArgs e)
        {

            if (ddlistCourse.SelectedItem.Text == "PGDCA")
            {
                ViewState["Index"] = 0;
                Session["SNo"] = 1;
                populateStudents();
                lnkbtnNext.Visible = true;
               
            }

            else
            {
                populateStudents1();
                lnkbtnNext.Visible = false;
                lnkbtnPrevious.Visible = false;
            }
           
            findFee();

        }

        private void populateStudents1()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select SRID,StudyCentreCode,EnrollmentNo,StudentName,FatherName from DDEStudentRecord where Course='" + ddlistCourse.SelectedItem.Value + "' and Session='" + ddlistSession.SelectedItem.Text + "' and RecordStatus='True' order by StudentName ", con);
            con.Open();
            SqlDataReader dr;


            dr = cmd.ExecuteReader();


            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("ExamFee");
            DataColumn dtcol8 = new DataColumn("LateExamFee");
            DataColumn dtcol9 = new DataColumn("ExamCity");
            DataColumn dtcol10 = new DataColumn("ExamJone");
            DataColumn dtcol11 = new DataColumn("FeeFilled");
            DataColumn dtcol12 = new DataColumn("ExamCentreFilled");



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

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["SRID"] = Convert.ToString(dr["SRID"]);
                drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                if (feeFilled(Convert.ToInt32(dr["SRID"])))
                {
                    drow["FeeFilled"] = "True";
                    popuLateExamFee(drow, Convert.ToInt32(dr["SRID"]));
                }

                else
                {
                    drow["FeeFilled"] = "False";
                    drow["ExamFee"] = "";
                    drow["LateExamFee"] = "";

                }

                if (examCentreFilled(Convert.ToInt32(dr["SRID"])))
                {
                    drow["ExamCentreFilled"] = "True";

                }

                else
                {
                    drow["ExamCentreFilled"] = "False";

                }

                popuLateExamCentre(drow, Convert.ToInt32(dr["SRID"]));
                dt.Rows.Add(drow);
                i = i + 1;
            }



            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            if (i > 1)
            {
                dtlistShowStudents.Visible = true;
                btnSubmit.Visible = true;
                btnOK.Visible = false;
                pnlMSG.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }

            con.Close();

            fillSetExamCentres(dt);
        }

        private bool examCentreFilled(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from DDEExamRecord where SRID='" + srid + "'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            bool ecfilled = false;
            if (dr.HasRows)
            {
                ecfilled = true;
            }


            con.Close();

            return ecfilled;
        }

        private void fillSetExamCentres(DataTable dt1)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("Select City from CityList order by City ", con);
            SqlDataReader dr;

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("City");
            dt.Columns.Add(dtcol1);


            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();

                drow["City"] = Convert.ToString(dr["City"]);
                dt.Rows.Add(drow);
            }


            con.Close();

            int j = 0;
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {

                Label srid = (Label)dli.FindControl("lblSRID");
                DropDownList ec = (DropDownList)dli.FindControl("ddlistExamCentre");
                DropDownList jone = (DropDownList)dli.FindControl("ddlistJone");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ec.Items.Add(dt.Rows[i]["City"].ToString());

                }
                ec.Items.Add("NOT AVAILABLE");

                if (dt1.Rows[j]["ExamCity"].ToString() != "")
                {
                    try
                    {
                        ec.Items.FindByText(dt1.Rows[j]["ExamCity"].ToString()).Selected = true;
                    }
                    catch
                    {
                        ec.Items.FindByText("NOT AVAILABLE").Selected = true;
                    }
                }

                if (dt1.Rows[j]["ExamJone"].ToString() != "")
                {

                    jone.Items.FindByText(dt1.Rows[j]["ExamJone"].ToString()).Selected = true;
                }
                j++;
            }


        }



        private void popuLateExamFee(DataRow drow, int srid)
        {
            string colEF = "ExamFee_" + ddlistExam.SelectedItem.Value;
            string colLEF = "LateExamFee_" + ddlistExam.SelectedItem.Value;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + colEF + "," + colLEF + " from DDEFeeRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            dr.Read();
            drow["ExamFee"] = dr[0].ToString();
            drow["LateExamFee"] = dr[1].ToString();
            con.Close();

        }

        private bool feeFilled(int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SRID from DDEFeeRecord where SRID='"+srid+"'", con);
            con.Open();
            dr = cmd.ExecuteReader();

            bool feefilled = false;
            if (dr.HasRows)
            {
                feefilled = true;
            }
                   

            con.Close();

            return feefilled;

        }



        private void populateStudents()
        {

            DataTable pdt = new DataTable();
            DataColumn col = new DataColumn("ColName");
            pdt.Columns.Add(col);

            DataRow drow1 = pdt.NewRow();
            drow1["ColName"] = "SRID";
            pdt.Rows.Add(drow1);




            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("SCCode");
            DataColumn dtcol4 = new DataColumn("EnrollmentNo");
            DataColumn dtcol5 = new DataColumn("StudentName");
            DataColumn dtcol6 = new DataColumn("FatherName");
            DataColumn dtcol7 = new DataColumn("CourseID");
            DataColumn dtcol8 = new DataColumn("Batch");
            DataColumn dtcol9 = new DataColumn("ExamFee");
            DataColumn dtcol10 = new DataColumn("LateExamFee");
            DataColumn dtcol11 = new DataColumn("ExamCity");
            DataColumn dtcol12 = new DataColumn("ExamJone");
            DataColumn dtcol13 = new DataColumn("FeeFilled");
            DataColumn dtcol14 = new DataColumn("ExamCentreFilled");



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
     
            int totalpages;
            int totalrecords;

            DataTable rdt = new DataTable();
            rdt = Paging.SelectStudentsByCB("SP_DDEFeeRecord", pdt, Convert.ToInt32(ViewState["Index"]),ddlistCourse.SelectedItem.Value,ddlistSession.SelectedItem.Text, out totalrecords, out totalpages);
            ViewState["TotalPages"] = totalpages;
            ViewState["TotalRecords"] = totalrecords;

            populateStudentsList(dt, rdt);

            dtlistShowStudents.DataSource = dt;
            dtlistShowStudents.DataBind();

            lblTotalRecords.Text = "Total Records : " + totalrecords.ToString();

            if (Convert.ToInt32(Session["SNo"]) > 1)
            {
                dtlistShowStudents.Visible = true;
                btnSubmit.Visible = true;
                pnlPaging.Visible =true;
                btnOK.Visible = false;
                pnlMSG.Visible = false;
                pnlPaging.Visible = true;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                pnlPaging.Visible = false;
                lblMSG.Text = "Sorry !! No record found";
                pnlMSG.Visible = true;
            }


            fillSetExamCentres(dt);

            PopulatePaging(totalpages);

            if (totalpages <= 1)
            {
                lnkbtnNext.Visible = false;

            }

            if (Convert.ToInt32(Session["SNo"]) == 101)
            {
                lnkbtnPrevious.Visible = false;
            }
        }

        private void populateStudentsList(DataTable dt, DataTable rdt)
        {
            int sno = Convert.ToInt32(Session["SNo"]); 
            int i=1;
            while (i<=rdt.Rows.Count)
            {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = sno;
                    drow["SRID"] = rdt.Rows[i-1]["SRID"].ToString();
                    populateStudentDetail(drow, Convert.ToInt32(drow["SRID"]));

                    if (drow["CourseID"].ToString() == ddlistCourse.SelectedItem.Value)
                    {
                        if (drow["Batch"].ToString() == ddlistSession.SelectedItem.Text)
                        {
                            if (feeFilled(Convert.ToInt32(drow["SRID"])))
                            {
                                drow["FeeFilled"] = "True";
                                popuLateExamFee(drow, Convert.ToInt32(drow["SRID"]));
                            }
                            else
                            {
                                drow["FeeFilled"] = "False";
                                drow["ExamFee"] = "";
                                drow["LateExamFee"] = "";
                            }

                            if (examCentreFilled(Convert.ToInt32(drow["SRID"])))
                            {
                                drow["ExamCentreFilled"] = "True";

                            }

                            else
                            {
                                drow["ExamCentreFilled"] = "False";

                            }

                            popuLateExamCentre(drow, Convert.ToInt32(drow["SRID"]));
                            dt.Rows.Add(drow);
                            sno = sno + 1;
                            i = i + 1;
                        }
                    }
                }

             Session["SNo"] = sno;

            }



        private void populateStudentDetail(DataRow drow, int srid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select StudyCentreCode,EnrollmentNo,StudentName,FatherName,Course,CYear,Session from DDEStudentRecord where SRID='" + srid + "' ", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                drow["SCCode"] = Convert.ToString(dr["StudyCentreCode"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["StudentName"] = Convert.ToString(dr["StudentName"]);
                drow["FatherName"] = Convert.ToString(dr["FatherName"]);
                drow["CourseID"] = Convert.ToInt32(dr["Course"]);
                drow["Batch"] = Convert.ToString(dr["Session"]);
            }
            con.Close();
        }

        private void popuLateExamCentre(DataRow drow, int srid)
        {
            string colECity = "ExamCentreCity_" + ddlistExam.SelectedItem.Value;
            string colEJone = "ExamCentreJone_" + ddlistExam.SelectedItem.Value;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select " + colECity + "," + colEJone + " from DDEExamRecord where SRID='" + srid + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                drow["ExamCity"] = dr[0].ToString();
                drow["ExamJone"] = dr[1].ToString();
            }
            con.Close();


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string colRollNo = "RollNo_" + ddlistExam.SelectedItem.Value;
            string colBPRollNo = "BPRollNo_" + ddlistExam.SelectedItem.Value;
            string colEF = "ExamFee_" + ddlistExam.SelectedItem.Value;
            string colLEF = "LateExamFee_" + ddlistExam.SelectedItem.Value;
            string colBPEF = "BPExamFee_" + ddlistExam.SelectedItem.Value;
            string colECID = "ExamCentreID_" + ddlistExam.SelectedItem.Value;
            string colECity = "ExamCentreCity_" + ddlistExam.SelectedItem.Value;
            string colEJone = "ExamCentreJone_" + ddlistExam.SelectedItem.Value;
            string colSubjects1 = "BPSubjects1_" + ddlistExam.SelectedItem.Value;
            string colSubjects2 = "BPSubjects2_" + ddlistExam.SelectedItem.Value;
            string colSubjects3 = "BPSubjects3_" + ddlistExam.SelectedItem.Value;

            if (validAmount())
            {
                foreach (DataListItem dli in dtlistShowStudents.Items)
                {

                    Label srid = (Label)dli.FindControl("lblSRID");
                    Label eno = (Label)dli.FindControl("lblENo");
                    
                    Label lblff = (Label)dli.FindControl("lblFeeFilled");
                    Label lblecf = (Label)dli.FindControl("lblExamCentreFilled");
                    TextBox ef = (TextBox)dli.FindControl("tbExamFee");
                    TextBox lf = (TextBox)dli.FindControl("tbLateExamFee");
                    Label lef = (Label)dli.FindControl("lblExamFee");
                    Label llf = (Label)dli.FindControl("lblLateExamFee");
                    DropDownList ec = (DropDownList)dli.FindControl("ddlistExamCentre");
                    DropDownList jone = (DropDownList)dli.FindControl("ddlistJone");


                    if (lblff.Text == "False")
                    {


                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("insert into DDEFeeRecord values(@SRID,@" + colEF + ",@" + colLEF + ",@" + colBPEF + ")", con);


                        cmd.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));

                        cmd.Parameters.AddWithValue("@" + colEF, ef.Text);
                        cmd.Parameters.AddWithValue("@" + colLEF, lf.Text);
                        cmd.Parameters.AddWithValue("@" + colBPEF, "");
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        if (ef.Text != "" || lf.Text != "")
                        {

                            Log.createLogNow("Fee Filling", "Filled Fee of a student with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        }
                    }

                    else if (lblff.Text == "True")
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd = new SqlCommand("update DDEFeeRecord set " + colEF + "=@" + colEF + "," + colLEF + "=@" + colLEF + " where SRID='" + srid.Text + "'", con);
                        cmd.Parameters.AddWithValue("@" + colEF, ef.Text);
                        cmd.Parameters.AddWithValue("@" + colLEF, lf.Text);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        if (lef.Text != ef.Text || llf.Text != lf.Text)
                        {
                            Log.createLogNow("Fee Updation", "Updated fee of a student with Enrollment No '" + eno.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                        }

                    }

                    if(lblecf.Text=="False")
                    {


                        int courseid = FindInfo.findCourseIDBySRID(Convert.ToInt32(srid.Text));
                        int counter = FindInfo.findRollNoCounter(courseid,ddlistExam.SelectedItem.Value,"R");
                        int newcounter = counter + 1;
                        string sno = string.Format("{0:0000}", newcounter);
                        string rollno = "A12" + FindInfo.findCourseCodeByID(courseid) + sno;

                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd1 = new SqlCommand("insert into DDEExamRecord values(@SRID,@" + colRollNo + ",@" + colECID + ",@" + colECity + ",@" + colEJone + ",@" + colBPRollNo + ",@" + colSubjects1 + ",@" + colSubjects2 + ",@" + colSubjects3 + ")", con1);

                        cmd1.Parameters.AddWithValue("@SRID", Convert.ToInt32(srid.Text));
                        cmd1.Parameters.AddWithValue("@" + colRollNo, rollno);
                        cmd1.Parameters.AddWithValue("@" + colECID, "");
                        cmd1.Parameters.AddWithValue("@" + colECity, ec.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@" + colEJone, jone.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@" + colBPRollNo,"");
                        cmd1.Parameters.AddWithValue("@" + colSubjects1, "");
                        cmd1.Parameters.AddWithValue("@" + colSubjects2, "");
                        cmd1.Parameters.AddWithValue("@" + colSubjects3, "");

                        cmd1.Connection = con1;
                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();

                        FindInfo.updateRollNoCounter(courseid, newcounter,ddlistExam.SelectedItem.Value);

                       
                     }

                    else if (lblecf.Text == "True")
                    {
                        
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                        SqlCommand cmd1 = new SqlCommand("update DDEExamRecord set " + colECity + "=@" + colECity + "," + colEJone + "=@" + colEJone + " where SRID='" + srid.Text + "'", con1);
                        cmd1.Parameters.AddWithValue("@" + colECity, ec.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@" + colEJone, jone.SelectedItem.Text);


                        con1.Open();
                        cmd1.ExecuteNonQuery();
                        con1.Close();

                       
                    }

                }

                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Fee have been submitted successfully";
                pnlMSG.Visible = true;
                btnOK.Visible = false;
                lnkbtnNext.Visible = false;
                lnkbtnPrevious.Visible = false;
            }

            else
            {
                dtlistShowStudents.Visible = false;
                btnSubmit.Visible = false;
                lblMSG.Text = "Invalid Amount is filled at S.No. " + "' " + Session["SNo"].ToString() + " '" + "</br> Please check exam fee should be " + Session["ExamFee"].ToString() + " and late fee should be " + Session["LateExamFee"].ToString();
                pnlMSG.Visible = true;
                btnOK.Visible = true;
                lnkbtnNext.Visible = false;
                lnkbtnPrevious.Visible = false;

            }

            pnlPaging.Visible = false;
        }


        private void findFee()
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd1 = new SqlCommand("Select FeeAmount from DDEFee where FeeHead='EXAM FEE' ", con);
            SqlCommand cmd2 = new SqlCommand("Select FeeAmount from DDEFee where FeeHead='LATE EXAM FEE' ", con);
            SqlDataReader dr1;
            SqlDataReader dr2;

            con.Open();
            dr1 = cmd1.ExecuteReader();
            dr1.Read();
            Session["ExamFee"] = Convert.ToInt32(dr1[0]);
            con.Close();


            con.Open();
            dr2 = cmd2.ExecuteReader();
            dr2.Read();
            Session["LateExamFee"] = Convert.ToInt32(dr2[0]);
            con.Close();



        }

        private bool validAmount()
        {

            bool validamount = false;
            int SNo = 0;
            foreach (DataListItem dli in dtlistShowStudents.Items)
            {
                try
                {
                    Label sno = (Label)dli.FindControl("lblSNo");
                    Label srid = (Label)dli.FindControl("lblSRID");
                    TextBox ef = (TextBox)dli.FindControl("tbExamFee");
                    TextBox lf = (TextBox)dli.FindControl("tbLateExamFee");

                    SNo = Convert.ToInt32(sno.Text);

                    int examfee;
                    int LateExamFee;


                    if (ef.Text == "")
                    {
                        examfee = 0;
                    }

                    else
                    {
                        examfee = Convert.ToInt32(ef.Text);
                    }





                    if (lf.Text == "")
                    {
                        LateExamFee = 0;
                    }

                    else
                    {
                        LateExamFee = Convert.ToInt32(lf.Text);
                    }


                    if ((examfee == 1000 || examfee == 0) && (LateExamFee == 200 || LateExamFee == 0))
                    {
                        validamount = true;

                    }

                    else
                    {
                        validamount = false;
                        Session["SNo"] = SNo;
                        break;
                    }
                }

                catch
                {
                    validamount = false;
                    Session["SNo"] = SNo;
                    break;
                }


            }

            return validamount;

        }






        protected void ddlistCourse_SelectedIndexChanged(object sender, EventArgs e)
        {


            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
            pnlPaging.Visible = false;
        }



        protected void ddlistSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
            pnlPaging.Visible = false;
        }


        protected void btnOK_Click(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = true;
            btnSubmit.Visible = true;
            pnlPaging.Visible = false;
            pnlMSG.Visible = false;
            btnOK.Visible = false;

        }


        protected void ddlistYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void ddlistExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowStudents.Visible = false;
            btnSubmit.Visible = false;
        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            int totalpages = Convert.ToInt32(ViewState["TotalPages"]);
            ViewState["Index"] = Convert.ToInt32(e.CommandArgument) - 1;

            if (Convert.ToInt32(ViewState["Index"]) != 0)
            {
                Session["SNo"] = (Convert.ToInt32(ViewState["Index"]) * 100)+1;
            }

            else
            {
                Session["SNo"] = 1;
            }

            populateStudents();





            if (Convert.ToInt32(e.CommandArgument) == 1)
            {
                if (totalpages > 1)
                {
                    lnkbtnPrevious.Visible = false;
                    lnkbtnNext.Visible = true;

                }
                else
                {
                    lnkbtnPrevious.Visible = false;
                    lnkbtnNext.Visible = false;

                }

            }
            else if (Convert.ToInt32(e.CommandArgument) == totalpages)
            {
                lnkbtnNext.Visible = false;
                lnkbtnPrevious.Visible = true;

            }
        }

        protected void lnkbtnPrevious_Click(object sender, EventArgs e)
        {
            ViewState["Index"] = Convert.ToInt32(ViewState["Index"]) - 1;

            if (Convert.ToInt32(ViewState["Index"]) != 0)
            {
                Session["SNo"] = (Convert.ToInt32(ViewState["Index"]) * 100) + 1;
            }

            else
            {
                Session["SNo"] = 1;
            }
            populateStudents();
            if (Convert.ToInt32(ViewState["Index"]) > 0)
            {
                lnkbtnNext.Visible = true;

            }
            if (Convert.ToInt32(ViewState["Index"]) == 0)
            {

                lnkbtnPrevious.Visible = false;
                lnkbtnNext.Visible = true;

            }
        }

        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            int totalpages = Convert.ToInt32(ViewState["TotalPages"]);
            if (Convert.ToInt32(ViewState["Index"]) != 0)
            {
                Session["SNo"] = (Convert.ToInt32(ViewState["Index"]) * 100) + 1;
            }

            else
            {
                Session["SNo"] = 1;
            }
            if (totalpages > 1)
            {
                if (Convert.ToInt32(ViewState["TotalPages"]) - 1 > Convert.ToInt32(ViewState["Index"]))
                {
                    ViewState["Index"] = Convert.ToInt32(ViewState["Index"]) + 1;
                }

                populateStudents();

                if (Convert.ToInt32(ViewState["TotalPages"]) > 1)
                {
                    lnkbtnPrevious.Visible = true;
                }
                if (Convert.ToInt32(ViewState["TotalPages"]) - 1 == Convert.ToInt32(ViewState["Index"]))
                {
                    lnkbtnNext.Visible = false;
                    lnkbtnPrevious.Visible = true;
                }
            }
        }

        private void PopulatePaging(int totalpages)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("PageNo", typeof(Int32));
            dt.Columns.Add(dc);
            for (int i = 1; i <= totalpages; i++)
            {
                DataRow dr = dt.NewRow();
                dr["PageNo"] = i;
                dt.Rows.Add(dr);
            }
            rptPaging.DataSource = dt;
            rptPaging.DataBind();
        }
    }
}
