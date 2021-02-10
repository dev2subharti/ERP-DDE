<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowListofAwardSheets.aspx.cs" Inherits="DDE.Web.Admin.ShowListofAwardSheets" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div>
                <asp:Panel ID="pnlFilter" runat="server">
                    <table cellspacing="10px" class="tableStyle1">
                        <tr>
                            <td valign="top" align="left">
                                <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="1">Date wise entry</asp:ListItem>
                                    <asp:ListItem Value="2">Student wise entry</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                List of Award Sheets
            </div>
            <div style="padding-bottom: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            Examination
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" runat="server">
                                <asp:ListItem Value="A13">JUNE 2013</asp:ListItem>
                                <asp:ListItem Value="B13">DECEMBER 2013</asp:ListItem>
                                <asp:ListItem Value="A14">JUNE 2014</asp:ListItem>
                                <asp:ListItem Value="B14">DECEMBER 2014</asp:ListItem>
                                <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                 <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                  <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                                    <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                                <asp:ListItem Value="W10" Selected="True">JUNE 2019</asp:ListItem>
                                 <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Select Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistType_SelectedIndexChanged">
                                <asp:ListItem>NOT PRINTED</asp:ListItem>
                                <asp:ListItem>PRINTED</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Panel ID="pnlRange" runat="server" Visible="false">
                    <table class="tableStyle2">
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                To
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="tbFrom" runat="server" Width="80px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="tbTo" runat="server" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div align="center" style="padding: 10px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>
            <div align="center" style="padding: 10px" class="text">
                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
            </div>
            <div align="center">
                <asp:DataList ID="dtlistASReport" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistASReport_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>AS No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="left" style="width: 350px">
                                    <b>Subject Name</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Total</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Present</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Absent</b>
                                </td>
                               
                                <td align="left" style="width: 120px">
                                    <b>Printed On</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Printed By</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Allotted To</b>
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
                                <td align="left" style="width: 80px">
                                    <asp:Label ID="lblASNo" runat="server" Text='<%#Eval("ASNo")%>'></asp:Label>
                                    <asp:Label ID="lblPM" runat="server" Text='<%#Eval("PrintMode")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("PaperCode")%>
                                </td>
                                <td align="left" style="width: 350px">
                                    <%#Eval("SubjectName")%>
                                </td>
                                <td align="left" style="width: 80px">
                                     <%#Eval("TotalStudents")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <asp:Label ID="lblTS" runat="server" Text='<%#Eval("Present")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("Absent")%>
                                </td>
                               
                                <td align="left" style="width: 120px">
                                    <%#Eval("Period")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("PrintedBy")%>
                                </td>
                                 <td align="left" style="width: 150px">
                                    <%#Eval("AT")%>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnPrint" runat="server" Text="Print" CommandName='<%#Eval("ASNo") %>'
                                        CommandArgument='<%#Eval("PaperCode") %>'></asp:LinkButton>
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
