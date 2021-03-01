<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowProvisionalDegree.aspx.cs" Inherits="DDE.Web.Admin.ShowProvisionalDegree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            Show Provisional Degree
        </div>
        <div style="padding-top: 20px">
            <table class="tableStyle2" cellspacing="10px" width="450px">
                <tr>
                    <%--<td>

                    </td>--%>
                    <td>
                        <tr>
                            <td align="left">
                                <b>Exam</b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true">
                                    <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Enrollment No</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbENo" runat="server" OnTextChanged="tbENo_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Student Name</td>
                            <td>
                                <asp:TextBox ID="txtStudentName" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Father's Name</td>
                            <td>
                                <asp:TextBox ID="txtFName" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Course Name</td>
                            <td>
                                <asp:TextBox ID="txtCourse" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>

                            <td>Admission Date</td>
                            <td>
                                <asp:TextBox ID="txtAdminDate" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td align="left"><b>Maximum Marks</b></td>
                            <td>
                                <asp:TextBox ID="txtMM" runat="server" onkeydown="return (!(event.keyCode>=65 && event.keyCode<=90 ) && event.keyCode!=32);"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td align="left"><b>Obtained Marks</b></td>
                            <td>
                                <asp:TextBox ID="txtObtMarks" runat="server" onkeydown="return (!(event.keyCode>=65 && event.keyCode<=90 ) && event.keyCode!=32);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"><b>Grade</b></td>
                            <td>
                                <asp:TextBox ID="txtGrade" runat="server" MaxLength="1"></asp:TextBox>
                            </td>
                        </tr>
                    </td>
                </tr>
            </table>
        </div>

        <div style="padding-top: 10px">
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>

    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table class="tableStyle2">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="padding: 30px">
                    <asp:Button ID="btnClose" runat="server" Text="OK" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
