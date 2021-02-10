<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DegreeLetter.aspx.cs" Inherits="DDE.Web.Admin.DegreeLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DDE</title>
    <script type="text/javascript">
        function pr() {

            window.print();
        }
    </script>
    <style type="text/css">
        .head {
            text-align: center;
            font-size: 26px;
            font-weight: bold;
            margin: 0px;
        }

        .head1 {
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin: 0px;
        }

        .hd {
        }

        .add {
            text-align: center;
            font-family: Verdana;
            margin: 0px;
        }

        .to {
            font-family: Verdana;
            margin-left: 10px;
            text-align: left;
        }

        .gv {
            margin-left: 5px;
        }

        .msgpnl {
            padding-top: 100px;
        }


        .msg {
            font-family: Verdana;
            font-size: 14px;
            font-weight: bold;
        }

        .pnlmsg {
            padding-top: 50px;
        }

        .tableStyle2 {
            font-family: Verdana;
            font-weight: bold;
            font-size: 12px;
            color: #003f6f;
            text-decoration: none;
            background-image: url(../Images/upperstrip3.jpg);
            border: solid 1px #003f6f;
        }

        .style1 {
            height: 25px;
        }
    </style>
</head>
<body style="font-family: Verdana; font-size: 14px; margin: 0px">
    <form id="form1" runat="server">
        <div align="center">
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" OnClick="btnPrint_Click" />
                </div>
                <div align="center" style="width: 100%; margin: 2px">
                    <div align="center" style="width: 330px;">
                        <div style="font-size: 13px; font-family: Berlin Sans FB">
                            Distance Learning Manager (D.L.M.)
                        </div>
                        <div style="width: 320px; font-family: Cambria; font-size: 10px; margin: 0px" align="right">
                            <i>...An ERP System Managing DDE, SVSU</i>
                        </div>
                    </div>
                </div>
                <div style="width: 800px; margin: 0px">
                    <div style="width: 800px; margin: 0px">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../Images/logo1.png" />
                                </td>
                                <td>
                                    <p class="head">
                                        DIRECTORATE OF DISTANCE EDUCATION
                                    </p>
                                    <p class="head1">
                                        SWAMI VIVEKANAND SUBHARTI UNIVERSITY
                                    </p>
                                    <p class="add">
                                        Subhartipuram,NH-58,Delhi-Haridwar-Meerut By-Pass, Meerut-250005<br />
                                        Phone : 0121-3055000, Ext : 2800,2801 Fax : 0121-2439067<br />
                                        Website : www.subhartidde.com, E-mail : distance@subharti.org
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <hr style="border-bottom-style: solid; margin: 0px" />
                    </div>
                    <div style="background-image: url(Images/ddelogo3.png); background-repeat: no-repeat; background-position: center">
                        <div>
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="padding-left: 10px; width: 400px">
                                            <asp:Label ID="lblRefNo" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:Label ID="lblDIID" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblPNo" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblDate" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="padding-top: 0px">
                                <p class="to">
                                    To<br />
                                    The Degree Incharge,<br />
                                    <br />
                                </p>
                               <%-- <p>
                                    <b><u>Through: The Director, DDE, SVSU</u> </b>
                                </p>--%>
                                <p class="to">
                                    <u><b>Subject: For printing of Final Degree</b></u>
                                </p>
                                <p align="left" style="padding-left: 10px">
                                    Sir,<br />
                                    <br />
                                    We are forwarding you one application along with required relevant documents for
                                printing of Final Degree.
                                </p>
                            </div>
                            <div align="center" style="padding-left: 0px">
                                <table width="550px" cellspacing="15px">
                                    <tr>
                                        <td align="left" style="width: 200px">
                                            <b>Name of Faculty : DDE</b>
                                        </td>
                                        <td align="right">
                                            <b>
                                                <asp:Label ID="lblPassingYear" runat="server" Text=""></asp:Label>
                                            </b>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div align="center" style="padding-left: 0px">
                                <table width="600px" rules="all" cellpadding="5px" border="1px">
                                    <tbody align="left" style="font-size: 12px; font-weight: bold">
                                        <tr>
                                            <td class="hd" rowspan="2" style="width: 150px">Name of Student
                                            </td>
                                            <td class="hd" style="width: 100px">In English
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSNameE" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd">In Hindi
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSNameH" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" rowspan="2">Father's Name
                                            </td>
                                            <td class="hd">In English
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFNameE" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd">In Hindi
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFNameH" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" rowspan="2">Mother's Name
                                            </td>
                                            <td class="hd">In English
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMNameE" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd">In Hindi
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMNameH" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" valign="top" colspan="2">Name of Course
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourse" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" valign="top" colspan="2">Specialization (If any)
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSpec" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" valign="top" colspan="2">Roll No.
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRollNo" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" valign="top" colspan="2">Enrollment No.
                                            </td>
                                            <td>
                                                <asp:Label ID="lblENo" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" valign="top" colspan="2">Final Division
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDiv" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" valign="top" colspan="2">Degree/Diploma to be issued
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDegreeType" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="hd" valign="top" colspan="2">Aadhaar No. of Student
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAadhaarNo" runat="server" Font-Bold="false" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div>
                            <p align="left" style="padding-left: 10px; padding-top: 10px">
                                You are requested to kindly accord your sanction for the same.
                            <br />
                                <br />
                                Thanking you.
                            </p>
                        </div>
                        <div style="padding-top: 30px">
                            <table>
                                <tr>
                                    <td>
                                        <p align="left" style="padding-right: 20px; padding-bottom: 40px; margin: 0px;width:120px">
                                            <b>Prepared By</b>
                                        </p>
                                    </td>
                                    <td>
                                        <p align="center" style="padding-right: 20px; padding-bottom: 40px; margin: 0px;width:320px">
                                            <b>Checked By                                            </b>
                                        </p>
                                    </td>
                                    <td>
                                        <p align="right" style="padding-right: 20px; padding-bottom: 40px; margin: 0px;width:220px">
                                            <b>Director<br />
                                                DDE, SVSU</b>
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="margin-top: 0px">
                        <hr style="border-bottom-style: solid; margin: 2px" />
                        Assuring you of our best Educational Services always
                    <hr style="border-bottom-style: solid; margin: 2px" />
                    </div>
                    <div align="center" style="font-size: 10px; padding-top: 10px">
                        <div style="width: 280px; border: 1px solid black; border-radius: 3px; padding: 3px">
                            'DLM'<br />
                            Designed & Developed By : IT Cell, DDE
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
                <table class="tableStyle2">
                    <tr>
                        <td align="center" style="padding: 30px">
                            <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <div>
                    <table cellspacing="20px">
                        <tr>
                            <td>
                                <asp:Button ID="btnYes" runat="server" Text="Yes" Width="60px" OnClick="btnYes_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnNo" runat="server" Text="No" Width="60px" OnClick="btnNo_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
