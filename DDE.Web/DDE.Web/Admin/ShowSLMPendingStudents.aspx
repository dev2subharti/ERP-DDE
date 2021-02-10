<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowSLMPendingStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowSLMPendingStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Show SLM Pending Students (On Trial)
        </div>
        <div>
            <table cellspacing="10px" class="tableStyle1">
                <tr>
                    <td valign="top" align="left">
                        <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="1">ALL</asp:ListItem>
                            <asp:ListItem Value="2">By Date</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div align="center" class="text" style="padding-top: 10px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>SC Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" AutoPostBack="true" Width="100px" runat="server"
                                OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 5px">
                <asp:Panel ID="pnlDate" runat="server" Visible="false">
                    <table class="tableStyle2" cellspacing="5px">
                        <tr>
                            <td align="left">
                                <b>From</b>
                            </td>
                            <td valign="middle">
                                <asp:DropDownList ID="ddlistDOADayFrom" runat="server">
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
                                <asp:DropDownList ID="ddlistDOAMonthFrom" runat="server">
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
                                <asp:DropDownList ID="ddlistDOAYearFrom" runat="server">
                                    <asp:ListItem>2009</asp:ListItem>
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                   <asp:ListItem>2017</asp:ListItem>
                                   <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem> 
                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10px">
                                &nbsp;
                            </td>
                            <td align="left">
                                <b>To</b>
                            </td>
                            <td valign="middle">
                                <asp:DropDownList ID="ddlistDOADayTo" runat="server">
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
                                <asp:DropDownList ID="ddlistDOAMonthTo" runat="server">
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
                                <asp:DropDownList ID="ddlistDOAYearTo" runat="server">
                                    <asp:ListItem>2009</asp:ListItem>
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                   <asp:ListItem>2017</asp:ListItem>
                                   <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem> 
                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="padding-top: 10px; padding-bottom: 20px">
                <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div style="padding-bottom: 5px">
                <asp:Panel ID="pnlSelect" runat="server" Visible="false">
                    <table class="tableStyle2" cellspacing="10px">
                        <tr>
                            <td>
                                <b>Course</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistCourse" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Year</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistYear" runat="server">
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                 <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                  <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnRemoveYear" runat="server" Text="Remove" 
                                    onclick="btnRemoveYear_Click"  />
                            </td>
                            <td>
                                <asp:Button ID="btnSelectYear" runat="server" Text="Select" 
                                    onclick="btnSelectYear_Click"  />
                            </td>
                        </tr>

                    </table>
                </asp:Panel>
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowSLM" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS">
                    <HeaderTemplate>
                        <table align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 50px">
                                    <b>SNo</b>
                                </td>
                                <td align="left" style="width: 165px">
                                    <b>Enrollment No.</b>
                                </td>
                                <td align="left" style="width: 208px">
                                    <b>Student Name</b>
                                </td>
                                <td align="left" style="width: 212px">
                                    <b>Father Name</b>
                                </td>
                                <td align="left" style="width: 210px">
                                    <b>Course</b>
                                </td>
                                <td align="left" style="width: 50px">
                                    <b>Year</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 40px">
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                    <asp:Label ID="lblSLMRID" runat="server" Text='<%#Eval("SLMRID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" class="border_rb" style="width: 150px">
                                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%#Eval("EnrollmentNo")%>'></asp:Label>
                                </td>
                                <td align="left" class="border_rb" style="width: 200px">
                                    <asp:Label ID="lblSName" runat="server" Text='<%#Eval("StudentName")%>'></asp:Label>
                                </td>
                                <td align="left" class="border_rb" style="width: 200px">
                                    <asp:Label ID="lblFName" runat="server" Text='<%#Eval("FatherName")%>'></asp:Label>
                                </td>
                                <td align="left" class="border_rb" style="width: 200px">
                                    <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
                                    <asp:Label ID="lblCourseID" runat="server" Visible="false" Text='<%#Eval("CourseID")%>'></asp:Label>
                                </td>
                                <td align="center" class="border_rb" style="width: 40px">
                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                                </td>
                                <td align="center" class="border_rb" style="width: 40px">
                                    <asp:CheckBox ID="cbSelect" runat="server" Checked="true" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnPublish" runat="server" Text="Publish Letter" Visible="false"
                    OnClick="btnPublish_Click" />
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
