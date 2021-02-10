<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="CheckQueryStatusForExam.aspx.cs" Inherits="DDE.Web.Admin.CheckQueryStatusForExam" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Show Query Status For  Examination
            </div>
             <div style="padding: 10px">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td>
                                    Filter Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistFilter" runat="server" Width="150px">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem Value="1">PENDING</asp:ListItem>
                                        <asp:ListItem Value="2">OK</asp:ListItem>
                                        <asp:ListItem Value="3">ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" />
                                </td>
                                
                            </tr>
                        </table>
                    </div>
            <div align="center">
                <asp:DataList ID="dtlistQStatus" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistQStatus_ItemCommand" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>For Year</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 115px">
                                    <b>SC Code</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>Status</b>
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
                                <td align="left" style="width: 150px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 90px">
                                    <%#Eval("ForYear")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblQStatus" runat="server" Text='<%#Eval("Status")%>' Width="70px" Height="20px"></asp:Label>   
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CommandName='<%#Eval("ForYear")%>' CommandArgument='<%#Eval("ExamRecordID") %>'></asp:Button>
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
     </ContentTemplate>
        <Triggers>
           <%-- <asp:AsyncPostBackTrigger ControlID="btnConfirm" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
