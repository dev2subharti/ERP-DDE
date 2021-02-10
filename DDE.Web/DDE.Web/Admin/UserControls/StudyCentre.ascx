<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudyCentre.ascx.cs"
    Inherits="DDE.Web.Admin.UserControls.StudyCentre" %>
<div align="center" id="menu">
    <ul class="menu">
        <li><a href="ShowDStudent.aspx"><span>Show Students</span></a>
            
        </li>
       <%-- <li><a href="SendFeeBySC.aspx"><span>Send Fee</span></a> </li>--%>
       <li><a href="#"><span>Download Admit Cards</span></a> </li>
       <%-- <li><a href="ChalanForm.aspx"><span>Chalan Form</span></a> </li>--%>
         <li><a href="#"><span>Upload Marks</span></a> 
         
         <div>
                <ul>
                    <li><a href="FillIAAWMarksBySC.aspx"><span>IA/AW Marks</span></a> </li>
                    <li><a href="FillPracticalMarksBySC.aspx"><span>Practical Marks</span></a> </li>
                    
                </ul>
            </div>
         
         </li>
        <li><a href="ChangePassword.aspx"><span>Change Password</span></a> </li>
        <li>
            <asp:LinkButton ID="lnkbtnAccountAdminSignout" runat="server" OnClick="lnkbtnAccountAdminSignout_Click"
                CausesValidation="false"><span>Sign Out</span></asp:LinkButton>
        </li>
    </ul>
</div>
