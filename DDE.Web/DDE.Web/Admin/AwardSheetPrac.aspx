<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AwardSheetPrac.aspx.cs"
    Inherits="DDE.Web.Admin.AwardSheetPrac" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Practical Award Sheet</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
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
            <div class="heading3" style="padding-top: 10px">
                <b>Directorate of Distance Education</b>
                <br />
                Swami Vivekanand Subharti University, Meerut
            </div>
            <div class="heading2" style="padding-top: 20px">
                Practical Award-Sheet
            </div>
            <div class="heading3" style="padding-top: 5px">
                <asp:Label ID="lblExamName" runat="server" Text=""></asp:Label>
            </div>
            <div class="astable" style="padding-top: 20px">
                <table width="1000px" cellspacing="10px">
                    <tr>
                        <td valign="top" align="left">
                            Practical Name
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td valign="top" align="left" style="width: 480px">
                            <asp:Label ID="lblPracticalName" runat="server" Text=""></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            Practical Code 
                        </td>
                        <td>
                        :
                        </td>
                        <td valign="top" align="left">
                            <asp:Label ID="lblPracticalCode" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">
                            Course
                        </td>
                        <td valign="top">
                            :
                        </td>
                        <td valign="top" align="left" style="width: 450px">
                            <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label><asp:Label ID="lblCourseID"
                                runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            Year
                        </td>
                          <td>
                        :
                        </td>
                        <td valign="top" align="left">
                            <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 140px">
                            A.F. Code
                        </td>
                        <td>
                            :
                        </td>
                        <td align="left">
                            <asp:Label ID="lblSCCode" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                            Max. Marks 
                        </td>
                          <td>
                        :
                        </td>
                        <td align="left">
                             <asp:Label ID="lblMMarks" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 20px">
                <asp:GridView ID="gvAwarsSheet" UseAccessibleHeader="true" AutoGenerateColumns="false" HeaderStyle-CssClass="asgvheader"
                    RowStyle-CssClass="asgvrow" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="S.No." DataField="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Enrollment No." DataField="EnrollmentNo" HeaderStyle-Width="150px"
                            ItemStyle-HorizontalAlign="left" />
                        <asp:BoundField HeaderText="Roll No." DataField="RollNo" HeaderStyle-Width="120px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="" HeaderStyle-Width="100px" HtmlEncode="false" ItemStyle-HorizontalAlign="Center"
                            ItemStyle-CssClass="ascourse" />
                        <asp:BoundField HeaderText="" HeaderStyle-Width="100px" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="" HeaderStyle-Width="100px" HtmlEncode="false" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Marks Obt. (In Words)" HeaderStyle-Width="220px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Remarks" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </div>
            <div align="center" class="asfooter" style="padding-top: 80px">
                <table cellspacing="8px">
                    <tbody align="left">
                        <tr>
                            <td>
                                Signature of Examiner
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
                                Full Name of Examiner
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
                    </tbody>
                </table>
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
