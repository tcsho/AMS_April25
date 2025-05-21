<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveAdjustment.aspx.cs" Inherits="LeaveAdjustment" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style-changes td {
            vertical-align: top !important;
        }

        .style-changes textarea {
            resize: vertical;
        }

        /* Panel Header */
        .panel_header {
            text-align: center;
            font-size: 21px;
            font-weight: bold;
            margin-bottom: 20px;
            color: #fff;
        }

        /* Button */
        .button {
            background: linear-gradient(#CCC, #FFF);
            color: #09F;
            padding: 10px 15px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            font-size: 16px;
        }

            .button:hover {
                background: linear-gradient(#CCC,rgba(102,102,102,1));
                color: #09F;
            }

        .container {
            display: flex;
            flex-direction: column;
            gap: 10px;
            width: 100%;
        }

        .row {
            display: flex;
            justify-content: space-between;
        }

        .column {
            flex: -0.5;
            padding: 5px;
        }

            .column:first-child {
                font-weight: bold;
            }

        .RadioButtonWidth label {
            margin-right: 30px;
        }

        input[type="radio"] {
            width: 20px; /* Set the width of the radio button */
            height: 20px; /* Set the height of the radio button */
            vertical-align: middle; /* Align the radio button vertically with the text */
            cursor: pointer; /* Change cursor to pointer when hovering */
            margin-right: 8px; /* Space between radio button and label */
        }

        label {
            font-size: 16px; /* Set a consistent font size for the labels */
            vertical-align: middle; /* Vertically align label with radio button */
            cursor: pointer; /* Make label clickable */
        }

        input[type="radio"]:checked {
            background-color: #007bff; /* Custom color when selected */
            border-color: #007bff; /* Border color when selected */
        }

        .white-background {
            background: white !important;
        }

        .alert {
            padding: 5px;
        }
    </style>
    <script type="text/javascript">  
        $(document).ready(document_Ready);

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
            });

            /*Date Picker*/
            $(function () {
                //$('.datepicker').datepicker({
                //    dateFormat: "mm/dd/yy" 
                //});

                // Get today's date
                var today = new Date();

                // Get the current month (0-based, so 0 is January, 11 is December)
                var currentMonth = today.getMonth();
                var currentYear = today.getFullYear();

                // Calculate the 26th of the last month
                var lastMonth = new Date(currentYear, currentMonth - 1, 26);

                // Calculate the 25th of the current month
                var currentMonthEnd = new Date(currentYear, currentMonth, 25);

                // Initialize the datepicker
                $('.datepicker').datepicker({
                    dateFormat: "dd/mm/yy",
                    minDate: lastMonth,    // Enable from 26th of last month
                    maxDate: currentMonthEnd  // Enable until 25th of current month
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
            <div class="fullrow">
                <div class="inner_wrap">
                    <div class="panel">
                        <div class="panel_head">
                            Leave Adjustment Filter
                        </div>
                        <div class="form-horizontal">
                            <div class="new_event">
                                <div>
                                    <div style="display: flex;">
                                        <span class="LabelWhite"></span>
                                        <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control white-background" AutoPostBack="true" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                            <asp:ListItem Text="All Employees" Value="1" />
                                            <asp:ListItem Text="Single Employee" Value="0" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div id="singleemployee" runat="server">
                                    <div style="display: flex;">
                                        <asp:TextBox ID="txtUser" runat="server" MaxLength="50" CssClass="form-control" placeholder="Type Employee Code"
                                            AutoPostBack="true" OnTextChanged="txtUser_TextChanged" />

                                        <%--<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                            CssClass="btn btn-primary" Text="Search" Width="25%" />--%>
                                    </div>
                                </div>
                                <div>
                                    <asp:RadioButtonList ID="rblLeaveType" RepeatDirection="Horizontal" CssClass="RadioButtonWidth" runat="server" OnSelectedIndexChanged="rblLeaveType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="All" Value="0" />
                                        <asp:ListItem Text="LWP" Value="6070" />
                                        <asp:ListItem Text="CL" Value="6072" />
                                        <asp:ListItem Text="AL" Value="6071" />
                                    </asp:RadioButtonList>
                                </div>
                                <div>
                                    <asp:RadioButtonList ID="rblEmpType" RepeatDirection="Horizontal" CssClass="RadioButtonWidth" runat="server" OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="All" Value="0" />
                                        <asp:ListItem Text="TCS" Value="1" />
                                        <asp:ListItem Text="TSS" Value="2" />
                                    </asp:RadioButtonList>
                                </div>
                                <div>
                                    <asp:RadioButtonList ID="rblStaffType" RepeatDirection="Horizontal" CssClass="RadioButtonWidth" runat="server" OnSelectedIndexChanged="rblStaffType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="All" Value="0" />
                                        <asp:ListItem Text="Domestic Staff" Value="1" />
                                        <asp:ListItem Text="Non Domestic Staff" Value="2" />
                                    </asp:RadioButtonList>
                                </div>
                                <div>
                                    <asp:RadioButtonList ID="rblattended" RepeatDirection="Horizontal" CssClass="RadioButtonWidth" runat="server" OnSelectedIndexChanged="rblattended_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="All" Value="0" />
                                        <asp:ListItem Text="Attended" Value="1" />
                                        <asp:ListItem Text="Not Attended" Value="2" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="outer_wrap">
                <div class="body_content fullrow">
                    <%--Resigned Employee Listing Start--%>
                    <div class="panel" id="div_leaveRequests" runat="server">
                        <div class="panel_header" style="position: relative; display: flex; justify-content: space-between; align-items: center;">
                            <div style="flex: 1; text-align: left;">
                                Leave Adjustment Listing
                            </div>
                            <div style="position: absolute; left: 50%; transform: translateX(-50%);">
                                <asp:Label ID="lblRecordCount" runat="server" CssClass="alert alert-info" />
                            </div>
                            <div style="flex: 1; display: flex; justify-content: flex-end;">
                                <asp:Button ID="btnSubmitSelected" runat="server" Text="Submit" OnClick="btnSubmitSelected_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                        <!--end panel_head-->
                        <div class="panel_body">
                            <!--Paste Grid code here-->
                            <asp:GridView ID="gvLeaveAdjustment" runat="server" AutoGenerateColumns="False"
                                CssClass="table table-bordered" OnRowDataBound="gvLeaveAdjustment_RowDataBound" EmptyDataText="No leave adjustments found.">
                                <Columns>
                                    <asp:BoundField DataField="FinalID" HeaderText="FinalID" SortExpression="FinalID" Visible="False" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfFinalID" runat="server" Value='<%# Eval("FinalID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" />
                                    <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                    <asp:TemplateField HeaderText="CL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalCasual" runat="server" Text='<%# Eval("balCasual") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalAnnual" runat="server" Text='<%# Eval("balAnnual") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" SortExpression="LeaveType" />
                                    <asp:BoundField DataField="LeaveDays" HeaderText="Leave Days" SortExpression="LeaveDays" />
                                    <asp:BoundField DataField="LeaveFrom" SortExpression="LeaveFrom" HeaderText="Leave From" DataFormatString="{0:dddd dd/MM/yyyy}"
                                        HtmlEncode="False">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="leaveto" SortExpression="leaveto" HeaderText="Leave To" DataFormatString="{0:dddd dd/MM/yyyy}"
                                        HtmlEncode="False">
                                        <ItemStyle Width="70px" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="EmpReason" HeaderText="Leave Reason" SortExpression="EmpReason" />--%>
                                    <asp:BoundField DataField="HR_LeaveDays" HeaderText="HR Leave Days" SortExpression="HR_LeaveDays" />
                                    <%--       <asp:TemplateField HeaderText="HR Leave Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select" Value="-1" />
                                                <asp:ListItem Text="Annual Leave" Value="6071" />
                                                <asp:ListItem Text="Casual Leave" Value="6072" />
                                                <asp:ListItem Text="Half Casual Leave" Value="6072" />
                                                <asp:ListItem Text="Remove" Value="9999" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="HR Leave Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Select" Value="-1" />
                                                <asp:ListItem Text="AL" Value="6071" />
                                                <asp:ListItem Text="CL" Value="6072" />
                                                <asp:ListItem Text="Half CL" Value="9000" />
                                                <asp:ListItem Text="Remove" Value="9999" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave From Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="HR_LeaveFrom" runat="server" CssClass="form-control datepicker" MaxLength="10"
                                                placeholder="Date" Text='<%# Bind("HR_LeaveFrom", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="260px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave To Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="HR_LeaveTo" runat="server" CssClass="form-control datepicker" MaxLength="10"
                                                placeholder="Date" Text='<%# Bind("HR_LeaveTo", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="260px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HR Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                MaxLength="500"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="400px" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSubmitByHr" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CommandName="submitbyhr" Text="Submit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <!-- Replace Button with Checkbox -->
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                        </div>
                        <!--end panel_footer-->
                    </div>
                    <%--Resigned Employee Listing End--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>