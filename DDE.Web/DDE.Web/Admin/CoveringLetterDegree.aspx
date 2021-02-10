<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CoveringLetterDegree.aspx.cs" Inherits="DDE.Web.Admin.CoveringLetterDegree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DDE</title>
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
        .hd
        {
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
         .dtlist
        {
            border: 1px solid #0099FF;
        }
        
        .dtlistItemAC
        {
            background: url(images/ac_body.jpg) repeat-x top;
            border: 1px solid #0099FF;
            font-family: Verdana;
            text-align: left;
            font-size: 12px;
            color: #003f6f;
        }
        
        .dtlistItemIC
        {
            border: 1px solid #0099FF;
            font-family: Verdana;
            text-align: left;
            font-size: 12px;
            color: #003f6f;
        }
        .dtlistheader
        {
            background-image: url(../Images/GVHeader.jpg);
            background-repeat: repeat-x;
            height: 25px;
            font-family: Verdana;
            font-weight: bold;
            font-size: 14px;
            color: #003f6f;
        }
        
        .dtlistfooter
        {
            background-image: url(../Images/GVHeader.jpg);
            background-repeat: repeat-x;
            height: 25px;
            font-family: Verdana;
            font-weight: bold;
            font-size: 14px;
            color: #003f6f;
        }
        
        
        .dtlistItem
        {
            background-color: #FFFFFF;
            border: 1px solid #0099FF;
            font-family: Verdana;
            text-align: left;
            margin-left: 10px;
            font-size: 12px;
            padding-left: 10px;
            margin-left: 10px;
            color: #003f6f;
        }
        
        .dtlistheaderDS
        {
            background-image: url(../Images/GVHeader.jpg);
            background-repeat: repeat-x;
            height: 25px;
            font-family: Verdana;
            font-weight: bold;
            font-size: 14px;
            color: #003f6f;
            border: 1px solid #0099FF;
        }
        
        .dtlistItemDS
        {
            background-color: White;
            margin: 0px;
            font-family: Verdana;
            text-align: left;
            font-size: 12px;
            padding-left: 0px;
            color: #003f6f;
        }
        .style1
        {
            height: 25px;
        }
    </style>
</head>
<body style="font-family: Verdana; font-size: 14px; margin: 0px">
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div>
                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="70px" OnClick="btnPrint_Click" />
            </div>
            <div align="center" style="width: 100%; margin: 2px">
                <div align="center" style="width: 330px;">
                    <div style="font-size: 13px; font-family: Berlin Sans FB">
                        Distance Learning Manager (D.L.M.)
                    </div>
                    <div style="width: 320px; font-family: Cambria; font-size: 10px; margin: 0px" align="right">
                        <i>...An ERP System Managing DDE, SVSU</i>
                    </div>
                </div>
            </div>
            <div style="width: 800px; margin: 0px">
                <div style="width: 800px; margin: 0px">
                    <table width="100%">
                        <tr>
                            <td>
                                <img src="../Images/logo1.png" />
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
                    <hr style="border-bottom-style: solid; margin: 0px" />
                </div>
                <div style="background-image: url(Images/ddelogo3.png); background-repeat: no-repeat;
                    background-position: center">
                    <div>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td align="left" style="padding-left: 10px; width: 400px">
                                        <asp:Label ID="lblRefNo" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMLID" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblPNo" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblDate" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="padding-top: 0px">
                            <p class="to">
                                To<br />
                                The Degree Incharge<br />
                                
                            </p>
                           <%-- <p>
                                <b><u>Through: The Director, DDE</u> </b>
                            </p>--%>
                            <p class="to">
                                <u><b>Subject: For issuing of Final Degree/s</b></u>
                            </p>
                            <p align="left" style="padding-left: 10px">
                                Respected Sir,<br />
                                <br />
                                We are forwarding you following application/s along with required relevant documents for
                                issuing of Final Degree/s. Student name and detail are as under:-
                            </p>
                        </div>
                       
                        <div align="center" style="padding-left: 0px">
                           <asp:DataList ID="dtlistSLM" GridLines="Both" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                    ItemStyle-CssClass="dtlistItem">
                                    <HeaderTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 40px">
                                                    <b>SNo.</b>
                                                </td>
                                                <td align="left" style="width: 300px; padding-left: 10px">
                                                    <b>Student's Name</b>
                                                </td>
                                                <td align="left" style="width: 150px">
                                                    <b>Enrollment No</b>
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
                                                <td align="left" style="width: 300px">
                                                   <%#Eval("StudentName")%>
                                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblMID" runat="server" Text='<%#Eval("DIID")%>' Visible="false"></asp:Label>                                          
                                                </td>
                                                <td align="left" style="width: 150px">
                                                 <%#Eval("ENo")%>
                                                </td>                   
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList> 
                        </div>
                    </div>
                    <div>
                        <p align="left" style="padding-left: 10px; padding-top: 10px">
                            You are requested to kindly accord your sanction for the same.
                            <br />
                            <br />
                            Thanking you.
                        </p>
                    </div>
                    <div style="padding-top: 20px">
                    <p align="left" style="padding-left: 20px; padding-bottom: 0px; margin: 0px; float:left">
                    <br />
                            <b>Authorised Signatory</b>
                        </p>
                        <p align="right" style="padding-right: 20px; padding-bottom: 0px; margin: 0px; float:right">
                            <b>
                                Director<br />
                                DDE, SVSU</b>
                        </p>
                    </div>
                </div>
                <div style="padding-top: 80px">
                    <hr style="border-bottom-style: solid; margin: 2px" />
                    Assuring you of our best Educational Services always
                    <hr style="border-bottom-style: solid; margin: 2px" />
                </div>
                <div align="center" style="font-size: 10px; padding-top: 10px">
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
