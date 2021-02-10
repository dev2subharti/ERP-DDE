<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ChangeExamCentreByENo.aspx.cs" Inherits="DDE.Web.Admin.ChangeExamCentreByENo" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
<div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Change Examination Centre By Enrollment
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>Mode of Exam</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistMOE" runat="server">
                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                <asp:ListItem Value="B" >BACKPAPER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                            <b>Examination</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                            <td>
                                <b>Enrollment No</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:Panel ID="pnlStudentDetails" runat="server" Visible="false">
                    <div>
                        <table cellpadding="0px" class="tableStyle2" cellspacing="0px">
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Image ID="imgStudent" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                        runat="server" Width="100px" Height="120px" />
                                </td>
                                <td valign="top">
                                    <table cellspacing="10px">
                                        <tr>
                                            <td align="left">
                                                <b>Enrollment No</b>
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
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding-top: 5px">
                        <table align="center" class="tableStyle2" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td align="center" style="padding: 5px">
                                    <table width="100%" border="1">
                                        <tbody align="left">
                                            <tr>
                                                <td valign="top" align="center" style="width:50%">
                                                    <asp:Label ID="lbl1Year" runat="server" Text="Current Exam Centre"></asp:Label>
                                                </td>
                                                <td valign="top" align="center">
                                                    <asp:Label ID="lbl2Year" runat="server" Text="Change To"></asp:Label>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:DropDownList ID="ddlistCEC" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="center">
                                                    <asp:DropDownList ID="ddlistNEC" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <div style="padding: 10px">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" 
                    onclick="btnUpdate_Click"  />
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
    <div style="padding-top: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
    </div>
</asp:Content>