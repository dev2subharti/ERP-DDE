﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishMarkSheetBySC4.aspx.cs" Inherits="DDE.Web.Admin.PublishMarkSheetBySC4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/DDE.css" type="text/css" rel="Stylesheet" />
    <style>
        .rp
        {
            padding-left: 5px;
        }
        .note
        {
            padding: 5px;
        }
    </style>
</head>
<body style="margin: 0px; background-image: url(images/msback2.jpg); width: 100%; height:100%;
    background-repeat: repeat">
    <form id="form1" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" style="font-family: Arial; font-size: 19px">
            <asp:DataList ID="dtlistMS" runat="server" Width="100%" Height="100%">
                <ItemTemplate>
                 <div style="float:right; padding-right:100px; padding-top:40px; page-break-before:always">
                       <asp:Label ID="lblCounter" Visible="false" runat="server" Font-Bold="true" Font-Size="30px" Text=""></asp:Label>      
                     </div>
                    <div align="center" style="background-image: url(images/ddelogo5.png); background-repeat: no-repeat;
                        background-position: center; font-family: Arial; font-size: 19px; padding-top: 320px;
                        height: 1150px">
                        <table width="100%" style="height: 100%">
                            <tr>
                                <td align="center">
                                    <table align="center" width="1260px">
                                        <tr>
                                            <td align="center" style="padding-top: 30px">
                                                <div style="text-align: center; width: 1160px">
                                                    <div>
                                                        <asp:Panel ID="pnlMarkSheet" runat="server">
                                                            <div style="padding-left: 15px; padding-top: 25px; padding-right: 15px">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="center" colspan="7" style="padding-bottom: 10px">
                                                                            <div>
                                                                                <asp:Label ID="lblCourseFullName" runat="server" Font-Bold="true" Text='<%#Eval("Course")%>'
                                                                                    Font-Size="Larger" Font-Underline="True"></asp:Label>
                                                                            </div>
                                                                            <div style="padding-top: 5px">
                                                                                <asp:Label ID="lblExamination" runat="server" Text='<%#Eval("Examination")%>'></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 140px" align="left">
                                                                            <b>Name</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>:</b>
                                                                        </td>
                                                                        <td align="left" style="width: 450px">
                                                                            <asp:Label ID="lblSName" runat="server" Text='<%#Eval("StudentName")%>'></asp:Label>
                                                                            <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblCID" runat="server" Text='<%#Eval("CID")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblSS" runat="server" Text='<%#Eval("SySession")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 180px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 160px" align="left">
                                                                            <b>Roll No.</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>:</b>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblRNo" runat="server" Text='<%#Eval("RollNo")%>'></asp:Label>
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
                                                                            <asp:Label ID="lblFName" runat="server" Text='<%#Eval("FatherName")%>'></asp:Label>
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
                                                                            <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <b>Mother's Name</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>:</b>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblMName" runat="server" Text='<%#Eval("MotherName")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            <b>Mode of Delivery</b>
                                                                        </td>
                                                                        <td>
                                                                            <b>:</b>
                                                                        </td>
                                                                        <td align="left">
                                                                            Distance Mode
                                                                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div align="left" style="padding-top: 18px">
                                                                <asp:DataList ID="dtlistSubMarks" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                    BorderWidth="1px">
                                                                    <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                                    <HeaderTemplate>
                                                                        <table align="left" border="1" cellpadding="0px" cellspacing="0px">
                                                                            <tr>
                                                                                <td align="center" style="width: 186px">
                                                                                    <b>Course Code</b>
                                                                                </td>
                                                                                <td align="center" style="width: 387px">
                                                                                    <b>Subject</b>
                                                                                </td>
                                                                                <td>
                                                                                    <table cellpadding="0px" cellspacing="0px">
                                                                                        <tbody align="center">
                                                                                            <tr>
                                                                                                <td colspan="3" class="borderbottom">
                                                                                                    <b>Maximum Marks</b>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td rowspan="2" class="borderright" style="width: 52px">
                                                                                                    <b>TEE</b>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <table cellpadding="0px" cellspacing="0px">
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
                                                                                </td>
                                                                                <td>
                                                                                    <table cellpadding="0px" cellspacing="0px">
                                                                                        <tr>
                                                                                            <td colspan="3" class="borderbottom">
                                                                                                <b>Marks Obtained</b>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td rowspan="2" class="borderright" style="width: 52px">
                                                                                                <b>TEE</b>
                                                                                            </td>
                                                                                            <td>
                                                                                                <table cellpadding="0px" cellspacing="0px">
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
                                                                                </td>
                                                                                <td align="center" style="width: 53px">
                                                                                    <b>Grade</b>
                                                                                </td>
                                                                                <td align="center" style="width: 56px">
                                                                                    <b>Status</b>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <table align="left" cellpadding="0px" cellspacing="0px">
                                                                            <tbody align="left">
                                                                                <tr>
                                                                                    <td class="borderright" style="width: 182px; text-align: left; padding-left: 5px">
                                                                                        <%#Eval("SubjectCode")%>
                                                                                        <asp:Label ID="lblSubjectID" runat="server" Text='<%#Eval("SubjectID")%>' Visible="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" class="borderright_tal" style="width: 381px">
                                                                                        <%#Eval("SubjectName")%>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 52px">
                                                                                        <%#Eval("MTheory")%>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 53px">
                                                                                        <%#Eval("MIA")%>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 56px">
                                                                                        <%#Eval("MAW")%>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 50px">
                                                                                        <%#Eval("MTotal")%>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 52px">
                                                                                        <asp:Label ID="lblTheory" runat="server" Text='<%#Eval("Theory")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 54px">
                                                                                        <asp:Label ID="lblIA" runat="server" Text=' <%#Eval("IA")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 56px">
                                                                                        <asp:Label ID="lblAW" runat="server" Text=' <%#Eval("AW")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 50px">
                                                                                        <b>
                                                                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                                                        </b>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 55px">
                                                                                        <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td align="center" style="width: 54px">
                                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                <asp:DataList ID="dtlistPracMarks" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                    BorderWidth="1px">
                                                                    <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                                    <ItemTemplate>
                                                                        <table align="left" cellpadding="0px" cellspacing="0px">
                                                                            <tbody align="left">
                                                                                <tr>
                                                                                    <td class="borderright" style="width: 182px; text-align: left; padding-left: 5px">
                                                                                        <%#Eval("PracticalCode")%>
                                                                                        <asp:Label ID="lblPracticalID" runat="server" Text='<%#Eval("PracticalID")%>' Visible="false"></asp:Label>
                                                                                    </td>
                                                                                    <td align="left" class="borderright_tal" style="width: 381px">
                                                                                        <%#Eval("PracticalName")%>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 52px">
                                                                                        -
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 53px">
                                                                                        -
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 56px">
                                                                                        -
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 50px">
                                                                                        <asp:Label ID="lblPMMarks" runat="server" Text='<%#Eval("PracticalMaxMarks")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 52px">
                                                                                        -
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 54px">
                                                                                        -
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 56px">
                                                                                        -
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 50px">
                                                                                        <b>
                                                                                            <asp:Label ID="lblPOMarks" runat="server" Text='<%#Eval("PracticalObtainedMarks")%>'></asp:Label>
                                                                                        </b>
                                                                                    </td>
                                                                                    <td class="borderright" style="width: 55px">
                                                                                        <asp:Label ID="lblPGrade" runat="server" Text='<%#Eval("PracticalGrade")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td align="center" style="width: 62px">
                                                                                        <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("PracticalStatus")%>'></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                <div align="left">
                                                                    <table cellpadding="0px" cellspacing="0px" border="1">
                                                                        <tr>
                                                                            <td align="center" style="width: 577px">
                                                                                <h3 style="margin: 0px">
                                                                                    Grand Total</h3>
                                                                            </td>
                                                                            <td style="width: 166px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="center" style="width: 50px">
                                                                                <asp:Label ID="lblGTMMarks" runat="server" Font-Bold="True"></asp:Label>
                                                                            </td>
                                                                            <td style="width: 167px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="center" style="width: 51px">
                                                                                <asp:Label ID="lblGrandTotal" runat="server" Font-Bold="True"></asp:Label>
                                                                            </td>
                                                                            <td align="center" style="width: 55px">
                                                                                <asp:Label ID="lblGrade" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                            <td align="center" style="width: 63px">
                                                                                <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div align="center" style="padding-top: 20px">
                                                        <asp:Table ID="tblResult" Width="220px" runat="server" CellSpacing="0">
                                                            <asp:TableRow ID="tr1" runat="server">
                                                                <asp:TableCell ID="td1" CssClass="rp" runat="server" Width="80px" BorderColor="Black"
                                                                    BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"><b>Result</b></asp:TableCell>
                                                                <asp:TableCell ID="td2" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                    HorizontalAlign="Center">
                                                                    <asp:Label ID="lblResult" runat="server" Font-Bold="True"></asp:Label></asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="tr2" runat="server">
                                                                <asp:TableCell ID="td3" CssClass="rp" runat="server" Width="70px" BorderColor="Black"
                                                                    BorderStyle="Solid" BorderWidth="1px"><b>Division</b></asp:TableCell>
                                                                <asp:TableCell ID="td4" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                                    HorizontalAlign="Center">
                                                                    <asp:Label ID="lblDivision" runat="server" Font-Bold="True"></asp:Label></asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </div>
                                                    <div align="left" style="padding-left: 15px; padding-right: 15px; padding-top: 5px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="left" style="width: 70%">
                                                                    <b>Abbreviations:</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <p style="margin-top: 0px">
                                                            <b>TEE:</b> Term End Examinations, <b>IA:</b> Internal Assessment, <b>AW:</b> Assignment
                                                            Work, <b>CC:</b> Credits Clear, <b>NC:</b> Not Clear, <b>AB:</b> Absent<br />
                                                            <b>* </b>Back Paper Marks
                                                        </p>
                                                        <p>
                                                            <b>Grade:</b><br />
                                                            <b>A++:</b> 85% and above, <b>A+:</b> 75% and above but below 85%, <b>A:</b> 60%
                                                            and above but below 75%, <b>B:</b> 50% and above but below 60%,<br />
                                                            <b>C:</b> 40% and above but below 50%, <b>D:</b> Below 40%
                                                        </p>
                                                        <p style="margin-bottom:0px">
                                                <b>Pass Marks: 40% in aggregate and in each paper separately for T.E.E. and Continuous
                                                    Internal Assessment. </b>
                                            </p>
                                           <div style="background-image: url(images/PKS1.png);
                                                background-repeat: no-repeat; background-position: right; 
                                                height: 350px">
                                            <table width="100%" cellpadding="0px" cellspacing="0px" >
                                                
                                                <tr>
                                                    <td  valign="middle" style="background-image: url(images/Anil2.png);
                                                background-repeat: no-repeat; background-position: left;
                                                height: 90px; padding-top:50px">
                                                        Prepared by:
                                                    </td>
                                                    <td valign="middle" style="background-image: url(images/Priyanka2.png);
                                                background-repeat: no-repeat; background-position: left;
                                                height: 90px; padding-top:50px">
                                                        Checked by:
                                                    </td>
                                                     <td align="left"  valign="bottom">
                                                        <asp:Panel ID="pnlBC" runat="server">
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="right" valign="middle" style="padding-top:50px">
                                                    
                                                        <b>Controller of Examinations </b>
                                                        <%--  <br />
                                                        <img src="images/RA.png" height="200px" width="150px" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" valign="bottom">
                                                        <p style="margin-top: 20px">
                                                            <asp:Label ID="lblDOI" runat="server" Text=""></asp:Label></p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="padding-top:60px">
                                                        <div align="center" style="padding-top: 0px">
                                                            <asp:Label ID="lblNote" CssClass="note" Font-Bold="false" Font-Size="14px" runat="server"
                                                                Width="1110px" BorderStyle="Solid" BorderColorColor="Black" BorderWidth="2px"
                                                                Visible="true">
                                                            If there is any discrepancy between the mark sheet issued and in the University record then the University record will be considered final.<br />
(No reliance should be placed on the accuracy of the statement of marks should there be any alteration or erasure or tear in it)

                                                            </asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle2">
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