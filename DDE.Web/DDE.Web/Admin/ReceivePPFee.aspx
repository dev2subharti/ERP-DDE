<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ReceivePPFee.aspx.cs" Inherits="DDE.Web.Admin.ReceivePPFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Receive Instrument (Prospectus Fee Only Type)
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
                                                        <b>PROSPECTUS TYPE</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlistFeeHead" runat="server" Width="190px" 
                                                            AutoPostBack="true" onselectedindexchanged="ddlistFeeHead_SelectedIndexChanged"
                                                           >
                                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            <asp:ListItem Value="11">STUDENT (Rs. 125)</asp:ListItem>
                                                            <asp:ListItem Value="31">AF CENTRE(Rs. 100)</asp:ListItem>
                                                          
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>INSTRUMENT TYPE</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlistDraftType" runat="server" Width="190px" Enabled="false">                                                                        
                                                            <asp:ListItem Value="3">CASH</asp:ListItem>                                                         
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>INSTRUMENT No.</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbDNo" runat="server" ForeColor="Black"></asp:TextBox><br />
                                                      
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
                                                                       <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td valign="top" align="left">
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
                                                
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
