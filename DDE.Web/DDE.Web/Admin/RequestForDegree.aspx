﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="RequestForDegree.aspx.cs" Inherits="DDE.Web.Admin.RequestForDegree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi">
    </script>
    <script type="text/javascript">

        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.HINDI],
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.
            control.makeTransliteratable(['ctl00_cphBody_tbSNameH']);
            control.makeTransliteratable(['ctl00_cphBody_tbFNameH']);
            control.makeTransliteratable(['ctl00_cphBody_tbMNameH']);


        }
        google.setOnLoadCallback(onLoad);
    </script>
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
                Fill Degree Information
            </div>
            <div style="padding-top: 20px; padding-bottom: 20px">
                <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                            <td>
                                <b>Enrollment No</b>
                            </td>
                            <td>
                                <asp:TextBox ID="tbENo" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <b>Year of Passing</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistExam" runat="server" AutoPostBack="true" 
                                    onselectedindexchanged="ddlistExam_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnsearch_Click"
                                    Width="75px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div>
                <asp:Panel ID="pnlStudentDetails" runat="server" Visible="false">
                    <div>
                        <table cellpadding="0px" class="tableStyle2" cellspacing="0px">
                            <tr>
                                <td valign="top" style="padding-left: 5px">
                                    <asp:Image ID="imgStudent" BorderWidth="2px" BorderStyle="Solid" BorderColor="#3399FF"
                                        runat="server" Width="100px" Height="120px" />
                                </td>
                                <td valign="top">
                                    <table cellspacing="10px">
                                     <tr>
                                            <td valign="top" align="left">
                                                <b>Student&#39;s Name *</b>
                                            </td>
                                            <td valign="top" align="left">
                                                <b>:</b>
                                            </td>
                                            <td valign="top" align="left" >
                                                <asp:TextBox ID="tbSNameE" runat="server" Width="200px" Enabled="false"  ForeColor="Black"></asp:TextBox>(In English)<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbSNameE" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator><br /><br />
                                                 <asp:TextBox ID="tbSNameH" Width="200px" runat="server" ForeColor="Black"></asp:TextBox>(In Hindi)<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbSNameH" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                
                                            </td>
                                        </tr>
                                         <tr>
                                            <td valign="top" align="left">
                                                <b>Father&#39;s Name *</b>
                                            </td>
                                            <td valign="top" align="left">
                                                <b>:</b>
                                            </td>
                                           
                                               <td valign="top" align="left">
                                                <asp:TextBox ID="tbFNameE" runat="server" Width="200px" Enabled="false"  ForeColor="Black"></asp:TextBox>(In English)<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbFNameE" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator><br /><br />
                                                 <asp:TextBox ID="tbFNameH" Width="200px" runat="server" ForeColor="Black"></asp:TextBox>(In Hindi)<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbFNameH" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                            </td>
                                          
                                        </tr>
                                         <tr>
                                            <td valign="top" align="left">
                                                <b>Mother&#39;s Name *</b>
                                            </td>
                                            <td valign="top" align="left">
                                                <b>:</b>
                                            </td>
                                           
                                               <td valign="top" align="left">
                                                <asp:TextBox ID="tbMNameE" runat="server" Width="200px" Enabled="false"  ForeColor="Black"></asp:TextBox>(In English)<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbMNameE" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator><br /><br />
                                                 <asp:TextBox ID="tbMNameH" Width="200px" runat="server" ForeColor="Black"></asp:TextBox>(In Hindi)<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="tbMNameH" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Enrollment No *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbEnNo" Enabled="false" runat="server"  ForeColor="Black"></asp:TextBox>
                                                <asp:Label ID="lblSRID" runat="server" Text="" Visible="false"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbEnNo" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Roll No. *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbRollNo" Enabled="false" runat="server"  ForeColor="Black"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbRollNo" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Final Division *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlistDiv" runat="server">
                                              <asp:ListItem>FIRST</asp:ListItem>
                                              <asp:ListItem>SECOND</asp:ListItem>
                                              <asp:ListItem>THIRD</asp:ListItem>
                                              <asp:ListItem>NOT FOUND</asp:ListItem>
                                                </asp:DropDownList> 
                                               
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="left">
                                                <b>Gender *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlistGender" Enabled="false" runat="server">
                                                 <asp:ListItem>MALE</asp:ListItem>
                                                 <asp:ListItem>FEMALE</asp:ListItem>
                                                </asp:DropDownList>                                               
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="left">
                                                <b>Course *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbCourse" Width="200px" Enabled="false" runat="server" ForeColor="Black"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbCourse" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Specialization</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbSpecialization" Enabled="false" Width="200px" runat="server"  ForeColor="Black"></asp:TextBox>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left">
                                                <b>Address *</b>
                                            </td>
                                            <td valign="top" align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbAddress" TextMode="MultiLine"  Width="200px" Height="100px" runat="server"  ForeColor="Black"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="tbAddress" runat="server" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Pin Code *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbPin"  Width="200px" runat="server"  ForeColor="Black"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbPin" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Mobile No. *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbMNo" Width="200px" runat="server"  ForeColor="Black"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tbMNo" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Aadhaar No. *</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbADNo" Width="200px" runat="server"  ForeColor="Black"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="tbADNo" ErrorMessage="Please fill this entry"></asp:RequiredFieldValidator>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <b>Degree/Diploma to be Issued</b>
                                            </td>
                                            <td align="left">
                                                <b>:</b>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlistDegreeType" runat="server">
                                                <asp:ListItem>ORIGINAL</asp:ListItem>
                                                <asp:ListItem>DUPLICATE</asp:ListItem>
                                                <asp:ListItem>CORRECTED</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                </asp:Panel>
            </div>
            <div style="padding: 10px">
                <asp:Button ID="btnSubmit" runat="server" Text="" Visible="false" 
                    onclick="btnSubmit_Click" />
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
    <div style="padding-top: 20px">
        <asp:Button ID="btnOK" runat="server" Text="OK" Visible="false" Width="60px" OnClick="btnOK_Click" />
    </div>
</asp:Content>
