<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadStudents.aspx.cs"
    Inherits="DDE.Web.UploadStudents" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style>
        .f
        {
            font-family: Kruti Dev 011;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%-- <div align="center" style="padding-top: 50px">
         <asp:TextBox runat="server" ID="tbInput"></asp:TextBox>
        <asp:Button ID="SetInternalMarks" runat="server" Text="Set Internal Marks" OnClick="SetInternalMarks_Click" 
              />
    </div>--%>
   <%--  <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUploadExaminer" runat="server" Text="Upload Examiner" 
             onclick="btnUploadExaminer_Click"  />
    </div>--%>
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnDeleteSLMRecord" runat="server" Text="Delete SLM Record" onclick="btnDeleteSLMRecord_Click" 
               />
    </div>--%>
   <%-- <div>
        <asp:DropDownList ID="ddlistExam" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnPEReport" runat="server" Text="Publish Exam Report" 
            onclick="btnPEReport_Click" /><asp:GridView ID="gvPER"
            runat="server">
        </asp:GridView>
    </div>--%>
    <%-- <div>

   
     <asp:Button ID="btnShowPhoto" runat="server" Text="Show Photo" 
            onclick="btnShowPhoto_Click"  />
   <asp:GridView ID="gvPhoto" AutoGenerateColumns="false" HeaderStyle-CssClass="rpgvheader"
                    RowStyle-CssClass="rpgvrow" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="S.No." DataField="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="PSRID" DataField="PSRID" HeaderStyle-Width="80px"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Photo" DataField="Photo" HtmlEncode="false" HeaderStyle-Width="60px"
                            ItemStyle-HorizontalAlign="Center" />
                       
                    </Columns>
                </asp:GridView>
    </div>--%>
    <%-- <div class="f">
    <asp:textbox ID="tb" runat="server" CssClass="f" Font-Names="Kruti Dev 011"></asp:textbox>
<asp:Button runat="server" Text="Button" onclick="Unnamed2_Click"></asp:Button>
    </div>--%>
    <%-- <div align="center">
        <asp:Button ID="btnInsertCity" runat="server" Text="Insert City" onclick="btnInsertCity_Click" 
             />
    </div>--%>
    <%-- <div align="center">
        <asp:Button ID="btnCorrectExamCentre" runat="server" Text="Correct Exam Centre" onclick="btnCorrectExamCentre_Click" 
             />
    </div>--%>
    <%-- <div align="center">
        <asp:Button ID="btnSetPreSCCode" runat="server" Text="Set Previous SCCode" 
             onclick="btnSetPreSCCode_Click" />
    </div>--%>
    <%--<div align="center">
        <asp:DropDownList ID="ddlistCourse" runat="server">
        </asp:DropDownList>
    </div>--%>
  <%--  <div align="center">
        <asp:Button ID="btnUploadStudents" runat="server" Text="Upload Students" OnClick="btnUploadStudents_Click" />
    </div>--%>
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUploadAData" runat="server" Text="Upload Admission Data" OnClick="btnUploadAData_Click" />
    </div>--%>
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnCheckExamData" runat="server" Text="Check Examination Data" OnClick="btnCheckExamData_Click" />
    </div>--%>
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUploadExamData" runat="server" Text="Upload Examination Data"
            OnClick="btnUploadExamData_Click" />
    </div>--%>
    <%--<div align="center" style="padding-top: 20px">
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>--%>
  <%--  <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUpdateExamData" runat="server" Text="Update Examination Data"
            OnClick="btnUpdateExamData_Click" />
    </div>--%>
    
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnCancelStudents" runat="server" Text="Cancel Students" OnClick="btnCancelStudents_Click" />
    </div>
   --%>
  <%--  <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnABPRollNo" runat="server" Text="Assign BP Roll No's" OnClick="btnABPRollNo_Click" />
    </div>--%>
   
  <%--  <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnAECBP" runat="server" Text="Assign Exam Centres For BP" OnClick="btnAECBP_Click" />
    </div>--%>
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" OnClick="btnSendMail_Click" />
    </div>--%>
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnSetQStatus" runat="server" Text="Set Q Status" OnClick="btnSetQStatus_Click" />
    </div>--%>
   <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="findTotalCopies" runat="server" Text="Find Total Copies" 
            onclick="findTotalCopies_Click"  />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnAssignICardNo" runat="server" Text="Assign I Card No." onclick="btnAssignICardNo_Click" 
              />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUpdateYear" runat="server" Text="Update Year" onclick="btnUpdateYear_Click" 
              />
    </div>
     <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUpdateRecord" runat="server" Text="Update Record" onclick="btnUpdateRecord_Click" 
              />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnAPass" runat="server" Text="Allot Passwords" onclick="btnAPass_Click"  
              />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUploadExamCentre" runat="server" Text="Upload Examination Centre"
            OnClick="btnUploadExamCentre_Click" />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnARollNo" runat="server" Text="Assign Roll No's" OnClick="btnARollNo_Click" />
    </div>--%>
       <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnAssignExamCentres" runat="server" Text="Assign Exam Centres" OnClick="btnAssignExamCentres_Click" />
    </div>
        <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnShiftRollNo" runat="server" Text="Shift Roll Nos" OnClick="btnShiftRollNo_Click"/>
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnSetExam" runat="server" Text="SetExamOnCourseFee" 
             onclick="btnSetExam_Click"  />
    </div>--%>
    <%--<div align="center" style="padding-top: 50px">
        <asp:Button ID="btnSetYear" runat="server" Text="SetYearRecord" onclick="btnSetYear_Click" 
             />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnChangeExam" runat="server" Text="Delete Exam and Fee" onclick="btnChangeExam_Click"  
             />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUpdtaeDuplicateRollNo" runat="server" 
            Text="Update Duplicate Roll No" onclick="btnUpdtaeDuplicateRollNo_Click" 
             />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnSetForExam" runat="server" 
            Text="Set For Exam" onclick="btnSetForExam_Click" 
             />
    </div>--%>
    <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUploadSLM" runat="server" Text="Upload SLM" onclick="btnUploadSLM_Click" 
             />
    </div>--%>
     <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnLock" runat="server" Text="Lock RG Instruments" onclick="btnLock_Click"  
             />
    </div>--%>
     <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnCENo" runat="server" Text="CorrectENo" onclick="btnCENo_Click" 
             />
    </div>--%>

   <%-- <div align="center" style="padding-top: 50px">
      <asp:Button ID="btnCSS" runat="server" Text="Correct SLM Spec" OnClick="btnCSS_Click"  
             />
    </div>--%>

        <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnFindGender" runat="server" Text="Find Gender" OnClick="btnFindGender_Click" 
             />
    </div>--%>
        <%--<div>
<asp:GridView ID="gvStudents" runat="server"></asp:GridView>
        </div>--%>
       <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUpdateSpec" runat="server" Text="Update Spec on SLM Record" OnClick="btnUpdateSpec_Click"  
             />
    </div>--%>

        <%-- <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUpdateDOA" runat="server" Text="Set DOA" OnClick="btnUpdateDOA_Click"  
             />
    </div>--%>

        <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnUpdateCourseID" runat="server" Text="Set CourseID" OnClick="btnUpdateCourseID_Click"   
             />
    </div>
        <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnSetAB" runat="server" Text="Set Absent" OnClick="btnSetAB_Click"   
             />
    </div>
        <div align="center" style="padding-top: 50px">
            <asp:FileUpload ID="fuFile" runat="server" Multiple="Multiple" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload File" OnClick="btnUpload_Click"    
             />
    </div>
        <div align="center" style="padding-top: 50px">
            <asp:TextBox ID="tbSCCode" runat="server"></asp:TextBox>
        <asp:Button ID="btnAloowResult" runat="server" Text="Allow for Result" OnClick="btnAloowResult_Click"    
             />
    </div>
        <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnAllotPassword" runat="server" Text="Allot Password" OnClick="btnAllotPassword_Click"    
             />
    </div>
        <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnCorrectENo" runat="server" Text="Correct ENo" OnClick="btnCorrectENo_Click"  />   
             
    </div>
        <div align="center" style="padding-top: 50px">
        <asp:Button ID="btnAllowOE" runat="server" Text="Allow OE" OnClick="btnAllowOE_Click"   />   
             
    </div>
    <div align="center" style="padding-top: 50px">
          <asp:TextBox ID="tbENo" runat="server"></asp:TextBox><asp:Button ID="btnChangeSpec" runat="server" Text="Change Spec" OnClick="btnChangeSpec_Click"    />             
    </div> 
    <div align="center" style="padding-top: 50px">
         <asp:Button ID="uploadResult" runat="server" Text="Upload Result" OnClick="uploadResult_Click"     />             
    </div> 
        <div align="center" style="padding-top: 50px">
         <asp:Button ID="btnUploadFYResult" runat="server" Text="Upload FY Result" OnClick="btnUploadFYResult_Click"      />             
    </div> 
        <div align="center" style="padding-top: 50px">
         <asp:Button ID="btnDelete" runat="server" Text="Delete Duplicate ENo" OnClick="btnDelete_Click" />             
    </div> 
        <div align="center" style="padding-top: 50px">
         <asp:Button ID="btnCorrectSySession" runat="server" Text="Correct Syllabus Session" OnClick="btnCorrectSySession_Click"  />             
    </div> 
        <div align="center" style="padding-top: 50px">
         <asp:Button ID="btnDeleteDuplicate" runat="server" Text="Delete Duplicates" OnClick="btnDeleteDuplicate_Click"   />             
    </div> 
        <div align="center" style="padding-top: 50px">
         <asp:Button ID="btnUploadAss" runat="server" Text="Upload Assignment" OnClick="btnUploadAss_Click"   />             
    </div>
         <div align="center" style="padding-top: 50px">
         <asp:Button ID="btnUploadOR" runat="server" Text="Upload Online Result" OnClick="btnUploadOR_Click"    />             
    </div>
        <div align="center" style="padding-top: 50px">
         <asp:Button ID="btnUploadPracMarks" runat="server" Text="Upload Practical Marks" OnClick="btnUploadPracMarks_Click"  />             
    </div>
    </form>
</body>
</html>
