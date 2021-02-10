<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="RecycleStudent.aspx.cs" Inherits="DDE.Web.Admin.RecycleStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Student Recycler</b>
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
                                    <asp:Label ID="lblPSRID" runat="server" Visible="false" Text='<%#Eval("PSRID")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
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
                                <td align="left" style="width: 100px">
                                    <%#Eval("Session")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <asp:LinkButton ID="lnkbtnRecycle" runat="server" Text="Recycle" CommandName= "Recycle"
                                        CommandArgument='<%#Eval("SRID") %>'></asp:LinkButton>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:LinkButton ID="lnkbtnDelete" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to cancel the Admission?');" runat="server" Text="Delete Permanently" CommandName= "Delete"
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
