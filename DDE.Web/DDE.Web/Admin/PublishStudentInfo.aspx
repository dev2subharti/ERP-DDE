<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="PublishStudentInfo.aspx.cs" Inherits="DDE.Web.Admin.PublishStudentInfo" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <div class="heading" align="center">
        Publish Student Record
    </div>
    <asp:Panel ID="pnlInfoFields" runat="server" Visible="true">
        <div align="center" class="text" style="padding-top: 20px; padding-bottom: 10px">
            <table class="tableStyle2" cellspacing="10px">
                <tr>
                    <td align="center" colspan="4">
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:RadioButtonList ID="rblMode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblMode_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">Regular</asp:ListItem>
                                        <asp:ListItem Value="2">By Date of Admission</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="rblFilterByCourse" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblFilterByCourse_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">Course Wise</asp:ListItem>
                                        <asp:ListItem Value="2">Group Wise</asp:ListItem>
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
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem Value="1">1st Year</asp:ListItem>
                            <asp:ListItem Value="2">2nd Year</asp:ListItem>
                            <asp:ListItem Value="3">3rd Year</asp:ListItem>
                            <asp:ListItem Value="4">4th Year</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
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
                                    <asp:ListItem >2012</asp:ListItem>
                                    <asp:ListItem >2013</asp:ListItem>
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
                                    <asp:ListItem >2012</asp:ListItem>
                                    <asp:ListItem >2013</asp:ListItem>
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
            <br />
            <br />
            <div>
                <asp:Panel ID="pnlGroups" runat="server" Visible="false">
                    <asp:DataList class="tableStyle2" ID="dtlistGroup" runat="server" RepeatColumns="4">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbCourse" runat="server" Text='<%#Eval("Course")%>' />
                                        <asp:Label ID="lblCourse" runat="server" Text='<%#Eval("CourseID")%>' Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </asp:Panel>
            </div>
            <br />
            <br />
            <table class="tableStyle2" width="400px">
                <tr>
                    <td align="center">
                        <b>Select Information Fields</b>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table width="100%" border="1">
                            <tbody align="left">
                                <tr>
                                    <td valign="top" style="width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Application No
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbANo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Admission Through
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbAdmissionThrough" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Enrollment No
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbENo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    I Card No
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbICNo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Admission Type
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbAT" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Student Name
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbSName" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Father's Name
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbFName" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    SC Code
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbSCCode" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Batch
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbBatch" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Course
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbCourse" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" style="width: 50%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Year
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbYear" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    DOB
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbDOB" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Gender
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbGender" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Nationality
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbNationality" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Category
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbCategory" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Address
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbAddress" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Pincode
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbPincode" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Phone No.
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbPhoneNo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Mobile No.
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbMNo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Email Address
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbEAddress" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="padding: 10px">
            <asp:Button ID="btnFind" runat="server" Text="Find" Width="90px" OnClick="btnFind_Click" />
        </div>
    </asp:Panel>
    <div style="padding: 10px">
        <asp:GridView ID="gvShowStudent" CssClass="gridview" HeaderStyle-CssClass="gvheader"
            RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" runat="server">
        </asp:GridView>
    </div>
    <div style="padding: 10px">
        <asp:Button ID="Publish" runat="server" Text="Publish" Width="100px" Visible="false"
            OnClick="Publish_Click" />
    </div>
    <div style="padding: 10px">
        <asp:Label ID="lblMSG" runat="server" Visible="false" Text="Sorry !! No Record Found"
            Font-Bold="True" Font-Size="Larger" ForeColor="#FF3300"></asp:Label>
    </div>
</asp:Content>
