<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddCourse.aspx.cs" Inherits="DDE.Web.Admin.AddCourse" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
      <div align="center" class="heading" >
                Create Course
            </div>
        <div align="center" style="padding-top: 20px">
            <table class="tableStyle2" width="600px" cellspacing="20px">
                <tbody align="left">      
                    <tr>
                        <td >
                            Course Code *
                        </td>
                        <td >
                            <asp:TextBox ID="tbCourseCode" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvCC" runat="server" ControlToValidate="tbCourseCode"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Course Short Name *
                        </td>
                        <td>
                            <asp:TextBox ID="tbCourseSN" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvCSN" runat="server" ControlToValidate="tbCourseSN"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Course Full Name *
                        </td>
                        <td>
                            <asp:TextBox ID="tbCourseFN" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvCFN" runat="server" ControlToValidate="tbCourseFN"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Specialization *
                        </td>
                        <td>
                            <asp:TextBox ID="tbSpecialization" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvSp" runat="server" ControlToValidate="tbSpecialization"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Programme Code *
                        </td>
                        <td >
                            <asp:TextBox ID="tbProgCode" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvPC" runat="server" ControlToValidate="tbProgCode"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Admission Fee *
                        </td>
                        <td >
                            <asp:TextBox ID="tbAdmissionFee" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbAdmissionFee"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     
                    <tr>
                        <td >
                            Stream Name *
                        </td>
                        <td >
                            <asp:DropDownList ID="ddlistStream" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="ddlistStream_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Stream Fee *
                        </td>
                        <td >
                            <asp:TextBox ID="tbStreamFee" Enabled="false" runat="server"></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="tbStreamFee"
                                        ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Course Duration *
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCourseDuration" runat="server" Width="124px">
                               
                                <asp:ListItem Value="1">1 Year</asp:ListItem>
                                <asp:ListItem Value="2">2 Year</asp:ListItem>
                                <asp:ListItem Value="3">3 Year</asp:ListItem>
                                <asp:ListItem Value="4">4 Year</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Course Max Duration *
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCourseMaxDuration" runat="server" Width="124px">
                               
                                <asp:ListItem Value="1">1 Year</asp:ListItem>
                                <asp:ListItem Value="2">2 Year</asp:ListItem>
                                <asp:ListItem Value="3">3 Year</asp:ListItem>
                                <asp:ListItem Value="4">4 Year</asp:ListItem>
                                <asp:ListItem Value="5">5 Year</asp:ListItem>
                                <asp:ListItem Value="6">6 Year</asp:ListItem>
                                <asp:ListItem Value="7">7 Year</asp:ListItem>
                                <asp:ListItem Value="8">8 Year</asp:ListItem>
                            </asp:DropDownList>
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




