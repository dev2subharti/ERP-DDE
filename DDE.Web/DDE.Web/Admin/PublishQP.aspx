<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishQP.aspx.cs" Inherits="DDE.Web.Admin.PublishQP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                            <asp:Label ID="lblCourseCode" runat="server" Text=""></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblExamination" runat="server" Text=""></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblSubjectName" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div style="float: left">
                        Time : 3 Hours ]
                    </div>
                    <div style="float: right">
                        [ Max. Marks : 100
                    </div>
                    <br />
                    <br />
                    <div>
                        Note. Attempt any Five questions. All questions carry equal marks.
                    </div>
                </div>
                <div align="left" style="padding-left: 50px">
                    <asp:DataList ID="dtlistShowQP" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem">
                        <ItemTemplate>
                            <table align="left" cellspacing="0px">
                                <tr>
                                    <td valign="top" align="left" style="width: 50px; padding-top: 20px">
                                        Q.<asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                       
                                    </td>
                                    <td valign="top" align="left" style="width: 150px; padding-top: 20px">
                                     <asp:Label ID="lblQID" runat="server" Text='<%#Eval("QID")%>' Visible="false"></asp:Label>
                                        <asp:Image ID="imgQuestion" runat="server" />
                                    </td>
                                </tr>
                            </table>
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
