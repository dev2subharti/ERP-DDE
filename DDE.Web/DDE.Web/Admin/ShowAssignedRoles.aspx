<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="ShowAssignedRoles.aspx.cs" Inherits="DDE.Web.Admin.ShowAssignedRoles" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
<div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Assigned Roles of
            </div>
            <div class="text" style="padding: 20px">
                <table class="tableStyle2">
                    <tr>
                        <td style="padding-left: 5px">
                            <asp:Image ID="imgEmployee" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                runat="server" Width="100px" Height="120px" />
                        </td>
                        <td valign="top">
                            <table cellspacing="10px">
                                <tr>
                                    <td align="left">
                                        <b>Employee ID</b>
                                    </td>
                                    <td align="left">
                                        <b>:</b>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblEmpID" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>Employee Name</b>
                                    </td>
                                    <td align="left">
                                        <b>:</b>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblEName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>Designation</b>
                                    </td>
                                    <td align="left">
                                        <b>:</b>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>Department</b>
                                    </td>
                                    <td align="left">
                                        <b>:</b>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblEmpDepartment" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>Unit</b>
                                    </td>
                                    <td align="left">
                                        <b>:</b>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
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