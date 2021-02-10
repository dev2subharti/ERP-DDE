<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="DistributeAmount.aspx.cs" Inherits="DDE.Web.Admin.DistributeAmount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Distribute Amount of Instrument
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" >
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            
                            <td>
                                <b>Instrument No.</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbDCNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:DataList ID="dtlistTotalInstruments" Visible="false" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistTotalInstruments_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px; padding-left: 10px">
                                    <b>Type</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Date</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Total Amount</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Bank Name</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                     <asp:Label ID="lblVerified" runat="server" Text='<%#Eval("Verified")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                  
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblNo" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 140px">
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblIBN" runat="server" Text='<%#Eval("IBN")%>'></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Show Details" CommandName='<%#Eval("Distributed") %>'
                                        CommandArgument='<%#Eval("IID") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div>
                <asp:Panel ID="pnlDCDetail" runat="server" Visible="false">
                    <div class="data" style="padding-top: 0px" align="center">
                        <table class="tableStyle2" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <table width="388px" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="10px">
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT TYPE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDType" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT No.</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                            <asp:Label ID="lblIID" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT DATE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDCDate" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <b>ISSUING BANK NAME</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbIBN" runat="server" TextMode="MultiLine" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>TOTAL AMOUNT</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbTotalAmount" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>SC CODE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbSCCode" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <div>
                <div class="text" align="center" style="padding-top: 10px">
                    <asp:DataList ID="dtlistFeeHeads" CssClass="dtlist" RepeatColumns="2" RepeatDirection="Horizontal"
                        runat="server" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                        <HeaderTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 30px">
                                        <b>S.No.</b>
                                    </td>
                                    <td align="left" style="width: 250px; padding-left: 8px">
                                        <b>Fee Head</b>
                                    </td>
                                    <td align="left" style="width: 125px">
                                        <b>Amount</b>
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
                                    <td align="left" style="width: 250px">
                                        <%#Eval("FeeHead")%>
                                        <asp:Label ID="lblFHID" runat="server" Text='<%#Eval("FHID")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 125px">
                                        <asp:TextBox ID="tbAmount" runat="server" Text='<%#Eval("Amount")%>' Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div style="padding-top: 5px">
                    <asp:Panel ID="pnlBalance" runat="server" Visible="false">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td>
                                    Balance
                                </td>
                                <td>
                                    <asp:TextBox ID="tbBalance" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:LinkButton ID="lnkbtnBalance" runat="server" onclick="lnkbtnBalance_Click">Auto Calculate</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div align="center" style="padding-top:10px">
                    <table cellspacing="10px">
                        <tr>
                            <td>
                                <asp:Button ID="btnDistribute" runat="server" Visible="false" Text="" OnClick="btnDistribute_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td align="center" style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="padding-top: 20px">
        <asp:Button ID="btnOK" runat="server" Text="" Visible="false"  OnClick="btnOK_Click" />
    </div>
</asp:Content>
