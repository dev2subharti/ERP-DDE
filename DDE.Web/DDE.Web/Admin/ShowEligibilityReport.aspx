<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ShowEligibilityReport.aspx.cs" Inherits="DDE.Web.Admin.ShowEligibilityReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Show Form Allotment Status</b>
            </div>

            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <asp:Label ID="lblBatch" runat="server" Text="Batch" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch"  AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="ddlistBatch_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem>Q 2020</asp:ListItem>
                                <asp:ListItem>C 2020</asp:ListItem>
                                <asp:ListItem>Q 2019-20</asp:ListItem>
                                <asp:ListItem>A 2019-20</asp:ListItem>
                                <asp:ListItem>Q 2019</asp:ListItem>
                                <asp:ListItem>C 2019</asp:ListItem
                            </asp:DropDownList>
                        </td>
                      

                        <td>
                            <asp:Button ID="btnFind" runat="server" Visible="true" Text="Search" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
           
            <div class="text" align="center" style="padding-top: 10px">
                <asp:DataList ID="dtlistShowPending" runat="server" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Examiner Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Total Form</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Checked</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Pending</b>
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
                                <td align="left" style="width: 200px">
                                   <asp:Label ID="lblExID" runat="server" Visible="false" Text=' <%#Eval("ExID")%>'></asp:Label>
                                <%#Eval("Name")%>
                                </td>
                                
                                <td align="left" style="width: 200px">
                                    <%#Eval("TotalForms")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Checked")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Pending")%>
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