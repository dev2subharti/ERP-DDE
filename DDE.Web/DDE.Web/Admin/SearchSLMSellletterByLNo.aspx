<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="SearchSLMSellletterByLNo.aspx.cs" Inherits="DDE.Web.Admin.SearchSLMSellletterByLNo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Search SLM Sell Letter
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <table cellspacing="10px">
                                    <tr>
                                        <td>
                                            <b>Letter No.</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbLNo" runat="server"></asp:TextBox>
                                        </td>
                                         <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click" />
                                        </td>
                                    </tr>
                                </table>
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
   
</asp:Content>

