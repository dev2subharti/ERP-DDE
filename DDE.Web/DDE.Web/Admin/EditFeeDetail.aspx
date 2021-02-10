<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="EditFeeDetail.aspx.cs" Inherits="DDE.Web.Admin.EditFeeDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Edit Fee Detail
        </div>
        <div class="data" style="padding-top: 20px" align="center">
            <table class="tableStyle2" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td valign="top" style="width: 50%; border-right: solid 2px #003f6f">
                        <table cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Image ID="imgStudent" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                        runat="server" Width="100px" Height="120px" />
                                </td>
                                <td valign="top">
                                    <table cellspacing="10px">
                                        <tr>
                                            <td align="left">
                                                <b>
                                                    <asp:Label ID="lblEnrollment" runat="server" Text="Enrollment No"></asp:Label></b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbEnNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Student&#39;s Name</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbSName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Father&#39;s Name</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbFName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>S.C. Code</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbSCCode" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Course</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbCourse" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                <asp:Label ID="lblCID" runat="server" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="center">
                        <table cellspacing="10px" style="width: 100%">
                            <tbody align="left">
                                <tr>
                                    <td>
                                        Account&#39;s Session
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistAcountsSession" runat="server" Enabled="false" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <b>Fee Head</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlistFeeHead" Enabled="false" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbAmount" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblAmount" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistYear" runat="server" Width="150px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblPYear" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblExamination" runat="server" Text="Examination"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistExamination" runat="server" Width="150px" Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table cellspacing="20px">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
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
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Text="OK" Width="50px" Visible="false" OnClick="btnOK_Click" />
        </div>
    </asp:Panel>
</asp:Content>
