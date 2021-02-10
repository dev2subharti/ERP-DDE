<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowVerifiedDraft.aspx.cs" Inherits="DDE.Web.Admin.ShowVerifiedDraft" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Verified Draft
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowDrafts" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 70px; padding-left: 10px">
                                    <b>Type</b>
                                </td>
                                <td align="left" style="width: 150px; padding-left: 0px">
                                    <b>Draft No.</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Draft Date</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>IBN</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>DCAmount</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Verified On</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Verified By</b>
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
                                <td align="left" style="width: 70px">
                                    <%#Eval("Type")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("DCNumber")%>
                                   
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("DCDate")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("IBN")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("DCAmount")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("VOn")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("VBy")%>
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
