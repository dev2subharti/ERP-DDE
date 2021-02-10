<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="EditExamRecord.aspx.cs" Inherits="DDE.Web.Admin.EditExamRecord" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
   
   
       
            <div align="center">
                <asp:Panel ID="pnlData" runat="server" Visible="false">
                    <div class="heading">
                        Edit Examination Record
                    </div>
                    <div style="padding-top: 20px; padding-bottom: 20px">
                        <asp:Panel ID="pnlFeeHead" runat="server" Visible="false">
                            <table cellspacing="10px" class="tableStyle2">
                                <tr>
                                    <td>
                                        <b>Enrollment No</b>
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
                            <div class="data" style="padding-top: 20px" align="center">
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
                                                                    <b>Enrollment No</b>
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
                                                            <asp:Label ID="lblExamMode" runat="server" Text="Examination Mode"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistExamMode" runat="server" Width="150px" AutoPostBack="true"
                                                                onselectedindexchanged="ddlistExamMode_SelectedIndexChanged" >
                                                                <asp:ListItem Value="0">--SELECT ONE--</asp:ListItem>
                                                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                                                <asp:ListItem Value="B">BACK PAPER</asp:ListItem>                                                              
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>                                            
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblExamination" runat="server" Text="Examination" ></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistExamination" runat="server" Width="150px"  AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlistExamination_SelectedIndexChanged">
                                                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px" OnSelectedIndexChanged="ddlistYear_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">--SELECT ONE--</asp:ListItem>
                                                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSSession" runat="server" Text="Syllabus Session" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlistSyllabusSession" runat="server" Width="150px" AutoPostBack="true"
                                                                Visible="false" OnSelectedIndexChanged="ddlistSyllabussession_SelectedIndexChanged">
                                                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
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
                       <div style="padding-top: 5px; padding-bottom: 10px">
                        <asp:Button ID="btnFind" runat="server" Text="Find" Visible="false" CausesValidation="false" OnClick="btnFind_Click"
                            Width="75px" />
                    </div>
                    <div style="padding-top: 5px">
                        <asp:Panel ID="pnlBPExamRecord" runat="server" Visible="false">
                            <table align="center" class="tableStyle2" cellpadding="0" cellspacing="0" style="width: 815px">
                                <tr>
                                    <td align="center" style="padding: 5px" class="style1">
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
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                 
                    <div>
                        <asp:Panel ID="pnlDDFee" runat="server" Visible="false">
                           <%-- <div style="padding-bottom: 5px">
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
                            </div>--%>
                          <%--  <div>
                                <table align="center" class="tableStyle2" cellpadding="0" cellspacing="0" style="width: 815px">
                                    <tr>
                                        <td align="center" style="padding: 5px">
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="center" valign="top">
                                                        <table align="center" width="100%">
                                                            <tbody align="left">
                                                                <tr>
                                                                    <td style="height: 50px; width: 130px" valign="top">
                                                                        <asp:Label ID="lblDCNumber" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="tbDDNumber" runat="server" Width="250px"></asp:TextBox><br />
                                                                        <asp:LinkButton ID="lnkbtnFDCDetails" runat="server" CausesValidation="false" ForeColor="Black"
                                                                            OnClick="lnkbtnFDCDetails_Click">Fill DC Details</asp:LinkButton>
                                                                        <br />
                                                                        <asp:Label ID="lblNewDD" runat="server" ForeColor="Red" Text="Sorry ! this is new  draft"
                                                                            Visible="false"></asp:Label>
                                                                        <br />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Fill This Entry"
                                                                            ControlToValidate="tbDDNumber"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:Label ID="lblDCDate" runat="server" Text=""></asp:Label>
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
                                                                                        <asp:ListItem>2010</asp:ListItem>
                                                                                        <asp:ListItem>2011</asp:ListItem>
                                                                                        <asp:ListItem>2012</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 50px; width: 120px" valign="top">
                                                                        <asp:Label ID="lblSAmount" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="tbStudentAmount" runat="server" Width="141px"></asp:TextBox>
                                                                        <br />
                                                                        <asp:LinkButton ID="lbSetAIW" runat="server" CausesValidation="false" ForeColor="Black"
                                                                            OnClick="lbSetAIW_Click">Fill AIW</asp:LinkButton>
                                                                        <br />
                                                                        <br />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Fill This Entry"
                                                                            ControlToValidate="tbStudentAmount"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td style="height: 50px" valign="top">
                                                                        <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="tbTotalAmount" runat="server" Width="141px"></asp:TextBox><br />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Fill This Entry"
                                                                            ControlToValidate="tbTotalAmount"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 50px" valign="top">
                                                                        <asp:Label ID="lblDCAIW" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="tbDDAmountInWords" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox><br />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Fill This Entry"
                                                                            ControlToValidate="tbDDAmountInWords"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:Label ID="lblIBN" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="tbIBN" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox><br />
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
                            </div>--%>
                            <div style="padding-top: 5px">
                                <asp:Panel ID="pnlExamRecord" runat="server" Visible="false">
                                    <table align="center" class="tableStyle2" cellpadding="0" cellspacing="0" style="width: 815px">
                                        <tr>
                                            <td align="center" style="padding: 5px">
                                                <table align="center" width="100%">
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <table align="center" style="width: 400px">
                                                                <tbody align="left">
                                                                    <tr>
                                                                        <td>
                                                                            Examination City
                                                                            <asp:Label ID="lblExamCentre" runat="server" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td valign="top">
                                                                            <asp:DropDownList ID="ddlistCity" runat="server" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td valign="top">
                                                                            Zone
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlistZone" runat="server" Width="50px">
                                                                                <asp:ListItem>A</asp:ListItem>
                                                                                <asp:ListItem>B</asp:ListItem>
                                                                                <asp:ListItem>C</asp:ListItem>
                                                                                <asp:ListItem>D</asp:ListItem>
                                                                                <asp:ListItem>E</asp:ListItem>
                                                                                <asp:ListItem>F</asp:ListItem>
                                                                                <asp:ListItem>G</asp:ListItem>
                                                                                <asp:ListItem>H</asp:ListItem>
                                                                                <asp:ListItem>I</asp:ListItem>
                                                                                <asp:ListItem>J</asp:ListItem>
                                                                                <asp:ListItem>K</asp:ListItem>
                                                                            </asp:DropDownList>
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
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="padding: 10px">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
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
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="cphheadColleges">

    <style type="text/css">
        .style1
        {
            height: 84px;
        }
    </style>

</asp:Content>

