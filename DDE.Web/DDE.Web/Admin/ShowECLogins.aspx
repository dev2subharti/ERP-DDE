<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true" CodeBehind="ShowECLogins.aspx.cs" Inherits="DDE.Web.Admin.ShowECLogins" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div style="padding: 0px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                List of Examination Centres
            </div>
            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Examination</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExamination" runat="server">
                            <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                            <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                            <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                            <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
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
                <asp:DataList ItemStyle-Wrap="true" ID="dtlistShowExamCentres" CssClass="dtlist"
                    runat="server" HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem"
                   >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 60px">
                                    <b>S.No.</b>
                                </td>                             
                                <td align="left" style="width: 300px">
                                    <b>Centre Name</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>City</b>
                                </td>
                                 <td align="left" style="width: 300px">
                                    <b>Email ID</b>
                                </td>
                                   <td align="left" style="width: 100px">
                                    <b>ECCode</b>
                                </td>
                                 <td align="left" style="width: 150px">
                                    <b>Password</b>
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
                                <td align="left" style="width: 300px">
                                    <%#Eval("CentreName")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <%#Eval("City")%>
                                </td>
                                
                                <td align="left" style="width: 300px">
                                  <%#Eval("Email")%> 
                               </td>
                                <td align="left" style="width: 100px">
                                   <b><%#Eval("ExamCentreCode")%></b> 
                                </td>
                                <td align="left" style="width: 150px">
                                  <b><%#Eval("Password")%></b>  
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
