<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="ReceiveAssignment.aspx.cs" Inherits="DDE.Web.Admin.ReceiveAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Receive Assignment
            </div>

            <div>
                <asp:Panel ID="pnlSearch" runat="server">
                    <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td>
                            <b>
                                Exam</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistExam" Width="150px" runat="server">
                                <asp:ListItem>JUNE 2020</asp:ListItem>                            
                            </asp:DropDownList>
                        </td>
                        <td>
                            <b>
                                <asp:Label ID="lblNo" runat="server" Text="Enrollment No"></asp:Label></b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbENo" Width="150px" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                Width="75px" Height="26px" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                

            </div>
             <div align="center" style="padding-top:20px">
                <asp:Button ID="btnUDAnother" CssClass="btn" runat="server" Visible="false" Width="300px" Text="Upload Assignment of Another Student" OnClick="btnUDAnother_Click" />
                 <asp:Label ID="lblET" Visible="false" runat="server" Text=""></asp:Label>
             </div>
            <div>
                <asp:Panel ID="pnlAssDetails" runat="server" Visible="false">
                    <table class="tableStyle2" >
                    <tr>
                        <td>
                            <div align="center">
                                <asp:Panel ID="pnlStudentDetail" Font-Bold="true" GroupingText="Student Details"
                                    ForeColor="Red" runat="server">
                                    <table align="center" cellpadding="0px" class="tableStyle2" cellspacing="0px">
                                        <tr>
                                            <td align="center" valign="top" style="padding-left: 5px">
                                                <asp:Image ID="imgStudent" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                                    runat="server" Width="120px" Height="150px" />
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            <table>
                                                                <tr>
                                                                    <td align="left">
                                                                        <b>Enrollment No</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                    <asp:TextBox ID="tbEnrollmentNo" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <b>Student&#39;s Name</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbSName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                     &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <b>Father&#39;s Name</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbFName" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>

                                                                <tr>
                                                                    <td align="left">
                                                                        <b>Session</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbBatch" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <b>Course</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbCourse" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                        <asp:Label ID="lblCID" runat="server" Text="" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblCD" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <b>Current Year</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <b>:</b>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbYear" runat="server" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                                        <asp:Label ID="lblYear" runat="server" Text="" Visible="false"></asp:Label>
                                                                         <asp:Label ID="lblDocUploaded" runat="server" Text="" Visible="false"></asp:Label>
                                                                        
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <asp:Panel ID="pnlDocumentDetails"  Font-Bold="true" ForeColor="Red"
                                    runat="server" GroupingText="Assignment Details">
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <table align="center" class="tableStyle2">

                                                  
                                                    <tr>
                                                        <td align="center" style="padding-left: 10px" >
                                                            <table cellspacing="10px">
                                                                <tbody align="left" valign="top">                               
                                                                  <tr>                                                                                                         
                                                                    <td align="center" class="auto-style1">
                                                                        <asp:Button ID="btnUpload1" CssClass="btn" runat="server" Width="120px" Text="Submit" OnClick="btnUpload1_Click" /><br />
                                                                        <asp:Label ID="lblEMsg" runat="server" Text="Label"></asp:Label>
                                                                    </td>
                                                                 </tr>                                                                                                                                  
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                
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
            <div align="center" style="padding-top: 20px">
                <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" CssClass="btn" OnClick="btnOK_Click" />
            </div>
        </asp:Panel>
    </div>

</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="cphheadColleges">
    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>
</asp:Content>
