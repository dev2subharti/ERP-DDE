<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="FillReceivedCopy.aspx.cs" Inherits="DDE.Web.Admin.FillReceivedCopy" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center" class="heading">
                Manage Answer Sheet Record
            </div>
            <div style="padding-top: 10px">
                <table cellspacing="10px" class="tableStyle1">
                    <tr>
                        <td valign="top" align="left">
                            <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">By Enrollment No.</asp:ListItem>
                                <asp:ListItem Value="2">By Roll No.</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 10px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <div>
                        <table cellspacing="10px" class="tableStyle2">
                            <tbody align="left">
                                <tr>
                                    <td>
                                        <b>Examination</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistExamination" Width="150px" runat="server">
                                            <asp:ListItem Value="A13">JUNE 2013</asp:ListItem>
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
                                             <asp:ListItem Value="W10">JUNE 2019</asp:ListItem>
                                             <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>
                                             <asp:ListItem Value="W11">JUNE 2020</asp:ListItem>
                                             <asp:ListItem Value="Z11" Selected="True">DECEMBER 2020</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <b>Mode of Exam</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistMOE" Width="120px" runat="server">
                                            <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                            <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNo" runat="server" Text="Enrollment No."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblEYear" runat="server" Text="Year" Visible="false"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlistYear" Width="120px" runat="server" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div style="padding-top: 10px">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                        Width="75px" Height="26px" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnSearchAgain" runat="server" Visible="false" Text="Search" Width="75px"
                                        Height="26px" OnClick="btnSearchAgain_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <asp:Panel ID="pnlASRecord" runat="server" Visible="false">
                <div>
                    <div>
                        <table width="800px" class="tableStyle2">
                            <tr>
                                <td align="center" style="padding-top: 10px">
                                    <table align="left" width="100%" cellpadding="0" cellspacing="0">
                                        <tbody align="left">
                                            <tr>
                                                <td align="left">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <b>Enrollment No. : </b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                                                <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <b>Study Centre Code : </b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSCCode" runat="server" Text='<%#Eval("SCCode")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <b>Roll No. : </b>
                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRollNo" runat="server" Text='<%#Eval("RollNo")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding-top: 10px">
                                    <table cellspacing="0px">
                                        <tr>
                                            <td valign="top">
                                                <asp:Image ID="imgStudent" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                                    runat="server" Width="100px" Height="120px" />
                                            </td>
                                            <td valign="top">
                                                <table align="left" cellspacing="5px">
                                                    <tbody align="left">
                                                        <tr>
                                                            <td style="width: 170px">
                                                                <b>Full Name of Candidate</b>
                                                            </td>
                                                            <td>
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSName" runat="server" Text='<%#Eval("SName")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Father’s/Husband Name</b>
                                                            </td>
                                                            <td>
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFName" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Batch</b>
                                                            </td>
                                                            <td>
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBatch" runat="server"></asp:Label>
                                                                <asp:Label ID="lblSSession" runat="server"></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Course</b>
                                                            </td>
                                                            <td>
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Year</b>
                                                            </td>
                                                            <td>
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <b>Examination Centre</b>
                                                            </td>
                                                            <td valign="top">
                                                                <b>:</b>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:Label ID="lblEC" runat="server" Text='<%#Eval("ExamCentre")%>'></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align="center">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <h2 align="center" class="text" style="padding-top: 20px; padding-bottom: 0px">
                                        Subjects of Examination</h2>
                                    <div align="center" style="padding-bottom:10px">
                                        <asp:Button ID="btnSelectAll" CssClass="btn" Width="120px" runat="server" AccessKey="A" Text="Select All" OnClick="btnSelectAll_Click" />
                                    </div>
                                    <asp:DataList ID="dtlistRecAS" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                        ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistRecAS_ItemCommand">
                                        <HeaderTemplate>
                                            <table align="left" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td align="center" style="width: 50px">
                                                        SNo.
                                                    </td>
                                                    <td align="left" style="width: 120px">
                                                        Course Code
                                                    </td>
                                                    <td align="left" style="width: 100px">
                                                        Paper Code
                                                    </td>
                                                    <td align="center" style="width: 320px">
                                                        Title of Paper
                                                    </td>
                                                    <td align="left" style="width: 180px">
                                                        Ans. Sheet Status
                                                    </td>
                                                    <td align="center" style="width: 150px">
                                                        Entered By
                                                    </td>
                                                    <td align="center" style="width: 150px">
                                                        Time of Entry
                                                    </td>
                                                    <td align="center" style="width: 60px">
                                                        AS No.
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td align="left" style="width: 40px">
                                                        <%#Eval("SubjectSNo")%>
                                                      
                                                    </td>
                                                    <td align="left" style="width: 120px">
                                                        <%#Eval("SubjectCode")%>
                                                        <asp:Label ID="lblSubjectID" Visible="false" runat="server" Text=' <%#Eval("SubjectID")%>'></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 100px">
                                                        <asp:Label ID="lblPaperCode" runat="server" Text=' <%#Eval("PaperCode")%>'></asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 300px">
                                                        <%#Eval("SubjectName")%>
                                                    </td>
                                                    <td align="left" style="width: 50px">
                                                        <asp:ImageButton ID="imgError" ImageUrl="~/Admin/images/tenor.gif" Visible="false" Height="40px" runat="server" />
                                                    </td>
                                                    <td align="left" style="width: 190px">
                                                        <asp:RadioButtonList ID="rblRec" CellSpacing="10" RepeatDirection="Horizontal" runat="server">
                                                            <asp:ListItem Value="1" class="present">Received</asp:ListItem>
                                                            <asp:ListItem Value="0" class="absent">Absent</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:RadioButtonList ID="rblPRec" Visible="false" RepeatDirection="Horizontal" runat="server">
                                                            <asp:ListItem Value="1">Received</asp:ListItem>
                                                            <asp:ListItem Value="0">Absent</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:Label ID="lblAF" Visible="false" runat="server"></asp:Label>
                                                        <asp:Label ID="lblASRID" Visible="false" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="center" style="width: 150px">
                                                        <asp:Label ID="lblRecBy" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="center" style="width: 150px">
                                                        <asp:Label ID="lblRecTime" runat="server"></asp:Label>
                                                    </td>
                                                    <td align="center" style="width: 50px">
                                                        <asp:Label ID="lblASNo" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CausesValidation="false"
                                                            ImageUrl="~/Admin/images/delete.png" Visible="false" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                            runat="server" Width="20px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnSubmit" runat="server" AccessKey="S" Text="Submit" OnClick="btnSubmit_Click" />
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
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" OnClick="btnOK_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
