<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowRefundSCWise.aspx.cs" Inherits="DDE.Web.Admin.ShowRefundSCWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="scm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlData" runat="server" Visible="false">
                <div>
                    <div align="center" class="heading" style="padding-bottom: 20px">
                        Reimbursement to Study Centre
                    </div>
                    <div align="center" class="text">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td align="left">
                                    <b>Batch</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                        <asp:ListItem>C 2014</asp:ListItem>
                                        <asp:ListItem>A 2014-15</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <b>Study Centre</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistSC" runat="server" Width="150px">
                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding-top: 10px">
                        <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                            OnClick="btnFind_Click" />
                    </div>
                    <div>
                        <asp:Panel ID="pnlRefundList" runat="server" Visible="false">
                            <div class="text" align="center" style="padding-top: 30px">
                                <asp:DataList ID="dtlistDirectSC" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                    ItemStyle-CssClass="dtlistItem">
                                    <HeaderTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 50px">
                                                    <b>S.No.</b>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    <b>Enrollment No.</b>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    <b>Student Name</b>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    <b>Father Name</b>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <b>Course</b>
                                                </td>
                                                <%--<td align="left" style="width: 80px">
                                            <b>CYear</b>
                                        </td>--%>
                                                <td align="left" style="width: 40px">
                                                    <b>Year</b>
                                                </td>
                                                <td align="right" style="width: 70px">
                                                    <b>R. Fee</b>
                                                </td>
                                                <td align="right" style="width: 70px">
                                                    <b>P. Fee</b>
                                                </td>
                                                <td align="right" style="width: 70px">
                                                    <b>Fee(%)</b>
                                                </td>
                                                <td align="center" style="width: 310px">
                                                    <b>Fee Details</b>
                                                </td>
                                                <td align="right" style="width: 100px">
                                                    <b>Refund</b>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table align="left">
                                            <tr>
                                                <td align="left" style="width: 40px">
                                                    <%#Eval("SNo")%>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    <%#Eval("EnrollmentNo")%>
                                                    <asp:Label ID="lblSRID" runat="server" Visible="false" Text='<%#Eval("SRID")%>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    <%#Eval("SName")%>
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    <%#Eval("FName")%>
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <%#Eval("Course")%>
                                                </td>
                                                <%-- <td align="left" style="width: 80px">
                                            <%#Eval("CYear")%>
                                        </td>--%>
                                                <td align="left" style="width: 40px">
                                                    <%#Eval("FPYear")%>
                                                </td>
                                                <td align="right" style="width: 70px">
                                                    <%#Eval("RFee")%>
                                                </td>
                                                <td align="right" style="width: 70px">
                                                    <%#Eval("PFee")%>
                                                </td>
                                                <td align="right" style="width: 70px">
                                                    <asp:Label ID="lblFeePer" runat="server" Text='<%#Eval("FeePer")%>'></asp:Label>
                                                </td>
                                                <td align="left" style="width: 300px; padding-left: 10px">
                                                    <%#Eval("InsDetails")%>
                                                </td>
                                                <td align="right" style="width: 100px">
                                                    <asp:Label ID="lblRefund" runat="server" Text='<%#Eval("Refund")%>'></asp:Label>
                                                </td>
                                                <td align="right" style="width: 30px">
                                                    <asp:CheckBox ID="cbRefund" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div align="right" class="tableStyle2" style="width: 1298px; padding-top: 5px; padding-bottom: 5px;
                                padding-right: 30px">
                                <asp:Label ID="lblTotalRefund" runat="server" Text=""></asp:Label>
                            </div>
                            <div align="right" class="tableStyle2" style="width: 1298px; padding-top: 5px; padding-bottom: 5px;
                                padding-right: 30px">
                                
                                <asp:LinkButton ID="lnkDRefund" runat="server" OnClick="lnkDRefund_Click">Generate DFR Instrument</asp:LinkButton>
                                <asp:Label ID="lblDRAmount" runat="server" Text="Deduct Amount" Visible="false"></asp:Label>
                                <asp:TextBox ID="tbDRAmount" runat="server" Width="60px" Visible="false"></asp:TextBox><br />
                            </div>
                            <div align="right" class="tableStyle2" style="width: 1298px; padding-top: 5px; padding-bottom: 5px;
                                padding-right: 30px">
                                <asp:Label ID="lblFinalAmount" BorderColor="#003f6f" BorderStyle="Solid" BorderWidth="2px"
                                    runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                            <div align="center" style="padding-top:10px">
                                    <asp:Panel ID="pnlUP" runat="server" Visible="false">
                                        <table class="tableStyle2">
                                            <tr>
                                                <td>
                                                    User Name
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    Password
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbPassword" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                                        onclick="btnSubmit_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="5">
                                                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            <div align="center" style="padding-top: 20px">
                                <asp:Button ID="btnPublish" runat="server" Text="Publish Letter" Visible="false"
                                    OnClick="btnPublish_Click" />
                            </div>
                        </asp:Panel>
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
        </ContentTemplate>
         <Triggers>
      
            <asp:PostBackTrigger ControlID="btnFind" />
            <asp:PostBackTrigger ControlID="btnPublish" />
            <asp:PostBackTrigger ControlID="lnkDRefund" />
      
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
