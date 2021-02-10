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
    public partial class CoveringLetterDegree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85))
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["CLDID"] == null)
                    {
                        lblRefNo.Visible = false;
                        lblDate.Visible = false;
                        btnPrint.Visible = true;
                    }
                    else if (Request.QueryString["CLDID"] != null)
                    {
                        string pubdate = Convert.ToDateTime(FindInfo.findCLDegreePubDate(Convert.ToInt32(Request.QueryString["CLDID"]))).ToString("dd/MM/yyyy");
                        lblRefNo.Text = "Ref.No. DDE/SVSU/" + pubdate.Substring(6, 4) + "/CLD/" + Request.QueryString["CLDID"];
                        lblDate.Text = "Date : " + pubdate;
                        lblRefNo.Visible = true;
                        lblDate.Visible = true;
                        btnPrint.Visible = false;
                    }

                    populateRecord();
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

        private void populateRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            if (Request.QueryString["CLDID"] == null)
            {
                cmd.CommandText= "select DDEStudentRecord.Studentname,DDEStudentRecord.EnrollmentNo,DDEDegreeInfo.DIID,DDEDegreeInfo.SRID from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.DIID in (" + Convert.ToString(Session["DIIDS"]) + ")";
            }
            else if (Request.QueryString["CLDID"] != null)
            {
                cmd.CommandText = "select DDEStudentRecord.Studentname,DDEStudentRecord.EnrollmentNo,DDEDegreeInfo.DIID,DDEDegreeInfo.SRID from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.CLNo='" + Request.QueryString["CLDID"] + "'";
            }
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("DIID");
            DataColumn dtcol3 = new DataColumn("SRID");
            DataColumn dtcol4 = new DataColumn("StudentName");
            DataColumn dtcol5 = new DataColumn("ENo");


            dt.Columns.Add(dtcol1);
            dt.Columns.Add(dtcol2);
            dt.Columns.Add(dtcol3);
            dt.Columns.Add(dtcol4);
            dt.Columns.Add(dtcol5);

            cmd.Connection = con;
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow drow = dt.NewRow();
                    drow["SNo"] = i + 1;
                    drow["DIID"] = (ds.Tables[0].Rows[i]["DIID"]).ToString();
                    drow["SRID"] = (ds.Tables[0].Rows[i]["SRID"]).ToString();
                    drow["StudentName"] = (ds.Tables[0].Rows[i]["StudentName"]).ToString();
                    drow["ENo"] = (ds.Tables[0].Rows[i]["EnrollmentNo"]).ToString();

                    dt.Rows.Add(drow);

                }

            }

            dtlistSLM.DataSource = dt;
            dtlistSLM.DataBind();



        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (btnPrint.Visible == true)
            {

                if (Request.QueryString["CLDID"] != null)
                {
                    lblRefNo.Visible = true;
                    lblDate.Visible = true;
                    btnPrint.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                }
                else
                {

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
                    SqlCommand cmd = new SqlCommand("insert into DDECoveringLettersDegree OUTPUT INSERTED.CLDID values(@DIIDS,@PublishedOn)", con);

                    cmd.Parameters.AddWithValue("@DIIDS", Session["DIIDS"].ToString());
                    cmd.Parameters.AddWithValue("@PublishedOn", DateTime.Now.ToString());

                    cmd.Connection = con;
                    con.Open();
                    object lid = cmd.ExecuteScalar();
                    con.Close();



                    SqlCommand cmd1 = new SqlCommand("update DDEDegreeInfo set CLPublished=@CLPublished,CLPublishedOn=@CLPublishedOn,CLNo=@CLNo where DIID in (" + Session["DIIDS"].ToString() + ")", con);

                    cmd1.Parameters.AddWithValue("@CLPublished", "True");
                    cmd1.Parameters.AddWithValue("@CLPublishedOn", DateTime.Now.ToString());
                    cmd1.Parameters.AddWithValue("@CLNo", Convert.ToInt32(lid));

                    con.Open();
                    object res = cmd1.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToInt32(res) > 0)
                    {

                        Log.createLogNow("Degree Info", "Covering Letter of Degree printed with No : " + lid + ".", Convert.ToInt32(Session["ERID"].ToString()));

                        btnPrint.Visible = false;


                        //lblPNo.Text = "P-1";
                        //lblPNo.Visible = true;
                        lblRefNo.Text = "Ref.No. DDE/SVSU/" + DateTime.Now.ToString("yyyy") + "/CLD/" + lid.ToString();
                        lblDate.Text = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");
                        lblRefNo.Visible = true;
                        lblDate.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
                    }
                    else
                    {

                    }

                }

            }

        }




        protected void btnNo_Click(object sender, EventArgs e)
        {
            //pnlData.Visible = true;
            //pnlMSG.Visible = false;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            //pnlData.Visible = true;
            //pnlMSG.Visible = false;
            //btnPrint.Visible = false;
            //lblRefNo.Visible = true;
            //lblDate.Visible = true;



            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            //SqlCommand cmd = new SqlCommand("update DDEMigrationInfo set LetterPublished=@LetterPublished,LetterPublishedOn=@LetterPublishedOn where MID='" + lblMID.Text + "')", con);

            //cmd.Parameters.AddWithValue("@LetterPublished", "True");
            //cmd.Parameters.AddWithValue("@LetterPublishedOn", DateTime.Now.ToString());

            //con.Open();
            //object res = cmd.ExecuteNonQuery();
            //con.Close();



            //if (Convert.ToInt32(res) == 1)
            //{

            //    Log.createLogNow("Verification Letter Printed", "Instrument Verification Letter printed with No : " + lblLNo.Text + "with times : " + (Convert.ToInt32(Session["printcounter"]) + 1).ToString(), Convert.ToInt32(Session["ERID"].ToString()));


            //    lblPNo.Text = "P-" + (Convert.ToInt32(Session["printcounter"]) + 1).ToString();
            //    lblPNo.Visible = true;
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
            //}





        }
    }
}