<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishMarksheet.aspx.cs"
    Inherits="DDE.Web.Admin.PublishMarksheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/DDE.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 140px;
            height: 22px;
        }
        .style2
        {
            height: 22px;
        }
        .style3
        {
            width: 400px;
            height: 22px;
        }
        .style4
        {
            width: 130px;
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="font-family: Arial; font-size: 15px">
            <table width="100%">
                <tr>
                    <td align="center">
                        <table align="center" width="1040px">
                            <tr>
                                <td>
                                    <div align="center" style="padding-top: 320px">
                                        <%--<h1z style="margin: 0px">
                                        Swami Vivekanand Subharti University</h1>
                                    (A University under section 2(f) of the UGC Act, 1956 Established by U.P. Govt.
                                    under Act No.29 of 2008)<br />
                                    <h3 style="margin: 0px">
                                        Meerut - 250005 (U.P.)</h3>
                                    <h2 style="margin: 0px">
                                        Directorate of Distance Education</h2>
                                    <b>(Approved by UGC-AICTE-DEC)</b><h3 style="margin: 0px">
                                        Statement of Marks</h3>--%>
                                        <u>
                                            <h3 style="margin: 10px">
                                                <asp:Label ID="lblCourseFullName" runat="server" Font-Size="Larger" Font-Underline="True"></asp:Label></h3>
                                        </u>
                                        <asp:Panel ID="pnlMarkSheet" runat="server">
                                            <div style="padding-left: 15px; padding-top: 25px; padding-right: 15px">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" class="style1">
                                                            <b>Name</b>
                                                        </td>
                                                        <td class="style2">
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left" class="style3">
                                                            <asp:Label ID="lblSName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td class="style1">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left" class="style4">
                                                            <b>Roll No.</b>
                                                        </td>
                                                        <td class="style2">
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left" class="style2">
                                                            <asp:Label ID="lblRNo" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>Father's Name</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblFName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <b>Enrollment No.</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblENo" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>SC Code</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblSCCode" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <b>Examination</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblSession" runat="server"  Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div align="center" style="padding: 18px">
                                                <asp:Table Width="100%" ID="Table1" runat="server" BorderWidth="1px" BorderStyle="Solid"
                                                    BorderColor="Black" GridLines="Both">
                                                    <asp:TableRow ID="TableRow0" runat="server">
                                                        <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Center" Width="8%">
                                                            <b>Course Code</b>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell2" runat="server" HorizontalAlign="Center" Width="40%">
                                                            <b>Subject</b>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell3" runat="server" Width="200px">
                                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                            <tbody align="center">
                                                                <tr>
                                                                    <td colspan="3" class="borderbottom">
                                                                        <b>Maximum Marks</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td rowspan="2" class="borderright" style="width: 50px">
                                                                        <b>TEE</b>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <table width="100px" cellpadding="0px" cellspacing="0px">
                                                                            <tr>
                                                                                <td colspan="2" class="borderbottom">
                                                                                    <b>Continuous
                                                                                        <br />
                                                                                        Internal
                                                                                        <br />
                                                                                        Assessment</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 50%" class="borderright">
                                                                                    <b>I.A.</b>
                                                                                </td>
                                                                                <td align="center" style="width: 50%">
                                                                                    <b>A.W.</b>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 50px" class="borderleft">
                                                                        <b>Total</b>
                                                                    </td>
                                                                </tr>
                                                                </tbody>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell4" runat="server" Width="200px">
                                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td colspan="3" class="borderbottom">
                                                                        <b>Marks Obtained</b>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td rowspan="2" class="borderright" style="width: 50px">
                                                                        <b>TEE</b>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <table width="100px" cellpadding="0px" cellspacing="0px">
                                                                            <tr>
                                                                                <td colspan="2" class="borderbottom">
                                                                                    <b>Continuous
                                                                                        <br />
                                                                                        Internal
                                                                                        <br />
                                                                                        Assessment</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 50%" class="borderright">
                                                                                    <b>I.A.</b>
                                                                                </td>
                                                                                <td align="center" style="width: 50%">
                                                                                    <b>A.W.</b>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td style="width: 50px" class="borderleft">
                                                                        <b>Total</b>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell5" runat="server" HorizontalAlign="Center">
                                                            <b>Grade</b>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell36" runat="server" HorizontalAlign="Center">
                                                            <b>Status</b>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow1" runat="server">
                                                        <asp:TableCell ID="TableCell6" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC1" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell7" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub1" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell8" runat="server" Height="100%">
                                                            <table width="100%" style="height:100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell9" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell10" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade1" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell57" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus1" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow2" runat="server">
                                                        <asp:TableCell ID="TableCell11" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC2" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell12" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub2" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell13" runat="server" Height="100%">
                                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell14" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell15" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade2" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell58" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus2" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow3" runat="server">
                                                        <asp:TableCell ID="TableCell16" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC3" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell17" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub3" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell18" runat="server" Height="100%">
                                                            <table width="100%" style="height:100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell19" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory3" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA3" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW3" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal3" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell20" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade3" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell59" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus3" HorizontalAlign="Center" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow4" runat="server">
                                                        <asp:TableCell ID="TableCell21" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC4" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell22" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub4" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell23" runat="server" Height="100%">
                                                            <table width="100%" style="height:100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell24" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory4" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA4" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW4" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal4" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell25" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade4" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell60" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus4" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow5" runat="server">
                                                        <asp:TableCell ID="TableCell26" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC5" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell27" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub5" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell28" runat="server" Height="100%">
                                                            <table width="100%" style="height:100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell29" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory5" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA5" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW5" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal5" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell30" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade5" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell61" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus5" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow6" runat="server">
                                                        <asp:TableCell ID="TableCell31" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC6" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell32" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub6" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell33" runat="server" Height="100%">
                                                            <table width="100%" style="height:100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell34" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory6" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA6" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW6" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal6" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell35" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade6" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell62" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus6" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow7" runat="server">
                                                        <asp:TableCell ID="TableCell135" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC7" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell136" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub7" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell37" runat="server" Height="100%">
                                                       <table width="100%" style="height:100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell38" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory7" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA7" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW7" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal7" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell39" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade7" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell63" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus7" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow8" runat="server">
                                                        <asp:TableCell ID="TableCell40" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC8" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell41" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblSub8" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell42" runat="server" Height="100%">
                                                        <table width="100%" style="height:100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        60
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        20
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        20
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        100
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell43" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        <asp:Label ID="lblTheory8" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblIA8" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        <asp:Label ID="lblAW8" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblTotal8" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell44" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade8" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell64" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus8" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow9" runat="server">
                                                        <asp:TableCell ID="TableCell45" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC9" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell46" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblPrac1" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell47" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        -
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblMaxPracMarks1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell48" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        -
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblPracMaksObtained1" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell49" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade9" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell65" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus9" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow10" runat="server">
                                                        <asp:TableCell ID="TableCell50" runat="server" HorizontalAlign="Center">
                                                            <asp:Label ID="lblCC10" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell51" runat="server" CssClass="subjects">
                                                            <asp:Label ID="lblPrac2" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell52" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                                        -
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 49px">
                                                                        <asp:Label ID="lblMaxPracMarks2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell53" runat="server" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                                        -
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblPracMaksObtained2" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell54" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblGrade10" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell66" HorizontalAlign="Center" runat="server">
                                                            <asp:Label ID="lblStatus10" runat="server" Text=""></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow11" runat="server">
                                                        <asp:TableCell ID="TableCell55" runat="server" ColumnSpan="3">
                                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td align="center">
                                                                        <h3 style="margin: 0px">
                                                                            Grand Total</h3>
                                                                    </td>
                                                                    <td align="center" style="height: 30px; width: 48px" class="borderleft">
                                                                        <asp:Label ID="lblGTMMarks" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell56" runat="server" ColumnSpan="3" Height="100%">
                                                            <table width="100%" style="height: 100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td style="width: 135px" class="borderright">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 44px; height: 30px" align="right" class="borderright">
                                                                        <asp:Label ID="lblGrandTotal" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 47px" align="center" class="borderright">
                                                                        <asp:Label ID="lblGrade11" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 50px" align="center">
                                                                        <asp:Label ID="lblStatus11" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div align="center">
                                        <table width="200px" border="1" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="padding: 5px; width: 70px">
                                                    <b>Result</b>
                                                </td>
                                                <td align="center" style="padding: 5px">
                                                    <asp:Label ID="lblResult" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="padding: 5px">
                                                    <b>Division</b>
                                                </td>
                                                <td align="center" style="padding: 5px">
                                                    <asp:Label ID="lblDivision" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div align="left" style="padding-left: 15px; padding-right: 15px; padding-top: 25px">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" style="width: 70%">
                                                    <b>Abbreviations:</b>
                                                </td>
                                            </tr>
                                        </table>
                                        <p>
                                            <b>TEE:</b> Term End Examinations, <b>IA:</b> Internal Assessment, <b>AW:</b> Assignment
                                            Work, <b>CC:</b> Credits Clear, <b>NC:</b> Not Clear, <b>AB:</b> Absent
                                        </p>
                                        <p>
                                            <b>Grade:</b><br />
                                            <b>A++:</b> 85% and above, <b>A+:</b> 75% and above but below 85%, <b>A:</b> 60%
                                            and above but below 75%, <b>B:</b> 50% and above but below 60%,<br />
                                            <b>C:</b> 40% and above but below 50%, <b>D:</b> Below 40%
                                        </p>
                                        <p>
                                            <b>Pass Marks: 40% in aggregate and in each paper separately for T.E.E. and Continuous
                                                Internal Assessment. </b>
                                        </p>
                                        <br />
                                        <br />
                                        <p align="right">
                                            <b>Controller of Examinations </b>
                                        </p>
                                        <table width="600px" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td>
                                                    Prepared by:
                                                </td>
                                                <td>
                                                    Checked by:
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <p>
                                            Date of Issue ……………...</p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
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
    </form>
</body>
</html>
