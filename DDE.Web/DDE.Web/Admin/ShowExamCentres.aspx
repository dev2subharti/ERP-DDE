<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowExamCentres.aspx.cs" Inherits="DDE.Web.Admin.ShowExamCentres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 0px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                List of Examination Centres
            </div>
            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Examination</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExamination" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:DataList ItemStyle-Wrap="true" ID="dtlistShowExamCentres" CssClass="dtlist"
                    runat="server" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem"
                    OnItemCommand="dtlistShowExamCentres_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 130px">
                                    <b>City</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>ECCode</b>
                                </td>
                               
                                <td align="left" style="width: 210px">
                                    <b>Centre Name</b>
                                </td>
                                <td align="left" style="width: 220px">
                                    <b>Email</b>
                                </td>
                                <td align="left" style="width: 180px">
                                    <b>S.C.Codes</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Remark</b>
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
                                    <%#Eval("City")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("ExamCentreCode")%>
                                </td>
                               
                                <td align="left" style="width: 200px">
                                    <%#Eval("CentreName")%>
                                </td>
                                <td align="left" style="width: 250px; padding-left: 10px">
                                    <%#Eval("Email")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("SCCodes")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("Remark")%>
                                </td>  
                                  <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval(" ECID") %>'></asp:LinkButton>
                                </td>                         
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete"
                                        OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                        CommandArgument='<%#Eval("ECID") %>'></asp:LinkButton>
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
