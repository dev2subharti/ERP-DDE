<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="ShowProvisionalStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowProvisionalStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Provisional Students</b>
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowPending" runat="server"  CssClass="dtlist" 
                    runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" 
                    onitemcommand="dtlistShowPending_ItemCommand" >
                    
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Application No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>SC Code</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Batch</b>
                                </td>
                                 <td align="left" style="width: 80px">
                                    <b>Reason</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                  
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px; padding-left: 8px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("ApplicationNo")%>
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
                                <td align="left" style="width: 200px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("Session")%>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <%#Eval("Remark")%>
                                </td>
                                <td align="center" style="width: 120px">
                                    <asp:LinkButton ID="lnkbtnRecycle" runat="server" Text="Confirm Admission" CommandName= '<%#Eval("ApplicationNo")%>'
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
