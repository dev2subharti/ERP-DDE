<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="RegisterExaminer.aspx.cs" Inherits="DDE.Web.Admin.RegisterExaminer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading" style="padding-bottom: 20px">
            Register Examiner
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
                                                <td style="width:150px" >
                                                    Examination*
                                                </td>
                                                <td valign="middle" style="color: Red" >
                                                    <asp:DropDownList ID="ddlistExamination" Width="150px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        <tr>
                                                <td >
                                                    Exam Centre City *
                                                </td>
                                                <td valign="middle" style="color: Red" >
                                                    <asp:DropDownList ID="ddlistCity" Width="150px" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                         <tr>
                                             <td valign="top">
                                                 Exam Centre Code
                                             </td>
                                             <td style="color: Red" valign="middle">
                                                 <asp:TextBox ID="tbECCode" runat="server"></asp:TextBox>
                                             </td>
                                             </tr>
                                            <tr>
                                                <td valign="top">
                                                    Contact Person *
                                                </td>
                                                <td style="color: Red" valign="middle">
                                                    <asp:TextBox ID="tbContactPerson" runat="server" Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSCName" runat="server" 
                                                        ControlToValidate="tbContactPerson" ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Contact No *
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbCNo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCNo" runat="server" 
                                                        ControlToValidate="tbCNo" ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Centre Name *
                                                </td>
                                                <td style="color: Red">
                                                    <asp:TextBox ID="tbCentreName" runat="server" Height="90px" TextMode="MultiLine" 
                                                        Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCentreName" runat="server" 
                                                        ControlToValidate="tbCentreName" ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Location *
                                                </td>
                                                <td style="color: Red">
                                                    <asp:TextBox ID="tbAddress" runat="server" Height="90px" TextMode="MultiLine" 
                                                        Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPAddress" runat="server" 
                                                        ControlToValidate="tbAddress" ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    Email Address
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="tbEAddress" runat="server" Width="200px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revEmailAddress" runat="server" 
                                                        ControlToValidate="tbEAddress" 
                                                        ErrorMessage="Please Enter A Valid Email Address" 
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    Exam SC Codes
                                                </td>
                                                <td valign="top" style="color: #003f6f" valign="top">
                                                    <asp:TextBox ID="tbExamSCCodes" runat="server" Height="90px" 
                                                        TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                    <asp:Label ID="lblExamSCCodes" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Remark *
                                                </td>
                                                <td style="color: Red" valign="middle">
                                                    <asp:DropDownList ID="ddlistRemark" runat="server">                                                  
                                                    <asp:ListItem>NEUTRAL</asp:ListItem>
                                                    <asp:ListItem>SELF CENTRE</asp:ListItem>
                                                    <asp:ListItem>NA</asp:ListItem>
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
