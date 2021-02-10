<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="UpdateASAllotted.aspx.cs" Inherits="DDE.Web.Admin.UpdateASAllotted" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Update Award Sheet Allotment
            </div>
            <div style="padding-bottom: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            Examination
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" runat="server">
                                <asp:ListItem Value="A18" Selected="True">JUNE 2018</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            AS No.
                        </td>
                        <td>
                            <asp:TextBox ID="tbASNo" runat="server" Width="80px"></asp:TextBox>
                        </td>
                        <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
          
            <div style="padding-top: 5px">
                <table border="1" class="tableStyle2" cellpadding="10px">
                    <tbody align="left">
                        <tr>
                            <td valign="top" align="center" style="width: 50%">
                                <asp:Label ID="lbl1Year" runat="server" Text="Currently Allotted To"></asp:Label>
                            </td>
                            <td valign="top" align="center">
                                <asp:Label ID="lbl2Year" runat="server" Text="Now Allot To"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:DropDownList ID="ddlistCEC" Enabled="false" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td align="center">
                                <asp:DropDownList ID="ddlistNEC" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div style="padding: 10px">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
            </div>
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
</asp:Content>
