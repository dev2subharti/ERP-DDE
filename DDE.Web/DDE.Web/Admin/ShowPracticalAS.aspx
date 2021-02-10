<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowPracticalAS.aspx.cs" Inherits="DDE.Web.Admin.ShowPracticalAS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading" style="padding-top: 0px">
                Publish Practical Award Sheet
            </div>
            <div style="padding-top: 10px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td class="style1">
                                <b>Examination</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExam" runat="server">
                                    <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                    <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                   <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                   <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                    <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                    <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                    <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                                    <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                                     <asp:ListItem Value="W10" Selected="True">JUNE 2019</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>SC Code</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistSCCode" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>Practical Code</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistPracCode" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <div align="center" style="padding-top: 10px">
                        <asp:Button ID="btnPublish" runat="server" Text="Publish" OnClick="btnPublish_Click" />
                    </div>
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
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphheadColleges">
    <style type="text/css">
        .style1
        {
            width: 71px;
        }
    </style>
</asp:Content>
