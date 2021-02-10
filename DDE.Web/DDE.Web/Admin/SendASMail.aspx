<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="SendASMail.aspx.cs" Inherits="DDE.Web.Admin.SendASMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center" class="heading">
                Send Admission Status Mail
            </div>
            <div style="padding-top: 10px">
                <div style="padding-top: 10px">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td align="left">
                                Batch
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistBatch" runat="server" AutoPostBack="true" 
                                    onselectedindexchanged="ddlistBatch_SelectedIndexChanged">
                                <asp:ListItem>ALL</asp:ListItem>
                                  
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                Year
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistYear" runat="server" AutoPostBack="true" onselectedindexchanged="ddlistYear_SelectedIndexChanged" 
                                    >
                                <asp:ListItem>ALL</asp:ListItem>
                                    <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                    <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                    <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                   
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div>
                <div style="padding-top: 10px">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td align="left">
                                <b>From</b>
                            </td>
                            <td valign="middle">
                                <asp:DropDownList ID="ddlistDayFrom" runat="server">
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
                                <asp:DropDownList ID="ddlistMonthFrom" runat="server">
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
                                <asp:DropDownList ID="ddlistYearFrom" runat="server">
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
                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10px">
                                &nbsp;
                            </td>
                            <td align="left">
                                <b>To</b>
                            </td>
                            <td valign="middle">
                                <asp:DropDownList ID="ddlistDayTo" runat="server">
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
                                <asp:DropDownList ID="ddlistMonthTo" runat="server">
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
                                <asp:DropDownList ID="ddlistYearTo" runat="server">
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
                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="75px" Height="26px"
                        OnClick="btnSearch_Click" />
                </div>
                <div>
                    <asp:Panel ID="pnlReport" runat="server" Visible="false">
                        <div align="center" style="padding-top: 20px">
                            <asp:DataList ID="dtlistStudentList" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                ItemStyle-CssClass="dtlistItem" 
                                onitemcommand="dtlistStudentList_ItemCommand">
                                <HeaderTemplate>
                                    <table align="left">
                                        <tr>
                                            <td align="left" style="width: 50px">
                                                <b>SNo</b>
                                            </td>
                                            <td align="left" style="width: 90px">
                                                <b>SC Code</b>
                                            </td>
                                            <td align="left" style="width: 320px">
                                                <b>Email ID</b>
                                            </td>
                                            <td align="left" style="width: 130px">
                                                <b>Total Students</b>
                                            </td>
                                            <td align="left" style="width: 100px">
                                                <b>Confirmed</b>
                                            </td>
                                            <td align="left" style="width: 80px">
                                                <b>Pending</b>
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
                                            <td align="left" style="width: 80px">
                                                <asp:Label ID="lblSCCode" runat="server" Text='<%#Eval("SCCode")%>'></asp:Label>
                                            </td>
                                            <td align="left" style="width: 300px">
                                                <asp:Label ID="lblEmailID" runat="server" Text='<%#Eval("EmailID")%>'></asp:Label>
                                            </td>
                                            <td align="center" style="width: 140px">
                                                <asp:LinkButton ID="lnkbtnTStudents" ForeColor="Blue" runat="server" Text='<%#Eval("TStudents")%>'
                                                    CommandName="TASR" CommandArgument='<%#Eval("SRIDS")%>'></asp:LinkButton>
                                            </td>
                                            <td align="center" style="width: 100px">
                                                <%#Eval("Confirmed")%>
                                            </td>
                                            <td align="center" style="width: 80px">
                                                <%#Eval("Pending")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div align="center">
                            <div align="right" class="tableStyle2" style="width: 525px; padding-right: 260px;
                                padding-top: 5px; padding-bottom: 5px">
                                <asp:Label ID="lblTotal" runat="server" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div align="center" style="padding-top: 10px">
                            <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" 
                                onclick="btnSendMail_Click" />
                        </div>
                    </asp:Panel>
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
</asp:Content>
