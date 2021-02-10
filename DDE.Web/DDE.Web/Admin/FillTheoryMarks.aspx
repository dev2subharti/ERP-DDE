<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="FillTheoryMarks.aspx.cs" Inherits="DDE.Web.Admin.FillTheoryMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Fill Theory Marks
            </div>
            <div align="center" class="text" style="padding-top: 20px; padding-bottom: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td colspan="4" align="center">
                            <table cellspacing="10px">
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
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
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
                        <td align="left">
                            <b>Course</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Year</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistYear_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Syllabus Session</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSySession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSySession_SelectedIndexChanged">
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
                            <b>Subject</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSubject" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSubject_SelectedIndexChanged">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding-bottom: 20px">
                <asp:Button ID="Button1" runat="server" Text="Find" Style="height: 26px" Width="82px"
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
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 210px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Study Center Code</b>
                                </td>
                                <td align="left" style="width: 110px">
                                    <b>Roll No</b>
                                </td>
                                <td align="left" style="width: 110px; padding-left: 5px">
                                    <b>Theory Marks</b>
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
                                   
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 210px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 120px; padding-left: 45px">
                                    <asp:Label ID="lblSCCode" runat="server" Text='<%#Eval("SCCode")%>'></asp:Label>
                                </td>
                                <td align="center" style="width: 120px">
                                    <asp:Label ID="lblRNAllotted" runat="server" Text='<%#Eval("RNAllotted")%>' Visible="false"></asp:Label>
                                    <asp:TextBox ID="tbRollNo" runat="server" Text='<%#Eval("RollNo")%>' Width="120px"></asp:TextBox>
                                    <asp:Label ID="lblRollNo" runat="server" Text='<%#Eval("RollNo")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:Label ID="lblMarksFilled" runat="server" Text='<%#Eval("MarksFilled")%>' Visible="false"></asp:Label>
                                    <asp:TextBox ID="tbTheory" runat="server" Text='<%#Eval("TheoryMarks")%>' Width="50px"></asp:TextBox>
                                    <asp:Label ID="lblTheory" runat="server" Text='<%#Eval("TheoryMarks")%>' Visible="false"></asp:Label>
                                </td>
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
