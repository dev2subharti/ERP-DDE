<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowSCRenewalStatus.aspx.cs" Inherits="DDE.Web.Admin.ShowSCRenewalStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
   
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div style="padding: 0px">
                    <div align="center" class="heading" style="padding-bottom: 20px">
                        Show SC Renewal Status
                    </div>
                    <div align="center" class="text" style="padding: 20px">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td>
                                    <b>Status</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistStatus" runat="server">
                                        <asp:ListItem>OK</asp:ListItem>
                                        <asp:ListItem>PENDING</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                                        OnClick="btnFind_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align="center">
                        <asp:DataList ID="dtlistShowStudyCentres" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                            ItemStyle-CssClass="dtlistItem">
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
                                        <td align="left" style="width: 150px">
                                            <b>Renewal Status</b>
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
                                        <td align="left" style="width: 150px">
                                            <%#Eval("Renewal")%>
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
