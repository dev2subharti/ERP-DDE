<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="UploadQuestionPapers.aspx.cs" Inherits="DDE.Web.Admin.UploadQuestionPapers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Upload Question Papers
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
                            <b>Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server">
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
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
                <asp:DataList ID="dtlistShowDS" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS" OnItemCommand="dtlistShowDS_ItemCommand">
                    <HeaderTemplate>
                        <table align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 100px">
                                    <b>Date</b>
                                </td>
                                <td align="center" style="width: 140px">
                                    <b>Time</b>
                                </td>
                                <td align="center" style="width: 120px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="center" style="width: 300px">
                                    <b>Title of Paper</b>
                                </td>
                                <td align="center" style="width: 260px">
                                    <b>Upload Question Paper</b>
                                </td>
                                <td align="center" style="width: 140px">
                                <b>Question Paper</b> 
                                </td>
                                <td align="center" style="width: 50px">
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 80px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowSubjects" ItemStyle-Wrap="true" runat="server">
                                        <ItemTemplate>
                                            <table align="left" cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="center"  class="border_rb" style="width: 140px">
                                                        <%#Eval("Time")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 100px">
                                                      <asp:Label ID="lblPaperCode" runat="server" Text='<%#Eval("PaperCode")%>'></asp:Label>  
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("SubjectName")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 250px">
                                                        <asp:FileUpload ID="fuQP"  runat="server" />
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 120px">      
                                                      <a href="<%#Eval("QPFileURL")%>"><%#Eval("QPFileName")%></a>                                                   
                                                        <asp:Label ID="lblDSID" runat="server" Text='<%#Eval("DSID")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblfuploaded" runat="server" Text='<%#Eval("fuploaded")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 50px">
                                                        <asp:LinkButton ID="lnkbtnDelete" OnClick="lnkbtnDelete_Click" runat="server" CommandName='<%#Eval("PaperCode")%>' CommandArgument='<%#Eval("DSID")%>'>Delete</asp:LinkButton>
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
            <div align="center" style="padding-top:20px">
                <asp:Button ID="btnUpload" runat="server" Visible="false" Text="Upload" 
                    onclick="btnUpload_Click" />
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
        <div align="center" style="padding-top:10px">

            <asp:Button ID="btnOK" runat="server" Visible="false" Text="OK" Width="50px" 
                onclick="btnOK_Click" />   
        </div>
    </asp:Panel>
</asp:Content>
