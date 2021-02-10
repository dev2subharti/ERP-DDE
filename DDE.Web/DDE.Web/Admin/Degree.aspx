<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Degree.aspx.cs" Inherits="DDE.Web.Admin.Degree" %>

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
             <div align="center">
                 <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
             </div>
        <div align="center" style="font-family:Verdana; font-size:14px; font-weight:bold">
       <div align="center" style="width: 700px; position:relative">
           <img src="images/FORMAT-DDE2.jpg" height="1000px" />
                <div align="right" style="top:30px; right:80px; position:absolute">
                    <asp:Label ID="lblSNo" Font-Size="12px" runat="server" Text=""></asp:Label>
                    
                </div>
            <div align="right" style="top:70px; left:370px; position:absolute">
                <asp:Label ID="lblSRID" runat="server" Visible="false" Font-Size="12px" Text=""></asp:Label>
                    <asp:Label ID="lblBatchID" runat="server" Visible="false" Font-Size="12px" Text=""></asp:Label>
                </div>
                <div align="right" style="top:70px; left:565px; position:absolute">
                    <asp:Label ID="lblENo" runat="server" Font-Size="12px" Text=""></asp:Label>
                </div>
            <div align="left" style="top:250px; left:540px; position:absolute">
                <asp:Image ID="imgStudent" Height="100px" BorderColor="#003f6f" BorderWidth="2px" BorderStyle="Solid" runat="server" />
                </div>
                <div align="left" style="top:405px; left:240px; position:absolute">
                     <asp:Label ID="lblStudentNameEnglish" Font-Size="11px" runat="server" Text=""></asp:Label>
                </div>
                <div align="left" style="top:473px; left:120px; position:absolute">
                     <asp:Label ID="lblExamNameEnglish" Font-Size="11px" runat="server" Text=""></asp:Label>
                </div>
                 <div align="center" style="top:505px; left:200px; position:absolute">
                     <asp:Label ID="lblCourseNameEnglish" Font-Size="11px" runat="server" Text=""></asp:Label>
                </div>
                <div align="left" style="top:540px; left:70px;  position:absolute">
                     <asp:Label ID="lblDE" runat="server" Font-Size="11px" Text=""></asp:Label>
                </div>
                <div align="left" style="top:540px; left:420px;  position:absolute">
                     <asp:Label ID="lblSpecEnglish" Font-Size="11px" runat="server" Text=""></asp:Label>
                </div>
                <div align="left" style="top:655px; left:280px;  position:absolute">
                     <asp:Label ID="lblStudentNameHindi" Font-Size="12px" runat="server" Text=""></asp:Label>
                </div>
                <div align="left" style="top:685px; left:320px;  position:absolute">
                     <asp:Label ID="lblDH" runat="server" Font-Size="12px" Text=""></asp:Label>
                </div>
            <div align="left" style="top:715px; left:200px;  position:absolute">
                     <asp:Label ID="lblCourseNameHindi" Font-Size="12px" runat="server" Text=""></asp:Label>
                </div>
            <div align="left" style="top:745px; left:200px;  position:absolute">
                     <asp:Label ID="lblSpecHindi" runat="server" Font-Size="12px" Text=""></asp:Label>
                </div>
            <div align="left" style="top:775px; left:270px;  position:absolute">
                     <asp:Label ID="lblExamHindi" Font-Size="12px" runat="server" Text=""></asp:Label>
                </div>
            <div align="left" style="top:905px; left:100px;  position:absolute">
                     <asp:Label ID="lblDate" Font-Size="12px" runat="server" Text=""></asp:Label>
                </div>
            <div align="left" style="top:860px; left:230px;  position:absolute">
                      <asp:Panel ID="pnlBC" runat="server">
                                                                    </asp:Panel>
                </div>
            </div>           
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
