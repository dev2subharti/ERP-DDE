<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" ValidateRequest="false" AutoEventWireup="true"
    CodeBehind="AddSubject.aspx.cs" Inherits="DDE.Web.Admin.AddSubject" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
                Add Subjects
            </div>
        <div align="center" style="padding-top: 20px">
            <div>
                <div align="center" class="text" style="padding: 20px">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td>
                                <b>Select Syllabus Session</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistSySession" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>Select Course</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistCourse" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>Select Year/Semester</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistYear" Width="60px" runat="server">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                                    OnClick="btnFind_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div align="center" style="padding: 20px">
                <asp:Button ID="btnAddNewSub" runat="server" Text="Add New Subject in This Course"
                    Visible="false" OnClick="btnAddNewSub_Click" />
            </div>
            <div>
                <asp:Panel ID="pnlSubjectEntry" runat="server" Visible="false">
                    <table class="tableStyle2" width="500px" cellspacing="15px">
                        <tbody align="left">
                            <tr>
                                <td valign="top">
                                    S.No. *
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSNo" runat="server"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvSNo" runat="server" ControlToValidate="tbSNo"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Subject Code *
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSubCode" runat="server"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvSC" runat="server" ErrorMessage="Please fill this entry"
                                        ControlToValidate="tbSubCode"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Paper Code *
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPaperCode" runat="server"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvPC" runat="server" ErrorMessage="Please fill this entry"
                                        ControlToValidate="tbPaperCode"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Subject Name *
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSubName" runat="server" Height="76px" TextMode="MultiLine" Width="246px"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="rfvSN" runat="server" ErrorMessage="Please fill this entry"
                                        ControlToValidate="tbSubName"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <table cellspacing="10px">
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>         
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
