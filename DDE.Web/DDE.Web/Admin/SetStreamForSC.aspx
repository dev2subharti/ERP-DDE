<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="SetStreamForSC.aspx.cs" Inherits="DDE.Web.Admin.SetStreamForSC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            Allot Stream to Study Centre
        </div>
        <div style="padding-top: 20px; padding-bottom: 20px">
            <table cellspacing="10px" class="tableStyle2">
                <tr>
                    
                    <td>
                        SC Code
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistSCCode" runat="server" AutoPostBack="true" 
                            Width="150px" onselectedindexchanged="ddlistSCCode_SelectedIndexChanged">
                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                            Width="75px" Height="26px" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding: 20px">
            <div align="center">
                <asp:DataList ID="dtlistShowCourses" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 300px">
                                    <b>Course Name</b>
                                </td>
                                <td align="center" style="width: 50px">
                                    <b>Alloted</b>
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
                                <td align="left" style="width: 300px">
                                    <%#Eval("CourseName")%>
                                    <asp:Label ID="lblCourseID" runat="server" Text='<%#Eval("CourseID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 50px">
                                    <asp:CheckBox ID="cbAllot" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div align="center" style="padding-top:10px">
                <asp:Button ID="btnUpdate" runat="server" Visible="false" Text="Update Streams" 
                    onclick="btnUpdate_Click" />
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
