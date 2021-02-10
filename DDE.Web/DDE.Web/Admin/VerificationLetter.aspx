<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerificationLetter.aspx.cs"
    Inherits="DDE.Web.Admin.VerificationLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verification Letter</title>
    <script type="text/javascript">
        function pr() {

            window.print();
        }
    </script>
    <style type="text/css">
        .head
        {
            text-align: center;
            font-size: 26px;
            font-weight: bold;
            margin: 0px;
        }
        .head1
        {
            text-align: center;
            font-size: 20px;
            font-weight: bold;
            margin: 0px;
        }
        
        .add
        {
            text-align: center;
            font-family: Verdana;
            margin: 0px;
        }
        
        .to
        {
            font-family: Verdana;
            margin-left: 10px;
            text-align: left;
        }
        .gv
        {
            margin-left: 5px;
        }
        .msgpnl
        {
            padding-top: 100px;
        }
        
        
        .msg
        {
            font-family: Verdana;
            font-size: 14px;
            font-weight: bold;
        }
        
        .pnlmsg
        {
            padding-top: 50px;
        }
        
        .tableStyle2
        {
            font-family: Verdana;
            font-weight: bold;
            font-size: 12px;
            color: #003f6f;
            text-decoration: none;
            background-image: url(../Images/upperstrip3.jpg);
            border: solid 1px #003f6f;
        }
    </style>
