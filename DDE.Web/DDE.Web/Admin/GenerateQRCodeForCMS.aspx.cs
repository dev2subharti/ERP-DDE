using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDE.DAL;
using System.IO;
using QRCoder;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

namespace DDE.Web.Admin
{
    public partial class GenerateQRCodeForCMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authorisation.authorised(Convert.ToInt32(Session["ERID"]), 90))
            {
                pnlSearch.Visible = true;
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

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblMode.SelectedItem.Value == "1")
            {
                btnSearch.Text = "Generate";
            }
            else if (rblMode.SelectedItem.Value == "2")
            {
                btnSearch.Text = "Search";
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (FindInfo.isENoExist(tbID.Text))
            {
                if (rblMode.SelectedItem.Value == "1")
                {
                    if (!(File.Exists(Server.MapPath("CMSQRCodes/" + tbID.Text + ".png"))))
                    {

                        string error;
                        string code = findQRCode(tbID.Text, out error);

                        if (error == "")
                        {

                            QRCodeGenerator qrGenerator = new QRCodeGenerator();

                            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                            
                            using (Bitmap bitMap = qrCode.GetGraphic(20))
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    bitMap.Save(Server.MapPath("CMSQRCodes/" + tbID.Text + ".png"), System.Drawing.Imaging.ImageFormat.Png);

                                    
                                }

                                

                            }

                            imgQRCode.ImageUrl = "CMSQRCodes/" + tbID.Text + ".png";

                            lblOutput.Text = code;
                            pnlQRCode.Visible = true;

                            Log.createLogNow("Generate QR Code","Generated QR Code for Consolidated Mark Sheet of Student with ENo. "+tbID.Text,Convert.ToInt32(Session["ERID"]));
                        }
                        else
                        {

                            pnlQRCode.Visible = false;
                            lblMSG.Text = error;
                            pnlMSG.Visible = true;
                        }
                    }
                    else
                    {

                        pnlQRCode.Visible = false;
                        lblMSG.Text = "Sorry !! QR Code has already generated for this student.<br/>Please 'Search' the QR Code.'";
                        pnlMSG.Visible = true;
                    }
                }
                else if (rblMode.SelectedItem.Value == "2")
                {
                    populateQRCode(tbID.Text);
                }
            }
            else
            {
                pnlQRCode.Visible = false;
                lblMSG.Text = "Sorry !! Invalid Enrollment No.";
                pnlMSG.Visible = true;
            }
        }

        private void populateQRCode(string eno)
        {
            imgQRCode.ImageUrl="CMSQRCodes/"+eno+".png";
            lblOutput.Text = "For verification and more details please follow the link : http://www.subhartidde.com/CMS.aspx?EN=" + eno.ToUpper();
            pnlQRCode.Visible = true;
            pnlMSG.Visible = false;
            tbID.Enabled = false;
        }

        private string findQRCode(string eno, out string error)
        {
            string code = "";
            error = "";
           

            if (FindInfo.isEligibleForCMS(FindInfo.findSRIDByENo(tbID.Text)))
            {
               

                code = "For verification and more details please follow the link : www.subhartidde.com/CMS.aspx?EN=" +HttpUtility.UrlEncode(eno.ToUpper());
               

            }
            else
            {
                error = "Sorry !! Invalid Enrollment No.";
            }


            return code;
        }
    }
}