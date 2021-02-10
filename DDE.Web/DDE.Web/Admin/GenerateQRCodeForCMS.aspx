<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="GenerateQRCodeForCMS.aspx.cs" Inherits="DDE.Web.Admin.GenerateQRCodeForCMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Generate / Search QR Code for Consolidated Mark Sheet
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <div align="center">
                        <div align="left" style="width: 90px">
                            <asp:RadioButtonList ID="rblMode" CssClass="tableStyle2" CellSpacing="5" AutoPostBack="true" runat="server"
                                TextAlign="Right" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">Generate</asp:ListItem>
                                <asp:ListItem Value="2">Search</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div style="padding: 10px">
                        <table cellspacing="10px" class="tableStyle2">
                            <tr>
                                <td>
                                    Enrollment No.
                                </td>
                                <td>
                                    <asp:TextBox ID="tbID" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Generate" OnClick="btnsearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlQRCode" Visible="false" runat="server">
                    <table border="1" rules="all" width="500px" class="tableStyle2" cellpadding="10px">
                        <tr>
                            <td style="width: 240px">
                                QR Code
                            </td>
                            <td>
                                Assumed Output after Scan
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="background-color: White; padding:10px">
                                <asp:Image ID="imgQRCode" runat="server" Width="150px" Height="150px" />
                            </td>
                            <td style="background-color: White; padding:10px">
                                <asp:Label ID="lblOutput" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td align="center" style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    
</asp:Content>
