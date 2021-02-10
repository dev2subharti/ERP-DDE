<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeCourse.aspx.cs" Inherits="DDE.Web.ChangeCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <table cellspacing="20px" >
                <tr>
                    <td>
                        ENo.
                    </td>
                    <td>
                      <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="3">
                        <table cellspacing="20px">
                            <tr>
                                <td>
                                    Current Course

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistCurrentCourse" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    New Course
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlistNewCourse" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                                </td>
                            </tr>
                        </table>
                    </td>
                   
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
