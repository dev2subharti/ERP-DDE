<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="FillDec12Marks.aspx.cs" Inherits="DDE.Web.Admin.FillDec12Marks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            <b>Fill Marks for December 2012 Examination</b><br />
            (Regular Only)
        </div>
        <div style="padding: 20px">
            <asp:Panel ID="pnlSearch" runat="server">
                <table class="tableStyle2" cellpadding="10px" cellspacing="10px">
                    <tr>
                        <td>
                            Enrollment No.
                        </td>
                        <td>
                            <asp:TextBox ID="tbEnrollmentNo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Syllabus Session
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSySession"  runat="server">
                            <asp:ListItem>A 2010-11</asp:ListItem>
                             <asp:ListItem>A 2009-10</asp:ListItem>                            
                            </asp:DropDownList>
                            
                        </td>
                        <td>
                            Year
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server">
                            <asp:ListItem Value="0">--SELECT ONE--</asp:ListItem>
                             <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                             <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                             <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                             </asp:DropDownList>
                            <asp:DropDownList ID="ddlistMOE" Enabled="false" Visible="false" runat="server">
                            <asp:ListItem Value="R">REGULAR</asp:ListItem>
                            
                             </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="font-family: Arial; font-size: 15px">
            <asp:Panel ID="pnlMS" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table class="text" align="center">
                                <tr>
                                    <td>
                                        <div align="center">
                                            <asp:Label ID="lblCourseFullName" runat="server" Font-Bold="true" Font-Size="Larger"
                                                Font-Underline="True"></asp:Label>
                                            <br />
                                          
                                            <asp:Panel ID="pnlMarkSheet" runat="server">
                                                <div style="padding:10px">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width: 140px" align="left">
                                                                <b>Name</b>
                                                            </td>
                                                            <td>
                                                                <b>:</b>
                                                            </td>
                                                            <td align="left" style="width: 200px">
                                                                <asp:Label ID="lblSName" runat="server" Text=""></asp:Label>
                                                                <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label>
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
                                                <div align="center" style="padding-top: 0px; background-image:url(../Images/upperstrip3.jpg)">
                                                    <asp:DataList Width="100%" ID="dtlistSubMarks" runat="server" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="1px" >
                                                        <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                        <HeaderTemplate>
                                                            <table width="100%" border="1" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td align="center" style="width: 20%">
                                                                        <b>Course Code</b>
                                                                    </td>
                                                                    <td align="center" style="width: 55%">
                                                                        <b>Subject</b>
                                                                    </td>
                                                                    <td>
                                                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                            <tr>
                                                                                <td colspan="3" class="borderbottom">
                                                                                    <b>Marks Obtained</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="borderright" style="width: 50px">
                                                                                    <b>TEE</b>
                                                                                </td>
                                                                                <td style="width: 50px" class="borderright">
                                                                                    <b>I.A.</b>
                                                                                </td>
                                                                                <td align="center" style="width: 50px">
                                                                                    <b>A.W.</b>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table width="100%" align="left" cellpadding="0px" cellspacing="0px">
                                                                <tbody align="left">
                                                                    <tr>
                                                                        <td class="borderright" style="width: 20%; text-align: left; padding-left: 5px">
                                                                            <%#Eval("SubjectCode")%>
                                                                            <asp:Label ID="lblSubjectID" runat="server" Text='<%#Eval("SubjectID")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" class="borderright_tal" style="width: 55%">
                                                                            <asp:Label ID="lblSubjectName" runat="server" Text='<%#Eval("SubjectName")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="borderright" style="width: 50px; text-align: center">
                                                                            <asp:TextBox ID="tbTheory" runat="server" Width="50px" Text='<%#Eval("Theory")%>'></asp:TextBox>
                                                                            <asp:Label ID="lblTheory" runat="server" Text='<%#Eval("Theory")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td class="borderright" style="width: 50px">
                                                                            <asp:TextBox ID="tbIA" runat="server" Width="50px" Text=' <%#Eval("IA")%>'></asp:TextBox>
                                                                            <asp:Label ID="lblIA" runat="server" Text='<%#Eval("IA")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td class="borderright" style="width: 50px">
                                                                            <asp:TextBox ID="tbAW" runat="server" Width="50px" Text=' <%#Eval("AW")%>'></asp:TextBox>
                                                                            <asp:Label ID="lblAW" runat="server" Text='<%#Eval("AW")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <asp:DataList Width="100%" ID="dtlistPracMarks" runat="server" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="1px" >
                                                        <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                        <ItemTemplate>
                                                            <table width="100%" align="left" cellpadding="0px" cellspacing="0px">
                                                                <tbody align="left">
                                                                    <tr>
                                                                        <td class="borderright" style="width: 20%; text-align: left; padding-left: 5px">
                                                                            <%#Eval("PracticalCode")%>
                                                                            <asp:Label ID="lblPracticalID" runat="server" Text='<%#Eval("PracticalID")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td align="left" class="borderright_tal" style="width: 55%">
                                                                        <asp:Label ID="lblPracticalName" runat="server" Text='<%#Eval("PracticalName")%>' ></asp:Label>
                                                                            
                                                                        </td>
                                                                        <td class="borderright" style="width: 50px">
                                                                            <asp:TextBox ID="tbPOMarks" runat="server" Width="50px" Text='<%#Eval("PracticalObtainedMarks")%>'></asp:TextBox> 
                                                                            <asp:Label ID="lblPOMarks" runat="server" Text='<%#Eval("PracticalObtainedMarks")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div align="center" style="padding-top: 10px">
                   
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                        onclick="btnSubmit_Click" />
                </div>
                <div>
                 <asp:Label ID="lblTheoryExist" runat="server" Text="No" Visible="false"></asp:Label>
                    <asp:Label ID="lblPracticalExist" runat="server" Text="No" Visible="false"></asp:Label>
                </div>
            </asp:Panel>
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
        
        <div align="center" style="padding-top:5px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" onclick="btnOK_Click" />
        </div>
    </asp:Panel>
</asp:Content>
