<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowProcessedSLMLetters.aspx.cs" Inherits="DDE.Web.Admin.ShowProcessedSLMLetters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show Processed Letters (On Trial)
        </div>
        <div style="padding-top: 20px">
            <table class="tableStyle2" cellspacing="5px">
                <tr>
                    <td align="center" colspan="9">
                        <table  cellspacing="5px">
                            <tr>
                                <td>
                                    SC Code
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistSCCode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                                        <asp:ListItem>ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    &nbsp;
                                </td>
                                <td>
                                    Dispatch Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistDType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlistDType_SelectedIndexChanged">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem Value="1">COURIER</asp:ListItem>
                                        <asp:ListItem Value="2">BY HAND</asp:ListItem>
                                        <asp:ListItem Value="0">ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblDParty" runat="server" Text="Party" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistDParty" AutoPostBack="true" runat="server" Visible="false"
                                        OnSelectedIndexChanged="ddlistDParty_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td align="left">
                        <b>From</b>
                    </td>
                    <td valign="middle">
                        <asp:DropDownList ID="ddlistDOADayFrom" runat="server">
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
                        <asp:DropDownList ID="ddlistDOAMonthFrom" runat="server">
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
                        <asp:DropDownList ID="ddlistDOAYearFrom" runat="server">
                            <asp:ListItem>2009</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                            <asp:ListItem>2012</asp:ListItem>
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                           <asp:ListItem>2017</asp:ListItem> 
                            <asp:ListItem >2018</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem> 
                            <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10px">
                        &nbsp;
                    </td>
                    <td align="left">
                        <b>To</b>
                    </td>
                    <td valign="middle">
                        <asp:DropDownList ID="ddlistDOADayTo" runat="server">
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
                        <asp:DropDownList ID="ddlistDOAMonthTo" runat="server">
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
                        <asp:DropDownList ID="ddlistDOAYearTo" runat="server">
                            <asp:ListItem>2009</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                            <asp:ListItem>2012</asp:ListItem>
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                           <asp:ListItem>2017</asp:ListItem> 
                            <asp:ListItem >2018</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem> 
                            <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <div style="padding-top: 20px">
            <asp:Panel ID="pnlProcessedLetters" runat="server" Visible="false">
                <div align="center">
                    <asp:DataList ID="dtlistShowLetters" ShowFooter="true" runat="server" CellPadding="0"
                        CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS" FooterStyle-CssClass="dtlistfooterDS"
                        ItemStyle-CssClass="dtlistItemDS">
                        <HeaderTemplate>
                            <table align="left" cellspacing="0px" cellpadding="0px">
                                <tr>
                                    <td align="center" style="width: 40px; border-right: 1px solid black">
                                        <b>SNo</b>
                                    </td>
                                    <td align="center" style="width: 60px; border-right: 1px solid black">
                                        <b>LNo</b>
                                    </td>
                                    <td align="center" style="width: 84px; border-right: 1px solid black">
                                        <b>Letter Date</b>
                                    </td>
                                    <td align="center" style="width: 84px; border-right: 1px solid black">
                                        <b>Dispatch Date</b>
                                    </td>
                                    <td align="center" style="width: 60px; border-right: 1px solid black">
                                        <b>SC Code</b>
                                    </td>
                                    <td align="center" style="width: 130px; border-right: 1px solid black">
                                        <b>Party Name</b>
                                    </td>
                                    <td align="center" style="width: 210px; border-right: 1px solid black">
                                        <b>Address</b>
                                    </td>
                                    <td align="center" style="width: 121px; border-right: 1px solid black">
                                        <b>Total Pkt.<br />
                                            (Kg)</b>
                                    </td>
                                    <td align="center" style="width: 90px; border-right: 1px solid black">
                                        <b>Net Wt.<br />
                                            (Kg)</b>
                                    </td>
                                    <td align="center" style="width: 90px; border-right: 1px solid black">
                                        <b>Net Amt.<br />
                                            (Rs.)</b>
                                    </td>
                                    <td align="center" style="width: 90px; border-right: 1px solid black">
                                        <b>Dkt No.</b>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                                <tr>
                                    <td align="center" class="border_lbr" style="width: 29px">
                                        <%#Eval("SNo")%>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 50px">
                                        <asp:Label ID="lblLID" runat="server" Text='<%#Eval("LID")%>'></asp:Label>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 55px">
                                        <%#Eval("LDate")%>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 55px">
                                        <%#Eval("DDate")%>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 50px">
                                        <%#Eval("SCCode")%>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 120px">
                                        <%#Eval("PName")%>
                                    </td>
                                    <td align="left" class="border_rb" style="width: 200px">
                                        <%#Eval("Address")%>
                                    </td>
                                    <td>
                                        <asp:DataList ID="dtlistShowPackets" Height="100%" runat="server">
                                            <ItemTemplate>
                                                <table align="left" style="height: 100%" cellspacing="0px" cellpadding="0px">
                                                    <tr>
                                                        <td align="left" class="border_rb" style="width: 40px">
                                                            <%#Eval("PSNo")%>
                                                        </td>
                                                        <td align="left" class="border_rb" style="width: 60px">
                                                            <%#Eval("PWeight")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 80px">
                                        <%#Eval("NetWeight")%>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 80px">
                                        <%#Eval("NetAmount")%>
                                    </td>
                                    <td align="center" class="border_rb" style="width: 80px">
                                        <%#Eval("DocketNo")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div align="left" style="width: 1070px">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td align="center" style="width: 660px">
                                <b>Total</b>
                            </td>
                            <td align="left" style="width: 130px">
                                <asp:Label ID="lblTotalPkt" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="left" style="width: 75px">
                                <asp:Label ID="lblTotalWt" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="left" style="width: 155px">
                                <asp:Label ID="lblTotalCharge" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
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
    </asp:Panel>
</asp:Content>
