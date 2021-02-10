<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowSLMRequirements.aspx.cs" Inherits="DDE.Web.Admin.ShowSLMRequirements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show Current Requiremts of SLMs (On Trial)<br />
            <asp:Label ID="lblDateTime" runat="server" Text=""></asp:Label>
        </div>
        <div align="center" style="padding-top: 20px">
            <asp:Panel ID="pnlSLMList" runat="server" Visible="true">
                <asp:DataList ID="dtlistSLM" GridLines="Both" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" FooterStyle-CssClass="dtlistfooter">
                    <HeaderTemplate>
                        <table align="left" rules="all">
                            <tr>
                                <td align="left" style="width: 40px; padding: 5px">
                                    <b>SNo.</b>
                                </td>
                                <td align="left" style="width: 100px; padding: 5px">
                                    <b>SLMCode</b>
                                </td>
                                <td align="left" style="width: 300px; padding: 5px">
                                    <b>Title</b>
                                </td>
                                <td align="left" style="width: 100px; padding: 5px">
                                    <b>In Stock</b>
                                </td>
                                <td align="left" style="width: 95px; padding: 5px">
                                    <b>Required</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left" rules="all">
                            <tr>
                                <td align="left" style="width: 40px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 100px; padding: 5px">
                                    <asp:Label ID="lblSLMCode" runat="server" Text='<%#Eval("SLMCode")%>'></asp:Label>
                                    <asp:Label ID="lblSLMID" runat="server" Text='<%#Eval("SLMID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblLang" runat="server" Text='<%#Eval("Lang")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblDual" runat="server" Text='<%#Eval("Dual")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblGroupID" runat="server" Text='<%#Eval("GroupID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 300px; padding: 5px">
                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 100px; padding: 5px">
                                    <asp:Label ID="lblPS" runat="server" Text='<%#Eval("PresentStock")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 100px; padding: 5px">
                                    <asp:Label ID="lblTR" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                        <table align="left" rules="all">
                            <tr>
                                <td align="right" style="width: 563px; padding-right: 20px">
                                    <b>Total</b>
                                </td>
                                <td align="left" style="width: 105px; padding: 5px">
                                    <asp:Label ID="lblTotalRequired" runat="server" Text="" OnLoad="lblTotalRequired_OnLoad"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:DataList>
            </asp:Panel>
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
</asp:Content>
