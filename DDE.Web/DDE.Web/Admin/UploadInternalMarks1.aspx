<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="UploadInternalMarks1.aspx.cs" Inherits="DDE.Web.Admin.UploadInternalMarks1" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
    <div align="center" class="heading">
    Upload  Marks
    </div>
        <div style="font-family: Arial; font-size: 15px">
            <table width="100%">
                <tr>
                    <td align="center">
                        <table class="text" align="center" >
                            <tr>
                                <td>
                                    <div align="center" >                                      
                                        <asp:Label ID="lblBP" runat="server" Font-Size="14px" Font-Bold="true" Text="(Back Paper)"></asp:Label>
                                        <asp:Panel ID="pnlMarkSheet" runat="server">
                                            <div style="padding-left: 15px; padding-top: 25px; padding-right: 15px">
                                                <table width="100%" cellspacing="8px" class="tableStyle2">
                                                    <tr>
                                                        <td style="width: 110px" align="left">
                                                            <b>Name</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left" style="width: 380px">
                                                            <asp:Label ID="lblSName" runat="server" Text=""></asp:Label>
                                                            <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 110px">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 110px" align="left">
                                                            <b>Roll No.</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblRNo" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>Father's Name</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblFName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <b>Enrollment No.</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblENo" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <b>AF Code</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblSCCode" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <b>Examination</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblExamination" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left">
                                                            <b>Course</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="left">
                                                            <b>Year</b>
                                                        </td>
                                                        <td>
                                                            <b>:</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblYear" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div align="center" style="padding-top: 18px">
                                                <asp:DataList Width="100%" ID="dtlistSubMarks" runat="server" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="1px" >
                                                    <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    <HeaderTemplate>
                                                        <table width="100%" class="tableStyle2" border="1" cellpadding="0px" cellspacing="0px">
                                                            <tr>
                                                                <td align="center" style="width: 130px">
                                                                    <b>Course Code</b>
                                                                </td>
                                                                <td align="center" style="width: 300px">
                                                                    <b>Subject</b>
                                                                </td>
                                                                <td style="width: 200px">
                                                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                        <tbody align="center">
                                                                            <tr>
                                                                                <td colspan="3" class="borderbottom">
                                                                                    <b>Maximum Marks</b>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td >
                                                                                    <table style="width: 100%" cellpadding="0px" cellspacing="0px">
                                                                                         <tbody align="center">
                                                                                        <tr>
                                                                                            <td style="width: 33%" class="borderright">
                                                                                                <b>TH</b>
                                                                                            </td>
                                                                                            <td style="width: 33%" class="borderright">
                                                                                                <b>I.A.</b>
                                                                                            </td>
                                                                                            <td align="center" style="width: 33%">
                                                                                                <b>A.W.</b>
                                                                                            </td>
                                                                                        </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 200px">
                                                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                     <tbody align="center">
                                                                        <tr>
                                                                            <td colspan="3" class="borderbottom">
                                                                                <b>Marks Obtained</b>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td >
                                                                                <table style="width: 100%" cellpadding="0px" cellspacing="0px">
                                                                                    <tbody align="center">
                                                                                    <tr>
                                                                                         <td style="width: 33%" class="borderright">
                                                                                            <b>TH</b>
                                                                                        </td>
                                                                                        <td style="width: 33%" class="borderright">
                                                                                            <b>I.A.</b>
                                                                                        </td>
                                                                                        <td align="center" style="width: 33%">
                                                                                            <b>A.W.</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table style="width:100%" align="left" cellpadding="0px" cellspacing="0px">
                                                            <tbody align="left">
                                                                <tr>
                                                                    <td class="borderright" style="width: 150px; height:25px; text-align: left; padding-left: 5px">
                                                                        <%#Eval("SubjectCode")%>
                                                                        <asp:Label ID="lblSubjectID" runat="server" Text='<%#Eval("SubjectID")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblMarksFilled" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td align="left" class="borderright_tal" style="width: 340px; padding-left:5px">
                                                                    <asp:Label ID="lblSubjectName" runat="server" Text='<%#Eval("SubjectName")%>' ></asp:Label>
                                                                        
                                                                    </td>
                                                                    <td align="center" class="borderright" style="width: 80px">
                                                                        <%#Eval("MTH")%>
                                                                    </td>
                                                                    <td align="center" class="borderright" style="width: 75px">
                                                                        <%#Eval("MIA")%>
                                                                    </td>
                                                                    <td align="center" class="borderright" style="width: 75px">
                                                                        <%#Eval("MAW")%>
                                                                    </td>
                                                                    <td align="center" class="borderright" style="width: 80px">
                                                                        <asp:TextBox ID="tbTH" runat="server" Width="40px" Text='<%#Eval("TH")%>'></asp:TextBox>
                                                                        <asp:Label ID="lblTH" runat="server"  Text='<%#Eval("TH")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td align="center" class="borderright" style="width: 75px">
                                                                        <asp:TextBox ID="tbIA" runat="server" Width="40px" Text='<%#Eval("IA")%>'></asp:TextBox>
                                                                        <asp:Label ID="lblIA" runat="server"  Text='<%#Eval("IA")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td align="center" class="borderright" style="width:75px">
                                                                     <asp:TextBox ID="tbAW" runat="server" Width="40px" Text=' <%#Eval("AW")%>'></asp:TextBox>
                                                                     <asp:Label ID="lblAW" runat="server"  Text='<%#Eval("AW")%>' Visible="false"></asp:Label>
                                                                       
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <%--<asp:DataList Width="100%" ID="dtlistPracMarks" runat="server" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="1px" >
                                                    <ItemStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                                    <ItemTemplate>
                                                        <table style="width:100%" align="left" cellpadding="0px" cellspacing="0px">
                                                            <tbody align="left">
                                                                <tr>
                                                                    <td class="borderright" style="width: 150px; height:25px; text-align: left; padding-left: 5px">
                                                                        <%#Eval("PracticalCode")%>
                                                                        <asp:Label ID="lblPracticalID" runat="server" Text='<%#Eval("PracticalID")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblPracMarksFilled" runat="server" Text="" Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td align="left" class="borderright_tal" style="width: 430px; padding-left:5px">           
                                                                        <asp:Label ID="lblPracticalName" runat="server" Text='<%#Eval("PracticalName")%>' ></asp:Label>
                                                                    </td>
                                                                    
                                                                    <td align="left" class="borderright" style="width: 145px">
                                                                        <asp:Label ID="lblPMMarks" runat="server" Text='<%#Eval("PracticalMaxMarks")%>'></asp:Label>
                                                                    </td>
                                                                    
                                                                    <td align="left" class="borderright" style="width: 55px">
                                                                        <asp:TextBox ID="tbPOMarks" runat="server" Text='<%#Eval("PracticalObtainedMarks")%>' Width="40px"></asp:TextBox>
                                                                            <asp:Label ID="lblPOMarks" runat="server" Text='<%#Eval("PracticalObtainedMarks")%>' Visible="false"></asp:Label>
                                                                        
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>--%>
                                            </div>
                                            <div align="center" style="padding-top:15px">
                                                <asp:Button ID="btnUpload" runat="server" Text="Upload Marks" 
                                                    onclick="btnUpload_Click" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle2">
            <tr>
                <td align="center" style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div style="padding-top:10px">
        <asp:Button ID="btnOK" runat="server" Text="OK" onclick="btnOK_Click" />
        </div>
        
    </asp:Panel>
</asp:Content>