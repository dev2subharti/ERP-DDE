<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="FillPracticalMarks.aspx.cs" Inherits="DDE.Web.Admin.FillPracticalMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Fill Practical Marks
            </div>
            <div align="center" class="text" style="padding-top: 20px; padding-bottom: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Examination</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Mode of Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistMOE" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistMOE_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Course</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Year</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistYear_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="1">1st Year</asp:ListItem>
                                <asp:ListItem Value="2">2nd Year</asp:ListItem>
                                <asp:ListItem Value="3">3rd Year</asp:ListItem>
                                <asp:ListItem Value="4">4th Year</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Syllabus Session</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSySession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSySession_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Subject</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistPractical" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistPractical_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Batch</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Study Centre Code</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistStudyCentre" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistStudyCentre_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding-bottom: 20px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowStudents" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 210px">
                                    <b>Student Name</b>
                                </td>
                                <td align="center" style="width: 100px">
                                    <b>Roll No.</b>
                                </td>
                                <td align="center" style="width: 150px">
                                    <b>Practical Marks</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblMarksFilled" runat="server" Text='<%#Eval("MarksFilled")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 210px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="center" style="width: 100px">
                                    <%#Eval("RollNo")%>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:TextBox ID="tbPracticalMarks" runat="server" Text='<%#Eval("PracticalMarks")%>'
                                        Width="50px"></asp:TextBox>
                                    <asp:Label ID="lblPracticalMarks" runat="server" Text='<%#Eval("PracticalMarks")%>'
                                        Visible="false"></asp:Label>
                                </td>
                                <%-- <td>
                                <asp:RangeValidator ID="rvIA" runat="server" ControlToValidate="tbIA" MinimumValue="0"
                                    MaximumValue="20" ErrorMessage="*"></asp:RangeValidator>
                            </td>--%>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div align="center" style="padding: 20px">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit Marks" OnClick="btnSubmit_Click"
                    Visible="false" />
            </div>
            <div align="center" style="padding: 20px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="75px" OnClick="btnOK_Click" />
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
