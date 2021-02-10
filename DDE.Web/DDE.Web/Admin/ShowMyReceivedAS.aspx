<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="ShowMyReceivedAS.aspx.cs" Inherits="DDE.Web.Admin.ShowMyReceivedAS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center" class="heading">
                Show My AS Received
            </div>
            <div style="padding-top: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            Examination
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExamination" Width="150px"  runat="server">
                                 <asp:ListItem Value="Z10">DECEMBER 2019</asp:ListItem>
                                <asp:ListItem Value="W10" Selected="True">JUNE 2019</asp:ListItem>
                              <asp:ListItem Value="B18">DECEMBER 2018</asp:ListItem>
                             <asp:ListItem Value="A18">JUNE 2018</asp:ListItem>
                               <asp:ListItem Value="B17">DECEMBER 2017</asp:ListItem>
                                 <asp:ListItem Value="A17">JUNE 2017</asp:ListItem>
                                    <asp:ListItem Value="B16">DECEMBER 2016</asp:ListItem>
                                       <asp:ListItem Value="A16">JUNE 2016</asp:ListItem>
                                        <asp:ListItem Value="B15">DECEMBER 2015</asp:ListItem>
                                          <asp:ListItem Value="A15">JUNE 2015</asp:ListItem>
                                            <asp:ListItem Value="B14">DECEMBER 2014</asp:ListItem>
                                              <asp:ListItem Value="A14">JUNE 2014</asp:ListItem>
                                                <asp:ListItem Value="B13">DECEMBER 2013</asp:ListItem>
                                                   <asp:ListItem Value="A13">JUNE 2013</asp:ListItem>
              
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            Filter
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistFilter" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlistFilter_SelectedIndexChanged">
                                <asp:ListItem>NOT PUBLISHED</asp:ListItem>
                                <asp:ListItem>ALL</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblPC" runat="server" Visible="false" Text="Paper Code"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="tbPC"  Visible="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-top: 0px">
                <div style="padding-top: 10px">
                    <asp:Panel ID="pnlCalender" runat="server" Visible="false">
                        <table class="tableStyle2" cellspacing="10px">
                            <tr>
                                <td align="left">
                                    <b>From</b>
                                </td>
                                <td valign="middle">
                                    <asp:DropDownList ID="ddlistDayFrom" runat="server">
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
                                    <asp:DropDownList ID="ddlistMonthFrom" runat="server">
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
                                    <asp:DropDownList ID="ddlistYearFrom" runat="server">
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
                                        <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10px">
                                    &nbsp;
                                </td>
                                <td align="left">
                                    <b>To</b>
                                </td>
                                <td valign="middle">
                                    <asp:DropDownList ID="ddlistDayTo" runat="server">
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
                                    <asp:DropDownList ID="ddlistMonthTo" runat="server">
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
                                    <asp:DropDownList ID="ddlistYearTo" runat="server">
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
                                        <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div align="center" style="padding-top: 10px">
                    <asp:Button ID="btnPublish" runat="server" Text="Search" Width="75px" Height="26px"
                        OnClick="btnPublish_Click" />
                </div>
                <div align="center" style="padding-top:20px">
                <asp:DataList ID="dtlistASReport" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                    ItemStyle-CssClass="dtlistItem" onitemcommand="dtlistASReport_ItemCommand" >
                    <HeaderTemplate>
                        <table align="left" >
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>                            
                                <td align="left" style="width: 100px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="center" style="width: 320px">
                                    <b>Subject Name</b>
                                </td>
                                <td align="left" style="width: 140px">
                                    <b>Total Copies</b>
                                </td>
                                 <td align="left" style="width: 90px">
                                    <b>Present</b>
                                </td>
                                 <td align="left" style="width: 90px">
                                    <b>Absent</b>
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
                                    <%#Eval("PaperCode")%>
                                </td>
                                <td align="left" style="width: 350px">
                                    <%#Eval("SubjectName")%>
                                </td>
                                <td align="left" style="width: 140px">
                                    <%#Eval("TotalStudents")%>
                                </td>
                                 <td align="left" style="width: 80px">
                                    <%#Eval("Present")%>
                                </td>
                                 <td align="left" style="width: 80px">
                                    <%#Eval("Absent")%>
                                </td>
                             
                                <td align="center" style="width: 50px">
                                    <asp:LinkButton ID="lnkbtnPrint" runat="server" Text="Print" CommandName='<%#Eval("SubjectName")%>'
                                        CommandArgument='<%#Eval("PaperCode") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
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
</asp:Content>
