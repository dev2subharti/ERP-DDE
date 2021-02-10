<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master" CodeBehind="UploadStudentPhotos.aspx.cs" Inherits="DDE.Web.Admin.UploadStudentPhotos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div class="heading">
               Upload New / Change Old Student Photos
            </div>
            
            <div style="padding-top: 20px; padding-bottom: 20px">
               
                    <table cellspacing="10px" class="tableStyle2">
                        <tr>
                           <%-- <td>
                                <b>Batch</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlistBatch" runat="server" AutoPostBack="true"
                                    onselectedindexchanged="ddlistBatch_SelectedIndexChanged">
                              
                                </asp:DropDownList>
                               
                            </td>--%>
                            <td>
                            <asp:FileUpload ID="fupSPhoto" runat="server"  Multiple="Multiple" />
                            </td>
                           
                         
                        </tr>
                    </table>
               
            </div>
            
            <div align="center" style="padding-top:10px">
            <asp:Button ID="btnUploadSPhotos" runat="server" Text="Upload" 
                    onclick="btnUploadSPhotos_Click" />
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

</asp:Content>
