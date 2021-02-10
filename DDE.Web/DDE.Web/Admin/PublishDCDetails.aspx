<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishDCDetails.aspx.cs"
    Inherits="DDE.Web.Admin.PublishDCDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Draft Details</title>
    <link href="../CSS/DDE.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading" align="center">
                 Draft Details
            </div>
            <div style="padding-top:10px">
                <asp:Panel ID="pnlDCDetail" runat="server" Visible="false">
                    <div class="data" style="padding-top: 0px" align="center">
                        <table class="tableStyle2"  cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <table width="388px" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="10px">
                                                    <tr>
                                                        <td align="left">
                                                            <b>DRAFT TYPE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDType" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>DRAFT No.</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>DRAFT DATE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDCDate" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
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
                                                    <tr>
                                                        <td align="left">
                                                            <b>USED AMOUNT</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbUsedAmount" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>BALANCE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbBalance" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
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
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowTransactions" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 30px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 85px; padding-left: 8px">
                                    <b>Form No.</b>
                                </td>
                                <td align="left" style="width: 125px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Batch</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>SC Code</b>
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
                                <td align="left" style="width: 160px">
                                    <b>Fee Name</b>
                                </td>
                                <td align="right" style="width: 80px">
                                    <b>Amount</b>
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
                                <td align="left" style="width: 85px">
                                    <%#Eval("FormNo")%>
                                </td>
                                <td align="left" style="width: 125px">
                                    <asp:LinkButton ID="lnkbtnENo" runat="server" CommandName="ENo" CommandArgument='<%#Eval("SRID")%>'> <%#Eval("EnrollmentNo")%></asp:LinkButton>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("Batch")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("SCCode")%>
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
                                <td align="left" style="width: 150px">
                                    <%#Eval("FeeHead")%>
                                </td>
                                <td align="right" style="width: 80px">
                                    <%#Eval("Amount")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" HorizontalAlign="Center" runat="server" CssClass="msgpnl" Visible="false">
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
