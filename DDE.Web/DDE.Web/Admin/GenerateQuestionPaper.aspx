<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="GenerateQuestionPaper.aspx.cs" Inherits="DDE.Web.Admin.GenerateQuestionPaper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
<%-- <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl1" runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Set Question Paper
        </div>
        <div>
            <div align="center" class="text" style="padding-top: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td align="left">
                            Examination
                        </td>
                        <td align="left" colspan="5">
                            <asp:DropDownList ID="ddlistExam" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                  
                    <tr>
                        <td align="left">
                            <b>Date</b>
                        </td>
                        <td align="left" colspan="5">
                            <table>
                                <tr>
                                    <td valign="middle">
                                        <asp:DropDownList ID="ddlistDay" runat="server">
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
                                        <asp:DropDownList ID="ddlistMonth" runat="server">
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
                                        <asp:DropDownList ID="ddlistYear" runat="server">
                                            <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Shift</b>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rblShift" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Value="1">1ST</asp:ListItem>
                                <asp:ListItem Value="2">2ND</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Randome Range</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbFrom" runat="server" Text="9" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="tbTo" runat="server" Text="20" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding: 10px">
                <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" OnClick="btnFind_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowDS" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS">
                    <HeaderTemplate>
                        <table align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 100px">
                                    <b>Date</b>
                                </td>
                                <td align="center" style="width: 160px">
                                    <b>Time</b>
                                </td>
                                <td align="center" style="width: 120px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="center" style="width: 300px">
                                    <b>Title of Paper</b>
                                </td>
                                <td align="center" style="width: 70px">
                                    <b>Year</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 80px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowSubjects" ItemStyle-Wrap="true" runat="server">
                                        <ItemTemplate>
                                            <table align="left" cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="center" class="border_rb" style="width: 160px">
                                                        <%#Eval("Time")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 100px">
                                                        <asp:Label ID="lblPaperCode" runat="server" Text='<%#Eval("PaperCode")%>'></asp:Label>
                                                         <asp:Label ID="lblPaperSet" runat="server" Text='<%#Eval("PaperSet")%>' Visible="false"></asp:Label>
                                                         <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("SubjectName")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 70px">
                                                        <%#Eval("Year")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 100px">
                                                        <asp:LinkButton ID="lnkbtnSet" runat="server" Text="Generate" OnClick="lnkbtnSet_Click" ToolTip='<%#Eval("PaperCode") %>' CommandName=' <%#Eval("Year")%>' CommandArgument='<%#Eval("SySession") %>'></asp:LinkButton>
                                                    </td>
                                                     <td align="center" class="border_rb" style="width: 100px">
                                                        <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Show" OnClick="lnkbtnShow_Click" ToolTip='<%#Eval("PaperCode") %>' CommandName=' <%#Eval("Year")%>' CommandArgument='<%#Eval("SySession") %>'></asp:LinkButton>
                                                    </td>
                                                     <td align="center" class="border_rb" style="width: 100px">
                                                        <asp:Button ID="btnStatus" runat="server" Text='<%#Eval("Status")%>' OnClick="btnStatus_Click"  CommandArgument='<%#Eval("PaperCode") %>'></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
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
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Visible="false" Text="OK" Width="50px" OnClick="btnOK_Click" />
        </div>
    </asp:Panel>
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
