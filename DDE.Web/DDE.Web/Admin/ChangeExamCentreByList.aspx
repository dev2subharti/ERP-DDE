<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ChangeExamCentreByList.aspx.cs" Inherits="DDE.Web.Admin.ChangeExamCentreByList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Change Exam Centre By List
            </div>
            <div align="center" class="text">
                <table class="tableStyle2" cellspacing="10px">
                    
                    <tr>
                    <td>
                           <b>Mode of Exam</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistMOE" runat="server">
                                <asp:ListItem Value="R">REGULAR</asp:ListItem>
                                <asp:ListItem Value="B" >BACKPAPER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        <td align="left">
                            <b>Examination</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        
                        <td align="left">
                            <b>SC Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                  
                </table>
            </div>
           
            
            <div style="padding-top: 10px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowRegistration" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                               
                                <td align="left" style="width: 135px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Current E. C.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Change E.C. To</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                </td>
                               
                                <td align="left" style="width: 135px">
                                    <asp:Label ID="lblENo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                    <asp:Label ID="lblSRID" runat="server" Visible="false" Text='<%#Eval("SRID")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Label ID="lblECCode" runat="server" Text='<%#Eval("ECCode")%>'></asp:Label>
                                </td>
                                <td align="center" style="width: 120px">
                                    <asp:TextBox ID="tbNewECCode" Width="100px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 20px">
                <asp:Button ID="btnUpdate" runat="server" Visible="false" Text="Update Record" OnClick="btnUpdate_Click" />
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
        <div style="padding-top: 20px">
            <asp:Button ID="btnOK" runat="server" Width="100px" Visible="false" Text="OK" OnClick="btnOK_Click" />
        </div>
    </asp:Panel>
</asp:Content>
