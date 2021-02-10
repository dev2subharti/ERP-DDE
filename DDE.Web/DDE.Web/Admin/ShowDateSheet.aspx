<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowDateSheet.aspx.cs" Inherits="DDE.Web.Admin.ShowDateSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show Date Sheet
        </div>
        <div>
            <asp:Panel ID="pnlrbl" runat="server" >
                <table cellspacing="10px" class="tableStyle1">
                    <tr>
                        <td valign="top" align="left">
                            <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="T">Theory</asp:ListItem>
                                <asp:ListItem Value="P">Practical</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div>
            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Examination</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExamination" runat="server">
                            </asp:DropDownList>
                        </td>
                         <td>
                            <b>MOE</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistMOE" runat="server">
                                <asp:ListItem Value="B">BACK PAPER</asp:ListItem>
                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlistYear_SelectedIndexChanged">
                                <asp:ListItem Value="1">1st Year</asp:ListItem>
                                <asp:ListItem Value="2">2nd Year</asp:ListItem>
                                <asp:ListItem Value="3">3rd Year</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowTheoryDS" Visible="false"  runat="server" CellPadding="0" 
                    CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS" onitemcommand="dtlistShowTheoryDS_ItemCommand">
                    <HeaderTemplate>
                        <table  align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 92px; border-right:1px solid black">
                                    <b>Date</b>
                                </td>
                                 <td align="center" style="width: 90px; border-right:1px solid black">
                                    <b>Day</b>
                                </td>
                                <td align="center" style="width: 170px; border-right:1px solid black">
                                    <b>Time</b>
                                </td>
                                <td align="center" style="width: 310px; border-right:1px solid black">
                                    <b>Subject Code</b>
                                </td>
                                <td align="center" style="width: 110px; border-right:1px solid black">
                                    <b>Paper Code</b>
                                </td>
                                <td align="center" style="width: 310px; border-right:1px solid black">
                                    <b>Title of Paper</b>
                                </td>
                                <td align="center" style="width: 50px">
                                   
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin:0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width:80px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                 <td align="center" class="border_lbr" style="width:80px">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Day")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowSubjects"  runat="server" 
                                        >
                                        <ItemTemplate>
                                            <table align="left"   cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="center" class="border_rb" style="width: 160px">
                                                        <%#Eval("Time")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("SubjectCode")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 100px">
                                                        <%#Eval("PaperCode")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("SubjectName")%>
                                                    </td>
                                                      <td align="center" class="border_rb" style="width: 50px">
                                                     <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" OnClick="Edit_DateSheet" CommandName='<%#Eval("PaperCode")%>' CommandArgument='<%#Eval("DSID") %>'></asp:LinkButton>
                                                      
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 50px">
                                                     <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" OnClick="Delete_DateSheetRow" CommandName='<%#Eval("PaperCode")%>'
                                                       OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                       CommandArgument='<%#Eval("DSID") %>'></asp:LinkButton>                                                      
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <asp:DataList ID="dtlistShowPracDS" Visible="false"   runat="server" CellPadding="0" 
                    CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS" onitemcommand="dtlistShowPracDS_ItemCommand">
                    <HeaderTemplate>
                        <table  align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 92px; border-right:1px solid black">
                                    <b>Date</b>
                                </td>
                                 <td align="center" style="width: 90px; border-right:1px solid black">
                                    <b>Day</b>
                                </td>
                                <td align="center" style="width: 170px; border-right:1px solid black">
                                    <b>Time</b>
                                </td>
                                <td align="center" style="width: 110px; border-right:1px solid black">
                                    <b>Practical Code</b>
                                </td>                               
                                <td align="center" style="width: 310px; border-right:1px solid black">
                                    <b>Title of Practical</b>
                                </td>
                                <td align="center" style="width: 50px">
                                   
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin:0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width:80px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                 <td align="center" class="border_lbr" style="width:80px">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Day")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowPracticals"  runat="server" 
                                        >
                                        <ItemTemplate>
                                            <table align="left"   cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="center" class="border_rb" style="width: 160px">
                                                        <%#Eval("Time")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 100px">
                                                        <%#Eval("PracticalCode")%>
                                                    </td>
                                                   
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("PracticalName")%>
                                                    </td>
                                                      <td align="center" class="border_rb" style="width: 50px">
                                                     <asp:LinkButton ID="lnkbtnEdit" runat="server" Text="Edit" OnClick="Edit_DateSheet" CommandName='<%#Eval("PracticalCode")%>' CommandArgument='<%#Eval("DSID") %>'></asp:LinkButton>
                                                      
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 50px">
                                                     <asp:LinkButton ID="lnkbtnDelete" runat="server" Text="Delete" OnClick="Delete_DateSheetRow" CommandName='<%#Eval("PracticalCode")%>'
                                                        OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                        CommandArgument='<%#Eval("DSID") %>'></asp:LinkButton>                                                      
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top:10px">
                    <asp:Button ID="btnPublish" runat="server" Text="Publish" Visible="false"
                    onclick="btnPublish_Click" />
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
