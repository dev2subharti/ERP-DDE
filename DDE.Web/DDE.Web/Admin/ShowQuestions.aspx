<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ShowQuestions.aspx.cs" Inherits="DDE.Web.Admin.ShowQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show Questions
        </div>
        <div>
                <asp:RadioButtonList ID="rblType" AutoPostBack="true"   CssClass="tableStyle2" runat="server" OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="0">ENGLISH</asp:ListItem>
                    <asp:ListItem Value="1">HINDI</asp:ListItem>
                </asp:RadioButtonList>
         </div>
        <div>
            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Paper Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistPaperCode" runat="server">
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
                <asp:DataList ID="dtlistShowQuestions"   runat="server" CellPadding="0" 
                    CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS" OnItemCommand="dtlistShowQuestions_ItemCommand" >
                    <HeaderTemplate>
                        <table  align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 50px; border-right:1px solid black">
                                    <b>S.No.</b>
                                </td>
                                 <td align="center" style="width: 300px; border-right:1px solid black">
                                    <b>Question</b>
                                </td>
                                <td align="center" style="width: 100px; border-right:1px solid black">
                                    <b>Option A</b>
                                </td>
                                <td align="center" style="width: 100px; border-right:1px solid black">
                                    <b>Option B</b>
                                </td>
                                <td align="center" style="width: 100px; border-right:1px solid black">
                                    <b>Option C</b>
                                </td>
                               <td align="center" style="width: 100px; border-right:1px solid black">
                                    <b>Option D</b>
                                </td>
                                 <td align="center" style="width: 100px; border-right:1px solid black">
                                    <b>Key</b>
                                </td>
                               
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin:0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width:40px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblQID" Visible="false" runat="server" Text='<%#Eval("QID")%>'></asp:Label>
                                </td>
                                 <td align="center" class="border_lbr" style="width:290px">
                                     <%#Eval("Question")%>
                                </td>
                                <td align="center" class="border_lbr" style="width:90px">
                                     <%#Eval("A")%>
                                </td>
                                <td align="center" class="border_lbr" style="width:90px">
                                     <%#Eval("B")%>
                                </td>
                                <td align="center" class="border_lbr" style="width:90px">
                                     <%#Eval("C")%>
                                </td>
                                <td align="center" class="border_lbr" style="width:90px">
                                     <%#Eval("D")%>
                                </td>
                                 <td align="center" class="border_lbr" style="width:90px">
                                     <%#Eval("Key")%>
                                </td>
                                <td align="center" class="border_rb" style="width: 50px">
                                   <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit"
                                                                          CommandArgument='<%#Eval("QID") %>'></asp:LinkButton>                                                      
                                 </td>
                                <td align="center" class="border_rb" style="width: 50px">
                                   <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" CommandName="Delete"
                                     OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                     CommandArgument='<%#Eval("QID") %>'></asp:LinkButton>                                                      
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