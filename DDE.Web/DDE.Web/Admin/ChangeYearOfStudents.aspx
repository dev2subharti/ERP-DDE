<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ChangeYearOfStudents.aspx.cs" Inherits="DDE.Web.Admin.ChangeYearOfStudents" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading" align="center" style="padding: 20px">
            Change Year of Student
        </div>
        <div align="center" class="text">
            <table cellpadding="10px" cellspacing="10px" class="tableStyle2">
                <tr>
                    <td>
                        <b>Select Batch</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px" 
                            onselectedindexchanged="ddlistBatch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <b>Select Course</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistCourses" runat="server" Width="150px"
                            onselectedindexchanged="ddlistCourses_SelectedIndexChanged">
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnFind" runat="server" Text="Search" Width="90px" OnClick="btnFind_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="data" style="padding-top: 20px" align="center">
            <asp:DataList ID="dtlistShowRegistration" runat="server" CssClass="dtlist" HeaderStyle-CssClass="dtlistheader"
                ItemStyle-CssClass="dtlistItem">
                <HeaderTemplate>
                    <table align="left">
                        <tr>
                            <td align="left" style="width: 50px">
                                <b>S.No.</b>
                            </td>
                            <td align="left" style="width: 150px; padding-left: 10px">
                                <b>Enrollment No</b>
                            </td>
                            <td align="left" style="width: 200px">
                                <b>Student Name</b>
                            </td>
                            <td align="left" style="width: 200px">
                                <b>Father Name</b>
                            </td>
                             <td align="left" style="width: 220px">
                                <b>Course</b>
                            </td>
                            <td align="left" style="width: 150px; padding-left: 20px">
                                <b>Current Year</b>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table align="left">
                        <tr>
                            <td align="left" style="width: 50px">
                                <%#Eval("SNo")%>
                                <asp:Label ID="lblSRID" Text='<%#Eval("SRID") %>' runat="server" Visible="false"></asp:Label>
                            </td>
                            <td align="left" style="width: 150px">
                                <div>
                                    <%#Eval("EnrollmentNo")%>
                                </div>
                            </td>
                            <td align="left" style="width: 200px">
                                <div>
                                    <%#Eval("StudentName")%>
                                </div>
                            </td>
                            <td align="left" style="width: 200px">
                                <div>
                                    <%#Eval("FatherName")%>
                                </div>
                            </td>
                            <td align="left" style="width: 220px">
                                <div>
                                    <%#Eval("Course")%>
                                </div>
                            </td>
                            <td align="center" style="width: 150px">
                                <asp:DropDownList ID="ddlistCYear" runat="server">
                                    <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                    <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                    <asp:ListItem Value="3">3DR YEAR</asp:ListItem>
                                    <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                                    <asp:ListItem Value="5">PASSED OUT</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div align="center" style="padding: 20px">
            <asp:Button ID="btnUpdate" runat="server" Text="Update Record" OnClick="btnUpdate_Click" Visible="false" />
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
