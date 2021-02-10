<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarCode.aspx.cs" Inherits="DDE.Web.BarCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" id="clientEventHandlersJS">
        function save_onclick() {
            document.AxuEyeCam.SaveImage("test.jpg");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="pnlbc" runat="server">
        </asp:Panel>
        <asp:DropDownList ID="ddlistDoc" runat="server">
        <asp:ListItem>CMS</asp:ListItem>
        <asp:ListItem>DEGREE</asp:ListItem>
        <asp:ListItem>TS</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
        <asp:Button ID="btnGenerate" runat="server" Text="Generate QR Code" 
            onclick="btnGenerate_Click" />
        <asp:GridView ID="gv" runat="server">
        </asp:GridView>
    </div>
   
    <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" 
        onclick="btnSubmit_Click" />--%>
    </form>
</body>
</html>
