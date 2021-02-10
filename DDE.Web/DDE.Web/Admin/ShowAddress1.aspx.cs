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
    public partial class ShowAddress1 : System.Web.UI.Page
    {
        StringBuilder SB = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 118) || Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 119))
            {
                if (!IsPostBack)
                {
                    printAddress();
                }
            }
        }

        void printAddress()
        {
            String sqlDegree = "select a.EnrollmentNo,a.StudentName,a.FatherName,z.MailingAddress,z.PinCode,z.MobileNo,z.sno from DDEDegreeInfo z";
            sqlDegree += " inner join DDEStudentRecord a on a.SRID = z.SRID";
            sqlDegree += " where a.srid in (" + Session["Srid"].ToString() + "0)";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());
            SqlCommand cmd = new SqlCommand(sqlDegree, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsAddress = new DataSet();
            da.Fill(dsAddress);

            if (dsAddress.Tables[0].Rows.Count > 0)
            {
                SB.Length = 0;
                SB.Append("<center>");

                SB.Append("<div>");
                SB.Append("<table>");

                Int32 varPg = -1;
                for (int i = 0; i <= dsAddress.Tables[0].Rows.Count - 1; i++)
                {
                    varPg++;
                    if (varPg > 1)
                    {
                        varPg = 0;
                        SB.Append("</table>");
                        SB.Append("</div>");

                        SB.Append("<p style='page-break-before:always;'></p>");

                        SB.Append("<div>");
                        SB.Append("<table>");
                    }
                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>" + dsAddress.Tables[0].Rows[i]["StudentName"].ToString() + " </td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>" + dsAddress.Tables[0].Rows[i]["FatherName"].ToString() + "</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>" + dsAddress.Tables[0].Rows[i]["MailingAddress"].ToString() + "</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>Pin- " + dsAddress.Tables[0].Rows[i]["PinCode"].ToString() + "</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>Mobile: " + dsAddress.Tables[0].Rows[i]["MobileNo"].ToString() + "</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>Enrolment No." + dsAddress.Tables[0].Rows[i]["EnrollmentNo"].ToString() + "</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>Degree No.:" + dsAddress.Tables[0].Rows[i]["sno"].ToString() + "</td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>&nbsp;</td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("</tr>");

                    SB.Append("<tr>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'>  ");
                    SB.Append("<br /><br /><br /><i><u>If Undelivered Please Return It back TO:</u></i>  ");
                    SB.Append("<br />");

                    SB.Append("<br /><b>DIRECTORATE OF DISTANCE EDUCATION</b>");
                    SB.Append("<br /><b>SWAMI VIVEKANAND SUBHARTI UNIVERSITY</b>");
                    SB.Append("<br /><b>NH-58,DELHI-HARIDWAR BYPASS ROAD,</b>");
                    SB.Append("<br /><b>MEERUT</b>");
                    SB.Append("<br /><b>PIN - 250005</b><br /><br /><br /><br />");
                    SB.Append("</td> ");
                    SB.Append("</tr>");

                    SB.Append("<tr> ");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("<td width=382 valign=top style='padding:0in 5.4pt 0in 5.4pt'></td>");
                    SB.Append("</tr>");
                }

                SB.Append("</table>");
                SB.Append("</div>");
                PanelReport.Controls.Add(new LiteralControl(SB.ToString()));
                pnlData.Visible = true;
            }
            else
            {
                lblMSG.Text = "No Record Found!!";
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "pr();", true);
            //btnPrint.Visible = false;
        }
    }
}