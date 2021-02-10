<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowOnlineQueries.aspx.cs" Inherits="DDE.Web.Admin.ShowOnlineQueries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div>
                    <div align="center" class="heading" style="padding-bottom: 20px">
                        Show Online Queries
                    </div>
                    <div align="center" class="text" style="padding-top: 20px; padding-bottom: 20px">
                        <table cellspacing="10px" class="tableStyle2">
                            <tr>
                                <td align="left">
                                    <b>Status</b>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlistStatus" runat="server" Width="150px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlistStatus_SelectedIndexChanged">
                                        <asp:ListItem Value="1">PENDING</asp:ListItem>
                                        <asp:ListItem Value="2">SOLVED</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align="center" style="padding-bottom: 20px">
                        <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                            OnClick="btnFind_Click" />
                    </div>
                    <div align="center">
                        <asp:DataList ID="dtlistShowQueries" runat="server" Visible="false" CssClass="dtlist"
                            HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowQueries_ItemCommand">
                            <HeaderTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 50px">
                                            <b>S.No.</b>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <b>Name</b>
                                        </td>
                                        <td align="left" style="width: 250px">
                                            <b>Email ID</b>
                                        </td>
                                        <td align="left" style="width: 120px">
                                            <b>Contact No</b>
                                        </td>
                                        <td align="center" style="width: 360px">
                                            <b>Query</b>
                                        </td>
                                        <td align="center" style="width: 200px">
                                            <b>Remark</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 40px">
                                            <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                            <asp:Label ID="lblQID" runat="server" Text='<%#Eval("QID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <%#Eval("Name")%>
                                        </td>
                                        <td align="left" style="width: 250px">
                                            <%#Eval("EmailID")%>
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <%#Eval("ContactNo")%>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <%#Eval("Query")%>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <asp:TextBox ID="tbRemark" runat="server" Text='<%#Eval("Remark")%>' TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Make Offline" CausesValidation="false"
                                                ImageUrl="~/Admin/images/delete.png" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to make this query offline?');"
                                                runat="server" CommandArgument='<%#Eval("QID")%>' Width="20px" />
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:Button ID="btnSolved" CommandName="Solved" CommandArgument='<%#Eval("QID")%>'
                                                runat="server" Text="Make Solved" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <EditItemTemplate>
                            </EditItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
                <table align="center" class="tableStyle2">
                    <tr>
                        <td style="padding: 30px">
                            <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn1Year" />
            <asp:AsyncPostBackTrigger ControlID="btn2Year" />
            <asp:AsyncPostBackTrigger ControlID="btn3Year" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
