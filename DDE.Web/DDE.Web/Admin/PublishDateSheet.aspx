<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishDateSheet.aspx.cs" Inherits="DDE.Web.Admin.PublishDateSheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../CSS/DateSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center">
                <h1 style="margin: 0px">
                    Swami Vivekanand Subharti University</h1>
                (A University under section 2(f) of the UGC Act, 1956 Established by U.P. Govt.
                under Act No.29 of 2008)<br />
                <h3 style="margin: 0px">
                    Meerut - 250005 (U.P.)</h3>
                <h2 style="margin: 0px">
                    Directorate of Distance Education</h2>
                <b>(Approved by DEB of UGC)</b>
                <br />
                 <br />
                
                <u>
                    <h2 style="margin: 10px; color:#0977C0">
                       <asp:Label ID="lblExam" runat="server" Text=""></asp:Label>
                    </h2>
                </u>
                <u>
                    <h2 style="margin: 10px; color:#0977C0">
                       <asp:Label ID="lblMOE" runat="server" Text=""></asp:Label>
                    </h2>
                </u>
                <u>
                    <b>
                    <asp:Label ID="lblApplicableFor" runat="server" ForeColor="#0977C0" Font-Size="Larger" Text=""></asp:Label></b> </u>
            </div>
            <div align="center" style="padding-top:20px">
                <asp:DataList ID="dtlistShowTheoryDS" runat="server" Visible="false" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS" >
                    <HeaderTemplate>
                        <table  align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 92px">
                                    <b>Date</b>
                                </td>
                                <td align="center" style="width: 90px">
                                    <b>Day</b>
                                </td>
                                <td align="center" style="width: 160px">
                                    <b>Time</b>
                                </td>
                                <td align="center" style="width: 220px">
                                    <b>Subject Code</b>
                                </td>
                                <td align="center" style="width: 120px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="center" style="width: 300px">
                                    <b>Title of Paper</b>
                                </td>
                               
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 80px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                 <td align="center" class="border_rb" style="width: 80px">
                                    <asp:Label ID="lblDay" runat="server" Text='<%#Eval("Day")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowSubjects" CellPadding="0" CellSpacing="0" runat="server">
                                        <ItemTemplate>
                                            <table align="left" cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="left" class="border_rb" style="width: 160px">
                                                        <%#Eval("Time")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 200px">
                                                        <%#Eval("SubjectCode")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 100px">
                                                        <%#Eval("PaperCode")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("SubjectName")%>
                                                   </td>
                                                   
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dtlistShowPracDS" runat="server" Visible="false" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS" >
                    <HeaderTemplate>
                        <table  align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 92px">
                                    <b>Date</b>
                                </td>
                                <td align="center" style="width: 90px">
                                    <b>Day</b>
                                </td>
                                <td align="center" style="width: 160px">
                                    <b>Time</b>
                                </td>
                                <td align="center" style="width: 220px">
                                    <b>Practical Code</b>
                                </td>
                               
                                <td align="center" style="width: 300px">
                                    <b>Title of Practical</b>
                                </td>
                               
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 80px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                 <td align="center" class="border_rb" style="width: 80px">
                                    <asp:Label ID="lblDay" runat="server" Text='<%#Eval("Day")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowPracticals" CellPadding="0" CellSpacing="0" runat="server">
                                        <ItemTemplate>
                                            <table align="left" cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="left" class="border_rb" style="width: 160px">
                                                        <%#Eval("Time")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 200px">
                                                        <%#Eval("PracticalCode")%>
                                                    </td>
                                                   
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("PracticalName")%>
                                                    </td>
                                                   
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table class="tableStyle2" align="center">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
