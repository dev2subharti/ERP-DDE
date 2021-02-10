<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="SetQualifyingStatus.aspx.cs" Inherits="DDE.Web.Admin.SetQualifyingStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Set Qualifying Status
            </div>
            <div align="center" class="text" style="padding-top: 20px; padding-bottom: 20px">
                <table cellspacing="10px" class="tableStyle2">
                   <%-- <tr>
                        <td align="center">
                            <table>
                                <tr>
                                    <td align="left">
                                        <b>Select Mode</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlistMode" runat="server" Width="150px" 
                                            AutoPostBack="true" 
                                            onselectedindexchanged="ddlistMode_SelectedIndexChanged" >
                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                            <asp:ListItem>MANUAL</asp:ListItem>
                                            <asp:ListItem>AUTOMATIC</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <table cellspacing="10px">
                                <tr>
                                    <td align="left">
                                        <b>Batch</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <b>Course</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        <b>Year</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlistYear" runat="server" Width="150px" 
                                            AutoPostBack="true" onselectedindexchanged="ddlistYear_SelectedIndexChanged"
                                            >
                                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                            <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding-bottom: 20px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click"  /><br />
                    <asp:Button ID="btnSQS" runat="server" Text="Set Qualifying Status" Style="height: 26px" 
                     Visible="false" onclick="btnSQS_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowStudents" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 210px">
                                    <b>Student Name</b>
                                </td>
                                <td align="center" style="width: 150px">
                                    <b>Qualifying Status</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 210px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:DropDownList ID="ddlistQStatus" runat="server">
                                        <asp:ListItem Value="AC">All Clear</asp:ListItem>
                                        <asp:ListItem Value="PCP">PCP</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:Label ID="lblQStatus" runat="server" Text='<%#Eval("QStatus")%>' Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div align="center" style="padding: 20px">
                <asp:Button ID="btnSubmit" runat="server" Text="Set Status" OnClick="btnSubmit_Click"
                    Visible="false" />
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
         <div align="center" style="padding: 20px">
                <asp:Button ID="btnOK" runat="server" Text="OK"
                    Visible="false" onclick="btnOK_Click" />
            </div>
    </asp:Panel>
</asp:Content>
