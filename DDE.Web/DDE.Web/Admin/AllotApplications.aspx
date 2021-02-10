<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="AllotApplications.aspx.cs" Inherits="DDE.Web.Admin.AllotApplications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading">
                <b>Allot Applications</b>
            </div>

            <div align="center" class="text" style="padding: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>

                        <td>
                            <asp:Label ID="lblBatch" runat="server" Text="Batch" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch"  AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="ddlistBatch_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>                               
                                <asp:ListItem>C 2021</asp:ListItem>
                                <asp:ListItem>Q 2020-21</asp:ListItem>
                                <asp:ListItem>A 2020-21</asp:ListItem>
                                <asp:ListItem>Q 2020</asp:ListItem>
                                <asp:ListItem>C 2020</asp:ListItem>
                                <asp:ListItem>Q 2019-20</asp:ListItem>
                                <asp:ListItem>A 2019-20</asp:ListItem>
                                <asp:ListItem>Q 2019</asp:ListItem>
                                <asp:ListItem>C 2019</asp:ListItem>



                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblSCCode" runat="server" Text="SC Code" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode"  AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                        <td>
                            <asp:Button ID="btnFind" runat="server" Visible="true" Text="Search" Style="height: 26px" Width="82px"
                                OnClick="btnFind_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width:800px" align="right">
                <asp:Button ID="btnSelectAll" runat="server" Visible="false" Width="120px" CssClass="btn" Text="Select All" OnClick="btnSelectAll_Click" />

            </div>
            <div class="text" align="center" style="padding-top: 10px">
                <asp:DataList ID="dtlistShowPending" runat="server" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" >
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
                                   <asp:Label ID="lblPSRID" runat="server" Text=' <%#Eval("PSRID")%>'></asp:Label>
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

                                <td align="left" style="width: 50px">
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                </td>

                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div style="padding-top: 20px">
            <asp:Panel ID="pnlAllot" runat="server" Visible="false">
                <table class="tableStyle1" cellspacing="10px">
                <tr>
                    <td>
                        <b>Allot To</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlistExaminers" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAllot" runat="server" Width="100px" Text="Allot" CssClass="btn" OnClick="btnAllot_Click" />
                    </td>

                </tr>

            </table>
            </asp:Panel>
            
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
