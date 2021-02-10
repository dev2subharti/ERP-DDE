<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowECStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowECStudents" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
     <div class="heading" >
            List of Participating Students for June 2019 Examination
            </div>
        <div align="center" style="padding-top:10px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td>
                        <asp:Label ID="lblHeading" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding: 20px">
            <div align="center">
                <asp:GridView ID="gvShowMaildStudents" HeaderStyle-CssClass="gvheader" RowStyle-CssClass="gvitem"
                    runat="server">
                </asp:GridView>
            </div>
        </div>
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
</asp:Content>
