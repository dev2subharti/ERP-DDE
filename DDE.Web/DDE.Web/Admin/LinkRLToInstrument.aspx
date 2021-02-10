<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="LinkRLToInstrument.aspx.cs" Inherits="DDE.Web.Admin.LinkRLToInstrument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Pay Reimbursement
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>Letter No</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbLNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlLAndIDetails" runat="server" Visible="false">
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
                                        <asp:TextBox ID="tbTotalRefund" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
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
                                        <asp:TextBox ID="tbBalanceExtra" ForeColor="Black" Enabled="false" runat="server"></asp:TextBox>
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
                                        <asp:TextBox ID="tbBalanceShort" ForeColor="Black" Enabled="false" runat="server"></asp:TextBox>
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
                                        <asp:TextBox ID="tbNetRefund" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div align="center" class="text" style="padding-top: 20px">
                  <b>Details of Instrument to be paid</b>  
                    </div>
                    <div class="data"  style="padding-top: 10px" align="center">
                    <table class="tableStyle2" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td>
                                <table width="388px" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td valign="top">
                                            <table cellspacing="10px">
                                                <tr>
                                                    <td align="left">
                                                        <b>Instrument Type</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlistDraftType" runat="server" Width="190px" 
                                                           >
                                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            <asp:ListItem Value="1">DEMAND DRAFT</asp:ListItem>
                                                            <asp:ListItem Value="2">CHEQUE</asp:ListItem>                                                          
                                                            <asp:ListItem Value="4">RTGS</asp:ListItem>
                                                            <asp:ListItem Value="7">NEFT</asp:ListItem>                                                         
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>Instrument No.</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbDNo" runat="server" ForeColor="Black"></asp:TextBox>
                                                        <asp:Label ID="lblIID" runat="server" Visible="false"></asp:Label><br />
                                                        <asp:RequiredFieldValidator ID="rfvINo" runat="server" ControlToValidate="tbDNo" ErrorMessage="Please fill Ins. No."></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>Instrument Date</b>
                                                    </td>
                                                    <td valign="top">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlistDDDay" runat="server">
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
                                                                    <asp:DropDownList ID="ddlistDDMonth" runat="server">
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
                                                                    <asp:DropDownList ID="ddlistDDYear" runat="server">
                                                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                                        <asp:ListItem>2019</asp:ListItem>
                                                                    <asp:ListItem>2018</asp:ListItem>
                                                                    <asp:ListItem>2017</asp:ListItem>
                                                                    <asp:ListItem>2016</asp:ListItem>                                                                    
                                                                    <asp:ListItem>2015</asp:ListItem>
                                                                    <asp:ListItem>2014</asp:ListItem>                                                                                                                                           
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="left">
                                                        <b>Issuing Bank Name</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbIBN" runat="server" TextMode="MultiLine" ForeColor="Black"></asp:TextBox><br />
                                                        <asp:RequiredFieldValidator ID="rfvIBN" runat="server" ControlToValidate="tbIBN" ErrorMessage="Please fill IBN"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>Amount</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbTotalAmount" runat="server" ForeColor="Black"></asp:TextBox><br />
                                                        <asp:RequiredFieldValidator ID="rfvTA" runat="server" ControlToValidate="tbTotalAmount" ErrorMessage="Please fill amount"></asp:RequiredFieldValidator>
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
                <div style="padding-top:10px">
                    <asp:Button ID="btnSubmit" runat="server" Visible="false" Width="100px" Text="Pay" 
                        onclick="btnSubmit_Click" />
                
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
</asp:Content>
