<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="TransferCourse.aspx.cs" Inherits="DDE.Web.Admin.TransferCourse" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading" style="padding-top: 20px">
            Fill Detail of Student for Credit Transfer
        </div>
        <div align="center" class="text" style="padding: 20px">
            <table cellspacing="20px" class="tableStyle2">
                <tr>
                    <td align="left">
                        <b>Select Previous Institution</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistPInst" runat="server" Width="150px" AutoPostBack="true">
                            <asp:ListItem>--Select Here--</asp:ListItem>
                            <asp:ListItem Value="1">DDE (SVSU)</asp:ListItem>
                            <asp:ListItem Value="2">OUT SIDE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>Select Previous Course</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistPreCourse" runat="server" Width="150px" AutoPostBack="true">
                            <asp:ListItem>--Select Here--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>Select Current Course</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistCurCourse" runat="server" Width="150px" AutoPostBack="true"
                            >
                            <asp:ListItem>--Select Here--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table cellspacing="10px">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                    <td style="width: 20px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
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
