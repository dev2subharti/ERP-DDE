<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminHeader.ascx.cs"
    Inherits="DDE.Web.Admin.UserControls.AdminHeader" %>
<div align="center" id="menu">
    <ul class="menu">
        <li><a href="#"><span>Admin</span></a>
            <div>
                <ul>
                    <li><a href="CreateUser.aspx"><span>Create User</span></a> </li>
                    <li><a href="ShowUsers.aspx"><span>Show Users</span></a> </li>
                    <li><a href="ShowMyRoles.aspx"><span>Show My Roles</span></a> </li>
                    <li><a href="ShowMyLog.aspx"><span>Show My Logs</span></a> </li>
                    <li><a href="SendASMail.aspx"><span>Send Mail</span></a> </li>
                    <li><a href="SetStreamForSC.aspx"><span>Allot Streams</span></a> </li>
                    <li><a href="PublishReportSPCWiseGender1.aspx"><span>Publish Gender Report</span></a>
                    </li>
                    <li><a href="ShowOnlineQueries.aspx"><span>Online Queries</span></a> </li>
                    <li><a href="ShowGrievances.aspx"><span>Student Grievances</span></a> </li>
                    <li><a href="GenerateQuestionPaper1.aspx"><span>Set Ques. Paper</span></a> </li>
                    <li><a href="#" class="parent"><span>Send SMS</span></a>
                        <div>
                            <ul>
                                <li><a href="SendSMSToStudents.aspx"><span>To Student</span></a> </li>
                                <li><a href="SentSMSToMNo.aspx"><span>To MNo</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Online Exam</span></a>
                        <div>
                            <ul>
                                <li><a href="CreateQuestion.aspx"><span>Create Question</span></a> </li>
                                <li><a href="UploadQuestions.aspx"><span>Upload Questions</span></a> </li>
                                <li><a href="ShowQuestions.aspx"><span>Show Questions</span></a> </li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li><a href="#"><span>Create/Edit</span></a>
            <div>
                <ul>
                    <li><a href="#" class="parent"><span>Course</span></a>
                        <div>
                            <ul>
                                <li><a href="AddCourse.aspx"><span>Create Course</span></a> </li>
                                <li><a href="ShowCourse.aspx"><span>Show Courses</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Subject</span></a>
                        <div>
                            <ul>
                                <li><a href="AddSubject.aspx"><span>Create Subject</span></a> </li>
                                <li><a href="ShowSubject.aspx"><span>Show Subjects</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Practicals</span></a>
                        <div>
                            <ul>
                                <li><a href="AddPractical.aspx"><span>Create Practical</span></a> </li>
                                <li><a href="ShowPractical.aspx"><span>Show Practicals</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Study Centre</span></a>
                        <div>
                            <ul>
                                <li><a href="AddStudyCentre.aspx"><span>Create Study Centre</span></a> </li>
                                <li><a href="ShowStudyCentres.aspx"><span>Show Study Centres</span></a> </li>
                                <li><a href="PublishPasswordSC.aspx"><span>Passwords</span></a> </li>
                                <li><a href="ShowSCRenewalStatus.aspx"><span>Renewal Status</span></a> </li>
                                <li><a href="SetStreamFee.aspx"><span>Set Stream Fee</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Exam Centre</span></a>
                        <div>
                            <ul>
                                <li><a href="AddExamCentres.aspx"><span>Create Exam Centre</span></a> </li>
                                <li><a href="ShowExamCentres.aspx"><span>Show Exam Centres</span></a> </li>
                                <li><a href="PublishPasswordEC.aspx"><span>Show EC Passwords</span></a> </li>
                                <%-- <li><a href="ShowECLogins.aspx"><span>Show EC Logins</span></a> </li>--%>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Examiners</span></a>
                        <div>
                            <ul>
                                <li><a href="AddExaminer.aspx"><span>Create Examiner</span></a> </li>
                                <li><a href="ShowExaminers.aspx"><span>Show Examiner</span></a> </li>

                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Date Sheet</span></a>
                        <div>
                            <ul>
                                <li><a href="CreateDateSheet.aspx"><span>Create Date Sheet</span></a>
                                <li><a href="ShowDateSheet.aspx"><span>Show Date Sheet</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Question Papers</span></a>
                        <div>
                            <ul>
                                <li><a href="UploadQuestionPapers.aspx"><span>Upload Question papers</span></a></li>
                                <li><a href="SendQuestionPapers.aspx"><span>Send Question papers</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>City</span></a>
                        <div>
                            <ul>
                                <li><a href="AddCity.aspx"><span>Create City</span></a> </li>
                                <li><a href="ShowCityList.aspx"><span>Show City</span></a> </li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li><a href="#"><span>Student Record</span></a>
            <div>
                <ul>
                    <li><a href="DStudentRegistration.aspx"><span>Student Registration</span></a> </li>
                    <li><a href="#" class="parent"><span>Online Applications</span></a>
                        <div>
                            <ul>
                                <li><a href="ShowOnlineApplications.aspx"><span>Check Eligibility</span></a> </li>
                                <li><a href="ShowConfirmedApplications.aspx"><span>Fresh Registration</span></a> </li>
                                <li><a href="ShowOnlineCont.aspx"><span>Continuation</span></a> </li>
                                <li><a href="ShowOnlineREApplications.aspx"><span>Main Exam</span></a> </li>
                                <li><a href="ShowOnlineBPApplications.aspx"><span>Back Paper</span></a> </li>
                                <li><a href="AllotApplications.aspx"><span>Allot Applications</span></a> </li>
                                <li><a href="ShowEligibilityReport.aspx"><span>Show Allotment Report</span></a>
                                <li><a href="CheckEligibilityStatus.aspx"><span>Check Eligibility Status</span></a>
                                </li>
                            </ul>
                        </div>
                    </li>

                    <li><a href="#" class="parent"><span>Show Students</span></a>
                        <div>
                            <ul>
                                <li><a href="ShowDStudent.aspx"><span>ALL</span></a> </li>
                                <li><a href="ShowStudentsSCWise.aspx"><span>SC Code Wise</span></a> </li>
                                <li><a href="ShowStudentsCourseWise.aspx"><span>Course Wise</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="FindStudentByENo.aspx"><span>Search Student</span></a> </li>
                    <li><a href="PublishStudentInfo.aspx"><span>Publish Student Record</span></a></li>
                    <li><a href="PublishICards.aspx"><span>Publish I Cards</span></a></li>
                    <li><a href="SetYearRecord.aspx"><span>Manage Year of Student</span></a> </li>
                    <li><a href="ChangeStudyCentre.aspx"><span>Change Study Centre</span></a> </li>
                    <li><a href="#" class="parent"><span>Set Specialisation</span></a>
                        <div>
                            <ul>
                                <li><a href="SetSpecialisationByList.aspx"><span>By List</span></a> </li>
                                <li><a href="SetSpecialisation.aspx"><span>By Enrollment No</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="TransferCredits.aspx"><span>Transfer Credits</span></a> </li>
                    <li><a href="#" class="parent"><span>Show Defaulters</span></a>
                        <div>
                            <ul>
                                <li><a href="ShowPendingStudents.aspx"><span>Pending</span></a> </li>
                                <li><a href="ShowProvisionalStudents.aspx"><span>Provisional</span></a> </li>
                                <li><a href="ShowCancelStudents.aspx"><span>Canceled</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="ConfirmAdmissionByANo.aspx"><span>Confirm Admission</span></a> </li>
                    <li><a href="UploadStudentPhotos.aspx"><span>Upload Photos</span></a> </li>
                    <li><a href="RecycleStudent.aspx"><span>Student Recycler</span></a> </li>
                </ul>
            </div>
        </li>
        <li><a href="#"><span>Fee</span></a>
            <div>
                <ul>
                    <li><a href="FillFee.aspx"><span>Fill Student Fee</span></a> </li>
                    <li><a href="FillSCFee.aspx"><span>Fill SC Fee</span></a> </li>
                    <li><a href="AutoFillExamForm.aspx"><span>Auto Fill EF</span></a> </li>
                    <li><a href="ReceivePPFee.aspx"><span>Receive Pros. Fee</span></a> </li>
                    <li><a href="IssueProspectus.aspx"><span>Issue Prospectus</span></a> </li>
                    <li><a href="FillMultipleFee.aspx"><span>Fill Multiple Fee</span></a> </li>
                    <%--<li><a href="FillExamFees.aspx"><span>Fill Exam Fee</span></a> </li>
                    <li><a href="FillBPExamFee.aspx"><span>Fill Back Paper Fee</span></a> </li>--%>
                    <li><a href="ShowAdmissionDataReport.aspx"><span>Show Fee Record</span></a>
                        <%--<div>
                            <ul>
                                <li><a href="ShowFeeRecord.aspx"><span>New</span></a> </li>
                                <li><a href="ShowExamFeeRecord.aspx"><span>Old</span></a> </li>
                                <li><a href="ShowPaidFeeBySC.aspx"><span>Online Received</span></a> </li>
                            </ul>
                        </div>--%>
                    </li>
                    <li><a href="ShowPaidFeeByENo.aspx"><span>Show Paid Fee</span></a> </li>
                    <li><a href="CustomiseExamReport.aspx"><span>Form Entry Record</span></a> </li>
                    <%-- <li><a href="SCRefunds.aspx"><span>Show Refunds</span></a> </li>--%>
                    <li><a href="ShowDCDetails.aspx"><span>Show Draft Details</span></a> </li>
                    <%--<li><a href="ShowVerifiedDraft.aspx"><span>Show Verified Draft</span></a> </li>--%>
                    <li><a href="#" class="parent"><span>Fee Instrument</span></a>
                        <div>
                            <ul>
                                <li><a href="CreateInstrument.aspx"><span>Receive</span></a> </li>
                                <li><a href="VerifyDraft.aspx"><span>Verify</span></a> </li>
                                <li><a href="DistributeAmount.aspx"><span>Distribute</span></a> </li>

                                <li><a href="ShowInstruments.aspx"><span>Show Instruments</span></a>
                                    <%--<div>
                                        <ul>
                                            <li><a href="#"><span>Received Instruments</span></a> </li>
                                            <li><a href="#"><span>Verified Instruments</span></a> </li>
                                            <li><a href="#"><span>Distributed Instrument</span></a> </li>
                                        </ul>
                                    </div>--%>
                                </li>
                                <li><a href="ShowAmountDistribution.aspx"><span>Show Distribution</span></a> </li>
                                <li><a href="BlockUnblockInstrument.aspx"><span>Lock/Unlock</span></a> </li>
                                <%-- <li><a href="SetRequiredFee.aspx"><span>Show Req.Fee</span></a> </li>--%>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Show Ver. Letters</span></a>
                        <div>
                            <ul>
                                <li><a href="FindVLByNo.aspx"><span>By No.</span></a> </li>
                                <li><a href="ShowVerificationLetters.aspx"><span>By Date</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Refund</span></a>
                        <div>
                            <ul>
                                <li><a href="RefundByINo.aspx"><span>Generate</span></a> </li>
                                <li><a href="#" class="parent"><span>Show Refund Letter</span></a>
                                    <div>
                                        <ul>
                                            <li><a href="SearchRefundLetterByNo.aspx"><span>By No.</span></a> </li>
                                            <li><a href="ShowRefundLettersByList.aspx"><span>By Date</span></a> </li>
                                        </ul>
                                    </div>


                                </li>
                                <li><a href="LinkRLToInstrument.aspx"><span>Pay Refund</span></a> </li>
                                <li><a href="VerifyRefund.aspx"><span>Verify Refund</span></a> </li>
                                <%-- <li><a href="ShowRefundSCWise.aspx"><span>By SCCode</span></a> </li>--%>
                            </ul>
                        </div>
                    </li>
                    <li><a href="ShowAllFeePaidBySC.aspx"><span>Show Fee Report</span></a> </li>
                </ul>
            </div>
        </li>
        <li><a href="#"><span>SLM</span></a>
            <div>
                <ul>
                    <li><a href="#" class="parent"><span>SLM</span></a>
                        <div>
                            <ul>
                                <li><a href="CreateSLM.aspx"><span>Add New SLM</span></a> </li>
                                <li><a href="ShowSLM.aspx"><span>Show SLM</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Stock</span></a>
                        <div>
                            <ul>
                                <li><a href="EnterNewSLMStock.aspx"><span>Enter New Stock</span></a> </li>
                                <li><a href="ShowSLMStock.aspx"><span>Show Stock</span></a> </li>
                                <li><a href="ShowSLMStockBook.aspx"><span>Show Transactions</span></a> </li>
                                <li><a href="UpdateSLMStock.aspx"><span>Edit Stock</span></a> </li>
                            </ul>
                        </div>
                    </li>

                    <li><a href="#" class="parent"><span>SLM Letter</span></a>
                        <div>
                            <ul>
                                <li><a href="ShowSLMPendingStudents.aspx"><span>Generate</span></a> </li>
                                <li><a href="PublishListsOfSLMLetter.aspx"><span>Student List</span></a> </li>
                                <li><a href="ProcessSLMLetter.aspx"><span>Process</span></a> </li>
                                <li><a href="UpdateDNoOnSLMLetter.aspx"><span>Fill Dok.No.</span></a> </li>
                                <li><a href="ShowProcessedSLMLetters.aspx"><span>Show Processed</span></a> </li>
                                <li><a href="SearchSLMIssueLetter.aspx"><span>Search</span></a> </li>
                                <li><a href="ShowSLMLetters.aspx"><span>Show All Letters</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="ShowSLMRequirements.aspx"><span>Current SLM Req</span></a> </li>
                    <li><a href="#" class="parent"><span>Student</span></a>
                        <div>
                            <ul>
                                <li><a href="SearchSLMIssueStatausByENo.aspx"><span>SI Status</span></a> </li>
                                <li><a href="ClearSIStatus.aspx"><span>Delete SI Record</span></a> </li>
                                <li><a href="ChangeSLMCourse.aspx"><span>Change Student Details</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Sell SLMs</span></a>
                        <div>
                            <ul>
                                <li><a href="SellSLM.aspx"><span>Generate Letter</span></a> </li>
                                <li><a href="SearchSLMSellletterByLNo.aspx"><span>Search Letter</span></a> </li>
                                <li><a href="#"><span>Show All Letters</span></a> </li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li><a href="#"><span>Examination</span></a>
            <div>
                <ul>
                    <li><a href="#" class="parent"><span>Exam Reports</span></a>
                        <div>
                            <ul>
                                <li><a href="ShowRegularExamReport.aspx"><span>Regular Exam Report</span></a></li>
                                <%--<li><a href="ShowReappearExamReport.aspx"><span>Show RA. Ex. Record</span></a></li>--%>
                                <li><a href="ShowBPExamReport.aspx"><span>BP Exam Report</span></a> </li>
                                <li><a href="ShowCompleteExamReport.aspx"><span>Show Comp. Report</span></a> </li>
                                <li><a href="ShowNAECStudents.aspx"><span>EC NS Students</span></a> </li>
                                <li><a href="ShowECPStudents.aspx"><span>EC Students</span></a>
                                <li><a href="ShowExaminationCentreReport.aspx"><span>Exam Centre Report</span></a>

                                </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="FindAdmitCard.aspx"><span>Find Admit Card</span></a> </li>
                    <li><a href="#" class="parent"><span>Change Exam Centre</span></a>
                        <div>
                            <ul>
                                <li><a href="ChangeExamCentreByENo.aspx"><span>By ENo.</span></a> </li>
                                <li><a href="ChangeExamCentreByList.aspx"><span>By List</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="SetCTPapersByEno.aspx"><span>Set CT Papers</span></a> </li>
                    <li><a href="ShowCTStudents.aspx"><span>Show CTM Papers</span></a> </li>
                    <li><a href="ReceiveProjects.aspx"><span>Receive Project</span></a> </li>
                    <li><a href="ShowMyRecProject.aspx"><span>My Rec Project</span></a> </li>
                    <li><a href="FillReceivedCopy.aspx"><span>Receive Ans. Sheet</span></a> </li>
                    <li><a href="ReceiveAssignment.aspx"><span>Receive Assignment</span></a> </li>
                    <li><a href="ShowMyReceivedAS.aspx"><span>My Rec AS</span></a> </li>
                    <li><a href="#" class="parent"><span>Award Sheet</span></a>
                        <div>
                            <ul>
                                <li><a href="PublishAwardSheet.aspx"><span>Theory</span></a> </li>
                                <li><a href="ShowPracticalAS.aspx"><span>Practical</span></a> </li>
                                <li><a href="ShowRASReportByDate.aspx"><span>Show Report By date</span></a> </li>
                                <li><a href="ShowListofAwardSheets.aspx"><span>List of Award Sheet</span></a> </li>
                                <li><a href="ShowASFilledRecord.aspx"><span>AS Upload Report</span></a> </li>
                                <li><a href="UpdateASAllotment.aspx"><span>Change AS Evaluator</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <%--   <li><a href="SetQualifyingStatus.aspx"><span>Set Qualifying Status</span></a> </li>--%>
                    <li><a href="#" class="parent"><span>Fill Marks</span></a>
                        <div>
                            <ul>
                                <li><a href="UploadInternalMarksByENo.aspx"><span>Fill Marks Manually</span></a></li>
                                <li><a href="FillTheoryMarks.aspx"><span>Fill Theory Marks</span></a> </li>
                                <li><a href="FillPracticalMarks.aspx"><span>Fill Practical Marks</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="UploadASMarks.aspx"><span>Upload AS Marks</span></a> </li>
                    <li><a href="UploadPracAS.aspx"><span>Upload Prac Marks</span></a> </li>
                    <li><a href="FillProjectMarks.aspx"><span>Upload Project Marks</span></a> </li>
                    <li><a href="SetTMSCWiseDec12.aspx"><span>Set Dec 12 Data</span></a> </li>
                    <li><a href="FillDec12Marks.aspx"><span>Upload Dec12 Marks</span></a> </li>
                </ul>
            </div>
        </li>
        <li><a href="#"><span>Results</span></a>
            <div>
                <ul>
                    <li><a href="MakeRROnline.aspx"><span>Set Result Status</span></a></li>
                    <li><a href="ShowGracedStudents.aspx"><span>Show Graced Students</span></a></li>
                    <li><a href="DetainStudent.aspx"><span>Detain Student</span></a></li>
                    <li><a href="RemoveDetainedStatus.aspx"><span>Remove Detained</span></a></li>
                    <li><a href="ShowDetainedStudents.aspx"><span>Show Detained Student</span></a></li>
                    <li><a href="#" class="parent"><span>New Degree</span></a>
                        <div>
                            <ul>
                                <li><a href="#"><span>Apply</span></a>
                                     <div>
                                        <ul>
                                            <li><a href="RequestForDegree.aspx"><span>Fill Application</span></a> </li>
                                            <li><a href="ShowDegreeRequests.aspx"><span>Show Applications</span></a></li>
                                        </ul>
                                    </div>

                                </li>
                                <li><a href="ShowDAProofReading.aspx"><span>Proof Reading</span></a></li>                                
                                <li><a href="ShowDANoDues.aspx"><span>No Dues</span></a> </li>                               
                                <li><a href="ShowDAApproved.aspx"><span>Print</span></a> </li>                              
                                <li><a href="ShowDAStatus.aspx"><span>Status</span></a> </li>                              
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Degree</span></a>
                        <div>
                            <ul>
                                <li><a href="RequestForDegree.aspx"><span>Apply</span></a> </li>
                                <li><a href="SearchDegreeApplication.aspx"><span>Search</span></a> </li>
                                <li><a href="#" class="parent"><span>Letter</span></a>
                                    <div>
                                        <ul>
                                            <li><a href="ShowDegreeRequests.aspx"><span>Publish</span></a> </li>
                                            <li><a href="SearchDL.aspx"><span>Search</span></a></li>
                                        </ul>
                                    </div>
                                </li>
                                <li><a href="FillDegreeInfo.aspx"><span>Receive</span></a></li>
                                <li><a href="#"><span>Post</span></a> </li>
                            </ul>
                        </div>
                    </li>
                     <li><a href="ShowProvisionalDegree.aspx"><span>Provisional Degree</span></a>
                        
                    </li>
                    <li><a href="#" class="parent"><span>Migration</span></a>
                        <div>
                            <ul>
                                <li><a href="RequestForMigration.aspx"><span>Apply</span></a> </li>
                                  <li><a href="SeachMigrationApplication.aspx"><span>Search</span></a> </li>
                                <li><a href="#" class="parent"><span>Letter</span></a>
                                    <div>
                                        <ul>
                                            <li><a href="ShowMigrationRequests.aspx"><span>Publish</span></a> </li>
                                            <li><a href="SearchML.aspx"><span>Search</span></a></li>
                                        </ul>
                                    </div>
                                </li>
                                <li><a href="FillMigrationInfo.aspx"><span>Receive</span></a></li>
                                <li><a href="#"><span>Post</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="DetailsOfMarks.aspx"><span>Find Tabular</span></a> </li>
                    <li><a href="ShowCompleteResult.aspx"><span>Complete Report</span></a> </li>
                    <li><a href="SearchAbsentesInExam.aspx"><span>Search Absents</span></a></li>
                    <li><a href="#" class="parent"><span>QR Code</span></a>
                        <div>
                            <ul>
                                <li><a href="GenerateQRCodeForDegree.aspx"><span>Degree</span></a> </li>
                                <li><a href="GenerateQRCodeForCMS.aspx"><span>Cons. MS</span></a> </li>
                                <li><a href="GenerateQRCodeForTS.aspx"><span>TS</span></a> </li>
                            </ul>
                        </div>
                    </li>
                    <li><a href="#" class="parent"><span>Find Marksheet</span></a>
                        <div>
                            <ul>
                                <li><a href="FindMarkSheetByENo.aspx"><span>By Enrollment No.</span></a> </li>
                                <li><a href="ShowMarkSheetSCWise.aspx"><span>By Study Centre</span></a> </li>
                                <li><a href="BlankMarksheet.aspx"><span>Blank Marksheet</span></a> </li>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </li>
        <li><a href="ShowLogs.aspx"><span>Show Logs</span></a> </li>
        <li><a href="ChangePassword.aspx"><span>Change Password</span></a> </li>
        <li>
            <asp:LinkButton ID="lnkbtnAccountAdminSignout" runat="server" OnClick="lnkbtnAccountAdminSignout_Click"
                CausesValidation="false"><span>Sign Out</span></asp:LinkButton>
        </li>
    </ul>
</div>
