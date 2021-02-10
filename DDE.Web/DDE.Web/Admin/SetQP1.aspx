<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="SetQP1.aspx.cs" Inherits="DDE.Web.Admin.SetQP1" %>

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
                 <tr>
                    <td>
                        MOE
                    </td>
                    <td>
                    :
                    </td>
                    <td>
                        <asp:Label ID="lblMOEA" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblMOE" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                  </tbody>
            </table>
        </div>
            <div  align="center">
                 <div align="center" style="padding-left: 50px">
                    <asp:DataList ID="dtlistShowQP" Width="760px" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem">

                        <ItemTemplate>
                            <div align="left">
                                <table>
                                <tbody >
                                     <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblSNo" Font-Bold="true" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>.&nbsp;
                                       
                                        <asp:Label ID="lblQID" runat="server" Text='<%#Eval("QID")%>' CssClass="ef" Visible="false"></asp:Label>
                                       
                                    </td>
                                    <td valign="top">
                                     <b><%#Eval("Question")%></b> 
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">&nbsp;
                                    </td>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td>
                                                    (A)
                                                </td>
                                                <td>
                                                   <%#Eval("A")%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    (B)
                                                </td>
                                                <td>
                                                   <%#Eval("B")%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    (C)
                                                </td>
                                                <td>
                                                   <%#Eval("C")%>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    (D)
                                                </td>
                                                <td>
                                                   <%#Eval("D")%>
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                                </tbody>
                               
                            </table>
                            </div>
                            
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:DataList ID="dtlistShowQPI" Width="760px" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem">

                        <ItemTemplate>
                            <div align="left">
                                <table>
                                <tbody >
                                     <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblSNo" Font-Bold="true" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>.&nbsp;
                                        <asp:Label ID="lblQID" runat="server" Text='<%#Eval("QID")%>' CssClass="ef" Visible="false"></asp:Label>
                                       
                                    </td>
                                    <td valign="top">
                                    <asp:Image ID="imgQues" runat="server"  />
                                    </td>
                                </tr>
                                
                                </tbody>
                               
                            </table>
                            </div>
                            
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnSet" CssClass="btn" Width="150px" runat="server" Text="Set Question Paper" 
                    onclick="btnSet_Click" />
            </div>
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