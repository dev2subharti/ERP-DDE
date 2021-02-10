using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DDE.DAL;

namespace DDE.Web.Admin
{
    public partial class FillMarksheet : System.Web.UI.Page
    {

        int totsub = 0;
        int totprac = 0;
        string finalstatus = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 30))
            {
  
                if (!IsPostBack)
                {
                    PopulateCourses();
                    populateSySessions();

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

        private void populateSySessions()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("Select SySession from DDESySession ", con);
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                ddlistSySession.Items.Add(dr["SySession"].ToString());


            }

            con.Close();
        }

        private void PopulateCourses()
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

       
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            

            totsub = getAndPopulateSubjects();
            totprac= getAndPopulatePracticals();
            if (alreadyExist(tbEnrolNo.Text))
            {
                populateMarks(totsub, totprac);
                btnUpdate.Text = "Update";
            }

            else
            {
                clearData();
                btnUpdate.Text = "Submit";
                pnlMarkSheet.Visible = true;
                btnCancel.Visible = true;
                btnUpdate.Visible = true;

            }
           
            lblTotSub.Text = Convert.ToString(totsub);
            lblTotPrac.Text = Convert.ToString(totprac);
           
           
        }

       

        private bool alreadyExist(string eno)
        {
            bool auth = false;

            if (FindInfo.validExamForAugust2011(ddlistExam.SelectedItem.Text, ddlistYear.SelectedItem.Text, findCourse(ddlistCourse.SelectedItem.Value)))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb2"].ToString());
                SqlCommand cmd = new SqlCommand("select * from " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " where Enrollment_No='" + tbEnrolNo.Text + "'", con);
                SqlDataReader dr;

               
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    auth = true;
                }


                con.Close();
            }
            else
            {
               
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                SqlCommand cmd = new SqlCommand("select * from " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " where Enrollment_No='" + tbEnrolNo.Text + "'", con);
                SqlDataReader dr;


                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    auth = true;
                }


                con.Close();
            }
            

            return auth;

            
        }

        private int getAndPopulatePracticals()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select * from DDEPractical  where SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' and CourseName='" + ddlistCourse.SelectedItem.Text + "'and Year='" + ddlistYear.SelectedItem.Text + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("PracticalCode");
            DataColumn dtcol2 = new DataColumn("PracticalName");
            DataColumn dtcol3 = new DataColumn("PracticalMaxMarks");



            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);



            int totalsubject = totsub;

            while (dr.Read())
            {
                totalsubject = totalsubject + 1;
                totprac = totprac + 1;

                DataRow drow = dt.NewRow();
                drow["PracticalCode"] = Convert.ToString(dr["PracticalCode"]);
                drow["PracticalName"] = Convert.ToString(dr["PracticalName"]);
                drow["PracticalMaxMarks"] = Convert.ToString(dr["PracticalMaxMarks"]);

                dt.Rows.Add(drow);


            }



            if (totprac == 0)
            {
                lblCC9.Visible = false;
                lblPrac1.Visible = false;
                lblMaxPracMarks1.Visible = false;
                tbPracMaksObtained1.Visible = false;
                lblGrade9.Visible = false;
                lblStatus9.Visible = false;

                lblCC10.Visible = false;
                lblPrac2.Visible = false;
                lblMaxPracMarks2.Visible = false;
                tbPracMaksObtained2.Visible = false;
                lblGrade10.Visible = false;
                lblStatus10.Visible = false;

            }

           else if (totprac == 1)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC10.Text = dtrw1["PracticalCode"].ToString();
                lblPrac2.Text = dtrw1["PracticalName"].ToString();
                lblMaxPracMarks2.Text = dtrw1["PracticalMaxMarks"].ToString();
                tbPracMaksObtained1.Visible = false;
    
            }

           else if (totprac == 2)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC9.Text = dtrw1["PracticalCode"].ToString();
                lblPrac1.Text = dtrw1["PracticalName"].ToString();
                lblMaxPracMarks1.Text = dtrw1["PracticalMaxMarks"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC10.Text = dtrw2["PracticalCode"].ToString();
                lblPrac2.Text = dtrw2["PracticalName"].ToString();
                lblMaxPracMarks2.Text = dtrw2["PracticalMaxMarks"].ToString();

            }

            con.Close();

            return totprac;

            
        }

        private int getAndPopulateSubjects()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand("select SubjectCode,SubjectName from DDESubject where SyllabusSession='" + ddlistSySession.SelectedItem.Text + "' and CourseName='" + ddlistCourse.SelectedItem.Text + "'and Year='" + ddlistYear.SelectedItem.Text + "'", con);
            SqlDataReader dr;

            con.Open();
            dr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SubjectCode");
            DataColumn dtcol2 = new DataColumn("SubjectName");

            

            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);


            
          
            while (dr.Read())
            {
                totsub = totsub + 1;

                DataRow drow = dt.NewRow();
                drow["SubjectCode"] = Convert.ToString(dr["SubjectCode"]);
                drow["SubjectName"] = Convert.ToString(dr["SubjectName"]);
               
                dt.Rows.Add(drow);
               
             
            }

           

            if (totsub == 4)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


            }

            if (totsub == 5)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


                DataRow dtrw5 = dt.Rows[4];
                lblCC5.Text = dtrw5["SubjectCode"].ToString();
                lblSub5.Text = dtrw5["SubjectName"].ToString();




            }

            if (totsub == 6)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();


                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();


                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


                DataRow dtrw5 = dt.Rows[4];
                lblCC5.Text = dtrw5["SubjectCode"].ToString();
                lblSub5.Text = dtrw5["SubjectName"].ToString();


                DataRow dtrw6 = dt.Rows[5];
                lblCC6.Text = dtrw6["SubjectCode"].ToString();
                lblSub6.Text = dtrw6["SubjectName"].ToString();



            }

            else if (totsub == 7)
            {

            DataRow dtrw1 = dt.Rows[0];
            lblCC1.Text = dtrw1["SubjectCode"].ToString();
            lblSub1.Text = dtrw1["SubjectName"].ToString();

            DataRow dtrw2 = dt.Rows[1];
            lblCC2.Text = dtrw2["SubjectCode"].ToString();
            lblSub2.Text = dtrw2["SubjectName"].ToString();

            DataRow dtrw3 = dt.Rows[2];
            lblCC3.Text = dtrw3["SubjectCode"].ToString();
            lblSub3.Text = dtrw3["SubjectName"].ToString();


            DataRow dtrw4 = dt.Rows[3];
            lblCC4.Text = dtrw4["SubjectCode"].ToString();
            lblSub4.Text = dtrw4["SubjectName"].ToString();


            DataRow dtrw5 = dt.Rows[4];
            lblCC5.Text = dtrw5["SubjectCode"].ToString();
            lblSub5.Text = dtrw5["SubjectName"].ToString();


            DataRow dtrw6 = dt.Rows[5];
            lblCC6.Text = dtrw6["SubjectCode"].ToString();
            lblSub6.Text = dtrw6["SubjectName"].ToString();


            DataRow dtrw7 = dt.Rows[6];
            lblCC7.Text = dtrw7["SubjectCode"].ToString();
            lblSub7.Text = dtrw7["SubjectName"].ToString();


            }


            else if (totsub == 8)
            {
                DataRow dtrw1 = dt.Rows[0];
                lblCC1.Text = dtrw1["SubjectCode"].ToString();
                lblSub1.Text = dtrw1["SubjectName"].ToString();

                DataRow dtrw2 = dt.Rows[1];
                lblCC2.Text = dtrw2["SubjectCode"].ToString();
                lblSub2.Text = dtrw2["SubjectName"].ToString();

                DataRow dtrw3 = dt.Rows[2];
                lblCC3.Text = dtrw3["SubjectCode"].ToString();
                lblSub3.Text = dtrw3["SubjectName"].ToString();


                DataRow dtrw4 = dt.Rows[3];
                lblCC4.Text = dtrw4["SubjectCode"].ToString();
                lblSub4.Text = dtrw4["SubjectName"].ToString();


                DataRow dtrw5 = dt.Rows[4];
                lblCC5.Text = dtrw5["SubjectCode"].ToString();
                lblSub5.Text = dtrw5["SubjectName"].ToString();


                DataRow dtrw6 = dt.Rows[5];
                lblCC6.Text = dtrw6["SubjectCode"].ToString();
                lblSub6.Text = dtrw6["SubjectName"].ToString();


                DataRow dtrw7 = dt.Rows[6];
                lblCC7.Text = dtrw7["SubjectCode"].ToString();
                lblSub7.Text = dtrw7["SubjectName"].ToString();

                DataRow dtrw8 = dt.Rows[7];
                lblCC8.Text = dtrw8["SubjectCode"].ToString();
                lblSub8.Text = dtrw8["SubjectName"].ToString();

            }

           

            con.Close();

            return totsub;
            
        }

       

        private void populateMarks( int tsub, int tprac)
        {
           try
           {
               

               SqlConnection con;

               if (FindInfo.validExamForAugust2011(ddlistExam.SelectedItem.Text, ddlistYear.SelectedItem.Text, findCourse(ddlistCourse.SelectedItem.Value)))
               {
                   con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb2"].ToString());
               }

               else
               {
                   con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
               }

                SqlCommand cmd = new SqlCommand("select * from " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " where Enrollment_No='" + tbEnrolNo.Text + "'", con);
                SqlDataReader dr;

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //while (dr.Read())
                    //{

                    tbSName.Text = dr["Student_Name"].ToString();
                    tbRNo.Text = dr["Roll_No"].ToString();
                    tbFName.Text = dr["Father_Name"].ToString();
                    tbENo.Text = tbEnrolNo.Text;
                    tbSCCode.Text = dr["SC_Code"].ToString();

                    tbTheory1.Text =getMarksifPresent(dr["a_Theory"].ToString()) ;
                    tbIA1.Text = getMarksifPresent(dr["a_IA"].ToString());
                    tbAW1.Text = getMarksifPresent(dr["a_AW"].ToString());
                    tbTotal1.Text = Convert.ToString(getMarks(dr["a_Theory"].ToString()) + getMarks(dr["a_IA"].ToString()) + getMarks(dr["a_AW"].ToString()));
                    lblGrade1.Text = findGrade(tbTotal1.Text);
                    lblStatus1.Text = findStatus(tbTheory1.Text, tbIA1.Text, tbAW1.Text);


                    tbTheory2.Text = getMarksifPresent(dr["b_Theory"].ToString());
                    tbIA2.Text = getMarksifPresent(dr["b_IA"].ToString());
                    tbAW2.Text = getMarksifPresent(dr["b_AW"].ToString());
                    tbTotal2.Text = Convert.ToString(getMarks(dr["b_Theory"].ToString()) + getMarks(dr["b_IA"].ToString()) + getMarks(dr["b_AW"].ToString()));
                    lblGrade2.Text = findGrade(tbTotal2.Text);
                    lblStatus2.Text = findStatus(tbTheory2.Text, tbIA2.Text, tbAW2.Text);


                    tbTheory3.Text = getMarksifPresent(dr["c_Theory"].ToString());
                    tbIA3.Text = getMarksifPresent(dr["c_IA"].ToString());
                    tbAW3.Text = getMarksifPresent(dr["c_AW"].ToString());
                    tbTotal3.Text = Convert.ToString(getMarks(dr["c_Theory"].ToString()) + getMarks(dr["c_IA"].ToString()) + getMarks(dr["c_AW"].ToString()));
                    lblGrade3.Text = findGrade(tbTotal3.Text);
                    lblStatus3.Text = findStatus(tbTheory3.Text, tbIA3.Text, tbAW3.Text);


                    tbTheory4.Text = getMarksifPresent(dr["d_Theory"].ToString());
                    tbIA4.Text = getMarksifPresent(dr["d_IA"].ToString());
                    tbAW4.Text = getMarksifPresent(dr["d_AW"].ToString());
                    tbTotal4.Text = Convert.ToString(getMarks(dr["d_Theory"].ToString()) + getMarks(dr["d_IA"].ToString()) + getMarks(dr["d_AW"].ToString()));
                    lblGrade4.Text = findGrade(tbTotal4.Text);
                    lblStatus4.Text = findStatus(tbTheory4.Text, tbIA4.Text, tbAW4.Text);


                    if (tsub == 5)
                    {
                        tbTheory5.Text = getMarksifPresent(dr["e_Theory"].ToString());
                        tbIA5.Text = getMarksifPresent(dr["e_IA"].ToString());
                        tbAW5.Text = getMarksifPresent(dr["e_AW"].ToString());
                        tbTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                        lblGrade5.Text = findGrade(tbTotal5.Text);
                        lblStatus5.Text = findStatus(tbTheory5.Text, tbIA5.Text, tbAW5.Text);

                    }


                    else if (tsub == 6)
                    {
                        tbTheory5.Text = getMarksifPresent(dr["e_Theory"].ToString());
                        tbIA5.Text = getMarksifPresent(dr["e_IA"].ToString());
                        tbAW5.Text = getMarksifPresent(dr["e_AW"].ToString());
                        tbTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                        lblGrade5.Text = findGrade(tbTotal5.Text);
                        lblStatus5.Text = findStatus(tbTheory5.Text, tbIA5.Text, tbAW5.Text);


                        tbTheory6.Text = getMarksifPresent(dr["f_Theory"].ToString());
                        tbIA6.Text = getMarksifPresent(dr["f_IA"].ToString());
                        tbAW6.Text = getMarksifPresent(dr["f_AW"].ToString());
                        tbTotal6.Text = Convert.ToString(getMarks(dr["f_Theory"].ToString()) + getMarks(dr["f_IA"].ToString()) + getMarks(dr["f_AW"].ToString()));
                        lblGrade6.Text = findGrade(tbTotal6.Text);
                        lblStatus6.Text = findStatus(tbTheory6.Text, tbIA6.Text, tbAW6.Text);
                    }




                    if (tsub == 7)
                    {
                        tbTheory5.Text = getMarksifPresent(dr["e_Theory"].ToString());
                        tbIA5.Text =getMarksifPresent( dr["e_IA"].ToString());
                        tbAW5.Text =getMarksifPresent( dr["e_AW"].ToString());
                        tbTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                        lblGrade5.Text = findGrade(tbTotal5.Text);
                        lblStatus5.Text = findStatus(tbTheory5.Text, tbIA5.Text, tbAW5.Text);


                        tbTheory6.Text =getMarksifPresent( dr["f_Theory"].ToString());
                        tbIA6.Text = getMarksifPresent(dr["f_IA"].ToString());
                        tbAW6.Text =getMarksifPresent( dr["f_AW"].ToString());
                        tbTotal6.Text = Convert.ToString(getMarks(dr["f_Theory"].ToString()) + getMarks(dr["f_IA"].ToString()) + getMarks(dr["f_AW"].ToString()));
                        lblGrade6.Text = findGrade(tbTotal6.Text);
                        lblStatus6.Text = findStatus(tbTheory6.Text, tbIA6.Text, tbAW6.Text);


                        tbTheory7.Text =getMarksifPresent( dr["g_Theory"].ToString());
                        tbIA7.Text = getMarksifPresent(dr["g_IA"].ToString());
                        tbAW7.Text = getMarksifPresent(dr["g_AW"].ToString());
                        tbTotal7.Text = Convert.ToString(getMarks(dr["g_Theory"].ToString()) + getMarks(dr["g_IA"].ToString()) + getMarks(dr["g_AW"].ToString()));
                        lblGrade7.Text = findGrade(tbTotal7.Text);
                        lblStatus7.Text = findStatus(tbTheory7.Text, tbIA7.Text, tbAW7.Text);


                        pnlMM7.Visible = true;
                        pnlMO7.Visible = true;


                    }

                    else if (tsub == 8)
                    {
                        tbTheory5.Text= getMarksifPresent( dr["e_Theory"].ToString());
                        tbIA5.Text = getMarksifPresent(dr["e_IA"].ToString());
                        tbAW5.Text =getMarksifPresent( dr["e_AW"].ToString());
                        tbTotal5.Text = Convert.ToString(getMarks(dr["e_Theory"].ToString()) + getMarks(dr["e_IA"].ToString()) + getMarks(dr["e_AW"].ToString()));
                        lblGrade5.Text = findGrade(tbTotal5.Text);
                        lblStatus5.Text = findStatus(tbTheory5.Text, tbIA5.Text, tbAW5.Text);


                        tbTheory6.Text =getMarksifPresent( dr["f_Theory"].ToString());
                        tbIA6.Text = getMarksifPresent(dr["f_IA"].ToString());
                        tbAW6.Text = getMarksifPresent(dr["f_AW"].ToString());
                        tbTotal6.Text = Convert.ToString(getMarks(dr["f_Theory"].ToString()) + getMarks(dr["f_IA"].ToString()) + getMarks(dr["f_AW"].ToString()));
                        lblGrade6.Text = findGrade(tbTotal6.Text);
                        lblStatus6.Text = findStatus(tbTheory6.Text, tbIA6.Text, tbAW6.Text);


                        tbTheory7.Text = getMarksifPresent(dr["g_Theory"].ToString());
                        tbIA7.Text = getMarksifPresent(dr["g_IA"].ToString());
                        tbAW7.Text =getMarksifPresent( dr["g_AW"].ToString());
                        tbTotal7.Text = Convert.ToString(getMarks(dr["g_Theory"].ToString()) + getMarks(dr["g_IA"].ToString()) + getMarks(dr["g_AW"].ToString()));
                        lblGrade7.Text = findGrade(tbTotal7.Text);
                        lblStatus7.Text = findStatus(tbTheory7.Text, tbIA7.Text, tbAW7.Text);


                        tbTheory8.Text =getMarksifPresent( dr["h_Theory"].ToString());
                        tbIA8.Text =getMarksifPresent( dr["h_IA"].ToString());
                        tbAW8.Text =getMarksifPresent( dr["h_AW"].ToString());
                        tbTotal8.Text = Convert.ToString(getMarks(dr["h_Theory"].ToString()) + getMarks(dr["h_IA"].ToString()) + getMarks(dr["h_AW"].ToString()));
                        lblGrade8.Text = findGrade(tbTotal8.Text);
                        lblStatus8.Text = findStatus(tbTheory8.Text, tbIA8.Text, tbAW8.Text);


                        pnlMM7.Visible = true;
                        pnlMO7.Visible = true;

                        pnlMM8.Visible = true;
                        pnlMO8.Visible = true;


                    }

                    if (tprac == 1)
                    {
                        tbPracMaksObtained1.Visible = false;
                        tbPracMaksObtained2.Text = getMarksifPresent(dr["Prac_Marks1"].ToString());
                        lblGrade10.Text = findPracGrade(tbPracMaksObtained2.Text, lblMaxPracMarks2.Text);
                        lblStatus10.Text = findPracStatus(tbPracMaksObtained2.Text, lblMaxPracMarks2.Text);

                    }

                    else if (tprac == 2)
                    {
                        tbPracMaksObtained1.Text =getMarksifPresent( dr["Prac_Marks1"].ToString());
                        tbPracMaksObtained2.Text =getMarksifPresent( dr["Prac_Marks2"].ToString());

                        lblGrade9.Text = findPracGrade(tbPracMaksObtained1.Text, lblMaxPracMarks1.Text);
                        lblStatus9.Text = findPracStatus(tbPracMaksObtained1.Text, lblMaxPracMarks1.Text);

                        lblGrade10.Text = findPracGrade(tbPracMaksObtained2.Text, lblMaxPracMarks2.Text);
                        lblStatus10.Text = findPracStatus(tbPracMaksObtained2.Text, lblMaxPracMarks2.Text);

                    }


                   lblGTMMarks.Text = Convert.ToString(tsub * 100 + getMarks(lblMaxPracMarks1.Text) + getMarks(lblMaxPracMarks2.Text));
                   tbGrandTotal.Text = Convert.ToString(getMarks(tbTotal1.Text) + getMarks(tbTotal2.Text) + getMarks(tbTotal3.Text) + getMarks(tbTotal4.Text) + getMarks(tbTotal5.Text) + getMarks(tbTotal6.Text) + getMarks(tbTotal7.Text) + getMarks(tbTotal8.Text) + getMarks(tbPracMaksObtained1.Text) + getMarks(tbPracMaksObtained2.Text));

                   lblGrade11.Text= findPracGrade(tbGrandTotal.Text, lblGTMMarks.Text);
                   lblStatus11.Text = findFinalStatus();

                    // }
                    pnlMarkSheet.Visible = true;
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                    btnPubMar.Visible = true;
                    pnlMSG.Visible = false;
                }

                else
                {
                    pnlMarkSheet.Visible = false;
                    btnUpdate.Visible = false;
                    btnCancel.Visible = false;
                    btnPubMar.Visible = false;
                    lblMSG.Text = "Sorry !! No Record Found";
                    pnlMSG.Visible = true;
                }

                con.Close();
           }
           catch
           {
               pnlMarkSheet.Visible = false;
               btnUpdate.Visible = false;
               btnCancel.Visible = false;
               btnPubMar.Visible = false;
               lblMSG.Text = "Sorry !! No Record Found";
               pnlMSG.Visible = true;
           }
        }


        private string findFinalStatus()
        {
            if (lblStatus1.Text == "NC" || lblStatus2.Text == "NC" || lblStatus3.Text == "NC" || lblStatus4.Text == "NC" || lblStatus4.Text == "NC" || lblStatus5.Text == "NC" || lblStatus6.Text == "NC" || lblStatus7.Text == "NC" || lblStatus8.Text == "NC" || lblStatus9.Text == "NC" || lblStatus10.Text == "NC")
            {
                return "NC";

            }

            else
            {
                return "CC";
            }

        }

        private string getMarksifPresent(string marks)
        {
            if (marks == "")
            {
                return "AB";
            }
            else if (marks == "A")
            {
                return "AB";
            }

            else return marks;
           
        }

        private Int32 getMarks(string marks)
        {
            if (marks == "" )
            {
                return 0;
            }

            else if (marks == "A")
            {
                return 0;
            }
            else if (marks == "AB")
            {
                return 0;
            }

            else return Convert.ToInt32(marks);
           
        }

        protected void btnPubMar_Click(object sender, EventArgs e)
        {
            Session["CourseID"] = ddlistCourse.SelectedItem.Value;
            Session["CourseName"] = ddlistCourse.SelectedItem.Text;
            Session["CourseYear"] = ddlistYear.SelectedItem.Value;
            Session["CourseYearAlpha"] = ddlistYear.SelectedItem.Text;
            Session["Exam"] = ddlistExam.SelectedItem.Text;
            Session["EnrollmentNo"] = tbENo.Text;
            Session["SySession"] = ddlistSySession.SelectedItem.Text;
            //Log.createLogNow("Marksheet Publishing", "Marksheet Published of a student of Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
            Response.Redirect("PublishMarksheet.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            totsub = Convert.ToInt32(lblTotSub.Text);
            totprac = Convert.ToInt32(lblTotPrac.Text);


            if (btnUpdate.Text == "Update")
            {

                SqlConnection con;

                if (FindInfo.validExamForAugust2011(ddlistExam.SelectedItem.Text, ddlistYear.SelectedItem.Text, findCourse(ddlistCourse.SelectedItem.Value)))
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb2"].ToString());
                }

                else
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                }




                SqlCommand cmd = new SqlCommand();

                if (totsub == 4 && totprac == 0)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }

                else if (totsub == 4 && totprac == 1)
                {

                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,Prac_Marks1=@Prac_Marks1,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";
                }

                else if (totsub == 4 && totprac == 2)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,Prac_Marks1=@Prac_Marks1,Prac_Marks2=@Prac_Marks2,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }

                else if (totsub == 5 && totprac == 0)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }

                else if (totsub == 5 && totprac == 1)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,Prac_Marks1=@Prac_Marks1,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }

                else if (totsub == 5 && totprac == 2)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,Prac_Marks1=@Prac_Marks1,Prac_Marks2=@Prac_Marks2,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }

                else if (totsub == 6 && totprac == 0)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }
                else if (totsub == 6 && totprac == 1)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,Prac_Marks1=@Prac_Marks1,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }

                else if (totsub == 6 && totprac == 2)
                {

                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,Prac_Marks1=@Prac_Marks1,Prac_Marks2=@Prac_Marks2,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";
                }

                else if (totsub == 7 && totprac == 0)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,g_Theory=@g_Theory,g_IA=@g_IA,g_AW=@g_AW,g_Total=@g_Total,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }
                else if (totsub == 7 && totprac == 1)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,g_Theory=@g_Theory,g_IA=@g_IA,g_AW=@g_AW,g_Total=@g_Total,Prac_Marks1=@Prac_Marks1,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }
                else if (totsub == 7 && totprac == 2)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,g_Theory=@g_Theory,g_IA=@g_IA,g_AW=@g_AW,g_Total=@g_Total,Prac_Marks1=@Prac_Marks1,Prac_Marks2=@Prac_Marks2,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }


                else if (totsub == 8 && totprac == 0)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,g_Theory=@g_Theory,g_IA=@g_IA,g_AW=@g_AW,g_Total=@g_Total,h_Theory=@h_Theory,h_IA=@h_IA,h_AW=@h_AW,h_Total=@h_Total,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }
                else if (totsub == 8 && totprac == 1)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,g_Theory=@g_Theory,g_IA=@g_IA,g_AW=@g_AW,g_Total=@g_Total,h_Theory=@h_Theory,h_IA=@h_IA,h_AW=@h_AW,h_Total=@h_Total,Prac_Marks1=@Prac_Marks1,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }
                else if (totsub == 8 && totprac == 2)
                {
                    cmd.CommandText = "update " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " set Student_Name=@Student_Name,Roll_No=@Roll_No,Father_Name=@Father_Name,Enrollment_No=@Enrollment_No,SC_Code=@SC_Code,a_Theory=@a_Theory,a_IA=@a_IA,a_AW=@a_AW,a_Total=@a_Total,b_Theory=@b_Theory,b_IA=@b_IA,b_AW=@b_AW,b_Total=@b_Total,c_Theory=@c_Theory,c_IA=@c_IA,c_AW=@c_AW,c_Total=@c_Total,d_Theory=@d_Theory,d_IA=@d_IA,d_AW=@d_AW,d_Total=@d_Total,e_Theory=@e_Theory,e_IA=@e_IA,e_AW=@e_AW,e_Total=@e_Total,f_Theory=@f_Theory,f_IA=@f_IA,f_AW=@f_AW,f_Total=@f_Total,g_Theory=@g_Theory,g_IA=@g_IA,g_AW=@g_AW,g_Total=@g_Total,h_Theory=@h_Theory,h_IA=@h_IA,h_AW=@h_AW,h_Total=@h_Total,Prac_Marks1=@Prac_Marks1,Prac_Marks2=@Prac_Marks2,Grand_Total=@Grand_Total where Enrollment_No='" + tbEnrolNo.Text + "'";

                }



                cmd.Parameters.AddWithValue("@SC_Code", tbSCCode.Text);
                cmd.Parameters.AddWithValue("@Roll_No", tbRNo.Text);
                cmd.Parameters.AddWithValue("@Enrollment_No", tbEnrolNo.Text);
                cmd.Parameters.AddWithValue("@Student_Name", tbSName.Text);
                cmd.Parameters.AddWithValue("@Father_Name", tbFName.Text);



                cmd.Parameters.AddWithValue("@a_Theory", tbTheory1.Text);
                cmd.Parameters.AddWithValue("@a_IA", tbIA1.Text);
                cmd.Parameters.AddWithValue("@a_AW", tbAW1.Text);
                cmd.Parameters.AddWithValue("@a_Total", tbTotal1.Text = Convert.ToString(getMarks(tbTheory1.Text) + getMarks(tbIA1.Text) + getMarks(tbAW1.Text)));

                cmd.Parameters.AddWithValue("@b_Theory", tbTheory2.Text);
                cmd.Parameters.AddWithValue("@b_IA", tbIA2.Text);
                cmd.Parameters.AddWithValue("@b_AW", tbAW2.Text);
                cmd.Parameters.AddWithValue("@b_Total", tbTotal2.Text = Convert.ToString(getMarks(tbTheory2.Text) + getMarks(tbIA2.Text) + getMarks(tbAW2.Text)));

                cmd.Parameters.AddWithValue("@c_Theory", tbTheory3.Text);
                cmd.Parameters.AddWithValue("@c_IA", tbIA3.Text);
                cmd.Parameters.AddWithValue("@c_AW", tbAW3.Text);
                cmd.Parameters.AddWithValue("@c_Total", tbTotal3.Text = Convert.ToString(getMarks(tbTheory3.Text) + getMarks(tbIA3.Text) + getMarks(tbAW3.Text)));

                cmd.Parameters.AddWithValue("@d_Theory", tbTheory4.Text);
                cmd.Parameters.AddWithValue("@d_IA", tbIA4.Text);
                cmd.Parameters.AddWithValue("@d_AW", tbAW4.Text);
                cmd.Parameters.AddWithValue("@d_Total", tbTotal4.Text = Convert.ToString(getMarks(tbTheory4.Text) + getMarks(tbIA4.Text) + getMarks(tbAW4.Text)));

                cmd.Parameters.AddWithValue("@e_Theory", tbTheory5.Text);
                cmd.Parameters.AddWithValue("@e_IA", tbIA5.Text);
                cmd.Parameters.AddWithValue("@e_AW", tbAW5.Text);
                cmd.Parameters.AddWithValue("@e_Total", tbTotal5.Text = Convert.ToString(getMarks(tbTheory5.Text) + getMarks(tbIA5.Text) + getMarks(tbAW5.Text)));

                cmd.Parameters.AddWithValue("@f_Theory", tbTheory6.Text);
                cmd.Parameters.AddWithValue("@f_IA", tbIA6.Text);
                cmd.Parameters.AddWithValue("@f_AW", tbAW6.Text);
                cmd.Parameters.AddWithValue("@f_Total", tbTotal6.Text = Convert.ToString(getMarks(tbTheory6.Text) + getMarks(tbIA6.Text) + getMarks(tbAW6.Text)));

                cmd.Parameters.AddWithValue("@g_Theory", tbTheory7.Text);
                cmd.Parameters.AddWithValue("@g_IA", tbIA7.Text);
                cmd.Parameters.AddWithValue("@g_AW", tbAW7.Text);
                cmd.Parameters.AddWithValue("@g_Total", tbTotal7.Text = Convert.ToString(getMarks(tbTheory7.Text) + getMarks(tbIA7.Text) + getMarks(tbAW7.Text)));

                cmd.Parameters.AddWithValue("@h_Theory", tbTheory8.Text);
                cmd.Parameters.AddWithValue("@h_IA", tbIA8.Text);
                cmd.Parameters.AddWithValue("@h_AW", tbAW8.Text);
                cmd.Parameters.AddWithValue("@h_Total", tbTotal8.Text = Convert.ToString(getMarks(tbTheory8.Text) + getMarks(tbIA8.Text) + getMarks(tbAW8.Text)));

                if (totprac == 1)
                {
                    cmd.Parameters.AddWithValue("@Prac_Marks1", tbPracMaksObtained2.Text);
                }

                else if (totprac == 2)
                {
                    cmd.Parameters.AddWithValue("@Prac_Marks1", tbPracMaksObtained1.Text);
                    cmd.Parameters.AddWithValue("@Prac_Marks2", tbPracMaksObtained2.Text);

                }


                cmd.Parameters.AddWithValue("@Grand_Total", Convert.ToString(getMarks(tbTotal1.Text) + getMarks(tbTotal2.Text) + getMarks(tbTotal3.Text) + getMarks(tbTotal4.Text) + getMarks(tbTotal5.Text) + getMarks(tbTotal6.Text) + getMarks(tbTotal7.Text) + getMarks(tbTotal8.Text) + getMarks(tbPracMaksObtained1.Text) + getMarks(tbPracMaksObtained2.Text)));

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Marks Updation", "Marks updated of a student with Enrollment No '" + tbENo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                populateMarks(totsub, totprac);
            }

           else  if (btnUpdate.Text == "Submit")
            {

                SqlConnection con;

                if (FindInfo.validExamForAugust2011(ddlistExam.SelectedItem.Text, ddlistYear.SelectedItem.Text, findCourse(ddlistCourse.SelectedItem.Value)))
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb2"].ToString());
                }
   
                else
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                }

                SqlCommand cmd = new SqlCommand();

                if (totsub == 4 && totprac == 0)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@Grand_Total)";

                }

                else if (totsub == 4 && totprac == 1)
                {

                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@Prac_Marks1,@Grand_Total)";
                }

                else if (totsub == 4 && totprac == 2)
                {

                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@Prac_Marks1,@Prac_Marks2,@Grand_Total)";
                }

                else if (totsub == 5 && totprac == 0)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@Grand_Total)";
                }

                else if (totsub == 5 && totprac == 1)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@Prac_Marks1,@Grand_Total)";
                }

                else if (totsub == 5 && totprac == 2)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@Prac_Marks1,@Prac_Marks2,@Grand_Total)";

                }
                else if (totsub == 6 && totprac == 0)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@Grand_Total)";
                }
                else if (totsub == 6 && totprac == 1)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@Prac_Marks1,@Grand_Total)";
                }

                else if (totsub == 6 && totprac == 2)
                {

                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@Prac_Marks1,@Prac_Marks2,@Grand_Total)";
                }

                else if (totsub == 7 && totprac == 0)
                {

                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@g_Theory,@g_IA,@g_AW,@g_Total,@Grand_Total)";
                }
                else if (totsub == 7 && totprac == 1)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@g_Theory,@g_IA,@g_AW,@g_Total,@Prac_Marks1,@Grand_Total)";
                }
                else if (totsub == 7 && totprac == 2)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@g_Theory,@g_IA,@g_AW,@g_Total,@Prac_Marks1,@Prac_Marks2,@Grand_Total)";

                }
                else if (totsub == 8 && totprac == 0)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@g_Theory,@g_IA,@g_AW,@g_Total,@h_Theory,@h_IA,@h_AW,@h_Total,@Grand_Total)";

                }
                else if (totsub == 8 && totprac == 1)
                {
                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@g_Theory,@g_IA,@g_AW,@g_Total,@h_Theory,@h_IA,@h_AW,@h_Total,@Prac_Marks1,@Grand_Total)";
                }
                else if (totsub == 8 && totprac == 2)
                {

                    cmd.CommandText = "insert into " + findCourse(ddlistCourse.SelectedItem.Value) + "_" + ddlistYear.SelectedItem.Value + " values(@S_No,@SC_Code,@Roll_No,@Enrollment_No,@Student_Name,@Father_Name,@a_Theory,@a_IA,@a_AW,@a_Total,@b_Theory,@b_IA,@b_AW,@b_Total,@c_Theory,@c_IA,@c_AW,@c_Total,@d_Theory,@d_IA,@d_AW,@d_Total,@e_Theory,@e_IA,@e_AW,@e_Total,@f_Theory,@f_IA,@f_AW,@f_Total,@g_Theory,@g_IA,@g_AW,@g_Total,@h_Theory,@h_IA,@h_AW,@h_Total,@Prac_Marks1,@Prac_Marks2,@Grand_Total)";
                }


                cmd.Parameters.AddWithValue("@S_No", "");
                cmd.Parameters.AddWithValue("@SC_Code", tbSCCode.Text);
                cmd.Parameters.AddWithValue("@Roll_No", tbRNo.Text);
                cmd.Parameters.AddWithValue("@Enrollment_No", tbEnrolNo.Text);
                cmd.Parameters.AddWithValue("@Student_Name", tbSName.Text);
                cmd.Parameters.AddWithValue("@Father_Name", tbFName.Text);
              
                cmd.Parameters.AddWithValue("@a_Theory", tbTheory1.Text);
                cmd.Parameters.AddWithValue("@a_IA", tbIA1.Text);
                cmd.Parameters.AddWithValue("@a_AW", tbAW1.Text);
                cmd.Parameters.AddWithValue("@a_Total", tbTotal1.Text = Convert.ToString(getMarks(tbTheory1.Text) + getMarks(tbIA1.Text) + getMarks(tbAW1.Text)));

                cmd.Parameters.AddWithValue("@b_Theory", tbTheory2.Text);
                cmd.Parameters.AddWithValue("@b_IA", tbIA2.Text);
                cmd.Parameters.AddWithValue("@b_AW", tbAW2.Text);
                cmd.Parameters.AddWithValue("@b_Total", tbTotal2.Text = Convert.ToString(getMarks(tbTheory2.Text) + getMarks(tbIA2.Text) + getMarks(tbAW2.Text)));

                cmd.Parameters.AddWithValue("@c_Theory", tbTheory3.Text);
                cmd.Parameters.AddWithValue("@c_IA", tbIA3.Text);
                cmd.Parameters.AddWithValue("@c_AW", tbAW3.Text);
                cmd.Parameters.AddWithValue("@c_Total", tbTotal3.Text = Convert.ToString(getMarks(tbTheory3.Text) + getMarks(tbIA3.Text) + getMarks(tbAW3.Text)));

                cmd.Parameters.AddWithValue("@d_Theory", tbTheory4.Text);
                cmd.Parameters.AddWithValue("@d_IA", tbIA4.Text);
                cmd.Parameters.AddWithValue("@d_AW", tbAW4.Text);
                cmd.Parameters.AddWithValue("@d_Total", tbTotal4.Text = Convert.ToString(getMarks(tbTheory4.Text) + getMarks(tbIA4.Text) + getMarks(tbAW4.Text)));

                cmd.Parameters.AddWithValue("@e_Theory", tbTheory5.Text);
                cmd.Parameters.AddWithValue("@e_IA", tbIA5.Text);
                cmd.Parameters.AddWithValue("@e_AW", tbAW5.Text);
                cmd.Parameters.AddWithValue("@e_Total", tbTotal5.Text = Convert.ToString(getMarks(tbTheory5.Text) + getMarks(tbIA5.Text) + getMarks(tbAW5.Text)));

                cmd.Parameters.AddWithValue("@f_Theory", tbTheory6.Text);
                cmd.Parameters.AddWithValue("@f_IA", tbIA6.Text);
                cmd.Parameters.AddWithValue("@f_AW", tbAW6.Text);
                cmd.Parameters.AddWithValue("@f_Total", tbTotal6.Text = Convert.ToString(getMarks(tbTheory6.Text) + getMarks(tbIA6.Text) + getMarks(tbAW6.Text)));

                cmd.Parameters.AddWithValue("@g_Theory", tbTheory7.Text);
                cmd.Parameters.AddWithValue("@g_IA", tbIA7.Text);
                cmd.Parameters.AddWithValue("@g_AW", tbAW7.Text);
                cmd.Parameters.AddWithValue("@g_Total", tbTotal7.Text = Convert.ToString(getMarks(tbTheory7.Text) + getMarks(tbIA7.Text) + getMarks(tbAW7.Text)));

                cmd.Parameters.AddWithValue("@h_Theory", tbTheory8.Text);
                cmd.Parameters.AddWithValue("@h_IA", tbIA8.Text);
                cmd.Parameters.AddWithValue("@h_AW", tbAW8.Text);
                cmd.Parameters.AddWithValue("@h_Total", tbTotal8.Text = Convert.ToString(getMarks(tbTheory8.Text) + getMarks(tbIA8.Text) + getMarks(tbAW8.Text)));

                if (totprac == 1)
                {
                    cmd.Parameters.AddWithValue("@Prac_Marks1", tbPracMaksObtained2.Text);
                }

                else if (totprac == 2)
                {
                    cmd.Parameters.AddWithValue("@Prac_Marks1", tbPracMaksObtained1.Text);
                    cmd.Parameters.AddWithValue("@Prac_Marks2", tbPracMaksObtained2.Text);

                }


                cmd.Parameters.AddWithValue("@Grand_Total", Convert.ToString(getMarks(tbTotal1.Text) + getMarks(tbTotal2.Text) + getMarks(tbTotal3.Text) + getMarks(tbTotal4.Text) + getMarks(tbTotal5.Text) + getMarks(tbTotal6.Text) + getMarks(tbTotal7.Text) + getMarks(tbTotal8.Text) + getMarks(tbPracMaksObtained1.Text) + getMarks(tbPracMaksObtained2.Text)));

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Log.createLogNow("Marks Entered", "Marks Entered of a student with Enrollment No '" + tbEnrolNo.Text + "'", Convert.ToInt32(Session["ERID"].ToString()));
                pnlMarkSheet.Visible = false;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;
                lblMSG.Text = "Marks has been submitted successfully";
                pnlMSG.Visible = true;
            }
                   

          

        }

        private string findCourse(string courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select CourseShortName,Specialization from DDECourse where CourseID='" + courseid + "'", con);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            string course = "";

            while (dr.Read())
            {
                if (dr[1].ToString() == "")
                {

                    course = dr[0].ToString();
                }

                else
                {

                    course = dr[0].ToString() + "_" + findSpecialisation(dr[1].ToString());
                }

            }


            con.Close();

            return course;



        }

        private string findSpecialisation(string sp)
        {
            string oldstr = sp.Substring(0, 1);
            string newstr = oldstr;
            int i = 1;
            while (oldstr != "")
            {
                try
                {
                    oldstr = sp.Substring(i, 1);
                    newstr = newstr + findchar(oldstr);
                    i++;
                }
                catch
                {
                    return newstr;
                }


            }

            return newstr;

        }

        private string findchar(string oldstr)
        {
            if (oldstr == "&")
            {
                return "_";
            }

            else if (oldstr == ".")
            {
                return "";
            }
            else if (oldstr == " ")
            {
                return "";
            }

            else return oldstr;
        }

        private string findCourseShortName(string courseid)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());

            SqlCommand cmd = new SqlCommand("Select CourseShortName from DDECourse where CourseID='" + courseid + "'", con);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();

            string course = "";

            while (dr.Read())
            {
                course = dr[0].ToString();



            }
            con.Close();

            return course;

        }



       

        private string findStatus(string tee, string ia, string aw)
        {

            string status = "";

            int teepercent = (getMarks(tee) * 100) / 60;
            int iapercent = (getMarks(ia) * 100) / 20;
            int awpercent = (getMarks(aw) * 100) / 20;

            if (teepercent < 40 || iapercent < 40 || awpercent < 40)
            {
                finalstatus = status = "NC";
                
            }

            else
            {
                finalstatus = status = "CC";
            }

            return status;


            
        }


        private string findPracStatus(string pracmarksobtained, string maxpracmarks)
        {
            string status = "";


            int pracpercent = (getMarks(pracmarksobtained) * 100 /getMarks(maxpracmarks));

            if (pracpercent < 40)
            {
                finalstatus = status = "NC";
            }

            else
            {
                finalstatus = status = "CC";
            }

            return status;


        }



        private string findGrade(string total)
        {
            string grade="";

            int percent = (getMarks(total) * 100) / 100;

            if(percent>=85)
            {
                grade = "A++";
            }

            else if (percent < 85 && percent >= 75)
            {
                grade = "A+";
            }

            else if (percent < 75 && percent >= 60)
            {
                grade = "A";
            }
            else if (percent < 60 && percent >= 50)
            {
                grade = "B";
            }

            else if (percent < 50 && percent >= 40)
            {
                grade = "C";
            }

            else if (percent < 40)
            {
                grade = "D";
            }

            return grade;
           
        }

        private string findPracGrade(string pracmarksobtained, string maxpracmarks)
        {
            string grade = "";

            int percent = (getMarks(pracmarksobtained) * 100) / getMarks(maxpracmarks);

            if (percent >= 85)
            {
                grade = "A++";
            }

            else if (percent < 85 && percent >= 75)
            {
                grade = "A+";
            }

            else if (percent < 75 && percent >= 60)
            {
                grade = "A";
            }
            else if (percent < 60 && percent >= 50)
            {
                grade = "B";
            }

            else if (percent < 50 && percent >= 40)
            {
                grade = "C";
            }

            else if (percent < 40)
            {
                grade = "D";
            }

            return grade;

        }


        private void clearData()
        {
            tbFName.Text = "";
            tbSCCode.Text = "";
            tbSName.Text = "";
            tbENo.Text = "";
            tbRNo.Text = "";
            tbTheory1.Text = "";
            tbTheory2.Text = "";
            tbTheory3.Text = "";
            tbTheory4.Text = "";
            tbTheory5.Text = ""; 
            tbTheory6.Text = "";
            tbTheory7.Text = "";
            tbTheory8.Text = "";
            tbIA1.Text = "";
            tbIA2.Text = "";
            tbIA3.Text = "";
            tbIA4.Text = "";
            tbIA5.Text = "";
            tbIA6.Text = "";
            tbIA7.Text = "";
            tbIA8.Text = "";
            tbAW1.Text = "";
            tbAW2.Text = "";
            tbAW3.Text = "";
            tbAW4.Text = "";
            tbAW5.Text = "";
            tbAW6.Text = "";
            tbAW7.Text = "";
            tbAW8.Text = "";
            tbTotal1.Text = "";
            tbTotal2.Text = "";
            tbTotal3.Text = "";
            tbTotal4.Text = "";
            tbTotal5.Text = "";
            tbTotal6.Text = "";
            tbTotal7.Text = "";
            tbTotal8.Text = "";
            tbPracMaksObtained1.Text = "";
            tbPracMaksObtained2.Text = "";
            tbGrandTotal.Text = "";
            lblGrade1.Text = "";
            lblGrade2.Text = "";
            lblGrade3.Text = "";
            lblGrade4.Text = "";
            lblGrade5.Text = "";
            lblGrade6.Text = "";
            lblGrade7.Text = "";
            lblGrade8.Text = "";
            lblGrade9.Text = "";
            lblGrade10.Text = "";
            lblGrade11.Text = "";
            lblStatus1.Text = "";
            lblStatus2.Text = "";
            lblStatus3.Text = "";
            lblStatus4.Text = "";
            lblStatus5.Text = "";
            lblStatus6.Text = "";
            lblStatus7.Text = "";
            lblStatus8.Text = "";
            lblStatus9.Text = "";
            lblStatus10.Text = "";
            lblStatus11.Text = "";
          
        }

       
        
    }
}
