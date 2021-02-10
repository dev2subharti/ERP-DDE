<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="FillExamRecord_June2013.aspx.cs" Inherits="DDE.Web.Admin.FillExamRecord_June2013" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Show Examination Record June 2013
            </div>
            <div align="center" class="text">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td align="center" colspan="4">
                            <table cellspacing="10px" class="tableStyle1">
                                <tr>
                                    <td valign="top" align="left">
                                        <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">By All Dates</asp:ListItem>
                                            <asp:ListItem Value="2">By Date of Admission</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:RadioButtonList ID="rblAdmissionType" runat="server">
                                            <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">Credit Transfer</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td valign="top" align="left">
                                        <asp:RadioButtonList ID="rblPhoto" runat="server">
                                            <asp:ListItem Selected="True">Without Photo</asp:ListItem>
                                            <asp:ListItem>With Photo</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Batch</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch" runat="server" Width="150px">
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
                    <tr>
                        <td align="left">
                            <b>Course</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCourse" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistYear" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>Category</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistCategory" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Selected="True">ALL</asp:ListItem>
                                <asp:ListItem>GENERAL</asp:ListItem>
                                <asp:ListItem>O.B.C.</asp:ListItem>
                                <asp:ListItem>S.C.</asp:ListItem>
                                <asp:ListItem>S.T.</asp:ListItem>
                                <asp:ListItem>N.R.I.</asp:ListItem>
                                <asp:ListItem>FOREIGN NATIONAL</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Gender</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistGender" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Selected="True">ALL</asp:ListItem>
                                <asp:ListItem>MALE</asp:ListItem>
                                <asp:ListItem>FEMALE</asp:ListItem>
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 10px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Submit Exam Fee For Year</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExamYear" runat="server" Width="150px">
                                <asp:ListItem>--Select One--</asp:ListItem>
                                <asp:ListItem Value="1">1ST YEAR</asp:ListItem>
                                <asp:ListItem Value="2">2ND YEAR</asp:ListItem>
                                <asp:ListItem Value="3">3RD YEAR</asp:ListItem>
                                <asp:ListItem Value="4">4TH YEAR</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 10px">
                <asp:Panel runat="server" ID="pnlDOA" Visible="false">
                    <table class="tableStyle2" cellspacing="10px">
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
                                    <asp:ListItem >2018</asp:ListItem>
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
                                    <asp:ListItem >2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem> 
                                    <asp:ListItem>2020</asp:ListItem><asp:ListItem Selected="True">2021</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnFind" runat="server" Text="Find" Style="height: 26px" Width="82px"
                    OnClick="btnFind_Click" />
            </div>
            <div class="text" align="center" style="padding-top: 30px">
                <asp:DataList ID="dtlistShowRegistration" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" >
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="center" style="width: 148px">
                                    <b>Photo</b>
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
                                <td align="left" style="width: 100px">
                                    <b>Exam Fee</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Exam City</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Exam Centre</b>
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
                                <td align="left" style="width: 140px">
                                    <asp:Image ID="imgPhoto" ImageUrl='<%#Eval("StudentPhoto")%>' runat="server" Width="100px"
                                        Height="100px" />
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
                                    <asp:TextBox ID="tbExamFee" runat="server" Enabled="false" Width="50px" Text='<%#Eval("ExamFee")%>'></asp:TextBox>
                                    <asp:Label ID="lblExamFee" Visible="false" runat="server" Text='<%#Eval("ExamFee")%>'></asp:Label>
                                  
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("ExamCity")%>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("ExamCentre")%>
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
