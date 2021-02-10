<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="FindMarkSheetByENo.aspx.cs" Inherits="DDE.Web.Admin.FindMarkSheetByENo" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" style="padding-bottom: 100px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Find Mark Sheet
            </div>
            <div style="padding-bottom: 50px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true" 
                                onselectedindexchanged="ddlistExam_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
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
                    <tr>
                      <td align="left">
                            <b>Syllabus Session</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSySession" runat="server" Width="150px">
                               <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem >A 2009-10</asp:ListItem>
                                <asp:ListItem Selected="True" >A 2010-11</asp:ListItem>
                                 <asp:ListItem >A 2013-14</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td align="left">
                            <asp:Label ID="lblY" runat="server" Text="Year" Visible="false"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSYear" runat="server" Width="150px" Visible="false">
                               <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
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
                            <asp:DropDownList ID="ddlistYear" runat="server" 
                                Visible="false" >
                               <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                  
                   
                </table>
            </div>
            <div align="center" style="padding-top:10px">
            <asp:Button ID="btnFind2" runat="server" Text="Find" Visible="false" 
                                Width="80px" onclick="btnFind2_Click" />
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
        <div style="padding-top:10px">
            <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" 
                onclick="btnOK_Click" />
        
        </div>
    </asp:Panel>
</asp:Content>
