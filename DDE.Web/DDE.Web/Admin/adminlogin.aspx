<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminlogin.aspx.cs" Inherits="DDE.Web.Admin.adminlogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>D.L.M. Login</title>
    <link rel="Shortcut Icon" type="image/png" href="../images/dde_icon.png"  />
     <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="../css/SubhartiERPS.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!--wrapper -->
    <div id="outer_wrapper">
        <div id="wrapper">
            <!--header -->
            <div id="header">
                <a href="#">
                    <img src="../images/logo.png" alt="" width="285" height="95" id="logo" /></a>
                <div id="right_header">
                    <div id="top_nav">
                        <ul>
                            <li><a href="#" class="active"><span>About ERP</span></a></li>
                            <li><a href="#"><span>Contact Us</span></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!--content area -->
            <div id="content" align="center">
                <!--banner section -->
                <div id="banner_wrapp_inner" align="center">
                    <div id="banner_inner">
                        <img src="../images/Contact us.jpg" height="200px" width="900px" alt="" />
                    </div>
                </div>
                <div id="left_content1">
                    <div>
                        <asp:Panel ID="pnlLogin" Visible="true" runat="server">
                            <div style="padding-top: 60px; padding-bottom: 60px">
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <h2>
                                                <asp:Label ID="lblRole" runat="server" Text=""></asp:Label> </h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="middle">
                                            <table align="center" border="1">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="20px" align="center" style="background-color: #ececec;">
                                                            <tr>
                                                                <td align="left">
                                                                    User Name
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtUserName" Visible="true" runat="server" class="username"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    Password
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" class="password"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <table>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Width="70px" />
                                                                            </td>
                                                                            <td style="width: 40px">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnCancel" runat="server" Text="Reset" Width="70px" OnClick="btnCancel_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <div align="center" style="padding-top: 20px">
                                    <asp:Label ID="lblError" runat="server" Text="Invalid User Name or Password" Font-Bold="True"
                                        ForeColor="Red" Visible="False"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <br class="clear" />
                </div>
            </div>
        </div>
        <!--footer -->
        <div id="outer_footer">
            <div id="footer">
                <div class="bottom_footer">
                    <p>
                        &copy; 2011 SUBHARTI UNIVERSITY All Rights Reserved</p>
                    <a href="#" id="topScroll">Back to Top</a>
                </div>
                <div align="center" style="font-family: Arial; font-size: 12px; color: White">
                    Designed & Developed By : Subharti Software Development Centre ( S.S.D.C ).
                </div>
            </div>
        </div>
        <br class="clear" />
    </div>
    </form>
</body>
</html>
