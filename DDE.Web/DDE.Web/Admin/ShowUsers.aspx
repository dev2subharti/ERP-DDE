<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="ShowUsers.aspx.cs" Inherits="DDE.Web.Admin.ShowUsers" %>


<asp:Content ContentPlaceHolderID="cphBody" runat="server">
 
<div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading" >
             Users 
            </div>
            <div class="data" style="padding-top: 20px" align="center">
                <asp:DataList ID="dtlistShowUser" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowUser_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="center" style="width: 140px">
                                    <b>Photo</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>User Detail</b>
                                </td>
                               
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblQuesID" Text='<%#Eval("ERID") %>' runat="server" Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <a href="<%#Eval("PhotoID")%>">
                                        <img src="<%#Eval("PhotoID")%>" width="100px" height="100px" />
                                    </a>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("UserDetail")%>
                                </td>
                                
                                <td align="center" style="width: 100px">
                                    <asp:LinkButton ID="lnkbtnAssRole" runat="server" Text="Assign Role" CommandName="Assign Role"
                                        CommandArgument='<%#Eval("ERID") %>'></asp:LinkButton>
                                </td>
                                <td align="center" style="width: 170px">
                                    <asp:LinkButton ID="lnkbtnShowRoles" runat="server" Text="Show Assigned Roles" CommandName="Show Assigned Role"
                                        CommandArgument='<%#Eval("ERID") %>'></asp:LinkButton>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:LinkButton ID="btnPublishPassword" runat="server" Text="Publish Password" CommandName="Publish Password"
                                        CommandArgument='<%#Eval("ERID") %>'></asp:LinkButton>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                        CommandName="Delete" CommandArgument='<%#Eval("ERID") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
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