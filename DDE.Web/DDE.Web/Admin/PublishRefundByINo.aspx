<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishRefundByINo.aspx.cs"
    Inherits="DDE.Web.Admin.PublishRefundByINo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/DDE.css" type="text/css" rel="Stylesheet" />
    <title>Reimbursement Letter</title>
    <script type="text/javascript">
        function pr() {

            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
    </div>
    <div style="float:left">
           <asp:HyperLink ID="hl" runat="server" NavigateUrl="RefundByINo.aspx">Back</asp:HyperLink>
    </div>
   <div align="center" style="width: 100%; margin:2px">
                <div align="center" style="width: 330px">
                    <div style="font-size: 13px; font-family: Berlin Sans FB">
                       Distance Learning Manager (D.L.M.)
                    </div>
                    <div style="width: 320px; font-family: Cambria; font-size: 10px; margin: 0px" align="right">
                        <i>...An ERP System Managing DDE, SVSU</i>
                    </div>
                </div>
            </div>
    <div align="center" style="padding-top: 10px">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Reimbursement To Admission Facilitator For Their Expenses
            </div>
            <div class="text" style="padding: 20px">
                <div style="float: left">
                    <asp:Label ID="lblLNo" runat="server" Font-Bold="true" Text=""></asp:Label>
                </div>
                <div style="float: right">
                    <asp:Label ID="lblDate" runat="server" Font-Bold="true" Text=""></asp:Label>
                </div>
            </div>
            <div style="padding-top: 10px">
                <asp:Panel ID="pnlDCDetail" runat="server">
                    <div class="data" style="padding-top: 0px" align="center">
                        <table class="tableStyle2" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <table width="388px" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="10px">
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT No.</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbINo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                            <asp:Label ID="lblIID" runat="server" Visible="false" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT TYPE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbIType" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT DATE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbIDate" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <b>ISSUING BANK NAME</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbIBN" runat="server" TextMode="MultiLine" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>TOTAL AMOUNT</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbTotalAmount" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
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
                </asp:Panel>
            </div>
            <div style="padding-top: 5px">
                <asp:Panel ID="pnlTransactions" runat="server">
                    <div>
                        <table class="tableStyle2" cellpadding="5px" cellspacing="5px">
                            <tr>
                                <td>
                                    Batch
                                </td>
                                <td>
                                    <asp:TextBox ID="tbBatch" runat="server" Enabled="false" Width="100px" ForeColor="Black"></asp:TextBox>
                                </td>
                                <td>
                                    Admission Facilitator
                                </td>
                                <td>
                                    <asp:TextBox ID="tbSCCode" runat="server" Enabled="false" Width="60px" ForeColor="Black"></asp:TextBox>
                                </td>
                                <td>
                                    Course
                                </td>
                                <td>
                                    <asp:TextBox ID="tbCourse" runat="server" Enabled="false" Width="200px" ForeColor="Black"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="text" align="center" style="padding-top: 10px">
                        <asp:DataList ID="dtlistShowTransactions" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                            ItemStyle-CssClass="dtlistItem">
                            <HeaderTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 30px">
                                            <b>S.No.</b>
                                        </td>
                                        <td align="left" style="width: 125px">
                                            <b>Enrollment No.</b>
                                        </td>
                                        <td align="left" style="width: 150px">
                                            <b>Student Name</b>
                                        </td>
                                        <td align="left" style="width: 150px">
                                            <b>Father Name</b>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <b>Course</b>
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <b>Year</b>
                                        </td>
                                        <td align="left" style="width: 80px">
                                            <b>Req.Fee</b>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <b>Paid Fee</b>
                                        </td>
                                        <td align="left" style="width: 80px">
                                            <b>Fee (%)</b>
                                        </td>
                                        <td align="left" style="width: 80px">
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
                                            <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 125px">
                                            <%#Eval("EnrollmentNo")%>
                                        </td>
                                        <td align="left" style="width: 150px">
                                            <%#Eval("StudentName")%>
                                        </td>
                                        <td align="left" style="width: 150px">
                                            <%#Eval("FatherName")%>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <%#Eval("Course")%>
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <%#Eval("Year")%>
                                        </td>
                                        <td align="left" style="width: 80px">
                                            <%#Eval("ReqFee")%>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <%#Eval("Trans")%>
                                            <asp:Label ID="lblPaidFee" runat="server" Visible="false" Text='<%#Eval("PaidFee")%>'></asp:Label>
                                        </td>
                                        <td align="left" style="width: 80px">
                                            <%#Eval("FeePer")%>
                                        </td>
                                        <td align="left" style="width: 80px">
                                            <asp:Label ID="lblRefund" runat="server" Text='<%#Eval("Refund")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div align="center" style="padding-top: 10px">
                        <table class="tableStyle2" cellpadding="0px" cellspacing="10px">
                            <tbody align="left">
                                <tr>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        Reimbursement to A.F.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbTotalRefund" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        2
                                    </td>
                                    <td>
                                        Balance to be Pay/Extra
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbBalanceExtra" ForeColor="Black" Enabled="false" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        3
                                    </td>
                                    <td>
                                        Balance to be Received/Short
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbBalanceShort" ForeColor="Black" Enabled="false" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        4
                                    </td>
                                    <td>
                                        Net Payable to A.F.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbNetRefund" runat="server" ForeColor="Black" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="text" style="padding-top: 90px">
                        <table width="1100px">
                            <tbody align="center">
                                <tr>
                                    <td style="width: 33%">
                                        <b>Prepared By</b>
                                    </td>
                                    <td style="width: 33%">
                                        <b>Checked By</b>
                                    </td>
                                    <td style="width: 33%">
                                        <b>Accounts Officer</b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div align="center" style="font-size: 12px; padding-top: 20px; font-family:Cambria">
                    <div style="width: 280px; border: 1px solid black; border-radius: 3px; padding: 3px">
                        'DLM'<br />
                        Designed & Developed By : IT Cell, DDE
                    </div>
                </div>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td align="center" style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
