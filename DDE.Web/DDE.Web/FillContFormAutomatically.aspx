<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FillContFormAutomatically.aspx.cs" Inherits="DDE.Web.FillContFormAutomatically" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <asp:CheckBox ID="cbTransfer" runat="server" Text="Transfer to 001" />
        </div>
        <br />
    <div align="center">
    <asp:Button ID="btnSubmit" runat="server" Text="Fill Excel Sheet Cont Form Automatically" 
            onclick="btnSubmit_Click" />
    </div>
    <br />
    <br />

    <div align="center">
    <asp:Button ID="btnSubmitOnlineCont" runat="server" 
            Text="Fill Online Cont Form Automatically" onclick="btnSubmitOnlineCont_Click" 
             />
    </div>
    <br />
    <br />
   <%-- <div align="center">
    <asp:Button ID="btnCutSpFee" runat="server" 
            Text="Cut Spcial Fee Automatically" onclick="btnCutSpFee_Click"
             />
    </div>--%>
    </form>
    
</body>
</html>
