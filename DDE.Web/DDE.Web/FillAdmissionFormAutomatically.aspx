<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillAdmissionFormAutomatically.aspx.cs" Inherits="DDE.Web.FillAdmissionFormAutomatically" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div align="center" style="padding-top:20px">
        <asp:Button ID="btnAutoFeed" runat="server" Text="Auto Feed Admission Form" 
            onclick="btnAutoFeed_Click" />
    </div>
    <%--<div>
        <asp:Button ID="btnUpdateANo" runat="server" Text="Update ANo" 
            onclick="btnUpdateANo_Click" />
    
    </div>--%>
    </div>
    </form>
</body>
</html>
