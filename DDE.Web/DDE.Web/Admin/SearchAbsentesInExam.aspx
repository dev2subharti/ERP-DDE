<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="SearchAbsentesInExam.aspx.cs" Inherits="DDE.Web.Admin.SearchAbsentesInExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Search Absentes in Examination
            </div>
            <div align="center" class="text" style="padding: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Examination</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Mode of Examination</b>
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
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Year</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px" 
                                AutoPostBack="true" onselectedindexchanged="ddlistYear_SelectedIndexChanged1"
                                >
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
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
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                 <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Study Centre Code</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistStudyCentre" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistStudyCentre_SelectedIndexChanged">
                                 <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding-bottom: 20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="height: 26px" Width="82px"
                    OnClick="btnSearch_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowStudents" runat="server" CssClass="dtlist" 
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>SC Code</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 230px">
                                    <b>Student Name</b>
                                </td>
                                <td align="center" style="width: 100px">
                                    <b>Sub. Code</b>
                                </td>
                                <td align="center" style="width: 200px">
                                    <b>Sub. Name</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td  align="left" style="width: 70px">
                                    <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 90px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 210px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="center" style="width: 340px">
                                    <asp:DataList ID="dtlistSubjects" runat="server" Visible="true">
                                        <ItemTemplate>
                                            <table  cellpadding="0px" cellspacing="3px" style="border:solid 1px black">
                                                <tr>
                                                <td valign="top" align="left" style="width: 25px">
                                                        <%#Eval("SubNo")%>
                                                    </td>
                                                    <td valign="top" align="left" style="width: 100px">
                                                        <%#Eval("SubjectCode")%>
                                                    </td>
                                                    <td valign="top" align="left" style="width: 200px">
                                                        <%#Eval("SubjectName")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
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
