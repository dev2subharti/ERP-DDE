<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/StudyCentre.Master" AutoEventWireup="true"
    CodeBehind="PayAmountBySC.aspx.cs" Inherits="DDE.Web.Admin.PayAmountBySC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Make Payment
            </div>
            <div align="center" class="text">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td align="left">
                            <b>Total Payment</b>
                        </td>
                        <td>
                            <asp:Label ID="lblTP" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 10px">
                <table cellspacing="10px">
                    <tr>
                        <td>
                            <asp:Button ID="btnPBChalan" runat="server" Width="100px"
                                Text="Pay By Chalan" onclick="btnPBChalan_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btn_Request" runat="server" Width="100px" OnClick="btn_Request_Click"
                                Text="Pay Online" />
                        </td>
                    </tr>
                </table>
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
