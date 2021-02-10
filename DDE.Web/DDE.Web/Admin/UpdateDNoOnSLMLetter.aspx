<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="UpdateDNoOnSLMLetter.aspx.cs" Inherits="DDE.Web.Admin.UpdateDNoOnSLMLetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Fill Docket No. on SLM Letters (On Trial)
        </div>
        <div style="padding-top: 20px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td align="center" colspan="9">
                        <table class="tableStyle2" cellspacing="5px">
                            <tr>
                                <td>
                                    SC Code
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistSCCode" runat="server">
                                        <asp:ListItem>ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
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
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" />
        </div>
        <div style="padding-top: 20px">
            <div align="center">
                <asp:DataList ID="dtlistShowLetters" runat="server" CellPadding="0" CellSpacing="0"
                    HeaderStyle-CssClass="dtlistheaderDS" ItemStyle-CssClass="dtlistItemDS">
                    <HeaderTemplate>
                        <table align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_r" style="width: 40px">
                                    <b>SNo</b>
                                </td>
                                <td align="center" class="border_r" style="width: 60px">
                                    <b>LNo</b>
                                </td>
                                <td align="center" class="border_r" style="width: 84px">
                                    <b>Letter Date</b>
                                </td>
                                <td align="center" class="border_r" style="width: 84px">
                                    <b>Dispatch Date</b>
                                </td>
                                <td align="center" class="border_r" style="width: 60px">
                                    <b>SC Code</b>
                                </td>
                                <td align="center" class="border_r" style="width: 90px">
                                    <b>Net Wt.</b>
                                </td>
                                <td align="center" class="border_r" style="width: 90px">
                                    <b>Net Amt.</b>
                                </td>
                                <td align="center" style="width: 155px">
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
                                <td align="center" class="border_rb" style="width: 80px">
                                    <%#Eval("NetWeight")%>
                                </td>
                                <td align="center" class="border_rb" style="width: 80px">
                                    <%#Eval("NetAmount")%>
                                </td>
                                <td align="center" class="border_rb" style="width: 80px">
                                    <asp:TextBox ID="tbDNo" runat="server" Text='<%#Eval("DocketNo")%>'></asp:TextBox>
                                    <asp:Label ID="lblDNo" runat="server" Visible="false" Text='<%#Eval("DocketNo")%>'></asp:Label>
                                        
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top:10px" align="center">
                <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Submit" 
                    onclick="btnSubmit_Click" />           
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
