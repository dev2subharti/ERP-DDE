<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="MakeRROnline.aspx.cs" Inherits="DDE.Web.Admin.MakeRROnline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
<asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Make Result Online
            </div>
            <div align="center" class="text" style="padding-top: 20px; padding-bottom: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Examination</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged1">
                                <asp:ListItem>--Select One--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Mode of Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistMOE" runat="server" Width="150px" 
                                AutoPostBack="true" onselectedindexchanged="ddlistMOE_SelectedIndexChanged1">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
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
                <asp:DataList ID="dtlistShowCourses" runat="server" Visible="false" CssClass="dtlist"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" 
                    OnItemCommand="dtlistShowCourses_ItemCommand" 
                   >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 320px">
                                    <b>Course Name</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>1st Year</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>2nd Year</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>3rd Year</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblCourseID" runat="server" Text='<%#Eval("CourseID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 280px">
                                    <%#Eval("CourseName")%>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:Button ID="btn1Year" runat="server" Text="MAKE ONLINE" CommandArgument="1" BackColor="#F8A403" Width="110px" />
                                    <asp:Label ID="R1Y" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:Button ID="btn2Year" runat="server" Text="MAKE ONLINE" CommandArgument="2" BackColor="#F8A403" Width="110px" />
                                    <asp:Label ID="R2Y" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 150px">
                                     <asp:Button ID="btn3Year" runat="server" Text="MAKE ONLINE" CommandArgument="3" BackColor="#F8A403" Width="110px" />
                                    <asp:Label ID="R3Y" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
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
    </ContentTemplate>
       <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn1Year" />
            <asp:AsyncPostBackTrigger ControlID="btn2Year" />
            <asp:AsyncPostBackTrigger ControlID="btn3Year" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
