<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="FillExamFees.aspx.cs" Inherits="DDE.Web.Admin.FillExamFees" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-top: 20px">
                Fill Examination Fees
            </div>
            <div align="center" class="text" style="padding: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Select Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                                <asp:ListItem>--Select Here--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Select Course</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                                <asp:ListItem>--Select Here--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Select Batch</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                                <asp:ListItem>--Select Here--</asp:ListItem>
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
                <asp:DataList ID="dtlistShowStudents" runat="server" CssClass="dtlist" 
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 70px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>S.C.Code</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Enrollment No</b>
                                </td>
                                <td align="left" style="width: 180px; padding-left: 10px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 210px">
                                    <b>Father's Name</b>
                                </td>
                                <td align="center" style="width: 100px">
                                    <b>Exam Fee</b>
                                </td>
                                <td align="center" style="width: 100px; padding-left: 10px">
                                    <b>Late Fee</b>
                                </td>
                                <td align="left" style="width: 120px; padding-left: 15px">
                                    <b>Exam Centre</b>
                                </td>
                                <td align="left" style="width: 50px; padding-left: 10px">
                                    <b>Jone</b>
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
                                    <asp:Label ID="lblFeeFilled" runat="server" Text='<%#Eval("FeeFilled")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblExamCentreFilled" runat="server" Text='<%#Eval("ExamCentreFilled")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("SCCode")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 180px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 210px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:TextBox ID="tbExamFee" runat="server" Text='<%#Eval("ExamFee")%>' Width="50px"></asp:TextBox>
                                    <asp:Label ID="lblExamFee" runat="server" Text='<%#Eval("ExamFee")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:TextBox ID="tbLateExamFee" runat="server" Text='<%#Eval("LateExamFee")%>' Width="50px"></asp:TextBox>
                                    <asp:Label ID="lblLateExamFee" runat="server" Text='<%#Eval("LateExamFee")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="center" style="width: 150px">
                                    <asp:DropDownList ID="ddlistExamCentre" runat="server">
                                        <asp:ListItem>NONE</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:DropDownList ID="ddlistJone" runat="server">
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>B</asp:ListItem>
                                        <asp:ListItem>C</asp:ListItem>
                                        <asp:ListItem>D</asp:ListItem>
                                        <asp:ListItem>E</asp:ListItem>
                                        <asp:ListItem>F</asp:ListItem>
                                        <asp:ListItem>G</asp:ListItem>
                                        <asp:ListItem>H</asp:ListItem>
                                        <asp:ListItem>I</asp:ListItem>
                                        <asp:ListItem>J</asp:ListItem>
                                        <asp:ListItem>K</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 10px">
                <asp:Panel ID="pnlPaging" runat="server">
                    <table align="center" width="1000px">
                        <tr>
                            <td>
                                <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                            </td>
                            <td align="left" style="padding-left: 200px">
                                <asp:LinkButton ID="lnkbtnPrevious" runat="server" Text="< Previous" ForeColor="Blue"
                                    Font-Bold="true" Visible="false" OnClick="lnkbtnPrevious_Click"></asp:LinkButton>
                                &nbsp;&nbsp;
                                <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnPageing" runat="server" ForeColor="Blue" Text='<%#Eval("PageNo") %>'
                                            CommandArgument='<%#Eval("PageNo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lnkbtnNext" runat="server" Text="Next >" ForeColor="Blue" Font-Bold="true"
                                    Visible="false" OnClick="lnkbtnNext_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div align="center" style="padding: 20px">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    Visible="false" />
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
    <div align="center" style="padding: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="75px" OnClick="btnOK_Click" />
    </div>
</asp:Content>
