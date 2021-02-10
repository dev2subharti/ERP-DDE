<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowCompleteExamReport.aspx.cs" Inherits="DDE.Web.Admin.ShowCompleteExamReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Complete Examination Report
            </div>
            <div style="padding-bottom: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tbody align="left">
                        <tr>
                            <td>
                                For
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistFor" runat="server" Width="140px">
                                    <asp:ListItem Value="1">DDE</asp:ListItem>
                                    <asp:ListItem Value="2">STUDY CENTRE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Examination
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExam" runat="server" Width="140px">
                                   
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Mode of Exam
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistMOE" runat="server" Width="140px">
                                    <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                    <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Year
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistYear" runat="server" Width="140px">
                                    <asp:ListItem>ALL</asp:ListItem>
                                    <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                    <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                    <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                SC Code
                            </td>
                            <td colspan="2" align="left">
                                <asp:DropDownList ID="ddlistSCCode" runat="server">
                                    <asp:ListItem>ALL</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div>
                <asp:Button ID="btnFind" runat="server" Text="Find" Width="80px" OnClick="btnFind_Click" />
            </div>
            <div style="padding-top: 20px">
                <asp:GridView ID="gvShowReport" AutoGenerateColumns="false" HeaderStyle-CssClass="rpgvheader"
                    RowStyle-CssClass="rpgvrow" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="S.No." DataField="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Enrollment No." DataField="EnrollmentNo" HeaderStyle-Width="80px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Roll No." DataField="RollNo" HeaderStyle-Width="60px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Student Name" DataField="StudentName" HeaderStyle-Width="120px"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="gvlitem" />
                        <asp:BoundField HeaderText="SC Code" DataField="SCCode" HeaderStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Course" DataField="Course" HeaderStyle-Width="170px"
                            ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="gvlitem" />
                        <asp:BoundField HeaderText="AFP" DataField="AFP" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="EFP" DataField="EFP" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Eligible" DataField="Eligible" HeaderStyle-Width="80px"
                            ItemStyle-HorizontalAlign="Center" HtmlEncode="false" />
                        <asp:BoundField HeaderText="Mark Sheet Status" DataField="MSStatus" HeaderStyle-Width="80px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Remark" DataField="Remark" HeaderStyle-Width="300px"
                            ItemStyle-HorizontalAlign="left" HtmlEncode="false" ItemStyle-CssClass="gvlitem" />
                        <asp:BoundField HeaderText="Printed" DataField="Printed" HtmlEncode="false" HeaderStyle-Width="50px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Attendence" DataField="Attendence" HtmlEncode="false"
                            HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
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
