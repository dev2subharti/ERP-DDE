<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="UploadQuestions.aspx.cs" Inherits="DDE.Web.Admin.UploadQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Upload Questions
            </div>
            <div>
                <asp:RadioButtonList ID="rblType" CssClass="tableStyle2" runat="server">
                    <asp:ListItem Selected="True" Value="0">TEXT</asp:ListItem>
                    <asp:ListItem Value="1">IMAGE</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div style="padding-top: 10px">
                <table cellspacing="20px" class="tableStyle2">
                    <tr>
                        <td>
                            <b>Paper Code</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPaperCode" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="fuQuestion" Multiple="Multiple" runat="server"></asp:FileUpload>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnUploadQP" runat="server" Text="Upload" OnClick="btnUploadQP_Click" />
                        </td>
                    </tr>
                </table>
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
