<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPages/Admin.Master"
    CodeBehind="ShowASReceivedByEmployee.aspx.cs" Inherits="DDE.Web.Admin.ShowASReceivedByEmployee" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <div align="center">
        <asp:Panel ID="pnlData" runat="server" Visible="false">
            <div align="center" class="heading">
                Show Total Ans. Sheet Received Employee Wise
            </div>
            <div style="padding-top: 10px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        
                        <td>
                            <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                       
                        <td>
                            <asp:Label ID="lblPeriod" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
           
            <div style="padding-top: 10px">
                <div>
                    <asp:GridView ID="gvAwarsSheet" BackColor="White" AutoGenerateColumns="false" HeaderStyle-CssClass="rgvheader"
                        RowStyle-CssClass="rgvrow" Width="1000px" runat="server">
                        <Columns>
                            <asp:BoundField HeaderText="S.No." DataField="SNo" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Enrollment No." DataField="EnrollmentNo" HeaderStyle-Width="80px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Roll No." DataField="RollNo" HeaderStyle-Width="80px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Paper Code" DataField="PaperCode" HeaderStyle-Width="80px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Subject Name" DataField="Subject" HeaderStyle-Width="300px"
                                ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="rcourse" />
                            <asp:BoundField HeaderText="Course" DataField="Course" HeaderStyle-Width="250px"
                                ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="rcourse" />
                                 <asp:BoundField HeaderText="Time of Receiving" DataField="TOR" HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="rcourse" />
                        </Columns>
                    </asp:GridView>
                </div>
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
