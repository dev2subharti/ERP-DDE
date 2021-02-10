<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="SetRequiredFee.aspx.cs" Inherits="DDE.Web.Admin.SetRequiredFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 0px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Required Amount of Fee Heads
            </div>
            <div align="center" class="text">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td align="left">
                            <b>Batch</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 10px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="height: 26px" Width="82px"
                    OnClick="btnSearch_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowReqAmount" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" 
                    onitemcommand="dtlistShowReqAmount_ItemCommand" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 75px; padding-left: 5px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 400px">
                                    <b>Fee Head</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Req. Amount</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>From</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>To</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 400px">
                                    <%#Eval("FeeHead")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <asp:Label ID="lblRequiredFee" runat="server" Text='<%#Eval("RequiredFee")%>'></asp:Label> 
                                </td>
                                <td align="left" style="width: 140px">
                                    <%#Eval("From")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <%#Eval("To")%>
                                </td>
                                <td align="center" style="width: 100px">
                                     <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("RFID") %>'></asp:LinkButton>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:LinkButton ID="lnkbtnCreate" runat="server" Text="Create New" CommandName="Create" CommandArgument='<%#Eval("FHID") %>'></asp:LinkButton>
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
