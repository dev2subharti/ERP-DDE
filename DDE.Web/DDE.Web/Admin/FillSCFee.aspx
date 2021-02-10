<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="FillSCFee.aspx.cs" Inherits="DDE.Web.Admin.FillSCFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlData" runat="server" Visible="false">
                    <div class="heading">
                        Fill Study Centre Fee (Under Testing)
                    </div>
                    <div style="padding-top: 20px; padding-bottom: 20px">
                        <asp:Panel ID="pnlFeeHead" runat="server" Visible="false">
                            <table cellspacing="10px" class="tableStyle2">
                                <tr>
                                    <td>
                                        SC Code
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbSCCode" runat="server"></asp:TextBox>
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
                        <asp:Panel ID="pnlStudentDetail" runat="server" Visible="false">
                            <div class="data" style="padding-top: 10px" align="center">
                                <table class="tableStyle2" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td valign="top" style="width: 50%; border-right: solid 2px #003f6f">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td valign="top">
                                                        <table cellspacing="10px">
                                                            <tbody valign="top">
                                                                <tr>
                                                                    <td align="left">
                                                                        SC Code
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbSCCode1" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                        <asp:Label ID="lblSCID" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        SC Name
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbSName" runat="server" TextMode="MultiLine" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        City
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbCity" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" align="center">
                                            <table cellspacing="10px" style="width: 100%">
                                                <tbody align="left" valign="top">
                                                    
                                                    <tr>
                                                        <td align="left">
                                                            <b>Fee Head</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlistFeeHead" runat="server" AutoPostBack="true" Width="150px"
                                                                OnSelectedIndexChanged="ddlistFeeHead_SelectedIndexChanged">
                                                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Payment Mode
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistPaymentMode" runat="server" OnSelectedIndexChanged="ddlistPaymentMode_SelectedIndexChanged"
                                                                Width="150px">
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
                                                  
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <div>
                        <asp:Panel ID="pnlStream" runat="server" Visible="false">
                            <asp:DataList ID="dtlistStream" runat="server">
                                <ItemTemplate>
                                    <table rules="cols" width="100%" class="tableStyle2" style="font-weight: lighter">
                                        <tr>
                                            <td style="width: 30px; padding-left: 5px">
                                                <%#Eval("SNo") %>
                                                <asp:Label ID="lblSID" runat="server" Text='<%#Eval("StreamID") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblSFee" runat="server" Text='<%#Eval("StreamFee") %>' Visible="false"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 400px; padding-left: 5px">
                                                <%#Eval("StreamName") %>
                                            </td>
                                            <td align="center" style="width: 30px">
                                                <asp:CheckBox ID="cbStream" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </asp:Panel>
                    </div>
                    <div style="padding-top: 5px; padding-bottom: 10px">
                        <asp:Button ID="btnFind" runat="server" Text="Find" Visible="false" CausesValidation="false"
                            OnClick="btnFind_Click" Width="75px" />
                    </div>
                    <div>
                        <asp:Panel ID="pnlRPeriod" runat="server" Visible="false">
                            <table class="tableStyle2" cellpadding="10px" cellspacing="10px">
                                <tr>
                                    <td>
                                        Renewal Extended For
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlistRSession" runat="server">
                                            <asp:ListItem>2016</asp:ListItem><asp:ListItem>2017</asp:ListItem>
                                           
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        Renewal Validity
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    From
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="rfDay" runat="server">
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
                                                    <asp:DropDownList ID="rfMonth" runat="server">
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
                                                <td align="left" style="width: 80px">
                                                    <asp:DropDownList ID="rfYear" runat="server"> 
                                                        <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                    <asp:ListItem>2019</asp:ListItem>                                                    
                                                     <asp:ListItem>2018</asp:ListItem>
                                                      <asp:ListItem>2017</asp:ListItem> 
                                                      <asp:ListItem>2016</asp:ListItem>
                                                       <asp:ListItem>2015</asp:ListItem>
                                                         <asp:ListItem>2014</asp:ListItem>
                                                           <asp:ListItem>2013</asp:ListItem>
                                                            <asp:ListItem>2012</asp:ListItem>
                                                             <asp:ListItem>2011</asp:ListItem>
                                                        <asp:ListItem>2010</asp:ListItem>
                                                     
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    To
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="rtDay" runat="server">
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
                                                    <asp:DropDownList ID="rtMonth" runat="server">
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
                                                    <asp:DropDownList ID="rtYear" runat="server">
                                                        <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                    <asp:ListItem>2019</asp:ListItem>
                                                       <asp:ListItem>2018</asp:ListItem>
                                                       <asp:ListItem>2017</asp:ListItem> 
                                                       <asp:ListItem>2016</asp:ListItem>
                                                       <asp:ListItem>2015</asp:ListItem>
                                                         <asp:ListItem>2014</asp:ListItem>
                                                           <asp:ListItem>2013</asp:ListItem>
                                                            <asp:ListItem>2012</asp:ListItem>
                                                             <asp:ListItem>2011</asp:ListItem>
                                                        <asp:ListItem>2010</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
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
                                <table class="tableStyle2" cellspacing="10px" style="width: 815px">
                                    <tr>
                                        <td>
                                            Fee Required
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbReqFee" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            Fee Paid
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbPaidFee" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            Due Fee
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbDueFee" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
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
                                                                        <asp:Label ID="lblCon" runat="server" Visible="false" Text="Concession (%)"></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="tbCon" runat="server" Enabled="false" Visible="false" Width="50px"></asp:TextBox>&nbsp;
                                                                        <asp:LinkButton ID="linkbtnFillCon" CausesValidation="false" ForeColor="Blue" Font-Bold="false"
                                                                            runat="server" OnClick="linkbtnFillCon_Click">Fill Concession</asp:LinkButton>
                                                                        <br />
                                                                        <asp:RequiredFieldValidator ID="rfvtbCon" runat="server" ErrorMessage="Please Fill This Entry"
                                                                            ControlToValidate="tbCon"></asp:RequiredFieldValidator>
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
                           
                        </asp:Panel>
                    </div>
                    <div align="center">
                        <asp:Panel ID="pnlUnlockCon" runat="server" Visible="false">
                            <table class="tableStyle2">
                                <tr>
                                <td>
                                User Name
                                </td>
                                    <td>
                                        <asp:TextBox ID="tbUName" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                    Password
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbPass" TextMode="Password" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUnlock" runat="server" Text="Unlock" CausesValidation="false"
                                            OnClick="btnUnlock_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        <asp:Label ID="lblMSGCon" runat="server" ForeColor="Red" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div style="padding: 10px">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit Fee" Visible="false" OnClick="btnSubmit_Click" />
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
            <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="ddlistFeeHead" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
