<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="CreateQuestion.aspx.cs" Inherits="DDE.Web.Admin.CreateQuestion" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
      <div align="center" class="heading" >
                Create Question
            </div>
        <div align="center" style="padding-top: 20px">
            <table class="tableStyle2" width="600px" cellspacing="20px">
                <tbody align="left">      
                     <tr>
                        <td >
                           Paper Code *
                        </td>
                        <td >
                            <asp:TextBox ID="tbPaperCode"  runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  ControlToValidate="tbPaperCode"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   
                    <tr>
                        <td>
                            Question *
                        </td>
                        <td>
                            <asp:TextBox ID="tbQuestion" runat="server" Width="400px" Wrap="true" Height="100px" TextMode="MultiLine"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvCFN" runat="server" ControlToValidate="tbQuestion"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                  
                    <tr>
                        <td >
                           A*
                        </td>
                        <td >
                            <asp:TextBox ID="tbA" Width="400px" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvPC" runat="server" ControlToValidate="tbA"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            B *
                        </td>
                        <td >
                            <asp:TextBox ID="tbB" Width="400px" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbB"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     
                   
                    <tr>
                        <td >
                            C *
                        </td>
                        <td >
                            <asp:TextBox ID="tbC" Width="400px"  runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="tbC"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                  <tr>
                        <td >
                            D *
                        </td>
                        <td >
                            <asp:TextBox ID="tbD" Width="400px" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="tbD"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td >
                            Ans *
                        </td>
                        <td >
                            <asp:TextBox ID="tbAns" Width="50px" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ControlToValidate="tbAns"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div >
            <table cellspacing="20px">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
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
        <div align="center" style="padding-top:10px">
            <asp:Button ID="btnOK" runat="server" Text="OK" Width="50px" Visible="false" 
                onclick="btnOK_Click" />
        </div>
    </asp:Panel>
</asp:Content>