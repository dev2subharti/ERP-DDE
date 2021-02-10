<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="SetQP.aspx.cs" Inherits="DDE.Web.Admin.SetQP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Set Question Paper
            </div>
             <div style="padding: 10px">
            <table class="tableStyle2" cellspacing="10px">
            <tbody align="left">
            
          
                <tr>
                    <td>
                        Paper Code
                    </td>
                    <td>
                    :
                    </td>
                    <td>
                        <asp:Label ID="lblPaperCode" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        Subject Codes
                    </td>
                    <td>
                    :
                    </td>
                    <td>
                        <asp:Label ID="lblSubjectCode" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        Subject Name
                    </td>
                    <td>
                    :
                    </td>
                    <td>
                        <asp:Label ID="lblSubjectName" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        Year
                    </td>
                    <td>
                    :
                    </td>
                    <td>
                        <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        Examination
                    </td>
                    <td>
                    :
                    </td>
                    <td>
                        <asp:Label ID="lblExamName" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblExamCode" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                  </tbody>
            </table>
        </div>
            <div align="center">
                <asp:DataList ID="dtlistShowQP" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left">
                                    <b>Question</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                   Q.<asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblQN" runat="server" Text='<%#Eval("QN")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left">
                                   <asp:Image ID="imgQuestion" ImageUrl='<%#Eval("Question")%>' runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnSet" CssClass="btn" runat="server" Text="Set Question Paper" 
                    onclick="btnSet_Click" />
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
        <div style="padding-top:10px">
         <asp:Button ID="btnShow" runat="server" CssClass="btn" Text="Show Question Paper" 
                onclick="btnShow_Click" />
        </div>
       

    </asp:Panel>
</asp:Content>
