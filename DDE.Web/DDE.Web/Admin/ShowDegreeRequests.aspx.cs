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
    public partial class ShowDegreeRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 85) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 113))
            {
                pnlData.Visible = true;
                pnlMSG.Visible = false;

                if (!IsPostBack)
                {
                    setCurrentDate();
                }
            }
           

            else
            {
                pnlData.Visible = false;
                lblMSG.Text = "Sorry !! You are not authorised for this control";
                pnlMSG.Visible = true;
            }

        }

        private void setCurrentDate()
        {
            ddlistDayFrom.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthFrom.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearFrom.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;

            ddlistDayTo.Items.FindByText(DateTime.Now.ToString("dd")).Selected = true;
            ddlistMonthTo.Items.FindByText(DateTime.Now.ToString("MMMM").ToUpper()).Selected = true;
            ddlistYearTo.Items.FindByText(DateTime.Now.ToString("yyyy")).Selected = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            populateList();
            setColor();
        }

        private void setColor()
        {
            bool auth = false;
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 113))
            {
                auth = true;
            }

            foreach (DataListItem dli in dtlistShowDegreeInfo.Items)
            {
                Button btnpub = (Button)dli.FindControl("btnPublish");
                Button btnedit = (Button)dli.FindControl("btnEdit");
                //Label pub = (Label)dli.FindControl("lblPublished");
                Label cl = (Label)dli.FindControl("lblCL");
                Label rec = (Label)dli.FindControl("lblReceived");
                Label post = (Label)dli.FindControl("lblPosted");
                
                CheckBox select = (CheckBox)dli.FindControl("cbSelect");

                //Image pubY = (Image)dli.FindControl("imgPubY");
                //Image pubN = (Image)dli.FindControl("imgPubN");
                Image recY = (Image)dli.FindControl("imgRecY");
                Image recN = (Image)dli.FindControl("imgRecN");
                Image postY = (Image)dli.FindControl("imgPostY");
                Image postN = (Image)dli.FindControl("imgPostN");

                if (ddlistType.SelectedItem.Value == "1")
                {
                    btnpub.Visible = true;
                }
                else
                {
                    btnpub.Visible = false;
                }

                if (ddlistType.SelectedItem.Value == "2")
                {
                    if (Convert.ToInt32(cl.Text) == 0)
                    {
                        select.Visible = true;
                    }
                    else
                    {
                        select.Visible = false;
                    }
                }
                else
                {
                    select.Visible = false;
                }

                

                //if (pub.Text == "Yes")
                //{
                //    pubY.Visible = true;
                //}
                //else if (pub.Text == "No")
                //{
                //    pubN.Visible = true;
                //}
                if (rec.Text == "Yes")
                {
                    recY.Visible = true;
                }
                else if (rec.Text == "No")
                {
                    recN.Visible = true;
                }
                if (post.Text == "Yes")
                {
                    postY.Visible = true;
                }
                else if (post.Text == "No")
                {
                    postN.Visible = true;
                }
               
                if(auth==true)
                {
                    btnedit.Visible = true;
                }
                else
                {
                    btnedit.Visible = false;
                }
            }

            
        }

        private void populateList()
        {
            string from = ddlistYearFrom.SelectedItem.Text + "-" + ddlistMonthFrom.SelectedItem.Value + "-" + ddlistDayFrom.SelectedItem.Text + " 00:00:01 AM";
            string to = ddlistYearTo.SelectedItem.Text + "-" + ddlistMonthTo.SelectedItem.Value + "-" + ddlistDayTo.SelectedItem.Text + " 11:59:59 PM";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlDataReader dr;

            if (ddlistType.SelectedItem.Value == "1")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEDegreeInfo.LetterPublished,DDEDegreeInfo.CLNo,DDEDegreeInfo.DegreeReceived,DDEDegreeInfo.DegreePosted from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.LetterPublished='False' and DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "2")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEDegreeInfo.LetterPublished,DDEDegreeInfo.CLNo,DDEDegreeInfo.DegreeReceived,DDEDegreeInfo.DegreePosted from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.LetterPublished='True' and DDEDegreeInfo.CLNo='0' and DDEDegreeInfo.LetterPublishedOn>='" + from + "' and DDEDegreeInfo.LetterPublishedOn<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "3")
            {
                cmd.CommandText = "Select  DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEDegreeInfo.LetterPublished,DDEDegreeInfo.CLNo,DDEDegreeInfo.DegreeReceived,DDEDegreeInfo.DegreePosted from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.LetterPublished='True' and DDEDegreeInfo.CLNo!='0' and DDEDegreeInfo.CLPublishedOn>='" + from + "' and DDEDegreeInfo.CLPublishedOn<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "4")
            {
                cmd.CommandText = "Select DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEDegreeInfo.LetterPublished,DDEDegreeInfo.CLNo,DDEDegreeInfo.DegreeReceived,DDEDegreeInfo.DegreePosted from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.DegreeReceived='True' and DDEDegreeInfo.ReceivedOn>='" + from + "' and DDEDegreeInfo.ReceivedOn<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "5")
            {
                cmd.CommandText = "Select DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEDegreeInfo.LetterPublished,DDEDegreeInfo.CLNo,DDEDegreeInfo.DegreeReceived,DDEDegreeInfo.DegreePosted from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.DegreePosted='True' and DDEDegreeInfo.PostedOn>='" + from + "' and DDEDegreeInfo.PostedOn<='" + to + "' order by DDEDegreeInfo.DIID";
            }
            else if (ddlistType.SelectedItem.Value == "0")
            {
                cmd.CommandText = "Select DDEDegreeInfo.DIID,DDEDegreeInfo.SRID,DDEStudentRecord.EnrollmentNo,DDEStudentRecord.StudentName,DDEDegreeInfo.LetterPublished,DDEDegreeInfo.CLNo,DDEDegreeInfo.DegreeReceived,DDEDegreeInfo.DegreePosted from DDEDegreeInfo inner join DDEStudentRecord on DDEStudentRecord.SRID=DDEDegreeInfo.SRID where DDEDegreeInfo.EntryTime>='" + from + "' and DDEDegreeInfo.EntryTime<='" + to + "' order by DDEDegreeInfo.DIID";
            }

            cmd.Connection = con;

            dr = cmd.ExecuteReader();

           

            DataTable dt = new DataTable();

            DataColumn dtcol1 = new DataColumn("SNo");
            DataColumn dtcol2 = new DataColumn("SRID");
            DataColumn dtcol3 = new DataColumn("EnrollmentNo");
            DataColumn dtcol4 = new DataColumn("SName");
            DataColumn dtcol5 = new DataColumn("Published");
            DataColumn dtcol6 = new DataColumn("Posted");
            DataColumn dtcol7 = new DataColumn("Received");          
            DataColumn dtcol8 = new DataColumn("DIID");
            DataColumn dtcol9 = new DataColumn("DLNo");
            DataColumn dtcol10 = new DataColumn("CLNo");

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

            int i = 1;
            while (dr.Read())
            {
                DataRow drow = dt.NewRow();
                drow["SNo"] = i;
                drow["DIID"] = Convert.ToInt32(dr["DIID"]);
                drow["SRID"] = Convert.ToInt32(dr["SRID"]);
                drow["EnrollmentNo"] = Convert.ToString(dr["EnrollmentNo"]);
                drow["SName"] = Convert.ToString(dr["StudentName"]);
                drow["CLNo"] = Convert.ToInt32(dr["CLNo"]);
                if (Convert.ToString(dr["LetterPublished"]) == "True")
                {
                    drow["Published"] = "Yes";
                    drow["DLNo"] = Convert.ToInt32(dr["DIID"]);
                }
                else
                {
                    drow["Published"] = "No";
                    drow["DLNo"] = "0";
                }
                if (Convert.ToString(dr["DegreeReceived"]) == "True")
                {
                    drow["Received"] = "Yes";
                }
                else
                {
                    drow["Received"] = "No";
                }
                if (Convert.ToString(dr["DegreePosted"]) == "True")
                {
                    drow["Posted"] = "Yes";
                }
                else
                {
                    drow["Posted"] = "No";
                }
               

                dt.Rows.Add(drow);
                i = i + 1;
            }

            con.Close();

            if (i > 1)
            {
                dtlistShowDegreeInfo.DataSource = dt;
                dtlistShowDegreeInfo.DataBind();
                dtlistShowDegreeInfo.Visible = true;
                if (ddlistType.SelectedItem.Value == "2")
                {
                    btnPubCoveringLetter.Visible = true;
                }
               
                pnlMSG.Visible = false;
            }
            else
            {
                dtlistShowDegreeInfo.Visible = false;
                btnPubCoveringLetter.Visible = false;
                lblMSG.Text = "Sorry!! No Record Found.";
                pnlMSG.Visible = true;
            }
        }

        protected void dtlistShowDegreeInfo_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label srid = (Label)e.Item.FindControl("lblSRID");
            if (e.CommandName == "Publish")
            {
                Session["DIID"] = e.CommandArgument;
                Session["CF"] = "New";
                Response.Redirect("DegreeLetter.aspx");
            }
            else if (e.CommandName == "Edit")
            {
                //Response.Redirect("Degree.aspx?SRID=" + srid.Text);
                Response.Redirect("RequestForDegree.aspx?DIID="+e.CommandArgument);
            }
        }

        protected void ddlistType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtlistShowDegreeInfo.Visible = false;
            btnPubCoveringLetter.Visible = false;
        }

        protected void btnPubCoveringLetter_Click(object sender, EventArgs e)
        {
            Session["DIIDS"] = findAllSelected();
            Response.Redirect("CoveringLetterDegree.aspx");
        }

        private string findAllSelected()
        {
            string selected = "";

            foreach (DataListItem dli in dtlistShowDegreeInfo.Items)
            {

                Label mid = (Label)dli.FindControl("lblDIID");
                CheckBox cb = (CheckBox)dli.FindControl("cbSelect");


                if (cb.Checked == true)
                {
                    if (selected == "")
                    {
                        selected = mid.Text;
                    }
                    else
                    {
                        selected = selected + "," + mid.Text;
                    }
                }


            }

            return selected;


        }
    }
}