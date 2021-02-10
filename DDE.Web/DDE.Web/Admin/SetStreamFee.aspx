<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="SetStreamFee.aspx.cs" Inherits="DDE.Web.Admin.SetStreamFee" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Set Stream Fee
        </div>
        <div style="padding: 20px">
            <div align="center">
                <asp:DataList ID="dtlistShowCourses" CssClass="dtlist" GridLines="Both" BorderWidth="1px"
                    runat="server" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table width="600px" align="left">
                            <tr>
                                <td align="left" style="width: 50px; padding-left: 8px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Course Code</b>
                                </td>
                                <td align="left" style="width: 260px">
                                    <b>Course Name</b>
                                </td>
                                <td align="left" style="width: 90px">
                                    <b>Stream Fee</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="600px" rules="cols" align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblCourseID" Visible="false" runat="server" Text='<%#Eval("CourseID")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("CourseCode")%>
                                </td>
                                <td align="left" style="width: 250px">
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%#Eval("CourseShortName")%>'></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:TextBox ID="tbStreamFee" Text='<%#Eval("StreamFee")%>' Width="60px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
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
