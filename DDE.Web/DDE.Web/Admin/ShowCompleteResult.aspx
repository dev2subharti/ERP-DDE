<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowCompleteResult.aspx.cs" Inherits="DDE.Web.Admin.ShowCompleteResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 10px">
                Complete Result Report
            </div>
            <div align="center" class="text" style="padding-bottom: 10px">
            (Under Testing)
            </div>
            <div style="padding-bottom: 10px">
                <table class="tableStyle2" cellspacing="10px">
                    <tbody align="left">
                        <tr>
                            <td>
                                Examination
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExam" AutoPostBack="true" runat="server" 
                                    Width="140px" onselectedindexchanged="ddlistExam_SelectedIndexChanged">
                                     <asp:ListItem >--SELECT ONE--</asp:ListItem>
                                    <asp:ListItem Value="A13">JUNE 2013</asp:ListItem>
                                    <asp:ListItem Value="B13">DECEMBER 2013</asp:ListItem>
                                    <asp:ListItem Value="A14">JUNE 2014</asp:ListItem>
                                    <asp:ListItem Value="B14">DECEMBER 2014</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div align="center" style="padding-bottom:10px">
                <asp:Panel ID="pnlRange" runat="server" Visible="false">
                    <table class="tableStyle2" cellspacing="5px">
                        <tr>
                            <td align="center" colspan="4">
                                Examination Record
                            </td>
                        </tr>
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                <asp:TextBox ID="tbFrom" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                To
                            </td>
                            <td>
                                <asp:TextBox ID="tbTo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:Button ID="btnFind" runat="server" Text="Search" Width="80px" OnClick="btnFind_Click" />
            </div>
            <div style="padding-top: 20px">
                <asp:GridView ID="gvShowReport" HCssClass="gridview" HeaderStyle-CssClass="gvheader"
                    RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" runat="server">
                </asp:GridView>
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
