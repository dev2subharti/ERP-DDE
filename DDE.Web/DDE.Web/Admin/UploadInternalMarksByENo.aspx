<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="UploadInternalMarksByENo.aspx.cs" Inherits="DDE.Web.Admin.UploadInternalMarksByENo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" style="padding-bottom: 100px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Upload Internal Marks
            </div>
            <div style="padding-bottom: 50px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
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
                                <asp:ListItem Value="B14">DECEMBER 2014</asp:ListItem>
                                <asp:ListItem Value="A14">JUNE 2014</asp:ListItem>
                                <asp:ListItem Value="B13">DECEMBER 2013</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Mode of Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistMOE" runat="server" Width="150px">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                    </tr>
                </table>
            </div>
            <div>
                <table class="tableStyle2" cellspacing="15px">
                    <tr>
                        <td>
                            <b>Enrollment No</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Find" OnClick="btnFind_Click" Width="75px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblYear" runat="server" Text="Year" Visible="false"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:DropDownList ID="ddlistYear" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>

                </table>
            </div>
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnFind2" runat="server" Text="Find" Visible="false"
                    Width="80px" OnClick="btnFind2_Click" />
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
        <div style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px"
                OnClick="btnOK_Click" />

        </div>
    </asp:Panel>
</asp:Content>
