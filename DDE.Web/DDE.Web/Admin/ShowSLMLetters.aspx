﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowSLMLetters.aspx.cs" Inherits="DDE.Web.Admin.ShowSLMLetters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show SLM Letters
        </div>
        <div>
            <div align="center" class="text" style="padding-top: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>SC Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" runat="server">
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
                </table>
            </div>
            <div style="padding-top:5px; padding-bottom:10px">
           
            
           
            <table class="tableStyle2" cellspacing="5px">
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
                           <asp:ListItem>2018</asp:ListItem>
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
            <div align="center" style="padding-bottom:20px">
             
                            <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                       
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowSLMLetters" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" 
                    onitemcommand="dtlistShowSLMLetters_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 60px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Letter No.</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>SC Code</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Total SLM</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Generated On</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Processed On</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>By</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>Docket No.</b>
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
                                    <%#Eval("LID")%>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("TotalSLM")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("PublishedOn")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("ProcessedOn")%>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <%#Eval("By")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("DocketNo")%>
                                </td>
                                <td align="center" style="width: 60px">
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Show" CommandName="Show" CommandArgument='<%#Eval("LID") %>'></asp:LinkButton>
                                </td>
                                 <td align="center" style="width: 90px">
                                    <asp:LinkButton ID="lnkbtnShowStudents" runat="server" Text="Student List" CommandName="StudentList" CommandArgument='<%#Eval("LID") %>'></asp:LinkButton>
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
