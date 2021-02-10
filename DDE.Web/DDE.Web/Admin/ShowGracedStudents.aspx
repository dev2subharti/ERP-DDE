<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowGracedStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowGracedStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Grace Marks Allotted Students </b>
            </div>
            <div style="padding-top:10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            Examination
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" runat="server">
                                
                                <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                <asp:ListItem Value="B17" Selected="True">DECEMBER 2017</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowStudents" runat="server" CssClass="dtlist" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 180px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>Year</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>SCCode</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Subject Name</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>Pre Marks</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>Cur Marks</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>Grace</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px; padding-left: 8px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 180px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 50px">
                                    <%#Eval("Year")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("SubjectName")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <%#Eval("PMarks")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <%#Eval("CMarks")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <b>
                                        <%#Eval("Grace")%></b>
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
