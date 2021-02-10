<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="CreateUser.aspx.cs" Inherits="DDE.Web.Admin.CreateUser" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <div align="center" style="height: 300px">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
               
                    Create New User
            </div>
            <div style="padding: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            <b>Select Employee</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistEmployee" runat="server">
                                <asp:ListItem>--Select Here--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnCreateUser" runat="server" Text="Create User" OnClick="btnCreateUser_Click" />
                        </td>
                    </tr>
                </table>
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
    </div>
</asp:Content>
