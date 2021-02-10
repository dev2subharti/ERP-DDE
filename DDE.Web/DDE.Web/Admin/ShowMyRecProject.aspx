<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ShowMyRecProject.aspx.cs" Inherits="DDE.Web.Admin.ShowMyRecProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center" class="heading">
                Show My Received Projects
            </div>
            <div style="padding-top: 10px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            Examination
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistExamination" Width="150px"  runat="server">
                              
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
                   
                </table>
            </div>
            <div style="padding-top: 0px">
                <div style="padding-top: 10px">
                    <asp:Panel ID="pnlCalender" runat="server" >
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
                                        <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                      <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
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
                                        <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                       <asp:ListItem>2018</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
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
                <asp:GridView ID="gvAwarsSheet" BackColor="White" AutoGenerateColumns="false" HeaderStyle-CssClass="rgvheader"
                        RowStyle-CssClass="rgvrow" Width="1000px" runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="S.No." DataField="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Enrollment No." DataField="EnrollmentNo" HeaderStyle-Width="80px"
                                ItemStyle-HorizontalAlign="Center" />
                         
                            <asp:BoundField HeaderText="Practical Code" DataField="PracticalCode" HeaderStyle-Width="90px"
                                ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Course" DataField="Course" HeaderStyle-Width="200px"
                                ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="rcourse" />
                            <asp:BoundField HeaderText="Project Name" DataField="ProjectName" HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="rcourse" />
                            
                                 <asp:BoundField HeaderText="Marks Obt." DataField="MO" HeaderStyle-Width="80px"
                                ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="rcourse" />
                        </Columns>
                    </asp:GridView>
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
