<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/StudyCentre.Master"
    CodeBehind="StudentRegistrationBySC.aspx.cs" Inherits="DDE.Web.Admin.StudentRegistrationBySC" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div align="center" class="heading" style="padding-bottom: 20px">
                    Register New Student
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
                                                            <td>
                                                                Batch *
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
                                                                Admission Type *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistAdmissionType" runat="server" Width="150px" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlistAdmissionType_SelectedIndexChanged">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem Value="1">REGULAR</asp:ListItem>
                                                                    <asp:ListItem Value="2">CREDIT TRANSFER</asp:ListItem>
                                                                </asp:DropDownList>
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
                                                                                    <asp:ListItem Value="2">OUT SIDE</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" style="width: 198px; height: 30px">
                                                                                <b>Previous Course</b>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlistPreCourse" runat="server" Width="150px" AutoPostBack="true">
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
                                                                <asp:DropDownList ID="ddlistCourses" runat="server" Width="150px">
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
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                                                    <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                                                    <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
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
                                                                City/Town *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistCity" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style1">
                                                                District *
                                                            </td>
                                                            <td style="color: Red" class="style1">
                                                                <asp:DropDownList ID="ddlistDistrict" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                State *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:DropDownList ID="ddlistState" runat="server" Width="150px">
                                                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                                    <asp:ListItem>UTTAR PRADESH</asp:ListItem>
                                                                    <asp:ListItem>DELHI</asp:ListItem>
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
                                                            <td>
                                                                Nationality *
                                                            </td>
                                                            <td style="color: Red">
                                                                <asp:TextBox ID="tbNationality" runat="server" Width="150px"></asp:TextBox>
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
                                                    </table>
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
                                    <td align="left" style="padding-left: 10px">
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
                                                        <asp:TextBox ID="lblExam1" runat="server">
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
                                                        <asp:TextBox ID="lblexam2" runat="server">
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
                                                        <asp:TextBox ID="lblExam3" runat="server">
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
                                                        <asp:TextBox ID="lblexam4" runat="server">
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
                                            </tbody>
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
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                Style="height: 26px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="Reset" Style="height: 26px" OnClick="btnReset_Click"
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
                <div style="padding-top: 20px">
                    <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlistPaymentMode" />
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
