<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowSLM.aspx.cs" Inherits="DDE.Web.Admin.ShowSLM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show SLM Linking with Course and Year (On Trial)
        </div>
        <div style="padding-top:20px">
           
            <div align="center">
                <asp:DataList ID="dtlistShowSLM" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS">
                    <HeaderTemplate>
                        <table align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 50px; border-right: 1px solid black">
                                    <b>SNo</b>
                                </td>
                                <td align="center" style="width: 112px; border-right: 1px solid black">
                                    <b>SLM Code</b>
                                </td>
                                <td align="center" style="width: 310px; border-right: 1px solid black">
                                    <b>Title</b>
                                </td>
                                <td align="center" style="width: 80px; border-right: 1px solid black">
                                    <b>Cost</b>
                                </td>
                                <td align="center" style="width: 371px; border-right: 1px solid black">
                                    <b>Courses</b>
                                </td>
                                <td align="center" style="width: 60px; border-right: 1px solid black">
                                    <b>Year</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 39px">
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblSLMID" runat="server" Text='<%#Eval("SLMID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="center" class="border_rb" style="width: 102px">
                                    <asp:Label ID="lblSLMCode" runat="server" Text='<%#Eval("SLMCode")%>'></asp:Label>
                                </td>
                                <td align="center" class="border_rb" style="width: 300px">
                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                </td>
                                <td align="center" class="border_rb" style="width: 70px">
                                    <asp:Label ID="lblCost" runat="server" Text='<%#Eval("Cost")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowCourses" runat="server">
                                        <ItemTemplate>
                                            <table align="left" cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="left" class="border_rb" style="width: 50px">
                                                        <%#Eval("CSNo")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("Course")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 50px">
                                                        <%#Eval("Year")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                <td align="center" class="border_rb" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit" Visible="false" CommandArgument='<%#Eval("SLMID")%>' OnClick="Edit_SLM">Edit</asp:LinkButton>
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
