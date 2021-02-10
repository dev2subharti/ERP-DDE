<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="PublishListsOfSLMLetter.aspx.cs" Inherits="DDE.Web.Admin.PublishListsOfSLMLetter" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Publish Lists Attached With SLM Letter
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
                                         
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table cellspacing="10px">
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnPublishStudentList" runat="server" 
                                                Text="Publish Student List" onclick="btnPublishStudentList_Click" />
                                        </td>
                                       <%-- <td>
                                            <asp:Button ID="btnPublishSLMSetList" runat="server" 
                                                Text="SLM Set List" onclick="btnPublishSLMSetList_Click" 
                                                />
                                        </td>--%>
                                       
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