<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="PublishPassword.aspx.cs" Inherits="DDE.Web.Admin.PublishPassword" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div style="padding-bottom: 50px">
                <div class="heading">
                    Guide lines for login in ERP System
                </div>
                <div class="text" style="padding: 30px">
                    <table class="tableStyle2">
                        <tr>
                            <td style="padding: 5px">
                                <asp:Image ID="imgEmployee" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                    runat="server" Width="100px" Height="120px" />
                            </td>
                            <td valign="top">
                                <table cellspacing="10px">
                                    <tr>
                                        <td align="left">
                                            <b>Employee Name</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEName" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>Designation</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>Department</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblEmpDepartment" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>Unit</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align="center" style="padding: 0px" class="text">
                    <table>
                        <tr>
                            <td align="left">
                                <ol style="line-height: 40px; list-style-type: upper-roman">
                                    <li>Open Mozilla Firefox on your system.</li>
                                    <li>Type <b>192.168.62.254</b> in Addressbar of your web browser and press ENTER.</li>
                                   
                                    <li>
                                        <asp:Label ID="lblGateway" runat="server" Text=""></asp:Label></li>
                                    <li>Fill your User ID and Password and press 'LOGIN' Button.</li>
                                    <li>Your User ID and Password is :
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    User ID
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUserID" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Password
                                                </td>
                                                <td>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPassword" runat="server" Font-Bold="true" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                                    <li>If you are loging in first time than you are advised to change your password first
                                        of all.</li>
                                </ol>
                            </td>
                        </tr>
                    </table>
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
        <br class="clear" />
    </div>
</asp:Content>
