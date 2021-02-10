<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="FillFee.aspx.cs" Inherits="DDE.Web.Admin.FillFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlData" runat="server" Visible="false">
                    <div class="heading">
                        Fill Student Fee
                    </div>
                    <div>
                     <asp:Panel ID="pnlrbl" runat="server" Visible="false">
                        <table cellspacing="10px" class="tableStyle1">
                            <tr>
                                <td valign="top" align="left">
                                    <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">Enrollment No.</asp:ListItem>
                                        <asp:ListItem Value="2">Application No.</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </div>
                    <div style="padding-top: 20px; padding-bottom: 20px">
                        <asp:Panel ID="pnlFeeHead" runat="server" Visible="false">
                            <table cellspacing="10px" class="tableStyle2">
                                <tr>
                                    <td>
                                        <b>
                                            <asp:Label ID="lblNo" runat="server" Text="Enrollment No."></asp:Label></b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
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
                            <div>
                                <table cellspacing="10px" class="tableStyle1">
                                    <tr>
                                        <td valign="top" align="left">
                                            <asp:RadioButtonList ID="rblEntryType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblEntryType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="1">New Entry</asp:ListItem>
                                                <asp:ListItem Value="3">Re Entry</asp:ListItem>
                                                <asp:ListItem Value="2">Back Log</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="data" style="padding-top: 10px" align="center">
                                <table class="tableStyle2" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td valign="top" style="width: 50%; border-right: solid 2px #003f6f">
                                            <table cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td style="padding-left: 5px">
                                                        <asp:Image ID="imgStudent" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                                            runat="server" Width="100px" Height="120px" />
                                                    </td>
                                                    <td valign="top">
                                                        <table cellspacing="10px">
                                                            <tr>
                                                                <td align="left">
                                                                    <b>
                                                                        <asp:Label ID="lblNo1" runat="server" Text=""></asp:Label></b>
                                                                </td>
                                                                <td align="left">
                                                                    <b>:</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbEnNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>Student&#39;s Name</b>
                                                                </td>
                                                                <td align="left">
                                                                    <b>:</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbSName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    <asp:Label ID="lblSS" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>Father&#39;s Name</b>
                                                                </td>
                                                                <td align="left">
                                                                    <b>:</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbFName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>S.C. Code</b>
                                                                </td>
                                                                <td align="left">
                                                                    <b>:</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbSCCode" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>Course</b>
                                                                </td>
                                                                <td align="left">
                                                                    <b>:</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbCourse" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    <asp:Label ID="lblCID" runat="server" Text="" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <b>Year</b>
                                                                </td>
                                                                <td align="left">
                                                                    <b>:</b>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="tbYear" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top" align="center">
                                            <table cellspacing="10px" style="width: 100%">
                                                <tbody align="left">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistYear" runat="server" AutoPostBack="true" Width="150px"
                                                                OnSelectedIndexChanged="ddlistYear_SelectedIndexChanged">
                                                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

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
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblExamination" runat="server" Text="Examination" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistExamination" runat="server" Width="150px" AutoPostBack="true"
                                                                Visible="false" OnSelectedIndexChanged="ddlistExamination_SelectedIndexChanged">
                                                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                        <td>
                                                            <asp:Label ID="lblSSession" runat="server" Text="Syllabus Session" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistSyllabusSession" runat="server" Width="150px" AutoPostBack="true"
                                                                Visible="false" OnSelectedIndexChanged="ddlistSyllabussession_SelectedIndexChanged">
                                                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="padding-top: 5px">
                        <asp:Panel ID="pnlBPExamRecord" runat="server" Visible="false">
                            <table align="center" class="tableStyle2" cellpadding="0" cellspacing="0" style="width: 815px">
                                <tr>
                                    <td align="center" style="padding: 5px">
                                        <div style="padding: 5px">
                                            <b>Student is Re-Appearing in Subjects</b>
                                        </div>
                                        <table width="80%" border="1">
                                            <tbody align="left">
                                                <tr>
                                                    <td valign="top" align="center">
                                                        <asp:Label ID="lbl1Year" runat="server" Text="1 Year"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="center">
                                                        <asp:Label ID="lbl2Year" runat="server" Text="2 Year"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="center">
                                                        <asp:Label ID="lbl3Year" runat="server" Text="3 Year"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:CheckBoxList ID="cb1Year" runat="server">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:CheckBoxList ID="cb2Year" runat="server">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:CheckBoxList ID="cb3Year" runat="server">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <div style="padding-top: 20px">
                                            <b>Student is Re-Appearing in Practicals</b>
                                        </div>
                                        <table width="80%" border="1">
                                            <tbody align="left">
                                                <tr>
                                                    <td valign="top" align="center">
                                                        <asp:Label ID="lblP1Year" runat="server" Text="1 Year"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="center">
                                                        <asp:Label ID="lblP2Year" runat="server" Text="2 Year"></asp:Label>
                                                    </td>
                                                    <td valign="top" align="center">
                                                        <asp:Label ID="lblP3Year" runat="server" Text="3 Year"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:CheckBoxList ID="cbP1Year" runat="server">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:CheckBoxList ID="cbP2Year" runat="server">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:CheckBoxList ID="cbP3Year" runat="server">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div style="padding-top: 5px">
                        <asp:Panel ID="pnlMBAS" runat="server" Visible="false">
                            <table class="tableStyle2" cellspacing="10px">
                                <tr>
                                    <td>
                                        Set Specialization
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistMBAS" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSet" Width="60px" runat="server" Text="Set" OnClick="btnSet_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div style="padding-top: 5px; padding-bottom: 10px">
                        <asp:Button ID="btnFind" runat="server" Text="Find" Visible="false" CausesValidation="false"
                            OnClick="btnFind_Click" Width="75px" />
                    </div>
                    <div>
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
                                                <asp:ListItem>2020</asp:ListItem>
                                                <asp:ListItem>2021</asp:ListItem>
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
                                                                        <asp:Label ID="lblDCNumber" runat="server" Text="Instrument No."></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="tbDDNumber" runat="server" Width="200px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:DropDownList ID="ddlistIns" runat="server" Visible="false" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlistIns_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <asp:LinkButton ID="lnkbtnFDCDetails" runat="server" CausesValidation="false" ForeColor="Black"
                                                                            OnClick="lnkbtnFDCDetails_Click">Fill DC Details</asp:LinkButton>
                                                                        <br />
                                                                        <asp:Label ID="lblNewDD" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblIID" runat="server" Text="" Visible="false"></asp:Label>
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
                                                                                        <asp:ListItem>2020</asp:ListItem>
                                                                                        <asp:ListItem>2021</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 50px" valign="top">
                                                                        <asp:Label ID="lblSAmount" runat="server" Text="Amount of Student"></asp:Label>
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
                                                                        <asp:DropDownList ID="ddlistAcountsSession"  runat="server" Width="150px">
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
            <div>
                <table cellspacing="10px">
                    <tr>
                        <td>
                            <asp:Button ID="btnYes" runat="server" Text="Yes" Visible="false" Width="60px" OnClick="btnYes_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnNo" runat="server" Text="No" Visible="false" Width="60px" OnClick="btnNo_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 20px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
            </div>
            <div>
                <table cellspacing="0px">
                    <tr>
                        <td>
                            <asp:Button ID="btnSameFee" runat="server" Text="Submit Same Fee of this Draft" Visible="false"
                                OnClick="btnSameFee_Click" />
                        </td>
                        <td style="width: 20px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnNewFee" runat="server" Text="Submit New Fee" Visible="false" OnClick="btnNewFee_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
              <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnYes" />
            <asp:PostBackTrigger ControlID="btnNo" />
            <asp:PostBackTrigger ControlID="ddlistFeeHead" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
