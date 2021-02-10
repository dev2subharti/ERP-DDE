<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="ShowMyRoles.aspx.cs" Inherits="DDE.Web.Admin.ShowMyRoles" %>


<asp:Content ContentPlaceHolderID="cphBody" runat="server">
<div align="center" >
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                My Roles
            </div>
            <div class="data" style="padding-top: 20px" align="center">
                <asp:DataList ID="dtlistMyRoles" runat="server" CssClass="dtlist" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="center" style="width: 300px">
                                    <b>Role Name</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblRoleID" Text='<%#Eval("RoleID") %>' runat="server" Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 300px; padding-left: 30px">
                                    <%#Eval("RoleName")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" Height="300px" Visible="false">
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