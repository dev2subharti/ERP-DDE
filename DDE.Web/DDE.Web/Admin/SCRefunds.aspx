<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="SCRefunds.aspx.cs" Inherits="DDE.Web.Admin.SCRefunds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Show Refunds of Study Centres<br />(Under Testing)
            </div>
            <div align="center" class="text">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td align="left">
                            <b>SC Type</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCType" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="1">UNIVERSITY</asp:ListItem>
                                <asp:ListItem Value="2">WEM</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Batch</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistDirectSC" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>SC Code</b>
                                </td>
                                <td align="right" style="width: 120px">
                                    <b>Required Fee</b>
                                </td>
                                <td align="right" style="width: 120px">
                                    <b>Late Fee</b>
                                </td>
                                <td align="right" style="width: 120px">
                                    <b>Paid Fee</b>
                                </td>
                                <td align="right" style="width: 120px">
                                    <b>Due Fee</b>
                                </td>
                                <td align="right" style="width: 170px">
                                    <b>University(60%)</b>
                                </td>
                                <td align="right" style="width: 170px">
                                    <b>Refund(40%)</b>
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
                                <td align="right" style="width: 110px">
                                    <%#Eval("ReqFee")%>
                                </td>
                                <td align="right" style="width: 110px">
                                    <%#Eval("LateFee")%>
                                </td>
                                <td align="right" style="width: 110px">
                                    <%#Eval("PaidFee")%>
                                </td>
                                <td align="right" style="width: 120px">
                                    <%#Eval("DueFee")%>
                                </td>
                                <td align="right" style="width: 170px">
                                    <%#Eval("University")%>
                                </td>
                                <td align="right" style="width: 170px">
                                    <%#Eval("Refund")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dtlistWEMSC" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>SC Code</b>
                                </td>
                                <td align="right" style="width: 120px">
                                    <b>Required Fee</b>
                                </td>
                                 <td align="right" style="width: 120px">
                                    <b>Late Fee</b>
                                </td>
                                <td align="right" style="width: 120px">
                                    <b>Paid Fee</b>
                                </td>
                                <td align="right" style="width: 120px">
                                    <b>Due Fee</b>
                                </td>
                                <td align="right" style="width: 170px">
                                    <b>University(50%)</b>
                                </td>
                                <td align="right" style="width: 150px">
                                    <b>WEM(10%)</b>
                                </td>
                                <td align="right" style="width: 170px">
                                    <b>Refund(40%)</b>
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
                                <td align="right" style="width: 110px">
                                    <%#Eval("ReqFee")%>
                                </td>
                                <td align="right" style="width: 110px">
                                    <%#Eval("LateFee")%>
                                </td>
                                <td align="right" style="width: 110px">
                                    <%#Eval("PaidFee")%>
                                </td>
                                <td align="right" style="width: 120px">
                                    <%#Eval("DueFee")%>
                                </td>
                                <td align="right" style="width: 150px">
                                    <%#Eval("University")%>
                                </td>
                                <td align="right" style="width: 170px">
                                    <%#Eval("WEM")%>
                                </td>
                                <td align="right" style="width: 170px">
                                    <%#Eval("Refund")%>
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
