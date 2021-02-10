<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ReceiveProjects.aspx.cs" Inherits="DDE.Web.Admin.ReceiveProjects" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center" class="heading">
                Receive Project
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
                                         
                                        </asp:DropDownList>
                                    </td>
                                   
                                </tr>
                                <tr> <td>
                                        <b>Mode of Exam</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistMOE" Width="120px" runat="server">
                                            <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                            <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                                        </asp:DropDownList>
                                    </td></tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNo" runat="server" Text="Enrollment No."></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
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
                                                                <asp:Label ID="lblSSession" Visible="false" runat="server"></asp:Label>
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
                                                                <asp:Label ID="lblCourseID" runat="server" Visible="false" Text='<%#Eval("CourseID")%>'></asp:Label>
                                                               
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
                                       Project of Examination</h2>
                                    <asp:DataList ID="dtlistRecAS" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                        ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistRecAS_ItemCommand">
                                        <HeaderTemplate>
                                            <table align="left" cellpadding="0px" cellspacing="0px">
                                                <tr>
                                                    <td align="center" style="width: 50px">
                                                        SNo.
                                                    </td>
                                                    <td align="left" style="width: 120px">
                                                        Project Code
                                                    </td>
                                                   
                                                    <td align="center" style="width: 320px">
                                                        Title of Project
                                                    </td>
                                                    <td align="left" style="width: 180px">
                                                        Project Status
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
                                                      <asp:Label ID="lblCourseCode"  runat="server" Text=' <%#Eval("SubjectCode")%>'></asp:Label>   
                                                        <asp:Label ID="lblSubjectID" Visible="false" runat="server" Text=' <%#Eval("SubjectID")%>'></asp:Label>
                                                    </td>
                                                    
                                                    <td align="left" style="width: 300px">
                                                        <%#Eval("SubjectName")%>
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
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
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
