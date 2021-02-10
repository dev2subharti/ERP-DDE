<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowRegularExamReport.aspx.cs" Inherits="DDE.Web.Admin.ShowRegularExamReport" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            Show Regular Examination Report
        </div>
        <div style="padding: 10px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td align="left">
                        Select Examination
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistExam" AutoPostBack="true" Width="150px" runat="server"
                            OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Select Year
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistYear" Width="150px" runat="server">
                            <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                            <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                            <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">ALL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
            </table>
        </div>
        
        <div style="padding-top: 10px">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <div style="padding-top: 20px">
            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Visible="false"
                OnClick="btnExport_Click" />
        </div>
        <div style="padding: 10px">
            <asp:GridView ID="gvShowStudent" CssClass="gridview" HeaderStyle-CssClass="gvheader"
                RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" runat="server">
            </asp:GridView>
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
