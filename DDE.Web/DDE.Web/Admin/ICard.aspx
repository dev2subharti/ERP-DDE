<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ICard.aspx.cs" Inherits="DDE.Web.Admin.ICard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>I Cards</title>
    <link href="../css/DDE.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div style="padding: 20px">
                <div align="center">
                    <asp:DataList ID="dtlistICards" Width="1000px" ItemStyle-VerticalAlign="Top" RepeatDirection="Horizontal"
                        RepeatColumns="2" runat="server" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItemIC">
                        <ItemTemplate>
                            <table class="icard_text" width="500px" style="margin: 10px" cellpadding="0" cellspacing="0"
                                border="1">
                                <tr>
                                    <td align="left" style="width: 100%; background-color: #003f6f">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="padding-left: 10px; padding-bottom: 5px; padding-top: 2px">
                                                    <img src="images/dde_logo.png" width="80px" height="80px" />
                                                </td>
                                                <td align="center" valign="top">
                                                <div class="icard_dde">
                                                        DIRECTORATE OF DISTANCE EDUCATION
                                                    </div>
                                                    <div class="icard_svsu">
                                                        SWAMI VIVEKANAND SUBHARTI UNIVERSITY, MEERUT - 250005
                                                    </div>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <div class="icard_bk">
                                            <table width="100%">
                                                <tr>
                                                    <td style="padding: 10px; width: 100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <b>I Card No. :</b>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("ICardNo")%>
                                                                </td>
                                                                <td style="width: 50px">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <b>Enrollment No. :</b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                                                    <%#Eval("EnrollmentNo")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 100%">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="padding-left: 10px">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 110px">
                                                                                <b>Student Name</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("SName")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Father's Name</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("FName")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Mother's Name</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("MName")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Programme</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("Course")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Session</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("Batch")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Gender</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("Gender")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Category</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("Category")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Phone No.</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("MNo")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Email ID</b>
                                                                            </td>
                                                                            <td>
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td>
                                                                                <%#Eval("Email")%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Address</b>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <b>:</b>
                                                                            </td>
                                                                            <td valign="top" style="height: 40px">
                                                                                <%#Eval("Address")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 110px" valign="top" align="right">
                                                                      <asp:Image ID="imgStudentPhoto" runat="server" CssClass="img_stph"/>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="padding-top: 0px; padding-left: 0px; width: 100%">
                                                        <div align="center" style="width: 100%">
                                                            <table width="100%">
                                                                <tbody align="center">
                                                                    <tr>
                                                                        <td style="text-align:center">
                                                                          <img src="images/PKS1.png" width="100px" height="50px" />
                                                                        </td>
                                                                        <td style="text-align:center">
                                                                            
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr> 
                                                                    <tr>
                                                                        <td style="text-align:left; padding-left:10px">
                                                                            <b>Director/Ad. Director</b>
                                                                        </td>
                                                                        <td style="text-align:center">
                                                                            <b>Officer In Charge (Admission)</b>
                                                                        </td>
                                                                        <td style="text-align:right; padding-right:10px">
                                                                            <b>(Student’s Sign)</b>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
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
