<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="CustomiseExamReport.aspx.cs" Inherits="DDE.Web.Admin.CustomiseExamReport" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div class="heading">
            Show Form Feeding Record
        </div>
        <div style="padding: 10px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td>
                        Examination
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistExam" AutoPostBack="true" Width="150px" runat="server"
                            OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                             <asp:ListItem Value="G10">MARCH 2021</asp:ListItem>
                            <asp:ListItem Value="Z11">DECEMBER 2020</asp:ListItem>
                            <asp:ListItem Value="W11">JUNE 2020</asp:ListItem>
                             <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>                   
                            <asp:ListItem Value="W10">JUNE 2019</asp:ListItem>
                            <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                            <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                            <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                            <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                            <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                            <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                            <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                            <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        SC Code
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistSCCode" AutoPostBack="true" Width="150px" runat="server"
                            OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px; padding-bottom: 10px; padding-left: 50px">
            <table width="800px">
                <tr>
                    <td>
                        <asp:Label ID="lblAd" runat="server" Text="" Visible="false" CssClass="ad"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExam" runat="server" Text="" Visible="false" CssClass="ex"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblRem" runat="server" Text="" Visible="false" CssClass="re"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px">
            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" Visible="false"
                OnClick="btnExport_Click" />
        </div>
        <div style="padding: 10px">
            <asp:GridView ID="gvShowStudent"  HeaderStyle-CssClass="gvheader"
                RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" AutoGenerateColumns="false"
                runat="server" onrowcommand="gvShowStudent_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="S.No." DataField="SNo" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                   
                    <asp:BoundField HeaderText="Enrollment No" DataField="EnrollmentNo" ItemStyle-Width="120px" />
                    <asp:BoundField HeaderText="Roll No" DataField="RollNo" ItemStyle-Width="18px" />
                    <asp:BoundField HeaderText="Student Name" DataField="StudentName" ItemStyle-Width="150px"
                        ItemStyle-HorizontalAlign="left" />
                    <asp:BoundField HeaderText="Father Name" DataField="FatherName" ItemStyle-Width="150px" />
                    <asp:BoundField HeaderText="SC Code" DataField="SCCode" ItemStyle-Width="70px" />
                    <asp:BoundField HeaderText="Batch" DataField="Batch" ItemStyle-Width="80px" />
                    <asp:BoundField HeaderText="AT" DataField="AT" ItemStyle-Width="50px" />
                    <asp:BoundField HeaderText="Course" DataField="Course" ItemStyle-Width="200px" />
                  
                    <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField HeaderText="EF Filled" DataField="ExFormFeeded" ItemStyle-Width="80px" />
                    <asp:TemplateField ControlStyle-Width="60px">
                        <ItemTemplate>
                            <div align="center">
                                <asp:LinkButton ID="lnkbtnFillEF" runat="server" Text="Fill EF" OnClientClick="aspnetForm.target ='_blank';" Width="40px" CommandName="FillEF"
                                    CausesValidation="false" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                 <asp:Label ID="lblCID" runat="server" Text='<%#Eval("CID")%>' Visible="false"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ControlStyle-Width="70px">
                        <ItemTemplate>
                            <div align="center">
                                <asp:LinkButton ID="lnkbtnShowAC" runat="server" Text="Show AC" OnClientClick="aspnetForm.target ='_blank';" Width="40px" CommandName="ShowAC"
                                    CausesValidation="false" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                                                          
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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
