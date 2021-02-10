<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ProcessSLMLetter.aspx.cs" Inherits="DDE.Web.Admin.ProcessSLMLetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="smslm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div align="center" class="heading">
                    Process SLM Letter (On Trial)
                </div>
                <div style="padding-top: 20px; padding-bottom: 20px">
                    <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                        <table cellspacing="10px" class="tableStyle2">
                            <tr>
                                <td>
                                    <table cellspacing="10px">
                                        <tr>
                                            <td>
                                                <b>Letter No.</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbLNo" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div style="padding-top: 20px">
                    <asp:Panel ID="pnlAllDetails" runat="server" Visible="false">
                        <table width="500px" class="tableStyle2" cellpadding="0px" cellspacing="10px">
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="pnlLetterDetails" runat="server" GroupingText="Letter Details">
                                        <table  cellspacing="10px" width="100%">
                                            <tr>
                                                <td align="left" style="width: 100px">
                                                    Letter No.
                                                </td>
                                                <td align="left">
                                                <asp:TextBox ID="tbLetterNo" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                       
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 100px">
                                                    Letter Date
                                                </td>
                                                <td align="left">
                                                   <asp:TextBox ID="tbLetterDate" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 100px">
                                                    SC Code
                                                </td>
                                                <td align="left">
                                                     <asp:TextBox ID="tbSCCode" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 100px">
                                                    Total SLM
                                                </td>
                                                <td align="left">
                                                     <asp:TextBox ID="tbTotalSLM" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td align="left" style="width: 100px">
                                                    Party
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlistParty" AutoPostBack="true" runat="server" 
                                                        onselectedindexchanged="ddlistParty_SelectedIndexChanged">
                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblPRate" runat="server" Visible="false" Text=""></asp:Label>
                                                    <asp:CheckBox ID="cbByHand" AutoPostBack="true" runat="server" Text="By Hand" 
                                                        oncheckedchanged="cbByHand_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="pnlDispatchDetails" runat="server" GroupingText="Dispatch Details">
                                        <div align="center" style="padding: 10px">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add Packet" OnClick="btnAdd_Click" />
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
                                                                    <td align="left" style="width: 125px">
                                                                        <b>Weight (kg)</b>
                                                                    </td>
                                                                    <td align="left" style="width: 120px">
                                                                        <b>Rate/Kg</b>
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
                                                                        <asp:Label ID="lblPID" runat="server" Visible="false" Text='<%#Eval("PID")%>'></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="width: 125px">
                                                                        <asp:TextBox ID="tbWeight" runat="server" Width="50px" Text='<%#Eval("PWeight")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td align="left" style="width: 120px">
                                                                        <asp:TextBox ID="tbRate" Width="50px" Enabled="false" runat="server" Text='<%#Eval("Rate")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td align="right" style="width: 100px">
                                                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                                    </td>
                                                                    <td align="right" style="width: 30px">
                                                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CausesValidation="false"
                                                                            ImageUrl="~/Admin/images/delete.png" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                                            runat="server" CommandArgument='<%#Eval("PID")%>' Width="20px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                                <div>
                                                    <table width="460px" class="tableStyle2" cellspacing="10px">
                                                        <tr>
                                                            <td align="left" style="width: 40px">
                                                                <b>Total</b>
                                                            </td>
                                                            <td align="left" style="width: 180px">
                                                                <asp:Label ID="lblTotalWt" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                            </td>
                                                            <td align="right" style="width: 80px; padding-right: 25px">
                                                                <asp:Label ID="lblTotalAmount" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div align="left" style="width: 460px">
                                                    <asp:Button ID="btnLock" runat="server" Text="Lock Details" OnClick="btnLock_Click" />
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                         <div style="padding-top: 10px">
                    <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Process Letter" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                    </asp:Panel>
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
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnOK" runat="server" Width="50px" Text="OK" 
                        onclick="btnOK_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
