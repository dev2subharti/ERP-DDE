<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="EnterNewSLMStock.aspx.cs" Inherits="DDE.Web.Admin.EnterNewSLMStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
     <asp:ScriptManager ID="smslm" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upnl" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Enter New SLM Stock with Bill Details (On Trial)
        </div>
        <div style="padding-top: 20px">
            <table class="tableStyle2" cellpadding="0px" cellspacing="10px">
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnlBillDetails" runat="server" GroupingText="Bill Details">
                            <table cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td valign="top">
                                        <table cellspacing="10px">
                                            <tr>
                                                <td valign="top" align="left">
                                                    <b>BILL NO. *</b>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:TextBox ID="tbBillNo" runat="server" ForeColor="Black"></asp:TextBox><br />
                                                    <asp:RequiredFieldValidator ID="rfvBillNo" runat="server" ControlToValidate="tbBillNo"
                                                        ErrorMessage="Please fill Bill No."></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="top" align="left">
                                                    <b>BILL DATE *</b>
                                                </td>
                                                <td valign="top" align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistBDay" runat="server">
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistBMonth" runat="server">
                                                                    <asp:ListItem Value="01">JANUARY</asp:ListItem>
                                                                    <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                                                                    <asp:ListItem Value="03">MARCH</asp:ListItem>
                                                                    <asp:ListItem Value="04">APRIL</asp:ListItem>
                                                                    <asp:ListItem Value="05">MAY</asp:ListItem>
                                                                    <asp:ListItem Value="06">JUNE</asp:ListItem>
                                                                    <asp:ListItem Value="07">JULY</asp:ListItem>
                                                                    <asp:ListItem Value="08">AUGUST</asp:ListItem>
                                                                    <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                                                                    <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                                                                    <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                                                    <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistBYear" runat="server">
                                                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                                    <asp:ListItem>2019</asp:ListItem>
                                                                    <asp:ListItem>2018</asp:ListItem>
                                                                    <asp:ListItem>2017</asp:ListItem>
                                                                    <asp:ListItem>2016</asp:ListItem>
                                                                    <asp:ListItem>2015</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <b>ORDER NO. *</b>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:TextBox ID="tbONo" runat="server" ForeColor="Black"></asp:TextBox><br />
                                                    <asp:RequiredFieldValidator ID="rfvONo" runat="server" ControlToValidate="tbONo"
                                                        ErrorMessage="Please fill Order No."></asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="top" align="left">
                                                    <b>ORDER DATE *</b>
                                                </td>
                                                <td valign="top" align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistODay" runat="server">
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistOMonth" runat="server">
                                                                    <asp:ListItem Value="01">JANUARY</asp:ListItem>
                                                                    <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                                                                    <asp:ListItem Value="03">MARCH</asp:ListItem>
                                                                    <asp:ListItem Value="04">APRIL</asp:ListItem>
                                                                    <asp:ListItem Value="05">MAY</asp:ListItem>
                                                                    <asp:ListItem Value="06">JUNE</asp:ListItem>
                                                                    <asp:ListItem Value="07">JULY</asp:ListItem>
                                                                    <asp:ListItem Value="08">AUGUST</asp:ListItem>
                                                                    <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                                                                    <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                                                                    <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                                                    <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistOYear" runat="server">
                                                                <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                                    <asp:ListItem>2019</asp:ListItem>
                                                                    <asp:ListItem>2018</asp:ListItem>
                                                                    <asp:ListItem>2017</asp:ListItem>
                                                                    <asp:ListItem>2016</asp:ListItem>
                                                                    <asp:ListItem>2015</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" align="left">
                                                    <b>CHALLAN NO. </b>
                                                </td>
                                                <td valign="top" align="left">
                                                    <asp:TextBox ID="tbCNo" runat="server" ForeColor="Black"></asp:TextBox><br />
                                                </td>
                                                <td valign="top" align="left">
                                                    <b>CHALLAN DATE </b>
                                                </td>
                                                <td valign="top" align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistCDay" runat="server">
                                                                    <asp:ListItem>01</asp:ListItem>
                                                                    <asp:ListItem>02</asp:ListItem>
                                                                    <asp:ListItem>03</asp:ListItem>
                                                                    <asp:ListItem>04</asp:ListItem>
                                                                    <asp:ListItem>05</asp:ListItem>
                                                                    <asp:ListItem>06</asp:ListItem>
                                                                    <asp:ListItem>07</asp:ListItem>
                                                                    <asp:ListItem>08</asp:ListItem>
                                                                    <asp:ListItem>09</asp:ListItem>
                                                                    <asp:ListItem>10</asp:ListItem>
                                                                    <asp:ListItem>11</asp:ListItem>
                                                                    <asp:ListItem>12</asp:ListItem>
                                                                    <asp:ListItem>13</asp:ListItem>
                                                                    <asp:ListItem>14</asp:ListItem>
                                                                    <asp:ListItem>15</asp:ListItem>
                                                                    <asp:ListItem>16</asp:ListItem>
                                                                    <asp:ListItem>17</asp:ListItem>
                                                                    <asp:ListItem>18</asp:ListItem>
                                                                    <asp:ListItem>19</asp:ListItem>
                                                                    <asp:ListItem>20</asp:ListItem>
                                                                    <asp:ListItem>21</asp:ListItem>
                                                                    <asp:ListItem>22</asp:ListItem>
                                                                    <asp:ListItem>23</asp:ListItem>
                                                                    <asp:ListItem>24</asp:ListItem>
                                                                    <asp:ListItem>25</asp:ListItem>
                                                                    <asp:ListItem>26</asp:ListItem>
                                                                    <asp:ListItem>27</asp:ListItem>
                                                                    <asp:ListItem>28</asp:ListItem>
                                                                    <asp:ListItem>29</asp:ListItem>
                                                                    <asp:ListItem>30</asp:ListItem>
                                                                    <asp:ListItem>31</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistCMonth" runat="server">
                                                                    <asp:ListItem Value="01">JANUARY</asp:ListItem>
                                                                    <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                                                                    <asp:ListItem Value="03">MARCH</asp:ListItem>
                                                                    <asp:ListItem Value="04">APRIL</asp:ListItem>
                                                                    <asp:ListItem Value="05">MAY</asp:ListItem>
                                                                    <asp:ListItem Value="06">JUNE</asp:ListItem>
                                                                    <asp:ListItem Value="07">JULY</asp:ListItem>
                                                                    <asp:ListItem Value="08">AUGUST</asp:ListItem>
                                                                    <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                                                                    <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                                                                    <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                                                    <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlistCYear" runat="server">
                                                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                                                    <asp:ListItem>2019</asp:ListItem>
                                                                    <asp:ListItem>2018</asp:ListItem>
                                                                    <asp:ListItem>2017</asp:ListItem>
                                                                    <asp:ListItem>2016</asp:ListItem>
                                                                    <asp:ListItem>2015</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <b>PARTY NAME *</b>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlistParty" runat="server" Width="160px">
                                                        <asp:ListItem>--SELECT ONE--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlCourseDetails" runat="server" GroupingText="Item Description">
                            <div align="center" style="padding: 10px">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlistSLMCode" runat="server">
                                                <asp:ListItem Value="0">--SELECT SLM CODE--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAdd" Width="60px" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <asp:Panel ID="pnlBill" runat="server" Visible="false">
                                    <div style="padding-top: 10px">
                                        <asp:DataList ID="dtlistSLM" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                                            ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistSLM_ItemCommand">
                                            <HeaderTemplate>
                                                <table align="left">
                                                    <tr>
                                                        <td align="left" style="width: 50px">
                                                            <b>SNo.</b>
                                                        </td>
                                                        <td align="left" style="width: 100px">
                                                            <b>SLMCode</b>
                                                        </td>
                                                        <td align="left" style="width: 300px">
                                                            <b>Title</b>
                                                        </td>
                                                        <td align="left" style="width: 50px">
                                                            <b>Qty.</b>
                                                        </td>
                                                        <td align="left" style="width: 90px">
                                                            <b>Rate</b>
                                                        </td>
                                                        <td align="left" style="width: 80px">
                                                            <b>Amount</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table align="left" style="font-family: Cambria; font-weight: normal; font-size: 14px">
                                                    <tr>
                                                        <td align="left" style="width: 40px">
                                                            <%#Eval("SNo")%>
                                                        </td>
                                                        <td align="left" style="width: 100px">
                                                            <asp:Label ID="lblSLMID" runat="server" Text='<%#Eval("SLMID")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblSLMCode" runat="server" Text='<%#Eval("SLMCode")%>'></asp:Label>
                                                        </td>
                                                        <td align="left" style="width: 300px">
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 50px">
                                                            <asp:TextBox ID="tbQty" runat="server" Width="50px" Text='<%#Eval("Qty")%>'></asp:TextBox>
                                                        </td>
                                                        <td align="right" style="width: 50px">
                                                            <asp:TextBox ID="tbRate" Width="50px" runat="server" Text='<%#Eval("Cost")%>'></asp:TextBox>
                                                        </td>
                                                        <td align="right" style="width: 100px">
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                        </td>
                                                        <td align="right" style="width: 30px">
                                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CausesValidation="false"
                                                                ImageUrl="~/Admin/images/delete.png" OnClientClick="javascript:return confirm('Are you sure!!!\nYou want to delete the record permanently?');"
                                                                runat="server" CommandArgument='<%#Eval("SLMID")%>' Width="20px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div>
                                        <table width="700px" class="tableStyle2" cellspacing="10px">
                                            <tr>
                                                <td align="right" style="width: 300px">
                                                    <b>Total</b>
                                                </td>
                                                <td align="left" style="width: 50px; padding-left: 70px">
                                                    <asp:Label ID="lblTotalQty" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td align="right" style="width: 80px; padding-right: 22px">
                                                    <asp:Label ID="lblTotalAmount" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div align="right" style="width: 700px">
                                        <div style="float: left">
                                            <asp:Button ID="btnLock" runat="server" Text="Lock Bill Details" OnClick="btnLock_Click" />
                                        </div>
                                        <div style="float: right">
                                        </div>
                                        <table class="tableStyle2" cellspacing="10px">
                                            <tr>
                                                <td style="font-weight: normal">
                                                    Discount (%)
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox ID="tbDiscount" Width="80px" runat="server" Text="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal">
                                                    Postage Charge (Rs.)
                                                </td>
                                                <td align="right">
                                                    <asp:TextBox ID="tbPostageCharge" Width="80px" Text="0" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Net Amount Payable (Rs.)
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNetAmount" Font-Bold="true" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 10px">
            <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Submit Bill and Update Stock"
                OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancel" runat="server" Visible="false" Text="Cancel" OnClick="btnCancel_Click" />
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
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Width="50px" Text="OK" />
        </div>
    </asp:Panel>
     </ContentTemplate>
        <Triggers>
             <asp:PostBackTrigger ControlID="btnSubmit" />
             <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
