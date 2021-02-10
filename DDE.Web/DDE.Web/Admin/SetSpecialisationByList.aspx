<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="SetSpecialisationByList.aspx.cs" Inherits="DDE.Web.Admin.SetSpecialisationByList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading" style="padding-bottom: 20px">
            Set Specialisation
        </div>
        <div>
            <div align="center" class="text" style="padding-bottom: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Session</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSession" runat="server">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>Course</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCourse" runat="server">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-bottom: 20px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Style="height: 26px" 
                    Width="82px" onclick="btnSearch_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowStudents" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 130px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father's Name</b>
                                </td>
                                <td align="center" style="width: 170px">
                                    <b>1st Year</b>
                                </td>
                                <td align="center" style="width: 170px">
                                    <b>2nd Year</b>
                                </td>
                                <td align="center" style="width: 170px">
                                    <b>3rd Year</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"> 
                                    </asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'> 
                                    </asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 170px">
                                    <asp:DropDownList ID="ddlist1Year" Width="150px" Enabled="false" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 170px">
                                    <asp:DropDownList ID="ddlist2Year" Width="150px" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="left" style="width: 170px">
                                    <asp:DropDownList ID="ddlist3Year" Width="150px" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 20px">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" Style="height: 26px" 
                    Width="82px" onclick="btnUpdate_Click"  />
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