</head>
<body style="font-family: Verdana; font-size: 14px">
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div>
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" OnClick="btnPrint_Click" />
            </div>
           <div align="center" style="width: 100%; margin:2px">
                <div align="center" style="width: 330px; 
                   ">
                    <div style="font-size: 13px; font-family: Berlin Sans FB">
                       Distance Learning Manager (D.L.M.)
                    </div>
                    <div style="width: 320px; font-family: Cambria; font-size: 10px; margin: 0px" align="right">
                        <i>...An ERP System Managing DDE, SVSU</i>
                    </div>
                </div>
            </div>
            <div style="width: 800px">
                <div style="width: 800px">
                    <table width="100%">
                        <tr>
                            <td>
                                <img src="../Images/logo.jpg" />
                            </td>
                            <td>
                                <p class="head">
                                    DIRECTORATE OF DISTANCE EDUCATION
                                </p>
                                <p class="head1">
                                    SWAMI VIVEKANAND SUBHARTI UNIVERSITY
                                </p>
                                <p class="add">
                                     Subhartipuram,NH-58,Delhi-Haridwar-Meerut By-Pass, Meerut-250005<br />
                                    Phone : 0121-3055000, Ext : 2800,2801 Fax : 0121-2439067<br />
                                    Website : www.subhartidde.com, E-mail : distance@subharti.org
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <hr style="border-bottom-style: solid" />
                </div>
                <div style="background-image: url(Images/ddelogo3.png); background-repeat: no-repeat;
                    background-position: center">
                    <div >
                        <div>
                            <table width="100%">
                                <tr>
                                    <td align="left" style="padding-left: 10px; width: 400px">
                                        <asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblVLNo" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPNo" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="padding-top: 0px">
                            <p class="to">
                                To<br />
                                The Account Officer<br />
                                DDE, SVSU<br />
                                Meerut
                            </p>
                            <p class="to">
                                <u><b>Subject : Submission of fee details :-</b></u>
                            </p>
                        </div>
                        <div align="left">
                            <asp:DataList ID="dtlistSC" runat="server">
                                <ItemTemplate>
                                    <table width="98%">
                                        <tr>
                                            <td valign="top" align="left" style="padding-left: 5px; width: 85px">
                                                A.F. Code :
                                            </td>
                                            <td valign="top" align="center" style="width: 80px; border: 1px solid black">
                                                <asp:Label ID="lblSCCode" runat="server" Text='<%#Eval("SCCode")%>'></asp:Label>
                                            </td>
                                            <td style="width: 30px">
                                                &nbsp;
                                            </td>
                                            <td valign="top" align="left" style="width: 85px">
                                                A.F. Name :
                                            </td>
                                            <td valign="top" align="left" style="border: 1px solid black; padding-left: 2px; width:450px">
                                                <asp:Label ID="lblSCName" runat="server" Text='<%#Eval("SCName")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div style="padding-top: 10px">
                            <p class="to">
                                <b>Types of Fee :-</b>
                            </p>
                        </div>
                        <div align="left" style="padding-left: 10px">
                            <asp:DataList ID="dtlistFeeType" runat="server">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 30px">
                                                <%#Eval("SNo")%>
                                            </td>
                                            <td style="width: 250px">
                                                <%#Eval("FeeHead")%>
                                            </td>
                                            <td align="center" style="border: 1px solid black; width: 60px">
                                                <img src="../Images/rt.png" height="15px" width="15px" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div style="padding-top: 20px">
                            <p class="to">
                                <b>Instruments Details :-</b>
                            </p>
                        </div>
                        <div align="left" style="padding-left: 10px">
                            <asp:GridView ID="gvIDetails" RowStyle-CssClass="gv" HeaderStyle-CssClass="gv" Width="98%"
                                runat="server">
                            </asp:GridView>
                            <%--<asp:DataList ID="dtlistIDetails" Width="98%" runat="server">
                    <HeaderTemplate>
                        <table width="100%" border="1" rules="all">
                            <tr>
                                <td>
                                    <b>S. No.</b>
                                </td>
                                <td>
                                    <b>Instrument Type</b>
                                </td>
                                <td>
                                    <b>Instrument No.</b>
                                </td>
                                <td>
                                    <b>Date</b>
                                </td>
                                <td>
                                    <b>Amount</b>
                                </td>
                                <td>
                                    <b>Bank</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table width="100%" border="1" rules="all">
                            <tr>
                                <td>
                                    <%#Eval("SNo") %>
                                </td>
                                <td>
                                    <%#Eval("Type") %>
                                </td>
                                <td>
                                    <%#Eval("DCNumber") %>
                                </td>
                                <td>
                                    <%#Eval("DCDate") %>
                                </td>
                                <td>
                                    <%#Eval("DCAmount") %>
                                </td>
                                <td>
                                    <%#Eval("IBN") %>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>--%>
                        </div>
                        <div align="left" style="width: 560px; padding-top: 5px">
                        <table>
                        <tr>
                        <td>
                       <b>Total Amount</b> 
                        </td>
                        <td>
                       <b>:</b> 
                        </td>
                        <td>
                        <asp:Label ID="lblTotal" Font-Bold="true" runat="server" Text=""></asp:Label>
                        </td>
                        </tr>
                      <%--  <tr>
                        <td>
                      <b>In Words</b> 
                        </td>
                        <td>
                       <b>:</b>
                        </td>
                        <td>
                         <asp:Label ID="lbTotalInWords" Font-Bold="true" runat="server" Text=""></asp:Label>
                        </td>
                        </tr>--%>
                        </table>
                           
                           
                        </div>
                        <div>
                            <p class="to">
                                <asp:Label ID="lblAdjustment" Font-Bold="true" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div>
                            <p class="to">
                                <b>Note :-</b> The detail of the students can be obtained from ERP, after verification
                                of instrument from Accounts and subsequenty after submission of Admission/ Exam
                                Form into the ERP System
                            </p>
                        </div>
                    </div>
                    <div style="padding-top: 20px">
                        <table width="100%">
                            <tr>
                                <td style="width: 34%" align="left">
                                    <b>Prepared by :<br />
                                        (C.P.U. Cell)
                                        <br />
                                        Signature : </b>
                                </td>
                                <td style="width: 34%" valign="bottom" align="left">
                                    <b>Authorised by :
                                        <br />
                                        <asp:Label ID="lblAuthorisedBy" runat="server" Text="(Director)"></asp:Label>
                                        <br />
                                        Signature : </b>
                                </td>
                                <td style="width: 32%" align="left">
                                    <b>Received by :<br />
                                        (Accounts)
                                        <br />
                                        Signature : </b>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="padding-top: 20px">
                    <hr style="border-bottom-style: solid; margin: 2px" />
                    Assuring you of our best Educational Services always
                    <hr style="border-bottom-style: solid; margin: 2px" />
                </div>
                <div align="center" style="font-size: 12px; padding-top: 10px; font-family:Cambria">
                    <div style="width: 280px; border: 1px solid black; border-radius: 3px; padding: 3px">
                        'DLM'<br />
                        Designed & Developed By : IT Cell, DDE
                    </div>
                </div>
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
            <div>
                <table cellspacing="20px">
                    <tr>
                        <td>
                            <asp:Button ID="btnYes" runat="server" Text="Yes" Width="60px" OnClick="btnYes_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnNo" runat="server" Text="No" Width="60px" OnClick="btnNo_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
