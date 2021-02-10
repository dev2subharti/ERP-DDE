<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="PublishReportSPCWiseGender.aspx.cs" Inherits="DDE.Web.Admin.PublishReportSPCWiseGender" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Program Code and Gender Wise Report
        </div>
        <div>
            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Batch</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch" runat="server">
                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                           
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:GridView ID="gvReport" HeaderStyle-CssClass="gvheader" RowStyle-CssClass="gvitem" runat="server">
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
