<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishStudentListofSLMLetter.aspx.cs"
    Inherits="DDE.Web.Admin.PublishStudentListofSLMLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/DDE.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
   
    <div align="center" style="padding: 20px">
     <div align="center" class="heading">
        List of Students Attached with SLM Issue Letter
    </div>
    <div style="padding: 10px" class="text">
        <asp:Label ID="lblLNo" Font-Bold="true" runat="server" Text=""></asp:Label>
    </div>
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <asp:GridView ID="gvShowStudent" Width="800px" CssClass="gridview" HeaderStyle-CssClass="gvheader"
                RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" runat="server">
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
