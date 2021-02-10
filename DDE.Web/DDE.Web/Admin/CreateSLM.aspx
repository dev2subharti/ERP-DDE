<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="CreateSLM.aspx.cs" Inherits="DDE.Web.Admin.CreateSLM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="smslm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnlslm" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div align="center" class="heading">
                    Create SLM (On Trial)
                </div>
                <div align="center" style="padding-top: 20px">
                    <table class="tableStyle2" width="550px" cellspacing="20px">
                        <tbody align="left">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlSLMDetails" runat="server" GroupingText="SLM Details">
                                        <div>
                                            <table class="text" cellspacing="10px">
                                                <tr>
                                                    <td valign="top" style="width: 100px">
                                                        SLM Code *
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:TextBox ID="tbSLMCode" runat="server"></asp:TextBox><asp:Label ID="lblSLMCode"
                                                            runat="server" Text="" Visible="false"></asp:Label><br />
                                                        <asp:RequiredFieldValidator ID="rfvCC" runat="server" ControlToValidate="tbSLMCode"
                                                            ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:RadioButtonList ID="rblLang" AutoPostBack="true" RepeatDirection="Horizontal"
                                                            runat="server" >
                                                            <asp:ListItem Selected="True" Value="E">English</asp:ListItem>
                                                            <asp:ListItem Value="H">Hindi</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        Title *
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbTitle" runat="server" Width="340px" Enabled="true" TextMode="MultiLine"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvCFN" runat="server" ControlToValidate="tbTitle"
                                                            ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        Cost (Rs.)*
                                                    </td>
                                                    <td style="width: 150px">
                                                        <asp:TextBox ID="tbCost" runat="server"></asp:TextBox><br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbCost"
                                                            ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Panel ID="pnlCourseDetails" runat="server" GroupingText="Attached Courses">
                                        <div style="padding: 10px">
                                            <asp:DropDownList ID="ddlistCourse" Width="200px" runat="server">
                                                <asp:ListItem Value="0">--SELECT COURSE--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlistYear" runat="server">
                                                <asp:ListItem Value="0">--SELECT YEAR--</asp:ListItem>
                                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnAdd" Width="60px" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                        </div>
                                        <div>
                                            <div style="padding: 10px">
                                                <asp:DataList ID="dtlistCourse" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                                    ItemStyle-CssClass="dtlistItem" OnDeleteCommand="dtlistCourse_DeleteCommand">
                                                    <HeaderTemplate>
                                                        <table align="left">
                                                            <tr>
                                                                <td align="left" style="width: 50px">
                                                                    <b>S.No.</b>
                                                                </td>
                                                                <td align="left" style="width: 300px; padding-left: 10px">
                                                                    <b>Course Name</b>
                                                                </td>
                                                                <td align="left" style="width: 50px">
                                                                    <b>Year</b>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table align="left" style="font-family: Cambria; font-weight: normal; font-size: 14px">
                                                            <tr>
                                                                <td align="left" style="width: 50px">
                                                                    <%#Eval("SNo")%>
                                                                </td>
                                                                <td align="left" style="width: 300px">
                                                                    <asp:Label ID="lblCID" runat="server" Text='<%#Eval("CID")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblSLMLRID" runat="server" Text='<%#Eval("SLMLRID")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblCName" runat="server" Text='<%#Eval("CName")%>'></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 50px">
                                                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 30px">
                                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CausesValidation="false"
                                                                        ImageUrl="~/Admin/images/delete.png" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');" runat="server" CommandArgument='<%#Eval("SLMLRID")%>'
                                                                        Width="20px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div>
                    <table cellspacing="20px">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnSubmit" runat="server" Text="" OnClick="btnSubmit_Click" />
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
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnOK" runat="server" Text="OK" Width="50px" Visible="false" OnClick="btnOK_Click" />
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
            <%--<asp:PostBackTrigger ControlID="btnAdd" />--%>
            <asp:PostBackTrigger ControlID="btnOK" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
