<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="SearchML.aspx.cs" Inherits="DDE.Web.Admin.SearchML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Search Migration Letter
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                <div style="padding-bottom:10px">
                 <table cellspacing="10px" class="tableStyle1">
                    <tr>
                        <td valign="top" align="left">
                            <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">By Enrollment No.</asp:ListItem>
                                <asp:ListItem Value="2">By Letter No.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                </div>
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                               
                            </td>
                            <td>
                                <asp:TextBox ID="tbLNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
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
    <div style="padding-top: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
    </div>
</asp:Content>

