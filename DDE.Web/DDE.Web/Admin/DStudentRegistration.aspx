<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="DStudentRegistration.aspx.cs" Inherits="DDE.Web.Admin.DStudentRegistration" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div align="center" class="heading" style="padding-bottom: 10px">
                    Register Student
                </div>
                <div align="center" style="padding-bottom: 5px">
                    <table cellspacing="10px" class="tableStyle1">
                        <tr>
                            <td valign="top" align="left">
                                <asp:RadioButtonList ID="rblEntryType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblEntryType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="1">Current Entry</asp:ListItem>
                                    <asp:ListItem Value="2">Back Log</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table align="center" width="800px" class="tableStyle2">
                                <tr>
                                    <td align="left">
                                        <table>
                                            <tr>
                                                <td valign="top" style="width: 150px">
                                                    <asp:Image ID="imsStudent" runat="server" Width="150px" Height="150px" BorderStyle="Solid"
                                                        BorderColor="#003f6f" BorderWidth="2px" />
                                                </td>
                                                <td style="padding-left: 10px">
                                                    <table align="center" cellspacing="10px">
                                                        <tr>
                                                            <td class="style1">
                                                                Application No *
                                                            </td>
                                                            <td valign="middle" style="color: Red" class="style1">
                                                                <asp:TextBox ID="tbANo" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please fill up this entry"
                                                                    ControlToValidate="tbANo"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Admission Through *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistAdmissionThrough" AutoPostBack="true" runat="server"
                                                                    Width="150px" OnSelectedIndexChanged="ddlistAdmissionThrough_SelectedIndexChanged">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem Value="1">DIRECT</asp:ListItem>
                                                                    <asp:ListItem Value="2">WEM</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Admission Type *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistAdmissionType" runat="server" Width="150px" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlistAdmissionType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem Value="1">REGULAR</asp:ListItem>
                                                                    <asp:ListItem Value="2">CREDIT TRANSFER</asp:ListItem>
                                                                    <asp:ListItem Value="3">LATERAL ENTRY</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Study Centre Code *
                                                            </td>
                                                            <td valign="middle" style="color: Red">
                                                                <asp:DropDownList ID="ddlistStudyCentre" AutoPostBack="true" runat="server" Width="150px"
                                                                    OnSelectedIndexChanged="ddlistStudyCentre_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                Transfered *
                                                            </td>
                                                            <td valign="top">
                                                                <div style="float: left; width: 150px">
                                                                    <asp:RadioButtonList ID="rblTrans" RepeatDirection="Horizontal" AutoPostBack="true"
                                                                        runat="server" OnSelectedIndexChanged="rblTrans_SelectedIndexChanged">
                                                                        <asp:ListItem>Yes</asp:ListItem>
                                                                        <asp:ListItem>No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div align="left" style="float: right; width: 250px">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblTrans" runat="server" Visible="false" Text="From"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 10px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlistTransSC" Visible="false" runat="server"
                                                                                    Width="150px" >
                                                                                    <asp:ListItem Value="0">--SELECT ONE--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Admission Session *
                                                            </td>
                                                            <td valign="middle" style="color: Red">
                                                                <asp:DropDownList ID="ddlistSession" Width="150px" runat="server" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Enrollment No.
                                                            </td>
                                                            <td valign="middle" style="color: Red">
                                                                <asp:TextBox ID="tbENo" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                I Card No.
                                                            </td>
                                                            <td valign="middle" style="color: Red">
                                                                <asp:TextBox ID="tbICNo" runat="server" Enabled="false" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Panel ID="pnlCT" runat="server" Visible="false">
                                                                    <table cellspacing="0px" class="tableStyle1">
                                                                        <tr>
                                                                            <td align="left" style="width: 198px; height: 30px">
                                                                                <b>Previous Institution</b>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlistPInst" runat="server" Width="150px" AutoPostBack="true">
                                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                                    <asp:ListItem Value="1">DDE (SVSU)</asp:ListItem>
                                                                                    <asp:ListItem Value="2">OTHER</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="width: 198px; height: 30px">
                                                                                <b>Previous Course</b>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlistPreCourse" runat="server" Width="150px" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="ddlistPreCourse_SelectedIndexChanged">
                                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPAF" runat="server" Text="Programme Applied For *"></asp:Label>
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistCourses" AutoPostBack="true" runat="server" Width="150px"
                                                                    OnSelectedIndexChanged="ddlistCourses_SelectedIndexChanged">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Syllabus Session *
                                                            </td>
                                                            <td valign="middle" style="color: Red">
                                                                <asp:DropDownList ID="ddlistSySession" Width="150px" Enabled="false" runat="server">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Year *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistCYear" runat="server" Width="150px" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlistCYear_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Student's Name *
                                                                <br />
                                                            </td>
                                                            <td valign="middle" style="color: Red">
                                                                <asp:TextBox ID="tbSName" runat="server" Width="220px"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="rfvSName" runat="server" ErrorMessage="Please fill up this entry" ControlToValidate="tbSName"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Father's Name *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:TextBox ID="tbFName" runat="server" Width="220px"></asp:TextBox><asp:RequiredFieldValidator
                                                                    ID="rfvFName" runat="server" ErrorMessage="Please fill up this entry" ControlToValidate="tbFName"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Mother's Name *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:TextBox ID="tbMName" runat="server" Width="220px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvMName" runat="server" ErrorMessage="Please fill up this entry"
                                                                    ControlToValidate="tbMName"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Gender *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistGender" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem Value="1">MALE</asp:ListItem>
                                                                    <asp:ListItem Value="2">FEMALE</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                Date of Birth *
                                                            </td>
                                                            <td style="color: Red" valign="top">
                                                                <table cellpadding="5" cellspacing="5">
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <asp:DropDownList ID="ddlistDOBDay" runat="server">
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
                                                                            <asp:DropDownList ID="ddlistDOBMonth" runat="server">
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
                                                                            <asp:DropDownList ID="ddlistDOBYear" runat="server">
                                                                                <asp:ListItem>1945</asp:ListItem>
                                                                                <asp:ListItem>1946</asp:ListItem>
                                                                                <asp:ListItem>1947</asp:ListItem>
                                                                                <asp:ListItem>1948</asp:ListItem>
                                                                                <asp:ListItem>1949</asp:ListItem>
                                                                                <asp:ListItem>1950</asp:ListItem>
                                                                                <asp:ListItem>1951</asp:ListItem>
                                                                                <asp:ListItem>1952</asp:ListItem>
                                                                                <asp:ListItem>1953</asp:ListItem>
                                                                                <asp:ListItem>1954</asp:ListItem>
                                                                                <asp:ListItem>1955</asp:ListItem>
                                                                                <asp:ListItem>1956</asp:ListItem>
                                                                                <asp:ListItem>1957</asp:ListItem>
                                                                                <asp:ListItem>1958</asp:ListItem>
                                                                                <asp:ListItem>1959</asp:ListItem>
                                                                                <asp:ListItem>1960</asp:ListItem>
                                                                                <asp:ListItem>1961</asp:ListItem>
                                                                                <asp:ListItem>1962</asp:ListItem>
                                                                                <asp:ListItem>1963</asp:ListItem>
                                                                                <asp:ListItem>1964</asp:ListItem>
                                                                                <asp:ListItem>1965</asp:ListItem>
                                                                                <asp:ListItem>1966</asp:ListItem>
                                                                                <asp:ListItem>1967</asp:ListItem>
                                                                                <asp:ListItem>1968</asp:ListItem>
                                                                                <asp:ListItem>1969</asp:ListItem>
                                                                                <asp:ListItem>1970</asp:ListItem>
                                                                                <asp:ListItem>1971</asp:ListItem>
                                                                                <asp:ListItem>1972</asp:ListItem>
                                                                                <asp:ListItem>1973</asp:ListItem>
                                                                                <asp:ListItem>1974</asp:ListItem>
                                                                                <asp:ListItem>1975</asp:ListItem>
                                                                                <asp:ListItem>1976</asp:ListItem>
                                                                                <asp:ListItem>1977</asp:ListItem>
                                                                                <asp:ListItem>1978</asp:ListItem>
                                                                                <asp:ListItem>1979</asp:ListItem>
                                                                                <asp:ListItem>1980</asp:ListItem>
                                                                                <asp:ListItem>1981</asp:ListItem>
                                                                                <asp:ListItem>1982</asp:ListItem>
                                                                                <asp:ListItem>1982</asp:ListItem>
                                                                                <asp:ListItem>1983</asp:ListItem>
                                                                                <asp:ListItem>1984</asp:ListItem>
                                                                                <asp:ListItem Selected="True">1985</asp:ListItem>
                                                                                <asp:ListItem>1986</asp:ListItem>
                                                                                <asp:ListItem>1987</asp:ListItem>
                                                                                <asp:ListItem>1988</asp:ListItem>
                                                                                <asp:ListItem>1989</asp:ListItem>
                                                                                <asp:ListItem>1990</asp:ListItem>
                                                                                <asp:ListItem>1991</asp:ListItem>
                                                                                <asp:ListItem>1992</asp:ListItem>
                                                                                <asp:ListItem>1993</asp:ListItem>
                                                                                <asp:ListItem>1994</asp:ListItem>
                                                                                <asp:ListItem>1995</asp:ListItem>
                                                                                <asp:ListItem>1996</asp:ListItem>
                                                                                <asp:ListItem>1997</asp:ListItem>
                                                                                <asp:ListItem>1998</asp:ListItem>
                                                                                <asp:ListItem>1999</asp:ListItem>
                                                                                <asp:ListItem>2000</asp:ListItem>
                                                                                <asp:ListItem>2001</asp:ListItem>
                                                                                <asp:ListItem>2002</asp:ListItem>
                                                                                <asp:ListItem>2003</asp:ListItem>
                                                                                <asp:ListItem>2004</asp:ListItem>
                                                                                <asp:ListItem>2005</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Permanent Address *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:TextBox ID="tbPAddress" runat="server" TextMode="MultiLine" Height="90px" Width="200px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPAddress" runat="server" ControlToValidate="tbPAddress"
                                                                    ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                State *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistState" AutoPostBack=true runat="server" 
                                                                    Width="150px" onselectedindexchanged="ddlistState_SelectedIndexChanged">                                                                   
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td class="style1">
                                                                District *
                                                            </td>
                                                            <td style="color: Red" class="style1">
                                                                <asp:DropDownList ID="ddlistDistrict" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td>
                                                                City/Town *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistCity" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        
                                                        
                                                        <tr>
                                                            <td>
                                                                Pin Code
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbPinCode" runat="server" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Phone No.( With STD Code )
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbPNo" runat="server" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Mobile No.
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbMNo" runat="server" Width="150px"></asp:TextBox>
                                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please Enter A Valid Mobile No."
                                                                    MaximumValue="99999999999" MinimumValue="7000000000" ControlToValidate="tbMNo"></asp:RangeValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                Email Address
                                                            </td>
                                                            <td valign="top">
                                                                <asp:TextBox ID="tbEAddress" runat="server" Width="150px"></asp:TextBox><asp:RegularExpressionValidator
                                                                    ID="revEmailAddress" runat="server" ErrorMessage="Please Enter A Valid Email Address"
                                                                    ControlToValidate="tbEAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                Aadhaar No. *
                                                                
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="tbAadhaarNo" runat="server" Width="150px"></asp:TextBox>(12 digit no. without space)<br />
                                                                <asp:RequiredFieldValidator ID="rfvAaNo" runat="server" ErrorMessage="Fill Aadhaar No." ControlToValidate="tbAadhaarNo"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="revAaNo" runat="server" ErrorMessage="Please fill valid 12 digit no." ControlToValidate="tbAadhaarNo" ValidationExpression="^[0-9]{10,12}$"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                Date of Admission *
                                                            </td>
                                                            <td style="color: Red" valign="top">
                                                                <table cellpadding="5" cellspacing="5">
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <asp:DropDownList ID="ddlistDOADay" Enabled="false" runat="server">
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
                                                                            <asp:DropDownList ID="ddlistDOAMonth" Enabled="false" runat="server">
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
                                                                            <asp:DropDownList ID="ddlistDOAYear" Enabled="false" runat="server">
                                                                                <asp:ListItem>2009</asp:ListItem>
                                                                                <asp:ListItem>2010</asp:ListItem>
                                                                                <asp:ListItem>2011</asp:ListItem>
                                                                                <asp:ListItem>2012</asp:ListItem>
                                                                                <asp:ListItem>2013</asp:ListItem>
                                                                                <asp:ListItem>2014</asp:ListItem>
                                                                                <asp:ListItem>2015</asp:ListItem>
                                                                                <asp:ListItem>2016</asp:ListItem> 
                                                                                <asp:ListItem>2016</asp:ListItem>
                                                                                <asp:ListItem>2017</asp:ListItem>
                                                                                <asp:ListItem>2018</asp:ListItem>
                                                                                <asp:ListItem>2019</asp:ListItem>
                                                                                <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                                            <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Nationality *
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistNationality" runat="server">
                                                                    <asp:ListItem>INDIAN</asp:ListItem>
                                                                    <asp:ListItem>NRI</asp:ListItem>
                                                                     <asp:ListItem>FOREIGN NATIONAL</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Category *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistCategory" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem>GENERAL</asp:ListItem>
                                                                    <asp:ListItem>O.B.C.</asp:ListItem>
                                                                    <asp:ListItem>S.C.</asp:ListItem>
                                                                    <asp:ListItem>S.T.</asp:ListItem>
                                                                    <asp:ListItem>N.R.I.</asp:ListItem>
                                                                    <asp:ListItem>FOREIGN NATIONAL</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Employment Status
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlsitEmploymentlist" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem>EMPLOYED</asp:ListItem>
                                                                    <asp:ListItem>UNEMPLOYED</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Eligible For Exam
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistExamination" ForeColor="Black" Enabled="false" runat="server"
                                                                    Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left" style="padding-left: 15px">
                                                    <asp:Label ID="lblFeeHeading" runat="server" Text="Details Of fee" Font-Bold="True"
                                                        Font-Size="Medium" ForeColor="#006600"></asp:Label>
                                                    <br />
                                                    <hr align="left" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center" style="padding: 10px">
                                                    <table align="center">
                                                        <tr>
                                                            <td align="center">
                                                                <table cellspacing="10px" class="tableStyle1">
                                                                    <tr>
                                                                        
                                                                        <td>
                                                                            Payment Mode
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlistPaymentMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistPaymentMode_SelectedIndexChanged"
                                                                                Width="150px">
                                                                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                                <asp:ListItem Value="1">DEMAND DRAFT</asp:ListItem>
                                                                                <asp:ListItem Value="2">CHEQUE</asp:ListItem>
                                                                                <asp:ListItem Value="3">CASH</asp:ListItem>
                                                                                <asp:ListItem Value="4">RTGS/NEFT</asp:ListItem>
                                                                                <asp:ListItem Value="5">DEDUCT FROM REFUND</asp:ListItem>
                                                                                <asp:ListItem Value="6">DIRECT CASH TRANSFER</asp:ListItem>
                                                                                <asp:ListItem Value="7">ADJUSTMENT AGAINST DISCOUNT</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Panel ID="pnlDDFee" runat="server" Visible="false">
                                                        <table align="center" class="tableStyle1" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="padding: 10px">
                                                                    <table align="center">
                                                                        <tr>
                                                                            <td align="center" valign="top">
                                                                                <table align="center">
                                                                                    <tbody align="left">
                                                                                        <tr>
                                                                                            <td style="height: 50px" valign="top">
                                                                                                <asp:Label ID="lblDCNumber" runat="server" Text="Instrument No."></asp:Label>
                                                                                            </td>
                                                                                            <td valign="top">
                                                                                                <asp:TextBox ID="tbDDNumber" runat="server" Width="230px"></asp:TextBox><br />
                                                                                                <asp:DropDownList ID="ddlistIns" runat="server" Visible="false" AutoPostBack="true"
                                                                                                    OnSelectedIndexChanged="ddlistIns_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                                <asp:LinkButton ID="lnkbtnFDCDetails" runat="server" CausesValidation="false" ForeColor="Black"
                                                                                                    OnClick="lnkbtnFDCDetails_Click">Fill DC Details</asp:LinkButton>
                                                                                                <br />
                                                                                                <asp:Label ID="lblNewDD" runat="server" ForeColor="Red" Text="" Visible="false"></asp:Label><asp:Label
                                                                                                    ID="lblIID" runat="server" Text="" Visible="false"></asp:Label>
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
                                                                                            <td style="height: 50px; width: 130px" valign="top">
                                                                                                <asp:Label ID="lblSAmount" runat="server" Text="Amount of Student"></asp:Label>
                                                                                            </td>
                                                                                            <td valign="top">
                                                                                                <asp:TextBox ID="tbStudentAmount" Enabled="false" runat="server" Width="141px" 
                                                                                                    ></asp:TextBox>
                                                                                                <br />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Fill This Entry"
                                                                                                    ControlToValidate="tbStudentAmount"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="height: 50px; width: 160px" valign="top">
                                                                                                <asp:Label ID="lblTotalAmount" runat="server" Text="Total Amount of Ins."></asp:Label>
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
                                                                        <asp:DropDownList ID="ddlistAcountsSession" Enabled="false" runat="server" 
                                                                            Width="150px" 
                                                                            >
                                                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <br />
                                                                        <br />
                                                                        <asp:Label ID="lblARDate" Font-Bold="false" runat="server" Text=""></asp:Label>
                                                                        <asp:Label ID="lblARPDate" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </td>
                                                                                            <td valign="top">
                                                                                                <asp:Label ID="lblIBN" runat="server" Text="Issuning Bank Name"></asp:Label>
                                                                                            </td>
                                                                                            <td valign="top">
                                                                                                <asp:TextBox ID="tbIBN" Enabled="false" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox><br />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Fill This Entry"
                                                                                                    ControlToValidate="tbSName"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <table class="text">
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="Label3" runat="server" Text="Details Of Educational Qualifications (from Matriculation onwards):"
                                            Font-Bold="True" Font-Size="Medium" ForeColor="#006600"></asp:Label>
                                        <br />
                                        <hr align="left" style="width: 700px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="padding-left: 10px">
                                        <table cellspacing="10px">
                                            <tbody align="center">
                                                <tr>
                                                    <td>
                                                        Examination
                                                    </td>
                                                    <td>
                                                        Subject
                                                    </td>
                                                    <td>
                                                        Year of Passing
                                                    </td>
                                                    <td>
                                                        University / Board
                                                    </td>
                                                    <td>
                                                        Division / Grade
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="lblExam1" ForeColor="Black" runat="server" Text="X" Enabled="false">
                                                        
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblSubject" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblYearpass" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblUniversityBoard" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblDivisiongrade" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="lblexam2" ForeColor="Black" runat="server" Text="XII" Enabled="false">
                                                     
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblsubject2" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblyearpass2" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblUniversityBoard2" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblDivision2" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="lblExam3" ForeColor="Black" runat="server" Text="UG" Enabled="false">
                                                        
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblSubject3" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblyearpass3" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblUniversityBoard3" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblDivision3" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="lblexam4" ForeColor="Black" runat="server" Text="PG" Enabled="false">
                                                       
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblsubject4" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblyearpass4" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblUniversityBoard4" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lbldivision4" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="lblexam5" ForeColor="Black" runat="server" Text="OTHER" Enabled="false">
                                                     
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblsubject5" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblyearpass5" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblUniversityBoard5" runat="server">
                                            
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lbldivision5" runat="server">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="padding-left: 20px; padding-top: 20px">
                                        <asp:Label ID="Label2" runat="server" Text="For Office Use Only" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#006600"></asp:Label>
                                        <br />
                                        <hr align="left" style="width: 700px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="center" style="padding-left: 10px">
                                                    <table cellspacing="10px">
                                                        <tr>
                                                            <td>
                                                                Eligible
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistEligible" runat="server">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem>YES</asp:ListItem>
                                                                    <asp:ListItem>NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                           <%-- <td>
                                                                Fee Receipt Issued
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistFeeRecIssued" runat="server" 
                                                                    >
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem>YES</asp:ListItem>
                                                                    <asp:ListItem>NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>--%>
                                                            <td>
                                                                Originals Verified
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistOriginalsVer" runat="server">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem>YES</asp:ListItem>
                                                                    <asp:ListItem>NO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                             <td colspan="3" align="right">
                                                                Admission Status
                                                            </td>
                                                            <td colspan="3" align="left">
                                                                <asp:DropDownList ID="ddlistAdmissionStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistAdmissionStatus_SelectedIndexChanged">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem Value="1">CONFIRM</asp:ListItem>
                                                                    <asp:ListItem Value="2">PENDING</asp:ListItem>
                                                                    <asp:ListItem Value="3">PROVISIONAL</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                       </tr>                                                  
                                                        <tr>
                                                            <td colspan="3" align="right">
                                                                <asp:Label ID="lblReason" runat="server" Visible="false" Text="Reason"></asp:Label>
                                                            </td>
                                                            <td colspan="3" align="left">
                                                                <asp:TextBox ID="tbReason" runat="server" Visible="false" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td align="center" colspan="1" style="padding: 20px">
                         <asp:Button ID="btnUpd" runat="server" Text="Update" 
                                Style="height: 26px" Visible="false" onclick="btnUpd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" OnClick="btnSubmit_Click"
                                Style="height: 26px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" Visible="false" Style="height: 26px" OnClick="btnReset_Click"
                                CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
                <table align="center" class="tableStyle2">
                    <tr>
                        <td align="center" style="padding: 30px">
                            <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                 <div style="padding-top: 10px">
                    <asp:Button ID="btnYes" runat="server" Text="YES" Visible="false" Width="60px" 
                         onclick="btnYes_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnNo" runat="server" Text="NO" Visible="false" Width="60px" 
                         onclick="btnNo_Click" />
                </div>
                <div style="padding-top: 0px">
                    <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlistPaymentMode" />
            <asp:AsyncPostBackTrigger ControlID="btnUpd"></asp:AsyncPostBackTrigger>      
            <asp:AsyncPostBackTrigger ControlID="lnkbtnFDCDetails"></asp:AsyncPostBackTrigger>
            <asp:PostBackTrigger ControlID="btnSubmit"></asp:PostBackTrigger>

        </Triggers> 
         <Triggers>  
           <asp:PostBackTrigger ControlID="btnSubmit" />       
        </Triggers>    
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="cphheadColleges">
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
</asp:Content>

