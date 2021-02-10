<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="UpdateSLMStock.aspx.cs" Inherits="DDE.Web.Admin.UpdateSLMStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="smslm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div align="center" class="heading">
                    Update SLM Stock
                </div>
                <div style="padding-top: 20px">
                    <div align="center">
                        <asp:DataList ID="dtlistShowSLM" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                            ItemStyle-CssClass="dtlistItemDS" OnItemCommand="dtlistShowSLM_ItemCommand">
                            <HeaderTemplate>
                                <table align="left" cellspacing="0px" cellpadding="0px">
                                    <tr>
                                        <td align="center" style="width: 50px; border-right: 1px solid black">
                                            <b>SNo</b>
                                        </td>
                                        <td align="center" style="width: 112px; border-right: 1px solid black">
                                            <b>SLM Code</b>
                                        </td>
                                        <td align="center" style="width: 310px; border-right: 1px solid black">
                                            <b>Title</b>
                                        </td>
                                       
                                        <td align="center" style="width: 90px; border-right: 1px solid black">
                                            <b>In Stock</b>
                                        </td>
                                        <td align="center" style="width: 60px; border-right: 1px solid black">
                                            <b>Mode</b>
                                        </td>
                                         <td align="center" style="width: 60px; border-right: 1px solid black">
                                            <b>Qty.</b>
                                        </td>
                                        <td align="center" style="width: 150px; border-right: 1px solid black">
                                            <b>Remark</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                                    <tr>
                                        <td align="center" class="border_lbr" style="width: 39px">
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                            <asp:Label ID="lblSLMID" runat="server" Text='<%#Eval("SLMID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td align="center" class="border_rb" style="width: 102px">
                                            <asp:Label ID="lblSLMCode" runat="server" Text='<%#Eval("SLMCode")%>'></asp:Label>
                                        </td>
                                        <td align="left" class="border_rb" style="width: 300px">
                                            <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                        </td>
                                       
                                        <td align="center" class="border_rb" style="width: 80px">
                                            <asp:Label ID="lblPS" runat="server" Font-Bold="true" Text='<%#Eval("PS")%>'></asp:Label>
                                        </td>
                                        <td align="center" class="border_rb" style="width: 50px">
                                            <asp:DropDownList ID="ddlistCalc" runat="server">
                                                <asp:ListItem>+</asp:ListItem>
                                                <asp:ListItem>-</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" class="border_rb" style="width: 70px">
                                            <asp:TextBox ID="tbNewEntry" Width="60px" runat="server"></asp:TextBox>
                                        </td>
                                         <td align="center" class="border_rb" style="width: 150px">
                                            <asp:TextBox ID="tbRemark" Width="140px" runat="server" Text=""></asp:TextBox>
                                        </td>
                                        <td align="center" class="border_rb" style="width: 70px">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandArgument='<%#Eval("SLMID")%>'
                                                CommandName='Update' />
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
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
