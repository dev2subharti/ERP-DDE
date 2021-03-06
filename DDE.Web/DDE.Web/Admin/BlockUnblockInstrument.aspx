﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="BlockUnblockInstrument.aspx.cs" Inherits="DDE.Web.Admin.BlockUnblockInstrument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Block / Unblock Instrument
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
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
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                    <asp:Label ID="lblTypeNo" runat="server" Visible="false" Text='<%#Eval("TypeNo")%>'></asp:Label>
                                    <asp:Label ID="lblRemark" runat="server" Visible="false" Text='<%#Eval("LockRemark")%>'></asp:Label>
                                      <asp:Label ID="lblVerified" runat="server" Visible="false" Text='<%#Eval("Verified")%>'></asp:Label>
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
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Show Details" CommandName="Show"
                                        CommandArgument='<%#Eval("SNo") %>'></asp:LinkButton>
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
                                                            <b>DRAFT TYPE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlistPaymentMode" runat="server" Enabled="false" BackColor="White" Width="150px">
                                                                <asp:ListItem Value="1">DEMAND DRAFT</asp:ListItem>
                                                                <asp:ListItem Value="2">CHEQUE</asp:ListItem>
                                                                <asp:ListItem Value="3">CASH</asp:ListItem>
                                                                <asp:ListItem Value="4">RTGS</asp:ListItem>
                                                                <asp:ListItem Value="5">DEDUCT FROM REFUND</asp:ListItem>
                                                                <asp:ListItem Value="6">DIRECT CASH TRANSFER</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblIID" runat="server" Visible="false" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>DRAFT No.</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDNo" runat="server" Enabled="false" BackColor="White" ForeColor="Black"></asp:TextBox>
                                                            <asp:Label ID="lblDNo" runat="server" Visible="false" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>DRAFT DATE</b>
                                                        </td>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlistDDDay" Enabled="false" BackColor="White" runat="server">
                                                                            <asp:ListItem>01</asp:ListItem>
                                                                            <asp:ListItem>02</asp:ListItem>
                                                                            <asp:ListItem>03</asp:ListItem>
                                                                            <asp:ListItem>04</asp:ListItem>
                                                                            <asp:ListItem>05</asp:ListItem>
                                                                            <asp:ListItem>06</asp:ListItem>
                                                                            <asp:ListItem>07</asp:ListItem>
                                                                            <asp:ListItem>08</asp:ListItem>
                                                                            <asp:ListItem>09</asp:ListItem>
                                                                            <asp:ListItem>10</asp:ListItem>
                                                                            <asp:ListItem>11</asp:ListItem>
                                                                            <asp:ListItem>12</asp:ListItem>
                                                                            <asp:ListItem>13</asp:ListItem>
                                                                            <asp:ListItem>14</asp:ListItem>
                                                                            <asp:ListItem>15</asp:ListItem>
                                                                            <asp:ListItem>16</asp:ListItem>
                                                                            <asp:ListItem>17</asp:ListItem>
                                                                            <asp:ListItem>18</asp:ListItem>
                                                                            <asp:ListItem>19</asp:ListItem>
                                                                            <asp:ListItem>20</asp:ListItem>
                                                                            <asp:ListItem>21</asp:ListItem>
                                                                            <asp:ListItem>22</asp:ListItem>
                                                                            <asp:ListItem>23</asp:ListItem>
                                                                            <asp:ListItem>24</asp:ListItem>
                                                                            <asp:ListItem>25</asp:ListItem>
                                                                            <asp:ListItem>26</asp:ListItem>
                                                                            <asp:ListItem>27</asp:ListItem>
                                                                            <asp:ListItem>28</asp:ListItem>
                                                                            <asp:ListItem>29</asp:ListItem>
                                                                            <asp:ListItem>30</asp:ListItem>
                                                                            <asp:ListItem>31</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlistDDMonth" Enabled="false" BackColor="White" runat="server">
                                                                            <asp:ListItem Value="01">JANUARY</asp:ListItem>
                                                                            <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                                                                            <asp:ListItem Value="03">MARCH</asp:ListItem>
                                                                            <asp:ListItem Value="04">APRIL</asp:ListItem>
                                                                            <asp:ListItem Value="05">MAY</asp:ListItem>
                                                                            <asp:ListItem Value="06">JUNE</asp:ListItem>
                                                                            <asp:ListItem Value="07">JULY</asp:ListItem>
                                                                            <asp:ListItem Value="08">AUGUST</asp:ListItem>
                                                                            <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                                                                            <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                                                                            <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                                                            <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlistDDYear" Enabled="false" BackColor="White" runat="server">
                                                                            <asp:ListItem>2010</asp:ListItem>
                                                                            <asp:ListItem>2011</asp:ListItem>
                                                                            <asp:ListItem>2012</asp:ListItem>
                                                                            <asp:ListItem>2013</asp:ListItem>
                                                                            <asp:ListItem>2014</asp:ListItem>
                                                                            <asp:ListItem>2015</asp:ListItem>
                                                                            <asp:ListItem>2016</asp:ListItem>
                                                                            <asp:ListItem>2017</asp:ListItem>
                                                                            <asp:ListItem>2018</asp:ListItem>
                                                                            <asp:ListItem>2019</asp:ListItem>
                                                                            <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <b>ISSUING BANK NAME</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbIBN" runat="server" TextMode="MultiLine" BackColor="White" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>TOTAL AMOUNT</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbTotalAmount" runat="server" Enabled="false" BackColor="White" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>USED AMOUNT</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbUsedAmount" runat="server" Enabled="false" BackColor="White" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>BALANCE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbBalance" runat="server" Enabled="false" BackColor="White" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>REMARK</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbRemark" TextMode="MultiLine" Height="100px"  runat="server" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                           &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <asp:LinkButton ID="lnkbtnUR" runat="server" Visible="false" 
                                                                onclick="lnkbtnUR_Click">Update Remark</asp:LinkButton>
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
                    <div style="padding-top: 10px; padding-bottom: 0px">
                        <asp:Button ID="btnLock" runat="server" Text="Lock"  Width="120px"
                            OnClick="btnLock_Click" />
                    </div>
                </asp:Panel>
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
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" OnClick="btnOK_Click" />
    </div>
</asp:Content>
