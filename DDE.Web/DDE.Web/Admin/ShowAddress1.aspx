<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowAddress1.aspx.cs" Inherits="DDE.Web.Admin.ShowAddress1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Degree</title>
    <script type="text/javascript">
        function pr() {

            window.print();
        }
    </script>
</head>
<body style="padding: 0px">
    <form id="form1" runat="server">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
           <%-- <div align="center">
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click"  />
            </div>--%>

            <asp:Panel ID="PanelReport" runat="server">
                <asp:Label ID="lblShow" runat="server"></asp:Label>
            </asp:Panel>

        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table align="center" class="tableStyle2">
                <tr>
                    <td style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
