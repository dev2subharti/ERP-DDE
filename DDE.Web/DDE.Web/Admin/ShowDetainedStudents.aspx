<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowDetainedStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowDetainedStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Show Detained Students</b>
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            Examination
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" AutoPostBack="true" runat="server" Width="150px"
                                OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Mode of Exam
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistMOE" AutoPostBack="true" runat="server" Width="150px"
                                OnSelectedIndexChanged="ddlistMOE_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                Width="75px" Height="26px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowDetained" runat="server" CssClass="dtlist" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>A. No</b>
                                </td>
                                <td align="left" style="width: 125px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>SC Code</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Batch</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Detained</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Remark</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 40px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("ApplicationNo")%>
                                </td>
                                <td align="left" style="width: 125px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("Session")%>
                                </td>
                                <td align="center" style="width: 80px">
                                    <%#Eval("Detained")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("Remark")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
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
