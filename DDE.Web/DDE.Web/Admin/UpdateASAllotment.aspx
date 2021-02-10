<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="UpdateASAllotment.aspx.cs" Inherits="DDE.Web.Admin.UpdateASAllotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Change Award Sheet Allotment
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td align="left">
                                <b>Examination</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExam" runat="server" Width="150px">
                                   
                                  
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>Award Sheet No</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbASNo" runat="server"></asp:TextBox>
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
                <asp:Panel ID="pnlASDetails" runat="server" Visible="false">
                  
                    <div style="padding-top: 5px">
                        <table align="center" class="tableStyle2" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" style="padding: 5px">
                                    <table width="100%" border="1">
                                        <tbody align="left">
                                            <tr>
                                                <td valign="top" align="center" style="width: 50%">
                                                    <asp:Label ID="lblCE" runat="server" Text="Current Evaluator"></asp:Label>
                                                </td>
                                                <td valign="top" align="center">
                                                    <asp:Label ID="lblNE" runat="server" Text="Allot To Evaluator"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:DropDownList ID="ddlistCE" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center">
                                                    <asp:DropDownList ID="ddlistNE" runat="server">
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
            <div style="padding: 10px">
                <asp:Button ID="btnUpdate" CssClass="btn" runat="server" Width="150px" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
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
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" CssClass="btn" OnClick="btnOK_Click" />
    </div>
</asp:Content>
