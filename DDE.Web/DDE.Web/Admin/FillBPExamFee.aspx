<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="FillBPExamFee.aspx.cs" Inherits="DDE.Web.Admin.FillBPExamFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Fill Back Paper Examination Fees
            </div>
            <div align="center" class="text" style="padding-top: 20px; padding-bottom: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="center" colspan="4">
                            <table>
                                <tr>
                                    <td align="left" colspan="2">
                                        <b>Selectz Exam</b>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                                            <asp:ListItem>--Select Here--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Select Syllabus Session</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSySession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSySession_SelectedIndexChanged">
                                <asp:ListItem>--Select Here--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Select Course</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                                <asp:ListItem>--Select Here--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Select Year</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistYear_SelectedIndexChanged1">
                                <asp:ListItem>--Select Here--</asp:ListItem>
                                <asp:ListItem Value="1">1st Year</asp:ListItem>
                                <asp:ListItem Value="2">2nd Year</asp:ListItem>
                                <asp:ListItem Value="3">3rd Year</asp:ListItem>
                                <asp:ListItem Value="4">4th Year</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Select Batch</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                <asp:ListItem>--Select Here--</asp:ListItem>
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
                <asp:DataList ID="dtlistShowStudents" runat="server" CssClass="dtlist" 
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem"  GridLines="Both">
                    <HeaderTemplate>
                    
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>S.C.Code</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 180px; padding-left: 10px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 180px">
                                    <b>Father's Name</b>
                                </td>
                                <td align="center" style="width: 120px">
                                    <b>Subjects</b>
                                </td>
                                <td align="center" style="width: 100px; padding-left: 10px">
                                    <b>Exam Fee</b>
                                </td>
                                <td align="left" style="width: 120px; padding-left: 15px">
                                    <b>Exam Centre</b>
                                </td>
                                <td align="left" style="width: 50px; padding-left: 10px">
                                    <b>Jone</b>
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
                                    <asp:Label ID="lblFeeFilled" runat="server" Text='<%#Eval("FeeFilled")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 180px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 180px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="center" style="width: 120px">
                                    <table width="100%">
                                        <tbody align="left">
                                            <tr>
                                                <td valign="top" style="width:20%">
                                                    1 Y
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="CBLSubjects1" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    2 Y
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="CBLSubjects2" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    3 Y
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="CBLSubjects3" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:TextBox ID="tbBPExamFee" runat="server" Text='<%#Eval("BPExamFee")%>' Width="50px"></asp:TextBox>
                                    <asp:Label ID="lblBPExamFee" runat="server" Text='<%#Eval("BPExamFee")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:DropDownList ID="ddlistExamCentre" runat="server">
                                        <asp:ListItem>NONE</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:DropDownList ID="ddlistJone" runat="server">
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
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div align="center" style="padding: 20px">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    Visible="false" />
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
