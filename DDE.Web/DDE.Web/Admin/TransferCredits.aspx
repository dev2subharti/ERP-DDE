<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="TransferCredits.aspx.cs" Inherits="DDE.Web.Admin.TransferCredits" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
      <div align="center" class="heading" style="padding-top: 20px">
               Transfer Credits
            </div>
            <div align="center" style="padding: 20px" class="text">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Select Session</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSession" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10px">
                            &nbsp;
                        </td>
                        <td>
                            <b>Select Course</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCourses" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10px">
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rblPhoto" runat="server">
                                <asp:ListItem>With Photo</asp:ListItem>
                                <asp:ListItem Selected="True">Without Photo</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 10px">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowRegistration" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowRegistration_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 132px; padding-left: 15px">
                                    <b>Photo</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px; padding-left: 8px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <asp:Image ID="imgPhoto" ImageUrl='<%#Eval("StudentPhoto")%>' runat="server" Width="100px"
                                        Height="100px" />
                                </td>
                                <td align="left" style="width: 125px">
                                    <%#Eval("EnrollmentNo")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="center" style="width: 120px">
                                    <asp:LinkButton ID="lnkbtnTC" runat="server" Text="Transfer Credits" CommandName="TC" CommandArgument='<%#Eval("SRID") %>'></asp:LinkButton>
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
