<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DDE.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>D.L.M.</title>
    <link rel="Shortcut Icon" type="image/png" href="images/dde_icon.png"  />
    <link href="CSS/DDE.css" rel="Stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/colorbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <!--wrapper -->
    <div id="outer_wrapper">
        <div id="wrapper">
            <!--header -->
            <div id="header">
                <%-- <asp:Button ID="Button1" runat="server" Text="ERID" onclick="Button1_Click" />--%>
                <%-- <asp:Button ID="btn" runat="server" Text="Change" onclick="btn_Click" />--%>
                <table width="100%" cellpadding="0px" cellspacing="0px">
                    <tr>
                        <td style="width: 33%">
                            <a href="#">
                                <img src="./images/logo.png" alt="" width="285" height="95" id="logo" /></a>
                        </td>
                        <td align="center" style="width: 33%">
                            <div align="center" id="erps">
                                <div>
                                    D.L.M.
                                </div>
                                <div id="slogen">
                                    Managing Subharti....
                                </div>
                            </div>
                        </td>
                        <td>
                            <div id="right_header">
                                <div id="top_nav">
                                    <ul>
                                        <li><a href="#" class="active"><span>About ERP</span></a></li>
                                        <li><a href="#"><span>Contact Us</span></a></li>
                                    </ul>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <!--content area -->
            <div id="content" align="center">
                <!--banner section -->
                <div id="banner_wrapp_inner" align="center">
                    <div id="banner_inner">
                        <img src="images/Contact us.jpg" height="200px" width="900px" alt="" />
                    </div>
                </div>
                <div id="left_content1">
                    <div align="center">
                        <table align="center" width="800px">
                            <tr>
                                <td align="center" valign="top" style="width: 800px; height: 300px; padding-top: 50px">
                                    <table width="88%" style="height: 200px" class="homebtn">
                                        <tr>
                                            <td align="left">
                                                <asp:LinkButton ID="lnkbtnAdmin"  CssClass="hpbtn" Width="180px" runat="server" OnClick="lnkbtnAdmin_Click">Administrator</asp:LinkButton>
                                            </td>
                                            <td align="right">
                                                <asp:LinkButton ID="lnkbtnSC"  CssClass="hpbtn" Width="180px" runat="server" OnClick="lnkbtnSC_Click">Study Centres</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <asp:LinkButton ID="lnkbtnUsers"  CssClass="hpbtn" Width="180px" runat="server" OnClick="lnkbtnUsers_Click">Users</asp:LinkButton>
                                            </td>
                                            <td align="right">
                                                <asp:LinkButton ID="lnkbtnStudents"  CssClass="hpbtn" Width="180px" runat="server"
                                                    OnClick="lnkbtnStudents_Click">Students</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
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
                    Designed & Developed By : IT Cell, DDE.
                </div>
            </div>
        </div>
        <br class="clear" />
    </div>
    </form>
</body>
</html>
