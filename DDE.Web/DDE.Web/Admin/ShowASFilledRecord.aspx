<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowASFilledRecord.aspx.cs" Inherits="DDE.Web.Admin.ShowASFilledRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 20px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Award Sheet Upload Report
            </div>
            <div style="padding-bottom: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            Examination
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" runat="server">
                                <asp:ListItem Value="A14">JUNE 2014</asp:ListItem>
                                <asp:ListItem Value="B14">DECEMBER 2014</asp:ListItem>
                                <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                <asp:ListItem Value="A18" >JUNE 2018</asp:ListItem>
                                <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                                <asp:ListItem Value="W10" Selected="True">JUNE 2019</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistType_SelectedIndexChanged">
                                <asp:ListItem Value="1">UPLOADED</asp:ListItem>
                                <asp:ListItem Value="0">NOT UPLOADED</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            Examiner
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExaminer" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlistExaminer_SelectedIndexChanged" >
                                
                            </asp:DropDownList>
                        </td>
                      
                    </tr>
                </table>
            </div>
            <div  style="padding-bottom: 10px">
            
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                       
            </div>
            <div class="text">
                <asp:Label ID="lblTS" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblTU" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
            </div>
            <div align="center" style="padding-top: 10px">
                <asp:DataList ID="dtlistASReport" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>AS No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="center" style="width: 400px">
                                    <b>Subject Name</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Total Students</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Total Uploaded</b>
                                </td>
                                 <td align="left" style="width: 200px">
                                    <b>Examiner</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("ASNo")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("PaperCode")%>
                                </td>
                                <td align="left" style="width: 390px">
                                    <%#Eval("SubjectName")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("TotalStudents")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <%#Eval("TotalUploaded")%>
                                </td>
                                 <td align="left" style="width: 200px">
                                    <%#Eval("Examiner")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
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
