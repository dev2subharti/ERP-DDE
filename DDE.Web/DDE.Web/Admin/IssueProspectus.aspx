<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="IssueProspectus.aspx.cs" Inherits="DDE.Web.Admin.IssueProspectus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Issue Prospectus
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
                                                        <asp:DropDownList ID="ddlistFeeHead" runat="server" Width="190px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlistFeeHead_SelectedIndexChanged">
                                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            <asp:ListItem Value="11">STUDENT (Rs. 125)</asp:ListItem>
                                                            <asp:ListItem Value="31">AF CENTRE(Rs. 100)</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <b>PAYMENT MODE</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlistPaymentMode" runat="server" Width="190px">
                                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
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
                                                    <td align="left" valign="top">
                                                        <b>SC CODE</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbSCCode" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td valign="top" align="left">
                                                        <b>TOTAL PROSPECTUS</b>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbTotalPP" runat="server" ForeColor="Black"></asp:TextBox><br />
                                                        <asp:RequiredFieldValidator ID="rfvTPP" runat="server" ControlToValidate="tbTotalPP"
                                                            ErrorMessage="Please fill total prospectus"></asp:RequiredFieldValidator>
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
                <asp:Button ID="btnFind" runat="server" Text="Find" onclick="btnFind_Click" />
            </div>
            <div style="padding-top: 10px">
                <asp:Panel ID="pnlDDFee" runat="server" Visible="false">
                    <div style="padding-bottom: 5px">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td>
                                    Form Receiving Date
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistFRDDay" runat="server" Enabled="false">
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
                                    <asp:DropDownList ID="ddlistFRDMonth" runat="server" Enabled="false">
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
                                    <asp:DropDownList ID="ddlistFRDYear" runat="server" Enabled="false">
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
                                <td>
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" OnClick="lnkbtnEdit_Click" CausesValidation="false">Edit</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding-bottom: 5px">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td>
                                    Fee Required
                                </td>
                                <td>
                                    <asp:TextBox ID="tbReqFee" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table align="center" class="tableStyle2" cellpadding="0" cellspacing="0" style="width: 815px">
                            <tr>
                                <td align="center" style="padding: 5px">
                                    <table align="center" width="100%">
                                        <tr>
                                            <td align="center" valign="top">
                                                <table align="center" width="100%">
                                                    <tbody align="left">
                                                        <tr>
                                                            <td style="height: 50px; width: 180px" valign="top">
                                                                <asp:Label ID="lblDCNumber" runat="server" Text="Instrument No."></asp:Label><br />
                                                                <asp:Label ID="lblIID" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="tbDDNumber" runat="server" Width="200px"></asp:TextBox><br />
                                                                <asp:DropDownList ID="ddlistIns" runat="server" Visible="false" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlistIns_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:LinkButton ID="lnkbtnFDCDetails" runat="server" CausesValidation="false" ForeColor="Black"
                                                                    OnClick="lnkbtnFDCDetails_Click">Fill DC Details</asp:LinkButton>
                                                                <br />
                                                                <asp:Label ID="lblNewDD" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Fill This Entry"
                                                                    ControlToValidate="tbDDNumber"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:Label ID="lblDCDate" runat="server" Text="Instrument Date"></asp:Label>
                                                            </td>
                                                            <td valign="top">
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
                                                            <td style="height: 50px" valign="top">
                                                                <asp:Label ID="lblSAmount" runat="server" Text="Paying Amount"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="tbStudentAmount" Enabled="false" runat="server" Width="141px"></asp:TextBox>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Fill This Entry"
                                                                    ControlToValidate="tbStudentAmount"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="height: 50px" valign="top">
                                                                <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount of Inst."></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="tbTotalAmount" Enabled="false" runat="server" Width="141px"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Fill This Entry"
                                                                    ControlToValidate="tbTotalAmount"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 50px" valign="top">
                                                                Account Session
                                                            </td>
                                                            <td valign="top">
                                                                <asp:DropDownList ID="ddlistAcountsSession" Enabled="false" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="lblARDate" Font-Bold="false" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblARPDate" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:Label ID="lblIBN" runat="server" Text="Issuing Bank Name"></asp:Label>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="tbIBN" runat="server" Enabled="false" TextMode="MultiLine" Width="250px"></asp:TextBox><br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Fill This Entry"
                                                                    ControlToValidate="tbIBN"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding: 10px">
                        <asp:Button ID="btnIP" runat="server" Text="Issue Propectus" Visible="false" 
                            onclick="btnIP_Click" />
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
        <asp:Button ID="btnOK" runat="server" Text="" Visible="false" OnClick="btnOK_Click" />
    </div>
</asp:Content>
