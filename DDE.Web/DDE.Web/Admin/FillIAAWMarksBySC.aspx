<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/StudyCentre.Master"
    CodeBehind="FillIAAWMarksBySC.aspx.cs" Inherits="DDE.Web.Admin.FillIAAWMarksBySC" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Fill IA and AW Marks
            </div>
            <div align="center" class="text" style="padding: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Examination</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Mode of Examination</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistMOE" runat="server" Width="150px" 
                                Enabled="false">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="R" Selected="True">REGULAR</asp:ListItem>
                                <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Course</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Year</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistYear_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="lblSubject" runat="server" Text="Subject" Visible="false"></asp:Label></b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSubject" runat="server" Width="150px" Visible="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlistSubject_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>
                                <asp:Label ID="lblBatch" runat="server" Text="Batch" Visible="false"></asp:Label></b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" Visible="false"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding-bottom: 20px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowStudents" runat="server" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 210px">
                                    <b>Student Name</b>
                                </td>
                                <td align="center" style="width: 60px; padding-left: 5px">
                                    <b>IA</b>
                                </td>
                                <td align="center" style="width: 60px; padding-left: 10px">
                                    <b>AW</b>
                                </td>
                                <td align="center" style="width: 200px; padding-left: 10px">
                                    <b>Remark</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblMarksFilled" runat="server" Text='<%#Eval("MarksFilled")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 210px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="center" style="width: 60px">
                                    <asp:TextBox ID="tbIA" runat="server" Text='<%#Eval("IAMarks")%>' Width="50px"></asp:TextBox>
                                    <asp:Label ID="lblIA" runat="server" Text='<%#Eval("IAMarks")%>' Visible="false"></asp:Label>
                                </td>
                                <%-- <td>
                                <asp:RangeValidator ID="rvIA" runat="server" ControlToValidate="tbIA" MinimumValue="0"
                                    MaximumValue="20" ErrorMessage="*"></asp:RangeValidator>
                            </td>--%>
                                <td align="center" style="width: 60px">
                                    <asp:TextBox ID="tbAW" runat="server" Text='<%#Eval("AWMarks")%>' Width="50px"></asp:TextBox>
                                    <asp:Label ID="lblAW" runat="server" Text='<%#Eval("AWMarks")%>' Visible="false"></asp:Label>
                                </td>
                                <%--<td>
                                <asp:RangeValidator ID="rvAW" runat="server" ControlToValidate="tbAW" MinimumValue="0"
                                    MaximumValue="20" ErrorMessage="*"></asp:RangeValidator>
                            </td>--%>
                                <td align="center" style="width: 200px">
                                    <asp:Label ID="lblRemark" runat="server" ForeColor="#F8A403" Text='<%#Eval("Remark")%>'></asp:Label>  
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div align="center" style="padding: 20px">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit Marks" OnClick="btnSubmit_Click"
                    Visible="false" />
            </div>
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
    <div align="center" style="padding: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="75px" OnClick="btnOK_Click" />
    </div>
</asp:Content>
