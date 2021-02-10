<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="RefundByINo.aspx.cs" Inherits="DDE.Web.Admin.RefundByINo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="smrefund" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnlrefund" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlData" runat="server" Visible="false">
                    <div class="heading">
                        Show Refund of Instrument
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
                                             <asp:Label ID="lbIID" runat="server" Visible="false" Text='<%#Eval("IID")%>'></asp:Label>
                                        </td>
                                        <td align="left" style="width: 150px">
                                            <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                            <asp:Label ID="lblTypeNo" runat="server" Visible="false" Text='<%#Eval("TypeNo")%>'></asp:Label>
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
                                        <td align="center" style="width: 120px">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Generate Refund" CommandName="Show"
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
                                                                    <b>INSTRUMENT No.</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbDNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    <asp:Label ID="lblIID" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>INSTRUMENT TYPE</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlistPaymentMode" runat="server" Width="150px" Enabled="false">
                                                                        <asp:ListItem Value="1">DEMAND DRAFT</asp:ListItem>
                                                                        <asp:ListItem Value="2">CHEQUE</asp:ListItem>
                                                                        <asp:ListItem Value="3">CASH</asp:ListItem>
                                                                        <asp:ListItem Value="4">RTGS</asp:ListItem>
                                                                        <asp:ListItem Value="5">DEDUCT FROM REFUND</asp:ListItem>
                                                                        <asp:ListItem Value="6">DIRECT CASH TRANSFER</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>INSTRUMENT DATE</b>
                                                                </td>
                                                                <td align="left">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlistDDDay" Enabled="false" runat="server">
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
                                                                                <asp:DropDownList ID="ddlistDDMonth" Enabled="false" runat="server">
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
                                                                                <asp:DropDownList ID="ddlistDDYear" Enabled="false" runat="server">
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
                                                                    <b>USED AMOUNT</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbUsedAmount" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>BALANCE</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbBalance" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
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
                            <%--<div style="padding-top: 10px; padding-bottom: 0px">
                        <asp:Button ID="btnUpdateDD" CssClass="btn" Visible="false" runat="server" Text="Update Draft Detail"
                            CausesValidation="false" OnClick="btnUpdateDD_Click" />
                    </div>--%>
                            <%-- <div style="padding-top: 10px; padding-bottom: 0px">
                        <asp:Button ID="btnFindTransactions" runat="server" Text="Show All Transactions"
                            CausesValidation="false" OnClick="btnFindTransactions_Click" />
                    </div>--%>
                        </asp:Panel>
                    </div>
                    <div style="padding-top: 5px">
                        <asp:Panel ID="pnlTransactions" runat="server" Visible="false">
                            <div>
                                <table class="tableStyle2" cellpadding="10px" cellspacing="10px">
                                    <tr>
                                        <td>
                                            Batch
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistBatch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistBatch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Study Centre
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistSCCode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Course
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlistCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="text" align="center" style="padding-top: 10px">
                                <asp:DataList ID="dtlistShowTransactions" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                    ItemStyle-CssClass="dtlistItem">
                                    <HeaderTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 30px">
                                                    <b>S.No.</b>
                                                </td>
                                                <td align="left" style="width: 125px">
                                                    <b>Enrollment No.</b>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <b>Student Name</b>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <b>Father Name</b>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <b>Course</b>
                                                </td>
                                                <td align="left" style="width: 50px">
                                                    <b>Year</b>
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <b>Req.Fee</b>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <b>Paid Fee</b>
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <b>Fee (%)</b>
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <b>Refund</b>
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
                                                <td align="left" style="width: 125px">
                                                    <%#Eval("EnrollmentNo")%>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <%#Eval("StudentName")%>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <%#Eval("FatherName")%>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <%#Eval("Course")%>
                                                </td>
                                                <td align="left" style="width: 50px">
                                                    <%#Eval("Year")%>
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <%#Eval("ReqFee")%>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                <%#Eval("Trans")%>
                                                    <asp:Label ID="lblPaidFee" runat="server" Visible="false" Text='<%#Eval("PaidFee")%>'></asp:Label>    
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <%#Eval("FeePer")%>
                                                </td>
                                                <td align="left" style="width: 80px">
                                                    <asp:Label ID="lblRefund" runat="server" Width="80px"  Text='<%#Eval("Refund")%>'></asp:Label>
                                                   <%#Eval("Col")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div align="center" style="padding-top: 10px">
                                <table class="tableStyle2" cellpadding="0px" cellspacing="10px">
                                    <tbody align="left">
                                        <tr>
                                            <td>
                                                1
                                            </td>
                                            <td>
                                                Reimbursement to Centre
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbTotalRefund" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                2
                                            </td>
                                            <td>
                                                Balance to be Pay/Extra
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbBalanceExtra" runat="server" Text="0"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                3
                                            </td>
                                            <td>
                                                Balance to be Received/Short
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbBalanceShort" runat="server" Text="0"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                4
                                            </td>
                                            <td>
                                                Net Payable to Centre
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbNetRefund" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div align="center" style="padding-top: 10px">
                                <asp:Button ID="btnPublish" runat="server" Text="Publish" Visible="false" OnClick="btnPublish_Click" />
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
                <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlistBatch" />
            <asp:PostBackTrigger ControlID="ddlistSCCode" />
            <asp:PostBackTrigger ControlID="ddlistCourse" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="dtlistTotalInstruments" />
            <asp:PostBackTrigger ControlID="btnOK" />
            <asp:PostBackTrigger ControlID="btnCalculate" />
            <asp:PostBackTrigger ControlID="btnPublish" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
