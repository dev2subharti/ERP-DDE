<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="SetTMSCWiseDec12.aspx.cs" Inherits="DDE.Web.Admin.SetTMSCWiseDec12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <div align="center">
                <asp:Panel ID="pnlData" runat="server" Visible="false">
                    <div class="heading">
                        Set Total Mark Sheet for December 12 Examination
                    </div>
                    <div style="padding-top: 20px; padding-bottom: 20px">
                        <asp:Panel ID="pnlSet" runat="server" Visible="false">
                            <table cellspacing="10px" class="tableStyle2">
                                <tr>
                                    <td>
                                        SC Code
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistSCCode" Width="80px" runat="server">
                                       
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                       Total Mark Sheet
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbTM" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSet" runat="server" Text="Set" 
                                            Width="75px" Height="26px" onclick="btnSet_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                   <div>
                   <asp:DataList ID="dtlistShowSC" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistShowSC_ItemCommand" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px; padding-left:10px">
                                    <b>SC Code</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Total Mark Sheet</b>
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
                                <td align="left" style="width: 150px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("TM")%>
                                </td>
                               
                                <td align="center" style="width: 100px">
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Show List" CommandName="Show" CommandArgument='<%#Eval("SCCode") %>'></asp:LinkButton>
                                </td>                            
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                   </div>
                    
                </asp:Panel>
                <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
                    <table class="tableStyle2">
                        <tr>
                            <td align="center" style="padding: 30px">
                                <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="padding-top: 20px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
            <asp:PostBackTrigger ControlID="btnSet" />
            <asp:PostBackTrigger ControlID="dtlistShowSC" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
