<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowGrievances.aspx.cs" MasterPageFile="~/Admin/MasterPages/Admin.Master" Inherits="DDE.Web.Admin.ShowGrievances" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
     <script type="text/javascript">
        window.onload = function () {
            var objDiv = document.getElementById("message");
            objDiv.scrollTop = objDiv.scrollHeight;
        }
    </script>
      <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.5;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 300px;
        }
    </style>
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div>
            <div align="center" class="heading" style="padding-bottom: 20px">
                Show Student Grievances
            </div>
            <div align="center" class="text" style="padding-top: 20px; padding-bottom: 0px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                           <asp:CheckBox ID="cbNewMsg" Checked="true" AutoPostBack="true" runat="server" Text="New Message" OnCheckedChanged="cbNewMsg_CheckedChanged" />
                        </td>
                        
                     
                    </tr>
                </table>
            </div>
            <div align="center" class="text" style="padding-top: 10px; padding-bottom: 20px">
                <table cellspacing="10px" class="tableStyle2">
                    <tr>
                        <td align="left">
                            <b>Status</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistStatus" runat="server" Width="150px" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlistStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">OPEN</asp:ListItem>
                                <asp:ListItem Value="1">CLOSED</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td align="left">
                            <b>Subject</b>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlistGC" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlistGC_SelectedIndexChanged"
                               >
                              
                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding-bottom: 20px">
                <asp:Button ID="btnFind" runat="server" Text="Search" Style="height: 26px" Width="100px"
                    OnClick="btnFind_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowQueries" runat="server" Visible="false" CssClass="dtlist"
                    HeaderStyle-CssClass="dtlistheader" ItemStyle-CssClass="dtlistItem" OnItemCommand="dtlistShowQueries_ItemCommand">
                    <HeaderTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>Grievance ID</b>
                                </td>
                                 <td align="left" style="width: 150px">
                                    <b>Subject</b>
                                </td>
                                <td align="left" style="width: 120px">
                                    <b>ENo</b>
                                </td>
                                <td align="left" style="width: 200px">
                                    <b>Name</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Registered No</b>
                                </td>
                                <td align="left" style="width: 150px">
                                    <b>Closed On</b>
                                </td>

                                <td align="left" style="width: 120px">
                                    <b>New Message</b>
                                </td>
                               

                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table align="left">
                            <tr>
                                <td align="left" style="width: 40px">
                                    <asp:Label ID="lblSNo" runat="server" Text=' <%#Eval("SNo")%>'></asp:Label>
                                  
                                     <asp:Label ID="lblSRID" runat="server" Text='<%#Eval("SRID")%>' Visible="false"></asp:Label>
                                </td>
                                <td align="left" style="width: 120px">
                                   <asp:Label ID="lblGID" runat="server" Text='<%#Eval("GID")%>' ></asp:Label>
                                </td>
                                 <td align="left" style="width: 150px">
                                    <asp:Label ID="lblSubject" runat="server" Text='<%#Eval("Subject")%>'></asp:Label>
                                </td>
                                 <td align="left" style="width: 120px">
                                    <asp:Label ID="lblENo" runat="server" Text=' <%#Eval("ENo")%>'></asp:Label>
                                </td>
                                <td align="left" style="width: 200px">
                                    <%#Eval("Name")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("ROn")%>
                                </td>
                                <td align="left" style="width: 150px">
                                    <%#Eval("COn")%>
                                </td>
                                <td align="left" style="width: 120px">
                                    <%#Eval("NewMsg")%>
                                </td>
                                <td align="center" style="width: 30px">
                                    <asp:ImageButton ID="imgbtnShow" ImageUrl="~/Admin/images/show-icon.png" CommandName="Show" CommandArgument='<%#Eval("GID")%>' Width="20px" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div align="left">
                <asp:Button ID="btnpopup" runat="server" Text="" />
            </div>
        <div>
             <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="btnpopup" CancelControlID="imgbtnClose" PopupControlID="pnlGT" runat="server" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
            
            <asp:Panel ID="pnlGT" runat="server" CssClass="pnldiv1" Visible="false">
                 <div align="right">

                            <asp:ImageButton ID="imgbtnClose" runat="server" ImageUrl="images/delete.png" Width="30px" />
                        </div>
        <table class="pnldiv1">
            <tr>
                <td valign="top">
                    <div>
                        <table cellspacing="20px" class="pnldiv1" style="margin: 0px">
                        <tbody align="left">
                             <tr>
                                <td>
                                    <b>Subject</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblSubject" runat="server" Text=""></asp:Label>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Enrollment No</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblENo" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblSRID" runat="server" Visible="false" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Grievance ID</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblGID" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <b>Status</b>
                                </td>
                                <td>
                                     <asp:RadioButtonList ID="rblStatus" CellSpacing="10" AutoPostBack="true" RepeatDirection="Vertical" Width="100px" runat="server" OnSelectedIndexChanged="rblStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">OPEN</asp:ListItem>
                            <asp:ListItem Value="1">CLOSE</asp:ListItem>
                        </asp:RadioButtonList>
                                </td>
                            </tr>
                        </tbody>

                    </table>
                    </div>
                  

                </td>
                <td valign="top">

                    <div id="message" class="pnldiv" style="height: 300px; overflow: scroll; margin: 0px; padding-left: 0px; padding-right: 0px">
                        <div>
                            <asp:DataList ID="dtlist" runat="server">
                                <ItemTemplate>
                                    <table cellspacing="0px" width="500px">
                                        <tr>
                                            <td align="left" style="width: 20px" valign="top">
                                                <asp:Image ID="imgLeft" runat="server" ImageUrl="images/left_send.png" Height="20px" />
                                                <asp:Label ID="lblTT" runat="server" Visible="false" Text='<%#Eval("TT")%>'></asp:Label>
                                            </td>
                                            <td align="left" style="width: 220px">
                                                <asp:Label ID="lblReply" runat="server" Width="200px" CssClass="msgReply" Text='<%#Eval("Query")%>'></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td align="right" style="width: 220px">&nbsp;
                                                                    <asp:Label ID="lblQuery" runat="server" Width="200px" CssClass="msgQuery" Text='<%#Eval("Reply")%>'></asp:Label>

                                            </td>
                                            <td align="left" style="width: 20px" valign="top">
                                                <asp:Image ID="imgRight" runat="server" ImageUrl="images/right_send.png" Height="20px" />

                                            </td>

                                        </tr>
                                    </table>
                                </ItemTemplate>

                            </asp:DataList>
                        </div>


                    </div>


                    <div>
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="tbQuery" TextMode="MultiLine" Width="430px" Height="60px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnSubmit" ImageUrl="images/send.png" Height="40px" Width="40px" runat="server" OnClick="imgbtnSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
        </div>
    </asp:Panel>
    
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table align="center" class="tableStyle2">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>
