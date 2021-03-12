<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProvisionalDegree.aspx.cs" Inherits="DDE.Web.Admin.ProvisionalDegree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Provisional Degree</title>
    <script type="text/javascript">
        function pr() {

            window.print();
        }
    </script>

   <%-- <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert(" You can enter only Number 0 to 9 ");
                return false;
            }
            return true;
        }
        function PrintDiv() {
            var contents = document.getElementById("<%=PanelReport.ClientID %>").innerHTML;
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);
            var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
            frameDoc.document.open();
            frameDoc.document.write('<html><head>');
            frameDoc.document.write('</head><body>');
            frameDoc.document.write(contents);
            frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                document.body.removeChild(frame1);
            }, 500);
            return false;
        }
    </script>--%>

    <style type="text/css">
        /*@page {
            size: A4 landscape;*/ /* can use also 'landscape' for orientation */
        /*}*/

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

        .dtlist {
            border: 1px solid #0099FF;
        }

        .dtlistItemAC {
            background: url(images/ac_body.jpg) repeat-x top;
            border: 1px solid #0099FF;
            font-family: Verdana;
            text-align: left;
            font-size: 12px;
            color: #003f6f;
        }

        .dtlistItemIC {
            border: 1px solid #0099FF;
            font-family: Verdana;
            text-align: left;
            font-size: 12px;
            color: #003f6f;
        }

        .dtlistheader {
            background-image: url(../Images/GVHeader.jpg);
            background-repeat: repeat-x;
            height: 25px;
            font-family: Verdana;
            font-weight: bold;
            font-size: 14px;
            color: #003f6f;
        }

        .dtlistfooter {
            background-image: url(../Images/GVHeader.jpg);
            background-repeat: repeat-x;
            height: 25px;
            font-family: Verdana;
            font-weight: bold;
            font-size: 14px;
            color: #003f6f;
        }


        .dtlistItem {
            background-color: #FFFFFF;
            border: 1px solid #0099FF;
            font-family: Verdana;
            text-align: left;
            margin-left: 10px;
            font-size: 12px;
            padding-left: 10px;
            margin-left: 10px;
            color: #003f6f;
        }

        .dtlistheaderDS {
            background-image: url(../Images/GVHeader.jpg);
            background-repeat: repeat-x;
            height: 25px;
            font-family: Verdana;
            font-weight: bold;
            font-size: 14px;
            color: #003f6f;
            border: 1px solid #0099FF;
        }

        .dtlistItemDS {
            background-color: White;
            margin: 0px;
            font-family: Verdana;
            text-align: left;
            font-size: 12px;
            padding-left: 0px;
            color: #003f6f;
        }

        .Landscape {
            width: 100%;
            height: 100%;
            margin: 0% 0% 0% 0%;
            rotation: 90deg;
            /*filter: progid:DXImageTransform.Microsoft.BasicImage(Rotation=3);*/
        }

        body {
            width: 600px;
            height: 720px;
        }
    </style>
</head>
<body style="font-family: Verdana; font-size: 14px">
    <%--<body style="margin: 0px; background-image: url(images/ddelogo5.png); width: 100%; background-repeat: repeat">--%>
    <form id="form1" runat="server">
        <div align="center">
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div>
                    <%--<asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" OnClick="btnPrint_Click" />--%>
                </div>
                <div>
                    <div class="Landscape">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint2" CssClass="btn btn-danger" runat="server" Text="Print"
                                        CausesValidation="False" TabIndex="14" OnClientClick="return PrintDiv();" Visible="false"
                                        Width="200px"></asp:Button>
                                </td>
                            </tr>

                            <tr>
                                <td style="background-image: url(images/ddelogo5.png); width: 100%; background-repeat: no-repeat">
                                    <asp:Panel ID="PanelReport" runat="server">
                                        <asp:Label ID="lblshow" runat="server"></asp:Label>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
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
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
