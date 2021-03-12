<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowPractical.aspx.cs" Inherits="DDE.Web.Admin.ShowPractical" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
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
                                <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
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
                <asp:DataList ID="dtlistShowPracticals" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowPracticals_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="center" style="width: 250px">
                                    <b>Practical Name</b>
                                </td>
                                <td align="center" style="width: 150px">
                                    <b>Practical Max Marks</b>
                                </td>
                                <td align="center" style="width: 200px">
                                    <b>Allowed for Award Sheet</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("PracticalSNo")%>
                                    <asp:Label ID="lblPracticalID" runat="server" Text='<%#Eval("PracticalID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 250px">
                                    <%#Eval("PracticalName")%>
                                </td>
                                <td align="center" style="width: 150px">
                                    <%#Eval("PracticalMaxMarks")%>
                                </td>
                                <td align="left" style="width: 180px">
                                   <%#Eval("AllowedForAS")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("PracticalID") %>'></asp:LinkButton>
                                </td>
                                 <td align="center" style="width: 50px">
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete"
                                    OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                    CommandArgument='<%#Eval("PracticalID") %>'></asp:LinkButton>
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
