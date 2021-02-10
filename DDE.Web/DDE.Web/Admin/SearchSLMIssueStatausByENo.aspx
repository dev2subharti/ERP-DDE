<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="SearchSLMIssueStatausByENo.aspx.cs" Inherits="DDE.Web.Admin.SearchSLMIssueStatausByENo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Search SLM Issue Status
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                               Enrollment No.
                            </td>
                            <td>
                                <asp:TextBox ID="tbENo" runat="server" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search"  OnClick="btnsearch_Click"
                                    Width="120px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
            <asp:DataList ID="dtlistShowStudents" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" 
                   
                   >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>SNo.</b>
                                </td>
                                 <td align="left" style="width: 130px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 60px">
                                    <b>Year</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>SC Code</b>
                                </td>
                                 <td align="left" style="width: 50px">
                                    <b>L.G.</b>
                                </td>
                                <td align="left" style="width:50px">
                                    <b>LNo.</b>
                                </td>                    
                                <td align="left" style="width: 120px">
                                    <b>Generated On</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>L.P.</b>
                                </td>
                                 <td align="left" style="width: 120px">
                                    <b>Processed On</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>Dis. Type</b>
                                </td>
                                 <td align="left" style="width: 120px">
                                    <b>Docket No</b>
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
                                <td align="left" style="width: 130px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 60px">
                                    <%#Eval("Year")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 50px">
                                   <asp:Label ID="lblLG" runat="server"  Text=' <%#Eval("LG")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 50px">
                                    <%#Eval("LID")%>
                                </td>                            
                                 <td align="left" style="width:120px">
                                    <%#Eval("LGTime")%>
                                </td>
                                <td align="left" style="width: 50px">
                                     <asp:Label ID="lblLP" runat="server" Text=' <%#Eval("LP")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("LPTime")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("DType")%>
                                </td>
                                 <td align="left" style="width: 120px">
                                    <%#Eval("DokNo")%>
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
   
</asp:Content>

