<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowPaidFeeByENo.aspx.cs" Inherits="DDE.Web.Admin.ShowPaidFeeByENo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Show Total Fee Paid By Student
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>Enrollment No</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:Panel ID="pnlStudentDetails" runat="server" Visible="false">
                    <div>
                        <table cellpadding="0px" class="tableStyle2" cellspacing="0px">
                            <tr>
                                <td style="padding-left: 5px">
                                    <asp:Image ID="imgStudent" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                        runat="server" Width="100px" Height="120px" />
                                </td>
                                <td valign="top">
                                    <table cellspacing="10px">
                                        <tr>
                                            <td align="left">
                                                <b>Enrollment No</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbEnNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Student&#39;s Name</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbSName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Father&#39;s Name</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbFName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>S.C. Code</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbSCCode" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Course</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbCourse" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align="center" style="padding-top: 10px">
                        <asp:GridView ID="gvShowStudent" CssClass="gridview" HeaderStyle-CssClass="gvheader"
                            RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" runat="server" OnRowCommand="gvShowStudent_RowCommand"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="S.No." DataField="SNo" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Fee Head" DataField="FeeHead" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Exam" DataField="Exam" ItemStyle-Width="90px" />
                                <asp:BoundField HeaderText="Form No." DataField="FormNo" ItemStyle-Width="80px" />
                                <asp:BoundField HeaderText="Amount" DataField="Amount" ItemStyle-Width="100px" />
                                <asp:BoundField HeaderText="Payment Mode" DataField="Payment Mode" ItemStyle-Width="120px" />
                                <asp:BoundField HeaderText="D/C No." DataField="D/C No." ItemStyle-Width="140px" />
                                <asp:BoundField HeaderText="D/C Date" DataField="D/C Date" ItemStyle-Width="120px" />
                                <asp:BoundField HeaderText="Total D/C Amount" DataField="Total D/C Amount" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Submitted On" DataField="SubmittedOn" ItemStyle-Width="160px" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" Width="40px" CommandName='<%#Eval("Discription_E")%>'
                                                CommandArgument='<%#Eval("FRID")%>' CausesValidation="false" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" Width="50px" CommandName='<%#Eval("Discription_D")%>'
                                                CommandArgument='<%#Eval("FRID")%>' CausesValidation="false" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:Panel>
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
        </asp:Panel>
    </div>
    <div style="padding-top: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
    </div>
</asp:Content>
