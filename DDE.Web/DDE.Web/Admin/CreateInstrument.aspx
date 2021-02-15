<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="CreateInstrument.aspx.cs" Inherits="DDE.Web.Admin.CreateInstrument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Receive Fee Payment Instrument
            </div>
            <div style="padding-top: 20px">
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
                                                        <b>INSTRUMENT TYPE</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlistDraftType" runat="server" Width="190px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlistDraftType_SelectedIndexChanged">
                                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            <asp:ListItem Value="1">DEMAND DRAFT</asp:ListItem>
                                                            <asp:ListItem Value="2">CHEQUE</asp:ListItem>
                                                            <asp:ListItem Value="3">CASH</asp:ListItem>
                                                            <asp:ListItem Value="4">RTGS</asp:ListItem>
                                                            <asp:ListItem Value="5">DEDUCT FROM REFUND</asp:ListItem>
                                                            <asp:ListItem Value="6">DIRECT CASH TRANSFER</asp:ListItem>
                                                            <asp:ListItem Value="7">ADJUSTMENT AGAINST DISCOUNT</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>INSTRUMENT No.</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbDNo" runat="server" ForeColor="Black"></asp:TextBox>
                                                        <asp:Label ID="lblIID" runat="server" Visible="false"></asp:Label><br />
                                                        <asp:RequiredFieldValidator ID="rfvINo" runat="server" ControlToValidate="tbDNo" ErrorMessage="Please fill Ins. No."></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>INSTRUMENT DATE</b>
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" align="left">
                                                        <b>ISSUING BANK NAME</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlistIBN" runat="server"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>TOTAL AMOUNT</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbTotalAmount" runat="server" ForeColor="Black"></asp:TextBox><br />
                                                        <asp:RequiredFieldValidator ID="rfvTA" runat="server" ControlToValidate="tbTotalAmount" ErrorMessage="Please fill total amount"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <b>SC CODE</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbSCCode" runat="server" TextMode="MultiLine"></asp:TextBox> 
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td align="left" valign="top">
                                                        <b>PRO SC CODE</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbProSCCode" runat="server" TextMode="MultiLine"></asp:TextBox> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>REMARK</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlistRemark" runat="server">
                                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            <asp:ListItem Value="1">60%</asp:ListItem>
                                                            <asp:ListItem Value="2">100%</asp:ListItem>
                                                            <asp:ListItem Value="3">BOTH</asp:ListItem>
                                                            <asp:ListItem Value="4">CONCESSION</asp:ListItem>
                                                            <asp:ListItem Value="5">NA</asp:ListItem>
                                                            <asp:ListItem Value="6">REMAINING COURSE FEE</asp:ListItem>
                                                        </asp:DropDownList>
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
                <div class="text" align="center" style="padding-top: 10px">
                    <asp:DataList ID="dtlistFeeHeads" CssClass="dtlist" RepeatColumns="2" RepeatDirection="Horizontal"
                        runat="server" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                        <HeaderTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 30px">
                                        <b>S.No.</b>
                                    </td>
                                    <td align="left" style="width: 250px; padding-left: 8px">
                                        <b>Fee Head</b>
                                    </td>
                                    <td align="left" style="width: 125px">
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
                                    <td align="left" style="width: 250px">
                                        <%#Eval("FeeHead")%>
                                        <asp:Label ID="lblFHID" runat="server" Text='<%#Eval("FHID")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 30px">
                                        <asp:CheckBox ID="cbChecked" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div align="center" style="padding-top: 20px">
                <asp:Button ID="btnReceive" runat="server" Text="Receive" OnClick="btnReceive_Click" />
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
        <asp:Button ID="btnOK" runat="server" Text="" Visible="false" OnClick="btnOK_Click" />
    </div>
</asp:Content>
