<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillAdmissionFormAutomatically2.aspx.cs" Inherits="DDE.Web.FillAdmissionFormAutomatically2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <asp:Button ID="btnAutoFeed" runat="server" Text="Auto Fill Admission Form" OnClick="btnAutoFeed_Click" />
        </div>
        <div align="center" style="padding-top:50px">
            <asp:Button ID="btnAutoFeedFromExcel" runat="server" Text="Auto Fill Admission Form from Excel Sheet" OnClick="btnAutoFeedFromExcel_Click"  />
        </div>
      <%--  <div align="center" style="padding-top:50px">
            <asp:Button ID="btnDetain" runat="server" Text="Detain Student" OnClick="btnDetain_Click" />
        </div>--%>
    </form>
</body>
</html>
