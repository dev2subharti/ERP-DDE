<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SentSMSToMNo.aspx.cs" MasterPageFile="~/Admin/MasterPages/Admin.Master" Inherits="DDE.Web.Admin.SentSMSToMNo" %>

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
        <div align="center" style="padding: 0px">
            <div align="center" class="heading" style="padding-bottom: 20px">
                Send SMS to Any Contact No
            </div>


            <div class="pnldiv1" style="width: 900px">
                <div align="center">
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
                    <div>
                        <table class="pnldiv1" cellspacing="10px">
                            <tr>
                                <td valign="top">
                                    <b>Mobile No's</b>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="tbContactNo" runat="server" Width="400px" Height="80px" TextMode="MultiLine"></asp:TextBox>

                                    <br />
                                    (If multiple MNo then seperated them by ','  )<br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbContactNo" ErrorMessage="Please Fill"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>

                                <td align="left" valign="top">
                                    <b>Message *</b>
                                </td>
                                <td align="left">
                                     <asp:TextBox ID="tbMSG"  runat="server" onkeyup="Count(this.form.tbMSG,this.form.countdown,160)" Width="400px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                 <asp:TextBox ID="tbMSGH" Visible="false"  runat="server" onkeyup="CountH(this.form.tbMSGH,this.form.countdown,160)" Width="400px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbMSG" ErrorMessage="Please Fill"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbMSGH" ErrorMessage="Please Fill"></asp:RequiredFieldValidator>
                                <br />
                                <b>Remaining Characters :
                                    <asp:Label ID="lblDisplay" runat="server" Text="160"></asp:Label>
                                    <asp:Label ID="lblDisplayH" Visible="false" runat="server" Text="160"></asp:Label>
                                </b>
                                </td>

                            </tr>
                           
                        </table>
                    </div>
                    <div style="padding-top:10px">
                        
                                    <asp:Button ID="btnSendSMS" CssClass="btn" Width="150px" Height="25px" runat="server" Text="Send SMS" OnClick="btnSendSMS_Click" />
                              
                    </div>
                </div>

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