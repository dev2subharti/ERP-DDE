<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowExamFeeRecord.aspx.cs" Inherits="DDE.Web.Admin.ShowExamFeeRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
               Show Examination  Record
            </div>
            <div align="center" class="text" style="padding: 20px">
                <asp:Panel ID="pnlSearch" runat="server">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td align="center" colspan="4">
                                <table cellspacing="10px">
                                    <tr>
                                        <td>
                                            <b>Select Mode of Exam</b>
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
                                </table>
                            </td>
                        </tr>
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
                            <td>
                                <b>Study Centre Code</b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlistSCCode" runat="server" Width="150px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                                    <asp:ListItem>--Select One--</asp:ListItem>
                                    <asp:ListItem>ALL</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Examination Centre</b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlistExamCentre" runat="server" Width="150px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlistExamCentre_SelectedIndexChanged">
                                    <asp:ListItem>--Select One--</asp:ListItem>
                                    <asp:ListItem>ALL</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <b>Jone</b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlistJone" runat="server" Width="150px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlistJone_SelectedIndexChanged">
                                    <asp:ListItem>--Select One--</asp:ListItem>
                                    <asp:ListItem>ALL</asp:ListItem>
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem>C</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                    <asp:ListItem>E</asp:ListItem>
                                    <asp:ListItem>F</asp:ListItem>
                                    <asp:ListItem>G</asp:ListItem>
                                    <asp:ListItem>H</asp:ListItem>
                                    <asp:ListItem>I</asp:ListItem>
                                    <asp:ListItem>J</asp:ListItem>
                                    <asp:ListItem>K</asp:ListItem>
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
                                    <asp:ListItem>ALL</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <b>Batch</b>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                    <asp:ListItem>--Select One--</asp:ListItem>
                                    <asp:ListItem>ALL</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div align="center" style="padding-bottom: 20px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowStudents" runat="server" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 90px">
                                    <b>S.C.Code</b>
                                </td>
                                <td align="left" style="width: 130px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>RollNo</b>
                                </td>
                                <td align="left" style="width: 180px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Year</b>
                                </td>
                                <td align="left" style="width: 180px">
                                    <b>Subjects</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Exam Centre</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>Jone</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="center" style="width: 130px">
                                    <%#Eval("RollNo")%>
                                </td>
                                <td align="left" style="width: 180px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("CYear")%>
                                </td>
                                <td align="left" style="width: 180px">
                                    <%#Eval("Subjects")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("ExamCity")%>
                                </td>
                                <td align="left" style="width: 50px">
                                    <%#Eval("ExamJone")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>;
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
    <div align="center" style="padding: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="75px" OnClick="btnOK_Click" />
    </div>
</asp:Content>
