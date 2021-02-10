<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SLMLetter.aspx.cs" Inherits="DDE.Web.Admin.SLMLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SLM Letter</title>
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
    </style>
</head>
<body style="font-family: Verdana; font-size: 14px">
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
            <div style="width: 800px">
                <div style="width: 800px">
                    <table width="100%">
                        <tr>
                            <td>
                                <img src="../Images/logo.jpg" />
                            </td>
                            <td>
                                <div align="center" style="font-size: 10px">
                                    [On Trial]
                                </div>
                                <p class="head">
                                    DIRECTORATE OF DISTANCE EDUCATION
                                </p>
                                <p class="head1">
                                    SWAMI VIVEKANAND SUBHARTI UNIVERSITY
                                </p>
                                <p class="add">
                                    Subhartipuram,NH-58,Delhi-Haridwar-Meerut By-Pass, Meerut-250005<br />
                                    Phone : 0121-3055029/3055028/2439043, Fax : 0121-2439067<br />
                                    Website : www.subhartidde.com, E-mail : ddesvsu@gmail.com
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <hr style="border-bottom-style: solid" />
                </div>
                <div>
                    <div>
                        <div>
                            <table width="100%">
                                <tr>
                                    <td align="left" style="padding-left: 10px; width: 400px">
                                        <asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblLNo" runat="server" Text="" Visible="false"></asp:Label>
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
                                <br />
                                <asp:Label ID="lblTo" runat="server">
                                 
                                </asp:Label>
                            </p>
                            <p class="to">
                                <u><b>Subject : Issue of SLM</b></u>
                            </p>
                            <p class="to">
                                Dear Sir / Madam,
                            </p>
                            <p class="to">
                                We are issuing you SLM as per following details
                            </p>
                        </div>
                        <%--<div align="left" style="padding-left: 10px">
                            <asp:DataList ID="dtlistShowSet" GridLines="Both" Visible="false" runat="server">
                                <HeaderTemplate>
                                    <table align="left">
                                        <tr>
                                            <td align="left" style="width: 40px">
                                                <b>SNo.</b>
                                            </td>
                                            <td align="left" style="width: 250px; padding-left: 10px">
                                                <b>Course</b>
                                            </td>
                                            <td align="left" style="width: 100px">
                                                <b>Year</b>
                                            </td>
                                            <td align="left" style="width: 100px">
                                                <b>No. of Set</b>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table align="left">
                                        <tr>
                                            <td align="left" style="width: 50px">
                                                <%#Eval("SNo")%>
                                            </td>
                                            <td align="left" style="width: 250px">
                                                <%#Eval("Course")%>
                                            </td>
                                            <td align="left" style="width: 100px">
                                                <%#Eval("Year")%>
                                            </td>
                                            <td align="left" style="width: 100px">
                                                <%#Eval("TotalStudents")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>--%>
                        <div align="left" style="padding-left: 10px; padding-top: 0px">
                            <asp:Panel ID="pnlSLMList" runat="server" Visible="true">
                                <asp:DataList ID="dtlistSLM" GridLines="Both" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                    ItemStyle-CssClass="dtlistItem">
                                    <HeaderTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 40px">
                                                    <b>SNo.</b>
                                                </td>
                                                <td align="left" style="width: 100px; padding-left: 10px">
                                                    <b>SLMCode</b>
                                                </td>
                                                <td align="left" style="width: 300px">
                                                    <b>Title</b>
                                                </td>
                                                <td align="left" style="width: 100px">
                                                    <b>Qty.</b>
                                                </td>
                                                <%-- <td align="left" style="width: 100px">
                                                <b>In Stock</b>
                                            </td>--%>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 50px">
                                                    <%#Eval("SNo")%>
                                                </td>
                                                <td align="left" style="width: 100px">
                                                    <asp:Label ID="lblSLMCode" runat="server" Text='<%#Eval("SLMCode")%>'></asp:Label>
                                                    <asp:Label ID="lblSLMID" runat="server" Text='<%#Eval("SLMID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblLang" runat="server" Text='<%#Eval("Lang")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblDual" runat="server" Text='<%#Eval("Dual")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblGroupID" runat="server" Text='<%#Eval("GroupID")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 300px">
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 60px">
                                                    <asp:TextBox ID="tbQty" Width="50px" Enabled="false" ForeColor="Black"  runat="server" Text='<%#Eval("Quantity")%>'></asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 50px">
                                                    <asp:Label ID="lblPresentStock" runat="server" Text='<%#Eval("PresentStock")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                                <div align="right" style="width: 500px">
                                    <table>
                                        <tr>
                                            <td>
                                                <b>Total SLM</b>
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTotalSLM" Font-Bold="true" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                        <div align="left">
                            <p class="to">
                                Thanking you
                            </p>
                            <br />
                            <br />
                            <br />
                            <p>
                                <b>[Manoj Kumar]<br />
                                    Material Incharge</b>
                            </p>
                        </div>
                    </div>
                </div>
                <div style="padding-top: 20px">
                    <hr style="border-bottom-style: solid; margin: 2px" />
                    Assuring you of our best Educational Services always
                    <hr style="border-bottom-style: solid; margin: 2px" />
                </div>
                <div align="center" style="font-size: 12px; padding-top: 10px; font-family: Cambria">
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
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
