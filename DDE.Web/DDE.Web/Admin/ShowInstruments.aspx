<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowInstruments.aspx.cs" Inherits="DDE.Web.Admin.ShowInstruments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding-bottom: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Show Instruments
            </div>
            <div align="center" style="padding: 0px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            Instrument Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistIType" runat="server">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem>RECEIVED</asp:ListItem>
                                <asp:ListItem>VERIFIED</asp:ListItem>
                                <asp:ListItem>DISTRIBUTED</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>SC Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlistSCCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 10px">
                <asp:Panel runat="server" ID="pnlDOA">
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
                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div align="right" style="padding-top: 10px; float:left; width:53%">
                <asp:Button ID="btnSearch" CssClass="btn" runat="server" Text="Search" OnClick="btnSearch_Click" />
                
            </div>
            <div style="float:right; padding: 10px; width:45%px">
                 <asp:Button ID="btnSelectAll" CssClass="btn"  runat="server" Text="Select All" 
                     onclick="btnSelectAll_Click" />
            </div>
            <div align="center" style="padding-top:50px">
                <asp:DataList ID="dtlistShowDrafts" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" 
                    onitemcommand="dtlistShowDrafts_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 40px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 90px; padding-left: 10px">
                                    <b>Type</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>RG</b>
                                </td>
                                <td align="left" style="width: 250px">
                                    <b>No.</b>
                                </td>
                                <td align="left" style="width: 60px">
                                    <b>AF</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Date</b>
                                </td>                             
                                <td align="left" style="width: 100px">
                                    <b>Amount</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Balance</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>On</b>
                                </td>
                                 <td align="left" style="width: 120px">
                                    <b>Remark</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>By</b>
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
                                <td align="left" style="width: 90px">
                                    <%#Eval("Type")%>
                                </td>
                                <td align="left" style="width: 50px">
                                    <%#Eval("RG")%>
                                </td>
                                <td align="left" style="width: 10px">
                                    <asp:Label ID="lblVerification" runat="server" ForeColor="Black" BackColor="Orange"
                                        Text='<%#Eval("VStatus")%>' Width="10px"></asp:Label>
                                </td>
                                <td align="left" style="width: 20px">
                                    <asp:Label ID="lblDistribution" Text='<%#Eval("DStatus")%>' ForeColor="White" BackColor="Red"
                                        Width="10px" runat="server"></asp:Label>
                                </td>
                                <td align="left" style="width: 250px">
                                    <%#Eval("DCNumber")%>
                                </td>
                                <td align="left" style="width: 60px">
                                    <%#Eval("SCCode")%>
                                     <asp:Label ID="lblSCMode" Text='<%#Eval("SCMode")%>' runat="server" Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("DCDate")%>
                                </td>
                                
                                <td align="left" style="width: 100px">
                                    <%#Eval("DCAmount")%>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <%#Eval("Balance")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("VOn")%>
                                </td>
                                  <td align="left" style="width: 120px">
                                    <%#Eval("LockRemark")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("VBy")%>
                                </td>
                                <td align="left" style="width: 70px">
                                     <asp:LinkButton ID="lnkbtnEdit" CommandArgument='<%#Eval("IID")%>' Visible="false" CommandName="Edit" runat="server">Edit</asp:LinkButton>
                                </td>                                                             
                                <td align="left" style="width: 30px">
                                    <asp:Label ID="lblIID" runat="server" Text='<%#Eval("IID")%>' Visible="false"></asp:Label>
                                    <asp:CheckBox ID="cbChecked" runat="server" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div>
                <asp:CheckBox ID="cbAdjustment" runat="server" CssClass="tableStyle2" Visible="false" AutoPostBack="true"
                    Text="Add Adjustment" OnCheckedChanged="cbAdjustment_CheckedChanged" />
                <asp:Panel ID="pnlAdjustment" runat="server" Visible="false">
                    <table class="tableStyle2">
                        <tr>
                            <td>
                                Letter No.
                            </td>
                            <td>
                                <asp:TextBox ID="tbLNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Date.
                            </td>
                            <td>
                                <asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Amount
                            </td>
                            <td>
                                <asp:TextBox ID="tbAmount" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnPublish" runat="server" CssClass="btn"  Text="Publish Letter" Visible="false"
                    OnClick="btnPublish_Click" />
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
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" OnClick="btnOK_Click" />
        </div>
    </asp:Panel>
</asp:Content>
