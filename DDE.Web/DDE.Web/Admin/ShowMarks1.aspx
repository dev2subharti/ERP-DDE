<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ShowMarks1.aspx.cs" Inherits="DDE.Web.Admin.ShowMarks1" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="font-family: Arial; font-size: 15px">
            <table width="100%">
                <tr>
                    <td align="center">
                        <table class="text" align="center" width="1140px">
                            <tr>
                                <td>
                                    <div align="center">
                                        <asp:Label ID="lblCourseFullName" runat="server" Font-Bold="true" Font-Size="Larger"
                                            Font-Underline="True"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblBP" runat="server" Font-Bold="true" Text="(BACK PAPER)"></asp:Label>
                                        <asp:Panel ID="pnlMarkSheet" runat="server">
                                            <div style="padding-left: 15px; padding-top: 25px; padding-right: 15px">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 140px" align="left">
                                                            <b>Name</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left" style="width: 550px">
                                                            <asp:Label ID="lblSName" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblCID" runat="server" Text="" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblSySession" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 140px">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 130px" align="left">
                                                            <b>Roll No.</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblRNo" runat="server" Text=""></asp:Label>
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
                                                            <asp:Label ID="lblFName" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="lblMName" runat="server" Text="" Visible="false"></asp:Label>
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
                                                            <asp:Label ID="lblENo" runat="server" Text=""></asp:Label>
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
                                                            <asp:Label ID="lblSCCode" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <b>Examination</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblExamination" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div align="center" style="padding-top: 18px">
                                                <asp:DataList Width="100%" ID="dtlistSubMarks" runat="server" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="1px" OnItemCommand="dtlistSubMarks_ItemCommand">
                                                    <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    <HeaderTemplate>
                                                        <table width="100%" border="1" cellpadding="0px" cellspacing="0px">
                                                            <tr>
                                                                <td align="center" style="width: 12%">
                                                                    <b>Course Code</b>
                                                                </td>
                                                                <td align="center" style="width: 36%">
                                                                    <b>Subject</b>
                                                                </td>
                                                                <td style="width: 200px">
                                                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                        <tbody align="center">
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
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 200px">
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
                                                                <td align="center">
                                                                    <b>Grade</b>
                                                                </td>
                                                                <td align="center">
                                                                    <b>Status</b>
                                                                </td>
                                                                <td style="width: 70px" align="center">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table align="left" cellpadding="0px" cellspacing="0px">
                                                            <tbody align="left">
                                                                <tr>
                                                                    <td class="borderright" style="width: 131px; text-align: left; padding-left: 5px">
                                                                        <%#Eval("SubjectCode")%>
                                                                        <asp:Label ID="lblSubjectID" runat="server" Text='<%#Eval("SubjectID")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td align="left" class="borderright_tal" style="width: 400px">
                                                                        <%#Eval("SubjectName")%>
                                                                    </td>
                                                                    <td class="borderright" style="width: 47px">
                                                                        <%#Eval("MTheory")%>
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        <%#Eval("MIA")%>
                                                                    </td>
                                                                    <td class="borderright" style="width: 51px">
                                                                        <%#Eval("MAW")%>
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        <%#Eval("MTotal")%>
                                                                    </td>
                                                                    <td class="borderright" style="width: 32px; text-align: left; padding-left: 15px">
                                                                        <asp:Label ID="lblTheory" runat="server" Text='<%#Eval("Theory")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        <asp:Label ID="lblIA" runat="server" Text=' <%#Eval("IA")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="width: 51px">
                                                                        <asp:Label ID="lblAW" runat="server" Text=' <%#Eval("AW")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        <b>
                                                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                                        </b>
                                                                    </td>
                                                                    <td class="borderright" style="width: 31px; text-align: left; padding-left: 22px">
                                                                        <asp:Label ID="lblGrade" runat="server" Text='<%#Eval("Grade")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" align="center" style="width: 56px">
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="width: 70px">
                                                                        <asp:LinkButton ID="lnkbtnDeleteSMarks" runat="server" Text="Delete" CommandName="Delete"
                                                                            OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                                            CommandArgument='<%#Eval("RID") %>'></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:DataList Width="100%" ID="dtlistPracMarks" runat="server" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="1px" OnItemCommand="dtlistPracMarks_ItemCommand">
                                                    <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    <ItemTemplate>
                                                        <table align="left" cellpadding="0px" cellspacing="0px">
                                                            <tbody align="left">
                                                                <tr>
                                                                    <td class="borderright" style="width: 131px; text-align: left; padding-left: 5px">
                                                                        <%#Eval("PracticalCode")%>
                                                                        <asp:Label ID="lblPracticalID" runat="server" Text='<%#Eval("PracticalID")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td align="left" class="borderright_tal" style="width: 400px">
                                                                        <%#Eval("PracticalName")%>
                                                                    </td>
                                                                    <td class="borderright" style="width: 47px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="width: 51px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        <asp:Label ID="lblPMMarks" runat="server" Text='<%#Eval("PracticalMaxMarks")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" style="width: 47px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="width: 51px">
                                                                        -
                                                                    </td>
                                                                    <td class="borderright" style="width: 48px">
                                                                        <b>
                                                                            <asp:Label ID="lblPOMarks" runat="server" Text='<%#Eval("PracticalObtainedMarks")%>'></asp:Label>
                                                                        </b>
                                                                    </td>
                                                                    <td class="borderright" style="width: 53px">
                                                                        <asp:Label ID="lblPGrade" runat="server" Text='<%#Eval("PracticalGrade")%>'></asp:Label>
                                                                    </td>
                                                                    <td class="borderright" align="center" style="width: 56px">
                                                                        <asp:Label ID="lblPStatus" runat="server" Text='<%#Eval("PracticalStatus")%>'></asp:Label>
                                                                    </td>
                                                                    <td align="center" style="width: 70px">
                                                                        <asp:LinkButton ID="lnkbtnDeletePMarks" runat="server" Text="Delete" CommandName="Delete"
                                                                            OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                                            CommandArgument='<%#Eval("PID") %>'></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <table width="100%" cellpadding="0px" cellspacing="0px" border="1">
                                                    <tr>
                                                        <td align="center" style="width: 542px">
                                                            <h3 style="margin: 0px">
                                                                Grand Total</h3>
                                                        </td>
                                                        <td style="width: 150px">
                                                            &nbsp;
                                                        </td>
                                                        <td align="center" style="width: 48px">
                                                            <asp:Label ID="lblGTMMarks" runat="server" Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td style="width: 150px">
                                                            &nbsp;
                                                        </td>
                                                        <td align="center" style="width: 48px">
                                                            <asp:Label ID="lblGrandTotal" runat="server" Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 53px">
                                                            <asp:Label ID="lblGrade" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 56px">
                                                            <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="center" style="width: 70px">
                                                            <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </div>
                                            <div align="left" style="padding-top:20px">                                              
                                            <table width="100%">
                                                <tr>
                                                    <td align="left" style="width: 70%">
                                                        <b>Abbreviations:</b>
                                                    </td>
                                                </tr>
                                            </table>
                                            <p style="margin-top: 0px">
                                                <b>TEE:</b> Term End Examinations, <b>IA:</b> Internal Assessment, <b>AW:</b> Assignment
                                                Work, <b>CC:</b> Credits Clear, <b>NC:</b> Not Clear, <b>AB:</b> Absent,
                                                <b>* </b>Back Paper Marks.
                                            </p>                                                      
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div align="center" style="padding-top: 20px">
                                        <table width="200px" border="1" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td align="left" style="padding: 5px; width: 70px">
                                                    <b>Result</b>
                                                </td>
                                                <td align="center" style="padding: 5px">
                                                    <asp:Label ID="lblResult" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="padding: 5px">
                                                    <b>Division</b>
                                                </td>
                                                <td align="center" style="padding: 5px">
                                                    <asp:Label ID="lblDivision" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div align="center" style="padding-top: 20px">
                                        <table cellspacing="10px">
                                            <tr>
                                                <td>
                                                    Auto Date
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rblAutoDate" CssClass="tableStyle2"
                                                        runat="server">
                                                        <asp:ListItem Selected="True">YES</asp:ListItem>
                                                        <asp:ListItem>NO</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                
                                                <td style="padding-left: 20px">
                                                    Print Mode
                                                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rblPrintMode" CssClass="tableStyle2"
                                                        runat="server">
                                                        <asp:ListItem Value="N" Selected="True">NEW</asp:ListItem>
                                                        <asp:ListItem Value="C">CORRECTION</asp:ListItem>
                                                         <asp:ListItem Value="D">DUPLICATE</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div align="center" style="padding-top: 10px">
                                        <asp:Button ID="btnPubMar" runat="server" Text="Publish Marksheet" Visible="false"
                                            OnClick="btnPubMar_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle2">
            <tr>
                <td align="center" style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
