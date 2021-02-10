<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadExcel.aspx.cs" Inherits="DDE.Web.UploadExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <div>
            <%--  <asp:FileUpload ID="fuExcel" runat="server"></asp:FileUpload> &nbsp;&nbsp; --%>
            <asp:Button ID="btnUploadExcel" runat="server" Text="Upload Excel" OnClick="btnUploadExcel_Click" />
        </div>
        <div>
        </div>
        <div>
            <asp:FileUpload ID="fuStPhotos" Multiple="Multiple" runat="server"></asp:FileUpload>&nbsp;&nbsp;
            <asp:Button ID="btnUploadStPhoto" runat="server" Text="Upload Photos" OnClick="btnUploadStPhoto_Click" />
        </div>
      <%--<div style="padding-top: 20px">
            <asp:Button ID="btnDelete" runat="server" Text="Delete Data" 
                onclick="btnDelete_Click" />
        </div>--%>
        
        <div style="padding-top:50px">
            <asp:RadioButtonList ID="rblType" runat="server">
                <asp:ListItem Value="0">TEXT</asp:ListItem>
                 <asp:ListItem Value="1">IMAGE</asp:ListItem>
            </asp:RadioButtonList>
            <asp:TextBox ID="tbPaperCode" runat="server"></asp:TextBox>
            <asp:FileUpload ID="fuQuestion" Multiple="Multiple" runat="server"></asp:FileUpload>&nbsp;&nbsp;
            <asp:Button ID="btnUploadQP" runat="server" Text="Upload Questions" OnClick="btnUploadQP_Click" />
        </div>
    </div>
    </form>
</body>
</html>
