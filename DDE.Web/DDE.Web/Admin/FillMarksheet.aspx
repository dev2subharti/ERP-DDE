<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="FillMarksheet.aspx.cs" Inherits="DDE.Web.Admin.FillMarksheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div>
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center" class="text" style="padding-top: 20px">
                <div>
                    <h3>
                        Find Marksheet</h3>
                </div>
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="center">
                            <table cellspacing="10px">
                                <tr>
                                    <td>
                                        <b>Select Syllabus Session</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistSySession" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Select Course</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistCourse" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="10px">
                                <tr>
                                    <td>
                                        <b>Select Year</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistYear" runat="server">
                                            <asp:ListItem Value="1">1st Year</asp:ListItem>
                                            <asp:ListItem Value="2">2nd Year</asp:ListItem>
                                            <asp:ListItem Value="3">3rd Year</asp:ListItem>
                                            <asp:ListItem Value="4">4th Year</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Select Exam</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistExam" runat="server">
                                            <asp:ListItem>June 2011</asp:ListItem>
                                            <asp:ListItem>August 2011</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Enrollment No.</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbEnrolNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnFind" runat="server" Text="Find" Width="80px" OnClick="btnFind_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:Label ID="lblTotSub" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblTotPrac" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div align="center">
                <asp:Panel ID="pnlMarkSheet" runat="server" Visible="false">
                    <div style="padding-left: 0px; padding-top: 20px; padding-right: 0px">
                        <table  class="tableStyle2">
                            <tr>
                                <td style="width: 120px" align="left">
                                    <b>Name</b>
                                </td>
                                <td>
                                    <b>:</b>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbSName" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 400px">
                                    &nbsp;
                                </td>
                                <td style="width: 120px" align="left">
                                    <b>Roll No.</b>
                                </td>
                                <td>
                                    <b>:</b>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbRNo" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>Father's Name</b>
                                </td>
                                <td>
                                    <b>:</b>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbFName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <b>Enrollment No.</b>
                                </td>
                                <td>
                                    <b>:</b>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbENo" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>SC Code</b>
                                </td>
                                <td>
                                    <b>:</b>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbSCCode" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align="center" style="padding-top: 20px">
                        <table  border="1" class="tableStyle2" width="1000px" cellpadding="0px" cellspacing="0px">
                            <tbody align="center">
                                <tr>
                                    <td width="8%">
                                        <b>Course Code</b>
                                    </td>
                                    <td width="35%">
                                        <b>Subject</b>
                                    </td>
                                    <td align="center" style="width: 200px">
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td colspan="3" class="borderbottom">
                                                    <b>Maximum Marks</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="2" class="borderright" style="width: 50px">
                                                    <b>TEE</b>
                                                </td>
                                                <td style="width: 100px">
                                                    <table width="100px" cellpadding="0px" cellspacing="0px">
                                                        <tr>
                                                            <td colspan="2" class="borderbottom">
                                                                <b>Continuous
                                                                    <br />
                                                                    Internal
                                                                    <br />
                                                                    Assessment</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%" class="borderright">
                                                                <b>I.A.</b>
                                                            </td>
                                                            <td align="center" style="width: 50%">
                                                                <b>A.W.</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 50px" class="borderleft">
                                                    <b>Total</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="center" style="width: 200px">
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td colspan="3" class="borderbottom">
                                                    <b>Marks Obtained</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td rowspan="2" class="borderright" style="width: 50px">
                                                    <b>TEE</b>
                                                </td>
                                                <td style="width: 100px">
                                                    <table width="100px" cellpadding="0px" cellspacing="0px">
                                                        <tr>
                                                            <td colspan="2" class="borderbottom">
                                                                <b>Continuous
                                                                    <br />
                                                                    Internal
                                                                    <br />
                                                                    Assessment</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%" class="borderright">
                                                                <b>I.A.</b>
                                                            </td>
                                                            <td align="center" style="width: 50%">
                                                                <b>A.W.</b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 50px" class="borderleft">
                                                    <b>Total</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <b>Grade</b>
                                    </td>
                                    <td>
                                        <b>Status</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCC1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="center">
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                    60
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                    20
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                    20
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    100
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbTheory1" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbIA1" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbAW1" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTotal1" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCC2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                    60
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                    20
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                    20
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    100
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbTheory2" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbIA2" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbAW2" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTotal2" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCC3" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub3" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                    60
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                    20
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                    20
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    100
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbTheory3" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbIA3" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbAW3" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTotal3" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade3" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus3" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCC4" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub4" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                    60
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                    20
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                    20
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    100
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbTheory4" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbIA4" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbAW4" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTotal4" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade4" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus4" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCC5" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub5" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                    60
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                    20
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                    20
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    100
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbTheory5" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbIA5" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbAW5" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTotal5" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade5" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus5" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblCC6" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub6" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                    60
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                    20
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                    20
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    100
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="tbTheory6" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbIA6" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbAW6" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbTotal6" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade6" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus6" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 30px">
                                        <asp:Label ID="lblCC7" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub7" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlMM7" runat="server" Visible="false">
                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                        <asp:Label ID="lblMMTheory7" runat="server" Text="60"></asp:Label>
                                                    </td>
                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                        <asp:Label ID="lblMMIW7" runat="server" Text="20"></asp:Label>
                                                    </td>
                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                        <asp:Label ID="lblMMAW7" runat="server" Text="20"></asp:Label>
                                                    </td>
                                                    <td align="center" style="height: 30px; width: 49px">
                                                        <asp:Label ID="lblMMTotal7" runat="server" Text="100"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlMO7" runat="server" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="tbTheory7" runat="server" Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbIA7" runat="server" Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbAW7" runat="server" Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbTotal7" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade7" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus7" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 30px">
                                        <asp:Label ID="lblCC8" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblSub8" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlMM8" runat="server" Visible="false">
                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td class="borderright" style="height: 30px; width: 48px">
                                                        <asp:Label ID="lblMMTheory8" runat="server" Text="60"></asp:Label>
                                                    </td>
                                                    <td class="borderright" style="height: 30px; width: 49px">
                                                        <asp:Label ID="lblMMIW8" runat="server" Text="20"></asp:Label>
                                                    </td>
                                                    <td class="borderright" style="height: 30px; width: 50px">
                                                        <asp:Label ID="lblMMAW8" runat="server" Text="20"></asp:Label>
                                                    </td>
                                                    <td align="center" style="height: 30px; width: 49px">
                                                        <asp:Label ID="lblMMTotal8" runat="server" Text="100"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlMO8" runat="server" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="tbTheory8" runat="server" Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbIA8" runat="server" Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbAW8" runat="server" Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbTotal8" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade8" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus8" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 30px">
                                        <asp:Label ID="lblCC9" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblPrac1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    <asp:Label ID="lblMaxPracMarks1" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox ID="tbPracMaksObtained1" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade9" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus9" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="height: 30px">
                                        <asp:Label ID="lblCC10" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="left" style="padding-left: 20px">
                                        <asp:Label ID="lblPrac2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td class="borderright" style="height: 30px; width: 48px">
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 49px">
                                                </td>
                                                <td class="borderright" style="height: 30px; width: 50px">
                                                </td>
                                                <td align="center" style="height: 30px; width: 49px">
                                                    <asp:Label ID="lblMaxPracMarks2" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox ID="tbPracMaksObtained2" runat="server" Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGrade10" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus10" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="center">
                                                    <h3 style="margin: 0px">
                                                        Grand Total</h3>
                                                </td>
                                                <td align="center" style="height: 30px; width: 48px" class="borderleft">
                                                    <h3 style="margin: 0px">
                                                        <asp:Label ID="lblGTMMarks" runat="server" Text=""></asp:Label></h3>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="3">
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td style="width: 150px">
                                                    &nbsp;
                                                </td>
                                                <td align="right" class="borderright" style="height: 30px">
                                                    <asp:TextBox ID="tbGrandTotal" runat="server" Width="40px" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td align="center" style="width: 78px" class="borderright">
                                                    <h3 style="margin: 0px">
                                                        <asp:Label ID="lblGrade11" runat="server" Text=""></asp:Label></h3>
                                                </td>
                                                <td align="center" style="width: 79px">
                                                    <h3 style="margin: 0px">
                                                        <asp:Label ID="lblStatus11" runat="server" Text=""></asp:Label></h3>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>
                <div>
                    <table width="200px" cellspacing="20px">
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" OnClick="btnUpdate_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" OnClick="btnCancel_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnPubMar" runat="server" Text="Publish Marksheet" Visible="false"
                                    OnClick="btnPubMar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table align="center" class="tableStyle2">
                <tr>
                    <td style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
