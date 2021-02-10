<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="PublishPasswordSC.aspx.cs" Inherits="DDE.Web.Admin.PublishPasswordSC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 0px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Passwords of Study Centres
            </div>
            <div style="padding-bottom: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            Status
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistStatus" runat="server">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="1">ONLINE</asp:ListItem>
                                <asp:ListItem Value="0">OFFLINE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                onclick="btnSearch_Click" />
                        </td>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowStudyCentres" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowStudyCentres_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 130px">
                                    <b>S.C. Code</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Password</b>
                                </td>
                                <td align="left" style="width: 500px">
                                    <b>Study Centre Name</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblPassMailSent" runat="server" Text='<%#Eval("PassMailSent")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 500px">
                                    <%#Eval("SCName")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Button ID="btnSendMail" runat="server" Text="Send To Mail" CommandArgument='<%#Eval("SCID")%>'
                                        CommandName="Send Mail" />
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CommandArgument='<%#Eval("SCID")%>'
                                        CommandName="Reset Password" />
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
