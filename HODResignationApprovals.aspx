<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HODResignationApprovals.aspx.cs" Inherits="HODResignationApprovals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <%--    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <script type="text/javascript">  
        $(document).ready(document_Ready);

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
            });

            /*Date Picker*/
            $(function () {
                $('.datepicker').datepicker({
                    dateFormat: "mm/dd/yy",
                    minDate: 0
                });
                $("#anim").change(function () {
                    $(".datepicker").datepicker("option", "showAnim", $(this).val());
                });
            });
        }

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            document_Ready();
        });
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="outer_wrap">
                <%-- ****************************** drop down section  *****************************--%>
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="controls round_corner">
                            <div class="show_hide_btn btn round_corner">
                                <p>
                                    Show/Hide Approved
                                </p>
                            </div>
                            <%--<asp:DropDownList ID="ddlLeavesStatus" runat="server" CssClass="dropdownlist" Width="130px"
                                                    AutoPostBack="True" 
                        onselectedindexchanged="ddlLeavesStatus_SelectedIndexChanged" >

                        <asp:ListItem Text="">
                        </asp:ListItem>
                                                </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdownlist" Width="130px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEmp" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%-- <div>
                                <asp:Button runat="server" ID="autoApproveBtn" class="round_corner btn" Text="Auto Approve"
                                                OnClick="autoApproveBtn_Click" />
                            </div>--%>
                        </div>
                        <!--end controls-->
                        <div class="clear">
                        </div>
                    </div>
                    <!--end inner wrap-->
                </div>
                <!--end fullrow-->
                <div class="body_content fullrow">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="upResignations" runat="server">
                            <%--Resignation section--%>
                            <ContentTemplate>
                                <%-- ****************************** Resignation UnApproved records section  *****************************--%>
                                <div class="panel" id="div_ResignationsUnApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Resignation Approval
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--Paste Grid code here-->
                                        <asp:GridView ID="gvResignationUnApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center"
                                            OnRowDataBound="gvResignationUnApproved_RowDataBound"
                                            OnRowCommand="gvResignationUnApproved_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>

                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="8px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:MM/dd/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>

                                                <%--<asp:TemplateField HeaderText="Last Working Date">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="text_date" runat="server" Width="140px" CssClass="datepicker"></asp:TextBox>

                                                    </EditItemTemplate>
                                                    <ItemStyle Width="15px" />
                                                </asp:TemplateField>--%>

                                                <%--  <asp:TemplateField HeaderText="Last Working Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="LastWorkingDate" runat="server" CssClass="datepicker lastdate" AutoPostBack="true" Text='<%# Bind("LastWorkingDate", "{0:dd/MM/yyyy}") %>' OnTextChanged="LastWorkingDate_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>--%>

                                                <asp:BoundField DataField="LastWorkingDate" HeaderText="Last Working Date" DataFormatString="{0:MM/dd/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Notice Period">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="NoticePeriod" runat="server" Enabled="false" CssClass="textbox" Text='<%# Bind("NoticePeriod") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="NoticePeriod" HeaderText="Notice Period">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>--%>

                                                <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Comments" SortExpression="Comments" HeaderText="Comments">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastWorkingDateRemarks" SortExpression="LastWorkingDateRemarks" HeaderText="Last Working Date Remarks">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RegionId" SortExpression="RegionId" HeaderText="RegionId">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CenterId" SortExpression="CenterId" HeaderText="CenterId">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="HOD Propose Last Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="hodProposeLastDate" Enabled="false" Text='<%# Bind("LastWorkingDate", "{0:MM/dd/yyyy}") %>' placeholder="Select Date.." AutoPostBack="true" runat="server" CssClass="datepicker" OnTextChanged="LastWorkingDate_TextChanged"
                                                            MaxLength="250" Width="98%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Approve">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlApproveResignation" CssClass="dropdownlist" runat="server"
                                                            OnSelectedIndexChanged="ddlApproveResignation_SelectedIndexChanged" AutoPostBack="true"
                                                            Width="100px" Height="40px" BackColor="Chartreuse">
                                                            <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Approve" Value="true"></asp:ListItem>
                                                            <asp:ListItem Text="Reject" Value="false"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" Width="98%" BackColor="Chartreuse"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkResignations" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnResignationCheck" runat="server" CausesValidation="False"
                                                            CommandName="toggleCheck">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BackColor="Chartreuse" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkResignations" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->

                                    <div class="panel_footer">

                                        <div class="save_btn">
                                            <asp:Button runat="server" ID="btnResignationSave" class="round_corner btn" Text="Save"
                                                OnClick="btnResignationSave_Click" />
                                        </div>
                                    </div>
                                    <!--end panel_footer-->

                                </div>
                                <!--end panel-->

                                <%-- ****************************** Termination UnApproved records section for supervisors  *****************************--%>
                                <div class="panel" id="div_TerminationsUnApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Termination Approval (Supervisor)
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--Paste Grid code here-->
                                        <asp:GridView ID="gvTerminationUnApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center"
                                            OnRowDataBound="gvTerminationUnApproved_RowDataBound"
                                            OnRowCommand="gvTerminationUnApproved_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>

                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="8px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:MM/dd/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>

                                                <%--<asp:TemplateField HeaderText="Last Working Date">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="text_date" runat="server" Width="140px" CssClass="datepicker"></asp:TextBox>

                                                    </EditItemTemplate>
                                                    <ItemStyle Width="15px" />
                                                </asp:TemplateField>--%>

                                                <%--  <asp:TemplateField HeaderText="Last Working Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="LastWorkingDate" runat="server" CssClass="datepicker lastdate" AutoPostBack="true" Text='<%# Bind("LastWorkingDate", "{0:dd/MM/yyyy}") %>' OnTextChanged="LastWorkingDate_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>--%>

                                                <asp:BoundField DataField="LastWorkingDate" HeaderText="Last Working Date" DataFormatString="{0:MM/dd/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Notice Period">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="NoticePeriod" runat="server" Enabled="false" CssClass="textbox" Text='<%# Bind("NoticePeriod") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="NoticePeriod" HeaderText="Notice Period">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>--%>

                                                <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Comments" SortExpression="Comments" HeaderText="Comments">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastWorkingDateRemarks" SortExpression="LastWorkingDateRemarks" HeaderText="Last Working Date Remarks">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="RegionId" SortExpression="RegionId" HeaderText="RegionId">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CenterId" SortExpression="CenterId" HeaderText="CenterId">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="HOD Supervisor Approve">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlApproveTermination" CssClass="dropdownlist" runat="server"
                                                            OnSelectedIndexChanged="ddlApproveTermination_SelectedIndexChanged" AutoPostBack="true"
                                                            Width="45px">
                                                            <asp:ListItem Enabled="true" Text="No" Value="False"></asp:ListItem>
                                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Supervisor Propose Last Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="hodSupervisorApprovedDate" Enabled="false" Text='<%# Bind("LastWorkingDate", "{0:MM/dd/yyyy}") %>' placeholder="Select Date.." AutoPostBack="true" runat="server" CssClass="datepicker" OnTextChanged="TerminationLastWorkingDate_TextChanged"
                                                            MaxLength="250" Width="98%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Supervisor Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" Width="98%" BackColor="Chartreuse"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkTerminations" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnResignationCheck" runat="server" CausesValidation="False"
                                                            CommandName="toggleCheck">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" BackColor="Chartreuse" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkTerminations" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->

                                    <div class="panel_footer">

                                        <div class="save_btn">
                                            <asp:Button runat="server" ID="btnTerminationSave" class="round_corner btn" Text="Save"
                                                OnClick="btnTerminationSave_Click" />
                                        </div>
                                    </div>
                                    <!--end panel_footer-->

                                </div>
                                <!--end panel-->

                                <%-- ****************************** Resignation Approved records section  *****************************--%>
                                <div class="panel approved" id="div_resignationApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Resignation [Approved]
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--paste grid code here-->
                                        <asp:GridView ID="gvResignationApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center"
                                            AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="8px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Email" Visible="false" SortExpression="Email" HeaderText="Email">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Category" SortExpression="Category" HeaderText="Category">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SubmissionDate" SortExpression="SubmissionDate" HeaderText="Submission Date"
                                                    DataFormatString="{0:dddd dd/MM/yyyy}" HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastWorkingDate" SortExpression="LastWorkingDate" HeaderText="Last Working Date"
                                                    DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NoticePeriod" SortExpression="NoticePeriod" HeaderText="Notice Period">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Comments" SortExpression="Comments" HeaderText="Comments">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODApproval" SortExpression="HODApproval" HeaderText="HOD Approval">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                </div>
                                <!--end panel-->

                                <%-- ****************************** Termination Approved records section  *****************************--%>
                                <div class="panel approved" id="div_terminationsApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Termination [Approved]
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--paste grid code here-->
                                        <asp:GridView ID="gvTerminationApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center"
                                            AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="8px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Email" Visible="false" SortExpression="Email" HeaderText="Email">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Category" SortExpression="Category" HeaderText="Category">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SubmissionDate" SortExpression="SubmissionDate" HeaderText="Submission Date"
                                                    DataFormatString="{0:dddd dd/MM/yyyy}" HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LastWorkingDate" SortExpression="LastWorkingDate" HeaderText="Last Working Date"
                                                    DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NoticePeriod" SortExpression="NoticePeriod" HeaderText="Notice Period">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Comments" SortExpression="Comments" HeaderText="Comments">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODSupervisorApproval" SortExpression="HODSupervisorApproval" HeaderText="HOD Supervisor Approval">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                </div>
                                <!--end panel-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="clear">
                        </div>
                    </div>
                    <!--end inner_wrap-->
                </div>
                <!--end body_content-->
            </div>
            <!--end outer_wrap-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>