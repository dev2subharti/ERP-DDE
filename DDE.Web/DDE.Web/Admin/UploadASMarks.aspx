<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="UploadASMarks.aspx.cs" Inherits="DDE.Web.Admin.UploadASMarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery/jquery-1.4.2.min.js">
    </script>
    <script type="text/javascript">
        $(function () {
            $('input:text:first').focus();
            var $inp = $('input:text');
            $inp.bind('keydown', function (e) {
                //var key = (e.keyCode ? e.keyCode : e.charCode);
                var key = e.which;
                if (key == 13) {
                    e.preventDefault();
                    var nxtIdx = $inp.index(this) + 1;
                    $(":input:text:eq(" + nxtIdx + ")").focus();
                }
            });
        });
    </script>
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading" style="padding-top: 0px">
                Upload Award-Sheet Marks
            </div>
            <div style="padding-top: 10px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>Examination</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExam" runat="server">
                                    <asp:ListItem Value="B13">DECEMBER 2013</asp:ListItem>
                                    <asp:ListItem Value="A14">JUNE 2014</asp:ListItem>
                                    <asp:ListItem Value="B14">DECEMBER 2014</asp:ListItem>
                                    <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                    <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                    <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                    <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                    <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                   <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                  <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                                   <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                                    <asp:ListItem Value="W10" Selected="True">JUNE 2019</asp:ListItem>
                                    <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <b>Award Sheet No.</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbASNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <asp:Panel ID="pnlAS" runat="server" Visible="false">
                <div style="padding-top: 20px">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td valign="top" align="left">
                                Subject
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td valign="top" align="left" style="width: 380px">
                                <asp:Label ID="lblSubjectName" runat="server" Text=""></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                Subject Code
                            </td>
                            <td valign="top">
                                :
                            </td>
                            <td valign="top" align="left">
                                <asp:Label ID="lblSubjectCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="left" class="style1">
                                Max. Marks
                            </td>
                            <td valign="top" class="style1">
                                :
                            </td>
                            <td valign="top" align="left" class="style1">
                                100
                            </td>
                            <td valign="top" align="left" class="style1">
                                Award Sheet No
                            </td>
                            <td valign="top" class="style1">
                                :
                            </td>
                            <td valign="top" align="left" class="style1">
                                <asp:Label ID="lblASCounter" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="padding-top: 20px">
                    <asp:DataList ID="dtlistAS" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem">
                        <HeaderTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 50px">
                                        <b>S.No.</b>
                                    </td>
                                    <td align="left" style="width: 150px; padding-left: 10px">
                                        <b>Enrollment No.</b>
                                    </td>
                                    <td align="left" style="width: 150px">
                                        <b>Roll No.</b>
                                    </td>
                                    <td align="left" style="width: 250px">
                                        <b>Course</b>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <b>Marks Obt.</b>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 50px">
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                        <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSubjectID" runat="server" Text='<%#Eval("SubjectID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblSCCode" runat="server" Text='<%#Eval("SCCode")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblMOE" runat="server" Text='<%#Eval("MOE")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td align="right" style="width: 120px; padding-right: 30px">
                                        <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                    </td>
                                    <td align="left" style="width: 150px">
                                        <%#Eval("RollNo")%>
                                    </td>
                                    <td align="left" style="width: 250px">
                                        <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <asp:TextBox ID="tbMO" Width="50px" MaxLength="2" runat="server" Text='<%#Eval("MarksObt")%>'></asp:TextBox>
                                        <asp:Label ID="lblMO" runat="server" Text='<%#Eval("MarksObt")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblMarksFilled" runat="server" Text='<%#Eval("MarksFilled")%>' Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div style="padding-top: 5px">
                    <asp:Panel ID="pnlExaminer" runat="server" Visible="false">
                        <table class="tableStyle2">
                            <tr>
                                <td>
                                    <b>Select Examiner</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistExaminer" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div style="padding-top: 5px">
                    <asp:Button ID="btnUploadMarks" runat="server" Text="Upload Marks" OnClick="btnUploadMarks_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td align="center" style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="padding-top: 10px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Width="60px" Visible="false" OnClick="btnOK_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphheadColleges">
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
    </style>
</asp:Content>
