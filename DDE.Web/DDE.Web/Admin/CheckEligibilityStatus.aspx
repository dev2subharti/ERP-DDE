<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="CheckEligibilityStatus.aspx.cs" Inherits="DDE.Web.Admin.CheckEligibilityStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Show Eligibility Status</b>
            </div>

            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>





                        <td>
                            <asp:Label ID="lblPANo" runat="server" Text="Pro. ANo." Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPANo" runat="server" Visible="true"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Visible="true" Text="Search" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>


                <div style="padding-top: 10px">
                    <table align="center" style="margin: 0px; padding: 0px" cellpadding="0px" class="tableStyle2" cellspacing="5px">
                        <tr>
                            <td valign="top">
                                <div>
                                    <asp:Image ID="imgStudent" Height="120px" Width="100px" CssClass="imguser" runat="server" />
                                </div>
                            </td>
                            <td>

                                <table cellspacing="5px">
                                    <tr>
                                        <td align="left">
                                            <b>OANo</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbPSRID" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
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
                                            <b>AF Code</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbSCCode" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>

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

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>Year</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbYear" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left">
                                            <b>Allotted To</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbExName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <b>Eligibility Status</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbCurrentStatus" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="left">
                                            <b>Remark</b>
                                        </td>
                                        <td align="left">
                                            <b>:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbRemark" runat="server" TextMode="MultiLine" Enabled="false" ForeColor="Black"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>



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
