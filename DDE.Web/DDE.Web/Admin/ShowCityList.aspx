<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowCityList.aspx.cs" Inherits="DDE.Web.Admin.ShowCityList" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                List of Cities
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowCity" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistShowCity_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px; padding-left:10px">
                                    <b>City</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>State</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Country</b>
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
                                <td align="left" style="width: 150px">
                                    <%#Eval("City")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("State")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("Country")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("CityID") %>'></asp:LinkButton>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete"
                                        OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                        CommandArgument='<%#Eval("CityID") %>'></asp:LinkButton>
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
</asp:Content>
