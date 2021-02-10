<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowMigrationRequests.aspx.cs"
    MasterPageFile="~/Admin/MasterPages/Admin.Master" Inherits="DDE.Web.Admin.ShowMigrationRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            Show Migration Requests
        </div>
        <div style="padding-top: 20px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td>
                        Type
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistType" Width="120px" runat="server">
                            <asp:ListItem Value="1">REQUESTED</asp:ListItem>
                             <asp:ListItem Value="2">PUBLISHED</asp:ListItem>
                            <asp:ListItem Value="3">RECEIVED</asp:ListItem>
                            <asp:ListItem Value="4">POSTED</asp:ListItem>
                            <asp:ListItem Value="0">ALL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding: 20px">
            <div align="center">
                <asp:DataList ID="dtlistShowMigrationInfo" CssClass="dtlist" Visible="false" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowMigrationInfo_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>SNo.</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 220px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Published</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Received</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Posted</b>
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
                                <td align="left" style="width: 140px">
                                    <%#Eval("EnrollmentNo")%>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblMID" runat="server" Text='<%#Eval("MID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("SName")%>
                                </td>
                                 <td align="center" style="width: 100px">
                                    <asp:Label ID="lblPublished" runat="server" Text='<%#Eval("Published")%>' Visible="false"></asp:Label>
                                    <asp:Image ID="imgPubY" runat="server" Visible="false" Height="25px" Width="25px"
                                        ImageUrl="~/Admin/images/Y1.jpg" />
                                    <asp:Image ID="imgPubN" runat="server" Visible="false" Height="25px" Width="25px"
                                        ImageUrl="~/Admin/images/N1.jpg" />
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblReceived" runat="server" Text='<%#Eval("Received")%>' Visible="false"></asp:Label>
                                    <asp:Image ID="imgRecY" runat="server" Visible="false" Height="25px" Width="25px"
                                        ImageUrl="~/Admin/images/Y1.jpg" />
                                    <asp:Image ID="imgRecN" runat="server" Visible="false" Height="25px" Width="25px"
                                        ImageUrl="~/Admin/images/N1.jpg" />
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblPosted" runat="server" Text='<%#Eval("Posted")%>' Visible="false"></asp:Label>
                                    <asp:Image ID="imgPostY" runat="server" Visible="false" Height="25px" Width="25px"
                                        ImageUrl="~/Admin/images/Y1.jpg" />
                                    <asp:Image ID="imgPostN" runat="server" Visible="false" Height="25px" Width="25px"
                                        ImageUrl="~/Admin/images/N1.jpg" />
                                </td>
                                <td align="left" style="width: 40px">
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                </td>
                                <td align="center" style="width: 60px">
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="50px" CommandName="Edit"
                                        CommandArgument='<%#Eval("MID")%>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnPublish" runat="server" Visible="false" Text="Publish Letter" CommandName="Publish"
                     OnClick="btnPublish_Click" />
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
