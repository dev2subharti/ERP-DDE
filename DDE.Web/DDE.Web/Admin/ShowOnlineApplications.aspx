<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowOnlineApplications.aspx.cs" Inherits="DDE.Web.Admin.ShowOnlineApplications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Show Online Applications</b>
            </div>
             <div align="center" style="padding-bottom: 5px">
                    <table cellspacing="10px" class="tableStyle1">
                        <tr>
                            <td valign="top" align="left">
                                <asp:RadioButtonList ID="rblSearchType" AutoPostBack="true" runat="server" 
                                    onselectedindexchanged="rblSearchType_SelectedIndexChanged" >
                                    <asp:ListItem Selected="True" Value="1">By Pro. ANo.</asp:ListItem>
                                     <asp:ListItem Value="3">By Form Counter</asp:ListItem>
                                    <asp:ListItem Value="2">By List</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </div>
            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        
                        <td>
                             <asp:Label ID="lblBatch" runat="server" Text="Batch" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch" Visible="false" AutoPostBack="true" 
                                runat="server" onselectedindexchanged="ddlistBatch_SelectedIndexChanged">
                             <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                 <asp:ListItem>Q 2020</asp:ListItem>
                                <asp:ListItem>C 2020</asp:ListItem>
                                <asp:ListItem>Q 2019-20</asp:ListItem>
                                <asp:ListItem>A 2019-20</asp:ListItem>
                                 <asp:ListItem>Q 2019</asp:ListItem>
                              <asp:ListItem>Q 2018-19</asp:ListItem>
                               <asp:ListItem>C 2019</asp:ListItem>
                              <asp:ListItem>A 2018-19</asp:ListItem>
                              <asp:ListItem>C 2018</asp:ListItem>
                              <asp:ListItem>A 2017-18</asp:ListItem>
                              <asp:ListItem>C 2017</asp:ListItem>
                             <asp:ListItem>A 2016-17</asp:ListItem>
                             <asp:ListItem>C 2016</asp:ListItem>
                             <asp:ListItem>A 2015-16</asp:ListItem>
                            <asp:ListItem>C 2015</asp:ListItem>
                          
                            </asp:DropDownList>
                        </td>
                        <td>
                             <asp:Label ID="lblSCCode" runat="server" Text="SC Code" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" Visible="false" AutoPostBack="true" 
                                runat="server" onselectedindexchanged="ddlistSCCode_SelectedIndexChanged">
                            <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td>
                           <asp:Label ID="lblPANo" runat="server" Text="Pro. ANo." Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPANo" runat="server" Visible="true"></asp:TextBox>
                        </td>
                        <td>
                         <asp:Button ID="btnFind" runat="server" Visible="true" Text="Search" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            
          
            <div class="text" align="center" style="padding-top: 0px">
                <asp:DataList ID="dtlistShowPending" runat="server" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowPending_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                  <td align="left" style="width: 100px">
                                    <b>Pro. ANo.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 80px">
                                    <b>Batch</b>
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
                                <td align="left" style="width: 100px">
                                    <%#Eval("PSRID")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Course")%>
                                </td>
                                <td align="left" style="width: 80px">
                                    <%#Eval("Session")%>
                                </td>
                                
                                <td align="center" style="width: 120px">
                                    <asp:LinkButton ID="lnkbtnShowDetails" runat="server" Visible="false" Text="Show Details" CommandName="Show"
                                        CommandArgument='<%#Eval("PSRID") %>'></asp:LinkButton>
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
