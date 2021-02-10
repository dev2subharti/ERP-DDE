<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerificationSheatList.aspx.cs"
    Inherits="DDE.Web.Admin.VerificationSheatList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link type="text/css" href="../CSS/menu.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/DDE.css" rel="stylesheet" type="text/css" />
    <link href="../css/colorbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/menu.js"></script>
    <script type="text/javascript" src="../JS/jquery.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div style="padding: 20px">
                <div align="center">
                    <asp:DataList ID="dtlistAdmitCards" Width="800px" CssClass="dtlist" runat="server"
                        HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItemAC">
                        <ItemTemplate>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" style="width: 100%;padding-bottom:250px">
                                        <table class="text" width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="background-color: #003f6f; color: White">
                                                    <table width="85%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left" style="padding-left: 15px; padding-bottom: 5px; padding-top: 2px">
                                                                <img src="images/logo1-final.png" width="100px" height="100px" />
                                                            </td>
                                                            <td align="center">
                                                                <div style="padding: 10px">
                                                                    <h1 style="margin: 0px; color: White; font-family: Verdana">
                                                                       ATTENDANCE/VERIFICATION SHEET</h1>
                                                                    <h2 style="margin: 0px; color: White; font-family: Verdana">
                                                                        DIRECTORATE OF DISTANCE EDUCATION<br />
                                                                        SWAMI VIVEKANAND SUBHARTI UNIVERSITY
                                                                    </h2>
                                                                    <asp:Label ID="lblExam" runat="server" Text='<%#Eval("Exam")%>' Font-Bold="true"  Font-Names="Verdana" Font-Size="14px"></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px">
                                                    <table>
                                                        <tr>
                                                            <td align="center" style="padding-top: 10px; width: 100%">
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
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td align="left">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <b>Study Centre Code : </b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("SCCode")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
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
                                                            <td align="left" style="padding-top: 20px">
                                                                <table cellspacing="0px">
                                                                    <tr>
                                                                        <td valign="top">
                                                                           <asp:Image ID="imgStudentPhoto" runat="server" CssClass="img_stph"/>
                                                                        </td>
                                                                        <td valign="top">
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
                                                                                            <asp:Label ID="lblExamCentre" runat="server" Text='<%#Eval("ExamCentre")%>'></asp:Label>
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
                                                            <td style="padding-top: 20px">
                                                                <h2 class="text" style="margin: 0px">
                                                                    DESCRIPTION OF THE PRESENCE OF THE CANDIDATE IN THE EXAMINATION HALL :</h2>
                                                                <table align="left" border="1" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="center" style="width: 50px">
                                                                            <b>S.No.</b>
                                                                        </td>
                                                                        <td align="center" style="width: 100px">
                                                                            <b>Course Code </b>
                                                                        </td>
                                                                        <td align="center" style="width: 50px">
                                                                            <b>SLM Code </b>
                                                                        </td>
                                                                        <td align="center" style="width: 250px">
                                                                            <b>Subject </b>
                                                                        </td>
                                                                        <td align="center" style="width: 100px">
                                                                            <b>Exam Date </b>
                                                                        </td>
                                                                        <td align="center" style="width: 50px">
                                                                            <b>Room No. </b>
                                                                        </td>
                                                                        <td align="center" style="width: 100px">
                                                                            <b>Signature<br />
                                                                                of the Candidate</b>
                                                                        </td>
                                                                        <td align="center" style="width: 100px">
                                                                            <b>Signature<br />
                                                                                of the Invigilator </b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 35px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 35px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 35px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 35px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 35px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center" style="height: 35px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <tr>
                                                                            <td align="center" style="height: 35px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="height: 35px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="height: 35px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="height: 35px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="left">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <br />
                                                                The invigilator is requested to get the signature of the examinee and verify the
                                                                signature & photograph
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <div align="center" style="float: left">
                                                                    <br />
                                                                    <br />
                                                                    ...............................................<br />
                                                                    (Signature of the Candidate)<br />
                                                                    Signature of the Candidate and the photograph<br />
                                                                    affixed above have been attested by the<br />
                                                                    coordinator study centre the Permission Granted
                                                                </div>
                                                                <div>
                                                                    <br />
                                                                    <br />
                                                                    ...............................................................................<br />
                                                                    (Name & Signature of the Centre Superintendent)
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="float: left; padding-top: 45px">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                Dated :&nbsp;
                                                                                <br />
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("Date")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div style="float: right">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <img src="images/COE2.jpg" width="120px" height="50px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <b>Controller of Examinations<br />
                                                                                    SVSU, Meerut
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        
                                                       <%-- <tr>
                                                            <td align="left">
                                                                <p align="center">
                                                                    <b>Undertaking by Student</b></p>
                                                                <p>
                                                                    I hereby undertake that</p>
                                                                <div style="padding-left: 40px">
                                                                    <ol style="list-style-type: decimal">
                                                                        <li>I know the eligibility to appear in the annual examination.</li>
                                                                        <li>I have mentioned and submitted the course fee to the university.</li>
                                                                        <li>I have correctly mentioned my enrollment number.</li>
                                                                        <li>I have clearly filled all the subjects of my course in which I will appear in the
                                                                            examination.</li>
                                                                        <li>I have mentioned my study centre in the form.</li>
                                                                        <li>I have attended all the necessary pratcal classes at my study centre.</li>
                                                                        <li>I have self attest my photograph on the form.</li>
                                                                        <li>I have submitted by assignment work at my study centre.</li>
                                                                        <li>I have clearly mentioned the city in which I wish to give my examination.</li>
                                                                    </ol>
                                                                    <p>
                                                                        And all the information filled by me in the examination form is true with best of
                                                                        my knowledge.</p>
                                                                    <p style="margin: 0px">
                                                                        Name of the Student ___________________________________</p>
                                                                    <p style="margin: 0px">
                                                                        Phone Number _______________________________________
                                                                    </p>
                                                                    <p style="margin: 0px">
                                                                        E-mail Id ____________________________________________</p>
                                                                    <p style="margin: 0px">
                                                                        Enrollment Number ___________________________________</p>
                                                                    <p style="margin: 0px">
                                                                        Study centre Code ____________________________________</p>
                                                                    <p style="margin: 0px">
                                                                        Study centre Name ___________________________________</p>
                                                                    <br />
                                                                    <br />
                                                                </div>
                                                            </td>
                                                        </tr>--%>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div align="center">
             <asp:Label ID="lblTotalCards" runat="server" Font-Bold="True" ForeColor="#003f6f" Font-Size="14px"></asp:Label>
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
