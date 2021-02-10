<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPages/Admin.Master" AutoEventWireup="true"
    CodeBehind="SendQuestionPapers.aspx.cs" Inherits="DDE.Web.Admin.SendQuestionPapers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Panel ID="pnlData" runat="server" Visible="false">
        <div align="center" class="heading">
            Send Question Papers to Examination Centres
        </div>
        <div>
             <div align="center" class="text" style="padding-top: 20px">
                <table class="tableStyle2" cellspacing="10px">
                    <tr>
                        <td>
                            <b>Date</b>
                        </td>
                        <td align="left" colspan="5">
                            <table>
                                <tr>
                                    <td valign="middle">
                                        <asp:DropDownList ID="ddlistDay" runat="server">
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistMonth" runat="server">
                                            <asp:ListItem Value="01">JANUARY</asp:ListItem>
                                            <asp:ListItem Value="02">FEBRUARY</asp:ListItem>
                                            <asp:ListItem Value="03">MARCH</asp:ListItem>
                                            <asp:ListItem Value="04">APRIL</asp:ListItem>
                                            <asp:ListItem Value="05">MAY</asp:ListItem>
                                            <asp:ListItem Value="06">JUNE</asp:ListItem>
                                            <asp:ListItem Value="07">JULY</asp:ListItem>
                                            <asp:ListItem Value="08">AUGUST</asp:ListItem>
                                            <asp:ListItem Value="09">SEPTEMBER</asp:ListItem>
                                            <asp:ListItem Value="10">OCTOBER</asp:ListItem>
                                            <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                            <asp:ListItem Value="12">DECEMBER</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistYear" runat="server">
                                            <asp:ListItem>2009</asp:ListItem>
                                            <asp:ListItem>2010</asp:ListItem>
                                            <asp:ListItem>2011</asp:ListItem>
                                            <asp:ListItem>2012</asp:ListItem>
                                            <asp:ListItem>2013</asp:ListItem>
                                            <asp:ListItem>2014</asp:ListItem>
                                            <asp:ListItem>2015</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem><asp:ListItem>2021</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Time</b>
                        </td>
                        <td style="font-weight: normal">
                            From
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlistHourFrom" runat="server">
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistMinutesFrom" runat="server">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistSectionFrom" runat="server">
                                            <asp:ListItem Value="1">AM</asp:ListItem>
                                            <asp:ListItem Value="2">PM</asp:ListItem>
                                            <asp:ListItem Value="3">NOON</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="font-weight: normal">
                            To
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlistHourTo" runat="server">
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistMinutesTo" runat="server">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                            <asp:ListItem>32</asp:ListItem>
                                            <asp:ListItem>33</asp:ListItem>
                                            <asp:ListItem>34</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>36</asp:ListItem>
                                            <asp:ListItem>37</asp:ListItem>
                                            <asp:ListItem>38</asp:ListItem>
                                            <asp:ListItem>39</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>41</asp:ListItem>
                                            <asp:ListItem>42</asp:ListItem>
                                            <asp:ListItem>43</asp:ListItem>
                                            <asp:ListItem>44</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>46</asp:ListItem>
                                            <asp:ListItem>47</asp:ListItem>
                                            <asp:ListItem>48</asp:ListItem>
                                            <asp:ListItem>49</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>51</asp:ListItem>
                                            <asp:ListItem>52</asp:ListItem>
                                            <asp:ListItem>53</asp:ListItem>
                                            <asp:ListItem>54</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem>56</asp:ListItem>
                                            <asp:ListItem>57</asp:ListItem>
                                            <asp:ListItem>58</asp:ListItem>
                                            <asp:ListItem>59</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlistSectionTo" runat="server">
                                            <asp:ListItem Value="1">AM</asp:ListItem>
                                            <asp:ListItem Value="2">PM</asp:ListItem>
                                            <asp:ListItem Value="3">NOON</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding:10px">
                <asp:Button ID="btnFind" runat="server" Text="Search Papers" Style="height: 26px"
                    OnClick="btnFind_Click" />
            </div>
            <div align="center">
                <asp:DataList ID="dtlistShowDS" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS">
                    <HeaderTemplate>
                        <table align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 100px">
                                    <b>Date</b>
                                </td>
                                <td align="center" style="width: 160px">
                                    <b>Time</b>
                                </td>
                                <td align="center" style="width: 120px">
                                    <b>Paper Code</b>
                                </td>
                                <td align="center" style="width: 300px">
                                    <b>Title of Paper</b>
                                </td>
                                <td align="center" style="width: 70px">
                                    <b>Year</b>
                                </td>
                                <td align="center" style="width: 200px">
                                    <b>Question Paper</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 80px">
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:DataList ID="dtlistShowSubjects" ItemStyle-Wrap="true" runat="server">
                                        <ItemTemplate>
                                            <table align="left" cellspacing="0px" cellpadding="0px">
                                                <tr>
                                                    <td align="center" class="border_rb" style="width: 160px">
                                                        <%#Eval("Time")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 100px">
                                                        <asp:Label ID="lblPaperCode" runat="server" Text='<%#Eval("PaperCode")%>'></asp:Label>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 300px">
                                                        <%#Eval("SubjectName")%>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 70px">
                                                        <%#Eval("Year")%>
                                                    </td>
                                                    <td align="left" class="border_rb" style="width: 200px">
                                                        <a href="<%#Eval("QPFileURL")%>">
                                                            <%#Eval("QPFileName")%></a>
                                                    </td>
                                                    <td align="center" class="border_rb" style="width: 50px">
                                                        <asp:CheckBox ID="cbSelect" runat="server" Checked="true" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div style="padding-top: 10px">
                <asp:Button ID="btnSearchEC" runat="server" Visible="false" Text="Search Examination Centres"
                    OnClick="btnSearchEC_Click" />
            </div>
            <div align="right" style="width: 1070px">
                <table cellspacing="10px">
                    <tr>
                        <td>
                            <asp:Button ID="btnSelectAll" runat="server" Text="Select All" Visible="false" OnClick="btnSelectAll_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnClearAll" runat="server" Text="Clear All" Visible="false" OnClick="btnClearAll_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center" style="padding-top: 10px">
                <asp:DataList ID="dtlistEC" runat="server" CellPadding="0" CellSpacing="0" HeaderStyle-CssClass="dtlistheaderDS"
                    ItemStyle-CssClass="dtlistItemDS">
                    <HeaderTemplate>
                        <table align="left" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" style="width: 50px">
                                    <b>S.No.</b>
                                </td>
                                <td align="center" style="width: 100px">
                                    <b>EC Code</b>
                                </td>
                                <td align="center" style="width: 300px">
                                    <b>Exam Centre Name</b>
                                </td>
                                <td align="center" style="width: 350px">
                                    <b>Email ID</b>
                                </td>
                                <td align="center" style="width: 200px">
                                    <b>Question Papers</b>
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="margin: 0px" cellspacing="0px" cellpadding="0px">
                            <tr>
                                <td align="center" class="border_lbr" style="width: 50px">
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                </td>
                                <td align="center" class="border_rb" style="width: 100px">
                                    <asp:Label ID="lblECCode" runat="server" Text='<%#Eval("ECCode")%>'></asp:Label>
                                </td>
                                <td align="left" class="border_rb" style="width: 300px">
                                    <asp:Label ID="lblECName" runat="server" Text='<%#Eval("ECName")%>'></asp:Label>
                                </td>
                                <td align="left" class="border_rb" style="width: 300px">
                                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                </td>
                                <td align="left" class="border_rb" style="width: 200px">
                                    <asp:Label ID="lblQP" runat="server" Text='<%#Eval("QP")%>'></asp:Label>
                                </td>
                                <td align="center" class="border_rb" style="width: 30px">
                                    <asp:CheckBox ID="cbESelect" runat="server" Checked="true" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div style="padding-top: 10px">
            <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" Visible="false" OnClick="btnSendMail_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMSG" runat="server" CssClass="msgpnl" Visible="false">
        <table class="tableStyle2">
            <tr>
                <td style="padding: 30px">
                    <asp:Label ID="lblMSG" CssClass="msg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <div align="center" style="padding-top: 10px">
            <asp:Button ID="btnOK" runat="server" Visible="false" Text="OK" Width="50px" OnClick="btnOK_Click" />
        </div>
    </asp:Panel>
</asp:Content>
