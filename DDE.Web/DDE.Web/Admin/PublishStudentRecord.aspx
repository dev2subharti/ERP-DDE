<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishStudentRecord.aspx.cs"
    Inherits="DDE.Web.Admin.PublishStudentRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/DDE.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="heading" align="center" style="padding: 10px">
        <asp:Label ID="lblHeading" runat="server" Text="Label"></asp:Label>
    </div>
    <div align="center" style="padding: 20px">
        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" />
    </div>
    <div align="center">
        <asp:GridView ID="gvShowStudent" CssClass="gridview" AutoGenerateColumns="false" HeaderStyle-CssClass="gvheader"
            RowStyle-CssClass="gvitem" FooterStyle-CssClass="gvfooter" runat="server">
            <Columns>
                <asp:BoundField HeaderText="S.No." DataField="SNo" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="ANo" DataField="ANo" ItemStyle-Width="100px" />
                <asp:BoundField HeaderText="Ad.Through" DataField="Ad.Through" ItemStyle-Width="40px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="EnrollmentNo" DataField="EnrollmentNo" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="ICardNo" DataField="ICardNo" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Ad.Type" DataField="Ad.Type" ItemStyle-Width="100px" />
                <asp:BoundField HeaderText="StudentName" DataField="StudentName" ItemStyle-Width="120px" />
                <asp:BoundField HeaderText="FatherName" DataField="FatherName" ItemStyle-Width="140px" />
                <asp:BoundField HeaderText="Batch" DataField="Batch" ItemStyle-Width="120px" />
                <asp:BoundField HeaderText="SCCode" DataField="SCCode" ItemStyle-Width="150px" />
                <asp:BoundField HeaderText="Course" DataField="Course" ItemStyle-Width="160px" />
                <asp:BoundField HeaderText="Year" DataField="CurrentYear" ItemStyle-Width="100px" />
                <asp:BoundField HeaderText="DOB" DataField="DOB" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Gender" DataField="Gender" ItemStyle-Width="90px" />
                <asp:BoundField HeaderText="Nationality" DataField="Nationality" ItemStyle-Width="80px" />
                <asp:BoundField HeaderText="Category" DataField="Category" ItemStyle-Width="100px" />
                <asp:BoundField HeaderText="Address" DataField="Address" ItemStyle-Width="120px" />
                <asp:BoundField HeaderText="Pincode" DataField="Pincode" ItemStyle-Width="140px" />
                <asp:BoundField HeaderText="PhoneNo" DataField="PhoneNo" ItemStyle-Width="120px" />
                <asp:BoundField HeaderText="MobileNo" DataField="MobileNo" ItemStyle-Width="150px" />
                <asp:BoundField HeaderText="EMail" DataField="EMail" ItemStyle-Width="160px" />
                <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-Width="100px" />
                 <asp:BoundField HeaderText="Remark" DataField="Remark" ItemStyle-Width="150px" />
                <asp:BoundField HeaderText="S.No. - Fee Head - Payment Mode - Amount - D/C No. - D/C Date - Total D/C Amount - Bank Name" DataField="FeeDetails" ItemStyle-Wrap="false" ItemStyle-Width="400px"
                    HtmlEncode="false" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
