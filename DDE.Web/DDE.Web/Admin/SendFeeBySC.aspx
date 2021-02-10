<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/StudyCentre.Master" AutoEventWireup="true"
    CodeBehind="SendFeeBySC.aspx.cs" Inherits="DDE.Web.Admin.SendFeeBySC" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <%-- <div align="center" class="heading" style="padding-bottom: 20px">
                <marquee>This Online Payment System is under testing presently !!  Please do not use this system untill University launch it officially.</marquee>
            </div>--%>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Send Fee of Students
            </div>
            <div>
                <asp:Panel ID="pnlFilters" runat="server">
                    <div align="center" class="text">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td align="center" colspan="4">
                                    <table cellspacing="10px" class="tableStyle1">
                                        <tr>
                                            <td valign="top" align="left">
                                                <asp:RadioButtonList ID="rblAdmissionType" runat="server">
                                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                                    <asp:ListItem Value="1">Regular</asp:ListItem>
                                                    <asp:ListItem Value="2">Credit Transfer</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td valign="top" align="left">
                                                <asp:RadioButtonList ID="rblPhoto" runat="server">
                                                    <asp:ListItem Selected="True">Without Photo</asp:ListItem>
                                                    <asp:ListItem>With Photo</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>Fee Head</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistFeeHead" runat="server" Width="150px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlistFeeHead_SelectedIndexChanged">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblExamination" runat="server" Text="Examination" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistExamination" runat="server" Width="150px" Visible="false">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>Batch</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem>ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <b>SC Code</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistSCCode" runat="server" Width="150px">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem>ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <b>Course</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem>ALL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <b>Year</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistYear" runat="server" Width="150px">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem Value="0">ALL</asp:ListItem>
                                        <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                        <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                        <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                        <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding-top: 10px">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Style="height: 26px" Width="82px"
                            OnClick="btnSearch_Click" />
                    </div>
                </asp:Panel>
            </div>
            <div class="text" align="center" style="padding-top: 20px">
                <asp:Panel ID="pnlStList" runat="server" Visible="false">
                    <asp:DataList ID="dtlistShowStudents" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                        ItemStyle-CssClass="dtlistItem" >
                        <HeaderTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 50px">
                                        <b>S.No.</b>
                                    </td>
                                    <td align="center" style="width: 140px">
                                        <b>Photo</b>
                                    </td>
                                    <td align="left" style="width: 135px">
                                        <b>Enrollment No.</b>
                                    </td>
                                    <td align="left" style="width: 150px">
                                        <b>Student Name</b>
                                    </td>
                                    <td align="left" style="width: 150px">
                                        <b>Father Name</b>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <b>Req. Amt.</b>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <b>Paid Amt.</b>
                                    </td>
                                    <td align="left" style="width: 90px">
                                        <b>Due Amt.</b>
                                    </td>
                                    <td align="center" style="width: 100px">
                                        <b>Amount</b>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table align="left">
                                <tr>
                                    <td align="left" style="width: 50px">
                                        <%#Eval("SNo")%>
                                        <asp:Label ID="lblSRID" runat="server" Text=' <%#Eval("SRID")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 130px">
                                        <asp:Image ID="imgPhoto" ImageUrl='<%#Eval("StudentPhoto")%>' runat="server" Width="100px"
                                            Height="100px" />
                                    </td>
                                    <td align="left" style="width: 135px">
                                        <%#Eval("EnrollmentNo")%>
                                    </td>
                                    <td align="left" style="width: 150px">
                                        <%#Eval("StudentName")%>
                                    </td>
                                    <td align="left" style="width: 150px">
                                        <%#Eval("FatherName")%>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <%#Eval("RequiredAmount")%>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <%#Eval("PaidAmount")%>
                                    </td>
                                    <td align="left" style="width: 100px">
                                        <asp:Label ID="lblDueAmount" runat="server" Text='<%#Eval("DueAmount")%>'></asp:Label>
                                    </td>
                                    <td align="center" style="width: 50px">
                                        <asp:TextBox ID="tbAmount" runat="server" Width="80px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </asp:Panel>
                <div>
                    
                    <asp:Panel ID="pnlChart" runat="server" Visible="false">
                        <asp:DataList ID="dtlistChart" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                            ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistChart_ItemCommand" >
                            <HeaderTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 50px">
                                            <b>S.No.</b>
                                        </td>
                                        <td align="center" style="width: 210px">
                                            <b>Fee Head</b>
                                        </td>
                                        <td align="right" style="width: 100px">
                                            <b>Amount</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 50px">
                                            <%#Eval("SNo")%>
                                            <asp:Label ID="lblFHID" runat="server" Text=' <%#Eval("FHID")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <%#Eval("FeeHead")%>
                                        </td>
                                        <td align="right" style="width: 100px">
                                            <%#Eval("Amount")%>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("FHID") %>'></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </asp:Panel>
                    <asp:Panel ID="pnlTotal" runat="server" Visible="false">
                        <table width="472px" class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td style="width: 300px" align="right">
                                    <b>Total :</b>
                                </td>
                                <td align="left">
                                    <asp:Label ID="lbltamount" Font-Bold="true" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div>
                    <table cellspacing="20px">
                        <tr>
                            
                            <td>
                                <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Visible="false"
                                    OnClick="btnConfirm_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Visible="false" Style="height: 26px"
                                    Width="82px" OnClick="btnSubmit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table class="tableStyle2">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
