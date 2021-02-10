<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowFeeRecord.aspx.cs" Inherits="DDE.Web.Admin.ShowFeeRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Show Fee Record
            </div>
            <div align="center" class="text">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td align="center" colspan="4">
                            <table cellspacing="10px" class="tableStyle1">
                                <tr>
                                    <td valign="top" align="left">
                                        <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">By All Dates</asp:ListItem>
                                            <asp:ListItem Value="2">By Date of Admission</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:RadioButtonList ID="rblAdmissionType" runat="server">
                                            <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">Credit Transfer</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:RadioButtonList ID="rblPhoto" runat="server">
                                            <asp:ListItem Selected="True">Without Photo</asp:ListItem>
                                            <asp:ListItem>With Photo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <table>
                                <tr>
                                    <td>
                                        <b>Mode</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistRecordMode" AutoPostBack="true" runat="server" 
                                            Width="150px" onselectedindexchanged="ddlistRecordMode_SelectedIndexChanged">
                                            <asp:ListItem>--Select One--</asp:ListItem>
                                            <asp:ListItem>ALL</asp:ListItem>
                                            <asp:ListItem>SUBMITTED</asp:ListItem>
                                            <asp:ListItem>VERIFIED</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Fee Head</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistFeeHead" AutoPostBack="true" runat="server" Width="150px"
                                OnSelectedIndexChanged="ddlistFeeHead_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblExamination" runat="server" Text="Exam" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExamination" runat="server" Width="150px" Visible="false">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>SC Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Batch</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Course</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                                <asp:ListItem Value="5">ALL</asp:ListItem>
                                <asp:ListItem Value="0">NOT APPLICABLE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 10px">
                <asp:Panel runat="server" ID="pnlDOA" Visible="false">
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
                                    <asp:ListItem Selected="True">2012</asp:ListItem>
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
                                    <asp:ListItem Selected="True">2012</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:Panel ID="pnlRecord" runat="server" Visible="false">
                    <table>
                        <tr>
                            <td>
                                <asp:DataList ID="dtlistShowRegistration" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowRegistration_ItemCommand">
                                    <HeaderTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 30px">
                                                    <b>S.No.</b>
                                                </td>
                                                <td align="left" style="width: 60px; padding-left: 8px">
                                                    <b>A.No.</b>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <b>Enroll. No.</b>
                                                </td>
                                                <td align="left" style="width: 160px">
                                                    <b>Student Name</b>
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <b>Course</b>
                                                </td>
                                                <td align="left" style="width: 90px">
                                                    <b>Req. Fee</b>
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <b>Late Fee</b>
                                                </td>
                                                <td align="left" style="width: 78px">
                                                    <b>Paid Fee</b>
                                                </td>
                                                <td align="left" style="width: 68px">
                                                    <b>MOP</b>
                                                </td>
                                                <td align="left" style="width: 160px">
                                                    <b>D/C No.</b>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <b>Total Paid</b>
                                                </td>
                                                <td align="center" style="width: 75px">
                                                    <b>Due Fee</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 40px">
                                                    <%#Eval("SNo")%>
                                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 60px">
                                                    <%#Eval("FormNo")%>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:LinkButton ID="lnkbtnENo" OnClientClick="return PostToNewWindow();" runat="server"
                                                        CommandName="ENo" CommandArgument='<%#Eval("SRID")%>'> <%#Eval("EnrollmentNo")%></asp:LinkButton>
                                                </td>
                                                <td align="left" style="width: 160px">
                                                    <%#Eval("StudentName")%>
                                                </td>
                                                <td align="left" style="width: 50px">
                                                    <%#Eval("Course")%>
                                                </td>
                                                <td align="right" style="width: 80px">
                                                    <%#Eval("ReqFee")%>
                                                </td>
                                                <td align="right" style="width: 80px">
                                                    <%#Eval("LateFee")%>
                                                </td>
                                                <td align="left" style="width: 305px">
                                                    <asp:DataList Width="100%" ID="dtlistPayDetail" runat="server">
                                                        <ItemTemplate>
                                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td align="right" style="width: 90px">
                                                                        <%#Eval("PaidFee")%>
                                                                    </td>
                                                                    <td align="left" style="width: 60px; padding-left: 30px">
                                                                        <%#Eval("MOP")%>
                                                                    </td>
                                                                    <td align="left" style="width: 110px">
                                                                        <asp:LinkButton ID="lnkbtnDCNo" PostBackUrl='<%#Eval("URL")%>' OnClientClick="return PostToNewWindow();"
                                                                            runat="server" CommandName="DCNo" CommandArgument='<%#Eval("DCNo")%>'> <%#Eval("DCNo")%></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                                <td align="right" style="width: 110px">
                                                    <%#Eval("TotalPaidFee")%>
                                                </td>
                                                <td align="right" style="width: 110px">
                                                    <%#Eval("DueFee")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <table width="100%" class="tableStyle2" cellspacing="10px">
                                    <tr>
                                        <td align="center" style="width: 370px">
                                            TOTAL
                                        </td>
                                        <td align="right" style="width: 96px">
                                            <asp:Label ID="lblTotalRF" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" style="width: 66px">
                                            <asp:Label ID="lblTotalLF" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" style="width: 70px">
                                        </td>
                                        <td align="right" style="width: 294px">
                                            <asp:Label ID="lblTotalFPF" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" style="width: 93px; padding-right: 6px">
                                            <asp:Label ID="lblTotalDF" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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

    <script type="text/javascript">
        function PostToNewWindow() {
            originalTarget = document.forms[0].target;
            document.forms[0].target = '_blank';
            window.setTimeout("document.forms[0].target=originalTarget;", 300);
            return true;
        }
    </script>

</asp:Content>
