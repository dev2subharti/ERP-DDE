<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="CPFirstTime.aspx.cs" Inherits="DDE.Web.Admin.CPFirstTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
<div align="center" >
        <asp:Panel ID="pnlData" runat="server"  >
            <div >
              <h3>Change Password</h3> 
            </div>
           <div style="padding:20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tbody align="left" valign="top">
                        <tr>
                            <td>
                                Enter Current Password
                            </td>
                            <td>
                                <asp:TextBox ID="tbPriviousPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPrePass" ForeColor="Red" ControlToValidate="tbPriviousPassword"
                                    runat="server" ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Enter New Password
                            </td>
                            <td>
                                <asp:TextBox ID="tbNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNewPass" ForeColor="Red" ControlToValidate="tbNewPassword" runat="server"
                                    ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Confirm New Password
                            </td>
                            <td>
                                <asp:TextBox ID="tbConPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConNewPass" ForeColor="Red" ControlToValidate="tbConPassword"
                                    runat="server" ErrorMessage="Please fill up this entry"></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="cvConPass" ForeColor="Red" ControlToCompare="tbNewPassword" ControlToValidate="tbConPassword"
                                    runat="server" ErrorMessage="Password did not match"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" class="style1">
                                <asp:Button ID="btnUpdate" runat="server" Text="Change" OnClick="btnUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
           
        </asp:Panel>
         <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

</asp:Content>
