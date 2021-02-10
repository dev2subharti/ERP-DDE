<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowMarkSheetSCWise.aspx.cs" Inherits="DDE.Web.Admin.ShowMarkSheetSCWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" style="padding-bottom: 100px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Find Mark Sheet SC Code Wise
            </div>
            <div style="padding-bottom: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExam" runat="server" Width="150px">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="A13">JUNE 2013</asp:ListItem>
                                <asp:ListItem Value="B13">DECEMBER 2013</asp:ListItem>
                                <asp:ListItem Value="A14">JUNE 2014</asp:ListItem>
                                <asp:ListItem Value="B14">DECEMBER 2014</asp:ListItem>
                                <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                                <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                                <asp:ListItem Value="W10">JUNE 2019</asp:ListItem>
                                <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>
                                <asp:ListItem Value="W11">JUNE 2020</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>SC Code</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistSC" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistSC_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Mode of Exam</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistMOE" runat="server" Enabled="false" Width="150px">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                <asp:ListItem Value="R" Selected="True">REGULAR</asp:ListItem>
                                <%-- <asp:ListItem Value="B">BACK PAPER</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div >       
                <asp:Panel ID="pnlRange" runat="Server" Visible="False">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td align="left">
                                <b>From</b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbFrom" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td align="left">
                                <b>To</b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="tbTo" runat="server" Width="60px"></asp:TextBox>
                            </td>
                            <td>
                            <asp:Label ID="lblTotalMS" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    

                </asp:Panel>
            </div>
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Width="80px" OnClick="btnFind_Click" />
            </div>
            <div align="right" style="padding-top: 10px; width: 910px">
                <asp:Panel ID="pnlTaskbar" runat="server" Visible="false">
                    <div align="center">
                        <table cellspacing="10px" width="100%" class="tableStyle2">
                            <tbody align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPending" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOK" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPrinted" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPendingPrint" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div align="center" style="padding-top: 10px">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td>
                                    Select
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistSelect" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlistSelect_SelectedIndexChanged">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem>TOTAL MS</asp:ListItem>
                                        <asp:ListItem>PENDING MS</asp:ListItem>
                                        <asp:ListItem>OK MS</asp:ListItem>
                                        <asp:ListItem>PRINTED</asp:ListItem>
                                        <asp:ListItem>PENDING PRINT</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnPublishList" runat="server" Text="Publist Selected Student List"
                                        OnClick="btnPublishList_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align="center" style="padding-top: 10px">
                        <asp:Button ID="btnPublish1" runat="server" CssClass="btn" Text="Publish Selected Mark Sheet"
                            OnClick="btnPublish1_Click" />
                    </div>
                </asp:Panel>
            </div>
            <div style="padding-top: 10px">
                <asp:DataList ID="dtlistShowStudents" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 145px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 70px">
                                    <b>Year</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Status</b>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <b>MS SNo.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Printed</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <%#Eval("SNo")%>
                                    <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 135px">
                                    <%#Eval("EnrollmentNo")%>
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("StudentName")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("FatherName")%>
                                </td>
                                <td align="left" style="width: 70px">
                                    <%#Eval("Year")%>
                                </td>
                                <td align="left" style="width: 100px">
                                   
                                    <asp:Label ID="lblMSStatus" runat="server" Text='<%#Eval("MSStatus")%>'></asp:Label>
                                    <asp:Label ID="lblMName" runat="server" Text='<%#Eval("MotherName")%>' Visible="false"></asp:Label>
                                     <asp:Label ID="lblEC" runat="server" Text='<%#Eval("VECID")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblM" runat="server" Text="" ForeColor="Black" BackColor="Yellow"
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblGracedStudent" runat="server" Text="G" Width="20px" ForeColor="White"
                                        BackColor="Red" Visible="false"></asp:Label>
                                </td>
                                 <td align="left" style="width: 100px">
                                    <%#Eval("MSCounter")%>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:Label ID="lblPrinted" runat="server" Height="20px" Width="50px" Text='<%#Eval("Printed")%>'></asp:Label>
                                </td>
                                <td align="center" style="width: 50px">
                                    <asp:Label ID="lblDetained" runat="server" Visible="false" Text='<%#Eval("Detained")%>'></asp:Label>
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div align="center" style="padding-top: 10px">
                <asp:Button ID="btnPublish" runat="server" Text="Publish Selected Mark Sheet" CssClass="btn"
                    Visible="false" OnClick="btnPublish_Click" />
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
        <%-- <div style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
        </div>--%>
    </asp:Panel>
</asp:Content>
