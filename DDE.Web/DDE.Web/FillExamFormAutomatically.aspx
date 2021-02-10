<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillExamFormAutomatically.aspx.cs"
    Inherits="DDE.Web.FillExamFormAutomatically" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   
    <div align="center" style="padding-top:20px">
        <asp:Button ID="btnAutoFeed" runat="server" Text="Auto Feed Exam Form" 
            onclick="btnAutoFeed_Click" />
    </div>
         <div align="center" style="padding-top:20px">
        <asp:Button ID="btnLateFee" runat="server" Text="Auto Feed Late Fee" OnClick="btnLateFee_Click" 
             />
    </div>
    </form>
</body>
</html>
