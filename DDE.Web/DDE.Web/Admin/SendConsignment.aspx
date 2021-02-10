<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="SendConsignment.aspx.cs" Inherits="DDE.Web.Admin.SendConsignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
           Send Consignement (On Trial)
        </div>
        <div style="padding-top: 20px">
            <table class="tableStyle2" cellpadding="0px" cellspacing="10px">
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnlItemDetails" runat="server" GroupingText="Consignment Details">
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td valign="top">
                                        <table cellspacing="10px">
                                          
                                            <tr>
                                                <td align="left">
                                                    <b>COURIER PARTY  *</b>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlistParty" runat="server" Width="160px">
                                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <b>CONSIGNMENT TYPE *</b>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlistConsType" AutoPostBack="true" runat="server" Width="160px" OnSelectedIndexChanged="ddlistConsType_SelectedIndexChanged">
                                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                        <asp:ListItem Value="1">DEGREE</asp:ListItem>
                                                        <asp:ListItem Value="2">MIGRATION</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlRecDetail" runat="server" GroupingText="Receiver Details">
                            <div align="center" style="padding: 10px">
                                <table cellspacing="20px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRecID" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbRecID" runat="server"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </div>
                            
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px">
            <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Submit"
                OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClick="btnCancel_Click" />
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
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Width="50px" Text="OK" />
        </div>
    </asp:Panel>
    
</asp:Content>