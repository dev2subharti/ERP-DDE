<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="RemoveDetainedStatus.aspx.cs" Inherits="DDE.Web.Admin.RemoveDetainedStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
               Remove Detained Student From June 19 Exam
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>OANo</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbOANo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Examination
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExam" Enabled="false" runat="server" Width="150px">
                                    <asp:ListItem Value="W10">JUNE 2019</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Mode of Exam
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistMOE" Enabled="false" runat="server" Width="150px">
                                   
                                    <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                    <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Remove" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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