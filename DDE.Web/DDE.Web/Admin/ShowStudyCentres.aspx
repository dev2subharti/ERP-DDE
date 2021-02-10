<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowStudyCentres.aspx.cs" Inherits="DDE.Web.Admin.ShowStudyCentres" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div style="padding: 0px">
                    <div align="center" class="heading" style="padding-bottom: 20px">
                        List of Study Centres
                    </div>
                    <div align="center">
                        <asp:DataList ID="dtlistShowStudyCentres" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                            ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowStudyCentres_ItemCommand">
                            <HeaderTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 50px">
                                            <b>S.No.</b>
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <b>S.C. Code</b>
                                        </td>
                                        <td align="left" style="width: 140px">
                                            <b>City</b>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <b>Study Centre Name</b>
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <b>Status</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 50px">
                                            <%#Eval("SNo")%>
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <%#Eval("SCCode")%>
                                        </td>
                                        <td align="left" style="width: 140px">
                                            <%#Eval("City")%>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <%#Eval("SCName")%>
                                        </td>
                                        <td align="center" style="width: 110px">
                                            <asp:Button ID="btnMode" runat="server" Text='<%#Eval("Authorised") %>' Width="100px"
                                                CommandName="" CommandArgument='<%#Eval("SCID") %>' />
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:Button ID="btnOnline" runat="server" Text='<%#Eval("Online") %>' Width="120px"
                                                CommandName='<%#Eval("Online") %>' CommandArgument='<%#Eval("SCID") %>' />
                                        </td>
                                        <td align="center" style="width: 50px">
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("SCID") %>'></asp:LinkButton>
                                        </td>
                                        <td align="center" style="width: 50px">
                                            <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete"
                                                OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                CommandArgument='<%#Eval("SCID") %>'></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
