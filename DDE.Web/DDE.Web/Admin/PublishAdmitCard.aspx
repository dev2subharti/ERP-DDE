<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishAdmitCard.aspx.cs"
    Inherits="DDE.Web.Admin.PublishAdmitCard" %>

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
                    <table width="800px" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="width: 100%; padding-bottom: 375px">
                                <table class="text" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="background-color: #003f6f; color: White">
                                            <table width="85%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left" style="padding-left: 15px; padding-bottom: 5px; padding-top: 2px">
                                                        <img src="images/logo1-final.png" width="100px" height="100px" />
                                                    </td>
                                                    <td align="center">
                                                        <div>
                                                            <h1 style="margin: 0px; color: White; font-family: Verdana">
                                                                ADMIT-CARD</h1>
                                                            <h2 style="margin: 0px; color: White; font-family: Verdana">
                                                                DIRECTORATE OF DISTANCE EDUCATION<br />
                                                                SWAMI VIVEKANAND SUBHARTI UNIVERSITY
                                                            </h2>
                                                            <asp:Label ID="lblExam" runat="server"  Font-Bold="true"
                                                                Font-Names="Verdana" Font-Size="14px"></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px">
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
                                                                                    <asp:Label ID="lblENo" runat="server" ></asp:Label>
                                                                                    <asp:Label ID="lblSRID" runat="server"  Visible="false"></asp:Label>
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
                                                                                    <asp:Label ID="lblSCCode" runat="server" ></asp:Label>
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
                                                                                    <asp:Label ID="lblRollNo" runat="server" ></asp:Label>
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
                                                                <td valign="top">
                                                                    <asp:Image ID="imgStudentPhoto" runat="server" Height="120px" Width="100px" BorderStyle="Solid" BorderColor="#003f6f" BorderWidth="2px" />
                                                                    
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
                                                                                    <asp:Label ID="lblSName" runat="server" ></asp:Label>
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
                                                                                    <asp:Label ID="lblFName" runat="server" ></asp:Label>
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
                                                                                    <asp:Label ID="lblBatch" runat="server" ></asp:Label>
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
                                                                                    <asp:Label ID="lblCourse" runat="server" ></asp:Label>
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
                                                                                    <asp:Label ID="lblYear" runat="server" ></asp:Label>
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
                                                                                    <asp:Label ID="lblECentre" runat="server" ></asp:Label>
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
                                                        <h2 class="text" style="padding-top: 10px; padding-bottom: 0px">
                                                            Subjects of Examination:</h2>
                                                        <table align="left" border="1" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <b>S.No.</b>
                                                                </td>
                                                                <td align="center" style="width: 100px">
                                                                    <b>Course Course</b>
                                                                </td>
                                                                <td align="center" style="width: 500px">
                                                                    <b>Title of Paper</b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo1" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode1" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName1" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo2" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode2" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName2" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo3" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode3" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName3" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo4" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode4" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName4" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo5" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode5" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName5" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo6" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode6" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName6" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo7" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode7" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName7" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo8" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode8" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName8" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" style="width: 50px">
                                                                    <asp:Label ID="lblSNo9" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 100px">
                                                                    <asp:Label ID="lblSubCode9" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 500px">
                                                                    <asp:Label ID="lblSubName9" runat="server" Text="-"></asp:Label>
                                                                </td>
                                                            </tr>
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
                                                            Signature of the Administrative<br />
                                                            Controller/Coordinator
                                                        </div>
                                                        <div style="float: right">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <img src="images/Checking Officer.jpg" width="120px" height="50px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        (Signature of Checking Officer)
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-top: 20px">
                                                        <div style="float: left; padding-top: 45px">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        Dated :&nbsp;
                                                                        <br />
                                                                    </td>
                                                                    <td>
                                                                         <asp:Label ID="lbldate" runat="server" Text="-"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div style="float: right">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td align="center">
                                                                        <img src="images/Controller Of Examiner.jpg" width="120px" height="50px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <b>Controller of Examinations<br />
                                                                            SVSU, Meerut<br />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
        </asp:Panel>
    </div>
    </form>
</body>
</html>
