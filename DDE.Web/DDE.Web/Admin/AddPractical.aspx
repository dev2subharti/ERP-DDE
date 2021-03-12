<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="AddPractical.aspx.cs" Inherits="DDE.Web.Admin.AddPractical" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
<asp:Panel ID="pnlData" runat="server" Visible="false">
    <div align="center" >
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
                            <b>Select Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server">
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
            <asp:Button ID="btnAddNewPrac" runat="server" Text="Add New Practical in This Course"
                Visible="false" OnClick="btnAddNewSub_Click" />
        </div>
        <div>
            <asp:Panel ID="pnlPracticalEntry" runat="server" Visible="false">
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
                                Practical Name *
                            </td>
                            <td>
                                <asp:TextBox ID="tbPracName" runat="server" Height="67px" TextMode="MultiLine" 
                                    Width="213px"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPracName"
                                    ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <tr>
                            <td valign="top">
                                Practical Code *
                            </td>
                            <td>
                                <asp:TextBox ID="tbPracCode" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPracCode"
                                    ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Practical Max Marks *
                            </td>
                            <td>
                                <asp:TextBox ID="tbPracMaxMarks" runat="server"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPracMaxMarks"
                                    ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                         <%--<tr>
                            <td valign="top">
                                Allow SC to Upload Marks *
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistMU" Width="80px" runat="server">
                                <asp:ListItem Value="False">NO</asp:ListItem>
                                <asp:ListItem Value="True">YES</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                         <tr>
                            <td valign="top">
                                Practical Type *
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistPType"  runat="server">
                                <asp:ListItem Value="1">PRACTICAL</asp:ListItem>
                                <asp:ListItem Value="2">PROJECT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Allow for Award Sheet *
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistAllowAS" Width="80px" runat="server">
                                <asp:ListItem Value="False">NO</asp:ListItem>
                                <asp:ListItem Value="True">YES</asp:ListItem>
                                </asp:DropDownList>
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
