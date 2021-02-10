<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="UploadPracAS.aspx.cs" Inherits="DDE.Web.Admin.UploadPracAS" %>

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
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading" style="padding-top: 20px">
            Practical Award-Sheet
        </div>
        <div style="padding-top: 10px">
            <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td class="style1">
                            <b>Examination</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlistExam_SelectedIndexChanged">
                                <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                 <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                                  <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                                 <asp:ListItem Value="W10" Selected="True">JUNE 2019</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>AF Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlistSCCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>Practical Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistPracCode" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlistPracCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnPublish" runat="server" Text="Search" OnClick="btnPublish_Click" />
                </div>
            </asp:Panel>
        </div>
        <div class="text" style="padding-top: 20px">
        <asp:Panel ID="pnlAS" runat="server" Visible="false">
            <table cellspacing="10px" class="tableStyle2">
                <tr>
                    <td valign="top" align="left" style="width: 120px">
                        Practical Name
                    </td>
                    <td valign="top">
                        :
                    </td>
                    <td valign="top" align="left" style="width: 200px">
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
                    <td valign="top" align="left">
                        <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblCourseID" runat="server" Text="" Visible="false"></asp:Label>
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
                    <td align="left">
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
            </asp:Panel>
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
                                 <asp:Label ID="lblMOE" runat="server" Text='<%#Eval("MOE")%>' Visible="false"></asp:Label>                                                                                              
                            </td>
                            <td align="right" style="width: 120px; padding-right: 30px">
                                <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                            </td>
                            <td align="left" style="width: 150px">
                                <%#Eval("RollNo")%>
                            </td>
                            <td align="left" style="width: 100px">
                                <asp:TextBox ID="tbMO" Width="50px" MaxLength="3" runat="server" Text='<%#Eval("MarksObt")%>'></asp:TextBox>
                                <asp:Label ID="lblMO" runat="server" Text='<%#Eval("MarksObt")%>' Visible="false"></asp:Label>
                                <asp:Label ID="lblMarksFilled" runat="server" Text='<%#Eval("MarksFilled")%>' Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
         <div style="padding-top: 5px">
                    <asp:Button ID="btnUploadMarks" runat="server" Visible="false" Text="Upload Marks" OnClick="btnUploadMarks_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
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
         <div style="padding-top: 10px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Width="60px" Visible="false" OnClick="btnOK_Click" />
            </div>
    </asp:Panel>
</asp:Content>
