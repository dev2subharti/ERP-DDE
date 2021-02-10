<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishRefundLetter.aspx.cs"
    Inherits="DDE.Web.Admin.PublishRefundLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/DDE.css" type="text/css" rel="Stylesheet" />
    <link href="../CSS/Style.css" type="text/css" rel="Stylesheet" />
    <title>Reimbursement Letter-DDE</title>
    <script type="text/javascript">
        function pr() {

            window.print();
        }
    </script>
</head>
<body style="background: url(../Images/bg-body-inner2.jpg) repeat-x">
    <form id="form1" runat="server">
    
    <div id="header">
        <%-- <asp:Button ID="Button1" runat="server" Text="ERID" onclick="Button1_Click" />--%>
        <%-- <asp:Button ID="btn" runat="server" Text="Change" onclick="btn_Click" />--%>
        <table width="100%" cellpadding="0px" cellspacing="0px">
            <tr>
                <td style="width: 33%">
                    <a href="#">
                        <img src="../images/logo.png" alt="" width="285" height="95" id="logo" /></a>
                </td>
                <td align="center" style="width: 33%">
                    <div align="center" id="erps">
                        <div>
                            D.L.M.
                        </div>
                        <div id="slogen">
                            Managing Subharti....
                        </div>
                    </div>
                </td>
                <td>
                    <div id="right_header">
                        <div id="top_nav">
                            <ul>
                                <li><a href="#" class="active"><span>About ERP</span></a></li>
                                <li><a href="#"><span>Contact Us</span></a></li>
                            </ul>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div align="center" style="padding-top: 200px; padding-bottom: 50px">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center">
                <div align="center" class="heading" style="padding-bottom: 20px">
                    Reimbursement to Admission Facilitator
                </div>
                <div align="center" class="text">
                    <table class="tableStyle2" cellspacing="10px">
                        <tbody align="left">
                            <tr>
                                <td>
                                    Letter No.
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblLNo" runat="server" Visible="false" Text=""></asp:Label>
                                </td>
                                <td>
                                    Date
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    A.F. Code
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblSCCode" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    A.F. Name
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblSCName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div align="center" style="padding-top:10px">
        <asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" OnClick="btnPrint_Click" />
    </div>
                <div class="text" align="center" style="padding-top: 30px">
                    <asp:DataList ID="dtlistDirectSC" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem">
                        <HeaderTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 50px">
                                        <b>S.No.</b>
                                    </td>
                                    <td align="left" style="width: 120px">
                                        <b>Enrollment No.</b>
                                    </td>
                                    <td align="left" style="width: 120px">
                                        <b>Student Name</b>
                                    </td>
                                    <td align="left" style="width: 120px">
                                        <b>Father Name</b>
                                    </td>
                                    <td align="left" style="width: 200px">
                                        <b>Course</b>
                                    </td>
                                    <td align="left" style="width: 80px">
                                        <b>CYear</b>
                                    </td>
                                    <td align="left" style="width: 80px">
                                        <b>FPYear</b>
                                    </td>
                                    <td align="right" style="width: 80px">
                                        <b>Req. Fee</b>
                                    </td>
                                    <td align="right" style="width: 100px">
                                        <b>Paid Fee</b>
                                    </td>
                                    <td align="right" style="width: 100px">
                                        <b>Fee(%)</b>
                                    </td>
                                    <td align="right" style="width: 100px">
                                        <b>Refund</b>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 40px">
                                        <%#Eval("SNo")%>
                                    </td>
                                    <td align="left" style="width: 120px">
                                        <%#Eval("EnrollmentNo")%>
                                        <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 120px">
                                        <%#Eval("SName")%>
                                    </td>
                                    <td align="left" style="width: 120px">
                                        <%#Eval("FName")%>
                                    </td>
                                    <td align="left" style="width: 200px">
                                        <%#Eval("Course")%>
                                    </td>
                                    <td align="left" style="width: 80px">
                                        <%#Eval("CYear")%>
                                    </td>
                                    <td align="left" style="width: 60px">
                                        <%#Eval("FPYear")%>
                                        <asp:Label ID="lblFPYear" runat="server" Text='<%#Eval("FPYear")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 80px">
                                       <asp:Label ID="lblRFee" runat="server" Text='<%#Eval("RFee")%>'></asp:Label> 
                                    </td>
                                    <td align="right" style="width: 100px">
                                        <asp:Label ID="lblPFee" runat="server" Text='<%#Eval("PFee")%>' ></asp:Label> 
                                    </td>
                                    <td align="right" style="width: 100px">
                                        <asp:Label ID="lblFeePer" runat="server" Text='<%#Eval("FeePer")%>'></asp:Label>
                                    </td>
                                    <td align="right" style="width: 100px">
                                        <asp:Label ID="lblRefund" runat="server" Text='<%#Eval("Refund")%>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div align="right" class="tableStyle2" style="width: 1144px; padding-top: 5px; padding-bottom: 5px;
                    padding-right: 30px">
                    <asp:Label ID="lblTotalRefund" runat="server" Text=""></asp:Label>
                </div>
                <div align="right" class="tableStyle2" style="width: 1144px; padding-top: 5px; padding-bottom: 5px;
                    padding-right: 30px">
                    <asp:Label ID="lblDRAmount" runat="server" Visible="true" Text=""></asp:Label>
                    
                </div>
                <div align="right" class="tableStyle2" style="width: 1144px; padding-top: 5px; padding-bottom: 5px;
                    padding-right: 30px">
                    <asp:Label ID="lblFinalAmount" BorderColor="#003f6f" BorderStyle="Solid" BorderWidth="2px" runat="server" Visible="true" Text=""></asp:Label>
                </div>
                <div style="padding-top: 80px; width: 100%">
                    <table class="text" width="100%">
                        <tr>
                            <td align="center">
                                <b>Prepared By</b>
                            </td>
                            <td align="center">
                                <b>Accounts Officer</b>
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
    <div id="outer_footer">
        <div id="footer">
            <div class="bottom_footer">
                <p>
                    &copy; 2011 SUBHARTI UNIVERSITY All Rights Reserved</p>
                <a href="#" id="topScroll">Back to Top</a>
            </div>
            <div align="center" style="font-family: Arial; font-size: 12px; color: White">
                Designed & Developed By : IT Cell, DDE.
            </div>
        </div>
    </div>
    </form>
</body>
</html>
