<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmitCardList2.aspx.cs" Inherits="DDE.Web.Admin.AdmitCardList2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" href="../../CSS/menu.css" rel="stylesheet" />
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/DDE.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:none">
    <form id="form1" runat="server">
    <div align="center" style="background-color:White">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div style="padding: 20px">
                <div align="center">
                    <asp:DataList ID="dtlistAdmitCards" Width="800px" CssClass="dtlist" runat="server"
                        HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItemAC">
                        <ItemTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" style="width: 100%; padding-bottom: 300px">
                                        <table class="text" width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="background-color: #003f6f; color: White">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>                                                      
                                                            <td align="center">
                                                                <div style="height:110px">
                                                                    <h1 style="margin: 0px; color: White; font-family: Verdana; padding-top:10px">
                                                                        ADMIT-CARD</h1>
                                                                    <h2 style="margin: 0px; color: White; font-family: Verdana">
                                                                        DIRECTORATE OF DISTANCE EDUCATION<br />
                                                                        SWAMI VIVEKANAND SUBHARTI UNIVERSITY
                                                                    </h2>
                                                                    <asp:Label ID="lblExam" runat="server" Text='<%#Eval("Exam")%>' Font-Bold="true"
                                                                        Font-Names="Verdana" Font-Size="14px"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px; color:#003f6f" >
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="padding-top: 10px">
                                                                <table align="left" width="100%" cellpadding="0" cellspacing="0">
                                                                    <tbody align="left">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>Enrollment No. : </b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                                                                            <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                           <%-- <td align="left">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>A.F. Code : </b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblSCCode" runat="server" Text='<%#Eval("SCCode")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>--%>
                                                                            <td align="right">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>Roll No. : </b>
                                                                                            <br />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRollNo" runat="server" Text='<%#Eval("RollNo")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-top: 10px">
                                                                <table cellspacing="0px">
                                                                    <tr>
                                                                        <td valign="top" style="padding-top:5px">
                                                                           <asp:Image ID="imgStudentPhoto" runat="server" CssClass="img_stph"/>
                                                                        </td>
                                                                        <td valign="top" style="padding-left:20px">
                                                                            <table align="left" cellspacing="5px">
                                                                                <tbody align="left">
                                                                                    <tr>
                                                                                        <td style="width: 170px">
                                                                                            <b>Full Name of Candidate</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblSName" runat="server" Text='<%#Eval("SName")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>Father’s/Husband Name</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblFName" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>Batch</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblBatch" runat="server" Text='<%#Eval("Batch")%>'></asp:Label>
                                                                                            <asp:Label ID="lblSySession" runat="server" Text='<%#Eval("SySession")%>' Visible="false"></asp:Label>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>Course</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>Year</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <b>Examination Centre</b>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("ExamCentre")%>'></asp:Label>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <b>Examination City</b>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <b>:</b>
                                                                                        </td>
                                                                                        <td valign="top">
                                                                                            <asp:Label ID="lblECity" runat="server" Text='<%#Eval("ExamCity")%>'></asp:Label>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="actext">
                                                                <h2 class="text" style="padding-top: 10px; color:#003f6f; padding-bottom: 0px">
                                                                    Subjects of Examination:</h2>
                                                                <table align="left" border="1" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="center" style="width: 20px">
                                                                            <b>S.No.</b>
                                                                        </td>
                                                                        <td align="center" style="width: 70px">
                                                                            <b>Paper Code</b>
                                                                        </td>
                                                                        <td align="center" style="width: 280px">
                                                                            <b>Title of Paper</b>
                                                                        </td>
                                                                         <td align="center" style="width: 80px">
                                                                            <b>Date</b>
                                                                        </td>
                                                                         <td align="center" style="width: 130px">
                                                                            <b>Time</b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo1" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode1" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName1" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate1" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime1" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo2" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode2" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName2" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate2" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime2" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo3" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode3" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName3" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate3" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime3" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo4" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode4" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName4" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate4" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime4" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo5" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode5" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName5" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate5" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime5" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo6" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode6" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName6" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate6" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime6" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblSNo7" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode7" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName7" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate7" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime7" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo8" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode8" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName8" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate8" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime8" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo9" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode9" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName9" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate9" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime9" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo10" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode10" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName10" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate10" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime10" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                   <%-- <tr>
                                                                        <td align="center" >
                                                                            <asp:Label ID="lblSNo11" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubCode11" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblSubName11" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                         <td align="left" >
                                                                            <asp:Label ID="lblDate11" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                        <td align="left" >
                                                                            <asp:Label ID="lblTime11" runat="server" Text="-"></asp:Label>
                                                                        </td>
                                                                    </tr>--%>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 20px">
                                                                <div style="float: left">
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    ...............................................<br />
                                                                    (Signature of the Candidate)
                                                                </div>
                                                                <div align="center" style="float: left; padding-left: 50px">
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    ..............................................................<br />                                                                  
                                                                    Coordinator
                                                                </div>
                                                                <div style="float: right">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="center" style="padding-top:35px">
                                                                               &nbsp;
                                                                            </td>

                                                                        </tr> 
                                                                        <tr>
                                                                            <td align="center" style="padding-top:0px" >  
                                                                                <b>Controller of Examinations<br />
                                                                                    SVSU, Meerut<br />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-top: 20px">
                                                                <%--<div style="float: left; padding-top: 10px">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                Dated :&nbsp;
                                                                                <br />
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("TodayDate")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td style="padding-top:45px">
                                                        <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                        <td valign="top" style="width:85px">
                                                        <b>Disclaimer : </b>
                                                        </td>
                                                        <td>
                                                         In case of any change in the Examination Schedule, the Examination Schedule placed on DDE website will be treated as final.
                                                        </td>
                                                        </tr>
                                                        </table>
                                                       
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                         <div style="page-break-after: always;">
                                                                </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div align="center">
                <asp:Label ID="lblTotalCards" runat="server" Font-Bold="True" ForeColor="#003f6f"
                    Font-Size="14px"></asp:Label>
            </div>
            <div style="padding-top: 10px">
                <asp:Panel ID="pnlPaging" runat="server">
                    <table align="center" width="1000px">
                        <tr>
                            <td>
                                <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                            </td>
                            <td align="left" style="padding-left: 200px">
                                <asp:LinkButton ID="lnkbtnPrevious" runat="server" Text="< Previous" ForeColor="Blue"
                                    Font-Bold="true" Visible="false" OnClick="lnkbtnPrevious_Click"></asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnPageing" runat="server" ForeColor="Blue" Text='<%#Eval("PageNo") %>'
                                            CommandArgument='<%#Eval("PageNo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkbtnNext" runat="server" Text="Next >" ForeColor="Blue" Font-Bold="true"
                                    Visible="false" OnClick="lnkbtnNext_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
    </div>
    </form>
</body>
</html>
