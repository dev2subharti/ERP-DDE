<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ShowNAECStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowNAECStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Students Who Are Not Alotted Exam Centre</b>
            </div>
            <div align="center" class="text" style="padding-top: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Examination</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExamination" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="text" align="center" style="padding-top: 20px">
                <asp:DataList ID="dtlistShowRegistration" runat="server" CssClass="dtlist" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowRegistration_ItemCommand">
                   
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 60px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Roll No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                
                                <td align="left" style="width: 100px">
                                    <b>SC Code</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>MOE</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                 <td align="left" style="width: 120px">
                                    <%#Eval("RollNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                              
                                <td align="left" style="width: 80px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 50px">
                                    <%#Eval("MOE")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnAlotEC" runat="server" Text="Alot EC" CommandName= '<%#Eval("EnrollmentNo")%>'
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

