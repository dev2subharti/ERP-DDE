<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="PublishPasswordEC.aspx.cs" Inherits="DDE.Web.Admin.PublishPasswordEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 0px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Passwords of Examination Centres
            </div>
            <div style="padding-bottom: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            Examination
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistEC" runat="server">
                             <asp:ListItem Value="Z11">DECEMBER 2020</asp:ListItem>
                             <asp:ListItem Value="W11">JUNE 2020</asp:ListItem>
                             <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>
                             <asp:ListItem Value="W10">JUNE 2019</asp:ListItem>
                             <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                             <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                             <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                             <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                             <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                             <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                             <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>                              
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                onclick="btnSearch_Click" />
                        </td>
                       
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowExamCentres" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowExamCentres_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 60px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 110px">
                                    <b>E.C. Code</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>S.C. Code</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Password</b>
                                </td>
                                <td align="left" style="width: 300px">
                                    <b>Email</b>
                                </td>
                                <td align="left" style="width: 300px">
                                    <b>Exam Centre Name</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblPassMailSent" runat="server" Text='<%#Eval("PassMailSent")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 110px">
                                    <%#Eval("ExamCentreCode")%>
                                </td>
                                 <td align="left" style="width: 80px">
                                    <%#Eval("SCCodes")%>
                                </td>
                                  <td align="left" style="width: 100px">
                                    <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password")%>'></asp:Label>
                                </td>
                                 <td align="left" style="width: 300px">
                                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                </td>
                              
                                <td align="left" style="width: 300px">
                                    <%#Eval("CentreName")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Button ID="btnSendMail" runat="server" Text="Send To Mail" CommandArgument='<%#Eval("ECID")%>'
                                        CommandName="Send Mail" />
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CommandArgument='<%#Eval("ECID")%>'
                                        CommandName="Reset Password" />
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

