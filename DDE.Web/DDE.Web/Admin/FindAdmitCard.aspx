<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="FindAdmitCard.aspx.cs" Inherits="DDE.Web.Admin.FindAdmitCard" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading" style="padding-bottom: 20px">
            Find Admit Card / Verification Sheet
        </div>
        <div>
            <table cellspacing="10px" class="tableStyle2">
                <tr>
                    <td align="left">
                        <b>Card</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistCard" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistCard_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                            <asp:ListItem>ADMIT CARD</asp:ListItem>
                            <asp:ListItem>VERIFICATION SEAT</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <b>Card Type</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistCardType" runat="server" Width="150px" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlistCardType_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                            <asp:ListItem Value="R">REGULAR</asp:ListItem>
                            <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>Mode</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistMode" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistMode_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                            <asp:ListItem>BY ENROLLMENT</asp:ListItem>
                            <asp:ListItem>BY COURSE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <b>Exam</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistExam" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistExam_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>Syllabus Session</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistSySession" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistSySession_SelectedIndexChanged">
                           <asp:ListItem>--Select One--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <b>Course</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistCourse_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>Batch</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistSession" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistSession_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <b>Study Centre Code</b>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistSCCode" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblExamCity" Visible="false" runat="server" Text="Exam City"></asp:Label>
                       
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistExamCentre" Visible="false" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistExamCentre_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                     <asp:Label ID="lblECCode" runat="server" Visible="false" Text="Exam Centre Code"></asp:Label>
                        
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlistJone" Visible="false" runat="server" Width="150px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlistJone_SelectedIndexChanged">
                            <asp:ListItem>--Select One--</asp:ListItem>                         
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 50px">
            <asp:Panel ID="pnlFind" runat="server" Visible="false">
                <table cellspacing="10px">
                    <tr>
                        <td class="text">
                            <asp:Label ID="lblENo" runat="server" Text="Enrollment No" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Button ID="btnPublish" runat="server" Text="Publish" Width="82px" OnClick="btnPublish_Click" />
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
</asp:Content>
