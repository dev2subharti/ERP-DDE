<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddCity.aspx.cs" Inherits="DDE.Web.Admin.AddCity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" style="padding-top: 50px">
            <table class="tableStyle2"  cellspacing="20px">
                <tbody align="left">
                    <tr>
                        <td>
                            City Name
                        </td>
                        <td>
                            <asp:TextBox ID="tbCity" runat="server"></asp:TextBox><br />
                              <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="tbCity"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            State
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistState" runat="server">
                          

                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Country
                        </td>
                        <td>
                            
                            <asp:DropDownList ID="ddlistCountry" runat="server">
                            <asp:ListItem>INDIA</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
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
