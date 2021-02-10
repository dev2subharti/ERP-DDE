<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowStudentsCourseWise.aspx.cs" Inherits="DDE.Web.Admin.ShowStudentsCourseWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show Students Course Wise (Under Testing)
        </div>
        <div>
            <div style="padding-top: 20px; padding-bottom: 10px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Batch</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSession" AutoPostBack="true" Width="120px" runat="server"
                                OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" AutoPostBack="true" Width="120px" runat="server"
                                OnSelectedIndexChanged="ddlistYear_SelectedIndexChanged">
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <asp:Panel ID="pnlRecord" runat="server" Visible="false">
                <div align="center" style="padding-top: 20px">
                    <asp:DataList ID="dtlistRecord" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistRecord_ItemCommand">
                        <HeaderTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 60px">
                                        <b>S.No.</b>
                                    </td>
                                    <td align="left" style="width: 300px">
                                        <b>Course</b>
                                    </td>
                                    <td align="left" style="width: 140px">
                                        <b>No. of Students</b>
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
                                    <td align="left" style="width: 300px">
                                        <%#Eval("Course")%>
                                    </td>
                                    <td align="right" style="width: 50px">
                                        <asp:LinkButton ID="lnkbtnTS" ForeColor="Blue" runat="server" Text='<%#Eval("TS") %>'
                                            CommandName='<%#Eval("Course") %>' CommandArgument='<%#Eval("SRIDS") %>'></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div align="center">
                    <div align="right" class="tableStyle2" style="width: 400px; padding-right: 80px;
                        padding-top: 5px; padding-bottom: 5px">
                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </asp:Panel>
    </div> </asp:Panel>
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
