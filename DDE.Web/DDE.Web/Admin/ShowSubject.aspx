<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowSubject.aspx.cs" Inherits="DDE.Web.Admin.ShowSubject" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
     <div align="center" class="heading">
                Show Subjects
            </div>
        <div>
            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Select Syllabus Session</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSySession" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>Select Course</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCourse" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>Select Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server">
                                <asp:ListItem>1st Year</asp:ListItem>
                                <asp:ListItem>2nd Year</asp:ListItem>
                                <asp:ListItem>3rd Year</asp:ListItem>
                                <asp:ListItem>4th Year</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowSubjects" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowSubjects_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 60px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Subject Code</b>
                                </td>
                                <td align="center" style="width: 200px">
                                    <b>Subject Name</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SubjectSNo")%>
                                    <asp:Label ID="lblSubjectID" Visible="false" runat="server" Text='<%#Eval("SubjectID") %>'></asp:Label>
                                </td>
                                <td align="left" style="width: 100px">
                                  <%#Eval("PaperCode")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <%#Eval("SubjectCode")%>
                                </td>
                                 
                                <td align="left" style="width: 400px">
                                    <%#Eval("SubjectName")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("SubjectID") %>'></asp:LinkButton>
                                </td>
                                <%-- <td align="center" style="width: 50px">
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete"
                                    OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                    CommandArgument='<%#Eval("SubjectID") %>'></asp:LinkButton>
                            </td>--%>
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
