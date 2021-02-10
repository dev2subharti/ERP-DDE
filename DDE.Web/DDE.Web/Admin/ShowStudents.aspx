<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowStudents.aspx.cs" Inherits="DDE.Web.Admin.ShowStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div>
        <div class="GridviewDiv">
            <table border="0" cellpadding="0" cellspacing="1" class="GridviewTable">
                <tr>
                    <td style="width: 100px;">
                        Enrollment No
                    </td>
                    <td style="width: 150px;">
                        Student Name
                    </td>
                    <td style="width: 130px;">
                        Category
                    </td>
                    <td style="width: 130px;">
                        Gender
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px;">
                    </td>
                    <td style="width: 150px;">
                    </td>
                    <td style="width: 130px;">
                        <asp:DropDownList ID="ddlUserName" runat="server" DataSourceID="dsUserName" DataValueField="Category"
                            AutoPostBack="true" Width="120px" Font-Size="11px" AppendDataBoundItems="true">
                            <asp:ListItem Text="All" Value="%" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 130px;">
                        <asp:DropDownList ID="ddlLocation" runat="server" DataSourceID="dsLocation" DataValueField="Gender"
                            AutoPostBack="true" Width="120px" Font-Size="11px" AppendDataBoundItems="true">
                            <asp:ListItem Text="All" Value="%" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView runat="server" ID="gvdetails" ShowHeader="false" AllowPaging="true"
                            PageSize="10" PagerSettings-Mode="NumericFirstLast"  DataSourceID="dsdetails"
                            AutoGenerateColumns="false" CssClass="Gridview">
                            <Columns>
                                <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" ItemStyle-Width="150px" />
                                <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-Width="130px" />
                                <asp:BoundField DataField="Gender" HeaderText="Gender" ItemStyle-Width="130px" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <asp:SqlDataSource ID="dsUserName" runat="server" ConnectionString="<%$ConnectionStrings:CSddedb %>"
            SelectCommand="Select Distinct Category from DDEStudentRecord"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsLocation" runat="server" ConnectionString="<%$ConnectionStrings:CSddedb %>"
            SelectCommand="Select Distinct Gender from DDEStudentRecord"></asp:SqlDataSource>
        <asp:SqlDataSource ID="dsdetails" runat="server" ConnectionString="<%$ConnectionStrings:CSddedb %>"
            SelectCommand="select * from DDEStudentRecord" FilterExpression=" Category Like '{0}%' and Gender Like '{1}%'">
            <FilterParameters>
                <asp:ControlParameter Name="UserName" ControlID="ddlUserName" PropertyName="SelectedValue" />
                <asp:ControlParameter Name="Location" ControlID="ddlLocation" PropertyName="SelectedValue" />
            </FilterParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
