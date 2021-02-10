<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="VerifyRefund.aspx.cs" Inherits="DDE.Web.Admin.VerifyRefund" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
     <div class="heading">
                Verify Reimbursement
            </div>
        <div style="padding: 20px">
            <div align="center">
                <asp:DataList ID="dtlistRL" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistRL_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>SNo.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Letter No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Net Payable</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Ins. Amount</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Ins. Type</b>
                                </td>
                                <td align="left" style="width: 180px">
                                    <b>Ins. No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Ins. Date</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>IBN</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 40px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("RLID")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("NetRefund")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("IAmount")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("IType")%>
                                </td>
                               <td align="left" style="width: 180px">
                                    <%#Eval("INo")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("IDate")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("IBN")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Button ID="btnVerify" runat="server" Width="80px" Text="Verify" CommandName="Verify" CommandArgument='<%#Eval("RLID")%>' />
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
