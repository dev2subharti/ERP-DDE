<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendSMSToStudents.aspx.cs" MasterPageFile="~/Admin/MasterPages/Admin.Master" Inherits="DDE.Web.Admin.SendSMSToStudents" %>

<asp:Content ID="contenthead" ContentPlaceHolderID="cphHead" runat="server">
    <script type="text/javascript">  
        function Count(limitField, limitCount, limitNum) {

            var i = document.getElementById("ctl00_cphBody_tbMSG").value.length;
            document.getElementById("ctl00_cphBody_lblDisplay").innerHTML = 160 - i;

           

            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            }
            else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

    </script>
    <script type="text/javascript">  
        function CountH(limitField, limitCount, limitNum) {

            var i = document.getElementById("ctl00_cphBody_tbMSGH").value.length;
            document.getElementById("ctl00_cphBody_lblDisplayH").innerHTML = 160 - i;

           

            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            }
            else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

    </script>
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
            control.makeTransliteratable(['ctl00_cphBody_tbMSGH']);
           


        }
        google.setOnLoadCallback(onLoad);
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Send SMS to Students
            </div>


            <div align="center" class="text">
                <table class="tableStyle1" cellspacing="10px">
                    
                    <tr>
                           <td align="left">
                            <b>SC Code</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistSCCode" AutoPostBack="true" runat="server" Width="150px" OnSelectedIndexChanged="ddlistSCCode_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <b>Batch</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlistBatch" AutoPostBack="true" runat="server" Width="150px"
                                OnSelectedIndexChanged="ddlistBatch_SelectedIndexChanged">
                                <asp:ListItem>--SELECT ONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                     
                    </tr>
                    
                </table>
            </div>
            <div style="padding-top:10px">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" OnClick="btnSearch_Click" />

            </div>
            <div  align="center" style="padding: 20px">
                <asp:Panel ID="pnlStudentList" CssClass="pnldiv1" Width="900px" runat="server" Visible="false">
                    <div align="center" class="pnldiv1" style="width: 450px; padding-right: 8px; height: 30px; padding-top: 5px; font-size: 20px; font-family: Cambria">
                        <b>SMS Balance : </b>
                        <asp:Label ID="lblSMSBalance" ForeColor="Green" Font-Bold="true" runat="server" Text=""></asp:Label>

                    </div>
                    <div class="pnldiv1" style="width:200px">
                             <asp:RadioButtonList ID="rblMsgType" AutoPostBack="true" RepeatDirection="Horizontal" runat="server" OnSelectedIndexChanged="rblMsgType_SelectedIndexChanged">
                                 <asp:ListItem Value="text" Selected="True">English</asp:ListItem>
                                 <asp:ListItem Value="unicode">Hindi</asp:ListItem>
                             </asp:RadioButtonList>
                         </div>
                    <div align="center" >
                        <table class="pnldiv1" cellspacing="20px">
                            <tr>
                                <td align="left" valign="top">
                                    <b>Message *</b>
                                </td>
                                <td align="left" style="width: 300px">
                                     <asp:TextBox ID="tbMSG"  runat="server" onkeyup="Count(this.form.tbMSG,this.form.countdown,160)" Width="400px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                 <asp:TextBox ID="tbMSGH" Visible="false"  runat="server" onkeyup="CountH(this.form.tbMSGH,this.form.countdown,160)" Width="400px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbMSG" ErrorMessage="Please Fill"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbMSGH" ErrorMessage="Please Fill"></asp:RequiredFieldValidator>
                                <br />
                                <b>Remaining Characters :
                                    <asp:Label ID="lblDisplay" runat="server" Text="160"></asp:Label>
                                    <asp:Label ID="lblDisplayH" Visible="false" runat="server" Text="160"></asp:Label>
                                </b>
                                </td>
                                <td valign="top">
                                    <asp:Button ID="btnSendSMS" CssClass="btn" runat="server" Text="Send SMS" OnClick="btnSendSMS_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="width: 900px" align="right">
                        <asp:Button ID="btnSelectAll" CausesValidation="false" runat="server" CssClass="btn" OnClick="btnSelectAll_Click" Text="Select All" />
                    <div style="padding-top: 10px">
                        <asp:DataList ID="dtlistShowRegistration" CssClass="dtlist" runat="server" HeaderStyle-CssClass="dtlistheader"
                            ItemStyle-CssClass="dtlistItem">
                            <HeaderTemplate>
                                <table align="left">
                                    <tr>
                                        <td align="left" style="width: 50px">
                                            <b>S.No.</b>
                                        </td>
                                        <td align="left" style="width:100px">
                                            <b>OANo</b>
                                        </td>
                                        <td align="left" style="width: 120px">
                                            <b>ENo</b>
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <b>SC Code</b>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <b>Student Name</b>
                                        </td>
                                      
                                        <td align="left" style="width: 120px">
                                            <b>Course</b>
                                        </td>
                                        <td align="left" style="width: 120px">
                                            <b>Mobile No</b>
                                        </td>
                                        <td align="left" style="width: 40px">
                                            <b>Select</b>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table align="left" style="font-weight: normal">
                                    <tr>
                                        <td align="left" style="width: 40px">
                                            <%#Eval("SNo")%>
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <asp:Label ID="lblOANo" runat="server" Text='<%#Eval("OANO")%>'></asp:Label>
                                            <asp:Label ID="lblSID" runat="server" Visible="false" Text='<%#Eval("SRID")%>'></asp:Label>
                                        </td>
                                        <td align="left" style="width: 120px">
                                            <%#Eval("ENo")%>
                                        </td>
                                        <td align="left" style="width: 100px">
                                            <%#Eval("SubCenterCode")%>
                                        </td>
                                        <td align="left" style="width: 200px">
                                            <%#Eval("StudentName")%>
                                        </td>
                                        
                                        <td align="left" style="width: 120px">
                                            <%#Eval("Course")%>
                                        </td>
                                        <td align="left" style="width: 120px">
                                            <asp:Label ID="lblMNo" runat="server" Text='<%#Eval("MNo")%>'></asp:Label>
                                        </td>
                                        <td align="left" style="width: 40px">
                                            <asp:CheckBox ID="cbSelect" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    </div>
                    
                </asp:Panel>


            </div>

        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
