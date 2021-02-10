<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowECPStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowECPStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show Participating Students of Examination Centres
        </div>
        <div align="center" class="text" style="padding-top: 20px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td align="left">
                        <b>Examination</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistExam" AutoPostBack="true" Width="150px" runat="server"
                            OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>Mode of Examination</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistMOE" AutoPostBack="true" Width="150px" runat="server"
                            OnSelectedIndexChanged="ddlistMOE_SelectedIndexChanged">
                            <asp:ListItem Value="R">REGULAR</asp:ListItem>
                            <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>SC Code</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistSCCode" AutoPostBack="true" Width="150px" runat="server"
                            OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                            <asp:ListItem>ALL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" class="text" style="padding-top: 20px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td align="left">
                        <b>Examination Centre</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistEC" Width="150px" runat="server" OnSelectedIndexChanged="ddlistEC_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px">
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" />
        </div>
        <div>
            <div align="center" style="padding-top: 20px">
                <asp:DataList ID="dtlistShowDS" runat="server" CellPadding="0" CssClass="dtlist" CellSpacing="0" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left" >
                            <tr>
                                <td align="left" style="width: 60px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Roll No</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>AF Code</b>
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
                                <td align="left" style="width: 80px">
                                    <b>Year</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="font-weight: normal" >
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("RollNo")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("SName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("Year")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" Visible="false">
        <div style="padding-top: 20px">
            <table class="tableStyle2">
                <tr>
                    <td style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
