<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChalanForm.aspx.cs" Inherits="DDE.Web.Admin.ChalanForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/DDE.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: 10px">
        <asp:DataList ID="dtlistChalanForm" CellPadding="0" CellSpacing="0" CssClass="dtlist" runat="server" RepeatColumns="3"
            RepeatDirection="Horizontal" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
            <ItemTemplate >
                <table border="1" style="background-color: White; font-size:10px" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td colspan="2" align="left">
                            Challan No:
                            <%#Eval("ChallanNo")%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            DATE:<%#Eval("Date")%>
                        </td>
                        <td align="center">
                            <%#Eval("Heading")%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <img src="../Images/logo.jpg" width="100px" />
                        </td>
                        <td valign="top">
                            <table width="100%" style="height: 90px" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td align="center" class="td_bottom">
                                        SWAMI VIVEKANAND SUBHARTI UNIVERSITY
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="td_bottom">
                                        DISTANCE EDUCATION
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        ICICI
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Account to be credited
                        </td>
                        <td align="center">
                            SVSU
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Study Centre Name
                        </td>
                        <td align="left">
                            <%#Eval("SCName")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Study Centre Code
                        </td>
                        <td>
                            <table cellpadding="0px" cellspacing="0px" style="height:100%">
                                <tr>
                                    <td style="width: 50px; border-right: solid 1px black">
                                        <%#Eval("SCCode")%>
                                    </td>
                                    <td align="center" style="width: 80px; border-right: solid 1px black">
                                        Place
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cash Detail
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Denomination
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        Amount
                                    </td>
                                    <td align="center">
                                        FEE DETAIL
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            1000 *
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="center" style="width:160px" class="td_right">
                                                    FEE HEAD
                                                </td>
                                                <td align="center">
                                                    AMOUNT
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            500 *
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH1")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount1")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            100 *
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH2")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount2")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            50 *
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH3")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount3")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            10 *
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH4")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount4")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            5 *
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH5")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount5")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Coins
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH6")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount6")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH7")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount7")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cheque / DD No.
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="width:160px" class="td_right">
                                                    <%#Eval("FH8")%>
                                                </td>
                                                <td align="right">
                                                    <%#Eval("Amount8")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td style="width: 50px" class="td_right">
                                        &nbsp;
                                    </td>
                                    <td >
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="center" style="width:160px" class="td_right">
                                                   <b>Total Amount</b> 
                                                </td>
                                                <td align="right">
                                                  <b><%#Eval("Total")%></b>  
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="1" width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td align="center" valign="bottom" style="height: 50px; width:47%">
                                        Signature / Stamp ICICI
                                    </td>
                                    <td align="center" valign="bottom">
                                        Signature of Depositor
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="bottom" style="height: 50px">
                                        Journal / Transaction ID No.
                                    </td>
                                    <td align="center" valign="bottom">
                                        .............................
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
