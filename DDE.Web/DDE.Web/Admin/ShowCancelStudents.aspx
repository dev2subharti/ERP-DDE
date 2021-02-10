<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="ShowCancelStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowCancelStudents" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Canceled Admissions</b>
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowRegistration" runat="server" BorderColor="#0099FF" BorderStyle="Solid"
                    BorderWidth="1px" OnItemCommand="dtlistShowRegistration_ItemCommand">
                    <ItemStyle BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" />
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px; padding-left: 8px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Batch</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <HeaderStyle BackColor="#66CCFF" BorderColor="Black" />
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px; padding-left: 8px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("Session")%>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:LinkButton ID="lnkbtnRecycle" runat="server" Text="Re-Admit" CommandName= '<%#Eval("EnrollmentNo")%>'
                                        CommandArgument='<%#Eval("SRID") %>'></asp:LinkButton>
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