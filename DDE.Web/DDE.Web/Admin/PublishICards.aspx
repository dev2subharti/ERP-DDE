<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="PublishICards.aspx.cs" Inherits="DDE.Web.Admin.PublishICards" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading" style="padding-bottom: 20px">
            Publish I Cards
        </div>
        <div>
            <table cellspacing="10px" class="tableStyle2">
                <tr>
                    <td align="left">
                        <b>Batch</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <b>Study Centre Code</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistSCCode" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnSearch" runat="server" Text="Search" onclick="btnSearch_Click" 
                 />
        </div>
        <asp:Panel ID="pnlStudentList1" runat="server" Visible="false">
            <div align="center" style="padding-top: 20px">
                <table width="800px">
                    <tr>
                        <td style="padding-left: 150px">
                            <asp:Button ID="btnPublish1" runat="server" Text="Publish I Card" CssClass="btn"
                                OnClick="btnPublish1_Click" />
                        </td>
                        <td align="right" style="width: 80px">
                            <asp:Button ID="btnSelectAll" runat="server" Text="Select All" OnClick="btnSelectAll_Click" />
                        </td>
                        <td align="right" style="width: 80px">
                            <asp:Button ID="btnClearAll" runat="server" Text="Clear All" OnClick="btnClearAll_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="text" align="center" style="padding-top: 10px">
                <asp:DataList ID="dtlistShowRegistration" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                               <%-- <td align="center" style="width: 148px">
                                    <b>Photo</b>
                                </td>--%>
                                <td align="left" style="width: 135px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="center" style="width: 50px">
                                    <b>Select</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                </td>
                              <%--  <td align="left" style="width: 140px">
                                    <asp:Image ID="imgPhoto" ImageUrl='<%#Eval("StudentPhoto")%>' runat="server" Width="100px"
                                        Height="100px" />
                                </td>--%>
                                <td align="left" style="width: 135px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 50px">
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnPublish" runat="server" Text="Publish I Card" CssClass="btn" OnClick="btnPublish_Click" />
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle2">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
