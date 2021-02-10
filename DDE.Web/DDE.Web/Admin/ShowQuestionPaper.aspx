<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowQuestionPaper.aspx.cs" Inherits="DDE.Web.Admin.ShowQuestionPaper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Question Paper</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center">
            <div align="left">
                <div>
                    <div style="float: right">
                        Paper Code :
                        <asp:Label ID="lblPaperCode" runat="server" Text=""></asp:Label>
                    </div>
                    <br />
                    <br />
                    <div style="float: right">
                        Roll No :..................................
                    </div>
                    <br />
                    <div align="center">
                        <div>
                            <asp:Label ID="lblCourseCode" Visible="false" runat="server" Text=""></asp:Label>
                        </div>
                        <div>
                            <b>Examination : </b><asp:Label ID="lblExamination" Font-Bold="true" runat="server" Text=""></asp:Label><asp:Label ID="lblExamCode"  Visible="false" runat="server" Text=""></asp:Label><asp:Label ID="lblMOE"  Visible="false" runat="server" Text=""></asp:Label><br />
                           <b>(<asp:Label ID="lblMOEA" runat="server" Text=""></asp:Label>)</b> 
                        </div>
                        <div>
                            <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblSubjectName" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div style="float: left">
                        Time : 1 Hours ]
                    </div>
                    <div style="float: right">
                        [ Max. Marks : 60
                    </div>
                    <br />
                    <br />
                    <div>
                       <b>Instructions :-</b> 
                        <ul style="margin-top:5px">
                            <li>Attempt All Questions. All questions carry equal marks.</li>
                            <li>Fill the 'Bubble' available in OMR Sheet to mark your Answer.</li>
                            <li>Only one option is correct out of all available option.</li>
                            <li>There is 1 mark for correct answer.</li>
                            <li>There is No Negative marking for incorrect answer.</li>
                        </ul>
                    </div>
                </div>
                <div align="left" style="padding-left: 50px">
                    <asp:DataList ID="dtlistShowQP" Width="760px" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem">

                        <ItemTemplate>
                            <div align="left">
                                <table>
                                <tbody >
                                     <tr>
                                    <td valign="top">
                                        <b><%#Eval("SNo")%>.&nbsp</b>
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
                                        <b><%#Eval("SNo")%>.&nbsp</b>
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
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle2">
            <tr>
                <td align="center" style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
