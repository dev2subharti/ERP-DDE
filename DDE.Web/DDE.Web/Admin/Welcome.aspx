<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="Welcome.aspx.cs" Inherits="DDE.Web.Admin.Welcome" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <table align="center" class="tableStyle2" width="550px" style="height: 320px">
        <tr>
            <td valign="top" align="center" class="anchor">
                <h1 style="margin-top:10px; margin-bottom:0px">
                    Welcome</h1>
                <asp:Image ID="imgEmployee" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                    runat="server" Width="100px" Height="120px" />
                <br />
                <br />
                <asp:Label ID="lblName" runat="server" Text=""></asp:Label><br />
                <br />
                To<br />
                <br />
                D.L.M.
                <br />
                <br />
                (Distance Learning Manager)
            </td>
        </tr>
    </table>
</asp:Content>
