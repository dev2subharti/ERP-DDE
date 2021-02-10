<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowDegreeRequests.aspx.cs" Inherits="DDE.Web.Admin.ShowDegreeRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            Show Degree Requests
        </div>
        <div style="padding-top: 20px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td>Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistType" Width="150px" AutoPostBack="true"
                            runat="server" OnSelectedIndexChanged="ddlistType_SelectedIndexChanged">
                            <asp:ListItem Value="1">REQUESTED</asp:ListItem>
                            <asp:ListItem Value="2">DL PUBLISHED</asp:ListItem>
                            <asp:ListItem Value="3">CL PUBLISHED</asp:ListItem>
                            <asp:ListItem Value="4">RECEIVED</asp:ListItem>
                            <asp:ListItem Value="5">POSTED</asp:ListItem>
                            <asp:ListItem Value="0">ALL</asp:ListItem>
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
                                <asp:ListItem>2014</asp:ListItem>
                                <asp:ListItem>2015</asp:ListItem>
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                                <asp:ListItem>2020</asp:ListItem>
                                <asp:ListItem Selected="True">2021</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10px">&nbsp;
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
                                <asp:ListItem>2020</asp:ListItem>
                                <asp:ListItem Selected="True">2021</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="padding-top: 10px">

            <asp:Button ID="btnSearch" runat="server" Text="Search"
                OnClick="btnSearch_Click" />

        </div>
        <div style="padding: 20px">
            <div align="center">
                <asp:DataList ID="dtlistShowDegreeInfo" CssClass="dtlist" Visible="false"
                    runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem"
                    OnItemCommand="dtlistShowDegreeInfo_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>SNo.</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>DLNo</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>CLNo</b>
                                </td>
                                <%-- <td align="left" style="width: 100px">
                                    <b>Published</b>
                                </td>--%>
                                <td align="left" style="width: 100px">
                                    <b>Received</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Posted</b>
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
                                <td align="left" style="width: 140px">
                                    <%#Eval("EnrollmentNo")%>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblDIID" runat="server" Text='<%#Eval("DIID")%>' Visible="false"></asp:Label>

                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("SName")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <asp:Label ID="lblDL" runat="server" Text='<%#Eval("DLNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 80px">
                                    <asp:Label ID="lblCL" runat="server" Text='<%#Eval("CLNo")%>'></asp:Label>
                                </td>
                                <%-- <td align="center" style="width: 100px">
                                    <asp:Label ID="lblPublished" runat="server" Text='<%#Eval("Published")%>' Visible="false"></asp:Label>
                                    <asp:Image ID="imgPubY" runat="server" Visible="false" Height="25px" Width="25px" ImageUrl="~/Admin/images/Y1.jpg" />
                                    <asp:Image ID="imgPubN" runat="server" Visible="false" Height="25px" Width="25px" ImageUrl="~/Admin/images/N1.jpg" />
                                </td>--%>
                                <td align="left" style="width: 100px">
                                    <asp:Label ID="lblReceived" runat="server" Text='<%#Eval("Received")%>' Visible="false"></asp:Label>
                                    <asp:Image ID="imgRecY" runat="server" Visible="false" Height="25px" Width="25px" ImageUrl="~/Admin/images/Y1.jpg" />
                                    <asp:Image ID="imgRecN" runat="server" Visible="false" Height="25px" Width="25px" ImageUrl="~/Admin/images/N1.jpg" />
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Label ID="lblPosted" runat="server" Text='<%#Eval("Posted")%>' Visible="false"></asp:Label>
                                    <asp:Image ID="imgPostY" runat="server" Visible="false" Height="25px" Width="25px" ImageUrl="~/Admin/images/Y1.jpg" />
                                    <asp:Image ID="imgPostN" runat="server" Visible="false" Height="25px" Width="25px" ImageUrl="~/Admin/images/N1.jpg" />
                                </td>
                                <td align="left" style="width: 40px">
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                </td>
                                <td align="center" style="width: 120px">
                                    <asp:Button ID="btnPublish" runat="server" Text="Publish Letter" CommandName="Publish" CommandArgument='<%#Eval("DIID")%>' />
                                </td>
                                <td align="center" style="width: 60px">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="50px" CommandName="Edit" CommandArgument='<%#Eval("DIID")%>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>

            <div style="padding-top: 10px">
                <asp:Button ID="btnPubCoveringLetter" runat="server" Visible="false"
                    Text="Publish Covering Letter" OnClick="btnPubCoveringLetter_Click" />
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
