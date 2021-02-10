<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AwardSheet.aspx.cs" Inherits="DDE.Web.Admin.AwardSheet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Theory Award Sheet</title>
    <script type="text/javascript">
        function pr() {

            window.print();
        }
    </script>
    <style type="text/css">
        .heading2
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 26px;
            color: Black;
            text-decoration: underline;
        }
        .heading3
        {
            font-family: Arial;
            font-size: 22px;
            color: Black;
        }
        .asfooter
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 18px;
            color: Black;
        }
        .astable table td
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 18px;
            color: Black;
        }
        .asgvheader
        {
            height: 45px;
            text-align: center;
            font-family: Arial;
            font-weight: bold;
            font-size: 18px;
            color: Black;
        }
        .asgvrow
        {
            height: 35px;
            text-align: center;
            font-family: Arial;
            margin: 5px;
            font-size: 18px;
            color: Black;
        }
        .ascourse
        {
            padding-left: 5px;
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
        
        .table1
        {
            font-family: Verdana;
            font-size: 16px;
        }
        
        .table1 td
        {
            padding: 5px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div align="center" style="padding-top: 10px">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading3" style="padding-top: 0px">
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
                <div style="float: right">
                    <asp:HyperLink ID="hlinkQPFile" Font-Size="12px" runat="server">Download QP</asp:HyperLink>
                </div>
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
            <div align="left" class="heading3" style="padding-left: 10px; width: 48%; float: left">
                <asp:Label ID="lblAT" runat="server" Text="Allot To : "></asp:Label>
                <asp:DropDownList ID="ddlistAT" runat="server">
                </asp:DropDownList>
            </div>
            <div align="right" class="heading3" style="padding-right: 10px; width: 48%; float: right">
                <asp:Label ID="lblASCounter" runat="server" Text="0" Visible="false"></asp:Label>
            </div>
            <div class="heading3" style="padding-top: 30px">
                <b>Directorate of Distance Education</b>
                <br />
                Swami Vivekanand Subharti University, Meerut
            </div>
            <div class="heading2" style="padding-top: 20px">
                Award-Sheet
            </div>
            <div class="heading3" style="padding-top: 5px">
                <asp:Label ID="lblExamName" runat="server" Text=""></asp:Label>
            </div>
            <div class="astable" style="padding-top: 20px">
                <table width="1000px" cellspacing="10px">
                    <tr>
                        <td valign="top" align="left">
                            Subject
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td valign="top" align="left" style="width: 550px">
                            <asp:Label ID="lblSubjectName" runat="server" Text=""></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            Subject Code :
                        </td>
                        <td valign="top" align="left">
                            <asp:Label ID="lblSubjectCode" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100px">
                            Max. Marks
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            100
                        </td>
                        <td align="right">
                            No. of Copies :
                        </td>
                        <td align="left">
                            <asp:Label ID="lblTC" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 20px">
                <asp:GridView ID="gvAwarsSheet" AutoGenerateColumns="false" HeaderStyle-CssClass="asgvheader"
                    RowStyle-CssClass="asgvrow" Width="1000px" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="S.No." DataField="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Enrollment No." DataField="EnrollmentNo" HeaderStyle-Width="150px"
                            ItemStyle-HorizontalAlign="right" />
                        <asp:BoundField HeaderText="Roll No." DataField="RollNo" HeaderStyle-Width="120px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Course" DataField="Course" HeaderStyle-Width="300px"
                            ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="ascourse" />
                        <asp:BoundField HeaderText="Marks Obt. (In Figures)" DataField="MarksObt" HeaderStyle-Width="120px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Marks Obt. (In Words)" DataField="MarksObt(In Words)"
                            HeaderStyle-Width="260px" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </div>
            <div align="center" style="padding-top: 80px">
                <div class="asfooter" align="left" style="width: 780px">
                    <table cellspacing="8px" style="width: 780px">
                        <tr>
                            <td>
                                Signature & Full Name of Faculty
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                ..............................................................................
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Designation
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                ..............................................................................
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                ..............................................................................
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                ..............................................................................
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mobile No.
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                ..............................................................................
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                Note : Please send the Award Sheet in Triplicate.
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div align="right" style="padding-right: 20px">
                <asp:Label ID="lblPrintedBy" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div align="center" style="font-size: 12px; padding-top: 20px; font-family: Cambria">
                <div style="width: 280px; border: 1px solid black; border-radius: 3px; padding: 3px">
                    'DLM'<br />
                    Designed & Developed By : IT Cell, DDE
                </div>
            </div>
        </asp:Panel>
        <br />
        <br />
        <br style="page-break-before: always" />
        <asp:Panel ID="pnlQP" runat="server" Visible="false">
            <div align="center">
            <div class="heading2">
            Question Paper
            </div>
                <div align="left">
                    <div>
                        <div style="float: right">
                            Paper Code :
                            <asp:Label ID="lblPaperCode" runat="server" Text=""></asp:Label>
                        </div>
                        <br />
                        <br />
                        <div style="float: right">
                            Roll No :..................................
                        </div>
                        <br />
                        <div align="center">
                            <div>
                                <asp:Label ID="lblCourseCode" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblExamination" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblSubName" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div style="float: left">
                            Time : 3 Hours ]
                        </div>
                        <div style="float: right">
                            [ Max. Marks : 100
                        </div>
                        <br />
                        <br />
                        <div>
                            Note. Attempt any Five questions. All questions carry equal marks.
                        </div>
                    </div>
                    <div align="left" style="padding-left: 50px">
                        <asp:DataList ID="dtlistShowQP" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                            ItemStyle-CssClass="dtlistItem">
                            <ItemTemplate>
                                <table align="left" cellspacing="0px">
                                    <tr>
                                        <td valign="top" align="left" style="width: 50px; padding-top: 20px">
                                            Q.<asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                        </td>
                                        <td valign="top" align="left" style="width: 150px; padding-top: 20px">
                                            <asp:Label ID="lblQID" runat="server" Text='<%#Eval("QID")%>' Visible="false"></asp:Label>
                                            <asp:Image ID="imgQuestion" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </asp:Panel>
      <br />
        <br />
        <br style="page-break-before: always" />
        <asp:Panel ID="pnlASlip" runat="server" Visible="false">
            <div align="center" style="padding-top: 80px">
                <div class="heading2">
                    Acknowledgement Slip DDE
                </div>
                <br />
                <div align="center">
                    <table class="table1" rules="all" style="border: 1px solid black">
                        <tbody align="left">
                            <tr>
                                <td>
                                    <b>Award Sheet No.</b>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblASNoAS" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <b>Total Copies</b>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblTCAS" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Paper Code</b>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblPaperCodeAS" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <b>Total Disqualified</b>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblTDAS" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Evaluator Name</b>
                                </td>
                                <td>
                                    :
                                </td>
                                <td colspan="4">
                                    <asp:Label ID="lblENAS" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Date of Issue</b>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblDOIAS" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    <b>Date of Submit</b>
                                </td>
                                <td>
                                    :
                                </td>
                                <td>
                                    <asp:Label ID="lblDOSAS" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <br />
                                   <b>Note :</b> 
                                    <ul style="margin-left:20px; margin-top:5px">
                                        <li>Please preserve a copy of filled Award sheet with you. </li>
                                        <li>This signed and stamped acknowledgement Slip will be treated as the only <br /> evidence of submission of Award Sheet along with Answer Sheets.</li>
                                    </ul>
                                    <br /><br /><br /><br />.................................................<br />
                                   <b>Signature of Receiver with Stamp</b> 
                                </td>
                            </tr>
                        </tbody>
                    </table>
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
