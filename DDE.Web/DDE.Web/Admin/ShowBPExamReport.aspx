<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowBPExamReport.aspx.cs" Inherits="DDE.Web.Admin.ShowBPExamReport" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            Show Back Paper Examination Report
        </div>
        <div style="padding: 10px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td>
                        Select Examination
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistExam" Width="150px" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Visible="false"
                OnClick="btnExport_Click" />
        </div>
        <div style="padding: 10px">
            <asp:GridView ID="gvShowStudent" CssClass="gridview" HeaderStyle-CssClass="gvheader"
                RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" runat="server">
            </asp:GridView>
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
