<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="SellSLM.aspx.cs" Inherits="DDE.Web.Admin.SellSLM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="smslm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div align="center" class="heading">
                    Sell SLM (On Trial)
                </div>
                <div style="padding-top: 20px">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td>
                                Party Name
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistParty" AutoPostBack="true"  runat="server" 
                                    onselectedindexchanged="ddlistParty_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="padding-top: 20px">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlistSLM" Width="100px" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnAdd" runat="server" Text="Add SLM" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center">
                    <asp:Panel ID="pnlBill" runat="server" Visible="false">
                        <div style="padding-top: 10px">
                            <asp:DataList ID="dtlistSLM" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistSLM_ItemCommand">
                                <HeaderTemplate>
                                    <table align="left">
                                        <tr>
                                            <td align="left" style="width: 70px">
                                                <b>SNo.</b>
                                            </td>
                                            <td align="left" style="width: 150px">
                                                <b>SLM Code</b>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <b>Title</b>
                                            </td>
                                            <td align="right" style="width: 80px">
                                                <b>Quantity</b>
                                            </td>
                                            <td align="right" style="width: 100px">
                                                <b>Rate</b>
                                            </td>
                                            <td align="right" style="width: 100px">
                                                <b>Amount</b>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table align="left" style="font-family: Cambria; font-weight: normal; font-size: 14px">
                                        <tr>
                                            <td align="left" style="width: 60px">
                                                <%#Eval("SNo")%>
                                                <asp:Label ID="lblSLMID" runat="server" Visible="false" Text='<%#Eval("SLMID")%>'></asp:Label>
                                            </td>
                                            <td align="left" style="width: 150px">
                                                <asp:Label ID="lblSLMCode" runat="server" Text='<%#Eval("SLMCode")%>'></asp:Label>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                            </td>
                                            <td align="right" style="width: 80px">
                                                <asp:TextBox ID="tbQuantity" runat="server" Width="50px" Text='<%#Eval("Quantity")%>'></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 100px">
                                                <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")%>'></asp:Label>
                                            </td>
                                            <td align="right" style="width: 100px">
                                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                            </td>
                                            <td align="right" style="width: 30px">
                                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CausesValidation="false"
                                                    ImageUrl="~/Admin/images/delete.png" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                    runat="server" CommandArgument='<%#Eval("SLMID")%>' Width="20px" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div>
                            <table class="tableStyle2" width="800px" cellspacing="10px">
                                <tr>
                                    <td align="right" style="width: 250px; padding-right:50px">
                                        <b>Total</b>
                                    </td>
                                    <td align="left" style="width: 70px; padding-left:70px">
                                        <asp:Label ID="lblTotalSLM" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="right" style="width: 80px; padding-right: 25px">
                                        <asp:Label ID="lblTotalAmount" Font-Bold="true" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div align="left" style="width: 800px">
                            <asp:Button ID="btnLock" runat="server" Text="Lock Details" OnClick="btnLock_Click" />
                        </div>
                    </asp:Panel>
                </div>
                <div style="padding-top: 10px">
                    <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Publish Letter" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
            </div>
            <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
                <table class="tableStyle2">
                    <tr>
                        <td style="padding: 30px">
                            <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnOK" runat="server" Width="50px" Text="OK" OnClick="btnOK_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
