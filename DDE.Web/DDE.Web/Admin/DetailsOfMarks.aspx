<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="DetailsOfMarks.aspx.cs" Inherits="DDE.Web.Admin.DetailsOfMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Details of Marks
            </div>
            <div align="center" class="text" style="padding: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Mode of Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistMOE" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistMOE_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
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
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Syllabus Session</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSySession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSySession_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
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
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Batch</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>SC Code</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSCCode" runat="server" Width="150px" AutoPostBack="true">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" class="smalltext">
                <asp:Panel ID="pnlMarksList" align="center" runat="server" Visible="false">
                    <table align="center" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td>
                                <table width="100%" cellpadding="0px" cellspacing="0px">
                                    <tr>
                                        <td style="width: 520px">
                                            <table border="1" width="100%" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td align="center" style="width: 20px; height: 48px">
                                                        SNo
                                                    </td>
                                                    <td align="center" style="width: 50px">
                                                        SCCode
                                                    </td>
                                                    <td align="center" style="width: 80px">
                                                        RollNumber.
                                                    </td>
                                                    <td align="center" style="width: 80px">
                                                        EnrollmentNo.&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="center" style="width: 120px">
                                                        StudentName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="center" style="width: 120px">
                                                        FatherName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="left">
                                            <asp:DataList ID="dtlistSub" runat="server" RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <table border="1" cellpadding="0px" cellspacing="0px">
                                                        <tr>
                                                            <td colspan="5" align="center">
                                                                <asp:Label ID="lblSub" runat="server" Text='<%#Eval("SubjectCode")%>'>
                           
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                Th
                                                            </td>
                                                            <td>
                                                                IA
                                                            </td>
                                                            <td>
                                                                AW
                                                            </td>
                                                            <td>
                                                                Tot
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                100
                                                            </td>
                                                            <td>
                                                                60&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                20&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                20&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                100
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DataList Width="100%" ID="dtlistPrac" runat="server" RepeatDirection="Horizontal">
                                                <ItemTemplate>
                                                    <table width="100%" border="1" cellpadding="0px" cellspacing="0px">
                                                        <tr>
                                                            <td style="height: 33px" align="center">
                                                                <asp:Label ID="lblPrac" runat="server" Text='<%#Eval("PracticalCode")%>'>
                           
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalPracMaxMarks" runat="server" Text='<%#Eval("PracticalMaxMarks")%>'>
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                        <td align="left" valign="top">
                                            <table align="left" border="1" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td style="height: 33px" align="center">
                                                        Grand<br />
                                                        Total
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblTotalMaxMarks" Font-Bold="true" runat="server" Text="">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="left" valign="top">
                                            <table border="1" style="height: 50px" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td style="height: 49px" align="center">
                                                        Rem.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="left" valign="top">
                                            <table border="1" style="height: 50px" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td style="height: 49px" align="center">
                                                        Grd.
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="left" valign="top">
                                            <table border="1" style="height: 50px" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td style="height: 49px; width: 30px" align="center">
                                                        Div
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="left">
                                <asp:DataList Width="100%" ID="dtlistShowStudents" runat="server" UseAccessibleHeader="true"
                                    BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px">
                                    <ItemStyle BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td>
                                                    <table align="left">
                                                        <tr>
                                                            <td style="width: 527px">
                                                                <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                    <tr>
                                                                        <td align="left" style="width: 30px">
                                                                            <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                                                            <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblCYear" runat="server" Text='<%#Eval("CYear")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" style="width: 40px">
                                                                            <%#Eval("SCCode")%>
                                                                        </td>
                                                                        <td align="left" style="width: 70px">
                                                                            <%#Eval("RollNo")%>
                                                                        </td>
                                                                        <td align="left" style="width: 100px">
                                                                            <%#Eval("EnrollmentNo")%>
                                                                        </td>
                                                                        <td align="left" style="width: 140px">
                                                                            <%#Eval("StudentName")%>
                                                                        </td>
                                                                        <td align="left" style="width: 147px">
                                                                            <%#Eval("FatherName")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <table cellpadding="0px" cellspacing="0px">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="ps1" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF1")%>
                                                                                        </td>
                                                                                        <td style="width: 25px">
                                                                                            <%#Eval("TM1")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA1")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW1")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total1")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="ps2" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF2")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TM2")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA2")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW2")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total2")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="ps3" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF3")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TM3")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA3")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW3")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total3")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="ps4" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF4")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TM4")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA4")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW4")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total4")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="ps5" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF5")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TM5")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA5")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW5")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total5")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="ps6" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF6")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TM6")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA6")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW6")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total6")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="ps7" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF7")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TM7")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA7")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW7")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total7")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Panel ID="ps8" runat="server">
                                                                                <table cellpadding="0px" cellspacing="0px">
                                                                                    <tr>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TMF8")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("TM8")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("IA8")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <%#Eval("AW8")%>
                                                                                        </td>
                                                                                        <td style="width: 24px">
                                                                                            <b>
                                                                                                <%#Eval("Total8")%></b>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: auto">
                                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblPM1" runat="server" Width="35px" Text='<%#Eval("PM1")%>'></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblPM2" runat="server" Width="35px" Text='<%#Eval("PM2")%>'></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblPM3" runat="server" Width="35px" Text='<%#Eval("PM3")%>'></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblGrandTotal" Width="30px" runat="server" Font-Bold="true" Text='<%#Eval("GrandTotal")%>'></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblRemark" Width="30px" runat="server" Font-Bold="true" Text='<%#Eval("Remark")%>'></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblGrade" Width="30px" runat="server" Font-Bold="true" Text='<%#Eval("Grade")%>'></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblDiv" Width="30px" runat="server" Font-Bold="true" Text='<%#Eval("Div")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                    <div align="center" style="padding-top: 10px">
                        <asp:Label ID="lbldata" runat="server" ForeColor="#003f6f" Font-Size="14px" Font-Bold="true"
                            Text="lblData" ></asp:Label>
                    </div>
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
