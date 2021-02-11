<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ResultCalcuate.aspx.cs" Inherits="DDE.Web.Admin.ResultCalcuate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Search Student
            </div>
            <div align="center" style="padding-bottom: 5px">
                <table cellspacing="10px" class="tableStyle1">
                    <tr>
                        <td valign="top" align="left">
                            <asp:RadioButtonList ID="rblSearchType" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="rblSearchType_SelectedIndexChanged">
                                <%--<asp:ListItem Selected="True" Value="1">By OANo.</asp:ListItem>
                                     <asp:ListItem Value="2">By Application No</asp:ListItem>--%>
                                <asp:ListItem Value="3" Selected="True">By Enrollment No</asp:ListItem>
                                <%--<asp:ListItem Value="4">By Student Name</asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>

                            <td>
                                <asp:TextBox ID="tbID" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCenterCode" runat="server" Width="200px"></asp:DropDownList>

                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="120px" Height="26px" />
                            </td>

                            <td>
                                <asp:Button ID="btncalculate" runat="server" Text="Result Clculate" OnClick="btncalculate_Click"
                                    Width="120px" Height="26px" />
                            </td>
                            <%--<td>
                                <asp:Button ID="btnCalculate" runat="server" Text="Result Clculate1" OnClick="btnCalculate_Click1"
                                    Width="120px" Height="26px" />
                            </td>--%>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:DataList ID="dtlistShowStudents" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem"
                    OnItemCommand="dtlistShowStudents_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px; padding-left: 5px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 220px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>SC Code</b>
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

                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 220px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Print Result" OnClick="lnkbtnEdit_Click"></asp:LinkButton>
                                </td>

                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>

            <div>
                <asp:DataList ID="dtaLstError" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 120px; padding-left: 5px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Remarks</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 120px">
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("papercd")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("remarks")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td align="center" style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="padding-top: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
    </div>
</asp:Content>
