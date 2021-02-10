<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ShowOnlineREApplications.aspx.cs" Inherits="DDE.Web.Admin.ShowOnlineREApplications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Show Online Main Exam Applications</b>
            </div>
            <div align="center" class="text" style="padding-bottom: 10px; padding-top: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                    <td>
                            <asp:Label ID="lblExamination" runat="server" Text="Batch"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExamination"  AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlistExamination_SelectedIndexChanged">
                                <asp:ListItem Value="0">--SELECT ONE--</asp:ListItem>
                                 <asp:ListItem Value="Z11">DECEMBER 2020</asp:ListItem>
                                 <asp:ListItem Value="W11">JUNE 2020</asp:ListItem>
                                 <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>
                                <asp:ListItem Value="A19">OCTOBER 2019</asp:ListItem>
                             <asp:ListItem Value="W10">JUNE 2019</asp:ListItem>
                             <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                            <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                            <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblSCCode" runat="server" Text="SC Code" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode"  runat="server" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="btnFind" runat="server" Visible="true" Text="Search" Style="height: 26px"
                    Width="82px" OnClick="btnFind_Click" />
            </div>
            <div class="text" align="center" style="padding-top: 20px">
                <asp:DataList ID="dtlistShowPending" runat="server" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowPending_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Course</b>
                                </td>
                                 <td align="center" style="width: 80px">
                                    <b>Year</b>
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
                                <td align="left" style="width: 150px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Course")%>
                                </td>
                                 <td align="center" style="width: 80px">
                                    <%#Eval("Year")%>
                                </td>
                                <td align="center" style="width: 80px">
                                    <asp:LinkButton ID="lnkbtnFillFee" runat="server" Text="Register" OnClientClick="aspnetForm.target ='_blank';" CommandName="Register"
                                        CommandArgument='<%#Eval("OERID") %>'></asp:LinkButton>
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
