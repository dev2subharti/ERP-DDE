<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="ShowCTStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowCTStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Show CT Students
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" >
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>Examination</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExamination" runat="server">
                              
                                </asp:DropDownList>
                               
                            </td>
                           
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            
             <div align="center">
                <asp:DataList ID="dtlistShowList" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>SC Code</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Course</b>
                                </td>
                                 <td align="left" style="width: 60px">
                                    <b>Year</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Previous Inst.</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>Paper 1</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>Paper 2</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 40px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("SCCode")%>
                                </td>
                                  <td align="left" style="width: 200px">
                                    <%#Eval("Course")%>
                                </td>
                                 <td align="left" style="width:60px">
                                    <%#Eval("Year")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <%#Eval("PreIns")%>
                                </td>
                              <td align="left" style="width: 100px">
                                    <%#Eval("Paper1")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("Paper2")%>
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