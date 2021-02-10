<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="AddStudyCentre.aspx.cs" Inherits="DDE.Web.Admin.AddStudyCentre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading" style="padding-bottom: 20px">
            Study Centre Registration
        </div>
        <div>
            <table align="center" width="600px" class="tableStyle2">
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td style="padding-top: 10px">
                                    <table align="center" cellpadding="10" cellspacing="10" class="text">
                                        <tbody align="left">
                                            <tr>
                                                <td>
                                                    Mode *
                                                </td>
                                                <td valign="middle" style="color: Red">
                                                    <asp:DropDownList ID="ddlistMode" AutoPostBack="true" runat="server" 
                                                        onselectedindexchanged="ddlistMode_SelectedIndexChanged">
                                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                        <asp:ListItem Value="1">CONFIRMED</asp:ListItem>
                                                        <asp:ListItem Value="0">PROVISIONAL</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Study Centre Code *
                                                </td>
                                                <td valign="middle" style="color: Red">
                                                    <asp:TextBox ID="tbSCCode" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSCCode" runat="server" ErrorMessage="Please fill up this entry" ControlToValidate="tbSCCode"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Name of Study Centre *
                                                </td>
                                                <td valign="middle" style="color: Red">
                                                    <asp:TextBox ID="tbSCName" TextMode="MultiLine" Height="90px" Width="200px" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvSCName" runat="server" ErrorMessage="Please fill up this entry" ControlToValidate="tbSCName"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Recommanded By
                                                </td>
                                                <td style="color: Red">
                                                    <asp:DropDownList ID="ddlistRBy" runat="server">
                                                        <asp:ListItem>UNIVERSITY</asp:ListItem>
                                                        <asp:ListItem>WEM</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Administrator *
                                                </td>
                                                <td valign="middle" style="color: Red">
                                                    <asp:TextBox ID="tbAdmin" runat="server" Width="250px"></asp:TextBox><br />
                                                    <asp:RequiredFieldValidator ID="rfvAdmin" runat="server" ErrorMessage="Please fill up this entry"
                                                        ControlToValidate="tbAdmin"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Allowed
                                                </td>
                                                <td style="color: Red">
                                                    <asp:DropDownList ID="ddlistAllowed" runat="server">
                                                        <asp:ListItem>YES</asp:ListItem>
                                                        <asp:ListItem>NO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Address *
                                                </td>
                                                <td style="color: Red">
                                                    <asp:TextBox ID="tbAddress" runat="server" TextMode="MultiLine" Height="90px" Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPAddress" runat="server" ControlToValidate="tbAddress"
                                                        ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    City *
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlistCity" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    District *
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlistDistrict" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Area
                                                </td>
                                                <td style="color: Red">
                                                    <asp:DropDownList ID="ddlistArea" runat="server">
                                                        <asp:ListItem Value="U">URBEN</asp:ListItem>
                                                        <asp:ListItem Value="R">RURAL</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Mobile No.
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbMNo" runat="server"></asp:TextBox><br />
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please Enter A Valid Mobile No."
                                                        MaximumValue="99999999999" MinimumValue="7000000000" ControlToValidate="tbMNo"></asp:RangeValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 200px">
                                                    Landline No.( With STD Code )
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbPNo" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Email Address
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="tbEAddress" runat="server" Width="250px"></asp:TextBox><br />
                                                    <asp:RegularExpressionValidator ID="revEmailAddress" runat="server" ErrorMessage="Please Enter A Valid Email Address"
                                                        ControlToValidate="tbEAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Date of Issue
                                                </td>
                                                <td style="color: #003f6f" valign="top">
                                                    <asp:TextBox ID="tbDate" runat="server"></asp:TextBox><br />
                                                    (dd-mm-yyyy)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Account No. *
                                                </td>
                                                <td style="color: #003f6f" valign="top">
                                                    <asp:TextBox ID="tbANo" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvANo" runat="server" ErrorMessage="Please fill up this entry"
                                                        ControlToValidate="tbANo"></asp:RequiredFieldValidator>                                              
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                   Bank Name *
                                                </td>
                                                <td style="color: #003f6f" valign="top">
                                                    <asp:TextBox ID="tbBankName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvBankName" runat="server" ErrorMessage="Please fill up this entry"
                                                        ControlToValidate="tbBankName"></asp:RequiredFieldValidator>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    IFSC Code *
                                                </td>
                                                <td style="color: #003f6f" valign="top">
                                                    <asp:TextBox ID="tbIFSC" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvIFSC" runat="server" ErrorMessage="Please fill up this entry"
                                                        ControlToValidate="tbIFSC"></asp:RequiredFieldValidator>
                                                 
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
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle2">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
