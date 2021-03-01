using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using QRCoder;

using System.IO;
using System.Drawing;

namespace DDE.Web.Admin
{
    public partial class ProvisionalDegree : System.Web.UI.Page
    {
        StringBuilder SB = new StringBuilder(90000000);
        StringBuilder SB1 = new StringBuilder(90000000);
        StringBuilder SB2 = new StringBuilder(90000000);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 129))
            {
                if (!IsPostBack)
                {
                    SB.Length = 0;
                    SB.Append("<center>");
                    printRecord();
                    PanelReport.Controls.Add(new LiteralControl(SB.ToString()));
                    SB.Append("</center>");
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

        void printRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CSddedb"].ToString());


            if (con.State == ConnectionState.Closed)
                con.Open();

            String sqlProvDegree = "select z.*,a.CourseFullName,b.EnrollmentNo,b.StudentName,b.FatherName,b.gender,b.StudentPhoto from ddeProvisionalDegree z";
            sqlProvDegree += " inner join DDECourse a on a.CourseID=z.courseid";
            sqlProvDegree += " inner join DDEStudentRecord b on b.srid=z.srid";
            sqlProvDegree += " where z.srid=" + Session["srid"].ToString();

            SqlCommand cmdProDegree = new SqlCommand();
            cmdProDegree = new SqlCommand(sqlProvDegree, con);
            cmdProDegree.CommandTimeout = 0;
            SqlDataAdapter daProDegree = new SqlDataAdapter(cmdProDegree);
            DataSet dsProDegree = new DataSet();
            daProDegree.Fill(dsProDegree);

            if (dsProDegree.Tables[0].Rows.Count > 0)
            { }

            String varServer = string.Empty;
            SqlDataAdapter adp = new SqlDataAdapter("select getdate()", con);
            if (adp.SelectCommand.ExecuteScalar() != null)
                varServer = adp.SelectCommand.ExecuteScalar().ToString();

            Int32 varTblWidth = 813;
            SB.Append("<div>");

            SB.Append("<table width =" + varTblWidth + ">");
            SB.Append("<tr>");
            SB.Append("<td width = 120 valign = top style = 'border:none;border-bottom:solid windowtext 2.25pt;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<img width = 107 height = 100 id = 'Picture 0' src='../Images/logo.jpg'>");
            SB.Append("</td > ");

            SB.Append("<td align = center width = 625 colspan = 2 valign = top style = 'border:none;border-bottom:solid windowtext 2.25pt;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<span style = 'font-size:18.0pt;font-family:'Times New Roman''>Swami Vivekanand Subharti University </span>");
            SB.Append("<br /><b><span style = 'font-size:18.0pt;font-family:'Cambria''>Directorate of Distance Education </span></b>");
            SB.Append("<br /><span style = 'font-size:10.0pt;font-family:'Times New Roman''>NH - 58, Subhartipuram, Delhi Haridwar Bypass Road,</span>");

            SB.Append("<br /><span style = 'font-size:10.0pt;font-family:'Times New Roman''>Meerut – 250005(U.P.) INDIA </span>");
            SB.Append("</td>");

            SB.Append("<td width = 120align=right valign = top style = 'border:none;border-bottom:solid windowtext 2.25pt;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<img width = 127 height = 100 id = 'Picture 0' src='../Images/naac.jpg'>");
            SB.Append("</td > ");
            SB.Append("</tr>");
            SB.Append("</table");
            SB.Append("</div>");

            SB.Append("<div>");
            SB.Append("<table width =" + varTblWidth + ">");
            SB.Append("<tr>");
            SB.Append("<td valign = middle height = 70px align = center width = " + varTblWidth + " valign = top colspan=4 style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<b><span style = 'font-size:8.0pt;font-family:'Times New Roman''>&nbsp;</span></b>");
            SB.Append("<b><span style = 'font-size:18.0pt;font-family:'Times New Roman''>PROVISIONAL CERTIFICATE</span></b>");
            SB.Append("<b><span style = 'font-size:4.0pt;font-family:'Times New Roman''>&nbsp;</span></b>");
            SB.Append("</td>");
            SB.Append("</tr>");
            SB.Append("</table");
            SB.Append("</div>");

            SB.Append("<div>");

            SB.Append("<table width =" + varTblWidth + ">");
            SB.Append("<tr>");

            SB.Append("<td colspan=2 valign = bottom height = 50px align = left width = 288 valign = top style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<span style = 'font-size:12.0pt;line-height:150%;font-family:'Cambria''>Certificate Serial Number: </span>");
            SB.Append("</td>");

            SB.Append("<td valign = bottom height = 50px align = left width = 900 colspan=2 valign = top style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<span style = 'font-size:12.0pt;line-height:150%;font-family:'Cambria''>" + dsProDegree.Tables[0].Rows[0]["SRLNO"].ToString() + "</span>");
            SB.Append("</td>");

            byte[] bytes = (byte[])dsProDegree.Tables[0].Rows[0]["studentphoto"];
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

            SB.Append("<td rowspan=2 align=right width=625 colspan=2>");
            SB.Append("<img src='data:image/jpg;base64," + base64String + "' />");
            SB.Append("</td>");
            SB.Append("</tr>");

            SB.Append("<tr>");
            SB.Append("<td valign = middle height = 50px align = left width = 288 valign = top style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<span style = 'font-size:12.0pt;line-height:150%;font-family:'Cambria''>Enrolment Number: </span>");
            SB.Append("</td>");

            SB.Append("<td valign = middle height = 50px align = left width = 900 colspan=2 valign = top style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<span style = 'font-size:12.0pt;line-height:150%;font-family:'Cambria''>" + dsProDegree.Tables[0].Rows[0]["enrollmentno"].ToString() + "</span>");
            SB.Append("</td>");
            SB.Append("</tr>");
            SB.Append("</table");
            SB.Append("</div>");

            SB.Append("<div>");
            SB.Append("<table width =" + varTblWidth + ">");
            //SB.Append("<br /><br />");
            SB.Append("<tr>");
            //SB.Append("<td valign = top style='background-image: url(images/ddelogo5.png); width:100 %; background-repeat: no-repeat;border:none;padding:0in 5.4pt 0in 5.4pt;line-height:200%;font-size:14.0pt;' colspan=4 align=justify heighy=100px>");
            SB.Append("<td valign = top style='border:none;padding:0in 5.4pt 0in 5.4pt;line-height:200%;font-size:14.0pt;' colspan=4 align=justify heighy=100px>");
            SB.Append("<span style = 'text-align:justify;font-size:14.0pt;font-family:'Cambria''>");
            SB.Append("This is certify that ");
            if (dsProDegree.Tables[0].Rows[0]["gender"].ToString().ToUpper() == "MALE".ToUpper())
                SB.Append("<b>Mr.</b>");
            else
                SB.Append("<b>Ms.</b>");

            SB.Append("<b>" + dsProDegree.Tables[0].Rows[0]["studentname"].ToString());

            if (dsProDegree.Tables[0].Rows[0]["gender"].ToString().ToUpper() == "MALE".ToUpper())
                SB.Append(" S/O ");
            else
                SB.Append(" D/O ");

            SB.Append(" Mr." + dsProDegree.Tables[0].Rows[0]["fathername"].ToString() + "</b> is/was a student of <b>" + dsProDegree.Tables[0].Rows[0]["CourseFullName"].ToString() + "</b> programme");
            SB.Append(" through Distance mode.");

            if (dsProDegree.Tables[0].Rows[0]["gender"].ToString().ToUpper() == "MALE".ToUpper())
                SB.Append(" His ");
            else
                SB.Append(" Her ");

            SB.Append(" date of programme completion is <b>" + Convert.ToDateTime(dsProDegree.Tables[0].Rows[0]["CompletionDT"].ToString()).ToString("dd-MM-yyyy") + ".</b></span>");

            if (dsProDegree.Tables[0].Rows[0]["gender"].ToString().ToUpper() == "MALE".ToUpper())
                SB.Append(" He ");
            else
                SB.Append(" She ");

            SB.Append("appeared / passed / failed in the Examination  <b>" + dsProDegree.Tables[0].Rows[0]["exam"].ToString() + "</b> having Roll Number <b>" + dsProDegree.Tables[0].Rows[0]["rollno"].ToString() + "</b>");
            SB.Append(" Securing <b>" + dsProDegree.Tables[0].Rows[0]["obtain"].ToString() + "</b> marks out of Grand Total <b>" + dsProDegree.Tables[0].Rows[0]["mm"].ToString() + "</b> with the grade <b>'" + dsProDegree.Tables[0].Rows[0]["grade"].ToString() + "'</b></span>");
            SB.Append("</td>");
            SB.Append("</tr>");

            SB.Append("<br /><br /><br /><br />");
            SB.Append("<tr style = 'height:70pt'>");
            SB.Append("<td valign = bottom style = 'border:none;padding:0in 5.4pt 0in 5.4pt;'>");
            SB.Append("<span style = 'font-size:12.0pt;font-family:'Cambria''><b>Note:</b></span>");
            SB.Append("<br /><span style = 'font-size:11.0pt;line-height:150%;font-family:'Cambria''>This provisional certified is valid till the issuance of Original Final Degree certificate</span>");
            SB.Append("<br /><br /><span style = 'font-size:12.0pt;line-height:150%;font-family:'Cambria''>Date of Issue:" + Convert.ToDateTime(varServer.ToString()).ToString("dd-MM-yyyy") + "</span>");
            SB.Append("</td>");
            SB.Append("</tr>");
            SB.Append("</table>");

            SB.Append("<table width =" + varTblWidth + ">");
            SB.Append("<tr>");
            SB.Append("<br /><br /><br /><br />");
            SB.Append("<td width=200px valign = bottom style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<span style = 'font-size:4.0pt;font-family:'Cambria''> &nbsp;</span>");
            SB.Append("<span style = 'font-size:12.0pt;font-family:'Cambria''><b> Prepared By:</b></span>");
            SB.Append("</td>");

            string code = "Enrolment No:" + dsProDegree.Tables[0].Rows[0]["enrollmentno"].ToString() + ", " + System.Environment.NewLine;
            code += " Programe: " + dsProDegree.Tables[0].Rows[0]["CourseFullName"].ToString() + ", " + System.Environment.NewLine;
            code += " Examination: " + dsProDegree.Tables[0].Rows[0]["exam"].ToString();

            qrGenerator(code);

            SB.Append("<td align = right colspan = 2 valign = top style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<img height='100px' width='100px' src='data:image/jpg;base64," + Convert.ToBase64String(byteImage) + "' />");
            SB.Append("</td>");

            SB.Append("<td align = right colspan = 2 valign = bottom style = 'border:none;padding:0in 5.4pt 0in 5.4pt'>");
            SB.Append("<span style = 'font-size:4.0pt;font-family:'Cambria''> &nbsp;</span>");
            SB.Append("<span style = 'font-size:12.0pt;font-family:'Cambria''><b>Signing Authority</span>");
            SB.Append("</td>");
            SB.Append("</tr>");

            SB.Append("<td width = 188 style = 'border:none'></td>");
            SB.Append("<td width = 99 style = 'border:none'></td>");
            SB.Append("</tr>");
            SB.Append("</table>");
            SB.Append("</div>");
        }

        System.Web.UI.WebControls.Image imgBarCode;
        byte[] byteImage;
        void qrGenerator(string varRn)
        {
            string code = varRn;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 150;
            imgBarCode.Width = 150;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                Session.Add(varRn, byteImage);
                Session["rn"] = varRn;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}