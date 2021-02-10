<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="AutoFillExamForm.aspx.cs" Inherits="DDE.Web.Admin.AutoFillExamForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
               Auto Fill Exam Fee
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>Instrument No.</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbDCNo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:DataList ID="dtlistTotalInstruments" Visible="false" CssClass="dtlist" runat="server"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistTotalInstruments_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 150px; padding-left: 10px">
                                    <b>Type</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>No.</b>
                                </td>
                                <td align="left" style="width: 100px">
                                    <b>Date</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Total Amount</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Bank Name</b>
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
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblNo" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 100px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 140px">
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 150px">
                                    <asp:Label ID="lblIBN" runat="server" Text='<%#Eval("IBN")%>'></asp:Label>
                                </td>
                                <td align="center" style="width: 100px">
                                    <asp:LinkButton ID="lnkbtnShow" runat="server" Text="Show Details" CommandName='<%#Eval("Status") %>'
                                        CommandArgument='<%#Eval("IID") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div>
                <asp:Panel ID="pnlDCDetail" runat="server" Visible="false">
                    <div class="data" style="padding-top: 0px" align="center">
                        <table class="tableStyle2" cellpadding="0px" cellspacing="0px">
                            <tr>
                                <td>
                                    <table width="388px" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td valign="top">
                                                <table cellspacing="10px">
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT TYPE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDType" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                         <asp:Label ID="lblIT" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT No.</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                            <asp:Label ID="lblIID" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>INSTRUMENT DATE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbDCDate" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="left">
                                                            <b>ISSUING BANK NAME</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbIBN" runat="server" TextMode="MultiLine" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>TOTAL AMOUNT</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbTotalAmount" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>SC CODE</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="tbSCCode" runat="server" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            <b>For Exam</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlistExam" runat="server">
                                                                <asp:ListItem Value="Z11">DECEMBER 2020</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>Exam Fee</b>
                                                        </td>
                                                        <td align="left">
                                                           <asp:TextBox ID="tbExamFee" runat="server"  ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    <div align="center">
                        <table cellspacing="10px">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAutoFillExamForm" CssClass="btn" Width="200px" runat="server" Text="Auto Fill Exam Fee" OnClick="btnAutoFillExamForm_Click"   />
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                </asp:Panel>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
            <table class="tableStyle2">
                <tr>
                    <td align="center" style="padding: 30px">
                        <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div style="padding-top: 20px">
        <asp:Button ID="btnOK" runat="server" Text="" Visible="false" OnClick="btnOK_Click" />
    </div>
</asp:Content>